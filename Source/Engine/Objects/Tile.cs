using Junior.Common;

using TextAdventure.Engine.Common;

namespace TextAdventure.Engine.Objects
{
	public class Tile
	{
		private Character _character;

		public Tile(
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
			get
			{
				return _character;
			}
			set
			{
				value.ThrowIfNull("value");

				_character = value;
			}
		}
	}
}