using System;

using Junior.Common;

using TextAdventure.Engine.Common;

namespace TextAdventure.Engine.Serializers.Compact
{
	public class ColorSerializer
	{
		public static readonly ColorSerializer Instance = new ColorSerializer();

		private ColorSerializer()
		{
		}

		public byte[] Serialize(Color color)
		{
			var serializer = new CompactSerializer();

			serializer[0] = BitConverter.GetBytes(color.R);
			serializer[1] = BitConverter.GetBytes(color.G);
			serializer[2] = BitConverter.GetBytes(color.B);
			serializer[3] = BitConverter.GetBytes(color.A);

			return serializer.Serialize();
		}

		public Color Deserialize(byte[] serializedData)
		{
			serializedData.ThrowIfNull("serializedData");

			var serializer = new CompactSerializer(serializedData);
			float r = BitConverter.ToSingle(serializer[0], 0);
			float g = BitConverter.ToSingle(serializer[1], 0);
			float b = BitConverter.ToSingle(serializer[2], 0);
			float a = BitConverter.ToSingle(serializer[3], 0);

			return new Color(r, g, b, a);
		}
	}
}