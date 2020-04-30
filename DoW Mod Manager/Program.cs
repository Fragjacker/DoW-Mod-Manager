using System;
using System.Windows.Forms;

namespace DoW_Mod_Manager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // If OS is Vista or newer - enable DPI awareness
            if (Environment.OSVersion.Version.Major >= 6)
                SetProcessDPIAware();
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ModManagerForm());
        }

        // Those two lines enables HDPI support
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}
