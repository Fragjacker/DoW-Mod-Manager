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
            this.howtolabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.UsedModsList = new System.Windows.Forms.ListBox();
            this.usedModsLabel = new System.Windows.Forms.Label();
            this.AvailableModsList = new System.Windows.Forms.ListBox();
            this.availableModsLabel = new System.Windows.Forms.Label();
            this.buttonSaveFile = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonArrowUp = new System.Windows.Forms.Button();
            this.buttonArrowDown = new System.Windows.Forms.Button();
            this.buttonActivate = new System.Windows.Forms.Button();
            this.buttonDeactivate = new System.Windows.Forms.Button();
            this.loadedModBox = new System.Windows.Forms.ComboBox();
            this.ActivationButtonTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.DeactivationButtonTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.AddModTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.ArrowUpTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.ArrowDownTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.RemoveModTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.disclaimerLabel = new System.Windows.Forms.Label();
            this.deleteButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // howtolabel
            // 
            this.howtolabel.AutoSize = true;
            this.howtolabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.howtolabel.Location = new System.Drawing.Point(15, 62);
            this.howtolabel.Name = "howtolabel";
            this.howtolabel.Size = new System.Drawing.Size(410, 39);
            this.howtolabel.TabIndex = 0;
            this.howtolabel.Text = resources.GetString("howtolabel.Text");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.label1.Location = new System.Drawing.Point(14, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
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
            this.UsedModsList.Location = new System.Drawing.Point(3, 22);
            this.UsedModsList.Name = "UsedModsList";
            this.UsedModsList.ScrollAlwaysVisible = true;
            this.UsedModsList.Size = new System.Drawing.Size(475, 134);
            this.UsedModsList.TabIndex = 3;
            this.UsedModsList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.UsedModsList_DrawItem);
            this.UsedModsList.SelectedIndexChanged += new System.EventHandler(this.UsedModsList_SelectedIndexChanged);
            // 
            // usedModsLabel
            // 
            this.usedModsLabel.AutoSize = true;
            this.usedModsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.usedModsLabel.Location = new System.Drawing.Point(3, 3);
            this.usedModsLabel.Name = "usedModsLabel";
            this.usedModsLabel.Size = new System.Drawing.Size(64, 13);
            this.usedModsLabel.TabIndex = 4;
            this.usedModsLabel.Text = "Used Mods:";
            // 
            // AvailableModsList
            // 
            this.AvailableModsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AvailableModsList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.AvailableModsList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.AvailableModsList.FormattingEnabled = true;
            this.AvailableModsList.Location = new System.Drawing.Point(3, 3);
            this.AvailableModsList.Name = "AvailableModsList";
            this.AvailableModsList.ScrollAlwaysVisible = true;
            this.AvailableModsList.Size = new System.Drawing.Size(475, 186);
            this.AvailableModsList.TabIndex = 3;
            this.AvailableModsList.SelectedIndexChanged += new System.EventHandler(this.AvailableModsList_SelectedIndexChanged);
            // 
            // availableModsLabel
            // 
            this.availableModsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.availableModsLabel.AutoSize = true;
            this.availableModsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.availableModsLabel.Location = new System.Drawing.Point(3, 206);
            this.availableModsLabel.Name = "availableModsLabel";
            this.availableModsLabel.Size = new System.Drawing.Size(82, 13);
            this.availableModsLabel.TabIndex = 4;
            this.availableModsLabel.Text = "Available Mods:";
            // 
            // buttonSaveFile
            // 
            this.buttonSaveFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.buttonSaveFile.Enabled = false;
            this.buttonSaveFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSaveFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.buttonSaveFile.Location = new System.Drawing.Point(442, 110);
            this.buttonSaveFile.Name = "buttonSaveFile";
            this.buttonSaveFile.Size = new System.Drawing.Size(114, 39);
            this.buttonSaveFile.TabIndex = 5;
            this.buttonSaveFile.Text = "SAVE MERGE";
            this.buttonSaveFile.UseVisualStyleBackColor = false;
            this.buttonSaveFile.Click += new System.EventHandler(this.ButtonSaveFile_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.buttonAdd.BackgroundImage = global::DoW_Mod_Manager.Properties.Resources.plus_disabled;
            this.buttonAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonAdd.Enabled = false;
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.buttonAdd.Location = new System.Drawing.Point(130, 166);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(50, 50);
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
            this.buttonRemove.BackgroundImage = global::DoW_Mod_Manager.Properties.Resources.minus_disabled;
            this.buttonRemove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonRemove.Enabled = false;
            this.buttonRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.buttonRemove.Location = new System.Drawing.Point(186, 166);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(50, 50);
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
            this.buttonArrowUp.BackgroundImage = global::DoW_Mod_Manager.Properties.Resources.arrow_up_disabled;
            this.buttonArrowUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonArrowUp.Enabled = false;
            this.buttonArrowUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonArrowUp.Location = new System.Drawing.Point(484, 55);
            this.buttonArrowUp.Name = "buttonArrowUp";
            this.buttonArrowUp.Size = new System.Drawing.Size(51, 46);
            this.buttonArrowUp.TabIndex = 6;
            this.ArrowUpTooltip.SetToolTip(this.buttonArrowUp, "Move selected Mod one row up in the sort order.");
            this.buttonArrowUp.UseVisualStyleBackColor = false;
            this.buttonArrowUp.Click += new System.EventHandler(this.ButtonArrowUp_Click);
            // 
            // buttonArrowDown
            // 
            this.buttonArrowDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonArrowDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.buttonArrowDown.BackgroundImage = global::DoW_Mod_Manager.Properties.Resources.arrow_down_disabled;
            this.buttonArrowDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonArrowDown.Enabled = false;
            this.buttonArrowDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonArrowDown.Location = new System.Drawing.Point(484, 107);
            this.buttonArrowDown.Name = "buttonArrowDown";
            this.buttonArrowDown.Size = new System.Drawing.Size(51, 46);
            this.buttonArrowDown.TabIndex = 6;
            this.ArrowDownTooltip.SetToolTip(this.buttonArrowDown, "Move selected Mod one row down in the sort order.");
            this.buttonArrowDown.UseVisualStyleBackColor = false;
            this.buttonArrowDown.Click += new System.EventHandler(this.ButtonArrowDown_Click);
            // 
            // buttonActivate
            // 
            this.buttonActivate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonActivate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.buttonActivate.BackgroundImage = global::DoW_Mod_Manager.Properties.Resources.checkmark_disabled;
            this.buttonActivate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonActivate.Enabled = false;
            this.buttonActivate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonActivate.Location = new System.Drawing.Point(484, 3);
            this.buttonActivate.Name = "buttonActivate";
            this.buttonActivate.Size = new System.Drawing.Size(51, 46);
            this.buttonActivate.TabIndex = 6;
            this.ActivationButtonTooltip.SetToolTip(this.buttonActivate, "Set selected Mod as active.");
            this.buttonActivate.UseVisualStyleBackColor = false;
            this.buttonActivate.Click += new System.EventHandler(this.ButtonActivate_Click);
            // 
            // buttonDeactivate
            // 
            this.buttonDeactivate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeactivate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.buttonDeactivate.BackgroundImage = global::DoW_Mod_Manager.Properties.Resources.cross_disabled;
            this.buttonDeactivate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonDeactivate.Enabled = false;
            this.buttonDeactivate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDeactivate.Location = new System.Drawing.Point(484, 161);
            this.buttonDeactivate.Name = "buttonDeactivate";
            this.buttonDeactivate.Size = new System.Drawing.Size(51, 46);
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
            this.loadedModBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.loadedModBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.loadedModBox.FormattingEnabled = true;
            this.loadedModBox.Location = new System.Drawing.Point(17, 128);
            this.loadedModBox.Name = "loadedModBox";
            this.loadedModBox.Size = new System.Drawing.Size(409, 21);
            this.loadedModBox.TabIndex = 8;
            this.loadedModBox.SelectedIndexChanged += new System.EventHandler(this.LoadedModBox_SelectedIndexChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(18, 155);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.buttonActivate);
            this.splitContainer1.Panel1.Controls.Add(this.availableModsLabel);
            this.splitContainer1.Panel1.Controls.Add(this.buttonArrowUp);
            this.splitContainer1.Panel1.Controls.Add(this.buttonArrowDown);
            this.splitContainer1.Panel1.Controls.Add(this.buttonRemove);
            this.splitContainer1.Panel1.Controls.Add(this.usedModsLabel);
            this.splitContainer1.Panel1.Controls.Add(this.buttonDeactivate);
            this.splitContainer1.Panel1.Controls.Add(this.buttonAdd);
            this.splitContainer1.Panel1.Controls.Add(this.UsedModsList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.AvailableModsList);
            this.splitContainer1.Size = new System.Drawing.Size(538, 427);
            this.splitContainer1.SplitterDistance = 219;
            this.splitContainer1.TabIndex = 10;
            // 
            // disclaimerLabel
            // 
            this.disclaimerLabel.AutoSize = true;
            this.disclaimerLabel.ForeColor = System.Drawing.Color.Red;
            this.disclaimerLabel.Location = new System.Drawing.Point(15, 9);
            this.disclaimerLabel.Name = "disclaimerLabel";
            this.disclaimerLabel.Size = new System.Drawing.Size(459, 39);
            this.disclaimerLabel.TabIndex = 11;
            this.disclaimerLabel.Text = resources.GetString("disclaimerLabel.Text");
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.deleteButton.Enabled = false;
            this.deleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.deleteButton.Location = new System.Drawing.Point(442, 62);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(114, 39);
            this.deleteButton.TabIndex = 5;
            this.deleteButton.Text = "DELETE MERGE";
            this.deleteButton.UseVisualStyleBackColor = false;
            this.deleteButton.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // ModMergerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.ClientSize = new System.Drawing.Size(568, 583);
            this.Controls.Add(this.disclaimerLabel);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.loadedModBox);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.buttonSaveFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.howtolabel);
            this.MinimumSize = new System.Drawing.Size(505, 622);
            this.Name = "ModMergerForm";
            this.Text = "Mod Merger v2.0.0.0";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label howtolabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox UsedModsList;
        private System.Windows.Forms.Label usedModsLabel;
        private System.Windows.Forms.ListBox AvailableModsList;
        private System.Windows.Forms.Label availableModsLabel;
        private System.Windows.Forms.Button buttonSaveFile;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonArrowUp;
        private System.Windows.Forms.Button buttonArrowDown;
        private System.Windows.Forms.Button buttonActivate;
        private System.Windows.Forms.Button buttonDeactivate;
        private System.Windows.Forms.ComboBox loadedModBox;
        private System.Windows.Forms.ToolTip ActivationButtonTooltip;
        private System.Windows.Forms.ToolTip DeactivationButtonTooltip;
        private System.Windows.Forms.ToolTip AddModTooltip;
        private System.Windows.Forms.ToolTip ArrowUpTooltip;
        private System.Windows.Forms.ToolTip ArrowDownTooltip;
        private System.Windows.Forms.ToolTip RemoveModTooltip;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label disclaimerLabel;
        private System.Windows.Forms.Button deleteButton;
    }
}