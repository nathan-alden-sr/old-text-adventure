using System;
using System.Text;

using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers.Compact
{
	public class MessagePartSerializer
	{
		public static readonly MessagePartSerializer Instance = new MessagePartSerializer();

		private MessagePartSerializer()
		{
		}

		public byte[] Serialize(IMessagePart messagePart)
		{
			messagePart.ThrowIfNull("messagePart");

			var serializer = new CompactSerializer();
			var messageColor = messagePart as MessageColor;
			var messageLineBreak = messagePart as MessageLineBreak;
			var messageQuestion = messagePart as MessageQuestion;
			var messageText = messagePart as MessageText;

			if (messageColor != null)
			{
				serializer[0] = Encoding.UTF8.GetBytes("Color");
				serializer[1] = MessageColorSerializer.Instance.Serialize(messageColor);
			}
			else if (messageLineBreak != null)
			{
				serializer[0] = Encoding.UTF8.GetBytes("LineBreak");
				serializer[1] = MessageLineBreakSerializer.Instance.Serialize(messageLineBreak);
			}
			else if (messageQuestion != null)
			{
				serializer[0] = Encoding.UTF8.GetBytes("Question");
				serializer[1] = MessageQuestionSerializer.Instance.Serialize(messageQuestion);
			}
			else if (messageText != null)
			{
				serializer[0] = Encoding.UTF8.GetBytes("Text");
				serializer[1] = MessageTextSerializer.Instance.Serialize(messageText);
			}
			else
			{
				throw new ArgumentException(String.Format("Unknown message part type '{0}'.", messagePart.GetType().Name));
			}

			return serializer.Serialize();
		}

		public IMessagePart Deserialize(byte[] serializedData)
		{
			serializedData.ThrowIfNull("serializedData");

			var serializer = new CompactSerializer(serializedData);
			string partType = Encoding.UTF8.GetString(serializer[0]);

			switch (partType)
			{
				case "Color":
					return MessageColorSerializer.Instance.Deserialize(serializer[1]);
				case "LineBreak":
					return MessageLineBreakSerializer.Instance.Deserialize(serializer[1]);
				case "Question":
					return MessageQuestionSerializer.Instance.Deserialize(serializer[1]);
				case "Text":
					return MessageTextSerializer.Instance.Deserialize(serializer[1]);
				default:
					throw new ArgumentException(String.Format("Unknown message part type '{0}'.", partType));
			}
		}
	}
}