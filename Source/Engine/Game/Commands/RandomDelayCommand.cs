using System;

using Junior.Common;

namespace TextAdventure.Engine.Game.Commands
{
	public class RandomDelayCommand : Command
	{
		private static readonly Random _random = new Random();
		private readonly TimeSpan _maximumDelay;
		private readonly TimeSpan _minimumDelay;
		private TimeSpan _delay;
		private TimeSpan? _targetTotalWorldTime;

		public RandomDelayCommand(TimeSpan minimumDelay, TimeSpan maximumDelay)
		{
			if (minimumDelay < TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException("minimumDelay");
			}
			if (maximumDelay < minimumDelay)
			{
				throw new ArgumentOutOfRangeException("maximumDelay");
			}

			_minimumDelay = minimumDelay;
			_maximumDelay = maximumDelay;
		}

		public override string Title
		{
			get
			{
				return String.Format(@"Randomly delayed {0:h\:mm\:ss\.f}", _delay);
			}
		}

		protected override void Reset()
		{
			_targetTotalWorldTime = null;

			base.Reset();
		}

		protected override CommandResult OnExecute(CommandContext context)
		{
			context.ThrowIfNull("context");

			if (_targetTotalWorldTime == null)
			{
				double percentage = _random.NextDouble();

				_delay = TimeSpan.FromTicks((long)(_minimumDelay.Ticks + ((_maximumDelay.Ticks - _minimumDelay.Ticks) * percentage)));
				_targetTotalWorldTime = context.WorldTime.Total + _delay;
			}

			return context.WorldTime.Total >= _targetTotalWorldTime.Value ? CommandResult.Succeeded : CommandResult.Deferred;
		}
	}
}