using System;
using System.Xml.Linq;

using Junior.Common;

using TextAdventure.Engine.Game.Events;
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
				actorInstance.ActorInstanceCreatedEventHandler.IfNotNull(arg => EventHandlerSerializer.Instance.Serialize(arg, "actorInstanceCreatedEventHandler")),
				actorInstance.ActorInstanceDestroyedEventHandler.IfNotNull(arg => EventHandlerSerializer.Instance.Serialize(arg, "actorInstanceDestroyedEventHandler")),
				actorInstance.ActorInstanceTouchedActorInstanceEventHandler.IfNotNull(arg => EventHandlerSerializer.Instance.Serialize(arg, "actorInstanceTouchedActorInstanceEventHandler")),
				actorInstance.PlayerTouchedActorInstanceEventHandler.IfNotNull(arg => EventHandlerSerializer.Instance.Serialize(arg, "playerTouchedActorInstanceEventHandler")),
				actorInstance.ActorInstanceMovedEventHandler.IfNotNull(arg => EventHandlerSerializer.Instance.Serialize(arg, "actorInstanceMovedEventHandler")),
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
				actorInstanceElement.Element("actorInstanceCreatedEventHandler").IfNotNull(EventHandlerSerializer.Instance.Deserialize<ActorInstanceCreatedEvent>),
				actorInstanceElement.Element("actorInstanceDestroyedEventHandler").IfNotNull(EventHandlerSerializer.Instance.Deserialize<ActorInstanceDestroyedEvent>),
				actorInstanceElement.Element("actorInstanceTouchedActorInstanceEventHandler").IfNotNull(EventHandlerSerializer.Instance.Deserialize<ActorInstanceTouchedActorInstanceEvent>),
				actorInstanceElement.Element("playerTouchedActorInstanceEventHandler").IfNotNull(EventHandlerSerializer.Instance.Deserialize<PlayerTouchedActorInstanceEvent>),
				actorInstanceElement.Element("actorInstanceMovedEventHandler").IfNotNull(EventHandlerSerializer.Instance.Deserialize<ActorInstanceMovedEvent>));
		}
	}
}