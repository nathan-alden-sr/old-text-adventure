using Junior.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using TextAdventure.WindowsGame.Extensions;
using TextAdventure.WindowsGame.Helpers;
using TextAdventure.WindowsGame.RendererStates;

namespace TextAdventure.WindowsGame.InputHandlers
{
	public class LogInputHandler : IInputHandler
	{
		private readonly KeyboardStateHelper _keyboardStateHelper;
		private readonly LogRendererState _logRendererState;

		public LogInputHandler(LogRendererState logRendererState)
		{
			logRendererState.ThrowIfNull("logRendererState");

			_logRendererState = logRendererState;
			_keyboardStateHelper = new KeyboardStateHelper(KeyDown, null, null, Constants.LogRenderer.Input.VisibilityToggleKeysSets);
		}

		public void Update(GameTime gameTime, Focus focus)
		{
			gameTime.ThrowIfNull("gameTime");

			_keyboardStateHelper.Update();
		}

		private void KeyDown(KeyboardState keyboardState, Keys keys)
		{
			if (keyboardState.IsSetOfKeysDown(Constants.LogRenderer.Input.VisibilityToggleKeysSets))
			{
				_logRendererState.Visible = !_logRendererState.Visible;
			}
		}
	}
}