using System;
using System.Collections.Generic;
using System.Linq;

using Junior.Common;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Commands
{
	public class ActorInstanceRandomMoveCommand : ActorInstanceMoveCommand
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

				yield return "Possible direction: " + directionText;
				if (_actualDirection != null)
				{
					yield return "Actual direction: " + GetDirectionText(_actualDirection.Value);
				}
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

		protected override Coordinate? ModifyCoordinate(Board board, Player player, int x, int y)
		{
			board.ThrowIfNull("board");
			player.ThrowIfNull("player");

			var directions = new List<RandomMoveDirection>();

			switch (_direction)
			{
				case RandomMoveDirection.Any:
					directions.Add(RandomMoveDirection.Up);
					directions.Add(RandomMoveDirection.Down);
					directions.Add(RandomMoveDirection.Left);
					directions.Add(RandomMoveDirection.Right);
					break;
				case RandomMoveDirection.AnyUnoccupied:
					if (x > 0 && !IsValidCoordinate(board, player, new Coordinate(x - 1, y)))
					{
						directions.Add(RandomMoveDirection.Left);
					}
					if (x < board.Size.Width - 1 && !IsValidCoordinate(board, player, new Coordinate(x + 1, y)))
					{
						directions.Add(RandomMoveDirection.Right);
					}
					if (y > 0 && !IsValidCoordinate(board, player, new Coordinate(x, y - 1)))
					{
						directions.Add(RandomMoveDirection.Up);
					}
					if (y < board.Size.Height - 1 && !IsValidCoordinate(board, player, new Coordinate(x, y + 1)))
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
					return new Coordinate(x, y - 1);
				case RandomMoveDirection.Down:
					return new Coordinate(x, y + 1);
				case RandomMoveDirection.Left:
					return new Coordinate(x - 1, y);
				case RandomMoveDirection.Right:
					return new Coordinate(x + 1, y);
				default:
					throw new Exception(String.Format("Unexpected direction '{0}'.", _actualDirection));
			}
		}
	}
}