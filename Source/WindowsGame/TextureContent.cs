using Junior.Common;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TextAdventure.WindowsGame
{
	public class TextureContent
	{
		private readonly Texture2D _characters;
		private readonly Texture2D _gameBackground;
		private readonly Texture2D _pixel;
		private readonly WindowTextureContent _windows;

		public TextureContent(ContentManager contentManager)
		{
			contentManager.ThrowIfNull("contentManager");

			_pixel = contentManager.Load<Texture2D>(@"Textures\Pixel");
			_characters = contentManager.Load<Texture2D>(@"Textures\Characters");
			_gameBackground = contentManager.Load<Texture2D>(@"Textures\Game Background");
			_windows = new WindowTextureContent(contentManager);
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

		public Texture2D GameBackground
		{
			get
			{
				return _gameBackground;
			}
		}

		public WindowTextureContent Windows
		{
			get
			{
				return _windows;
			}
		}

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
}