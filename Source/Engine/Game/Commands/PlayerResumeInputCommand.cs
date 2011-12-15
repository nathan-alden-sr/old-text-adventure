using Junior.Common;

namespace TextAdventure.Engine.Game.Commands
{
	public class PlayerResumeInputCommand : Command
	{
		protected override CommandResult OnExecute(CommandContext context)
		{
			context.ThrowIfNull("worldInstance");

			context.PlayerInput.Resume();

			return CommandResult.Succeeded;
		}
	}
}