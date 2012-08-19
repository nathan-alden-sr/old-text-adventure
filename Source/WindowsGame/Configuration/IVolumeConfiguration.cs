using TextAdventure.Engine.Game.Commands;

namespace TextAdventure.WindowsGame.Configuration
{
	public interface IVolumeConfiguration
	{
		Volume SoundEffects
		{
			get;
		}
		Volume Music
		{
			get;
		}
	}
}