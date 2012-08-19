using System;

using Junior.Common;

namespace TextAdventure.WindowsGame.Helpers
{
	public class TimerHelper
	{
		private readonly Action _completedDelegate;
		private readonly TimeSpan _duration;
		private readonly TimeSpan _startTotalTime;
		private bool _complete;

		public TimerHelper(TimeSpan startTotalTime, TimeSpan duration, Action completedDelegate)
		{
			completedDelegate.ThrowIfNull("completedDelegate");
			if (startTotalTime < TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException("startTotalTime");
			}
			if (duration < TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException("duration");
			}

			_duration = duration;
			_startTotalTime = startTotalTime;
			_completedDelegate = completedDelegate;
		}

		public void Update(TimeSpan totalTime)
		{
			if (totalTime < TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException("totalTime");
			}

			bool timerRunning = _complete || totalTime - _startTotalTime < _duration;

			if (timerRunning)
			{
				return;
			}

			_complete = true;
			_completedDelegate();
		}
	}
}