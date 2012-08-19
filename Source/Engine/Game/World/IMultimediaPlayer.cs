using System;

using TextAdventure.Engine.Game.Commands;

namespace TextAdventure.Engine.Game.World
{
	public interface IMultimediaPlayer
	{
		void PlaySoundEffect(Guid id, byte[] data, Volume volume);
		void PlaySong(Guid id, byte[] data, Volume volume);
		void StopSong();
	}
}