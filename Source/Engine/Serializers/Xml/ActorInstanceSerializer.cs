using System;
using System.Xml.Linq;

using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers.Xml
{
	public class ActorInstanceSerializer
	{
		public static readonly ActorInstanceSerializer Instance = new ActorInstanceSerializer();

		private ActorInstanceSerializer()
		{
		}

		public XElement Serialize(ActorInstance actorInstance, string elementName = "actorInstance")
		{
			actorInstance.ThrowIfNull("actorInstance");
			elementName.ThrowIfNull("elementName");

			return new XElement(
				elementName,
				CharacterSerializer.Instance.Serialize(actorInstance.Character),
				EventHandlerCollectionSerializer.Instance.Serialize(actorInstance.EventHandlerCollection),
				new XAttribute("id", actorInstance.Id),
				new XAttribute("name", actorInstance.Name),
				new XAttribute("description", actorInstance.Description),
				new XAttribute("actorId", actorInstance.ActorId),
				new XAttribute("coordinate", CoordinateSerializer.Instance.Serialize(actorInstance.Coordinate)));
		}

		public ActorInstance Deserialize(XElement actorInstanceElement)
		{
			actorInstanceElement.ThrowIfNull("actorInstanceElement");

			return new ActorInstance(
				(Guid)actorInstanceElement.Attribute("id"),
				(string)actorInstanceElement.Attribute("name"),
				(string)actorInstanceElement.Attribute("description"),
				(Guid)actorInstanceElement.Attribute("actorId"),
				CoordinateSerializer.Instance.Deserialize((string)actorInstanceElement.Attribute("coordinate")),
				CharacterSerializer.Instance.Deserialize(actorInstanceElement.Element("character")),
				EventHandlerCollectionSerializer.Instance.Deserialize(actorInstanceElement));
		}
	}
}