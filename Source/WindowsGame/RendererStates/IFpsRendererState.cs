using System;

namespace TextAdventure.WindowsGame.RendererStates
{
	public interface IFpsRendererState
	{
		bool Visible
		{
			get;
		}
		int FrameCount
		{
			get;
		}

		void FrameRendered();
		void UpdateElapsedGameTime(TimeSpan elapsedGameTime);
		void UpdateFrameCount();
	}
}