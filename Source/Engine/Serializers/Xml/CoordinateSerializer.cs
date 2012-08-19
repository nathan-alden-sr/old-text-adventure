using System;
using System.Linq;

using Junior.Common;

using TextAdventure.Engine.Common;

namespace TextAdventure.Engine.Serializers.Xml
{
	public class CoordinateSerializer
	{
		public static readonly CoordinateSerializer Instance = new CoordinateSerializer();

		private CoordinateSerializer()
		{
		}

		public string Serialize(Coordinate coordinate)
		{
			coordinate.ThrowIfNull("coordinate");

			return String.Format("{0},{1}", coordinate.X, coordinate.Y);
		}

		public Coordinate Deserialize(string value)
		{
			value.ThrowIfNull("value");

			int[] ordinates = value.Split(',').Select(Int32.Parse).ToArray();

			return new Coordinate(ordinates[0], ordinates[1]);
		}
	}
}