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

		public void Update(UpdaterParameters parameters)
		{
			parameters.ThrowIfNull("parameters");

			if (_worldInstance.WorldTime.Paused || _worldInstance.PlayerInput.Suspended || parameters.Focus != Focus.Player)
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
					command = Commands.PlayerMove(MoveDirection.Up);
					break;
				case Constants.PlayerRenderer.Input.MoveDownKey:
					command = Commands.PlayerMove(MoveDirection.Down);
					break;
				case Constants.PlayerRenderer.Input.MoveLeftKey:
					command = Commands.PlayerMove(MoveDirection.Left);
					break;
				case Constants.PlayerRenderer.Input.MoveRightKey:
					command = Commands.PlayerMove(MoveDirection.Right);
					break;
			}

			_lastKeyDownTotalWorldTime = totalWorldTime;
			_worldInstance.CurrentCommandQueue.EnqueueCommand(command);
		}
	}
}