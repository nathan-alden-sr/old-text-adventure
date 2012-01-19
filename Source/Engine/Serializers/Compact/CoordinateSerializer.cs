using System;

using Junior.Common;

using TextAdventure.Engine.Common;

namespace TextAdventure.Engine.Serializers.Compact
{
	public class CoordinateSerializer
	{
		public static readonly CoordinateSerializer Instance = new CoordinateSerializer();

		private CoordinateSerializer()
		{
		}

		public byte[] Serialize(Coordinate coordinate)
		{
			var serializer = new CompactSerializer();

			serializer[0] = BitConverter.GetBytes(coordinate.X);
			serializer[1] = BitConverter.GetBytes(coordinate.Y);

			return serializer.Serialize();
		}

		public Coordinate Deserialize(byte[] serializedData)
		{
			serializedData.ThrowIfNull("serializedData");

			var serializer = new CompactSerializer(serializedData);
			int x = BitConverter.ToInt32(serializer[0], 0);
			int y = BitConverter.ToInt32(serializer[1], 0);

			return new Coordinate(x, y);
		}
	}
}