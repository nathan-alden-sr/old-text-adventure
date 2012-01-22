using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using Junior.Common;

using TextAdventure.Editor.RendererStates;
using TextAdventure.Editor.ToolActions;
using TextAdventure.Editor.Tools;
using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

using Size = TextAdventure.Engine.Common.Size;

namespace TextAdventure.Editor.Forms
{
	public partial class GameForm : Form
	{
		private readonly BoardRendererState _boardRendererState = new BoardRendererState();
		private readonly EditorView _editorView = new EditorView();
		private readonly PencilAction _pencilAction;
		private readonly PencilRendererState _pencilRendererState = new PencilRendererState();
		private readonly ToolSelectionUserControl _toolSelectionUserControl;
		private TextAdventureEditorGame _game;

		public GameForm()
		{
			InitializeComponent();

			_pencilAction = new PencilAction(_boardRendererState, _pencilRendererState);

			_toolSelectionUserControl = new ToolSelectionUserControl(_pencilRendererState)
			                            	{
			                            		Dock = DockStyle.Fill
			                            	};
			panelTool.Controls.Add(_toolSelectionUserControl);
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

			CalculateEditorView();

			_game = new TextAdventureEditorGame(
				xnaControl.GraphicsDevice,
				_editorView,
				_boardRendererState,
				_pencilRendererState);
			_game.Run();
			xnaControl.DrawBackground = false;
		}

		private void CalculateEditorView()
		{
			_editorView.Calculate(xnaControl.ClientSize, _boardRendererState.Board.IfNotNull(arg => (Size?)arg.Size), new Point(hScrollBar.Value, vScrollBar.Value));

			const int tileWidth = TextAdventure.Xna.Constants.Tile.TileWidth;
			const int tileHeight = TextAdventure.Xna.Constants.Tile.TileHeight;

			hScrollBar.Maximum = _editorView.BoardSizeInTiles.Width * tileWidth;
			hScrollBar.LargeChange = Math.Min(hScrollBar.Maximum, _editorView.ClientSizeInPixels.Width);
			hScrollBar.SmallChange = tileWidth;
			hScrollBar.Value = Math.Max(0, Math.Min(hScrollBar.Value, _editorView.BoardSizeInPixels.Width - _editorView.ClientSizeInPixels.Width));
			hScrollBar.Enabled = _editorView.ClientSizeInPixels.Width < _editorView.BoardSizeInTiles.Width * tileWidth;

			vScrollBar.Maximum = _editorView.BoardSizeInTiles.Height * tileHeight;
			vScrollBar.LargeChange = Math.Min(hScrollBar.Maximum, _editorView.ClientSizeInPixels.Height);
			vScrollBar.SmallChange = tileHeight;
			vScrollBar.Value = Math.Max(0, Math.Min(vScrollBar.Value, _editorView.BoardSizeInPixels.Height - _editorView.ClientSizeInPixels.Height));
			vScrollBar.Enabled = _editorView.ClientSizeInPixels.Height < _editorView.BoardSizeInTiles.Height * tileHeight;
		}

		private void SetSelections(Coordinate coordinate)
		{
			if (_pencilRendererState.Enabled)
			{
				_pencilRendererState.SetSelection(coordinate);
			}
		}

		private void ClearSelections()
		{
			_pencilRendererState.ClearSelection();
		}

		private void PerformToolAction()
		{
			if (_pencilRendererState.Enabled)
			{
				_pencilAction.Draw();
			}
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
			e.NewValue = Math.Min(hScrollBar.Maximum - hScrollBar.LargeChange, e.NewValue);
			hScrollBar.Value = e.NewValue;
			CalculateEditorView();
			Render();
		}

		private void VScrollBarOnScroll(object sender, ScrollEventArgs e)
		{
			e.NewValue = Math.Min(vScrollBar.Maximum - vScrollBar.LargeChange, e.NewValue);
			vScrollBar.Value = e.NewValue;
			CalculateEditorView();
			Render();
		}

		private void XnaControlOnResize(object sender, EventArgs e)
		{
			CalculateEditorView();
			Render();
		}

		private void XnaControlOnMouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				PerformToolAction();
			}
		}

		private void XnaControlOnMouseMove(object sender, MouseEventArgs e)
		{
			Text = e.Button.ToString();

			Coordinate? coordinate = _editorView.GetCoordinateFromClientPoint(e.Location);

			if (coordinate != null && xnaControl.ClientRectangle.Contains(e.Location))
			{
				SetSelections(coordinate.Value);

				if (e.Button == MouseButtons.Left)
				{
					PerformToolAction();
				}
			}
			else
			{
				ClearSelections();
			}
		}

		private void XnaControlOnMouseLeave(object sender, EventArgs e)
		{
			ClearSelections();
		}
	}
}