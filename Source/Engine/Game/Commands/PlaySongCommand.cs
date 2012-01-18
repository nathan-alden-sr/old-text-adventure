using System.Collections.Generic;

using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Commands
{
	public class PlaySongCommand : Command
	{
		private readonly SoundParameters _parameters;
		private readonly Song _song;

		public PlaySongCommand(Song song, SoundParameters parameters)
		{
			song.ThrowIfNull("song");
			parameters.ThrowIfNull("parameters");

			_song = song;
			_parameters = parameters;
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

			context.MultimediaPlayer.PlaySong(_song.Id, _song.Data, _parameters);

			return CommandResult.Succeeded;
		}
	}
}