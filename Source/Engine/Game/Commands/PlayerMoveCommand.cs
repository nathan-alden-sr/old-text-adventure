using System;
using System.Collections.Generic;
using System.Linq;

using Junior.Common;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Commands
{
	public class PlayerMoveCommand : MoveCommand
	{
		private readonly MoveDirection _direction;
		private Coordinate? _destinationCoordinate;

		public PlayerMoveCommand(MoveDirection direction)
		{
			_direction = direction;
		}

		public override IEnumerable<string> Details
		{
			get
			{
				if (_destinationCoordinate != null)
				{
					yield return "Destination coordinate: " + _destinationCoordinate.Value;
				}
			}
		}

		protected override CommandResult OnExecute(CommandContext context)
		{
			context.ThrowIfNull("worldInstance");

			Coordinate coordinate = context.Player.Coordinate;

			switch (_direction)
			{
				case MoveDirection.Up:
					_destinationCoordinate = new Coordinate(coordinate.X, coordinate.Y - 1);
					break;
				case MoveDirection.Down:
					_destinationCoordinate = new Coordinate(coordinate.X, coordinate.Y + 1);
					break;
				case MoveDirection.Left:
					_destinationCoordinate = new Coordinate(coordinate.X - 1, coordinate.Y);
					break;
				case MoveDirection.Right:
					_destinationCoordinate = new Coordinate(coordinate.X + 1, coordinate.Y);
					break;
				default:
					throw new Exception(String.Format("Unexpected direction '{0}'", _direction));
			}

			return ProcessDestinationCoordinate(context, context.CurrentBoard, _destinationCoordinate.Value);
		}

		private CommandResult ProcessDestinationCoordinate(Context context, Board board, Coordinate destinationCoordinate)
		{
			Coordinate currentCoordinate = context.Player.Coordinate;
			BoardExitDirection boardExitDirection = GetBoardExitDirection(currentCoordinate, destinationCoordinate);
			BoardExit boardExit = board.Exits.SingleOrDefault(arg => arg.Coordinate == currentCoordinate && arg.Direction == boardExitDirection);

			if (boardExit != null)
			{
				Board destinationBoard = context.GetBoardById(boardExit.DestinationBoardId);

				context.EnqueueCommand(new BoardChangeCommand(destinationBoard, boardExit.DestinationCoordinate));

				return CommandResult.Succeeded;
			}

			if (!board.CoordinateIntersects(destinationCoordinate))
			{
				return CommandResult.Failed;
			}

			ActorInstance targetActorInstance = board.ActorInstanceLayer[destinationCoordinate];

			if (targetActorInstance != null)
			{
				TouchDirection? touchDirection = GetTouchDirection(currentCoordinate, destinationCoordinate);

				if (touchDirection != null)
				{
					context.RaiseEvent(targetActorInstance.OnTouchedByPlayer, new PlayerTouchedActorInstanceEvent(targetActorInstance, touchDirection.Value));
				}
			}

			return context.Player.ChangeLocation(context.CurrentBoard, destinationCoordinate) ? CommandResult.Succeeded : CommandResult.Failed;
		}

		private static BoardExitDirection GetBoardExitDirection(Coordinate currentCoordinate, Coordinate destinationCoordinate)
		{
			if (destinationCoordinate.Y < currentCoordinate.Y)
			{
				return BoardExitDirection.Up;
			}
			if (destinationCoordinate.Y > currentCoordinate.Y)
			{
				return BoardExitDirection.Down;
			}
			if (destinationCoordinate.X < currentCoordinate.X)
			{
				return BoardExitDirection.Left;
			}
			if (destinationCoordinate.X > currentCoordinate.X)
			{
				return BoardExitDirection.Right;
			}

			throw new ArgumentException("_destinationCoordinate");
		}
	}
}