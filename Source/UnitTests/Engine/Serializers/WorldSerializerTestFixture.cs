using System;
using System.Linq;
using System.Text;

using Junior.Common;

using NUnit.Framework;

using TextAdventure.Engine;
using TextAdventure.Engine.Common;
using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Objects;

namespace TextAdventure.UnitTests.Engine.Serializers
{
	public abstract class WorldSerializerTestFixture
	{
		protected void Assert(World world)
		{
			AssertWorld(world);

			Player startingPlayer = world.StartingPlayer;
			Board board = world.Boards.Single();
			Actor actor = world.Actors.Single();
			Message message = world.Messages.Single();
			Timer timer = world.Timers.Single();
			SoundEffect soundEffect = world.SoundEffects.Single();
			Song song = world.Songs.Single();

			AssertStartingPlayer(board, startingPlayer);
			AssertBoard(board);
			AssertActor(actor);
			AssertBackgroundLayer(board);
			AssertForegroundLayer(board);
			AssertActorInstanceLayer(board);
			AssertExit(board);
			AssertMessage(message);
			AssertTimer(timer);
			AssertSoundEffect(soundEffect);
			AssertSong(song);
		}

		private static void AssertWorld(World world)
		{
			NUnit.Framework.Assert.That(world.Id, Is.EqualTo(Guid.Parse("9846b8bf-6312-4dd0-a70b-022d1ea2d65e")));
			NUnit.Framework.Assert.That(world.Version, Is.EqualTo(1));
			NUnit.Framework.Assert.That(world.Title, Is.EqualTo("Title"));
			NUnit.Framework.Assert.That(world.Boards.CountEqual(1), Is.True);
			NUnit.Framework.Assert.That(world.Actors.CountEqual(1), Is.True);
			NUnit.Framework.Assert.That(world.Messages.CountEqual(1), Is.True);
		}

		private static void AssertBoard(Board board)
		{
			NUnit.Framework.Assert.That(board.Id, Is.EqualTo(Guid.Parse("be68b2a8-8b40-440f-a93f-6c5986a000bc")));
			NUnit.Framework.Assert.That(board.Name, Is.EqualTo("Board"));
			NUnit.Framework.Assert.That(board.Description, Is.EqualTo("Board description"));
			NUnit.Framework.Assert.That(board.Size, Is.EqualTo(new Size(12, 34)));
			NUnit.Framework.Assert.That(board.BoardEnteredEventHandler, Is.Not.Null);
			NUnit.Framework.Assert.That(board.BoardExitedEventHandler, Is.Not.Null);
		}

		private static void AssertActor(Actor actor)
		{
			NUnit.Framework.Assert.That(actor.Id, Is.EqualTo(Guid.Parse("677ae75c-117c-4992-8dec-ffa645308f82")));
			NUnit.Framework.Assert.That(actor.Name, Is.EqualTo("Actor"));
			NUnit.Framework.Assert.That(actor.Description, Is.EqualTo("Actor description"));
			NUnit.Framework.Assert.That(actor.Character.Symbol, Is.EqualTo(2));
			NUnit.Framework.Assert.That(actor.Character.ForegroundColor, Is.EqualTo(new Color(0.2f, 0.3f, 0.7f, 0.8f)));
			NUnit.Framework.Assert.That(actor.Character.BackgroundColor, Is.EqualTo(new Color(1f, 1f, 1f)));
		}

		private static void AssertStartingPlayer(IUnique board, Player startingPlayer)
		{
			NUnit.Framework.Assert.That(startingPlayer.Id, Is.EqualTo(Guid.Parse("e64d573a-20e2-4f26-9367-3687b250917b")));
			NUnit.Framework.Assert.That(startingPlayer.BoardId, Is.EqualTo(board.Id));
			NUnit.Framework.Assert.That(startingPlayer.Character.Symbol, Is.EqualTo(2));
			NUnit.Framework.Assert.That(startingPlayer.Character.ForegroundColor, Is.EqualTo(new Color(0.5f, 0.7f, 0.9f)));
			NUnit.Framework.Assert.That(startingPlayer.Character.BackgroundColor, Is.EqualTo(Color.TransparentBlack));
			NUnit.Framework.Assert.That(startingPlayer.Coordinate, Is.EqualTo(new Coordinate(10, 10)));
			NUnit.Framework.Assert.That(startingPlayer.ActorInstanceTouchedPlayerEventHandler, Is.Not.Null);
		}

