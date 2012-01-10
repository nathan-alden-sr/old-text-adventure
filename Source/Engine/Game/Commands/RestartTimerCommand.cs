using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Commands
{
	public class RestartTimerCommand : Command
	{
		private readonly Timer _timer;

		public RestartTimerCommand(Timer timer)
		{
			timer.ThrowIfNull("timer");

			_timer = timer;
		}

		protected override CommandResult OnExecute(CommandContext context)
		{
			_timer.Restart();

			return CommandResult.Succeeded;
		}
	}
}