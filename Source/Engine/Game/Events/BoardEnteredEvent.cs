using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Events
{
	public class BoardEnteredEvent : Event
	{
		private readonly Board _currentBoard;
		private readonly Board _oldBoard;

		public BoardEnteredEvent(Board currentBoard, Board oldBoard)
		{
			currentBoard.ThrowIfNull("currentBoard");
			oldBoard.ThrowIfNull("oldBoard");

			_currentBoard = currentBoard;
			_oldBoard = oldBoard;
		}

		public Board CurrentBoard
		{
			get
			{
				return _currentBoard;
			}
		}

		public Board OldBoard
		{
			get
			{
				return _oldBoard;
			}
		}
	}
}