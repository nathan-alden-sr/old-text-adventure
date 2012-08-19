using System.Collections.Generic;

using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Events
{
	public class BoardEnteredEvent : Event
	{
		private readonly Board _newBoard;
		private readonly Board _oldBoard;

		public BoardEnteredEvent(Board oldBoard, Board newBoard)
		{
			newBoard.ThrowIfNull("NewBoard");
			oldBoard.ThrowIfNull("oldBoard");

			_newBoard = newBoard;
			_oldBoard = oldBoard;
		}

		public Board OldBoard
		{
			get
			{
				return _oldBoard;
			}
		}

		public Board NewBoard
		{
			get
			{
				return _newBoard;
			}
		}

		public override IEnumerable<string> Details
		{
			get
			{
				yield return FormatNamedObjectDetailText("Old board", OldBoard);
				yield return FormatNamedObjectDetailText("New board", NewBoard);
			}
		}
	}
}