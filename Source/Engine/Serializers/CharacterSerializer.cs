using System;
using System.Xml.Linq;

using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers
{
	public class CharacterSerializer
	{
		public static readonly CharacterSerializer Instance = new CharacterSerializer();

		private CharacterSerializer()
		{
		}

		public XElement Serialize(Character character, string elementName = "character")
		{
			character.ThrowIfNull("character");
			elementName.ThrowIfNull("elementName");

			return new XElement(
				elementName,
				new XAttribute("symbol", character.Symbol),
				new XAttribute("foregroundColor", ColorSerializer.Instance.Serialize(character.ForegroundColor)),
				new XAttribute("backgroundColor", ColorSerializer.Instance.Serialize(character.BackgroundColor)));
		}

		public Character Deserialize(XElement characterElement)
		{
			characterElement.ThrowIfNull("characterElement");

			return new Character(
				Byte.Parse((string)characterElement.Attribute("symbol")),
				ColorSerializer.Instance.Deserialize((string)characterElement.Attribute("foregroundColor")),
				ColorSerializer.Instance.Deserialize((string)characterElement.Attribute("backgroundColor")));
		}
	}
}