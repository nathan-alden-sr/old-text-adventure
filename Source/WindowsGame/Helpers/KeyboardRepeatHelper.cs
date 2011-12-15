using System;

using Microsoft.Xna.Framework;

namespace TextAdventure.WindowsGame.Helpers
{
	public class KeyboardRepeatHelper
	{
		private TimeSpan _initialInterval;
		private bool _initialUpdateProcessed;
		private TimeSpan _repeatingInterval;
		private bool _started;
		private TimeSpan? _totalGameTime;

		public TimeSpan InitialInterval
		{
			get
			{
				return _initialInterval;
			}
			set
			{
				if (value <= TimeSpan.Zero)
				{
					throw new ArgumentOutOfRangeException("value", "Initial interval must be greater than 0.");
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
				if (value <= TimeSpan.Zero)
				{
					throw new ArgumentOutOfRangeException("value", "Repeating interval must be greater than 0.");
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
			_totalGameTime = null;
		}

		public void Stop()
		{
			_started = false;
			_initialUpdateProcessed = false;
			_totalGameTime = null;
		}

		public bool UpdateRequired(GameTime gameTime)
		{
			if (!_started)
			{
				return false;
			}
			if (_totalGameTime == null)
			{
				_totalGameTime = gameTime.TotalGameTime;
				return true;
			}

			TimeSpan interval = !_initialUpdateProcessed ? _initialInterval : _repeatingInterval;
			bool intervalElapsed = gameTime.TotalGameTime - _totalGameTime >= interval;

			if (intervalElapsed)
			{
				_initialUpdateProcessed = true;
				_totalGameTime = gameTime.TotalGameTime;
				return true;
			}

			return false;
		}
	}
}