using System;

namespace TextAdventure.Engine.Game.World
{
	public interface IMultimediaPlayer
	{
		void PlaySoundEffect(Guid id, byte[] data);
	}
}