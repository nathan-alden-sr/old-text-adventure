using System;
using System.Windows.Forms;

using TextAdventure.Editor.Objects;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Editor.Forms
{
	public partial class GameForm : Form
	{
		private TextAdventureEditorGame _game;

		public GameForm()
		{
			InitializeComponent();
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

		private void CreateGame(World world)
		{
			_game = new TextAdventureEditorGame(xnaControl.GraphicsDevice, world);
			_game.Run();
		}

		protected override void OnShown(EventArgs e)
		{
			xnaControl.CreateGraphicsDevice();
			CreateGame(DefaultWorldFactory.Instance.CreateWorld());

			base.OnShown(e);
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			_game.Dispose();

			base.OnFormClosing(e);
		}
	}
}