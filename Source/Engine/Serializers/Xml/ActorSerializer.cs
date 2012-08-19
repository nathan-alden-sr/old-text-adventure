using System;
using System.Xml.Linq;

using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers.Xml
{
	public class ActorSerializer
	{
		public static readonly ActorSerializer Instance = new ActorSerializer();

		private ActorSerializer()
		{
		}

		public XElement Serialize(Actor actor, string elementName = "actor")
		{
			actor.ThrowIfNull("actor");
			elementName.ThrowIfNull("elementName");

			return new XElement(
				elementName,
				CharacterSerializer.Instance.Serialize(actor.Character),
				new XAttribute("id", actor.Id),
				new XAttribute("name", actor.Name),
				new XAttribute("description", actor.Description));
		}

		public Actor Deserialize(XElement actorElement)
		{
			actorElement.ThrowIfNull("actorElement");

			return new Actor(
				(Guid)actorElement.Attribute("id"),
				(string)actorElement.Attribute("name"),
				(string)actorElement.Attribute("description"),
				CharacterSerializer.Instance.Deserialize(actorElement.Element("character")));
		}
	}
}