using System;

using Junior.Common;

using TextAdventure.Engine.Game.Commands;
using TextAdventure.Engine.Game.World;
using TextAdventure.WindowsGame.Helpers;

namespace TextAdventure.WindowsGame.Updaters
{
	public class PlayerInputHandler : IUpdater
	{
		private readonly KeyboardRepeatHelper _keyboardRepeatHelper = new KeyboardRepeatHelper();
		private readonly KeyboardStateHelper _keyboardStateHelper;
		private readonly WorldInstance _worldInstance;
		private TimeSpan _lastKeyDownTotalWorldTime = TimeSpan.Zero;

		public PlayerInputHandler(WorldInstance worldInstance)
		{
			worldInstance.ThrowIfNull("worldInstance");

			_worldInstance = worldInstance;
			_keyboardStateHelper = new KeyboardStateHelper(
				_keyboardRepeatHelper,
				Constants.PlayerRenderer.Input.MoveUpKey,
				Constants.PlayerRenderer.Input.MoveDownKey,
				Constants.PlayerRenderer.Input.MoveLeftKey,
				Constants.PlayerRenderer.Input.MoveRightKey);
		}

		public void Update(IUpdaterParameters parameters)
		{
			parameters.ThrowIfNull("parameters");

			if (_worldInstance.WorldTime.Paused || parameters.Focus != Focus.Player || _worldInstance.PlayerInput.Suspended)
			{
				return;
			}

			_keyboardRepeatHelper.InitialInterval = TimeSpan.FromMilliseconds(Constants.PlayerRenderer.Input.InitialInterval.TotalMilliseconds / _worldInstance.WorldTime.Speed);
			_keyboardRepeatHelper.RepeatingInterval = TimeSpan.FromMilliseconds(Constants.PlayerRenderer.Input.RepeatingInterval.TotalMilliseconds / _worldInstance.WorldTime.Speed);

			_keyboardStateHelper.Update();

			TimeSpan totalWorldTime = _worldInstance.WorldTime.Total;

			if (!_keyboardRepeatHelper.IntervalElapsed(totalWorldTime) || totalWorldTime - _lastKeyDownTotalWorldTime < _keyboardRepeatHelper.RepeatingInterval)
			{
				return;
			}

			PlayerMoveCommand command = null;

			switch (_keyboardStateHelper.LastKeyDown)
			{
				case Constants.PlayerRenderer.Input.MoveUpKey:
					command = new PlayerMoveUpCommand();
					break;
				case Constants.PlayerRenderer.Input.MoveDownKey:
					command = new PlayerMoveDownCommand();
					break;
				case Constants.PlayerRenderer.Input.MoveLeftKey:
					command = new PlayerMoveLeftCommand();
					break;
				case Constants.PlayerRenderer.Input.MoveRightKey:
					command = new PlayerMoveRightCommand();
					break;
			}

			_lastKeyDownTotalWorldTime = totalWorldTime;
			_worldInstance.CurrentCommandQueue.EnqueueCommand(command);
		}
	}
}