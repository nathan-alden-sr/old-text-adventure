using System;
using System.Collections.Generic;
using System.Linq;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Commands
{
	public class ActorInstanceRandomMoveCommand : ActorInstanceMoveCommand
	{
		private static readonly Random _random = new Random();
		private readonly List<RandomMoveDirection> _directions = new List<RandomMoveDirection>();
		private RandomMoveDirection _direction;

		public ActorInstanceRandomMoveCommand(ActorInstance actorInstance, RandomMoveDirection directions = RandomMoveDirection.All)
			: base(actorInstance)
		{
			bool all = directions.HasFlag(RandomMoveDirection.All);

			if (all || directions.HasFlag(RandomMoveDirection.Up))
			{
				_directions.Add(RandomMoveDirection.Up);
			}
			if (all || directions.HasFlag(RandomMoveDirection.Down))
			{
				_directions.Add(RandomMoveDirection.Down);
			}
			if (all || directions.HasFlag(RandomMoveDirection.Left))
			{
				_directions.Add(RandomMoveDirection.Left);
			}
			if (all || directions.HasFlag(RandomMoveDirection.Right))
			{
				_directions.Add(RandomMoveDirection.Right);
			}
		}

		public override IEnumerable<string> Details
		{
			get
			{
				string directions = String.Join(", ", _directions.Select(arg => arg.ToString()));

				yield return "Possible directions: " + directions;
				yield return "Actual direction: " + _direction;
			}
		}

		protected override Coordinate ModifyCoordinate(int x, int y)
		{
			int index = _random.Next(0, _directions.Count);

			_direction = _directions[index];

			switch (_directions[index])
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
					throw new Exception(String.Format("Unexpected direction '{0}'.", _directions[index]));
			}
		}
	}
}