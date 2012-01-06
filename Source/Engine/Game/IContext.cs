using System;
using System.Collections.Generic;

using TextAdventure.Engine.Game.Commands;
using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Game.World;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game
{
	public interface IContext
	{
		IEnumerable<IActor> Actors
		{
			get;
		}
		IEnumerable<IMessage> Messages
		{
			get;
		}
		IBoard CurrentBoard
		{
			get;
		}
		IPlayer Player
		{
			get;
		}
		IWorldTime WorldTime
		{
			get;
		}
		IPlayerInput PlayerInput
		{
			get;
		}

		EventResult RaiseEvent<TEvent>(IEventHandler<TEvent> eventHandler, TEvent @event)
			where TEvent : Event;

		IBoard GetBoardById(Guid id);
		IActor GetActorById(Guid id);
		IActorInstance GetActorInstanceById(Guid id);
		IEnumerable<IActorInstance> GetActorInstancesByActorId(Guid actorId);
		void EnqueueCommand(Command command, Action<CommandResult> commandExecutedDelegate = null);
		void EnqueueCommandToExecuteAtTime(Command command, TimeSpan totalWorldTime, Action<CommandResult> commandExecutedDelegate = null);
		void EnqueueCommandWithExecutionDelay(Command command, TimeSpan executionDelay, Action<CommandResult> commandExecutedDelegate = null);
		void CancelCommand(Command command);
		void CancelCommand(Guid commandId);
		void CancelCommands(IUnique context);
		bool CommandQueued(Command command);
		bool CommandQueued(Guid commandId);
	}
}