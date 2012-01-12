using System.Collections.ObjectModel;
using System.Linq;

using Junior.Common;

using Microsoft.Xna.Framework;

namespace TextAdventure.WindowsGame.Updaters
{
	public class UpdaterCollection
	{
		private readonly Collection<IUpdater> _updaters = new Collection<IUpdater>();

		public void Add(IUpdater updater)
		{
			updater.ThrowIfNull("updater");

			_updaters.Add(updater);
		}

		public void Remove(IUpdater updater)
		{
			updater.ThrowIfNull("updater");

			_updaters.Remove(updater);
		}

		public void Update(GameTime gameTime, Focus focus)
		{
			gameTime.ThrowIfNull("gameTime");

			foreach (IUpdater updater in _updaters.ToArray())
			{
				updater.Update(new UpdaterParameters(gameTime, focus));
			}
		}

		private class UpdaterParameters : IUpdaterParameters
		{
			private readonly Focus _focus;
			private readonly GameTime _gameTime;

			public UpdaterParameters(GameTime gameTime, Focus focus)
			{
				_gameTime = gameTime;
				_focus = focus;
			}

			public GameTime GameTime
			{
				get
				{
					return _gameTime;
				}
			}

			public Focus Focus
			{
				get
				{
					return _focus;
				}
			}
		}
	}
}