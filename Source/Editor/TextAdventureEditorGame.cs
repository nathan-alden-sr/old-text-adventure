using System;
using System.Linq;

using Junior.Common;

using Microsoft.Xna.Framework.Graphics;

using TextAdventure.Editor.Renderers;
using TextAdventure.Editor.Xna;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Editor
{
	public class TextAdventureEditorGame : XnaGame
	{
		private readonly RendererCollection _rendererCollection = new RendererCollection();
		private readonly World _world;
		private TextureContent _textureContent;

		public TextAdventureEditorGame(GraphicsDevice graphicsDevice, World world)
			: base(graphicsDevice)
		{
			world.ThrowIfNull("world");

			_world = world;
			CurrentBoard = world.Boards.First();
		}

		public Board CurrentBoard
		{
			get;
			private set;
		}

		public void SetCurrentBoard(Guid boardId)
		{
			CurrentBoard = _world.GetBoardById(boardId);
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
		}
	}
}