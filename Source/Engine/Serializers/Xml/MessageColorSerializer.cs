using System.Xml.Linq;

using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers.Xml
{
	public class MessageColorSerializer
	{
		public static readonly MessageColorSerializer Instance = new MessageColorSerializer();

		private MessageColorSerializer()
		{
		}

		public XElement Serialize(MessageColor messageColor, string elementName = "color")
		{
			messageColor.ThrowIfNull("messageColor");
			elementName.ThrowIfNull("elementName");

			return new XElement(
				elementName,
				new XAttribute("color", ColorSerializer.Instance.Serialize(messageColor.Color)));
		}

		public MessageColor Deserialize(XElement colorElement)
		{
			colorElement.ThrowIfNull("colorElement");

			return new MessageColor(
				ColorSerializer.Instance.Deserialize((string)colorElement.Attribute("color")));
		}
	}
}