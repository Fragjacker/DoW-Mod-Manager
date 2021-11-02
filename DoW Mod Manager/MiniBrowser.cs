using System;
using System.Windows.Forms;

namespace DoW_Mod_Manager
{
    public partial class MiniBrowser : Form
    {
        public MiniBrowser(string URL)
        {
            InitializeComponent();

            webBrowser1.ScriptErrorsSuppressed = true;

            try
            {
                webBrowser1.Navigate(new Uri(URL));
            }
            catch (Exception)
            {
                ThemedMessageBox.Show("Something wrong with this URL:\n" + URL);
            }
        }
    }
}