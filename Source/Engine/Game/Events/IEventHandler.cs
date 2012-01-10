namespace TextAdventure.Engine.Game.Events
{
	public interface IEventHandler<in TEvent> : ILoggable
		where TEvent : Event
	{
		void HandleEvent(EventContext context, TEvent @event);
	}
}