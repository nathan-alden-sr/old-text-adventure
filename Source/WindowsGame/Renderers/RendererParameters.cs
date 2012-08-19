using Junior.Common;

using Microsoft.Xna.Framework.Graphics;

using TextAdventure.Xna;

namespace TextAdventure.WindowsGame.Renderers
{
	public class RendererParameters
	{
		private readonly FontContent _fontContent;
		private readonly IXnaGameTime _gameTime;
		private readonly SpriteBatch _spriteBatch;
		private readonly TextureContent _textureContent;

		public RendererParameters(IXnaGameTime gameTime, SpriteBatch spriteBatch, FontContent fontContent, TextureContent textureContent)
		{
			gameTime.ThrowIfNull("gameTime");
			spriteBatch.ThrowIfNull("spriteBatch");
			fontContent.ThrowIfNull("fontContent");
			textureContent.ThrowIfNull("textureContent");

			_gameTime = gameTime;
			_spriteBatch = spriteBatch;
			_fontContent = fontContent;
			_textureContent = textureContent;
		}

		public IXnaGameTime GameTime
		{
			get
			{
				return _gameTime;
			}
		}

		public SpriteBatch SpriteBatch
		{
			get
			{
				return _spriteBatch;
			}
		}

		public FontContent FontContent
		{
			get
			{
				return _fontContent;
			}
		}

		public TextureContent TextureContent
		{
			get
			{
				return _textureContent;
			}
		}
	}
}