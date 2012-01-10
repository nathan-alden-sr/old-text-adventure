using System;
using System.Collections.Generic;

using Junior.Common;

using TextAdventure.Engine.Game.Events;

namespace TextAdventure.Engine.Objects
{
	public class World : IWorld
	{
		private readonly Dictionary<Guid, Actor> _actorsById = new Dictionary<Guid, Actor>();
		private readonly IEventHandler<AnswerSelectedEvent> _answerSelectedEventHandler;
		private readonly Dictionary<Guid, Board> _boardsById = new Dictionary<Guid, Board>();
		private readonly Guid _id;
		private readonly Dictionary<Guid, Message> _messagesById = new Dictionary<Guid, Message>();
		private readonly Dictionary<Guid, Timer> _timersById = new Dictionary<Guid, Timer>();
		private Player _startingPlayer;
		private int _version;

		public World(
			Guid id,
			int version,
			Player startingPlayer,
			IEnumerable<Board> boards,
			IEnumerable<Actor> actors,
			IEnumerable<Message> messages,
			IEnumerable<Timer> timers,
			IEventHandler<AnswerSelectedEvent> answerSelectedEventHandler = null)
		{
			startingPlayer.ThrowIfNull("startingPlayer");
			boards.ThrowIfNull("boards");
			actors.ThrowIfNull("actors");
			messages.ThrowIfNull("messages");
			timers.ThrowIfNull("timers");

			_id = id;
			_answerSelectedEventHandler = answerSelectedEventHandler;
			Version = version;
			StartingPlayer = startingPlayer;
			Boards = boards;
			Actors = actors;
			Messages = messages;
			Timers = timers;
		}

		public Player StartingPlayer
		{
			get
			{
				return _startingPlayer;
			}
			set
			{
				value.ThrowIfNull("value");

				_startingPlayer = value;
			}
		}

		public IEnumerable<Board> Boards
		{
			get
			{
				return _boardsById.Values;
			}
			set
			{
				value.ThrowIfNull("value");

				_boardsById.Clear();
				foreach (Board board in value)
				{
					_boardsById.Add(board.Id, board);
				}
			}
		}

		public IEnumerable<Actor> Actors
		{
			get
			{
				return _actorsById.Values;
			}
			set
			{
				value.ThrowIfNull("value");

				_actorsById.Clear();
				foreach (Actor actor in value)
				{
					_actorsById.Add(actor.Id, actor);
				}
			}
		}

		public IEnumerable<Message> Messages
		{
			get
			{
				return _messagesById.Values;
			}
			set
			{
				_messagesById.Clear();
				foreach (Message message in value)
				{
					_messagesById.Add(message.Id, message);
				}
			}
		}

		public IEnumerable<Timer> Timers
		{
			get
			{
				return _timersById.Values;
			}
			set
			{
				_timersById.Clear();
				foreach (Timer timer in value)
				{
					_timersById.Add(timer.Id, timer);
				}
			}
		}

		IEnumerable<ITimer> IWorld.Timers
		{
			get
			{
				return Timers;
			}
		}

		public Guid Id
		{
			get
			{
				return _id;
			}
		}

		public int Version
		{
			get
			{
				return _version;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("value", "Version must be at least 1.");
				}

				_version = value;
			}
		}

		IPlayer IWorld.StartingPlayer
		{
			get
			{
				return _startingPlayer;
			}
		}

		IEnumerable<IBoard> IWorld.Boards
		{
			get
			{
				return Boards;
			}
		}

		IEnumerable<IActor> IWorld.Actors
		{
			get
			{
				return Actors;
			}
		}

		IEnumerable<IMessage> IWorld.Messages
		{
			get
			{
				return Messages;
			}
		}

		public IEventHandler<AnswerSelectedEvent> AnswerSelectedEventHandler
		{
			get
			{
				return _answerSelectedEventHandler;
			}
		}

		IBoard IWorld.GetBoardById(Guid id)
		{
			return GetBoardById(id);
		}

		IActor IWorld.GetActorById(Guid id)
		{
			return GetActorById(id);
		}

		IMessage IWorld.GetMessageById(Guid id)
		{
			return GetMessageById(id);
		}

		ITimer IWorld.GetTimerById(Guid id)
		{
			return GetTimerById(id);
		}

		public Board GetBoardById(Guid id)
		{
			return _boardsById[id];
		}

		public Actor GetActorById(Guid id)
		{
			return _actorsById[id];
		}

		public Message GetMessageById(Guid id)
		{
			return _messagesById[id];
		}

		public Timer GetTimerById(Guid id)
		{
			return _timersById[id];
		}
	}
}