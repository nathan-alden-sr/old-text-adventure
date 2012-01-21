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
		private readonly Guid _boardId = Guid.NewGuid();

		private DefaultWorldFactory()
		{
		}

		public World CreateWorld(string title = "Untitled")
		{
			return new World(
				Guid.NewGuid(),
				1,
				title,
				GetStartingPlayer(),
				GetBoards(),
				GetActors(),
				GetMessages(),
				GetTimers(),
				GetSoundEffects(),
				GetSongs());
		}

		private Player GetStartingPlayer()
		{
			return new Player(
				Guid.NewGuid(),
				_boardId,
				new Coordinate(_boardSize.Width / 2, _boardSize.Height / 2),
				new Character(2, Color.White, Color.Blue));
		}

		private static IEnumerable<Board> GetBoards()
		{
			yield return new Board(
				Guid.NewGuid(),
				"Board",
				"The default board for a new world.",
				_boardSize,
				GetBackgroundLayer(),
				GetForegroundLayer(),
				GetActorInstanceLayer(),
				GetBoardExits());
		}

		private static SpriteLayer GetBackgroundLayer()
		{
			return new SpriteLayer(_boardSize, GetBackgroundLayerSprites());
		}

		private static SpriteLayer GetForegroundLayer()
		{
			return new SpriteLayer(_boardSize, GetForegroundLayerSprites());
		}

		private static ActorInstanceLayer GetActorInstanceLayer()
		{
			return new ActorInstanceLayer(_boardSize, GetActorInstanceLayerActorInstances());
		}

		private static IEnumerable<Sprite> GetBackgroundLayerSprites()
		{
			for (int x = 0; x < _boardSize.Width; x++)
			{
				for (int y = 0; y < _boardSize.Height; y++)
				{
					var coordinate = new Coordinate(x, y);
					var character = new Character(0xb0, new Color(0, 96, 0), new Color(0, 64, 0));

					yield return new Sprite(coordinate, character);
				}
			}
		}

		private static IEnumerable<Sprite> GetForegroundLayerSprites()
		{
			for (int x = 0; x < _boardSize.Width; x++)
			{
				yield return new Sprite(new Coordinate(x, 0), new Character(0x23, Color.White, Color.TransparentBlack));
				yield return new Sprite(new Coordinate(x, _boardSize.Height - 1), new Character(0x23, Color.White, Color.TransparentBlack));
			}
			for (int y = 1; y < _boardSize.Height - 1; y++)
			{
				yield return new Sprite(new Coordinate(0, y), new Character(0x23, Color.White, Color.TransparentBlack));
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

		private static IEnumerable<Timer> GetTimers()
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