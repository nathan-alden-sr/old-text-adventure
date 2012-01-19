using System;

using Junior.Common;

using TextAdventure.Engine.Common;

namespace TextAdventure.Engine.Serializers.Compact
{
	public class SizeSerializer
	{
		public static readonly SizeSerializer Instance = new SizeSerializer();

		private SizeSerializer()
		{
		}

		public byte[] Seralize(Size size)
		{
			var serializer = new CompactSerializer();

			serializer[0] = BitConverter.GetBytes(size.Width);
			serializer[1] = BitConverter.GetBytes(size.Height);

			return serializer.Serialize();
		}

		public Size Deserialize(byte[] serializedData)
		{
			serializedData.ThrowIfNull("serializedData");

			var serializer = new CompactSerializer(serializedData);
			int width = BitConverter.ToInt32(serializer[0], 0);
			int height = BitConverter.ToInt32(serializer[1], 0);

			return new Size(width, height);
		}
	}
}