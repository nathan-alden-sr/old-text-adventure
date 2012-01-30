using System;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Samples.Ctxna.Songs
{
	public class FlourishSong : Song
	{
		public static readonly Guid SongId = Guid.Parse("ea6e12b2-b2a6-4ba6-8541-c184a3fbef65");

		public FlourishSong()
			: base(SongId, "Flourish", "", Songs.flourish)
		{
		}
	}
}