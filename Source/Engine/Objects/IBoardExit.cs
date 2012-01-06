using System;

using TextAdventure.Engine.Common;

namespace TextAdventure.Engine.Objects
{
	public interface IBoardExit
	{
		Coordinate Coordinate
		{
			get;
		}
		BoardExitDirection Direction
		{
			get;
		}
		Guid DestinationBoardId
		{
			get;
		}
		Coordinate DestinationCoordinate
		{
			get;
		}
	}
}