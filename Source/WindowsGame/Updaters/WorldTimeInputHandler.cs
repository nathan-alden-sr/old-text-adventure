using Junior.Common;

using Microsoft.Xna.Framework.Input;

using TextAdventure.WindowsGame.Helpers;
using TextAdventure.WindowsGame.RendererStates;

namespace TextAdventure.WindowsGame.Updaters
{
	public class WorldTimeInputHandler : IUpdater
	{
		private readonly KeyboardStateHelper _keyboardStateHelper;
		private readonly WorldTimeRendererState _worldTimeRendererState;

		public WorldTimeInputHandler(WorldTimeRendererState worldTimeRendererState)
		{
			worldTimeRendererState.ThrowIfNull("worldTimeRenderer");

			_worldTimeRendererState = worldTimeRendererState;
			_keyboardStateHelper = new KeyboardStateHelper(
				KeyDown,
				null,
				null,
				Constants.WorldTimeRenderer.Input.PauseKey,
				Constants.WorldTimeRenderer.Input.FasterKey,
				Constants.WorldTimeRenderer.Input.SlowerKey);
		}

		public void Update(IUpdaterParameters parameters)
		{
			parameters.ThrowIfNull("parameters");

			_keyboardStateHelper.Update();
		}

		private void KeyDown(KeyboardState keyboardState, Keys keys)
		{
			switch (keys)
			{
				case Constants.WorldTimeRenderer.Input.PauseKey:
					if (_worldTimeRendererState.Paused)
					{
						_worldTimeRendererState.Unpause();
					}
					else
					{
						_worldTimeRendererState.Pause();
					}
					break;
				case Constants.WorldTimeRenderer.Input.FasterKey:
					_worldTimeRendererState.SpeedUp();
					break;
				case Constants.WorldTimeRenderer.Input.SlowerKey:
					_worldTimeRendererState.SlowDown();
					break;
			}
		}
	}
}