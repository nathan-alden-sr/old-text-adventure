using System;
using System.Collections.Generic;
using System.Linq;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;
using TextAdventure.Samples.Introduction.EventHandlers;

namespace TextAdventure.Samples.Introduction
{
	/// <remarks>
	/// Be sure to call ToArray() on any LINQ expressions to keep a single copy of objects persistent in memory.
	/// </remarks>
	public class IntroductionWorld : World
	{
		private static readonly Guid[] _actorIds = new[]
		                                           	{
		                                           		Guid.Parse("ecb07aa8-ca78-4107-99fb-e067be640f50"),
		                                           		Guid.Parse("c06a51d4-f2e6-4066-80c3-bcfc6824072b")
		                                           	};
		private static readonly Guid[] _actorInstanceIds = new[]
		                                                   	{
		                                                   		Guid.Parse("a45dafc2-d36a-439f-be6c-dc0963d2324b"),
		                                                   		Guid.Parse("0dfc2f1d-8c91-453a-a87f-c6a7dd5786cd")
		                                                   	};
		private static readonly Guid[] _boardIds = new[]
		                                           	{
		                                           		Guid.Parse("062fab52-de12-4eaf-a14b-4f4d38bd6a03"),
		                                           		Guid.Parse("35e56e6b-5460-435d-82b1-74b4d2bdc98c")
		                                           	};
		private static readonly Guid[] _messageIds = new[]
		                                             	{
		                                             		Guid.Parse("889d17fa-04ca-4eca-aa36-18589ce5f958")
		                                             	};
		private static readonly Guid _playerId = Guid.Parse("bab8a3d6-8c1b-455c-b9ea-c12445270ac8");
		private static readonly Size _size = new Size(19, 15);
		private static readonly Guid[] _songIds = new[]
		                                          	{
		                                          		Guid.Parse("9c01844c-4786-4a93-8092-66036666bf2f")
		                                          	};
		private static readonly Guid[] _soundEffectIds = new[]
		                                                 	{
		                                                 		Guid.Parse("4f095264-d8bb-49da-b1bb-38c81e4a90a9")
		                                                 	};

		public IntroductionWorld()
			: base(
				Guid.Parse("b4320172-092e-4388-b876-99eb6b0b7960"),
				1,
				"Introduction",
				GetStartingPlayer(),
				GetBoards().ToArray(),
				GetActors().ToArray(),
				GetMessages().ToArray(),
				GetTimers().ToArray(),
				GetSoundEffects().ToArray(),
				GetSongs().ToArray())
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
			var backgroundLayer = new SpriteLayer(_size, GetBackgroundLayerSprites(_boardIds[0]).ToArray());
			var foregroundLayer = new SpriteLayer(_size, GetForegroundLayerSprites(_boardIds[0]).ToArray());
			var actorLayer = new ActorInstanceLayer(_size, GetActorInstanceLayerActorInstances(_boardIds[0]).ToArray());

			yield return new Board(_boardIds[0], "", "", _size, backgroundLayer, foregroundLayer, actorLayer, Enumerable.Empty<BoardExit>());
		}

		private static IEnumerable<Actor> GetActors()
		{
			yield return new Actor(_actorIds[0], "", "", new Character(2, Color.Yellow, Color.TransparentBlack));
			yield return new Actor(_actorIds[1], "", "", new Character(14, Color.Cyan, Color.TransparentBlack));
		}

		private static IEnumerable<Message> GetMessages()
		{
			yield return Message.Build(_messageIds[0], new Color(0f, 0f, 0.5f))
				.Text("Thank you for playing ")
				.Color(Color.Yellow)
				.Text("Text Adventure")
				.Color(Color.White)
				.Text("!", 2)
				.Text("Use the menus to open a world or a saved game.", 2)
				.Text("Please visit my blog at ")
				.Color(Color.Cyan)
				.Text("http://blog.TheCognizantCoder.com")
				.Color(Color.White)
				.Text(" or track the progress of Text Adventure development at ")
				.Color(Color.Cyan)
				.Text("https://GitHub.com/NathanAlden/TextAdventure")
				.Color(Color.White)
				.Text(".");
		}

		private static IEnumerable<Timer> GetTimers()
		{
			yield break;
		}

		private static IEnumerable<SoundEffect> GetSoundEffects()
		{
			yield return new SoundEffect(_soundEffectIds[0], "", "", Introduction.SoundEffects.SoundEffects.Windows_Balloon.GetBuffer());
		}

		private static IEnumerable<Song> GetSongs()
		{
			yield return new Song(_songIds[0], "", "", Introduction.Songs.Songs.The_Experiment);
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
			for (int x = 0; x < _size.Width; x++)
			{
				foreach (int y in new[] { 0, _size.Height - 1 })
				{
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
					"",
					"",
					_actorIds[0],
					new Coordinate((_size.Width / 2) - 1, (_size.Height / 2) - 3),
					new Character(2, Color.Yellow, Color.TransparentBlack),
					playerTouchedActorInstanceEventHandler:new PlayerTouchedActorInstanceEventHandler1());
				yield return new ActorInstance(
					_actorInstanceIds[1],
					"",
					"",
					_actorIds[1],
					new Coordinate((_size.Width / 2) + 1, (_size.Height / 2) - 3),
					new Character(14, Color.Cyan, Color.TransparentBlack),
					playerTouchedActorInstanceEventHandler:new PlayerTouchedActorInstanceEventHandler2());
			}
		}
	}
}