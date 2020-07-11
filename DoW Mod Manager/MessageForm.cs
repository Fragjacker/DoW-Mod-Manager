using System.Windows.Forms;

namespace DoW_Mod_Manager
{
    internal partial class MessageForm : Form
    {
        public MessageForm(string message, string title)
        {
            InitializeComponent();

            messageLabel.Text = message;
            Text = title;
        }

        private void OKButton_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }

    /// <summary>
    /// Your custom message box helper.
    /// </summary>
    public static class ThemedMessageBox
    {
        public static void Show(string message, string title = "")
        {
            // "using" construct ensures the resources are freed when form is closed
            using (MessageForm form = new MessageForm(message, title))
            {
                form.ShowDialog();
            }

            // TODO: this way may be faster - you have to test it!
            //MessageForm form = new MessageForm(message, title);
            //form.ShowDialog();
            //form.Dispose();
        }
    }
}
