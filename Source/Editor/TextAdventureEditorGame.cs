using Junior.Common;

using Microsoft.Xna.Framework.Graphics;

using TextAdventure.Editor.RendererStates;
using TextAdventure.Editor.Renderers;
using TextAdventure.Editor.Xna;

namespace TextAdventure.Editor
{
	public class TextAdventureEditorGame : XnaGame
	{
		private readonly IBoardRendererState _boardRendererState;
		private readonly IPencilRendererState _pencilRendererState;
		private readonly RendererCollection _rendererCollection = new RendererCollection();
		private TextureContent _textureContent;

		public TextAdventureEditorGame(
			GraphicsDevice graphicsDevice,
			IEditorView editorView,
			IBoardRendererState boardRendererState,
			IPencilRendererState pencilRendererState)
			: base(graphicsDevice, editorView)
		{
			boardRendererState.ThrowIfNull("boardRendererState");
			pencilRendererState.ThrowIfNull("pencilRendererState");

			_boardRendererState = boardRendererState;
			_pencilRendererState = pencilRendererState;
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

		private void AddRenderers()
		{
			_rendererCollection.Add(new HatchRenderer());
			_rendererCollection.Add(new BoardRenderer(_boardRendererState, EditorView));
			_rendererCollection.Add(new PencilRenderer(_pencilRendererState, EditorView));
		}
	}
}