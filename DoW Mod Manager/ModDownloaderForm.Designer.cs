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
            this.popularModsLabel.Location = new System.Drawing.Point(13, 13);
            this.popularModsLabel.Name = "popularModsLabel";
            this.popularModsLabel.Size = new System.Drawing.Size(89, 13);
            this.popularModsLabel.TabIndex = 0;
            this.popularModsLabel.Text = "Popular mods for ";
            // 
            // modListBox
            // 
            this.modListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.modListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.modListBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.modListBox.FormattingEnabled = true;
            this.modListBox.Location = new System.Drawing.Point(16, 56);
            this.modListBox.Name = "modListBox";
            this.modListBox.ScrollAlwaysVisible = true;
            this.modListBox.Size = new System.Drawing.Size(448, 420);
            this.modListBox.TabIndex = 1;
            // 
            // openModDBButton
            // 
            this.openModDBButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.openModDBButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.openModDBButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.openModDBButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.openModDBButton.Location = new System.Drawing.Point(484, 30);
            this.openModDBButton.Name = "openModDBButton";
            this.openModDBButton.Size = new System.Drawing.Size(109, 38);
            this.openModDBButton.TabIndex = 2;
            this.openModDBButton.Text = "Open ModDB.com";
            this.openModDBButton.UseVisualStyleBackColor = false;
            this.openModDBButton.Click += new System.EventHandler(this.OpenModDBButton_Click);
            // 
            // openModPageButton
            // 
            this.openModPageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.openModPageButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.openModPageButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.openModPageButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.openModPageButton.Location = new System.Drawing.Point(484, 74);
            this.openModPageButton.Name = "openModPageButton";
            this.openModPageButton.Size = new System.Drawing.Size(109, 38);
            this.openModPageButton.TabIndex = 4;
            this.openModPageButton.Text = "Open mod page";
            this.openModPageButton.UseVisualStyleBackColor = false;
            this.openModPageButton.Click += new System.EventHandler(this.OpenModPageButton_Click);
            // 
            // downloadModButton
            // 
            this.downloadModButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.downloadModButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.downloadModButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.downloadModButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.downloadModButton.Location = new System.Drawing.Point(484, 118);
            this.downloadModButton.Name = "downloadModButton";
            this.downloadModButton.Size = new System.Drawing.Size(109, 38);
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
            this.searchTextBox.Location = new System.Drawing.Point(16, 30);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(448, 20);
            this.searchTextBox.TabIndex = 5;
            this.searchTextBox.Text = "Search...";
            this.searchTextBox.TextChanged += new System.EventHandler(this.SearchTextBox_TextChanged);
            this.searchTextBox.Enter += new System.EventHandler(this.SearchTextBox_Enter);
            this.searchTextBox.Leave += new System.EventHandler(this.SearchTextBox_Leave);
            // 
            // ModDownloaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.ClientSize = new System.Drawing.Size(621, 490);
            this.Controls.Add(this.searchTextBox);
            this.Controls.Add(this.openModPageButton);
            this.Controls.Add(this.openModDBButton);
            this.Controls.Add(this.downloadModButton);
            this.Controls.Add(this.modListBox);
            this.Controls.Add(this.popularModsLabel);
            this.MinimumSize = new System.Drawing.Size(650, 530);
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