using System.Collections.Generic;
using System.Linq;

using Junior.Common;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers.Compact
{
	public class SpriteLayerSerializer
	{
		public static readonly SpriteLayerSerializer Instance = new SpriteLayerSerializer();

		private SpriteLayerSerializer()
		{
		}

		public byte[] Serialize(SpriteLayer spriteLayer)
		{
			spriteLayer.ThrowIfNull("spriteLayer");

			var serializer = new CompactSerializer();

			serializer[0] = SizeSerializer.Instance.Seralize(spriteLayer.Size);

			var spriteSerializer = new CompactSerializer();
			int index = 0;

			foreach (Sprite sprite in spriteLayer.Sprites)
			{
				spriteSerializer[index++] = SpriteSerializer.Instance.Serialize(sprite);
			}

			serializer[1] = spriteSerializer.Serialize();

			return serializer.Serialize();
		}

		public SpriteLayer Deserialize(byte[] serializedData)
		{
			serializedData.ThrowIfNull("serializedData");

			var serializer = new CompactSerializer(serializedData);
			Size size = SizeSerializer.Instance.Deserialize(serializer[0]);
			var spriteSerializer = new CompactSerializer(serializer[1]);
			IEnumerable<Sprite> sprites = spriteSerializer.FieldIndices.Select(arg => SpriteSerializer.Instance.Deserialize(spriteSerializer[arg]));

			return new SpriteLayer(size, sprites);
		}
	}
}