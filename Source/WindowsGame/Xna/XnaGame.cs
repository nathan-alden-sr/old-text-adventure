using System;
using System.Diagnostics;

using Junior.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TextAdventure.WindowsGame.Xna
{
	public class XnaGame : IDisposable
	{
		private readonly ContentManager _content;
		private readonly Stopwatch _elapsedGameTimeStopwatch = new Stopwatch();
		private readonly GraphicsDevice _graphicsDevice;
		private readonly Stopwatch _totalGameTimeStopwatch = new Stopwatch();

		public XnaGame(GraphicsDevice graphicsDevice)
		{
			graphicsDevice.ThrowIfNull("graphicsDevice");

			_graphicsDevice = graphicsDevice;
			_content = new ContentManager(new XnaServiceProvider(graphicsDevice))
			           	{
			           		RootDirectory = "Content"
			           	};
		}

		public GraphicsDevice GraphicsDevice
		{
			get
			{
				return _graphicsDevice;
			}
		}

		public ContentManager Content
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

		public void Start()
		{
			Initialize();
			LoadContent();

			_totalGameTimeStopwatch.Start();
			_elapsedGameTimeStopwatch.Start();
		}

		public void Tick()
		{
			TimeSpan totalGameTime = _totalGameTimeStopwatch.Elapsed;
			TimeSpan elapsedGameTime = _elapsedGameTimeStopwatch.Elapsed;

			_elapsedGameTimeStopwatch.Restart();

			var gameTime = new GameTime(totalGameTime, elapsedGameTime, false);

			Update(gameTime);
			Draw(gameTime);
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

		protected virtual void Update(GameTime gameTime)
		{
		}

		protected virtual void Draw(GameTime gameTime)
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