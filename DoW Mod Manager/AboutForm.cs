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
            {
                AOTCompilationCheckBox.Checked = true;
                multithreadedJITCompilationCheckBox.Enabled = false;
            }
            else if (modManager.GetSetting(ModManagerForm.MULTITHREADED_JIT) == 1)
            {
                multithreadedJITCompilationCheckBox.Checked = true;
                AOTCompilationCheckBox.Enabled = false;
            }

            // We have to add those methods to the EventHandler here so we could avoid accidental firing of those methods after we would change the state of the CheckBox
            autoupdateCheckBox.CheckedChanged += new EventHandler(AutoupdateCheckBox_CheckedChanged);
            multithreadedJITCompilationCheckBox.CheckedChanged += new EventHandler(MultithreadedJITCompilationCheckBox_CheckedChanged);
            AOTCompilationCheckBox.CheckedChanged += new EventHandler(AOTCompilationCheckBox_CheckedChanged);
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

        private void MultithreadedJITCompilationCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (multithreadedJITCompilationCheckBox.Checked)
            {
                modManager.ChangeSetting(ModManagerForm.MULTITHREADED_JIT, 1);
                modManager.ChangeSetting(ModManagerForm.ACTION_STATE, (int)ModManagerForm.Action.None);

                AOTCompilationCheckBox.Checked = false;
                AOTCompilationCheckBox.Enabled = false;
            }
            else
            {
                modManager.ChangeSetting(ModManagerForm.MULTITHREADED_JIT, 0);
                modManager.ChangeSetting(ModManagerForm.ACTION_STATE, (int)ModManagerForm.Action.DeleteJITProfile);

                AOTCompilationCheckBox.Enabled = true;
            }
        }

        private void AOTCompilationCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (AOTCompilationCheckBox.Checked)
            {
                modManager.ChangeSetting(ModManagerForm.AOT_COMPILATION, 1);
                modManager.ChangeSetting(ModManagerForm.ACTION_STATE, (int)ModManagerForm.Action.CreateNativeImage);

                multithreadedJITCompilationCheckBox.Checked = false;
                multithreadedJITCompilationCheckBox.Enabled = false;
            }
            else
            {
                modManager.ChangeSetting(ModManagerForm.AOT_COMPILATION, 0);
                modManager.ChangeSetting(ModManagerForm.ACTION_STATE, (int)ModManagerForm.Action.DeleteNativeImage);
                
                multithreadedJITCompilationCheckBox.Enabled = true;
            }
        }
    }
}
