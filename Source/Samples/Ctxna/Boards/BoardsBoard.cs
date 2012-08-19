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
using TextAdventure.Samples.Ctxna.Timers;
using TextAdventure.Samples.Factories;

namespace TextAdventure.Samples.Ctxna.Boards
{
	public class BoardsBoard : Board
	{
		public static readonly Guid BoardExitsActorInstanceId = Guid.Parse("83843875-b4df-4932-90a1-d7f22e172d50");
		public static readonly Guid BoardId = Guid.Parse("001f5ae5-c617-48c9-8afe-19f784740f64");
		public static readonly Size BoardSize = new Size(17, 15);
		public static readonly Coordinate[] ExitCoordinates = new[]
			{
				new Coordinate(2, 3),
				new Coordinate(16, 8)
			};
		private static readonly Coordinate _layerOriginCoordinate = new Coordinate(0, 3);
		private static readonly Size _layerSize = new Size(17, 12);

		public BoardsBoard()
			: base(BoardId, "Boards", "", BoardSize, GetBackgroundLayer(), GetForegroundLayer(), GetActorInstanceLayer(), GetExits().ToArray(), GetTimers().ToArray())
		{
		}

		protected override EventResult OnEntered(EventContext context, BoardEnteredEvent @event)
		{
			foreach (Timer timer in Timers)
			{
				context.EnqueueCommand(Commands.PerformTimerAction(timer, TimerAction.Start));
			}

			return base.OnEntered(context, @event);
		}

		private static SpriteLayer GetBackgroundLayer()
		{
			var character = new Character(Symbol.LightShade, new Color(48, 48, 48), Color.Black);
			IEnumerable<Sprite> sprites = SpriteFactory.Instance.CreateArea(_layerOriginCoordinate, _layerSize, character);

			return new SpriteLayer(BoardId, BoardSize, sprites);
		}

		private static SpriteLayer GetForegroundLayer()
		{
			var character = new Character(Symbol.Number, Color.White, Color.TransparentBlack);
			IEnumerable<Sprite> borderSprites = SpriteFactory.Instance.CreateBorder(_layerOriginCoordinate, _layerSize, character);
			IEnumerable<Sprite> textLine1Sprites = SpriteFactory.Instance.CreateCenteredText(
				"Boards",
				0,
				BoardSize.Width,
				Color.White,
				Color.TransparentBlack);
			IEnumerable<Sprite> textLine2Sprites = SpriteFactory.Instance.CreateCenteredText(
				"Board Exits",
				1,
				BoardSize.Width,
				Color.White,
				Color.TransparentBlack);
			var sprites = new List<Sprite>(borderSprites
				                               .Concat(textLine1Sprites)
				                               .Concat(textLine2Sprites));

			sprites.RemoveAll(arg => ExitCoordinates.Contains(arg.Coordinate));

			return new SpriteLayer(BoardId, BoardSize, sprites);
		}

		private static ActorInstanceLayer GetActorInstanceLayer()
		{
			var actorInstances = new List<ActorInstance>();
			var boardsActor = new BoardsActor();

			for (int i = 0; i < 5; i++)
			{
				ActorInstance boardsActorInstance = boardsActor.CreateActorInstance(
					BoardId,
					new Coordinate(_layerOriginCoordinate.X + (i * 3) + 1, _layerOriginCoordinate.Y + (i * 2) + 1),
					new EventHandlerCollection(new PlayerTouchedBoardsActorInstanceEventHandler()));

				actorInstances.Add(boardsActorInstance);
			}

			var boardExitsActor = new BoardExitsActor();
			ActorInstance boardExitsActorInstance = boardExitsActor.CreateActorInstance(
				BoardExitsActorInstanceId,
				BoardId,
				new Coordinate(16, 8),
				new EventHandlerCollection(new PlayerTouchedBoardExitsActorInstanceEventHandler()));

			actorInstances.Add(boardExitsActorInstance);

			return new ActorInstanceLayer(BoardId, BoardSize, actorInstances);
		}

		private static IEnumerable<BoardExit> GetExits()
		{
			yield return new BoardExit(ExitCoordinates[0], BoardExitDirection.Up, ActorsBoard.BoardId, ActorsBoard.ExitCoordinates[1]);
			yield return new BoardExit(ExitCoordinates[1], BoardExitDirection.Right, MessagesBoard.BoardId, MessagesBoard.ExitCoordinates[0]);
		}

		private static IEnumerable<Timer> GetTimers()
		{
			yield return new BoardsActorInstancesMoveTimer();
		}

		private class PlayerTouchedBoardExitsActorInstanceEventHandler : Engine.Game.Events.EventHandler<PlayerTouchedActorInstanceEvent>
		{
			public override EventResult HandleEvent(EventContext context, PlayerTouchedActorInstanceEvent @event)
			{
				Color indent0 = Color.Yellow;
				Color indent1 = Color.White;
				MessageBuilder messageBuilder = Message
					.Build(Color.DarkBlue, new EventHandlerCollection(new PlayerTouchedBoardExitsActorInstanceMessageClosedEventHandler()))
					.Text(indent0, "Board Exits", 1)
					.Text(indent1, "  - Travel points to other boards");

				context.EnqueueCommand(Commands.Message(messageBuilder));

				return EventResult.Completed;
			}
		}

		private class PlayerTouchedBoardExitsActorInstanceMessageClosedEventHandler : Engine.Game.Events.EventHandler<MessageClosedEvent>
		{
			public override EventResult HandleEvent(EventContext context, MessageClosedEvent @event)
			{
				ActorInstance actorInstance = context.GetActorInstanceById(BoardExitsActorInstanceId);

				context.EnqueueCommand(Commands.ActorInstanceDestroy(actorInstance));

				return EventResult.Completed;
			}
		}

		private class PlayerTouchedBoardsActorInstanceEventHandler : Engine.Game.Events.EventHandler<PlayerTouchedActorInstanceEvent>
		{
			public override EventResult HandleEvent(EventContext context, PlayerTouchedActorInstanceEvent @event)
			{
				Color indent0 = Color.Yellow;
				Color indent1 = Color.White;
				MessageBuilder messageBuilder = Message
					.Build(Color.DarkBlue)
					.Text(indent0, "Boards", 1)
					.Text(indent1, "  - An area within a world", 1)
					.Text(indent1, "    - Background layer", 1)
					.Text(indent1, "      - Sprites that can be walked over", 1)
					.Text(indent1, "    - Foreground layer", 1)
					.Text(indent1, "      - Sprites that can't be walked over", 1)
					.Text(indent1, "    - Actor instance layer", 1)
					.Text(indent1, "      - Player lives here also");

				context.EnqueueCommand(Commands.Message(messageBuilder));

				return EventResult.Completed;
			}
		}
	}
}