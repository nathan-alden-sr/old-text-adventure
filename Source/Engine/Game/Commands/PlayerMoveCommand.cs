using System;
using System.Collections.Generic;
using System.Linq;

using Junior.Common;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Commands
{
	public abstract class PlayerMoveCommand : MoveCommand
	{
		private Coordinate? _destinationCoordinate;

		public override IEnumerable<string> Details
		{
			get
			{
				return _destinationCoordinate.IfNotNull<Coordinate, string[]>(arg => new[] { "New coordinate: " + _destinationCoordinate.Value }) ?? base.Details;
			}
		}

		protected override CommandResult OnExecute(CommandContext context)
		{
			context.ThrowIfNull("worldInstance");

			Coordinate playerCoordinate = context.Player.Coordinate;

			_destinationCoordinate = ModifyCoordinate(playerCoordinate.X, playerCoordinate.Y);

			return ProcessDestinationCoordinate(context, context.CurrentBoard, _destinationCoordinate.Value);
		}

		protected abstract Coordinate ModifyCoordinate(int x, int y);

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
					context.RaiseEvent(targetActorInstance.PlayerTouchedActorInstanceEventHandler, new PlayerTouchedActorInstanceEvent(targetActorInstance, touchDirection.Value));
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