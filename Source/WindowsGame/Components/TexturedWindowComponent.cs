using System;

using Junior.Common;

using Microsoft.Xna.Framework;

using TextAdventure.WindowsGame.Helpers;
using TextAdventure.WindowsGame.Managers;
using TextAdventure.WindowsGame.Windows;

namespace TextAdventure.WindowsGame.Components
{
	public abstract class TexturedWindowComponent : BorderedWindowComponent
	{
		private readonly Padding _padding;
		private readonly WindowBackgroundDrawingHelper _windowBackgroundDrawingHelper;
		private readonly WindowBorderDrawingHelper _windowBorderDrawingHelper;

		protected TexturedWindowComponent(GameManager gameManager, Func<TextureContent, WindowTexture> windowTextureDelegate)
			: base(gameManager)
		{
			windowTextureDelegate.ThrowIfNull("windowTextureDelegate");

			WindowTexture windowTexture = windowTextureDelegate(TextureContent);

			if (windowTexture == null)
			{
				throw new ArgumentException("Must provide a WindowTexture instance.", "windowTextureDelegate");
			}

			_padding = new Padding(windowTexture.SpriteWidth, windowTexture.SpriteHeight);
			_windowBackgroundDrawingHelper = new WindowBackgroundDrawingHelper(windowTexture);
			_windowBorderDrawingHelper = new WindowBorderDrawingHelper(windowTexture);
			BorderColor = Color.White;
		}

		protected Color BorderColor
		{
			get;
			set;
		}

		public override void Draw(GameTime gameTime)
		{
			_windowBackgroundDrawingHelper.Draw(SpriteBatch);
			_windowBorderDrawingHelper.Draw(SpriteBatch);

			base.Draw(gameTime);
		}

		public override void Update(GameTime gameTime)
		{
			_windowBackgroundDrawingHelper.Alpha = Alpha;
			_windowBackgroundDrawingHelper.BackgroundColor = BackgroundColor;
			_windowBackgroundDrawingHelper.Window = Window;

			_windowBorderDrawingHelper.Alpha = Alpha;
			_windowBorderDrawingHelper.BorderColor = BorderColor;
			_windowBorderDrawingHelper.Window = Window;

			base.Update(gameTime);
		}

		protected void SetWindowRectangle(Rectangle windowRectangle)
		{
			SetWindowRectangle(windowRectangle, _padding);
		}

		protected void SetWindowRectangle(int x, int y, int width, int height)
		{
			SetWindowRectangle(x, y, width, height, _padding);
		}

		protected void SetWindowRectangleUsingWindowLocationAndClientSize(int windowX, int windowY, int clientWidth, int clientHeight)
		{
			SetWindowRectangleUsingWindowLocationAndClientSize(windowX, windowY, clientWidth, clientHeight, _padding);
		}

		protected void SetWindowRectangleUsingClientLocationAndClientSize(int clientX, int clientY, int clientWidth, int clientHeight)
		{
			SetWindowRectangleUsingClientLocationAndClientSize(clientX, clientY, clientWidth, clientHeight, _padding);
		}

		protected void SetWindowRectangleUsingWindowSize(WindowAlignment alignment, int windowWidth, int windowHeight)
		{
			SetWindowRectangle(alignment, windowWidth, windowHeight, _padding);
		}

		protected void SetWindowRectangleUsingClientSize(WindowAlignment alignment, int clientWidth, int clientHeight)
		{
			SetWindowRectangleUsingClientSize(alignment, clientWidth, clientHeight, _padding);
		}

		protected void SetWindowRectangleUsingWindowYAndWindowSize(WindowHorizontalAlignment alignment, int windowY, int windowWidth, int windowHeight)
		{
			SetWindowRectangleUsingWindowYAndWindowSize(alignment, windowY, windowWidth, windowHeight, _padding);
		}

		protected void SetWindowRectangleUsingWindowXAndWindowSize(WindowVerticalAlignment alignment, int windowX, int windowWidth, int windowHeight)
		{
			SetWindowRectangleUsingWindowXAndWindowSize(alignment, windowX, windowWidth, windowHeight, _padding);
		}

		protected void SetWindowRectangleUsingWindowYAndClientSize(WindowHorizontalAlignment alignment, int windowY, int clientWidth, int clientHeight)
		{
			SetWindowRectangleUsingWindowYAndClientSize(alignment, windowY, clientWidth, clientHeight, _padding);
		}

		protected void SetWindowRectangleUsingWindowXAndClientSize(WindowVerticalAlignment alignment, int windowX, int clientWidth, int clientHeight)
		{
			SetWindowRectangleUsingWindowXAndClientSize(alignment, windowX, clientWidth, clientHeight, _padding);
		}
	}
}