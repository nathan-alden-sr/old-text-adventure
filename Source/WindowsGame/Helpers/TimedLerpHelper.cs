using System;

using Microsoft.Xna.Framework;

namespace TextAdventure.WindowsGame.Helpers
{
	public class TimedLerpHelper
	{
		private readonly TimeSpan _duration;
		private readonly float _endValue;
		private readonly TimeSpan _startTotalTime;
		private readonly float _startValue;

		public TimedLerpHelper(TimeSpan startTotalTime, TimeSpan duration, float startValue, float endValue)
		{
			if (startTotalTime < TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException("startTotalTime");
			}
			if (duration < TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException("duration");
			}

			_startTotalTime = startTotalTime;
			_duration = duration;
			_startValue = MathHelper.Clamp(startValue, 0f, 1f);
			_endValue = MathHelper.Clamp(endValue, 0f, 1f);
			Value = _startValue;
		}

		public float Value
		{
			get;
			private set;
		}

		public void Update(TimeSpan totalTime)
		{
			if (totalTime < TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException("totalTime");
			}

			var fadeFactor = (float)((totalTime - _startTotalTime).TotalSeconds / _duration.TotalSeconds);

			Value = MathHelper.Clamp(MathHelper.Lerp(_startValue, _endValue, fadeFactor), _startValue, _endValue);
		}
	}
}