using System.Collections.Generic;

using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Events
{
	public class BoardExitedEvent : Event
	{
		private readonly Board _newBoard;
		private readonly Board _oldBoard;

		public BoardExitedEvent(Board oldBoard, Board newBoard)
		{
			oldBoard.ThrowIfNull("OldBoard");
			newBoard.ThrowIfNull("newBoard");

			_oldBoard = oldBoard;
			_newBoard = newBoard;
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
				yield return FormatNamedObjectDetailText("Old board:", OldBoard);
				yield return FormatNamedObjectDetailText("New board:", NewBoard);
			}
		}
	}
}