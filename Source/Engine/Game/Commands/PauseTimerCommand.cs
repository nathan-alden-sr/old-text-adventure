using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Commands
{
	public class PauseTimerCommand : Command
	{
		private readonly Timer _timer;

		public PauseTimerCommand(Timer timer)
		{
			timer.ThrowIfNull("timer");

			_timer = timer;
		}

		protected override CommandResult OnExecute(CommandContext context)
		{
			_timer.Pause();

			return CommandResult.Succeeded;
		}
	}
}