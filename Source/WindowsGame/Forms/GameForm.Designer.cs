using TextAdventure.WindowsGame.Xna;

namespace TextAdventure.WindowsGame.Forms
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
			System.Windows.Forms.MenuStrip menuStrip;
			System.Windows.Forms.ToolStripMenuItem worldToolStripMenuItem;
			System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
			System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
			System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
			System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
			System.Windows.Forms.ToolStripMenuItem normalSizeToolStripMenuItem;
			System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameForm));
			this.fpsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.logToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.worldTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.xnaControl = new TextAdventure.WindowsGame.Xna.XnaControl();
			menuStrip = new System.Windows.Forms.MenuStrip();
			worldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			normalSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			menuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip
			// 
			menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            worldToolStripMenuItem,
            viewToolStripMenuItem});
			menuStrip.Location = new System.Drawing.Point(0, 0);
			menuStrip.Name = "menuStrip";
			menuStrip.Size = new System.Drawing.Size(284, 24);
			menuStrip.TabIndex = 0;
			menuStrip.Text = "menuStrip1";
			menuStrip.MenuActivate += new System.EventHandler(this.MenuStripOnMenuActivate);
			menuStrip.MenuDeactivate += new System.EventHandler(this.MenuStripOnMenuDeactivate);
			// 
			// worldToolStripMenuItem
			// 
			worldToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            loadToolStripMenuItem,
            toolStripMenuItem1,
            exitToolStripMenuItem});
			worldToolStripMenuItem.Name = "worldToolStripMenuItem";
			worldToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
			worldToolStripMenuItem.Text = "&World";
			// 
			// loadToolStripMenuItem
			// 
			loadToolStripMenuItem.Name = "loadToolStripMenuItem";
			loadToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
			loadToolStripMenuItem.Text = "&Load...";
			// 
			// toolStripMenuItem1
			// 
			toolStripMenuItem1.Name = "toolStripMenuItem1";
			toolStripMenuItem1.Size = new System.Drawing.Size(106, 6);
			// 
			// exitToolStripMenuItem
			// 
			exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			exitToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
			exitToolStripMenuItem.Text = "E&xit";
			exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemOnClick);
			// 
			// viewToolStripMenuItem
			// 
			viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            normalSizeToolStripMenuItem,
            toolStripMenuItem2,
            this.fpsToolStripMenuItem,
            this.logToolStripMenuItem,
            this.worldTimeToolStripMenuItem});
			viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			viewToolStripMenuItem.Text = "&View";
			// 
			// normalSizeToolStripMenuItem
			// 
			normalSizeToolStripMenuItem.Name = "normalSizeToolStripMenuItem";
			normalSizeToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			normalSizeToolStripMenuItem.Text = "&Normal Size";
			normalSizeToolStripMenuItem.Click += new System.EventHandler(this.NormalSizeToolStripMenuItemOnClick);
			// 
			// toolStripMenuItem2
			// 
			toolStripMenuItem2.Name = "toolStripMenuItem2";
			toolStripMenuItem2.Size = new System.Drawing.Size(174, 6);
			// 
			// fpsToolStripMenuItem
			// 
			this.fpsToolStripMenuItem.CheckOnClick = true;
			this.fpsToolStripMenuItem.Name = "fpsToolStripMenuItem";
			this.fpsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
			this.fpsToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.fpsToolStripMenuItem.Text = "&FPS";
			this.fpsToolStripMenuItem.CheckedChanged += new System.EventHandler(this.FpsToolStripMenuItemOnCheckedChanged);
			// 
			// logToolStripMenuItem
			// 
			this.logToolStripMenuItem.CheckOnClick = true;
			this.logToolStripMenuItem.Name = "logToolStripMenuItem";
			this.logToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
			this.logToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.logToolStripMenuItem.Text = "&Log";
			this.logToolStripMenuItem.Click += new System.EventHandler(this.LogToolStripMenuItemOnClick);
			// 
			// worldTimeToolStripMenuItem
			// 
			this.worldTimeToolStripMenuItem.CheckOnClick = true;
			this.worldTimeToolStripMenuItem.Name = "worldTimeToolStripMenuItem";
			this.worldTimeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
			this.worldTimeToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.worldTimeToolStripMenuItem.Text = "&World Time";
			this.worldTimeToolStripMenuItem.Click += new System.EventHandler(this.WorldTimeToolStripMenuItemOnClick);
			// 
			// xnaControl
			// 
			this.xnaControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xnaControl.Location = new System.Drawing.Point(0, 24);
			this.xnaControl.Name = "xnaControl";
			this.xnaControl.Size = new System.Drawing.Size(284, 238);
			this.xnaControl.TabIndex = 1;
			this.xnaControl.TabStop = false;
			this.xnaControl.Text = "xnaControl1";
			// 
			// GameForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.Controls.Add(this.xnaControl);
			this.Controls.Add(menuStrip);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = menuStrip;
			this.Name = "GameForm";
			this.Text = "Text Adventure";
			menuStrip.ResumeLayout(false);
			menuStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private XnaControl xnaControl;
		private System.Windows.Forms.ToolStripMenuItem fpsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem logToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem worldTimeToolStripMenuItem;


	}
}