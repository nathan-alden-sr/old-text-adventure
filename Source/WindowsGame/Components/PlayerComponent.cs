using System;

using Junior.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using TextAdventure.Engine.Game.Commands;
using TextAdventure.Engine.Game.World;
using TextAdventure.WindowsGame.Helpers;
using TextAdventure.WindowsGame.Managers;

namespace TextAdventure.WindowsGame.Components
{
	public class PlayerComponent : TextAdventureDrawableGameComponent
	{
		private const int InitialIntervalInMilliseconds = 250;
		private const Keys MoveDown = Keys.Down;
		private const Keys MoveLeft = Keys.Left;
		private const Keys MoveRight = Keys.Right;
		private const Keys MoveUp = Keys.Up;
		private const int RepeatingIntervalInMilliseconds = 30;
		private readonly KeyboardRepeatHelper _keyboardRepeatHelper = new KeyboardRepeatHelper();
		private readonly KeyboardStateHelper _keyboardStateHelper;
		private readonly IWorldInstance _worldInstance;
		private TimeSpan _lastKeyDownTotalWorldTime = TimeSpan.Zero;

		public PlayerComponent(GameManager gameManager, IWorldInstance worldInstance)
			: base(gameManager)
		{
			worldInstance.ThrowIfNull("worldInstance");

			_worldInstance = worldInstance;
			_keyboardStateHelper = new KeyboardStateHelper(_keyboardRepeatHelper, MoveUp, MoveDown, MoveLeft, MoveRight);

			DrawOrder = ComponentDrawOrder.Player;
		}

		public override void Update(GameTime gameTime)
		{
			if (!_worldInstance.WorldTime.Paused && Focused)
			{
				_keyboardRepeatHelper.InitialInterval = TimeSpan.FromMilliseconds(InitialIntervalInMilliseconds / _worldInstance.WorldTime.Speed);
				_keyboardRepeatHelper.RepeatingInterval = TimeSpan.FromMilliseconds(RepeatingIntervalInMilliseconds / _worldInstance.WorldTime.Speed);

				_keyboardStateHelper.Update();

				if (_keyboardRepeatHelper.UpdateRequired(gameTime))
				{
					PlayerMoveCommand command = null;
					TimeSpan totalWorldTime = _worldInstance.WorldTime.Total;

					if (totalWorldTime - _lastKeyDownTotalWorldTime >= _keyboardRepeatHelper.RepeatingInterval)
					{
						switch (_keyboardStateHelper.LastKeyDown)
						{
							case MoveUp:
								command = new PlayerMoveUpCommand();
								break;
							case MoveDown:
								command = new PlayerMoveDownCommand();
								break;
							case MoveLeft:
								command = new PlayerMoveLeftCommand();
								break;
							case MoveRight:
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
			SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);

			SpriteBatch.Draw(TextureContent.Pixel, DrawingConstants.Player.DestinationRectangle, TextureContent.Pixel.Bounds, Color.DarkBlue);
			SpriteBatch.Draw(TextureContent.Characters, DrawingConstants.Player.DestinationRectangle, DrawingConstants.Player.TextureRectangle, Color.White);

			SpriteBatch.End();

			base.Draw(gameTime);
		}
	}
}