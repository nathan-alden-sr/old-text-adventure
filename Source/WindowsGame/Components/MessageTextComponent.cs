using System.Collections.Generic;
using System.Linq;

using Junior.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;
using TextAdventure.WindowsGame.Extensions;
using TextAdventure.WindowsGame.Helpers;
using TextAdventure.WindowsGame.Managers;

using Color = Microsoft.Xna.Framework.Color;

namespace TextAdventure.WindowsGame.Components
{
	public class MessageTextComponent : WindowComponent
	{
		private static readonly Color _shadowColor = Color.Black;
		private readonly MessageAnswerSelectionManager _answerSelectionManager;
		private readonly SpriteFont _font;
		private readonly MessageTextFormatter _formatter;
		private readonly WindowTexture _selectedAnswerWindowTexture;
		private readonly WindowBackgroundDrawingHelper _windowBackgroundDrawingHelper;
		private readonly WindowBorderDrawingHelper _windowBorderDrawingHelper;

		public MessageTextComponent(GameManager gameManager, IMessage message, float maximumLineWidth)
			: base(gameManager)
		{
			message.ThrowIfNull("message");

			_font = FontContent.Calibri12pt;
			_selectedAnswerWindowTexture = TextureContent.Windows.Glow1;
			_formatter = new MessageTextFormatter(message, _font, _selectedAnswerWindowTexture, maximumLineWidth);
			_windowBackgroundDrawingHelper = new WindowBackgroundDrawingHelper(_selectedAnswerWindowTexture);
			_windowBorderDrawingHelper = new WindowBorderDrawingHelper(_selectedAnswerWindowTexture);
			_answerSelectionManager = _formatter.Answers.Any() ? new MessageAnswerSelectionManager(_formatter.Answers) : null;

			DrawOrder = ComponentDrawOrder.MessageText;
		}

		public float MaximumLineWidthAfterFormatting
		{
			get
			{
				return _formatter.MaximumLineWidthAfterFormatting;
			}
		}

		public float TotalHeightAfterFormatting
		{
			get
			{
				return _formatter.TotalHeightAfterFormatting;
			}
		}

		public MessageAnswerSelectionManager AnswerSelectionManager
		{
			get
			{
				return _answerSelectionManager;
			}
		}

		public void SetAlpha(float alpha)
		{
			Alpha = alpha;
		}

		public override void Draw(GameTime gameTime)
		{
			Color textColor = Color.White * Alpha;
			Color shadowColor = _shadowColor * Alpha;
			var position = new Vector2(Window.WindowRectangle.X, Window.WindowRectangle.Y);

			for (int lineIndex = 0; lineIndex < _formatter.LineCount; lineIndex++)
			{
				MessageTextWord[] words = _formatter.GetWordsByLine(lineIndex).ToArray();
				MessageTextAnswer[] answers = words.OfType<MessageTextAnswer>().ToArray();

				if (answers.Length > 0)
				{
					ProcessAnswers(lineIndex, answers, shadowColor, position);
				}
				else
				{
					ProcessWords(lineIndex, words, shadowColor, ref position, ref textColor);
				}
			}

			base.Draw(gameTime);
		}

		private void ProcessWords(int lineIndex, IList<MessageTextWord> words, Color shadowColor, ref Vector2 position, ref Color textColor)
		{
			SpriteBatch.Begin();

			MessageTextAlignment alignment = _formatter.GetAlignmentByLine(lineIndex);
			Vector2 lineSize = _formatter.GetLineSizeByLine(lineIndex);

			if (alignment == MessageTextAlignment.Center)
			{
				position.X += (Window.WindowRectangle.Width - lineSize.X) / 2;
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

				SpriteBatch.DrawStringWithShadow(_font, word.Text, position.Round(), textColor, shadowColor, Vector2.One);
				position.X += word.Size.X;
			}

			position.X = Window.WindowRectangle.X;
			position.Y += lineSize.Y;

			SpriteBatch.End();
		}

		private void ProcessAnswers(int lineIndex, IEnumerable<MessageTextAnswer> answers, Color shadowColor, Vector2 position)
		{
			answers = answers.ToArray();

			const int answerPadding = 5;
			const int answerTextPadding = 5;
			int answerCount = answers.Count();
			int totalAnswerPadding = ((answerCount - 1) * answerPadding);
			float lineWidth = answers.Sum(arg => arg.Size.X + (_selectedAnswerWindowTexture.SpriteWidth * 2) + (answerTextPadding * 2)) + totalAnswerPadding;
			float lineHeight = answers.Max(arg => arg.Size.Y);
			position.X += (Window.WindowRectangle.Width - lineWidth) / 2;

			foreach (MessageTextAnswer answer in answers)
			{
				var window = new Window(
					new Rectangle(
						position.X.Round(),
						position.Y.Round(),
						(answer.Size.X + (_selectedAnswerWindowTexture.SpriteWidth * 2) + (answerTextPadding * 2)).Round(),
						(lineHeight + (_selectedAnswerWindowTexture.SpriteHeight * 2)).Round()),
					new Padding(_selectedAnswerWindowTexture.SpriteWidth, _selectedAnswerWindowTexture.SpriteHeight));

				if (answer.Answer == _answerSelectionManager.SelectedAnswer)
				{
					_windowBackgroundDrawingHelper.Alpha = answer.SelectedAnswerBackgroundColor.A * Alpha;
					_windowBackgroundDrawingHelper.BackgroundColor = answer.SelectedAnswerBackgroundColor.ToXnaColor();
					_windowBackgroundDrawingHelper.Window = window;
					_windowBackgroundDrawingHelper.Draw(SpriteBatch);

					_windowBorderDrawingHelper.Alpha = answer.SelectedAnswerBackgroundColor.A * Alpha;
					_windowBorderDrawingHelper.BorderColor = answer.SelectedAnswerBackgroundColor.ToXnaColor();
					_windowBorderDrawingHelper.Window = window;
					_windowBorderDrawingHelper.Draw(SpriteBatch);
				}

				var textPosition = new Vector2(
					window.AbsoluteClientRectangle.X + ((window.AbsoluteClientRectangle.Width - answer.Size.X) / 2),
					window.AbsoluteClientRectangle.Y + ((window.AbsoluteClientRectangle.Height - lineHeight) / 2));

				SpriteBatch.Begin();

				Color textColor = answer.Answer == _answerSelectionManager.SelectedAnswer ? answer.SelectedAnswerForegroundColor.ToXnaColor() : answer.UnselectedAnswerForegroundColor.ToXnaColor();

				SpriteBatch.DrawStringWithShadow(
					_font,
					answer.Text,
					textPosition.Round(),
					textColor * Alpha,
					shadowColor,
					Vector2.One);

				SpriteBatch.End();

				position.X += window.WindowRectangle.Width + answerPadding;
			}
		}
	}
}