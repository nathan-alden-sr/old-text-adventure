using System;
using System.Collections.Generic;
using System.IO;

using Microsoft.Xna.Framework.Audio;

namespace TextAdventure.WindowsGame.Managers
{
	public class SoundEffectManager : IDisposable
	{
		private readonly Dictionary<Guid, SoundEffectInstanceManager> _soundEffectInstanceManagersById = new Dictionary<Guid, SoundEffectInstanceManager>();

		public void Dispose()
		{
			OnDispose(true);
			GC.SuppressFinalize(this);
		}

		public void Play(Guid id, byte[] data)
		{
			SoundEffectInstanceManager manager;

			if (!_soundEffectInstanceManagersById.TryGetValue(id, out manager))
			{
				manager = new SoundEffectInstanceManager(data);
				_soundEffectInstanceManagersById.Add(id, manager);
			}

			SoundEffectInstance instance = manager.Next();

			instance.Play();
		}

		protected virtual void OnDispose(bool disposing)
		{
			if (disposing)
			{
				foreach (SoundEffectInstanceManager manager in _soundEffectInstanceManagersById.Values)
				{
					manager.Dispose();
				}
			}
		}

		private class SoundEffectInstanceManager : IDisposable
		{
			private const int InstanceCount = 16;
			private readonly SoundEffectInstance[] _instances = new SoundEffectInstance[InstanceCount];
			private readonly SoundEffect _soundEffect;
			private int _index = -1;

			public SoundEffectInstanceManager(byte[] data)
			{
				for (int i = 0; i < InstanceCount; i++)
				{
					_soundEffect = SoundEffect.FromStream(new MemoryStream(data));
					_instances[i] = _soundEffect.CreateInstance();
				}
			}

			public void Dispose()
			{
				_soundEffect.Dispose();
			}

			public SoundEffectInstance Next()
			{
				if (++_index == InstanceCount)
				{
					_index = 0;
				}

				SoundEffectInstance instance = _instances[_index];

				instance.Stop();

				return instance;
			}
		}
	}
}