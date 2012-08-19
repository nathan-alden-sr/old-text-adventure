using System.Text;

using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers.Compact
{
	public class MessageTextSerializer
	{
		public static readonly MessageTextSerializer Instance = new MessageTextSerializer();

		private MessageTextSerializer()
		{
		}

		public byte[] Serialize(MessageText messageText)
		{
			messageText.ThrowIfNull("messageText");

			var serializer = new CompactSerializer();

			serializer[0] = Encoding.UTF8.GetBytes(messageText.Text);

			return serializer.Serialize();
		}

		public MessageText Deserialize(byte[] serializedData)
		{
			serializedData.ThrowIfNull("serializedData");

			var serializer = new CompactSerializer(serializedData);
			string text = Encoding.UTF8.GetString(serializer[0]);

			return new MessageText(text);
		}
	}
}