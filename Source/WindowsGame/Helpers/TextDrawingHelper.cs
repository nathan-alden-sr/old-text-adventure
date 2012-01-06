using System;

using Junior.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TextAdventure.WindowsGame.Extensions;

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
					return new Point((destinationRectangle.Center.X - (textSize.X / 2)).Round(), destinationRectangle.Y);
				case Alignment.TopRight:
					return new Point((destinationRectangle.Right - textSize.X).Round(), destinationRectangle.Y);
				case Alignment.RightCenter:
					return new Point((destinationRectangle.Right - textSize.X).Round(), (destinationRectangle.Center.Y - (textSize.Y / 2)).Round());
				case Alignment.BottomRight:
					return new Point((destinationRectangle.Right - textSize.X).Round(), (destinationRectangle.Bottom - textSize.Y).Round());
				case Alignment.BottomCenter:
					return new Point((destinationRectangle.Center.X - (textSize.X / 2)).Round(), (destinationRectangle.Bottom - textSize.Y).Round());
				case Alignment.BottomLeft:
					return new Point(destinationRectangle.X, (destinationRectangle.Bottom - textSize.Y).Round());
				case Alignment.LeftCenter:
					return new Point(destinationRectangle.X, (destinationRectangle.Center.Y - (textSize.Y / 2)).Round());
				case Alignment.Center:
					return new Point((destinationRectangle.Center.X - (textSize.X / 2)).Round(), (destinationRectangle.Center.Y - (textSize.Y / 2)).Round());
				default:
					throw new ArgumentOutOfRangeException("alignment");
			}
		}
	}
}