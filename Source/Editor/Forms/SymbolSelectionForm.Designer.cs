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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SymbolSelectionForm));
			this.buttonCancel = new System.Windows.Forms.Button();
			this.pictureBoxSymbols = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxSymbols)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(210, 240);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(76, 24);
			this.buttonCancel.TabIndex = 0;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// pictureBoxSymbols
			// 
			this.pictureBoxSymbols.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBoxSymbols.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pictureBoxSymbols.Location = new System.Drawing.Point(0, 0);
			this.pictureBoxSymbols.Name = "pictureBoxSymbols";
			this.pictureBoxSymbols.Size = new System.Drawing.Size(294, 232);
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
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(294, 272);
			this.Controls.Add(this.buttonCancel);
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
		private System.Windows.Forms.Button buttonCancel;

	}
}