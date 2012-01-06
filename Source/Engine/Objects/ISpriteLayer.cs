using System.Collections.Generic;

namespace TextAdventure.Engine.Objects
{
	public interface ISpriteLayer : ILayer<ISprite>
	{
		IEnumerable<ISprite> Sprites
		{
			get;
		}
	}
}