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
		Rectangle? ScissorRectangle
		{
			get;
			set;
		}
		Vector2 Origin
		{
			get;
			set;
		}
		Effect Effect
		{
			get;
			set;
		}
		Matrix TransformMatrix
		{
			get;
			set;
		}
	}
}