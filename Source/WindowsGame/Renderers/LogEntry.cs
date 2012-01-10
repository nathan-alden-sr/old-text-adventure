using System;
using System.Collections.Generic;
using System.Linq;

using TextAdventure.Engine.Game;
using TextAdventure.WindowsGame.Helpers;

namespace TextAdventure.WindowsGame.Renderers
{
	public class LogEntry
	{
		private readonly IEnumerable<string> _details;
		private readonly LogEntryType _entryType;
		private readonly FadeHelper _fadeHelper;
		private readonly TimeSpan _loggedTotalWorldTime;
		private readonly string _title;

		public LogEntry(TimeSpan loggedTotalWorldTime, ILoggable loggable, LogEntryType entryType, TimeSpan logEntryLifetime)
		{
			_loggedTotalWorldTime = loggedTotalWorldTime;
			_entryType = entryType;
			_title = loggable.Title;
			_details = loggable.Details.ToArray();
			_fadeHelper = new FadeHelper(loggedTotalWorldTime + logEntryLifetime - Constants.LogRenderer.FadeDuration, Constants.LogRenderer.FadeDuration, 1f, 0f);
		}

		public string Title
		{
			get
			{
				return _title;
			}
		}

		public IEnumerable<string> Details
		{
			get
			{
				return _details;
			}
		}

		public int LineCount
		{
			get
			{
				return _details.Count() + 1;
			}
		}

		public TimeSpan LoggedTotalWorldTime
		{
			get
			{
				return _loggedTotalWorldTime;
			}
		}

		public LogEntryType EntryType
		{
			get
			{
				return _entryType;
			}
		}

		public FadeHelper FadeHelper
		{
			get
			{
				return _fadeHelper;
			}
		}
	}
}