using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;
using System.Security.Permissions;
using System.Reflection;
using System.Threading;

namespace DoW_Mod_Manager
{
    public partial class ModManagerForm : Form
    {
        public static class GameExecutable
        {
            public const string ORIGINAL = "W40k.exe";
            public const string WINTER_ASSAULT = "W40kWA.exe";
            public const string DARK_CRUSADE = "DarkCrusade.exe";
            public const string SOULSTORM = "Soulstorm.exe";
        }

        private const int IMAGE_FILE_LARGE_ADDRESS_AWARE = 0x20;

        private readonly string currentDir = Directory.GetCurrentDirectory();

        private bool[] isInstalled;                                                 // A boolean array that maps Index-wise to the filepaths indices. Index 0 checks if required mod at index 0 in the FilePaths is installed or not.
        private bool isGameEXELAAPatched = false;
        private bool isGraphicsConfigLAAPatched = false;
        private bool isMessageBoxOnScreen = false;

        public readonly string CurrentGameEXE = "";
        public readonly string GraphicsConfigEXE = "GraphicsConfig.exe";
        public string[] ModuleFilePaths;
        public string[] ModFolderPaths;
        public List<string> AllFoundModules;                                        // Contains the list of all available Mods that will be used by the Mod Merger
        public List<string> AllValidModules;                                        // Contains the list of all playable Mods that will be used by the Mod Merger

        private const string CONFIG_FILE_NAME = "DoW Mod Manager.ini";

        private const string CHOICE_INDEX = "ChoiceIndex";
        public const string DEV = "Dev";
        public const string NO_MOVIES = "NoMovies";
        public const string FORCE_HIGH_POLY = "ForceHighPoly";
        public const string OPTIMIZATIONS = "Optimizations";

        // Don't make Settings readonly or it couldn't be changed from outside the class!
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0044:Add readonly modifier", Justification = "<Pending>")]
        private Dictionary<string, int> settings = new Dictionary<string, int>
        {
            [CHOICE_INDEX]    = 0,
            [DEV]             = 0,
            [NO_MOVIES]       = 1,
            [FORCE_HIGH_POLY] = 0,
            [OPTIMIZATIONS]   = 0
        };

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public ModManagerForm()
        {
            InitializeComponent();

            // Sets Title of the form to be the same as Assembly Name
            Text = Assembly.GetExecutingAssembly().GetName().Name;

            // Use the same icon as executable
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            // Read *.ini file and load settings in memory
            if (File.Exists(CONFIG_FILE_NAME))
            {
                // Read every line of config file and try to ignore or correct all the possible mistakes
                string[] lines = File.ReadAllLines(CONFIG_FILE_NAME);
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    line = line.Replace(" ", "");

                    int firstIndexOfEqualSign = line.IndexOf('=');
                    int lastIndexOfEqualSign = line.LastIndexOf('=');

                    // There must be only one "=" in the line!
                    if (firstIndexOfEqualSign == lastIndexOfEqualSign)
                    {
                        if (firstIndexOfEqualSign > 0)
                        {
                            string setting = Convert.ToString(line.Substring(0, firstIndexOfEqualSign));
                            int value;

                            try
                            {
                                value = Convert.ToInt32(line.Substring(firstIndexOfEqualSign + 1, line.Length - firstIndexOfEqualSign - 1));
                            }
                            catch (Exception)
                            {
                                value = 0;
                            }

                            if (setting == CHOICE_INDEX)
                            {
                                if (value >= 0)
                                    settings[setting] = value;
                                else
                                    settings[setting] = 0;
                            }

                            if (setting == DEV || setting == NO_MOVIES || setting == FORCE_HIGH_POLY || setting == OPTIMIZATIONS)
                            {
                                if (value == 0 || value == 1)
                                    settings[setting] = value;
                                else
                                {
                                    if (value > 1)
                                        settings[setting] = 1;
                                    else
                                        settings[setting] = 0;
                                }
                            }
                        }
                    }
                }
            }

            ReselectSavedMod();

