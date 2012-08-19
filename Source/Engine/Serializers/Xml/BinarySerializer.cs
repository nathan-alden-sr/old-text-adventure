using System;

using Junior.Common;

namespace TextAdventure.Engine.Serializers.Xml
{
	public class BinarySerializer
	{
		public static readonly BinarySerializer Instance = new BinarySerializer();

		private BinarySerializer()
		{
		}

		public string Serialize(byte[] value)
		{
			value.ThrowIfNull("value");

			return Convert.ToBase64String(value);
		}

		public byte[] Deserialize(string value)
		{
			value.ThrowIfNull("value");

			return Convert.FromBase64String(value);
		}
	}
}