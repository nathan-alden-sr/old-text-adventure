using System;
using System.Collections.Generic;
using System.Linq;

using Junior.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;
using TextAdventure.WindowsGame.Extensions;
using TextAdventure.WindowsGame.Managers;
using TextAdventure.WindowsGame.RendererStates;
using TextAdventure.WindowsGame.Windows;

using Color = Microsoft.Xna.Framework.Color;

namespace TextAdventure.WindowsGame.Renderers
{
	public class MessageRenderer : TexturedWindowRenderer
	{
		private readonly MessageAnswerRenderer _messageAnswerRenderer;
		private readonly IMessageRendererState _state;
		private MessageAnswerSelectionManager _answerSelectionManager;
		private MessageFormatter _formatter;
		private bool _windowRectangleSet;

		public MessageRenderer(IMessageRendererState state)
			: base(textureContent => textureContent.Windows.InnerBevel1)
		{
			state.ThrowIfNull("state");

			_state = state;
			_messageAnswerRenderer = new MessageAnswerRenderer(textureContent => textureContent.Windows.Glow1);
		}

		protected override void BeforeRender(IRendererParameters parameters)
		{
			parameters.ThrowIfNull("parameters");

			base.BeforeRender(parameters);

			if (!_windowRectangleSet)
			{
				var messageWithBackgroundColor = _state.Message as IMessageWithBackgroundColor;

				if (messageWithBackgroundColor != null)
				{
					BackgroundColor = messageWithBackgroundColor.BackgroundColor.ToXnaColor();
				}

				WindowTexture windowWindowTexture = parameters.TextureContent.Windows.InnerBevel1;
				WindowTexture selectedAnswerWindowTexture = parameters.TextureContent.Windows.Glow1;
				SpriteFont font = parameters.FontContent.Calibri12Pt;
				Rectangle destinationRectangle = Constants.GameWindow.DestinationRectangle;
				float maximumLineWidth = destinationRectangle.Width * Constants.MessageRenderer.MaximumLineWidthAsPercentageOfGameWindowDestinationRectangle;
				float maximumClientHeight =
					destinationRectangle.Height -
					(destinationRectangle.Center.Y + Constants.MessageRenderer.VerticalOffsetFromGameWindowDestinationRectangleCenter) -
					(Constants.MessageRenderer.VerticalWindowPadding * 2) -
					(windowWindowTexture.SpriteHeight * 2);

				_formatter = new MessageFormatter(_state.Message, font, selectedAnswerWindowTexture, maximumLineWidth);
				if (_formatter.Answers.Any())
				{
					_answerSelectionManager = new MessageAnswerSelectionManager(_formatter.Answers);
				}

				int clientWidth = _formatter.MaximumLineWidthAfterFormatting.Round();
				int clientHeight = Math.Min(maximumClientHeight, _formatter.TotalHeightAfterFormatting).Round();

				if (clientHeight < _formatter.TotalHeightAfterFormatting)
				{
					clientWidth += Constants.MessageRenderer.ArrowHorizontalPadding + windowWindowTexture.SpriteWidth;
				}

				SetWindowRectangleUsingWindowYAndClientSize(
					WindowHorizontalAlignment.Center,
					destinationRectangle.Center.Y + Constants.Tile.TileHeight * 2,
					clientWidth,
					clientHeight,
					windowWindowTexture.Padding);

				_windowRectangleSet = true;

				_state.AnswerSelectionManager = _answerSelectionManager;
				_state.MaximumScrollPosition = _formatter.TotalHeightAfterFormatting - clientHeight;
				_state.VisibleHeight = clientHeight;
			}

			Alpha = _state.Alpha;
		}

		protected override void RenderContents(IRendererParameters parameters)
		{
			parameters.ThrowIfNull("parameters");

			base.RenderContents(parameters);

			RenderMessage(parameters);
			if (_formatter.TotalHeightAfterFormatting > Window.AbsoluteClientRectangle.Height)
			{
				RenderScrollArrows(parameters);
			}
		}

