using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TextAdventure.WindowsGame
{
	public class WindowTextureContent
	{
		private readonly WindowTexture _glow1;
		private readonly WindowTexture _innerBevel1;

		public WindowTextureContent(ContentManager contentManager)
		{
			_innerBevel1 = new WindowTexture(contentManager.Load<Texture2D>(@"Textures\Windows\Inner Bevel 1"), 8, 8);
			_glow1 = new WindowTexture(contentManager.Load<Texture2D>(@"Textures\Windows\Glow 1"), 4, 4);
		}

		public WindowTexture InnerBevel1
		{
			get
			{
				return _innerBevel1;
			}
		}

		public WindowTexture Glow1
		{
			get
			{
				return _glow1;
			}
		}
	}
}