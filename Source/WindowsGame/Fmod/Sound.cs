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
				throw new Exception(GetExceptionMessage("Failed to create FMOD sound.", result));
			}

			_soundSystem = soundSystem;
		}

		public void Dispose()
		{
			_sound.release();
		}

		public void Play(SoundParameters parameters)
		{
			RESULT result = _soundSystem.System.playSound(CHANNELINDEX.FREE, _sound, true, ref _channel);

			_channel.setVolume(parameters.Volume);
			_channel.setMute(parameters.Muted);
			_channel.setPaused(false);

			if (result != RESULT.OK)
			{
				throw new Exception(GetExceptionMessage("Failed to play sound.", result));
			}
		}

		public void Stop()
		{
			if (_channel != null)
			{
				_channel.stop();
			}
		}

		public void Mute()
		{
			if (_channel != null)
			{
				_channel.setMute(true);
			}
		}

		public void Unmute()
		{
			if (_channel != null)
			{
				_channel.setMute(false);
			}
		}

		private static string GetExceptionMessage(string message, RESULT result)
		{
			return String.Format("{0}{1}{1}Error {2}", message, Environment.NewLine, result);
		}
	}
}