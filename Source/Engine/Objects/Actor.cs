using System;

using Junior.Common;

namespace TextAdventure.Engine.Objects
{
	public class Actor : IUnique, INamedObject, IDescribedObject
	{
		private readonly Guid _id;
		private Character _character;
		private string _description;
		private string _name;

		public Actor(
			Guid id,
			string name,
			string description,
			Character character)
		{
			name.ThrowIfNull("name");
			description.ThrowIfNull("description");
			character.ThrowIfNull("character");

			_id = id;
			Name = name;
			Description = description;
			Character = character;
		}

		public Character Character
		{
			get
			{
				return _character;
			}
			protected internal set
			{
				value.ThrowIfNull("value");

				_character = value;
			}
		}

		public string Description
		{
			get
			{
				return _description;
			}
			protected internal set
			{
				value.ThrowIfNull("value");

				_description = value;
			}
		}

		public string Name
		{
			get
			{
				return _name;
			}
			protected internal set
			{
				value.ThrowIfNull("value");

				_name = value;
			}
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