            // Initialize values from saved values or defaults.
            if (Convert.ToBoolean(settings[DEV]))
                devCheckBox.Checked = true;
            else
                devCheckBox.Checked = false;

            if (Convert.ToBoolean(settings[NO_MOVIES]))
                nomoviesCheckBox.Checked = true;
            else
                nomoviesCheckBox.Checked = false;

            if (Convert.ToBoolean(settings[FORCE_HIGH_POLY]))
                highpolyCheckBox.Checked = true;
            else
                highpolyCheckBox.Checked = false;

            if (Convert.ToBoolean(settings[OPTIMIZATIONS]))
                optimizationsCheckBox.Checked = true;
            else
                optimizationsCheckBox.Checked = false;

            CurrentGameEXE = GetCurrentGameEXE();
            CheckForGraphicsConfigEXE();

            currentDirTextBox.Text = currentDir;
            SetUpAllNecessaryMods();
            isGameEXELAAPatched = IsLargeAware(Directory.GetFiles(currentDir, CurrentGameEXE)[0]);
            SetGameLAALabelText();
            isGraphicsConfigLAAPatched = IsLargeAware(Directory.GetFiles(currentDir, GraphicsConfigEXE)[0]);
            SetGraphicsConfigLAALabelText();

            // Watch for any changes in game directory
            AddFileSystemWatcher();

            // Sets the focus to the mod list
            installedModsListBox.Select();
        }

        private void ModManagerForm_Closing(object sender, EventArgs e)
        {
            string str = $"{CHOICE_INDEX}={settings[CHOICE_INDEX]}\n" +
                         $"{DEV}={settings[DEV]}\n" +
                         $"{NO_MOVIES}={settings[NO_MOVIES]}\n" +
                         $"{FORCE_HIGH_POLY}={settings[FORCE_HIGH_POLY]}\n" +
                         $"{OPTIMIZATIONS}={settings[OPTIMIZATIONS]}";
            File.WriteAllText(CONFIG_FILE_NAME, str);
        }

        private void ReselectSavedMod()
        {
            int index = settings[CHOICE_INDEX];

            if (installedModsListBox.Items.Count > index)
                installedModsListBox.SelectedIndex = index;
            else
                installedModsListBox.SelectedIndex = installedModsListBox.Items.Count - 1;
        }

        private void AddFileSystemWatcher()
        {
            fileSystemWatcher1.Path = currentDir;

            fileSystemWatcher1.NotifyFilter = NotifyFilters.LastAccess
                                    | NotifyFilters.LastWrite
                                    | NotifyFilters.FileName
                                    | NotifyFilters.DirectoryName;

            fileSystemWatcher1.Changed += FileSystemWatcherOnChanged;
            fileSystemWatcher1.Created += FileSystemWatcherOnChanged;
            fileSystemWatcher1.Deleted += FileSystemWatcherOnChanged;
            fileSystemWatcher1.Renamed += FileSystemWatcherOnChanged;

            // Begin watching.
            fileSystemWatcher1.EnableRaisingEvents = true;
        }

        private void FileSystemWatcherOnChanged(object source, FileSystemEventArgs e)
        {
            SetUpAllNecessaryMods();
        }

        /// <summary>
        /// A refactored wrapper method that is used to initialize or refresh the Mod Managers main form
        /// </summary>
        public void SetUpAllNecessaryMods()
        {
            GetMods();
            GetModFoldersFromFile();
            ReselectSavedMod();
        }

