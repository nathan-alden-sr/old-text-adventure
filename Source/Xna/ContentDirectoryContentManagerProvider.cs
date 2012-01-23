using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TextAdventure.Xna
{
	public class ContentDirectoryContentManagerProvider : IContentManagerProvider
	{
		public ContentManager GetContentManager(GraphicsDevice graphicsDevice)
		{
			return new ContentManager(new XnaServiceProvider(graphicsDevice))
			       	{
			       		RootDirectory = "Content"
			       	};
		}
	}
}