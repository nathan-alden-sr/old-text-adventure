using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Junior.Common;

using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers.Compact
{
	public class MessageAnswerSerializer
	{
		public static readonly MessageAnswerSerializer Instance = new MessageAnswerSerializer();

		private MessageAnswerSerializer()
		{
		}

		public byte[] Serialize(MessageAnswer messageAnswer)
		{
			messageAnswer.ThrowIfNull("messageAnswer");

			var serializer = new CompactSerializer();

			serializer[0] = messageAnswer.Id.ToByteArray();
			serializer[1] = Encoding.UTF8.GetBytes(messageAnswer.Text);

			var partSerializer = new CompactSerializer();
			int index = 0;

			foreach (IMessagePart part in messageAnswer.Parts)
			{
				partSerializer[index++] = MessagePartSerializer.Instance.Serialize(part);
			}

			serializer[2] = partSerializer.Serialize();
			serializer[3] = EventHandlerSerializer.Instance.Serialize(messageAnswer.AnswerSelectedEventHandler);

			return serializer.Serialize();
		}

		public MessageAnswer Deserialize(byte[] serializedData)
		{
			serializedData.ThrowIfNull("serializedData");

			var serializer = new CompactSerializer(serializedData);
			var id = new Guid(serializer[0]);
			string text = Encoding.UTF8.GetString(serializer[1]);
			var partSerializer = new CompactSerializer(serializer[2]);
			IEnumerable<IMessagePart> parts = partSerializer.FieldIndices.Select(arg => MessagePartSerializer.Instance.Deserialize(partSerializer[arg]));
			IEventHandler<AnswerSelectedEvent> answerSelectedEventHandler = EventHandlerSerializer.Instance.Deserialize<AnswerSelectedEvent>(serializer[3]);

			return new MessageAnswer(id, text, parts, answerSelectedEventHandler);
		}
	}
}