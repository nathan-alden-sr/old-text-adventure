using System;
using System.Collections.Generic;

using Junior.Common;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Commands
{
	public class ActorInstanceMoveCommand : ActorInstanceMoveCommandBase
	{
		private readonly MoveDirection _direction;

		public ActorInstanceMoveCommand(ActorInstance actorInstance, MoveDirection direction)
			: base(actorInstance)
		{
			_direction = direction;
		}

		public override IEnumerable<string> Details
		{
			get
			{
				foreach (string detail in base.Details)
				{
					yield return detail;
				}
				yield return "Direction: " + _direction;
			}
		}

		protected override Coordinate? GetDestinationCoordinate(CommandContext context)
		{
			context.ThrowIfNull("context");

			Coordinate coordinate = ActorInstance.Coordinate;

			switch (_direction)
			{
				case MoveDirection.Up:
					return new Coordinate(coordinate.X, coordinate.Y - 1);
				case MoveDirection.Down:
					return new Coordinate(coordinate.X, coordinate.Y + 1);
				case MoveDirection.Left:
					return new Coordinate(coordinate.X - 1, coordinate.Y);
				case MoveDirection.Right:
					return new Coordinate(coordinate.X + 1, coordinate.Y);
				default:
					throw new Exception(String.Format("Unexpected direction '{0}'.", _direction));
			}
		}
	}
}