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

			var updaterParameters = new UpdaterParameters(gameTime, focus);

			// Must call ToArray() because collection could be modified during iteration
			foreach (IUpdater updater in _updaters.ToArray())
			{
				updater.Update(updaterParameters);
			}
		}
	}
}