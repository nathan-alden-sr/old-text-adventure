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
	public class OtherTopicsBoard : Board
	{
		public static readonly Guid BoardId = Guid.Parse("c3eda69e-c71e-4bb0-9e7a-a7055ebb6b9c");
		public static readonly Size BoardSize = new Size(21, 13);
		public static readonly Coordinate[] ExitCoordinates = new[]
			{
				new Coordinate(10, 2)
			};

		private static readonly Coordinate _layerOriginCoordinate = new Coordinate(0, 2);
		private static readonly Size _layerSize = new Size(21, 10);

		public OtherTopicsBoard()
			: base(BoardId, "OtherTopics", "", BoardSize, GetBackgroundLayer(), GetForegroundLayer(), GetActorInstanceLayer(), GetExits().ToArray(), GetTimers().ToArray())
		{
		}

		private static SpriteLayer GetBackgroundLayer()
		{
			var character = new Character(Symbol.LightShade, new Color(91, 86, 69), new Color(130, 122, 99));
			IEnumerable<Sprite> sprites = SpriteFactory.Instance.CreateArea(_layerOriginCoordinate, _layerSize, character);

			return new SpriteLayer(BoardId, BoardSize, sprites);
		}

		private static SpriteLayer GetForegroundLayer()
		{
			var character = new Character(Symbol.MediumShade, Color.LightGray, Color.TransparentBlack);
			IEnumerable<Sprite> borderSprites = SpriteFactory.Instance.CreateBorder(_layerOriginCoordinate, _layerSize, character);
			IEnumerable<Sprite> textLineSprites = SpriteFactory.Instance.CreateCenteredText(
				"Other Topics",
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
			var otherTopicsActor = new OtherTopicsActor();
			ActorInstance otherTopicsActorInstance = otherTopicsActor.CreateActorInstance(
				BoardId,
				new Coordinate(10, 6),
				new EventHandlerCollection(new PlayerTouchedOtherTopicsActorInstanceEventHandler()));

			return new ActorInstanceLayer(BoardId, BoardSize, otherTopicsActorInstance);
		}

		private static IEnumerable<BoardExit> GetExits()
		{
			yield return new BoardExit(ExitCoordinates[0], BoardExitDirection.Up, OtherObjectsBoard.BoardId, OtherObjectsBoard.ExitCoordinates[1]);
		}

		private static IEnumerable<Timer> GetTimers()
		{
			yield break;
		}

		private class PlayerTouchedOtherTopicsActorInstanceEventHandler : Engine.Game.Events.EventHandler<PlayerTouchedActorInstanceEvent>
		{
			public override EventResult HandleEvent(EventContext context, PlayerTouchedActorInstanceEvent @event)
			{
				Color indent0 = Color.Yellow;
				Color indent1 = Color.White;
				MessageBuilder messageBuilder = Message
					.Build(Color.DarkMagenta)
					.Text(indent0, "Commands", 1)
					.Text(indent1, "  - Control how the world changes", 1)
					.Text(indent1, "  - Game designers use commands to modify object state instead of modifying objects directly", 1)
					.Text(indent1, "  - Can be chained, repeated and retried", 2)
					.Text(indent0, "Events", 1)
					.Text(indent1, "  - Raised when something interesting happens", 1)
					.Text(indent1, "  - Provide context to event handlers", 2)
					.Text(indent0, "Serialization and Deserialization", 1)
					.Text(indent1, "  - XML", 1)
					.Text(indent1, "  - Binary");

				context.EnqueueCommand(Commands.Message(messageBuilder));

				messageBuilder = Message
					.Build(Color.DarkRed)
					.Text(indent0, "Replacing XnaGame", 1)
					.Text(indent1, "  - Game class has too many responsibilities", 1)
					.Text(indent1, "  - XnaGame updates and draws", 1)
					.Text(indent1, "    - Doesn't handle Windows message pump", 1)
					.Text(indent1, "    - Doesn't act as a game loop", 1)
					.Text(indent1, "    - No game component concept", 1)
					.Text(indent1, "  - TimedXnaGame manages game time", 1)
					.Text(indent1, "  - XnaControl manages the GraphicsDevice", 1)
					.Text(indent1, "  - Updaters and renderers", 1)
					.Text(indent1, "    - Replaces XNA's game components", 1)
					.Text(indent1, "    - Given only the information they need", 1)
					.Text(indent1, "      - UpdateParameters, RenderParameters and state objects", 1)
					.Text(indent1, "    - Update/draw order controlled by order in which instances are added to collections", 1)
					.Text(indent1, "  - Windows message pump and game loop", 1)
					.Text(indent1, "  - GameForm");

				context.EnqueueCommand(Commands.Message(messageBuilder));

				messageBuilder = Message
					.Build(Color.DarkBlue)
					.Text(indent0, "Managing Input", 1)
					.Text(indent1, "  - Keyboard helpers", 1)
					.Text(indent1, "  - Focus");

				context.EnqueueCommand(Commands.Message(messageBuilder));

				messageBuilder = Message
					.Build(Color.DarkGreen)
					.Text(indent0, "Windows", 1)
					.Text(indent1, "  - Abstractions for a rectangular area", 1)
					.Text(indent1, "  - Bordered and textured", 1)
					.Text(indent1, "  - Support basic alignment");

				context.EnqueueCommand(Commands.Message(messageBuilder));

				messageBuilder = Message
					.Build(Color.DarkGray)
					.Text(indent0, "Text Adventure Editor", 1)
					.Text(indent1, "  - Also uses XnaGame", 1)
					.Text(indent1, "  - Not much done yet");

				context.EnqueueCommand(Commands.Message(messageBuilder));

				messageBuilder = Message
					.Build(Color.DarkBlue)
					.Text(indent0, "That's all!", 1)
					.Text(indent1, "  - Questions?", 2)
					.Text(indent0, "Text Adventure code: ")
					.Text(Color.White, "https://github.com/NathanAlden/TextAdventure", 2)
					.Text(indent0, "My blog: ")
					.Text(Color.White, "http://blog.TheCognizantCoder.com", 2)
					.Text(Color.LightMagenta, "Thank you for your time!");

				context.EnqueueCommand(Commands.Message(messageBuilder));

				return EventResult.Completed;
			}
		}
	}
}