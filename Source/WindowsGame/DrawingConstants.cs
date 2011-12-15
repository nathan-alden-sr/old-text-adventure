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
			public const int TextureSpriteHeight = 8;
			public const int TextureSpriteWidth = 8;
			public static readonly Rectangle BackgroundTextureBottomLeftRectangle;
			public static readonly Rectangle BackgroundTextureBottomRectangle;
			public static readonly Rectangle BackgroundTextureBottomRightRectangle;
			public static readonly Rectangle BackgroundTextureCenterRectangle;
			public static readonly Rectangle BackgroundTextureLeftRectangle;
			public static readonly Rectangle BackgroundTextureRightRectangle;
			public static readonly Rectangle BackgroundTextureTopLeftRectangle;
			public static readonly Rectangle BackgroundTextureTopRectangle;
			public static readonly Rectangle BackgroundTextureTopRightRectangle;
			public static readonly Rectangle BorderTextureBottomLeftRectangle;
			public static readonly Rectangle BorderTextureBottomRectangle;
			public static readonly Rectangle BorderTextureBottomRightRectangle;
			public static readonly Rectangle BorderTextureLeftRectangle;
			public static readonly Rectangle BorderTextureRightRectangle;
			public static readonly Rectangle BorderTextureTopLeftRectangle;
			public static readonly Rectangle BorderTextureTopRectangle;
			public static readonly Rectangle BorderTextureTopRightRectangle;

			static Window()
			{
				BackgroundTextureTopLeftRectangle = new Rectangle(0, TextureSpriteHeight * 3, TextureSpriteWidth, TextureSpriteHeight);
				BackgroundTextureTopRectangle = new Rectangle(TextureSpriteWidth, TextureSpriteHeight * 3, TextureSpriteWidth, TextureSpriteHeight);
				BackgroundTextureTopRightRectangle = new Rectangle(TextureSpriteWidth * 2, TextureSpriteHeight * 3, TextureSpriteWidth, TextureSpriteHeight);
				BackgroundTextureRightRectangle = new Rectangle(TextureSpriteWidth * 2, TextureSpriteHeight + (TextureSpriteHeight * 3), TextureSpriteWidth, TextureSpriteHeight);
				BackgroundTextureBottomRightRectangle = new Rectangle(TextureSpriteWidth * 2, (TextureSpriteHeight * 2) + (TextureSpriteHeight * 3), TextureSpriteWidth, TextureSpriteHeight);
				BackgroundTextureBottomRectangle = new Rectangle(TextureSpriteWidth, (TextureSpriteHeight * 2) + (TextureSpriteHeight * 3), TextureSpriteWidth, TextureSpriteHeight);
				BackgroundTextureBottomLeftRectangle = new Rectangle(0, (TextureSpriteHeight * 2) + (TextureSpriteHeight * 3), TextureSpriteWidth, TextureSpriteHeight);
				BackgroundTextureLeftRectangle = new Rectangle(0, TextureSpriteHeight + (TextureSpriteHeight * 3), TextureSpriteWidth, TextureSpriteHeight);
				BackgroundTextureCenterRectangle = new Rectangle(TextureSpriteWidth, TextureSpriteHeight + (TextureSpriteHeight * 3), TextureSpriteWidth, TextureSpriteHeight);
				BorderTextureTopLeftRectangle = new Rectangle(0, 0, TextureSpriteWidth, TextureSpriteHeight);
				BorderTextureTopRectangle = new Rectangle(TextureSpriteWidth, 0, TextureSpriteWidth, TextureSpriteHeight);
				BorderTextureTopRightRectangle = new Rectangle(TextureSpriteWidth * 2, 0, TextureSpriteWidth, TextureSpriteHeight);
				BorderTextureRightRectangle = new Rectangle(TextureSpriteWidth * 2, TextureSpriteHeight, TextureSpriteWidth, TextureSpriteHeight);
				BorderTextureBottomRightRectangle = new Rectangle(TextureSpriteWidth * 2, TextureSpriteHeight * 2, TextureSpriteWidth, TextureSpriteHeight);
				BorderTextureBottomRectangle = new Rectangle(TextureSpriteWidth, TextureSpriteHeight * 2, TextureSpriteWidth, TextureSpriteHeight);
				BorderTextureBottomLeftRectangle = new Rectangle(0, TextureSpriteHeight * 2, TextureSpriteWidth, TextureSpriteHeight);
				BorderTextureLeftRectangle = new Rectangle(0, TextureSpriteHeight, TextureSpriteWidth, TextureSpriteHeight);
			}
		}
	}
}