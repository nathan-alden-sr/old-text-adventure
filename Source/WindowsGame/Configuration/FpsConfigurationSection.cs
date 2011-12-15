using System.Configuration;

namespace TextAdventure.WindowsGame.Configuration
{
	public class FpsConfigurationSection : ConfigurationSection
	{
		[ConfigurationProperty("visible", IsRequired = false, DefaultValue = false)]
		public bool Visible
		{
			get
			{
				return (bool)this["visible"];
			}
		}
	}
}