		private static void AssertBackgroundLayer(Board board)
		{
			SpriteLayer backgroundLayer = board.BackgroundLayer;
			Sprite sprite1 = backgroundLayer.Sprites.Single(arg => arg.Coordinate.X == 10 && arg.Coordinate.Y == 4);

			NUnit.Framework.Assert.That(sprite1.Character.Symbol, Is.EqualTo(65));
			NUnit.Framework.Assert.That(sprite1.Character.ForegroundColor, Is.EqualTo(new Color(0f, 0.1f, 0.2f, 0.3f)));
			NUnit.Framework.Assert.That(sprite1.Character.BackgroundColor, Is.EqualTo(new Color(0.4f, 0.5f, 0.6f, 0.7f)));

			Sprite sprite2 = backgroundLayer.Sprites.Single(arg => arg.Coordinate.X == 1 && arg.Coordinate.Y == 0);

			NUnit.Framework.Assert.That(sprite2.Character.Symbol, Is.EqualTo(66));
			NUnit.Framework.Assert.That(sprite2.Character.ForegroundColor, Is.EqualTo(new Color(0.8f, 0.9f, 1f, 0.01f)));
			NUnit.Framework.Assert.That(sprite2.Character.BackgroundColor, Is.EqualTo(new Color(0.02f, 0.03f, 0.04f, 0.05f)));
		}

		private static void AssertForegroundLayer(Board board)
		{
			SpriteLayer backgroundLayer = board.ForegroundLayer;
			Sprite sprite1 = backgroundLayer.Sprites.Single(arg => arg.Coordinate.X == 5 && arg.Coordinate.Y == 6);

			NUnit.Framework.Assert.That(sprite1.Character.Symbol, Is.EqualTo(70));
			NUnit.Framework.Assert.That(sprite1.Character.ForegroundColor, Is.EqualTo(new Color(0.9f, 0.8f, 0.7f, 0.6f)));
			NUnit.Framework.Assert.That(sprite1.Character.BackgroundColor, Is.EqualTo(new Color(0.5f, 0.4f, 0.3f, 0.2f)));

			Sprite sprite2 = backgroundLayer.Sprites.Single(arg => arg.Coordinate.X == 10 && arg.Coordinate.Y == 30);

			NUnit.Framework.Assert.That(sprite2.Character.Symbol, Is.EqualTo(71));
			NUnit.Framework.Assert.That(sprite2.Character.ForegroundColor, Is.EqualTo(new Color(0.2f, 0.4f, 0.6f, 0.8f)));
			NUnit.Framework.Assert.That(sprite2.Character.BackgroundColor, Is.EqualTo(new Color(0.8f, 0.6f, 0.4f, 0.2f)));
		}

		private static void AssertActorInstanceLayer(Board board)
		{
			ActorInstanceLayer actorInstanceLayer = board.ActorInstanceLayer;
			ActorInstance actor1 = actorInstanceLayer.ActorInstances.Single(arg => arg.Coordinate.X == 11 && arg.Coordinate.Y == 31);

			NUnit.Framework.Assert.That(actor1.Id, Is.EqualTo(Guid.Parse("f5ef050a-cf2a-4d83-b320-75478c77af4f")));
			NUnit.Framework.Assert.That(actor1.Name, Is.EqualTo("Actor instance 1"));
			NUnit.Framework.Assert.That(actor1.Description, Is.EqualTo("Actor instance 1 description"));
			NUnit.Framework.Assert.That(actor1.ActorId, Is.EqualTo(Guid.Parse("677ae75c-117c-4992-8dec-ffa645308f82")));
			NUnit.Framework.Assert.That(actor1.Character.Symbol, Is.EqualTo(68));
			NUnit.Framework.Assert.That(actor1.Character.ForegroundColor, Is.EqualTo(new Color(0.15f, 0.16f, 0.17f, 0.18f)));
			NUnit.Framework.Assert.That(actor1.Character.BackgroundColor, Is.EqualTo(new Color(0.2f, 0.21f, 0.22f, 0.23f)));
			NUnit.Framework.Assert.That(actor1.ActorInstanceCreatedEventHandler, Is.Not.Null);
			NUnit.Framework.Assert.That(actor1.ActorInstanceDestroyedEventHandler, Is.Not.Null);
			NUnit.Framework.Assert.That(actor1.ActorInstanceTouchedActorInstanceEventHandler, Is.Not.Null);
			NUnit.Framework.Assert.That(actor1.PlayerTouchedActorInstanceEventHandler, Is.Not.Null);
			NUnit.Framework.Assert.That(actor1.ActorInstanceMovedEventHandler, Is.Not.Null);

			ActorInstance actor2 = actorInstanceLayer.ActorInstances.Single(arg => arg.Coordinate.X == 11 && arg.Coordinate.Y == 33);

			NUnit.Framework.Assert.That(actor2.Id, Is.EqualTo(Guid.Parse("706da31e-31b1-40be-9eab-648f5a574acc")));
			NUnit.Framework.Assert.That(actor2.Name, Is.EqualTo("Actor instance 2"));
			NUnit.Framework.Assert.That(actor2.Description, Is.EqualTo("Actor instance 2 description"));
			NUnit.Framework.Assert.That(actor2.ActorId, Is.EqualTo(Guid.Parse("677ae75c-117c-4992-8dec-ffa645308f82")));
			NUnit.Framework.Assert.That(actor2.Character.Symbol, Is.EqualTo(67));
			NUnit.Framework.Assert.That(actor2.Character.ForegroundColor, Is.EqualTo(new Color(0.06f, 0.07f, 0.08f, 0.09f)));
			NUnit.Framework.Assert.That(actor2.Character.BackgroundColor, Is.EqualTo(new Color(0.11f, 0.12f, 0.13f, 0.14f)));
		}

