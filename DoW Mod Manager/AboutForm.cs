using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DoW_Mod_Manager
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();

            // Use the same icon as executable
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            pictureBox.Image = Icon.ToBitmap();
        }
        private void HomePageButton_Click(object sender, System.EventArgs e)
        {
            Process.Start("https://github.com/Fragjacker/DoW-Mod-Manager");
        }

        private void OKButton_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void UpdateButton_Click(object sender, System.EventArgs e)
        {
            DownloadHelper.CheckForUpdates();
        }

        //private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        //{
        //    MessageBox.Show(e.ProgressPercentage + " %");
        //}



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
    }
}
