using System.Collections.Generic;
using System.Linq;

using Junior.Common;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Game.World;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Commands
{
	public abstract class ActorInstanceMoveCommandBase : MoveCommand
	{
		private readonly ActorInstance _actorInstance;
		private Coordinate? _destinationCoordinate;

		protected ActorInstanceMoveCommandBase(ActorInstance actorInstance)
		{
			actorInstance.ThrowIfNull("actorInstance");

			_actorInstance = actorInstance;
		}

		public ActorInstance ActorInstance
		{
			get
			{
				return _actorInstance;
			}
		}

		public override IEnumerable<string> Details
		{
			get
			{
				yield return FormatNamedObjectDetailText("Actor instance", _actorInstance);
				if (_destinationCoordinate != null)
				{
					yield return "Destination coordinate: " + _destinationCoordinate;
				}
			}
		}

		protected override sealed CommandResult OnExecute(CommandContext context)
		{
			context.ThrowIfNull("context");

			_destinationCoordinate = GetDestinationCoordinate(context);

			return _destinationCoordinate != null ? MoveActorInstance(context, _destinationCoordinate.Value) : CommandResult.Failed;
		}

		protected bool IsValidDestinationCoordinate(CommandContext context, Coordinate destinationCoordinate)
		{
			context.ThrowIfNull("context");

			Board board = context.GetBoardById(_actorInstance.BoardId);
			Player player = context.Player;
			var exitUpCoordinate = new Coordinate(destinationCoordinate.X, destinationCoordinate.Y + 1);
			var exitDownCoordinate = new Coordinate(destinationCoordinate.X, destinationCoordinate.Y - 1);
			var exitLeftCoordinate = new Coordinate(destinationCoordinate.X + 1, destinationCoordinate.Y);
			var exitRightCoordinate = new Coordinate(destinationCoordinate.X - 1, destinationCoordinate.Y);
			bool invalidCoordinate =
				board.IsCoordinateOccupied(destinationCoordinate) ||
				(player.BoardId == board.Id && player.Coordinate == destinationCoordinate) ||
				board.Exits.Any(arg => arg.Coordinate == exitUpCoordinate && arg.Direction == BoardExitDirection.Up) ||
				board.Exits.Any(arg => arg.Coordinate == exitDownCoordinate && arg.Direction == BoardExitDirection.Down) ||
				board.Exits.Any(arg => arg.Coordinate == exitLeftCoordinate && arg.Direction == BoardExitDirection.Left) ||
				board.Exits.Any(arg => arg.Coordinate == exitRightCoordinate && arg.Direction == BoardExitDirection.Right);

			return !invalidCoordinate;
		}

		protected abstract Coordinate? GetDestinationCoordinate(CommandContext context);

		private CommandResult MoveActorInstance(Context context, Coordinate destinationCoordinate)
		{
			Board board = context.GetBoardById(_actorInstance.BoardId);

			if (!board.CoordinateIntersects(destinationCoordinate))
			{
				return CommandResult.Failed;
			}

			Coordinate currentCoordinate = _actorInstance.Coordinate;
			ActorInstance targetActorInstance = board.ActorInstanceLayer[destinationCoordinate];
			TouchDirection? touchDirection = GetTouchDirection(currentCoordinate, destinationCoordinate);

			if (targetActorInstance != null)
			{
				if (touchDirection != null)
				{
					context.RaiseEvent(targetActorInstance.OnTouchedByActorInstance, new ActorInstanceTouchedActorInstanceEvent(_actorInstance, targetActorInstance, touchDirection.Value));
				}

				return CommandResult.Failed;
			}
			if (context.Player.Coordinate == destinationCoordinate)
			{
				if (touchDirection != null)
				{
					context.RaiseEvent(context.Player.OnTouchedByActorInstance, new ActorInstanceTouchedPlayerEvent(_actorInstance, context.Player, touchDirection.Value));
				}

				return CommandResult.Failed;
			}
			if (!_actorInstance.ChangeCoordinate(context.CurrentBoard, context.Player, destinationCoordinate))
			{
				return CommandResult.Failed;
			}

			return context.RaiseEvent(_actorInstance.OnMoved, new ActorInstanceMovedEvent(_actorInstance, destinationCoordinate)) == EventResult.Canceled ? CommandResult.Failed : CommandResult.Succeeded;
		}
	}
}