using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoW_Mod_Manager
{
    public partial class DialogueForm : Form
    {
        public DialogueForm(string message, string title)
        {
            InitializeComponent();

            dmessageLabel.Text = message;
            Text = title;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            DownloadHelper.DownloadUpdate();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    /// <summary>
    /// Your custom dialogue box helper.
    /// </summary>
    public static class ThemedDialogueBox
    {
        public static void Show(string message, string title = "")
        {
            // "using" construct ensures the resources are freed when form is closed
            using (DialogueForm form = new DialogueForm(message, title))
            {
                form.ShowDialog();
            }
        }
    }
}
