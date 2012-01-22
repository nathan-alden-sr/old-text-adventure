namespace TextAdventure.Editor.Forms
{
	partial class GameForm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.Panel panel3;
			System.Windows.Forms.Panel panel1;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameForm));
			this.hScrollBar = new System.Windows.Forms.HScrollBar();
			this.vScrollBar = new System.Windows.Forms.VScrollBar();
			this.panelTool = new System.Windows.Forms.Panel();
			this.xnaControl = new TextAdventure.Editor.Xna.XnaControl();
			panel3 = new System.Windows.Forms.Panel();
			panel1 = new System.Windows.Forms.Panel();
			panel1.SuspendLayout();
			this.panelTool.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel3
			// 
			panel3.BackColor = System.Drawing.SystemColors.ControlDark;
			panel3.Dock = System.Windows.Forms.DockStyle.Right;
			panel3.Location = new System.Drawing.Point(208, 0);
			panel3.Name = "panel3";
			panel3.Size = new System.Drawing.Size(2, 564);
			panel3.TabIndex = 0;
			// 
			// panel1
			// 
			panel1.Controls.Add(this.hScrollBar);
			panel1.Controls.Add(this.vScrollBar);
			panel1.Controls.Add(this.xnaControl);
			panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			panel1.Location = new System.Drawing.Point(210, 0);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(570, 564);
			panel1.TabIndex = 0;
			// 
			// hScrollBar
			// 
			this.hScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.hScrollBar.Enabled = false;
			this.hScrollBar.LargeChange = 1;
			this.hScrollBar.Location = new System.Drawing.Point(0, 547);
			this.hScrollBar.Maximum = 0;
			this.hScrollBar.Name = "hScrollBar";
			this.hScrollBar.Size = new System.Drawing.Size(554, 17);
			this.hScrollBar.TabIndex = 2;
			this.hScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.HScrollBarOnScroll);
			// 
			// vScrollBar
			// 
			this.vScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.vScrollBar.Enabled = false;
			this.vScrollBar.LargeChange = 1;
			this.vScrollBar.Location = new System.Drawing.Point(553, 0);
			this.vScrollBar.Maximum = 0;
			this.vScrollBar.Name = "vScrollBar";
			this.vScrollBar.Size = new System.Drawing.Size(17, 548);
			this.vScrollBar.TabIndex = 1;
			this.vScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.VScrollBarOnScroll);
			// 
			// panelTool
			// 
			this.panelTool.Controls.Add(panel3);
			this.panelTool.Dock = System.Windows.Forms.DockStyle.Left;
			this.panelTool.Location = new System.Drawing.Point(0, 0);
			this.panelTool.Name = "panelTool";
			this.panelTool.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
			this.panelTool.Size = new System.Drawing.Size(210, 564);
			this.panelTool.TabIndex = 1;
			// 
			// xnaControl
			// 
			this.xnaControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.xnaControl.DrawBackground = true;
			this.xnaControl.Location = new System.Drawing.Point(0, 0);
			this.xnaControl.Name = "xnaControl";
			this.xnaControl.Size = new System.Drawing.Size(553, 547);
			this.xnaControl.TabIndex = 0;
			this.xnaControl.TabStop = false;
			this.xnaControl.MouseLeave += new System.EventHandler(this.XnaControlOnMouseLeave);
			this.xnaControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.XnaControlOnMouseMove);
			this.xnaControl.Resize += new System.EventHandler(this.XnaControlOnResize);
			// 
			// GameForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(780, 564);
			this.Controls.Add(panel1);
			this.Controls.Add(this.panelTool);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "GameForm";
			this.Text = "Text Adventure Editor";
			panel1.ResumeLayout(false);
			this.panelTool.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.HScrollBar hScrollBar;
		private System.Windows.Forms.VScrollBar vScrollBar;
		private Xna.XnaControl xnaControl;
		private System.Windows.Forms.Panel panelTool;

	}
}