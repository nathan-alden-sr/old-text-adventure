using System;
using System.Collections.Generic;
using System.Linq;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Game.Commands;
using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Game.Messages;
using TextAdventure.Engine.Objects;
using TextAdventure.Samples.Factories;
using TextAdventure.Samples.Introduction.Actors;
using TextAdventure.Samples.Introduction.Timers;

namespace TextAdventure.Samples.Introduction.Boards
{
	public class BoardsBoard : Board
	{
		public static readonly Guid BoardId = Guid.Parse("001f5ae5-c617-48c9-8afe-19f784740f64");
		public static readonly Size BoardSize = new Size(17, 15);
		public static readonly Coordinate[] ExitCoordinates = new[]
		                                                      	{
		                                                      		new Coordinate(2, 2),
		                                                      		new Coordinate(16, 8)
		                                                      	};
		private static readonly Coordinate _layerOriginCoordinate = new Coordinate(0, 2);
		private static readonly Size _layerSize = new Size(17, 12);

		public BoardsBoard()
			: base(BoardId, "Boards", "", BoardSize, GetBackgroundLayer(), GetForegroundLayer(), GetActorInstanceLayer(), GetExits(), new BoardEnteredEventHandler(), new BoardExitedEventHandler())
		{
		}

		private static SpriteLayer GetBackgroundLayer()
		{
			var character = new Character(Symbol.LightShade, new Color(48, 48, 48), Color.Black);
			IEnumerable<Sprite> sprites = SpriteFactory.Instance.CreateArea(_layerOriginCoordinate, _layerSize, character);

			return new SpriteLayer(BoardSize, sprites);
		}

		private static SpriteLayer GetForegroundLayer()
		{
			var character = new Character(Symbol.Number, Color.White, Color.TransparentBlack);
			IEnumerable<Sprite> borderSprites = SpriteFactory.Instance.CreateBorder(_layerOriginCoordinate, _layerSize, character);
			IEnumerable<Sprite> textLineSprites = SpriteFactory.Instance.CreateCenteredText(
				"Boards",
				0,
				BoardSize.Width,
				Color.White,
				Color.TransparentBlack);
			var sprites = new List<Sprite>(borderSprites.Concat(textLineSprites));

			sprites.RemoveAll(arg => ExitCoordinates.Contains(arg.Coordinate));

			return new SpriteLayer(BoardSize, sprites);
		}

		private static ActorInstanceLayer GetActorInstanceLayer()
		{
			var actorInstances = new List<ActorInstance>();
			var boardsActor = new BoardsActor();

			for (int i = 0; i < 5; i++)
			{
				ActorInstance actorInstance = ActorInstanceFactory.Instance.CreateActorInstance(
					boardsActor,
					new Coordinate(_layerOriginCoordinate.X + (i * 3) + 1, _layerOriginCoordinate.Y + i + 1),
					playerTouchedActorInstanceEventHandler:new PlayerTouchedBoardsActorInstanceEventHandler());

				actorInstances.Add(actorInstance);
			}

			return new ActorInstanceLayer(BoardSize, actorInstances);
		}

		private static IEnumerable<BoardExit> GetExits()
		{
			yield return new BoardExit(ExitCoordinates[0], BoardExitDirection.Up, ActorsBoard.BoardId, ActorsBoard.ExitCoordinates[1]);
			yield return new BoardExit(ExitCoordinates[1], BoardExitDirection.Right, ActorsBoard.BoardId, ActorsBoard.ExitCoordinates[1]);
		}

		private new class BoardEnteredEventHandler : Engine.Game.Events.EventHandler<BoardEnteredEvent>
		{
			public override void HandleEvent(EventContext context, BoardEnteredEvent @event)
			{
				Timer timer = context.GetTimerById(BoardsActorMoveTimer.TimerId);
				StartTimerCommand command = Commands.StartTimer(timer);

				context.EnqueueCommand(command);
			}
		}

		private new class BoardExitedEventHandler : Engine.Game.Events.EventHandler<BoardExitedEvent>
		{
			public override void HandleEvent(EventContext context, BoardExitedEvent @event)
			{
				Timer timer = context.GetTimerById(BoardsActorMoveTimer.TimerId);
				StopTimerCommand command = Commands.StopTimer(timer);

				context.EnqueueCommand(command);
			}
		}

		private class PlayerTouchedBoardsActorInstanceEventHandler : Engine.Game.Events.EventHandler<PlayerTouchedActorInstanceEvent>
		{
			public override void HandleEvent(EventContext context, PlayerTouchedActorInstanceEvent @event)
			{
				Color indent0 = Color.Yellow;
				Color indent1 = Color.White;
				MessageBuilder messageBuilder = Message
					.Build(Color.DarkBlue)
					.Text(indent0, "Boards", 1)
					.Text(indent1, "  - An area within a world", 1)
					.Text(indent1, "    - Background", 1)
					.Text(indent1, "      - Sprites that can be walked over", 1)
					.Text(indent1, "    - Foreground", 1)
					.Text(indent1, "      - Sprites that can't be walked over", 1)
					.Text(indent1, "    - Actor instance", 1)
					.Text(indent1, "      - Player lives here also");

				context.EnqueueCommand(Commands.Message(messageBuilder));
			}
		}
	}
}