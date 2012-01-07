using Junior.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TextAdventure.WindowsGame.States;
using TextAdventure.WindowsGame.Windows;

namespace TextAdventure.WindowsGame.Helpers
{
	public class WindowBorderDrawingHelper
	{
		private readonly Vector2 _borderSize;
		private readonly WindowTexture _windowTexture;
		private float _alpha = 1f;

		public WindowBorderDrawingHelper(WindowTexture windowTexture, bool beginAndEndSpriteBatch = true)
		{
			windowTexture.ThrowIfNull("windowTexture");

			_windowTexture = windowTexture;
			_borderSize = new Vector2(windowTexture.SpriteWidth, windowTexture.SpriteHeight);
			BorderColor = Color.White;
		}

		public Vector2 BorderSize
		{
			get
			{
				return _borderSize;
			}
		}

		public BorderedWindow Window
		{
			get;
			set;
		}

		public float Alpha
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

		public Color BorderColor
		{
			get;
			set;
		}

		public void Draw(SpriteBatch spriteBatch, Rectangle? scissorRectangle = null, Matrix? translationMatrix = null)
		{
			spriteBatch.ThrowIfNull("spriteBatch");

			RasterizerState rasterizerState = scissorRectangle != null ? new ScissoringRasterizerState(Window.WindowRectangle) : RasterizerState.CullNone;

			if (scissorRectangle != null)
			{
				spriteBatch.GraphicsDevice.ScissorRectangle = scissorRectangle.Value;
			}

			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, rasterizerState, null, translationMatrix ?? Matrix.Identity);

			Color borderColor = BorderColor * _alpha;

			spriteBatch.Draw(_windowTexture.Texture, Window.TopLeftCornerRectangle, _windowTexture.BorderTopLeftRectangle, borderColor);
			spriteBatch.Draw(_windowTexture.Texture, Window.TopRightCornerRectangle, _windowTexture.BorderTopRightRectangle, borderColor);
			spriteBatch.Draw(_windowTexture.Texture, Window.BottomLeftCornerRectangle, _windowTexture.BorderBottomLeftRectangle, borderColor);
			spriteBatch.Draw(_windowTexture.Texture, Window.BottomRightCornerRectangle, _windowTexture.BorderBottomRightRectangle, borderColor);
			spriteBatch.Draw(_windowTexture.Texture, Window.LeftRectangle, _windowTexture.BorderTextureLeftRectangle, borderColor);
			spriteBatch.Draw(_windowTexture.Texture, Window.RightRectangle, _windowTexture.BorderRightRectangle, borderColor);
			spriteBatch.Draw(_windowTexture.Texture, Window.TopRectangle, _windowTexture.BorderTopRectangle, borderColor);
			spriteBatch.Draw(_windowTexture.Texture, Window.BottomRectangle, _windowTexture.BorderBottomRectangle, borderColor);

			spriteBatch.End();
		}
	}
}