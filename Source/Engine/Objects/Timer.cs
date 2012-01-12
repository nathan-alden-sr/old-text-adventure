using System;

using TextAdventure.Engine.Game.Events;

namespace TextAdventure.Engine.Objects
{
	public class Timer : IUnique
	{
		private readonly Guid _id;
		private readonly IEventHandler<TimerElapsedEvent> _timerElapsedEventHandler;
		private readonly TimeSpan _interval;

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
				throw new ArgumentOutOfRangeException("interval");
			}

			_id = id;
			_interval = interval;
			State = state;
			ElapsedTime = elapsed;
			_timerElapsedEventHandler = timerElapsedEventHandler;
		}

		public TimeSpan Interval
		{
			get
			{
				return _interval;
			}
		}

		public TimerState State
		{
			get;
			private set;
		}

		public TimeSpan ElapsedTime
		{
			get;
			private set;
		}

		public bool HasElapsed
		{
			get
			{
				return ElapsedTime >= Interval;
			}
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

		protected internal void Start()
		{
			switch (State)
			{
				case TimerState.Stopped:
					ElapsedTime = TimeSpan.Zero;
					break;
				case TimerState.Running:
					return;
			}
			State = TimerState.Running;
		}

		protected internal void Restart()
		{
			ElapsedTime = TimeSpan.Zero;
			State = TimerState.Running;
		}

		protected internal void Pause()
		{
			if (State == TimerState.Stopped)
			{
				return;
			}
			State = TimerState.Paused;
		}

		protected internal void Reset()
		{
			ElapsedTime = TimeSpan.Zero;
		}

		protected internal void Stop()
		{
			State = TimerState.Stopped;
		}

		protected internal void Update(TimeSpan elapsedTime)
		{
			if (elapsedTime < TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException("elapsedTime");
			}

			ElapsedTime += elapsedTime;
		}
	}
}