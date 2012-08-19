using System;
using System.Collections.Generic;
using System.Linq;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Game.Commands;
using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Game.Messages;
using TextAdventure.Engine.Game.World;
using TextAdventure.Engine.Objects;
using TextAdventure.Samples.Ctxna.Actors;
using TextAdventure.Samples.Ctxna.SoundEffects;
using TextAdventure.Samples.Factories;

namespace TextAdventure.Samples.Ctxna.Boards
{
	public class IntroductionBoard : Board
	{
		public static readonly Guid BoardId = Guid.Parse("dae415ca-ca40-4745-8126-217e43530170");
		public static readonly Size BoardSize = new Size(80, 20);
		public static readonly Coordinate[] ExitCoordinates = new[]
			{
				new Coordinate(50, 11)
			};
		private static readonly Coordinate _layerOriginCoordinate = new Coordinate(30, 8);
		private static readonly Size _layerSize = new Size(21, 7);

		public IntroductionBoard()
			: base(BoardId, "Introduction", "", BoardSize, GetBackgroundLayer(), GetForegroundLayer(), GetActorInstanceLayer(), GetExits().ToArray(), GetTimers().ToArray())
		{
		}

		private static SpriteLayer GetBackgroundLayer()
		{
			var character = new Character(Symbol.LightShade, new Color(14, 119, 11), new Color(9, 80, 8));
			IEnumerable<Sprite> sprites = SpriteFactory.Instance.CreateArea(_layerOriginCoordinate, _layerSize, character);

			return new SpriteLayer(BoardId, BoardSize, sprites);
		}

		private static SpriteLayer GetForegroundLayer()
		{
			var character = new Character(Symbol.Number, Color.White, Color.TransparentBlack);
			IEnumerable<Sprite> borderSprites = SpriteFactory.Instance.CreateBorder(_layerOriginCoordinate, _layerSize, character);
			IEnumerable<Sprite> textLine1Sprites = SpriteFactory.Instance.CreateCenteredText(
				"Text Adventure Game Engine Using XNA",
				0,
				BoardSize.Width,
				Color.Yellow,
				Color.TransparentBlack);
			IEnumerable<Sprite> textLine2Sprites = SpriteFactory.Instance.CreateCenteredText(
				"Hosted by Nathan Alden, Sr. and ctxna.org",
				2,
				BoardSize.Width,
				Color.White,
				Color.TransparentBlack);
			IEnumerable<Sprite> textLine3Sprites = SpriteFactory.Instance.CreateCenteredText(
				"https://github.com/NathanAlden/TextAdventure",
				4,
				BoardSize.Width,
				Color.White,
				Color.TransparentBlack);
			IEnumerable<Sprite> textLine4Sprites = SpriteFactory.Instance.CreateCenteredText(
				"http://blog.TheCognizantCoder.com",
				6,
				BoardSize.Width,
				Color.White,
				Color.TransparentBlack);
			var sprites = new List<Sprite>(borderSprites
				                               .Concat(textLine1Sprites)
				                               .Concat(textLine2Sprites)
				                               .Concat(textLine3Sprites)
				                               .Concat(textLine4Sprites));

			sprites.RemoveAll(arg => ExitCoordinates.Contains(arg.Coordinate));
			sprites.Add(new Sprite(ExitCoordinates[0], new Character(Symbol.InverseCircle, Color.Magenta, Color.Black)));

			return new SpriteLayer(BoardId, BoardSize, sprites);
		}

		private static ActorInstanceLayer GetActorInstanceLayer()
		{
			var welcomeActor = new IntroductionActor();
			ActorInstance welcomeActorInstance = welcomeActor.CreateActorInstance(
				BoardId,
				new Coordinate(40, 10),
				new EventHandlerCollection(new PlayerTouchedIntroductionActorEventHandler()));

			return new ActorInstanceLayer(BoardId, BoardSize, welcomeActorInstance);
		}

		private static IEnumerable<BoardExit> GetExits()
		{
			yield return new BoardExit(ExitCoordinates[0], BoardExitDirection.Right, ObjectsBoard.BoardId, ObjectsBoard.ExitCoordinates[0]);
		}

		private static IEnumerable<Timer> GetTimers()
		{
			yield break;
		}

