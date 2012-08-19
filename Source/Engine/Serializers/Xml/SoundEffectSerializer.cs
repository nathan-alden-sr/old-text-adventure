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
				new XAttribute("id", soundEffect.Id),
				new XAttribute("name", soundEffect.Name),
				new XAttribute("description", soundEffect.Description));
		}

		public SoundEffect Deserialize(XElement soundEffectElement)
		{
			soundEffectElement.ThrowIfNull("soundEffectElement");

			return new SoundEffect(
				(Guid)soundEffectElement.Attribute("id"),
				(string)soundEffectElement.Attribute("name"),
				(string)soundEffectElement.Attribute("description"),
				BinarySerializer.Instance.Deserialize((string)soundEffectElement.Element("data")));
		}
	}
}