using System.Collections.Generic;
using System.Xml.Linq;

using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers.Xml
{
	public class MessagePartSerializer
	{
		public static readonly MessagePartSerializer Instance = new MessagePartSerializer();

		private MessagePartSerializer()
		{
		}

		public IEnumerable<XElement> Serialize(IEnumerable<IMessagePart> parts)
		{
			parts.ThrowIfNull("parts");

			foreach (IMessagePart part in parts)
			{
				var messageColor = part as MessageColor;
				var messageLineBreak = part as MessageLineBreak;
				var messageQuestion = part as MessageQuestion;
				var messageText = part as MessageText;

				if (messageColor != null)
				{
					yield return MessageColorSerializer.Instance.Serialize(messageColor);
				}
				if (messageLineBreak != null)
				{
					yield return MessageLineBreakSerializer.Instance.Serialize(messageLineBreak);
				}
				if (messageQuestion != null)
				{
					yield return MessageQuestionSerializer.Instance.Serialize(messageQuestion);
				}
				if (messageText != null)
				{
					yield return MessageTextSerializer.Instance.Serialize(messageText);
				}
			}
		}

		public IEnumerable<IMessagePart> Deserialize(XContainer partsContainer)
		{
			partsContainer.ThrowIfNull("partsContainer");

			foreach (XElement partElement in partsContainer.Elements())
			{
				switch (partElement.Name.LocalName)
				{
					case "color":
						yield return MessageColorSerializer.Instance.Deserialize(partElement);
						break;
					case "lineBreak":
						yield return MessageLineBreakSerializer.Instance.Deserialize(partElement);
						break;
					case "question":
						yield return MessageQuestionSerializer.Instance.Deserialize(partElement);
						break;
					case "text":
						yield return MessageTextSerializer.Instance.Deserialize(partElement);
						break;
				}
			}
		}
	}
}