using Junior.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MathHelper = TextAdventure.Engine.Helpers.MathHelper;

namespace TextAdventure.Editor.Renderers
{
	public class HatchRenderer : IRenderer
	{
		private readonly IEditorView _editorView;

		public HatchRenderer(IEditorView editorView)
		{
			editorView.ThrowIfNull("editorView");

			_editorView = editorView;
		}

		public void Render(RendererParameters parameters)
		{
			parameters.ThrowIfNull("parameters");

			parameters.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullNone);

			var rectangle = new Rectangle(0, 0, _editorView.VisibleBoardSizeInPixels.Width, _editorView.VisibleBoardSizeInPixels.Height);
			Texture2D texture = parameters.TextureContent.Hatch;

			rectangle.Width = MathHelper.Instance.QuantizationCeiling(rectangle.Width, 8);
			rectangle.Height = MathHelper.Instance.QuantizationCeiling(rectangle.Height, 8);

			parameters.SpriteBatch.Draw(texture, rectangle, rectangle, Constants.HatchRenderer.Color);

			parameters.SpriteBatch.End();
		}
	}
}