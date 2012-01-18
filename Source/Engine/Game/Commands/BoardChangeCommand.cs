using System.Collections.Generic;

using Junior.Common;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Game.World;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Commands
{
	public class BoardChangeCommand : Command
	{
		private readonly Board _board;
		private readonly Coordinate _playerCoordinate;

		public BoardChangeCommand(Board board, Coordinate playerCoordinate)
		{
			board.ThrowIfNull("board");

			_board = board;
			_playerCoordinate = playerCoordinate;
		}

		public override IEnumerable<string> Details
		{
			get
			{
				yield return FormatNamedObjectDetailText("Changed to board", _board);
				yield return "Player coordinate: " + _playerCoordinate;
			}
		}

		protected override CommandResult OnExecute(CommandContext context)
		{
			context.ThrowIfNull("context");

			Board oldBoard = context.CurrentBoard;

			if (context.RaiseEvent(oldBoard.BoardExitedEventHandler, new BoardExitedEvent(oldBoard, _board)) == EventResult.Canceled)
			{
				return CommandResult.Failed;
			}

			CommandResult result = context.Player.ChangeLocation(_board, _playerCoordinate) ? CommandResult.Succeeded : CommandResult.Failed;

			context.RaiseEvent(_board.BoardEnteredEventHandler, new BoardEnteredEvent(oldBoard, _board));

			return result;
		}
	}
}