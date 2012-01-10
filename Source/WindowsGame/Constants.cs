using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using TextAdventure.WindowsGame.Extensions;
using TextAdventure.WindowsGame.Renderers;

namespace TextAdventure.WindowsGame
{
	public static class Constants
	{
		public static class BoardRenderer
		{
			public const byte BoardExitDownSymbol = 31;
			public const byte BoardExitLeftSymbol = 16;
			public const byte BoardExitRightSymbol = 17;
			public const byte BoardExitUpSymbol = 30;
			public static readonly Color BoardExitSymbolColor;

			static BoardRenderer()
			{
				BoardExitSymbolColor = Color.White * 0.25f;
			}
		}

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

			public static class Input
			{
				public static readonly IEnumerable<IEnumerable<Keys>> VisibilityToggleKeysSets;

				static Input()
				{
					VisibilityToggleKeysSets = new[]
					                           	{
					                           		new[] { Keys.LeftControl, Keys.F },
					                           		new[] { Keys.RightControl, Keys.F }
					                           	};
				}
			}
		}

		public static class GameBackgroundRenderer
		{
			public static readonly Color Color;

			static GameBackgroundRenderer()
			{
				Color = Color.White;
			}
		}

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
				TilesToLeftInclusive = (int)Math.Ceiling(PlayerRenderer.DestinationRectangle.Right / (double)Tile.TileWidth);
				TilesToTopInclusive = (int)Math.Ceiling(PlayerRenderer.DestinationRectangle.Bottom / (double)Tile.TileHeight);
				TilesToRightExclusive = (PreferredBackBufferWidth - PlayerRenderer.DestinationRectangle.Left) / Tile.TileWidth;
				TilesToBottomExclusive = (PreferredBackBufferHeight - PlayerRenderer.DestinationRectangle.Top) / Tile.TileHeight;
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
				               		{ LogEntryType.EventCanceled, Color.Black },
				               		{ LogEntryType.EventHandled, Color.Black }
				               	};
				ShadowOffset = Vector2.One;
				TextColors = new Dictionary<LogEntryType, Color>
				             	{
				             		{ LogEntryType.CommandExecutedSuccessfully, Color.White },
				             		{ LogEntryType.CommandExecutionFailed, Color.Red },
				             		{ LogEntryType.EventRaising, Color.LightGreen },
				             		{ LogEntryType.EventComplete, Color.LightBlue },
				             		{ LogEntryType.EventCanceled, Color.PaleVioletRed },
				             		{ LogEntryType.EventHandled, Color.LightBlue }
				             	};
			}

			public static class Input
			{
				public static readonly IEnumerable<IEnumerable<Keys>> VisibilityToggleKeysSets;

				static Input()
				{
					VisibilityToggleKeysSets = new[]
					                           	{
					                           		new[] { Keys.LeftControl, Keys.L },
					                           		new[] { Keys.RightControl, Keys.L }
					                           	};
				}
			}
		}

		public static class MessageRenderer
		{
			public const int AnswerHorizontalPadding = 5;
			public const int AnswerHorizontalTextPadding = 5;
			public const int ArrowHorizontalPadding = 5;
			public const float MaximumLineWidthAsPercentageOfGameWindowDestinationRectangle = 0.75f;
			public const int ScrollStep = 5;
			public const int VerticalOffsetFromGameWindowDestinationRectangleCenter = 30;
			public const int VerticalWindowPadding = 10;
			public static readonly Color ArrowColor;
			public static readonly Color DisabledArrowColor;
			public static readonly TimeSpan FadeInDuration;
			public static readonly TimeSpan FadeOutDuration;
			public static readonly Color ShadowColor;
			public static readonly Vector2 ShadowOffset;
			public static readonly Color TextColor;

			static MessageRenderer()
			{
				ArrowColor = Color.White;
				DisabledArrowColor = Color.White * 0.25f;
				FadeInDuration = TimeSpan.FromMilliseconds(60);
				FadeOutDuration = TimeSpan.FromMilliseconds(30);
				ShadowColor = Color.Black;
				ShadowOffset = Vector2.One;
				TextColor = Color.White;
			}

			public static class Input
			{
				public const Keys AcceptKey = Keys.Enter;
				public const Keys EndKey = Keys.End;
				public const Keys HomeKey = Keys.Home;
				public const Keys NextAnswerKey = Keys.Right;
				public const Keys PageDownKey = Keys.PageDown;
				public const Keys PageUpKey = Keys.PageUp;
				public const Keys PreviousAnswerKey = Keys.Left;
				public const Keys ScrollDownKey = Keys.Down;
				public const Keys ScrollUpKey = Keys.Up;
				public static readonly TimeSpan ScrollKeyboardInterval;

				static Input()
				{
					ScrollKeyboardInterval = TimeSpan.Zero;
				}
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
					GameWindow.DestinationRectangle.Center.X - (Tile.TileWidth / 2),
					GameWindow.DestinationRectangle.Center.Y - (Tile.TileHeight / 2),
					Tile.TileWidth,
					Tile.TileHeight);
				ForegroundColor = Color.White;
				TextureRectangle = new Rectangle(Tile.TileWidth * 2, 0, Tile.TileWidth, Tile.TileHeight);
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
					RepeatingInterval = TimeSpan.FromMilliseconds(30);
				}
			}
		}

		public static class Tile
		{
			public const int TileHeight = 24;
			public const int TileWidth = 14;
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
				public static readonly IEnumerable<IEnumerable<Keys>> VisibilityToggleKeysSets;

				static Input()
				{
					VisibilityToggleKeysSets = new[]
					                           	{
					                           		new[] { Keys.LeftControl, Keys.T },
					                           		new[] { Keys.RightControl, Keys.T }
					                           	};
				}
			}
		}
	}
}