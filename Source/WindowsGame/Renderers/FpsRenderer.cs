using Junior.Common;

using Microsoft.Xna.Framework.Graphics;

using TextAdventure.WindowsGame.RendererStates;
using TextAdventure.WindowsGame.Windows;
using TextAdventure.Xna.Extensions;

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

			SpriteFont font = parameters.FontContent.Calibri10Pt;

			SetWindowRectangleUsingClientSize(
				WindowAlignment.BottomLeft,
				(font.MeasureString("000 fps").X + Constants.FpsRenderer.ShadowOffset.X).Round(),
				(font.LineSpacing + Constants.FpsRenderer.ShadowOffset.Y).Round(),
				new Padding(Constants.BorderedWindow.Padding));
			_windowRectangleSet = true;
		}

		protected override void RenderContents(RendererParameters parameters)
		{
			parameters.ThrowIfNull("parameters");

			SpriteFont font = parameters.FontContent.Calibri10Pt;
			string text = _state.FrameCount + " fps";

			parameters.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);

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