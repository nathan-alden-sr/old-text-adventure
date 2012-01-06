using System;

using Microsoft.Xna.Framework;

namespace TextAdventure.WindowsGame.Helpers
{
	public class FadeHelper
	{
		private readonly TimeSpan _duration;
		private readonly float _endAlpha;
		private readonly float _startAlpha;
		private readonly TimeSpan _startTotalGameTime;

		public FadeHelper(TimeSpan startTotalGameTime, TimeSpan duration, float startAlpha, float endAlpha)
		{
			_startTotalGameTime = startTotalGameTime;
			_duration = duration;
			_startAlpha = MathHelper.Clamp(startAlpha, 0f, 1f);
			_endAlpha = MathHelper.Clamp(endAlpha, 0f, 1f);
			Alpha = _startAlpha;
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

			Alpha = MathHelper.Clamp(MathHelper.Lerp(_startAlpha, _endAlpha, fadeFactor), 0f, 1f);
		}
	}
}