using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

using Junior.Common;

using TextAdventure.Engine.Objects;
using TextAdventure.WindowsGame.Configuration;

namespace TextAdventure.WindowsGame.Forms
{
	public partial class GameForm : Form
	{
		private static readonly FpsConfigurationSection _fpsConfigurationSection = (FpsConfigurationSection)ConfigurationManager.GetSection("fps");
		private static readonly LogConfigurationSection _logConfigurationSection = (LogConfigurationSection)ConfigurationManager.GetSection("log");
		private static readonly WorldTimeConfigurationSection _worldTimeConfigurationSection = (WorldTimeConfigurationSection)ConfigurationManager.GetSection("worldTime");

		public GameForm(Engine.Objects.World world, Player startingPlayer)
		{
			world.ThrowIfNull("world");
			startingPlayer.ThrowIfNull("startingPlayer");

			InitializeComponent();
			SetNormalViewSize();

			xnaControl.CreateGraphicsDevice();

			fpsToolStripMenuItem.Checked = _fpsConfigurationSection.Visible;
			logToolStripMenuItem.Checked = _logConfigurationSection.Visible;
			worldTimeToolStripMenuItem.Checked = _worldTimeConfigurationSection.Visible;

			Game = new TextAdventureGame(xnaControl.GraphicsDevice, world, startingPlayer, _fpsConfigurationSection, _logConfigurationSection, _worldTimeConfigurationSection);
		}

		public bool CloseRequested
		{
			get;
			private set;
		}

		public TextAdventureGame Game
		{
			get;
			private set;
		}

		protected override void OnShown(EventArgs e)
		{
			SetNormalViewSize();
			xnaControl.CreateGraphicsDevice();

			base.OnShown(e);
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			CloseRequested = true;
			e.Cancel = true;

			base.OnFormClosing(e);
		}

		protected override void OnResizeBegin(EventArgs e)
		{
			Game.Pause();

			base.OnResizeBegin(e);
		}

		protected override void OnResizeEnd(EventArgs e)
		{
			Game.Unpause();

			base.OnResizeEnd(e);
		}

		private void SetNormalViewSize()
		{
			Size = new Size(
				Width + (Constants.GameWindow.PreferredBackBufferWidth - xnaControl.Size.Width),
				Height + (Constants.GameWindow.PreferredBackBufferHeight - xnaControl.Size.Height));
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

		private void MenuStripOnMenuActivate(object sender, EventArgs e)
		{
			Game.Pause();
		}

		private void MenuStripOnMenuDeactivate(object sender, EventArgs e)
		{
			Game.Unpause();
		}
	}
}