using System;
using System.Linq;

using Junior.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TextAdventure.WindowsGame.RendererStates;
using TextAdventure.WindowsGame.Windows;
using TextAdventure.Xna.Extensions;

namespace TextAdventure.WindowsGame.Renderers
{
	public class LogRenderer : BorderedWindowRenderer
	{
		private const string LineFormatString = @"({0:h\:mm\:ss\.f})  {1}";
		private readonly ILogRendererState _state;

		public LogRenderer(ILogRendererState state)
		{
			state.ThrowIfNull("state");

			_state = state;
			BackgroundColor = Constants.LogRenderer.BackgroundColor;
		}

		protected override bool Visible
		{
			get
			{
				return _state.Visible;
			}
		}

		protected override void BeforeRender(RendererParameters parameters)
		{
			parameters.ThrowIfNull("parameters");

			base.BeforeRender(parameters);

			SpriteFont font = parameters.FontContent.Calibri10Pt;
			LogEntry[] logEntries = _state.GetFilteredLogEntries().ToArray();
			int maximumTitleTextWidth = logEntries.Any() ? logEntries.Max(arg => MeasureLineWidth(font, arg.LoggedTotalWorldTime, arg.Title, 0, _state.ShowTimestamps)) : 0;
			int maximumDetailTextWidth =
				logEntries.Any(arg => arg.Details.Any())
					? logEntries.Max(arg1 => arg1.Details.Any() ? arg1.Details.Max(arg2 => MeasureLineWidth(font, arg1.LoggedTotalWorldTime, arg2, Constants.LogRenderer.DetailIndent, false)) : 0)
					: 0;
			int lineCount = Math.Min(_state.MaximumVisibleLogLines, logEntries.Sum(arg => arg.LineCount));
			int clientWidth = (Math.Max(_state.MinimumWindowWidth ?? 0, Math.Max(maximumTitleTextWidth, maximumDetailTextWidth)) + Constants.LogRenderer.ShadowOffset.X).Round();
			int clientHeight = ((font.LineSpacing + Constants.LogRenderer.ShadowOffset.Y) * lineCount).Round();

			SetWindowRectangleUsingClientSize(WindowAlignment.TopLeft, clientWidth, clientHeight, new Padding(Constants.BorderedWindow.Padding));
		}

		protected override void RenderContents(RendererParameters parameters)
		{
			parameters.ThrowIfNull("parameters");

			SpriteFont font = parameters.FontContent.Calibri10Pt;
			Vector2 textVector = Window.AbsoluteClientRectangle.Location.ToVector2();
			int lineCount = 0;

			parameters.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);

			foreach (LogEntry logEntry in _state.GetFilteredLogEntries())
			{
				Color textColor = Constants.LogRenderer.TextColors.Single(arg => arg.Key == logEntry.EntryType).Value * logEntry.TimedLerpHelper.Value;
				Color shadowColor = Constants.LogRenderer.ShadowColors.Single(arg => arg.Key == logEntry.EntryType).Value * logEntry.TimedLerpHelper.Value;

				logEntry.TimedLerpHelper.Update(parameters.GameTime.TotalGameTime);

				string titleText = _state.ShowTimestamps ? String.Format(LineFormatString, logEntry.LoggedTotalWorldTime, logEntry.Title) : logEntry.Title;

				parameters.SpriteBatch.DrawStringWithShadow(font, titleText, textVector, textColor, shadowColor, Constants.LogRenderer.ShadowOffset);

				if (++lineCount >= _state.MaximumVisibleLogLines)
				{
					break;
				}

				textVector.X += Constants.LogRenderer.DetailIndent;
				textVector.Y += font.LineSpacing + Constants.LogRenderer.ShadowOffset.Y;

				foreach (string detail in logEntry.Details)
				{
					parameters.SpriteBatch.DrawStringWithShadow(font, detail, textVector, textColor, shadowColor, Constants.LogRenderer.ShadowOffset);

					if (++lineCount >= _state.MaximumVisibleLogLines)
					{
						break;
					}

					textVector.Y += font.LineSpacing + Constants.LogRenderer.ShadowOffset.Y;
				}

				if (lineCount >= _state.MaximumVisibleLogLines)
				{
					break;
				}

				textVector.X -= Constants.LogRenderer.DetailIndent;
			}

			parameters.SpriteBatch.End();
		}

		private static int MeasureLineWidth(SpriteFont font, TimeSpan loggedTotalTime, string text, int indent, bool showTimestamps)
		{
			if (showTimestamps)
			{
				text = String.Format(LineFormatString, loggedTotalTime, text);
			}

			return (font.MeasureString(text).X + Constants.LogRenderer.ShadowOffset.X + indent).Round();
		}
	}
}