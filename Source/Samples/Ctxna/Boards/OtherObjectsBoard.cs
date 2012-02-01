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
using TextAdventure.Samples.Ctxna.Songs;
using TextAdventure.Samples.Ctxna.SoundEffects;
using TextAdventure.Samples.Factories;

namespace TextAdventure.Samples.Ctxna.Boards
{
	public class OtherObjectsBoard : Board
	{
		public static readonly Guid BoardId = Guid.Parse("b8bd2e61-5102-499a-bfe0-89254628527b");
		public static readonly Size BoardSize = new Size(21, 13);
		public static readonly Coordinate[] ExitCoordinates = new[]
		                                                      	{
		                                                      		new Coordinate(20, 3),
		                                                      		new Coordinate(10, 11)
		                                                      	};
		public static readonly Guid TimerActorInstanceId = Guid.Parse("e151b505-0ea8-4b5d-b8f3-889ee7041980");
		public static readonly Guid TimerId = Guid.Parse("dfaed992-cfdd-4702-993f-74cebf2f9a30");

		private static readonly Coordinate _layerOriginCoordinate = new Coordinate(0, 2);
		private static readonly Size _layerSize = new Size(21, 10);

		public OtherObjectsBoard()
			: base(BoardId, "OtherObjects", "", BoardSize, GetBackgroundLayer(), GetForegroundLayer(), GetActorInstanceLayer(), GetExits().ToArray(), GetTimers().ToArray())
		{
		}

		private static SpriteLayer GetBackgroundLayer()
		{
			var character = new Character(Symbol.LightShade, new Color(119, 27, 66), new Color(152, 35, 84));
			IEnumerable<Sprite> sprites = SpriteFactory.Instance.CreateArea(_layerOriginCoordinate, _layerSize, character);

			return new SpriteLayer(BoardId, BoardSize, sprites);
		}

		private static SpriteLayer GetForegroundLayer()
		{
			var character = new Character(Symbol.DarkShade, Color.LightGray, Color.TransparentBlack);
			IEnumerable<Sprite> borderSprites = SpriteFactory.Instance.CreateBorder(_layerOriginCoordinate, _layerSize, character);
			IEnumerable<Sprite> textLineSprites = SpriteFactory.Instance.CreateCenteredText(
				"Other Objects",
				0,
				BoardSize.Width,
				Color.White,
				Color.TransparentBlack);
			var sprites = new List<Sprite>(borderSprites.Concat(textLineSprites));

			sprites.RemoveAll(arg => ExitCoordinates.Contains(arg.Coordinate));

			return new SpriteLayer(BoardId, BoardSize, sprites);
		}

		private static ActorInstanceLayer GetActorInstanceLayer()
		{
			var playerActor = new PlayerActor();
			ActorInstance playerActorInstance = playerActor.CreateActorInstance(
				BoardId,
				new Coordinate(4, 4),
				new EventHandlerCollection(new PlayerTouchedPlayerActorInstanceEventHandler()));
			var soundEffectActor = new SoundEffectsActor();
			ActorInstance soundEffectsActorInstance = soundEffectActor.CreateActorInstance(
				BoardId,
				new Coordinate(7, 4),
				new EventHandlerCollection(new PlayerTouchedSoundEffectActorInstanceEventHandler()));
			var songActor = new SongsActor();
			ActorInstance songActorsInstance = songActor.CreateActorInstance(
				BoardId,
				new Coordinate(10, 4),
				new EventHandlerCollection(new PlayerTouchedSongActorInstanceEventHandler()));
			var timerActor = new TimerActor();
			ActorInstance timerActorInstance = timerActor.CreateActorInstance(
				TimerActorInstanceId,
				BoardId,
				new Coordinate(13, 4),
				new EventHandlerCollection(new PlayerTouchedTimerActorInstanceEventHandler()));

			return new ActorInstanceLayer(BoardId, BoardSize, playerActorInstance, soundEffectsActorInstance, songActorsInstance, timerActorInstance);
		}

		private static IEnumerable<BoardExit> GetExits()
		{
			yield return new BoardExit(ExitCoordinates[0], BoardExitDirection.Right, MessagesBoard.BoardId, MessagesBoard.ExitCoordinates[1]);
			yield return new BoardExit(ExitCoordinates[1], BoardExitDirection.Down, OtherTopicsBoard.BoardId, OtherTopicsBoard.ExitCoordinates[0]);
		}

