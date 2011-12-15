using TextAdventure.Engine.Game.Commands;
using TextAdventure.Engine.Game.Events;

namespace TextAdventure.Engine.Game.World
{
	public interface IWorldObserver
	{
		void CommandExecuting(Command command);
		void CommandExecuted(Command command, CommandResult result);
		void EventRaising(Event @event);
		void EventRaised(Event @event);

		void EventHandled<TEvent>(IEventHandler<TEvent> eventHandler, TEvent @event, EventResult result)
			where TEvent : Event;
	}
}