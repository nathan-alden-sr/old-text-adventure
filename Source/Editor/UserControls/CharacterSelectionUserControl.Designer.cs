namespace TextAdventure.Editor.UserControls
{
	partial class CharacterSelectionUserControl
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
			System.Windows.Forms.Label label1;
			this.symbolControl = new TextAdventure.Editor.Controls.SymbolControl();
			this.colorSelectionUserControl = new TextAdventure.Editor.UserControls.ColorSelectionUserControl();
			label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(0, 0);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(45, 13);
			label1.TabIndex = 0;
			label1.Text = "Symbol:";
			// 
			// symbolControl
			// 
			this.symbolControl.Cursor = System.Windows.Forms.Cursors.Hand;
			this.symbolControl.Location = new System.Drawing.Point(4, 16);
			this.symbolControl.Name = "symbolControl";
			this.symbolControl.Size = new System.Drawing.Size(32, 52);
			this.symbolControl.SymbolBackgroundColor = System.Drawing.Color.Transparent;
			this.symbolControl.TabIndex = 1;
			this.symbolControl.TabStop = false;
			this.symbolControl.Text = "symbolControl1";
			this.symbolControl.SymbolChanged += new System.EventHandler(this.SymbolControlOnSymbolChanged);
			this.symbolControl.Click += new System.EventHandler(this.SymbolControlOnClick);
			// 
			// colorSelectionUserControl
			// 
			this.colorSelectionUserControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.colorSelectionUserControl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colorSelectionUserControl.Location = new System.Drawing.Point(0, 76);
			this.colorSelectionUserControl.Name = "colorSelectionUserControl";
			this.colorSelectionUserControl.Size = new System.Drawing.Size(200, 81);
			this.colorSelectionUserControl.TabIndex = 2;
			this.colorSelectionUserControl.SelectedBackgroundColorChanged += new System.EventHandler(this.ColorSelectionUserControlOnSelectedBackgroundColorChanged);
			this.colorSelectionUserControl.SelectedForegroundColorChanged += new System.EventHandler(this.ColorSelectionUserControlOnSelectedForegroundColorChanged);
			// 
			// CharacterSelectionUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.symbolControl);
			this.Controls.Add(label1);
			this.Controls.Add(this.colorSelectionUserControl);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Name = "CharacterSelectionUserControl";
			this.Size = new System.Drawing.Size(200, 157);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private ColorSelectionUserControl colorSelectionUserControl;
		private Controls.SymbolControl symbolControl;
	}
}
