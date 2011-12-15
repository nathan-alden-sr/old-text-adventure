using System;
using System.Collections.Generic;

using Junior.Common;

namespace TextAdventure.Engine.Game.Commands
{
	public class RetryCommand : Command
	{
		private readonly Command _command;
		private int _completedAttempts;
		private bool _executingNestedCommand;
		private int _maximumAttempts;
		private CommandResult? _result;
		private TimeSpan _retryDelay;

		public RetryCommand(Command command, int maximumAttempts = MaximumAttempts.Infinite)
			: this(command, TimeSpan.Zero, maximumAttempts)
		{
		}

		public RetryCommand(Command command, double retryDelayInSeconds, int maximumAttempts = MaximumAttempts.Infinite)
			: this(command, TimeSpan.FromSeconds(retryDelayInSeconds), maximumAttempts)
		{
		}

		public RetryCommand(Command command, TimeSpan retryDelay, int maximumAttempts = MaximumAttempts.Infinite)
		{
			command.ThrowIfNull("command");

			_command = command;
			Delay(retryDelay);
			Times(maximumAttempts);
		}

		public override string Title
		{
			get
			{
				return _maximumAttempts > MaximumAttempts.Infinite
				       	? String.Format("Retried {0} of {1} time{2}", _completedAttempts, _maximumAttempts, _maximumAttempts == 1 ? "" : "s")
				       	: String.Format("Retried {0} time{1}", _completedAttempts, _completedAttempts == 1 ? "" : "s");
			}
		}

		public override IEnumerable<Command> NestedCommands
		{
			get
			{
				yield return _command;
			}
		}

		public RetryCommand InfiniteTimes()
		{
			_maximumAttempts = MaximumAttempts.Infinite;

			return this;
		}

		public RetryCommand Times(int maximumAttempts = MaximumAttempts.Infinite)
		{
			if (maximumAttempts < 0)
			{
				throw new ArgumentOutOfRangeException("maximumAttempts", "Total attempts must be at least 0 (infinite).");
			}
			_maximumAttempts = maximumAttempts;

			return this;
		}

		public RetryCommand NoDelay()
		{
			_retryDelay = TimeSpan.Zero;

			return this;
		}

		public RetryCommand Delay(TimeSpan retryDelay)
		{
			if (retryDelay < TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException("retryDelay", "Retry delay must be at least TimeSpan.Zero.");
			}
			_retryDelay = retryDelay;

			return this;
		}

		protected override void Reset()
		{
			_completedAttempts = 0;
			_result = null;
			_executingNestedCommand = false;

			base.Reset();
		}

		protected override CommandResult OnExecute(CommandContext context)
		{
			context.ThrowIfNull("worldInstance");

			if (_executingNestedCommand)
			{
				return CommandResult.Deferred;
			}
			if (_result != null)
			{
				return _result.Value;
			}

			_executingNestedCommand = true;

			context.EnqueueCommandWithExecutionDelay(_command, _completedAttempts == 0 ? TimeSpan.Zero : _retryDelay, NestedCommandExecuted);

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
					_result = ++_completedAttempts > 1 ? CommandResult.Succeeded : CommandResult.None;
					break;
				case CommandResult.Failed:
					_completedAttempts++;
					if (_maximumAttempts != MaximumAttempts.Infinite && _completedAttempts >= _maximumAttempts)
					{
						_result = CommandResult.Failed;
					}
					break;
			}

			_executingNestedCommand = false;
		}
	}
}