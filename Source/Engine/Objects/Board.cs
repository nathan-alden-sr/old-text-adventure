using System;
using System.Collections.Generic;
using System.Linq;

using Junior.Common;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Game.Events;

namespace TextAdventure.Engine.Objects
{
	public class Board : IBoard
	{
		private static readonly Random _random = new Random();
		private readonly Guid _id;
		private ActorInstanceLayer _actorInstanceLayer;
		private SpriteLayer _backgroundLayer;
		private SpriteLayer _foregroundLayer;

		public Board(
			Guid id,
			Size size,
			SpriteLayer backgroundLayer,
			SpriteLayer foregroundLayer,
			ActorInstanceLayer actorInstanceLayer,
			IEnumerable<BoardExit> exits,
			IEventHandler<BoardEnteredEvent> boardEnteredEventHandler = null,
			IEventHandler<BoardExitedEvent> boardExitedEventHandler = null)
		{
			backgroundLayer.ThrowIfNull("backgroundLayer");
			foregroundLayer.ThrowIfNull("foregroundLayer");
			actorInstanceLayer.ThrowIfNull("actorInstanceLayer");
			exits.ThrowIfNull("exits");

			_id = id;
			Size = size;
			BackgroundLayer = backgroundLayer;
			ForegroundLayer = foregroundLayer;
			ActorInstanceLayer = actorInstanceLayer;
			Exits = exits;
			BoardEnteredEventHandler = boardEnteredEventHandler;
			BoardExitedEventHandler = boardExitedEventHandler;
		}

		public SpriteLayer BackgroundLayer
		{
			get
			{
				return _backgroundLayer;
			}
			set
			{
				value.ThrowIfNull("value");

				_backgroundLayer = value;
			}
		}

		public SpriteLayer ForegroundLayer
		{
			get
			{
				return _foregroundLayer;
			}
			set
			{
				value.ThrowIfNull("value");

				_foregroundLayer = value;
			}
		}

		public ActorInstanceLayer ActorInstanceLayer
		{
			get
			{
				return _actorInstanceLayer;
			}
			set
			{
				value.ThrowIfNull("value");

				_actorInstanceLayer = value;
			}
		}

		public IEnumerable<BoardExit> Exits
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

		public Size Size
		{
			get;
			set;
		}

		ISpriteLayer IBoard.BackgroundLayer
		{
			get
			{
				return BackgroundLayer;
			}
		}

		ISpriteLayer IBoard.ForegroundLayer
		{
			get
			{
				return ForegroundLayer;
			}
		}

		IActorInstanceLayer IBoard.ActorInstanceLayer
		{
			get
			{
				return ActorInstanceLayer;
			}
		}

		IEnumerable<IBoardExit> IBoard.Exits
		{
			get
			{
				return Exits;
			}
		}

		public IEventHandler<BoardEnteredEvent> BoardEnteredEventHandler
		{
			get;
			set;
		}

		public IEventHandler<BoardExitedEvent> BoardExitedEventHandler
		{
			get;
			set;
		}

		public bool CoordinateIntersects(Coordinate coordinate)
		{
			return coordinate.X >= 0 && coordinate.Y >= 0 && coordinate.X < Size.Width && coordinate.Y < Size.Height;
		}

		public Coordinate GetRandomEmptyActorInstanceLayerCoordinate()
		{
			Coordinate[] emptyCoordinates = _foregroundLayer.EmptyTiles
				.Intersect(_actorInstanceLayer.EmptyTiles)
				.ToArray();
			int index = _random.Next(0, emptyCoordinates.Length);

			return emptyCoordinates[index];
		}
	}
}