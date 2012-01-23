using System;

using Junior.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TextAdventure.Xna
{
	public abstract class XnaGame : IDisposable
	{
		private readonly ContentManager _content;
		private readonly GraphicsDevice _graphicsDevice;

		protected XnaGame(GraphicsDevice graphicsDevice, IContentManagerProvider contentManagerProvider)
		{
			graphicsDevice.ThrowIfNull("graphicsDevice");
			contentManagerProvider.ThrowIfNull("contentManagerProvider");

			_graphicsDevice = graphicsDevice;
			_content = contentManagerProvider.GetContentManager(graphicsDevice);
		}

		public bool Running
		{
			get;
			private set;
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
			if (Running)
			{
				throw new InvalidOperationException("Game is already running.");
			}

			Running = true;

			Initialize();
			LoadContent();
		}

		public void Tick()
		{
			if (!Running)
			{
				throw new InvalidOperationException("Game is not running.");
			}

			BeforeTick();

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

		protected virtual void BeforeTick()
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

		protected abstract void Present();
	}
}