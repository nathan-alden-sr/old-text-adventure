using System;

using Junior.Common;

using TextAdventure.WindowsGame.Xna;

namespace TextAdventure.WindowsGame.RendererStates
{
	public class WorldTimeRendererState : IWorldTimeRendererState
	{
		private float _speed = 1f;

		public bool Visible
		{
			get;
			set;
		}

		public TimeSpan ElapsedWorldTime
		{
			get;
			private set;
		}

		public TimeSpan TotalWorldTime
		{
			get;
			private set;
		}

		public float Speed
		{
			get
			{
				return _speed;
			}
			set
			{
				_speed = Math.Max(Constants.WorldTimeRenderer.MinimumSpeedFactor, Math.Min(Constants.WorldTimeRenderer.MaximumSpeedFactor, value));
			}
		}

		public bool Paused
		{
			get;
			private set;
		}

		public void Pause()
		{
			Paused = true;
		}

		public void Unpause()
		{
			Paused = false;
		}

		public void UpdateWorldTimes(XnaGameTime gameTime)
		{
			gameTime.ThrowIfNull("gameTime");

			if (Paused)
			{
				return;
			}

			TimeSpan elapsed = TimeSpan.FromTicks((long)(gameTime.ElapsedGameTime.Ticks * _speed));

			ElapsedWorldTime = elapsed;
			TotalWorldTime += elapsed;
		}
	}
}