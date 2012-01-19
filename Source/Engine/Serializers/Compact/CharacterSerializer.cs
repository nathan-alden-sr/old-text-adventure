using Junior.Common;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers.Compact
{
	public class CharacterSerializer
	{
		public static readonly CharacterSerializer Instance = new CharacterSerializer();

		private CharacterSerializer()
		{
		}

		public byte[] Serialize(Character character)
		{
			var serializer = new CompactSerializer();

			serializer[0] = new[] { character.Symbol };
			serializer[1] = ColorSerializer.Instance.Serialize(character.ForegroundColor);
			serializer[2] = ColorSerializer.Instance.Serialize(character.BackgroundColor);

			return serializer.Serialize();
		}

		public Character Deserialize(byte[] serializedData)
		{
			serializedData.ThrowIfNull("serializedData");

			var serializer = new CompactSerializer(serializedData);
			byte symbol = serializer[0][0];
			Color foregroundColor = ColorSerializer.Instance.Deserialize(serializer[1]);
			Color backgroundColor = ColorSerializer.Instance.Deserialize(serializer[2]);

			return new Character(symbol, foregroundColor, backgroundColor);
		}
	}
}