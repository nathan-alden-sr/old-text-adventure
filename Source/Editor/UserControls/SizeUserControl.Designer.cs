namespace TextAdventure.Editor.UserControls
{
	partial class SizeUserControl
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
			this.labelSize = new System.Windows.Forms.Label();
			this.trackBarSize = new System.Windows.Forms.TrackBar();
			((System.ComponentModel.ISupportInitialize)(this.trackBarSize)).BeginInit();
			this.SuspendLayout();
			// 
			// labelSize
			// 
			this.labelSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelSize.Location = new System.Drawing.Point(0, 32);
			this.labelSize.Name = "labelSize";
			this.labelSize.Size = new System.Drawing.Size(200, 16);
			this.labelSize.TabIndex = 1;
			this.labelSize.Text = "1 x 1";
			this.labelSize.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// trackBarSize
			// 
			this.trackBarSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.trackBarSize.AutoSize = false;
			this.trackBarSize.LargeChange = 1;
			this.trackBarSize.Location = new System.Drawing.Point(0, 0);
			this.trackBarSize.Minimum = 1;
			this.trackBarSize.Name = "trackBarSize";
			this.trackBarSize.Size = new System.Drawing.Size(200, 32);
			this.trackBarSize.TabIndex = 0;
			this.trackBarSize.Value = 1;
			this.trackBarSize.Scroll += new System.EventHandler(this.TrackBarSizeOnScroll);
			// 
			// SizeUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.labelSize);
			this.Controls.Add(this.trackBarSize);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "SizeUserControl";
			this.Size = new System.Drawing.Size(200, 48);
			((System.ComponentModel.ISupportInitialize)(this.trackBarSize)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label labelSize;
		private System.Windows.Forms.TrackBar trackBarSize;
	}
}
