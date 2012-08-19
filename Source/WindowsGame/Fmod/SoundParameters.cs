using TextAdventure.Engine.Game.Commands;

namespace TextAdventure.WindowsGame.Fmod
{
	public class SoundParameters
	{
		private readonly bool _muted;
		private readonly Volume _volume;

		public SoundParameters(Volume volume, bool muted)
		{
			_volume = volume;
			_muted = muted;
		}

		public Volume Volume
		{
			get
			{
				return _volume;
			}
		}

		public bool Muted
		{
			get
			{
				return _muted;
			}
		}
	}
}