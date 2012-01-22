namespace TextAdventure.Editor.Forms
{
	partial class SymbolSelectionForm
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
			System.Windows.Forms.Button buttonCancel;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SymbolSelectionForm));
			this.pictureBoxSymbols = new System.Windows.Forms.PictureBox();
			buttonCancel = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxSymbols)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonCancel
			// 
			buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.Location = new System.Drawing.Point(780, 624);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(76, 24);
			buttonCancel.TabIndex = 0;
			buttonCancel.Text = "Cancel";
			buttonCancel.UseVisualStyleBackColor = true;
			// 
			// pictureBoxSymbols
			// 
			this.pictureBoxSymbols.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pictureBoxSymbols.Dock = System.Windows.Forms.DockStyle.Top;
			this.pictureBoxSymbols.Location = new System.Drawing.Point(0, 0);
			this.pictureBoxSymbols.Name = "pictureBoxSymbols";
			this.pictureBoxSymbols.Size = new System.Drawing.Size(864, 616);
			this.pictureBoxSymbols.TabIndex = 0;
			this.pictureBoxSymbols.TabStop = false;
			this.pictureBoxSymbols.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureBoxSymbolsOnPaint);
			this.pictureBoxSymbols.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PictureBoxSymbolsOnMouseClick);
			this.pictureBoxSymbols.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBoxSymbolsOnMouseMove);
			// 
			// SymbolSelectionForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = buttonCancel;
			this.ClientSize = new System.Drawing.Size(864, 656);
			this.Controls.Add(buttonCancel);
			this.Controls.Add(this.pictureBoxSymbols);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SymbolSelectionForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select a Symbol";
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxSymbols)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBoxSymbols;

	}
}