using System;
using System.Collections.Generic;
using System.Linq;

using Junior.Common;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Game.Events;

namespace TextAdventure.Engine.Objects
{
	public class Board : IUnique
	{
		private static readonly Random _random = new Random();
		private readonly ActorInstanceLayer _actorInstanceLayer;
		private readonly SpriteLayer _backgroundLayer;
		private readonly IEventHandler<BoardEnteredEvent> _boardEnteredEventHandler;
		private readonly IEventHandler<BoardExitedEvent> _boardExitedEventHandler;
		private readonly IEnumerable<BoardExit> _exits;
		private readonly SpriteLayer _foregroundLayer;
		private readonly Guid _id;
		private readonly Size _size;

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
			_size = size;
			_backgroundLayer = backgroundLayer;
			_foregroundLayer = foregroundLayer;
			_actorInstanceLayer = actorInstanceLayer;
			_exits = exits;
			_boardEnteredEventHandler = boardEnteredEventHandler;
			_boardExitedEventHandler = boardExitedEventHandler;
		}

		public SpriteLayer BackgroundLayer
		{
			get
			{
				return _backgroundLayer;
			}
		}

		public SpriteLayer ForegroundLayer
		{
			get
			{
				return _foregroundLayer;
			}
		}

		public ActorInstanceLayer ActorInstanceLayer
		{
			get
			{
				return _actorInstanceLayer;
			}
		}

		public IEnumerable<BoardExit> Exits
		{
			get
			{
				return _exits;
			}
		}

		public Size Size
		{
			get
			{
				return _size;
			}
		}

		public IEventHandler<BoardEnteredEvent> BoardEnteredEventHandler
		{
			get
			{
				return _boardEnteredEventHandler;
			}
		}

		public IEventHandler<BoardExitedEvent> BoardExitedEventHandler
		{
			get
			{
				return _boardExitedEventHandler;
			}
		}

		public Guid Id
		{
			get
			{
				return _id;
			}
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