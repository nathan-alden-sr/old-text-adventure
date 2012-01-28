using System;

using TextAdventure.Engine.Game.World;

namespace TextAdventure.Engine.Game.Events
{
	public abstract class EventHandler<TEvent> : IEventHandler<TEvent>
		where TEvent : Event
	{
		public string EventHandlerTypeName
		{
			get
			{
				Type type = GetType();

				return String.Format("{0}, {1}", type.FullName, type.Assembly.GetName().Name);
			}
		}

		public abstract EventResult HandleEvent(EventContext context, TEvent @event);
	}
}