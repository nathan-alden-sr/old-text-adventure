using System;

using Junior.Common;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers.Compact
{
	public class PlayerSerializer
	{
		public static readonly PlayerSerializer Instance = new PlayerSerializer();

		private PlayerSerializer()
		{
		}

		public byte[] Serialize(Player player)
		{
			player.ThrowIfNull("player");

			var serializer = new CompactSerializer();

			serializer[0] = player.Id.ToByteArray();
			serializer[1] = player.BoardId.ToByteArray();
			serializer[2] = CoordinateSerializer.Instance.Serialize(player.Coordinate);
			serializer[3] = CharacterSerializer.Instance.Serialize(player.Character);
			serializer[4] = EventHandlerSerializer.Instance.Serialize(player.ActorInstanceTouchedPlayerEventHandler);

			return serializer.Serialize();
		}

		public Player Deserialize(byte[] serializedData)
		{
			serializedData.ThrowIfNull("serializedData");

			var serializer = new CompactSerializer(serializedData);
			var id = new Guid(serializer[0]);
			var boardId = new Guid(serializer[1]);
			Coordinate coordinate = CoordinateSerializer.Instance.Deserialize(serializer[2]);
			Character character = CharacterSerializer.Instance.Deserialize(serializer[3]);
			IEventHandler<ActorInstanceTouchedPlayerEvent> actorInstanceTouchedPlayerEventHandler = EventHandlerSerializer.Instance.Deserialize<ActorInstanceTouchedPlayerEvent>(serializer[4]);

			return new Player(id, boardId, coordinate, character, actorInstanceTouchedPlayerEventHandler);
		}
	}
}