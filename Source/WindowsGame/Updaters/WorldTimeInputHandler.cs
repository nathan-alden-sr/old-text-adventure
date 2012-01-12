using System.Linq;

using Junior.Common;

using Microsoft.Xna.Framework.Input;

using TextAdventure.WindowsGame.Extensions;
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
				Constants.WorldTimeRenderer.Input.VisibilityToggleKeysSets.Concat(
					new[]
						{
							new[] { Constants.WorldTimeRenderer.Input.PauseKey },
							new[] { Constants.WorldTimeRenderer.Input.FasterKey },
							new[] { Constants.WorldTimeRenderer.Input.SlowerKey }
						}));
		}

		public void Update(IUpdaterParameters parameters)
		{
			parameters.ThrowIfNull("parameters");

			_keyboardStateHelper.Update();
		}

		private void KeyDown(KeyboardState keyboardState, Keys keys)
		{
			if (keyboardState.IsSetOfKeysDown(Constants.WorldTimeRenderer.Input.VisibilityToggleKeysSets))
			{
				_worldTimeRendererState.Visible = !_worldTimeRendererState.Visible;
			}
			else
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
						_worldTimeRendererState.Speed *= 2;
						_worldTimeRendererState.Unpause();
						break;
					case Constants.WorldTimeRenderer.Input.SlowerKey:
						if (new FloatToInt(_worldTimeRendererState.Speed) == new FloatToInt(Constants.WorldTimeRenderer.MinimumSpeedFactor))
						{
							_worldTimeRendererState.Pause();
						}
						else
						{
							_worldTimeRendererState.Speed /= 2;
						}
						break;
				}
			}
		}
	}
}