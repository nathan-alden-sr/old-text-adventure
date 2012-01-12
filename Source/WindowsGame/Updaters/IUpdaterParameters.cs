using Microsoft.Xna.Framework;

namespace TextAdventure.WindowsGame.Updaters
{
	public interface IUpdaterParameters
	{
		GameTime GameTime
		{
			get;
		}
		Focus Focus
		{
			get;
		}
	}
}