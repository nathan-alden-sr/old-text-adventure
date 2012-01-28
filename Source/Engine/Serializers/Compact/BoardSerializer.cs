using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Junior.Common;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers.Compact
{
	public class BoardSerializer
	{
		public static readonly BoardSerializer Instance = new BoardSerializer();

		private BoardSerializer()
		{
		}

		public byte[] Serializer(Board board)
		{
			board.ThrowIfNull("board");

			var serializer = new CompactSerializer();

			serializer[0] = board.Id.ToByteArray();
			serializer[1] = Encoding.UTF8.GetBytes(board.Name);
			serializer[2] = Encoding.UTF8.GetBytes(board.Description);
			serializer[3] = SizeSerializer.Instance.Seralize(board.Size);
			serializer[4] = SpriteLayerSerializer.Instance.Serialize(board.BackgroundLayer);
			serializer[5] = SpriteLayerSerializer.Instance.Serialize(board.ForegroundLayer);
			serializer[6] = ActorInstanceLayerSerializer.Instance.Serialize(board.ActorInstanceLayer);

			var boardExitSerializer = new CompactSerializer();
			int index = 0;

			foreach (BoardExit boardExit in board.Exits)
			{
				boardExitSerializer[index++] = BoardExitSerializer.Instance.Serialize(boardExit);
			}

			serializer[7] = boardExitSerializer.Serialize();
			serializer[8] = EventHandlerCollectionSerializer.Instance.Serialize(board.EventHandlerCollection);

			return serializer.Serialize();
		}

		public Board Deserialize(byte[] serializedData)
		{
			serializedData.ThrowIfNull("serializedData");

			var serializer = new CompactSerializer(serializedData);
			var id = new Guid(serializer[0]);
			string name = Encoding.UTF8.GetString(serializer[1]);
			string description = Encoding.UTF8.GetString(serializer[2]);
			Size size = SizeSerializer.Instance.Deserialize(serializer[3]);
			SpriteLayer backgroundLayer = SpriteLayerSerializer.Instance.Deserialize(serializer[4]);
			SpriteLayer foregroundLayer = SpriteLayerSerializer.Instance.Deserialize(serializer[5]);
			ActorInstanceLayer actorInstanceLayer = ActorInstanceLayerSerializer.Instance.Deserialize(serializer[6]);
			var boardExitSerializer = new CompactSerializer(serializer[7]);
			IEnumerable<BoardExit> exits = boardExitSerializer.FieldIndices.Select(arg => BoardExitSerializer.Instance.Deserialize(boardExitSerializer[arg]));
			EventHandlerCollection eventHandlerCollection = EventHandlerCollectionSerializer.Instance.Deserialize(serializer[8]);

			return new Board(id, name, description, size, backgroundLayer, foregroundLayer, actorInstanceLayer, exits, eventHandlerCollection);
		}
	}
}