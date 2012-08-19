using System;

using Junior.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TextAdventure.Editor.RendererStates;
using TextAdventure.Editor.Renderers;
using TextAdventure.Xna;

namespace TextAdventure.Editor.Xna
{
	public class TextAdventureEditorGame : XnaGame
	{
		private readonly IBoardRendererState _boardRendererState;
		private readonly IEditorView _editorView;
		private readonly IEraserRendererState _eraserRendererState;
		private readonly IPencilRendererState _pencilRendererState;
		private readonly RendererCollection _rendererCollection = new RendererCollection();
		private TextureContent _textureContent;

		public TextAdventureEditorGame(
			GraphicsDevice graphicsDevice,
			IEditorView editorView,
			IBoardRendererState boardRendererState,
			IPencilRendererState pencilRendererState,
			IEraserRendererState eraserRendererState)
			: base(graphicsDevice, new ContentDirectoryContentManagerProvider())
		{
			editorView.ThrowIfNull("editorView");
			boardRendererState.ThrowIfNull("boardRendererState");
			pencilRendererState.ThrowIfNull("pencilRendererState");
			eraserRendererState.ThrowIfNull("eraserRendererState");

			_editorView = editorView;
			_boardRendererState = boardRendererState;
			_pencilRendererState = pencilRendererState;
			_eraserRendererState = eraserRendererState;
		}

		protected override void Initialize()
		{
			AddRenderers();

			base.Initialize();
		}

		protected override void LoadContent()
		{
			_textureContent = new TextureContent(Content);

			base.LoadContent();
		}

		protected override void Draw()
		{
			base.Draw();

			var spriteBatch = new SpriteBatch(GraphicsDevice);

			_rendererCollection.Render(spriteBatch, GraphicsDevice.Viewport.Bounds, _textureContent);

			spriteBatch.Dispose();
		}

		protected override void Present()
		{
			var sourceRectangle = new Rectangle(0, 0, _editorView.ClientSizeInPixels.Width, _editorView.ClientSizeInPixels.Height);

			GraphicsDevice.Present(sourceRectangle, null, IntPtr.Zero);
		}

		private void AddRenderers()
		{
			_rendererCollection.Add(new HatchRenderer(_editorView));
			_rendererCollection.Add(new BoardRenderer(_boardRendererState, _editorView));
			_rendererCollection.Add(new PencilRenderer(_pencilRendererState, _editorView));
			_rendererCollection.Add(new EraserRenderer(_eraserRendererState, _editorView));
		}
	}
}