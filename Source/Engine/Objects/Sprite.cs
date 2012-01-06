using TextAdventure.Engine.Common;

namespace TextAdventure.Engine.Objects
{
	public class Sprite : Tile, ISprite
	{
		public Sprite(
			Coordinate coordinate,
			Character character)
			: base(coordinate, character)
		{
		}
	}
}