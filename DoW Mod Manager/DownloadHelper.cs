using IWshRuntimeLibrary;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace DoW_Mod_Manager
{
    static class DownloadHelper
    {
        private const string EXE_VERSION_TEXT_URL = "https://raw.githubusercontent.com/Fragjacker/DoW-Mod-Manager/master/DoW%20Mod%20Manager/LatestStable/version";
        private const string EXECUTABLE_URL = "https://github.com/Fragjacker/DoW-Mod-Manager/raw/master/DoW%20Mod%20Manager/LatestStable/DoW%20Mod%20Manager.exe";

        private const string MODLIST_VERSION_TEXT_URL = "https://raw.githubusercontent.com/IgorTheLight/DoW-Mod-Manager/master/DoW%20Mod%20Manager/ModList/version";
        private const string MODLIST_URL = "https://github.com/IgorTheLight/DoW-Mod-Manager/raw/master/DoW%20Mod%20Manager/ModList/DoW%20Mod%20Manager%20Download%20Mods.list";

        private static readonly string currentDir = Directory.GetCurrentDirectory();
        private static string latestStringVersion = "";
        private static readonly Version currentVersion = Assembly.GetExecutingAssembly().GetName().Version;

        private static bool closeAndDelete;

        public static DialogResult CheckForUpdates(bool silently)
        {
            // That fixes problem in Windows 7
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            // Checking version mentioned in "version" file on GitHub
            latestStringVersion = DownloadString(EXE_VERSION_TEXT_URL);

            Version latestVersion;
            bool showMessageBox;
            string message;
            string title;
            DialogResult result;

            if (latestStringVersion.Length == 0)
            {
                showMessageBox = true;
                message = "There is no data in \"version\" file on GitHub!";
                title = "Warning!";
                result = DialogResult.Abort;
                goto SHOW_MESSAGEBOX;
            }

            try
            {
                latestVersion = new Version(latestStringVersion);
            }
            catch (Exception ex)
            {
                showMessageBox = true;
                message = "There is something wrong with version number in \"version\" file on GitHub!\n" + ex.Message;
                title = "Warning!";
                result = DialogResult.Abort;
                goto SHOW_MESSAGEBOX;
            }

            if (currentVersion < latestVersion)
            {
                return ThemedDialogueBox.Show($"The new DoW Mod Manager v{latestStringVersion} is available. Do you wish to update now?", "New update available", exeORmods: "exe");
            }
            else
            {
                showMessageBox = true;
                message = "You have the latest version!";
                title = "Good news!";
                result = DialogResult.Cancel;
            }

            SHOW_MESSAGEBOX:

            if (!silently)
            {
                if (showMessageBox)
                    ThemedMessageBox.Show(message, title);
            }

            return result;
        }

        // TODO: It looks very similar to CheckForUpdates()
        public static DialogResult CheckForNewModlist(bool silently)
        {
            // That fixes problem in Windows 7
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            // Checking version mentioned in "version" file on GitHub
            latestStringVersion = DownloadString(MODLIST_VERSION_TEXT_URL);

            int latestModlistVersion;
            bool showMessageBox;
            string message;
            string title;
            DialogResult result;

            if (latestStringVersion.Length == 0)
            {
                showMessageBox = true;
                message = "There is no data in \"version\" file on GitHub!";
                title = "Warning!";
                result = DialogResult.Abort;
                goto SHOW_MESSAGEBOX;
            }

            try
            {
                latestModlistVersion = Convert.ToInt32(latestStringVersion.Replace(".", ""));
            }
            catch (Exception ex)
            {
                showMessageBox = true;
                message = "There is something wrong with version number in \"version\" file on GitHub!\n" + ex.Message;
                title = "Warning!";
                result = DialogResult.Abort;
                goto SHOW_MESSAGEBOX;
            }

            // Check for version string in Modlist file
            int modlistVersion = 0;
            using (StreamReader file = new StreamReader(currentDir + "\\" + ModDownloaderForm.MODLIST_FILE))
            {
                string line;

                if ((line = file.ReadLine()) != null)
                {
                    try
                    {
                        modlistVersion = Convert.ToInt32(line.Replace(".", ""));
                    }
                    catch (Exception ex)
                    {
                        showMessageBox = true;
                        message = $"There is something wrong with version number in {ModDownloaderForm.MODLIST_FILE}\n" + ex.Message;
                        title = "Warning!";
                        result = DialogResult.Abort;
                        goto SHOW_MESSAGEBOX;
                    }
                }
            }

            if (modlistVersion < latestModlistVersion)
            {
                return ThemedDialogueBox.Show($"The new Modlist v{latestStringVersion} is available. Do you wish to update now?", "New update available", exeORmods: "mods");
            }
            else
            {
                showMessageBox = true;
                message = "You have the latest version!";
                title = "Good news!";
                result = DialogResult.Cancel;
            }

            SHOW_MESSAGEBOX:

            if (!silently)
            {
                if (showMessageBox)
                    ThemedMessageBox.Show(message, title);
            }

            return result;
        }

        // Request the inlining of this method
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DownloadUpdate()
        {
            DownloadFile(EXECUTABLE_URL, currentDir + $"\\DoW Mod Manager v{latestStringVersion}.exe", closeAndDeleteApplication: true);
        }

        // Request the inlining of this method
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DownloadModlist()
        {
            DownloadFile(MODLIST_URL, currentDir + $"\\{ModDownloaderForm.MODLIST_FILE}", closeAndDeleteApplication: false);
        }

        // Request the inlining of this method
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string DownloadString(string address)
        {
            string str = "";

            using (WebClient webClient = new WebClient())
            {
                try
                {
                    str = webClient.DownloadString(address);
                }
                catch (Exception ex)
                {
                    ThemedMessageBox.Show(ex.Message, "Download Error:");
                }
            }
            return str;
        }

        // Request the inlining of this method
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void DownloadFile(string address, string downloadPath, bool closeAndDeleteApplication)
        {
            // Start a new thread for the download part only.
            new Thread(() =>
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
                        while (webClient.IsBusy) { Application.DoEvents(); }
                        closeAndDelete = closeAndDeleteApplication;
                    }
                    catch (Exception ex)
                    {
                        ThemedMessageBox.Show(ex.Message, "Download Error:");
                    }
                }
            }).Start();
        }

        //private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        //{
        //    ThemedMessageBox.Show(e.ProgressPercentage + "%");
        //}

        private static void Completed(object sender, AsyncCompletedEventArgs e)
        {
            if (closeAndDelete)
            {
                ThemedMessageBox.Show("Download completed!\nApplication will restart to take effect", "Good news!");
                CleanupAndStartApp();
            }
        }

        /// <summary>
        /// This method terminates the original program, deletes the old executable, starts the new app
        /// and creates a new shortcut on the desktop for it.
        /// Delete code was taken from https://www.codeproject.com/articles/31454/how-to-make-your-application-delete-itself-immedia.
        /// </summary>
        // Request the inlining of this method
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void CleanupAndStartApp()
        {
            string oldExecutablePath = currentDir + "\\" + AppDomain.CurrentDomain.FriendlyName;
            string newExecutablePath = currentDir + $"\\DoW Mod Manager v{latestStringVersion}.exe";

            // Start new downloaded exectuable
            Process.Start(newExecutablePath);
            // Delete the old executable after 3 seconds have passed using cmd!
            Process.Start("cmd.exe", "/C choice /C Y /N /D Y /T 1 & Del \"" + oldExecutablePath + "\"");
            CreateShortcut($"DoW Mod Manager v{latestStringVersion}", newExecutablePath);
            Program.TerminateApp();
        }

        /// <summary>
        /// This method creates a new shortcut of a newly created Mod Manager file!
        /// </summary>
        /// <param name="shortcutName"></param>
        // Request the inlining of this method
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void CreateShortcut(string shortcutName, string targetPath)
        {
            string shortcutLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), shortcutName + ".lnk");
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

            shortcut.Description = $"The latest DoW Mod Manager v{latestStringVersion}";
            shortcut.TargetPath = targetPath;
            shortcut.WorkingDirectory = currentDir;
            shortcut.Save();
        }
    }
}
