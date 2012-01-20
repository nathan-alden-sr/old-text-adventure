using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TextAdventure.Xna
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
				FadeInDuration = TimeSpan.FromMilliseconds(150);
				FadeOutDuration = TimeSpan.FromMilliseconds(75);
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

		public static class Tile
		{
			public const int TileHeight = 24;
			public const int TileWidth = 14;
		}
	}
}