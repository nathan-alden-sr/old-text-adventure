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
				return _inputFocusStack.Peek();
			}
		}

		public void ClaimFocus(IGameComponent gameComponent)
		{
			gameComponent.ThrowIfNull("gameComponent");

			_inputFocusStack.Push(gameComponent);
		}

		public void RelinquishFocus(IGameComponent gameComponent)
		{
			gameComponent.ThrowIfNull("gameComponent");

			_inputFocusStack.Pop();
		}
	}
}