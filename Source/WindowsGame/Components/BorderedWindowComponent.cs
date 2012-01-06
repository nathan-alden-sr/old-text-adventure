using System;

using Junior.Common;

using Microsoft.Xna.Framework;

using TextAdventure.WindowsGame.Extensions;
using TextAdventure.WindowsGame.Helpers;
using TextAdventure.WindowsGame.Managers;

namespace TextAdventure.WindowsGame.Components
{
	public abstract class BorderedWindowComponent : TextAdventureDrawableGameComponent
	{
		private readonly int _textureSpriteHeight;
		private readonly int _textureSpriteWidth;
		private readonly WindowBackgroundDrawingHelper _windowBackgroundDrawingHelper;
		private readonly WindowBorderDrawingHelper _windowBorderDrawingHelper;
		private float _alpha = 1f;
		private Color _backgroundColor;
		private Color _borderColor;

		protected BorderedWindowComponent(GameManager gameManager, Func<TextureContent, WindowTexture> windowTextureDelegate)
			: base(gameManager)
		{
			windowTextureDelegate.ThrowIfNull("windowTextureDelegate");

			WindowTexture windowTexture = windowTextureDelegate(TextureContent);

			if (windowTexture == null)
			{
				throw new ArgumentException("Must provide a WindowTexture instance.", "windowTextureDelegate");
			}

			_textureSpriteWidth = windowTexture.SpriteWidth;
			_textureSpriteHeight = windowTexture.SpriteHeight;
			_windowBackgroundDrawingHelper = new WindowBackgroundDrawingHelper(windowTexture);
			_windowBorderDrawingHelper = new WindowBorderDrawingHelper(windowTexture);
			BackgroundColor = Color.Transparent;
			BorderColor = Color.White;
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

		public override void Update(GameTime gameTime)
		{
			_windowBackgroundDrawingHelper.Alpha = _alpha;
			_windowBorderDrawingHelper.Alpha = _alpha;

			base.Update(gameTime);
		}

		public void SetWindowRectangle(int x, int y, int width, int height)
		{
			Window = new Window(new Rectangle(x, y, width, height), new Padding(_textureSpriteWidth, _textureSpriteHeight));
			_windowBackgroundDrawingHelper.Window = Window;
			_windowBorderDrawingHelper.Window = Window;
		}

		public void SetWindowRectangleUsingClientSize(int x, int y, int clientWidth, int clientHeight)
		{
			float width = clientWidth + (_windowBorderDrawingHelper.BorderSize.X * 2);
			float height = clientHeight + (_windowBorderDrawingHelper.BorderSize.Y * 2);

			SetWindowRectangle(x, y, width.Round(), height.Round());
		}

		public void SetWindowRectangle(Alignment alignment, int clientWidth, int clientHeight)
		{
			SetWindowRectangle(alignment, Point.Zero, clientWidth, clientHeight);
		}

		public void SetWindowRectangle(Alignment alignment, Point alignmentOffset, int clientWidth, int clientHeight)
		{
			float width = clientWidth + (_windowBorderDrawingHelper.BorderSize.X * 2);
			float height = clientHeight + (_windowBorderDrawingHelper.BorderSize.Y * 2);
			Rectangle windowDestinationRectangle = DrawingConstants.GameWindow.DestinationRectangle;
			Point windowCenterPoint = windowDestinationRectangle.Center;
			Rectangle windowRectangle;

			switch (alignment)
			{
				case Alignment.TopLeft:
					windowRectangle = new Rectangle(0, 0, width.Round(), height.Round());
					break;
				case Alignment.TopCenter:
					windowRectangle = new Rectangle((windowCenterPoint.X - (width / 2)).Round(), 0, width.Round(), height.Round());
					break;
				case Alignment.TopRight:
					windowRectangle = new Rectangle((windowDestinationRectangle.Right - width).Round(), 0, width.Round(), height.Round());
					break;
				case Alignment.RightCenter:
					windowRectangle = new Rectangle((windowDestinationRectangle.Right - width).Round(), (windowCenterPoint.Y - (height / 2)).Round(), width.Round(), height.Round());
					break;
				case Alignment.BottomRight:
					windowRectangle = new Rectangle((windowDestinationRectangle.Right - width).Round(), (windowDestinationRectangle.Bottom - height).Round(), width.Round(), height.Round());
					break;
				case Alignment.BottomCenter:
					windowRectangle = new Rectangle((windowCenterPoint.X - (width / 2)).Round(), (windowDestinationRectangle.Bottom - height).Round(), width.Round(), height.Round());
					break;
				case Alignment.BottomLeft:
					windowRectangle = new Rectangle(0, (windowDestinationRectangle.Bottom - height).Round(), width.Round(), height.Round());
					break;
				case Alignment.LeftCenter:
					windowRectangle = new Rectangle(0, (windowCenterPoint.Y - (height / 2)).Round(), width.Round(), height.Round());
					break;
				case Alignment.Center:
					windowRectangle = new Rectangle((windowCenterPoint.X - (width / 2)).Round(), (windowCenterPoint.Y - (height / 2)).Round(), width.Round(), height.Round());
					break;
				default:
					throw new ArgumentOutOfRangeException("alignment");
			}

			windowRectangle.Offset(alignmentOffset);

			SetWindowRectangle(windowRectangle.X, windowRectangle.Y, windowRectangle.Width, windowRectangle.Height);
		}
	}
}