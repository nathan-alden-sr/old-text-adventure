using Junior.Common;

using TextAdventure.Engine.Common;

namespace TextAdventure.Engine.Objects
{
	public abstract class Tile
	{
		protected Tile(
			Coordinate coordinate,
			Character character)
		{
			character.ThrowIfNull("character");

			Coordinate = coordinate;
			Character = character;
		}

		public Coordinate Coordinate
		{
			get;
			protected set;
		}

		public Character Character
		{
			get;
			protected internal set;
		}
	}
}