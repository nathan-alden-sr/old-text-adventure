using System;

namespace TextAdventure.WindowsGame.RendererStates
{
	public interface IWorldTimeRendererState
	{
		bool Visible
		{
			get;
		}
		TimeSpan ElapsedWorldTime
		{
			get;
		}
		TimeSpan TotalWorldTime
		{
			get;
		}
		float Speed
		{
			get;
		}
		bool Paused
		{
			get;
		}
	}
}