		private static void AssertExit(Board board)
		{
			BoardExit boardExit = board.Exits.Single();

			NUnit.Framework.Assert.That(boardExit.Coordinate, Is.EqualTo(new Coordinate(0, 0)));
			NUnit.Framework.Assert.That(boardExit.Direction, Is.EqualTo(BoardExitDirection.Up));
			NUnit.Framework.Assert.That(boardExit.DestinationBoardId, Is.EqualTo(Guid.Parse("be68b2a8-8b40-440f-a93f-6c5986a000bc")));
			NUnit.Framework.Assert.That(boardExit.DestinationCoordinate, Is.EqualTo(new Coordinate(2, 3)));
		}

		private static void AssertMessage(Message message)
		{
			NUnit.Framework.Assert.That(message.Id, Is.EqualTo(Guid.Parse("fee40b1a-1aa8-467d-9c8d-49b39a1641a9")));
			NUnit.Framework.Assert.That(message.Name, Is.EqualTo("Message"));
			NUnit.Framework.Assert.That(message.Description, Is.EqualTo("Message description"));
			NUnit.Framework.Assert.That(message.BackgroundColor, Is.EqualTo(new Color(0f, 0f, 0.5f)));

			IMessagePart[] messageParts = message.Parts.ToArray();

			NUnit.Framework.Assert.That(messageParts.Length, Is.EqualTo(4));

			var color = messageParts[0] as MessageColor;
			var text = messageParts[1] as MessageText;
			var lineBreak = messageParts[2] as MessageLineBreak;
			var question = messageParts[3] as MessageQuestion;

			NUnit.Framework.Assert.That(color, Is.Not.Null);
			NUnit.Framework.Assert.That(color.Color, Is.EqualTo(Color.Cyan));

			NUnit.Framework.Assert.That(text, Is.Not.Null);
			NUnit.Framework.Assert.That(text.Text, Is.EqualTo("Lorem ipsum"));

			NUnit.Framework.Assert.That(lineBreak, Is.Not.Null);

			NUnit.Framework.Assert.That(question, Is.Not.Null);
			NUnit.Framework.Assert.That(question.QuestionForegroundColor, Is.EqualTo(new Color(0.1f, 0.2f, 0.3f, 0.4f)));
			NUnit.Framework.Assert.That(question.UnselectedAnswerForegroundColor, Is.EqualTo(new Color(0.4f, 0.5f, 0.6f, 0.7f)));
			NUnit.Framework.Assert.That(question.SelectedAnswerForegroundColor, Is.EqualTo(new Color(0.5f, 0.2f, 0.9f)));
			NUnit.Framework.Assert.That(question.SelectedAnswerBackgroundColor, Is.EqualTo(new Color(0.2f, 0.3f, 0.1f, 0.05f)));
			NUnit.Framework.Assert.That(question.Prompt, Is.EqualTo("prompt"));

			MessageAnswer[] messageAnswers = question.Answers.ToArray();

			NUnit.Framework.Assert.That(messageAnswers.Length, Is.EqualTo(2));

			NUnit.Framework.Assert.That(messageAnswers[0].Id, Is.EqualTo(Guid.Parse("bf61ef08-2bd2-4273-a1f4-641e22415047")));
			NUnit.Framework.Assert.That(messageAnswers[0].Text, Is.EqualTo("Yes"));
			NUnit.Framework.Assert.That(messageAnswers[0].MessageAnswerSelectedEventHandler, Is.Not.Null);

			IMessagePart[] messageAnswerParts = messageAnswers[0].Parts.ToArray();

			NUnit.Framework.Assert.That(messageAnswerParts.Length, Is.EqualTo(3));

			NUnit.Framework.Assert.That(messageAnswerParts[0], Is.InstanceOf<MessageColor>());
			NUnit.Framework.Assert.That(messageAnswerParts[1], Is.InstanceOf<MessageText>());
			NUnit.Framework.Assert.That(messageAnswerParts[2], Is.InstanceOf<MessageLineBreak>());

			NUnit.Framework.Assert.That(messageAnswers[1].Id, Is.EqualTo(Guid.Parse("bc7cdc46-27f7-4d6b-8fbd-25ea6053a551")));
			NUnit.Framework.Assert.That(messageAnswers[1].Text, Is.EqualTo("No"));
		}