        /// <summary>
        /// Checks if the Mods are actually REALLY installed by checking if their asset folders are present by the name specified within the .module files "Modfolder" tagline
        /// </summary>
        public void CheckforInstalledMods()
        {
            startModButton.Enabled = true;

            Stream myStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DoW_Mod_Manager.DoW_Mod_Manager_Resources.Checkmark.png");
            pictureBox.Image = Image.FromStream(myStream);

            string localstring;
            string folderPath;
            int itemsCount = requiredModsList.Items.Count;
            isInstalled = new bool[itemsCount];

            for (int i = 0; i < itemsCount; i++)
            {
                folderPath = currentDir + "\\" + ModFolderPaths[i];

                if (Directory.Exists(folderPath))
                {
                    localstring = requiredModsList.Items[i].ToString();
                    requiredModsList.Items.RemoveAt(i);
                    requiredModsList.Items.Insert(i, localstring + "...INSTALLED!");
                    isInstalled[i] = true;
                }
                else
                {
                    localstring = requiredModsList.Items[i].ToString();
                    requiredModsList.Items.RemoveAt(i);
                    requiredModsList.Items.Insert(i, localstring + "...MISSING!");
                    isInstalled[i] = false;
                    startModButton.Enabled = false;

                    myStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DoW_Mod_Manager.DoW_Mod_Manager_Resources.cross.png");
                    pictureBox.Image = Image.FromStream(myStream);
                }
            }
            myStream.Close();
        }

        private string GetLastEntryFromLine(string text)
        {
            const string pattern = @"\S*\s*$";

            // Instantiate the regular expression object.
            Regex require = new Regex(pattern, RegexOptions.IgnoreCase);

            // Match the regular expression pattern against a text string.
            Match match = require.Match(text);

            if (match.Success)
                return match.Value.Replace(" ", "");
            else
                return "";
        }

        /// <summary>
        /// Returns the name of the Module file without it's extension and all it's whitespaces removed.
        /// </summary>
        private string GetModFolderFromFile(string text)
        {
            const string pattern = @"\S*\s*$";

            // Instantiate the regular expression object.
            Regex require = new Regex(pattern, RegexOptions.IgnoreCase);

            // Match the regular expression pattern against a text string.
            Match match = require.Match(text);

            if (match.Success)
                return match.Value.Replace(" ", "").Replace(".module", "");
            else
                return "";
        }

        /// <summary>
        /// Finds all installed .module files and displays them in the Installed Mods Listbox without their .module extension
        /// </summary>
        private void GetMods()
        {
            List<string> newfilePathsList = new List<string>();        // Make a new list for the new Pathitems
            AllFoundModules = new List<string>();
            AllValidModules = new List<string>();

            installedModsListBox.Items.Clear();

            ModuleFilePaths = Directory.GetFiles(currentDir, "*.module");
            if (ModuleFilePaths.Length > 0)
            {
                for (int i = 0; i < ModuleFilePaths.Length; i++)
                {
                    string filePath = ModuleFilePaths[i];

                    // There is no point of adding base module to the list
                    if (filePath.Contains("W40k.module"))
                        continue;

                    // Find the List of ALL found module files for the Mod Merger available Mods List
                    AllFoundModules.Add(Path.GetFileNameWithoutExtension(filePath));

                    // Read the *.module file to see if the mod is playable
                    using (StreamReader file = new StreamReader(filePath))
                    {
                        string line;
                        
                        // Filter the unplayable mods and populate the List only with playable mods
                        while ((line = file.ReadLine()) != null)
                        {
                            if (ModIsPlayable(line))
                            {
                                newfilePathsList.Add(ModuleFilePaths[i]);
                                installedModsListBox.Items.Add(Path.GetFileNameWithoutExtension(filePath));
                                AllValidModules.Add(Path.GetFileNameWithoutExtension(filePath));
                            }

                            // We will not find unplayable mods in Original or Winter Assault - there was no "Playable" state
                            if (CurrentGameEXE == GameExecutable.WINTER_ASSAULT || CurrentGameEXE == GameExecutable.ORIGINAL)
                                break;
                        }
                    }
                }
                ModuleFilePaths = newfilePathsList.ToArray();        //Override the old array that contained unplayable mods with the new one.
            }
            else
            {
                if (!isMessageBoxOnScreen)
                {
                    MessageBox.Show("No mods were found in the specified directory! Please check your current directory again!");
                    isMessageBoxOnScreen = true;
                    Application.Exit();
                }
            }
        }

