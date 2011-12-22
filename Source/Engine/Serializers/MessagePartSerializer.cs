using System.Collections.Generic;
using System.Xml.Linq;

using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers
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
				if (part is MessageColor)
				{
					yield return MessageColorSerializer.Instance.Serialize((MessageColor)part);
				}
				if (part is MessageLineBreak)
				{
					yield return MessageLineBreakSerializer.Instance.Serialize((MessageLineBreak)part);
				}
				if (part is MessageQuestion)
				{
					yield return MessageQuestionSerializer.Instance.Serialize((MessageQuestion)part);
				}
				if (part is MessageText)
				{
					yield return MessageTextSerializer.Instance.Serialize((MessageText)part);
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