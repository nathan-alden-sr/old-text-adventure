using Junior.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TextAdventure.Editor.Renderers
{
	public class RendererParameters
	{
		private readonly SpriteBatch _spriteBatch;
		private readonly TextureContent _textureContent;
		private readonly Rectangle _viewportRectangle;

		public RendererParameters(SpriteBatch spriteBatch, Rectangle viewportRectangle, TextureContent textureContent)
		{
			spriteBatch.ThrowIfNull("spriteBatch");
			textureContent.ThrowIfNull("textureContent");

			_spriteBatch = spriteBatch;
			_viewportRectangle = viewportRectangle;
			_textureContent = textureContent;
		}

		public SpriteBatch SpriteBatch
		{
			get
			{
				return _spriteBatch;
			}
		}

		public TextureContent TextureContent
		{
			get
			{
				return _textureContent;
			}
		}

		public Rectangle ViewportRectangle
		{
			get
			{
				return _viewportRectangle;
			}
		}
	}
}