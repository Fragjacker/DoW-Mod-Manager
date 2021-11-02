using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace DoW_Mod_Manager
{
    public partial class ModDownloaderForm : Form
    {
        public class Mod
        {
            public string ModName;
            public string ModuleFileName;
            public string SiteLink;
            public string DownloadLink;
            public string PatchLink;

            public Mod(string modName, string moduleFileName, string siteLink, string modDownloadLink, string patchDwonloadLink)
            {
                ModName = modName;
                ModuleFileName = moduleFileName;
                SiteLink = siteLink;
                DownloadLink = modDownloadLink;
                PatchLink = patchDwonloadLink;
            }
        }

        public const string MODLIST_FILE = "DoW Mod Manager Download Mods.list";
        private const string SEARCH_TEXT = "Search...";
        private readonly List<Mod> modlist;

        private bool findByModuleName = true;

        private readonly ModManagerForm modManager;

        /// <summary>
        /// Creates the Form of the Mod Downloader Window
        /// </summary>
        /// <param name="form"></param>
        /// <param name="moduleFileName"></param>
        public ModDownloaderForm(ModManagerForm form, string moduleFileName = "")
        {
            InitializeComponent();

            modManager = form;

            // Use the same icon as executable
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            // Add mods to the modlist based on what version of Dawn of War was detected
            // DO NOT extract this as a method - it will be x2 times slower! Even with "AggressiveInlining"
            modlist = new List<Mod>();

            if (modManager.CurrentGameEXE == ModManagerForm.GameExecutable.SOULSTORM)
            {
                popularModsLabel.Text += "Soulstorm:";

                ReadModsFromFile("[SS]");
            }
            else if (modManager.CurrentGameEXE == ModManagerForm.GameExecutable.DARK_CRUSADE)
            {
                popularModsLabel.Text += "Dark Crusade:";

                ReadModsFromFile("[DC]");
            }
            else if (modManager.CurrentGameEXE == ModManagerForm.GameExecutable.WINTER_ASSAULT)
            {
                popularModsLabel.Text += "Winter Assault:";

                ReadModsFromFile("[WA]");
            }
            else if (modManager.CurrentGameEXE == ModManagerForm.GameExecutable.ORIGINAL)
            {
                popularModsLabel.Text += "Original:";

                modlist = new List<Mod>()
                {
                    new Mod("First I have to find a few mods for Original :-)",
                            "",
                            "",
                            "",
                            "")
                };

                openModPageButton.Enabled = false;
                downloadModDefaultButton.Enabled = false;
            }

            // If we want to search by the *.module file name - we don't have to populate modListBox.Items
            if (moduleFileName.Length > 0)
            {
                searchTextBox.Text = moduleFileName;
                return;
            }

            for (int i = 0; i < modlist.Count; i++)
            {
                modListBox.Items.Add(modlist[i].ModName);
            }

            findByModuleName = false;
            modListBox.Select();

            // AddRange(new Mod()) 3.5 ms
            // Add(new Mod())      2.87 ms
            // { new Mod() }       2.7 ms
            // AddRange() is the slowest? NANI? BAKANA! :-)
        }

        /// <summary>
        /// This method reds mods from a modlist file
        /// </summary>
        /// <param name="parameter"></param>
        // Request the inlining of this method
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ReadModsFromFile(string parameter)
        {
            if (File.Exists(MODLIST_FILE))
                DownloadHelper.CheckForNewModlist(silently: true);
            else
                DownloadHelper.DownloadModlist();

            // Wait 2 seconds for Modlist to download
            int counter = 0;
            while (!IsFileReady(MODLIST_FILE) && counter < 200)
            {
                Thread.Sleep(10);
                counter++;
            }

            using (StreamReader file = new StreamReader(MODLIST_FILE))
            {
                string line;

                while ((line = file.ReadLine()) != null)
                {
                    if (line.StartsWith(parameter))
                    {
                        // Skipping an empty line
                        file.ReadLine();

                        // Read lines untill we will found mods for the different version
                        while (!(Convert.ToChar(file.Peek()) == '['))
                        {
                            // Reading 5 lines and creating a Mod instance
                            string[] modProperties = new string[5];

                            for (int i = 0; i < 5; i++)
                            {
                                if ((line = file.ReadLine()) != null)
                                {
                                    if (line.StartsWith("-"))
                                        line = "";

                                    modProperties[i] = line;
                                }
                                else
                                    modProperties[i] = "";      // There is no line to read - we have to add something to a Mod instance!
                            }

                            modlist.Add(new Mod(modProperties[0], modProperties[1], modProperties[2], modProperties[3], modProperties[4]));

                            // Skipping an empty line
                            file.ReadLine();
                        }

                        // We found all mods for current DoW version
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// This method reads mods from a modlist file
        /// </summary>
        /// <param name="filename"></param>
        private static bool IsFileReady(string filename)
        {
            // If the file can be opened for exclusive access it means that the file
            // is no longer locked by another process.
            try
            {
                using (FileStream fs = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    return fs.Length > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// This method opens the ModDB.com web page
        /// </summary>
        private void OpenModDBButton_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.moddb.com/games/dawn-of-war/mods?sort=rating-desc");
        }

        /// <summary>
        /// This method opens selected mod web page
        /// </summary>
        private void OpenModPageButton_Click(object sender, EventArgs e)
        {
            if (modListBox.SelectedItem == null)
                return;

            string siteAddress = "";

            for (int i = 0; i < modlist.Count; i++)
            {
                if (modListBox.SelectedItem.ToString() == modlist[i].ModName)
                    siteAddress = modlist[i].SiteLink;
            }

            if (siteAddress.Length > 0)
                Process.Start(siteAddress);
        }

        /// <summary>
        /// This method downloads selected mod using Internet Explorer's 11 engine
        /// </summary>
        private void downloadModIEButton_Click(object sender, EventArgs e)
        {
            DownloadMod(false);
        }

        /// <summary>
        /// This method downloads selected mod using external browser
        /// </summary>
        private void DownloadModDefaultButton_Click(object sender, EventArgs e)
        {
            DownloadMod(true);
        }

        // Request the inlining of this method
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void DownloadMod(bool isExternalBrowser)
        {
            if (modListBox.SelectedItem == null)
                return;

            string modAddress = "";
            string patchAddress = "";

            for (int i = 0; i < modlist.Count; i++)
            {
                if (modListBox.SelectedItem.ToString() == modlist[i].ModName)
                    modAddress = modlist[i].DownloadLink;

                if (modListBox.SelectedItem.ToString() == modlist[i].ModName)
                    patchAddress = modlist[i].PatchLink;
            }

            if (modAddress.Length > 0)
                if (isExternalBrowser)
                    Process.Start(modAddress);
                else
                    new MiniBrowser(modAddress).Show();

            if (patchAddress.Length > 0)
            {
                Thread.Sleep(250);                                               // Some small delay before trying to download second file

                if (isExternalBrowser)
                    Process.Start(patchAddress);
                else
                    new MiniBrowser(patchAddress).Show();
            }
        }

        /// <summary>
        /// This method reacts to changes in searchTextBox.Text
        /// </summary>
        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            modListBox.Items.Clear();

            if (searchTextBox.Text.Length > 0 && searchTextBox.Text != SEARCH_TEXT)
            {
                if (findByModuleName)
                {
                    for (int i = 0; i < modlist.Count; i++)
                    {
                        if (modlist[i].ModuleFileName.Contains(searchTextBox.Text))
                        {
                            modListBox.Items.Add(modlist[i].ModName);
                        }
                    }

                    findByModuleName = false;
                }
                else
                {
                    for (int i = 0; i < modlist.Count; i++)
                    {
                        string modLowerCase = modlist[i].ModName.ToLower();
                        string searchLowerCase = searchTextBox.Text.ToLower();

                        if (modLowerCase.Contains(searchLowerCase))
                        {
                            modListBox.Items.Add(modlist[i].ModName);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < modlist.Count; i++)
                {
                    modListBox.Items.Add(modlist[i].ModName);
                }
            }
        }

        /// <summary>
        /// This method reacts to changes in searchTextBox.Focused
        /// </summary>
        private void SearchTextBox_Enter(object sender, EventArgs e)
        {
            if (searchTextBox.Text == SEARCH_TEXT)
            {
                searchTextBox.Text = "";
                searchTextBox.ForeColor = Color.FromArgb(200, 200, 200);
            }
        }

        /// <summary>
        /// This method reacts to changes in searchTextBox.Focused
        /// </summary>
        private void SearchTextBox_Leave(object sender, EventArgs e)
        {
            if (searchTextBox.Text == "")
            {
                searchTextBox.Text = SEARCH_TEXT;
                searchTextBox.ForeColor = Color.Gray;
            }
        }
    }
}