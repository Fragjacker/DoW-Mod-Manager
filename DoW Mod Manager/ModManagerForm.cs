using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Security.Permissions;
using System.Reflection;
using System.Threading;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Runtime;
using SSNoFog;
using SSUNI_EXTTDLL;

namespace DoW_Mod_Manager
{
    /// <summary>
    /// This struct contains the module name and mod folder path for easy access.
    /// </summary>
    public struct ModuleEntry
    {
        private string ModuleName, ModuleFolder;
        public ModuleEntry(string moduleName, string moduleFolder)
        {
            ModuleName = moduleName;
            ModuleFolder = moduleFolder;
        }
        public string getName { get { return ModuleName; } }
        public string getPath { get { return ModuleFolder; } }
    };
    public partial class ModManagerForm : Form
    {
        public struct GameExecutable
        {
            public const string ORIGINAL = "W40k.exe";
            public const string WINTER_ASSAULT = "W40kWA.exe";
            public const string DARK_CRUSADE = "DarkCrusade.exe";
            public const string SOULSTORM = "Soulstorm.exe";
        }

        private const int IMAGE_FILE_LARGE_ADDRESS_AWARE = 0x20;

        private const string CONFIG_FILE_NAME = "DoW Mod Manager.ini";
        private const string JIT_PROFILE_FILE_NAME = "DoW Mod Manager.JITProfile";
        private const string WARNINGS_LOG = "warnings.log";

        // This is a State Machine which determmens what action must be performed
        public enum Action { None, CreateNativeImage, CreateNativeImageAndDeleteJITProfile, DeleteJITProfile, DeleteNativeImage, DeleteJITProfileAndNativeImage }

        public const string ACTION_STATE = "ActionState";
        private const string CHOICE_INDEX = "ChoiceIndex";
        public const string DEV = "Dev";
        public const string NO_MOVIES = "NoMovies";
        public const string FORCE_HIGH_POLY = "ForceHighPoly";
        public const string NO_FOG = "RemoveMapFog";
        public const string UNI_EXTDLL = "LoadUNI_EXTDLL";
        public const string DOW_OPTIMIZATIONS = "DowOptimizations";
        public const string AUTOUPDATE = "Autoupdate";
        public const string MULTITHREADED_JIT = "MultithreadedJIT";
        public const string AOT_COMPILATION = "AOTCompilation";

        // A boolean array that maps Index-wise to the filepaths indices. Index 0 checks if required mod at index 0 in the FilePaths is installed or not.
        private bool[] _isInstalled;
        private bool _isGameEXELAAPatched = false;
        private bool _isGraphicsConfigLAAPatched = false;
        private bool _isMessageBoxOnScreen = false;
        private bool _isOldGame;
        private bool _IsNoFogTooltipShown = false;
        private string _dowProcessName = "";
        private readonly ToolTip _disabledNoFogTooltip = new ToolTip();
        private readonly ToolTip _disabledLoadUNI_EXTDLLCheckBoxTooltip = new ToolTip();
		private Control _currentToolTipControl;
        private ModMergerForm modMerger = null;

        public static int _maxDefeatedRaces = 0x0A;

        public readonly string CurrentDir = Directory.GetCurrentDirectory();
        public readonly string CurrentGameEXE = "";
        public readonly string GraphicsConfigEXE = "GraphicsConfig.exe";
        public string[] ModuleFilePaths;
        public string[] ModFolderPaths;
        public List<string> AllFoundModules;                                        // Contains the list of all available Mods that will be used by the Mod Merger
        public List<ModuleEntry> AllValidModules;                                        // Contains the list of all playable Mods that will be used by the Mod Merger
        public bool IsTimerResolutionLowered = false;
        string currentModuleFilePath = "";                                          // Contains the name of the current selected Mod.

        // Don't make Settings readonly or it couldn't be changed from outside the class!
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0044:Add readonly modifier", Justification = "<Pending>")]
        private Dictionary<string, int> settings = new Dictionary<string, int>
        {
            [ACTION_STATE] = (int)Action.CreateNativeImage,
            [CHOICE_INDEX] = 0,
            [DEV] = 0,
            [NO_MOVIES] = 1,
            [FORCE_HIGH_POLY] = 0,
            [NO_FOG] = 0,
            [UNI_EXTDLL] = 0,
            [DOW_OPTIMIZATIONS] = 0,
            [AUTOUPDATE] = 1,
            [MULTITHREADED_JIT] = 0,
            [AOT_COMPILATION] = 1
        };

