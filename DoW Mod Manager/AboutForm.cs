using System.Diagnostics;
using System.Drawing;
using System;
using System.Windows.Forms;

namespace DoW_Mod_Manager
{
    public partial class AboutForm : Form
    {
        private readonly ModManagerForm modManager;

        public AboutForm(ModManagerForm form)
        {
            InitializeComponent();

            modManager = form;

            // Use the same icon as executable
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            pictureBox.Image = Icon.ToBitmap();

            // Get values from ModManagerForm
            autoupdateCheckBox.Checked = modManager.GetSetting(ModManagerForm.AUTOUPDATE) == 1;
            if (modManager.GetSetting(ModManagerForm.AOT_COMPILATION) == 1)
                AOTCompilationRadioButton.Checked = true;
            else if (modManager.GetSetting(ModManagerForm.MULTITHREADED_JIT) == 1)
                multithreadedJITCompilationRadioButton.Checked = true;
            else
                singlethreadedJITCompilationRadioButton.Checked = true;

            // We have to add those methods to the EventHandler here so we could avoid accidental firing of those methods after we would change the state of the CheckBox
            autoupdateCheckBox.CheckedChanged += new EventHandler(AutoupdateCheckBox_CheckedChanged);
            singlethreadedJITCompilationRadioButton.CheckedChanged += new EventHandler(SinglethreadedJITCompilationRadioButton_CheckedChanged);
            multithreadedJITCompilationRadioButton.CheckedChanged += new EventHandler(MultithreadedJITCompilationRadioButton_CheckedChanged);
            AOTCompilationRadioButton.CheckedChanged += new EventHandler(AOTCompilationRadioButton_CheckedChanged);
        }

        private void HomePageButton_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/Fragjacker/DoW-Mod-Manager");
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            DialogResult result = DownloadHelper.CheckForUpdates(silently: false);

            if (result == DialogResult.OK && modManager.GetSetting(ModManagerForm.AOT_COMPILATION) == 1)
                modManager.ChangeSetting(ModManagerForm.ACTION_STATE, (int)ModManagerForm.Action.CreateNativeImage);
        }

        private void SpecialThanks1LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://stackoverflow.com");
        }

        private void SpecialThanks2LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/tebjan/TimerTool");
        }

        private void SpecialThanks3LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/zayenCh/DownloadFile");
        }

        private void AutoupdateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (autoupdateCheckBox.Checked)
                modManager.ChangeSetting(ModManagerForm.AUTOUPDATE, 1);
            else
                modManager.ChangeSetting(ModManagerForm.AUTOUPDATE, 0);
        }

        private void SinglethreadedJITCompilationRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (singlethreadedJITCompilationRadioButton.Checked)
            {
                modManager.ChangeSetting(ModManagerForm.MULTITHREADED_JIT, 0);
                modManager.ChangeSetting(ModManagerForm.AOT_COMPILATION, 0);

                modManager.ChangeSetting(ModManagerForm.ACTION_STATE, (int)ModManagerForm.Action.DeleteJITProfileAndNativeImage);
            }
        }

        private void MultithreadedJITCompilationRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (multithreadedJITCompilationRadioButton.Checked)
            {
                modManager.ChangeSetting(ModManagerForm.MULTITHREADED_JIT, 1);
                modManager.ChangeSetting(ModManagerForm.AOT_COMPILATION, 0);

                modManager.ChangeSetting(ModManagerForm.ACTION_STATE, (int)ModManagerForm.Action.DeleteNativeImage);
            }
        }

        private void AOTCompilationRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (AOTCompilationRadioButton.Checked)
            {
                modManager.ChangeSetting(ModManagerForm.MULTITHREADED_JIT, 0);
                modManager.ChangeSetting(ModManagerForm.AOT_COMPILATION, 1);

                modManager.ChangeSetting(ModManagerForm.ACTION_STATE, (int)ModManagerForm.Action.CreateNativeImageAndDeleteJITProfile);
            }
        }
    }
}
