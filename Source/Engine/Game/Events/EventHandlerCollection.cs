using System;
using System.Collections.Generic;
using System.Linq;

using Junior.Common;

using TextAdventure.Engine.Game.World;

namespace TextAdventure.Engine.Game.Events
{
	public class EventHandlerCollection
	{
		private readonly List<IEventHandler> _eventHandlers = new List<IEventHandler>();

		public EventHandlerCollection(params IEventHandler[] eventHandlers)
			: this((IEnumerable<IEventHandler>)eventHandlers)
		{
		}

		public EventHandlerCollection(IEnumerable<IEventHandler> eventHandlers)
		{
			eventHandlers.ThrowIfNull("eventHandlers");

			_eventHandlers.AddRange(eventHandlers);
		}

		public EventHandlerCollection(params string[] eventHandlerTypeNames)
			: this((IEnumerable<string>)eventHandlerTypeNames)
		{
		}

		public EventHandlerCollection(IEnumerable<string> eventHandlerTypeNames)
		{
			eventHandlerTypeNames.ThrowIfNull("eventHandlerTypeNames");

			foreach (string eventHandlerTypeName in eventHandlerTypeNames)
			{
				Type eventHandlerType;

				try
				{
					eventHandlerType = Type.GetType(eventHandlerTypeName);
				}
				catch (Exception exception)
				{
					throw new ArgumentException(String.Format("Error creating event handler '{0}':{1}{1}{2}", eventHandlerTypeName, Environment.NewLine, exception.Message), "eventHandlerTypeNames", exception);
				}

				if (eventHandlerType == null)
				{
					throw new ArgumentException(String.Format("Type '{0}' not found.", eventHandlerTypeName), "eventHandlerTypeNames");
				}
				if (!eventHandlerType.ImplementsInterface(typeof(IEventHandler<>)))
				{
					throw new ArgumentException(String.Format("Type '{0}' must implement IEventHandler<>.", eventHandlerTypeName), "eventHandlerTypeNames");
				}

				IEventHandler eventHandler;

				try
				{
					eventHandler = (IEventHandler)Activator.CreateInstance(eventHandlerType);
				}
				catch (Exception exception)
				{
					throw new ArgumentException(String.Format("Error creating event handler '{0}':{1}{1}{2}", eventHandlerTypeName, Environment.NewLine, exception.Message), "eventHandlerTypeNames", exception);
				}

				_eventHandlers.Add(eventHandler);
			}
		}

		protected internal IEnumerable<IEventHandler> EventHandlers
		{
			get
			{
				return _eventHandlers.AsReadOnly();
			}
		}

		protected internal EventResult Invoke<T>(EventContext context, T @event)
			where T : Event
		{
			var result = EventResult.None;

			foreach (EventHandler<T> eventHandler in _eventHandlers.OfType<EventHandler<T>>())
			{
				result = eventHandler.HandleEvent(context, @event);

				if (result == EventResult.Canceled)
				{
					return EventResult.Canceled;
				}
			}

			return result;
		}
	}
}