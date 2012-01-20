using Junior.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MathHelper = TextAdventure.Engine.Helpers.MathHelper;

namespace TextAdventure.Editor.Renderers
{
	public class HatchRenderer : IRenderer
	{
		public void Render(IRendererParameters parameters)
		{
			parameters.ThrowIfNull("parameters");

			parameters.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullNone);

			Rectangle rectangle = parameters.ViewportRectangle;
			Texture2D texture = parameters.TextureContent.Hatch;

			rectangle.Width = MathHelper.Instance.QuantizationCeiling(rectangle.Width, 8);
			rectangle.Height = MathHelper.Instance.QuantizationCeiling(rectangle.Height, 8);

			parameters.SpriteBatch.Draw(texture, rectangle, rectangle, Constants.HatchRenderer.Color);

			parameters.SpriteBatch.End();
		}
	}
}