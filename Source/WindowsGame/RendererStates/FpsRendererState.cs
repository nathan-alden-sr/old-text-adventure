using System;

namespace TextAdventure.WindowsGame.RendererStates
{
	public class FpsRendererState : IFpsRendererState
	{
		private double _elapsedSeconds;
		private int _framesPerSecond;

		public bool Visible
		{
			get;
			set;
		}

		public int FrameCount
		{
			get;
			private set;
		}

		public void FrameRendered()
		{
			_framesPerSecond++;
		}

		public void UpdateElapsedGameTime(TimeSpan elapsedGameTime)
		{
			_elapsedSeconds += elapsedGameTime.TotalSeconds;
		}

		public void UpdateFrameCount()
		{
			if (_elapsedSeconds <= 1)
			{
				return;
			}

			FrameCount = _framesPerSecond;
			_framesPerSecond = 0;
			_elapsedSeconds -= 1;
		}
	}
}