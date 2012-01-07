using System;

using Microsoft.Xna.Framework;

using TextAdventure.WindowsGame.Managers;
using TextAdventure.WindowsGame.Windows;

namespace TextAdventure.WindowsGame.Components
{
	public abstract class BorderedWindowComponent : WindowComponentBase
	{
		protected BorderedWindowComponent(GameManager gameManager)
			: base(gameManager)
		{
			Window = new BorderedWindow(Rectangle.Empty, Padding.None);
		}

		protected BorderedWindow Window
		{
			get;
			private set;
		}

		protected void SetWindowRectangle(Rectangle windowRectangle, Padding padding)
		{
			Window = new BorderedWindow(windowRectangle, padding);
		}

		protected void SetWindowRectangle(int x, int y, int width, int height, Padding padding)
		{
			Window = new BorderedWindow(new Rectangle(x, y, width, height), padding);
		}

		protected void SetWindowRectangleUsingWindowLocationAndClientSize(int windowX, int windowY, int clientWidth, int clientHeight, Padding padding)
		{
			Window = new BorderedWindow(new Rectangle(windowX, windowY, clientWidth + padding.Left + padding.Right, clientHeight + padding.Top + padding.Bottom), padding);
		}

		protected void SetWindowRectangleUsingClientLocationAndClientSize(int clientX, int clientY, int clientWidth, int clientHeight, Padding padding)
		{
			Window = new BorderedWindow(new Rectangle(clientX - padding.Left, clientY - padding.Top, clientWidth + padding.Left + padding.Right, clientHeight + padding.Top + padding.Bottom), padding);
		}

		protected void SetWindowRectangle(WindowAlignment alignment, int windowWidth, int windowHeight, Padding padding)
		{
			Rectangle windowDestinationRectangle = DrawingConstants.GameWindow.DestinationRectangle;
			Point windowCenterPoint = windowDestinationRectangle.Center;
			Rectangle rectangle;

			switch (alignment)
			{
				case WindowAlignment.TopLeft:
					rectangle = new Rectangle(0, 0, windowWidth, windowHeight);
					break;
				case WindowAlignment.TopCenter:
					rectangle = new Rectangle(windowCenterPoint.X - (windowWidth / 2), 0, windowWidth, windowHeight);
					break;
				case WindowAlignment.TopRight:
					rectangle = new Rectangle(windowDestinationRectangle.Right - windowWidth, 0, windowWidth, windowHeight);
					break;
				case WindowAlignment.RightCenter:
					rectangle = new Rectangle(windowDestinationRectangle.Right - windowWidth, windowCenterPoint.Y - (windowHeight / 2), windowWidth, windowHeight);
					break;
				case WindowAlignment.BottomRight:
					rectangle = new Rectangle(windowDestinationRectangle.Right - windowWidth, windowDestinationRectangle.Bottom - windowHeight, windowWidth, windowHeight);
					break;
				case WindowAlignment.BottomCenter:
					rectangle = new Rectangle(windowCenterPoint.X - (windowWidth / 2), windowDestinationRectangle.Bottom - windowHeight, windowWidth, windowHeight);
					break;
				case WindowAlignment.BottomLeft:
					rectangle = new Rectangle(0, windowDestinationRectangle.Bottom - windowHeight, windowWidth, windowHeight);
					break;
				case WindowAlignment.LeftCenter:
					rectangle = new Rectangle(0, windowCenterPoint.Y - (windowHeight / 2), windowWidth, windowHeight);
					break;
				case WindowAlignment.Center:
					rectangle = new Rectangle(windowCenterPoint.X - (windowWidth / 2), windowCenterPoint.Y - (windowHeight / 2), windowWidth, windowHeight);
					break;
				default:
					throw new ArgumentOutOfRangeException("alignment");
			}

			Window = new BorderedWindow(rectangle, padding);
		}

		protected void SetWindowRectangleUsingClientSize(WindowAlignment alignment, int clientWidth, int clientHeight, Padding padding)
		{
			SetWindowRectangle(alignment, clientWidth + padding.Left + padding.Right, clientHeight + padding.Top + padding.Bottom, padding);
		}

		protected void SetWindowRectangleUsingWindowYAndWindowSize(WindowHorizontalAlignment alignment, int windowY, int windowWidth, int windowHeight, Padding padding)
		{
			Rectangle windowDestinationRectangle = DrawingConstants.GameWindow.DestinationRectangle;
			Point windowCenterPoint = windowDestinationRectangle.Center;
			Rectangle windowRectangle;

			switch (alignment)
			{
				case WindowHorizontalAlignment.Left:
					windowRectangle = new Rectangle(0, windowY, windowWidth, windowHeight);
					break;
				case WindowHorizontalAlignment.Center:
					windowRectangle = new Rectangle(windowCenterPoint.X - (windowWidth / 2), windowY, windowWidth, windowHeight);
					break;
				case WindowHorizontalAlignment.Right:
					windowRectangle = new Rectangle(windowDestinationRectangle.Right - windowWidth, windowY, windowWidth, windowHeight);
					break;
				default:
					throw new ArgumentOutOfRangeException("alignment");
			}

			Window = new BorderedWindow(windowRectangle, padding);
		}

		protected void SetWindowRectangleUsingWindowXAndWindowSize(WindowVerticalAlignment alignment, int windowX, int windowWidth, int windowHeight, Padding padding)
		{
			Rectangle windowDestinationRectangle = DrawingConstants.GameWindow.DestinationRectangle;
			Point windowCenterPoint = windowDestinationRectangle.Center;
			Rectangle windowRectangle;

			switch (alignment)
			{
				case WindowVerticalAlignment.Top:
					windowRectangle = new Rectangle(windowX, 0, windowWidth, windowHeight);
					break;
				case WindowVerticalAlignment.Center:
					windowRectangle = new Rectangle(windowX, windowCenterPoint.Y - (windowHeight / 2), windowWidth, windowHeight);
					break;
				case WindowVerticalAlignment.Bottom:
					windowRectangle = new Rectangle(windowX, windowDestinationRectangle.Bottom - windowHeight, windowWidth, windowHeight);
					break;
				default:
					throw new ArgumentOutOfRangeException("alignment");
			}

			Window = new BorderedWindow(windowRectangle, padding);
		}

		protected void SetWindowRectangleUsingWindowYAndClientSize(WindowHorizontalAlignment alignment, int windowY, int clientWidth, int clientHeight, Padding padding)
		{
			SetWindowRectangleUsingWindowYAndWindowSize(alignment, windowY, clientWidth + padding.Left + padding.Right, clientHeight + padding.Top + padding.Bottom, padding);
		}

		protected void SetWindowRectangleUsingWindowXAndClientSize(WindowVerticalAlignment alignment, int windowX, int clientWidth, int clientHeight, Padding padding)
		{
			SetWindowRectangleUsingWindowXAndWindowSize(alignment, windowX, clientWidth + padding.Left + padding.Right, clientHeight + padding.Top + padding.Bottom, padding);
		}
	}
}