using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
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
        private bool hasNoInactiveMods = false;

        public ModMergerForm(ModManagerForm Form)
        {
            InitializeComponent();
            modManager = Form;
            GetLoadableMods();

            // Use the same icon as executable
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            // Disable All buttons at first
            DisableArrowDownButton();
            DisableArrowUpButton();
            DisableCheckmarkButton();
            DisableCrossButton();
            DisableMinusButton();
            DisablePlusButton();

            // Disable the simpler textboxes
            UsedModsList.Enabled = false;
            AvailableModsList.Enabled = false;
        }

        private void GetAvailableMods()
        {
            AvailableModsList.Items.Clear();
            AvailableModsList.Items.AddRange(modManager.AllFoundModules.ToArray());
        }

        private void GetLoadableMods()
        {
            loadedModBox.Items.Clear();

            int modCount = modManager.AllValidModules.Count;
            string[] modsList = new string[modCount];

            for (int i = 0; i < modCount; i++)
            {
                modsList[i] = modManager.AllValidModules[i];
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

            for (int i = 0; i < modlist.Count; i++)
            {
                UsedModsList.Items.Add(modlist[i].Name + " ..." + modlist[i].State);
            }
            HideUnavailableMods();
        }

        private void HideUnavailableMods()
        {
            GetAvailableMods();                             // Get a Fresh new List everytime
            HideOrReinsertLastSelectedMod();

            for (int i = 0; i < modlist.Count; i++)
            {
                if (AvailableModsList.Items.Contains(modlist[i].Name))
                    AvailableModsList.Items.Remove(modlist[i].Name);
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
                    i--;                                    // Go one step Back to stay in place for enxt entry
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
            string currentPath = modManager.ModuleFilePaths[index];
            string line;

            modlist.Clear();

            // Read the file and display it line by line.
            using (StreamReader file = new StreamReader(currentPath))
            {
                // Populate the Required Mods List with entries from the .module file
                while ((line = file.ReadLine()) != null)
                {
                    if (line.Contains("RequiredMod"))
                        modlist.Add(new Mod(GetValueFromLine(line, false), IsModRequired(line)));
                }
            }
        }

        private string GetValueFromLine(string line, bool deleteModule)
        {
            int indexOfEqual = line.IndexOf('=');

            if (indexOfEqual > 0)
            {
                line = line.Substring(indexOfEqual + 1, line.Length - indexOfEqual - 1);
                if (deleteModule)
                    return line.Replace(" ", "").Replace(".module", "");
                else
                    return line.Replace(" ", "");
            }
            else
                return "";
        }

        private string IsModRequired(string modName)
        {
            const string pattern = "RequiredMod";
            const string patternCommented1 = ";;";
            const string patternCommented2 = "--";
            const string patternCommented3 = "//";

            if (modName.Contains(pattern))
                return MOD_ACTIVE;
            if (modName.StartsWith(patternCommented1) || modName.StartsWith(patternCommented2) || modName.StartsWith(patternCommented3))
                return MOD_INACTIVE;

            return MOD_INACTIVE;
        }

        /// <summary>
        /// Draws the Items inside the Used Mods Listbox in a specified Color
        /// </summary>
        private void UsedModsList_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            // Draw the background of the ListBox control for each item.
            e.DrawBackground();

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

            // Copy Old Entries that will be swapped
            bottomPos = UsedModsList.SelectedIndex;
            topPos = bottomPos - 1;

            if (topPos >= 0)
            {
                topItem = modlist[topPos];
                bottomItem = modlist[bottomPos];

                // Swap the Items
                modlist[bottomPos] = topItem;
                modlist[topPos] = bottomItem;

                // Redraw the list
                DrawAllRequiredModsFromList();

                // Reselect the newly placed item to allow for quick traverse through the list
                UsedModsList.SelectedIndex = topPos;
            }
        }

        private void ButtonArrowDown_Click(object sender, EventArgs e)
        {
            Mod topItem, bottomItem;
            int topPos, bottomPos;

            if (UsedModsList.SelectedIndex != -1)
            {
                // Copy Old Entries that will be swapped
                topPos = UsedModsList.SelectedIndex;
                bottomPos = topPos + 1;

                if (bottomPos <= modlist.Count - 1)
                {
                    topItem = modlist[topPos];
                    bottomItem = modlist[bottomPos];

                    // Swap the Items
                    modlist[bottomPos] = topItem;
                    modlist[topPos] = bottomItem;

                    // Redraw the list
                    DrawAllRequiredModsFromList();

                    // Reselect the newly placed item to allow for quick traverse through the list
                    UsedModsList.SelectedIndex = bottomPos;
                }
            }
        }

        private void ButtonSaveFile_Click(object sender, EventArgs e)
        {
            string filePath = modManager.ModuleFilePaths[loadedModBox.SelectedIndex];
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

            // Write more info into the list of text
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

            // Finally write the stuff
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                for (int i = 0; i < listOfMods.Count; i++)
                {
                    sw.WriteLine(listOfMods[i]);
                }
            }

            // Update the Main Mod Manager List with possible new entries
            modManager.SetUpAllNecessaryMods();

            // Update the Dropdown list with the new entries
            GetLoadableMods();
            ReselectLastItems();

            // Show a Succesprompt
            MessageBox.Show("Module file changes were successfully applied!", "Saving successful");
        }

        private void ButtonActivate_Click(object sender, EventArgs e)
        {
            int lastSelectedIndex = 0;

            // Get the currently selected element from the Used Mods List
            int selection = UsedModsList.SelectedIndex;

            if (selection != -1)
            {
                // Toggle it to be active
                SetModState(selection, MOD_ACTIVE);
                lastSelectedIndex = UsedModsList.SelectedIndex;
            }
            else if (selection == -1)
            {
                lastSelectedIndex = 0;
                UsedModsList.SelectedIndex = lastSelectedIndex;
            }

            // Redraw the List of Items
            DrawAllRequiredModsFromList();

            // Reselect Elements at the last place
            if (ModlistContainsNoActiveMods())
            {
                UsedModsList.SelectedIndex = lastSelectedIndex;
                EnableCrossButton();
            }
            if (ModlistContainsNoInactiveMods())
            {
                UsedModsList.SelectedIndex = lastSelectedIndex;
                DisableCheckmarkButton();
            }
            if (lastSelectedIndex == (UsedModsList.Items.Count - 1) && modlist[lastSelectedIndex].State == MOD_ACTIVE)
                UsedModsList.SelectedIndex = lastSelectedIndex;
            if (lastSelectedIndex == (UsedModsList.Items.Count - 1) && modlist[lastSelectedIndex].State == MOD_INACTIVE)
                UsedModsList.SelectedIndex = lastSelectedIndex;
            else if (lastSelectedIndex < (UsedModsList.Items.Count - 1) && modlist[lastSelectedIndex].State != MOD_ACTIVE)
                UsedModsList.SelectedIndex = lastSelectedIndex + 1;
            else if (lastSelectedIndex < (UsedModsList.Items.Count - 1) && modlist[lastSelectedIndex].State == MOD_ACTIVE)
                UsedModsList.SelectedIndex = lastSelectedIndex + 1;
        }

        private void ButtonDeactivate_Click(object sender, EventArgs e)
        {
            int lastSelectedIndex = 0;

            // Get the currently selected element from the Used Mods List
            int selection = UsedModsList.SelectedIndex;

            if (selection != -1)
            {
                // Store last position
                lastSelectedIndex = UsedModsList.SelectedIndex;

                // Toggle it to be inactive
                if (selection == 0 && modlist[1].State == MOD_INACTIVE)
                {
                    SetModState(selection, MOD_ACTIVE);
                    DisableCrossButton();
                }
                else
                    SetModState(selection, MOD_INACTIVE);
            }
            else if (selection == 1)
            {
                lastSelectedIndex = 0;
                UsedModsList.SelectedIndex = lastSelectedIndex;
            }

            // Redraw the List of Items
            DrawAllRequiredModsFromList();

            // Reselect Elements at the last place
            if (ModlistContainsNoActiveMods())
            {
                UsedModsList.SelectedIndex = lastSelectedIndex;
                DisableCrossButton();
            }
            if (ModlistContainsNoInactiveMods())
                UsedModsList.SelectedIndex = lastSelectedIndex;
            if (lastSelectedIndex == (UsedModsList.Items.Count - 1) && modlist[lastSelectedIndex].State == MOD_INACTIVE)
                UsedModsList.SelectedIndex = lastSelectedIndex - 1;
            if (lastSelectedIndex == 0 && modlist[lastSelectedIndex + 1].State == MOD_INACTIVE)
                UsedModsList.SelectedIndex = lastSelectedIndex;
            else if (lastSelectedIndex < (UsedModsList.Items.Count - 1) && modlist[lastSelectedIndex].State != MOD_INACTIVE)
                UsedModsList.SelectedIndex = lastSelectedIndex;
            else if (lastSelectedIndex < (UsedModsList.Items.Count - 1) && modlist[lastSelectedIndex].State == MOD_INACTIVE)
                UsedModsList.SelectedIndex = lastSelectedIndex - 1;
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            int lastSelectedIndex = 0;

            if (AvailableModsList.SelectedItem != null)
            {
                // Get the new addable Mod candidate
                string newMod = AvailableModsList.SelectedItem.ToString();
                lastSelectedIndex = AvailableModsList.SelectedIndex;

                // Add the Mod to the selection of used Mods
                modlist.Add(new Mod(newMod, MOD_ACTIVE));
            }

            // TODO: Make new Mods be pending again on beeing Added
            //Modlist.Add(new Mod(newMod, "Pending"));

            // Redraw the List to display the added candidate
            DrawAllRequiredModsFromList();

            // Reselect Elements at the last place
            if (lastSelectedIndex < AvailableModsList.Items.Count)
                AvailableModsList.SelectedIndex = lastSelectedIndex;
            else if (lastSelectedIndex == AvailableModsList.Items.Count && AvailableModsList.Items.Count > 0)
                AvailableModsList.SelectedIndex = lastSelectedIndex - 1;
            else if (lastSelectedIndex == 0 && AvailableModsList.Items.Count == 0)
                DisablePlusButton();
        }

        private void ButtonRemove_Click(object sender, EventArgs e)
        {
            int lastSelectedIndex = 0;

            // Get the new addable Mod candidate
            int delMod = UsedModsList.SelectedIndex;

            if (delMod != -1)
            {
                // Add the Mod to the selection of used Mods
                modlist.RemoveAt(delMod);
                lastSelectedIndex = UsedModsList.SelectedIndex;
            }

            // Redraw the List to display the added candidate
            DrawAllRequiredModsFromList();

            // Reselect Elements at the last place
            if (lastSelectedIndex < UsedModsList.Items.Count)
                UsedModsList.SelectedIndex = lastSelectedIndex;
            else if (lastSelectedIndex == UsedModsList.Items.Count && UsedModsList.Items.Count > 0)
                UsedModsList.SelectedIndex = lastSelectedIndex - 1;
            else if (lastSelectedIndex == 0 && UsedModsList.Items.Count == 0)
            {
                DisableMinusButton();
                DisableCheckmarkButton();
                DisableArrowUpButton();
                DisableArrowDownButton();
                DisableCrossButton();
            }
        }

        private bool ModlistContainsNoInactiveMods()
        {
            hasNoInactiveMods = true;

            for (int i = 0; i < modlist.Count; i++)
            {
                if (modlist[i].State == MOD_INACTIVE)
                {
                    hasNoInactiveMods = false;
                    break;
                }
            }

            return hasNoInactiveMods;
        }

        private bool ModlistContainsNoActiveMods()
        {
            hasNoActiveMods = true;

            for (int i = 0; i < modlist.Count; i++)
            {
                if (modlist[i].State == MOD_ACTIVE)
                {
                    hasNoActiveMods = false;
                    break;
                }
            }

            return hasNoActiveMods;
        }

        private void SetModState(int count, string state)
        {
            // Use this if Mod is a class
            modlist[count].State = state;

            // Use this if Mod is a struct
            //Mod currentMod = modlist[count];
            //currentMod.State = state;
            //modlist[count] = currentMod;
        }

        /// <summary>
        /// Deletes an Item from the available Mods List as soon as you select it in the Dropdown List
        /// </summary>
        private void LoadedModBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetActiveModsFromFile();
            ModlistContainsNoActiveMods();
            ModlistContainsNoInactiveMods();

            if (modlist.Count != 0)
            {
                DrawAllRequiredModsFromList();
                buttonSaveFile.Enabled = true;

                // Enable UI Elements
                UsedModsList.Enabled = true;
                AvailableModsList.Enabled = true;
            }
            else
            {
                UsedModsList.Items.Clear();
                GetAvailableMods();
                HideOrReinsertLastSelectedMod();
            }
            ReselectLastItems();
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

                // This activates/deactivates that Buttons to toggle Mods active/inactive depending if there's only Active or only Inactive Mods Left
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
        private void ReselectLastItems() 
        {
            // Safety checks for the mods list since they can change in size during runtime
            int numAvailableMods = AvailableModsList.Items.Count;
            int numLoadedMods = UsedModsList.Items.Count;
            if (lastAvailableModIndex != -1 && numAvailableMods > 0)
            {
                if (lastAvailableModIndex < numAvailableMods)
                    AvailableModsList.SelectedIndex = lastAvailableModIndex;
                else
                {
                    lastAvailableModIndex = numAvailableMods - 1;
                    AvailableModsList.SelectedIndex = lastAvailableModIndex;
                }
            }
            if (lastUsedModIndex != -1 && numLoadedMods > 0)
            {
                if (lastUsedModIndex < numLoadedMods)
                    UsedModsList.SelectedIndex = lastUsedModIndex;
                else
                {
                    lastUsedModIndex = numLoadedMods - 1;
                    UsedModsList.SelectedIndex = lastUsedModIndex;
                }
            }
            if (lastItem != null)
                loadedModBox.SelectedItem = lastItem;
        }
    }
}