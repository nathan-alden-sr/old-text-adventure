using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Commands
{
	public class PlaySongCommand : Command
	{
		private readonly Song _song;

		public PlaySongCommand(Song song)
		{
			song.ThrowIfNull("song");

			_song = song;
		}

		protected override CommandResult OnExecute(CommandContext context)
		{
			context.ThrowIfNull("context");

			context.MultimediaPlayer.PlaySong(_song.Id, _song.Data);

			return CommandResult.Succeeded;
		}
	}
}