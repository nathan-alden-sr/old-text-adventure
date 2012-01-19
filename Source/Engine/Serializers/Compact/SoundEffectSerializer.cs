using System;
using System.Text;

using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers.Compact
{
	public class SoundEffectSerializer
	{
		public static readonly SoundEffectSerializer Instance = new SoundEffectSerializer();

		private SoundEffectSerializer()
		{
		}

		public byte[] Serialize(SoundEffect soundEffect)
		{
			soundEffect.ThrowIfNull("soundEffect");

			var serializer = new CompactSerializer();

			serializer[0] = soundEffect.Id.ToByteArray();
			serializer[1] = Encoding.UTF8.GetBytes(soundEffect.Name);
			serializer[2] = Encoding.UTF8.GetBytes(soundEffect.Description);
			serializer[3] = soundEffect.Data;

			return serializer.Serialize();
		}

		public SoundEffect Deserialize(byte[] serializedData)
		{
			serializedData.ThrowIfNull("serializedData");

			var serializer = new CompactSerializer(serializedData);
			var id = new Guid(serializer[0]);
			string name = Encoding.UTF8.GetString(serializer[1]);
			string description = Encoding.UTF8.GetString(serializer[2]);
			byte[] data = serializer[3];

			return new SoundEffect(id, name, description, data);
		}
	}
}