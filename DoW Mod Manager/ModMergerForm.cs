using System;
using System.Windows.Forms;

namespace DoW_Mod_Manager
{
    public partial class ModMergerForm : Form
    {
        private ModManagerForm ModManager;
        private object lastItem = null;
        private int lastPosition = 0;

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
            if (lastItem != null && !AvailableModsList.Items.Contains(lastItem))
            {
                AvailableModsList.Items.Insert(lastPosition, lastItem);
            }

            lastItem = loadedModBox.SelectedItem;
            lastPosition = AvailableModsList.Items.IndexOf(lastItem);

            AvailableModsList.Items.Remove(loadedModBox.SelectedItem);
        }
    }
}
