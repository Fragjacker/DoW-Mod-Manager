using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using IWshRuntimeLibrary;

namespace DoW_Mod_Manager
{
    public partial class AboutForm : Form
    {
        // !
        private const string VERSION_TEXT_URL = "https://raw.githubusercontent.com/IgorTheLight/DoW-Mod-Manager/master/DoW%20Mod%20Manager/LatestStable/version";
        // !
        private readonly string executableURL  = "https://github.com/IgorTheLight/DoW-Mod-Manager/raw/master/DoW%20Mod%20Manager/LatestStable/DoW%20Mod%20Manager.exe";
        private string executablePath = Directory.GetCurrentDirectory();
        private string oldexecutablePath = "";
        private string latestStringVersion = "";

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
            // Checking version of this executable
            string currentStringVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            currentStringVersion = currentStringVersion.Remove(5);          // Delete the last version number and a point
            int currentVersion = Convert.ToInt32(currentStringVersion.Replace(".", ""));

            // Checking version mentioned in "version" file on github
            latestStringVersion = DownloadString(VERSION_TEXT_URL);
            if (latestStringVersion.Length == 0)
            {
                ThemedMessageBox.Show("There is no data in \"version\" file on GitHub!", "Warning!");
                return;
            }

            int latestVersion;
            try
            {
                latestVersion = Convert.ToInt32(latestStringVersion.Replace(".", ""));
            }
            catch (Exception ex)
            {
                ThemedMessageBox.Show("There is something wrong with version number in \"version\" file on GitHub!\n" + ex.Message, "Warning!");
                return;
            }

            if (currentVersion < latestVersion)
            {
                oldexecutablePath = executablePath + $"\\DoW Mod Manager v{currentStringVersion}.exe";
                executablePath += $"\\DoW Mod Manager v{latestStringVersion}.exe";
                DownloadFile(executableURL, executablePath);
            }
            else
                ThemedMessageBox.Show("You have the latest version!", "Good news!");
        }

        /// <summary>
        /// This method terminates the original program, deletes the old executable, starts the new app
        /// and creates a new shortcut on the desktop for it.
        /// This code was taken from https://www.codeproject.com/articles/31454/how-to-make-your-application-delete-itself-immedia.
        /// </summary>
        private void CleanupAndStartApp()
        {
            // Start new downloaded executable
            Process.Start(executablePath);
            // Delete the old executable after 3 seconds have passed using cmd!
            Process.Start("cmd.exe", "/C choice /C Y /N /D Y /T 3 & Del \"" + oldexecutablePath + "\"");
            CreateShortcut($"DoW Mod Manager v{latestStringVersion}");
            Program.TerminateApp();
        }

        /// <summary>
        /// This method creates a new shortcut of a newly created Mod Manager file!
        /// </summary>
        /// <param name="shortcutName"></param>
        // Request the inlining of this method
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void CreateShortcut(string shortcutName)
        {
            string shortcutLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), shortcutName + ".lnk");
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

            shortcut.Description = "The latest version of the DoW Mod Manager!";   // The description of the shortcut
            shortcut.TargetPath = executablePath;                                  // The path of the file that will launch when the shortcut is run
            shortcut.Save();                                                       // Save the shortcut
        }

        private string DownloadString(string address)
        {
            string str = "";

            using (WebClient webClient = new WebClient())
            {
                try
                {
                    str = webClient.DownloadString(address);

                    // Async method is harder to handle
                    //webClient.DownloadStringAsync(new Uri(address), str);
                }
                catch (Exception ex)
                {
                    ThemedMessageBox.Show(ex.Message, "Download Error:");
                }
            }
            return str;
        }

        private void DownloadFile(string address, string downloadPath)
        {
            // WebClient is more high level than HttpClient
            using (WebClient webClient = new WebClient())
            {
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                //webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);

                try
                {
                    webClient.DownloadFileAsync(new Uri(address), downloadPath);
                }
                catch (Exception ex)
                {
                    ThemedMessageBox.Show(ex.Message, "Download Error:");
                }
            }
        }

        //private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        //{
        //    MessageBox.Show(e.ProgressPercentage + " %");
        //}

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            ThemedMessageBox.Show("Download completed!\nApplication will restart to take effect", "Good news!");

            Process.Start(executablePath);
            CleanupAndStartApp();
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
    }
}
