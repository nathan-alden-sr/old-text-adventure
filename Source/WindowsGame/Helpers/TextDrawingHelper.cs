using System;

using Junior.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TextAdventure.WindowsGame.Helpers
{
	public class TextDrawingHelper
	{
		public static readonly TextDrawingHelper Instance = new TextDrawingHelper();

		private TextDrawingHelper()
		{
		}

		public Point GetAlignedOrigin(SpriteFont spriteFont, string text, Rectangle destinationRectangle, Alignment alignment = Alignment.TopLeft)
		{
			spriteFont.ThrowIfNull("spriteFont");
			text.ThrowIfNull("text");

			Vector2 textSize = spriteFont.MeasureString(text);

			return GetAlignedOrigin(textSize, text, destinationRectangle, alignment);
		}

		public Point GetAlignedOrigin(Vector2 textSize, string text, Rectangle destinationRectangle, Alignment alignment = Alignment.TopLeft)
		{
			text.ThrowIfNull("text");

			switch (alignment)
			{
				case Alignment.TopLeft:
					return new Point(destinationRectangle.X, destinationRectangle.Y);
				case Alignment.TopCenter:
					return new Point((int)(destinationRectangle.Center.X - (textSize.X / 2)), destinationRectangle.Y);
				case Alignment.TopRight:
					return new Point((int)(destinationRectangle.Right - textSize.X), destinationRectangle.Y);
				case Alignment.RightCenter:
					return new Point((int)(destinationRectangle.Right - textSize.X), (int)(destinationRectangle.Center.Y - (textSize.Y / 2)));
				case Alignment.BottomRight:
					return new Point((int)(destinationRectangle.Right - textSize.X), (int)(destinationRectangle.Bottom - textSize.Y));
				case Alignment.BottomCenter:
					return new Point((int)(destinationRectangle.Center.X - (textSize.X / 2)), (int)(destinationRectangle.Bottom - textSize.Y));
				case Alignment.BottomLeft:
					return new Point(destinationRectangle.X, (int)(destinationRectangle.Bottom - textSize.Y));
				case Alignment.LeftCenter:
					return new Point(destinationRectangle.X, (int)(destinationRectangle.Center.Y - (textSize.Y / 2)));
				case Alignment.Center:
					return new Point((int)(destinationRectangle.Center.X - (textSize.X / 2)), (int)(destinationRectangle.Center.Y - (textSize.Y / 2)));
				default:
					throw new ArgumentOutOfRangeException("alignment");
			}
		}
	}
}