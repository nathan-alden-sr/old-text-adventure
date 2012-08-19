using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using TextAdventure.Editor.Forms;

namespace TextAdventure.Editor.UserControls
{
	public partial class CharacterSelectionUserControl : UserControl
	{
		[Category("Property Changed")]
		public event EventHandler SymbolBackgroundColorChanged;
		[Category("Property Changed")]
		public event EventHandler SymbolChanged;
		[Category("Property Changed")]
		public event EventHandler SymbolForegroundColorChanged;

		public CharacterSelectionUserControl()
		{
			InitializeComponent();
		}

		[Category("Appearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public byte Symbol
		{
			get
			{
				return symbolControl.SelectedSymbol;
			}
			set
			{
				symbolControl.SelectedSymbol = value;
			}
		}

		[Category("Appearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Color SymbolForegroundColor
		{
			get
			{
				return colorSelectionUserControl.SelectedForegroundColor;
			}
			set
			{
				colorSelectionUserControl.SelectedForegroundColor = value;
			}
		}

		[Category("Appearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Color SymbolBackgroundColor
		{
			get
			{
				return colorSelectionUserControl.SelectedBackgroundColor;
			}
			set
			{
				colorSelectionUserControl.SelectedBackgroundColor = value;
			}
		}

		protected override Size DefaultSize
		{
			get
			{
				return new Size(200, 157);
			}
		}

		private void RaiseSymbolChanged(EventArgs e)
		{
			EventHandler handler = SymbolChanged;
			if (handler != null)
			{
				handler(this, e);
			}
		}

		private void RaiseSymbolForegroundColorChanged(EventArgs e)
		{
			EventHandler handler = SymbolForegroundColorChanged;
			if (handler != null)
			{
				handler(this, e);
			}
		}

		private void RaiseSymbolBackgroundColorChanged(EventArgs e)
		{
			EventHandler handler = SymbolBackgroundColorChanged;
			if (handler != null)
			{
				handler(this, e);
			}
		}

		private void SymbolControlOnClick(object sender, EventArgs e)
		{
			byte? selectedSymbol = SymbolSelectionForm.Display(this, symbolControl.SelectedSymbol);

			if (selectedSymbol == null)
			{
				return;
			}

			symbolControl.SelectedSymbol = selectedSymbol.Value;
		}

		private void SymbolControlOnSymbolChanged(object sender, EventArgs e)
		{
			RaiseSymbolChanged(EventArgs.Empty);
		}

		private void ColorSelectionUserControlOnSelectedBackgroundColorChanged(object sender, EventArgs e)
		{
			symbolControl.SymbolBackgroundColor = colorSelectionUserControl.SelectedBackgroundColor;
			RaiseSymbolBackgroundColorChanged(EventArgs.Empty);
		}

		private void ColorSelectionUserControlOnSelectedForegroundColorChanged(object sender, EventArgs e)
		{
			symbolControl.SymbolForegroundColor = colorSelectionUserControl.SelectedForegroundColor;
			RaiseSymbolForegroundColorChanged(EventArgs.Empty);
		}
	}
}