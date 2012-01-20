using System.Collections.ObjectModel;

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

			foreach (IRenderer renderer in _renderers)
			{
				var parameters = new RenderParameters(spriteBatch, viewRectangle, textureContent);

				renderer.Render(parameters);
			}
		}

		private class RenderParameters : IRendererParameters
		{
			private readonly SpriteBatch _spriteBatch;
			private readonly TextureContent _textureContent;
			private readonly Rectangle _viewportRectangle;

			public RenderParameters(SpriteBatch spriteBatch, Rectangle viewportRectangle, TextureContent textureContent)
			{
				_spriteBatch = spriteBatch;
				_viewportRectangle = viewportRectangle;
				_textureContent = textureContent;
				Origin = Vector2.Zero;
				TransformMatrix = Matrix.Identity;
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

			public Rectangle? ScissorRectangle
			{
				get;
				set;
			}

			public Vector2 Origin
			{
				get;
				set;
			}

			public Effect Effect
			{
				get;
				set;
			}

			public Matrix TransformMatrix
			{
				get;
				set;
			}
		}
	}
}