using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TextAdventure.WindowsGame.Xna;

namespace TextAdventure.WindowsGame.Renderers
{
	public interface IRendererParameters
	{
		XnaGameTime GameTime
		{
			get;
		}
		SpriteBatch SpriteBatch
		{
			get;
		}
		FontContent FontContent
		{
			get;
		}
		TextureContent TextureContent
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