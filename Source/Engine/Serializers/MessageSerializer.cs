using System;
using System.Xml.Linq;

using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers
{
	public class MessageSerializer
	{
		public static readonly MessageSerializer Instance = new MessageSerializer();

		private MessageSerializer()
		{
		}

		public XElement Serialize(Message message, string elementName = "message")
		{
			message.ThrowIfNull("message");
			elementName.ThrowIfNull("elementName");

			return new XElement(
				elementName,
				MessagePartSerializer.Instance.Serialize(message.Parts),
				new XAttribute("id", message.Id));
		}

		public Message Deserialize(XElement messageElement)
		{
			messageElement.ThrowIfNull("messageContainer");

			return new Message(
				(Guid)messageElement.Attribute("id"),
				MessagePartSerializer.Instance.Deserialize(messageElement));
		}
	}
}