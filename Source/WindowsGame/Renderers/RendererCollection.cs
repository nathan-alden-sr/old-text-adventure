using System.Collections.ObjectModel;
using System.Linq;

using Junior.Common;

using Microsoft.Xna.Framework.Graphics;

using TextAdventure.Xna;

namespace TextAdventure.WindowsGame.Renderers
{
	public class RendererCollection
	{
		private readonly Collection<IRenderer> _renderers = new Collection<IRenderer>();

		public void Add(IRenderer renderer)
		{
			renderer.ThrowIfNull("renderer");

			_renderers.Add(renderer);
		}

		public void Remove(IRenderer renderer)
		{
			renderer.ThrowIfNull("renderer");

			_renderers.Remove(renderer);
		}

		public void Render(SpriteBatch spriteBatch, IXnaGameTime gameTime, FontContent fontContent, TextureContent textureContent)
		{
			spriteBatch.ThrowIfNull("spriteBatch");
			gameTime.ThrowIfNull("gameTime");
			fontContent.ThrowIfNull("fontContent");
			textureContent.ThrowIfNull("textureContent");

			var parameters = new RendererParameters(gameTime, spriteBatch, fontContent, textureContent);

			// Must call ToArray() because collection could be modified during iteration
			foreach (IRenderer renderer in _renderers.ToArray())
			{
				renderer.Render(parameters);
			}
		}
	}
}