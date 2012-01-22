namespace TextAdventure.Editor.Tools
{
	partial class ToolSelectionUserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.ToolStrip toolStrip;
			this.pencilToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.panelTools = new System.Windows.Forms.Panel();
			toolStrip = new System.Windows.Forms.ToolStrip();
			toolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip
			// 
			toolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
			toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pencilToolStripButton});
			toolStrip.Location = new System.Drawing.Point(0, 0);
			toolStrip.Name = "toolStrip";
			toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			toolStrip.Size = new System.Drawing.Size(200, 25);
			toolStrip.TabIndex = 0;
			// 
			// pencilToolStripButton
			// 
			this.pencilToolStripButton.Checked = true;
			this.pencilToolStripButton.CheckState = System.Windows.Forms.CheckState.Checked;
			this.pencilToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.pencilToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.pencilToolStripButton.Name = "pencilToolStripButton";
			this.pencilToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.pencilToolStripButton.Text = "Pencil";
			this.pencilToolStripButton.CheckedChanged += new System.EventHandler(this.PencilToolStripButtonOnCheckedChanged);
			// 
			// panelTools
			// 
			this.panelTools.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelTools.Location = new System.Drawing.Point(0, 25);
			this.panelTools.Name = "panelTools";
			this.panelTools.Size = new System.Drawing.Size(200, 125);
			this.panelTools.TabIndex = 1;
			// 
			// ToolSelectionUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panelTools);
			this.Controls.Add(toolStrip);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "ToolSelectionUserControl";
			this.Size = new System.Drawing.Size(200, 150);
			toolStrip.ResumeLayout(false);
			toolStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStripButton pencilToolStripButton;
		private System.Windows.Forms.Panel panelTools;

	}
}
