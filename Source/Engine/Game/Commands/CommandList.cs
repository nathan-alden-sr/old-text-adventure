using System;
using System.Collections.Generic;
using System.Linq;

using Junior.Common;

using TextAdventure.Engine.Game.World;

namespace TextAdventure.Engine.Game.Commands
{
	public class CommandList
	{
		private readonly List<CommandListEntry> _commands = new List<CommandListEntry>();
		private readonly CommandContext _context;
		private readonly List<CommandListEntry> _deferredCommands = new List<CommandListEntry>();
		private readonly IWorldObserver _worldObserver;

		public CommandList(IWorldObserver worldObserver, CommandContext context)
		{
			worldObserver.ThrowIfNull("worldObserver");
			context.ThrowIfNull("context");

			_worldObserver = worldObserver;
			_context = context;
		}

		public void Add(Command command, TimeSpan executeAtTime, Action<CommandResult> commandExecutedDelegate = null)
		{
			command.ThrowIfNull("command");

			_commands.Add(new CommandListEntry(command, executeAtTime, commandExecutedDelegate));
		}

		public void Execute()
		{
			IEnumerable<CommandListEntry> entries = _commands
				.Where(arg => arg.ExecuteAtTime <= _context.WorldTime.Total)
				.ToArray();

			foreach (CommandListEntry entry in entries)
			{
				bool wasCommandDeferred = _deferredCommands.Contains(entry);

				if (!wasCommandDeferred)
				{
					_worldObserver.CommandExecuting(entry.Command);
				}

				CommandResult result = entry.Command.Execute(_context);
				bool wasDeferred = result == CommandResult.Deferred;
				CommandListEntry tempEntry = entry;

				if (wasDeferred)
				{
					_deferredCommands.Add(entry);
				}
				else
				{
					_deferredCommands.Remove(tempEntry);
					_commands.Remove(tempEntry);
				}

				entry.ProcessResult(result);

				if (!wasDeferred)
				{
					_worldObserver.CommandExecuted(entry.Command, result);
				}
			}
		}

		public void Remove(Command command)
		{
			command.ThrowIfNull("command");

			_commands.RemoveAll(arg => arg.Command == command);
			foreach (Command nestedCommand in command.NestedCommands)
			{
				Remove(nestedCommand);
			}
		}

		public void RemoveByCommandId(Guid commandId)
		{
			Command command = _commands.FirstOrDefault(arg => arg.Command.Id == commandId).IfNotNull(arg => arg.Command);

			if (command != null)
			{
				Remove(command);
			}
		}

		public void RemoveByTag(Guid tag)
		{
			IEnumerable<Command> commandsWithTag = _commands
				.Where(arg => arg.Command.Tag == tag)
				.Select(arg => arg.Command)
				.ToArray();

			foreach (Command command in commandsWithTag)
			{
				Remove(command);
			}
		}

		public bool Contains(Command command)
		{
			command.ThrowIfNull("command");

			return _commands.Any(arg => arg.Command == command);
		}

		public bool Contains(Guid commandId)
		{
			return _commands.Any(arg => arg.Command.Id == commandId);
		}

		private class CommandListEntry
		{
			private readonly Command _command;
			private readonly Action<CommandResult> _commandExecutedDelegate;
			private readonly TimeSpan _executeAtTime;

			public CommandListEntry(Command command, TimeSpan executeAtTime, Action<CommandResult> commandExecutedDelegate = null)
			{
				_command = command;
				_executeAtTime = executeAtTime;
				_commandExecutedDelegate = commandExecutedDelegate;
			}

			public Command Command
			{
				get
				{
					return _command;
				}
			}

			public TimeSpan ExecuteAtTime
			{
				get
				{
					return _executeAtTime;
				}
			}

			public void ProcessResult(CommandResult result)
			{
				if (_commandExecutedDelegate != null)
				{
					_commandExecutedDelegate(result);
				}
			}
		}
	}
}