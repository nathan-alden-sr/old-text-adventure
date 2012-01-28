using System;
using System.Text;

using Junior.Common;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers.Compact
{
	public class ActorInstanceSerializer
	{
		public static readonly ActorInstanceSerializer Instance = new ActorInstanceSerializer();

		private ActorInstanceSerializer()
		{
		}

		public byte[] Serialize(ActorInstance actorInstance)
		{
			actorInstance.ThrowIfNull("actorInstance");

			var serializer = new CompactSerializer();

			serializer[0] = actorInstance.Id.ToByteArray();
			serializer[1] = Encoding.UTF8.GetBytes(actorInstance.Name);
			serializer[2] = Encoding.UTF8.GetBytes(actorInstance.Description);
			serializer[3] = actorInstance.ActorId.ToByteArray();
			serializer[4] = CoordinateSerializer.Instance.Serialize(actorInstance.Coordinate);
			serializer[5] = CharacterSerializer.Instance.Serialize(actorInstance.Character);
			serializer[6] = EventHandlerCollectionSerializer.Instance.Serialize(actorInstance.EventHandlerCollection);

			return serializer.Serialize();
		}

		public ActorInstance Deserialize(byte[] serializedData)
		{
			serializedData.ThrowIfNull("serializedData");

			var serializer = new CompactSerializer(serializedData);
			var id = new Guid(serializer[0]);
			string name = Encoding.UTF8.GetString(serializer[1]);
			string description = Encoding.UTF8.GetString(serializer[2]);
			var actorId = new Guid(serializer[3]);
			Coordinate coordinate = CoordinateSerializer.Instance.Deserialize(serializer[4]);
			Character character = CharacterSerializer.Instance.Deserialize(serializer[5]);
			EventHandlerCollection eventHandlers = EventHandlerCollectionSerializer.Instance.Deserialize(serializer[6]);

			return new ActorInstance(id, name, description, actorId, coordinate, character, eventHandlers);
		}
	}
}