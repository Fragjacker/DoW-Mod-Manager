using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DoW_Mod_Manager
{
    public partial class ModMergerForm : Form
    {
        private ModManagerForm ModManager;
        private object _lastItem = null;
        private int _lastPosition = 0;

        private List<Mod> _Modlist = new List<Mod>();


        /// <summary>
        /// The class Mod contains the Name and the current State of the Mod beeing "Active" "Inactive" or "Pending"
        /// </summary>
        public class Mod
        {
            public String Name, State;

            /// <summary>
            /// Creates and instace of the class Mod and initializes it's values Name and State
            /// </summary>
            /// <param name="NameString"></param>
            /// <param name="StateString"></param>
            public Mod(String NameString, String StateString)
            {
                Name = NameString;
                State = StateString;
            }
        }

        /// <summary>
        /// Creates the Form of the Mod Merger WIndows
        /// </summary>
        /// <param name="Form"></param>
        public ModMergerForm(ModManagerForm Form)
        {
            InitializeComponent();
            ModManager = Form;
            getAvailableMods();
            getLoadableMods();
        }


        /// <summary>
        /// Gets all available Mods from the Mod Manager Panel
        /// </summary>
        private void getAvailableMods()
        {
            AvailableModsList.Items.Clear();
            AvailableModsList.Items.AddRange(ModManager.InstalledModsList.Items);
        }

        /// <summary>
        /// Gets all of the loadable Mods for the Dropdown list
        /// </summary>
        private void getLoadableMods()
        {
            String[] modsList = new String[ModManager.InstalledModsList.Items.Count];
            int counter = 0;
            foreach (var listBoxItem in ModManager.InstalledModsList.Items)
            {
                modsList[counter] = listBoxItem.ToString();
                counter++;
            }
            loadedModBox.Items.AddRange(modsList);
        }

        /// <summary>
        /// Deletes an Item from the available Mods List as soon as you select it in the Dropdown List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadedModBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_lastItem != null && !AvailableModsList.Items.Contains(_lastItem))
            {
                AvailableModsList.Items.Insert(_lastPosition, _lastItem);
            }

            _lastItem = loadedModBox.SelectedItem;
            _lastPosition = AvailableModsList.Items.IndexOf(_lastItem);

            AvailableModsList.Items.Remove(loadedModBox.SelectedItem);

            getActiveModsFromFile();
            if (_Modlist.Count != 0)
            {
                drawAllRequiredModsFromList();
            }
        }

        private void drawAllRequiredModsFromList()
        {
            string entry = "";
            foreach (var item in _Modlist)
            {
                entry = item.Name + " ..." + item.State;
                UsedModsList.Items.Add(entry);
            }
            hideUnavailableMods();
        }

        private void hideUnavailableMods()
        {
            getAvailableMods(); //Get a Fresh new List everytime
            foreach (var item in _Modlist)
            {
                if (AvailableModsList.Items.Contains(item.Name))
                {
                    AvailableModsList.Items.Remove(item.Name);
                }
            }
        }

        /// <summary>
        /// Fills the Used Mods Listbox with the Mods that are currently set as RequiredMod inside the .module file
        /// </summary>
        private void getActiveModsFromFile()
        {

            int index = loadedModBox.SelectedIndex;
            string currentPath = ModManager._filePaths[index];
            string line = "";

            //Clear all to repopulate everything again
            _Modlist.Clear();
            UsedModsList.Items.Clear();

            // Read the file and display it line by line.
            System.IO.StreamReader file = new System.IO.StreamReader(currentPath);

            // Populate the Required Mods List with entries from the .module file

            while ((line = file.ReadLine()) != null)
            {
                if (checkForRequiredMods(line) == true)
                {
                    _Modlist.Add(new Mod(getNameOfRequiredMod(line), regexGetStateOfRequiredMod(line)));
                }
            }

            //getModFoldersFromFile();
            //checkforInstalledMods();
            file.Close();
        }

        /// <summary>
        /// Returns True if there was found a List of required Mods
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private bool checkForRequiredMods(string input)
        {
            string text = input;
            string pat = @"\bRequiredMod\b";
            bool matchresult = false;

            // Instantiate the regular expression object.
            Regex require = new Regex(pat, RegexOptions.IgnoreCase);

            // Match the regular expression pattern against a text string.

            Match m = require.Match(text);

            foreach (Match match in Regex.Matches(input, pat))
            {
                matchresult = true;
            }

            return matchresult;
        }

        /// <summary>
        /// Returns the State of a required Mod beeing "Active" or "Inactive".
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string regexGetStateOfRequiredMod(string input)
        {
            string text = input;
            string pat = @"\bRequiredMod\b";
            string patCommented1 = @";;";
            string patCommented2 = @"//";
            string state = "";

            // Instantiate the regular expression object.
            Regex require = new Regex(pat, RegexOptions.IgnoreCase);
            Regex notrequire = new Regex(patCommented1, RegexOptions.IgnoreCase);

            // Match the regular expression pattern against a text string.

            Match m = require.Match(text);

            foreach (Match match in Regex.Matches(input, pat))
            {
                state = "Active";
            }

            foreach (Match match in Regex.Matches(input, patCommented1))
            {
                state = "Inactive";
            }

            foreach (Match match in Regex.Matches(input, patCommented2))
            {
                state = "Inactive";
            }

            return state;
        }

        /// <summary>
        /// This method returns the last word from an inputstring by using regex. For example using "RequiredMod.1 = Yourmod" will result in "Yourmod" beeing returned
        /// </summary>
        /// <param name="inputstring"></param>
        /// <returns>string</returns>
        private string getNameOfRequiredMod(string inputstring)
        {

            string text = inputstring;
            string pat = @"\S*\s*$";
            string result = "";

            // Instantiate the regular expression object.
            Regex require = new Regex(pat, RegexOptions.IgnoreCase);

            // Match the regular expression pattern against a text string.

            Match m = require.Match(text);

            foreach (Match match in Regex.Matches(inputstring, pat))
            {
                result = m.Value.Replace(" ", "");
                // result = m.Value;
            }
            return result;
        }

        /// <summary>
        /// Draws the Items inside the Used Mods Listbox in a specified Color
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UsedModsList_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
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
            //This switch accesses the Mod Struct and gets the current State of the selected Mod
            switch (_Modlist[e.Index].State)
            {
                case "Active":
                    myBrush = Brushes.Green;
                    break;

                case "Inactive":
                    myBrush = Brushes.Red;
                    break;

                case "Pending":
                    myBrush = Brushes.Orange;
                    break;

                default:
                    myBrush = Brushes.Black;
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
        /// Sets that State of the current Mod beeing namely: "Active" "Inactive" "Pending"-For yet to be determined file clashes. The parameter determines which Mod gets its state changed
        /// </summary>
        /// <param name="count"></param>
        /// <param name="state"></param>
        void setModState(int count, string state)
        {
            _Modlist[count].State = state;
        }

        /// <summary>
        /// Gets the current state of the currently selected Mod
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        string getModState(int count)
        {
            return _Modlist[count].State;
        }
    }
}
