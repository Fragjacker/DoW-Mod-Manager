namespace DoW_Mod_Manager
{
    partial class AboutForm
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
            this.aboutLabel1 = new System.Windows.Forms.Label();
            this.OKButton = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.aboutLabel2 = new System.Windows.Forms.Label();
            this.homePageButton = new System.Windows.Forms.Button();
            this.updateButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // aboutLabel1
            // 
            this.aboutLabel1.AutoSize = true;
            this.aboutLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.aboutLabel1.Location = new System.Drawing.Point(77, 12);
            this.aboutLabel1.Name = "aboutLabel1";
            this.aboutLabel1.Size = new System.Drawing.Size(287, 13);
            this.aboutLabel1.TabIndex = 0;
            this.aboutLabel1.Text = "This application was created by FragJacker (original author)";
            // 
            // OKButton
            // 
            this.OKButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.OKButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OKButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.OKButton.Location = new System.Drawing.Point(164, 143);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(106, 38);
            this.OKButton.TabIndex = 2;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = false;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(17, 12);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(50, 50);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox.TabIndex = 3;
            this.pictureBox.TabStop = false;
            // 
            // aboutLabel2
            // 
            this.aboutLabel2.AutoSize = true;
            this.aboutLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.aboutLabel2.Location = new System.Drawing.Point(77, 34);
            this.aboutLabel2.Name = "aboutLabel2";
            this.aboutLabel2.Size = new System.Drawing.Size(205, 13);
            this.aboutLabel2.TabIndex = 4;
            this.aboutLabel2.Text = " and IgorTheLight (new active contributor)";
            // 
            // homePageButton
            // 
            this.homePageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.homePageButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.homePageButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.homePageButton.Location = new System.Drawing.Point(26, 143);
            this.homePageButton.Name = "homePageButton";
            this.homePageButton.Size = new System.Drawing.Size(106, 38);
            this.homePageButton.TabIndex = 5;
            this.homePageButton.Text = "Visit home page";
            this.homePageButton.UseVisualStyleBackColor = false;
            this.homePageButton.Click += new System.EventHandler(this.HomePageButton_Click);
            // 
            // updateButton
            // 
            this.updateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.updateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.updateButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.updateButton.Location = new System.Drawing.Point(303, 143);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(106, 38);
            this.updateButton.TabIndex = 6;
            this.updateButton.Text = "Check for updates";
            this.updateButton.UseVisualStyleBackColor = false;
            this.updateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.ClientSize = new System.Drawing.Size(438, 193);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.homePageButton);
            this.Controls.Add(this.aboutLabel2);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.aboutLabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About DoW Mod Manager";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label aboutLabel1;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label aboutLabel2;
        private System.Windows.Forms.Button homePageButton;
        private System.Windows.Forms.Button updateButton;
    }
}