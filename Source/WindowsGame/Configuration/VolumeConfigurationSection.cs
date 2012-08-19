using System.ComponentModel;
using System.Configuration;

using TextAdventure.Engine.Game.Commands;

namespace TextAdventure.WindowsGame.Configuration
{
	public class VolumeConfigurationSection : ConfigurationSection, IVolumeConfiguration
	{
		[ConfigurationProperty("soundEffects", IsRequired = true)]
		[TypeConverter(typeof(VolumeTypeConverter))]
		public Volume SoundEffects
		{
			get
			{
				return (Volume)this["soundEffects"];
			}
			set
			{
				this["soundEffects"] = value;
			}
		}

		[ConfigurationProperty("music", IsRequired = true)]
		[TypeConverter(typeof(VolumeTypeConverter))]
		public Volume Music
		{
			get
			{
				return (Volume)this["music"];
			}
			set
			{
				this["music"] = value;
			}
		}

		public override bool IsReadOnly()
		{
			return false;
		}
	}
}