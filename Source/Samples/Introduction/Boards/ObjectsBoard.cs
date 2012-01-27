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

namespace TextAdventure.Samples.Introduction.Boards
{
	public class ObjectsBoard : Board
	{
		public static readonly Guid BoardId = Guid.Parse("7754ecf6-445f-46ff-97b6-7649e5fc2d3e");
		public static readonly Size BoardSize = new Size(7, 9);
		private static readonly Coordinate _layerOriginCoordinate = new Coordinate(0, 2);
		private static readonly Size _layerSize = new Size(7, 7);

		public ObjectsBoard()
			: base(BoardId, "Objects", "", BoardSize, GetBackgroundLayer(), GetForegroundLayer(), GetActorInstanceLayer(), GetExits())
		{
		}

		private static SpriteLayer GetBackgroundLayer()
		{
			var character = new Character(Symbol.LightShade, new Color(116, 90, 60), new Color(152, 118, 79));
			IEnumerable<Sprite> sprites = SpriteFactory.Instance.CreateArea(_layerOriginCoordinate, _layerSize, character);

			return new SpriteLayer(BoardSize, sprites);
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
			var doorCoordinates = new[]
			                      	{
			                      		new Coordinate(_layerOriginCoordinate.X, _layerOriginCoordinate.Y + 4),
			                      		new Coordinate(_layerOriginCoordinate.X + (_layerSize.Width / 2), _layerOriginCoordinate.Y + _layerSize.Height - 1)
			                      	};

			sprites.RemoveAll(arg => doorCoordinates.Contains(arg.Coordinate));

			return new SpriteLayer(BoardSize, sprites);
		}

		private static ActorInstanceLayer GetActorInstanceLayer()
		{
			var objectsActor = new ObjectsActor();
			ActorInstance actorInstance = ActorInstanceFactory.Instance.CreateActorInstance(
				objectsActor,
				new Coordinate(BoardSize.Width / 2, 4),
				playerTouchedActorInstanceEventHandler:new PlayerTouchedObjectsActorEventHandler());

			return new ActorInstanceLayer(_layerSize, new[] { actorInstance });
		}

		private static IEnumerable<BoardExit> GetExits()
		{
			yield return new BoardExit(new Coordinate(0, 6), BoardExitDirection.Left, IntroductionBoard.BoardId, new Coordinate(50, 11));
		}

		private class PlayerTouchedObjectsActorEventHandler : Engine.Game.Events.EventHandler<PlayerTouchedActorInstanceEvent>
		{
			public override void HandleEvent(EventContext context, PlayerTouchedActorInstanceEvent @event)
			{
				Color indent0 = Color.Yellow;
				Color indent1 = Color.White;
				MessageBuilder messageBuilder = Message
					.Build(Color.DarkBlue)
					.Text(indent0, "Objects", 1)
					.Text(indent1, "  - Any class that helps compose a world -- actors, boards, timers, etc.", 1)
					.Text(indent1, "  - Some objects can respond to events", 1)
					.Text(indent1, "  - Serialized and deserialized");

				context.EnqueueCommand(Commands.Message(messageBuilder));
			}
		}
	}
}