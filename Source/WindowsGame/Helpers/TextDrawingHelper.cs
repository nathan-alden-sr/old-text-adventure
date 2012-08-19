using System;

using Junior.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TextAdventure.WindowsGame.Windows;
using TextAdventure.Xna.Extensions;

namespace TextAdventure.WindowsGame.Helpers
{
	public class TextDrawingHelper
	{
		public static readonly TextDrawingHelper Instance = new TextDrawingHelper();

		private TextDrawingHelper()
		{
		}

		public Point GetAlignedOrigin(SpriteFont spriteFont, string text, Rectangle destinationRectangle, WindowAlignment alignment = WindowAlignment.TopLeft)
		{
			spriteFont.ThrowIfNull("spriteFont");
			text.ThrowIfNull("text");

			Vector2 textSize = spriteFont.MeasureString(text);

			return GetAlignedOrigin(textSize, text, destinationRectangle, alignment);
		}

		public Point GetAlignedOrigin(Vector2 textSize, string text, Rectangle destinationRectangle, WindowAlignment alignment = WindowAlignment.TopLeft)
		{
			text.ThrowIfNull("text");

			switch (alignment)
			{
				case WindowAlignment.TopLeft:
					return new Point(destinationRectangle.X, destinationRectangle.Y);
				case WindowAlignment.TopCenter:
					return new Point((destinationRectangle.Center.X - (textSize.X / 2)).Round(), destinationRectangle.Y);
				case WindowAlignment.TopRight:
					return new Point((destinationRectangle.Right - textSize.X).Round(), destinationRectangle.Y);
				case WindowAlignment.RightCenter:
					return new Point((destinationRectangle.Right - textSize.X).Round(), (destinationRectangle.Center.Y - (textSize.Y / 2)).Round());
				case WindowAlignment.BottomRight:
					return new Point((destinationRectangle.Right - textSize.X).Round(), (destinationRectangle.Bottom - textSize.Y).Round());
				case WindowAlignment.BottomCenter:
					return new Point((destinationRectangle.Center.X - (textSize.X / 2)).Round(), (destinationRectangle.Bottom - textSize.Y).Round());
				case WindowAlignment.BottomLeft:
					return new Point(destinationRectangle.X, (destinationRectangle.Bottom - textSize.Y).Round());
				case WindowAlignment.LeftCenter:
					return new Point(destinationRectangle.X, (destinationRectangle.Center.Y - (textSize.Y / 2)).Round());
				case WindowAlignment.Center:
					return new Point((destinationRectangle.Center.X - (textSize.X / 2)).Round(), (destinationRectangle.Center.Y - (textSize.Y / 2)).Round());
				default:
					throw new ArgumentOutOfRangeException("alignment");
			}
		}
	}
}