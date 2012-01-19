using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Junior.Common;

namespace TextAdventure.Engine.Serializers.Compact
{
	/// <remarks>
	/// Serialized data is in the following format:
	/// [FieldCount]
	/// [FieldIndex][FieldLength][Data]
	/// </remarks>
	public class CompactSerializer
	{
		private readonly Dictionary<int, byte[]> _dataByFieldIndex = new Dictionary<int, byte[]>();

		public CompactSerializer()
		{
		}

		public CompactSerializer(byte[] serializedData)
		{
			serializedData.ThrowIfNull("serializedData");

			int index = 0;
			int fieldCount = ReadInt32(serializedData, ref index);

			for (int i = 0; i < fieldCount; i++)
			{
				ReadFieldData(serializedData, ref index);
			}
		}

		public IEnumerable<int> FieldIndices
		{
			get
			{
				return _dataByFieldIndex.Keys.OrderBy(arg => arg);
			}
		}

		public int FieldCount
		{
			get
			{
				return _dataByFieldIndex.Count;
			}
		}

		public byte[] this[int fieldIndex]
		{
			get
			{
				return _dataByFieldIndex[fieldIndex];
			}
			set
			{
				_dataByFieldIndex[fieldIndex] = value;
			}
		}

		public byte[] Serialize()
		{
			var stream = new MemoryStream();

			using (var writer = new BinaryWriter(stream))
			{
				writer.Write(_dataByFieldIndex.Count);
				foreach (var pair in _dataByFieldIndex)
				{
					writer.Write(pair.Key);
					writer.Write(pair.Value.Length);
					writer.Write(pair.Value);
				}
			}

			return stream.ToArray();
		}

		private void ReadFieldData(byte[] serializedData, ref int index)
		{
			int fieldIndex = ReadInt32(serializedData, ref index);
			int fieldLength = ReadInt32(serializedData, ref index);
			byte[] data = ReadBytes(serializedData, ref index, fieldLength);

			_dataByFieldIndex[fieldIndex] = data;
		}

		private static int ReadInt32(byte[] serializedData, ref int index)
		{
			int int32 = BitConverter.ToInt32(serializedData, index);

			index += sizeof(int);

			return int32;
		}

		private static byte[] ReadBytes(byte[] serializedData, ref int index, int length)
		{
			var data = new byte[length];

			Array.Copy(serializedData, index, data, 0, length);
			index += length;

			return data;
		}
	}
}