using System;
using System.Linq;

using Junior.Common;

using TextAdventure.Engine.Common;

namespace TextAdventure.Engine.Serializers.Xml
{
	public class ColorSerializer
	{
		public static readonly ColorSerializer Instance = new ColorSerializer();

		private ColorSerializer()
		{
		}

		public string Serialize(Color color)
		{
			color.ThrowIfNull("color");

			return String.Format("{0},{1},{2},{3}", color.R, color.G, color.B, color.A);
		}

		public Color Deserialize(string value)
		{
			value.ThrowIfNull("value");

			float[] components = value.Split(',').Select(Single.Parse).ToArray();

			return new Color(components[0], components[1], components[2], components[3]);
		}
	}
}