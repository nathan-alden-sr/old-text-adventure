using Junior.Common;

namespace TextAdventure.Engine.Game.Commands
{
	public class PlayerSuspendInputCommand : Command
	{
		protected override CommandResult OnExecute(CommandContext context)
		{
			context.ThrowIfNull("worldInstance");

			context.PlayerInput.Suspend();

			return CommandResult.Succeeded;
		}
	}
}