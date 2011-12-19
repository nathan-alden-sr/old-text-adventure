using System;
using System.Collections.Generic;

using Junior.Common;

namespace TextAdventure.Engine.Objects
{
	public class World
	{
		private readonly Dictionary<Guid, Actor> _actorsById = new Dictionary<Guid, Actor>();
		private readonly Dictionary<Guid, Board> _boardsById = new Dictionary<Guid, Board>();
		private readonly Guid _id;
		private readonly Dictionary<Guid, Message> _messagesById = new Dictionary<Guid, Message>();
		private Player _startingPlayer;
		private int _version;

		public World(
			Guid id,
			int version,
			Player startingPlayer,
			IEnumerable<Board> boards,
			IEnumerable<Actor> actors,
			IEnumerable<Message> messages)
		{
			startingPlayer.ThrowIfNull("startingPlayer");
			boards.ThrowIfNull("boards");
			actors.ThrowIfNull("actors");
			messages.ThrowIfNull("messages");

			_id = id;
			Version = version;
			StartingPlayer = startingPlayer;
			Boards = boards;
			Actors = actors;
			Messages = messages;
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
	}
}