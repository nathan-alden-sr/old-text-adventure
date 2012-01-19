using System;
using System.Text;

using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers.Compact
{
	public class ActorSerializer
	{
		public static readonly ActorSerializer Instance = new ActorSerializer();

		private ActorSerializer()
		{
		}

		public byte[] Serialize(Actor actor)
		{
			actor.ThrowIfNull("actor");

			var serializer = new CompactSerializer();

			serializer[0] = actor.Id.ToByteArray();
			serializer[1] = Encoding.UTF8.GetBytes(actor.Name);
			serializer[2] = Encoding.UTF8.GetBytes(actor.Description);
			serializer[3] = CharacterSerializer.Instance.Serialize(actor.Character);

			return serializer.Serialize();
		}

		public Actor Deserialize(byte[] serializedData)
		{
			serializedData.ThrowIfNull("serializedData");

			var serializer = new CompactSerializer(serializedData);
			var id = new Guid(serializer[0]);
			string name = Encoding.UTF8.GetString(serializer[1]);
			string description = Encoding.UTF8.GetString(serializer[2]);
			Character character = CharacterSerializer.Instance.Deserialize(serializer[3]);

			return new Actor(id, name, description, character);
		}
	}
}