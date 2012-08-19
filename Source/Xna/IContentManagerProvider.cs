using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TextAdventure.Xna
{
	public interface IContentManagerProvider
	{
		ContentManager GetContentManager(GraphicsDevice graphicsDevice);
	}
}