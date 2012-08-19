using System;
using System.Collections.Generic;
using System.Linq;

using Junior.Common;

namespace TextAdventure.Engine.Game.Commands
{
	public class ContextCommand : Command
	{
		private readonly Func<CommandContext, Command> _commandDelegate;
		private Command _command;

		public ContextCommand(Func<CommandContext, Command> commandDelegate)
		{
			commandDelegate.ThrowIfNull("commandDelegate");

			_commandDelegate = commandDelegate;
		}

		public override IEnumerable<string> Details
		{
			get
			{
				return _command.IfNotNull(arg => arg.Details) ?? Enumerable.Empty<string>();
			}
		}

		public override string Title
		{
			get
			{
				return _command.IfNotNull(arg => arg.Title) ?? base.Title;
			}
		}

		public override IEnumerable<Command> NestedCommands
		{
			get
			{
				return _command.IfNotNull(arg => arg.NestedCommands) ?? Enumerable.Empty<Command>();
			}
		}

		protected override CommandResult OnExecute(CommandContext context)
		{
			_command = _commandDelegate(context);

			return _command.Execute(context);
		}
	}
}