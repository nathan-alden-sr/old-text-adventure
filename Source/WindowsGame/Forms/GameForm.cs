using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

using Junior.Common;

using TextAdventure.Engine.Objects;
using TextAdventure.WindowsGame.Configuration;
using TextAdventure.WindowsGame.Helpers;
using TextAdventure.WindowsGame.Updaters;

namespace TextAdventure.WindowsGame.Forms
{
	public partial class GameForm : Form
	{
		private static readonly FpsConfigurationSection _fpsConfigurationSection = (FpsConfigurationSection)ConfigurationManager.GetSection("fps");
		private static readonly LogConfigurationSection _logConfigurationSection = (LogConfigurationSection)ConfigurationManager.GetSection("log");
		private static readonly VolumeConfigurationSection _volumeConfigurationSection = (VolumeConfigurationSection)ConfigurationManager.GetSection("volume");
		private static readonly WorldTimeConfigurationSection _worldTimeConfigurationSection = (WorldTimeConfigurationSection)ConfigurationManager.GetSection("worldTime");
		private readonly MultimediaPlayer _multimediaPlayer = new MultimediaPlayer(_volumeConfigurationSection);
		private TextAdventureGame _game;
		private Player _startingPlayer;
		private Engine.Objects.World _world;

		public GameForm(Engine.Objects.World world, Player startingPlayer)
		{
			world.ThrowIfNull("world");
			startingPlayer.ThrowIfNull("startingPlayer");

			_world = world;
			_startingPlayer = startingPlayer;
			_multimediaPlayer = new MultimediaPlayer(_volumeConfigurationSection);

			InitializeComponent();
			SetNormalViewSize();

			fpsToolStripMenuItem.Checked = _fpsConfigurationSection.Visible;
			logToolStripMenuItem.Checked = _logConfigurationSection.Visible;
			worldTimeToolStripMenuItem.Checked = _worldTimeConfigurationSection.Visible;
			soundEffectsToolStripMenuItem.Checked = true;
			musicToolStripMenuItem.Checked = true;
		}

		private bool GameRunning
		{
			get
			{
				return _game != null;
			}
		}

		public void Render()
		{
			if (GameRunning)
			{
				_game.Tick();
			}
		}

		private void SetNormalViewSize()
		{
			Size = new Size(
				Width + (Constants.GameWindow.PreferredBackBufferWidth - xnaControl.Size.Width),
				Height + (Constants.GameWindow.PreferredBackBufferHeight - xnaControl.Size.Height));
		}

		private TextAdventureGame CreateGame(Engine.Objects.World world, Player startingPlayer)
		{
			return new TextAdventureGame(
				xnaControl.GraphicsDevice,
				world,
				startingPlayer,
				_multimediaPlayer,
				_fpsConfigurationSection,
				_logConfigurationSection,
				_worldTimeConfigurationSection);
		}

		private void OpenGame(string path)
		{
			try
			{
				Engine.Objects.World world = WorldLoader.Instance.FromAssembly(path);

				LoadGame(CreateGame(world, world.StartingPlayer), world);
			}
			catch (Exception exception)
			{
				MessageBoxHelper.Instance.ShowError(this, String.Format("Error opening game:{0}{0}{1}", Environment.NewLine, exception.Message));
			}
		}

		private void CloseGame()
		{
			if (!GameRunning)
			{
				return;
			}

			_game.Dispose();
			_game = null;
			_multimediaPlayer.Reset();
			xnaControl.Refresh();
		}

		private void LoadGame(TextAdventureGame game, Engine.Objects.World world)
		{
			CloseGame();

			Text = world.Title + " - Text Adventure";
			_game = game;
			_game.Run();
		}

		protected override void OnShown(EventArgs e)
		{
			SetNormalViewSize();
			xnaControl.CreateGraphicsDevice();
			LoadGame(CreateGame(_world, _startingPlayer), _world);
			_world = null;
			_startingPlayer = null;

			base.OnShown(e);
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			CloseGame();

			base.OnFormClosing(e);
		}

		protected override void OnResizeBegin(EventArgs e)
		{
			if (GameRunning)
			{
				_game.Pause();
			}

			base.OnResizeBegin(e);
		}

		protected override void OnResizeEnd(EventArgs e)
		{
			if (GameRunning)
			{
				_game.Unpause();
			}

			base.OnResizeEnd(e);
		}

		private void MenuStripOnMenuActivate(object sender, EventArgs e)
		{
			if (GameRunning)
			{
				_game.Pause();
			}

			fpsToolStripMenuItem.Enabled = GameRunning;
			logToolStripMenuItem.Enabled = GameRunning;
			worldTimeToolStripMenuItem.Enabled = GameRunning;
			closeToolStripMenuItem.Enabled = GameRunning;
			soundEffectsToolStripMenuItem.Enabled = GameRunning;
			musicToolStripMenuItem.Enabled = GameRunning;
		}

		private void MenuStripOnMenuDeactivate(object sender, EventArgs e)
		{
			if (GameRunning)
			{
				_game.Unpause();
			}
		}

		private void OpenToolStripMenuItemOnClick(object sender, EventArgs e)
		{
			if (GameRunning)
			{
				_game.Pause();
			}

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				OpenGame(openFileDialog.FileName);
			}
			else if (GameRunning)
			{
				_game.Unpause();
			}
		}

		private void CloseToolStripMenuItemOnClick(object sender, EventArgs e)
		{
			CloseGame();
		}

		private void ExitToolStripMenuItemOnClick(object sender, EventArgs e)
		{
			Close();
		}

		private void NormalSizeToolStripMenuItemOnClick(object sender, EventArgs e)
		{
			SetNormalViewSize();
		}

		private void FpsToolStripMenuItemOnCheckedChanged(object sender, EventArgs e)
		{
			_fpsConfigurationSection.Visible = fpsToolStripMenuItem.Checked;
		}

		private void LogToolStripMenuItemOnClick(object sender, EventArgs e)
		{
			_logConfigurationSection.Visible = logToolStripMenuItem.Checked;
		}

		private void WorldTimeToolStripMenuItemOnClick(object sender, EventArgs e)
		{
			_worldTimeConfigurationSection.Visible = worldTimeToolStripMenuItem.Checked;
		}

		private void SoundEffectsToolStripMenuItemOnCheckedChanged(object sender, EventArgs e)
		{
			if (_multimediaPlayer == null)
			{
				return;
			}
			if (soundEffectsToolStripMenuItem.Checked)
			{
				_multimediaPlayer.UnmuteSoundEffects();
			}
			else
			{
				_multimediaPlayer.MuteSoundEffects();
			}
		}

		private void MusicToolStripMenuItemOnCheckedChanged(object sender, EventArgs e)
		{
			if (_multimediaPlayer == null)
			{
				return;
			}
			if (musicToolStripMenuItem.Checked)
			{
				_multimediaPlayer.UnmuteSongs();
			}
			else
			{
				_multimediaPlayer.MuteSongs();
			}
		}
	}
}