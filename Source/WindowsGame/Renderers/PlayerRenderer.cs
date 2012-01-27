using Microsoft.Xna.Framework.Graphics;

namespace TextAdventure.WindowsGame.Renderers
{
	public class PlayerRenderer : IRenderer
	{
		public void Render(IRendererParameters parameters)
		{
			parameters.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);

			parameters.SpriteBatch.Draw(
				parameters.TextureContent.Pixel,
				Constants.PlayerRenderer.DestinationRectangle,
				parameters.TextureContent.Pixel.Bounds,
				Constants.PlayerRenderer.BackgroundColor);
			parameters.SpriteBatch.Draw(
				parameters.TextureContent.Characters,
				Constants.PlayerRenderer.DestinationRectangle,
				Constants.PlayerRenderer.TextureRectangle,
				Constants.PlayerRenderer.ForegroundColor);

			parameters.SpriteBatch.End();
		}
	}
}