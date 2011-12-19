using System.Xml.Linq;

using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers
{
	public class MessageTextSerializer
	{
		public static readonly MessageTextSerializer Instance = new MessageTextSerializer();

		private MessageTextSerializer()
		{
		}

		public XElement Serialize(MessageText messageText, string elementName = "text")
		{
			messageText.ThrowIfNull("messageText");
			elementName.ThrowIfNull("elementName");

			return new XElement(
				elementName,
				messageText.Text);
		}

		public MessageText Deserialize(XElement textElement)
		{
			textElement.ThrowIfNull("textElement");

			return new MessageText(textElement.Value);
		}
	}
}