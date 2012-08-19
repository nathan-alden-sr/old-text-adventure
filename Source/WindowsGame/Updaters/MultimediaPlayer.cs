using System;

using Junior.Common;

using TextAdventure.Engine.Game.Commands;
using TextAdventure.Engine.Game.World;
using TextAdventure.WindowsGame.Configuration;
using TextAdventure.WindowsGame.Fmod;
using TextAdventure.WindowsGame.Managers;

namespace TextAdventure.WindowsGame.Updaters
{
	public class MultimediaPlayer : IMultimediaPlayer, IDisposable
	{
		private readonly SongManager _songManager = new SongManager();
		private readonly SoundEffectManager _soundEffectManager = new SoundEffectManager();
		private readonly IVolumeConfiguration _volumeConfiguration;
		private bool _songsMuted;
		private bool _soundEffectsMuted;

		public MultimediaPlayer(IVolumeConfiguration volumeConfiguration)
		{
			volumeConfiguration.ThrowIfNull("volumeConfiguration");

			_volumeConfiguration = volumeConfiguration;
		}

		public void Dispose()
		{
			OnDispose(true);
			GC.SuppressFinalize(this);
		}

		public void PlaySoundEffect(Guid id, byte[] data, Volume volume)
		{
			data.ThrowIfNull("data");

			var adjustedParameters = new SoundParameters(volume * _volumeConfiguration.SoundEffects, _soundEffectsMuted);

			_soundEffectManager.Play(id, data, adjustedParameters);
		}

		public void PlaySong(Guid id, byte[] data, Volume volume)
		{
			data.ThrowIfNull("data");

			var adjustedParameters = new SoundParameters(volume * _volumeConfiguration.Music, _songsMuted);

			_songManager.Play(id, data, adjustedParameters);
		}

		public void StopSong()
		{
			_songManager.Stop();
		}

		public void MuteSoundEffects()
		{
			_soundEffectsMuted = true;
			_soundEffectManager.Mute();
		}

		public void MuteSongs()
		{
			_songsMuted = true;
			_songManager.Mute();
		}

		public void UnmuteSoundEffects()
		{
			_soundEffectsMuted = false;
			_soundEffectManager.Unmute();
		}

		public void UnmuteSongs()
		{
			_songsMuted = false;
			_songManager.Unmute();
		}

		public void Reset()
		{
			_soundEffectManager.Reset();
			_songManager.Reset();
		}

		~MultimediaPlayer()
		{
			OnDispose(false);
		}

		protected virtual void OnDispose(bool disposing)
		{
			if (disposing)
			{
				_soundEffectManager.Dispose();
				_songManager.Dispose();
			}
		}
	}
}