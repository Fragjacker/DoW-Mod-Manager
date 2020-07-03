using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows.Forms;

namespace DoW_Mod_Manager
{
    public partial class AboutForm : Form
    {
        private readonly string versionTextURL = "https://github.com/IgorTheLight/DoW-Mod-Manager/blob/master/DoW%20Mod%20Manager/LatestStable/version";
        private readonly string executableURL = "https://github.com/IgorTheLight/DoW-Mod-Manager/blob/master/DoW%20Mod%20Manager/LatestStable/DoW_Mod_Manager_v2.0.0.exe";
        private readonly string executablePath = Directory.GetCurrentDirectory() + "\\DoW_Mod_Manager_v2.0.0";

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
            //DownloadFile(new Uri(versionTextURL), versionTextPath);

            string stringCurrentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            int currentVersion = Convert.ToInt32(stringCurrentVersion.Replace(".", ""));

            string latestStringVersion = DownloadAsString(versionTextURL);
            int katestVersion = Convert.ToInt32(latestStringVersion);

            if (currentVersion < katestVersion)
                DownloadFile(executableURL, executablePath);
            else
                Debug.WriteLine("You have the latest version!");
        }

        private void DownloadFile(string address, string downloadPath)
        {
            // WebClient is more high level than HttpClient
            using (WebClient webClient = new WebClient())
            {
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);

                try
                {
                    webClient.DownloadFileAsync(new Uri(address), downloadPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Download Error :");
                }
            }
        }

        public string DownloadAsString(string address)
        {
            using (WebClient webClient = new WebClient())
            {
                return webClient.DownloadString(address);
            }
        }

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            //MessageBox.Show(e.ProgressPercentage + " %");
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            MessageBox.Show("Download completed!");
        }
    }
}
