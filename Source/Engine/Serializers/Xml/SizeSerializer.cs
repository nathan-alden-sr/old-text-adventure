using System;
using System.Linq;

using Junior.Common;

using TextAdventure.Engine.Common;

namespace TextAdventure.Engine.Serializers.Xml
{
	public class SizeSerializer
	{
		public static readonly SizeSerializer Instance = new SizeSerializer();

		private SizeSerializer()
		{
		}

		public string Serialize(Size size)
		{
			size.ThrowIfNull("size");

			return String.Format("{0},{1}", size.Width, size.Height);
		}

		public Size Deserialize(string value)
		{
			value.ThrowIfNull("value");

			int[] dimensions = value.Split(',').Select(Int32.Parse).ToArray();

			return new Size(dimensions[0], dimensions[1]);
		}
	}
}