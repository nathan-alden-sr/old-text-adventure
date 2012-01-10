using Junior.Common;

using TextAdventure.Engine.Common;

namespace TextAdventure.Engine.Objects
{
	public abstract class Tile
	{
		private readonly Character _character;

		protected Tile(
			Coordinate coordinate,
			Character character)
		{
			character.ThrowIfNull("character");

			Coordinate = coordinate;
			_character = character;
		}

		public Coordinate Coordinate
		{
			get;
			protected set;
		}

		public Character Character
		{
			get
			{
				return _character;
			}
		}
	}
}