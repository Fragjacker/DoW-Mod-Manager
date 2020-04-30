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
            this.components = new System.ComponentModel.Container();
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
            this.ActivationButtonTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.DeactivationButtonTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.AddModTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.ArrowUpTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.ArrowDownTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.RemoveModTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labell
            // 
            this.labell.AutoSize = true;
            this.labell.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.labell.Location = new System.Drawing.Point(16, 11);
            this.labell.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labell.Name = "labell";
            this.labell.Size = new System.Drawing.Size(421, 48);
            this.labell.TabIndex = 0;
            this.labell.Text = "HOW-TO: Use the dropdown list to select a Mod that you want to have \r\nmore mods l" +
    "oaded with. \r\nThen select and add the available Mods from the Lists below";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.label1.Location = new System.Drawing.Point(20, 66);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Currently loaded Mod";
            // 
            // UsedModsList
            // 
            this.UsedModsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UsedModsList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.UsedModsList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.UsedModsList.FormattingEnabled = true;
            this.UsedModsList.Location = new System.Drawing.Point(4, 27);
            this.UsedModsList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.UsedModsList.Name = "UsedModsList";
            this.UsedModsList.ScrollAlwaysVisible = true;
            this.UsedModsList.Size = new System.Drawing.Size(527, 186);
            this.UsedModsList.TabIndex = 3;
            this.UsedModsList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.UsedModsList_DrawItem);
            this.UsedModsList.SelectedIndexChanged += new System.EventHandler(this.UsedModsList_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.label2.Location = new System.Drawing.Point(4, 4);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Used Mods:";
            // 
            // AvailableModsList
            // 
            this.AvailableModsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AvailableModsList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.AvailableModsList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.AvailableModsList.FormattingEnabled = true;
            this.AvailableModsList.ItemHeight = 16;
            this.AvailableModsList.Location = new System.Drawing.Point(4, 4);
            this.AvailableModsList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.AvailableModsList.Name = "AvailableModsList";
            this.AvailableModsList.ScrollAlwaysVisible = true;
            this.AvailableModsList.Size = new System.Drawing.Size(527, 260);
            this.AvailableModsList.TabIndex = 3;
            this.AvailableModsList.SelectedIndexChanged += new System.EventHandler(this.AvailableModsList_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.label3.Location = new System.Drawing.Point(4, 279);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Available Mods:";
            // 
            // buttonSaveFile
            // 
            this.buttonSaveFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.buttonSaveFile.Enabled = false;
            this.buttonSaveFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSaveFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.buttonSaveFile.Location = new System.Drawing.Point(485, 66);
            this.buttonSaveFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonSaveFile.Name = "buttonSaveFile";
            this.buttonSaveFile.Size = new System.Drawing.Size(152, 48);
            this.buttonSaveFile.TabIndex = 5;
            this.buttonSaveFile.Text = "SAVE";
            this.buttonSaveFile.UseVisualStyleBackColor = false;
            this.buttonSaveFile.Click += new System.EventHandler(this.ButtonSaveFile_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.buttonAdd.BackgroundImage = global::DoW_Mod_Manager.Properties.Resources.plus;
            this.buttonAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.buttonAdd.Location = new System.Drawing.Point(173, 230);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(67, 62);
            this.buttonAdd.TabIndex = 5;
            this.AddModTooltip.SetToolTip(this.buttonAdd, "Adds selected mod from below to the above list of active mods to be loaded in-gam" +
        "e.");
            this.buttonAdd.UseVisualStyleBackColor = false;
            this.buttonAdd.Click += new System.EventHandler(this.ButtonAdd_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRemove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.buttonRemove.BackgroundImage = global::DoW_Mod_Manager.Properties.Resources.minus;
            this.buttonRemove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.buttonRemove.Location = new System.Drawing.Point(248, 230);
            this.buttonRemove.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(67, 62);
            this.buttonRemove.TabIndex = 5;
            this.RemoveModTooltip.SetToolTip(this.buttonRemove, "Remove selected mod from the list above and reinsert it to the list of available " +
        "Mods below.");
            this.buttonRemove.UseVisualStyleBackColor = false;
            this.buttonRemove.Click += new System.EventHandler(this.ButtonRemove_Click);
            // 
            // buttonArrowUp
            // 
            this.buttonArrowUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonArrowUp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.buttonArrowUp.BackgroundImage = global::DoW_Mod_Manager.Properties.Resources.arrow_up;
            this.buttonArrowUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonArrowUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonArrowUp.Location = new System.Drawing.Point(540, 68);
            this.buttonArrowUp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonArrowUp.Name = "buttonArrowUp";
            this.buttonArrowUp.Size = new System.Drawing.Size(68, 57);
            this.buttonArrowUp.TabIndex = 6;
            this.ArrowUpTooltip.SetToolTip(this.buttonArrowUp, "Move selected Mod one row up in the sort order.");
            this.buttonArrowUp.UseVisualStyleBackColor = false;
            this.buttonArrowUp.Click += new System.EventHandler(this.ButtonArrowUp_Click);
            // 
            // buttonArrowDown
            // 
            this.buttonArrowDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonArrowDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.buttonArrowDown.BackgroundImage = global::DoW_Mod_Manager.Properties.Resources.arrow_down;
            this.buttonArrowDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonArrowDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonArrowDown.Location = new System.Drawing.Point(540, 132);
            this.buttonArrowDown.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonArrowDown.Name = "buttonArrowDown";
            this.buttonArrowDown.Size = new System.Drawing.Size(68, 57);
            this.buttonArrowDown.TabIndex = 6;
            this.ArrowDownTooltip.SetToolTip(this.buttonArrowDown, "Move selected Mod one row down in the sort order.");
            this.buttonArrowDown.UseVisualStyleBackColor = false;
            this.buttonArrowDown.Click += new System.EventHandler(this.ButtonArrowDown_Click);
            // 
            // buttonActivate
            // 
            this.buttonActivate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonActivate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.buttonActivate.BackgroundImage = global::DoW_Mod_Manager.Properties.Resources.Checkmark;
            this.buttonActivate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonActivate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonActivate.Location = new System.Drawing.Point(540, 4);
            this.buttonActivate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonActivate.Name = "buttonActivate";
            this.buttonActivate.Size = new System.Drawing.Size(68, 57);
            this.buttonActivate.TabIndex = 6;
            this.ActivationButtonTooltip.SetToolTip(this.buttonActivate, "Set selected Mod as active.");
            this.buttonActivate.UseVisualStyleBackColor = false;
            this.buttonActivate.Click += new System.EventHandler(this.ButtonActivate_Click);
            // 
            // buttonDeactivate
            // 
            this.buttonDeactivate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeactivate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.buttonDeactivate.BackgroundImage = global::DoW_Mod_Manager.Properties.Resources.cross;
            this.buttonDeactivate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonDeactivate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDeactivate.Location = new System.Drawing.Point(540, 198);
            this.buttonDeactivate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonDeactivate.Name = "buttonDeactivate";
            this.buttonDeactivate.Size = new System.Drawing.Size(68, 57);
            this.buttonDeactivate.TabIndex = 6;
            this.DeactivationButtonTooltip.SetToolTip(this.buttonDeactivate, "Set the selected Mod as inactive. That mod won\'t be loaded in-game but will stay " +
        "inside the module file for later possible activation.");
            this.buttonDeactivate.UseVisualStyleBackColor = false;
            this.buttonDeactivate.Click += new System.EventHandler(this.ButtonDeactivate_Click);
            // 
            // loadedModBox
            // 
            this.loadedModBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loadedModBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.loadedModBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.loadedModBox.FormattingEnabled = true;
            this.loadedModBox.Location = new System.Drawing.Point(24, 89);
            this.loadedModBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.loadedModBox.Name = "loadedModBox";
            this.loadedModBox.Size = new System.Drawing.Size(439, 24);
            this.loadedModBox.TabIndex = 8;
            this.loadedModBox.Text = "--None--";
            this.loadedModBox.SelectedIndexChanged += new System.EventHandler(this.LoadedModBox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.label4.Location = new System.Drawing.Point(21, 698);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "Written by FragJacker";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(24, 122);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.buttonActivate);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.buttonArrowUp);
            this.splitContainer1.Panel1.Controls.Add(this.buttonArrowDown);
            this.splitContainer1.Panel1.Controls.Add(this.buttonRemove);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.buttonDeactivate);
            this.splitContainer1.Panel1.Controls.Add(this.buttonAdd);
            this.splitContainer1.Panel1.Controls.Add(this.UsedModsList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.AvailableModsList);
            this.splitContainer1.Size = new System.Drawing.Size(612, 574);
            this.splitContainer1.SplitterDistance = 295;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 10;
            // 
            // ModMergerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.ClientSize = new System.Drawing.Size(652, 718);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.loadedModBox);
            this.Controls.Add(this.buttonSaveFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labell);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(668, 757);
            this.Name = "ModMergerForm";
            this.Text = "Mod Merger v1.2";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
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
        private System.Windows.Forms.ToolTip ActivationButtonTooltip;
        private System.Windows.Forms.ToolTip DeactivationButtonTooltip;
        private System.Windows.Forms.ToolTip AddModTooltip;
        private System.Windows.Forms.ToolTip ArrowUpTooltip;
        private System.Windows.Forms.ToolTip ArrowDownTooltip;
        private System.Windows.Forms.ToolTip RemoveModTooltip;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}