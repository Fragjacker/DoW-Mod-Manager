using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;
using System.Security.Permissions;

/// <summary>
/// Contains the strings for each supported Executbale.
/// </summary>
public struct GameExecutable
{
    public string Soulstorm, DarkCrusade;

    public GameExecutable(string SS,
                          string DC)
    {
        Soulstorm = SS;
        DarkCrusade = DC;
    }
}

namespace DoW_Mod_Manager
{
    /// <summary>
    ///  Contains all elements necessary for the Form
    /// </summary>
    public partial class ModManagerForm : Form
    {

        public string currentDir = ""; //Is the current Directory of Soulstorm
        private string _devMode = ""; //Contains the argument for starting the .exe in dev mode
        private string _noIntroMode = " -nomovies"; //Contains the argument for starting the .exe with no Intromovies
        private string _highPolyMode = "";//Contains the argument for starting the .exe in High Poly Mode.
        public string[] _filePaths; //Stores the paths of the found .module files in the Soulstorm directory
        public string[] _modFolderPaths;//Stores the paths of the Required Mods stored within the .module files. This will be used to check for their actual presence/absence in the Soulstorm Dir.
        public List<string> allFoundModules = null; //Contains the list of all available Mods that will be used by the Mod Merger
        public List<string> allValidModules = null; //Contains the list of all playable Mods that will be used by the Mod Merger
        private bool[] _isInstalled; //A boolean array that maps Index-wise to the filepaths indices. Index 0 checks if required mod at index 0 in the _filepaths is installed or not.
        private bool isGameEXELAAPatched = false; //Tells if soulstorm is LAA patched or NOT.
        private bool isGraphicsConfigLAAPatched = false; //Tells if graphicsconfig is LAA patched or NOT.
        private GameExecutable gameExe = new GameExecutable("Soulstorm.exe", "DarkCrusade.exe");
        private string currentGameEXE = "";

        /// <summary>
        ///  Initializes all the necessary components used by the GUI
        /// </summary>
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public ModManagerForm()
        {
            InitializeComponent();
        }

        private void Form1_Closing(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Performs actions when the Application is started via the .exe file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            //TODO: Set proper directory again
            //currentDir = "D:\\THQ\\Dawn of War - Soulstorm";
            currentDir = Directory.GetCurrentDirectory();
            currentGameEXE = getCurrentEXE();
            _filePaths = Directory.GetFiles(currentDir, currentGameEXE);

            //Check if there was a valid Directory detected previously, then perform getting all the info to populate the lists
            if (_filePaths.Length != 0)
            {
                textBox1.AppendText(currentDir);
                setUpAllNecessaryMods();
                isGameEXELAAPatched = IsLargeAware(Directory.GetFiles(currentDir, currentGameEXE)[0]);
                isGraphicsConfigLAAPatched = IsLargeAware(Directory.GetFiles(currentDir, "GraphicsConfig.exe")[0]);
                setSoulstormLAALabelText();
                setGraphicsConfigLAALabelText();
                AddFileSystemWatcher();
                // Initialize values with values from previous values or defaults.
                reselectSavedMod();
                checkBox1.Checked = (bool)Properties.Settings.Default["DEV"];
                checkBox2.Checked = (bool)Properties.Settings.Default["NOMOVIES"];
                checkBox3.Checked = (bool)Properties.Settings.Default["HIGHPOLY"];
            }
            else
            {
                MessageBox.Show("ERROR finding " + currentGameEXE + " in this directory! Please put the DoW Mod Manager v1.5.exe in the directory that contains the correct executable!");
                Application.Exit();
                return;
            }

            // This was implemented to find soulstorm by using the Registry Key Install location. But since the resource folder must be placed in a certain direction i've decided that a local directory scan would suffice.
            // Uncomment this to make the Form Window open up. Since the program will exit if there's no local Soulstorm.exe file be found.

            //TODO: Uncommoment below block and comment try and catch block again!

            //try
            //{
            //    RegistryKey regKey = Registry.LocalMachine;
            //    regKey = regKey.OpenSubKey(@"Software\\THQ\\Dawn of War - Soulstorm\\");

            //    if (regKey != null)
            //    {
            //        currentDir = regKey.GetValue("InstallLocation").ToString();
            //    }
            //}
            //catch (Exception eventos)
            //{
            //    throw new FileNotFoundException("ERROR finding Soulstorm on your Computer! If you're using the Disc version please place the .exe in the root directory! Else reinstall on STEAM!", eventos);
            //}

            //textBox1.AppendText(currentDir);
            //setUpAllNecessaryMods();
            //isSoulstormLAAPatched = IsLargeAware(Directory.GetFiles(currentDir, currentGameEXE)[0]);
            //isGraphicsConfigLAAPatched = IsLargeAware(Directory.GetFiles(currentDir, "GraphicsConfig.exe")[0]);
            //setSoulstormLAALabelText();
            //setGraphicsConfigLAALabelText();
            //AddFileSystemWatcher();
            //// Initialize values with values from previous values or defaults.
            //InstalledModsList.SelectedIndex = (int)Properties.Settings.Default["ChoiceIndex"]; //Set default selection to index 0 in order to avoid crashes
            //checkBox1.Checked = (bool)Properties.Settings.Default["DEV"];
            //checkBox2.Checked = (bool)Properties.Settings.Default["NOMOVIES"];
            //checkBox3.Checked = (bool)Properties.Settings.Default["HIGHPOLY"];
        }

