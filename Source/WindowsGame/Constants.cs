using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using TextAdventure.WindowsGame.Renderers;
using TextAdventure.Xna.Extensions;

namespace TextAdventure.WindowsGame
{
	public static class Constants
	{
		public static class BorderedWindow
		{
			public const int Padding = 2;
		}

		public static class FpsRenderer
		{
			public static readonly Color BackgroundColor;
			public static readonly Color ShadowColor;
			public static readonly Vector2 ShadowOffset;
			public static readonly Color TextColor;

			static FpsRenderer()
			{
				BackgroundColor = Color.Black.WithAlpha(0.25f);
				ShadowColor = Color.Black;
				ShadowOffset = Vector2.One;
				TextColor = Color.White;
			}
		}

		public static class GameBackgroundRenderer
		{
			public static readonly Color Color;

			static GameBackgroundRenderer()
			{
				Color = new Color(34, 52, 72);
			}
		}

		public static class GameWindow
		{
			public static readonly Rectangle DestinationRectangle;
			public static readonly int Height;
			public static readonly int Width;
			public static readonly int TilesToBottomExclusive;
			public static readonly int TilesToLeftInclusive;
			public static readonly int TilesToRightExclusive;
			public static readonly int TilesToTopInclusive;

			static GameWindow()
			{
				Width = 60 * TextAdventure.Xna.Constants.Tile.TileWidth;
				Height = 30 * TextAdventure.Xna.Constants.Tile.TileHeight;
				DestinationRectangle = new Rectangle(0, 0, Width, Height);
				TilesToLeftInclusive = (int)Math.Ceiling(PlayerRenderer.DestinationRectangle.Right / (double)TextAdventure.Xna.Constants.Tile.TileWidth);
				TilesToTopInclusive = (int)Math.Ceiling(PlayerRenderer.DestinationRectangle.Bottom / (double)TextAdventure.Xna.Constants.Tile.TileHeight);
				TilesToRightExclusive = (Width - PlayerRenderer.DestinationRectangle.Left) / TextAdventure.Xna.Constants.Tile.TileWidth;
				TilesToBottomExclusive = (Height - PlayerRenderer.DestinationRectangle.Top) / TextAdventure.Xna.Constants.Tile.TileHeight;
			}
		}

		public static class LogRenderer
		{
			public const int DetailIndent = 10;
			public static readonly Color BackgroundColor;
			public static readonly TimeSpan FadeDuration;
			public static readonly IEnumerable<KeyValuePair<LogEntryType, Color>> ShadowColors;
			public static readonly Vector2 ShadowOffset;
			public static readonly IEnumerable<KeyValuePair<LogEntryType, Color>> TextColors;

			static LogRenderer()
			{
				BackgroundColor = Color.Black.WithAlpha(0.25f);
				FadeDuration = TimeSpan.FromMilliseconds(100);
				ShadowColors = new Dictionary<LogEntryType, Color>
				               	{
				               		{ LogEntryType.CommandExecutedSuccessfully, Color.Black },
				               		{ LogEntryType.CommandExecutionFailed, Color.Black },
				               		{ LogEntryType.EventRaising, Color.Black },
				               		{ LogEntryType.EventComplete, Color.Black },
				               		{ LogEntryType.EventCanceled, Color.Black }
				               	};
				ShadowOffset = Vector2.One;
				TextColors = new Dictionary<LogEntryType, Color>
				             	{
				             		{ LogEntryType.CommandExecutedSuccessfully, Color.White },
				             		{ LogEntryType.CommandExecutionFailed, Color.Red },
				             		{ LogEntryType.EventRaising, Color.LightGreen },
				             		{ LogEntryType.EventComplete, Color.LightBlue },
				             		{ LogEntryType.EventCanceled, Color.PaleVioletRed }
				             	};
			}
		}

		public static class PlayerRenderer
		{
			public static readonly Color BackgroundColor;
			public static readonly Rectangle DestinationRectangle;
			public static readonly Color ForegroundColor;
			public static readonly Rectangle TextureRectangle;

			static PlayerRenderer()
			{
				BackgroundColor = Color.DarkBlue;
				DestinationRectangle = new Rectangle(
					GameWindow.DestinationRectangle.Center.X - (TextAdventure.Xna.Constants.Tile.TileWidth / 2),
					GameWindow.DestinationRectangle.Center.Y - (TextAdventure.Xna.Constants.Tile.TileHeight / 2),
					TextAdventure.Xna.Constants.Tile.TileWidth,
					TextAdventure.Xna.Constants.Tile.TileHeight);
				ForegroundColor = Color.White;
				TextureRectangle = new Rectangle(TextAdventure.Xna.Constants.Tile.TileWidth * 2, 0, TextAdventure.Xna.Constants.Tile.TileWidth, TextAdventure.Xna.Constants.Tile.TileHeight);
			}

			public static class Input
			{
				public const Keys MoveDownKey = Keys.Down;
				public const Keys MoveLeftKey = Keys.Left;
				public const Keys MoveRightKey = Keys.Right;
				public const Keys MoveUpKey = Keys.Up;
				public static readonly TimeSpan InitialInterval;
				public static readonly TimeSpan RepeatingInterval;

				static Input()
				{
					InitialInterval = TimeSpan.FromMilliseconds(250);
					RepeatingInterval = TimeSpan.FromMilliseconds(75);
				}
			}
		}

		public static class WorldTimeRenderer
		{
			public const float MaximumSpeedFactor = 8;
			public const float MinimumSpeedFactor = 1f / 8f;
			public static readonly Color BackgroundColor;
			public static readonly Color PausedColor;
			public static readonly Color ShadowColor;
			public static readonly Vector2 ShadowOffset;
			public static readonly Color UnpausedColor;

			static WorldTimeRenderer()
			{
				BackgroundColor = Color.Black.WithAlpha(0.25f);
				PausedColor = Color.Yellow;
				ShadowColor = Color.Black;
				ShadowOffset = Vector2.One;
				UnpausedColor = Color.White;
			}

			public static class Input
			{
				public const Keys FasterKey = Keys.Add;
				public const Keys PauseKey = Keys.Pause;
				public const Keys SlowerKey = Keys.Subtract;
			}
		}
	}
}