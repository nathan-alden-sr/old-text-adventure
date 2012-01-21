using System;
using System.Linq;
using System.Windows.Forms;

using Junior.Common;

using Microsoft.Xna.Framework;

using TextAdventure.Editor.RendererStates;
using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Editor.Forms
{
	public partial class GameForm : Form
	{
		private readonly BoardRendererState _boardRendererState = new BoardRendererState();
		private readonly EditorView _editorView = new EditorView();
		private readonly PencilRendererState _pencilRendererState = new PencilRendererState();
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
			_boardRendererState.Board = world.Boards.FirstOrDefault();

			RecalculateView();

			_game = new TextAdventureEditorGame(
				xnaControl.GraphicsDevice,
				_editorView,
				_boardRendererState,
				_pencilRendererState);
			_game.Run();
			xnaControl.DrawBackground = false;
		}

		private void RecalculateView()
		{
			const int tileWidth = TextAdventure.Xna.Constants.Tile.TileWidth;
			const int tileHeight = TextAdventure.Xna.Constants.Tile.TileHeight;
			int clientWidth = xnaControl.ClientSize.Width;
			int clientHeight = xnaControl.ClientSize.Height;
			int boardWidth = _boardRendererState.Board.IfNotNull(arg => (int?)arg.Size.Width) ?? 0;
			int boardHeight = _boardRendererState.Board.IfNotNull(arg => (int?)arg.Size.Height) ?? 0;
			var visibleTileWidth = (int)Math.Ceiling((clientWidth + (hScrollBar.Value % tileWidth)) / (double)tileWidth);
			var visibleTileHeight = (int)Math.Ceiling((clientHeight + (vScrollBar.Value % tileHeight)) / (double)tileHeight);

			hScrollBar.Maximum = boardWidth * tileWidth;
			hScrollBar.LargeChange = clientWidth;
			hScrollBar.SmallChange = tileWidth;
			hScrollBar.Enabled = clientWidth < boardWidth * tileWidth;

			vScrollBar.Maximum = boardHeight * tileHeight;
			vScrollBar.LargeChange = clientHeight;
			vScrollBar.SmallChange = tileHeight;
			vScrollBar.Enabled = clientHeight < boardHeight * tileHeight;

			var topLeftCoordinate = new Coordinate(hScrollBar.Value / tileWidth, vScrollBar.Value / tileHeight);

			_editorView.TopLeftPoint = new Point(-(hScrollBar.Value % tileWidth), -(vScrollBar.Value % tileHeight));
			_editorView.TopLeftCoordinate = topLeftCoordinate;
			_editorView.VisibleSizeInTiles = new Size(visibleTileWidth, visibleTileHeight);
			_editorView.VisibleSizeInPixels = xnaControl.ClientSize;
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

		private void HScrollBarOnScroll(object sender, ScrollEventArgs e)
		{
			hScrollBar.Value = e.NewValue;
			RecalculateView();
			Render();
		}

		private void VScrollBarOnScroll(object sender, ScrollEventArgs e)
		{
			vScrollBar.Value = e.NewValue;
			RecalculateView();
			Render();
		}

		private void XnaControlOnResize(object sender, EventArgs e)
		{
			RecalculateView();
			Render();
		}
	}
}