		private class PlayerTouchedIntroductionActorEventHandler : Engine.Game.Events.EventHandler<PlayerTouchedActorInstanceEvent>
		{
			public override EventResult HandleEvent(EventContext context, PlayerTouchedActorInstanceEvent @event)
			{
				Color indent0 = Color.Yellow;
				Color indent1 = Color.White;
				MessageBuilder messageBuilder = Message
					.Build(Color.DarkBlue)
					.Text(indent0, "Introduction", 1)
					.Text(indent1, "  - Who am I?", 1)
					.Text(indent0, "What is ZZT?", 1)
					.Text(indent1, "  - Created by Tim Sweeney (of Unreal fame) in 1991", 1)
					.Text(indent1, "  - ZZT is accessible -- you don't have to be an artist or sound engineer to create a game", 1)
					.Text(indent0, "Why recreate an old DOS game?", 1)
					.Text(indent1, "  - Rekindle the awesomeness of ZZT using modern tools and graphics capabilities", 1)
					.Text(indent1, "  - For the challenge and learning experience", 1)
					.Text(indent1, "  - For fun!");

				context.EnqueueCommand(Commands.Message(messageBuilder));

				messageBuilder = Message
					.Build(Color.DarkRed)
					.Question(
						"Unlock the next board?",
						Color.Yellow,
						Color.White,
						Color.Yellow,
						Color.Gray,
						MessageAnswer.Build("Yes", new EventHandlerCollection(new YesAnswerSelectedEventHandler(@event.Target))),
						MessageAnswer.Build("No"));

				context.EnqueueCommand(Commands.Message(messageBuilder));

				return EventResult.Completed;
			}
		}

		private class SlapMessageClosedEventHandler : Engine.Game.Events.EventHandler<MessageClosedEvent>
		{
			private readonly Command _command;

			public SlapMessageClosedEventHandler(Command command)
			{
				_command = command;
			}

			public override EventResult HandleEvent(EventContext context, MessageClosedEvent @event)
			{
				context.EnqueueCommand(_command);

				return EventResult.Completed;
			}
		}

		private class YesAnswerSelectedEventHandler : Engine.Game.Events.EventHandler<MessageAnswerSelectedEvent>
		{
			private readonly ActorInstance _actorInstance;

			public YesAnswerSelectedEventHandler(ActorInstance actorInstance)
			{
				_actorInstance = actorInstance;
			}

			public override EventResult HandleEvent(EventContext context, MessageAnswerSelectedEvent @event)
			{
				context.PlayerInput.Suspend();

				ChainedCommand moveActorCommand = Commands
					.Chain(Commands
						       .ActorInstanceMove(_actorInstance, MoveDirection.Right)
						       .Repeat(TimeSpan.FromMilliseconds(100), 9))
					.And(Commands.Delay(TimeSpan.FromMilliseconds(100)))
					.And(Commands.ActorInstanceMove(_actorInstance, MoveDirection.Down))
					.And(Commands.Delay(TimeSpan.FromMilliseconds(500)))
					.And(Commands.PlaySoundEffect(context.GetSoundEffectById(ExplodeSoundEffect.SoundEffectId), Volume.Full))
					.And(Commands.RemoveSprite(context.CurrentBoard.ForegroundLayer, new Coordinate(50, 11)))
					.And(Commands.ActorInstanceDestroy(_actorInstance))
					.And(Commands.PlayerResumeInput());

				if (context.Player.Coordinate == new Coordinate(_actorInstance.Coordinate.X + 1, _actorInstance.Coordinate.Y))
				{
					ChainedCommand command = Commands
						.Chain(Commands.Delay(TimeSpan.FromSeconds(1)))
						.And(Commands.PlaySoundEffect(context.GetSoundEffectById(SlapSoundEffect.SoundEffectId), Volume.Full))
						.And(Commands.PlayerMove(MoveDirection.Down))
						.And(Commands.Message(Message.Build(Color.DarkRed, new EventHandlerCollection(new SlapMessageClosedEventHandler(moveActorCommand))).Text(Color.Yellow, "WHAP!")))
						.And(Commands.Delay(TimeSpan.FromSeconds(1)));

					context.EnqueueCommand(command);

					return EventResult.Completed;
				}

				context.EnqueueCommand(moveActorCommand);

				return EventResult.Completed;
			}
		}
	}
}