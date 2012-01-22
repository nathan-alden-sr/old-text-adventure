namespace TextAdventure.Editor.Tools
{
	partial class EraserUserControl
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
			System.Windows.Forms.GroupBox groupBox2;
			this.sizeUserControl = new TextAdventure.Editor.UserControls.SizeUserControl();
			groupBox2 = new System.Windows.Forms.GroupBox();
			groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox2
			// 
			groupBox2.Controls.Add(this.sizeUserControl);
			groupBox2.Location = new System.Drawing.Point(0, 0);
			groupBox2.Name = "groupBox2";
			groupBox2.Size = new System.Drawing.Size(200, 72);
			groupBox2.TabIndex = 0;
			groupBox2.TabStop = false;
			groupBox2.Text = "Size";
			// 
			// sizeUserControl
			// 
			this.sizeUserControl.Dock = System.Windows.Forms.DockStyle.Top;
			this.sizeUserControl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.sizeUserControl.Location = new System.Drawing.Point(3, 17);
			this.sizeUserControl.Name = "sizeUserControl";
			this.sizeUserControl.Size = new System.Drawing.Size(194, 48);
			this.sizeUserControl.TabIndex = 0;
			this.sizeUserControl.SelectedSizeChanged += new System.EventHandler(this.SizeUserControlOnSelectedSizeChanged);
			// 
			// EraserUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(groupBox2);
			this.Name = "EraserUserControl";
			this.Size = new System.Drawing.Size(200, 72);
			groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private UserControls.SizeUserControl sizeUserControl;
	}
}
