using System;

namespace TextAdventure.Engine.Game.World
{
	public interface IWorldTime
	{
		TimeSpan Elapsed
		{
			get;
		}
		TimeSpan Total
		{
			get;
		}
		float Speed
		{
			get;
			set;
		}
		bool Paused
		{
			get;
		}

		void Pause();
		void Unpause();
	}
}