using System;
using System.Configuration;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using TextAdventure.WindowsGame.Configuration;
using TextAdventure.WindowsGame.Helpers;
using TextAdventure.WindowsGame.Managers;

namespace TextAdventure.WindowsGame.Components
{
	public class FpsComponent : WindowComponent
	{
		private const Keys VisibilityToggleKey = Keys.F;
		private readonly KeyboardStateHelper _keyboardStateHelper;
		private double _elapsedSeconds;
		private int _frameCount;
		private int _framesPerSecond;
		private bool _visible;

		public FpsComponent(GameManager gameManager)
			: base(gameManager)
		{
			BackgroundColor = Color.Black.WithAlpha(0.25f);
			_keyboardStateHelper = new KeyboardStateHelper(KeyDown, null, null, Keys.LeftControl, Keys.RightControl, VisibilityToggleKey);

			var configurationSection = (FpsConfigurationSection)ConfigurationManager.GetSection("fps");

			_visible = configurationSection.Visible;

			SetWindowRectangleUsingClientSize(Alignment.BottomLeft, (int)Math.Ceiling(FontContent.Calibri.MeasureString("000 fps").X), FontContent.Calibri.LineSpacing);

			DrawOrder = ComponentDrawOrder.Fps;
			UpdateOrder = ComponentUpdateOrder.Fps;
		}

		public override void Update(GameTime gameTime)
		{
			_keyboardStateHelper.Update();

			_elapsedSeconds += gameTime.ElapsedGameTime.TotalSeconds;

			Visible = _visible;

			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime)
		{
			_framesPerSecond++;

			if (_elapsedSeconds > 1)
			{
				_frameCount = _framesPerSecond;
				_framesPerSecond = 0;
				_elapsedSeconds -= 1;
			}

			string text = _frameCount + " fps";
			SpriteFont spriteFont = FontContent.Calibri;

			SpriteBatch.Begin();

			SpriteBatch.DrawStringWithShadow(
				spriteFont,
				text,
				ClientRectangle.Location.ToVector2(),
				Color.White,
				Color.Black,
				Vector2.One);

			SpriteBatch.End();

			base.Draw(gameTime);
		}

		private void KeyDown(KeyboardState keyboardState, Keys keys)
		{
			_visible = keyboardState.AreKeysDown(Keys.LeftControl, VisibilityToggleKey) | keyboardState.AreKeysDown(Keys.RightControl, VisibilityToggleKey) ? !_visible : _visible;
		}
	}
}