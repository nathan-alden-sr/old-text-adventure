using System;
using System.Collections.Generic;

using Junior.Common;

using TextAdventure.Engine.Common;

namespace TextAdventure.Engine.Objects
{
	public class SpriteLayer : Layer<Sprite>
	{
		public SpriteLayer(
			Guid boardId,
			Size size,
			params Sprite[] sprites)
			: this(boardId, size, (IEnumerable<Sprite>)sprites)
		{
		}

		public SpriteLayer(
			Guid boardId,
			Size size,
			IEnumerable<Sprite> sprites)
			: base(boardId, size)
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