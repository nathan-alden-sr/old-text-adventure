using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.WindowsGame.RendererStates
{
	public class BoardRendererState : IBoardRendererState
	{
		private readonly Player _player;
		private Board _board;

		public BoardRendererState(Player player)
		{
			player.ThrowIfNull("player");

			_player = player;
		}

		public Board Board
		{
			get
			{
				return _board;
			}
			set
			{
				value.ThrowIfNull("value");

				_board = value;
			}
		}

		public Player Player
		{
			get
			{
				return _player;
			}
		}
	}
}