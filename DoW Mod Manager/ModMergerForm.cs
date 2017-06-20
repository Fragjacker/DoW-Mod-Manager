﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DoW_Mod_Manager
{
    public partial class ModMergerForm : Form
    {
        private ModManagerForm _ModManager;
        private object _lastItem = null;
        private int _lastPosition = 0;
        //TODO: Uncomment the block below and remove fixed path!
        //private string _currentDir = Directory.GetCurrentDirectory();
        private string _currentDir = "D:\\THQ\\Dawn of War - Soulstorm";


        private List<Mod> _Modlist = new List<Mod>();
        private bool _hasNoActiveMods;
        private bool _hasNoInActiveMods;



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
            _ModManager = Form;
            getLoadableMods();


            //Disable All buttons at first
            disableArrowDownButton();
            disableArrowUpButton();
            disableCheckmarkButton();
            disableCrossButton();
            disableMinusButton();
            disablePlusButton();

            //Disable the simpler textboxes
            UsedModsList.Enabled = false;
            AvailableModsList.Enabled = false;
            //deleteButton.Enabled = false;
        }


        /// <summary>
        /// Gets all available Mods from the Mod Manager Panel
        /// </summary>
        private void getAvailableMods()
        {
            AvailableModsList.Items.Clear();
            AvailableModsList.Items.AddRange(_ModManager.allFoundModules.ToArray());
        }

        /// <summary>
        /// Gets all of the loadable Mods for the Dropdown list
        /// </summary>
        private void getLoadableMods()
        {
            loadedModBox.Items.Clear();
            String[] modsList = new String[_ModManager.allValidModules.Count];
            int counter = 0;
            foreach (var listBoxItem in _ModManager.allValidModules)
            {
                modsList[counter] = listBoxItem.ToString();
                counter++;
            }
            loadedModBox.Items.AddRange(modsList);
        }


        private void hideOrReinsertLastSelectedMod()
        {
            if (_lastItem != null && !AvailableModsList.Items.Contains(_lastItem))
            {
                AvailableModsList.Items.Insert(_lastPosition, _lastItem);
            }

            _lastItem = loadedModBox.SelectedItem; //Stores the last selected item in order to reinsert it once the selection changes again.
            _lastPosition = AvailableModsList.Items.IndexOf(_lastItem);

            AvailableModsList.Items.Remove(loadedModBox.SelectedItem);
        }

        private void drawAllRequiredModsFromList()
        {
            UsedModsList.Items.Clear();

            sortInactiveModsToBottom();

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
            hideOrReinsertLastSelectedMod();

            foreach (var item in _Modlist)
            {
                if (AvailableModsList.Items.Contains(item.Name))
                {
                    AvailableModsList.Items.Remove(item.Name);
                }
            }
        }

        private void sortInactiveModsToBottom()
        {
            Mod item = null;
            List<Mod> _inactiveModsList = new List<Mod>();

            for (int i = 0; i < _Modlist.Count; i++)
            {
                if (_Modlist[i].State.Equals("Inactive"))
                {
                    item = _Modlist[i];
                    _inactiveModsList.Add(item);
                    _Modlist.RemoveAt(i);
                    i--; //Go one step Back to stay in place for enxt entry
                }
            }
            _Modlist.AddRange(_inactiveModsList);
        }

        /// <summary>
        /// Fills the Used Mods Listbox with the Mods that are currently set as RequiredMod inside the .module file
        /// </summary>
        private void getActiveModsFromFile()
        {

            int index = loadedModBox.SelectedIndex;
            string currentPath = _ModManager._filePaths[index];
            string line = "";

            //Clear all to repopulate everything again
            _Modlist.Clear();

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
            string patCommented1 = @"^[;]+";
            string patCommented2 = @"^[\/]+";
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

        private void enablePlusButton()
        {
            buttonAdd.Enabled = true;
            string str_Path = Path.GetFullPath(_currentDir + "\\DoW Mod Manager Resources\\plus.png");
            buttonAdd.BackgroundImage = Image.FromFile(str_Path);
        }

        private void disablePlusButton()
        {
            buttonAdd.Enabled = false;
            string str_Path = Path.GetFullPath(_currentDir + "\\DoW Mod Manager Resources\\plus_disabled.png");
            buttonAdd.BackgroundImage = Image.FromFile(str_Path);
        }

        private void enableMinusButton()
        {
            buttonRemove.Enabled = true;
            string str_Path = Path.GetFullPath(_currentDir + "\\DoW Mod Manager Resources\\minus.png");
            buttonRemove.BackgroundImage = Image.FromFile(str_Path);
        }

        private void disableMinusButton()
        {
            buttonRemove.Enabled = false;
            string str_Path = Path.GetFullPath(_currentDir + "\\DoW Mod Manager Resources\\minus_disabled.png");
            buttonRemove.BackgroundImage = Image.FromFile(str_Path);
        }

        private void enableCheckmarkButton()
        {
            buttonActivate.Enabled = true;
            string str_Path = Path.GetFullPath(_currentDir + "\\DoW Mod Manager Resources\\checkmark.png");
            buttonActivate.BackgroundImage = Image.FromFile(str_Path);
        }

        private void disableCheckmarkButton()
        {
            buttonActivate.Enabled = false;
            string str_Path = Path.GetFullPath(_currentDir + "\\DoW Mod Manager Resources\\checkmark_disabled.png");
            buttonActivate.BackgroundImage = Image.FromFile(str_Path);
        }

        private void enableCrossButton()
        {
            buttonDeactivate.Enabled = true;
            string str_Path = Path.GetFullPath(_currentDir + "\\DoW Mod Manager Resources\\cross.png");
            buttonDeactivate.BackgroundImage = Image.FromFile(str_Path);
        }

        private void disableCrossButton()
        {
            buttonDeactivate.Enabled = false;
            string str_Path = Path.GetFullPath(_currentDir + "\\DoW Mod Manager Resources\\cross_disabled.png");
            buttonDeactivate.BackgroundImage = Image.FromFile(str_Path);
        }

        private void enableArrowUpButton()
        {
            buttonArrowUp.Enabled = true;
            string str_Path = Path.GetFullPath(_currentDir + "\\DoW Mod Manager Resources\\arrow_up.png");
            buttonArrowUp.BackgroundImage = Image.FromFile(str_Path);
        }

        private void disableArrowUpButton()
        {
            buttonArrowUp.Enabled = false;
            string str_Path = Path.GetFullPath(_currentDir + "\\DoW Mod Manager Resources\\arrow_up_disabled.png");
            buttonArrowUp.BackgroundImage = Image.FromFile(str_Path);
        }

        private void enableArrowDownButton()
        {
            buttonArrowDown.Enabled = true;
            string str_Path = Path.GetFullPath(_currentDir + "\\DoW Mod Manager Resources\\arrow_down.png");
            buttonArrowDown.BackgroundImage = Image.FromFile(str_Path);
        }

        private void disableArrowDownButton()
        {
            buttonArrowDown.Enabled = false;
            string str_Path = Path.GetFullPath(_currentDir + "\\DoW Mod Manager Resources\\arrow_down_disabled.png");
            buttonArrowDown.BackgroundImage = Image.FromFile(str_Path);
        }


        private void buttonArrowUp_Click(object sender, EventArgs e)
        {
            BottomUpSwapRequiredMod();
        }

        private void buttonArrowDown_Click(object sender, EventArgs e)
        {
            TopDownSwapRequiredMod();
        }

        private void buttonSaveFile_Click(object sender, EventArgs e)
        {
            writeModLoadoutToFile();
        }

        private void buttonActivate_Click(object sender, EventArgs e)
        {
            setModtoActive();
        }

        private void buttonDeactivate_Click(object sender, EventArgs e)
        {
            setModtoInactive();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            addAvailableMod();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            removeUsedMod();
        }


        private void removeUsedMod()
        {
            int lastSelectedIndex = 0;
            //Get the new addable Mod candidate
            int delMod = UsedModsList.SelectedIndex;

            if (delMod != -1)
            {
                //Add the Mod to the selection of used Mods
                _Modlist.RemoveAt(delMod);
                lastSelectedIndex = UsedModsList.SelectedIndex;
            }

            //Redraw the List to display the added candidate
            drawAllRequiredModsFromList();

            //Reselect Elements at the last place
            if (lastSelectedIndex < UsedModsList.Items.Count)
            {
                UsedModsList.SelectedIndex = lastSelectedIndex;
            }
            else if (lastSelectedIndex == UsedModsList.Items.Count && UsedModsList.Items.Count > 0)
            {
                UsedModsList.SelectedIndex = lastSelectedIndex - 1;
            }
            else if (lastSelectedIndex == 0 && UsedModsList.Items.Count == 0)
            {
                disableMinusButton();
                disableCheckmarkButton();
                disableArrowUpButton();
                disableArrowDownButton();
                disableCrossButton();
            }
        }

        private void addAvailableMod()
        {
            int lastSelectedIndex = 0;
            if (AvailableModsList.SelectedItem != null)
            {
                //Get the new addable Mod candidate
                string newMod = AvailableModsList.SelectedItem.ToString();
                lastSelectedIndex = AvailableModsList.SelectedIndex;

                //Add the Mod to the selection of used Mods
                _Modlist.Add(new Mod(newMod, "Active"));
            }

            //TODO: Make new Mods be pending again on beeing Added
            //_Modlist.Add(new Mod(newMod, "Pending"));

            //Redraw the List to display the added candidate
            drawAllRequiredModsFromList();

            //Reselect Elements at the last place
            if (lastSelectedIndex < AvailableModsList.Items.Count)
            {
                AvailableModsList.SelectedIndex = lastSelectedIndex;
            }
            else if (lastSelectedIndex == AvailableModsList.Items.Count && AvailableModsList.Items.Count > 0)
            {
                AvailableModsList.SelectedIndex = lastSelectedIndex - 1;
            }
            else if(lastSelectedIndex == 0 && AvailableModsList.Items.Count == 0)
            {
                disablePlusButton();
            }
        }

        private void setModtoActive()
        {
            int lastSelectedIndex = 0;
            //Get the currently selected element from the Used Mods List
            int selection = UsedModsList.SelectedIndex;

            if (selection != -1)
            {
                //Toggle it to be active
                //_Modlist[selection].State = "Active";
                setModState(selection, "Active");
                lastSelectedIndex = UsedModsList.SelectedIndex;
            }
            else if(selection == -1)
            {
                lastSelectedIndex = 0;
                UsedModsList.SelectedIndex = lastSelectedIndex;
            }
            //Redraw the List of Items
            drawAllRequiredModsFromList();

            //Reselect Elements at the last place
            if (ModlistContainsNoActiveMods())
            {
                UsedModsList.SelectedIndex = lastSelectedIndex;
                enableCrossButton();
            }
            if (ModlistContainsNoInActiveMods())
            {
                UsedModsList.SelectedIndex = lastSelectedIndex;
                disableCheckmarkButton();
            }
            if (lastSelectedIndex == (UsedModsList.Items.Count - 1) && _Modlist[lastSelectedIndex].State == "Active")
            {
                UsedModsList.SelectedIndex = lastSelectedIndex;
            }
            if (lastSelectedIndex == (UsedModsList.Items.Count - 1) && _Modlist[lastSelectedIndex].State == "Inactive")
            {
                UsedModsList.SelectedIndex = lastSelectedIndex;
            }
            else if (lastSelectedIndex < (UsedModsList.Items.Count - 1) && _Modlist[lastSelectedIndex].State != "Active")
            {
                UsedModsList.SelectedIndex = lastSelectedIndex + 1;
            }
            else if (lastSelectedIndex < (UsedModsList.Items.Count - 1) && _Modlist[lastSelectedIndex].State == "Active")
            {
                UsedModsList.SelectedIndex = lastSelectedIndex + 1;
            }
        }

        private bool ModlistContainsNoInActiveMods()
        {
            _hasNoInActiveMods = true;
            foreach (var item in _Modlist)
            {
                if (item.State.Equals("Inactive"))
                {
                    _hasNoInActiveMods = false;
                    break;
                }
            }
            return _hasNoInActiveMods;
        }

        private void setModtoInactive()
        {
            int lastSelectedIndex = 0;
            //Get the currently selected element from the Used Mods List
            int selection = UsedModsList.SelectedIndex;

            if (selection != -1)
            {
                //Store last position
                lastSelectedIndex = UsedModsList.SelectedIndex;

                //Toggle it to be active
                //_Modlist[selection].State = "Inactive";
                if (selection == 0 && _Modlist[1].State.Equals("Inactive"))
                {
                    setModState(selection, "Active");
                    disableCrossButton();
                }
                else
                {
                    setModState(selection, "Inactive");
                }
            }
            else if(selection == 1)
            {
                lastSelectedIndex = 0;
                UsedModsList.SelectedIndex = lastSelectedIndex;
            }
            //Redraw the List of Items
            drawAllRequiredModsFromList();

            //Reselect Elements at the last place
            if (ModlistContainsNoActiveMods())
            {
                UsedModsList.SelectedIndex = lastSelectedIndex;
                disableCrossButton();
            }
            if (ModlistContainsNoInActiveMods())
            {
                UsedModsList.SelectedIndex = lastSelectedIndex;
                //enableCheckmarkButton();
            }
            if (lastSelectedIndex == (UsedModsList.Items.Count - 1) && _Modlist[lastSelectedIndex].State == "Inactive")
            {
                UsedModsList.SelectedIndex = lastSelectedIndex - 1;
            }
            if (lastSelectedIndex == 0 && _Modlist[lastSelectedIndex + 1].State == "Inactive")
            {
                UsedModsList.SelectedIndex = lastSelectedIndex;
            }
            else if (lastSelectedIndex < (UsedModsList.Items.Count - 1) && _Modlist[lastSelectedIndex].State != "Inactive")
            {
                UsedModsList.SelectedIndex = lastSelectedIndex;
            }
            else if (lastSelectedIndex < (UsedModsList.Items.Count - 1) && _Modlist[lastSelectedIndex].State == "Inactive")
            {
                UsedModsList.SelectedIndex = lastSelectedIndex - 1;
            }
        }

        private bool ModlistContainsNoActiveMods()
        {
            _hasNoActiveMods = true;
            foreach (var item in _Modlist)
            {
                if(item.State.Equals("Active"))
                {
                    _hasNoActiveMods = false;
                    break;
                }
            }
            return _hasNoActiveMods;
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

        /// <summary>
        /// Swaps two Used Mods List Elements from Bottom to Top.
        /// </summary>
        private void BottomUpSwapRequiredMod()
        {
            Mod topItem, bottomItem;
            int topPos, bottomPos;

            //Copy Old Entries that will be Swapped
            topPos = UsedModsList.SelectedIndex - 1;
            bottomPos = UsedModsList.SelectedIndex;

            if (topPos >= 0)
            {
                topItem = _Modlist[topPos];
                bottomItem = _Modlist[bottomPos];

                //Swap the Items
                _Modlist[bottomPos] = topItem;
                _Modlist[topPos] = bottomItem;

                //Redraw the list
                drawAllRequiredModsFromList();

                //Reselect the newly placed item to allow for quick traverse through the list
                UsedModsList.SelectedIndex = topPos;
            }
        }

        /// <summary>
        /// Swaps two Used Mods List Elements from Top to Bottom.
        /// </summary>
        private void TopDownSwapRequiredMod()
        {
            Mod topItem, bottomItem;
            int topPos, bottomPos;

            if (UsedModsList.SelectedIndex != -1)
            {
                //Copy Old Entries that will be Swapped
                topPos = UsedModsList.SelectedIndex;
                bottomPos = UsedModsList.SelectedIndex + 1;

                if (bottomPos <= _Modlist.Count - 1)
                {
                    topItem = _Modlist[topPos];
                    bottomItem = _Modlist[bottomPos];

                    //Swap the Items
                    _Modlist[bottomPos] = topItem;
                    _Modlist[topPos] = bottomItem;

                    //Redraw the list
                    drawAllRequiredModsFromList();

                    //Reselect the newly placed item to allow for quick traverse through the list
                    UsedModsList.SelectedIndex = bottomPos;
                }
            }
        }

        /// <summary>
        /// writes the current Mod loadout into a .module file.
        /// </summary>
        private void writeModLoadoutToFile()
        {

            //SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            //saveFileDialog1.Filter = "DoW Mod Module file|*.module";
            //saveFileDialog1.Title = "Save your Mod Loadout";
            //saveFileDialog1.FileName = loadedModBox.SelectedItem.ToString();//Gets the the Text of the current loaded Mod for the save Dialog
            //saveFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();

            string filePath = _ModManager._filePaths[loadedModBox.SelectedIndex];

            string modString = "";


            //if (saveFileDialog1.ShowDialog() == DialogResult.OK)

            //{

            System.IO.StreamReader reader = new System.IO.StreamReader(filePath);
            List<string> writer = readFileTilRequiredMod(reader);

            //Write more info into the list of text
            for (int i = 0; i < _Modlist.Count; i++)

            {
                if (_Modlist[i].State.Equals("Active"))
                {
                    modString = "RequiredMod." + (i + 1) + " = " + _Modlist[i].Name;
                    writer.Add(modString);
                }
                if (_Modlist[i].State.Equals("Inactive"))
                {
                    modString = "//RequiredMod." + (i + 1) + " = " + _Modlist[i].Name;
                    writer.Add(modString);
                }

            }

            //Finally write the stuff
            int index = loadedModBox.SelectedIndex;
            string currentPath = _ModManager._filePaths[index];
            StreamWriter newFile = new StreamWriter(filePath);

            foreach (var line in writer)
            {
                newFile.WriteLine(line);
            }

            newFile.Dispose();

            newFile.Close();

            //Update the Main Mod Manager List with possible new entries
            _ModManager.setUpAllNecessaryMods();
            //Update the Dropdown list with the new entries
            getLoadableMods();
            loadedModBox.SelectedItem = _lastItem;
            //}
            //Show a Succesprompt
            MessageBox.Show("Module file changes were successfully applied!","Saving successful");
        }



        /// <summary>
        /// Reads the .module file that shall be saved and overrides it's Required Mods entries.
        /// </summary>
        /// <param name="reader"></param>
        private List<string> readFileTilRequiredMod(StreamReader reader)
        {
            List<string> cacheList = new List<string>();
            int index = loadedModBox.SelectedIndex;
            //string currentPath = ModManager._filePaths[index];
            string line = "";

            // Displays a SaveFileDialog so the user can save the .module File

            // Populate the Required Mods List with entries from the .module file

            while ((line = reader.ReadLine()) != null && !line.Contains("RequiredMod"))
            {
                cacheList.Add(line);
                //file.WriteLine(line);
            }
            reader.Close();
            return cacheList;
        }

        /// <summary>
        /// Deletes an Item from the available Mods List as soon as you select it in the Dropdown List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadedModBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            getActiveModsFromFile();
            ModlistContainsNoActiveMods();
            ModlistContainsNoInActiveMods();

            if (_Modlist.Count != 0)
            {
                drawAllRequiredModsFromList();
                buttonSaveFile.Enabled = true;

                //enable UI Elements
                UsedModsList.Enabled = true;
                AvailableModsList.Enabled = true;
                //deleteButton.Enabled = true;
            }
            else
            {
                UsedModsList.Items.Clear();
                getAvailableMods();
                hideOrReinsertLastSelectedMod();
            }
        }


        private void UsedModsList_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (UsedModsList.SelectedItem != null)
            {
                disablePlusButton();

                enableMinusButton();
                enableArrowUpButton();
                enableArrowDownButton();

                //This activates/deactivates that Buttons to toggle Mods active/inactive depending if there's only Active or only Inactive Mods Left
                checkForProperButtonActivation();

            }
        }


        private void AvailableModsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AvailableModsList.SelectedItem != null)
            {
                enablePlusButton();

                disableMinusButton();
                disableCheckmarkButton();
                disableArrowUpButton();
                disableArrowDownButton();
                disableCrossButton();
            }
        }


        /// <summary>
        /// THis function handles the Activation and Deactivation of the Mod Merger buttons when the user or the program changes the selected Item of the used mods list.
        /// </summary>
        private void checkForProperButtonActivation()
        {
            if (_Modlist[UsedModsList.SelectedIndex].State.Equals("Inactive"))
            {
                enableCheckmarkButton();
                disableCrossButton();
            }
            if (_Modlist.Count > 1)
            {
                if (UsedModsList.SelectedIndex == 0 && _Modlist[UsedModsList.SelectedIndex + 1].State.Equals("Inactive") && _Modlist[UsedModsList.SelectedIndex].State.Equals("Active"))
                {
                    disableCheckmarkButton();
                    disableCrossButton();
                    disableMinusButton();
                }
                else if (_Modlist[UsedModsList.SelectedIndex].State.Equals("Active"))
                {
                    disableCheckmarkButton();
                    enableCrossButton();
                }
            }
            else if (_Modlist.Count == 1)
            {
                if (_Modlist[UsedModsList.SelectedIndex].State.Equals("Active"))
                {
                    disableCheckmarkButton();
                    disableCrossButton();
                    disableMinusButton();
                }
            }
        }


        ///// <summary>
        ///// This will delete the currently inside the dropdownlist selected Mod's module file from both the List and the System.
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void deleteButton_Click(object sender, EventArgs e)
        //{
        //    if (loadedModBox.Items.Count > 1)
        //    {
        //        deleteButton.Enabled = true;
        //        //Show a Messagebox for confirmation
        //        DialogResult result1 = MessageBox.Show("Do you really want to delete the " + loadedModBox.SelectedItem.ToString() + ".module file? This will NOT uninstall the Mod but you won't be able to start the Mod anymore. This cannot be undone unless you made a backup of this file.", "Delete File", MessageBoxButtons.YesNo);

        //        if (result1 == DialogResult.Yes)
        //        {
        //            File.Delete(ModManager._filePaths[loadedModBox.SelectedIndex]);
        //            loadedModBox.SelectedIndex = loadedModBox.SelectedIndex - 1;

        //            //If the last item from the list was removed
        //            if (loadedModBox.Items.Count == 0)
        //            {
        //                deleteButton.Enabled = false;
        //            }

        //            //Update the Main Mod Manager List with possible new entries
        //            ModManager.setUpAllNecessaryMods();
        //            //Update the Dropdown list with the new entries
        //            getLoadableMods();
        //            getAvailableMods();
        //            loadedModBox.SelectedItem = _lastItem;
        //        }
        //    }
        //    else
        //    {
        //        deleteButton.Enabled = false;
        //    }
        //}
    }
}
