namespace DoW_Mod_Manager
{
    partial class SystemPerformanceForm
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
            this.timerResolutionLabel = new System.Windows.Forms.Label();
            this.setTimerResolutionButton = new System.Windows.Forms.Button();
            this.defaultTimerResolutionButton = new System.Windows.Forms.Button();
            this.currentTimerResolutionLabel = new System.Windows.Forms.Label();
            this.currentTimerResolutionTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // timerResolutionLabel
            // 
            this.timerResolutionLabel.AutoSize = true;
            this.timerResolutionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.timerResolutionLabel.Location = new System.Drawing.Point(17, 16);
            this.timerResolutionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.timerResolutionLabel.Name = "timerResolutionLabel";
            this.timerResolutionLabel.Size = new System.Drawing.Size(110, 16);
            this.timerResolutionLabel.TabIndex = 0;
            this.timerResolutionLabel.Text = "Timer Resolution";
            // 
            // setTimerResolutionButton
            // 
            this.setTimerResolutionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.setTimerResolutionButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.setTimerResolutionButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.setTimerResolutionButton.Location = new System.Drawing.Point(21, 104);
            this.setTimerResolutionButton.Margin = new System.Windows.Forms.Padding(4);
            this.setTimerResolutionButton.Name = "setTimerResolutionButton";
            this.setTimerResolutionButton.Size = new System.Drawing.Size(160, 47);
            this.setTimerResolutionButton.TabIndex = 2;
            this.setTimerResolutionButton.Text = "Lower Timer Resolution";
            this.setTimerResolutionButton.UseVisualStyleBackColor = false;
            this.setTimerResolutionButton.Click += new System.EventHandler(this.SetTimerResolutionButton_Click);
            // 
            // defaultTimerResolutionButton
            // 
            this.defaultTimerResolutionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.defaultTimerResolutionButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.defaultTimerResolutionButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.defaultTimerResolutionButton.Location = new System.Drawing.Point(234, 104);
            this.defaultTimerResolutionButton.Margin = new System.Windows.Forms.Padding(4);
            this.defaultTimerResolutionButton.Name = "defaultTimerResolutionButton";
            this.defaultTimerResolutionButton.Size = new System.Drawing.Size(160, 47);
            this.defaultTimerResolutionButton.TabIndex = 3;
            this.defaultTimerResolutionButton.Text = "Default Timer Resolution";
            this.defaultTimerResolutionButton.UseVisualStyleBackColor = false;
            this.defaultTimerResolutionButton.Click += new System.EventHandler(this.DefaultTimerResolutionButton_Click);
            // 
            // currentTimerResolutionLabel
            // 
            this.currentTimerResolutionLabel.AutoSize = true;
            this.currentTimerResolutionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.currentTimerResolutionLabel.Location = new System.Drawing.Point(17, 50);
            this.currentTimerResolutionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.currentTimerResolutionLabel.Name = "currentTimerResolutionLabel";
            this.currentTimerResolutionLabel.Size = new System.Drawing.Size(158, 16);
            this.currentTimerResolutionLabel.TabIndex = 4;
            this.currentTimerResolutionLabel.Text = "Current Timer Resolution:";
            // 
            // currentTimerResolutionTextBox
            // 
            this.currentTimerResolutionTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.currentTimerResolutionTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.currentTimerResolutionTextBox.Location = new System.Drawing.Point(192, 47);
            this.currentTimerResolutionTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.currentTimerResolutionTextBox.Name = "currentTimerResolutionTextBox";
            this.currentTimerResolutionTextBox.ReadOnly = true;
            this.currentTimerResolutionTextBox.Size = new System.Drawing.Size(134, 22);
            this.currentTimerResolutionTextBox.TabIndex = 5;
            // 
            // SystemPerformanceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.ClientSize = new System.Drawing.Size(416, 166);
            this.Controls.Add(this.currentTimerResolutionTextBox);
            this.Controls.Add(this.currentTimerResolutionLabel);
            this.Controls.Add(this.defaultTimerResolutionButton);
            this.Controls.Add(this.setTimerResolutionButton);
            this.Controls.Add(this.timerResolutionLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "SystemPerformanceForm";
            this.Text = "Sytem Performance";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label timerResolutionLabel;
        private System.Windows.Forms.Button setTimerResolutionButton;
        private System.Windows.Forms.Button defaultTimerResolutionButton;
        private System.Windows.Forms.Label currentTimerResolutionLabel;
        private System.Windows.Forms.TextBox currentTimerResolutionTextBox;
    }
}