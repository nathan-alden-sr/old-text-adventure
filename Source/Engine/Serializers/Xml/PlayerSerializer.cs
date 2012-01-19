using System;
using System.Xml.Linq;

using Junior.Common;

using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers.Xml
{
	public class PlayerSerializer
	{
		public static readonly PlayerSerializer Instance = new PlayerSerializer();

		private PlayerSerializer()
		{
		}

		public XElement Serialize(Player player, string elementName = "player")
		{
			player.ThrowIfNull("player");
			elementName.ThrowIfNull("elementName");

			return new XElement(
				elementName,
				CharacterSerializer.Instance.Serialize(player.Character),
				player.ActorInstanceTouchedPlayerEventHandler.IfNotNull(arg => EventHandlerSerializer.Instance.Serialize(arg, "actorInstanceTouchedPlayerEventHandler")),
				new XAttribute("id", player.Id),
				new XAttribute("boardId", player.BoardId),
				new XAttribute("coordinate", CoordinateSerializer.Instance.Serialize(player.Coordinate)));
		}

		public Player Deserialize(XElement playerElement)
		{
			playerElement.ThrowIfNull("playerElement");

			return new Player(
				(Guid)playerElement.Attribute("id"),
				(Guid)playerElement.Attribute("boardId"),
				CoordinateSerializer.Instance.Deserialize((string)playerElement.Attribute("coordinate")),
				CharacterSerializer.Instance.Deserialize(playerElement.Element("character")),
				playerElement.Element("actorInstanceTouchedPlayerEventHandler").IfNotNull(EventHandlerSerializer.Instance.Deserialize<ActorInstanceTouchedPlayerEvent>));
		}
	}
}