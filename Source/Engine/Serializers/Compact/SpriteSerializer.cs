using Junior.Common;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers.Compact
{
	public class SpriteSerializer
	{
		public static readonly SpriteSerializer Instance = new SpriteSerializer();

		private SpriteSerializer()
		{
		}

		public byte[] Serialize(Sprite sprite)
		{
			sprite.ThrowIfNull("sprite");

			var serializer = new CompactSerializer();

			serializer[0] = CoordinateSerializer.Instance.Serialize(sprite.Coordinate);
			serializer[1] = CharacterSerializer.Instance.Serialize(sprite.Character);

			return serializer.Serialize();
		}

		public Sprite Deserialize(byte[] serializedData)
		{
			serializedData.ThrowIfNull("serializedData");

			var serializer = new CompactSerializer(serializedData);
			Coordinate coordinate = CoordinateSerializer.Instance.Deserialize(serializer[0]);
			Character character = CharacterSerializer.Instance.Deserialize(serializer[1]);

			return new Sprite(coordinate, character);
		}
	}
}