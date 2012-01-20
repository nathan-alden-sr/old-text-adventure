using Junior.Common;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TextAdventure.Editor
{
	public class TextureContent
	{
		private readonly Texture2D _characters;
		private readonly Texture2D _hatch;
		private readonly Texture2D _pixel;

		public TextureContent(ContentManager contentManager)
		{
			contentManager.ThrowIfNull("contentManager");

			_pixel = contentManager.Load<Texture2D>(@"Textures\Pixel");
			_characters = contentManager.Load<Texture2D>(@"Textures\Characters");
			_hatch = contentManager.Load<Texture2D>(@"Textures\Hatch");
		}

		public Texture2D Pixel
		{
			get
			{
				return _pixel;
			}
		}

		public Texture2D Characters
		{
			get
			{
				return _characters;
			}
		}

		public Texture2D Hatch
		{
			get
			{
				return _hatch;
			}
		}
	}
}