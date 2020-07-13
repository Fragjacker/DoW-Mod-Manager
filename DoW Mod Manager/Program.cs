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
                // WinForms app
                Application.Exit();
            }
            else
            {
                // Console app
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// This method gets a value from a line of text
        /// </summary>
        /// <param name="line"></param>
        /// <param name="deleteModule"></param>
        /// <returns>string</returns>
        public static string GetValueFromLine(string line, bool deleteModule)
        {
            int indexOfEqualSigh = line.IndexOf('=');

            if (indexOfEqualSigh > 0)
            {
                // Deleting all chars before equal sigh
                line = line.Substring(indexOfEqualSigh + 1, line.Length - indexOfEqualSigh - 1);

                if (deleteModule)
                    return line.Replace(" ", "").Replace(".module", "");
                else
                    return line.Replace(" ", "");
            }
            else
                return "";
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