using System;

using Junior.Common;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Game.Events;

namespace TextAdventure.Engine.Objects
{
	public class ActorInstance : Sprite, IUnique, INamedObject, IDescribedObject
	{
		private readonly Guid _actorId;
		private readonly IEventHandler<ActorInstanceCreatedEvent> _actorInstanceCreatedEventHandler;
		private readonly IEventHandler<ActorInstanceDestroyedEvent> _actorInstanceDestroyedEventHandler;
		private readonly IEventHandler<ActorInstanceMovedEvent> _actorInstanceMovedEventHandler;
		private readonly IEventHandler<ActorInstanceTouchedActorInstanceEvent> _actorInstanceTouchedActorInstanceEventHandler;
		private readonly Guid _id;
		private readonly IEventHandler<PlayerTouchedActorInstanceEvent> _playerTouchedActorInstanceEventHandler;
		private string _description;
		private string _name;

		public ActorInstance(
			Guid id,
			string name,
			string description,
			Guid actorId,
			Coordinate coordinate,
			Character character,
			IEventHandler<ActorInstanceCreatedEvent> actorInstanceCreatedEventHandler = null,
			IEventHandler<ActorInstanceDestroyedEvent> actorInstanceDestroyedEventHandler = null,
			IEventHandler<ActorInstanceTouchedActorInstanceEvent> actorInstanceTouchedActorInstanceEventHandler = null,
			IEventHandler<PlayerTouchedActorInstanceEvent> playerTouchedActorInstanceEventHandler = null,
			IEventHandler<ActorInstanceMovedEvent> actorInstanceMovedEventHandler = null)
			: base(coordinate, character)
		{
			name.ThrowIfNull("name");
			description.ThrowIfNull("description");

			_id = id;
			Name = name;
			Description = description;
			_actorId = actorId;
			_actorInstanceCreatedEventHandler = actorInstanceCreatedEventHandler;
			_actorInstanceDestroyedEventHandler = actorInstanceDestroyedEventHandler;
			_actorInstanceTouchedActorInstanceEventHandler = actorInstanceTouchedActorInstanceEventHandler;
			_playerTouchedActorInstanceEventHandler = playerTouchedActorInstanceEventHandler;
			_actorInstanceMovedEventHandler = actorInstanceMovedEventHandler;
		}

		public Guid ActorId
		{
			get
			{
				return _actorId;
			}
		}

		public IEventHandler<ActorInstanceCreatedEvent> ActorInstanceCreatedEventHandler
		{
			get
			{
				return _actorInstanceCreatedEventHandler;
			}
		}

		public IEventHandler<ActorInstanceDestroyedEvent> ActorInstanceDestroyedEventHandler
		{
			get
			{
				return _actorInstanceDestroyedEventHandler;
			}
		}

		public IEventHandler<ActorInstanceTouchedActorInstanceEvent> ActorInstanceTouchedActorInstanceEventHandler
		{
			get
			{
				return _actorInstanceTouchedActorInstanceEventHandler;
			}
		}

		public IEventHandler<PlayerTouchedActorInstanceEvent> PlayerTouchedActorInstanceEventHandler
		{
			get
			{
				return _playerTouchedActorInstanceEventHandler;
			}
		}

		public IEventHandler<ActorInstanceMovedEvent> ActorInstanceMovedEventHandler
		{
			get
			{
				return _actorInstanceMovedEventHandler;
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

		protected internal bool ChangeCoordinate(Board board, Player player, Coordinate newCoordinate)
		{
			board.ThrowIfNull("board");
			player.ThrowIfNull("player");

			if (board.ActorInstanceLayer[Coordinate] != this)
			{
				throw new ArgumentException("Board's actor instance layer does not contain this actor instance.", "board");
			}

			ActorInstance destinationActorInstance = board.ActorInstanceLayer[newCoordinate];
			Sprite foregroundSprite = board.ForegroundLayer[newCoordinate];

			if (destinationActorInstance != null || foregroundSprite != null || player.Coordinate == newCoordinate)
			{
				return destinationActorInstance == this;
			}

			board.ActorInstanceLayer.MoveTile(Coordinate, newCoordinate);

			Coordinate = newCoordinate;

			return true;
		}
	}
}