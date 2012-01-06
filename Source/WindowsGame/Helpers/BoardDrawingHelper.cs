using System;

using Microsoft.Xna.Framework;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.WindowsGame.Helpers
{
	public class BoardDrawingHelper
	{
		public static readonly BoardDrawingHelper Instance = new BoardDrawingHelper();

		private BoardDrawingHelper()
		{
		}

		public void GetDrawingParameters(
			IBoard board,
			IPlayer player,
			out Point topLeftPoint,
			out Coordinate topLeftLayerCoordinate,
			out Coordinate bottomRightCoordinate)
		{
			int drawableTilesToLeft = Math.Min(DrawingConstants.GameWindow.TilesToLeftInclusive, player.Coordinate.X);
			int drawableTilesToTop = Math.Min(DrawingConstants.GameWindow.TilesToTopInclusive, player.Coordinate.Y);
			int drawableTilesToRight = Math.Min(DrawingConstants.GameWindow.TilesToRightExclusive, board.Size.Width - player.Coordinate.X - 1);
			int drawableTilesToBottom = Math.Min(DrawingConstants.GameWindow.TilesToBottomExclusive, board.Size.Height - player.Coordinate.Y - 1);

			topLeftPoint = new Point(
				DrawingConstants.Player.DestinationRectangle.X - (drawableTilesToLeft * DrawingConstants.Tile.TileWidth),
				DrawingConstants.Player.DestinationRectangle.Y - (drawableTilesToTop * DrawingConstants.Tile.TileHeight));
			topLeftLayerCoordinate = new Coordinate(player.Coordinate.X - drawableTilesToLeft, player.Coordinate.Y - drawableTilesToTop);
			bottomRightCoordinate = new Coordinate(player.Coordinate.X + drawableTilesToRight, player.Coordinate.Y + drawableTilesToBottom);
		}

		public Rectangle GetCharacterSourceRectangle(byte symbol)
		{
			return new Rectangle(
				(symbol % 16) * DrawingConstants.Tile.TileWidth,
				(symbol / 16) * DrawingConstants.Tile.TileHeight,
				DrawingConstants.Tile.TileWidth,
				DrawingConstants.Tile.TileHeight);
		}

		public Rectangle GetTileDestinationRectangle(Point topLeftPoint, Coordinate topLeftCoordinate, Coordinate tileCoordinate)
		{
			return new Rectangle(
				topLeftPoint.X + ((tileCoordinate.X - topLeftCoordinate.X) * DrawingConstants.Tile.TileWidth),
				topLeftPoint.Y + ((tileCoordinate.Y - topLeftCoordinate.Y) * DrawingConstants.Tile.TileHeight),
				DrawingConstants.Tile.TileWidth,
				DrawingConstants.Tile.TileHeight);
		}
	}
}