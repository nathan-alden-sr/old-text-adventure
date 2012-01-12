using System;
using System.Configuration;

using Junior.Common;

using Microsoft.Xna.Framework;

using TextAdventure.Engine.Game.World;
using TextAdventure.Engine.Objects;
using TextAdventure.WindowsGame.Configuration;
using TextAdventure.WindowsGame.Managers;
using TextAdventure.WindowsGame.RendererStates;
using TextAdventure.WindowsGame.Renderers;
using TextAdventure.WindowsGame.Updaters;
using TextAdventure.WindowsGame.World;

namespace TextAdventure.WindowsGame
{
	public class TextAdventureGame : Game
	{
		private static readonly FpsConfigurationSection _fpsConfigurationSection = (FpsConfigurationSection)ConfigurationManager.GetSection("fps");
		private static readonly LogConfigurationSection _logConfigurationSection = (LogConfigurationSection)ConfigurationManager.GetSection("log");
		private static readonly TimeConfigurationSection _timeConfigurationSection = (TimeConfigurationSection)ConfigurationManager.GetSection("time");
		private readonly GraphicsDeviceManager _graphics;
		private readonly InputManager _inputManager = new InputManager();
		private readonly Player _player;
		private readonly RendererCollection _rendererCollection = new RendererCollection();
		private readonly UpdaterCollection _updaterCollection = new UpdaterCollection();
		private readonly Engine.Objects.World _world;
		private BoardRendererState _boardRendererState;
		private FontContent _fontContent;
		private FpsRendererState _fpsRendererState;
		private LogRendererState _logRendererState;
		private MessageFadeInAndScaleUpdater _messageFadeInAndScaleUpdater;
		private MessageFadeOutAndScaleUpdater _messageFadeOutAndScaleUpdater;
		private MessageInputHandler _messageInputHandler;
		private MessageRenderer _messageRenderer;
		private MessageRendererState _messageRendererState;
		private TextureContent _textureContent;
		private WorldInstance _worldInstance;
		private WorldTimeRendererState _worldTimeRendererState;

		public TextAdventureGame(Engine.Objects.World world, Player player)
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
			CreateRendererStates();
			AddRenderers();
			AddUpdaters();

			_inputManager.ClaimFocus(Focus.Player);

			base.Initialize();
		}

		protected override void LoadContent()
		{
			_fontContent = new FontContent(Content);
			_textureContent = new TextureContent(Content);

			base.LoadContent();
		}

		protected override void Update(GameTime gameTime)
		{
			ProcessWorldInstanceCommandQueue();

			UpdateFpsRendererState(gameTime.ElapsedGameTime);
			UpdateWorldTimeRendererState(gameTime);
			UpdateLogRendererState(gameTime);
			UpdateBoardRendererState();
			ProcessMessage(gameTime.TotalGameTime);

			_updaterCollection.Update(gameTime, _inputManager.Focus);

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			_rendererCollection.Render(GraphicsDevice, gameTime, _fontContent, _textureContent);

			base.Draw(gameTime);
		}

		private void InitializeWindow()
		{
			Window.Title = "Text Adventure";

			_graphics.PreferredBackBufferWidth = Constants.GameWindow.PreferredBackBufferWidth;
			_graphics.PreferredBackBufferHeight = Constants.GameWindow.PreferredBackBufferHeight;
			_graphics.IsFullScreen = false;
			_graphics.ApplyChanges();
		}

		private void CreateRendererStates()
		{
			_logRendererState = new LogRendererState
			                    	{
			                    		Visible = _logConfigurationSection.Visible,
			                    		MaximumVisibleLogLines = _logConfigurationSection.MaximumVisibleLogLines,
			                    		MinimumWindowWidth = _logConfigurationSection.MinimumWindowWidth,
			                    		LogEntryLifetime = _logConfigurationSection.LogEntryLifetime,
			                    		ShowTimestamps = _logConfigurationSection.ShowTimestamps
			                    	};
			_worldTimeRendererState = new WorldTimeRendererState
			                          	{
			                          		Visible = _timeConfigurationSection.Visible
			                          	};

			var worldTime = new WorldTime(_worldTimeRendererState);
			var worldObserver = new WorldObserver(worldTime, _logRendererState);

			_worldInstance = new WorldInstance(_world, _player, worldTime, worldObserver);

			_boardRendererState = new BoardRendererState(_worldInstance.Player);
			_fpsRendererState = new FpsRendererState
			                    	{
			                    		Visible = _fpsConfigurationSection.Visible
			                    	};
		}

