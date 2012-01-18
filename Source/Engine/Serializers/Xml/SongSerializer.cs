using System;
using System.Xml.Linq;

using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers.Xml
{
	public class SongSerializer
	{
		public static readonly SongSerializer Instance = new SongSerializer();

		private SongSerializer()
		{
		}

		public XElement Serialize(Song song, string elementName = "song")
		{
			song.ThrowIfNull("song");
			elementName.ThrowIfNull("elementName");

			return new XElement(
				elementName,
				new XElement("data", BinarySerializer.Instance.Serialize(song.Data)),
				new XAttribute("id", song.Id),
				new XAttribute("name", song.Name),
				new XAttribute("description", song.Description));
		}

		public Song Deserialize(XElement songElement)
		{
			songElement.ThrowIfNull("songElement");

			return new Song(
				(Guid)songElement.Attribute("id"),
				(string)songElement.Attribute("name"),
				(string)songElement.Attribute("description"),
				BinarySerializer.Instance.Deserialize((string)songElement.Element("data")));
		}
	}
}