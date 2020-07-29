using IWshRuntimeLibrary;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace DoW_Mod_Manager
{
    static class DownloadHelper
    {
        private const string VERSION_TEXT_URL = "https://raw.githubusercontent.com/Fragjacker/DoW-Mod-Manager/master/DoW%20Mod%20Manager/LatestStable/version";
        private static readonly string executableURL = "https://github.com/Fragjacker/DoW-Mod-Manager/raw/master/DoW%20Mod%20Manager/LatestStable/DoW%20Mod%20Manager.exe";

        private static string executablePath = Directory.GetCurrentDirectory();
        private static string oldexecutablePath = "";
        private static string latestStringVersion = "";
        private static readonly string currentStringVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString().Remove(5);
        private static readonly int currentVersion = Convert.ToInt32(currentStringVersion.Replace(".", ""));

        public static DialogResult CheckForUpdates(bool silently)
        {
            // Checking version mentioned in "version" file on GitHub
            latestStringVersion = DownloadString(VERSION_TEXT_URL);

            int latestVersion;

            // TODO: It would be a good idea to further differentiate between "true" and "false" branches
            if (silently)
            {
                if (latestStringVersion.Length == 0)
                    return DialogResult.Abort;

                try
                {
                    latestVersion = Convert.ToInt32(latestStringVersion.Replace(".", ""));
                }
                catch (Exception)
                {
                    return DialogResult.Abort;
                }

                if (currentVersion < latestVersion)
                    return ThemedDialogueBox.Show($"The new DoW Mod Manager v{latestStringVersion} is available. Do you wish to update now?", "New update available");
            }
            else
            {
                if (latestStringVersion.Length == 0)
                {
                    ThemedMessageBox.Show("There is no data in \"version\" file on GitHub!", "Warning!");
                    return DialogResult.Abort;
                }

                try
                {
                    latestVersion = Convert.ToInt32(latestStringVersion.Replace(".", ""));
                }
                catch (Exception ex)
                {
                    ThemedMessageBox.Show("There is something wrong with version number in \"version\" file on GitHub!\n" + ex.Message, "Warning!");
                    return DialogResult.Abort;
                }

                if (currentVersion < latestVersion)
                {
                    DownloadUpdate();
                    return DialogResult.OK;
                }
                else
                {
                    ThemedMessageBox.Show("You have the latest version!", "Good news!");
                    return DialogResult.Cancel;
                }
            }
            return DialogResult.Abort;
        }

        // Request the inlining of this method
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DownloadUpdate()
        {
            oldexecutablePath = executablePath + $"\\DoW Mod Manager v{currentStringVersion}.exe";
            executablePath += $"\\DoW Mod Manager v{latestStringVersion}.exe";
            DownloadFile(executableURL, executablePath);
        }

        private static string DownloadString(string address)
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

        private static void DownloadFile(string address, string downloadPath)
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
        //    ThemedMessageBox.Show(e.ProgressPercentage + " %");
        //}

        private static void Completed(object sender, AsyncCompletedEventArgs e)
        {
            ThemedMessageBox.Show("Download completed!\nApplication will restart to take effect", "Good news!");
            CleanupAndStartApp();
        }

        /// <summary>
        /// This method terminates the original program, deletes the old executable, starts the new app
        /// and creates a new shortcut on the desktop for it.
        /// Delete code was taken from https://www.codeproject.com/articles/31454/how-to-make-your-application-delete-itself-immedia.
        /// </summary>
        private static void CleanupAndStartApp()
        {
            // Start new downloaded exectuable
            Process.Start(executablePath);
            // Delete the old executable after 3 seconds have passed using cmd!
            Process.Start("cmd.exe", "/C choice /C Y /N /D Y /T 1 & Del \"" + oldexecutablePath + "\"");
            CreateShortcut($"DoW Mod Manager v{latestStringVersion}");
            Program.TerminateApp();
        }

        /// <summary>
        /// This method creates a new shortcut of a newly created Mod Manager file!
        /// </summary>
        /// <param name="shortcutName"></param>
        private static void CreateShortcut(string shortcutName)
        {
            string shortcutLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), shortcutName + ".lnk");
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

            shortcut.Description = $"The latest DoW Mod Manager v{latestStringVersion}";
            shortcut.TargetPath = executablePath;
            shortcut.WorkingDirectory = Directory.GetCurrentDirectory();
            shortcut.Save();
        }
    }
}
