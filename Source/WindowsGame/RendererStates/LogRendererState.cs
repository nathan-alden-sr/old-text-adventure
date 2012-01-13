using System;
using System.Collections.Generic;
using System.Linq;

using Junior.Common;

using TextAdventure.Engine.Game.Commands;
using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Game.World;
using TextAdventure.WindowsGame.Renderers;
using TextAdventure.WindowsGame.Xna;

namespace TextAdventure.WindowsGame.RendererStates
{
	public class LogRendererState : ILogRendererState
	{
		private readonly Queue<LogEntry> _logEntries = new Queue<LogEntry>();
		private bool _visible;

		public bool Visible
		{
			get
			{
				return _visible && _logEntries.Any();
			}
			set
			{
				_visible = value;
			}
		}

		public bool ShowTimestamps
		{
			get;
			set;
		}

		public int? MinimumWindowWidth
		{
			get;
			set;
		}

		public int MaximumVisibleLogLines
		{
			get;
			set;
		}

		public TimeSpan LogEntryLifetime
		{
			get;
			set;
		}

		public IEnumerable<LogEntry> LogEntries
		{
			get
			{
				return _logEntries;
			}
		}

		public void DequeueOldLogEntries(XnaGameTime gameTime)
		{
			gameTime.ThrowIfNull("gameTime");

			while (_logEntries.Count + _logEntries.Sum(arg => arg.Details.Count()) > MaximumVisibleLogLines ||
			       (_logEntries.Count + _logEntries.Sum(arg => arg.Details.Count()) > 0 && gameTime.TotalGameTime - _logEntries.Peek().LoggedTotalWorldTime > LogEntryLifetime))
			{
				_logEntries.Dequeue();
			}
		}

		public void EnqueueCommandExecutedLogEntry(TimeSpan loggedTotalWorldTime, Command command, CommandResult result)
		{
			command.ThrowIfNull("command");

			LogEntryType entryType;

			switch (result)
			{
				case CommandResult.None:
				case CommandResult.Deferred:
					return;
				case CommandResult.Succeeded:
					entryType = LogEntryType.CommandExecutedSuccessfully;
					break;
				case CommandResult.Failed:
					entryType = LogEntryType.CommandExecutionFailed;
					break;
				default:
					throw new ArgumentOutOfRangeException("result");
			}

			var entry = new LogEntry(loggedTotalWorldTime, command, entryType, LogEntryLifetime);

			EnqueueLogEntry(entry);
		}

		public void EnqueueEventRaisingLogEntry<TEvent>(TimeSpan loggedTotalWorldTime, TEvent @event)
			where TEvent : Event
		{
			@event.ThrowIfNull("event");

			var entry = new LogEntry(loggedTotalWorldTime, @event, LogEntryType.EventRaising, LogEntryLifetime);

			EnqueueLogEntry(entry);
		}

		public void EnqueueEventHandledLogEntry<TEvent>(TimeSpan loggedTotalWorldTime, IEventHandler<TEvent> eventHandler, TEvent @event, EventResult result)
			where TEvent : Event
		{
			@event.ThrowIfNull("event");
			eventHandler.ThrowIfNull("eventHandler");

			var entry = new LogEntry(loggedTotalWorldTime, eventHandler, result == EventResult.Complete ? LogEntryType.EventComplete : LogEntryType.EventCanceled, LogEntryLifetime);

			EnqueueLogEntry(entry);
		}

		public void ClearLog()
		{
			_logEntries.Clear();
		}

		private void EnqueueLogEntry(LogEntry entry)
		{
			DequeueIfFull();

			if (LogEntryLifetime > TimeSpan.Zero)
			{
				_logEntries.Enqueue(entry);
			}
		}

		private void DequeueIfFull()
		{
			if (_logEntries.Count == MaximumVisibleLogLines)
			{
				_logEntries.Dequeue();
			}
		}
	}
}