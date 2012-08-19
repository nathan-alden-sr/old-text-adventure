using System;

namespace TextAdventure.WindowsGame.Configuration
{
	public interface ILogConfiguration
	{
		bool Visible
		{
			get;
		}
		int MaximumVisibleLogLines
		{
			get;
		}
		int? MinimumWindowWidth
		{
			get;
		}
		TimeSpan LogEntryLifetime
		{
			get;
		}
		bool ShowTimestamps
		{
			get;
		}
		bool ShowRaisingEvents
		{
			get;
		}
	}
}