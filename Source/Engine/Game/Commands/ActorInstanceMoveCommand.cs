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

		protected ActorInstanceMoveCommand(ActorInstance actorInstance)
		{
			actorInstance.ThrowIfNull("actorInstance");

			_actorInstance = actorInstance;
		}

		public override IEnumerable<string> Details
		{
			get
			{
				yield return "Actor instance ID: " + _actorInstance.Id;
			}
		}

		protected override CommandResult OnExecute(CommandContext context)
		{
			Coordinate actorCoordinate = _actorInstance.Coordinate;
			Coordinate newCoordinate = ModifyCoordinate(actorCoordinate.X, actorCoordinate.Y);

			if (context.RaiseEvent(_actorInstance.ActorInstanceMovedEventHandler, new ActorInstanceMovedEvent(_actorInstance, newCoordinate)) == EventResult.Canceled)
			{
				return CommandResult.Failed;
			}

			return ProcessNewCoordinate(context, context.CurrentBoard, newCoordinate);
		}

		protected abstract Coordinate ModifyCoordinate(int x, int y);

		private CommandResult ProcessNewCoordinate(Context context, Board board, Coordinate newCoordinate)
		{
			if (!board.CoordinateIntersects(newCoordinate))
			{
				return CommandResult.Failed;
			}

			Coordinate currentCoordinate = _actorInstance.Coordinate;
			ActorInstance targetActorInstance = board.ActorInstanceLayer[newCoordinate];
			TouchDirection? touchDirection = GetTouchDirection(currentCoordinate, newCoordinate);

			if (targetActorInstance != null)
			{
				if (touchDirection != null)
				{
					context.RaiseEvent(targetActorInstance.ActorInstanceTouchedActorInstanceEventHandler, new ActorInstanceTouchedActorInstanceEvent(_actorInstance, targetActorInstance, touchDirection.Value));
				}

				return CommandResult.Failed;
			}
			if (context.Player.Coordinate == newCoordinate)
			{
				if (touchDirection != null)
				{
					context.RaiseEvent(context.Player.ActorInstanceTouchedPlayerEventHandler, new ActorInstanceTouchedPlayerEvent(_actorInstance, context.Player, touchDirection.Value));
				}

				return CommandResult.Failed;
			}

			return _actorInstance.ChangeCoordinate(context.CurrentBoard, context.Player, newCoordinate) ? CommandResult.Succeeded : CommandResult.Failed;
		}
	}
}