using System;
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

			serializer[0] = actorInstanceLayer.BoardId.ToByteArray();
			serializer[1] = SizeSerializer.Instance.Seralize(actorInstanceLayer.Size);

			var actorInstanceSerializer = new CompactSerializer();
			int index = 0;

			foreach (ActorInstance actorInstance in actorInstanceLayer.ActorInstances)
			{
				actorInstanceSerializer[index++] = ActorInstanceSerializer.Instance.Serialize(actorInstance);
			}

			serializer[2] = actorInstanceSerializer.Serialize();

			return serializer.Serialize();
		}

		public ActorInstanceLayer Deserialize(byte[] serializedData)
		{
			serializedData.ThrowIfNull("serializedData");

			var serializer = new CompactSerializer(serializedData);
			var boardId = new Guid(serializer[0]);
			Size size = SizeSerializer.Instance.Deserialize(serializer[1]);
			var actorInstanceSerializer = new CompactSerializer(serializer[2]);
			IEnumerable<ActorInstance> actorInstances = actorInstanceSerializer.FieldIndices.Select(arg => ActorInstanceSerializer.Instance.Deserialize(actorInstanceSerializer[arg]));

			return new ActorInstanceLayer(boardId, size, actorInstances);
		}
	}
}