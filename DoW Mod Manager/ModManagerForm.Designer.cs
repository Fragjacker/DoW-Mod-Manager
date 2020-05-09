﻿namespace DoW_Mod_Manager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModManagerForm));
            this.currentDirTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.InstalledModsList = new System.Windows.Forms.ListBox();
            this.RequiredModsList = new System.Windows.Forms.ListBox();
            this.startButton = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.devCheckBox = new System.Windows.Forms.CheckBox();
            this.nomoviesCheckBox = new System.Windows.Forms.CheckBox();
            this.highpolyCheckBox = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.startVanillaGameButton = new System.Windows.Forms.Button();
            this.SoulstormLAAStatusLabel = new System.Windows.Forms.Label();
            this.GraphicsConfigLAAStatusLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.buttonToggleLAA = new System.Windows.Forms.Button();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
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
            this.currentDirTextBox.Location = new System.Drawing.Point(179, 6);
            this.currentDirTextBox.Name = "currentDirTextBox";
            this.currentDirTextBox.ReadOnly = true;
            this.currentDirTextBox.Size = new System.Drawing.Size(627, 20);
            this.currentDirTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.label1.Location = new System.Drawing.Point(16, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Your current Soulstorm directory";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Currently Installed Mods";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.label4.Location = new System.Drawing.Point(7, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Required Mods";
            // 
            // InstalledModsList
            // 
            this.InstalledModsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InstalledModsList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.InstalledModsList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.InstalledModsList.FormattingEnabled = true;
            this.InstalledModsList.Location = new System.Drawing.Point(6, 23);
            this.InstalledModsList.Margin = new System.Windows.Forms.Padding(10);
            this.InstalledModsList.Name = "InstalledModsList";
            this.InstalledModsList.ScrollAlwaysVisible = true;
            this.InstalledModsList.Size = new System.Drawing.Size(380, 381);
            this.InstalledModsList.TabIndex = 10;
            this.InstalledModsList.SelectedIndexChanged += new System.EventHandler(this.InstalledModsList_SelectedIndexChanged);
            // 
            // RequiredModsList
            // 
            this.RequiredModsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RequiredModsList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.RequiredModsList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.RequiredModsList.FormattingEnabled = true;
            this.RequiredModsList.Location = new System.Drawing.Point(8, 23);
            this.RequiredModsList.Margin = new System.Windows.Forms.Padding(10);
            this.RequiredModsList.Name = "RequiredModsList";
            this.RequiredModsList.ScrollAlwaysVisible = true;
            this.RequiredModsList.Size = new System.Drawing.Size(388, 381);
            this.RequiredModsList.TabIndex = 10;
            this.RequiredModsList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.RequiredModsList_DrawItem);
            // 
            // startButton
            // 
            this.startButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.startButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.startButton.Enabled = false;
            this.startButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.startButton.Location = new System.Drawing.Point(632, 32);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(121, 48);
            this.startButton.TabIndex = 11;
            this.startButton.Text = "START MOD";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.StartButton_Click);
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
            this.pictureBox.TabIndex = 12;
            this.pictureBox.TabStop = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.label2.Location = new System.Drawing.Point(16, 544);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Written by FragJacker";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.label5.Location = new System.Drawing.Point(18, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Advanced Start Options:";
            // 
            // devCheckBox
            // 
            this.devCheckBox.AutoSize = true;
            this.devCheckBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.devCheckBox.Location = new System.Drawing.Point(18, 69);
            this.devCheckBox.Name = "devCheckBox";
            this.devCheckBox.Size = new System.Drawing.Size(137, 17);
            this.devCheckBox.TabIndex = 15;
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
            this.nomoviesCheckBox.TabIndex = 16;
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
            this.highpolyCheckBox.TabIndex = 17;
            this.highpolyCheckBox.Text = "-forcehighpoly: High Poly models at any Distance";
            this.highpolyCheckBox.UseVisualStyleBackColor = true;
            this.highpolyCheckBox.CheckedChanged += new System.EventHandler(this.HighpolyCheckBox_CheckedChanged);
            // 
            // modMergeButton
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.button1.Location = new System.Drawing.Point(568, 82);
            this.button1.Name = "modMergeButton";
            this.button1.Size = new System.Drawing.Size(121, 43);
            this.button1.TabIndex = 18;
            this.button1.Text = "Merge Mods...";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.ModMergeButton_Click);
            // 
            // startVanillaGameButton
            // 
            this.startVanillaGameButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.startVanillaGameButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.startVanillaGameButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startVanillaGameButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.startVanillaGameButton.Location = new System.Drawing.Point(505, 32);
            this.startVanillaGameButton.Name = "startVanillaGameButton";
            this.startVanillaGameButton.Size = new System.Drawing.Size(121, 48);
            this.startVanillaGameButton.TabIndex = 19;
            this.startVanillaGameButton.Text = "START BASE GAME";
            this.startVanillaGameButton.UseVisualStyleBackColor = false;
            this.startVanillaGameButton.Click += new System.EventHandler(this.StartVanillaGameButton_Click);
            // 
            // SoulstormLAAStatusLabel
            // 
            this.SoulstormLAAStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SoulstormLAAStatusLabel.Location = new System.Drawing.Point(315, 82);
            this.SoulstormLAAStatusLabel.Margin = new System.Windows.Forms.Padding(3);
            this.SoulstormLAAStatusLabel.Name = "SoulstormLAAStatusLabel";
            this.SoulstormLAAStatusLabel.Size = new System.Drawing.Size(210, 15);
            this.SoulstormLAAStatusLabel.TabIndex = 20;
            this.SoulstormLAAStatusLabel.Text = "Soulstorm.exe: LAA Active";
            // 
            // GraphicsConfigLAAStatusLabel
            // 
            this.GraphicsConfigLAAStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GraphicsConfigLAAStatusLabel.Location = new System.Drawing.Point(315, 96);
            this.GraphicsConfigLAAStatusLabel.Margin = new System.Windows.Forms.Padding(3);
            this.GraphicsConfigLAAStatusLabel.Name = "GraphicsConfigLAAStatusLabel";
            this.GraphicsConfigLAAStatusLabel.Size = new System.Drawing.Size(210, 15);
            this.GraphicsConfigLAAStatusLabel.TabIndex = 21;
            this.GraphicsConfigLAAStatusLabel.Text = "GraphicsConfig.exe: LAA Active";
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
            this.panel1.TabIndex = 22;
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
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.InstalledModsList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.RequiredModsList);
            this.splitContainer1.Size = new System.Drawing.Size(801, 407);
            this.splitContainer1.SplitterDistance = 394;
            this.splitContainer1.TabIndex = 23;
            // 
            // buttonToggleLAA
            // 
            this.buttonToggleLAA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.buttonToggleLAA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonToggleLAA.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.buttonToggleLAA.Location = new System.Drawing.Point(318, 42);
            this.buttonToggleLAA.Name = "buttonToggleLAA";
            this.buttonToggleLAA.Size = new System.Drawing.Size(104, 31);
            this.buttonToggleLAA.TabIndex = 23;
            this.buttonToggleLAA.Text = "TOGGLE LAA";
            this.buttonToggleLAA.UseVisualStyleBackColor = false;
            this.buttonToggleLAA.Click += new System.EventHandler(this.ButtonToggleLAA_Click);
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // ModManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.ClientSize = new System.Drawing.Size(828, 561);
            this.Controls.Add(this.buttonToggleLAA);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.SoulstormLAAStatusLabel);
            this.Controls.Add(this.GraphicsConfigLAAStatusLabel);
            this.Controls.Add(this.startVanillaGameButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.highpolyCheckBox);
            this.Controls.Add(this.nomoviesCheckBox);
            this.Controls.Add(this.devCheckBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.currentDirTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "ModManagerForm";
            this.Text = "DoW Mod Manager v1.57";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ModManagerForm_Closing);
            this.Load += new System.EventHandler(this.ModManagerForm_Load);
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox RequiredModsList;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox devCheckBox;
        private System.Windows.Forms.CheckBox nomoviesCheckBox;
        private System.Windows.Forms.CheckBox highpolyCheckBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox InstalledModsList;
        private System.Windows.Forms.Button startVanillaGameButton;
        private System.Windows.Forms.Label SoulstormLAAStatusLabel;
        private System.Windows.Forms.Label GraphicsConfigLAAStatusLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button buttonToggleLAA;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
    }
}

