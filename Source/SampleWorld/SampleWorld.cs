using System;
using System.Collections.Generic;
using System.Linq;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.SampleWorld
{
	/// <remarks>
	/// Be sure to call ToArray() on any LINQ expressions to keep a single copy of objects persistent in memory.
	/// </remarks>
	public class SampleWorld : World
	{
		private static readonly Guid _board1Id = Guid.Parse("062fab52-de12-4eaf-a14b-4f4d38bd6a03");
		private static readonly Guid _board2Id = Guid.Parse("35e56e6b-5460-435d-82b1-74b4d2bdc98c");
		private static readonly Guid _playerId = Guid.Parse("bab8a3d6-8c1b-455c-b9ea-c12445270ac8");
		private static readonly Size _size = new Size(20, 20);

		public SampleWorld()
			: base(Guid.Parse("b4320172-092e-4388-b876-99eb6b0b7960"), 1, GetStartingPlayer(), GetBoards().ToArray(), GetActors().ToArray(), GetMessages().ToArray())
		{
		}

		private static Player GetStartingPlayer()
		{
			var character = new Character(0x2, Color.White, Color.Blue);
			var coordinate = new Coordinate(_size.Width / 2, _size.Height / 2);

			return new Player(_playerId, _board1Id, character, coordinate);
		}

		private static IEnumerable<Board> GetBoards()
		{
			{
				var backgroundLayer = new SpriteLayer(_size, GetBackgroundLayerSprites().ToArray());
				var foregroundLayer = new SpriteLayer(_size, GetForegroundLayerSprites(0).ToArray());
				var actorLayer = new ActorInstanceLayer(_size, GetActorInstanceLayerActorInstances().ToArray());
				IEnumerable<BoardExit> boardExits = foregroundLayer.EmptyTiles
					.Where(arg => arg.Y == 0)
					.Select(arg => new BoardExit(arg, BoardExitDirection.Up, _board2Id, new Coordinate(arg.X, _size.Height - 1)));

				yield return new Board(_board1Id, _size, backgroundLayer, foregroundLayer, actorLayer, boardExits);
			}
			{
				var backgroundLayer = new SpriteLayer(_size, GetBackgroundLayerSprites().ToArray());
				var foregroundLayer = new SpriteLayer(_size, GetForegroundLayerSprites(_size.Height - 1).ToArray());
				var actorLayer = new ActorInstanceLayer(_size, GetActorInstanceLayerActorInstances().ToArray());
				IEnumerable<BoardExit> boardExits = foregroundLayer.EmptyTiles
					.Where(arg => arg.Y == _size.Height - 1)
					.Select(arg => new BoardExit(arg, BoardExitDirection.Down, _board1Id, new Coordinate(arg.X, 0)));

				yield return new Board(_board2Id, _size, backgroundLayer, foregroundLayer, actorLayer, boardExits);
			}
		}

		private static IEnumerable<Actor> GetActors()
		{
			yield break;
		}

		private static IEnumerable<Sprite> GetBackgroundLayerSprites()
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

		private static IEnumerable<Sprite> GetForegroundLayerSprites(int skipY)
		{
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

		private static IEnumerable<ActorInstance> GetActorInstanceLayerActorInstances()
		{
			yield break;
		}

		private static IEnumerable<Message> GetMessages()
		{
			yield break;
		}
	}
}