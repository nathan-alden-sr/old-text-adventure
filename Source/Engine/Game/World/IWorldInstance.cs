using TextAdventure.Engine.Game.Commands;
using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Game.Messages;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.World
{
	public interface IWorldInstance
	{
		IPlayer Player
		{
			get;
		}
		IWorld World
		{
			get;
		}
		IWorldTime WorldTime
		{
			get;
		}
		IBoard CurrentBoard
		{
			get;
		}
		CommandQueue CurrentCommandQueue
		{
			get;
		}
		IPlayerInput PlayerInput
		{
			get;
		}
		IMessageQueue MessageQueue
		{
			get;
		}

		void ProcessCommandQueue();

		EventResult RaiseEvent<TEvent>(IEventHandler<TEvent> eventHandler, TEvent @event)
			where TEvent : Event;
	}
}