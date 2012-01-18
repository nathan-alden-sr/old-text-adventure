using System;

using TextAdventure.Engine.Game.World;
using TextAdventure.WindowsGame.Managers;

namespace TextAdventure.WindowsGame.Updaters
{
	public class MultimediaPlayer : IMultimediaPlayer, IDisposable
	{
		private readonly SoundEffectManager _soundEffectManager = new SoundEffectManager();

		public void Dispose()
		{
			OnDispose(true);
			GC.SuppressFinalize(this);
		}

		public void PlaySoundEffect(Guid id, byte[] data)
		{
			_soundEffectManager.Play(id, data);
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