using Junior.Common;

namespace TextAdventure.Engine.Game.Commands
{
	public class StopSongCommand : Command
	{
		protected override CommandResult OnExecute(CommandContext context)
		{
			context.ThrowIfNull("context");

			context.MultimediaPlayer.StopSong();

			return CommandResult.Succeeded;
		}
	}
}