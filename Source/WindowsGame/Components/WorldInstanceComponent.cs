using Junior.Common;

using Microsoft.Xna.Framework;

using TextAdventure.Engine.Game.World;
using TextAdventure.WindowsGame.Managers;

namespace TextAdventure.WindowsGame.Components
{
	public class WorldInstanceComponent : TextAdventureGameComponent
	{
		private readonly IWorldInstance _worldInstance;

		public WorldInstanceComponent(GameManager gameManager, IWorldInstance worldInstance)
			: base(gameManager)
		{
			worldInstance.ThrowIfNull("worldInstance");

			_worldInstance = worldInstance;

			UpdateOrder = ComponentUpdateOrder.WorldInstance;
		}

		public override void Update(GameTime gameTime)
		{
			_worldInstance.ProcessCommandQueue();

			base.Update(gameTime);
		}
	}
}