        /// <summary>
        /// Initializes all the necessary components used by the GUI
        /// </summary>
        // We need this PermissionSet because we are using FilesystemWatcher
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public ModManagerForm()
        {
            ReadSettingsFromDoWModManagerINI();

            if (settings[MULTITHREADED_JIT] == 1)
            {
                // Enable Multithreaded JIT compilation. It's not a smart idea to use it with AOT compilation
                // So: Singethreaded JIT compilation < Multithreaded JIT compilation < AOT compilation < Native code (we don't have this option)
                // Defines where to store JIT profiles
                ProfileOptimization.SetProfileRoot(CurrentDir);
                // Enables Multicore JIT with the specified profile
                ProfileOptimization.StartProfile(JIT_PROFILE_FILE_NAME);
            }

            switch (settings[ACTION_STATE])
            {
                case (int)Action.CreateNativeImage:
                    CreateNativeImage();
                    break;
                case (int)Action.CreateNativeImageAndDeleteJITProfile:
                    CreateNativeImage();
                    DeleteJITProfile();
                    settings[ACTION_STATE] = (int)Action.CreateNativeImage;
                    break;
                case (int)Action.DeleteJITProfile:
                    if (settings[MULTITHREADED_JIT] == 0)
                        DeleteJITProfile();
                    settings[ACTION_STATE] = (int)Action.None;
                    break;
                case (int)Action.DeleteNativeImage:
                    DeleteNativeImage();
                    settings[ACTION_STATE] = (int)Action.None;
                    break;
                case (int)Action.DeleteJITProfileAndNativeImage:
                    DeleteJITProfile();
                    DeleteNativeImage();
                    settings[ACTION_STATE] = (int)Action.None;
                    break;
            }

            InitializeComponent();

            // Sets Title of the form to be the same as Assembly Name
            Text = Assembly.GetExecutingAssembly().GetName().Name;

            // Use the same icon as executable
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            // Initialize checkboxes with settings
            devCheckBox.Checked = settings[DEV] == 1;
            nomoviesCheckBox.Checked = settings[NO_MOVIES] == 1;
            highpolyCheckBox.Checked = settings[FORCE_HIGH_POLY] == 1;
            optimizationsCheckBox.Checked = settings[DOW_OPTIMIZATIONS] == 1;
            noFogCheckbox.Checked = settings[NO_FOG] == 1;
            loadUNI_EXTDLLCheckBox.Checked = settings[UNI_EXTDLL] == 1;

            CurrentGameEXE = GetCurrentGameEXE();
            CheckForGraphicsConfigEXE();

            currentDirTextBox.Text = CurrentDir;
            SetUpAllNecessaryMods();

            _isGameEXELAAPatched = IsLargeAware(Directory.GetFiles(CurrentDir, CurrentGameEXE)[0]);
            SetGameLAALabelText();
            _isGraphicsConfigLAAPatched = IsLargeAware(Directory.GetFiles(CurrentDir, GraphicsConfigEXE)[0]);
            SetGraphicsConfigLAALabelText();

            // Watch for any changes in game directory
            AddFileSystemWatcher();

            // Sets the focus to the mod list
            installedModsListBox.Select();

            // We have to add those methods to the EventHandler here so we could avoid accidental firing of those methods after we would change the state of the CheckBox
            requiredModsList.DrawItem += new DrawItemEventHandler(RequiredModsList_DrawItem);

            // Checkbox event handlers.
            devCheckBox.CheckedChanged += new EventHandler(DevCheckBox_CheckedChanged);
            nomoviesCheckBox.CheckedChanged += new EventHandler(NomoviesCheckBox_CheckedChanged);
            highpolyCheckBox.CheckedChanged += new EventHandler(HighpolyCheckBox_CheckedChanged);
            optimizationsCheckBox.CheckedChanged += new EventHandler(OptimizationsCheckBox_CheckedChanged);
            noFogCheckbox.CheckedChanged += new EventHandler(NoFogCheckboxCheckedChanged);
            loadUNI_EXTDLLCheckBox.CheckedChanged += new EventHandler(loadUNI_EXTDLLCheckBox_CheckedChanged);

            // Disable no Fog checkbox and UNI_EXTDLL checkbox if it's not Soulstorm because it only works on Soulstorm at all.
            if (CurrentGameEXE != GameExecutable.SOULSTORM)
            {
                noFogCheckbox.Enabled = false;
                noFogCheckbox.Checked = false;
                loadUNI_EXTDLLCheckBox.Enabled = false;
                loadUNI_EXTDLLCheckBox.Checked = false;
				
                flowLayoutPanel1.MouseMove += new MouseEventHandler(noFogCheckbox_hover);
                flowLayoutPanel1.MouseMove += new MouseEventHandler(loadUNI_EXTDLLCheckBox_hover);
            }

            // Perform Autoupdate
            if (settings[AUTOUPDATE] == 1)
            {
                // Threads could work even if application would be closed
                new Thread(() =>
                {
                    // Once all is done check for updates.
                    DialogResult result = DownloadHelper.CheckForUpdates(silently: true);

                    if (result == DialogResult.OK && settings[AOT_COMPILATION] == 1)
                        settings[ACTION_STATE] = (int)Action.CreateNativeImage;
                }
                ).Start();
            }
        }

        /// <summary>
        /// This function shows a tooltip, should the disable fog checkbox be disabled due to a wrong game version used.
        /// It has to be done since using normal tooltips won't work on disabled controls.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void noFogCheckbox_hover(object sender, MouseEventArgs e)
        {
            Control parent = sender as Control;

            if (parent == null)
                return;

            Control ctrl = parent.GetChildAtPoint(e.Location);
            if (ctrl != null)
            {
                if (ctrl == loadUNI_EXTDLLCheckBox && !_IsNoFogTooltipShown)
                {
                    _disabledNoFogTooltip.Show(
                        "Disable Fog only works in Dawn of War: Soulstorm",
                        loadUNI_EXTDLLCheckBox,
                        loadUNI_EXTDLLCheckBox.Width / 2, 
                        loadUNI_EXTDLLCheckBox.Height / 2);
                    _IsNoFogTooltipShown = true;
                }
            }
            else
            {
                _disabledNoFogTooltip.Hide(loadUNI_EXTDLLCheckBox);
                _IsNoFogTooltipShown = false;
            }
        }

        /// <summary>
        /// This function shows a tooltip, should the load Uni_ext.dll checkbox be disabled due to a wrong game version used.
        /// It has to be done since using normal tooltips won't work on disabled controls.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadUNI_EXTDLLCheckBox_hover(object sender, MouseEventArgs e)
        {
            var parent = sender as Control;
            if (parent == null)
            {
                return;
            }
            var ctrl = parent.GetChildAtPoint(e.Location);
            if (ctrl != null)
            {
                if (ctrl == noFogCheckbox && !_IsNoFogTooltipShown)
                {
                    _disabledNoFogTooltip.Show(
                        "Load UNI_EXT.DLL only works in Dawn of War: Soulstorm",
                        noFogCheckbox,
                        noFogCheckbox.Width / 2,
                        noFogCheckbox.Height / 2);
                    _IsNoFogTooltipShown = true;
                }
            }
            else
            {
                _disabledNoFogTooltip.Hide(noFogCheckbox);
                _IsNoFogTooltipShown = false;
            }
        }

        /// <summary>
        /// This method creates Native Image using ngen tool
        /// </summary>
        private static void CreateNativeImage()
        {
            // To enable AOT compilation we have to register DoW Mod Manager for NativeImage generation using ngen.exe
            string ModManagerName = AppDomain.CurrentDomain.FriendlyName;

            Process.Start(Environment.GetFolderPath(
                Environment.SpecialFolder.Windows) + @"\Microsoft.NET\Framework\v4.0.30319\ngen.exe",
                $"install \"{ModManagerName}\"");
        }

        /// <summary>
        /// This method deletes Native Image using ngen tool
        /// </summary>
        private static void DeleteNativeImage()
        {
            // To disable AOT compilation we have to unregister DoW Mod Manager for NativeImage generation using ngen.exe
            string ModManagerName = AppDomain.CurrentDomain.FriendlyName;

            Process.Start(Environment.GetFolderPath(
                Environment.SpecialFolder.Windows) + @"\Microsoft.NET\Framework\v4.0.30319\ngen.exe",
                $"uninstall \"{ModManagerName}\"");
        }

