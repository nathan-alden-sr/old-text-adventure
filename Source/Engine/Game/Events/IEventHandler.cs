using TextAdventure.Engine.Game.World;

namespace TextAdventure.Engine.Game.Events
{
	public interface IEventHandler
	{
		string EventHandlerTypeName
		{
			get;
		}
	}

	public interface IEventHandler<in TEvent> : IEventHandler
		where TEvent : Event
	{
		EventResult HandleEvent(EventContext context, TEvent @event);
	}
}