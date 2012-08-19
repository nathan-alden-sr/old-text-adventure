using System.Xml.Linq;

using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers.Xml
{
	public class SpriteSerializer
	{
		public static readonly SpriteSerializer Instance = new SpriteSerializer();

		private SpriteSerializer()
		{
		}

		public XElement Serialize(Sprite sprite, string elementName = "sprite")
		{
			sprite.ThrowIfNull("sprite");
			elementName.ThrowIfNull("elementName");

			return new XElement(
				elementName,
				CharacterSerializer.Instance.Serialize(sprite.Character),
				new XAttribute("coordinate", CoordinateSerializer.Instance.Serialize(sprite.Coordinate)));
		}

		public Sprite Deserialize(XElement spriteElement)
		{
			spriteElement.ThrowIfNull("spriteElement");

			return new Sprite(
				CoordinateSerializer.Instance.Deserialize((string)spriteElement.Attribute("coordinate")),
				CharacterSerializer.Instance.Deserialize(spriteElement.Element("character")));
		}
	}
}