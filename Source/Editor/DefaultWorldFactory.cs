using System;
using System.Collections.Generic;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Editor
{
	public class DefaultWorldFactory
	{
		public static readonly DefaultWorldFactory Instance = new DefaultWorldFactory();
		private static readonly Size _boardSize = new Size(21, 11);

		private DefaultWorldFactory()
		{
		}

		public World CreateWorld(string title = "Untitled")
		{
			Guid boardId = Guid.NewGuid();

			return new World(
				Guid.NewGuid(),
				1,
				title,
				GetStartingPlayer(boardId),
				GetBoards(boardId),
				GetActors(),
				GetMessages(),
				GetWorldTimers(),
				GetSoundEffects(),
				GetSongs());
		}

		private static Player GetStartingPlayer(Guid boardId)
		{
			return new Player(
				Guid.NewGuid(),
				boardId,
				new Coordinate(_boardSize.Width / 2, _boardSize.Height / 2),
				new Character(2, Color.White, Color.Blue));
		}

		private static IEnumerable<Board> GetBoards(Guid boardId)
		{
			yield return new Board(
				boardId,
				"Board",
				"The default board for a new world.",
				_boardSize,
				GetBackgroundLayer(boardId),
				GetForegroundLayer(boardId),
				GetActorInstanceLayer(boardId),
				GetBoardExits(),
				GetBoardTimers());
		}

		private static SpriteLayer GetBackgroundLayer(Guid boardId)
		{
			return new SpriteLayer(boardId, _boardSize, GetBackgroundLayerSprites());
		}

		private static SpriteLayer GetForegroundLayer(Guid boardId)
		{
			return new SpriteLayer(boardId, _boardSize, GetForegroundLayerSprites());
		}

		private static ActorInstanceLayer GetActorInstanceLayer(Guid boardId)
		{
			return new ActorInstanceLayer(boardId, _boardSize, GetActorInstanceLayerActorInstances());
		}

		private static IEnumerable<Sprite> GetBackgroundLayerSprites()
		{
			for (int x = 0; x < _boardSize.Width; x++)
			{
				for (int y = 0; y < _boardSize.Height; y++)
				{
					var coordinate = new Coordinate(x, y);
					var character = new Character(Symbol.LightShade, new Color(0, 96, 0), new Color(0, 64, 0));

					yield return new Sprite(coordinate, character);
				}
			}
		}

		private static IEnumerable<Sprite> GetForegroundLayerSprites()
		{
			for (int x = 0; x < _boardSize.Width; x++)
			{
				yield return new Sprite(new Coordinate(x, 0), new Character(Symbol.Number, Color.White, Color.TransparentBlack));
				yield return new Sprite(new Coordinate(x, _boardSize.Height - 1), new Character(0x23, Color.White, Color.TransparentBlack));
			}
			for (int y = 1; y < _boardSize.Height - 1; y++)
			{
				yield return new Sprite(new Coordinate(0, y), new Character(Symbol.Number, Color.White, Color.TransparentBlack));
				yield return new Sprite(new Coordinate(_boardSize.Width - 1, y), new Character(0x23, Color.White, Color.TransparentBlack));
			}
		}

		private static IEnumerable<ActorInstance> GetActorInstanceLayerActorInstances()
		{
			yield break;
		}

		private static IEnumerable<BoardExit> GetBoardExits()
		{
			yield break;
		}

		private static IEnumerable<Actor> GetActors()
		{
			yield break;
		}

		private static IEnumerable<Message> GetMessages()
		{
			yield break;
		}

		private static IEnumerable<Timer> GetWorldTimers()
		{
			yield break;
		}

		private static IEnumerable<Timer> GetBoardTimers()
		{
			yield break;
		}

		private static IEnumerable<SoundEffect> GetSoundEffects()
		{
			yield break;
		}

		private static IEnumerable<Song> GetSongs()
		{
			yield break;
		}
	}
}