using System;
using System.Collections.Generic;
using System.Linq;

using Junior.Common;

using TextAdventure.Engine.Common;

namespace TextAdventure.Engine.Objects
{
	public abstract class Layer<T> : ILayer
		where T : Tile
	{
		private readonly Guid _boardId;
		private readonly Size _size;
		private readonly T[,] _tiles;

		protected Layer(
			Guid boardId,
			Size size)
		{
			_boardId = boardId;
			_size = size;
			_tiles = new T[size.Height,size.Width];
		}

		protected IEnumerable<T> Tiles
		{
			get
			{
				return _tiles
					.Cast<T>()
					.Where(arg => arg != null);
			}
			set
			{
				value.ThrowIfNull("value");

				foreach (T tile in value)
				{
					_tiles[tile.Coordinate.Y, tile.Coordinate.X] = tile;
				}
			}
		}

		public T this[int x, int y]
		{
			get
			{
				return x < 0 || y < 0 || x >= _size.Width || y >= _size.Height ? null : _tiles[y, x];
			}
		}

		public T this[Coordinate coordinate]
		{
			get
			{
				return this[coordinate.X, coordinate.Y];
			}
		}

		public IEnumerable<Coordinate> EmptyTiles
		{
			get
			{
				for (int y = 0; y < _size.Height; y++)
				{
					for (int x = 0; x < _size.Width; x++)
					{
						if (_tiles[y, x] == null)
						{
							yield return new Coordinate(x, y);
						}
					}
				}
			}
		}

		public Guid BoardId
		{
			get
			{
				return _boardId;
			}
		}

		public Size Size
		{
			get
			{
				return _size;
			}
		}

		IEnumerable<Tile> ILayer.Tiles
		{
			get
			{
				return Tiles;
			}
		}

		Tile ILayer.this[int x, int y]
		{
			get
			{
				return _tiles[y, x];
			}
		}

		Tile ILayer.this[Coordinate coordinate]
		{
			get
			{
				return _tiles[coordinate.Y, coordinate.X];
			}
		}

		public bool CoordinateIntersects(Coordinate coordinate)
		{
			return coordinate.X >= 0 && coordinate.Y >= 0 && coordinate.X < _size.Width && coordinate.Y < _size.Height;
		}

		protected internal void SetTile(Coordinate coordinate, T tile)
		{
			tile.ThrowIfNull("tile");

			_tiles[coordinate.Y, coordinate.X] = tile;
		}

		protected internal void MoveTile(Coordinate fromCoordinate, Coordinate toCoordinate)
		{
			if (_tiles[toCoordinate.Y, toCoordinate.X] != null)
			{
				throw new Exception(String.Format("A tile already exists at ({0}, {1}).", toCoordinate.X, toCoordinate.Y));
			}

			_tiles[toCoordinate.Y, toCoordinate.X] = _tiles[fromCoordinate.Y, fromCoordinate.X];
			_tiles[fromCoordinate.Y, fromCoordinate.X] = null;
		}

		protected internal bool RemoveTile(T tile)
		{
			tile.ThrowIfNull("tile");

			if (_tiles[tile.Coordinate.Y, tile.Coordinate.X] != tile)
			{
				return false;
			}

			_tiles[tile.Coordinate.Y, tile.Coordinate.X] = null;
			return true;
		}

		protected internal bool RemoveTile(Coordinate coordinate)
		{
			if (_tiles[coordinate.Y, coordinate.X] == null)
			{
				return false;
			}

			_tiles[coordinate.Y, coordinate.X] = null;
			return true;
		}
	}
}