using System;
using System.Linq;
using System.Xml.Linq;

using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers
{
	public class WorldSerializer
	{
		public static readonly WorldSerializer Instance = new WorldSerializer();

		private WorldSerializer()
		{
		}

		public XElement Serialize(World world, string elementName = "world")
		{
			world.ThrowIfNull("world");
			elementName.ThrowIfNull("elementName");

			return new XElement(
				elementName,
				PlayerSerializer.Instance.Serialize(world.StartingPlayer, "startingPlayer"),
				world.Boards.Select(arg => BoardSerializer.Instance.Serialize(arg)),
				world.Actors.Select(arg => ActorSerializer.Instance.Serialize(arg)),
				world.Messages.Select(arg => MessageSerializer.Instance.Serialize(arg)),
				new XAttribute("id", world.Id),
				new XAttribute("version", world.Version));
		}

		public World Deserialize(XElement worldElement)
		{
			worldElement.ThrowIfNull("worldElement");

			return new World(
				(Guid)worldElement.Attribute("id"),
				(int)worldElement.Attribute("version"),
				PlayerSerializer.Instance.Deserialize(worldElement.Element("startingPlayer")),
				worldElement.Elements("board").Select(BoardSerializer.Instance.Deserialize),
				worldElement.Elements("actor").Select(ActorSerializer.Instance.Deserialize),
				worldElement.Elements("message").Select(MessageSerializer.Instance.Deserialize));
		}
	}
}