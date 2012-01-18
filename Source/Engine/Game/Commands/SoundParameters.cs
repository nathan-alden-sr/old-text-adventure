namespace TextAdventure.Engine.Game.Commands
{
	public class SoundParameters
	{
		private readonly Volume _volume;

		public SoundParameters(Volume volume)
		{
			_volume = volume;
		}

		public Volume Volume
		{
			get
			{
				return _volume;
			}
		}
	}
}