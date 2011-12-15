using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework.Input;

namespace TextAdventure.WindowsGame.Helpers
{
	public class KeyboardStateHelper
	{
		private readonly Action _allKeysUpDelegate;
		private readonly Action<KeyboardState, Keys> _keyDownDelegate;
		private readonly List<Keys> _keyStack = new List<Keys>();
		private readonly Action<KeyboardState, Keys> _keyUpDelegate;
		private readonly IEnumerable<Keys> _keysToMonitor;
		private KeyboardState _oldKeyboardState;

		public KeyboardStateHelper(KeyboardRepeatHelper keyboardRepeatHelper, params Keys[] keysToMonitor)
			: this((keyboardState, keys) => keyboardRepeatHelper.Start(), null, keyboardRepeatHelper.Stop, keysToMonitor)
		{
		}

		public KeyboardStateHelper(Action<KeyboardState, Keys> keyDownDelegate = null, Action<KeyboardState, Keys> keyUpDelegate = null, Action allKeysUpDelegate = null, params Keys[] keysToMonitor)
		{
			_allKeysUpDelegate = allKeysUpDelegate;
			_keyDownDelegate = keyDownDelegate;
			_keyUpDelegate = keyUpDelegate;
			_keysToMonitor = keysToMonitor ?? Enumerable.Empty<Keys>();
			_oldKeyboardState = Keyboard.GetState();
		}

		public Keys LastKeyDown
		{
			get
			{
				return _keyStack.Count > 0 ? _keyStack[0] : Keys.None;
			}
		}

		public void Update()
		{
			KeyboardState newKeyboardState = Keyboard.GetState();

			foreach (Keys key in _keysToMonitor.Where(arg => _oldKeyboardState.IsKeyUp(arg) && newKeyboardState.IsKeyDown(arg)))
			{
				_keyStack.Insert(0, key);
				if (_keyDownDelegate != null)
				{
					_keyDownDelegate(newKeyboardState, key);
				}
			}
			foreach (Keys key in _keysToMonitor.Where(arg => _oldKeyboardState.IsKeyDown(arg) && newKeyboardState.IsKeyUp(arg)))
			{
				Keys tempKey = key;

				_keyStack.RemoveAll(arg => arg == tempKey);
				if (_keyUpDelegate != null)
				{
					_keyUpDelegate(newKeyboardState, key);
				}
			}
			if (_keysToMonitor.All(newKeyboardState.IsKeyUp) && _allKeysUpDelegate != null)
			{
				_allKeysUpDelegate();
			}

			_oldKeyboardState = newKeyboardState;
		}
	}
}