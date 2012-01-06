using System;

using Microsoft.Xna.Framework;

namespace TextAdventure.WindowsGame
{
	public static class DrawingConstants
	{
		public static class GameWindow
		{
			public static readonly Rectangle DestinationRectangle;
			public static readonly int PreferredBackBufferHeight;
			public static readonly int PreferredBackBufferWidth;
			public static readonly int TilesToBottomExclusive;
			public static readonly int TilesToLeftInclusive;
			public static readonly int TilesToRightExclusive;
			public static readonly int TilesToTopInclusive;

			static GameWindow()
			{
				PreferredBackBufferWidth = 60 * Tile.TileWidth;
				PreferredBackBufferHeight = 30 * Tile.TileHeight;
				DestinationRectangle = new Rectangle(0, 0, PreferredBackBufferWidth, PreferredBackBufferHeight);
				TilesToLeftInclusive = (int)Math.Ceiling(Player.DestinationRectangle.Right / (double)Tile.TileWidth);
				TilesToTopInclusive = (int)Math.Ceiling(Player.DestinationRectangle.Bottom / (double)Tile.TileHeight);
				TilesToRightExclusive = (PreferredBackBufferWidth - Player.DestinationRectangle.Left) / Tile.TileWidth;
				TilesToBottomExclusive = (PreferredBackBufferHeight - Player.DestinationRectangle.Top) / Tile.TileHeight;
			}
		}

		public static class Player
		{
			public static readonly Rectangle DestinationRectangle;
			public static readonly Rectangle TextureRectangle;

			static Player()
			{
				TextureRectangle = new Rectangle(Tile.TileWidth * 2, 0, Tile.TileWidth, Tile.TileHeight);
				DestinationRectangle = new Rectangle(
					GameWindow.DestinationRectangle.Center.X - (Tile.TileWidth / 2),
					GameWindow.DestinationRectangle.Center.Y - (Tile.TileHeight / 2),
					Tile.TileWidth,
					Tile.TileHeight);
			}
		}

		public static class Tile
		{
			public const int TileHeight = 24;
			public const int TileWidth = 14;
		}

		public static class Window
		{
			public const int Padding = 2;
		}
	}
}