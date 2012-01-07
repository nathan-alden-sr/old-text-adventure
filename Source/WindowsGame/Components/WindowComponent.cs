using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TextAdventure.WindowsGame.Managers;
using TextAdventure.WindowsGame.Windows;

namespace TextAdventure.WindowsGame.Components
{
	public abstract class WindowComponent : WindowComponentBase
	{
		protected WindowComponent(GameManager gameManager)
			: base(gameManager)
		{
			Window = new Window(Rectangle.Empty);
		}

		protected Window Window
		{
			get;
			private set;
		}

		public override void Draw(GameTime gameTime)
		{
			SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);

			SpriteBatch.Draw(TextureContent.Pixel, Window.WindowRectangle, BackgroundColor * Alpha);

			SpriteBatch.End();

			base.Draw(gameTime);
		}

		protected void SetWindowRectangle(Rectangle windowRectangle)
		{
			Window = new Window(windowRectangle);
		}

		protected void SetWindowRectangle(int x, int y, int width, int height)
		{
			Window = new Window(new Rectangle(x, y, width, height));
		}

		protected void SetWindowRectangle(WindowAlignment alignment, int width, int height)
		{
			Rectangle windowDestinationRectangle = DrawingConstants.GameWindow.DestinationRectangle;
			Point windowCenterPoint = windowDestinationRectangle.Center;
			Rectangle windowRectangle;

			switch (alignment)
			{
				case WindowAlignment.TopLeft:
					windowRectangle = new Rectangle(0, 0, width, height);
					break;
				case WindowAlignment.TopCenter:
					windowRectangle = new Rectangle(windowCenterPoint.X - (width / 2), 0, width, height);
					break;
				case WindowAlignment.TopRight:
					windowRectangle = new Rectangle(windowDestinationRectangle.Right - width, 0, width, height);
					break;
				case WindowAlignment.RightCenter:
					windowRectangle = new Rectangle(windowDestinationRectangle.Right - width, windowCenterPoint.Y - (height / 2), width, height);
					break;
				case WindowAlignment.BottomRight:
					windowRectangle = new Rectangle(windowDestinationRectangle.Right - width, windowDestinationRectangle.Bottom - height, width, height);
					break;
				case WindowAlignment.BottomCenter:
					windowRectangle = new Rectangle(windowCenterPoint.X - (width / 2), windowDestinationRectangle.Bottom - height, width, height);
					break;
				case WindowAlignment.BottomLeft:
					windowRectangle = new Rectangle(0, windowDestinationRectangle.Bottom - height, width, height);
					break;
				case WindowAlignment.LeftCenter:
					windowRectangle = new Rectangle(0, windowCenterPoint.Y - (height / 2), width, height);
					break;
				case WindowAlignment.Center:
					windowRectangle = new Rectangle(windowCenterPoint.X - (width / 2), windowCenterPoint.Y - (height / 2), width, height);
					break;
				default:
					throw new ArgumentOutOfRangeException("alignment");
			}

			Window = new Window(windowRectangle);
		}

		protected void SetWindowRectangle(WindowHorizontalAlignment alignment, int y, int width, int height)
		{
			Rectangle windowDestinationRectangle = DrawingConstants.GameWindow.DestinationRectangle;
			Point windowCenterPoint = windowDestinationRectangle.Center;
			Rectangle windowRectangle;

			switch (alignment)
			{
				case WindowHorizontalAlignment.Left:
					windowRectangle = new Rectangle(0, y, width, height);
					break;
				case WindowHorizontalAlignment.Center:
					windowRectangle = new Rectangle(windowCenterPoint.X - (width / 2), y, width, height);
					break;
				case WindowHorizontalAlignment.Right:
					windowRectangle = new Rectangle(windowDestinationRectangle.Right - width, y, width, height);
					break;
				default:
					throw new ArgumentOutOfRangeException("alignment");
			}

			Window = new Window(windowRectangle);
		}

		protected void SetWindowRectangle(WindowVerticalAlignment alignment, int x, int width, int height)
		{
			Rectangle windowDestinationRectangle = DrawingConstants.GameWindow.DestinationRectangle;
			Point windowCenterPoint = windowDestinationRectangle.Center;
			Rectangle windowRectangle;

			switch (alignment)
			{
				case WindowVerticalAlignment.Top:
					windowRectangle = new Rectangle(x, 0, width, height);
					break;
				case WindowVerticalAlignment.Center:
					windowRectangle = new Rectangle(x, windowCenterPoint.Y - (height / 2), width, height);
					break;
				case WindowVerticalAlignment.Bottom:
					windowRectangle = new Rectangle(x, windowDestinationRectangle.Bottom - height, width, height);
					break;
				default:
					throw new ArgumentOutOfRangeException("alignment");
			}

			Window = new Window(windowRectangle);
		}
	}
}