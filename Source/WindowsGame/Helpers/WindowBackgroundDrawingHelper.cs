using Junior.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TextAdventure.WindowsGame.Helpers
{
	public class WindowBackgroundDrawingHelper
	{
		private Rectangle _destinationBottomLeftCornerRectangle;
		private Rectangle _destinationBottomRectangle;
		private Rectangle _destinationBottomRightCornerRectangle;
		private Rectangle _destinationCenterRectangle;
		private Rectangle _destinationLeftRectangle;
		private Rectangle _destinationRightRectangle;
		private Rectangle _destinationTopLeftCornerRectangle;
		private Rectangle _destinationTopRectangle;
		private Rectangle _destinationTopRightCornerRectangle;
		private Rectangle _windowRectangle;

		public WindowBackgroundDrawingHelper()
		{
			BackgroundColor = Color.White;
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
				_destinationCenterRectangle = new Rectangle(
					value.X + DrawingConstants.Window.TextureSpriteWidth,
					value.Y + DrawingConstants.Window.TextureSpriteHeight,
					value.Width - (DrawingConstants.Window.TextureSpriteWidth * 2),
					value.Height - (DrawingConstants.Window.TextureSpriteHeight * 2));

				_windowRectangle = value;
			}
		}

		public Texture2D WindowTexture
		{
			get;
			set;
		}

		public Color BackgroundColor
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

			spriteBatch.Begin();

			spriteBatch.Draw(WindowTexture, _destinationTopLeftCornerRectangle, DrawingConstants.Window.BorderTextureTopLeftRectangle, BackgroundColor);
			spriteBatch.Draw(WindowTexture, _destinationTopRightCornerRectangle, DrawingConstants.Window.BorderTextureTopRightRectangle, BackgroundColor);
			spriteBatch.Draw(WindowTexture, _destinationBottomLeftCornerRectangle, DrawingConstants.Window.BorderTextureBottomLeftRectangle, BackgroundColor);
			spriteBatch.Draw(WindowTexture, _destinationBottomRightCornerRectangle, DrawingConstants.Window.BorderTextureBottomRightRectangle, BackgroundColor);
			spriteBatch.Draw(WindowTexture, _destinationLeftRectangle, DrawingConstants.Window.BorderTextureLeftRectangle, BackgroundColor);
			spriteBatch.Draw(WindowTexture, _destinationRightRectangle, DrawingConstants.Window.BorderTextureRightRectangle, BackgroundColor);
			spriteBatch.Draw(WindowTexture, _destinationTopRectangle, DrawingConstants.Window.BorderTextureTopRectangle, BackgroundColor);
			spriteBatch.Draw(WindowTexture, _destinationBottomRectangle, DrawingConstants.Window.BorderTextureBottomRectangle, BackgroundColor);
			spriteBatch.Draw(WindowTexture, _destinationCenterRectangle, DrawingConstants.Window.BackgroundTextureCenterRectangle, BackgroundColor);

			spriteBatch.End();
		}
	}
}