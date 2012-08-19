using System;
using System.Diagnostics;

using Microsoft.Xna.Framework.Graphics;

namespace TextAdventure.Xna
{
	public abstract class TimedXnaGame : XnaGame
	{
		private readonly Stopwatch _elapsedGameTimeStopwatch = new Stopwatch();
		private readonly XnaGameTime _gameTime = new XnaGameTime();
		private readonly Stopwatch _totalGameTimeStopwatch = new Stopwatch();

		protected TimedXnaGame(GraphicsDevice graphicsDevice, IContentManagerProvider contentManagerProvider)
			: base(graphicsDevice, contentManagerProvider)
		{
		}

		public bool Paused
		{
			get;
			private set;
		}

		protected override void Initialize()
		{
			_totalGameTimeStopwatch.Start();
			_elapsedGameTimeStopwatch.Start();
		}

		protected override void BeforeTick()
		{
			if (Paused)
			{
				return;
			}

			TimeSpan totalGameTime = _totalGameTimeStopwatch.Elapsed;
			TimeSpan elapsedGameTime = _elapsedGameTimeStopwatch.Elapsed;

			_elapsedGameTimeStopwatch.Restart();

			_gameTime.TotalGameTime = totalGameTime;
			_gameTime.ElapsedGameTime = elapsedGameTime;
		}

		protected override void Update()
		{
			if (!Paused)
			{
				Update(_gameTime);
			}
		}

		protected override void Draw()
		{
			Draw(_gameTime);
		}

		public void Pause()
		{
			if (!Running)
			{
				throw new InvalidOperationException("Game is not running.");
			}

			Paused = true;
			_totalGameTimeStopwatch.Stop();
			_elapsedGameTimeStopwatch.Stop();
		}

		public void Unpause()
		{
			if (!Running)
			{
				throw new InvalidOperationException("Game is not running.");
			}

			Paused = false;
			_totalGameTimeStopwatch.Start();
			_elapsedGameTimeStopwatch.Start();
		}

		protected virtual void Update(IXnaGameTime gameTime)
		{
		}

		protected virtual void Draw(IXnaGameTime gameTime)
		{
		}

		private class XnaGameTime : IXnaGameTime
		{
			public TimeSpan TotalGameTime
			{
				get;
				set;
			}

			public TimeSpan ElapsedGameTime
			{
				get;
				set;
			}
		}
	}
}