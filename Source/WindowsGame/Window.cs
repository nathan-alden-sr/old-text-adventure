using Microsoft.Xna.Framework;

namespace TextAdventure.WindowsGame
{
	public struct Window
	{
		private readonly Rectangle _absoluteClientRectangle;
		private readonly Rectangle _bottomLeftCornerRectangle;
		private readonly Rectangle _bottomRectangle;
		private readonly Rectangle _bottomRightCornerRectangle;
		private readonly Rectangle _centerRectangle;
		private readonly Rectangle _leftRectangle;
		private readonly Rectangle _relativeClientRectangle;
		private readonly Rectangle _rightRectangle;
		private readonly Rectangle _topLeftCornerRectangle;
		private readonly Rectangle _topRectangle;
		private readonly Rectangle _topRightCornerRectangle;
		private readonly Rectangle _windowRectangle;

		public Window(Rectangle rectangle)
			: this(rectangle, Padding.None)
		{
		}

		public Window(Rectangle rectangle, Padding padding)
		{
			Rectangle absoluteClientRectangle = rectangle;
			int paddingX = padding.Left + padding.Right;
			int paddingY = padding.Top + padding.Bottom;

			absoluteClientRectangle.X += padding.Left;
			absoluteClientRectangle.Y += padding.Top;
			absoluteClientRectangle.Width -= paddingX;
			absoluteClientRectangle.Height -= paddingY;

			_absoluteClientRectangle = absoluteClientRectangle;
			_relativeClientRectangle = new Rectangle(padding.Left, padding.Top, rectangle.Width - paddingX, rectangle.Height - paddingY);
			_windowRectangle = rectangle;
			_topLeftCornerRectangle = new Rectangle(
				_windowRectangle.X,
				_windowRectangle.Y,
				_relativeClientRectangle.X,
				_relativeClientRectangle.Y);
			_topRightCornerRectangle = new Rectangle(
				_absoluteClientRectangle.Right,
				_windowRectangle.Y,
				_windowRectangle.Right - _absoluteClientRectangle.Right,
				_relativeClientRectangle.Y);
			_bottomLeftCornerRectangle = new Rectangle(
				_windowRectangle.X,
				_absoluteClientRectangle.Bottom,
				_relativeClientRectangle.X,
				_windowRectangle.Bottom - _absoluteClientRectangle.Bottom);
			_bottomRightCornerRectangle = new Rectangle(
				_absoluteClientRectangle.Right,
				_absoluteClientRectangle.Bottom,
				_windowRectangle.Right - _absoluteClientRectangle.Right,
				_windowRectangle.Bottom - _absoluteClientRectangle.Bottom);
			_leftRectangle = new Rectangle(
				_windowRectangle.X,
				_absoluteClientRectangle.Y,
				_relativeClientRectangle.X,
				_relativeClientRectangle.Height);
			_rightRectangle = new Rectangle(
				_absoluteClientRectangle.Right,
				_absoluteClientRectangle.Y,
				_windowRectangle.Right - _absoluteClientRectangle.Right,
				_relativeClientRectangle.Height);
			_topRectangle = new Rectangle(
				_absoluteClientRectangle.X,
				_windowRectangle.Y,
				_relativeClientRectangle.Width,
				_relativeClientRectangle.Y);
			_bottomRectangle = new Rectangle(
				_absoluteClientRectangle.X,
				_absoluteClientRectangle.Bottom,
				_relativeClientRectangle.Width,
				_windowRectangle.Bottom - _absoluteClientRectangle.Bottom);
			_centerRectangle = _absoluteClientRectangle;
		}

		public Rectangle AbsoluteClientRectangle
		{
			get
			{
				return _absoluteClientRectangle;
			}
		}

		public Rectangle RelativeClientRectangle
		{
			get
			{
				return _relativeClientRectangle;
			}
		}

		public Rectangle WindowRectangle
		{
			get
			{
				return _windowRectangle;
			}
		}

		public Rectangle BottomLeftCornerRectangle
		{
			get
			{
				return _bottomLeftCornerRectangle;
			}
		}

		public Rectangle BottomRectangle
		{
			get
			{
				return _bottomRectangle;
			}
		}

		public Rectangle BottomRightCornerRectangle
		{
			get
			{
				return _bottomRightCornerRectangle;
			}
		}

		public Rectangle CenterRectangle
		{
			get
			{
				return _centerRectangle;
			}
		}

		public Rectangle LeftRectangle
		{
			get
			{
				return _leftRectangle;
			}
		}

		public Rectangle RightRectangle
		{
			get
			{
				return _rightRectangle;
			}
		}

		public Rectangle TopLeftCornerRectangle
		{
			get
			{
				return _topLeftCornerRectangle;
			}
		}

		public Rectangle TopRectangle
		{
			get
			{
				return _topRectangle;
			}
		}

		public Rectangle TopRightCornerRectangle
		{
			get
			{
				return _topRightCornerRectangle;
			}
		}
	}
}