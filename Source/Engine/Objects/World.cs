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
		private IEnumerable<Actor> _actors;

		private Player _startingPlayer;

		public World(
			Guid id,
			Player startingPlayer,
			IEnumerable<Board> boards,
			IEnumerable<Actor> actors)
		{
			startingPlayer.ThrowIfNull("startingPlayer");
			boards.ThrowIfNull("boards");
			actors.ThrowIfNull("actors");

			_id = id;
			StartingPlayer = startingPlayer;
			Boards = boards;
			Actors = actors;
		}

		public Guid Id
		{
			get
			{
				return _id;
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
				return _actors;
			}
			set
			{
				value.ThrowIfNull("value");

				_actorsById.Clear();
				foreach (Actor actor in value)
				{
					_actorsById.Add(actor.Id, actor);
				}
				_actors = value;
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
	}
}