using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers.Compact
{
	public class MessageLineBreakSerializer
	{
		public static readonly MessageLineBreakSerializer Instance = new MessageLineBreakSerializer();

		private MessageLineBreakSerializer()
		{
		}

		public byte[] Serialize(MessageLineBreak messageLineBreak)
		{
			messageLineBreak.ThrowIfNull("messageLineBreak");

			return new CompactSerializer().Serialize();
		}

		public MessageLineBreak Deserialize(byte[] serializedData)
		{
			serializedData.ThrowIfNull("serializedData");

			return new MessageLineBreak();
		}
	}
}