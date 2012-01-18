using System;

using Junior.Common;

using TextAdventure.Engine.Game.Events;

namespace TextAdventure.Engine.Objects
{
	public class Timer : INamedObject, IDescribedObject
	{
		private readonly Guid _id;
		private readonly TimeSpan _interval;
		private readonly IEventHandler<TimerElapsedEvent> _timerElapsedEventHandler;
		private string _description;
		private string _name;

		public Timer(
			Guid id,
			string name,
			string description,
			TimeSpan interval,
			IEventHandler<TimerElapsedEvent> timerElapsedEventHandler = null)
			: this(id, name, description, interval, TimerState.Stopped, TimeSpan.Zero, timerElapsedEventHandler)
		{
		}

		public Timer(
			Guid id,
			string name,
			string description,
			TimeSpan interval,
			TimerState state,
			TimeSpan elapsed,
			IEventHandler<TimerElapsedEvent> timerElapsedEventHandler = null)
		{
			name.ThrowIfNull("name");
			description.ThrowIfNull("description");
			if (interval < TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException("interval");
			}

			_id = id;
			Name = name;
			Description = description;
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

		public string Description
		{
			get
			{
				return _description;
			}
			protected internal set
			{
				value.ThrowIfNull("value");

				_description = value;
			}
		}

		public string Name
		{
			get
			{
				return _name;
			}
			protected internal set
			{
				value.ThrowIfNull("value");

				_name = value;
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