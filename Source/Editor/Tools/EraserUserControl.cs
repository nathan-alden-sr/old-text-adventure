using System;
using System.Drawing;

using Junior.Common;

using TextAdventure.Editor.RendererStates;

namespace TextAdventure.Editor.Tools
{
	public partial class EraserUserControl : ToolUserControl
	{
		private readonly EraserRendererState _eraserRendererState;

		public EraserUserControl(EraserRendererState eraserRendererState)
		{
			eraserRendererState.ThrowIfNull("eraserRendererState");

			_eraserRendererState = eraserRendererState;

			InitializeComponent();
		}

		protected override Size DefaultSize
		{
			get
			{
				return new Size(200, 72);
			}
		}

		private void SizeUserControlOnSelectedSizeChanged(object sender, EventArgs e)
		{
			_eraserRendererState.SelectionSize = sizeUserControl.SelectedSize;
		}
	}
}