using System.Collections.ObjectModel;
using System.Linq;

using Junior.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TextAdventure.Editor.Renderers
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

		public void Render(SpriteBatch spriteBatch, Rectangle viewRectangle, TextureContent textureContent)
		{
			spriteBatch.ThrowIfNull("spriteBatch");
			textureContent.ThrowIfNull("textureContent");

			// Must call ToArray() because collection could be modified during iteration
			foreach (IRenderer renderer in _renderers.ToArray())
			{
				var parameters = new RendererParameters(spriteBatch, viewRectangle, textureContent);

				renderer.Render(parameters);
			}
		}
	}
}