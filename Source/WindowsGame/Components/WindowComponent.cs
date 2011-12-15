using System;

using Microsoft.Xna.Framework;

using TextAdventure.WindowsGame.Helpers;
using TextAdventure.WindowsGame.Managers;

namespace TextAdventure.WindowsGame.Components
{
	public abstract class WindowComponent : TextAdventureDrawableGameComponent
	{
		protected WindowComponent(GameManager gameManager)
			: base(gameManager)
		{
			BackgroundColor = Color.Transparent;
		}

		protected Rectangle WindowRectangle
		{
			get;
			private set;
		}

		protected Rectangle ClientRectangle
		{
			get;
			private set;
		}

		public int Padding
		{
			get;
			private set;
		}

		protected Color BackgroundColor
		{
			get;
			set;
		}

		public override void Draw(GameTime gameTime)
		{
			SpriteBatch.Begin();

			SpriteBatch.Draw(TextureContent.Pixel, WindowRectangle, BackgroundColor);

			SpriteBatch.End();

			base.Draw(gameTime);
		}

		public void SetWindowRectangleUsingWindowSize(int x, int y, int windowWidth, int windowHeight, int padding = DrawingConstants.Window.Padding)
		{
			WindowRectangle = new Rectangle(x, y, windowWidth, windowHeight);

			Rectangle newClientRectangle = WindowRectangle;

			newClientRectangle.Inflate(-padding, -padding);

			ClientRectangle = newClientRectangle;
			Padding = padding;
		}

		public void SetWindowRectangleUsingClientSize(int x, int y, int clientWidth, int clientHeight, int padding = DrawingConstants.Window.Padding)
		{
			SetWindowRectangleUsingWindowSize(x, y, clientWidth + (padding * 2), clientHeight + (padding * 2));
		}

		public void SetWindowRectangleUsingWindowSize(Alignment alignment, int windowWidth, int windowHeight, int padding = DrawingConstants.Window.Padding)
		{
			Rectangle windowDestinationRectangle = DrawingConstants.GameWindow.DestinationRectangle;
			Point windowCenterPoint = windowDestinationRectangle.Center;
			Rectangle newWindowRectangle;

			switch (alignment)
			{
				case Alignment.TopLeft:
					newWindowRectangle = new Rectangle(0, 0, windowWidth, windowHeight);
					break;
				case Alignment.TopCenter:
					newWindowRectangle = new Rectangle(windowCenterPoint.X - (windowWidth / 2), 0, windowWidth, windowHeight);
					break;
				case Alignment.TopRight:
					newWindowRectangle = new Rectangle(windowDestinationRectangle.Right - windowWidth, 0, windowWidth, windowHeight);
					break;
				case Alignment.RightCenter:
					newWindowRectangle = new Rectangle(windowDestinationRectangle.Right - windowWidth, windowCenterPoint.Y - (windowHeight / 2), windowWidth, windowHeight);
					break;
				case Alignment.BottomRight:
					newWindowRectangle = new Rectangle(windowDestinationRectangle.Right - windowWidth, windowDestinationRectangle.Bottom - windowHeight, windowWidth, windowHeight);
					break;
				case Alignment.BottomCenter:
					newWindowRectangle = new Rectangle(windowCenterPoint.X - (windowWidth / 2), windowDestinationRectangle.Bottom - windowHeight, windowWidth, windowHeight);
					break;
				case Alignment.BottomLeft:
					newWindowRectangle = new Rectangle(0, windowDestinationRectangle.Bottom - windowHeight, windowWidth, windowHeight);
					break;
				case Alignment.LeftCenter:
					newWindowRectangle = new Rectangle(0, windowCenterPoint.Y - (windowHeight / 2), windowWidth, windowHeight);
					break;
				case Alignment.Center:
					newWindowRectangle = new Rectangle(windowCenterPoint.X - (windowWidth / 2), windowCenterPoint.Y - (windowHeight / 2), windowWidth, windowHeight);
					break;
				default:
					throw new ArgumentOutOfRangeException("alignment");
			}

			SetWindowRectangleUsingWindowSize(newWindowRectangle.X, newWindowRectangle.Y, newWindowRectangle.Width, newWindowRectangle.Height);
		}

		public void SetWindowRectangleUsingClientSize(Alignment alignment, int clientWidth, int clientHeight, int padding = DrawingConstants.Window.Padding)
		{
			SetWindowRectangleUsingWindowSize(alignment, clientWidth + (padding * 2), clientHeight + (padding * 2), padding);
		}
	}
}