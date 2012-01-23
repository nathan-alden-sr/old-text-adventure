using System.Collections.ObjectModel;
using System.Linq;

using Junior.Common;

using TextAdventure.Xna;

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

		public void Update(IXnaGameTime gameTime, Focus focus)
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
			private readonly IXnaGameTime _gameTime;

			public UpdaterParameters(IXnaGameTime gameTime, Focus focus)
			{
				_gameTime = gameTime;
				_focus = focus;
			}

			public IXnaGameTime GameTime
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