using System;
using System.Windows.Forms;

namespace DoW_Mod_Manager
{
    public partial class SettingsManagerForm : Form
    {
        private const string SETTINGS_FILE = "Local.ini";
        
        public SettingsManagerForm()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {

        }

        private void DefaultsButton_Click(object sender, EventArgs e)
        {

        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
