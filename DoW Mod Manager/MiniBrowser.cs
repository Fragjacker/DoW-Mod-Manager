using System;
using System.Windows.Forms;
using System.Drawing;

namespace DoW_Mod_Manager
{
    public partial class MiniBrowser : Form
    {
        public MiniBrowser(string URL)
        {
            InitializeComponent();

            webBrowser1.ScriptErrorsSuppressed = true;

            // Use the same icon as executable
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            pictureBox.Image = Icon.ToBitmap();

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