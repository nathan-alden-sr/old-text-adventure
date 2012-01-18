using System;
using System.Xml.Linq;

using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers.Xml
{
	public class SoundEffectSerializer
	{
		public static readonly SoundEffectSerializer Instance = new SoundEffectSerializer();

		private SoundEffectSerializer()
		{
		}

		public XElement Serialize(SoundEffect soundEffect, string elementName = "soundEffect")
		{
			soundEffect.ThrowIfNull("soundEffect");
			elementName.ThrowIfNull("elementName");

			return new XElement(
				elementName,
				new XElement("data", BinarySerializer.Instance.Serialize(soundEffect.Data)),
				new XAttribute("id", soundEffect.Id));
		}

		public SoundEffect Deserialize(XElement playerElement)
		{
			playerElement.ThrowIfNull("playerElement");

			return new SoundEffect(
				(Guid)playerElement.Attribute("id"),
				BinarySerializer.Instance.Deserialize((string)playerElement.Element("data")));
		}
	}
}