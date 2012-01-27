using Junior.Common;

using TextAdventure.Xna;

namespace TextAdventure.WindowsGame.Updaters
{
	public class UpdaterParameters
	{
		private readonly Focus _focus;
		private readonly IXnaGameTime _gameTime;

		public UpdaterParameters(IXnaGameTime gameTime, Focus focus)
		{
			gameTime.ThrowIfNull("gameTime");

			_gameTime = gameTime;
			_focus = focus;
		}

		public IXnaGameTime GameTime
		{
			get
			{
				return _gameTime;
			}
		}

		public Focus Focus
		{
			get
			{
				return _focus;
			}
		}
	}
}