		private static void AssertTimer(Timer timer)
		{
			NUnit.Framework.Assert.That(timer.Id, Is.EqualTo(Guid.Parse("9d18f5e7-8199-4160-bff8-646ca6586ddb")));
			NUnit.Framework.Assert.That(timer.Name, Is.EqualTo("Timer"));
			NUnit.Framework.Assert.That(timer.Description, Is.EqualTo("Timer description"));
			NUnit.Framework.Assert.That(timer.Interval, Is.EqualTo(TimeSpan.FromSeconds(15)));
			NUnit.Framework.Assert.That(timer.TimerElapsedEventHandler, Is.Not.Null);
			NUnit.Framework.Assert.That(timer.State, Is.EqualTo(TimerState.Paused));
			NUnit.Framework.Assert.That(timer.ElapsedTime, Is.EqualTo(TimeSpan.FromSeconds(7)));
		}

		private static void AssertSoundEffect(SoundEffect soundEffect)
		{
			NUnit.Framework.Assert.That(soundEffect.Id, Is.EqualTo(Guid.Parse("b98aaa37-100a-429c-b440-a0943e294a6c")));
			NUnit.Framework.Assert.That(soundEffect.Name, Is.EqualTo("Sound effect"));
			NUnit.Framework.Assert.That(soundEffect.Description, Is.EqualTo("Sound effect description"));
			NUnit.Framework.Assert.That(soundEffect.Data, Is.EqualTo(Encoding.ASCII.GetBytes("test")));
		}

		private static void AssertSong(Song song)
		{
			NUnit.Framework.Assert.That(song.Id, Is.EqualTo(Guid.Parse("597a8458-a2be-4975-a6d5-69eebc701b9a")));
			NUnit.Framework.Assert.That(song.Name, Is.EqualTo("Song"));
			NUnit.Framework.Assert.That(song.Description, Is.EqualTo("Song description"));
			NUnit.Framework.Assert.That(song.Data, Is.EqualTo(Encoding.ASCII.GetBytes("test2")));
		}

		public class ActorInstanceCreatedEventHandler : TextAdventure.Engine.Game.Events.EventHandler<ActorInstanceCreatedEvent>
		{
			public override void HandleEvent(EventContext context, ActorInstanceCreatedEvent @event)
			{
			}
		}

		public class ActorInstanceDestroyedEventHandler : TextAdventure.Engine.Game.Events.EventHandler<ActorInstanceDestroyedEvent>
		{
			public override void HandleEvent(EventContext context, ActorInstanceDestroyedEvent @event)
			{
			}
		}

		public class ActorInstanceMovedEventHandler : TextAdventure.Engine.Game.Events.EventHandler<ActorInstanceMovedEvent>
		{
			public override void HandleEvent(EventContext context, ActorInstanceMovedEvent @event)
			{
			}
		}

		public class ActorInstanceTouchedActorInstanceEventHandler : TextAdventure.Engine.Game.Events.EventHandler<ActorInstanceTouchedActorInstanceEvent>
		{
			public override void HandleEvent(EventContext context, ActorInstanceTouchedActorInstanceEvent @event)
			{
			}
		}

		public class ActorInstanceTouchedPlayerEventHandler : TextAdventure.Engine.Game.Events.EventHandler<ActorInstanceTouchedPlayerEvent>
		{
			public override void HandleEvent(EventContext context, ActorInstanceTouchedPlayerEvent @event)
			{
			}
		}

		public class BoardEnteredEventHandler : TextAdventure.Engine.Game.Events.EventHandler<BoardEnteredEvent>
		{
			public override void HandleEvent(EventContext context, BoardEnteredEvent @event)
			{
			}
		}

		public class BoardExitedEventHandler : TextAdventure.Engine.Game.Events.EventHandler<BoardExitedEvent>
		{
			public override void HandleEvent(EventContext context, BoardExitedEvent @event)
			{
			}
		}

		public class MessageAnswerSelectedEventHandler : TextAdventure.Engine.Game.Events.EventHandler<MessageAnswerSelectedEvent>
		{
			public override void HandleEvent(EventContext context, MessageAnswerSelectedEvent @event)
			{
			}
		}

		public class PlayerTouchedActorInstanceEventHandler : TextAdventure.Engine.Game.Events.EventHandler<PlayerTouchedActorInstanceEvent>
		{
			public override void HandleEvent(EventContext context, PlayerTouchedActorInstanceEvent @event)
			{
			}
		}

		public class TimerElapsedEventHandler : TextAdventure.Engine.Game.Events.EventHandler<TimerElapsedEvent>
		{
			public override void HandleEvent(EventContext context, TimerElapsedEvent @event)
			{
			}
		}
	}
}