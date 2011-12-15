using System;

using Microsoft.Xna.Framework;

namespace TextAdventure.WindowsGame.Helpers
{
	public class FadeHelper
	{
		private readonly FadeDirection _direction;
		private readonly TimeSpan _duration;
		private readonly TimeSpan _startTotalGameTime;

		public FadeHelper(FadeDirection direction, TimeSpan startTotalGameTime, TimeSpan duration)
		{
			switch (direction)
			{
				case FadeDirection.In:
					Alpha = 0;
					break;
				case FadeDirection.Out:
					Alpha = 1;
					break;
				default:
					throw new ArgumentOutOfRangeException("direction");
			}
			_direction = direction;
			_startTotalGameTime = startTotalGameTime;
			_duration = duration;
		}

		public float Alpha
		{
			get;
			private set;
		}

		public void Update(TimeSpan totalGameTime)
		{
			if (totalGameTime <= _startTotalGameTime)
			{
				return;
			}

			var fadeFactor = (float)((totalGameTime - _startTotalGameTime).TotalSeconds / _duration.TotalSeconds);

			if (_direction == FadeDirection.Out)
			{
				fadeFactor = 1 - fadeFactor;
			}

			Alpha = MathHelper.Clamp(fadeFactor, 0, 1);
		}
	}
}