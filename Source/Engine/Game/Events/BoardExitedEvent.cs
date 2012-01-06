using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Events
{
	public class BoardExitedEvent : Event
	{
		private readonly IBoard _currentBoard;
		private readonly IBoard _newBoard;

		public BoardExitedEvent(IBoard currentBoard, IBoard newBoard)
		{
			currentBoard.ThrowIfNull("currentBoard");
			newBoard.ThrowIfNull("newBoard");

			_currentBoard = currentBoard;
			_newBoard = newBoard;
		}

		public IBoard CurrentBoard
		{
			get
			{
				return _currentBoard;
			}
		}

		public IBoard NewBoard
		{
			get
			{
				return _newBoard;
			}
		}
	}
}