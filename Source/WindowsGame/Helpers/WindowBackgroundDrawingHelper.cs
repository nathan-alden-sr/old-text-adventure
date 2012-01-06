using Junior.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

		public Window Window
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

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.ThrowIfNull("spriteBatch");

			if (_windowTexture == null)
			{
				return;
			}

			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
			spriteBatch.GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;

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