using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using Junior.Common;

using TextAdventure.Editor.Properties;
using TextAdventure.Editor.RendererStates;

namespace TextAdventure.Editor.Tools
{
	public partial class ToolSelectionUserControl : UserControl
	{
		private readonly EraserRendererState _eraserRendererState;
		private readonly EraserUserControl _eraserUserControl;
		private readonly PencilRendererState _pencilRendererState;
		private readonly PencilUserControl _pencilUserControl;

		public ToolSelectionUserControl(
			PencilRendererState pencilRendererState,
			EraserRendererState eraserRendererState)
		{
			pencilRendererState.ThrowIfNull("pencilRendererState");
			eraserRendererState.ThrowIfNull("eraserRendererState");

			_pencilRendererState = pencilRendererState;
			_eraserRendererState = eraserRendererState;

			InitializeComponent();

			toolStripButtonPencil.Image = Resources.Pencil;
			toolStripButtonEraser.Image = Resources.Eraser;

			_pencilUserControl = new PencilUserControl(pencilRendererState)
			                     	{
			                     		Dock = DockStyle.Fill,
			                     		Visible = false
			                     	};
			_eraserUserControl = new EraserUserControl(eraserRendererState)
			                     	{
			                     		Dock = DockStyle.Fill,
			                     		Visible = false
			                     	};

			AddToolUserControls();
			SelectPencil();
		}

		private void AddToolUserControls()
		{
			panelTools.Controls.Add(_pencilUserControl);
			panelTools.Controls.Add(_eraserUserControl);
		}

		private void DisableAllTools(IDisposable exceptFor)
		{
			_pencilRendererState.Enabled = false;

			foreach (Control control in panelTools.Controls)
			{
				control.Visible = false;
			}

			IEnumerable<ToolStripButton> toolStripButtonsToDisable = toolStrip.Items
				.OfType<ToolStripButton>()
				.Where(arg => arg != exceptFor);

			foreach (ToolStripButton toolStripButton in toolStripButtonsToDisable)
			{
				toolStripButton.Checked = false;
			}
		}

		private void SelectPencil()
		{
			_pencilRendererState.Enabled = true;
			_pencilUserControl.Visible = true;
		}

		private void SelectEraser()
		{
			_eraserRendererState.Enabled = true;
			_eraserUserControl.Visible = true;
		}

		private void PencilToolStripButtonOnCheckedChanged(object sender, EventArgs e)
		{
			if (!toolStripButtonPencil.Checked)
			{
				return;
			}

			DisableAllTools(toolStripButtonPencil);
			SelectPencil();
		}

		private void ToolStripButtonEraserOnCheckedChanged(object sender, EventArgs e)
		{
			if (!toolStripButtonEraser.Checked)
			{
				return;
			}

			DisableAllTools(toolStripButtonEraser);
			SelectEraser();
		}
	}
}