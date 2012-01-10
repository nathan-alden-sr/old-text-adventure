using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Events
{
	public class BoardExitedEvent : Event
	{
		private readonly Board _currentBoard;
		private readonly Board _newBoard;

		public BoardExitedEvent(Board currentBoard, Board newBoard)
		{
			currentBoard.ThrowIfNull("currentBoard");
			newBoard.ThrowIfNull("newBoard");

			_currentBoard = currentBoard;
			_newBoard = newBoard;
		}

		public Board CurrentBoard
		{
			get
			{
				return _currentBoard;
			}
		}

		public Board NewBoard
		{
			get
			{
				return _newBoard;
			}
		}
	}
}