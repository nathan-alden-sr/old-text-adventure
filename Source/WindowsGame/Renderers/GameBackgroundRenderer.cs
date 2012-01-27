using Junior.Common;

using Microsoft.Xna.Framework.Graphics;

namespace TextAdventure.WindowsGame.Renderers
{
	public class GameBackgroundRenderer : IRenderer
	{
		public void Render(IRendererParameters parameters)
		{
			parameters.ThrowIfNull("parameters");

			parameters.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullNone);

			parameters.SpriteBatch.Draw(parameters.TextureContent.GameBackground, Constants.GameWindow.DestinationRectangle, Constants.GameWindow.DestinationRectangle, Constants.GameBackgroundRenderer.Color);

			parameters.SpriteBatch.End();
		}
	}
}