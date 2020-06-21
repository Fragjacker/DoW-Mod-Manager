using System;
using System.Drawing;
using System.Windows.Forms;

namespace DoW_Mod_Manager
{
    public partial class SystemPerformanceForm : Form
    {
        private Timer timer;

        public SystemPerformanceForm()
        {
            InitializeComponent();

            timer = new Timer()
            {
                Enabled = true,
                Interval = 1000
            };
            timer.Tick += new EventHandler(TimerTick);

            // Use the same icon as executable
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        }

        private void TimerTick(object sender, EventArgs e)
        {
            TimerTool.TimerCaps caps = TimerTool.WinApiCalls.QueryTimerResolution();
            currentTimerResolutionTextBox.Text = caps.PeriodCurrent / 10000.0 + " ms";

            //CurrentLabel.Text = "Current: " + (caps.PeriodCurrent / 10000.0) + " ms";
            //MinLabel.Text = "Max: " + (caps.PeriodMin / 10000.0) + " ms";
            //MaxLabel.Text = "Min: " + (caps.PeriodMax / 10000.0) + " ms";
        }

        private void SetTimerResolutionButton_Click(object sender, EventArgs e)
        {
            TimerTool.WinApiCalls.SetTimerResolution((uint)(0.5 * 10000));
        }

        private void DefaultTimerResolutionButton_Click(object sender, EventArgs e)
        {
            TimerTool.WinApiCalls.SetTimerResolution(0, false);
        }
    }
}
