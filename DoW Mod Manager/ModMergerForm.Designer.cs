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
            this.buttonSaveFile = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonArrowUp = new System.Windows.Forms.Button();
            this.buttonArrowDown = new System.Windows.Forms.Button();
            this.buttonActivate = new System.Windows.Forms.Button();
            this.buttonDeactivate = new System.Windows.Forms.Button();
            this.loadedModBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.deleteButton = new System.Windows.Forms.Button();
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
            this.UsedModsList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.UsedModsList_DrawItem);
            this.UsedModsList.SelectedIndexChanged += new System.EventHandler(this.UsedModsList_SelectedIndexChanged);
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
            this.AvailableModsList.SelectedIndexChanged += new System.EventHandler(this.AvailableModsList_SelectedIndexChanged);
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
            // buttonSaveFile
            // 
            this.buttonSaveFile.Enabled = false;
            this.buttonSaveFile.Location = new System.Drawing.Point(364, 54);
            this.buttonSaveFile.Name = "buttonSaveFile";
            this.buttonSaveFile.Size = new System.Drawing.Size(114, 39);
            this.buttonSaveFile.TabIndex = 5;
            this.buttonSaveFile.Text = "SAVE";
            this.buttonSaveFile.UseVisualStyleBackColor = true;
            this.buttonSaveFile.Click += new System.EventHandler(this.buttonSaveFile_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.BackgroundImage = global::DoW_Mod_Manager.Properties.Resources.plus;
            this.buttonAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.buttonAdd.Location = new System.Drawing.Point(162, 324);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(38, 36);
            this.buttonAdd.TabIndex = 5;
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.BackgroundImage = global::DoW_Mod_Manager.Properties.Resources.minus;
            this.buttonRemove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.buttonRemove.Location = new System.Drawing.Point(206, 324);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(38, 36);
            this.buttonRemove.TabIndex = 5;
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonArrowUp
            // 
            this.buttonArrowUp.BackgroundImage = global::DoW_Mod_Manager.Properties.Resources.arrow_up;
            this.buttonArrowUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonArrowUp.Location = new System.Drawing.Point(426, 170);
            this.buttonArrowUp.Name = "buttonArrowUp";
            this.buttonArrowUp.Size = new System.Drawing.Size(51, 46);
            this.buttonArrowUp.TabIndex = 6;
            this.buttonArrowUp.UseVisualStyleBackColor = true;
            this.buttonArrowUp.Click += new System.EventHandler(this.buttonArrowUp_Click);
            // 
            // buttonArrowDown
            // 
            this.buttonArrowDown.BackgroundImage = global::DoW_Mod_Manager.Properties.Resources.arrow_down;
            this.buttonArrowDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonArrowDown.Location = new System.Drawing.Point(426, 222);
            this.buttonArrowDown.Name = "buttonArrowDown";
            this.buttonArrowDown.Size = new System.Drawing.Size(51, 46);
            this.buttonArrowDown.TabIndex = 6;
            this.buttonArrowDown.UseVisualStyleBackColor = true;
            this.buttonArrowDown.Click += new System.EventHandler(this.buttonArrowDown_Click);
            // 
            // buttonActivate
            // 
            this.buttonActivate.BackgroundImage = global::DoW_Mod_Manager.Properties.Resources.Checkmark;
            this.buttonActivate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonActivate.Location = new System.Drawing.Point(426, 118);
            this.buttonActivate.Name = "buttonActivate";
            this.buttonActivate.Size = new System.Drawing.Size(51, 46);
            this.buttonActivate.TabIndex = 6;
            this.buttonActivate.UseVisualStyleBackColor = true;
            this.buttonActivate.Click += new System.EventHandler(this.buttonActivate_Click);
            // 
            // buttonDeactivate
            // 
            this.buttonDeactivate.BackgroundImage = global::DoW_Mod_Manager.Properties.Resources.cross;
            this.buttonDeactivate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonDeactivate.Location = new System.Drawing.Point(426, 276);
            this.buttonDeactivate.Name = "buttonDeactivate";
            this.buttonDeactivate.Size = new System.Drawing.Size(51, 46);
            this.buttonDeactivate.TabIndex = 6;
            this.buttonDeactivate.UseVisualStyleBackColor = true;
            this.buttonDeactivate.Click += new System.EventHandler(this.buttonDeactivate_Click);
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
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(364, 9);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(114, 39);
            this.deleteButton.TabIndex = 10;
            this.deleteButton.Text = "DELETE";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // ModMergerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(489, 583);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.loadedModBox);
            this.Controls.Add(this.buttonDeactivate);
            this.Controls.Add(this.buttonArrowDown);
            this.Controls.Add(this.buttonActivate);
            this.Controls.Add(this.buttonArrowUp);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonSaveFile);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.AvailableModsList);
            this.Controls.Add(this.UsedModsList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labell);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ModMergerForm";
            this.Text = "Mod Merger v1.1";
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
        private System.Windows.Forms.Button buttonSaveFile;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonArrowUp;
        private System.Windows.Forms.Button buttonArrowDown;
        private System.Windows.Forms.Button buttonActivate;
        private System.Windows.Forms.Button buttonDeactivate;
        private System.Windows.Forms.ComboBox loadedModBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button deleteButton;
    }
}