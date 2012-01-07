using System;

using TextAdventure.Engine.Common;

namespace TextAdventure.Engine.Objects
{
	public class BoardExit : IBoardExit
	{
		public BoardExit(
			Coordinate coordinate,
			BoardExitDirection direction,
			Guid destinationBoardId,
			Coordinate destinationCoordinate)
		{
			Coordinate = coordinate;
			Direction = direction;
			DestinationBoardId = destinationBoardId;
			DestinationCoordinate = destinationCoordinate;
		}

		public Coordinate Coordinate
		{
			get;
			set;
		}

		public BoardExitDirection Direction
		{
			get;
			set;
		}

		public Guid DestinationBoardId
		{
			get;
			set;
		}

		public Coordinate DestinationCoordinate
		{
			get;
			set;
		}
	}
}