using System;

namespace TextAdventure.Engine.Game.Commands
{
	public interface ICommandQueue
	{
		void EnqueueCommand(Command command, Action<CommandResult> commandExecutedDelegate = null);
		void EnqueueCommandToExecuteAtTime(Command command, TimeSpan totalWorldTime, Action<CommandResult> commandExecutedDelegate = null);
		void EnqueueCommandWithExecutionDelay(Command command, TimeSpan executionDelay, Action<CommandResult> commandExecutedDelegate = null);
		void CancelCommand(Command command);
		void CancelCommand(Guid commandId);
		void CancelCommands(Guid contextId);
		bool CommandQueued(Command command);
		bool CommandQueued(Guid commandId);
	}
}