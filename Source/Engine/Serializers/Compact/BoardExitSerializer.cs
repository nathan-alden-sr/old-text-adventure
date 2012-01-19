using System;
using System.Text;

using Junior.Common;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers.Compact
{
	public class BoardExitSerializer
	{
		public static readonly BoardExitSerializer Instance = new BoardExitSerializer();

		private BoardExitSerializer()
		{
		}

		public byte[] Serialize(BoardExit boardExit)
		{
			boardExit.ThrowIfNull("boardExit");

			var serializer = new CompactSerializer();

			serializer[0] = CoordinateSerializer.Instance.Serialize(boardExit.Coordinate);
			serializer[1] = Encoding.UTF8.GetBytes(boardExit.Direction.ToString());
			serializer[2] = boardExit.DestinationBoardId.ToByteArray();
			serializer[3] = CoordinateSerializer.Instance.Serialize(boardExit.DestinationCoordinate);

			return serializer.Serialize();
		}

		public BoardExit Deserialize(byte[] serializedData)
		{
			serializedData.ThrowIfNull("serializedData");

			var serializer = new CompactSerializer(serializedData);
			Coordinate coordinate = CoordinateSerializer.Instance.Deserialize(serializer[0]);
			BoardExitDirection direction = Enum<BoardExitDirection>.Parse(Encoding.UTF8.GetString(serializer[1]));
			var destinationBoardId = new Guid(serializer[2]);
			Coordinate destinationCoordinate = CoordinateSerializer.Instance.Deserialize(serializer[3]);

			return new BoardExit(coordinate, direction, destinationBoardId, destinationCoordinate);
		}
	}
}