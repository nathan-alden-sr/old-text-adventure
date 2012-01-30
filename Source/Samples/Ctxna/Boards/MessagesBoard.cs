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
	public class MessagesBoard : Board
	{
		public static readonly Guid BoardId = Guid.Parse("5eb4bab4-95e6-4fd4-a38a-4245565d3182");
		public static readonly Size BoardSize = new Size(9, 9);
		public static readonly Coordinate[] ExitCoordinates = new[]
		                                                      	{
		                                                      		new Coordinate(0, 3)
		                                                      	};

		private static readonly Coordinate _layerOriginCoordinate = new Coordinate(0, 2);
		private static readonly Size _layerSize = new Size(9, 7);

		public MessagesBoard()
			: base(BoardId, "Messages", "", BoardSize, GetBackgroundLayer(), GetForegroundLayer(), GetActorInstanceLayer(), GetExits().ToArray(), GetTimers().ToArray())
		{
		}

		private static SpriteLayer GetBackgroundLayer()
		{
			var character = new Character(Symbol.LightShade, new Color(35, 103, 187), new Color(28, 83, 150));
			IEnumerable<Sprite> sprites = SpriteFactory.Instance.CreateArea(_layerOriginCoordinate, _layerSize, character);

			return new SpriteLayer(BoardId, BoardSize, sprites);
		}

		private static SpriteLayer GetForegroundLayer()
		{
			var character = new Character(Symbol.Number, Color.White, Color.TransparentBlack);
			IEnumerable<Sprite> borderSprites = SpriteFactory.Instance.CreateBorder(_layerOriginCoordinate, _layerSize, character);
			IEnumerable<Sprite> textLineSprites = SpriteFactory.Instance.CreateCenteredText(
				"Messages",
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
			var messagesActor = new MessagesActor();
			ActorInstance messagesActorInstance = messagesActor.CreateActorInstance(
				BoardId,
				new Coordinate(4, 6),
				new EventHandlerCollection(new PlayerTouchedMessagesActorInstanceEventHandler()));

			return new ActorInstanceLayer(BoardId, BoardSize, messagesActorInstance);
		}

		private static IEnumerable<BoardExit> GetExits()
		{
			yield return new BoardExit(ExitCoordinates[0], BoardExitDirection.Left, BoardsBoard.BoardId, BoardsBoard.ExitCoordinates[1]);
		}

		private static IEnumerable<Timer> GetTimers()
		{
			yield break;
		}

		private class PlayerTouchedMessagesActorInstanceEventHandler : Engine.Game.Events.EventHandler<PlayerTouchedActorInstanceEvent>
		{
			public override EventResult HandleEvent(EventContext context, PlayerTouchedActorInstanceEvent @event)
			{
				Color indent0 = Color.Yellow;
				Color indent1 = Color.White;
				MessageBuilder messageBuilder = Message
					.Build(new Color(43, 96, 80))
					.Text(indent0, "Messages", 1)
					.Text(indent1, "  - Displays scrollable text to the player", 1)
					.Text(indent1, "  - Text, colors, line breaks", 1)
					.Text(indent1, "  - Questions and answers", 1)
					.Text(indent1, "L", 1)
					.Text(indent1, "o", 1)
					.Text(indent1, "r", 1)
					.Text(indent1, "e", 1)
					.Text(indent1, "m", 2)
					.Text(indent1, "i", 1)
					.Text(indent1, "p", 1)
					.Text(indent1, "s", 1)
					.Text(indent1, "u", 1)
					.Text(indent1, "m", 2)
					.Text(indent1, "L", 1)
					.Text(indent1, "o", 1)
					.Text(indent1, "r", 1)
					.Text(indent1, "e", 1)
					.Text(indent1, "m", 2)
					.Text(indent1, "i", 1)
					.Text(indent1, "p", 1)
					.Text(indent1, "s", 1)
					.Text(indent1, "u", 1)
					.Text(indent1, "m", 2)
					.Question(
						"The only guarantee in life is...",
						Color.White,
						Color.Gray,
						Color.White,
						Color.DarkMagenta,
						MessageAnswer.Build("Death"),
						MessageAnswer.Build("Taxes"),
						MessageAnswer.Build("XNA"));

				context.EnqueueCommand(Commands.Message(messageBuilder));

				return EventResult.Complete;
			}
		}
	}
}