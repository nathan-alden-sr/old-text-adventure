using System;

using Junior.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TextAdventure.Engine.Game.World;
using TextAdventure.Engine.Objects;
using TextAdventure.WindowsGame.Configuration;
using TextAdventure.WindowsGame.Managers;
using TextAdventure.WindowsGame.RendererStates;
using TextAdventure.WindowsGame.Renderers;
using TextAdventure.WindowsGame.Updaters;
using TextAdventure.WindowsGame.World;
using TextAdventure.Xna;

namespace TextAdventure.WindowsGame.Xna
{
	public class TextAdventureGame : TimedXnaGame
	{
		private readonly IFpsConfiguration _fpsConfiguration;
		private readonly InputManager _inputManager = new InputManager();
		private readonly ILogConfiguration _logConfiguration;
		private readonly IMultimediaPlayer _multimediaPlayer;
		private readonly Player _player;
		private readonly RendererCollection _rendererCollection = new RendererCollection();
		private readonly UpdaterCollection _updaterCollection = new UpdaterCollection();
		private readonly Engine.Objects.World _world;
		private readonly IWorldTimeConfiguration _worldTimeConfiguration;
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

		public TextAdventureGame(
			GraphicsDevice graphicsDevice,
			Engine.Objects.World world,
			Player player,
			IMultimediaPlayer multimediaPlayer,
			IFpsConfiguration fpsConfiguration,
			ILogConfiguration logConfiguration,
			IWorldTimeConfiguration worldTimeConfiguration)
			: base(graphicsDevice, new ContentDirectoryContentManagerProvider())
		{
			world.ThrowIfNull("world");
			player.ThrowIfNull("player");
			multimediaPlayer.ThrowIfNull("multimediaPlayer");
			fpsConfiguration.ThrowIfNull("fpsConfiguration");
			logConfiguration.ThrowIfNull("logConfiguration");
			worldTimeConfiguration.ThrowIfNull("worldTimeConfiguration");

			_world = world;
			_player = player;
			_multimediaPlayer = multimediaPlayer;
			_fpsConfiguration = fpsConfiguration;
			_logConfiguration = logConfiguration;
			_worldTimeConfiguration = worldTimeConfiguration;
		}

		public float WorldSpeed
		{
			get
			{
				return _worldTimeRendererState.Speed;
			}
		}

		public bool WorldPaused
		{
			get
			{
				return _worldTimeRendererState.Paused;
			}
		}

		public void PauseWorld()
		{
			_worldTimeRendererState.Pause();
		}

		public void UnpauseWorld()
		{
			_worldTimeRendererState.Unpause();
		}

		public void SpeedUpWorld()
		{
			_worldTimeRendererState.SpeedUp();
		}

		public void SlowDownWorld()
		{
			_worldTimeRendererState.SlowDown();
		}

		public void ClearLog()
		{
			_logRendererState.ClearLog();
		}

		protected override void Initialize()
		{
			base.Initialize();

			CreateRendererStates();
			AddRenderers();
			AddUpdaters();

			_inputManager.ClaimFocus(Focus.Player);
		}

		protected override void LoadContent()
		{
			_fontContent = new FontContent(Content);
			_textureContent = new TextureContent(Content);
		}

		protected override void Update(IXnaGameTime gameTime)
		{
			gameTime.ThrowIfNull("gameTime");

			ProcessWorldInstanceCommandQueue();

			UpdateFpsRendererState(gameTime.ElapsedGameTime);
			UpdateWorldTimeRendererState(gameTime);
			UpdateLogRendererState(gameTime);
			UpdateBoardRendererState();
			ProcessMessage(gameTime.TotalGameTime);

			_updaterCollection.Update(gameTime, _inputManager.Focus);
		}

		protected override void Draw(IXnaGameTime gameTime)
		{
			gameTime.ThrowIfNull("gameTime");

			var spriteBatch = new SpriteBatch(GraphicsDevice);

			_rendererCollection.Render(spriteBatch, gameTime, _fontContent, _textureContent);

			spriteBatch.Dispose();
		}

		protected override void Present()
		{
			var sourceRectangle = new Rectangle(0, 0, Constants.GameWindow.Width, Constants.GameWindow.Height);

			GraphicsDevice.Present(sourceRectangle, null, IntPtr.Zero);
		}

