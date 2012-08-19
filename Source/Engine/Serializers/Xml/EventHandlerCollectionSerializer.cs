using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using TextAdventure.Engine.Game.Events;

namespace TextAdventure.Engine.Serializers.Xml
{
	public class EventHandlerCollectionSerializer
	{
		public static readonly EventHandlerCollectionSerializer Instance = new EventHandlerCollectionSerializer();

		private EventHandlerCollectionSerializer()
		{
		}

		public IEnumerable<XElement> Serialize(EventHandlerCollection eventHandlerCollection, string elementName = "eventHandler")
		{
			return eventHandlerCollection != null
				       ? eventHandlerCollection.EventHandlers.Select(arg => new XElement("eventHandler", new XAttribute("type", arg.EventHandlerTypeName)))
				       : Enumerable.Empty<XElement>();
		}

		public EventHandlerCollection Deserialize(XContainer eventHandlerContainer, string elementName = "eventHandler")
		{
			XElement[] eventHandlerElements = eventHandlerContainer.Elements(elementName).ToArray();

			return eventHandlerElements.Any() ? new EventHandlerCollection(eventHandlerElements.Select(arg => arg.Attribute("type").Value)) : null;
		}
	}
}