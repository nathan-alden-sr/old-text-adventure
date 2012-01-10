using Microsoft.Xna.Framework;

namespace TextAdventure.WindowsGame.InputHandlers
{
	public interface IInputHandler
	{
		void Update(GameTime gameTime, Focus focus);
	}
}