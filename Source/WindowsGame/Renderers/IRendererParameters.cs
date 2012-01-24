using Microsoft.Xna.Framework.Graphics;

using TextAdventure.Xna;

namespace TextAdventure.WindowsGame.Renderers
{
	public interface IRendererParameters
	{
		IXnaGameTime GameTime
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
	}
}