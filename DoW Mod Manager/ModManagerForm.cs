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
        private bool[] _isInstalled; //A boolean array that maps Index-wise to the filepaths indices. Index 0 checks if required mod at index 0 in the _filepaths is installed or not.

        /// <summary>
        ///  Initializes all the necessary components used by the GUI
        /// </summary>
        public ModManagerForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Performs actions when the Application is started via the .exe file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            currentDir = Directory.GetCurrentDirectory();
            _filePaths = Directory.GetFiles(currentDir, "Soulstorm.exe");


            //Check if there was a valid Directory detected previously, then perform getting all the info to populate the lists
            if (_filePaths.Length != 0)
            {
                textBox1.AppendText(currentDir);

                getMods();

                getModFoldersFromFile();
                InstalledModsList.SelectedIndex = 0; //Set default selection to index 0 in order to avoid crashes
            }

            //TODO: Uncommoment below block and comment try and catch block again!

            //else
            //{
            //    MessageBox.Show("ERROR finding Soulstorm on your Computer! Please put the DoW Mod Manager 1.3.exe in the directory that contains the Soulstorm.exe!");
            //    Application.Exit();
            //}

            // This was implemented to find soulstorm by using the Registry Key Install location. But since the resource folder must be placed in a certain direction i've decided that a local directory scan would suffice.
            // Uncomment this to make the Form Window open up. Since the program will exit if there's no local Soulstorm.exe file be found.
            else
            {
                try
                {
                    RegistryKey regKey = Registry.LocalMachine;
                    regKey = regKey.OpenSubKey(@"Software\\THQ\\Dawn of War - Soulstorm\\");

                    if (regKey != null)
                    {
                        currentDir = regKey.GetValue("InstallLocation").ToString();

                        textBox1.AppendText(currentDir);

                        getMods();

                        //getModFoldersFromFile();
                        InstalledModsList.SelectedIndex = 0; //Set default selection to index 0 in order to avoid crashes
                    }

                }
                catch (Exception eventos)
                {
                    throw new FileNotFoundException("ERROR finding Soulstorm on your Computer! If you're using the Disc version please place the .exe in the root directory! Else reinstall on STEAM!", eventos);
                }
            }
        }

        /// <summary>
        /// Checks if the Mods are actually REALLY installed by checking if their asset folders are present by the name specified within the .module files "Modfolder" tagline
        /// </summary>
        public void checkforInstalledMods()
        {
            startButton1.Enabled = true;

            string str_Path = Path.GetFullPath(currentDir + "\\DoW Mod Manager Resources\\Checkmark.png");
            pictureBox1.Image = Image.FromFile(str_Path);

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
            InstalledModsList.Items.Clear();
            string line = "";

            _filePaths = Directory.GetFiles(currentDir, "*.module");

            if (_filePaths.Length != 0)
            {
                foreach (string s in _filePaths)
                {

                    // Read the .module file to see if the mod is playable
                    System.IO.StreamReader file = new System.IO.StreamReader(s);

                    // Filter the unplayable mods and populate the List only with playable mods

                    while ((line = file.ReadLine()) != null)
                    {
                        if (modIsPlayable(line) == true)
                        {
                            InstalledModsList.Items.Add(Path.GetFileNameWithoutExtension(s));
                        }
                    }
                    file.Close();
                }
            }
            else
            {
                MessageBox.Show("No mods were found in the specified directory! Please check your current directory again!");
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
            startInfo.FileName = currentDir + "\\Soulstorm.exe";
            startInfo.Arguments = @"-modname " + InstalledModsList.SelectedItem + _devMode + _noIntroMode + _highPolyMode;
            Process.Start(startInfo);
        }

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
        }

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
        }

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
        }

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

        private void button1_Click(object sender, EventArgs e)
        {
            ModMergerForm mergerWindow = new ModMergerForm(this);
            mergerWindow.Show();
        }
    }
}
