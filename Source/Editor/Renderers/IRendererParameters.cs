using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TextAdventure.Editor.Renderers
{
	public interface IRendererParameters
	{
		SpriteBatch SpriteBatch
		{
			get;
		}
		TextureContent TextureContent
		{
			get;
		}
		Rectangle ViewportRectangle
		{
			get;
		}
	}
}