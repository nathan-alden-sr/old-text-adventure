using System.Collections.Generic;

using Junior.Common;

using TextAdventure.Engine.Common;

namespace TextAdventure.Engine.Objects
{
	public class SpriteLayer : Layer<Sprite>
	{
		public SpriteLayer(
			Size size,
			params Sprite[] sprites)
			: this(size, (IEnumerable<Sprite>)sprites)
		{
		}

		public SpriteLayer(
			Size size,
			IEnumerable<Sprite> sprites)
			: base(size)
		{
			sprites.ThrowIfNull("sprites");

			Tiles = sprites;
		}

		public IEnumerable<Sprite> Sprites
		{
			get
			{
				return Tiles;
			}
		}
	}
}