		private void AddRenderers()
		{
			_rendererCollection.Add(new GameBackgroundRenderer());
			_rendererCollection.Add(new BoardRenderer(_boardRendererState));
			_rendererCollection.Add(new PlayerRenderer());
			_rendererCollection.Add(new FpsRenderer(_fpsRendererState));
			_rendererCollection.Add(new WorldTimeRenderer(_worldTimeRendererState));
			_rendererCollection.Add(new LogRenderer(_logRendererState));
		}

		private void AddUpdaters()
		{
			_updaterCollection.Add(new FpsInputHandler(_fpsRendererState));
			_updaterCollection.Add(new LogInputHandler(_logRendererState));
			_updaterCollection.Add(new WorldTimeInputHandler(_worldTimeRendererState));
			_updaterCollection.Add(new PlayerInputHandler(_worldInstance));
		}

		private void ProcessWorldInstanceCommandQueue()
		{
			_worldInstance.ProcessCommandQueue();
		}

		private void UpdateFpsRendererState(TimeSpan elapsedGameTime)
		{
			_fpsRendererState.FrameRendered();
			_fpsRendererState.UpdateElapsedGameTime(elapsedGameTime);
			_fpsRendererState.UpdateFrameCount();
		}

		private void UpdateWorldTimeRendererState(GameTime gameTime)
		{
			_worldTimeRendererState.UpdateWorldTimes(gameTime);
		}

		private void UpdateLogRendererState(GameTime gameTime)
		{
			_logRendererState.DequeueOldLogEntries(gameTime);
		}

		private void UpdateBoardRendererState()
		{
			_boardRendererState.Board = _worldInstance.CurrentBoard;
		}

		private void ProcessMessage(TimeSpan totalGameTime)
		{
			bool processingMessage = _messageRenderer != null;
			bool messageAvailable = !_worldInstance.WorldTime.Paused && _worldInstance.MessageQueue.Count > 0;

			if (processingMessage || !messageAvailable)
			{
				return;
			}

			_messageRendererState = new MessageRendererState
			                        	{
			                        		Message = _worldInstance.MessageQueue.DequeueMessage()
			                        	};
			_messageRenderer = new MessageRenderer(_messageRendererState);
			_messageFadeInAndScaleUpdater = new MessageFadeInAndScaleUpdater(_messageRendererState, totalGameTime, MessageOpened);

			_rendererCollection.Add(_messageRenderer);
			_updaterCollection.Add(_messageFadeInAndScaleUpdater);

			_inputManager.ClaimFocus(Focus.Message);
		}

		private void MessageOpened(GameTime gameTime)
		{
			_updaterCollection.Remove(_messageFadeInAndScaleUpdater);
			_messageFadeInAndScaleUpdater = null;

			_messageInputHandler = new MessageInputHandler(_worldInstance, _messageRendererState, gameTime.TotalGameTime, MessageClosing);
			_updaterCollection.Add(_messageInputHandler);
		}

		private void MessageClosing(GameTime gameTime)
		{
			_updaterCollection.Remove(_messageInputHandler);
			_messageInputHandler = null;

			_messageFadeOutAndScaleUpdater = new MessageFadeOutAndScaleUpdater(_messageRendererState, gameTime.TotalGameTime, MessageClosed);
			_updaterCollection.Add(_messageFadeOutAndScaleUpdater);
		}

		private void MessageClosed()
		{
			_updaterCollection.Remove(_messageFadeOutAndScaleUpdater);
			_rendererCollection.Remove(_messageRenderer);
			_messageFadeOutAndScaleUpdater = null;
			_messageRenderer = null;
			_messageRendererState = null;

			_inputManager.RelinquishFocus();
		}
	}
}