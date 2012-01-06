using System;

using Junior.Common;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Game.Events;

namespace TextAdventure.Engine.Objects
{
	public class Player : IPlayer
	{
		private readonly IEventHandler<ActorInstanceTouchedPlayerEvent> _actorInstanceTouchedPlayerEventHandler;
		private readonly Guid _id;
		private Character _character;

		public Player(
			Guid id,
			Guid boardId,
			Character character,
			Coordinate coordinate,
			IEventHandler<ActorInstanceTouchedPlayerEvent> actorInstanceTouchedPlayerEventHandler = null)
		{
			character.ThrowIfNull("character");

			_id = id;
			BoardId = boardId;
			Character = character;
			Coordinate = coordinate;
			_actorInstanceTouchedPlayerEventHandler = actorInstanceTouchedPlayerEventHandler;
		}

		public Guid BoardId
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
			set
			{
				value.ThrowIfNull("value");

				_character = value;
			}
		}

		public Coordinate Coordinate
		{
			get;
			private set;
		}

		public IEventHandler<ActorInstanceTouchedPlayerEvent> ActorInstanceTouchedPlayerEventHandler
		{
			get
			{
				return _actorInstanceTouchedPlayerEventHandler;
			}
		}

		public Guid Id
		{
			get
			{
				return _id;
			}
		}

		public bool ChangeLocation(Board board, Coordinate newCoordinate)
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
	}
}