		private void RenderMessage(IRendererParameters parameters)
		{
			Color textColor = Constants.MessageRenderer.TextColor * Alpha;
			Color shadowColor = Constants.MessageRenderer.ShadowColor * Alpha;
			var position = new Vector2(Window.AbsoluteClientRectangle.X, Window.AbsoluteClientRectangle.Y);
			Matrix translationMatrix = Matrix.CreateTranslation(0f, -_state.ScrollPosition, 0f);
			SpriteFont font = parameters.FontContent.Calibri12Pt;
			WindowTexture selectedAnswerWindowTexture = parameters.TextureContent.Windows.Glow1;

			for (int lineIndex = 0; lineIndex < _formatter.LineCount; lineIndex++)
			{
				MessageTextWord[] words = _formatter.GetWordsByLine(lineIndex).ToArray();
				MessageTextAnswer[] answers = words.OfType<MessageTextAnswer>().ToArray();

				if (answers.Length > 0)
				{
					RenderAnswers(parameters, selectedAnswerWindowTexture, font, translationMatrix, answers, shadowColor, position);
				}
				else
				{
					RenderWords(parameters, font, translationMatrix, lineIndex, words, shadowColor, ref position, ref textColor);
				}
			}
		}

		private void RenderWords(
			IRendererParameters parameters,
			SpriteFont font,
			Matrix translationMatrix,
			int lineIndex,
			IList<MessageTextWord> words,
			Color shadowColor,
			ref Vector2 position,
			ref Color textColor)
		{
			parameters.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, new ScissoringRasterizerState(), null, translationMatrix);

			MessageTextAlignment alignment = _formatter.GetAlignmentByLine(lineIndex);
			Vector2 lineSize = _formatter.GetLineSizeByLine(lineIndex);

			if (alignment == MessageTextAlignment.Center)
			{
				position.X += (Window.AbsoluteClientRectangle.Width - lineSize.X) / 2;
			}

			for (int wordIndex = 0; wordIndex < words.Count; wordIndex++)
			{
				MessageTextWord word = words[wordIndex];
				Engine.Common.Color color;

				if (_formatter.TryGetColorByWordCoordinate(new Coordinate(wordIndex, lineIndex), out color))
				{
					textColor = color.ToXnaColor() * Alpha;
				}
				if (word.PrependSpace)
				{
					position.X += _formatter.SpaceWord.Size.X;
				}

				parameters.SpriteBatch.DrawStringWithShadow(font, word.Text, position.Round(), textColor, shadowColor, Vector2.One);
				position.X += word.Size.X;
			}

			position.X = Window.AbsoluteClientRectangle.X;
			position.Y += lineSize.Y;

			parameters.SpriteBatch.End();
		}

