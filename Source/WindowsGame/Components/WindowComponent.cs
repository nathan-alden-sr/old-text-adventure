using System;

using Microsoft.Xna.Framework;

using TextAdventure.WindowsGame.Helpers;
using TextAdventure.WindowsGame.Managers;

namespace TextAdventure.WindowsGame.Components
{
	public abstract class WindowComponent : TextAdventureDrawableGameComponent
	{
		private float _alpha = 1f;

		protected WindowComponent(GameManager gameManager)
			: base(gameManager)
		{
			BackgroundColor = Color.Transparent;
		}

		protected Window Window
		{
			get;
			private set;
		}

		protected float Alpha
		{
			get
			{
				return _alpha;
			}
			set
			{
				_alpha = MathHelper.Clamp(value, 0f, 1f);
			}
		}

		protected Color BackgroundColor
		{
			get;
			set;
		}

		public override void Draw(GameTime gameTime)
		{
			SpriteBatch.Begin();

			SpriteBatch.Draw(TextureContent.Pixel, Window.WindowRectangle, BackgroundColor * _alpha);

			SpriteBatch.End();

			base.Draw(gameTime);
		}

		public void SetWindowRectangle(Rectangle windowRectangle, Padding padding)
		{
			Window = new Window(windowRectangle, padding);
		}

		public void SetWindowRectangle(int x, int y, int width, int height, Padding padding)
		{
			Window = new Window(new Rectangle(x, y, width, height), padding);
		}

		public void SetWindowRectangleUsingWindowLocationAndClientSize(int windowX, int windowY, int clientWidth, int clientHeight, Padding padding)
		{
			Window = new Window(new Rectangle(windowX, windowY, clientWidth + padding.Left + padding.Right, clientHeight + padding.Top + padding.Bottom), padding);
		}

		public void SetWindowRectangleUsingClientLocationAndClientSize(int clientX, int clientY, int clientWidth, int clientHeight, Padding padding)
		{
			Window = new Window(new Rectangle(clientX - padding.Left, clientY - padding.Top, clientWidth + padding.Left + padding.Right, clientHeight + padding.Top + padding.Bottom), padding);
		}

		public void SetWindowRectangleUsingWindowSize(Alignment alignment, int windowWidth, int windowHeight, Padding padding)
		{
			Rectangle windowDestinationRectangle = DrawingConstants.GameWindow.DestinationRectangle;
			Point windowCenterPoint = windowDestinationRectangle.Center;
			Rectangle rectangle;

			switch (alignment)
			{
				case Alignment.TopLeft:
					rectangle = new Rectangle(0, 0, windowWidth, windowHeight);
					break;
				case Alignment.TopCenter:
					rectangle = new Rectangle(windowCenterPoint.X - (windowWidth / 2), 0, windowWidth, windowHeight);
					break;
				case Alignment.TopRight:
					rectangle = new Rectangle(windowDestinationRectangle.Right - windowWidth, 0, windowWidth, windowHeight);
					break;
				case Alignment.RightCenter:
					rectangle = new Rectangle(windowDestinationRectangle.Right - windowWidth, windowCenterPoint.Y - (windowHeight / 2), windowWidth, windowHeight);
					break;
				case Alignment.BottomRight:
					rectangle = new Rectangle(windowDestinationRectangle.Right - windowWidth, windowDestinationRectangle.Bottom - windowHeight, windowWidth, windowHeight);
					break;
				case Alignment.BottomCenter:
					rectangle = new Rectangle(windowCenterPoint.X - (windowWidth / 2), windowDestinationRectangle.Bottom - windowHeight, windowWidth, windowHeight);
					break;
				case Alignment.BottomLeft:
					rectangle = new Rectangle(0, windowDestinationRectangle.Bottom - windowHeight, windowWidth, windowHeight);
					break;
				case Alignment.LeftCenter:
					rectangle = new Rectangle(0, windowCenterPoint.Y - (windowHeight / 2), windowWidth, windowHeight);
					break;
				case Alignment.Center:
					rectangle = new Rectangle(windowCenterPoint.X - (windowWidth / 2), windowCenterPoint.Y - (windowHeight / 2), windowWidth, windowHeight);
					break;
				default:
					throw new ArgumentOutOfRangeException("alignment");
			}

			Window = new Window(rectangle, padding);
		}

		public void SetWindowRectangleUsingClientSize(Alignment alignment, int clientWidth, int clientHeight, Padding padding)
		{
			SetWindowRectangleUsingWindowSize(alignment, clientWidth + padding.Left + padding.Right, clientHeight + padding.Top + padding.Bottom, padding);
		}
	}
}