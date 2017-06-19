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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModManagerForm));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.InstalledModsList = new System.Windows.Forms.ListBox();
            this.RequiredModsList = new System.Windows.Forms.ListBox();
            this.startButton1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.startVanillaGameButton = new System.Windows.Forms.Button();
            this.LAAStatusLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(179, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(215, 20);
            this.textBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Your current Soulstorm directory";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Currently Installed Mods";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(397, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Required Mods";
            // 
            // InstalledModsList
            // 
            this.InstalledModsList.FormattingEnabled = true;
            this.InstalledModsList.Location = new System.Drawing.Point(19, 137);
            this.InstalledModsList.Name = "InstalledModsList";
            this.InstalledModsList.ScrollAlwaysVisible = true;
            this.InstalledModsList.Size = new System.Drawing.Size(375, 407);
            this.InstalledModsList.TabIndex = 10;
            this.InstalledModsList.SelectedIndexChanged += new System.EventHandler(this.InstalledModsList_SelectedIndexChanged);
            // 
            // RequiredModsList
            // 
            this.RequiredModsList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.RequiredModsList.FormattingEnabled = true;
            this.RequiredModsList.Location = new System.Drawing.Point(400, 137);
            this.RequiredModsList.Name = "RequiredModsList";
            this.RequiredModsList.ScrollAlwaysVisible = true;
            this.RequiredModsList.Size = new System.Drawing.Size(375, 407);
            this.RequiredModsList.TabIndex = 10;
            this.RequiredModsList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.RequiredModsList_DrawItem);
            // 
            // startButton1
            // 
            this.startButton1.Enabled = false;
            this.startButton1.Location = new System.Drawing.Point(588, 32);
            this.startButton1.Name = "startButton1";
            this.startButton1.Size = new System.Drawing.Size(121, 48);
            this.startButton1.TabIndex = 11;
            this.startButton1.Text = "START MOD";
            this.startButton1.UseVisualStyleBackColor = true;
            this.startButton1.Click += new System.EventHandler(this.startButton1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(715, 32);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 547);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Written by Cosmocrat";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Advanced Start Options:";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(19, 63);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(137, 17);
            this.checkBox1.TabIndex = 15;
            this.checkBox1.Text = "-dev: Developers Mode";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(19, 77);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(155, 17);
            this.checkBox2.TabIndex = 16;
            this.checkBox2.Text = "-nomovies: No Intro Movies";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(19, 91);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(256, 17);
            this.checkBox3.TabIndex = 17;
            this.checkBox3.Text = "-forcehighpoly: High Poly models at any Distance";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.button1.Location = new System.Drawing.Point(524, 82);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 43);
            this.button1.TabIndex = 18;
            this.button1.Text = "Manage Mods...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // startVanillaGameButton
            // 
            this.startVanillaGameButton.Location = new System.Drawing.Point(461, 32);
            this.startVanillaGameButton.Name = "startVanillaGameButton";
            this.startVanillaGameButton.Size = new System.Drawing.Size(121, 48);
            this.startVanillaGameButton.TabIndex = 19;
            this.startVanillaGameButton.Text = "START BASE GAME";
            this.startVanillaGameButton.UseVisualStyleBackColor = true;
            this.startVanillaGameButton.Click += new System.EventHandler(this.startVanillaGameButton_Click);
            // 
            // LAAStatusLabel
            // 
            this.LAAStatusLabel.AutoSize = true;
            this.LAAStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LAAStatusLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LAAStatusLabel.Location = new System.Drawing.Point(501, 5);
            this.LAAStatusLabel.Name = "LAAStatusLabel";
            this.LAAStatusLabel.Size = new System.Drawing.Size(162, 24);
            this.LAAStatusLabel.TabIndex = 20;
            this.LAAStatusLabel.Text = "LAA Flag is Active";
            this.LAAStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ModManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.LAAStatusLabel);
            this.Controls.Add(this.startVanillaGameButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.startButton1);
            this.Controls.Add(this.RequiredModsList);
            this.Controls.Add(this.InstalledModsList);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ModManagerForm";
            this.Text = "DoW Mod Manager v1.4";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox RequiredModsList;
        private System.Windows.Forms.Button startButton1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox InstalledModsList;
        private System.Windows.Forms.Button startVanillaGameButton;
        private System.Windows.Forms.Label LAAStatusLabel;
    }
}

