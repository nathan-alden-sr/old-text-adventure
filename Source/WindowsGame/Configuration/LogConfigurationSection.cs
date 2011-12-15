using System;
using System.Configuration;

namespace TextAdventure.WindowsGame.Configuration
{
	public class LogConfigurationSection : ConfigurationSection
	{
		[ConfigurationProperty("visible", IsRequired = false, DefaultValue = false)]
		public bool Visible
		{
			get
			{
				return (bool)this["visible"];
			}
		}

		[ConfigurationProperty("maximumVisibleLogLines", IsRequired = false, DefaultValue = 10)]
		public int MaximumVisibleLogLines
		{
			get
			{
				return Math.Max(0, (int)this["maximumVisibleLogLines"]);
			}
		}

		[ConfigurationProperty("minimumWindowWidth", IsRequired = false, DefaultValue = null)]
		public int? MinimumWindowWidth
		{
			get
			{
				var value = this["minimumWindowWidth"] as int?;

				return value < 0 ? null : value;
			}
		}

		[ConfigurationProperty("logEntryLifetime", IsRequired = false)]
		public TimeSpan LogEntryLifetime
		{
			get
			{
				var value = this["logEntryLifetime"] as TimeSpan?;

				return value ?? TimeSpan.FromDays(1);
			}
		}

		[ConfigurationProperty("showTimestamps", IsRequired = false, DefaultValue = true)]
		public bool ShowTimestamps
		{
			get
			{
				return (bool)this["showTimestamps"];
			}
		}
	}
}