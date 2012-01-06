using System;

using Junior.Common;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Game.Events;

namespace TextAdventure.Engine.Objects
{
	public class ActorInstance : Sprite, IActorInstance
	{
		private readonly Guid _id;

		public ActorInstance(
			Guid id,
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
			_id = id;
			ActorId = actorId;
			ActorInstanceCreatedEventHandler = actorInstanceCreatedEventHandler;
			ActorInstanceDestroyedEventHandler = actorInstanceDestroyedEventHandler;
			ActorInstanceTouchedActorInstanceEventHandler = actorInstanceTouchedActorInstanceEventHandler;
			PlayerTouchedActorInstanceEventHandler = playerTouchedActorInstanceEventHandler;
			ActorInstanceMovedEventHandler = actorInstanceMovedEventHandler;
		}

		public Guid ActorId
		{
			get;
			set;
		}

		public IEventHandler<ActorInstanceCreatedEvent> ActorInstanceCreatedEventHandler
		{
			get;
			set;
		}

		public IEventHandler<ActorInstanceDestroyedEvent> ActorInstanceDestroyedEventHandler
		{
			get;
			set;
		}

		public IEventHandler<ActorInstanceTouchedActorInstanceEvent> ActorInstanceTouchedActorInstanceEventHandler
		{
			get;
			set;
		}

		public IEventHandler<PlayerTouchedActorInstanceEvent> PlayerTouchedActorInstanceEventHandler
		{
			get;
			set;
		}

		public IEventHandler<ActorInstanceMovedEvent> ActorInstanceMovedEventHandler
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

		public bool ChangeCoordinate(Board board, Player player, Coordinate newCoordinate)
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