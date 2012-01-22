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
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.toolStripButtonPencil = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonEraser = new System.Windows.Forms.ToolStripButton();
			this.panelTools = new System.Windows.Forms.Panel();
			this.toolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip
			// 
			this.toolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonPencil,
            this.toolStripButtonEraser});
			this.toolStrip.Location = new System.Drawing.Point(0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip.Size = new System.Drawing.Size(200, 25);
			this.toolStrip.TabIndex = 0;
			// 
			// toolStripButtonPencil
			// 
			this.toolStripButtonPencil.Checked = true;
			this.toolStripButtonPencil.CheckOnClick = true;
			this.toolStripButtonPencil.CheckState = System.Windows.Forms.CheckState.Checked;
			this.toolStripButtonPencil.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonPencil.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonPencil.Name = "toolStripButtonPencil";
			this.toolStripButtonPencil.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonPencil.Text = "Pencil";
			this.toolStripButtonPencil.CheckedChanged += new System.EventHandler(this.PencilToolStripButtonOnCheckedChanged);
			// 
			// toolStripButtonEraser
			// 
			this.toolStripButtonEraser.CheckOnClick = true;
			this.toolStripButtonEraser.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonEraser.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonEraser.Name = "toolStripButtonEraser";
			this.toolStripButtonEraser.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonEraser.Text = "Eraser";
			this.toolStripButtonEraser.CheckedChanged += new System.EventHandler(this.ToolStripButtonEraserOnCheckedChanged);
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
			this.Controls.Add(this.toolStrip);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "ToolSelectionUserControl";
			this.Size = new System.Drawing.Size(200, 150);
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStripButton toolStripButtonPencil;
		private System.Windows.Forms.Panel panelTools;
		private System.Windows.Forms.ToolStripButton toolStripButtonEraser;
		private System.Windows.Forms.ToolStrip toolStrip;

	}
}
