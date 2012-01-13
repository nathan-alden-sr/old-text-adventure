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
		private readonly XnaGameTime _gameTime = new XnaGameTime();
		private readonly GraphicsDevice _graphicsDevice;
		private readonly Stopwatch _totalGameTimeStopwatch = new Stopwatch();
		private bool _paused;
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

			_totalGameTimeStopwatch.Start();
			_elapsedGameTimeStopwatch.Start();
		}

		public void Pause()
		{
			if (!_running)
			{
				throw new InvalidOperationException("Game is not running.");
			}

			_paused = true;
			_totalGameTimeStopwatch.Stop();
			_elapsedGameTimeStopwatch.Stop();
		}

		public void Unpause()
		{
			if (!_running)
			{
				throw new InvalidOperationException("Game is not running.");
			}

			_paused = false;
			_totalGameTimeStopwatch.Start();
			_elapsedGameTimeStopwatch.Start();
		}

		public void Tick()
		{
			if (!_running)
			{
				throw new InvalidOperationException("Game is not running.");
			}
			if (_paused)
			{
				return;
			}

			TimeSpan totalGameTime = _totalGameTimeStopwatch.Elapsed;
			TimeSpan elapsedGameTime = _elapsedGameTimeStopwatch.Elapsed;

			_elapsedGameTimeStopwatch.Restart();

			_gameTime.TotalGameTime = totalGameTime;
			_gameTime.ElapsedGameTime = elapsedGameTime;

			Update(_gameTime);

			_graphicsDevice.Clear(Color.Black);
			Draw(_gameTime);
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

		protected virtual void Update(XnaGameTime gameTime)
		{
		}

		protected virtual void Draw(XnaGameTime gameTime)
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