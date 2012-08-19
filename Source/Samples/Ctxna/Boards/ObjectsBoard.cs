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
using TextAdventure.Samples.Factories;

namespace TextAdventure.Samples.Ctxna.Boards
{
	public class ObjectsBoard : Board
	{
		public static readonly Guid BoardId = Guid.Parse("7754ecf6-445f-46ff-97b6-7649e5fc2d3e");
		public static readonly Size BoardSize = new Size(7, 9);
		public static readonly Coordinate[] ExitCoordinates = new[]
			{
				new Coordinate(0, 6),
				new Coordinate(3, 8)
			};

		private static readonly Coordinate _layerOriginCoordinate = new Coordinate(0, 2);
		private static readonly Size _layerSize = new Size(7, 7);

		public ObjectsBoard()
			: base(BoardId, "Objects", "", BoardSize, GetBackgroundLayer(), GetForegroundLayer(), GetActorInstanceLayer(), GetExits().ToArray(), GetTimers().ToArray())
		{
		}

		private static SpriteLayer GetBackgroundLayer()
		{
			var character = new Character(Symbol.LightShade, new Color(116, 90, 60), new Color(152, 118, 79));
			IEnumerable<Sprite> sprites = SpriteFactory.Instance.CreateArea(_layerOriginCoordinate, _layerSize, character);

			return new SpriteLayer(BoardId, BoardSize, sprites);
		}

		private static SpriteLayer GetForegroundLayer()
		{
			var character = new Character(Symbol.Number, Color.White, Color.TransparentBlack);
			IEnumerable<Sprite> borderSprites = SpriteFactory.Instance.CreateBorder(_layerOriginCoordinate, _layerSize, character);
			IEnumerable<Sprite> textLineSprites = SpriteFactory.Instance.CreateCenteredText(
				"Objects",
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
			var objectsActor = new ObjectsActor();
			ActorInstance objectsActorInstance = objectsActor.CreateActorInstance(
				BoardId,
				new Coordinate(3, 4),
				new EventHandlerCollection(new PlayerTouchedObjectsActorEventHandler()));

			return new ActorInstanceLayer(BoardId, BoardSize, objectsActorInstance);
		}

		private static IEnumerable<BoardExit> GetExits()
		{
			yield return new BoardExit(ExitCoordinates[0], BoardExitDirection.Left, IntroductionBoard.BoardId, IntroductionBoard.ExitCoordinates[0]);
			yield return new BoardExit(ExitCoordinates[1], BoardExitDirection.Down, ActorsBoard.BoardId, ActorsBoard.ExitCoordinates[0]);
		}

		private static IEnumerable<Timer> GetTimers()
		{
			yield break;
		}

		private class PlayerTouchedObjectsActorEventHandler : Engine.Game.Events.EventHandler<PlayerTouchedActorInstanceEvent>
		{
			public override EventResult HandleEvent(EventContext context, PlayerTouchedActorInstanceEvent @event)
			{
				Color indent0 = Color.Yellow;
				Color indent1 = Color.White;
				MessageBuilder messageBuilder = Message
					.Build(Color.DarkBlue)
					.Text(indent0, "Objects", 1)
					.Text(indent1, "  - Any class that helps compose a world -- actors, boards, timers, etc.", 1)
					.Text(indent1, "  - Most objects have unique IDs (GUIDs)", 1)
					.Text(indent1, "  - Some objects can respond to events", 1)
					.Text(indent1, "  - Serialized and deserialized");

				context.EnqueueCommand(Commands.Message(messageBuilder));

				return EventResult.Completed;
			}
		}
	}
}