        /// <summary>
        /// This method deletes JIT profile cache file
        /// </summary>
        private void DeleteJITProfile()
        {
            string JITProfilePath = CurrentDir + "\\" + JIT_PROFILE_FILE_NAME;

            if (File.Exists(JITProfilePath))
                File.Delete(JITProfilePath);
        }

        /// <summary>
        /// This method reads DoW Mod Manager.ini file and loads settings in memory
        /// </summary>
        // Request the inlining of this method
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ReadSettingsFromDoWModManagerINI()
        {
            if (File.Exists(CONFIG_FILE_NAME))
            {
                // Read every line of config file and try to ignore or correct all common mistakes
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

                            switch (setting)
                            {
                                case ACTION_STATE:
                                    if (value < 6)
                                        // if value <= 3 (we have only 3 states) - do the same as in CHOICE_INDEX case
                                        goto case CHOICE_INDEX;
                                    break;
                                case CHOICE_INDEX:
                                    if (value >= 0)
                                        settings[setting] = value;
                                    else
                                        settings[setting] = 0;
                                    break;
                                case DEV:
                                case NO_MOVIES:
                                case FORCE_HIGH_POLY:
                                case DOW_OPTIMIZATIONS:
                                case AUTOUPDATE:
                                case MULTITHREADED_JIT:
                                case AOT_COMPILATION:
                                case NO_FOG:
                                case UNI_EXTDLL:
                                    if (value == 0 || value == 1)
                                        settings[setting] = value;
                                    else
                                    {
                                        if (value > 1)
                                            settings[setting] = 1;
                                        else
                                            settings[setting] = 0;
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This method handles the reselection of a previously selected mod.
        /// </summary>
        // Request the inlining of this method
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ReselectSavedMod()
        {
            int index = getSelectedModIndex();

            if (installedModsListBox.Items.Count > index)
                installedModsListBox.SelectedIndex = index;
            else
                installedModsListBox.SelectedIndex = installedModsListBox.Items.Count - 1;
        }

        /// <summary>
        /// This method scans for either the Soulstorm, Dark Crusade, Winter Assault or Original version of the game.
        /// </summary>
        /// <returns>string</returns>
        // Request the inlining of this method
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private string GetCurrentGameEXE()
        {
            if (File.Exists(CurrentDir + "\\" + GameExecutable.SOULSTORM))
            {
                currentDirectoryLabel.Text = "Your current Soulstorm directory:";
                _isOldGame = false;
                return GameExecutable.SOULSTORM;
            }

            if (File.Exists(CurrentDir + "\\" + GameExecutable.DARK_CRUSADE))
            {
                currentDirectoryLabel.Text = "Your current Dark Crusade directory:";
                _isOldGame = false;
                return GameExecutable.DARK_CRUSADE;
            }

            if (File.Exists(CurrentDir + "\\" + GameExecutable.WINTER_ASSAULT))
            {
                currentDirectoryLabel.Text = "Your current Winter Assault directory:";
                _isOldGame = true;
                return GameExecutable.WINTER_ASSAULT;
            }

            // That part of the code will never be reached if you have Original + WA
            if (File.Exists(CurrentDir + "\\" + GameExecutable.ORIGINAL))
            {
                currentDirectoryLabel.Text = "   Your current Dawn of War directory";
                _isOldGame = true;
                return GameExecutable.ORIGINAL;
            }

            if (!_isMessageBoxOnScreen)
            {
                ThemedMessageBox.Show("Neither found the Soulstorm, Dark Crusade, Winter Assault nor Original in this directory!", "ERROR:");
                _isMessageBoxOnScreen = true;
                Program.TerminateApp();
            }

            return "";
        }

        /// <summary>
        /// This method scans for GraphicsConfig.exe
        /// </summary>
        // Request the inlining of this method
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void CheckForGraphicsConfigEXE()
        {
            if (!File.Exists(CurrentDir + "\\" + GraphicsConfigEXE))
            {
                if (!_isMessageBoxOnScreen)
                {
                    ThemedMessageBox.Show(GraphicsConfigEXE + " was not found!", "ERROR:");
                    _isMessageBoxOnScreen = true;
                    Program.TerminateApp();
                }
            }
        }

        /// <summary>
        /// A refactored wrapper method that is used to initialize or refresh the Mod Managers main form
        /// </summary>
        public void SetUpAllNecessaryMods()
        {
            GetMods();
            LoadModFoldersFromFile();
            ReselectSavedMod();
        }

        /// <summary>
        /// This method instigates the test if a given EXE is LAA patched or not.
        /// </summary>
        /// <param name="file"></param>
        /// <returns>bool</returns>
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

        /// <summary>
        /// This method draws the LAA text for the game label depending on whether the flag is true (Green) or false (Red).
        /// </summary>
        private void SetGameLAALabelText()
        {
            if (_isGameEXELAAPatched)
            {
                gameLAAStatusLabel.Text = CurrentGameEXE + ": LAA Active";
                gameLAAStatusLabel.ForeColor = Color.LimeGreen;
            }
            else
            {
                gameLAAStatusLabel.Text = CurrentGameEXE + ": LAA Inactive";
                gameLAAStatusLabel.ForeColor = Color.Red;
            }
        }

        /// <summary>
        /// This method draws the LAA text for the GraphicsConfig label depending on whether the flag is true (Green) or false (Red).
        /// </summary>
        private void SetGraphicsConfigLAALabelText()
        {
            if (_isGraphicsConfigLAAPatched)
            {
                graphicsConfigLAAStatusLabel.Text = GraphicsConfigEXE + ": LAA Active";
                graphicsConfigLAAStatusLabel.ForeColor = Color.LimeGreen;
            }
            else
            {
                graphicsConfigLAAStatusLabel.Text = GraphicsConfigEXE + ": LAA Inactive";
                graphicsConfigLAAStatusLabel.ForeColor = Color.Red;
            }
        }

        /// <summary>
        /// This method adds FileSystem watcher to capture any file changes in the game directories.
        /// </summary>
        // Request the inlining of this method
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AddFileSystemWatcher()
        {
            fileSystemWatcher1.Path = CurrentDir;

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

        /// <summary>
        /// This method finds all installed *.module files and displays them in the Installed Mods Listbox without extension
        /// </summary>
        // Request the inlining of this method
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void GetMods()
        {
            // Make a new list for the new Pathitems
            List<string> newfilePathsList = new List<string>();
            AllFoundModules = new List<string>();
            AllValidModules = new List<ModuleEntry>();

            installedModsListBox.Items.Clear();

            ModuleFilePaths = Directory.GetFiles(CurrentDir, "*.module");
            if (ModuleFilePaths.Length > 0)
            {
                for (int i = 0; i < ModuleFilePaths.Length; i++)
                {
                    string filePath = ModuleFilePaths[i];
                    string fileName = Path.GetFileNameWithoutExtension(filePath);

                    // Check if the file actually exists
                    if (!File.Exists(filePath))
                        continue;

                    // There is no point of adding base modules to the list
                    if (filePath.Contains("W40k.module") || filePath.Contains("DXP2.module"))
                        continue;

                    // Find the List of ALL found module files for the Mod Merger available Mods List
                    AllFoundModules.Add(fileName);

                    // Read the *.module file to see the version and if the mod is playable
                    using (StreamReader file = new StreamReader(filePath))
                    {
                        string line;
                        bool isPlayable = false;
                        string modVersion = "";
                        string modFolderName = "";

                        // Filter the unplayable mods and populate the List only with playable mods
                        while ((line = file.ReadLine()) != null)
                        {
                            // Winter Assault of Original doesn't have a "Playable" state
                            if (line.Contains("Playable = 1") || _isOldGame)
                                isPlayable = true;

                            // Add information about the home mod folder of a mod
                            if (line.Contains("ModFolder"))
                            {
                                int indexOfEqualSigh = line.IndexOf('=');
                                modFolderName = line.Substring(indexOfEqualSigh + 1, line.Length - indexOfEqualSigh - 1);
                                modFolderName = modFolderName.Replace(" ", ""); //Remove whitespaces
                            }
                            // Add information about a version of a mod
                            if (line.Contains("ModVersion"))
                            {
                                int indexOfEqualSigh = line.IndexOf('=');
                                modVersion = line.Substring(indexOfEqualSigh + 1, line.Length - indexOfEqualSigh - 1);
                            }
                        }

                        if (isPlayable)
                        {
                            ModuleEntry module = new ModuleEntry(fileName, modFolderName);

                            newfilePathsList.Add(ModuleFilePaths[i]);
                            AllValidModules.Add(module);

                            // Append version number to filename string for display
                            if (modVersion.Length > 0)
                                fileName += $"   (Version{modVersion})";

                            installedModsListBox.Items.Add(fileName);
                        }
                    }
                }

                // Override the old array that contained unplayable mods with the new one.
                ModuleFilePaths = newfilePathsList.ToArray();
            }
            else
            {
                if (!_isMessageBoxOnScreen)
                {
                    ThemedMessageBox.Show("No mods were found in the specified directory! Please check your current directory again!", "Warning!");
                    _isMessageBoxOnScreen = true;
                    Program.TerminateApp();
                }
            }
        }

        /// <summary>
        /// This method is called when Form is about to be closed.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void ModManagerForm_Closing(object sender, EventArgs e)
        {
            using (StreamWriter sw = File.CreateText(CONFIG_FILE_NAME))
            {
                sw.WriteLine($"{ACTION_STATE}={settings[ACTION_STATE]}");
                sw.WriteLine($"{CHOICE_INDEX}={settings[CHOICE_INDEX]}");
                sw.WriteLine($"{DEV}={settings[DEV]}");
                sw.WriteLine($"{NO_MOVIES}={settings[NO_MOVIES]}");
                sw.WriteLine($"{FORCE_HIGH_POLY}={settings[FORCE_HIGH_POLY]}");
                sw.WriteLine($"{DOW_OPTIMIZATIONS}={settings[DOW_OPTIMIZATIONS]}");
                sw.WriteLine($"{AUTOUPDATE}={settings[AUTOUPDATE]}");
                sw.WriteLine($"{MULTITHREADED_JIT}={settings[MULTITHREADED_JIT]}");
                sw.WriteLine($"{AOT_COMPILATION}={settings[AOT_COMPILATION]}");
                sw.WriteLine($"{NO_FOG}={settings[NO_FOG]}");
				sw.WriteLine($"{UNI_EXTDLL}={settings[UNI_EXTDLL]}");
            }

            // If Timer Resolution was lowered we have to keep DoW Mod Manager alive or Timer Resolution will be reset
            if (IsTimerResolutionLowered)
            {
                // Threads could work even if application would be closed
                new Thread(() =>
                {
                    int triesCount = 0;
                    string procName = "";

                    // Set it to ModManager first
                    Process dowOrModManager = Process.GetCurrentProcess();

                    // Remember DoW process Name but if DoW is not launched - just terminate the Thread
                    if (_dowProcessName.Length > 0)
                        procName = _dowProcessName;
                    else
                        return;

                    // We will try 30 times and then Thread will be terminated regardless
                    while (triesCount < 30)
                    {
                        Thread.Sleep(1000);
                        try
                        {
                            Process[] dowCandidate = Process.GetProcessesByName(procName);
                            dowOrModManager = dowCandidate[0];

                            // We've done what we intended to do
                            break;
                        }
                        catch (Exception)
                        {
                            triesCount++;
                        }
                    }

                    // Wait until DoW would exit and then terminate the Thread
                    while (!dowOrModManager.HasExited)
                        Thread.Sleep(10000);
                }
                ).Start();
            }
        }

        /// <summary>
        /// This method defines the event handlers for when some file was changed.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void FileSystemWatcherOnChanged(object source, FileSystemEventArgs e)
        {
            SetUpAllNecessaryMods();

            // Invoke Mod Merger refresh should it exist.
            if (modMerger != null)
                modMerger.refreshAllModEntries();
        }

        /// <summary>
        /// This method updates the required mods Listbox when selecting a different installed Mod
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InstalledModsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            startModButton.Enabled = true;

            int index = installedModsListBox.SelectedIndex;

            if (index < 0 || index >= installedModsListBox.Items.Count)
            {
                index = getSelectedModIndex();
                installedModsListBox.SelectedIndex = index;
            }
            else
                setSelectedModIndex(index);

            currentModuleFilePath = ModuleFilePaths[index];

            requiredModsList.Items.Clear();

            // Read the file and display it line by line.
            using (StreamReader file = new StreamReader(currentModuleFilePath))
            {
                string line;

                // Populate the Required Mods List with entries from the .module file
                while ((line = file.ReadLine()) != null)
                {
                    // This line is commented
                    if (line.StartsWith("//") || line.StartsWith(";;"))
                        continue;

                    if (line.Contains("RequiredMod"))
                    {
                        line = Program.GetValueFromLine(line, deleteModule: false);

                        requiredModsList.Items.Add(line);
                    }
                }

                LoadModFoldersFromFile();
                CheckforInstalledMods();
            }
        }
        /// <summary>
        /// Gets the numerical index of the currently selected Mod.
        /// </summary>
        /// <returns></returns>
        private int getSelectedModIndex()
        {
            return settings[CHOICE_INDEX];
        }
        /// <summary>
        /// Sets the numerical index of the currently selected Mod.
        /// </summary>
        /// <param name="index"></param>
        private void setSelectedModIndex(int index)
        {
            settings[CHOICE_INDEX] = index;
        }

        /// <summary>
        /// This method Checks if there is a "ModFolder" attribute in each *.module file
        /// </summary>
        // Request the inlining of this method
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void LoadModFoldersFromFile()
        {
            int requiredModsCount = requiredModsList.Items.Count;
            ModFolderPaths = new string[requiredModsCount];

            // Read the file line by line and check for "ModFolder" attribute
            for (int i = 0; i < requiredModsCount; i++)
            {
                string moduleFilePath = CurrentDir + "\\" + requiredModsList.Items[i].ToString() + ".module";

                if (File.Exists(moduleFilePath))
                {
                    using (StreamReader file = new StreamReader(moduleFilePath))
                    {
                        string line;

                        while ((line = file.ReadLine()) != null)
                        {
                            if (line.Contains("ModFolder"))
                                ModFolderPaths[i] = Program.GetValueFromLine(line, deleteModule: true);
                        }
                    }
                }
                else
                    ModFolderPaths[i] = "MISSING";
            }
        }

        /// <summary>
        /// This method checks if the Mods are actually REALLY installed by checking if their asset folders are present by the name specified within the *.module files "Modfolder" tagline
        /// </summary>
        // Request the inlining of this method
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CheckforInstalledMods()
        {
            startModButton.Enabled = true;

            Stream myStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DoW_Mod_Manager.Resources.checkmark.png");
            pictureBox.Image = Image.FromStream(myStream);

            string folderPath;
            int itemsCount = requiredModsList.Items.Count;
            _isInstalled = new bool[itemsCount];

            for (int i = 0; i < itemsCount; i++)
            {
                folderPath = CurrentDir + "\\" + ModFolderPaths[i];

                if (Directory.Exists(folderPath))
                {
                    requiredModsList.Items[i] += " ...INSTALLED!";
                    _isInstalled[i] = true;
                }
                else
                {
                    requiredModsList.Items[i] += " ...MISSING!";
                    _isInstalled[i] = false;
                    startModButton.Enabled = false;

                    // Select missed mod so user could find it more easily
                    requiredModsList.SelectedIndex = i;

                    myStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DoW_Mod_Manager.Resources.cross.png");
                    pictureBox.Image = Image.FromStream(myStream);
                }
            }
            myStream.Close();
        }

        /// <summary>
        /// When selecting a different required Mod, check if fixMod button is needed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RequiredModsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = requiredModsList.SelectedIndex;

            if (index < 0 || index > requiredModsList.Items.Count)
                return;

            try
            {
                if (requiredModsList.SelectedItem.ToString().Contains("MISSING"))
                    fixMissingModButton.Enabled = true;
                else
                    fixMissingModButton.Enabled = false;
            }
            catch (Exception)
            {
                requiredModsList.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// This is the button to start the vanilla unmodded base game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartVanillaGameButton_Click(object sender, EventArgs e)
        {
            StartGameWithOptions("W40k");
        }

        /// <summary>
        /// This is the actual start button with which you can start the game with the currently selected mod
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartButton_Click(object sender, EventArgs e)
        {
            StartGameWithOptions(installedModsListBox.SelectedItem.ToString());
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SECURITY_ATTRIBUTES
        {
            public int nLength;
            public IntPtr lpSecurityDescriptor;
            public bool bInheritHandle;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PROCESS_INFORMATION
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public int dwProcessId;
            public int dwThreadId;
        }

        [Flags]
        public enum StartFlags : int
        {
            STARTF_USESHOWWINDOW = 0x00000001,
            STARTF_USESIZE = 0x00000002,
            STARTF_USEPOSITION = 0x00000004,
            STARTF_USECOUNTCHARS = 0x00000008,
            STARTF_USEFILLATTRIBUTE = 0x00000010,
            STARTF_RUNFULLSCREEN = 0x00000020, // ignored by non-x86 platforms
            STARTF_FORCEONFEEDBACK = 0x00000040,
            STARTF_FORCEOFFFEEDBACK = 0x00000080,
            STARTF_USESTDHANDLES = 0x00000100,
        }

        public enum WindowShowStyle : uint
        {
            /// <summary>Hides the window and activates another window.</summary>
            /// <remarks>See SW_HIDE</remarks>
            Hide = 0,
            /// <summary>Activates and displays a window. If the window is minimized
            /// or maximized, the system restores it to its original size and
            /// position. An application should specify this flag when displaying
            /// the window for the first time.</summary>
            /// <remarks>See SW_SHOWNORMAL</remarks>
            ShowNormal = 1,
            /// <summary>Activates the window and displays it as a minimized window.</summary>
            /// <remarks>See SW_SHOWMINIMIZED</remarks>
            ShowMinimized = 2,
            /// <summary>Activates the window and displays it as a maximized window.</summary>
            /// <remarks>See SW_SHOWMAXIMIZED</remarks>
            ShowMaximized = 3,
            /// <summary>Maximizes the specified window.</summary>
            /// <remarks>See SW_MAXIMIZE</remarks>
            Maximize = 3,
            /// <summary>Displays a window in its most recent size and position.
            /// This value is similar to "ShowNormal", except the window is not
            /// actived.</summary>
            /// <remarks>See SW_SHOWNOACTIVATE</remarks>
            ShowNormalNoActivate = 4,
            /// <summary>Activates the window and displays it in its current size
            /// and position.</summary>
            /// <remarks>See SW_SHOW</remarks>
            Show = 5,
            /// <summary>Minimizes the specified window and activates the next
            /// top-level window in the Z order.</summary>
            /// <remarks>See SW_MINIMIZE</remarks>
            Minimize = 6,
            /// <summary>Displays the window as a minimized window. This value is
            /// similar to "ShowMinimized", except the window is not activated.</summary>
            /// <remarks>See SW_SHOWMINNOACTIVE</remarks>
            ShowMinNoActivate = 7,
            /// <summary>Displays the window in its current size and position. This
            /// value is similar to "Show", except the window is not activated.</summary>
            /// <remarks>See SW_SHOWNA</remarks>
            ShowNoActivate = 8,
            /// <summary>Activates and displays the window. If the window is
            /// minimized or maximized, the system restores it to its original size
            /// and position. An application should specify this flag when restoring
            /// a minimized window.</summary>
            /// <remarks>See SW_RESTORE</remarks>
            Restore = 9,
            /// <summary>Sets the show state based on the SW_ value specified in the
            /// STARTUPINFO structure passed to the CreateProcess function by the
            /// program that started the application.</summary>
            /// <remarks>See SW_SHOWDEFAULT</remarks>
            ShowDefault = 10,
            /// <summary>Windows 2000/XP: Minimizes a window, even if the thread
            /// that owns the window is hung. This flag should only be used when
            /// minimizing windows from a different thread.</summary>
            /// <remarks>See SW_FORCEMINIMIZE</remarks>
            ForceMinimized = 11
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct STARTUPINFO
        {
            public int cb;
            public string lpReserved;
            public string lpDesktop;
            public string lpTitle;
            public int dwX;
            public int dwY;
            public int dwXSize;
            public int dwYSize;
            public int dwXCountChars;
            public int dwYCountChars;
            public int dwFillAttribute;
            public StartFlags dwFlags;
            public WindowShowStyle wShowWindow;
            public short cbReserved2;
            public IntPtr lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool CreateProcess(
           string lpApplicationName,
           string lpCommandLine,
           ref SECURITY_ATTRIBUTES lpProcessAttributes,
           ref SECURITY_ATTRIBUTES lpThreadAttributes,
           bool bInheritHandles,
           uint dwCreationFlags,
           IntPtr lpEnvironment,
           string lpCurrentDirectory,
           [In] ref STARTUPINFO lpStartupInfo,
           out PROCESS_INFORMATION lpProcessInformation);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool CreateProcessAsUserA(
           IntPtr hToken,
           string lpApplicationName,
           string lpCommandLine,
           ref SECURITY_ATTRIBUTES lpProcessAttributes,
           ref SECURITY_ATTRIBUTES lpThreadAttributes,
           bool bInheritHandles,
           ProcessCreationFlags dwCreationFlags,
           IntPtr lpEnvironment,
           string lpCurrentDirectory,
           ref STARTUPINFO lpStartupInfo,
           out PROCESS_INFORMATION lpProcessInformation
         );

        [StructLayout(LayoutKind.Sequential)]
        public struct PDETOUR_CREATE_PROCESS_ROUTINEA
        {
            public string lpApplicationName;
            public string lpCommandLine;
            public SECURITY_ATTRIBUTES lpProcessAttributes;
            public SECURITY_ATTRIBUTES lpThreadAttributes;
            public bool bInheritHandles;
            public uint dwCreationFlags;
            public IntPtr lpEnvironment;
            public string lpCurrentDirectory;
            public STARTUPINFO lpStartupInfo;
            public PROCESS_INFORMATION lpProcessInformation;
        }

        public enum ProcessCreationFlags : uint
        {
            DEBUG_PROCESS = 0x00000001,
            DEBUG_ONLY_THIS_PROCESS = 0x00000002,
            CREATE_SUSPENDED = 0x00000004,
            DETACHED_PROCESS = 0x00000008,
            CREATE_NEW_CONSOLE = 0x00000010,
            NORMAL_PRIORITY_CLASS = 0x00000020,
            IDLE_PRIORITY_CLASS = 0x00000040,
            HIGH_PRIORITY_CLASS = 0x00000080,
            REALTIME_PRIORITY_CLASS = 0x00000100,
            CREATE_NEW_PROCESS_GROUP = 0x00000200,
            CREATE_UNICODE_ENVIRONMENT = 0x00000400,
            CREATE_SEPARATE_WOW_VDM = 0x00000800,
            CREATE_SHARED_WOW_VDM = 0x00001000,
            CREATE_FORCEDOS = 0x00002000,
            BELOW_NORMAL_PRIORITY_CLASS = 0x00004000,
            ABOVE_NORMAL_PRIORITY_CLASS = 0x00008000,
            INHERIT_PARENT_AFFINITY = 0x00010000,
            INHERIT_CALLER_PRIORITY = 0x00020000,
            CREATE_PROTECTED_PROCESS = 0x00040000,
            EXTENDED_STARTUPINFO_PRESENT = 0x00080000,
            PROCESS_MODE_BACKGROUND_BEGIN = 0x00100000,
            PROCESS_MODE_BACKGROUND_END = 0x00200000,
            CREATE_BREAKAWAY_FROM_JOB = 0x01000000,
            CREATE_PRESERVE_CODE_AUTHZ_LEVEL = 0x02000000,
            CREATE_DEFAULT_ERROR_MODE = 0x04000000,
            CREATE_NO_WINDOW = 0x08000000,
            PROFILE_USER = 0x10000000,
            PROFILE_KERNEL = 0x20000000,
            PROFILE_SERVER = 0x40000000,
            CREATE_IGNORE_SYSTEM_DEFAULT = 0x80000000,
        }

        [DllImport("UNI_EXT.dll", CharSet = CharSet.Auto)]
        public static extern void DetourCreateProcessWithDllEx(string lpApplicationName,
                                          string lpCommandLine,
                                          ref SECURITY_ATTRIBUTES lpProcessAttributes,
                                          ref SECURITY_ATTRIBUTES lpThreadAttributes,
                                          bool bInheritHandles,
                                          ProcessCreationFlags dwCreationFlags,
                                          IntPtr lpEnvironment,
                                          string lpCurrentDirectory,
                                          [In] ref STARTUPINFO lpStartupInfo,
                                          out PROCESS_INFORMATION lpProcessInformation,
                                          string lpDllName,
                                          out PDETOUR_CREATE_PROCESS_ROUTINEA pfCreateProcessA);

        /// <summary>
        /// This method handles starting an instance of CurrentGameEXE with arguments
        /// </summary>
        /// <param name="modName"></param>
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
           /*
            STARTUPINFO si = new STARTUPINFO();
            PROCESS_INFORMATION pi = new PROCESS_INFORMATION();
            SECURITY_ATTRIBUTES pa = new SECURITY_ATTRIBUTES();
            SECURITY_ATTRIBUTES ta = new SECURITY_ATTRIBUTES();
            PDETOUR_CREATE_PROCESS_ROUTINEA cp = new PDETOUR_CREATE_PROCESS_ROUTINEA();

            si.cb = Marshal.SizeOf(si);
            si.dwFlags = StartFlags.STARTF_USESHOWWINDOW;
            si.wShowWindow = WindowShowStyle.Show; 

            DetourCreateProcessWithDllEx(CurrentGameEXE, arguments, ref pa, ref ta, true,
                   ProcessCreationFlags.CREATE_DEFAULT_ERROR_MODE | ProcessCreationFlags.CREATE_SUSPENDED,
                   IntPtr.Zero, null, ref si, out pi,
                   "UNI_EXT.dll", out cp);
            */
            
            _dowProcessName = proc.ProcessName;

            // Create new thread to change the process CPU affinity after the game has started.
            if (settings[DOW_OPTIMIZATIONS] == 1)
            {
                // Threads could work even if application would be closed
                new Thread(() =>
                {
                    int triesCount = 0;
                    string procName = _dowProcessName;

                    // We will try 30 times and then Thread will be terminated regardless
                    while (triesCount < 30)
                    {
                        Thread.Sleep(1000);
                        try
                        {
                            Process[] dow = Process.GetProcessesByName(procName);
                            dow[0].PriorityClass = ProcessPriorityClass.High;
                            dow[0].ProcessorAffinity = (IntPtr)0x0006;          // Affinity 6 means using only CPU threads 2 and 3 (6 = 0110)
                            break;                                              // We've done what we intended to do
                        }
                        catch (Exception)
                        {
                            triesCount++;
                        }
                    }
                }
                ).Start();
            }

            // Create a new thread for the fog removal which manipulates the process memory after the game has started.
            if (settings[NO_FOG] == 1)
            {
                new Thread(() =>
                {
                    int timeOutCounter = 0;
                    string procName = _dowProcessName;

                    // We will try 30 times and then Thread will be terminated regardless
                    while (timeOutCounter < 30)
                    {
                        Thread.Sleep(1000);
                        try
                        {
                            Process[] dow = Process.GetProcessesByName(procName);
                            FogRemover.DisableFog(dow[0]);
                            break;                                              // We've done what we intended to do
                        }
                        catch (Exception)
                        {
                            timeOutCounter++;
                        }
                    }
                }
                ).Start();
            }

            // Create a new thread for UNI_EXTDLL which manipulates the process memory after the game has started.
            if (settings[UNI_EXTDLL] == 1)
            {
                new Thread(() =>
                {
                    int timeOutCounter = 0;
                    string procName = _dowProcessName;

                    // We will try 30 times and then Thread will be terminated regardless
                    while (timeOutCounter < 30)
                    {
                        Thread.Sleep(1000);
                        try
                        {
                            Process[] dow = Process.GetProcessesByName(procName);
                            UNI_EXTDLLLoader.UNI_EXTdllInjector(dow[0], CurrentDir + "\\UNI_EXT.DLL");
                            break;                                              // We've done what we intended to do
                        }
                        catch (Exception)
                        {
                            timeOutCounter++;
                        }
                    }
                }
                ).Start();
            }
            
        }

        /// <summary>
        /// This is the checkbox that controls the starting option '-dev'. 
        /// It allows for additional debug options in-game and log files.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OptimizationsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (optimizationsCheckBox.Checked)
                settings[DOW_OPTIMIZATIONS] = 1;
            else
                settings[DOW_OPTIMIZATIONS] = 0;
        }

        /// <summary>
        /// This checkbox removes the long distance fog in from the game, without having to remove it manually from each map.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NoFogCheckboxCheckedChanged(object sender, EventArgs e)
        {
            if (noFogCheckbox.Checked)
                settings[NO_FOG] = 1;
            else
                settings[NO_FOG] = 0;
        }

        /// <summary>
        /// This checkbox loads UNI_EXT.DLL to program memory.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadUNI_EXTDLLCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (loadUNI_EXTDLLCheckBox.Checked)
                settings[UNI_EXTDLL] = 1;
            else
                settings[UNI_EXTDLL] = 0;
        }

        /// <summary>
        /// This method collects and displays the list of required mods for a selected mod in order to function correctly.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RequiredModsList_DrawItem(object sender, DrawItemEventArgs e)
        {
            // No need to draw anything if there are no required mods
            if (requiredModsList.Items.Count == 0)
                return;

            // Draw the background of the ListBox control for each item.
            e.DrawBackground();

            Brush myBrush;

            if (_isInstalled[e.Index])
                myBrush = Brushes.LimeGreen;
            else
                myBrush = Brushes.Red;

            // Draw the current item text based on the current 
            // Font and the custom brush settings.
            e.Graphics.DrawString(((ListBox)sender).Items[e.Index].ToString(),
                                  e.Font,
                                  myBrush,
                                  e.Bounds);

            // If the ListBox has focus, draw a focus rectangle 
            // around the selected item.
            e.DrawFocusRectangle();
        }

        /// <summary>
        /// This method opens the Mod Downloader form when the button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownloadButton_Click(object sender, EventArgs e)
        {
            new ModDownloaderForm(this).Show();
        }

        /// <summary>
        /// This method opens the Mod Downloader form and gives it a mod name to search for
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FixMissingModButton_Click(object sender, EventArgs e)
        {
            if (requiredModsList.SelectedItem == null)
                return;

            string modName = requiredModsList.SelectedItem.ToString();

            int indexOfSpace = modName.IndexOf(" ");
            modName = modName.Substring(0, indexOfSpace);

            if (modName == "DXP2" || modName == "WXP" || modName == "W40k")
            {
                ThemedMessageBox.Show("You are missing one of the base modules! Reinstall the game to fix it", "Warning!");
                return;
            }

            new ModDownloaderForm(this, modName).Show();
        }

        /// <summary>
        /// This method opens the Mod Merger form when the button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModMergeButton_Click(object sender, EventArgs e)
        {
            modMerger = new ModMergerForm(this);
            modMerger.Show();
        }

        /// <summary>
        /// This method opens the Settings Manager form when the button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsButton_Click(object sender, EventArgs e)
        {
            new SettingsManagerForm(this).Show();
        }

        /// <summary>
        /// This method check for errors in warnings.log and shows them to user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckForErrorsButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(WARNINGS_LOG))
            {
                string errors = "";

                using (StreamReader logFile = new StreamReader(WARNINGS_LOG))
                {
                    string line;

                    while ((line = logFile.ReadLine()) != null)
                    {
                        if (line.Contains("Fatal Data Error"))
                            errors += line + "\n";
                    }
                }

                if (errors.Length > 0)
                    ThemedMessageBox.Show(errors, "Errors:");
                else
                    ThemedMessageBox.Show($"No errors were found in {WARNINGS_LOG}!", "Errors:");
            }
            else
                ThemedMessageBox.Show($"There is no {WARNINGS_LOG} file\nThat means that there is no errors!", "Errors:");
        }

        /// <summary>
        /// This method performs the necessary data operations in order to toggle the LAA for a given EXE file back and forth.
        /// </summary>
        /// <param name="file"></param>
        /// <returns>bool</returns>
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

        /// <summary>
        /// This method checks if a file is yet still opened and thus blocked.
        /// It prevents crashes when attempting to write to files not yet closed.
        /// </summary>
        /// <param name="file"></param>
        /// <returns>bool</returns>
        private bool IsFileNotLocked(string file)
        {
            FileStream fs = null;
            try
            {
                fs = File.Open(file, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                // The file is unavailable because it is:
                // still being written to
                // or being processed by another thread
                // or does not exist (has already been processed)
                return false;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }

            // File is not locked
            return true;
        }

        /// <summary>
        /// This method handles the proper toggling of the LAA flag for the Soulstorm.exe and the GraphicsConfig.exe.
        /// It can handle the cases when users have previously patched the EXE files only partially.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonToggleLAA_Click(object sender, EventArgs e)
        {
            // Check if the Game is LAA Patched and fill in the Label properly
            string currentGamePath = CurrentDir + "\\" + CurrentGameEXE;
            string currentGraphucsConfigPath = CurrentDir + "\\" + GraphicsConfigEXE;

            if (IsFileNotLocked(currentGamePath) && IsFileNotLocked(currentGraphucsConfigPath))
            {
                if ((_isGameEXELAAPatched && _isGraphicsConfigLAAPatched) || (!_isGameEXELAAPatched && !_isGraphicsConfigLAAPatched))
                {
                    _isGameEXELAAPatched = ToggleLAA(currentGamePath);
                    _isGraphicsConfigLAAPatched = ToggleLAA(currentGraphucsConfigPath);
                }
                else if (!_isGameEXELAAPatched)
                    _isGameEXELAAPatched = ToggleLAA(currentGamePath);
                else if (!_isGraphicsConfigLAAPatched)
                    _isGraphicsConfigLAAPatched = ToggleLAA(currentGraphucsConfigPath);

                SetGameLAALabelText();
                SetGraphicsConfigLAALabelText();
            }
        }

        /// <summary>
        /// This method can be used ouside this class to get a setting
        /// </summary>
        /// <param name="setting"></param>
        public int GetSetting(string setting)
        {
            if (settings.ContainsKey(setting))
                return settings[setting];
            else
                return -1;
        }

        /// <summary>
        /// This method can be used ouside this class to change a setting and update the GUI
        /// </summary>
        /// <param name="setting"></param>
        /// <param name="newValue"></param>
        public void ChangeSetting(string setting, int newValue)
        {
            if (setting != ACTION_STATE)
            {
                // Makes sure that newValue is in range of acceptable values. Basically a Clamp() method
                if (newValue < 0)
                    newValue = 0;
                else if (newValue > 1)
                    newValue = 1;
            }

            switch (setting)
            {
                case ACTION_STATE:
                    settings[ACTION_STATE] = newValue;
                    break;
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
                case DOW_OPTIMIZATIONS:
                    settings[DOW_OPTIMIZATIONS] = newValue;
                    optimizationsCheckBox.Checked = Convert.ToBoolean(newValue);
                    break;
                case AUTOUPDATE:
                    settings[AUTOUPDATE] = newValue;
                    break;
                case MULTITHREADED_JIT:
                    settings[MULTITHREADED_JIT] = newValue;
                    break;
                case AOT_COMPILATION:
                    settings[AOT_COMPILATION] = newValue;
                    break;
            }
        }

        /// <summary>
        /// This method opens an AboutForm
        /// </summary>
        private void HomePageLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new AboutForm(this).Show();
        }

        /// <summary>
        /// This event handles the case when the no fog checkbox and UNI_EXT.dll is disabled, to show a tooltip why it is disabled.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModManagerForm_MouseMove(object sender, MouseEventArgs e)
        {
            Control control = GetChildAtPoint(e.Location);
            if (control != null)
            {
                if (!control.Enabled && control == noFogCheckbox)
                {
                    string toolTipString = _disabledNoFogTooltip.GetToolTip(control);
                    _disabledNoFogTooltip.Show(toolTipString, control, control.Width / 2, control.Height / 2);
                    _currentToolTipControl = control;
                }
                if (!control.Enabled && control == loadUNI_EXTDLLCheckBox)
                {
                    string toolTipString = _disabledLoadUNI_EXTDLLCheckBoxTooltip.GetToolTip(control);
                    _disabledLoadUNI_EXTDLLCheckBoxTooltip.Show(toolTipString, control, control.Width / 2, control.Height / 2);
                    _currentToolTipControl = control;
                }
            }
            else
            {
                if (_currentToolTipControl != null) _disabledNoFogTooltip.Hide(_currentToolTipControl);
                if (_currentToolTipControl != null) _disabledLoadUNI_EXTDLLCheckBoxTooltip.Hide(_currentToolTipControl);
                _currentToolTipControl = null;
            }
        }

        /// <summary>
        /// This event opens a new browser window of the currently selected Mod upon pressing the linked button. 
        /// This code is based on this post: https://www.codeproject.com/Questions/852563/How-to-open-file-explorer-at-given-location-in-csh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void open_folder_button_Click(object sender, EventArgs e)
        {
            string pathToMod = Path.Combine(CurrentDir, AllValidModules[getSelectedModIndex()].getPath);
            try
            {
                if (Directory.Exists(pathToMod))
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        Arguments = pathToMod,
                        FileName = "explorer.exe"
                    };

                    Process.Start(startInfo);
                }
                else
                {
                    MessageBox.Show(string.Format("Directory: \"{0}\" does not exist!", pathToMod));
                }
            }
            catch (Exception)
            {
                MessageBox.Show(string.Format("Permission to access the folder: \"{0}\" denied! Make sure you have the necessary access rights!", pathToMod));
            }
        }
    }
}
