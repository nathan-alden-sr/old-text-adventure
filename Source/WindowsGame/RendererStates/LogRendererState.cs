using System;
using System.Collections.Generic;
using System.Linq;

using Junior.Common;

using TextAdventure.Engine.Game.Commands;
using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Game.World;
using TextAdventure.WindowsGame.Renderers;
using TextAdventure.Xna;

namespace TextAdventure.WindowsGame.RendererStates
{
	public class LogRendererState : ILogRendererState
	{
		private readonly Queue<LogEntry> _logEntries = new Queue<LogEntry>();
		private bool _visible;

		public bool ShowRaisingEvents
		{
			get;
			set;
		}

		public bool Visible
		{
			get
			{
				return _visible && GetFilteredLogEntries().Any();
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

		public IEnumerable<LogEntry> GetFilteredLogEntries()
		{
			return ShowRaisingEvents ? _logEntries : _logEntries.Where(arg => arg.EntryType != LogEntryType.EventRaising);
		}

		public void DequeueOldLogEntries(IXnaGameTime gameTime)
		{
			gameTime.ThrowIfNull("gameTime");

			while (_logEntries.Any() && gameTime.TotalGameTime - _logEntries.Peek().LoggedTotalWorldTime > LogEntryLifetime)
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

		public void EnqueueEventRaisedLogEntry<TEvent>(TimeSpan loggedTotalWorldTime, TEvent @event, EventResult result)
			where TEvent : Event
		{
			@event.ThrowIfNull("event");

			if (result == EventResult.None)
			{
				return;
			}

			var entry = new LogEntry(loggedTotalWorldTime, @event, result == EventResult.Complete ? LogEntryType.EventComplete : LogEntryType.EventCanceled, LogEntryLifetime);

			EnqueueLogEntry(entry);
		}

		public void ClearLog()
		{
			_logEntries.Clear();
		}

		private void EnqueueLogEntry(LogEntry entry)
		{
			DequeueIfFull(1 + entry.Details.Count());

			if (LogEntryLifetime > TimeSpan.Zero)
			{
				_logEntries.Enqueue(entry);
			}
		}

		private void DequeueIfFull(int newLines)
		{
			LogEntry[] filteredLogEntries = GetFilteredLogEntries().ToArray();

			while (filteredLogEntries.Length + filteredLogEntries.Sum(arg => arg.Details.Count()) + newLines >= MaximumVisibleLogLines)
			{
				_logEntries.Dequeue();
				filteredLogEntries = GetFilteredLogEntries().ToArray();
			}
		}
	}
}