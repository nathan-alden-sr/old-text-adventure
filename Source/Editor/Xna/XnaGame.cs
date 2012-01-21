using System;

using Junior.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using TextAdventure.Xna;

namespace TextAdventure.Editor.Xna
{
	public class XnaGame : IDisposable
	{
		private readonly ContentManager _content;
		private readonly IEditorView _editorView;
		private readonly GraphicsDevice _graphicsDevice;
		private bool _running;

		public XnaGame(GraphicsDevice graphicsDevice, IEditorView editorView)
		{
			graphicsDevice.ThrowIfNull("graphicsDevice");
			editorView.ThrowIfNull("editorView");

			_graphicsDevice = graphicsDevice;
			_editorView = editorView;
			_content = new ContentManager(new XnaServiceProvider(graphicsDevice))
			           	{
			           		RootDirectory = "Content"
			           	};
		}

		protected GraphicsDevice GraphicsDevice
		{
			get
			{
				return _graphicsDevice;
			}
		}

		protected IEditorView EditorView
		{
			get
			{
				return _editorView;
			}
		}

		protected ContentManager Content
		{
			get
			{
				return _content;
			}
		}

		public void Dispose()
		{
			OnDispose(true);
			GC.SuppressFinalize(this);
		}

		public void Run()
		{
			if (_running)
			{
				throw new InvalidOperationException("Game is already running.");
			}

			_running = true;

			Initialize();
			LoadContent();
		}

		public void Tick()
		{
			if (!_running)
			{
				throw new InvalidOperationException("Game is not running.");
			}

			FrameworkDispatcher.Update();
			Update();

			_graphicsDevice.Clear(Color.Black);
			Draw();
			Present();
		}

		~XnaGame()
		{
			OnDispose(false);
		}

		protected virtual void Initialize()
		{
		}

		protected virtual void LoadContent()
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void Draw()
		{
		}

		protected virtual void OnDispose(bool disposing)
		{
			if (disposing)
			{
				_content.Dispose();
			}
		}

		private void Present()
		{
			var sourceRectangle = new Rectangle(0, 0, _editorView.VisibleSizeInPixels.Width, _editorView.VisibleSizeInPixels.Height);

			_graphicsDevice.Present(sourceRectangle, null, IntPtr.Zero);
		}
	}
}