		private void RenderAnswers(
			IRendererParameters parameters,
			WindowTexture selectedAnswerWindowTexture,
			SpriteFont font,
			Matrix translationMatrix,
			IEnumerable<MessageTextAnswer> answers,
			Color shadowColor,
			Vector2 position)
		{
			answers = answers.ToArray();

			int answerCount = answers.Count();
			int totalAnswerPadding = ((answerCount - 1) * Constants.MessageRenderer.AnswerHorizontalPadding);
			float lineWidth = answers.Sum(arg => arg.Size.X + (selectedAnswerWindowTexture.SpriteWidth * 2) + (Constants.MessageRenderer.AnswerHorizontalTextPadding * 2)) + totalAnswerPadding;
			float lineHeight = answers.Max(arg => arg.Size.Y);

			position.X += (Window.AbsoluteClientRectangle.Width - lineWidth) / 2;

			foreach (MessageTextAnswer answer in answers)
			{
				var window = new BorderedWindow(
					new Rectangle(
						position.X.Round(),
						position.Y.Round(),
						(answer.Size.X + (selectedAnswerWindowTexture.SpriteWidth * 2) + (Constants.MessageRenderer.AnswerHorizontalTextPadding * 2)).Round(),
						(lineHeight + (selectedAnswerWindowTexture.SpriteHeight * 2)).Round()),
					selectedAnswerWindowTexture.Padding);

				if (answer.Answer == _answerSelectionManager.SelectedAnswer)
				{
					_messageAnswerRenderer.Update(
						window.WindowRectangle,
						selectedAnswerWindowTexture.Padding,
						answer.SelectedAnswerBackgroundColor.ToXnaColor(),
						Alpha,
						Window.AbsoluteClientRectangle,
						translationMatrix);
					_messageAnswerRenderer.Render(parameters);
				}

				var textPosition = new Vector2(
					window.AbsoluteClientRectangle.X + ((window.AbsoluteClientRectangle.Width - answer.Size.X) / 2),
					window.AbsoluteClientRectangle.Y + ((window.AbsoluteClientRectangle.Height - lineHeight) / 2));
				Color textColor = answer.Answer == _answerSelectionManager.SelectedAnswer ? answer.SelectedAnswerForegroundColor.ToXnaColor() : answer.UnselectedAnswerForegroundColor.ToXnaColor();


				parameters.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, new ScissoringRasterizerState(), null, translationMatrix);

				parameters.SpriteBatch.GraphicsDevice.ScissorRectangle = Window.AbsoluteClientRectangle;

				parameters.SpriteBatch.DrawStringWithShadow(
					font,
					answer.Text,
					textPosition.Round(),
					textColor * Alpha,
					shadowColor,
					Vector2.One);

				parameters.SpriteBatch.End();

				position.X += window.WindowRectangle.Width + Constants.MessageRenderer.AnswerHorizontalPadding;
			}
		}

		private void RenderScrollArrows(IRendererParameters parameters)
		{
			WindowTexture windowTexture = parameters.TextureContent.Windows.InnerBevel1;
			int x = Window.AbsoluteClientRectangle.Right - windowTexture.SpriteWidth;
			var scrollPosition = new FloatToInt(_state.ScrollPosition);
			var upArrowPosition = new Vector2(x, Window.AbsoluteClientRectangle.Y);
			var downArrowPosition = new Vector2(x, Window.AbsoluteClientRectangle.Bottom - windowTexture.SpriteHeight);
			Color upArrowColor = (scrollPosition == 0 ? Constants.MessageRenderer.DisabledArrowColor : Constants.MessageRenderer.ArrowColor) * Alpha;
			Color downArrowColor = (scrollPosition.FloatValue >= _state.MaximumScrollPosition ? Constants.MessageRenderer.DisabledArrowColor : Constants.MessageRenderer.ArrowColor) * Alpha;

			parameters.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);

			parameters.SpriteBatch.Draw(windowTexture.Texture, upArrowPosition, windowTexture.UpArrowRectangle, upArrowColor);
			parameters.SpriteBatch.Draw(windowTexture.Texture, downArrowPosition, windowTexture.DownArrowRectangle, downArrowColor);

			parameters.SpriteBatch.End();
		}

		private class MessageAnswerRenderer : TexturedWindowRenderer
		{
			private Rectangle _scissorRectangle;
			private Matrix _transformMatrix = Matrix.Identity;

			public MessageAnswerRenderer(Func<TextureContent, WindowTexture> getWindowTextureDelegate)
				: base(getWindowTextureDelegate)
			{
			}

			public void Update(Rectangle windowRectangle, Padding padding, Color color, float alpha, Rectangle scissorRectangle, Matrix transformMatrix)
			{
				SetWindowRectangle(windowRectangle, padding);
				BackgroundColor = color;
				BorderColor = color;
				Alpha = alpha;
				_scissorRectangle = scissorRectangle;
				_transformMatrix = transformMatrix;
			}

			protected override void RenderBackground(IRendererParameters parameters)
			{
				UpdateParameters(parameters);

				base.RenderBackground(parameters);
			}

			protected override void RenderBorder(IRendererParameters parameters)
			{
				UpdateParameters(parameters);

				base.RenderBorder(parameters);
			}

			private void UpdateParameters(IRendererParameters parameters)
			{
				parameters.ScissorRectangle = _scissorRectangle;
				parameters.TransformMatrix = _transformMatrix;
			}
		}
	}
}