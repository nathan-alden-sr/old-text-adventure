using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Commands
{
	public class StopTimerCommand : Command
	{
		private readonly Timer _timer;

		public StopTimerCommand(Timer timer)
		{
			timer.ThrowIfNull("timer");

			_timer = timer;
		}

		protected override CommandResult OnExecute(CommandContext context)
		{
			_timer.Stop();

			return CommandResult.Succeeded;
		}
	}
}