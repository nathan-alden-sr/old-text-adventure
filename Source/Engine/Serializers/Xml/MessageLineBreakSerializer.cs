using System.Xml.Linq;

using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers.Xml
{
	public class MessageLineBreakSerializer
	{
		public static readonly MessageLineBreakSerializer Instance = new MessageLineBreakSerializer();

		private MessageLineBreakSerializer()
		{
		}

		public XElement Serialize(MessageLineBreak messageLineBreak, string elementName = "lineBreak")
		{
			messageLineBreak.ThrowIfNull("messageLineBreak");
			elementName.ThrowIfNull("elementName");

			return new XElement(elementName);
		}

		public MessageLineBreak Deserialize(XElement lineBreakElement)
		{
			lineBreakElement.ThrowIfNull("lineBreakElement");

			return new MessageLineBreak();
		}
	}
}