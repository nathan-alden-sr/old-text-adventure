using System;

using Junior.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using TextAdventure.Engine.Game.Commands;
using TextAdventure.Engine.Game.World;
using TextAdventure.WindowsGame.Helpers;
using TextAdventure.WindowsGame.Managers;

namespace TextAdventure.WindowsGame.Components
{
	public class PlayerComponent : TextAdventureDrawableGameComponent
	{
		private readonly KeyboardRepeatHelper _keyboardRepeatHelper = new KeyboardRepeatHelper();
		private readonly KeyboardStateHelper _keyboardStateHelper;
		private readonly IWorldInstance _worldInstance;
		private TimeSpan _lastKeyDownTotalWorldTime = TimeSpan.Zero;

		public PlayerComponent(GameManager gameManager, IWorldInstance worldInstance)
			: base(gameManager)
		{
			worldInstance.ThrowIfNull("worldInstance");

			_worldInstance = worldInstance;
			_keyboardStateHelper = new KeyboardStateHelper(_keyboardRepeatHelper, Keys.Up, Keys.Down, Keys.Left, Keys.Right);

			DrawOrder = ComponentDrawOrder.Player;
		}

		public override void Update(GameTime gameTime)
		{
			if (!_worldInstance.WorldTime.Paused && Focused)
			{
				_keyboardRepeatHelper.InitialInterval = TimeSpan.FromMilliseconds(250 / _worldInstance.WorldTime.Speed);
				_keyboardRepeatHelper.RepeatingInterval = TimeSpan.FromMilliseconds(30 / _worldInstance.WorldTime.Speed);

				_keyboardStateHelper.Update();

				if (_keyboardRepeatHelper.UpdateRequired(gameTime))
				{
					PlayerMoveCommand command = null;
					TimeSpan totalWorldTime = _worldInstance.WorldTime.Total;

					if (totalWorldTime - _lastKeyDownTotalWorldTime >= _keyboardRepeatHelper.RepeatingInterval)
					{
						switch (_keyboardStateHelper.LastKeyDown)
						{
							case Keys.Up:
								command = new PlayerMoveUpCommand();
								break;
							case Keys.Down:
								command = new PlayerMoveDownCommand();
								break;
							case Keys.Left:
								command = new PlayerMoveLeftCommand();
								break;
							case Keys.Right:
								command = new PlayerMoveRightCommand();
								break;
						}

						_lastKeyDownTotalWorldTime = totalWorldTime;
						_worldInstance.CurrentCommandQueue.EnqueueCommand(command);
					}
				}
			}

			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime)
		{
			SpriteBatch.Begin();

			SpriteBatch.Draw(TextureContent.Pixel, DrawingConstants.Player.DestinationRectangle, TextureContent.Pixel.Bounds, Color.DarkBlue);
			SpriteBatch.Draw(TextureContent.Characters, DrawingConstants.Player.DestinationRectangle, DrawingConstants.Player.TextureRectangle, Color.White);

			SpriteBatch.End();

			base.Draw(gameTime);
		}
	}
}