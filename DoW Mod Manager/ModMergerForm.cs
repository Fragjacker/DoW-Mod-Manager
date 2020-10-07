using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace DoW_Mod_Manager
{
    public partial class ModMergerForm : Form
    {
        public enum ModState { Inactive, Active, Pending }

        public class Mod
        {
            public string Name;
            public ModState State;

            public Mod(string name, ModState state)
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

        /// <summary>
        /// Creates the Form of the Mod Merger Window
        /// </summary>
        /// <param name="Form"></param>
        public ModMergerForm(ModManagerForm Form)
        {
            InitializeComponent();

            modManager = Form;

            GetLoadableMods();

            // Use the same icon as executable
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            // Disable the simpler textboxes
            UsedModsList.Enabled = false;
            AvailableModsList.Enabled = false;
        }

        /// <summary>
        /// Gets all of the loadable Mods for the Dropdown list
        /// </summary>
        // Request the inlining of this method
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void GetLoadableMods()
        {
            loadedModBox.Items.Clear();

            int modCount = modManager.AllValidModules.Count;
            for (int i = 0; i < modCount; i++)
            {
                loadedModBox.Items.Add(modManager.AllValidModules[i]);
            }
        }

        /// <summary>
        /// Gets all available Mods from the Mod Manager Form
        /// </summary>
        // Request the inlining of this method
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void GetAvailableMods()
        {
            AvailableModsList.Items.Clear();

            AvailableModsList.Items.AddRange(modManager.AllFoundModules.ToArray());
        }

        /// <summary>
        /// This method hides or reinserts last selected mod
        /// </summary>
        // Request the inlining of this method
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void HideOrReinsertLastSelectedMod()
        {
            if (lastItem != null && !AvailableModsList.Items.Contains(lastItem))
                AvailableModsList.Items.Insert(lastDropDownItemIndex, lastItem);
            
            // Stores the last selected item in order to reinsert it once the selection changes again.
            lastItem = loadedModBox.SelectedItem;
            lastDropDownItemIndex = AvailableModsList.Items.IndexOf(lastItem);

            AvailableModsList.Items.Remove(loadedModBox.SelectedItem);
        }

        /// <summary>
        /// This method draws all required mods from a list
        /// </summary>
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

        /// <summary>
        /// This method sorts all inactive mods to bottom
        /// </summary>
        // Request the inlining of this method
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SortInactiveModsToBottom()
        {
            List<Mod> inactiveModsList = new List<Mod>();

            for (int i = 0; i < modlist.Count; i++)
            {
                if (modlist[i].State.Equals(ModState.Inactive))
                {
                    inactiveModsList.Add(modlist[i]);
                    modlist.RemoveAt(i);

                    // Go one step Back to stay in place for next entry
                    i--;
                }
            }
            modlist.AddRange(inactiveModsList);
        }

        /// <summary>
        /// This method hides all unavailable mods
        /// </summary>
        // Request the inlining of this method
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void HideUnavailableMods()
        {
            // Get a Fresh new List everytime
            GetAvailableMods();

            HideOrReinsertLastSelectedMod();

            for (int i = 0; i < modlist.Count; i++)
            {
                if (AvailableModsList.Items.Contains(modlist[i].Name))
                    AvailableModsList.Items.Remove(modlist[i].Name);
            }
        }

        /// <summary>
        /// Fills the Used Mods Listbox with the Mods that are currently set as RequiredMod inside the .module file
        /// </summary>
        // Request the inlining of this method
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void GetActiveModsFromFile()
        {
            int index = loadedModBox.SelectedIndex;
            string currentModuleFilePath = modManager.ModuleFilePaths[index];

            modlist.Clear();

            // Read the file and display it line by line.
            using (StreamReader file = new StreamReader(currentModuleFilePath))
            {
                string line;

                // Populate the Required Mods List with entries from the .module file
                while ((line = file.ReadLine()) != null)
                {
                    if (line.Contains("RequiredMod"))
                    {
                        if (line.StartsWith(";;") || line.StartsWith("//"))
                            modlist.Add(new Mod(Program.GetValueFromLine(line, false), ModState.Inactive));
                        else
                            modlist.Add(new Mod(Program.GetValueFromLine(line, false), ModState.Active));
                    }
                }
            }
        }

        /// <summary>
        /// Draws the Items inside the Used Mods Listbox in a specified Color
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UsedModsList_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Draw the background of the ListBox control for each item.
            e.DrawBackground();

            Brush myBrush;

            //This switch accesses the Mod Struct and gets the current State of the selected Mod
            switch (modlist[e.Index].State)
            {
                case ModState.Active:
                    myBrush = Brushes.Green;
                    break;
                case ModState.Inactive:
                    myBrush = Brushes.Red;
                    break;
                case ModState.Pending:
                    myBrush = Brushes.Orange;
                    break;
                default:
                    myBrush = Brushes.Black;
                    break;
            }

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
        /// This method enables Plus Button (buttonAdd)
        /// </summary>
        // Request the inlining of this method
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void EnablePlusButton()
        {
            buttonAdd.Enabled = true;
            Stream myStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DoW_Mod_Manager.Resources.plus.png");
            buttonAdd.BackgroundImage = Image.FromStream(myStream);
        }

        /// <summary>
        /// This method disables Plus Button (buttonAdd)
        /// </summary>
        // Request the inlining of this method
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void DisablePlusButton()
        {
            buttonAdd.Enabled = false;
            Stream myStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DoW_Mod_Manager.Resources.plus_disabled.png");
            buttonAdd.BackgroundImage = Image.FromStream(myStream);
        }

        /// <summary>
        /// This method enables minus Button (buttonRemove)
        /// </summary>
        // Request the inlining of this method
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void EnableMinusButton()
        {
            buttonRemove.Enabled = true;
            Stream myStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DoW_Mod_Manager.Resources.minus.png");
            buttonRemove.BackgroundImage = Image.FromStream(myStream);
        }

        /// <summary>
        /// This method disables minus Button (buttonRemove)
        /// </summary>
        // Request the inlining of this method
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void DisableMinusButton()
        {
            buttonRemove.Enabled = false;
            Stream myStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DoW_Mod_Manager.Resources.minus_disabled.png");
            buttonRemove.BackgroundImage = Image.FromStream(myStream);
        }

        /// <summary>
        /// This method enables Checkmark Button (buttonActivate)
        /// </summary>
        // Request the inlining of this method
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void EnableCheckmarkButton()
        {
            buttonActivate.Enabled = true;
            Stream myStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DoW_Mod_Manager.Resources.checkmark.png");
            buttonActivate.BackgroundImage = Image.FromStream(myStream);
        }

        /// <summary>
        /// This method disables Checkmark Button (buttonActivate)
        /// </summary>
        // Request the inlining of this method
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void DisableCheckmarkButton()
        {
            buttonActivate.Enabled = false;
            Stream myStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DoW_Mod_Manager.Resources.checkmark_disabled.png");
            buttonActivate.BackgroundImage = Image.FromStream(myStream);
        }

        /// <summary>
        /// This method enables Cross Button (buttonDeactivate)
        /// </summary>
        // Request the inlining of this method
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void EnableCrossButton()
        {
            buttonDeactivate.Enabled = true;
            Stream myStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DoW_Mod_Manager.Resources.cross.png");
            buttonDeactivate.BackgroundImage = Image.FromStream(myStream);
        }

        /// <summary>
        /// This method disables Cross Button (buttonDeactivate)
        /// </summary>
        // Request the inlining of this method
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void DisableCrossButton()
        {
            buttonDeactivate.Enabled = false;
            Stream myStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DoW_Mod_Manager.Resources.cross_disabled.png");
            buttonDeactivate.BackgroundImage = Image.FromStream(myStream);
        }

        /// <summary>
        /// This method enables ArrowUp Button (buttonArrowUp)
        /// </summary>
        // Request the inlining of this method
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void EnableArrowUpButton()
        {
            buttonArrowUp.Enabled = true;
            Stream myStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DoW_Mod_Manager.Resources.arrow_up.png");
            buttonArrowUp.BackgroundImage = Image.FromStream(myStream);
        }

        /// <summary>
        /// This method disables ArrowUp Button (buttonArrowUp)
        /// </summary>
        // Request the inlining of this method
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void DisableArrowUpButton()
        {
            buttonArrowUp.Enabled = false;
            Stream myStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DoW_Mod_Manager.Resources.arrow_up_disabled.png");
            buttonArrowUp.BackgroundImage = Image.FromStream(myStream);
        }

        /// <summary>
        /// This method enables ArrowDown Button (buttonArrowDown)
        /// </summary>
        // Request the inlining of this method
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void EnableArrowDownButton()
        {
            buttonArrowDown.Enabled = true;
            Stream myStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DoW_Mod_Manager.Resources.arrow_down.png");
            buttonArrowDown.BackgroundImage = Image.FromStream(myStream);
        }

        /// <summary>
        /// This method disables ArrowDown Button (buttonArrowDown)
        /// </summary>
        // Request the inlining of this method
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void DisableArrowDownButton()
        {
            buttonArrowDown.Enabled = false;
            Stream myStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DoW_Mod_Manager.Resources.arrow_down_disabled.png");
            buttonArrowDown.BackgroundImage = Image.FromStream(myStream);
        }

        /// <summary>
        /// This method swaps top and bottom mods up
        /// </summary>
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

        /// <summary>
        /// This method swaps top and bottom mods down
        /// </summary>
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

        /// <summary>
        /// This method saves changes to the *.module file
        /// </summary>
        private void ButtonSaveFile_Click(object sender, EventArgs e)
        {
            string filePath = modManager.ModuleFilePaths[loadedModBox.SelectedIndex];
            List<string> listOfMods = new List<string>();

            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;

                // Populate the Required Mods List with entries from the .module file
                while ((line = sr.ReadLine()) != null && !line.Contains("RequiredMod"))
                {
                    listOfMods.Add(line);
                }
            }

            string modString = "";

            // Write more info into the list of mods
            for (int i = 0; i < modlist.Count; i++)
            {
                switch (modlist[i].State)
                {
                    case ModState.Active:
                        break;
                    case ModState.Inactive:
                        modString = "//";
                        break;
                    case ModState.Pending:
                        break;
                }

                modString = "RequiredMod." + (i + 1) + " = " + modlist[i].Name;
                listOfMods.Add(modString);
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
            ThemedMessageBox.Show("Module file changes were successfully applied!", "Saving successful");
        }

        /// <summary>
        /// This method activates mod
        /// </summary>
        private void ButtonActivate_Click(object sender, EventArgs e)
        {
            int lastSelectedIndex;

            // Get the currently selected element from the Used Mods List
            int currentIndex = UsedModsList.SelectedIndex;

            if (currentIndex == -1)
            {
                lastSelectedIndex = 0;
                UsedModsList.SelectedIndex = lastSelectedIndex;
            }
            else
            {
                // Toggle it to be active
                SetModState(currentIndex, ModState.Active);
                lastSelectedIndex = UsedModsList.SelectedIndex;
            }

            // Redraw the List of Items
            DrawAllRequiredModsFromList();

            // Reselect Elements at the last place
            if (DoesModlistContainsNoActiveMods())
            {
                UsedModsList.SelectedIndex = lastSelectedIndex;
                EnableCrossButton();
            }
            if (DoesModlistContainsNoInactiveMods())
            {
                UsedModsList.SelectedIndex = lastSelectedIndex;
                DisableCheckmarkButton();
            }
            if (lastSelectedIndex == (UsedModsList.Items.Count - 1) && modlist[lastSelectedIndex].State == ModState.Active)
                UsedModsList.SelectedIndex = lastSelectedIndex;
            if (lastSelectedIndex == (UsedModsList.Items.Count - 1) && modlist[lastSelectedIndex].State == ModState.Inactive)
                UsedModsList.SelectedIndex = lastSelectedIndex;
            else if (lastSelectedIndex < (UsedModsList.Items.Count - 1) && modlist[lastSelectedIndex].State != ModState.Active)
                UsedModsList.SelectedIndex = lastSelectedIndex + 1;
            else if (lastSelectedIndex < (UsedModsList.Items.Count - 1) && modlist[lastSelectedIndex].State == ModState.Active)
                UsedModsList.SelectedIndex = lastSelectedIndex + 1;
        }

        /// <summary>
        /// This method deactivates mod
        /// </summary>
        private void ButtonDeactivate_Click(object sender, EventArgs e)
        {
            int lastSelectedIndex;

            // Get the currently selected element from the Used Mods List
            int currentIndex = UsedModsList.SelectedIndex;

            if (currentIndex == 1)
            {
                lastSelectedIndex = 0;
                UsedModsList.SelectedIndex = lastSelectedIndex;
            }
            else
            {
                // Store last position
                lastSelectedIndex = UsedModsList.SelectedIndex;

                // Toggle it to be inactive
                if (currentIndex == 0 && modlist[1].State == ModState.Inactive)
                {
                    SetModState(currentIndex, ModState.Active);
                    DisableCrossButton();
                }
                else
                    SetModState(currentIndex, ModState.Inactive);
            }

            // Redraw the List of Items
            DrawAllRequiredModsFromList();

            // Reselect Elements at the last place
            if (DoesModlistContainsNoActiveMods())
            {
                UsedModsList.SelectedIndex = lastSelectedIndex;
                DisableCrossButton();
            }
            if (DoesModlistContainsNoInactiveMods())
                UsedModsList.SelectedIndex = lastSelectedIndex;
            if (lastSelectedIndex == (UsedModsList.Items.Count - 1) && modlist[lastSelectedIndex].State == ModState.Inactive)
                UsedModsList.SelectedIndex = lastSelectedIndex - 1;
            if (lastSelectedIndex == 0 && modlist[lastSelectedIndex + 1].State == ModState.Inactive)
                UsedModsList.SelectedIndex = lastSelectedIndex;
            else if (lastSelectedIndex < (UsedModsList.Items.Count - 1) && modlist[lastSelectedIndex].State != ModState.Inactive)
                UsedModsList.SelectedIndex = lastSelectedIndex;
            else if (lastSelectedIndex < (UsedModsList.Items.Count - 1) && modlist[lastSelectedIndex].State == ModState.Inactive)
                UsedModsList.SelectedIndex = lastSelectedIndex - 1;
        }

        /// <summary>
        /// This method adds a mod
        /// </summary>
        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            int lastSelectedIndex = 0;

            if (AvailableModsList.SelectedItem != null)
            {
                // Get the new addable Mod candidate
                string newMod = AvailableModsList.SelectedItem.ToString();
                lastSelectedIndex = AvailableModsList.SelectedIndex;

                // Add the Mod to the selection of used Mods
                modlist.Add(new Mod(newMod, ModState.Active));
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

        /// <summary>
        /// This method removes a mod
        /// </summary>
        private void ButtonRemove_Click(object sender, EventArgs e)
        {
            int lastSelectedIndex = 0;

            // Get the new addable Mod candidate
            int modIndexToDelete = UsedModsList.SelectedIndex;

            if (modIndexToDelete != -1)
            {
                // Add the Mod to the selection of used Mods
                modlist.RemoveAt(modIndexToDelete);
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

        /// <summary>
        /// This method checks if modList contains Inactive mods or not
        /// </summary>
        /// <returns>bool</returns>
        private bool DoesModlistContainsNoInactiveMods()
        {
            hasNoInactiveMods = true;

            for (int i = 0; i < modlist.Count; i++)
            {
                if (modlist[i].State == ModState.Inactive)
                {
                    hasNoInactiveMods = false;
                    break;
                }
            }

            return hasNoInactiveMods;
        }

        /// <summary>
        /// This method checks if modList contains Active mods or not
        /// </summary>
        /// <returns>bool</returns>
        private bool DoesModlistContainsNoActiveMods()
        {
            hasNoActiveMods = true;

            for (int i = 0; i < modlist.Count; i++)
            {
                if (modlist[i].State == ModState.Active)
                {
                    hasNoActiveMods = false;
                    break;
                }
            }

            return hasNoActiveMods;
        }

        /// <summary>
        /// This method sets the state of a mod in a modList
        /// </summary>
        /// <param name="index"></param>
        /// <param name="state"></param>
        private void SetModState(int index, ModState state)
        {
            // Use this if Mod is a class
            modlist[index].State = state;

            // Use this if Mod is a struct
            //Mod currentMod = modlist[index];
            //currentMod.State = state;
            //modlist[index] = currentMod;
        }

        /// <summary>
        /// Deletes an Item from the available Mods List as soon as you select it in the Dropdown List
        /// </summary>
        private void LoadedModBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetActiveModsFromFile();
            DoesModlistContainsNoActiveMods();
            DoesModlistContainsNoInactiveMods();

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

        /// <summary>
        /// Changes selected used mod
        /// </summary>
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

                // This activates/deactivates Buttons that toggles Mods active/inactive depending if there's only Active or only Inactive Mods Left
                if (modlist[UsedModsList.SelectedIndex].State.Equals(ModState.Inactive))
                {
                    EnableCheckmarkButton();
                    DisableCrossButton();
                }
                if (modlist.Count > 1)
                {
                    if (UsedModsList.SelectedIndex == 0 && modlist[UsedModsList.SelectedIndex + 1].State.Equals(ModState.Inactive) && modlist[UsedModsList.SelectedIndex].State.Equals(ModState.Active))
                    {
                        DisableCheckmarkButton();
                        DisableCrossButton();
                        DisableMinusButton();
                    }
                    else if (modlist[UsedModsList.SelectedIndex].State.Equals(ModState.Active))
                    {
                        DisableCheckmarkButton();
                        EnableCrossButton();
                    }
                }
                else if (modlist.Count == 1)
                {
                    if (modlist[UsedModsList.SelectedIndex].State.Equals(ModState.Active))
                    {
                        DisableCheckmarkButton();
                        DisableCrossButton();
                        DisableMinusButton();
                    }
                }
            }
        }

        /// <summary>
        /// Changes selected availabe mod
        /// </summary>
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
        /// This method reselects the last selected available mod, used mod and loaded mod.
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