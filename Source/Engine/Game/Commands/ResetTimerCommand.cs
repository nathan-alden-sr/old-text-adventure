using System.Collections.Generic;

using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Commands
{
	public class ResetTimerCommand : Command
	{
		private readonly Timer _timer;

		public ResetTimerCommand(Timer timer)
		{
			timer.ThrowIfNull("timer");

			_timer = timer;
		}

		public override IEnumerable<string> Details
		{
			get
			{
				yield return "ID: " + _timer.Id;
			}
		}

		protected override CommandResult OnExecute(CommandContext context)
		{
			_timer.Reset();

			return CommandResult.Succeeded;
		}
	}
}