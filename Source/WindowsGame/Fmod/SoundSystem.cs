using System;

using FMOD;

namespace TextAdventure.WindowsGame.Fmod
{
	public class SoundSystem
	{
		public static readonly SoundSystem Instance = new SoundSystem();
		private readonly FMOD.System _system;

		private SoundSystem()
		{
			RESULT result = Factory.System_Create(ref _system);

			if (result != RESULT.OK)
			{
				throw new Exception("Failed to create FMOD system.");
			}

			uint version = 0;

			result = System.getVersion(ref version);

			if (result != RESULT.OK || version < VERSION.number)
			{
				throw new Exception("Failed to create FMOD system.");
			}

			result = System.init(32, INITFLAGS.NORMAL, IntPtr.Zero);

			if (result != RESULT.OK)
			{
				throw new Exception("Failed to create FMOD system.");
			}
		}

		public FMOD.System System
		{
			get
			{
				return _system;
			}
		}

		public void Release()
		{
			_system.release();
		}
	}
}