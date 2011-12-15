using System;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

using Junior.Common;

using TextAdventure.Engine.Game.Events;

namespace TextAdventure.Engine.Serializers
{
	public class EventHandlerSerializer<TEvent>
		where TEvent : Event
	{
		public static readonly EventHandlerSerializer<TEvent> Instance = new EventHandlerSerializer<TEvent>();

		private EventHandlerSerializer()
		{
		}

		public XElement Serialize(IEventHandler<TEvent> eventHandler, string elementName = "eventHandler")
		{
			eventHandler.ThrowIfNull("eventHandler");
			elementName.ThrowIfNull("elementName");

			Type type = eventHandler.GetType();

			return new XElement(
				elementName,
				new XAttribute("type", String.Format("{0}, {1}", type.FullName, type.Assembly.GetName().Name)));
		}

		public IEventHandler<TEvent> Deserialize(XElement eventHandlerElement)
		{
			eventHandlerElement.ThrowIfNull("eventHandlerElement");

			var typeName = (string)eventHandlerElement.Attribute("type");
			Type type = Type.GetType(typeName, false, false);

			if (type == null)
			{
				throw new TypeLoadException(String.Format("Event handler type '{0}' not found.", typeName));
			}
			if ((!type.IsPublic && !type.IsNestedPublic) || type.IsAbstract || !type.IsClass)
			{
				throw new TypeLoadException(String.Format("Event handler type '{0}' must be declared as a public instance class.", type.FullName));
			}
			if (!type.GetConstructors(BindingFlags.Public | BindingFlags.Instance).Any(arg => arg.GetParameters().Length == 0))
			{
				throw new TypeLoadException(String.Format("Event handler type '{0}' must have a public instance constructor that takes no parameters.", type.FullName));
			}

			return (IEventHandler<TEvent>)Activator.CreateInstance(type);
		}
	}
}