		private void CreateRendererStates()
		{
			_logRendererState = new LogRendererState
			                    	{
			                    		Visible = _logConfiguration.Visible,
			                    		MaximumVisibleLogLines = _logConfiguration.MaximumVisibleLogLines,
			                    		MinimumWindowWidth = _logConfiguration.MinimumWindowWidth,
			                    		LogEntryLifetime = _logConfiguration.LogEntryLifetime,
			                    		ShowTimestamps = _logConfiguration.ShowTimestamps,
										ShowRaisingEvents = _logConfiguration.ShowRaisingEvents
			                    	};
			_worldTimeRendererState = new WorldTimeRendererState
			                          	{
			                          		Visible = _worldTimeConfiguration.Visible
			                          	};

			var worldTime = new WorldTime(_worldTimeRendererState);
			var worldObserver = new WorldObserver(worldTime, _logRendererState);

			_worldInstance = new WorldInstance(_world, _player, worldTime, worldObserver, _multimediaPlayer);

			_boardRendererState = new BoardRendererState(_worldInstance.Player);
			_fpsRendererState = new FpsRendererState
			                    	{
			                    		Visible = _fpsConfiguration.Visible
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
			_updaterCollection.Add(new WorldTimeInputHandler(_worldTimeRendererState));
			_updaterCollection.Add(new PlayerInputHandler(_worldInstance));
		}

		private void ProcessWorldInstanceCommandQueue()
		{
			_worldInstance.ProcessCommandQueue();
		}

		#region Update states

		private void UpdateFpsRendererState(TimeSpan elapsedGameTime)
		{
			_fpsRendererState.Visible = _fpsConfiguration.Visible;
			_fpsRendererState.FrameRendered();
			_fpsRendererState.UpdateElapsedGameTime(elapsedGameTime);
			_fpsRendererState.UpdateFrameCount();
		}

		private void UpdateWorldTimeRendererState(IXnaGameTime gameTime)
		{
			_worldTimeRendererState.Visible = _worldTimeConfiguration.Visible;
			_worldTimeRendererState.UpdateWorldTimes(gameTime);
		}

		private void UpdateLogRendererState(IXnaGameTime gameTime)
		{
			_logRendererState.Visible = _logConfiguration.Visible;
			_logRendererState.DequeueOldLogEntries(gameTime);
		}

		private void UpdateBoardRendererState()
		{
			_boardRendererState.Board = _worldInstance.CurrentBoard;
		}

		#endregion

		#region Messages

		private void ProcessMessage(TimeSpan totalGameTime)
		{
			bool processingMessage = _messageRenderer != null;
			bool messageAvailable = !_worldInstance.WorldTime.Paused && _worldInstance.MessageMananger.Count > 0;

			if (processingMessage || !messageAvailable)
			{
				return;
			}

			_messageRendererState = new MessageRendererState
			                        	{
			                        		Message = _worldInstance.MessageMananger.DequeueMessage()
			                        	};
			_messageRenderer = new MessageRenderer(_messageRendererState);
			_messageFadeInAndScaleUpdater = new MessageFadeInAndScaleUpdater(_messageRendererState, totalGameTime, MessageOpened);

			_rendererCollection.Add(_messageRenderer);
			_updaterCollection.Add(_messageFadeInAndScaleUpdater);

			_inputManager.ClaimFocus(Focus.Message);
		}

		private void MessageOpened(IXnaGameTime gameTime)
		{
			_updaterCollection.Remove(_messageFadeInAndScaleUpdater);
			_messageFadeInAndScaleUpdater = null;

			_messageInputHandler = new MessageInputHandler(_worldInstance, _messageRendererState, gameTime.TotalGameTime, MessageClosing);
			_updaterCollection.Add(_messageInputHandler);

			_worldInstance.MessageMananger.MessageOpened(_messageRendererState.Message);
		}

		private void MessageClosing(IXnaGameTime gameTime)
		{
			_updaterCollection.Remove(_messageInputHandler);
			_messageInputHandler = null;

			_messageFadeOutAndScaleUpdater = new MessageFadeOutAndScaleUpdater(_messageRendererState, gameTime.TotalGameTime, MessageClosed);
			_updaterCollection.Add(_messageFadeOutAndScaleUpdater);
		}

		private void MessageClosed()
		{
			IMessage message = _messageRendererState.Message;

			_updaterCollection.Remove(_messageFadeOutAndScaleUpdater);
			_rendererCollection.Remove(_messageRenderer);
			_messageFadeOutAndScaleUpdater = null;
			_messageRenderer = null;
			_messageRendererState = null;

			_inputManager.RelinquishFocus();

			_worldInstance.MessageMananger.MessageClosed(message);
		}

		#endregion
	}
}