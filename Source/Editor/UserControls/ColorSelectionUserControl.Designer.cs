namespace TextAdventure.Editor.UserControls
{
	partial class ColorSelectionUserControl
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
			System.Windows.Forms.Label label2;
			System.Windows.Forms.Label label1;
			System.Windows.Forms.Label label3;
			System.Windows.Forms.Label label4;
			this.trackBarForegroundAlpha = new System.Windows.Forms.TrackBar();
			this.textBoxForegroundAlpha = new System.Windows.Forms.TextBox();
			this.textBoxBackgroundAlpha = new System.Windows.Forms.TextBox();
			this.trackBarBackgroundAlpha = new System.Windows.Forms.TrackBar();
			this.colorControlBackground = new TextAdventure.Editor.Controls.ColorControl();
			this.colorControlForeground = new TextAdventure.Editor.Controls.ColorControl();
			label2 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.trackBarForegroundAlpha)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarBackgroundAlpha)).BeginInit();
			this.SuspendLayout();
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(0, 0);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(93, 13);
			label2.TabIndex = 0;
			label2.Text = "Foreground color:";
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(52, 20);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(38, 13);
			label1.TabIndex = 2;
			label1.Text = "Alpha:";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(52, 64);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(38, 13);
			label3.TabIndex = 7;
			label3.Text = "Alpha:";
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(0, 44);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(93, 13);
			label4.TabIndex = 5;
			label4.Text = "Background color:";
			// 
			// trackBarForegroundAlpha
			// 
			this.trackBarForegroundAlpha.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.trackBarForegroundAlpha.AutoSize = false;
			this.trackBarForegroundAlpha.LargeChange = 16;
			this.trackBarForegroundAlpha.Location = new System.Drawing.Point(92, 16);
			this.trackBarForegroundAlpha.Maximum = 255;
			this.trackBarForegroundAlpha.Name = "trackBarForegroundAlpha";
			this.trackBarForegroundAlpha.Size = new System.Drawing.Size(76, 21);
			this.trackBarForegroundAlpha.TabIndex = 3;
			this.trackBarForegroundAlpha.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarForegroundAlpha.Value = 255;
			this.trackBarForegroundAlpha.Scroll += new System.EventHandler(this.TrackBarForegroundAlphaOnScroll);
			// 
			// textBoxForegroundAlpha
			// 
			this.textBoxForegroundAlpha.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxForegroundAlpha.Location = new System.Drawing.Point(172, 16);
			this.textBoxForegroundAlpha.MaxLength = 3;
			this.textBoxForegroundAlpha.Name = "textBoxForegroundAlpha";
			this.textBoxForegroundAlpha.Size = new System.Drawing.Size(28, 21);
			this.textBoxForegroundAlpha.TabIndex = 4;
			this.textBoxForegroundAlpha.Text = "255";
			this.textBoxForegroundAlpha.TextChanged += new System.EventHandler(this.TextBoxForegroundAlphaOnTextChanged);
			// 
			// textBoxBackgroundAlpha
			// 
			this.textBoxBackgroundAlpha.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxBackgroundAlpha.Location = new System.Drawing.Point(172, 60);
			this.textBoxBackgroundAlpha.MaxLength = 3;
			this.textBoxBackgroundAlpha.Name = "textBoxBackgroundAlpha";
			this.textBoxBackgroundAlpha.Size = new System.Drawing.Size(28, 21);
			this.textBoxBackgroundAlpha.TabIndex = 9;
			this.textBoxBackgroundAlpha.Text = "0";
			this.textBoxBackgroundAlpha.TextChanged += new System.EventHandler(this.TextBoxBackgroundAlphaOnTextChanged);
			// 
			// trackBarBackgroundAlpha
			// 
			this.trackBarBackgroundAlpha.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.trackBarBackgroundAlpha.AutoSize = false;
			this.trackBarBackgroundAlpha.LargeChange = 16;
			this.trackBarBackgroundAlpha.Location = new System.Drawing.Point(92, 60);
			this.trackBarBackgroundAlpha.Maximum = 255;
			this.trackBarBackgroundAlpha.Name = "trackBarBackgroundAlpha";
			this.trackBarBackgroundAlpha.Size = new System.Drawing.Size(76, 21);
			this.trackBarBackgroundAlpha.TabIndex = 8;
			this.trackBarBackgroundAlpha.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarBackgroundAlpha.Scroll += new System.EventHandler(this.TrackBarBackgroundAlphaOnScroll);
			// 
			// colorControlBackground
			// 
			this.colorControlBackground.BackColor = System.Drawing.Color.Black;
			this.colorControlBackground.Cursor = System.Windows.Forms.Cursors.Hand;
			this.colorControlBackground.Location = new System.Drawing.Point(4, 60);
			this.colorControlBackground.Name = "colorControlBackground";
			this.colorControlBackground.SelectedColor = System.Drawing.Color.Black;
			this.colorControlBackground.Size = new System.Drawing.Size(44, 21);
			this.colorControlBackground.TabIndex = 6;
			this.colorControlBackground.TabStop = false;
			this.colorControlBackground.SelectedColorChanged += new System.EventHandler(this.ColorControlBackgroundOnSelectedColorChanged);
			// 
			// colorControlForeground
			// 
			this.colorControlForeground.BackColor = System.Drawing.Color.White;
			this.colorControlForeground.Cursor = System.Windows.Forms.Cursors.Hand;
			this.colorControlForeground.Location = new System.Drawing.Point(4, 16);
			this.colorControlForeground.Name = "colorControlForeground";
			this.colorControlForeground.SelectedColor = System.Drawing.Color.White;
			this.colorControlForeground.Size = new System.Drawing.Size(44, 21);
			this.colorControlForeground.TabIndex = 1;
			this.colorControlForeground.TabStop = false;
			this.colorControlForeground.SelectedColorChanged += new System.EventHandler(this.ColorControlForegroundOnSelectedColorChanged);
			// 
			// ColorSelectionUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.textBoxBackgroundAlpha);
			this.Controls.Add(this.textBoxForegroundAlpha);
			this.Controls.Add(label3);
			this.Controls.Add(this.trackBarBackgroundAlpha);
			this.Controls.Add(this.colorControlBackground);
			this.Controls.Add(label4);
			this.Controls.Add(label1);
			this.Controls.Add(this.trackBarForegroundAlpha);
			this.Controls.Add(this.colorControlForeground);
			this.Controls.Add(label2);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "ColorSelectionUserControl";
			this.Size = new System.Drawing.Size(200, 81);
			((System.ComponentModel.ISupportInitialize)(this.trackBarForegroundAlpha)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarBackgroundAlpha)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TrackBar trackBarForegroundAlpha;
		private Controls.ColorControl colorControlForeground;
		private System.Windows.Forms.TextBox textBoxForegroundAlpha;
		private System.Windows.Forms.TextBox textBoxBackgroundAlpha;
		private System.Windows.Forms.TrackBar trackBarBackgroundAlpha;
		private Controls.ColorControl colorControlBackground;
	}
}
