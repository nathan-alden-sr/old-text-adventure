using System;

namespace TextAdventure.Engine.Game.Commands
{
	public class DelayCommand : Command
	{
		private readonly TimeSpan _delay;
		private TimeSpan? _targetTotalWorldTime;

		public DelayCommand(TimeSpan delay)
		{
			if (delay <= TimeSpan.Zero)
			{
				throw new ArgumentException("Delay must be greater than 0.", "delay");
			}

			_delay = delay;
		}

		public override string Title
		{
			get
			{
				return String.Format(@"Delayed {0:h\:mm\:ss\.f}", _delay);
			}
		}

		protected override void Reset()
		{
			_targetTotalWorldTime = null;

			base.Reset();
		}

		protected override CommandResult OnExecute(CommandContext context)
		{
			if (_targetTotalWorldTime == null)
			{
				_targetTotalWorldTime = context.WorldTime.Total + _delay;
			}

			return context.WorldTime.Total >= _targetTotalWorldTime.Value ? CommandResult.Succeeded : CommandResult.Deferred;
		}
	}
}