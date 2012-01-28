using System;
using System.Collections.Generic;

using Junior.Common;

using TextAdventure.Engine.Game.Commands;
using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Game.Messages;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.World
{
	public class WorldInstance
	{
		private readonly DelegateDictionary<Guid, Board> _boardsById = new DelegateDictionary<Guid, Board>();
		private readonly Dictionary<Board, CommandQueue> _commandQueuesByBoard = new Dictionary<Board, CommandQueue>();
		private readonly MessageMananger _messageMananger;
		private readonly IMultimediaPlayer _multimediaPlayer;
		private readonly Player _player;
		private readonly PlayerInput _playerInput = new PlayerInput();
		private readonly Objects.World _world;
		private readonly IWorldObserver _worldObserver;
		private readonly IWorldTime _worldTime;

		public WorldInstance(Objects.World world, Player player, IWorldTime worldTime, IWorldObserver worldObserver, IMultimediaPlayer multimediaPlayer)
		{
			world.ThrowIfNull("world");
			player.ThrowIfNull("player");
			worldTime.ThrowIfNull("worldTime");
			worldObserver.ThrowIfNull("worldObserver");
			multimediaPlayer.ThrowIfNull("multimediaPlayer");

			_world = world;
			_player = player;
			_worldTime = worldTime;
			_worldObserver = worldObserver;
			_multimediaPlayer = multimediaPlayer;
			_messageMananger = new MessageMananger(this);

			PopulateCommandQueues(world, worldObserver);
		}

		public Player Player
		{
			get
			{
				return _player;
			}
		}

		public Objects.World World
		{
			get
			{
				return _world;
			}
		}

		public Board CurrentBoard
		{
			get
			{
				return _boardsById[_player.BoardId, boardId => _world.GetBoardById(boardId)];
			}
		}

		public PlayerInput PlayerInput
		{
			get
			{
				return _playerInput;
			}
		}

		public MessageMananger MessageMananger
		{
			get
			{
				return _messageMananger;
			}
		}

		public IWorldTime WorldTime
		{
			get
			{
				return _worldTime;
			}
		}

		public CommandQueue CurrentCommandQueue
		{
			get
			{
				return _commandQueuesByBoard[CurrentBoard];
			}
		}

		public IMultimediaPlayer MultimediaPlayer
		{
			get
			{
				return _multimediaPlayer;
			}
		}

		public void ProcessCommandQueue()
		{
			UpdateTimers();

			_commandQueuesByBoard[CurrentBoard].ProcessQueue();
		}

		protected internal EventResult RaiseEvent<TEvent>(Func<EventContext, TEvent, EventResult> eventDelegate, TEvent @event)
			where TEvent : Event
		{
			eventDelegate.ThrowIfNull("eventDelegate");
			@event.ThrowIfNull("event");

			_worldObserver.EventRaising(@event);

			var eventContext = new EventContext(this, _commandQueuesByBoard[CurrentBoard]);
			EventResult result = eventDelegate(eventContext, @event);

			_worldObserver.EventRaised(@event, result);

			return result;
		}

		private void UpdateTimers()
		{
			foreach (Timer timer in _world.Timers)
			{
				timer.Update(_worldTime.Elapsed);

				bool elapsed = timer.State == TimerState.Running && timer.HasElapsed;

				if (!elapsed)
				{
					continue;
				}

				timer.Stop();

				Timer tempTimer = timer;

				RaiseEvent(tempTimer.OnElapsed, new TimerElapsedEvent(timer));
			}
		}

		private void PopulateCommandQueues(Objects.World world, IWorldObserver worldObserver)
		{
			foreach (Board board in world.Boards)
			{
				_commandQueuesByBoard.Add(board, new CommandQueue(worldObserver, this));
			}
		}
	}
}