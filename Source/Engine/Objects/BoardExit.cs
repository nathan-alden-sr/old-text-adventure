using System;

using TextAdventure.Engine.Common;

namespace TextAdventure.Engine.Objects
{
	public class BoardExit
	{
		private readonly Coordinate _coordinate;
		private readonly Guid _destinationBoardId;
		private readonly Coordinate _destinationCoordinate;
		private readonly BoardExitDirection _direction;

		public BoardExit(
			Coordinate coordinate,
			BoardExitDirection direction,
			Guid destinationBoardId,
			Coordinate destinationCoordinate)
		{
			_coordinate = coordinate;
			_direction = direction;
			_destinationBoardId = destinationBoardId;
			_destinationCoordinate = destinationCoordinate;
		}

		public Coordinate Coordinate
		{
			get
			{
				return _coordinate;
			}
		}

		public BoardExitDirection Direction
		{
			get
			{
				return _direction;
			}
		}

		public Guid DestinationBoardId
		{
			get
			{
				return _destinationBoardId;
			}
		}

		public Coordinate DestinationCoordinate
		{
			get
			{
				return _destinationCoordinate;
			}
		}
	}
}