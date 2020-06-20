namespace DoW_Mod_Manager
{
    partial class ModDownloaderForm
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
            this.popularModsLabel = new System.Windows.Forms.Label();
            this.modListBox = new System.Windows.Forms.ListBox();
            this.openModDBButton = new System.Windows.Forms.Button();
            this.openModPageButton = new System.Windows.Forms.Button();
            this.downloadModButton = new System.Windows.Forms.Button();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // popularModsLabel
            // 
            this.popularModsLabel.AutoSize = true;
            this.popularModsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.popularModsLabel.Location = new System.Drawing.Point(17, 16);
            this.popularModsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.popularModsLabel.Name = "popularModsLabel";
            this.popularModsLabel.Size = new System.Drawing.Size(113, 16);
            this.popularModsLabel.TabIndex = 0;
            this.popularModsLabel.Text = "Popular mods for ";
            // 
            // modListBox
            // 
            this.modListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.modListBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.modListBox.FormattingEnabled = true;
            this.modListBox.ItemHeight = 16;
            this.modListBox.Location = new System.Drawing.Point(21, 69);
            this.modListBox.Margin = new System.Windows.Forms.Padding(4);
            this.modListBox.Name = "modListBox";
            this.modListBox.ScrollAlwaysVisible = true;
            this.modListBox.Size = new System.Drawing.Size(531, 468);
            this.modListBox.TabIndex = 1;
            // 
            // openModDBButton
            // 
            this.openModDBButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.openModDBButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.openModDBButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.openModDBButton.Location = new System.Drawing.Point(581, 37);
            this.openModDBButton.Margin = new System.Windows.Forms.Padding(4);
            this.openModDBButton.Name = "openModDBButton";
            this.openModDBButton.Size = new System.Drawing.Size(145, 47);
            this.openModDBButton.TabIndex = 2;
            this.openModDBButton.Text = "Open ModDB.com";
            this.openModDBButton.UseVisualStyleBackColor = false;
            this.openModDBButton.Click += new System.EventHandler(this.OpenModDBButton_Click);
            // 
            // openModPageButton
            // 
            this.openModPageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.openModPageButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.openModPageButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.openModPageButton.Location = new System.Drawing.Point(581, 91);
            this.openModPageButton.Margin = new System.Windows.Forms.Padding(4);
            this.openModPageButton.Name = "openModPageButton";
            this.openModPageButton.Size = new System.Drawing.Size(145, 47);
            this.openModPageButton.TabIndex = 4;
            this.openModPageButton.Text = "Open mod page";
            this.openModPageButton.UseVisualStyleBackColor = false;
            this.openModPageButton.Click += new System.EventHandler(this.OpenModPageButton_Click);
            // 
            // downloadModButton
            // 
            this.downloadModButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.downloadModButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.downloadModButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.downloadModButton.Location = new System.Drawing.Point(581, 145);
            this.downloadModButton.Margin = new System.Windows.Forms.Padding(4);
            this.downloadModButton.Name = "downloadModButton";
            this.downloadModButton.Size = new System.Drawing.Size(145, 47);
            this.downloadModButton.TabIndex = 3;
            this.downloadModButton.Text = "Download mod";
            this.downloadModButton.UseVisualStyleBackColor = false;
            this.downloadModButton.Click += new System.EventHandler(this.DownloadModButton_Click);
            // 
            // searchTextBox
            // 
            this.searchTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.searchTextBox.ForeColor = System.Drawing.Color.Gray;
            this.searchTextBox.Location = new System.Drawing.Point(21, 37);
            this.searchTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(531, 22);
            this.searchTextBox.TabIndex = 5;
            this.searchTextBox.Text = "Search...";
            this.searchTextBox.TextChanged += new System.EventHandler(this.SearchTextBox_TextChanged);
            this.searchTextBox.Enter += new System.EventHandler(this.SearchTextBox_Enter);
            this.searchTextBox.Leave += new System.EventHandler(this.SearchTextBox_Leave);
            // 
            // ModDownloaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.ClientSize = new System.Drawing.Size(763, 554);
            this.Controls.Add(this.searchTextBox);
            this.Controls.Add(this.openModPageButton);
            this.Controls.Add(this.openModDBButton);
            this.Controls.Add(this.downloadModButton);
            this.Controls.Add(this.modListBox);
            this.Controls.Add(this.popularModsLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(757, 545);
            this.Name = "ModDownloaderForm";
            this.Text = "Mod Downloader";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label popularModsLabel;
        private System.Windows.Forms.ListBox modListBox;
        private System.Windows.Forms.Button openModDBButton;
        private System.Windows.Forms.Button openModPageButton;
        private System.Windows.Forms.Button downloadModButton;
        private System.Windows.Forms.TextBox searchTextBox;
    }
}