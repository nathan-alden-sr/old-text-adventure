using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TextAdventure.Editor.Controls
{
	[DefaultEvent("SelectedColorChanged")]
	public class ColorControl : Control
	{
		private readonly ColorDialog _dialog = new ColorDialog
			{
				FullOpen = true
			};

		[Category("Property Changed")]
		public event EventHandler SelectedColorChanged;

		public ColorControl()
		{
			TabStop = false;
		}

		[Category("Appearance")]
		public Color SelectedColor
		{
			get
			{
				return BackColor;
			}
			set
			{
				BackColor = value;
				RaiseSelectedColorChanged(EventArgs.Empty);
			}
		}

		protected override Size DefaultSize
		{
			get
			{
				return new Size(32, 32);
			}
		}

		protected override void OnClick(EventArgs e)
		{
			_dialog.Color = BackColor;

			if (_dialog.ShowDialog(this) == DialogResult.OK)
			{
				SelectedColor = _dialog.Color;
			}

			base.OnClick(e);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.DrawRectangle(Pens.Black, 0, 0, Width - 1, Height - 1);

			base.OnPaint(e);
		}

		private void RaiseSelectedColorChanged(EventArgs e)
		{
			EventHandler handler = SelectedColorChanged;

			if (handler != null)
			{
				handler(this, e);
			}
		}
	}
}