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
		private readonly GraphicsDevice _graphicsDevice;
		private bool _running;

		public XnaGame(GraphicsDevice graphicsDevice)
		{
			graphicsDevice.ThrowIfNull("graphicsDevice");

			_graphicsDevice = graphicsDevice;
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

			_graphicsDevice.Clear(Color.Black);
			Draw();
			_graphicsDevice.Present();
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
	}
}