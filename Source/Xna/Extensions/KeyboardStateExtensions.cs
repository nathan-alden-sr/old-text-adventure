using System.Collections.Generic;
using System.Linq;

using Junior.Common;

using Microsoft.Xna.Framework.Input;

namespace TextAdventure.Xna.Extensions
{
	public static class KeyboardStateExtensions
	{
		public static bool AreAllKeysDown(this KeyboardState keyboardState, IEnumerable<Keys> keys)
		{
			keyboardState.ThrowIfNull("keyboardState");
			keys.ThrowIfNull("keys");

			return keys.All(keyboardState.IsKeyDown);
		}

		public static bool AreAllKeysDown(this KeyboardState keyboardState, params Keys[] keys)
		{
			keyboardState.ThrowIfNull("keyboardState");
			keys.ThrowIfNull("keys");

			return AreAllKeysDown(keyboardState, (IEnumerable<Keys>)keys);
		}

		public static bool AreAllKeysUp(this KeyboardState keyboardState, IEnumerable<Keys> keys)
		{
			keyboardState.ThrowIfNull("keyboardState");
			keys.ThrowIfNull("keys");

			return keys.All(keyboardState.IsKeyUp);
		}

		public static bool AreAllKeysUp(this KeyboardState keyboardState, params Keys[] keys)
		{
			keyboardState.ThrowIfNull("keyboardState");
			keys.ThrowIfNull("keys");

			return AreAllKeysUp(keyboardState, (IEnumerable<Keys>)keys);
		}

		public static bool IsSetOfKeysDown(this KeyboardState keyboardState, IEnumerable<IEnumerable<Keys>> setsOfKeys)
		{
			keyboardState.ThrowIfNull("keyboardState");
			setsOfKeys.ThrowIfNull("setsOfKeys");

			return setsOfKeys.Any(arg => AreAllKeysDown(keyboardState, arg));
		}

		public static bool IsSetOfKeysDown(this KeyboardState keyboardState, params Keys[][] setsOfKeys)
		{
			keyboardState.ThrowIfNull("keyboardState");
			setsOfKeys.ThrowIfNull("setsOfKeys");

			return IsSetOfKeysDown(keyboardState, (IEnumerable<IEnumerable<Keys>>)setsOfKeys);
		}

		public static bool IsSetOfKeysUp(this KeyboardState keyboardState, IEnumerable<IEnumerable<Keys>> setsOfKeys)
		{
			keyboardState.ThrowIfNull("keyboardState");
			setsOfKeys.ThrowIfNull("setsOfKeys");

			return setsOfKeys.Any(arg => AreAllKeysUp(keyboardState, arg));
		}

		public static bool IsSetOfKeysUp(this KeyboardState keyboardState, params Keys[][] setsOfKeys)
		{
			keyboardState.ThrowIfNull("keyboardState");
			setsOfKeys.ThrowIfNull("setsOfKeys");

			return IsSetOfKeysUp(keyboardState, (IEnumerable<IEnumerable<Keys>>)setsOfKeys);
		}
	}
}