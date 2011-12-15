using System;

using Junior.Common;

namespace TextAdventure.Engine.Objects
{
	public class Actor : IUnique
	{
		private readonly Guid _id;
		private Character _character;

		public Actor(
			Guid id,
			Character character,
			bool allowPlayerOverlap)
		{
			character.ThrowIfNull("character");

			_id = id;
			Character = character;
			AllowPlayerOverlap = allowPlayerOverlap;
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

		public bool AllowPlayerOverlap
		{
			get;
			set;
		}
		public Guid Id
		{
			get
			{
				return _id;
			}
		}
	}
}