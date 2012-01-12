using System;

using Microsoft.Xna.Framework;

namespace TextAdventure.WindowsGame.Helpers
{
	public class InterpolationHelper
	{
		private readonly TimeSpan _duration;
		private readonly float _endValue;
		private readonly TimeSpan _startTime;
		private readonly float _startValue;

		public InterpolationHelper(TimeSpan startTime, TimeSpan duration, float startValue, float endValue)
		{
			_startTime = startTime;
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

		public void Update(TimeSpan time)
		{
			if (time <= _startTime)
			{
				return;
			}

			var fadeFactor = (float)((time - _startTime).TotalSeconds / _duration.TotalSeconds);

			Value = MathHelper.Clamp(MathHelper.Lerp(_startValue, _endValue, fadeFactor), 0f, 1f);
		}
	}
}