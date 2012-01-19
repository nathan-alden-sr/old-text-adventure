using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Junior.Common;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers.Compact
{
	public class MessageSerializer
	{
		public static readonly MessageSerializer Instance = new MessageSerializer();

		private MessageSerializer()
		{
		}

		public byte[] Serialize(Message message)
		{
			message.ThrowIfNull("message");

			var serializer = new CompactSerializer();

			serializer[0] = message.Id.ToByteArray();
			serializer[1] = Encoding.UTF8.GetBytes(message.Name);
			serializer[2] = Encoding.UTF8.GetBytes(message.Description);
			serializer[3] = ColorSerializer.Instance.Serialize(message.BackgroundColor);

			var partSerializer = new CompactSerializer();
			int index = 0;

			foreach (IMessagePart part in message.Parts)
			{
				partSerializer[index++] = MessagePartSerializer.Instance.Serialize(part);
			}

			serializer[4] = partSerializer.Serialize();

			return serializer.Serialize();
		}

		public Message Deserialize(byte[] serializedData)
		{
			serializedData.ThrowIfNull("serializedData");

			var serializer = new CompactSerializer(serializedData);
			var id = new Guid(serializer[0]);
			string name = Encoding.UTF8.GetString(serializer[1]);
			string description = Encoding.UTF8.GetString(serializer[2]);
			Color backgroundColor = ColorSerializer.Instance.Deserialize(serializer[3]);
			var partSerializer = new CompactSerializer(serializer[4]);
			IEnumerable<IMessagePart> parts = partSerializer.FieldIndices.Select(arg => MessagePartSerializer.Instance.Deserialize(partSerializer[arg]));

			return new Message(id, name, description, backgroundColor, parts);
		}
	}
}