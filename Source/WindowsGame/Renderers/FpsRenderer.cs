using Junior.Common;

using Microsoft.Xna.Framework.Graphics;

using TextAdventure.WindowsGame.Extensions;
using TextAdventure.WindowsGame.RendererStates;
using TextAdventure.WindowsGame.Windows;

namespace TextAdventure.WindowsGame.Renderers
{
	public class FpsRenderer : BorderedWindowRenderer
	{
		private readonly IFpsRendererState _state;
		private bool _windowRectangleSet;

		public FpsRenderer(IFpsRendererState state)
		{
			state.ThrowIfNull("state");

			_state = state;
			BackgroundColor = Constants.FpsRenderer.BackgroundColor;
		}

		protected override void BeforeRender(IRendererParameters parameters)
		{
			parameters.ThrowIfNull("parameters");

			if (!_state.Visible)
			{
				return;
			}

			base.BeforeRender(parameters);

			if (_windowRectangleSet)
			{
				return;
			}

			SpriteFont font = parameters.FontContent.Calibri10Pt;

			SetWindowRectangleUsingClientSize(
				WindowAlignment.BottomLeft,
				(font.MeasureString("000 fps").X + Constants.FpsRenderer.ShadowOffset.X).Round(),
				(font.LineSpacing + Constants.FpsRenderer.ShadowOffset.Y).Round(),
				new Padding(Constants.BorderedWindow.Padding));
			_windowRectangleSet = true;
		}

		protected override void RenderContents(IRendererParameters parameters)
		{
			parameters.ThrowIfNull("parameters");

			if (!_state.Visible)
			{
				return;
			}

			base.RenderContents(parameters);

			SpriteFont font = parameters.FontContent.Calibri10Pt;
			string text = _state.FrameCount + " fps";

			parameters.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);

			parameters.SpriteBatch.DrawStringWithShadow(
				font,
				text,
				Window.AbsoluteClientRectangle.Location.ToVector2(),
				Constants.FpsRenderer.TextColor,
				Constants.FpsRenderer.ShadowColor,
				Constants.FpsRenderer.ShadowOffset);

			parameters.SpriteBatch.End();
		}
	}
}