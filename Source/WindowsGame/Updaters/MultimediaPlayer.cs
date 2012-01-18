using System;

using Junior.Common;

using TextAdventure.Engine.Game.Commands;
using TextAdventure.Engine.Game.World;
using TextAdventure.WindowsGame.Configuration;
using TextAdventure.WindowsGame.Managers;

namespace TextAdventure.WindowsGame.Updaters
{
	public class MultimediaPlayer : IMultimediaPlayer, IDisposable
	{
		private readonly SongManager _songManager = new SongManager();
		private readonly SoundEffectManager _soundEffectManager = new SoundEffectManager();
		private readonly IVolumeConfiguration _volumeConfiguration;

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

		public void PlaySoundEffect(Guid id, byte[] data, SoundParameters parameters)
		{
			data.ThrowIfNull("data");

			var adjustedParameters = new SoundParameters(parameters.Volume * _volumeConfiguration.SoundEffects);

			_soundEffectManager.Play(id, data, adjustedParameters);
		}

		public void PlaySong(Guid id, byte[] data, SoundParameters parameters)
		{
			data.ThrowIfNull("data");

			var adjustedParameters = new SoundParameters(parameters.Volume * _volumeConfiguration.Music);

			_songManager.Play(id, data, adjustedParameters);
		}

		public void StopSong()
		{
			_songManager.Stop();
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
			}
		}
	}
}