using System;
using System.Linq;
using System.Reflection;

using Junior.Common;

using TextAdventure.Engine.Game.Events;

namespace TextAdventure.Engine.Serializers
{
	public class EventHandlerTypeSerializer
	{
		public static readonly EventHandlerTypeSerializer Instance = new EventHandlerTypeSerializer();

		private EventHandlerTypeSerializer()
		{
		}

		public string Serialize(Type eventHandlerType)
		{
			eventHandlerType.ThrowIfNull("eventHandlerType");

			return String.Format("{0}, {1}", eventHandlerType.FullName, eventHandlerType.Assembly.GetName().Name);
		}

		public IEventHandler<TEvent> Deserialize<TEvent>(string typeName)
			where TEvent : Event
		{
			typeName.ThrowIfNull("typeName");

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