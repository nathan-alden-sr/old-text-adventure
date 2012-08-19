using System;
using System.Text;

using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers.Compact
{
	public class SongSerializer
	{
		public static readonly SongSerializer Instance = new SongSerializer();

		private SongSerializer()
		{
		}

		public byte[] Serialize(Song song)
		{
			song.ThrowIfNull("song");

			var serializer = new CompactSerializer();

			serializer[0] = song.Id.ToByteArray();
			serializer[1] = Encoding.UTF8.GetBytes(song.Name);
			serializer[2] = Encoding.UTF8.GetBytes(song.Description);
			serializer[3] = song.Data;

			return serializer.Serialize();
		}

		public Song Deserialize(byte[] serializedData)
		{
			serializedData.ThrowIfNull("serializedData");

			var serializer = new CompactSerializer(serializedData);
			var id = new Guid(serializer[0]);
			string name = Encoding.UTF8.GetString(serializer[1]);
			string description = Encoding.UTF8.GetString(serializer[2]);
			byte[] data = serializer[3];

			return new Song(id, name, description, data);
		}
	}
}