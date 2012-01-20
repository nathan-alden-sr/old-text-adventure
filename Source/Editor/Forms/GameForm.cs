using System;
using System.Linq;
using System.Windows.Forms;

using TextAdventure.Editor.Objects;
using TextAdventure.Editor.RendererStates;
using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Editor.Forms
{
	public partial class GameForm : Form
	{
		private readonly BoardRendererState _boardRendererState = new BoardRendererState();
		private TextAdventureEditorGame _game;
		private bool _resizing;

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
			if (GameRunning && !_resizing)
			{
				_game.Tick();
			}
		}

		private void CreateGame(World world)
		{
			_boardRendererState.Board = world.Boards.FirstOrDefault();
			_boardRendererState.ScrollCoordinate = Coordinate.Zero;

			_game = new TextAdventureEditorGame(xnaControl.GraphicsDevice, _boardRendererState);
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

		protected override void OnResizeBegin(EventArgs e)
		{
			_resizing = true;

			base.OnResizeBegin(e);
		}

		protected override void OnResizeEnd(EventArgs e)
		{
			_resizing = false;

			base.OnResizeEnd(e);
		}
	}
}