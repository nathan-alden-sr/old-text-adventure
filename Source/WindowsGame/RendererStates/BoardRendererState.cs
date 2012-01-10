using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.WindowsGame.RendererStates
{
	public class BoardRendererState : IBoardRendererState
	{
		private readonly IPlayer _player;

		public BoardRendererState(IPlayer player)
		{
			player.ThrowIfNull("player");

			_player = player;
		}

		private IBoard _board;
		public IBoard Board
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

		public IPlayer Player
		{
			get
			{
				return _player;
			}
		}
	}
}