        /// <summary>
        /// This function handles the reselection of a previously selected mod.
        /// </summary>
        private void reselectSavedMod()
        {
            int savedIndex = (int)Properties.Settings.Default["ChoiceIndex"];
            if (InstalledModsList.Items.Count > savedIndex)
            {
                InstalledModsList.SelectedIndex = savedIndex;
            }
            else
            {
                InstalledModsList.SelectedIndex = InstalledModsList.Items.Count - 1;
            }
        }

        /// <summary>
        /// This adds FileSystem watcher to capture any file changes in the game directories.
        /// </summary>
        private void AddFileSystemWatcher()
        {
            fileSystemWatcher1.Path = currentDir;

            // Watch for changes in LastAccess and LastWrite times, and
            // the renaming of files or directories.
            fileSystemWatcher1.NotifyFilter = NotifyFilters.LastAccess
                                    | NotifyFilters.LastWrite
                                    | NotifyFilters.FileName
                                    | NotifyFilters.DirectoryName;

            // Add event handlers.
            fileSystemWatcher1.Changed += OnChanged;
            fileSystemWatcher1.Created += OnChanged;
            fileSystemWatcher1.Deleted += OnChanged;
            fileSystemWatcher1.Renamed += OnRenamed;

            // Begin watching.
            fileSystemWatcher1.EnableRaisingEvents = true;
        }

        /// <summary>
        /// This function defines the event handlers for when some file was changed.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            setUpAllNecessaryMods();
        }

        /// <summary>
        /// This function defines the event handlers for when some file was renamed.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnRenamed(object source, RenamedEventArgs e)
        {
            // Specify what is done when a file is renamed.
            setUpAllNecessaryMods();
        }


        /// <summary>
        /// A refactored method that is used to initialize or refresh the Mod Managers main page
        /// </summary>
        public void setUpAllNecessaryMods()
        {

            getMods();
            getModFoldersFromFile();
            int LastSelectedItem = InstalledModsList.SelectedIndex; //Store last selected Item to be reselected when Mod Merging is complete.
            InstalledModsList.SelectedIndex = LastSelectedItem; //Set default selection to index 0 in order to avoid crashes
        }

        /// <summary>
        /// Checks if the Mods are actually REALLY installed by checking if their asset folders are present by the name specified within the .module files "Modfolder" tagline
        /// </summary>
        public void checkforInstalledMods()
        {
            startButton1.Enabled = true;

            string str_Path = Path.GetFullPath(currentDir + "\\DoW Mod Manager Resources\\Checkmark.png");
            try
            {
                pictureBox1.Image = Image.FromFile(str_Path);
            }
            catch
            {
                MessageBox.Show("ERROR! COULD NOT FIND FOLDER 'DoW Mod Manager Resources' IN THIS DIRECTORY!");
                Application.Exit();
                return;
            }

            int counter = 0;
            string localstring = "";
            string folderPath = "";
            _isInstalled = new bool[RequiredModsList.Items.Count];

            while (counter < RequiredModsList.Items.Count)
            {
                //string folderPath = Directory.Exists((_filePaths[counter].ToString() + "\\" + workstring));
                folderPath = currentDir + "\\" + _modFolderPaths[counter];

                if (Directory.Exists(folderPath))
                {
                    localstring = RequiredModsList.Items[counter].ToString();
                    RequiredModsList.Items.RemoveAt(counter);
                    RequiredModsList.Items.Insert(counter, localstring + "...INSTALLED!");
                    _isInstalled[counter] = true;

                }
                else
                {
                    localstring = RequiredModsList.Items[counter].ToString();
                    RequiredModsList.Items.RemoveAt(counter);
                    RequiredModsList.Items.Insert(counter, localstring + "...MISSING!");
                    _isInstalled[counter] = false;
                    startButton1.Enabled = false;

                    str_Path = Path.GetFullPath(currentDir + "\\DoW Mod Manager Resources\\cross.png");
                    pictureBox1.Image = Image.FromFile(str_Path);

                }
                counter++;
            }
        }

