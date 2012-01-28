using System;
using System.Collections.Generic;

using TextAdventure.WindowsGame.Renderers;

namespace TextAdventure.WindowsGame.RendererStates
{
	public interface ILogRendererState
	{
		bool Visible
		{
			get;
		}
		bool ShowTimestamps
		{
			get;
		}
		int? MinimumWindowWidth
		{
			get;
		}
		int MaximumVisibleLogLines
		{
			get;
		}
		TimeSpan LogEntryLifetime
		{
			get;
		}

		IEnumerable<LogEntry> GetFilteredLogEntries();
	}
}