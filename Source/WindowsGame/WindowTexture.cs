using Junior.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TextAdventure.WindowsGame
{
	public class WindowTexture
	{
		private readonly Rectangle _backgroundBottomLeftRectangle;
		private readonly Rectangle _backgroundBottomRectangle;
		private readonly Rectangle _backgroundBottomRightRectangle;
		private readonly Rectangle _backgroundCenterRectangle;
		private readonly Rectangle _backgroundLeftRectangle;
		private readonly Rectangle _backgroundRightRectangle;
		private readonly Rectangle _backgroundTopLeftRectangle;
		private readonly Rectangle _backgroundTopRectangle;
		private readonly Rectangle _backgroundTopRightRectangle;
		private readonly Rectangle _borderBottomLeftRectangle;
		private readonly Rectangle _borderBottomRectangle;
		private readonly Rectangle _borderBottomRightRectangle;
		private readonly Rectangle _borderRightRectangle;
		private readonly Rectangle _borderTextureLeftRectangle;
		private readonly Rectangle _borderTopLeftRectangle;
		private readonly Rectangle _borderTopRectangle;
		private readonly Rectangle _borderTopRightRectangle;
		private readonly Rectangle _downArrowRectangle;
		private readonly Padding _padding;
		private readonly int _spriteHeight;
		private readonly int _spriteWidth;
		private readonly Texture2D _texture;
		private readonly Rectangle _upArrowRectangle;

		public WindowTexture(Texture2D texture, int spriteWidth, int spriteHeight)
		{
			texture.ThrowIfNull("texture");

			_texture = texture;
			_spriteWidth = spriteWidth;
			_spriteHeight = spriteHeight;
			_padding = new Padding(spriteWidth, spriteHeight);
			_borderTopLeftRectangle = new Rectangle(0, 0, spriteWidth, spriteHeight);
			_borderTopRectangle = new Rectangle(spriteWidth, 0, spriteWidth, spriteHeight);
			_borderTopRightRectangle = new Rectangle(spriteWidth * 2, 0, spriteWidth, spriteHeight);
			_borderRightRectangle = new Rectangle(spriteWidth * 2, spriteHeight, spriteWidth, spriteHeight);
			_borderBottomRightRectangle = new Rectangle(spriteWidth * 2, spriteHeight * 2, spriteWidth, spriteHeight);
			_borderBottomRectangle = new Rectangle(spriteWidth, spriteHeight * 2, spriteWidth, spriteHeight);
			_borderBottomLeftRectangle = new Rectangle(0, spriteHeight * 2, spriteWidth, spriteHeight);
			_borderTextureLeftRectangle = new Rectangle(0, spriteHeight, spriteWidth, spriteHeight);
			_backgroundTopLeftRectangle = new Rectangle(0, spriteHeight * 3, spriteWidth, spriteHeight);
			_backgroundTopRectangle = new Rectangle(spriteWidth, spriteHeight * 3, spriteWidth, spriteHeight);
			_backgroundTopRightRectangle = new Rectangle(spriteWidth * 2, spriteHeight * 3, spriteWidth, spriteHeight);
			_backgroundRightRectangle = new Rectangle(spriteWidth * 2, spriteHeight + (spriteHeight * 3), spriteWidth, spriteHeight);
			_backgroundBottomRightRectangle = new Rectangle(spriteWidth * 2, (spriteHeight * 2) + (spriteHeight * 3), spriteWidth, spriteHeight);
			_backgroundBottomRectangle = new Rectangle(spriteWidth, (spriteHeight * 2) + (spriteHeight * 3), spriteWidth, spriteHeight);
			_backgroundBottomLeftRectangle = new Rectangle(0, (spriteHeight * 2) + (spriteHeight * 3), spriteWidth, spriteHeight);
			_backgroundLeftRectangle = new Rectangle(0, spriteHeight + (spriteHeight * 3), spriteWidth, spriteHeight);
			_backgroundCenterRectangle = new Rectangle(spriteWidth, spriteHeight + (spriteHeight * 3), spriteWidth, spriteHeight);
			_upArrowRectangle = new Rectangle(spriteWidth * 3, 0, spriteWidth, spriteHeight);
			_downArrowRectangle = new Rectangle(spriteWidth * 3, spriteHeight, spriteWidth, spriteHeight);
		}

		public Texture2D Texture
		{
			get
			{
				return _texture;
			}
		}

		public int SpriteWidth
		{
			get
			{
				return _spriteWidth;
			}
		}

		public int SpriteHeight
		{
			get
			{
				return _spriteHeight;
			}
		}

		public Padding Padding
		{
			get
			{
				return _padding;
			}
		}

		public Rectangle BorderTopLeftRectangle
		{
			get
			{
				return _borderTopLeftRectangle;
			}
		}

		public Rectangle BorderTopRectangle
		{
			get
			{
				return _borderTopRectangle;
			}
		}

		public Rectangle BorderTopRightRectangle
		{
			get
			{
				return _borderTopRightRectangle;
			}
		}

		public Rectangle BorderRightRectangle
		{
			get
			{
				return _borderRightRectangle;
			}
		}

		public Rectangle BorderBottomRightRectangle
		{
			get
			{
				return _borderBottomRightRectangle;
			}
		}

		public Rectangle BorderBottomRectangle
		{
			get
			{
				return _borderBottomRectangle;
			}
		}

		public Rectangle BorderBottomLeftRectangle
		{
			get
			{
				return _borderBottomLeftRectangle;
			}
		}

		public Rectangle BorderTextureLeftRectangle
		{
			get
			{
				return _borderTextureLeftRectangle;
			}
		}

		public Rectangle BackgroundBottomLeftRectangle
		{
			get
			{
				return _backgroundBottomLeftRectangle;
			}
		}

		public Rectangle BackgroundBottomRectangle
		{
			get
			{
				return _backgroundBottomRectangle;
			}
		}

		public Rectangle BackgroundBottomRightRectangle
		{
			get
			{
				return _backgroundBottomRightRectangle;
			}
		}

		public Rectangle BackgroundCenterRectangle
		{
			get
			{
				return _backgroundCenterRectangle;
			}
		}

		public Rectangle BackgroundLeftRectangle
		{
			get
			{
				return _backgroundLeftRectangle;
			}
		}

		public Rectangle BackgroundRightRectangle
		{
			get
			{
				return _backgroundRightRectangle;
			}
		}

		public Rectangle BackgroundTopLeftRectangle
		{
			get
			{
				return _backgroundTopLeftRectangle;
			}
		}

		public Rectangle BackgroundTopRectangle
		{
			get
			{
				return _backgroundTopRectangle;
			}
		}

		public Rectangle BackgroundTopRightRectangle
		{
			get
			{
				return _backgroundTopRightRectangle;
			}
		}

		public Rectangle DownArrowRectangle
		{
			get
			{
				return _downArrowRectangle;
			}
		}

		public Rectangle UpArrowRectangle
		{
			get
			{
				return _upArrowRectangle;
			}
		}
	}
}