        /// <summary>
        /// This method returns the last word from an inputstring by using regex. For example using "RequiredMod.1 = Yourmod" will result in "Yourmod" beeing returned
        /// </summary>
        /// <param name="inputstring"></param>
        /// <returns>string</returns>
        private string getLastEntryFromLine(string inputstring)
        {

            string text = inputstring;
            string pat = @"\S*\s*$";
            string result = "";

            // Instantiate the regular expression object.
            Regex require = new Regex(pat, RegexOptions.IgnoreCase);

            // Match the regular expression pattern against a text string.

            Match match = require.Match(text);

            if (match.Success)
            {
                result = match.Value.Replace(" ", "");
            }
            return result;
        }

        /// <summary>
        /// Returns the name of the Module file without it's extension and all it's whitespaces removed.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string regexGetModFolderFromFile(string input)
        {
            string text = input;
            string pat = @"\S*\s*$";
            string result = "";

            // Instantiate the regular expression object.
            Regex require = new Regex(pat, RegexOptions.IgnoreCase);

            // Match the regular expression pattern against a text string.

            Match match = require.Match(text);

            if (match.Success)
            {
                result = match.Value.Replace(" ", "");
                result = match.Value.Replace(".module", "");
            }
            return result;
        }

        /// <summary>
        /// Finds all installed .module files and displays them in the Installed Mods Listbox without their .module extension
        /// </summary>
        private void getMods()
        {
            List<string> newfilePathsArray = new List<string>(); //Make a new list for the new Pathitems
            allValidModules = new List<string>();
            allFoundModules = new List<string>();

            int Index = 0;
            InstalledModsList.Items.Clear();
            string line = "";

            _filePaths = Directory.GetFiles(currentDir, "*.module");

            if (_filePaths.Length != 0)
            {
                foreach (string s in _filePaths)
                {
                    //Find the List of ALL found module files for the Mod Merger available Mods List
                    allFoundModules.Add(Path.GetFileNameWithoutExtension(s));

                    // Read the .module file to see if the mod is playable
                    System.IO.StreamReader file = new System.IO.StreamReader(s);

                    // Filter the unplayable mods and populate the List only with playable mods

                    while ((line = file.ReadLine()) != null)
                    {
                        if (modIsPlayable(line) == true)
                        {
                            newfilePathsArray.Add(_filePaths[Index]);
                            InstalledModsList.Items.Add(Path.GetFileNameWithoutExtension(s));
                            allValidModules.Add(Path.GetFileNameWithoutExtension(s));
                        }
                    }
                    file.Close();
                    Index++;
                }
                _filePaths = newfilePathsArray.ToArray(); //Override the old array that contained unplayable mods with the new one.
            }
            if (_filePaths.Length == 0 || allFoundModules.Count == 0)
            {
                MessageBox.Show("No mods were found in the specified directory! Please check your current directory again!");
                Application.Exit();
                return;
            }
        }



        /// <summary>
        /// This function returns 'true' if a Mod is set as "Playable = 1" in the .module file 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private bool modIsPlayable(string input)
        {
            bool isPlayable = false;
            string textline = input;

            string pat = @"Playable = 1";

            // Instantiate the regular expression object.
            Regex require = new Regex(pat, RegexOptions.IgnoreCase);

            // Match the regular expression pattern against a text string.

            Match match = require.Match(textline);

            if (match.Success)
            {
                isPlayable = true;
            }
            return isPlayable;
        }


