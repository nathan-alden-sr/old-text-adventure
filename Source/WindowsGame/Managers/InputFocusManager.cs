using System.Collections.Generic;

using Junior.Common;

using Microsoft.Xna.Framework;

namespace TextAdventure.WindowsGame.Managers
{
	public class InputFocusManager
	{
		private readonly Stack<IGameComponent> _inputFocusStack = new Stack<IGameComponent>();

		public IGameComponent FocusedComponent
		{
			get
			{
				return _inputFocusStack.Count > 0 ? _inputFocusStack.Peek() : null;
			}
		}

		public void ClaimFocus(IGameComponent gameComponent)
		{
			gameComponent.ThrowIfNull("gameComponent");

			if (_inputFocusStack.Count == 0 || _inputFocusStack.Peek() != gameComponent)
			{
				_inputFocusStack.Push(gameComponent);
			}
		}

		public void RelinquishFocus()
		{
			if (_inputFocusStack.Count > 0)
			{
				_inputFocusStack.Pop();
			}
		}
	}
}