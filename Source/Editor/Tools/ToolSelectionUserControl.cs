using System;
using System.Windows.Forms;

using Junior.Common;

using TextAdventure.Editor.Properties;
using TextAdventure.Editor.RendererStates;

namespace TextAdventure.Editor.Tools
{
	public partial class ToolSelectionUserControl : UserControl
	{
		private readonly PencilRendererState _pencilRendererState;
		private readonly PencilUserControl _pencilUserControl;

		public ToolSelectionUserControl(
			PencilRendererState pencilRendererState)
		{
			pencilRendererState.ThrowIfNull("pencilRendererState");

			_pencilRendererState = pencilRendererState;

			InitializeComponent();

			pencilToolStripButton.Image = Resources.Pencil;

			_pencilUserControl = new PencilUserControl(pencilRendererState)
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
		}

		private void DisableAllTools()
		{
			_pencilRendererState.Enabled = false;
			foreach (Control control in panelTools.Controls)
			{
				control.Visible = false;
			}
		}

		private void SelectPencil()
		{
			_pencilRendererState.Enabled = true;
			_pencilUserControl.Visible = true;
		}

		private void PencilToolStripButtonOnCheckedChanged(object sender, EventArgs e)
		{
			if (!pencilToolStripButton.Checked)
			{
				return;
			}

			DisableAllTools();
			SelectPencil();
		}
	}
}