using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DoW_Mod_Manager
{
    public partial class ModMergerForm : Form
    {
        private readonly ModManagerForm modManager;
        private object lastItem = null;
        private int lastPosition = 0;
        private readonly string currentDir = Directory.GetCurrentDirectory();
        private readonly List<Mod> modlist = new List<Mod>();
        private bool hasNoActiveMods;
        private bool hasNoInActiveMods;

        /// <summary>
        /// The class Mod contains the Name and the current State of the Mod beeing "Active" "Inactive" or "Pending"
        /// </summary>
        public class Mod
        {
            public string Name, State;

            /// <summary>
            /// Creates and instace of the class Mod and initializes it's values Name and State
            /// </summary>
            /// <param name="NameString"></param>
            /// <param name="StateString"></param>
            public Mod(string NameString, string StateString)
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
            modManager = Form;
            getLoadableMods();

            //Disable All buttons at first
            DisableArrowDownButton();
            DisableArrowUpButton();
            DisableCheckmarkButton();
            DisableCrossButton();
            DisableMinusButton();
            DisablePlusButton();

            //Disable the simpler textboxes
            UsedModsList.Enabled = false;
            AvailableModsList.Enabled = false;
            //deleteButton.Enabled = false;
        }

        /// <summary>
        /// Gets all available Mods from the Mod Manager Panel
        /// </summary>
        private void GetAvailableMods()
        {
            AvailableModsList.Items.Clear();
            AvailableModsList.Items.AddRange(modManager.AllFoundModules.ToArray());
        }

        /// <summary>
        /// Gets all of the loadable Mods for the Dropdown list
        /// </summary>
        private void getLoadableMods()
        {
            loadedModBox.Items.Clear();
            string[] modsList = new string[modManager.AllValidModules.Count];
            int counter = 0;
            foreach (var listBoxItem in modManager.AllValidModules)
            {
                modsList[counter] = listBoxItem.ToString();
                counter++;
            }
            loadedModBox.Items.AddRange(modsList);
        }

        private void HideOrReinsertLastSelectedMod()
        {
            if (lastItem != null && !AvailableModsList.Items.Contains(lastItem))
            {
                AvailableModsList.Items.Insert(lastPosition, lastItem);
            }

            lastItem = loadedModBox.SelectedItem;       //Stores the last selected item in order to reinsert it once the selection changes again.
            lastPosition = AvailableModsList.Items.IndexOf(lastItem);

            AvailableModsList.Items.Remove(loadedModBox.SelectedItem);
        }

        private void DrawAllRequiredModsFromList()
        {
            UsedModsList.Items.Clear();

            SortInactiveModsToBottom();

            string entry;
            foreach (var item in modlist)
            {
                entry = item.Name + " ..." + item.State;
                UsedModsList.Items.Add(entry);
            }
            HideUnavailableMods();
        }

        private void HideUnavailableMods()
        {
            GetAvailableMods();             //Get a Fresh new List everytime
            HideOrReinsertLastSelectedMod();

            foreach (var item in modlist)
            {
                if (AvailableModsList.Items.Contains(item.Name))
                {
                    AvailableModsList.Items.Remove(item.Name);
                }
            }
        }

        private void SortInactiveModsToBottom()
        {
            Mod item;
            List<Mod> inactiveModsList = new List<Mod>();

            for (int i = 0; i < modlist.Count; i++)
            {
                if (modlist[i].State.Equals("Inactive"))
                {
                    item = modlist[i];
                    inactiveModsList.Add(item);
                    modlist.RemoveAt(i);
                    i--;                //Go one step Back to stay in place for enxt entry
                }
            }
            modlist.AddRange(inactiveModsList);
        }

        /// <summary>
        /// Fills the Used Mods Listbox with the Mods that are currently set as RequiredMod inside the .module file
        /// </summary>
        private void GetActiveModsFromFile()
        {
            int index = loadedModBox.SelectedIndex;
            string currentPath = modManager.FilePaths[index];
            string line;

            //Clear all to repopulate everything again
            modlist.Clear();

            // Read the file and display it line by line.
            StreamReader file = new StreamReader(currentPath);

            // Populate the Required Mods List with entries from the .module file
            while ((line = file.ReadLine()) != null)
            {
                if (CheckForRequiredMods(line) == true)
                {
                    modlist.Add(new Mod(GetNameOfRequiredMod(line), RegexGetStateOfRequiredMod(line)));
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
        private bool CheckForRequiredMods(string input)
        {
            //string text = input;
            string pat = @"\bRequiredMod\b";
            bool matchresult = false;

            // Instantiate the regular expression object.
            //Regex require = new Regex(pat, RegexOptions.IgnoreCase);

            // Match the regular expression pattern against a text string.
            //Match m = require.Match(text);

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
        private string RegexGetStateOfRequiredMod(string input)
        {
            //string text = input;
            string pat = @"\bRequiredMod\b";
            string patCommented1 = @"^[;]+";
            string patCommented2 = @"^[\/]+";
            string state = "";

            // Instantiate the regular expression object.
            //Regex require = new Regex(pat, RegexOptions.IgnoreCase);
            //Regex notrequire = new Regex(patCommented1, RegexOptions.IgnoreCase);

            // Match the regular expression pattern against a text string.
            //Match m = require.Match(text);

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
        private string GetNameOfRequiredMod(string inputstring)
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
            Brush myBrush;
            //This switch accesses the Mod Struct and gets the current State of the selected Mod
            switch (modlist[e.Index].State)
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

        private void EnablePlusButton()
        {
            buttonAdd.Enabled = true;
            string str_Path = Path.GetFullPath(currentDir + "\\DoW Mod Manager Resources\\plus.png");
            buttonAdd.BackgroundImage = Image.FromFile(str_Path);
        }

        private void DisablePlusButton()
        {
            buttonAdd.Enabled = false;
            string str_Path = Path.GetFullPath(currentDir + "\\DoW Mod Manager Resources\\plus_disabled.png");
            buttonAdd.BackgroundImage = Image.FromFile(str_Path);
        }

        private void EnableMinusButton()
        {
            buttonRemove.Enabled = true;
            string str_Path = Path.GetFullPath(currentDir + "\\DoW Mod Manager Resources\\minus.png");
            buttonRemove.BackgroundImage = Image.FromFile(str_Path);
        }

        private void DisableMinusButton()
        {
            buttonRemove.Enabled = false;
            string str_Path = Path.GetFullPath(currentDir + "\\DoW Mod Manager Resources\\minus_disabled.png");
            buttonRemove.BackgroundImage = Image.FromFile(str_Path);
        }

        private void EnableCheckmarkButton()
        {
            buttonActivate.Enabled = true;
            string str_Path = Path.GetFullPath(currentDir + "\\DoW Mod Manager Resources\\checkmark.png");
            buttonActivate.BackgroundImage = Image.FromFile(str_Path);
        }

        private void DisableCheckmarkButton()
        {
            buttonActivate.Enabled = false;
            string str_Path = Path.GetFullPath(currentDir + "\\DoW Mod Manager Resources\\checkmark_disabled.png");
            buttonActivate.BackgroundImage = Image.FromFile(str_Path);
        }

        private void EnableCrossButton()
        {
            buttonDeactivate.Enabled = true;
            string str_Path = Path.GetFullPath(currentDir + "\\DoW Mod Manager Resources\\cross.png");
            buttonDeactivate.BackgroundImage = Image.FromFile(str_Path);
        }

        private void DisableCrossButton()
        {
            buttonDeactivate.Enabled = false;
            string str_Path = Path.GetFullPath(currentDir + "\\DoW Mod Manager Resources\\cross_disabled.png");
            buttonDeactivate.BackgroundImage = Image.FromFile(str_Path);
        }

        private void EnableArrowUpButton()
        {
            buttonArrowUp.Enabled = true;
            string str_Path = Path.GetFullPath(currentDir + "\\DoW Mod Manager Resources\\arrow_up.png");
            buttonArrowUp.BackgroundImage = Image.FromFile(str_Path);
        }

        private void DisableArrowUpButton()
        {
            buttonArrowUp.Enabled = false;
            string str_Path = Path.GetFullPath(currentDir + "\\DoW Mod Manager Resources\\arrow_up_disabled.png");
            buttonArrowUp.BackgroundImage = Image.FromFile(str_Path);
        }

        private void EnableArrowDownButton()
        {
            buttonArrowDown.Enabled = true;
            string str_Path = Path.GetFullPath(currentDir + "\\DoW Mod Manager Resources\\arrow_down.png");
            buttonArrowDown.BackgroundImage = Image.FromFile(str_Path);
        }

        private void DisableArrowDownButton()
        {
            buttonArrowDown.Enabled = false;
            string str_Path = Path.GetFullPath(currentDir + "\\DoW Mod Manager Resources\\arrow_down_disabled.png");
            buttonArrowDown.BackgroundImage = Image.FromFile(str_Path);
        }


        private void ButtonArrowUp_Click(object sender, EventArgs e)
        {
            BottomUpSwapRequiredMod();
        }

        private void ButtonArrowDown_Click(object sender, EventArgs e)
        {
            TopDownSwapRequiredMod();
        }

        private void ButtonSaveFile_Click(object sender, EventArgs e)
        {
            WriteModLoadoutToFile();
        }

        private void ButtonActivate_Click(object sender, EventArgs e)
        {
            SetModtoActive();
        }

        private void ButtonDeactivate_Click(object sender, EventArgs e)
        {
            SetModtoInactive();
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            AddAvailableMod();
        }

        private void ButtonRemove_Click(object sender, EventArgs e)
        {
            RemoveUsedMod();
        }

        private void RemoveUsedMod()
        {
            int lastSelectedIndex = 0;
            //Get the new addable Mod candidate
            int delMod = UsedModsList.SelectedIndex;

            if (delMod != -1)
            {
                //Add the Mod to the selection of used Mods
                modlist.RemoveAt(delMod);
                lastSelectedIndex = UsedModsList.SelectedIndex;
            }

            //Redraw the List to display the added candidate
            DrawAllRequiredModsFromList();

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
                DisableMinusButton();
                DisableCheckmarkButton();
                DisableArrowUpButton();
                DisableArrowDownButton();
                DisableCrossButton();
            }
        }

        private void AddAvailableMod()
        {
            int lastSelectedIndex = 0;
            if (AvailableModsList.SelectedItem != null)
            {
                //Get the new addable Mod candidate
                string newMod = AvailableModsList.SelectedItem.ToString();
                lastSelectedIndex = AvailableModsList.SelectedIndex;

                //Add the Mod to the selection of used Mods
                modlist.Add(new Mod(newMod, "Active"));
            }

            //TODO: Make new Mods be pending again on beeing Added
            //_Modlist.Add(new Mod(newMod, "Pending"));

            //Redraw the List to display the added candidate
            DrawAllRequiredModsFromList();

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
                DisablePlusButton();
            }
        }

        private void SetModtoActive()
        {
            int lastSelectedIndex = 0;
            //Get the currently selected element from the Used Mods List
            int selection = UsedModsList.SelectedIndex;

            if (selection != -1)
            {
                //Toggle it to be active
                //_Modlist[selection].State = "Active";
                SetModState(selection, "Active");
                lastSelectedIndex = UsedModsList.SelectedIndex;
            }
            else if(selection == -1)
            {
                lastSelectedIndex = 0;
                UsedModsList.SelectedIndex = lastSelectedIndex;
            }
            //Redraw the List of Items
            DrawAllRequiredModsFromList();

            //Reselect Elements at the last place
            if (ModlistContainsNoActiveMods())
            {
                UsedModsList.SelectedIndex = lastSelectedIndex;
                EnableCrossButton();
            }
            if (ModlistContainsNoInActiveMods())
            {
                UsedModsList.SelectedIndex = lastSelectedIndex;
                DisableCheckmarkButton();
            }
            if (lastSelectedIndex == (UsedModsList.Items.Count - 1) && modlist[lastSelectedIndex].State == "Active")
            {
                UsedModsList.SelectedIndex = lastSelectedIndex;
            }
            if (lastSelectedIndex == (UsedModsList.Items.Count - 1) && modlist[lastSelectedIndex].State == "Inactive")
            {
                UsedModsList.SelectedIndex = lastSelectedIndex;
            }
            else if (lastSelectedIndex < (UsedModsList.Items.Count - 1) && modlist[lastSelectedIndex].State != "Active")
            {
                UsedModsList.SelectedIndex = lastSelectedIndex + 1;
            }
            else if (lastSelectedIndex < (UsedModsList.Items.Count - 1) && modlist[lastSelectedIndex].State == "Active")
            {
                UsedModsList.SelectedIndex = lastSelectedIndex + 1;
            }
        }

        private bool ModlistContainsNoInActiveMods()
        {
            hasNoInActiveMods = true;
            foreach (var item in modlist)
            {
                if (item.State.Equals("Inactive"))
                {
                    hasNoInActiveMods = false;
                    break;
                }
            }
            return hasNoInActiveMods;
        }

        private void SetModtoInactive()
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
                if (selection == 0 && modlist[1].State.Equals("Inactive"))
                {
                    SetModState(selection, "Active");
                    DisableCrossButton();
                }
                else
                {
                    SetModState(selection, "Inactive");
                }
            }
            else if(selection == 1)
            {
                lastSelectedIndex = 0;
                UsedModsList.SelectedIndex = lastSelectedIndex;
            }
            //Redraw the List of Items
            DrawAllRequiredModsFromList();

            //Reselect Elements at the last place
            if (ModlistContainsNoActiveMods())
            {
                UsedModsList.SelectedIndex = lastSelectedIndex;
                DisableCrossButton();
            }
            if (ModlistContainsNoInActiveMods())
            {
                UsedModsList.SelectedIndex = lastSelectedIndex;
                //enableCheckmarkButton();
            }
            if (lastSelectedIndex == (UsedModsList.Items.Count - 1) && modlist[lastSelectedIndex].State == "Inactive")
            {
                UsedModsList.SelectedIndex = lastSelectedIndex - 1;
            }
            if (lastSelectedIndex == 0 && modlist[lastSelectedIndex + 1].State == "Inactive")
            {
                UsedModsList.SelectedIndex = lastSelectedIndex;
            }
            else if (lastSelectedIndex < (UsedModsList.Items.Count - 1) && modlist[lastSelectedIndex].State != "Inactive")
            {
                UsedModsList.SelectedIndex = lastSelectedIndex;
            }
            else if (lastSelectedIndex < (UsedModsList.Items.Count - 1) && modlist[lastSelectedIndex].State == "Inactive")
            {
                UsedModsList.SelectedIndex = lastSelectedIndex - 1;
            }
        }

        private bool ModlistContainsNoActiveMods()
        {
            hasNoActiveMods = true;
            foreach (var item in modlist)
            {
                if(item.State.Equals("Active"))
                {
                    hasNoActiveMods = false;
                    break;
                }
            }
            return hasNoActiveMods;
        }

        /// <summary>
        /// Sets that State of the current Mod beeing namely: "Active" "Inactive" "Pending"-For yet to be determined file clashes. The parameter determines which Mod gets its state changed
        /// </summary>
        /// <param name="count"></param>
        /// <param name="state"></param>
        void SetModState(int count, string state)
        {
            modlist[count].State = state;
        }

        /// <summary>
        /// Gets the current state of the currently selected Mod
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        //string getModState(int count)
        //{
        //    return modlist[count].State;
        //}

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
                topItem = modlist[topPos];
                bottomItem = modlist[bottomPos];

                //Swap the Items
                modlist[bottomPos] = topItem;
                modlist[topPos] = bottomItem;

                //Redraw the list
                DrawAllRequiredModsFromList();

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

                if (bottomPos <= modlist.Count - 1)
                {
                    topItem = modlist[topPos];
                    bottomItem = modlist[bottomPos];

                    //Swap the Items
                    modlist[bottomPos] = topItem;
                    modlist[topPos] = bottomItem;

                    //Redraw the list
                    DrawAllRequiredModsFromList();

                    //Reselect the newly placed item to allow for quick traverse through the list
                    UsedModsList.SelectedIndex = bottomPos;
                }
            }
        }

        /// <summary>
        /// writes the current Mod loadout into a .module file.
        /// </summary>
        private void WriteModLoadoutToFile()
        {
            //SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            //saveFileDialog1.Filter = "DoW Mod Module file|*.module";
            //saveFileDialog1.Title = "Save your Mod Loadout";
            //saveFileDialog1.FileName = loadedModBox.SelectedItem.ToString();//Gets the the Text of the current loaded Mod for the save Dialog
            //saveFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();

            string filePath = modManager.FilePaths[loadedModBox.SelectedIndex];
            string modString;

            //if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            //{

            StreamReader reader = new StreamReader(filePath);
            List<string> writer = ReadFileTilRequiredMod(reader);

            //Write more info into the list of text
            for (int i = 0; i < modlist.Count; i++)
            {
                if (modlist[i].State.Equals("Active"))
                {
                    modString = "RequiredMod." + (i + 1) + " = " + modlist[i].Name;
                    writer.Add(modString);
                }
                if (modlist[i].State.Equals("Inactive"))
                {
                    modString = "//RequiredMod." + (i + 1) + " = " + modlist[i].Name;
                    writer.Add(modString);
                }
            }

            //Finally write the stuff
            //int index = loadedModBox.SelectedIndex;
            //string currentPath = modManager.FilePaths[index];
            StreamWriter newFile = new StreamWriter(filePath);

            foreach (var line in writer)
            {
                newFile.WriteLine(line);
            }

            newFile.Dispose();
            newFile.Close();

            //Update the Main Mod Manager List with possible new entries
            modManager.SetUpAllNecessaryMods();
            //Update the Dropdown list with the new entries
            getLoadableMods();
            loadedModBox.SelectedItem = lastItem;
            //}
            //Show a Succesprompt
            MessageBox.Show("Module file changes were successfully applied!","Saving successful");
        }

        /// <summary>
        /// Reads the .module file that shall be saved and overrides it's Required Mods entries.
        /// </summary>
        /// <param name="reader"></param>
        private List<string> ReadFileTilRequiredMod(StreamReader reader)
        {
            List<string> cacheList = new List<string>();
            //int index = loadedModBox.SelectedIndex;
            //string currentPath = ModManager._filePaths[index];
            string line;

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
        private void LoadedModBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetActiveModsFromFile();
            ModlistContainsNoActiveMods();
            ModlistContainsNoInActiveMods();

            if (modlist.Count != 0)
            {
                DrawAllRequiredModsFromList();
                buttonSaveFile.Enabled = true;

                //enable UI Elements
                UsedModsList.Enabled = true;
                AvailableModsList.Enabled = true;
                //deleteButton.Enabled = true;
            }
            else
            {
                UsedModsList.Items.Clear();
                GetAvailableMods();
                HideOrReinsertLastSelectedMod();
            }
        }

        private void UsedModsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UsedModsList.SelectedItem != null)
            {
                DisablePlusButton();
                EnableMinusButton();
                EnableArrowUpButton();
                EnableArrowDownButton();

                //This activates/deactivates that Buttons to toggle Mods active/inactive depending if there's only Active or only Inactive Mods Left
                CheckForProperButtonActivation();
            }
        }

        private void AvailableModsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AvailableModsList.SelectedItem != null)
            {
                EnablePlusButton();
                DisableMinusButton();
                DisableCheckmarkButton();
                DisableArrowUpButton();
                DisableArrowDownButton();
                DisableCrossButton();
            }
        }

        /// <summary>
        /// THis function handles the Activation and Deactivation of the Mod Merger buttons when the user or the program changes the selected Item of the used mods list.
        /// </summary>
        private void CheckForProperButtonActivation()
        {
            if (modlist[UsedModsList.SelectedIndex].State.Equals("Inactive"))
            {
                EnableCheckmarkButton();
                DisableCrossButton();
            }
            if (modlist.Count > 1)
            {
                if (UsedModsList.SelectedIndex == 0 && modlist[UsedModsList.SelectedIndex + 1].State.Equals("Inactive") && modlist[UsedModsList.SelectedIndex].State.Equals("Active"))
                {
                    DisableCheckmarkButton();
                    DisableCrossButton();
                    DisableMinusButton();
                }
                else if (modlist[UsedModsList.SelectedIndex].State.Equals("Active"))
                {
                    DisableCheckmarkButton();
                    EnableCrossButton();
                }
            }
            else if (modlist.Count == 1)
            {
                if (modlist[UsedModsList.SelectedIndex].State.Equals("Active"))
                {
                    DisableCheckmarkButton();
                    DisableCrossButton();
                    DisableMinusButton();
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