        private bool ModIsPlayable(string textline)
        {
            // Original or Winter Assault module file don't have a "Playable" state
            if (CurrentGameEXE == GameExecutable.WINTER_ASSAULT || CurrentGameEXE == GameExecutable.ORIGINAL)
                return true;

            const string pattern = @"Playable = 1";

            Regex require = new Regex(pattern, RegexOptions.IgnoreCase);
            Match match = require.Match(textline);

            if (match.Success)
                return true;
            else
                return false;
        }

        private bool GetRequiredMod(string text)
        {
            const string pattern = @"\bRequiredMod\b";
            const string patternCommented1 = @"^[;]+";
            const string patternCommented2 = @"^[\/]+";
            bool state = false;

            foreach (Match match in Regex.Matches(text, pattern))
            {
                state = true;
            }
            foreach (Match match in Regex.Matches(text, patternCommented1))
            {
                state = false;
            }
            foreach (Match match in Regex.Matches(text, patternCommented2))
            {
                state = false;
            }
            return state;
        }

        private bool IsModFolderExist(string text)
        {
            const string pattern = @"ModFolder";
            bool state = false;

            foreach (Match match in Regex.Matches(text, pattern))
            {
                state = true;
            }
            return state;
        }

        private void InstalledModsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            startModButton.Enabled = true;

            int index = installedModsListBox.SelectedIndex;
            if (index < 0 || index >= installedModsListBox.Items.Count)
            {
                index = settings[CHOICE_INDEX];
                installedModsListBox.SelectedIndex = index;
            }
            else
                settings[CHOICE_INDEX] = index;

            string currentPath = ModuleFilePaths[index];
            string line;

            requiredModsList.Items.Clear();

