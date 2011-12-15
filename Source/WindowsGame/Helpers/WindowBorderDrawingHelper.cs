using Junior.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TextAdventure.Engine.Common;

using Color = Microsoft.Xna.Framework.Color;

namespace TextAdventure.WindowsGame.Helpers
{
	public class WindowBorderDrawingHelper
	{
		private static readonly Size _borderSize = new Size(DrawingConstants.Window.TextureSpriteWidth, DrawingConstants.Window.TextureSpriteHeight);
		private static readonly Size _borderSizeTwice = new Size(DrawingConstants.Window.TextureSpriteWidth * 2, DrawingConstants.Window.TextureSpriteHeight * 2);
		private Rectangle _destinationBottomLeftCornerRectangle;
		private Rectangle _destinationBottomRectangle;
		private Rectangle _destinationBottomRightCornerRectangle;
		private Rectangle _destinationLeftRectangle;
		private Rectangle _destinationRightRectangle;
		private Rectangle _destinationTopLeftCornerRectangle;
		private Rectangle _destinationTopRectangle;
		private Rectangle _destinationTopRightCornerRectangle;
		private Rectangle _windowRectangle;

		public WindowBorderDrawingHelper()
		{
			BorderColor = Color.White;
		}

		public static Size BorderSize
		{
			get
			{
				return _borderSize;
			}
		}

		public static Size BorderSizeTwice
		{
			get
			{
				return _borderSizeTwice;
			}
		}

		public Rectangle WindowRectangle
		{
			get
			{
				return _windowRectangle;
			}
			set
			{
				_destinationTopLeftCornerRectangle = new Rectangle(
					value.X,
					value.Y,
					DrawingConstants.Window.TextureSpriteWidth,
					DrawingConstants.Window.TextureSpriteHeight);
				_destinationTopRightCornerRectangle = new Rectangle(
					value.Right - DrawingConstants.Window.TextureSpriteWidth,
					value.Y,
					DrawingConstants.Window.TextureSpriteWidth,
					DrawingConstants.Window.TextureSpriteHeight);
				_destinationBottomLeftCornerRectangle = new Rectangle(
					value.X,
					value.Bottom - DrawingConstants.Window.TextureSpriteHeight,
					DrawingConstants.Window.TextureSpriteWidth,
					DrawingConstants.Window.TextureSpriteHeight);
				_destinationBottomRightCornerRectangle = new Rectangle(
					value.Right - DrawingConstants.Window.TextureSpriteWidth,
					value.Bottom - DrawingConstants.Window.TextureSpriteHeight,
					DrawingConstants.Window.TextureSpriteWidth,
					DrawingConstants.Window.TextureSpriteHeight);
				_destinationLeftRectangle = new Rectangle(
					value.X,
					value.Y + DrawingConstants.Window.TextureSpriteHeight,
					DrawingConstants.Window.TextureSpriteWidth,
					value.Height - (DrawingConstants.Window.TextureSpriteHeight * 2));
				_destinationRightRectangle = new Rectangle(
					value.Right - DrawingConstants.Window.TextureSpriteWidth,
					value.Y + DrawingConstants.Window.TextureSpriteHeight,
					DrawingConstants.Window.TextureSpriteWidth,
					value.Height - (DrawingConstants.Window.TextureSpriteHeight * 2));
				_destinationTopRectangle = new Rectangle(
					value.X + DrawingConstants.Window.TextureSpriteWidth,
					value.Y,
					value.Width - (DrawingConstants.Window.TextureSpriteWidth * 2),
					DrawingConstants.Window.TextureSpriteHeight);
				_destinationBottomRectangle = new Rectangle(
					value.X + DrawingConstants.Window.TextureSpriteWidth,
					value.Bottom - DrawingConstants.Window.TextureSpriteHeight,
					value.Width - (DrawingConstants.Window.TextureSpriteWidth * 2),
					DrawingConstants.Window.TextureSpriteHeight);

				_windowRectangle = value;
			}
		}

		public Texture2D WindowTexture
		{
			get;
			set;
		}

		public Color BorderColor
		{
			get;
			set;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (WindowTexture == null)
			{
				return;
			}

			spriteBatch.ThrowIfNull("spriteBatch");

			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

			spriteBatch.Draw(WindowTexture, _destinationTopLeftCornerRectangle, DrawingConstants.Window.BorderTextureTopLeftRectangle, BorderColor);
			spriteBatch.Draw(WindowTexture, _destinationTopRightCornerRectangle, DrawingConstants.Window.BorderTextureTopRightRectangle, BorderColor);
			spriteBatch.Draw(WindowTexture, _destinationBottomLeftCornerRectangle, DrawingConstants.Window.BorderTextureBottomLeftRectangle, BorderColor);
			spriteBatch.Draw(WindowTexture, _destinationBottomRightCornerRectangle, DrawingConstants.Window.BorderTextureBottomRightRectangle, BorderColor);
			spriteBatch.Draw(WindowTexture, _destinationLeftRectangle, DrawingConstants.Window.BorderTextureLeftRectangle, BorderColor);
			spriteBatch.Draw(WindowTexture, _destinationRightRectangle, DrawingConstants.Window.BorderTextureRightRectangle, BorderColor);
			spriteBatch.Draw(WindowTexture, _destinationTopRectangle, DrawingConstants.Window.BorderTextureTopRectangle, BorderColor);
			spriteBatch.Draw(WindowTexture, _destinationBottomRectangle, DrawingConstants.Window.BorderTextureBottomRectangle, BorderColor);

			spriteBatch.End();
		}
	}
}