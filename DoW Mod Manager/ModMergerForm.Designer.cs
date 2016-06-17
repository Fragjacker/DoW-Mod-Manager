namespace DoW_Mod_Manager
{
    partial class ModMergerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModMergerForm));
            this.labell = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.UsedModsList = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.AvailableModsList = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.loadedModBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labell
            // 
            this.labell.AutoSize = true;
            this.labell.Location = new System.Drawing.Point(12, 9);
            this.labell.Name = "labell";
            this.labell.Size = new System.Drawing.Size(345, 39);
            this.labell.TabIndex = 0;
            this.labell.Text = "HOW-TO: Use the dropdown list to select a Mod that you want to have \r\nmore mods l" +
    "oaded with. \r\nThen select and add the available Mods from the Lists below";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Currently loaded Mod";
            // 
            // UsedModsList
            // 
            this.UsedModsList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.UsedModsList.FormattingEnabled = true;
            this.UsedModsList.Location = new System.Drawing.Point(18, 119);
            this.UsedModsList.Name = "UsedModsList";
            this.UsedModsList.ScrollAlwaysVisible = true;
            this.UsedModsList.Size = new System.Drawing.Size(392, 199);
            this.UsedModsList.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Used Mods:";
            // 
            // AvailableModsList
            // 
            this.AvailableModsList.FormattingEnabled = true;
            this.AvailableModsList.Location = new System.Drawing.Point(18, 366);
            this.AvailableModsList.Name = "AvailableModsList";
            this.AvailableModsList.ScrollAlwaysVisible = true;
            this.AvailableModsList.Size = new System.Drawing.Size(392, 199);
            this.AvailableModsList.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 347);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Available Mods:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(364, 54);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(114, 39);
            this.button2.TabIndex = 5;
            this.button2.Text = "SAVE...";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.button3.Location = new System.Drawing.Point(162, 324);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(38, 36);
            this.button3.TabIndex = 5;
            this.button3.Text = "+";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.button4.Location = new System.Drawing.Point(206, 324);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(38, 36);
            this.button4.TabIndex = 5;
            this.button4.Text = "-";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(426, 170);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(51, 46);
            this.button5.TabIndex = 6;
            this.button5.Text = "UP";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(426, 222);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(51, 46);
            this.button6.TabIndex = 6;
            this.button6.Text = "DOWN";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.BackgroundImage = global::DoW_Mod_Manager.Properties.Resources.Checkmark;
            this.button7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button7.Location = new System.Drawing.Point(426, 118);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(51, 46);
            this.button7.TabIndex = 6;
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.BackgroundImage = global::DoW_Mod_Manager.Properties.Resources.cross;
            this.button8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button8.Location = new System.Drawing.Point(426, 276);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(51, 46);
            this.button8.TabIndex = 6;
            this.button8.UseVisualStyleBackColor = true;
            // 
            // loadedModBox
            // 
            this.loadedModBox.FormattingEnabled = true;
            this.loadedModBox.Location = new System.Drawing.Point(18, 72);
            this.loadedModBox.Name = "loadedModBox";
            this.loadedModBox.Size = new System.Drawing.Size(330, 21);
            this.loadedModBox.TabIndex = 8;
            this.loadedModBox.Text = "--None--";
            this.loadedModBox.SelectedIndexChanged += new System.EventHandler(this.loadedModBox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 568);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Written by Cosmocrat";
            // 
            // ModMergerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(489, 583);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.loadedModBox);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.AvailableModsList);
            this.Controls.Add(this.UsedModsList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labell);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ModMergerForm";
            this.Text = "Mod Merger";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labell;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox UsedModsList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox AvailableModsList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.ComboBox loadedModBox;
        private System.Windows.Forms.Label label4;
    }
}