		private static IEnumerable<Timer> GetTimers()
		{
			yield return new Timer(TimerId, "Timer", "", TimeSpan.FromMilliseconds(500), new EventHandlerCollection(new TimerElapsedEventHandler()));
		}

		private class PlayerTouchedPlayerActorInstanceEventHandler : Engine.Game.Events.EventHandler<PlayerTouchedActorInstanceEvent>
		{
			public override EventResult HandleEvent(EventContext context, PlayerTouchedActorInstanceEvent @event)
			{
				Color indent0 = Color.Yellow;
				Color indent1 = Color.White;
				MessageBuilder messageBuilder = Message
					.Build(Color.DarkCyan)
					.Text(indent0, "Player", 1)
					.Text(indent1, "  - You!");

				context.EnqueueCommand(Commands.Message(messageBuilder));

				return EventResult.Completed;
			}
		}

		private class PlayerTouchedSongActorInstanceEventHandler : Engine.Game.Events.EventHandler<PlayerTouchedActorInstanceEvent>
		{
			private bool _playing;

			public override EventResult HandleEvent(EventContext context, PlayerTouchedActorInstanceEvent @event)
			{
				if (!_playing)
				{
					context.EnqueueCommand(Commands.PlaySong(new FlourishSong(), 0.5f));

					Color indent0 = Color.Yellow;
					Color indent1 = Color.White;
					MessageBuilder messageBuilder = Message
						.Build(Color.DarkCyan)
						.Text(indent0, "Songs", 1)
						.Text(indent1, "  - Use FMOD instead of XNA's MediaPlayer class");

					context.EnqueueCommand(Commands.Message(messageBuilder));
				}
				else
				{
					context.EnqueueCommand(Commands.StopSong());
				}

				_playing = !_playing;

				return EventResult.Completed;
			}
		}

		private class PlayerTouchedSoundEffectActorInstanceEventHandler : Engine.Game.Events.EventHandler<PlayerTouchedActorInstanceEvent>
		{
			public override EventResult HandleEvent(EventContext context, PlayerTouchedActorInstanceEvent @event)
			{
				context.EnqueueCommand(Commands.PlaySoundEffect(new WindowsUserAccountControlSoundEffect()));

				Color indent0 = Color.Yellow;
				Color indent1 = Color.White;
				MessageBuilder messageBuilder = Message
					.Build(Color.DarkCyan)
					.Text(indent0, "Sound Effects", 1)
					.Text(indent1, "  - Use FMOD instead of XNA's SoundEffect class");

				context.EnqueueCommand(Commands.Message(messageBuilder));

				return EventResult.Completed;
			}
		}

		private class PlayerTouchedTimerActorInstanceEventHandler : Engine.Game.Events.EventHandler<PlayerTouchedActorInstanceEvent>
		{
			private bool _running;

			public override EventResult HandleEvent(EventContext context, PlayerTouchedActorInstanceEvent @event)
			{
				Timer timer = context.GetTimerById(TimerId);

				if (!_running)
				{
					context.EnqueueCommand(Commands.PerformTimerAction(timer, TimerAction.Restart));

					Color indent0 = Color.Yellow;
					Color indent1 = Color.White;
					MessageBuilder messageBuilder = Message
						.Build(Color.DarkCyan)
						.Text(indent0, "Timers", 1)
						.Text(indent1, "  - Raise an event at a predetermined interval");

					context.EnqueueCommand(Commands.Message(messageBuilder));
				}
				else
				{
					context.EnqueueCommand(Commands.PerformTimerAction(timer, TimerAction.Stop));
				}

				_running = !_running;

				return EventResult.Completed;
			}
		}

		private class TimerElapsedEventHandler : Engine.Game.Events.EventHandler<TimerElapsedEvent>
		{
			private static readonly byte[] _symbols = new[] { Symbol.VerticalBar, Symbol.ForwardSlash, Symbol.MinusSign, Symbol.Backslash };
			private int _symbolIndex = 1;

			public override EventResult HandleEvent(EventContext context, TimerElapsedEvent @event)
			{
				ActorInstance timerActorInstance = context.GetActorInstanceById(TimerActorInstanceId);
				SetTileCharacterCommand setTileCharacterCommand = Commands.SetTileCharacter(
					timerActorInstance,
					new Character(_symbols[_symbolIndex], timerActorInstance.Character.ForegroundColor, timerActorInstance.Character.BackgroundColor));

				context.EnqueueCommand(setTileCharacterCommand);

				if (++_symbolIndex == _symbols.Length)
				{
					_symbolIndex = 0;
				}

				return EventResult.Completed;
			}
		}
	}
}