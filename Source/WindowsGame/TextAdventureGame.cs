using System;

using Junior.Common;

using Microsoft.Xna.Framework.Graphics;

using TextAdventure.Engine.Game.World;
using TextAdventure.Engine.Objects;
using TextAdventure.WindowsGame.Configuration;
using TextAdventure.WindowsGame.Managers;
using TextAdventure.WindowsGame.RendererStates;
using TextAdventure.WindowsGame.Renderers;
using TextAdventure.WindowsGame.Updaters;
using TextAdventure.WindowsGame.World;
using TextAdventure.WindowsGame.Xna;

namespace TextAdventure.WindowsGame
{
	public class TextAdventureGame : XnaGame
	{
		private readonly IFpsConfiguration _fpsConfiguration;
		private readonly InputManager _inputManager = new InputManager();
		private readonly ILogConfiguration _logConfiguration;
		private readonly MultimediaPlayer _multimediaPlayer = new MultimediaPlayer();
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
			IFpsConfiguration fpsConfiguration,
			ILogConfiguration logConfiguration,
			IWorldTimeConfiguration worldTimeConfiguration)
			: base(graphicsDevice)
		{
			world.ThrowIfNull("world");
			player.ThrowIfNull("player");
			fpsConfiguration.ThrowIfNull("fpsConfiguration");
			logConfiguration.ThrowIfNull("logConfiguration");
			worldTimeConfiguration.ThrowIfNull("worldTimeConfiguration");

			_world = world;
			_player = player;
			_fpsConfiguration = fpsConfiguration;
			_logConfiguration = logConfiguration;
			_worldTimeConfiguration = worldTimeConfiguration;
		}

		protected override void Initialize()
		{
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

		protected override void Update(XnaGameTime gameTime)
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

		protected override void Draw(XnaGameTime gameTime)
		{
			var spriteBatch = new SpriteBatch(GraphicsDevice);

			_rendererCollection.Render(spriteBatch, gameTime, _fontContent, _textureContent);

			spriteBatch.Dispose();

			base.Draw(gameTime);
		}

		protected override void OnDispose(bool disposing)
		{
			if (disposing)
			{
				_multimediaPlayer.Dispose();
			}

			base.OnDispose(disposing);
		}

		private void CreateRendererStates()
		{
			_logRendererState = new LogRendererState
			                    	{
			                    		Visible = _logConfiguration.Visible,
			                    		MaximumVisibleLogLines = _logConfiguration.MaximumVisibleLogLines,
			                    		MinimumWindowWidth = _logConfiguration.MinimumWindowWidth,
			                    		LogEntryLifetime = _logConfiguration.LogEntryLifetime,
			                    		ShowTimestamps = _logConfiguration.ShowTimestamps
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

		private void UpdateWorldTimeRendererState(XnaGameTime gameTime)
		{
			_worldTimeRendererState.Visible = _worldTimeConfiguration.Visible;
			_worldTimeRendererState.UpdateWorldTimes(gameTime);
		}

		private void UpdateLogRendererState(XnaGameTime gameTime)
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

		private void MessageOpened(XnaGameTime gameTime)
		{
			_updaterCollection.Remove(_messageFadeInAndScaleUpdater);
			_messageFadeInAndScaleUpdater = null;

			_messageInputHandler = new MessageInputHandler(_worldInstance, _messageRendererState, gameTime.TotalGameTime, MessageClosing);
			_updaterCollection.Add(_messageInputHandler);
		}

		private void MessageClosing(XnaGameTime gameTime)
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

		#endregion
	}
}