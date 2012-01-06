using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Events
{
	public class BoardEnteredEvent : Event
	{
		private readonly IBoard _currentBoard;
		private readonly IBoard _oldBoard;

		public BoardEnteredEvent(IBoard currentBoard, IBoard oldBoard)
		{
			currentBoard.ThrowIfNull("currentBoard");
			oldBoard.ThrowIfNull("oldBoard");

			_currentBoard = currentBoard;
			_oldBoard = oldBoard;
		}

		public IBoard CurrentBoard
		{
			get
			{
				return _currentBoard;
			}
		}

		public IBoard OldBoard
		{
			get
			{
				return _oldBoard;
			}
		}
	}
}