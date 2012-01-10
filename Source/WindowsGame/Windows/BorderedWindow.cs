using Microsoft.Xna.Framework;

namespace TextAdventure.WindowsGame.Windows
{
	public class BorderedWindow : Window
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

		public BorderedWindow(Rectangle windowRectangle, Padding padding)
			: base(windowRectangle)
		{
			Rectangle absoluteClientRectangle = windowRectangle;

			absoluteClientRectangle.X += padding.Left;
			absoluteClientRectangle.Y += padding.Top;
			absoluteClientRectangle.Width -= padding.X;
			absoluteClientRectangle.Height -= padding.Y;

			_absoluteClientRectangle = absoluteClientRectangle;
			_relativeClientRectangle = new Rectangle(padding.Left, padding.Top, windowRectangle.Width - padding.X, windowRectangle.Height - padding.Y);
			_topLeftCornerRectangle = new Rectangle(
				WindowRectangle.X,
				WindowRectangle.Y,
				_relativeClientRectangle.X,
				_relativeClientRectangle.Y);
			_topRightCornerRectangle = new Rectangle(
				_absoluteClientRectangle.Right,
				WindowRectangle.Y,
				WindowRectangle.Right - _absoluteClientRectangle.Right,
				_relativeClientRectangle.Y);
			_bottomLeftCornerRectangle = new Rectangle(
				WindowRectangle.X,
				_absoluteClientRectangle.Bottom,
				_relativeClientRectangle.X,
				WindowRectangle.Bottom - _absoluteClientRectangle.Bottom);
			_bottomRightCornerRectangle = new Rectangle(
				_absoluteClientRectangle.Right,
				_absoluteClientRectangle.Bottom,
				WindowRectangle.Right - _absoluteClientRectangle.Right,
				WindowRectangle.Bottom - _absoluteClientRectangle.Bottom);
			_leftRectangle = new Rectangle(
				WindowRectangle.X,
				_absoluteClientRectangle.Y,
				_relativeClientRectangle.X,
				_relativeClientRectangle.Height);
			_rightRectangle = new Rectangle(
				_absoluteClientRectangle.Right,
				_absoluteClientRectangle.Y,
				WindowRectangle.Right - _absoluteClientRectangle.Right,
				_relativeClientRectangle.Height);
			_topRectangle = new Rectangle(
				_absoluteClientRectangle.X,
				WindowRectangle.Y,
				_relativeClientRectangle.Width,
				_relativeClientRectangle.Y);
			_bottomRectangle = new Rectangle(
				_absoluteClientRectangle.X,
				_absoluteClientRectangle.Bottom,
				_relativeClientRectangle.Width,
				WindowRectangle.Bottom - _absoluteClientRectangle.Bottom);
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