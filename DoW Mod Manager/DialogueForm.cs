using System;
using System.Windows.Forms;

namespace DoW_Mod_Manager
{
    public partial class DialogueForm : Form
    {
        readonly string exeORmods;

        public DialogueForm(string message, string title, string exeORmods)
        {
            InitializeComponent();

            this.exeORmods = exeORmods;

            dmessageLabel.Text = message;
            Text = title;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            if (exeORmods == "exe")
                DownloadHelper.DownloadUpdate();
            else if (exeORmods == "mods")
                DownloadHelper.DownloadModlist();

            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    /// <summary>
    /// A custom DialogueBox helper.
    /// </summary>
    public static class ThemedDialogueBox
    {
        public static DialogResult Show(string message, string title = " ", string exeORmods = "exe")
        {
            // "using" construct ensures the resources are freed when form is closed
            using (DialogueForm form = new DialogueForm(message, title, exeORmods))
            {
                return form.ShowDialog();
            }
        }
    }
}
