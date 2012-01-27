using System;

namespace TextAdventure.WindowsGame.Helpers
{
	public class KeyboardRepeatHelper
	{
		private TimeSpan _initialInterval;
		private bool _initialUpdateProcessed;
		private TimeSpan _repeatingInterval;
		private bool _started;
		private TimeSpan? _totalTime;

		public TimeSpan InitialInterval
		{
			get
			{
				return _initialInterval;
			}
			set
			{
				if (value < TimeSpan.Zero)
				{
					throw new ArgumentOutOfRangeException("value");
				}

				_initialInterval = value;
			}
		}

		public TimeSpan RepeatingInterval
		{
			get
			{
				return _repeatingInterval;
			}
			set
			{
				if (value < TimeSpan.Zero)
				{
					throw new ArgumentOutOfRangeException("value");
				}

				_repeatingInterval = value;
			}
		}

		public void Start()
		{
			if (_started)
			{
				return;
			}

			_started = true;
			_initialUpdateProcessed = false;
			_totalTime = null;
		}

		public void Stop()
		{
			_started = false;
			_initialUpdateProcessed = false;
			_totalTime = null;
		}

		public bool IntervalElapsed(TimeSpan totalTime)
		{
			if (totalTime < TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException("totalTime");
			}

			if (!_started)
			{
				return false;
			}
			if (_totalTime == null)
			{
				_totalTime = totalTime;
				return true;
			}

			TimeSpan interval = !_initialUpdateProcessed ? _initialInterval : _repeatingInterval;
			bool intervalElapsed = totalTime - _totalTime >= interval;

			if (intervalElapsed)
			{
				_initialUpdateProcessed = true;
				_totalTime = totalTime;
				return true;
			}

			return false;
		}
	}
}