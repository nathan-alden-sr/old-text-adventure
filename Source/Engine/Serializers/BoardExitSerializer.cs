using System;
using System.Xml.Linq;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers
{
	public class BoardExitSerializer
	{
		public static readonly BoardExitSerializer Instance = new BoardExitSerializer();

		private BoardExitSerializer()
		{
		}

		public XElement Serialize(BoardExit boardExit, string elementName = "boardExit")
		{
			return new XElement(
				elementName,
				new XAttribute("coordinate", CoordinateSerializer.Instance.Serialize(boardExit.Coordinate)),
				new XAttribute("direction", BoardExitDirectionSerializer.Instance.Serialize(boardExit.Direction)),
				new XAttribute("destinationBoardId", boardExit.DestinationBoardId),
				new XAttribute("destinationCoordinate", CoordinateSerializer.Instance.Serialize(boardExit.DestinationCoordinate)));
		}

		public BoardExit Deserialize(XElement boardExitElement)
		{
			return new BoardExit(
				CoordinateSerializer.Instance.Deserialize((string)boardExitElement.Attribute("coordinate")),
				BoardExitDirectionSerializer.Instance.Deserialize((string)boardExitElement.Attribute("direction")),
				(Guid)boardExitElement.Attribute("destinationBoardId"),
				CoordinateSerializer.Instance.Deserialize((string)boardExitElement.Attribute("destinationCoordinate")));
		}
	}
}