        /// <summary>
        /// Reads the .module file and scans for "RequiredMods" lines and returns if a line was found or not via true/false. This is used to add the lines to the Form Window.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>bool</returns>
        private bool regexGetRequiredMod(string input)
        {
            string text = input;
            string pat = @"\bRequiredMod\b";
            string patCommented1 = @"^[;]+";
            string patCommented2 = @"^[\/]+";
            bool state = false;

            // Instantiate the regular expression object.
            //Regex require = new Regex(pat, RegexOptions.IgnoreCase);
            //Regex notrequire = new Regex(patCommented1, RegexOptions.IgnoreCase);

            // Match the regular expression pattern against a text string.

            //Match m = require.Match(text);

            foreach (Match match in Regex.Matches(input, pat))
            {
                state = true;
            }

            foreach (Match match in Regex.Matches(input, patCommented1))
            {
                state = false;
            }

            foreach (Match match in Regex.Matches(input, patCommented2))
            {
                state = false;
            }

            return state;
        }

        /// <summary>
        /// Checks if a line contains the Word "Modfolder" with true/false
        /// </summary>
        /// <param name="input"></param>
        /// <returns>bool</returns>
        private bool CheckregexModFolderExist(string input)
        {
            string text = input;
            string pat = @"ModFolder";
            bool state = false;

            // Instantiate the regular expression object.
            //Regex require = new Regex(pat, RegexOptions.IgnoreCase);

            // Match the regular expression pattern against a text string.

            //Match m = require.Match(text);

            foreach (Match match in Regex.Matches(input, pat))
            {
                state = true;
            }
            return state;
        }

        /// <summary>
        /// When selecting a different installed Mod, update the required mods Listbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InstalledModsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            startButton1.Enabled = true;

            int index = InstalledModsList.SelectedIndex;
            Properties.Settings.Default["ChoiceIndex"] = index;
            string currentPath = _filePaths[index];
            string line = "";

            RequiredModsList.Items.Clear();

            // Read the file and display it line by line.
            System.IO.StreamReader file = new System.IO.StreamReader(currentPath);

            // Populate the Required Mods List with entries from the .module file

            while ((line = file.ReadLine()) != null)
            {
                if (regexGetRequiredMod(line) == true)
                {
                    RequiredModsList.Items.Add(line);
                }
            }

