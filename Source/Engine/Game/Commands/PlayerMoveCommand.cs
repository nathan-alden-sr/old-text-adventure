using System;
using System.Linq;

using Junior.Common;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Commands
{
	public abstract class PlayerMoveCommand : MoveCommand
	{
		protected override CommandResult OnExecute(CommandContext context)
		{
			context.ThrowIfNull("worldInstance");

			Coordinate playerCoordinate = context.Player.Coordinate;
			Coordinate newCoordinate = ModifyCoordinate(playerCoordinate.X, playerCoordinate.Y);

			return ProcessNewCoordinate(context, context.CurrentBoard, newCoordinate);
		}

		protected abstract Coordinate ModifyCoordinate(int x, int y);

		private CommandResult ProcessNewCoordinate(Context context, Board board, Coordinate newCoordinate)
		{
			Coordinate currentCoordinate = context.Player.Coordinate;
			BoardExitDirection boardExitDirection = GetBoardExitDirection(currentCoordinate, newCoordinate);
			BoardExit boardExit = board.Exits.SingleOrDefault(arg => arg.Coordinate == currentCoordinate && arg.Direction == boardExitDirection);

			if (boardExit != null)
			{
				Board destinationBoard = context.GetBoardById(boardExit.DestinationBoardId);

				context.EnqueueCommand(new BoardChangeCommand(destinationBoard, boardExit.DestinationCoordinate));

				return CommandResult.Succeeded;
			}

			if (!board.CoordinateIntersects(newCoordinate))
			{
				return CommandResult.Failed;
			}

			ActorInstance targetActorInstance = board.ActorInstanceLayer[newCoordinate];

			if (targetActorInstance != null)
			{
				TouchDirection? touchDirection = GetTouchDirection(currentCoordinate, newCoordinate);

				if (touchDirection != null)
				{
					context.RaiseEvent(targetActorInstance.PlayerTouchedActorInstanceEventHandler, new PlayerTouchedActorInstanceEvent(targetActorInstance, touchDirection.Value));
				}

				return CommandResult.Failed;
			}

			return context.Player.ChangeLocation(context.CurrentBoard, newCoordinate) ? CommandResult.Succeeded : CommandResult.Failed;
		}

		private static BoardExitDirection GetBoardExitDirection(Coordinate currentCoordinate, Coordinate newCoordinate)
		{
			if (newCoordinate.Y < currentCoordinate.Y)
			{
				return BoardExitDirection.Up;
			}
			if (newCoordinate.Y > currentCoordinate.Y)
			{
				return BoardExitDirection.Down;
			}
			if (newCoordinate.X < currentCoordinate.X)
			{
				return BoardExitDirection.Left;
			}
			if (newCoordinate.X > currentCoordinate.X)
			{
				return BoardExitDirection.Right;
			}

			throw new ArgumentException("newCoordinate");
		}
	}
}