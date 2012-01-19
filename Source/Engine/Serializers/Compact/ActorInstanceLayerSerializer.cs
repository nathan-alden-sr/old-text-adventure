using System.Collections.Generic;
using System.Linq;

using Junior.Common;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers.Compact
{
	public class ActorInstanceLayerSerializer
	{
		public static readonly ActorInstanceLayerSerializer Instance = new ActorInstanceLayerSerializer();

		private ActorInstanceLayerSerializer()
		{
		}

		public byte[] Serialize(ActorInstanceLayer actorInstanceLayer)
		{
			actorInstanceLayer.ThrowIfNull("actorInstanceLayer");

			var serializer = new CompactSerializer();

			serializer[0] = SizeSerializer.Instance.Seralize(actorInstanceLayer.Size);

			var actorInstanceSerializer = new CompactSerializer();
			int index = 0;

			foreach (ActorInstance actorInstance in actorInstanceLayer.ActorInstances)
			{
				actorInstanceSerializer[index++] = ActorInstanceSerializer.Instance.Serialize(actorInstance);
			}

			serializer[1] = actorInstanceSerializer.Serialize();

			return serializer.Serialize();
		}

		public ActorInstanceLayer Deserialize(byte[] serializedData)
		{
			serializedData.ThrowIfNull("serializedData");

			var serializer = new CompactSerializer(serializedData);
			Size size = SizeSerializer.Instance.Deserialize(serializer[0]);
			var actorInstanceSerializer = new CompactSerializer(serializer[1]);
			IEnumerable<ActorInstance> actorInstances = actorInstanceSerializer.FieldIndices.Select(arg => ActorInstanceSerializer.Instance.Deserialize(actorInstanceSerializer[arg]));

			return new ActorInstanceLayer(size, actorInstances);
		}
	}
}