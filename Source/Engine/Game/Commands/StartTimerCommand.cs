using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Commands
{
	public class StartTimerCommand : Command
	{
		private readonly Timer _timer;

		public StartTimerCommand(Timer timer)
		{
			timer.ThrowIfNull("timer");

			_timer = timer;
		}

		protected override CommandResult OnExecute(CommandContext context)
		{
			_timer.Start();

			return CommandResult.Succeeded;
		}
	}
}