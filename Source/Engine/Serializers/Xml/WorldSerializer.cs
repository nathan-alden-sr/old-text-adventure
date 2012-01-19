using System;
using System.Linq;
using System.Xml.Linq;

using Junior.Common;

using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers.Xml
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
				world.Timers.Select(arg => TimerSerializer.Instance.Serialize(arg)),
				world.SoundEffects.Select(arg => SoundEffectSerializer.Instance.Serialize(arg)),
				world.Songs.Select(arg => SongSerializer.Instance.Serialize(arg)),
				world.AnswerSelectedEventHandler.IfNotNull(arg => EventHandlerSerializer.Instance.Serialize(arg, "answerSelectedEventHandler")),
				new XAttribute("id", world.Id),
				new XAttribute("version", world.Version),
				new XAttribute("title", world.Title));
		}

		public World Deserialize(XElement worldElement)
		{
			worldElement.ThrowIfNull("worldElement");

			return new World(
				(Guid)worldElement.Attribute("id"),
				(int)worldElement.Attribute("version"),
				(string)worldElement.Attribute("title"),
				PlayerSerializer.Instance.Deserialize(worldElement.Element("startingPlayer")),
				worldElement.Elements("board").Select(BoardSerializer.Instance.Deserialize),
				worldElement.Elements("actor").Select(ActorSerializer.Instance.Deserialize),
				worldElement.Elements("message").Select(MessageSerializer.Instance.Deserialize),
				worldElement.Elements("timer").Select(TimerSerializer.Instance.Deserialize),
				worldElement.Elements("soundEffect").Select(SoundEffectSerializer.Instance.Deserialize),
				worldElement.Elements("song").Select(SongSerializer.Instance.Deserialize),
				worldElement.Element("answerSelectedEventHandler").IfNotNull(EventHandlerSerializer.Instance.Deserialize<AnswerSelectedEvent>));
		}
	}
}