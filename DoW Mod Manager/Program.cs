using System;
using System.Windows.Forms;

namespace DoW_Mod_Manager
{
    static class Program
    {
        /// <summary>
        /// This method properly terminates the Application
        /// </summary>
        public static void TerminateApp()
        {
            if (Application.MessageLoop)
            {
                // it's an WinForms app
                Application.Exit();
            }
            else
            {
                // It's a Console app
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ModManagerForm());
        }
    }
}