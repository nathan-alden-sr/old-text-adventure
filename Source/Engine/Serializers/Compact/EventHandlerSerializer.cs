using System;
using System.Text;

using Junior.Common;

using TextAdventure.Engine.Game.Events;

namespace TextAdventure.Engine.Serializers.Compact
{
	public class EventHandlerSerializer
	{
		public static readonly EventHandlerSerializer Instance = new EventHandlerSerializer();

		private EventHandlerSerializer()
		{
		}

		public byte[] Serialize<TEvent>(IEventHandler<TEvent> eventHandler)
			where TEvent : Event
		{
			var serializer = new CompactSerializer();

			if (eventHandler != null)
			{
				Type type = eventHandler.GetType();

				serializer[0] = Encoding.UTF8.GetBytes(EventHandlerTypeSerializer.Instance.Serialize(type));
			}

			return serializer.Serialize();
		}

		public IEventHandler<TEvent> Deserialize<TEvent>(byte[] serializedBytes)
			where TEvent : Event
		{
			serializedBytes.ThrowIfNull("serializedBytes");

			var serializer = new CompactSerializer(serializedBytes);

			if (serializer.FieldCount == 1)
			{
				string typeName = Encoding.UTF8.GetString(serializer[0]);

				return EventHandlerTypeSerializer.Instance.Deserialize<TEvent>(typeName);
			}

			return null;
		}
	}
}