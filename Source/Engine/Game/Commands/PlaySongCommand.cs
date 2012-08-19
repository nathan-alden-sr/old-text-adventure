using System.Collections.Generic;

using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Commands
{
	public class PlaySongCommand : Command
	{
		private readonly Song _song;
		private readonly Volume _volume;

		public PlaySongCommand(Song song)
			: this(song, Volume.Full)
		{
		}

		public PlaySongCommand(Song song, Volume volume)
		{
			song.ThrowIfNull("song");

			_song = song;
			_volume = volume;
		}

		public override IEnumerable<string> Details
		{
			get
			{
				yield return FormatNamedObjectDetailText("Song", _song);
			}
		}

		protected override CommandResult OnExecute(CommandContext context)
		{
			context.ThrowIfNull("context");

			context.MultimediaPlayer.PlaySong(_song.Id, _song.Data, _volume);

			return CommandResult.Succeeded;
		}
	}
}