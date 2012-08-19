using System.Collections.Generic;
using System.Text;

using TextAdventure.Engine.Game.Events;

namespace TextAdventure.Engine.Serializers.Compact
{
	public class EventHandlerCollectionSerializer
	{
		public static readonly EventHandlerCollectionSerializer Instance = new EventHandlerCollectionSerializer();

		private EventHandlerCollectionSerializer()
		{
		}

		public byte[] Serialize(EventHandlerCollection eventHandlerCollection)
		{
			var serializer = new CompactSerializer();
			int index = 0;

			if (eventHandlerCollection != null)
			{
				foreach (IEventHandler eventHandler in eventHandlerCollection.EventHandlers)
				{
					serializer[index++] = Encoding.ASCII.GetBytes(eventHandler.EventHandlerTypeName);
				}
			}

			return serializer.Serialize();
		}

		public EventHandlerCollection Deserialize(byte[] serializedData)
		{
			var serializer = new CompactSerializer(serializedData);

			if (serializer.FieldCount == 0)
			{
				return null;
			}

			var eventHandlerTypeNames = new List<string>();

			for (int i = 0; i < serializer.FieldCount; i++)
			{
				string typeName = Encoding.ASCII.GetString(serializer[i]);

				eventHandlerTypeNames.Add(typeName);
			}

			return new EventHandlerCollection(eventHandlerTypeNames);
		}
	}
}