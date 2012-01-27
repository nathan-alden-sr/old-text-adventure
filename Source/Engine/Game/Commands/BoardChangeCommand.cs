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
		private readonly Board _destinationBoard;
		private readonly Coordinate _playerCoordinate;

		public BoardChangeCommand(Board destinationBoard, Coordinate playerCoordinate)
		{
			destinationBoard.ThrowIfNull("destinationBoard");

			_destinationBoard = destinationBoard;
			_playerCoordinate = playerCoordinate;
		}

		public override IEnumerable<string> Details
		{
			get
			{
				yield return FormatNamedObjectDetailText("Destination board", _destinationBoard);
				yield return "Player coordinate: " + _playerCoordinate;
			}
		}

		protected override CommandResult OnExecute(CommandContext context)
		{
			context.ThrowIfNull("context");

			Board oldBoard = context.CurrentBoard;

			if (context.RaiseEvent(oldBoard.BoardExitedEventHandler, new BoardExitedEvent(oldBoard, _destinationBoard)) == EventResult.Canceled)
			{
				return CommandResult.Failed;
			}

			CommandResult result = context.Player.ChangeLocation(_destinationBoard, _playerCoordinate) ? CommandResult.Succeeded : CommandResult.Failed;

			context.RaiseEvent(_destinationBoard.BoardEnteredEventHandler, new BoardEnteredEvent(oldBoard, _destinationBoard));

			return result;
		}
	}
}