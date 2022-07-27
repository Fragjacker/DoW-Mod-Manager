using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace DoW_Mod_Manager
{
    public partial class ModMergerForm : Form
    {
        public enum ModState { Inactive, Active, Missing }
        public enum ModuleState { Original, Merged }

        public class Module
        {
            public string Name;
            public string FilePath;
            public ModuleState ModuleState;
            public Module Parent;

            public Module(string name, string fullpath, ModuleState modstate, Module parent = null)
            {
                Name = name;
                FilePath = fullpath;
                ModuleState = modstate;
                Parent = parent;
            }
        }

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
        private string mergePostfix = "_MERGED";

        private readonly List<Mod> modlist = new List<Mod>();
        private List<Module> moduleList = new List<Module>();
        private bool hasNoActiveMods = false;
        private bool hasNoInactiveMods = false;
        private bool usesW40kmodule = false;
        private bool usesDXP2module = false;
        public List<string> AlllegitModules;

        /// <summary>
        /// Creates the Form of the Mod Merger Window
        /// </summary>
        /// <param name="Form"></param>
        public ModMergerForm(ModManagerForm Form)
        {
            InitializeComponent();

            modManager = Form;
            AlllegitModules = modManager.AllFoundModules;

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
            moduleList.Clear();

            int modCount = modManager.AllValidModules.Count;
            for (int i = 0; i < modCount; i++)
            {
                //Check if new upcoming mod is a merged mod, if yes surpress original mod to be loadable.
                //Since we only want to apply changes to the merged version.
                string newMod = modManager.AllValidModules[i].getName;
                if (newMod.Contains(mergePostfix))
                {
                    int moduleCount = moduleList.Count;
                    string compareStr = newMod.Remove(newMod.Length - mergePostfix.Length);
                    for (int j = 0; j < moduleCount; j++)
                    {
                        if (moduleList[j].Name.Equals(compareStr))
                        {
                            Module newMergedMod = new Module(newMod,
                                modManager.CurrentDir + "\\" + newMod + ".module",
                                ModuleState.Merged);
                            moduleList.Add(newMergedMod); ;
                            moduleList[j].Parent = newMergedMod;
                            break;
                        }
                    }
                }
                else
                {
                    moduleList.Add(new Module(newMod, 
                        modManager.CurrentDir + "\\" + newMod + ".module", 
                        ModuleState.Original));
                }
            }
            //Fill the loaded Mod tab now.
            for (int i = 0; i < moduleList.Count; i++)
            {
                if (moduleList[i].Parent == null)
                {
                    loadedModBox.Items.Add(moduleList[i].Name);
                }
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

            AvailableModsList.Items.AddRange(AlllegitModules.ToArray());
        }

        /// <summary>
        /// This method hides or reinserts last selected mod
        /// </summary>
        // Request the inlining of this method
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void HideOrReinsertLastSelectedMod()
        {
            // Stores the last selected item in order to reinsert it once the selection changes again.
            //lastAvailableModIndex = AvailableModsList.Items.IndexOf(lastItem);
            int selIndex = AvailableModsList.SelectedIndex;
            if (selIndex != -1 && lastItem != null && !AvailableModsList.Items.Contains(lastItem))
            {
                AvailableModsList.Items.Insert(lastAvailableModIndex, lastItem);
                lastAvailableModIndex = selIndex;
            }

            AvailableModsList.Items.Remove(lastItem);
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
                if (modlist[i].State.Equals(ModState.Inactive) || modlist[i].State.Equals(ModState.Missing))
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
            string currentModuleFilePath;
            if (moduleList[index].Parent == null) currentModuleFilePath = moduleList[index].FilePath;
            else currentModuleFilePath = moduleList[index].Parent.FilePath;

            modlist.Clear();

            Regex rmRegex = new Regex("(;*\\/*)RequiredMod.\\s*\\d*\\s*=\\s*(\\w*)", (RegexOptions.Compiled | RegexOptions.IgnoreCase));

            // Read the file and display it line by line.
            using (StreamReader file = new StreamReader(currentModuleFilePath))
            {
                string line;

                // Populate the Required Mods List with entries from the .module file
                while ((line = file.ReadLine()) != null)
                {
                    if (isValidActiveMod(rmRegex, line))
                    {
                        string prefix = rmRegex.Match(line).Groups[1].Value;
                        string modName = rmRegex.Match(line).Groups[2].Value;
                        if (AlllegitModules.Contains(modName))
                        {
                            if (prefix.Equals(";;") || prefix.Equals("//"))
                                modlist.Add(new Mod(modName, ModState.Inactive));
                            else
                                modlist.Add(new Mod(modName, ModState.Active));
                        }
                        else
                            modlist.Add(new Mod(modName, ModState.Missing));
                    }
                }
            }
        }

        /// <summary>
        /// This function checks if the found line from the module file should be displayed
        /// in the active mods window.
        /// </summary>
        /// <param name="rmRegex"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        private bool isValidActiveMod(Regex rmRegex, string line)
        {
            string lowerCase = line.ToLower();
            bool isW40kMod = lowerCase.Contains("w40k");
            bool isDXP2Mod = lowerCase.Contains("dxp2");
            if (isW40kMod) { usesW40kmodule = true; }
            if (isDXP2Mod) { usesDXP2module = true; }
            return rmRegex.IsMatch(line) && !(isW40kMod || isDXP2Mod);
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
                    myBrush = Brushes.LimeGreen;
                    break;
                case ModState.Inactive:
                    myBrush = Brushes.Orange;
                    break;
                case ModState.Missing:
                    myBrush = Brushes.Red;
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

            // Add base game modules since they can't be picked but need to be there.
            int startOffset = 0;
            if (usesW40kmodule) { listOfMods.Add("RequiredMod.1 = W40k"); startOffset++; }
            if (usesDXP2module) { listOfMods.Add("RequiredMod.2 = DXP2"); startOffset++; }

            // Add the disabled and activate mods now.
            string disablerString = string.Empty;
            string modString = string.Empty;
            for (int i = 0; i < modlist.Count; i++)
            {
                switch (modlist[i].State)
                {
                    case ModState.Active:
                        break;
                    case ModState.Inactive:
                        disablerString = "//";
                        break;
                    case ModState.Missing:
                        disablerString = "//";
                        break;
                }

                modString = disablerString + "RequiredMod." + (i + 1 + startOffset) + " = " + modlist[i].Name;
                listOfMods.Add(modString);
            }

            // Finally write the stuff
            if (!filePath.Contains(mergePostfix))
            {
                filePath = filePath.Remove(filePath.Length - 7) + mergePostfix + ".module";
                lastDropDownItemIndex = loadedModBox.Items.IndexOf(lastItem);
            }
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                for (int i = 0; i < listOfMods.Count; i++)
                {
                    sw.WriteLine(listOfMods[i]);
                }
            }

            // Show a Succesprompt
            ThemedMessageBox.Show("New merged module file was created and the changes were successfully applied!\nYou can now select the new merged mod in the Mod Manager to play it.", "Saving successful!");
        }
        /// <summary>
        /// This function occurs when the delete button was pressed and removes a merge.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            int selIndex = loadedModBox.SelectedIndex;
            string filePath = moduleList[selIndex].Parent.FilePath;
            lastDropDownItemIndex = selIndex;
            File.Delete(filePath);

            // Show a Succesprompt
            ThemedMessageBox.Show("Current merged module file was deleted and all changes were successfully reverted!", "Delete successful!");
        }

        /// <summary>
        /// Refreshes all modlists and important internal lists.
        /// </summary>
        public void refreshAllModEntries()
        {
            // Update the Main Mod Manager List with possible new entries.
            //modManager.SetUpAllNecessaryMods();
            AlllegitModules = modManager.AllFoundModules;

            // Update the Dropdown list with the new entries.
            GetAvailableMods();
            GetLoadableMods();

            // Do reselection now.
            GetLoastDropDownItem();
            GetActiveModsFromFile();
            DrawAllRequiredModsFromList();
            ReselectLastItems();
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

            if (currentIndex == -1)
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
            if (modlist.Count != 0)
            {
                buttonSaveFile.Enabled = true;
                UsedModsList.Enabled = true;
            }
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
        /// This method defines the event handlers for when some file was changed.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void FileSystemWatcherOnChanged(object source, FileSystemEventArgs e)
        {
            refreshAllModEntries();
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
        /// Deletes an Item from the available Mods List as soon as you select it in the Dropdown List.
        /// </summary>
        private void LoadedModBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            lastItem = loadedModBox.SelectedItem;
            GetActiveModsFromFile();
            DrawAllRequiredModsFromList();
            //Handle Delete button
            if (moduleList[loadedModBox.SelectedIndex].Parent != null) deleteButton.Enabled = true;
            else deleteButton.Enabled = false;
            //Handle the other UI elements
            if (AlllegitModules.Count != 0 || modlist.Count != 0)
            {
                if (AlllegitModules.Count != 0) AvailableModsList.Enabled = true;
                if (AlllegitModules.Count == 0) AvailableModsList.Enabled = false;
                if (modlist.Count != 0)
                {
                    buttonSaveFile.Enabled = true;
                    UsedModsList.Enabled = true;
                }
                if (modlist.Count == 0)
                {
                    buttonSaveFile.Enabled = false;
                    UsedModsList.Enabled = false;
                }
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
                    else if (modlist[UsedModsList.SelectedIndex].State.Equals(ModState.Missing))
                    {
                        DisableCheckmarkButton();
                        DisableCrossButton();
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
            GetLoastDropDownItem();
        }

        /// <summary>
        /// Assigns a proper value to the last item selected in the loaded mod dropdownlist.
        /// </summary>
        private void GetLoastDropDownItem()
        {
            if (lastDropDownItemIndex > (loadedModBox.Items.Count - 1))
            {
                lastDropDownItemIndex = loadedModBox.Items.Count - 1;
                loadedModBox.SelectedIndex = lastDropDownItemIndex;
            }
            else if (lastItem != null && loadedModBox.Items.Contains(lastItem))
                loadedModBox.SelectedItem = lastItem;
            else
                loadedModBox.SelectedIndex = lastDropDownItemIndex;
            lastDropDownItemIndex = loadedModBox.SelectedIndex;
        }
    }
}
