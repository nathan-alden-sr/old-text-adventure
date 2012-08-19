using System;
using System.Collections.Generic;

using Junior.Common;

namespace TextAdventure.Engine.Game.Commands
{
	public class RepeatCommand : Command
	{
		private readonly Command _command;
		private bool _executingNestedCommand;
		private TimeSpan _repeatDelay;
		private int _repeats;
		private CommandResult? _result;
		private int _totalRepeats;

		public RepeatCommand(Command command, int totalRepeats = TotalRepeats.Infinite)
			: this(command, TimeSpan.Zero, totalRepeats)
		{
		}

		public RepeatCommand(Command command, double repeatDelayInSeconds, int totalRepeats = TotalRepeats.Infinite)
			: this(command, TimeSpan.FromSeconds(repeatDelayInSeconds), totalRepeats)
		{
		}

		public RepeatCommand(Command command, TimeSpan repeatDelay, int totalRepeats = TotalRepeats.Infinite)
		{
			command.ThrowIfNull("command");

			_command = command;
			Delay(repeatDelay);
			Times(totalRepeats);
		}

		public override string Title
		{
			get
			{
				return _totalRepeats > MaximumAttempts.Infinite
					       ? String.Format("Repeated {0} of {1} time{2}", _repeats, _totalRepeats, _totalRepeats == 1 ? "" : "s")
					       : String.Format("Repeated {0} time{1}", _repeats, _repeats == 1 ? "" : "s");
			}
		}

		public override IEnumerable<Command> NestedCommands
		{
			get
			{
				yield return _command;
			}
		}

		public RepeatCommand InfiniteTimes()
		{
			_totalRepeats = TotalRepeats.Infinite;

			return this;
		}

		public RepeatCommand Times(int totalRepeats)
		{
			if (totalRepeats < 0)
			{
				throw new ArgumentOutOfRangeException("totalRepeats", "Total repeats must be at least 0 (infinite).");
			}
			_totalRepeats = totalRepeats;

			return this;
		}

		public RepeatCommand NoDelay()
		{
			_repeatDelay = TimeSpan.Zero;

			return this;
		}

		public RepeatCommand Delay(TimeSpan repeatDelay)
		{
			if (repeatDelay < TimeSpan.Zero)
			{
				throw new ArgumentException("Repeat delay must be at least 0 (infinite).", "repeatDelay");
			}
			_repeatDelay = repeatDelay;

			return this;
		}

		protected override void Reset()
		{
			_repeats = 0;
			_result = null;
			_executingNestedCommand = false;

			base.Reset();
		}

		protected override CommandResult OnExecute(CommandContext context)
		{
			context.ThrowIfNull("context");

			if (_executingNestedCommand)
			{
				return CommandResult.Deferred;
			}
			if (_result != null)
			{
				return _result.Value;
			}

			_executingNestedCommand = true;

			context.EnqueueCommandWithExecutionDelay(_command, _repeats == 0 ? TimeSpan.Zero : _repeatDelay, NestedCommandExecuted);

			return CommandResult.Deferred;
		}

		private void NestedCommandExecuted(CommandResult result)
		{
			switch (result)
			{
				case CommandResult.Deferred:
					return;
				case CommandResult.None:
				case CommandResult.Succeeded:
					_result = _totalRepeats > TotalRepeats.Infinite && ++_repeats == _totalRepeats ? CommandResult.Succeeded : (CommandResult?)null;
					break;
				case CommandResult.Failed:
					_result = CommandResult.Failed;
					break;
			}

			_executingNestedCommand = false;
		}
	}
}