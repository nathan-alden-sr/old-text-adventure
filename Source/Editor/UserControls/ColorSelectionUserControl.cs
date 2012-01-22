using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

using Junior.Common;

using Microsoft.Xna.Framework;

using Color = System.Drawing.Color;

namespace TextAdventure.Editor.UserControls
{
	[DefaultEvent("SelectedBackgroundColorChanged")]
	public partial class ColorSelectionUserControl : UserControl
	{
		private bool _alphaChanging;

		[Category("Property Changed")]
		public event EventHandler SelectedBackgroundColorChanged;
		[Category("Property Changed")]
		public event EventHandler SelectedForegroundColorChanged;

		public ColorSelectionUserControl()
		{
			InitializeComponent();
		}

		[Category("Appearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Color SelectedForegroundColor
		{
			get
			{
				return Color.FromArgb((byte)trackBarForegroundAlpha.Value, colorControlForeground.BackColor);
			}
			set
			{
				trackBarForegroundAlpha.Value = value.A;
				colorControlForeground.SelectedColor = Color.FromArgb(255, value);
			}
		}

		[Category("Appearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Color SelectedBackgroundColor
		{
			get
			{
				return Color.FromArgb((byte)trackBarBackgroundAlpha.Value, colorControlBackground.BackColor);
			}
			set
			{
				trackBarBackgroundAlpha.Value = value.A;
				colorControlBackground.SelectedColor = Color.FromArgb(255, value);
			}
		}

		protected override Size DefaultSize
		{
			get
			{
				return new Size(200, 81);
			}
		}

		private void RaiseSelectedForegroundColorChanged(EventArgs e)
		{
			EventHandler handler = SelectedForegroundColorChanged;

			if (handler != null)
			{
				handler(this, e);
			}
		}

		private void RaiseSelectedBackgroundColorChanged(EventArgs e)
		{
			EventHandler handler = SelectedBackgroundColorChanged;

			if (handler != null)
			{
				handler(this, e);
			}
		}

		private void TrackBarForegroundAlphaOnScroll(object sender, EventArgs e)
		{
			if (_alphaChanging)
			{
				return;
			}

			_alphaChanging = true;

			textBoxForegroundAlpha.Text = trackBarForegroundAlpha.Value.ToString(CultureInfo.InvariantCulture);
			RaiseSelectedForegroundColorChanged(EventArgs.Empty);

			_alphaChanging = false;
		}

		private void TextBoxForegroundAlphaOnTextChanged(object sender, EventArgs e)
		{
			if (_alphaChanging)
			{
				return;
			}

			_alphaChanging = true;

			trackBarForegroundAlpha.Value = (byte)MathHelper.Clamp(textBoxForegroundAlpha.Text.Convert(255f), 0, 255);
			RaiseSelectedForegroundColorChanged(EventArgs.Empty);

			_alphaChanging = false;
		}

		private void TrackBarBackgroundAlphaOnScroll(object sender, EventArgs e)
		{
			if (_alphaChanging)
			{
				return;
			}

			_alphaChanging = true;

			textBoxBackgroundAlpha.Text = trackBarBackgroundAlpha.Value.ToString(CultureInfo.InvariantCulture);
			RaiseSelectedBackgroundColorChanged(EventArgs.Empty);
			_alphaChanging = false;
		}

		private void TextBoxBackgroundAlphaOnTextChanged(object sender, EventArgs e)
		{
			if (_alphaChanging)
			{
				return;
			}

			_alphaChanging = true;

			trackBarBackgroundAlpha.Value = (byte)MathHelper.Clamp(textBoxBackgroundAlpha.Text.Convert(255f), 0, 255);
			RaiseSelectedBackgroundColorChanged(EventArgs.Empty);

			_alphaChanging = false;
		}

		private void ColorControlForegroundOnSelectedColorChanged(object sender, EventArgs e)
		{
			RaiseSelectedForegroundColorChanged(EventArgs.Empty);
		}

		private void ColorControlBackgroundOnSelectedColorChanged(object sender, EventArgs e)
		{
			RaiseSelectedBackgroundColorChanged(EventArgs.Empty);
		}
	}
}