using System;

using TextAdventure.Engine.Game.Events;

namespace TextAdventure.Engine.Objects
{
	public class Timer : ITimer
	{
		private readonly Guid _id;
		private readonly IEventHandler<TimerElapsedEvent> _timerElapsedEventHandler;

		private TimeSpan _interval;

		public Timer(
			Guid id,
			TimeSpan interval,
			IEventHandler<TimerElapsedEvent> timerElapsedEventHandler = null)
			: this(id, interval, TimerState.Stopped, TimeSpan.Zero, timerElapsedEventHandler)
		{
		}

		public Timer(
			Guid id,
			TimeSpan interval,
			TimerState state,
			TimeSpan elapsed,
			IEventHandler<TimerElapsedEvent> timerElapsedEventHandler = null)
		{
			if (interval < TimeSpan.Zero)
			{
				throw new ArgumentException("Interval must be at least 0.", "interval");
			}

			_id = id;
			Interval = interval;
			State = state;
			Elapsed = elapsed;
			_timerElapsedEventHandler = timerElapsedEventHandler;
		}

		public TimeSpan Interval
		{
			get
			{
				return _interval;
			}
			set
			{
				if (value < TimeSpan.Zero)
				{
					throw new ArgumentException("Interval must be at least 0.", "value");
				}

				_interval = value;
			}
		}

		public TimerState State
		{
			get;
			private set;
		}

		public TimeSpan Elapsed
		{
			get;
			private set;
		}

		public IEventHandler<TimerElapsedEvent> TimerElapsedEventHandler
		{
			get
			{
				return _timerElapsedEventHandler;
			}
		}

		public Guid Id
		{
			get
			{
				return _id;
			}
		}

		public void Start()
		{
			switch (State)
			{
				case TimerState.Stopped:
					Elapsed = TimeSpan.Zero;
					break;
				case TimerState.Running:
					return;
			}
			State = TimerState.Running;
		}

		public void Restart()
		{
			Elapsed = TimeSpan.Zero;
			State = TimerState.Running;
		}

		public void Pause()
		{
			if (State == TimerState.Stopped)
			{
				return;
			}
			State = TimerState.Paused;
		}

		public void Reset()
		{
			Elapsed = TimeSpan.Zero;
		}

		public void Stop()
		{
			State = TimerState.Stopped;
		}

		public void Update(TimeSpan elapsedGameTime)
		{
			
		}
	}
}