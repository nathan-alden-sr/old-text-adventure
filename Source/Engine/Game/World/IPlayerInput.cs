namespace TextAdventure.Engine.Game.World
{
	public interface IPlayerInput
	{
		bool Suspended
		{
			get;
		}

		void Suspend();
		void Resume();
	}
}