            // Read the file and display it line by line.
            using (StreamReader file = new StreamReader(currentPath))
            {
                // Populate the Required Mods List with entries from the .module file
                while ((line = file.ReadLine()) != null)
                {
                    if (GetRequiredMod(line))
                        requiredModsList.Items.Add(line);
                }

                GetModFoldersFromFile();
                CheckforInstalledMods();
            }
        }

        /// <summary>
        ///  This one checks if the Mod Assett folders, specified in each .module file, do actually exist
        /// </summary>
        private void GetModFoldersFromFile()
        {
            int count = 0;
            int itemsCount = requiredModsList.Items.Count;
            ModFolderPaths = new string[itemsCount];

            // Read the file and display it line by line.
            for (int i = 0; i < itemsCount; i++)
            {
                string currentPath = currentDir + "\\" + GetLastEntryFromLine(requiredModsList.Items[i].ToString()) + ".module";
                
                if (File.Exists(currentPath))
                {
                    using (StreamReader file = new StreamReader(currentPath))
                    {
                        string line;

                        while ((line = file.ReadLine()) != null)
                        {
                            if (IsModFolderExist(line))
                            {
                                ModFolderPaths[count] = GetModFolderFromFile(line);
                                count++;
                            }
                        }
                    }
                }
                else
                {
                    ModFolderPaths[count] = "MISSING";
                    count++;
                }
            }
        }

        private void StartVanillaGameButton_Click(object sender, EventArgs e)
        {
            StartGameWithOptions("W40k");
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            StartGameWithOptions(installedModsListBox.SelectedItem.ToString());
        }

        private void StartGameWithOptions(string modName)
        {
            string arguments = "-modname " + modName;

            // Add additional arguments if needed
            if (settings[DEV] == 1)
                arguments += " -dev";
            if (settings[NO_MOVIES] == 1)
                arguments += " -nomovies";
            if (settings[FORCE_HIGH_POLY] == 1)
                arguments += " -forcehighpoly";

            Process proc = new Process();
            proc.StartInfo.FileName = CurrentGameEXE;
            proc.StartInfo.Arguments = arguments;
            proc.Start();

            string procName = proc.ProcessName;

            if (settings[OPTIMIZATIONS] == 1)
            {
                // Threads could work even if application would be closed
                new Thread(() =>
                {
                    int triesCount = 0;
                    TRY_AGAIN:
                    triesCount++;

                    // We can't change priority or affinity of the game right after it starts
                    Thread.Sleep(1000);
                    try
                    {
                        Process[] dow = Process.GetProcessesByName(procName);
                        dow[0].PriorityClass = ProcessPriorityClass.High;
                        dow[0].ProcessorAffinity = (IntPtr)0x0006;          // Affinity 6 means using only CPU threads 2 and 3 (6 = 0110)
                    }
                    catch (Exception)
                    {
                        // We will try 60 times and then Thread will be terminated regardless
                        if (triesCount < 61)
                            goto TRY_AGAIN;
                    }
                })
                .Start();
            }
        }

        /// <summary>
        /// This is the checkbox that controls the starting option '-dev'. 
        /// It allows for additional debug options in-game and log files.
        /// </summary>
        private void DevCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (devCheckBox.Checked)
                settings[DEV] = 1;
            else
                settings[DEV] = 0;
        }

        /// <summary>
        /// This is the checkbox that controls the starting option '-nomovies'. 
        /// It prevents any movies/intros from being played.
        /// </summary>
        private void NomoviesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (nomoviesCheckBox.Checked)
                settings[NO_MOVIES] = 1;
            else
                settings[NO_MOVIES] = 0;
        }

        /// <summary>
        /// This is the checkbox that controls the starting option '-forcehighpoly'. 
        /// This disabled the LOD system and will display the highes mesh detail at any distance.
        /// </summary>
        private void HighpolyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (highpolyCheckBox.Checked)
                settings[FORCE_HIGH_POLY] = 1;
            else
                settings[FORCE_HIGH_POLY] = 0;
        }

        /// <summary>
        /// This is the checkbox that sets the starting options '/high /affinity 6'. 
        /// This sets Dawn of War executable to High priority and use CPU1 and CPU2 (6 = 0110 in binary)
        /// You need at least 3 cores to make a difference (DoW would use CPU1 and CPU2, 
        /// CPU0 would be for any other application)
        /// </summary>
        private void OptimizationsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (optimizationsCheckBox.Checked)
                settings[OPTIMIZATIONS] = 1;
            else
                settings[OPTIMIZATIONS] = 0;
        }

        private void RequiredModsList_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Draw the background of the ListBox control for each item.
            e.DrawBackground();

            Brush myBrush;

            if (isInstalled[e.Index])
                myBrush = Brushes.Green;
            else
                myBrush = Brushes.Red;

            // Draw the current item text based on the current 
            // Font and the custom brush settings.
            e.Graphics.DrawString(((ListBox)sender).Items[e.Index].ToString(), e.Font, myBrush, e.Bounds, StringFormat.GenericDefault);

            // If the ListBox has focus, draw a focus rectangle 
            // around the selected item.
            e.DrawFocusRectangle();
        }

        private void DownloadButton_Click(object sender, EventArgs e)
        {
            ModDownloaderForm downloaderForm = new ModDownloaderForm(this);
            downloaderForm.Show();
        }

        private void ModMergeButton_Click(object sender, EventArgs e)
        {
            ModMergerForm mergerWindow = new ModMergerForm(this);
            mergerWindow.Show();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            SettingsManagerForm settingsForm = new SettingsManagerForm(this);
            settingsForm.Show();
        }

        private void SetGameLAALabelText()
        {
            if (isGameEXELAAPatched)
            {
                gameLAAStatusLabel.Text = CurrentGameEXE + ": LAA Active";
                gameLAAStatusLabel.ForeColor = Color.Green;
            }
            else
            {
                gameLAAStatusLabel.Text = CurrentGameEXE + ": LAA Inactive";
                gameLAAStatusLabel.ForeColor = Color.Red;
            }
        }

        private void SetGraphicsConfigLAALabelText()
        {
            if (isGraphicsConfigLAAPatched)
            {
                graphicsConfigLAAStatusLabel.Text = GraphicsConfigEXE + ": LAA Active";
                graphicsConfigLAAStatusLabel.ForeColor = Color.Green;
            }
            else
            {
                graphicsConfigLAAStatusLabel.Text = GraphicsConfigEXE + ": LAA Inactive";
                graphicsConfigLAAStatusLabel.ForeColor = Color.Red;
            }
        }

        static bool IsLargeAware(string file)
        {
            using (FileStream fs = File.OpenRead(file))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    if (br.ReadInt16() != 0x5A4D)       // No MZ Header
                        return false;

                    br.BaseStream.Position = 0x3C;
                    int peloc = br.ReadInt32();         // Get the PE header location.

                    br.BaseStream.Position = peloc;
                    if (br.ReadInt32() != 0x4550)       // No PE header
                        return false;

                    br.BaseStream.Position += 0x12;     // LAA flag position
                    short LAAFlag = br.ReadInt16();

                    return (LAAFlag & IMAGE_FILE_LARGE_ADDRESS_AWARE) == IMAGE_FILE_LARGE_ADDRESS_AWARE;
                }
            }
        }

        static bool ToggleLAA(string file)
        {
            bool result = false;
            using (FileStream fs = File.Open(file, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
            {
                BinaryReader br = new BinaryReader(fs);

                if (br.ReadInt16() != 0x5A4D)       // No MZ Header
                    return false;

                br.BaseStream.Position = 0x3C;
                int peloc = br.ReadInt32();         // Get the PE header location.

                br.BaseStream.Position = peloc;
                if (br.ReadInt32() != 0x4550)       // No PE header
                    return false;

                br.BaseStream.Position += 0x12;     // LAA flag position
                long nFilePos = (int)br.BaseStream.Position;
                short LAAFlag = br.ReadInt16();

                br.BaseStream.Position += 0x40;     // Checksum position
                long nSumPos = (int)br.BaseStream.Position;
                short ChckSum = br.ReadInt16();

                BinaryWriter bw = new BinaryWriter(fs);

                if ((LAAFlag & IMAGE_FILE_LARGE_ADDRESS_AWARE) != IMAGE_FILE_LARGE_ADDRESS_AWARE)
                {
                    LAAFlag += IMAGE_FILE_LARGE_ADDRESS_AWARE;
                    ChckSum += IMAGE_FILE_LARGE_ADDRESS_AWARE;
                    bw.Seek((int)nFilePos, SeekOrigin.Begin);
                    bw.Write(LAAFlag);
                    bw.Seek((int)nSumPos, SeekOrigin.Begin);
                    bw.Write(ChckSum);
                    bw.Flush();
                    result = true;
                }
                else if ((LAAFlag & IMAGE_FILE_LARGE_ADDRESS_AWARE) == IMAGE_FILE_LARGE_ADDRESS_AWARE)
                {
                    LAAFlag -= IMAGE_FILE_LARGE_ADDRESS_AWARE;
                    ChckSum -= IMAGE_FILE_LARGE_ADDRESS_AWARE;
                    bw.Seek((int)nFilePos, SeekOrigin.Begin);
                    bw.Write(LAAFlag);
                    bw.Seek((int)nSumPos, SeekOrigin.Begin);
                    bw.Write(ChckSum);
                    bw.Flush();
                    result = false;
                }
                bw.Close();
                br.Close();
            }
            return result;
        }

        private bool IsFileLocked(string file)
        {
            FileStream stream = null;
            try
            {
                stream = File.Open(file, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                // The file is unavailable because it is:
                // still being written to
                // or being processed by another thread
                // or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            // File is not locked
            return false;
        }

        /// <summary>
        /// This method handles the proper toggling of the LAA flag for the Soulstorm.exe and the GraphicsConfig.exe.
        /// It can handle the cases when users have previously patched the EXE files only partially.
        /// </summary>
        private void ButtonToggleLAA_Click(object sender, EventArgs e)
        {
            // Check if the Game is LAA Patched and fill in the Label properly
            string currentGamePath = Directory.GetFiles(currentDir, CurrentGameEXE)[0];
            string currentGraphucsConfigPath = Directory.GetFiles(currentDir, GraphicsConfigEXE)[0];

            if (!IsFileLocked(currentGamePath) && !IsFileLocked(currentGraphucsConfigPath))
            {
                if ((isGameEXELAAPatched && isGraphicsConfigLAAPatched) || (!isGameEXELAAPatched && !isGraphicsConfigLAAPatched))
                {
                    isGameEXELAAPatched = ToggleLAA(currentGamePath);
                    isGraphicsConfigLAAPatched = ToggleLAA(currentGraphucsConfigPath);
                }
                else if (!isGameEXELAAPatched)
                {
                    isGameEXELAAPatched = ToggleLAA(currentGamePath);
                }
                else if (!isGraphicsConfigLAAPatched)
                {
                    isGraphicsConfigLAAPatched = ToggleLAA(currentGraphucsConfigPath);
                }

                SetGameLAALabelText();
                SetGraphicsConfigLAALabelText();
            }
        }

        private string GetCurrentGameEXE()
        {
            string[] curDir = Directory.GetFiles(currentDir, GameExecutable.SOULSTORM);
            if (curDir.Length != 0)
            {
                currentDirectoryLabel.Text = "     Your current Soulstorm directory";
                return GameExecutable.SOULSTORM;
            }

            curDir = Directory.GetFiles(currentDir, GameExecutable.DARK_CRUSADE);
            if (curDir.Length != 0)
            {
                currentDirectoryLabel.Text = "  Your current Dark Crusade directory";
                return GameExecutable.DARK_CRUSADE;
            }

            curDir = Directory.GetFiles(currentDir, GameExecutable.WINTER_ASSAULT);
            if (curDir.Length != 0)
            {
                currentDirectoryLabel.Text = "Your current Winter Assault directory";
                return GameExecutable.WINTER_ASSAULT;
            }

            // That part of the code will never be reached if you have Original + WA
            curDir = Directory.GetFiles(currentDir, GameExecutable.ORIGINAL);
            if (curDir.Length != 0)
            {
                currentDirectoryLabel.Text = "   Your current Dawn of War directory";
                return GameExecutable.ORIGINAL;
            }

            if (!isMessageBoxOnScreen)
            {
                MessageBox.Show("ERROR: Neither found the Soulstorm, Dark Crusade, Winter Assault nor Original in this directory!");
                isMessageBoxOnScreen = true;
                Application.Exit();
            }
            return "";
        }

        private void CheckForGraphicsConfigEXE()
        {
            string[] curDir = Directory.GetFiles(currentDir, GraphicsConfigEXE);
            if (curDir.Length == 0)
            {
                if (!isMessageBoxOnScreen)
                {
                    MessageBox.Show("ERROR: " + GraphicsConfigEXE + " was not found!");
                    isMessageBoxOnScreen = true;
                    Application.Exit();
                }
            }
        }

        /// <summary>
        /// This method can be used ouside this class to change a setting and update the GUI
        /// </summary>
        public void ChangeSetting(string setting, int newValue)
        {
            // Makes sure that newValue is in range of acceptable values
            if (newValue < 0)
                newValue = 0;
            else if (newValue > 1)
                newValue = 1;

            switch (setting)
            {
                case DEV:
                    settings[DEV] = newValue;
                    devCheckBox.Checked = Convert.ToBoolean(newValue);
                    break;
                case NO_MOVIES:
                    settings[NO_MOVIES] = newValue;
                    nomoviesCheckBox.Checked = Convert.ToBoolean(newValue);
                    break;
                case FORCE_HIGH_POLY:
                    settings[FORCE_HIGH_POLY] = newValue;
                    highpolyCheckBox.Checked = Convert.ToBoolean(newValue);
                    break;
                case OPTIMIZATIONS:
                    settings[OPTIMIZATIONS] = newValue;
                    optimizationsCheckBox.Checked = Convert.ToBoolean(newValue);
                    break;
            }
        }
    }
}