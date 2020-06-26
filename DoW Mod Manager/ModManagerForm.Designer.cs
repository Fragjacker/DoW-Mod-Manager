namespace DoW_Mod_Manager
{
    partial class ModManagerForm
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
            this.currentDirTextBox = new System.Windows.Forms.TextBox();
            this.currentDirectoryLabel = new System.Windows.Forms.Label();
            this.installedModsLabel = new System.Windows.Forms.Label();
            this.requiredModsLabel = new System.Windows.Forms.Label();
            this.installedModsListBox = new System.Windows.Forms.ListBox();
            this.requiredModsList = new System.Windows.Forms.ListBox();
            this.startModButton = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.authorLabel = new System.Windows.Forms.Label();
            this.advancedStartOptionsLabel = new System.Windows.Forms.Label();
            this.devCheckBox = new System.Windows.Forms.CheckBox();
            this.nomoviesCheckBox = new System.Windows.Forms.CheckBox();
            this.highpolyCheckBox = new System.Windows.Forms.CheckBox();
            this.optimizationsCheckBox = new System.Windows.Forms.CheckBox();
            this.mergeButton = new System.Windows.Forms.Button();
            this.startVanillaButton = new System.Windows.Forms.Button();
            this.gameLAAStatusLabel = new System.Windows.Forms.Label();
            this.graphicsConfigLAAStatusLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toggleLAAButton = new System.Windows.Forms.Button();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.downloadButton = new System.Windows.Forms.Button();
            this.settingsButton = new System.Windows.Forms.Button();
            this.homePageLinkLabel = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // currentDirTextBox
            // 
            this.currentDirTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.currentDirTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.currentDirTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.currentDirTextBox.Location = new System.Drawing.Point(199, 6);
            this.currentDirTextBox.Name = "currentDirTextBox";
            this.currentDirTextBox.ReadOnly = true;
            this.currentDirTextBox.Size = new System.Drawing.Size(607, 20);
            this.currentDirTextBox.TabIndex = 0;
            // 
            // currentDirectoryLabel
            // 
            this.currentDirectoryLabel.AutoSize = true;
            this.currentDirectoryLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.currentDirectoryLabel.Location = new System.Drawing.Point(16, 9);
            this.currentDirectoryLabel.Name = "currentDirectoryLabel";
            this.currentDirectoryLabel.Size = new System.Drawing.Size(137, 13);
            this.currentDirectoryLabel.TabIndex = 1;
            this.currentDirectoryLabel.Text = "Your current game directory";
            // 
            // installedModsLabel
            // 
            this.installedModsLabel.AutoSize = true;
            this.installedModsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.installedModsLabel.Location = new System.Drawing.Point(3, 0);
            this.installedModsLabel.Name = "installedModsLabel";
            this.installedModsLabel.Size = new System.Drawing.Size(119, 13);
            this.installedModsLabel.TabIndex = 2;
            this.installedModsLabel.Text = "Currently Installed Mods";
            // 
            // requiredModsLabel
            // 
            this.requiredModsLabel.AutoSize = true;
            this.requiredModsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.requiredModsLabel.Location = new System.Drawing.Point(7, 0);
            this.requiredModsLabel.Name = "requiredModsLabel";
            this.requiredModsLabel.Size = new System.Drawing.Size(79, 13);
            this.requiredModsLabel.TabIndex = 3;
            this.requiredModsLabel.Text = "Required Mods";
            // 
            // installedModsListBox
            // 
            this.installedModsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.installedModsListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.installedModsListBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.installedModsListBox.FormattingEnabled = true;
            this.installedModsListBox.Location = new System.Drawing.Point(6, 23);
            this.installedModsListBox.Margin = new System.Windows.Forms.Padding(10);
            this.installedModsListBox.Name = "installedModsListBox";
            this.installedModsListBox.ScrollAlwaysVisible = true;
            this.installedModsListBox.Size = new System.Drawing.Size(380, 381);
            this.installedModsListBox.TabIndex = 4;
            this.installedModsListBox.SelectedIndexChanged += new System.EventHandler(this.InstalledModsList_SelectedIndexChanged);
            // 
            // requiredModsList
            // 
            this.requiredModsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.requiredModsList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.requiredModsList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.requiredModsList.FormattingEnabled = true;
            this.requiredModsList.Location = new System.Drawing.Point(8, 23);
            this.requiredModsList.Margin = new System.Windows.Forms.Padding(10);
            this.requiredModsList.Name = "requiredModsList";
            this.requiredModsList.ScrollAlwaysVisible = true;
            this.requiredModsList.Size = new System.Drawing.Size(388, 381);
            this.requiredModsList.TabIndex = 5;
            this.requiredModsList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.RequiredModsList_DrawItem);
            // 
            // startModButton
            // 
            this.startModButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.startModButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.startModButton.Enabled = false;
            this.startModButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startModButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.startModButton.Location = new System.Drawing.Point(632, 32);
            this.startModButton.Name = "startModButton";
            this.startModButton.Size = new System.Drawing.Size(121, 48);
            this.startModButton.TabIndex = 6;
            this.startModButton.Text = "START MOD";
            this.startModButton.UseVisualStyleBackColor = false;
            this.startModButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.ErrorImage = null;
            this.pictureBox.InitialImage = null;
            this.pictureBox.Location = new System.Drawing.Point(759, 32);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(50, 50);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox.TabIndex = 7;
            this.pictureBox.TabStop = false;
            // 
            // authorLabel
            // 
            this.authorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.authorLabel.AutoSize = true;
            this.authorLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.authorLabel.Location = new System.Drawing.Point(16, 544);
            this.authorLabel.Name = "authorLabel";
            this.authorLabel.Size = new System.Drawing.Size(198, 13);
            this.authorLabel.TabIndex = 8;
            this.authorLabel.Text = "Created by FragJacker and IgorTheLight";
            // 
            // advancedStartOptionsLabel
            // 
            this.advancedStartOptionsLabel.AutoSize = true;
            this.advancedStartOptionsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.advancedStartOptionsLabel.Location = new System.Drawing.Point(18, 48);
            this.advancedStartOptionsLabel.Name = "advancedStartOptionsLabel";
            this.advancedStartOptionsLabel.Size = new System.Drawing.Size(123, 13);
            this.advancedStartOptionsLabel.TabIndex = 9;
            this.advancedStartOptionsLabel.Text = "Advanced Start Options:";
            // 
            // devCheckBox
            // 
            this.devCheckBox.AutoSize = true;
            this.devCheckBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.devCheckBox.Location = new System.Drawing.Point(18, 69);
            this.devCheckBox.Name = "devCheckBox";
            this.devCheckBox.Size = new System.Drawing.Size(137, 17);
            this.devCheckBox.TabIndex = 10;
            this.devCheckBox.Text = "-dev: Developers Mode";
            this.devCheckBox.UseVisualStyleBackColor = true;
            this.devCheckBox.CheckedChanged += new System.EventHandler(this.DevCheckBox_CheckedChanged);
            // 
            // nomoviesCheckBox
            // 
            this.nomoviesCheckBox.AutoSize = true;
            this.nomoviesCheckBox.Checked = true;
            this.nomoviesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.nomoviesCheckBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.nomoviesCheckBox.Location = new System.Drawing.Point(18, 83);
            this.nomoviesCheckBox.Name = "nomoviesCheckBox";
            this.nomoviesCheckBox.Size = new System.Drawing.Size(155, 17);
            this.nomoviesCheckBox.TabIndex = 11;
            this.nomoviesCheckBox.Text = "-nomovies: No Intro Movies";
            this.nomoviesCheckBox.UseVisualStyleBackColor = true;
            this.nomoviesCheckBox.CheckedChanged += new System.EventHandler(this.NomoviesCheckBox_CheckedChanged);
            // 
            // highpolyCheckBox
            // 
            this.highpolyCheckBox.AutoSize = true;
            this.highpolyCheckBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.highpolyCheckBox.Location = new System.Drawing.Point(18, 97);
            this.highpolyCheckBox.Name = "highpolyCheckBox";
            this.highpolyCheckBox.Size = new System.Drawing.Size(256, 17);
            this.highpolyCheckBox.TabIndex = 12;
            this.highpolyCheckBox.Text = "-forcehighpoly: High Poly models at any Distance";
            this.highpolyCheckBox.UseVisualStyleBackColor = true;
            this.highpolyCheckBox.CheckedChanged += new System.EventHandler(this.HighpolyCheckBox_CheckedChanged);
            // 
            // optimizationsCheckBox
            // 
            this.optimizationsCheckBox.AutoSize = true;
            this.optimizationsCheckBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.optimizationsCheckBox.Location = new System.Drawing.Point(18, 111);
            this.optimizationsCheckBox.Name = "optimizationsCheckBox";
            this.optimizationsCheckBox.Size = new System.Drawing.Size(314, 17);
            this.optimizationsCheckBox.TabIndex = 13;
            this.optimizationsCheckBox.Text = "/high /affinity 6: Set to highest thread priority and CPU affinity";
            this.optimizationsCheckBox.UseVisualStyleBackColor = true;
            this.optimizationsCheckBox.CheckedChanged += new System.EventHandler(this.OptimizationsCheckBox_CheckedChanged);
            // 
            // mergeButton
            // 
            this.mergeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mergeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.mergeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mergeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.mergeButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.mergeButton.Location = new System.Drawing.Point(632, 83);
            this.mergeButton.Name = "mergeButton";
            this.mergeButton.Size = new System.Drawing.Size(121, 43);
            this.mergeButton.TabIndex = 14;
            this.mergeButton.Text = "Merge Mods...";
            this.mergeButton.UseVisualStyleBackColor = false;
            this.mergeButton.Click += new System.EventHandler(this.ModMergeButton_Click);
            // 
            // startVanillaButton
            // 
            this.startVanillaButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.startVanillaButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.startVanillaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startVanillaButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.startVanillaButton.Location = new System.Drawing.Point(505, 32);
            this.startVanillaButton.Name = "startVanillaButton";
            this.startVanillaButton.Size = new System.Drawing.Size(121, 48);
            this.startVanillaButton.TabIndex = 15;
            this.startVanillaButton.Text = "START BASE GAME";
            this.startVanillaButton.UseVisualStyleBackColor = false;
            this.startVanillaButton.Click += new System.EventHandler(this.StartVanillaGameButton_Click);
            // 
            // gameLAAStatusLabel
            // 
            this.gameLAAStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gameLAAStatusLabel.Location = new System.Drawing.Point(315, 82);
            this.gameLAAStatusLabel.Margin = new System.Windows.Forms.Padding(3);
            this.gameLAAStatusLabel.Name = "gameLAAStatusLabel";
            this.gameLAAStatusLabel.Size = new System.Drawing.Size(210, 15);
            this.gameLAAStatusLabel.TabIndex = 16;
            this.gameLAAStatusLabel.Text = "Game: LAA Active";
            // 
            // graphicsConfigLAAStatusLabel
            // 
            this.graphicsConfigLAAStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.graphicsConfigLAAStatusLabel.Location = new System.Drawing.Point(315, 96);
            this.graphicsConfigLAAStatusLabel.Margin = new System.Windows.Forms.Padding(3);
            this.graphicsConfigLAAStatusLabel.Name = "graphicsConfigLAAStatusLabel";
            this.graphicsConfigLAAStatusLabel.Size = new System.Drawing.Size(210, 15);
            this.graphicsConfigLAAStatusLabel.TabIndex = 17;
            this.graphicsConfigLAAStatusLabel.Text = "Graphics Config: LAA Active";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Location = new System.Drawing.Point(12, 131);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(804, 413);
            this.panel1.TabIndex = 18;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.installedModsLabel);
            this.splitContainer1.Panel1.Controls.Add(this.installedModsListBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.requiredModsLabel);
            this.splitContainer1.Panel2.Controls.Add(this.requiredModsList);
            this.splitContainer1.Size = new System.Drawing.Size(801, 407);
            this.splitContainer1.SplitterDistance = 394;
            this.splitContainer1.TabIndex = 19;
            // 
            // toggleLAAButton
            // 
            this.toggleLAAButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.toggleLAAButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.toggleLAAButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.toggleLAAButton.Location = new System.Drawing.Point(318, 42);
            this.toggleLAAButton.Name = "toggleLAAButton";
            this.toggleLAAButton.Size = new System.Drawing.Size(104, 31);
            this.toggleLAAButton.TabIndex = 20;
            this.toggleLAAButton.Text = "TOGGLE LAA";
            this.toggleLAAButton.UseVisualStyleBackColor = false;
            this.toggleLAAButton.Click += new System.EventHandler(this.ButtonToggleLAA_Click);
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // downloadButton
            // 
            this.downloadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.downloadButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.downloadButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.downloadButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.downloadButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.downloadButton.Location = new System.Drawing.Point(505, 83);
            this.downloadButton.Name = "downloadButton";
            this.downloadButton.Size = new System.Drawing.Size(121, 43);
            this.downloadButton.TabIndex = 21;
            this.downloadButton.Text = "Download Mod...";
            this.downloadButton.UseVisualStyleBackColor = false;
            this.downloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // settingsButton
            // 
            this.settingsButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.settingsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.settingsButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.settingsButton.Location = new System.Drawing.Point(199, 42);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(104, 31);
            this.settingsButton.TabIndex = 22;
            this.settingsButton.Text = "SETTINGS";
            this.settingsButton.UseVisualStyleBackColor = false;
            this.settingsButton.Click += new System.EventHandler(this.SettingsButton_Click);
            // 
            // homePageLinkLabel
            // 
            this.homePageLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.homePageLinkLabel.AutoSize = true;
            this.homePageLinkLabel.Location = new System.Drawing.Point(728, 544);
            this.homePageLinkLabel.Name = "homePageLinkLabel";
            this.homePageLinkLabel.Size = new System.Drawing.Size(85, 13);
            this.homePageLinkLabel.TabIndex = 23;
            this.homePageLinkLabel.TabStop = true;
            this.homePageLinkLabel.Text = "Visit Home Page";
            this.homePageLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.HomePageLinkLabel_LinkClicked);
            // 
            // ModManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.ClientSize = new System.Drawing.Size(828, 561);
            this.Controls.Add(this.homePageLinkLabel);
            this.Controls.Add(this.settingsButton);
            this.Controls.Add(this.downloadButton);
            this.Controls.Add(this.optimizationsCheckBox);
            this.Controls.Add(this.toggleLAAButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gameLAAStatusLabel);
            this.Controls.Add(this.graphicsConfigLAAStatusLabel);
            this.Controls.Add(this.startVanillaButton);
            this.Controls.Add(this.mergeButton);
            this.Controls.Add(this.highpolyCheckBox);
            this.Controls.Add(this.nomoviesCheckBox);
            this.Controls.Add(this.devCheckBox);
            this.Controls.Add(this.advancedStartOptionsLabel);
            this.Controls.Add(this.authorLabel);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.startModButton);
            this.Controls.Add(this.currentDirectoryLabel);
            this.Controls.Add(this.currentDirTextBox);
            this.MinimumSize = new System.Drawing.Size(828, 600);
            this.Name = "ModManagerForm";
            this.Text = "DoW Mod Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ModManagerForm_Closing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox currentDirTextBox;
        private System.Windows.Forms.Label currentDirectoryLabel;
        private System.Windows.Forms.Label installedModsLabel;
        private System.Windows.Forms.Label requiredModsLabel;
        private System.Windows.Forms.ListBox requiredModsList;
        private System.Windows.Forms.Button startModButton;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label authorLabel;
        private System.Windows.Forms.Label advancedStartOptionsLabel;
        private System.Windows.Forms.CheckBox devCheckBox;
        private System.Windows.Forms.CheckBox nomoviesCheckBox;
        private System.Windows.Forms.CheckBox highpolyCheckBox;
        private System.Windows.Forms.CheckBox optimizationsCheckBox;
        private System.Windows.Forms.Button mergeButton;
        private System.Windows.Forms.ListBox installedModsListBox;
        private System.Windows.Forms.Button startVanillaButton;
        private System.Windows.Forms.Label gameLAAStatusLabel;
        private System.Windows.Forms.Label graphicsConfigLAAStatusLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button toggleLAAButton;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.Button downloadButton;
        private System.Windows.Forms.Button settingsButton;
        private System.Windows.Forms.LinkLabel homePageLinkLabel;
    }
}