using System.Collections.Generic;

using TextAdventure.Engine.Common;

namespace TextAdventure.Engine.Objects
{
	public interface ILayer
	{
		IEnumerable<ITile> Tiles
		{
			get;
		}
		IEnumerable<Coordinate> EmptyTiles
		{
			get;
		}
		Size Size
		{
			get;
		}

		bool CoordinateIntersects(Coordinate coordinate);
	}

	public interface ILayer<out T> : ILayer
		where T : class, ITile
	{
		T this[int x, int y]
		{
			get;
		}

		T this[Coordinate coordinate]
		{
			get;
		}
	}
}