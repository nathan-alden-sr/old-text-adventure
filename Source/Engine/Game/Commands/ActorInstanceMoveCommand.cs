using System.Collections.Generic;

using Junior.Common;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Game.World;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Commands
{
	public abstract class ActorInstanceMoveCommand : MoveCommand
	{
		private readonly ActorInstance _actorInstance;
		private Coordinate? _destinationCoordinate;

		protected ActorInstanceMoveCommand(ActorInstance actorInstance)
		{
			actorInstance.ThrowIfNull("actorInstance");

			_actorInstance = actorInstance;
		}

		public override IEnumerable<string> Details
		{
			get
			{
				yield return FormatNamedObjectDetailText("Actor instance", _actorInstance);
				if (_destinationCoordinate != null)
				{
					yield return "Destination coordinate: " + _destinationCoordinate.Value;
				}
			}
		}

		protected override CommandResult OnExecute(CommandContext context)
		{
			context.ThrowIfNull("context");

			Coordinate actorCoordinate = _actorInstance.Coordinate;

			_destinationCoordinate = ModifyCoordinate(actorCoordinate.X, actorCoordinate.Y);

			if (context.RaiseEvent(_actorInstance.ActorInstanceMovedEventHandler, new ActorInstanceMovedEvent(_actorInstance, _destinationCoordinate.Value)) == EventResult.Canceled)
			{
				return CommandResult.Failed;
			}

			return ProcessNewCoordinate(context, context.CurrentBoard, _destinationCoordinate.Value);
		}

		protected abstract Coordinate ModifyCoordinate(int x, int y);

		private CommandResult ProcessNewCoordinate(Context context, Board board, Coordinate destinationCoordinate)
		{
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
					context.RaiseEvent(targetActorInstance.ActorInstanceTouchedActorInstanceEventHandler, new ActorInstanceTouchedActorInstanceEvent(_actorInstance, targetActorInstance, touchDirection.Value));
				}

				return CommandResult.Failed;
			}
			if (context.Player.Coordinate == destinationCoordinate)
			{
				if (touchDirection != null)
				{
					context.RaiseEvent(context.Player.ActorInstanceTouchedPlayerEventHandler, new ActorInstanceTouchedPlayerEvent(_actorInstance, context.Player, touchDirection.Value));
				}

				return CommandResult.Failed;
			}

			return _actorInstance.ChangeCoordinate(context.CurrentBoard, context.Player, destinationCoordinate) ? CommandResult.Succeeded : CommandResult.Failed;
		}
	}
}