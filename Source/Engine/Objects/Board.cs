using System;
using System.Collections.Generic;
using System.Linq;

using Junior.Common;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Game.Events;

namespace TextAdventure.Engine.Objects
{
	public class Board : IUnique, INamedObject, IDescribedObject
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
		private string _description;
		private string _name;

		public Board(
			Guid id,
			string name,
			string description,
			Size size,
			SpriteLayer backgroundLayer,
			SpriteLayer foregroundLayer,
			ActorInstanceLayer actorInstanceLayer,
			IEnumerable<BoardExit> exits,
			IEventHandler<BoardEnteredEvent> boardEnteredEventHandler = null,
			IEventHandler<BoardExitedEvent> boardExitedEventHandler = null)
		{
			name.ThrowIfNull("name");
			description.ThrowIfNull("description");
			backgroundLayer.ThrowIfNull("backgroundLayer");
			foregroundLayer.ThrowIfNull("foregroundLayer");
			actorInstanceLayer.ThrowIfNull("actorInstanceLayer");
			exits.ThrowIfNull("exits");

			_id = id;
			Name = name;
			Description = description;
			_size = size;
			_backgroundLayer = backgroundLayer;
			_foregroundLayer = foregroundLayer;
			_actorInstanceLayer = actorInstanceLayer;
			_exits = exits;
			_boardEnteredEventHandler = boardEnteredEventHandler;
			_boardExitedEventHandler = boardExitedEventHandler;
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