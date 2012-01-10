using System;

using Junior.Common;

using TextAdventure.Engine.Game.World;
using TextAdventure.WindowsGame.RendererStates;

namespace TextAdventure.WindowsGame.World
{
	public class WorldTime : IWorldTime
	{
		private readonly WorldTimeRendererState _worldTimeRendererState;

		public WorldTime(WorldTimeRendererState worldTimeRendererState)
		{
			worldTimeRendererState.ThrowIfNull("worldTimeRendererState");

			_worldTimeRendererState = worldTimeRendererState;
		}

		public TimeSpan Elapsed
		{
			get
			{
				return _worldTimeRendererState.ElapsedWorldTime;
			}
		}

		public TimeSpan Total
		{
			get
			{
				return _worldTimeRendererState.TotalWorldTime;
			}
		}

		public float Speed
		{
			get
			{
				return _worldTimeRendererState.Speed;
			}
		}

		public bool Paused
		{
			get
			{
				return _worldTimeRendererState.Paused;
			}
		}

		public void Pause()
		{
			_worldTimeRendererState.Pause();
		}

		public void Unpause()
		{
			_worldTimeRendererState.Unpause();
		}
	}
}