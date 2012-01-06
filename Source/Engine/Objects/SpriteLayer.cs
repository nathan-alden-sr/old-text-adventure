using System.Collections.Generic;

using Junior.Common;

using TextAdventure.Engine.Common;

namespace TextAdventure.Engine.Objects
{
	public class SpriteLayer : Layer<Sprite>, ISpriteLayer
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

		IEnumerable<ITile> ILayer.Tiles
		{
			get
			{
				return Sprites;
			}
		}

		ISprite ILayer<ISprite>.this[int x, int y]
		{
			get
			{
				return this[x, y];
			}
		}

		ISprite ILayer<ISprite>.this[Coordinate coordinate]
		{
			get
			{
				return this[coordinate];
			}
		}

		IEnumerable<ISprite> ISpriteLayer.Sprites
		{
			get
			{
				return Sprites;
			}
		}
	}
}