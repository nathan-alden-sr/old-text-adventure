using System.Linq;
using System.Xml.Linq;

using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers
{
	public class SpriteLayerSerializer
	{
		public static readonly SpriteLayerSerializer Instance = new SpriteLayerSerializer();

		private SpriteLayerSerializer()
		{
		}

		public XElement Serialize(SpriteLayer spriteLayer, string elementName = "spriteLayer")
		{
			spriteLayer.ThrowIfNull("spriteLayer");
			elementName.ThrowIfNull("elementName");

			return new XElement(
				elementName,
				spriteLayer.Sprites.Select(arg => SpriteSerializer.Instance.Serialize(arg)),
				new XAttribute("size", SizeSerializer.Instance.Serialize(spriteLayer.Size)));
		}

		public SpriteLayer Deserialize(XElement spriteLayerElement)
		{
			spriteLayerElement.ThrowIfNull("spriteLayerElement");

			return new SpriteLayer(
				SizeSerializer.Instance.Deserialize((string)spriteLayerElement.Attribute("size")),
				spriteLayerElement.Elements("sprite").Select(SpriteSerializer.Instance.Deserialize));
		}
	}
}