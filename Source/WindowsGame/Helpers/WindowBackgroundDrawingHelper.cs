using Junior.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TextAdventure.WindowsGame.States;
using TextAdventure.WindowsGame.Windows;

namespace TextAdventure.WindowsGame.Helpers
{
	public class WindowBackgroundDrawingHelper
	{
		private readonly WindowTexture _windowTexture;
		private float _alpha = 1f;

		public WindowBackgroundDrawingHelper(WindowTexture windowTexture)
		{
			_windowTexture = windowTexture;
			BackgroundColor = Color.White;
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

		public Color BackgroundColor
		{
			get;
			set;
		}

		public void Draw(SpriteBatch spriteBatch, Rectangle? scissorRectangle = null, Matrix? translationMatrix = null)
		{
			spriteBatch.ThrowIfNull("spriteBatch");

			if (_windowTexture == null)
			{
				return;
			}

			RasterizerState rasterizerState = scissorRectangle != null ? new ScissoringRasterizerState(Window.WindowRectangle) : RasterizerState.CullNone;

			if (scissorRectangle != null)
			{
				spriteBatch.GraphicsDevice.ScissorRectangle = scissorRectangle.Value;
			}

			if (scissorRectangle != null)
			{
				spriteBatch.GraphicsDevice.ScissorRectangle = scissorRectangle.Value;
			}

			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.None, rasterizerState, null, translationMatrix ?? Matrix.Identity);

			Color backgroundColor = BackgroundColor * _alpha;

			spriteBatch.Draw(_windowTexture.Texture, Window.TopLeftCornerRectangle, _windowTexture.BackgroundTopLeftRectangle, backgroundColor);
			spriteBatch.Draw(_windowTexture.Texture, Window.TopRightCornerRectangle, _windowTexture.BackgroundTopRightRectangle, backgroundColor);
			spriteBatch.Draw(_windowTexture.Texture, Window.BottomLeftCornerRectangle, _windowTexture.BackgroundBottomLeftRectangle, backgroundColor);
			spriteBatch.Draw(_windowTexture.Texture, Window.BottomRightCornerRectangle, _windowTexture.BackgroundBottomRightRectangle, backgroundColor);
			spriteBatch.Draw(_windowTexture.Texture, Window.LeftRectangle, _windowTexture.BackgroundLeftRectangle, backgroundColor);
			spriteBatch.Draw(_windowTexture.Texture, Window.RightRectangle, _windowTexture.BackgroundRightRectangle, backgroundColor);
			spriteBatch.Draw(_windowTexture.Texture, Window.TopRectangle, _windowTexture.BackgroundTopRectangle, backgroundColor);
			spriteBatch.Draw(_windowTexture.Texture, Window.BottomRectangle, _windowTexture.BackgroundBottomRectangle, backgroundColor);
			spriteBatch.Draw(_windowTexture.Texture, Window.CenterRectangle, _windowTexture.BackgroundCenterRectangle, backgroundColor);

			spriteBatch.End();
		}
	}
}