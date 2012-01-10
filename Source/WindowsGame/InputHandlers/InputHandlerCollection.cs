using System.Collections.ObjectModel;
using System.Linq;

using Junior.Common;

using Microsoft.Xna.Framework;

namespace TextAdventure.WindowsGame.InputHandlers
{
	public class InputHandlerCollection
	{
		private readonly Collection<IInputHandler> _inputHandlers = new Collection<IInputHandler>();

		public void Add(IInputHandler inputHandler)
		{
			inputHandler.ThrowIfNull("inputHandler");

			_inputHandlers.Add(inputHandler);
		}

		public void Remove(IInputHandler inputHandler)
		{
			inputHandler.ThrowIfNull("inputHandler");

			_inputHandlers.Remove(inputHandler);
		}

		public void Update(GameTime gameTime, Focus focus)
		{
			foreach (IInputHandler inputHandler in _inputHandlers.ToArray())
			{
				inputHandler.Update(gameTime, focus);
			}
		}
	}
}