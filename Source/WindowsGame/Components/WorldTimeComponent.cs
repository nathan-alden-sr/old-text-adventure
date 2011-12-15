using System;
using System.Configuration;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using TextAdventure.Engine.Game.World;
using TextAdventure.WindowsGame.Configuration;
using TextAdventure.WindowsGame.Helpers;
using TextAdventure.WindowsGame.Managers;

namespace TextAdventure.WindowsGame.Components
{
	public class WorldTimeComponent : WindowComponent, IWorldTime
	{
		private const float MaximumSpeed = 8;
		private const float MinimumSpeed = 1 / 8f;
		private const Keys VisibilityToggleKey = Keys.T;
		private readonly KeyboardStateHelper _keyboardStateHelper;
		private float _speed = 1f;
		private bool _visible;

		public WorldTimeComponent(GameManager gameManager)
			: base(gameManager)
		{
			BackgroundColor = Color.Black.WithAlpha(0.25f);
			Elapsed = TimeSpan.Zero;
			Total = TimeSpan.Zero;
			_keyboardStateHelper = new KeyboardStateHelper(KeyDown, null, null, Keys.LeftControl, Keys.RightControl, VisibilityToggleKey, Keys.Pause, Keys.Add, Keys.Subtract);

			var configurationSection = (TimeConfigurationSection)ConfigurationManager.GetSection("time");

			_visible = configurationSection.Visible;

			SetWindowRectangleUsingClientSize(Alignment.TopRight, (int)Math.Ceiling(FontContent.CalibriBold.MeasureString("Normal speed").X), FontContent.Calibri.LineSpacing * 3);

			UpdateOrder = ComponentUpdateOrder.WorldTime;
			DrawOrder = ComponentDrawOrder.WorldTime;
		}

		public bool Paused
		{
			get;
			private set;
		}

		public TimeSpan Elapsed
		{
			get;
			private set;
		}

		public TimeSpan Total
		{
			get;
			private set;
		}

		public float Speed
		{
			get
			{
				return _speed;
			}
			set
			{
				_speed = Math.Max(MinimumSpeed, Math.Min(MaximumSpeed, value));
			}
		}

		public void Pause()
		{
			Paused = true;
		}

		public void Unpause()
		{
			Paused = false;
		}

		public override void Update(GameTime gameTime)
		{
			_keyboardStateHelper.Update();

			if (!Paused)
			{
				TimeSpan elapsed = TimeSpan.FromTicks((long)(gameTime.ElapsedGameTime.Ticks * _speed));

				Elapsed = elapsed;
				Total += elapsed;
			}

			Visible = _visible;

			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);

			string gameTimeText = String.Format(
				"{0}:{1:00}:{2:00}.{3:0}",
				gameTime.TotalGameTime.Hours,
				gameTime.TotalGameTime.Minutes,
				gameTime.TotalGameTime.Seconds,
				gameTime.TotalGameTime.Milliseconds / 100);
			string worldTimeText = String.Format(
				"{0}:{1:00}:{2:00}.{3:0}",
				Total.Hours,
				Total.Minutes,
				Total.Seconds,
				Total.Milliseconds / 100);

			SpriteBatch.Begin();

			SpriteBatch.DrawStringWithShadow(
				FontContent.CalibriBold,
				gameTimeText,
				TextDrawingHelper.Instance.GetAlignedOrigin(FontContent.CalibriBold, gameTimeText, ClientRectangle, Alignment.TopRight).ToVector2(),
				Color.White,
				Color.Black,
				Vector2.One);

			Vector2 worldTimePosition = TextDrawingHelper.Instance.GetAlignedOrigin(FontContent.CalibriBold, worldTimeText, ClientRectangle, Alignment.TopRight).ToVector2();

			worldTimePosition.Y += FontContent.CalibriBold.LineSpacing;

			SpriteBatch.DrawStringWithShadow(
				FontContent.CalibriBold,
				worldTimeText,
				worldTimePosition,
				Paused ? Color.Yellow : Color.White,
				Color.Black,
				Vector2.One);

			string speedText;

			if (Paused)
			{
				speedText = "Paused";
			}
			else if (Speed < 1)
			{
				speedText = String.Format("1/{0} speed", (int)Math.Round(1 / Speed));
			}
			else if (Speed == 1)
			{
				speedText = "Normal speed";
			}
			else
			{
				speedText = (int)Speed + "x speed";
			}

			SpriteBatch.DrawStringWithShadow(
				FontContent.CalibriBold,
				speedText,
				TextDrawingHelper.Instance.GetAlignedOrigin(FontContent.CalibriBold, speedText, ClientRectangle, Alignment.BottomRight).ToVector2(),
				Paused ? Color.Yellow : Color.White,
				Color.Black,
				Vector2.One);

			SpriteBatch.End();
		}

		private void KeyDown(KeyboardState keyboardState, Keys keys)
		{
			_visible = keyboardState.AreKeysDown(Keys.LeftControl, VisibilityToggleKey) | keyboardState.AreKeysDown(Keys.RightControl, VisibilityToggleKey) ? !_visible : _visible;
			switch (keys)
			{
				case Keys.Pause:
					Paused = !Paused;
					break;
				case Keys.Add:
					Speed *= 2;
					Paused = false;
					break;
				case Keys.Subtract:
					if (Speed == MinimumSpeed)
					{
						Paused = true;
					}
					else
					{
						Speed /= 2;
					}
					break;
			}
		}
	}
}