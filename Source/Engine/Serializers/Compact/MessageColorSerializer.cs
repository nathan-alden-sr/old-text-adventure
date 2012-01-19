using Junior.Common;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers.Compact
{
	public class MessageColorSerializer
	{
		public static readonly MessageColorSerializer Instance = new MessageColorSerializer();

		private MessageColorSerializer()
		{
		}

		public byte[] Serialize(MessageColor messageColor)
		{
			messageColor.ThrowIfNull("messageColor");

			var serializer = new CompactSerializer();

			serializer[0] = ColorSerializer.Instance.Serialize(messageColor.Color);

			return serializer.Serialize();
		}

		public MessageColor Deserialize(byte[] serializedData)
		{
			serializedData.ThrowIfNull("serializedData");

			var serializer = new CompactSerializer(serializedData);
			Color color = ColorSerializer.Instance.Deserialize(serializer[0]);

			return new MessageColor(color);
		}
	}
}