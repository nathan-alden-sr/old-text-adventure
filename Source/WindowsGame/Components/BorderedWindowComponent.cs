using System;

using Microsoft.Xna.Framework;

using TextAdventure.WindowsGame.Helpers;
using TextAdventure.WindowsGame.Managers;

namespace TextAdventure.WindowsGame.Components
{
	public abstract class BorderedWindowComponent : TextAdventureDrawableGameComponent
	{
		private readonly WindowBackgroundDrawingHelper _windowBackgroundDrawingHelper = new WindowBackgroundDrawingHelper();
		private readonly WindowBorderDrawingHelper _windowBorderDrawingHelper = new WindowBorderDrawingHelper();
		private Color _backgroundColor;
		private Color _borderColor;
		private Rectangle _windowRectangle;

		protected BorderedWindowComponent(GameManager gameManager)
			: base(gameManager)
		{
			BackgroundColor = Color.Transparent;
			BorderColor = Color.White;
		}

		protected Rectangle WindowRectangle
		{
			get
			{
				return _windowRectangle;
			}
			set
			{
				value.Width = Math.Max(value.Width, DrawingConstants.Window.TextureSpriteWidth * 2);
				value.Height = Math.Max(value.Height, DrawingConstants.Window.TextureSpriteHeight * 2);

				ClientRectangle = new Rectangle(
					DrawingConstants.Window.TextureSpriteWidth,
					DrawingConstants.Window.TextureSpriteHeight,
					value.Width - (DrawingConstants.Window.TextureSpriteWidth * 2),
					value.Height - (DrawingConstants.Window.TextureSpriteHeight * 2));
				_windowBorderDrawingHelper.WindowRectangle = value;
				_windowRectangle = value;
			}
		}

		protected Rectangle ClientRectangle
		{
			get;
			private set;
		}

		protected Color BackgroundColor
		{
			get
			{
				return _backgroundColor;
			}
			set
			{
				_windowBackgroundDrawingHelper.BackgroundColor = value;
				_backgroundColor = value;
			}
		}

		protected Color BorderColor
		{
			get
			{
				return _borderColor;
			}
			set
			{
				_windowBorderDrawingHelper.BorderColor = value;
				_borderColor = value;
			}
		}

		public override void Draw(GameTime gameTime)
		{
			_windowBackgroundDrawingHelper.Draw(SpriteBatch);
			_windowBorderDrawingHelper.Draw(SpriteBatch);

			base.Draw(gameTime);
		}

		public void SetWindowRectangle(int x, int y, int clientWidth, int clientHeight)
		{
			int totalWidth = clientWidth + WindowBorderDrawingHelper.BorderSizeTwice.Width;
			int totalHeight = clientHeight + WindowBorderDrawingHelper.BorderSizeTwice.Height;

			WindowRectangle = new Rectangle(x, y, totalWidth, totalHeight);
		}

		public void SetWindowRectangle(Alignment alignment, int clientWidth, int clientHeight)
		{
			SetWindowRectangle(alignment, Point.Zero, clientWidth, clientHeight);
		}

		public void SetWindowRectangle(Alignment alignment, Point alignmentOffset, int clientWidth, int clientHeight)
		{
			int totalWidth = clientWidth + WindowBorderDrawingHelper.BorderSizeTwice.Width;
			int totalHeight = clientHeight + WindowBorderDrawingHelper.BorderSizeTwice.Height;
			Rectangle windowDestinationRectangle = DrawingConstants.GameWindow.DestinationRectangle;
			Point windowCenterPoint = windowDestinationRectangle.Center;
			Rectangle newWindowRectangle;

			switch (alignment)
			{
				case Alignment.TopLeft:
					newWindowRectangle = new Rectangle(0, 0, totalWidth, totalHeight);
					break;
				case Alignment.TopCenter:
					newWindowRectangle = new Rectangle(windowCenterPoint.X - (totalWidth / 2), 0, totalWidth, totalHeight);
					break;
				case Alignment.TopRight:
					newWindowRectangle = new Rectangle(windowDestinationRectangle.Right - totalWidth, 0, totalWidth, totalHeight);
					break;
				case Alignment.RightCenter:
					newWindowRectangle = new Rectangle(windowDestinationRectangle.Right - totalWidth, windowCenterPoint.Y - (totalHeight / 2), totalWidth, totalHeight);
					break;
				case Alignment.BottomRight:
					newWindowRectangle = new Rectangle(windowDestinationRectangle.Right - totalWidth, windowDestinationRectangle.Bottom - totalHeight, totalWidth, totalHeight);
					break;
				case Alignment.BottomCenter:
					newWindowRectangle = new Rectangle(windowCenterPoint.X - (totalWidth / 2), windowDestinationRectangle.Bottom - totalHeight, totalWidth, totalHeight);
					break;
				case Alignment.BottomLeft:
					newWindowRectangle = new Rectangle(0, windowDestinationRectangle.Bottom - totalHeight, totalWidth, totalHeight);
					break;
				case Alignment.LeftCenter:
					newWindowRectangle = new Rectangle(0, windowCenterPoint.Y - (totalHeight / 2), totalWidth, totalHeight);
					break;
				case Alignment.Center:
					newWindowRectangle = new Rectangle(windowCenterPoint.X - (totalWidth / 2), windowCenterPoint.Y - (totalHeight / 2), totalWidth, totalHeight);
					break;
				default:
					throw new ArgumentOutOfRangeException("alignment");
			}

			newWindowRectangle.Offset(alignmentOffset);

			WindowRectangle = newWindowRectangle;
		}
	}
}