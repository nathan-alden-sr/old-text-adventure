using System;
using System.Runtime.InteropServices;

using FMOD;

using Junior.Common;

namespace TextAdventure.WindowsGame.Fmod
{
	public class Sound : IDisposable
	{
		private readonly FMOD.Sound _sound;
		private readonly SoundSystem _soundSystem;
		private Channel _channel;

		public Sound(SoundSystem soundSystem, byte[] data)
		{
			soundSystem.ThrowIfNull("soundSystem");
			data.ThrowIfNull("data");

			var createsoundexinfo = new CREATESOUNDEXINFO
			                        	{
			                        		cbsize = Marshal.SizeOf(typeof(CREATESOUNDEXINFO)),
			                        		length = (uint)data.Length
			                        	};
			// ReSharper disable BitwiseOperatorOnEnumWihtoutFlags
			RESULT result = soundSystem.System.createSound(data, MODE.HARDWARE | MODE.OPENMEMORY, ref createsoundexinfo, ref _sound);
			// ReSharper restore BitwiseOperatorOnEnumWihtoutFlags

			if (result != RESULT.OK)
			{
				throw new Exception("Failed to create FMOD sound.");
			}

			_soundSystem = soundSystem;
		}

		public void Play()
		{
			RESULT result = _soundSystem.System.playSound(CHANNELINDEX.FREE, _sound, false, ref _channel);

			if (result != RESULT.OK)
			{
				throw new Exception("Failed to play sound.");
			}
		}

		public void Stop()
		{
			if (_channel != null)
			{
				_channel.stop();
			}
		}

		public void Dispose()
		{
			_sound.release();
		}
	}
}