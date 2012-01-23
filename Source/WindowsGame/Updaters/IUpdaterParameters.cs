using TextAdventure.Xna;

namespace TextAdventure.WindowsGame.Updaters
{
	public interface IUpdaterParameters
	{
		IXnaGameTime GameTime
		{
			get;
		}
		Focus Focus
		{
			get;
		}
	}
}