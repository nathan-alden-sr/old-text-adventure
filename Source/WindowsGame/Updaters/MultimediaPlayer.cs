using System;

using Junior.Common;

using TextAdventure.Engine.Game.World;
using TextAdventure.WindowsGame.Managers;

namespace TextAdventure.WindowsGame.Updaters
{
	public class MultimediaPlayer : IMultimediaPlayer, IDisposable
	{
		private readonly SongManager _songManager = new SongManager();
		private readonly SoundEffectManager _soundEffectManager = new SoundEffectManager();

		public void Dispose()
		{
			OnDispose(true);
			GC.SuppressFinalize(this);
		}

		public void PlaySoundEffect(Guid id, byte[] data)
		{
			data.ThrowIfNull("data");

			_soundEffectManager.Play(id, data);
		}

		public void PlaySong(Guid id, byte[] data)
		{
			data.ThrowIfNull("data");

			_songManager.Play(id, data);
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