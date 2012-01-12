using System.Collections.Generic;
using System.Linq;

using TextAdventure.WindowsGame.Updaters;

namespace TextAdventure.WindowsGame.Managers
{
	public class InputManager
	{
		private readonly Stack<Focus> _focusStack = new Stack<Focus>();

		public Focus Focus
		{
			get
			{
				return _focusStack.Any() ? _focusStack.Peek() : Focus.Global;
			}
		}

		public void ClaimFocus(Focus focus)
		{
			if (_focusStack.Count == 0 || _focusStack.Peek() != focus)
			{
				_focusStack.Push(focus);
			}
		}

		public void RelinquishFocus()
		{
			if (_focusStack.Any())
			{
				_focusStack.Pop();
			}
		}
	}
}