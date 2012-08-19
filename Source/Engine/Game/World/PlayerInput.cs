using System;

namespace TextAdventure.Engine.Game.World
{
	public class PlayerInput
	{
		private int _suspendCount;

		public bool Suspended
		{
			get
			{
				return _suspendCount > 0;
			}
		}

		public void Suspend()
		{
			_suspendCount++;
		}

		public void Resume()
		{
			_suspendCount = Math.Max(0, _suspendCount - 1);
		}
	}
}