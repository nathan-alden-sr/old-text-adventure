using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TextAdventure.Editor.UserControls
{
	[DefaultEvent("SelectedSizeChanged")]
	public partial class SizeUserControl : UserControl
	{
		[Category("Property Changed")]
		public event EventHandler SelectedSizeChanged;

		public SizeUserControl()
		{
			InitializeComponent();
		}

		[Category("Appearance")]
		[DefaultValue(1)]
		public int SelectedSize
		{
			get
			{
				return trackBarSize.Value;
			}
			set
			{
				trackBarSize.Value = value;
				labelSize.Text = String.Format("{0} x {0}", trackBarSize.Value);
				RaiseSelectedSizeChanged(EventArgs.Empty);
			}
		}

		protected override Size DefaultSize
		{
			get
			{
				return new Size(200, 48);
			}
		}

		private void RaiseSelectedSizeChanged(EventArgs e)
		{
			EventHandler handler = SelectedSizeChanged;

			if (handler != null)
			{
				handler(this, e);
			}
		}

		private void TrackBarSizeOnScroll(object sender, EventArgs e)
		{
			SelectedSize = trackBarSize.Value;
		}
	}
}