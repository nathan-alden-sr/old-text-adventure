using System;
using System.Xml.Linq;

using Junior.Common;

using TextAdventure.Engine.Game.Events;

namespace TextAdventure.Engine.Serializers.Xml
{
	public class EventHandlerSerializer
	{
		public static readonly EventHandlerSerializer Instance = new EventHandlerSerializer();

		private EventHandlerSerializer()
		{
		}

		public XElement Serialize<TEvent>(IEventHandler<TEvent> eventHandler, string elementName = "eventHandler")
			where TEvent : Event
		{
			eventHandler.ThrowIfNull("eventHandler");
			elementName.ThrowIfNull("elementName");

			Type type = eventHandler.GetType();

			return new XElement(
				elementName,
				new XAttribute("type", EventHandlerTypeSerializer.Instance.Serialize(type)));
		}

		public IEventHandler<TEvent> Deserialize<TEvent>(XElement eventHandlerElement)
			where TEvent : Event
		{
			eventHandlerElement.ThrowIfNull("eventHandlerElement");

			var typeName = (string)eventHandlerElement.Attribute("type");

			return EventHandlerTypeSerializer.Instance.Deserialize<TEvent>(typeName);
		}
	}
}