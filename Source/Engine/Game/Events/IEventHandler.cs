namespace TextAdventure.Engine.Game.Events
{
	public interface IEventHandler<in TEvent> : ILoggable
		where TEvent : Event
	{
		void HandleEvent(IEventContext context, TEvent @event);
	}
}