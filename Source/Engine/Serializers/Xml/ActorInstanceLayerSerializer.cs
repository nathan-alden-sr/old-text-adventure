using System;
using System.Linq;
using System.Xml.Linq;

using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers.Xml
{
	public class ActorInstanceLayerSerializer
	{
		public static readonly ActorInstanceLayerSerializer Instance = new ActorInstanceLayerSerializer();

		private ActorInstanceLayerSerializer()
		{
		}

		public XElement Serialize(ActorInstanceLayer actorInstanceLayer, string elementName = "actorInstanceLayer")
		{
			actorInstanceLayer.ThrowIfNull("actorInstanceLayer");
			elementName.ThrowIfNull("elementName");

			return new XElement(
				elementName,
				actorInstanceLayer.ActorInstances.Select(arg => ActorInstanceSerializer.Instance.Serialize(arg)),
				new XAttribute("boardId", actorInstanceLayer.BoardId),
				new XAttribute("size", SizeSerializer.Instance.Serialize(actorInstanceLayer.Size)));
		}

		public ActorInstanceLayer Deserialize(XElement actorInstanceLayerElement)
		{
			actorInstanceLayerElement.ThrowIfNull("actorInstanceLayerElement");

			return new ActorInstanceLayer(
				(Guid)actorInstanceLayerElement.Attribute("boardId"),
				SizeSerializer.Instance.Deserialize((string)actorInstanceLayerElement.Attribute("size")),
				actorInstanceLayerElement.Elements("actorInstance").Select(ActorInstanceSerializer.Instance.Deserialize));
		}
	}
}