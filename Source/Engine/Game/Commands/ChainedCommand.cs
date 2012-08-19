using System;
using System.Collections.Generic;
using System.Linq;

using Junior.Common;

namespace TextAdventure.Engine.Game.Commands
{
	public class ChainedCommand : Command
	{
		private readonly Queue<Command> _commandQueue = new Queue<Command>();
		private readonly List<Command> _originalCommands = new List<Command>();
		private int _executedCommandTotal;
		private bool _executingNestedCommand;

		public ChainedCommand(Command command)
		{
			command.ThrowIfNull("command");

			AddCommand(command);
		}

		public override string Title
		{
			get
			{
				return String.Format("Chained {0} command{1}", _executedCommandTotal, _executedCommandTotal == 1 ? "" : "s");
			}
		}

		public override IEnumerable<Command> NestedCommands
		{
			get
			{
				return _originalCommands.AsReadOnly();
			}
		}

		public override ChainedCommand And(Command command)
		{
			command.ThrowIfNull("command");

			AddCommand(command);

			return this;
		}

		protected override void Reset()
		{
			_executedCommandTotal = 0;
			_executingNestedCommand = false;
			_commandQueue.Clear();
			foreach (Command command in _originalCommands)
			{
				_commandQueue.Enqueue(command);
			}

			base.Reset();
		}

		protected override CommandResult OnExecute(CommandContext context)
		{
			context.ThrowIfNull("context");

			if (!_executingNestedCommand && _commandQueue.Any())
			{
				_executingNestedCommand = true;

				Command command = _commandQueue.Peek();

				context.EnqueueCommand(command, NestedCommandExecuted);
			}

			return _executingNestedCommand ? CommandResult.Deferred : CommandResult.Succeeded;
		}

		private void AddCommand(Command command)
		{
			_originalCommands.Add(command);
		}

		private void NestedCommandExecuted(CommandResult result)
		{
			if (result == CommandResult.Deferred)
			{
				return;
			}

			_commandQueue.Dequeue();
			_executingNestedCommand = false;
			_executedCommandTotal++;
		}
	}
}