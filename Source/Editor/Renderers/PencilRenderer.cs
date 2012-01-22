using Junior.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TextAdventure.Editor.RendererStates;
using TextAdventure.Xna;

namespace TextAdventure.Editor.Renderers
{
	public class PencilRenderer : IRenderer
	{
		private readonly IEditorView _editorView;
		private readonly ScissoringRasterizerState _rasterizerState = new ScissoringRasterizerState();
		private readonly IPencilRendererState _state;

		public PencilRenderer(IPencilRendererState state, IEditorView editorView)
		{
			state.ThrowIfNull("state");
			editorView.ThrowIfNull("editorView");

			_state = state;
			_editorView = editorView;
		}

		public void Render(IRendererParameters parameters)
		{
			parameters.ThrowIfNull("parameters");

			if (!_state.Enabled || _state.TopLeftSelectionCoordinate == null)
			{
				return;
			}

			const int tileWidth = TextAdventure.Xna.Constants.Tile.TileWidth;
			const int tileHeight = TextAdventure.Xna.Constants.Tile.TileHeight;
			var topLeftPoint = new Point(
				_editorView.TopLeftPoint.X + (_state.TopLeftSelectionCoordinate.Value.X * tileWidth),
				_editorView.TopLeftPoint.Y + (_state.TopLeftSelectionCoordinate.Value.Y * tileHeight));
			var destinationRectangle = new Rectangle(topLeftPoint.X, topLeftPoint.Y, tileWidth * _state.SelectionSize, tileHeight * _state.SelectionSize);
			var topLine = new Rectangle(destinationRectangle.X, destinationRectangle.Y, destinationRectangle.Width, 1);
			var bottomLine = new Rectangle(destinationRectangle.X, destinationRectangle.Bottom - 1, destinationRectangle.Width, 1);
			var leftLine = new Rectangle(destinationRectangle.X, destinationRectangle.Y, 1, destinationRectangle.Height);
			var rightLine = new Rectangle(destinationRectangle.Right - 1, destinationRectangle.Y, 1, destinationRectangle.Height);
			Texture2D pixelTexture = parameters.TextureContent.Pixel;

			parameters.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.None, _rasterizerState);

			parameters.SpriteBatch.GraphicsDevice.ScissorRectangle = new Rectangle(0, 0, _editorView.VisibleBoardSizeInPixels.Width, _editorView.VisibleBoardSizeInPixels.Height);

			parameters.SpriteBatch.Draw(pixelTexture, topLine, Color.Red);
			parameters.SpriteBatch.Draw(pixelTexture, bottomLine, Color.Red);
			parameters.SpriteBatch.Draw(pixelTexture, leftLine, Color.Red);
			parameters.SpriteBatch.Draw(pixelTexture, rightLine, Color.Red);

			parameters.SpriteBatch.End();
		}
	}
}