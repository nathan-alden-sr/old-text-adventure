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
				throw new Exception(GetExceptionMessage("Failed to create FMOD system.", result));
			}

			uint version = 0;

			result = System.getVersion(ref version);

			if (result != RESULT.OK || version < VERSION.number)
			{
				throw new Exception(GetExceptionMessage("Failed to create FMOD system.", result));
			}

			result = System.init(32, INITFLAGS.NORMAL, IntPtr.Zero);

			if (result != RESULT.OK)
			{
				throw new Exception(GetExceptionMessage("Failed to create FMOD system.", result));
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

		private static string GetExceptionMessage(string message, RESULT result)
		{
			return String.Format("{0}{1}{1}Error {2}", message, Environment.NewLine, result);
		}
	}
}