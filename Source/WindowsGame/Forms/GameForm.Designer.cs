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
			System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
			System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
			System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
			System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
			System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
			System.Windows.Forms.ToolStripMenuItem normalSizeToolStripMenuItem;
			System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
			System.Windows.Forms.ToolStripMenuItem audioToolStripMenuItem;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameForm));
			this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fpsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.logToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.worldTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.soundEffectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.musicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.xnaControl = new TextAdventure.WindowsGame.Xna.TextAdventureXnaControl();
			menuStrip = new System.Windows.Forms.MenuStrip();
			worldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			normalSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			audioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			menuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip
			// 
			menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            worldToolStripMenuItem,
            viewToolStripMenuItem,
            audioToolStripMenuItem});
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
            openToolStripMenuItem,
            this.closeToolStripMenuItem,
            toolStripMenuItem1,
            this.pauseToolStripMenuItem,
            toolStripMenuItem3,
            exitToolStripMenuItem});
			worldToolStripMenuItem.Name = "worldToolStripMenuItem";
			worldToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
			worldToolStripMenuItem.Text = "&World";
			// 
			// openToolStripMenuItem
			// 
			openToolStripMenuItem.Name = "openToolStripMenuItem";
			openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			openToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
			openToolStripMenuItem.Text = "&Open...";
			openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItemOnClick);
			// 
			// closeToolStripMenuItem
			// 
			this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			this.closeToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
			this.closeToolStripMenuItem.Text = "&Close";
			this.closeToolStripMenuItem.Click += new System.EventHandler(this.CloseToolStripMenuItemOnClick);
			// 
			// toolStripMenuItem1
			// 
			toolStripMenuItem1.Name = "toolStripMenuItem1";
			toolStripMenuItem1.Size = new System.Drawing.Size(152, 6);
			// 
			// pauseToolStripMenuItem
			// 
			this.pauseToolStripMenuItem.CheckOnClick = true;
			this.pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
			this.pauseToolStripMenuItem.ShortcutKeyDisplayString = "Pause";
			this.pauseToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
			this.pauseToolStripMenuItem.Text = "&Pause";
			this.pauseToolStripMenuItem.CheckedChanged += new System.EventHandler(this.PauseToolStripMenuItemOnCheckedChanged);
			// 
			// toolStripMenuItem3
			// 
			toolStripMenuItem3.Name = "toolStripMenuItem3";
			toolStripMenuItem3.Size = new System.Drawing.Size(152, 6);
			// 
			// exitToolStripMenuItem
			// 
			exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			exitToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
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
			normalSizeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D0)));
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
			// audioToolStripMenuItem
			// 
			audioToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.soundEffectsToolStripMenuItem,
            this.musicToolStripMenuItem});
			audioToolStripMenuItem.Name = "audioToolStripMenuItem";
			audioToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
			audioToolStripMenuItem.Text = "&Audio";
			// 
			// soundEffectsToolStripMenuItem
			// 
			this.soundEffectsToolStripMenuItem.CheckOnClick = true;
			this.soundEffectsToolStripMenuItem.Name = "soundEffectsToolStripMenuItem";
			this.soundEffectsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
			this.soundEffectsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.soundEffectsToolStripMenuItem.Text = "&Sound Effects";
			this.soundEffectsToolStripMenuItem.CheckedChanged += new System.EventHandler(this.SoundEffectsToolStripMenuItemOnCheckedChanged);
			// 
			// musicToolStripMenuItem
			// 
			this.musicToolStripMenuItem.CheckOnClick = true;
			this.musicToolStripMenuItem.Name = "musicToolStripMenuItem";
			this.musicToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
			this.musicToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.musicToolStripMenuItem.Text = "&Music";
			this.musicToolStripMenuItem.CheckedChanged += new System.EventHandler(this.MusicToolStripMenuItemOnCheckedChanged);
			// 
			// openFileDialog
			// 
			this.openFileDialog.Filter = "World Assemblies (*.dll)|*.dll|All Files (*.*)|*.*";
			this.openFileDialog.Title = "Open World";
			// 
			// xnaControl
			// 
			this.xnaControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xnaControl.DrawBackground = true;
			this.xnaControl.Location = new System.Drawing.Point(0, 24);
			this.xnaControl.Name = "xnaControl";
			this.xnaControl.Size = new System.Drawing.Size(284, 238);
			this.xnaControl.TabIndex = 1;
			this.xnaControl.TabStop = false;
			this.xnaControl.Resize += new System.EventHandler(this.XnaControlOnResize);
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
			this.KeyPreview = true;
			this.MainMenuStrip = menuStrip;
			this.Name = "GameForm";
			this.Text = "Text Adventure";
			menuStrip.ResumeLayout(false);
			menuStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private TextAdventureXnaControl xnaControl;
		private System.Windows.Forms.ToolStripMenuItem fpsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem logToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem worldTimeToolStripMenuItem;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.ToolStripMenuItem soundEffectsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem musicToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pauseToolStripMenuItem;


	}
}