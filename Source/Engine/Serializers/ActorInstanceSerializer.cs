using System;
using System.Xml.Linq;

using Junior.Common;

using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers
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
				actorInstance.ActorInstanceCreatedEventHandler
					.IfNotNull(arg => EventHandlerSerializer<ActorInstanceCreatedEvent>.Instance.Serialize(arg, "actorInstanceCreatedEventHandler")),
				actorInstance.ActorInstanceDestroyedEventHandler
					.IfNotNull(arg => EventHandlerSerializer<ActorInstanceDestroyedEvent>.Instance.Serialize(arg, "actorInstanceDestroyedEventHandler")),
				actorInstance.ActorInstanceTouchedActorInstanceEventHandler
					.IfNotNull(arg => EventHandlerSerializer<ActorInstanceTouchedActorInstanceEvent>.Instance.Serialize(arg, "actorInstanceTouchedActorInstanceEventHandler")),
				actorInstance.PlayerTouchedActorInstanceEventHandler
					.IfNotNull(arg => EventHandlerSerializer<PlayerTouchedActorInstanceEvent>.Instance.Serialize(arg, "playerTouchedActorInstanceEventHandler")),
				actorInstance.ActorInstanceMovedEventHandler
					.IfNotNull(arg => EventHandlerSerializer<ActorInstanceMovedEvent>.Instance.Serialize(arg, "actorInstanceMovedEventHandler")),
				new XAttribute("id", actorInstance.Id),
				new XAttribute("actorId", actorInstance.ActorId),
				new XAttribute("coordinate", CoordinateSerializer.Instance.Serialize(actorInstance.Coordinate)));
		}

		public ActorInstance Deserialize(XElement actorInstanceElement)
		{
			actorInstanceElement.ThrowIfNull("actorInstanceElement");

			return new ActorInstance(
				(Guid)actorInstanceElement.Attribute("id"),
				(Guid)actorInstanceElement.Attribute("actorId"),
				CoordinateSerializer.Instance.Deserialize((string)actorInstanceElement.Attribute("coordinate")),
				CharacterSerializer.Instance.Deserialize(actorInstanceElement.Element("character")),
				actorInstanceElement.Element("actorInstanceCreatedEventHandler").IfNotNull(EventHandlerSerializer<ActorInstanceCreatedEvent>.Instance.Deserialize),
				actorInstanceElement.Element("actorInstanceDestroyedEventHandler").IfNotNull(EventHandlerSerializer<ActorInstanceDestroyedEvent>.Instance.Deserialize),
				actorInstanceElement.Element("actorInstanceTouchedActorInstanceEventHandler").IfNotNull(EventHandlerSerializer<ActorInstanceTouchedActorInstanceEvent>.Instance.Deserialize),
				actorInstanceElement.Element("playerTouchedActorInstanceEventHandler").IfNotNull(EventHandlerSerializer<PlayerTouchedActorInstanceEvent>.Instance.Deserialize),
				actorInstanceElement.Element("actorInstanceMovedEventHandler").IfNotNull(EventHandlerSerializer<ActorInstanceMovedEvent>.Instance.Deserialize));
		}
	}
}