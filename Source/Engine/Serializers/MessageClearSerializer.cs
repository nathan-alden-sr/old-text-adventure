using System.Xml.Linq;

using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers
{
	public class MessageClearSerializer
	{
		public static readonly MessageClearSerializer Instance = new MessageClearSerializer();

		private MessageClearSerializer()
		{
		}

		public XElement Serialize(MessageClear messageClear, string elementName = "clear")
		{
			messageClear.ThrowIfNull("messageClear");
			elementName.ThrowIfNull("elementName");

			return new XElement(elementName);
		}

		public MessageClear Deserialize(XElement clearElement)
		{
			clearElement.ThrowIfNull("clearElement");

			return new MessageClear();
		}
	}
}