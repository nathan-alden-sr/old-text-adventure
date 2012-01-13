using TextAdventure.WindowsGame.Xna;

namespace TextAdventure.WindowsGame.Updaters
{
	public interface IUpdaterParameters
	{
		XnaGameTime GameTime
		{
			get;
		}
		Focus Focus
		{
			get;
		}
	}
}