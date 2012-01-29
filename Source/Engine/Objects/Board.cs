using System;
using System.Collections.Generic;
using System.Linq;

using Junior.Common;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Game.Commands;
using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Game.World;

namespace TextAdventure.Engine.Objects
{
	public class Board : INamedObject, IDescribedObject
	{
		private readonly ActorInstanceLayer _actorInstanceLayer;
		private readonly SpriteLayer _backgroundLayer;
		private readonly EventHandlerCollection _eventHandlerCollection;
		private readonly IEnumerable<BoardExit> _exits;
		private readonly SpriteLayer _foregroundLayer;
		private readonly Guid _id;
		private readonly Size _size;
		private readonly IEnumerable<Timer> _timers;
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
			IEnumerable<Timer> timers,
			EventHandlerCollection eventHandlerCollection = null)
		{
			name.ThrowIfNull("name");
			description.ThrowIfNull("description");
			backgroundLayer.ThrowIfNull("backgroundLayer");
			foregroundLayer.ThrowIfNull("foregroundLayer");
			actorInstanceLayer.ThrowIfNull("actorInstanceLayer");
			exits.ThrowIfNull("exits");
			timers.ThrowIfNull("timers");

			if (backgroundLayer.BoardId != id)
			{
				throw new ArgumentException("Background layer must belong to board.", "backgroundLayer");
			}
			if (foregroundLayer.BoardId != id)
			{
				throw new ArgumentException("Foreground layer must belong to board.", "backgroundLayer");
			}
			if (actorInstanceLayer.BoardId != id)
			{
				throw new ArgumentException("Actor instance layer must belong to board.", "backgroundLayer");
			}

			_id = id;
			Name = name;
			Description = description;
			_size = size;
			_backgroundLayer = backgroundLayer;
			_foregroundLayer = foregroundLayer;
			_actorInstanceLayer = actorInstanceLayer;
			_exits = exits;
			_timers = timers;
			_eventHandlerCollection = eventHandlerCollection;
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

		public IEnumerable<Timer> Timers
		{
			get
			{
				return _timers;
			}
		}

		public Size Size
		{
			get
			{
				return _size;
			}
		}

		protected internal EventHandlerCollection EventHandlerCollection
		{
			get
			{
				return _eventHandlerCollection;
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

		public bool CoordinateIntersects(Coordinate coordinate)
		{
			return coordinate.X >= 0 && coordinate.Y >= 0 && coordinate.X < Size.Width && coordinate.Y < Size.Height;
		}

		public bool IsCoordinateOccupied(Coordinate coordinate)
		{
			return _foregroundLayer[coordinate] != null || _actorInstanceLayer[coordinate] != null;
		}

		protected void PerformTimerAction(Timer timer, TimerAction action)
		{
			timer.ThrowIfNull("timer");

			if (!Timers.Contains(timer))
			{
				throw new ArgumentException("Board does not contain the specified timer.", "timer");
			}

			switch (action)
			{
				case TimerAction.Start:
					timer.Start();
					break;
				case TimerAction.Stop:
					timer.Stop();
					break;
				case TimerAction.Reset:
					timer.Reset();
					break;
				case TimerAction.Restart:
					timer.Restart();
					break;
				default:
					throw new ArgumentOutOfRangeException("action");
			}
		}

		protected void PerformTimerActionOnAllTimers(TimerAction action)
		{
			foreach (Timer timer in Timers)
			{
				PerformTimerAction(timer, action);
			}
		}

		protected internal virtual EventResult OnEntered(EventContext context, BoardEnteredEvent @event)
		{
			return _eventHandlerCollection.SafeInvoke(context, @event);
		}

		protected internal virtual EventResult OnExited(EventContext context, BoardExitedEvent @event)
		{
			return _eventHandlerCollection.SafeInvoke(context, @event);
		}
	}
}