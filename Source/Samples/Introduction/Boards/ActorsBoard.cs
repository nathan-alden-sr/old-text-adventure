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
	public class ActorsBoard : Board
	{
		public static readonly Guid BoardId = Guid.Parse("7f33c23c-58eb-44f5-904f-a7e7e971adb6");
		public static readonly Size BoardSize = new Size(17, 15);
		public static readonly Coordinate[] ExitCoordinates = new[]
		                                                      	{
		                                                      		new Coordinate(8, 3),
		                                                      		new Coordinate(13, 14)
		                                                      	};
		private static readonly Coordinate _layerOriginCoordinate = new Coordinate(0, 3);
		private static readonly Size _layerSize = new Size(17, 12);

		public ActorsBoard()
			: base(BoardId, "Actors", "", BoardSize, GetBackgroundLayer(), GetForegroundLayer(), GetActorInstanceLayer(), GetExits())
		{
		}

		private static SpriteLayer GetBackgroundLayer()
		{
			var character = new Character(Symbol.LightShade, new Color(132, 133, 32), new Color(92, 93, 23));
			IEnumerable<Sprite> sprites = SpriteFactory.Instance.CreateArea(_layerOriginCoordinate, _layerSize, character);

			return new SpriteLayer(BoardSize, sprites);
		}

		private static SpriteLayer GetForegroundLayer()
		{
			var character = new Character(Symbol.Number, Color.White, Color.TransparentBlack);
			IEnumerable<Sprite> borderSprites = SpriteFactory.Instance.CreateBorder(_layerOriginCoordinate, _layerSize, character);
			IEnumerable<Sprite> textLine1Sprites = SpriteFactory.Instance.CreateCenteredText(
				"Actors",
				0,
				BoardSize.Width,
				Color.White,
				Color.TransparentBlack);
			IEnumerable<Sprite> textLine2Sprites = SpriteFactory.Instance.CreateCenteredText(
				"Actor Instances",
				1,
				BoardSize.Width,
				Color.White,
				Color.TransparentBlack);
			var sprites = new List<Sprite>(borderSprites
			                               	.Concat(textLine1Sprites)
			                               	.Concat(textLine2Sprites));

			sprites.RemoveAll(arg => ExitCoordinates.Contains(arg.Coordinate));

			return new SpriteLayer(BoardSize, sprites);
		}

		private static ActorInstanceLayer GetActorInstanceLayer()
		{
			var actorInstances = new List<ActorInstance>();

			for (int x = _layerOriginCoordinate.X + 1; x < _layerOriginCoordinate.X + _layerSize.Width - 1; x++)
			{
				var boulderActor = new BoulderActor();
				ActorInstance boulderActorInstance = ActorInstanceFactory.Instance.CreateActorInstance(
					boulderActor,
					new Coordinate(x, 9),
					playerTouchedActorInstanceEventHandler:new PlayerTouchedBoulderActorEventHandler());

				actorInstances.Add(boulderActorInstance);
			}

			var actorsActor = new ActorsActor();
			ActorInstance actorsActorInstance = ActorInstanceFactory.Instance.CreateActorInstance(
				actorsActor,
				new Coordinate(2, 5),
				playerTouchedActorInstanceEventHandler:new PlayerTouchedActorsActorEventHandler());

			actorInstances.Add(actorsActorInstance);

			return new ActorInstanceLayer(BoardSize, actorInstances);
		}

		private static IEnumerable<BoardExit> GetExits()
		{
			yield return new BoardExit(ExitCoordinates[0], BoardExitDirection.Up, ObjectsBoard.BoardId, ObjectsBoard.ExitCoordinates[1]);
		}

		private class PlayerTouchedActorsActorCopyEventHandler : Engine.Game.Events.EventHandler<PlayerTouchedActorInstanceEvent>
		{
			public override void HandleEvent(EventContext context, PlayerTouchedActorInstanceEvent @event)
			{
				Color indent0 = Color.Yellow;
				Color indent1 = Color.White;
				MessageBuilder messageBuilder = Message
					.Build(Color.DarkBlue)
					.Text(indent0, "Actor Instances", 1)
					.Text(indent1, "  - Provide the player something with which to interact", 1)
					.Text(indent1, "    - Monsters, smileys (NPCs), items, etc.", 1)
					.Text(indent1, "  - Event handlers make them useful", 1)
					.Text(indent1, "  - Can also interact with other actor instances");

				context.EnqueueCommand(Commands.Message(messageBuilder));
			}
		}

		private class PlayerTouchedActorsActorEventHandler : Engine.Game.Events.EventHandler<PlayerTouchedActorInstanceEvent>
		{
			private bool _handledOnce;

			public override void HandleEvent(EventContext context, PlayerTouchedActorInstanceEvent @event)
			{
				if (_handledOnce)
				{
					return;
				}

				_handledOnce = true;

				Color indent0 = Color.Yellow;
				Color indent1 = Color.White;
				MessageBuilder messageBuilder = Message
					.Build(Color.DarkBlue)
					.Text(indent0, "Actors", 1)
					.Text(indent1, "  - Provide a template for actor instances");

				context.EnqueueCommand(Commands.Message(messageBuilder));

				Actor actor = context.GetActorById(@event.Target.ActorId);
				Guid actorInstanceId = Guid.NewGuid();
				ActorInstanceCreateCommand actorInstanceCreateCommand = Commands.ActorInstanceCreate(
					actor,
					actorInstanceId,
					ExitCoordinates[0],
					actor.Character,
					playerTouchedActorInstanceEventHandler:new PlayerTouchedActorsActorCopyEventHandler());

				context.EnqueueCommand(actorInstanceCreateCommand);

				ChainedCommand chainedCommand = Commands
					.Chain(Commands.Delay(TimeSpan.FromMilliseconds(200)))
					.And(Commands
					     	.Contextual(arg => Commands.ActorInstanceMoveDown(arg.GetActorInstanceById(actorInstanceId)))
					     	.Repeat(TimeSpan.FromMilliseconds(200), 2))
					.And(Commands.Delay(TimeSpan.FromMilliseconds(200)))
					.And(Commands
					     	.Contextual(arg => Commands.ActorInstanceMoveRight(arg.GetActorInstanceById(actorInstanceId)))
					     	.Repeat(TimeSpan.FromMilliseconds(200), 5));

				context.EnqueueCommand(chainedCommand);
			}
		}

		private class PlayerTouchedBoulderActorEventHandler : Engine.Game.Events.EventHandler<PlayerTouchedActorInstanceEvent>
		{
			public override void HandleEvent(EventContext context, PlayerTouchedActorInstanceEvent @event)
			{
				switch (@event.TouchDirection)
				{
					case TouchDirection.FromBelow:
						context.ExecuteCommand(Commands.ActorInstanceMoveUp(@event.Target));
						break;
					case TouchDirection.FromAbove:
						context.ExecuteCommand(Commands.ActorInstanceMoveDown(@event.Target));
						break;
					case TouchDirection.FromLeft:
						context.ExecuteCommand(Commands.ActorInstanceMoveRight(@event.Target));
						break;
					case TouchDirection.FromRight:
						context.ExecuteCommand(Commands.ActorInstanceMoveLeft(@event.Target));
						break;
				}
			}
		}
	}
}