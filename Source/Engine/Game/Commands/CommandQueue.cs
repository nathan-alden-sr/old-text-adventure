using System;

using Junior.Common;

using TextAdventure.Engine.Game.World;

namespace TextAdventure.Engine.Game.Commands
{
	public class CommandQueue
	{
		private readonly CommandList _commandList;
		private readonly CommandContext _context;
		private readonly WorldInstance _worldInstance;
		private readonly IWorldObserver _worldObserver;

		public CommandQueue(IWorldObserver worldObserver, WorldInstance worldInstance)
		{
			_worldObserver = worldObserver;
			_worldInstance = worldInstance;
			worldObserver.ThrowIfNull("worldObserver");
			worldInstance.ThrowIfNull("worldInstance");

			_context = new CommandContext(_worldInstance, this);
			_commandList = new CommandList(worldObserver, _context);
		}

		public void ExecuteCommand(Command command)
		{
			command.ThrowIfNull("command");

			_worldObserver.CommandExecuting(command);

			CommandResult result = command.Execute(_context);
			bool wasDeferred = result == CommandResult.Deferred;

			if (wasDeferred)
			{
				_commandList.Add(command, _worldInstance.WorldTime.Total);
			}
			else
			{
				_worldObserver.CommandExecuted(command, result);
			}
		}

		public void EnqueueCommand(Command command, Action<CommandResult> commandExecutedDelegate = null)
		{
			command.ThrowIfNull("command");

			_commandList.Add(command, _worldInstance.WorldTime.Total, commandExecutedDelegate);
		}

		public void EnqueueCommandToExecuteAtTime(Command command, TimeSpan totalWorldTime, Action<CommandResult> commandExecutedDelegate = null)
		{
			command.ThrowIfNull("command");

			_commandList.Add(command, totalWorldTime, commandExecutedDelegate);
		}

		public void EnqueueCommandWithExecutionDelay(Command command, TimeSpan executionDelay, Action<CommandResult> commandExecutedDelegate = null)
		{
			command.ThrowIfNull("command");

			_commandList.Add(command, _worldInstance.WorldTime.Total + executionDelay, commandExecutedDelegate);
		}

		public void CancelCommand(Command command)
		{
			command.ThrowIfNull("command");

			_commandList.Remove(command);
		}

		public void CancelCommand(Guid commandId)
		{
			_commandList.RemoveByCommandId(commandId);
		}

		public void CancelCommands(Guid tag)
		{
			_commandList.RemoveByTag(tag);
		}

		public bool CommandQueued(Command command)
		{
			command.ThrowIfNull("command");

			return _commandList.Contains(command);
		}

		public bool CommandQueued(Guid commandId)
		{
			return _commandList.Contains(commandId);
		}

		public void ProcessQueue()
		{
			_commandList.Execute();
		}
	}
}