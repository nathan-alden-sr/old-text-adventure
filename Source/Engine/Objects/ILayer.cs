using System;
using System.Collections.Generic;

using TextAdventure.Engine.Common;

namespace TextAdventure.Engine.Objects
{
	public interface ILayer
	{
		Guid BoardId
		{
			get;
		}
		Size Size
		{
			get;
		}

		IEnumerable<Tile> Tiles
		{
			get;
		}

		Tile this[int x, int y]
		{
			get;
		}

		Tile this[Coordinate coordinate]
		{
			get;
		}
	}
}