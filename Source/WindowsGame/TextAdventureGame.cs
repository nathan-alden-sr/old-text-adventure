using Junior.Common;

using Microsoft.Xna.Framework;

using TextAdventure.Engine.Game.World;
using TextAdventure.Engine.Objects;
using TextAdventure.WindowsGame.Components;
using TextAdventure.WindowsGame.Managers;

namespace TextAdventure.WindowsGame
{
	public class TextAdventureGame : Game
	{
		private readonly GraphicsDeviceManager _graphics;
		private readonly Player _player;
		private readonly World _world;
		private GameManager _gameManager;
		private WorldInstance _worldInstance;

		public TextAdventureGame(World world, Player player)
		{
			world.ThrowIfNull("world");
			player.ThrowIfNull("player");

			_world = world;
			_player = player;
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsFixedTimeStep = false;
		}

		protected override void Initialize()
		{
			InitializeWindow();
			InitializeGameManager();
			AddGameComponents();

			base.Initialize();
		}

		private void InitializeWindow()
		{
			Window.Title = "Text Adventure";

			_graphics.PreferredBackBufferWidth = DrawingConstants.GameWindow.PreferredBackBufferWidth;
			_graphics.PreferredBackBufferHeight = DrawingConstants.GameWindow.PreferredBackBufferHeight;
			_graphics.IsFullScreen = false;
			_graphics.ApplyChanges();
		}

		private void InitializeGameManager()
		{
			_gameManager = new GameManager(this);
		}

		private void AddGameComponents()
		{
			var worldTimeComponent = new WorldTimeComponent(_gameManager);
			var worldObserverComponent = new WorldObserverComponent(_gameManager);

			Components.Add(worldTimeComponent);
			Components.Add(worldObserverComponent);

			_worldInstance = new WorldInstance(_world, _player, worldTimeComponent, worldObserverComponent);

			Components.Add(new WorldInstanceComponent(_gameManager, _worldInstance));
			Components.Add(new GameBackgroundComponent(_gameManager));
			Components.Add(new BoardComponent(_gameManager, _worldInstance));
			Components.Add(new MessageComponent(_gameManager, _worldInstance));
			Components.Add(new FpsComponent(_gameManager));
		}
	}
}