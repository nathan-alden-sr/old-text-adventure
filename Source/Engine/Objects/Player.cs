using System;

using Junior.Common;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Game.World;

namespace TextAdventure.Engine.Objects
{
	public class Player : IUnique
	{
		private readonly Character _character;
		private readonly EventHandlerCollection _eventHandlerCollection;
		private readonly Guid _id;

		public Player(
			Guid id,
			Guid boardId,
			Coordinate coordinate,
			Character character,
			EventHandlerCollection eventHandlerCollection = null)
		{
			character.ThrowIfNull("character");

			_id = id;
			BoardId = boardId;
			_character = character;
			_eventHandlerCollection = eventHandlerCollection;
			Coordinate = coordinate;
		}

		public Guid BoardId
		{
			get;
			private set;
		}

		public Coordinate Coordinate
		{
			get;
			private set;
		}

		public Character Character
		{
			get
			{
				return _character;
			}
		}

		protected internal EventHandlerCollection EventHandlerCollection
		{
			get
			{
				return _eventHandlerCollection;
			}
		}

		public Guid Id
		{
			get
			{
				return _id;
			}
		}

		protected internal bool ChangeLocation(Board board, Coordinate newCoordinate)
		{
			board.ThrowIfNull("board");

			Sprite foregroundSprite = board.ForegroundLayer[newCoordinate];
			ActorInstance actorInstance = board.ActorInstanceLayer[newCoordinate];

			if (foregroundSprite != null || actorInstance != null)
			{
				return false;
			}

			BoardId = board.Id;
			Coordinate = newCoordinate;

			return true;
		}

		protected internal virtual EventResult OnTouchedByActorInstance(EventContext context, ActorInstanceTouchedPlayerEvent @event)
		{
			return _eventHandlerCollection.SafeInvoke(context, @event);
		}
	}
}