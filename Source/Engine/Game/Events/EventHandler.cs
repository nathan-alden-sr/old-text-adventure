using System.Collections.Generic;

namespace TextAdventure.Engine.Game.Events
{
	public abstract class EventHandler : ILoggable
	{
		public virtual string Title
		{
			get
			{
				string typeName = GetType().Name;

				if (typeName.EndsWith("EventHandler"))
				{
					typeName = typeName.Substring(0, typeName.Length - 12);
				}
				else if (typeName.EndsWith("Handler"))
				{
					typeName = typeName.Substring(0, typeName.Length - 7);
				}

				return typeName;
			}
		}

		public virtual IEnumerable<string> Details
		{
			get
			{
				yield break;
			}
		}
	}

	public abstract class EventHandler<TEvent> : EventHandler, IEventHandler<TEvent>
		where TEvent : Event
	{
		public abstract void HandleEvent(EventContext context, TEvent @event);
	}
}