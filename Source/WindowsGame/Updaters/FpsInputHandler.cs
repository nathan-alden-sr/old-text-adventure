using Junior.Common;

using Microsoft.Xna.Framework.Input;

using TextAdventure.WindowsGame.Extensions;
using TextAdventure.WindowsGame.Helpers;
using TextAdventure.WindowsGame.RendererStates;

namespace TextAdventure.WindowsGame.Updaters
{
	public class FpsInputHandler : IUpdater
	{
		private readonly FpsRendererState _fpsRendererState;
		private readonly KeyboardStateHelper _keyboardStateHelper;

		public FpsInputHandler(FpsRendererState fpsRendererState)
		{
			fpsRendererState.ThrowIfNull("fpsRenderer");

			_fpsRendererState = fpsRendererState;
			_keyboardStateHelper = new KeyboardStateHelper(KeyDown, null, null, Constants.FpsRenderer.Input.VisibilityToggleKeysSets);
		}

		public void Update(IUpdaterParameters parameters)
		{
			parameters.ThrowIfNull("parameters");

			_keyboardStateHelper.Update();
		}

		private void KeyDown(KeyboardState keyboardState, Keys keys)
		{
			if (keyboardState.IsSetOfKeysDown(Constants.FpsRenderer.Input.VisibilityToggleKeysSets))
			{
				_fpsRendererState.Visible = !_fpsRendererState.Visible;
			}
		}
	}
}