            getModFoldersFromFile();
            checkforInstalledMods();
            file.Close();
        }

        /// <summary>
        ///  This one checks if the Mod Assett folders, specified in each .module file, do actually exist
        /// </summary>
        private void getModFoldersFromFile()
        {
            int index = 0;
            int count = 0;
            string currentPath;
            string line = "";
            _modFolderPaths = new string[RequiredModsList.Items.Count];

            // Read the file and display it line by line.
            while (index < RequiredModsList.Items.Count)
            {
                currentPath = currentDir + "\\" + getLastEntryFromLine(RequiredModsList.Items[index].ToString()) + ".module";
                if (File.Exists(currentPath))
                {
                    System.IO.StreamReader file = new System.IO.StreamReader(currentPath);

                    while ((line = file.ReadLine()) != null)
                    {
                        if (CheckregexModFolderExist(line) == true)
                        {
                            _modFolderPaths[count] = regexGetModFolderFromFile(line);
                            count++;
                        }
                    }
                    file.Close();
                }
                else
                {
                    //_modFolderPaths[count] = getLastEntryFromLine("MISSING");
                    _modFolderPaths[count] = "MISSING";
                    count++;
                }
                index++;
            }
        }

        /// <summary>
        /// This is the actual start button with which you can start the game with the currently selected mod
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startButton1_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = currentDir + "\\" + currentGameEXE;
            startInfo.Arguments = @"-modname " + InstalledModsList.SelectedItem + _devMode + _noIntroMode + _highPolyMode;
            Process.Start(startInfo);
        }

        /// <summary>
        /// This is the button to start the vanilla unmodded base game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startVanillaGameButton_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = currentDir + "\\" + currentGameEXE;
            startInfo.Arguments = @"-modname W40k" + _devMode + _noIntroMode + _highPolyMode;
            Process.Start(startInfo);
        }

        /// <summary>
        /// This is the checkbox that controls the starting option '-dev'. 
        /// It allows for additional debug options in-game and log files.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                _devMode = " -dev";
            }
            else
            {
                _devMode = "";
            }
            Properties.Settings.Default["DEV"] = checkBox1.Checked;
        }

        /// <summary>
        /// This is the checkbox that controls the starting option '-nomovies'. 
        /// It prevents any movies/intros from being played.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                _noIntroMode = " -nomovies";
            }
            else
            {
                _noIntroMode = "";
            }
            Properties.Settings.Default["NOMOVIES"] = checkBox2.Checked;
        }

        /// <summary>
        /// This is the checkbox that controls the starting option '-forcehighpoly'. 
        /// This disabled the LOD system and will display the highes mesh detail at any distance.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                _highPolyMode = " -forcehighpoly";
            }
            else
            {
                _highPolyMode = "";
            }
            Properties.Settings.Default["HIGHPOLY"] = checkBox3.Checked;
        }

        /// <summary>
        /// This function collects and displays the list of required mods for a selected mod in order to function correctly.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RequiredModsList_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            //
            // Draw the background of the ListBox control for each item.
            // Create a new Brush and initialize to a Black colored brush
            // by default.
            //
            e.DrawBackground();
            //
            // Determine the color of the brush to draw each item based on 
            // the index of the item to draw. Could be extended for an Orange Brush for indicating outdated Mods.
            //
            Brush myBrush = Brushes.Black;

            switch (_isInstalled[e.Index])
            {
                case true:
                    myBrush = Brushes.Green;
                    break;

                case false:
                    myBrush = Brushes.Red;
                    break;
            }

            //
            // Draw the current item text based on the current 
            // Font and the custom brush settings.
            //
            e.Graphics.DrawString(((ListBox)sender).Items[e.Index].ToString(), e.Font, myBrush, e.Bounds, StringFormat.GenericDefault);
            //
            // If the ListBox has focus, draw a focus rectangle 
            // around the selected item.
            //
            e.DrawFocusRectangle();
        }

        /// <summary>
        /// This is function opens the Mod Merger form when the button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            ModMergerForm mergerWindow = new ModMergerForm(this);
            mergerWindow.Show();
        }

        /// <summary>
        /// This function draws the LAA text for the Soulstorm label depending on whether the flag is true (Green) or false (Red).
        /// </summary>
        private void setSoulstormLAALabelText()
        {
            switch (isGameEXELAAPatched)
            {
                case true:
                    SoulstormLAAStatusLabel.Text = currentGameEXE + ": LAA Active";
                    SoulstormLAAStatusLabel.ForeColor = System.Drawing.Color.Green;
                    break;
                case false:
                    SoulstormLAAStatusLabel.Text = currentGameEXE + ": LAA Inactive";
                    SoulstormLAAStatusLabel.ForeColor = System.Drawing.Color.Red;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// This function draws the LAA text for the GraphicsConfig label depending on whether the flag is true (Green) or false (Red).
        /// </summary>
        private void setGraphicsConfigLAALabelText()
        {
            switch (isGraphicsConfigLAAPatched)
            {
                case true:
                    GraphicsConfigLAAStatusLabel.Text = "GraphicsConfig.exe: LAA Active";
                    GraphicsConfigLAAStatusLabel.ForeColor = System.Drawing.Color.Green;
                    break;
                case false:
                    GraphicsConfigLAAStatusLabel.Text = "GraphicsConfig.exe: LAA Inactive";
                    GraphicsConfigLAAStatusLabel.ForeColor = System.Drawing.Color.Red;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// This function instigates the test if a given EXE is LAA patched or not.
        /// </summary>
        /// <param name="file"></param>
        /// <returns>bool</returns>
        static bool IsLargeAware(string file)
        {
            using (var fs = File.OpenRead(file))
            {
                return IsLargeAware(fs);
            }
        }

        /// <summary>
        /// This function performs the data probing to determine if a given file is LAA patched or not.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns>bool</returns>
        static bool IsLargeAware(Stream stream)
        {
            const int IMAGE_FILE_LARGE_ADDRESS_AWARE = 0x20;

            var br = new BinaryReader(stream);

            if (br.ReadInt16() != 0x5A4D)       //No MZ Header
                return false;

            br.BaseStream.Position = 0x3C;
            var peloc = br.ReadInt32();         //Get the PE header location.

            br.BaseStream.Position = peloc;
            if (br.ReadInt32() != 0x4550)       //No PE header
                return false;

            br.BaseStream.Position += 0x12;
            Int16 LAAFlag = br.ReadInt16();
            br.Close();
            return (LAAFlag & IMAGE_FILE_LARGE_ADDRESS_AWARE) == IMAGE_FILE_LARGE_ADDRESS_AWARE;
        }

        /// <summary>
        /// This function performs the necessary data operations in order to toggle the LAA for a given EXE file back and forth.
        /// </summary>
        /// <param name="file"></param>
        /// <returns>bool</returns>
        static bool toggleLAA(string file)
        {
            bool result = false;
            var fs = File.Open(file, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            const int IMAGE_FILE_LARGE_ADDRESS_AWARE = 0x20;

            var br = new BinaryReader(fs);
            var bw = new BinaryWriter(fs);

            if (br.ReadInt16() != 0x5A4D)       //No MZ Header
                return result;

            br.BaseStream.Position = 0x3C;
            var peloc = br.ReadInt32();         //Get the PE header location.

            br.BaseStream.Position = peloc;
            if (br.ReadInt32() != 0x4550)       //No PE header
                return result;

            br.BaseStream.Position += 0x12;
            long nFilePos = (int)br.BaseStream.Position;
            Int16 LAAFlag = br.ReadInt16();
            if ((LAAFlag & IMAGE_FILE_LARGE_ADDRESS_AWARE) != IMAGE_FILE_LARGE_ADDRESS_AWARE)
            {
                LAAFlag |= IMAGE_FILE_LARGE_ADDRESS_AWARE;
                long nFilePos1 = bw.Seek((int)nFilePos, SeekOrigin.Begin);
                bw.Write(LAAFlag);
                bw.Flush();
                result = true;
            }
            else if ((LAAFlag & IMAGE_FILE_LARGE_ADDRESS_AWARE) == IMAGE_FILE_LARGE_ADDRESS_AWARE)
            {
                LAAFlag ^= IMAGE_FILE_LARGE_ADDRESS_AWARE;
                long nFilePos1 = bw.Seek((int)nFilePos, SeekOrigin.Begin);
                bw.Write(LAAFlag);
                bw.Flush();
                result = false;
            }
            br.Close();
            bw.Close();
            return result;
        }

        /// <summary>
        /// This function checks if a file is yet still opened and thus blocked.
        /// It prevents crashes when attempting to write to files not yet closed.
        /// </summary>
        /// <param name="file"></param>
        /// <returns>bool</returns>
        private bool IsFileLocked(string file)
        {
            FileStream stream = null;
            try
            {
                stream = File.Open(file, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }

        /// <summary>
        /// This function handles the proper toggling of the LAA flag for the Soulstorm.exe and the GraphicsConfig.exe.
        /// It can handle the cases when users have previously patched the EXE files only partially.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonToggleLAA_Click(object sender, EventArgs e)
        {
            //Check if the Game is LAA Patched and fill in the Label properly
            string curDirSoul = Directory.GetFiles(currentDir, currentGameEXE)[0];
            string curDirGraph = Directory.GetFiles(currentDir, "GraphicsConfig.exe")[0];
            if (!IsFileLocked(curDirSoul) && !IsFileLocked(curDirGraph))
            {
                if ((isGameEXELAAPatched && isGraphicsConfigLAAPatched) || (!isGameEXELAAPatched && !isGraphicsConfigLAAPatched))
                {
                    isGameEXELAAPatched = toggleLAA(curDirSoul);
                    isGraphicsConfigLAAPatched = toggleLAA(curDirGraph);
                }
                else if (!isGameEXELAAPatched)
                {
                    isGameEXELAAPatched = toggleLAA(curDirSoul);
                }
                else if (!isGraphicsConfigLAAPatched)
                {
                    isGraphicsConfigLAAPatched = toggleLAA(curDirGraph);
                }

                setSoulstormLAALabelText();
                setGraphicsConfigLAALabelText();
            }

        }

        /// <summary>
        /// This function scans for either the Soulstorm or the Dark Crusade version of the game.
        /// </summary>
        private string getCurrentEXE()
        {
            string[] curDir;
            curDir = Directory.GetFiles(currentDir, gameExe.Soulstorm);

            if (curDir.Length != 0)
            {
                return gameExe.Soulstorm;
            }

            curDir = Directory.GetFiles(currentDir, gameExe.DarkCrusade);
            if (curDir.Length != 0)
            {
                return gameExe.DarkCrusade;
            }
            MessageBox.Show("ERROR! Neither found the Soulstorm.exe nor the DarkCrusade.exe in this directory!!");
            Application.Exit();
            return "";
        }

    }
}
