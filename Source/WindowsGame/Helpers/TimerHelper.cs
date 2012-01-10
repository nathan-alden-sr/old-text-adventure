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

		public TimerHelper(TimeSpan duration, TimeSpan startTotalTime, Action completedDelegate)
		{
			completedDelegate.ThrowIfNull("completedDelegate");

			_duration = duration;
			_startTotalTime = startTotalTime;
			_completedDelegate = completedDelegate;
		}

		public void Update(TimeSpan totalTime)
		{
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