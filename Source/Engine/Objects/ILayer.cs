using System.Collections.Generic;

using TextAdventure.Engine.Common;

namespace TextAdventure.Engine.Objects
{
	public interface ILayer
	{
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

		Size Size
		{
			get;
		}
	}
}