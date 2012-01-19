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
			serializer[6] = EventHandlerSerializer.Instance.Serialize(actorInstance.ActorInstanceCreatedEventHandler);
			serializer[7] = EventHandlerSerializer.Instance.Serialize(actorInstance.ActorInstanceDestroyedEventHandler);
			serializer[8] = EventHandlerSerializer.Instance.Serialize(actorInstance.ActorInstanceTouchedActorInstanceEventHandler);
			serializer[9] = EventHandlerSerializer.Instance.Serialize(actorInstance.PlayerTouchedActorInstanceEventHandler);
			serializer[10] = EventHandlerSerializer.Instance.Serialize(actorInstance.ActorInstanceMovedEventHandler);

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
			IEventHandler<ActorInstanceCreatedEvent> actorInstanceCreatedEventHandler = EventHandlerSerializer.Instance.Deserialize<ActorInstanceCreatedEvent>(serializer[6]);
			IEventHandler<ActorInstanceDestroyedEvent> actorInstanceDestroyedEventHandler = EventHandlerSerializer.Instance.Deserialize<ActorInstanceDestroyedEvent>(serializer[7]);
			IEventHandler<ActorInstanceTouchedActorInstanceEvent> actorInstanceTouchedActorInstanceEventHandler = EventHandlerSerializer.Instance.Deserialize<ActorInstanceTouchedActorInstanceEvent>(serializer[8]);
			IEventHandler<PlayerTouchedActorInstanceEvent> playerTouchedActorInstanceEventHandler = EventHandlerSerializer.Instance.Deserialize<PlayerTouchedActorInstanceEvent>(serializer[9]);
			IEventHandler<ActorInstanceMovedEvent> actorInstanceMovedEventHandler = EventHandlerSerializer.Instance.Deserialize<ActorInstanceMovedEvent>(serializer[10]);

			return new ActorInstance(
				id,
				name,
				description,
				actorId,
				coordinate,
				character,
				actorInstanceCreatedEventHandler,
				actorInstanceDestroyedEventHandler,
				actorInstanceTouchedActorInstanceEventHandler,
				playerTouchedActorInstanceEventHandler,
				actorInstanceMovedEventHandler);
		}
	}
}