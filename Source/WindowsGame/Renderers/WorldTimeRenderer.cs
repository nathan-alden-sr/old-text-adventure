using System;

using Junior.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TextAdventure.WindowsGame.Helpers;
using TextAdventure.WindowsGame.RendererStates;
using TextAdventure.WindowsGame.Windows;
using TextAdventure.Xna.Extensions;

namespace TextAdventure.WindowsGame.Renderers
{
	public class WorldTimeRenderer : BorderedWindowRenderer
	{
		private const string TimeFormat = "{0}:{1:00}:{2:00}.{3:0}";
		private readonly IWorldTimeRendererState _state;
		private bool _windowRectangleSet;

		public WorldTimeRenderer(IWorldTimeRendererState state)
		{
			state.ThrowIfNull("state");

			_state = state;
			BackgroundColor = Constants.WorldTimeRenderer.BackgroundColor;
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

			if (_windowRectangleSet)
			{
				return;
			}

			SpriteFont font = parameters.FontContent.Calibri10PtBold;
			int clientWidth = (font.MeasureString("Normal speed").X + Constants.WorldTimeRenderer.ShadowOffset.X).Round();
			int clientHeight = ((font.LineSpacing + Constants.WorldTimeRenderer.ShadowOffset.Y) * 3).Round();

			SetWindowRectangleUsingClientSize(WindowAlignment.TopRight, clientWidth, clientHeight, new Padding(Constants.BorderedWindow.Padding));
			_windowRectangleSet = true;
		}

		protected override void RenderContents(RendererParameters parameters)
		{
			parameters.ThrowIfNull("parameters");

			SpriteFont font = parameters.FontContent.Calibri10PtBold;
			string gameTimeText = String.Format(
				TimeFormat,
				parameters.GameTime.TotalGameTime.Hours,
				parameters.GameTime.TotalGameTime.Minutes,
				parameters.GameTime.TotalGameTime.Seconds,
				parameters.GameTime.TotalGameTime.Milliseconds / 100);
			string worldTimeText = String.Format(
				TimeFormat,
				_state.TotalWorldTime.Hours,
				_state.TotalWorldTime.Minutes,
				_state.TotalWorldTime.Seconds,
				_state.TotalWorldTime.Milliseconds / 100);

			parameters.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);

			parameters.SpriteBatch.DrawStringWithShadow(
				font,
				gameTimeText,
				TextDrawingHelper.Instance.GetAlignedOrigin(font, gameTimeText, Window.AbsoluteClientRectangle, WindowAlignment.TopRight).ToVector2(),
				Color.White,
				Color.Black,
				Constants.WorldTimeRenderer.ShadowOffset);

			Vector2 worldTimePosition = TextDrawingHelper.Instance.GetAlignedOrigin(font, worldTimeText, Window.AbsoluteClientRectangle, WindowAlignment.TopRight).ToVector2();

			worldTimePosition.Y += font.LineSpacing + Constants.WorldTimeRenderer.ShadowOffset.Y;

			parameters.SpriteBatch.DrawStringWithShadow(
				font,
				worldTimeText,
				worldTimePosition,
				_state.Paused ? Constants.WorldTimeRenderer.PausedColor : Constants.WorldTimeRenderer.UnpausedColor,
				Constants.WorldTimeRenderer.ShadowColor,
				Constants.WorldTimeRenderer.ShadowOffset);

			string speedText;

			if (_state.Paused)
			{
				speedText = "Paused";
			}
			else if (_state.Speed < 1)
			{
				speedText = String.Format("1/{0} speed", (int)Math.Round(1 / _state.Speed));
			}
			else if (new FloatToInt(_state.Speed) == 1)
			{
				speedText = "Normal speed";
			}
			else
			{
				speedText = (int)_state.Speed + "x speed";
			}

			parameters.SpriteBatch.DrawStringWithShadow(
				font,
				speedText,
				TextDrawingHelper.Instance.GetAlignedOrigin(font, speedText, Window.AbsoluteClientRectangle, WindowAlignment.BottomRight).ToVector2(),
				_state.Paused ? Constants.WorldTimeRenderer.PausedColor : Constants.WorldTimeRenderer.UnpausedColor,
				Constants.WorldTimeRenderer.ShadowColor,
				Constants.WorldTimeRenderer.ShadowOffset);

			parameters.SpriteBatch.End();
		}
	}
}