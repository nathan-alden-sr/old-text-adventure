using System;

using Junior.Common;

namespace TextAdventure.WindowsGame.Helpers
{
	public class TimerHelper
	{
		private readonly Action _completedDelegate;
		private readonly TimeSpan _duration;
		private readonly TimeSpan _startTotalGameTime;
		private bool _complete;

		public TimerHelper(TimeSpan duration, TimeSpan startTotalGameTime, Action completedDelegate)
		{
			completedDelegate.ThrowIfNull("completedDelegate");

			_duration = duration;
			_startTotalGameTime = startTotalGameTime;
			_completedDelegate = completedDelegate;
		}

		public void Update(TimeSpan totalGameTime)
		{
			bool timerRunning = _complete || totalGameTime - _startTotalGameTime < _duration;

			if (timerRunning)
			{
				return;
			}

			_complete = true;
			_completedDelegate();
		}
	}
}