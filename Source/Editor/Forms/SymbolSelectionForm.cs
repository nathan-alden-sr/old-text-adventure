using System.Drawing;
using System.Windows.Forms;

using TextAdventure.Editor.Properties;

namespace TextAdventure.Editor.Forms
{
	public partial class SymbolSelectionForm : Form
	{
		private byte _selectedSymbol;
		private Rectangle _selectedSymbolRectangle;

		private SymbolSelectionForm()
		{
			InitializeComponent();

			pictureBoxSymbols.Image = Resources.Symbol_Selection;
		}

		private SymbolSelectionForm(byte selectedSymbol)
			: this()
		{
			SetSelectedSymbol(selectedSymbol);
		}

		public static byte? Display(IWin32Window owner, byte selectedSymbol)
		{
			using (var form = new SymbolSelectionForm(selectedSymbol))
			{
				return form.ShowDialog(owner) == DialogResult.OK ? form._selectedSymbol : (byte?)null;
			}
		}

		private void SetSelectedSymbol(byte symbol)
		{
			if (_selectedSymbol == symbol)
			{
				return;
			}

			_selectedSymbol = symbol;

			Rectangle oldSelectedSymbolRectangle = _selectedSymbolRectangle;

			_selectedSymbolRectangle = Rectangle.Empty;
			pictureBoxSymbols.Invalidate(oldSelectedSymbolRectangle);

			_selectedSymbolRectangle = GetSymbolRectangle(_selectedSymbol);
			pictureBoxSymbols.Invalidate(_selectedSymbolRectangle);
		}

		private static Rectangle GetSymbolRectangle(byte symbol)
		{
			const int tileWidth = (TextAdventure.Xna.Constants.Tile.TileWidth * 2) + 8;
			const int tileHeight = (TextAdventure.Xna.Constants.Tile.TileHeight * 2) + 8;

			return new Rectangle((symbol % 24) * tileWidth, (symbol / 24) * tileHeight, tileWidth, tileHeight);
		}

		private void PictureBoxSymbolsOnMouseMove(object sender, MouseEventArgs e)
		{
			int symbolX = e.X / ((TextAdventure.Xna.Constants.Tile.TileWidth * 2) + 8);
			int symbolY = e.Y / ((TextAdventure.Xna.Constants.Tile.TileHeight * 2) + 8);
			int selectedSymbol = (symbolY * 24) + symbolX;

			SetSelectedSymbol(selectedSymbol > 255 ? (byte)0 : (byte)selectedSymbol);
		}

		private void PictureBoxSymbolsOnMouseClick(object sender, MouseEventArgs e)
		{
			DialogResult = DialogResult.OK;
		}

		private void PictureBoxSymbolsOnPaint(object sender, PaintEventArgs e)
		{
			Rectangle rectangle = _selectedSymbolRectangle;

			rectangle.Width--;
			rectangle.Height--;

			e.Graphics.DrawRectangle(Pens.Red, rectangle);
			rectangle.Inflate(-1, -1);
			e.Graphics.DrawRectangle(Pens.Red, rectangle);
		}
	}
}