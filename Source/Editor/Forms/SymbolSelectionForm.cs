using System;
using System.Drawing;
using System.Windows.Forms;

using TextAdventure.Editor.Properties;

namespace TextAdventure.Editor.Forms
{
	public partial class SymbolSelectionForm : Form
	{
		private const int SymbolsHigh = 11;
		private const int SymbolsWide = 24;
		private static readonly int _symbolHeight = Resources.Symbol_Selection.Height / SymbolsHigh;
		private static readonly int _symbolWidth = Resources.Symbol_Selection.Width / SymbolsWide;
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
			return new Rectangle((symbol % SymbolsWide) * _symbolWidth, (symbol / 24) * _symbolHeight, _symbolWidth, _symbolHeight);
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			SetClientSizeCore(Resources.Symbol_Selection.Width, Resources.Symbol_Selection.Height + buttonCancel.Height + 16);

			base.OnHandleCreated(e);
		}

		private void PictureBoxSymbolsOnMouseMove(object sender, MouseEventArgs e)
		{
			int symbolX = e.X / _symbolWidth;
			int symbolY = e.Y / _symbolHeight;
			int selectedSymbol = (symbolY * SymbolsWide) + symbolX;

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