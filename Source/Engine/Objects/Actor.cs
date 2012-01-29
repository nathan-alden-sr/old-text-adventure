using System;

using Junior.Common;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Game.Events;

namespace TextAdventure.Engine.Objects
{
	public class Actor : INamedObject, IDescribedObject
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

		public ActorInstance CreateActorInstance(Guid boardId, Coordinate coordinate, EventHandlerCollection eventHandlerCollection = null)
		{
			return new ActorInstance(Guid.NewGuid(), _name, _description, _id, boardId, coordinate, _character, eventHandlerCollection);
		}

		public ActorInstance CreateActorInstance(Guid boardId, string name, string description, Coordinate coordinate, EventHandlerCollection eventHandlerCollection = null)
		{
			return new ActorInstance(Guid.NewGuid(), name, description, _id, boardId, coordinate, _character, eventHandlerCollection);
		}

		public ActorInstance CreateActorInstance(Guid boardId, string name, string description, Coordinate coordinate, Character character, EventHandlerCollection eventHandlerCollection = null)
		{
			return new ActorInstance(Guid.NewGuid(), name, description, _id, boardId, coordinate, character, eventHandlerCollection);
		}

		public ActorInstance CreateActorInstance(Guid boardId, Coordinate coordinate, Character character, EventHandlerCollection eventHandlerCollection = null)
		{
			return new ActorInstance(Guid.NewGuid(), _name, _description, _id, boardId, coordinate, character, eventHandlerCollection);
		}

		public ActorInstance CreateActorInstance(Guid id, Guid boardId, Coordinate coordinate, EventHandlerCollection eventHandlerCollection = null)
		{
			return new ActorInstance(id, _name, _description, _id, boardId, coordinate, _character, eventHandlerCollection);
		}

		public ActorInstance CreateActorInstance(Guid id, Guid boardId, string name, string description, Coordinate coordinate, EventHandlerCollection eventHandlerCollection = null)
		{
			return new ActorInstance(id, name, description, _id, boardId, coordinate, _character, eventHandlerCollection);
		}

		public ActorInstance CreateActorInstance(Guid id, Guid boardId, string name, string description, Coordinate coordinate, Character character, EventHandlerCollection eventHandlerCollection = null)
		{
			return new ActorInstance(id, name, description, _id, boardId, coordinate, character, eventHandlerCollection);
		}

		public ActorInstance CreateActorInstance(Guid id, Guid boardId, Coordinate coordinate, Character character, EventHandlerCollection eventHandlerCollection = null)
		{
			return new ActorInstance(id, _name, _description, _id, boardId, coordinate, character, eventHandlerCollection);
		}
	}
}