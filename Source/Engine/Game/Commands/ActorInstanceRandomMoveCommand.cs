using System;
using System.Collections.Generic;
using System.Linq;

using Junior.Common;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Commands
{
	public class ActorInstanceRandomMoveCommand : ActorInstanceMoveCommandBase
	{
		private static readonly Random _random = new Random();
		private readonly RandomMoveDirection _direction;
		private RandomMoveDirection? _actualDirection;

		public ActorInstanceRandomMoveCommand(ActorInstance actorInstance, RandomMoveDirection direction = RandomMoveDirection.AnyUnoccupied)
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

				string directionText = GetDirectionText(_direction);

				yield return "Possible directions: " + directionText;
				if (_actualDirection != null)
				{
					yield return "Actual direction: " + GetDirectionText(_actualDirection.Value);
				}
			}
		}

		protected override Coordinate? GetDestinationCoordinate(CommandContext context)
		{
			context.ThrowIfNull("context");

			var directions = new List<RandomMoveDirection>();
			Coordinate coordinate = ActorInstance.Coordinate;
			var leftCoordinate = new Coordinate(coordinate.X - 1, coordinate.Y);
			var rightCoordinate = new Coordinate(coordinate.X + 1, coordinate.Y);
			var upCoordinate = new Coordinate(coordinate.X, coordinate.Y - 1);
			var downCoordinate = new Coordinate(coordinate.X, coordinate.Y + 1);
			Board board = context.GetBoardById(ActorInstance.BoardId);

			switch (_direction)
			{
				case RandomMoveDirection.Any:
					directions.Add(RandomMoveDirection.Up);
					directions.Add(RandomMoveDirection.Down);
					directions.Add(RandomMoveDirection.Left);
					directions.Add(RandomMoveDirection.Right);
					break;
				case RandomMoveDirection.AnyUnoccupied:
					if (coordinate.X > 0 && IsValidDestinationCoordinate(context, leftCoordinate))
					{
						directions.Add(RandomMoveDirection.Left);
					}
					if (coordinate.X < board.Size.Width - 1 && IsValidDestinationCoordinate(context, rightCoordinate))
					{
						directions.Add(RandomMoveDirection.Right);
					}
					if (coordinate.Y > 0 && IsValidDestinationCoordinate(context, upCoordinate))
					{
						directions.Add(RandomMoveDirection.Up);
					}
					if (coordinate.Y < board.Size.Height - 1 && IsValidDestinationCoordinate(context, downCoordinate))
					{
						directions.Add(RandomMoveDirection.Down);
					}
					break;
				default:
					directions.Add(_direction);
					break;
			}

			if (!directions.Any())
			{
				return null;
			}

			_actualDirection = directions[_random.Next(0, directions.Count)];

			switch (_actualDirection)
			{
				case RandomMoveDirection.Up:
					return upCoordinate;
				case RandomMoveDirection.Down:
					return downCoordinate;
				case RandomMoveDirection.Left:
					return leftCoordinate;
				case RandomMoveDirection.Right:
					return rightCoordinate;
				default:
					throw new Exception(String.Format("Unexpected direction '{0}'.", _actualDirection));
			}
		}

		private static string GetDirectionText(RandomMoveDirection direction)
		{
			string directionText;

			switch (direction)
			{
				case RandomMoveDirection.AnyUnoccupied:
					directionText = "Any unoccupied";
					break;
				default:
					directionText = direction.ToString();
					break;
			}
			return directionText;
		}
	}
}