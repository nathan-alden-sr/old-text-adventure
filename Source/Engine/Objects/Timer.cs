using System;

using Junior.Common;

using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Game.World;

namespace TextAdventure.Engine.Objects
{
	public class Timer : INamedObject, IDescribedObject
	{
		private readonly EventHandlerCollection _eventHandlerCollection;
		private readonly Guid _id;
		private readonly TimeSpan _interval;
		private string _description;
		private string _name;

		public Timer(
			Guid id,
			string name,
			string description,
			TimeSpan interval,
			EventHandlerCollection eventHandlers = null)
			: this(id, name, description, interval, TimerState.Stopped, TimeSpan.Zero, eventHandlers)
		{
		}

		public Timer(
			Guid id,
			string name,
			string description,
			TimeSpan interval,
			TimerState state,
			TimeSpan elapsedTime,
			EventHandlerCollection eventHandlerCollection = null)
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
			_eventHandlerCollection = eventHandlerCollection;
			State = state;
			ElapsedTime = elapsedTime;
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

		protected internal EventHandlerCollection EventHandlerCollection
		{
			get
			{
				return _eventHandlerCollection;
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

		protected internal void Stop()
		{
			State = TimerState.Stopped;
		}

		protected internal void Reset()
		{
			ElapsedTime = TimeSpan.Zero;
		}

		protected internal void Restart()
		{
			ElapsedTime = TimeSpan.Zero;
			State = TimerState.Running;
		}

		protected internal void Update(TimeSpan elapsedTime)
		{
			if (elapsedTime < TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException("elapsedTime");
			}

			ElapsedTime += elapsedTime;
		}

		protected internal virtual EventResult OnElapsed(EventContext context, TimerElapsedEvent @event)
		{
			context.ThrowIfNull("context");
			@event.ThrowIfNull("event");

			return _eventHandlerCollection.SafeInvoke(context, @event);
		}
	}
}