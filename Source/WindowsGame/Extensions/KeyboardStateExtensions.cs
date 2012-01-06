using System.Linq;

using Junior.Common;

using Microsoft.Xna.Framework.Input;

namespace TextAdventure.WindowsGame.Extensions
{
	public static class KeyboardStateExtensions
	{
		public static bool AreKeysDown(this KeyboardState keyboardState, params Keys[] keys)
		{
			keyboardState.ThrowIfNull("keyboardState");
			keys.ThrowIfNull("keys");

			return keys.All(keyboardState.IsKeyDown);
		}

		public static bool AreKeysUp(this KeyboardState keyboardState, params Keys[] keys)
		{
			keyboardState.ThrowIfNull("keyboardState");
			keys.ThrowIfNull("keys");

			return keys.All(keyboardState.IsKeyUp);
		}
	}
}