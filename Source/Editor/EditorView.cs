using System;
using System.Drawing;

using Junior.Common;

using TextAdventure.Engine.Common;

using Size = TextAdventure.Engine.Common.Size;

namespace TextAdventure.Editor
{
	public class EditorView : IEditorView
	{
		public Point ScrollPositionInPixels
		{
			get;
			private set;
		}

		public Size ClientSizeInTiles
		{
			get;
			private set;
		}

		public System.Drawing.Size ClientSizeInPixels
		{
			get;
			private set;
		}

		public Size BoardSizeInTiles
		{
			get;
			private set;
		}

		public System.Drawing.Size BoardSizeInPixels
		{
			get;
			private set;
		}

		public Microsoft.Xna.Framework.Point TopLeftPoint
		{
			get;
			private set;
		}

		public Coordinate TopLeftCoordinate
		{
			get;
			private set;
		}

		public Coordinate BottomRightCoordinate
		{
			get;
			private set;
		}

		public System.Drawing.Size VisibleBoardSizeInPixels
		{
			get;
			private set;
		}

		public void Calculate(System.Drawing.Size clientSizeInPixels, Size? boardSizeInTiles, Point scrollPositionInPixels)
		{
			const int tileWidth = TextAdventure.Xna.Constants.Tile.TileWidth;
			const int tileHeight = TextAdventure.Xna.Constants.Tile.TileHeight;

			ScrollPositionInPixels = scrollPositionInPixels;
			ClientSizeInTiles = new Size(
				(int)Math.Ceiling((clientSizeInPixels.Width + (scrollPositionInPixels.X % tileWidth)) / (double)tileWidth),
				(int)Math.Ceiling((clientSizeInPixels.Height + (scrollPositionInPixels.Y % tileHeight)) / (double)tileHeight));
			ClientSizeInPixels = clientSizeInPixels;
			BoardSizeInTiles = new Size(boardSizeInTiles.IfNotNull(arg => (int?)arg.Width) ?? 0, boardSizeInTiles.IfNotNull(arg => (int?)arg.Height) ?? 0);
			BoardSizeInPixels = new System.Drawing.Size(BoardSizeInTiles.Width * tileWidth, BoardSizeInTiles.Height * tileHeight);
			TopLeftPoint = new Microsoft.Xna.Framework.Point(-(scrollPositionInPixels.X % tileWidth), -(scrollPositionInPixels.Y % tileHeight));
			TopLeftCoordinate = new Coordinate(scrollPositionInPixels.X / tileWidth, scrollPositionInPixels.Y / tileHeight);
			BottomRightCoordinate = new Coordinate(
				Math.Min(BoardSizeInTiles.Width - 1, TopLeftCoordinate.X + ClientSizeInTiles.Width),
				Math.Min(BoardSizeInTiles.Height - 1, TopLeftCoordinate.Y + ClientSizeInTiles.Height));

			var visibleCoordinateSize = new Coordinate(BottomRightCoordinate.X - TopLeftCoordinate.X + 1, BottomRightCoordinate.Y - TopLeftCoordinate.Y + 1);

			VisibleBoardSizeInPixels = new System.Drawing.Size(
				Math.Min(clientSizeInPixels.Width, TopLeftPoint.X + (visibleCoordinateSize.X * tileWidth)),
				Math.Min(clientSizeInPixels.Height, TopLeftPoint.Y + (visibleCoordinateSize.Y * tileHeight)));
		}

		public Coordinate? GetCoordinateFromClientPoint(Point clientPoint)
		{
			const int tileWidth = TextAdventure.Xna.Constants.Tile.TileWidth;
			const int tileHeight = TextAdventure.Xna.Constants.Tile.TileHeight;
			var originCoordinate = new Coordinate(
				TopLeftCoordinate.X + ((clientPoint.X - TopLeftPoint.X) / tileWidth),
				TopLeftCoordinate.Y + ((clientPoint.Y - TopLeftPoint.Y) / tileHeight));

			return originCoordinate.X < BoardSizeInTiles.Width && originCoordinate.Y < BoardSizeInTiles.Height ? originCoordinate : (Coordinate?)null;
		}
	}
}