using System;
using System.Collections.Generic;

using TextAdventure.WindowsGame.Fmod;

namespace TextAdventure.WindowsGame.Managers
{
	public class SongManager : IDisposable
	{
		private readonly Dictionary<Guid, Sound> _soundsById = new Dictionary<Guid, Sound>();

		public void Dispose()
		{
			OnDispose(true);
			GC.SuppressFinalize(this);
		}

		public void Play(Guid id, byte[] data, SoundParameters parameters)
		{
			foreach (Sound existingSound in _soundsById.Values)
			{
				existingSound.Stop();
			}

			Sound sound;

			if (!_soundsById.TryGetValue(id, out sound))
			{
				sound = new Sound(SoundSystem.Instance, data);
				_soundsById.Add(id, sound);
			}

			sound.Play(parameters);
		}

		public void Stop()
		{
			foreach (Sound sound in _soundsById.Values)
			{
				sound.Stop();
			}
		}

		public void Mute()
		{
			foreach (Sound sound in _soundsById.Values)
			{
				sound.Mute();
			}
		}

		public void Unmute()
		{
			foreach (Sound sound in _soundsById.Values)
			{
				sound.Unmute();
			}
		}

		public void Reset()
		{
			DisposeAllSounds();
			_soundsById.Clear();
		}

		protected virtual void OnDispose(bool disposing)
		{
			if (disposing)
			{
				DisposeAllSounds();
			}
		}

		private void DisposeAllSounds()
		{
			foreach (Sound sound in _soundsById.Values)
			{
				sound.Dispose();
			}
		}
	}
}