using System.Collections.Generic;

using Junior.Common;

using TextAdventure.Engine.Common;

namespace TextAdventure.Engine.Objects
{
	public class SpriteLayer : Layer<Sprite>
	{
		public SpriteLayer(
			Size size,
			IEnumerable<Sprite> sprites)
			: base(size)
		{
			Sprites = sprites;
		}

		public IEnumerable<Sprite> Sprites
		{
			get
			{
				return Tiles;
			}
			set
			{
				value.ThrowIfNull("value");

				Tiles = value;
			}
		}
	}
}