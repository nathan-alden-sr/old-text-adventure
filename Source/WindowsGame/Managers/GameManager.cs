using Junior.Common;

using Microsoft.Xna.Framework;

namespace TextAdventure.WindowsGame.Managers
{
	public class GameManager
	{
		private readonly Game _game;
		private readonly InputFocusManager _inputFocusManager = new InputFocusManager();

		public GameManager(Game game)
		{
			game.ThrowIfNull("game");

			_game = game;
		}

		public Game Game
		{
			get
			{
				return _game;
			}
		}

		public InputFocusManager InputFocusManager
		{
			get
			{
				return _inputFocusManager;
			}
		}
	}
}