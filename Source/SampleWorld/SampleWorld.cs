using System;
using System.Collections.Generic;
using System.Linq;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;
using TextAdventure.SampleWorld.EventHandlers;

namespace TextAdventure.SampleWorld
{
	/// <remarks>
	/// Be sure to call ToArray() on any LINQ expressions to keep a single copy of objects persistent in memory.
	/// </remarks>
	public class SampleWorld : World
	{
		private static readonly Guid[] _actorIds = new[]
		                                           	{
		                                           		Guid.Parse("ecb07aa8-ca78-4107-99fb-e067be640f50")
		                                           	};
		private static readonly Guid[] _actorInstanceIds = new[]
		                                                   	{
		                                                   		Guid.Parse("a45dafc2-d36a-439f-be6c-dc0963d2324b")
		                                                   	};
		private static readonly Guid[] _boardIds = new[]
		                                           	{
		                                           		Guid.Parse("062fab52-de12-4eaf-a14b-4f4d38bd6a03"),
		                                           		Guid.Parse("35e56e6b-5460-435d-82b1-74b4d2bdc98c")
		                                           	};
		private static readonly Guid _playerId = Guid.Parse("bab8a3d6-8c1b-455c-b9ea-c12445270ac8");
		private static readonly Size _size = new Size(20, 20);

		public SampleWorld()
			: base(
				Guid.Parse("b4320172-092e-4388-b876-99eb6b0b7960"),
				1,
				GetStartingPlayer(),
				GetBoards().ToArray(),
				GetActors().ToArray(),
				GetMessages().ToArray(),
				GetTimers().ToArray())
		{
		}

		private static Player GetStartingPlayer()
		{
			var coordinate = new Coordinate(_size.Width / 2, _size.Height / 2);
			var character = new Character(0x2, Color.White, Color.Blue);

			return new Player(_playerId, _boardIds[0], coordinate, character);
		}

		private static IEnumerable<Board> GetBoards()
		{
			{
				var backgroundLayer = new SpriteLayer(_size, GetBackgroundLayerSprites(_boardIds[0]).ToArray());
				var foregroundLayer = new SpriteLayer(_size, GetForegroundLayerSprites(_boardIds[0]).ToArray());
				var actorLayer = new ActorInstanceLayer(_size, GetActorInstanceLayerActorInstances(_boardIds[0]).ToArray());
				IEnumerable<BoardExit> boardExits = foregroundLayer.EmptyTiles
					.Where(arg => arg.Y == 0)
					.Select(arg => new BoardExit(arg, BoardExitDirection.Up, _boardIds[1], new Coordinate(arg.X, _size.Height - 1)));

				yield return new Board(_boardIds[0], _size, backgroundLayer, foregroundLayer, actorLayer, boardExits);
			}
			{
				var backgroundLayer = new SpriteLayer(_size, GetBackgroundLayerSprites(_boardIds[1]).ToArray());
				var foregroundLayer = new SpriteLayer(_size, GetForegroundLayerSprites(_boardIds[1]).ToArray());
				var actorLayer = new ActorInstanceLayer(_size, GetActorInstanceLayerActorInstances(_boardIds[1]).ToArray());
				IEnumerable<BoardExit> boardExits = foregroundLayer.EmptyTiles
					.Where(arg => arg.Y == _size.Height - 1)
					.Select(arg => new BoardExit(arg, BoardExitDirection.Down, _boardIds[0], new Coordinate(arg.X, 0)));

				yield return new Board(_boardIds[1], _size, backgroundLayer, foregroundLayer, actorLayer, boardExits);
			}
		}

		private static IEnumerable<Actor> GetActors()
		{
			yield return new Actor(_actorIds[0], new Character(2, Color.Yellow, Color.TransparentBlack));
		}

		private static IEnumerable<Message> GetMessages()
		{
			yield break;
		}

		private static IEnumerable<Timer> GetTimers()
		{
			yield break;
		}

		private static IEnumerable<Sprite> GetBackgroundLayerSprites(Guid boardId)
		{
			for (int x = 0; x < _size.Width; x++)
			{
				for (int y = 0; y < _size.Height; y++)
				{
					var coordinate = new Coordinate(x, y);
					var character = new Character(0xb0, new Color(0, 96, 0), new Color(0, 64, 0));

					yield return new Sprite(coordinate, character);
				}
			}
		}

		private static IEnumerable<Sprite> GetForegroundLayerSprites(Guid boardId)
		{
			int skipY;

			if (boardId == _boardIds[0])
			{
				skipY = 0;
			}
			else if (boardId == _boardIds[1])
			{
				skipY = _size.Height - 1;
			}
			else
			{
				throw new ArgumentException("boardId");
			}

			for (int x = 0; x < _size.Width; x++)
			{
				foreach (int y in new[] { 0, _size.Height - 1 })
				{
					if ((x == _size.Width / 2 || x == (_size.Width / 2) + 1) && y == skipY)
					{
						continue;
					}

					var coordinate = new Coordinate(x, y);
					var character = new Character((byte)'#', Color.White, Color.TransparentBlack);

					yield return new Sprite(coordinate, character);
				}
			}

			for (int y = 1; y < _size.Height - 1; y++)
			{
				foreach (int x in new[] { 0, _size.Width - 1 })
				{
					var coordinate = new Coordinate(x, y);
					var character = new Character((byte)'#', Color.White, Color.TransparentBlack);

					yield return new Sprite(coordinate, character);
				}
			}
		}

		private static IEnumerable<ActorInstance> GetActorInstanceLayerActorInstances(Guid boardId)
		{
			if (boardId == _boardIds[0])
			{
				yield return new ActorInstance(
					_actorInstanceIds[0],
					_actorIds[0],
					new Coordinate(5, 5),
					new Character(2, Color.Yellow, Color.TransparentBlack),
					playerTouchedActorInstanceEventHandler:new PlayerTouchedActorInstanceEventHandler());
			}
		}
	}
}