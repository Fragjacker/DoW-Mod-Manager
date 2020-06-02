using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DoW_Mod_Manager
{
    public partial class ModMergerForm : Form
    {
        //public enum ModState { Inactive, Active, Pending }

        private const string MOD_ACTIVE = "Active";
        private const string MOD_INACTIVE = "Inactive";
        private const string MOD_PENDING = "Pending";

        public class Mod
        {
            public string Name;
            public string State;

            public Mod(string name, string state)
            {
                Name = name;
                State = state;
            }
        }

        private readonly ModManagerForm modManager;
        private object lastItem = null;
        private int lastDropDownItemIndex = -1;
        private int lastUsedModIndex = -1;
        private int lastAvailableModIndex = -1;

        private readonly List<Mod> modlist = new List<Mod>();
        private bool hasNoActiveMods = false;
        private bool hasNoInActiveMods = false;

        /// <summary>
        /// Creates the Form of the Mod Merger WIndows
        /// </summary>
        /// <param name="Form"></param>
        public ModMergerForm(ModManagerForm Form)
        {
            InitializeComponent();
            modManager = Form;
            GetLoadableMods();

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
        private void GetLoadableMods()
        {
            loadedModBox.Items.Clear();

            string[] modsList = new string[modManager.AllValidModules.Count];
            int counter = 0;
            foreach (string listBoxItem in modManager.AllValidModules)
            {
                modsList[counter] = listBoxItem.ToString();
                counter++;
            }
            loadedModBox.Items.AddRange(modsList);
        }

        private void HideOrReinsertLastSelectedMod()
        {
            if (lastItem != null && !AvailableModsList.Items.Contains(lastItem))
                AvailableModsList.Items.Insert(lastDropDownItemIndex, lastItem);

            lastItem = loadedModBox.SelectedItem;           //Stores the last selected item in order to reinsert it once the selection changes again.
            lastDropDownItemIndex = AvailableModsList.Items.IndexOf(lastItem);

            AvailableModsList.Items.Remove(loadedModBox.SelectedItem);
        }

        private void DrawAllRequiredModsFromList()
        {
            UsedModsList.Items.Clear();
            SortInactiveModsToBottom();

            string entry;
            foreach (Mod item in modlist)
            {
                entry = item.Name + " ..." + item.State;
                UsedModsList.Items.Add(entry);
            }
            HideUnavailableMods();
        }

        private void HideUnavailableMods()
        {
            GetAvailableMods();                             //Get a Fresh new List everytime
            HideOrReinsertLastSelectedMod();

            foreach (Mod item in modlist)
            {
                if (AvailableModsList.Items.Contains(item.Name))
                    AvailableModsList.Items.Remove(item.Name);
            }
        }

        private void SortInactiveModsToBottom()
        {
            List<Mod> inactiveModsList = new List<Mod>();

            for (int i = 0; i < modlist.Count; i++)
            {
                if (modlist[i].State.Equals(MOD_INACTIVE))
                {
                    inactiveModsList.Add(modlist[i]);
                    modlist.RemoveAt(i);
                    i--;                                    //Go one step Back to stay in place for enxt entry
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
            using (StreamReader file = new StreamReader(currentPath))
            {
                // Populate the Required Mods List with entries from the .module file
                while ((line = file.ReadLine()) != null)
                {
                    if (CheckForRequiredMods(line) == true)
                    {
                        modlist.Add(new Mod(GetNameOfRequiredMod(line), GetStateOfRequiredMod(line)));
                    }
                }

                //getModFoldersFromFile();
                //checkforInstalledMods();
            }
        }

        /// <summary>
        /// Returns True if there was found a List of required Mods
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private bool CheckForRequiredMods(string text)
        {
            const string pattern = @"\bRequiredMod\b";
            bool matchresult = false;

            // Instantiate the regular expression object.
            //Regex require = new Regex(pat, RegexOptions.IgnoreCase);

            // Match the regular expression pattern against a text string.
            //Match m = require.Match(text);

            foreach (Match match in Regex.Matches(text, pattern))
            {
                matchresult = true;
            }

            return matchresult;
        }

        /// <summary>
        /// Returns the State of a required Mod beeing "Active" or "Inactive".
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string GetStateOfRequiredMod(string text)
        {
            const string pattern = @"\bRequiredMod\b";
            const string patCommented1 = @"^[;]+";
            const string patCommented2 = @"^[\/]+";
            string state = "";

            // Instantiate the regular expression object.
            //Regex require = new Regex(pat, RegexOptions.IgnoreCase);
            //Regex notrequire = new Regex(patCommented1, RegexOptions.IgnoreCase);

            // Match the regular expression pattern against a text string.
            //Match m = require.Match(text);

            foreach (Match match in Regex.Matches(text, pattern))
            {
                state = MOD_ACTIVE;
            }

            foreach (Match match in Regex.Matches(text, patCommented1))
            {
                state = MOD_INACTIVE;
            }

            foreach (Match match in Regex.Matches(text, patCommented2))
            {
                state = MOD_INACTIVE;
            }

            return state;
        }

        /// <summary>
        /// This method returns the last word from an inputstring by using regex. For example using "RequiredMod.1 = Yourmod" will result in "Yourmod" beeing returned
        /// </summary>
        /// <param name="text"></param>
        /// <returns>string</returns>
        private string GetNameOfRequiredMod(string text)
        {
            const string pattern = @"\S*\s*$";
            string result = "";

            // Instantiate the regular expression object.
            Regex require = new Regex(pattern, RegexOptions.IgnoreCase);

            // Match the regular expression pattern against a text string.
            Match m = require.Match(text);

            foreach (Match match in Regex.Matches(text, pattern))
            {
                result = m.Value.Replace(" ", "");
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
            // Draw the background of the ListBox control for each item.
            e.DrawBackground();

            // Determine the color of the brush to draw each item based on 
            // the index of the item to draw. Could be extended for an Orange Brush for indicating outdated Mods.
            Brush myBrush;

            //This switch accesses the Mod Struct and gets the current State of the selected Mod
            switch (modlist[e.Index].State)
            {
                case MOD_ACTIVE:
                    myBrush = Brushes.Green;
                    break;
                case MOD_INACTIVE:
                    myBrush = Brushes.Red;
                    break;
                case MOD_PENDING:
                    myBrush = Brushes.Orange;
                    break;
                default:
                    myBrush = Brushes.Black;
                    break;
            }

            // Draw the current item text based on the current 
            // Font and the custom brush settings.
            e.Graphics.DrawString(((ListBox)sender).Items[e.Index].ToString(), e.Font, myBrush, e.Bounds, StringFormat.GenericDefault);

            // If the ListBox has focus, draw a focus rectangle 
            // around the selected item.
            e.DrawFocusRectangle();
        }

        private void EnablePlusButton()
        {
            buttonAdd.Enabled = true;
            Stream myStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DoW_Mod_Manager.DoW_Mod_Manager_Resources.plus.png");
            buttonAdd.BackgroundImage = Image.FromStream(myStream);
        }

        private void DisablePlusButton()
        {
            buttonAdd.Enabled = false;
            Stream myStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DoW_Mod_Manager.DoW_Mod_Manager_Resources.plus_disabled.png");
            buttonAdd.BackgroundImage = Image.FromStream(myStream);
        }

        private void EnableMinusButton()
        {
            buttonRemove.Enabled = true;
            Stream myStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DoW_Mod_Manager.DoW_Mod_Manager_Resources.minus.png");
            buttonRemove.BackgroundImage = Image.FromStream(myStream);
        }

        private void DisableMinusButton()
        {
            buttonRemove.Enabled = false;
            Stream myStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DoW_Mod_Manager.DoW_Mod_Manager_Resources.minus_disabled.png");
            buttonRemove.BackgroundImage = Image.FromStream(myStream);
        }

        private void EnableCheckmarkButton()
        {
            buttonActivate.Enabled = true;
            Stream myStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DoW_Mod_Manager.DoW_Mod_Manager_Resources.Checkmark.png");
            buttonActivate.BackgroundImage = Image.FromStream(myStream);
        }

        private void DisableCheckmarkButton()
        {
            buttonActivate.Enabled = false;
            Stream myStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DoW_Mod_Manager.DoW_Mod_Manager_Resources.Checkmark_disabled.png");
            buttonActivate.BackgroundImage = Image.FromStream(myStream);
        }

        private void EnableCrossButton()
        {
            buttonDeactivate.Enabled = true;
            Stream myStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DoW_Mod_Manager.DoW_Mod_Manager_Resources.cross.png");
            buttonDeactivate.BackgroundImage = Image.FromStream(myStream);
        }

        private void DisableCrossButton()
        {
            buttonDeactivate.Enabled = false;
            Stream myStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DoW_Mod_Manager.DoW_Mod_Manager_Resources.cross_disabled.png");
            buttonDeactivate.BackgroundImage = Image.FromStream(myStream);
        }

        private void EnableArrowUpButton()
        {
            buttonArrowUp.Enabled = true;
            Stream myStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DoW_Mod_Manager.DoW_Mod_Manager_Resources.arrow_up.png");
            buttonArrowUp.BackgroundImage = Image.FromStream(myStream);
        }

        private void DisableArrowUpButton()
        {
            buttonArrowUp.Enabled = false;
            Stream myStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DoW_Mod_Manager.DoW_Mod_Manager_Resources.arrow_up_disabled.png");
            buttonArrowUp.BackgroundImage = Image.FromStream(myStream);
        }

        private void EnableArrowDownButton()
        {
            buttonArrowDown.Enabled = true;
            Stream myStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DoW_Mod_Manager.DoW_Mod_Manager_Resources.arrow_down.png");
            buttonArrowDown.BackgroundImage = Image.FromStream(myStream);
        }

        private void DisableArrowDownButton()
        {
            buttonArrowDown.Enabled = false;
            Stream myStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DoW_Mod_Manager.DoW_Mod_Manager_Resources.arrow_down_disabled.png");
            buttonArrowDown.BackgroundImage = Image.FromStream(myStream);
        }

        private void ButtonArrowUp_Click(object sender, EventArgs e)
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

        private void ButtonArrowDown_Click(object sender, EventArgs e)
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

        private void ButtonSaveFile_Click(object sender, EventArgs e)
        {
            string filePath = modManager.FilePaths[loadedModBox.SelectedIndex];
            string modString;
            string line;
            List<string> listOfMods = new List<string>();

            using (StreamReader sr = new StreamReader(filePath))
            {
                // Populate the Required Mods List with entries from the .module file
                while ((line = sr.ReadLine()) != null && !line.Contains("RequiredMod"))
                {
                    listOfMods.Add(line);
                }
            }

            //Write more info into the list of text
            for (int i = 0; i < modlist.Count; i++)
            {
                if (modlist[i].State.Equals(MOD_ACTIVE))
                {
                    modString = "RequiredMod." + (i + 1) + " = " + modlist[i].Name;
                    listOfMods.Add(modString);
                }
                if (modlist[i].State.Equals(MOD_INACTIVE))
                {
                    modString = "//RequiredMod." + (i + 1) + " = " + modlist[i].Name;
                    listOfMods.Add(modString);
                }
            }

            //Finally write the stuff
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                foreach (string line2 in listOfMods)
                {
                    sw.WriteLine(line2);
                }
            }

            //Update the Main Mod Manager List with possible new entries
            modManager.SetUpAllNecessaryMods();

            //Update the Dropdown list with the new entries
            GetLoadableMods();
            reselectLastItems();

            //Show a Succesprompt
            MessageBox.Show("Module file changes were successfully applied!", "Saving successful");
        }

        private void ButtonActivate_Click(object sender, EventArgs e)
        {
            int lastSelectedIndex = 0;
            //Get the currently selected element from the Used Mods List
            int selection = UsedModsList.SelectedIndex;

            if (selection != -1)
            {
                //Toggle it to be active
                //_Modlist[selection].State = "Active";
                SetModState(selection, MOD_ACTIVE);
                lastSelectedIndex = UsedModsList.SelectedIndex;
            }
            else if (selection == -1)
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
            if (lastSelectedIndex == (UsedModsList.Items.Count - 1) && modlist[lastSelectedIndex].State == MOD_ACTIVE)
            {
                UsedModsList.SelectedIndex = lastSelectedIndex;
            }
            if (lastSelectedIndex == (UsedModsList.Items.Count - 1) && modlist[lastSelectedIndex].State == MOD_INACTIVE)
            {
                UsedModsList.SelectedIndex = lastSelectedIndex;
            }
            else if (lastSelectedIndex < (UsedModsList.Items.Count - 1) && modlist[lastSelectedIndex].State != MOD_ACTIVE)
            {
                UsedModsList.SelectedIndex = lastSelectedIndex + 1;
            }
            else if (lastSelectedIndex < (UsedModsList.Items.Count - 1) && modlist[lastSelectedIndex].State == MOD_ACTIVE)
            {
                UsedModsList.SelectedIndex = lastSelectedIndex + 1;
            }
        }

        private void ButtonDeactivate_Click(object sender, EventArgs e)
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
                if (selection == 0 && modlist[1].State == MOD_INACTIVE)
                {
                    SetModState(selection, MOD_ACTIVE);
                    DisableCrossButton();
                }
                else
                {
                    SetModState(selection, MOD_INACTIVE);
                }
            }
            else if (selection == 1)
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
            if (lastSelectedIndex == (UsedModsList.Items.Count - 1) && modlist[lastSelectedIndex].State == MOD_INACTIVE)
            {
                UsedModsList.SelectedIndex = lastSelectedIndex - 1;
            }
            if (lastSelectedIndex == 0 && modlist[lastSelectedIndex + 1].State == MOD_INACTIVE)
            {
                UsedModsList.SelectedIndex = lastSelectedIndex;
            }
            else if (lastSelectedIndex < (UsedModsList.Items.Count - 1) && modlist[lastSelectedIndex].State != MOD_INACTIVE)
            {
                UsedModsList.SelectedIndex = lastSelectedIndex;
            }
            else if (lastSelectedIndex < (UsedModsList.Items.Count - 1) && modlist[lastSelectedIndex].State == MOD_INACTIVE)
            {
                UsedModsList.SelectedIndex = lastSelectedIndex - 1;
            }
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            int lastSelectedIndex = 0;
            if (AvailableModsList.SelectedItem != null)
            {
                //Get the new addable Mod candidate
                string newMod = AvailableModsList.SelectedItem.ToString();
                lastSelectedIndex = AvailableModsList.SelectedIndex;

                //Add the Mod to the selection of used Mods
                modlist.Add(new Mod(newMod, MOD_ACTIVE));
            }

            //TODO: Make new Mods be pending again on beeing Added
            //Modlist.Add(new Mod(newMod, "Pending"));

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
            else if (lastSelectedIndex == 0 && AvailableModsList.Items.Count == 0)
            {
                DisablePlusButton();
            }
        }

        private void ButtonRemove_Click(object sender, EventArgs e)
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

        private bool ModlistContainsNoInActiveMods()
        {
            hasNoInActiveMods = true;
            foreach (Mod item in modlist)
            {
                if (item.State == MOD_INACTIVE)
                {
                    hasNoInActiveMods = false;
                    break;
                }
            }
            return hasNoInActiveMods;
        }

        private bool ModlistContainsNoActiveMods()
        {
            hasNoActiveMods = true;
            foreach (Mod item in modlist)
            {
                if (item.State == MOD_ACTIVE)
                {
                    hasNoActiveMods = false;
                    break;
                }
            }
            return hasNoActiveMods;
        }

        private void SetModState(int count, string state)
        {
            //Use this if Mod is a class
            modlist[count].State = state;

            //Use this if Mod is a struct
            //Mod currentMod = modlist[count];
            //currentMod.State = state;
            //modlist[count] = currentMod;
        }

        // <summary>
        // Gets the current state of the currently selected Mod
        // </summary>
        // <param name="count"></param>
        // <returns></returns>
        //private string GetModState(int count)
        //{
        //    return modlist[count].State;
        //}

        /// <summary>
        /// Deletes an Item from the available Mods List as soon as you select it in the Dropdown List
        /// </summary>
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
            reselectLastItems();
        }

        private void UsedModsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UsedModsList.SelectedItem != null)
            {
                DisablePlusButton();
                EnableMinusButton();
                EnableArrowUpButton();
                EnableArrowDownButton();
                lastUsedModIndex = UsedModsList.SelectedIndex;
                AvailableModsList.ClearSelected();
                lastAvailableModIndex = -1;

                //This activates/deactivates that Buttons to toggle Mods active/inactive depending if there's only Active or only Inactive Mods Left
                if (modlist[UsedModsList.SelectedIndex].State.Equals(MOD_INACTIVE))
                {
                    EnableCheckmarkButton();
                    DisableCrossButton();
                }
                if (modlist.Count > 1)
                {
                    if (UsedModsList.SelectedIndex == 0 && modlist[UsedModsList.SelectedIndex + 1].State.Equals(MOD_INACTIVE) && modlist[UsedModsList.SelectedIndex].State.Equals(MOD_ACTIVE))
                    {
                        DisableCheckmarkButton();
                        DisableCrossButton();
                        DisableMinusButton();
                    }
                    else if (modlist[UsedModsList.SelectedIndex].State.Equals(MOD_ACTIVE))
                    {
                        DisableCheckmarkButton();
                        EnableCrossButton();
                    }
                }
                else if (modlist.Count == 1)
                {
                    if (modlist[UsedModsList.SelectedIndex].State.Equals(MOD_ACTIVE))
                    {
                        DisableCheckmarkButton();
                        DisableCrossButton();
                        DisableMinusButton();
                    }
                }
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
                lastAvailableModIndex = AvailableModsList.SelectedIndex;
                UsedModsList.ClearSelected();
                lastUsedModIndex = -1;
            }
        }

        /// <summary>
        /// This function reselects the last selected available mod, used mod and loaded mod.
        /// </summary>
        private void reselectLastItems()
        {
            //Safety checks for the mods list since they can change in size during runtime
            int numAvailableMods = AvailableModsList.Items.Count;
            int numLoadedMods = UsedModsList.Items.Count;
            if (lastAvailableModIndex != -1 && numAvailableMods > 0)
            {
                if (lastAvailableModIndex < numAvailableMods)
                {
                    AvailableModsList.SelectedIndex = lastAvailableModIndex;
                }
                else
                {
                    lastAvailableModIndex = numAvailableMods - 1;
                    AvailableModsList.SelectedIndex = lastAvailableModIndex;
                }
            }
            if (lastUsedModIndex != -1 && numLoadedMods > 0)
            {
                if (lastUsedModIndex < numLoadedMods)
                {
                    UsedModsList.SelectedIndex = lastUsedModIndex;
                }
                else
                {
                    lastUsedModIndex = numLoadedMods - 1;
                    UsedModsList.SelectedIndex = lastUsedModIndex;
                }
            }
            if (lastItem != null)
            {
                loadedModBox.SelectedItem = lastItem;
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
