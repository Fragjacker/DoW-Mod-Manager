using Microsoft.Win32;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DoW_Mod_Manager
{
    public partial class SystemPerformanceForm : Form
    {
        private const string REG_PATH = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers";
        private const string REG_KEY = @"C:\SteamGames\steamapps\common\Dawn of War Soulstorm\Soulstorm.exe";
        private const string REG_VALUE_RUN_AS_ADMIN = "RUNASADMIN";
        private const string REG_VALUE_HDPI_AWARE = "HIGHDPIAWARE";
        private const string REG_VALUE_COMPATIBILITY_WITH = "WINXPSP2";

        private readonly Timer timer;
        private bool modifyRegistry = false;

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

            TimerTool.TimerCaps caps = TimerTool.WinApiCalls.QueryTimerResolution();
            minimumTimerResolutionTextBox.Text = caps.PeriodMax / 10000.0 + " ms";
            maximumTimerResolutionTextBox.Text = caps.PeriodMin / 10000.0 + " ms";
            currentTimerResolutionTextBox.Text = caps.PeriodCurrent / 10000.0 + " ms";

            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(REG_PATH, false);

                if (key != null)
                {
                    string value = key.GetValue(REG_KEY).ToString();

                    if (value.Contains(REG_VALUE_RUN_AS_ADMIN))
                        runAsAdministratorCheckBox.Checked = true;
                    if (value.Contains(REG_VALUE_HDPI_AWARE))
                        HDPIiScalingCheckBox.Checked = true;
                    if (value.Contains(REG_VALUE_COMPATIBILITY_WITH))
                        comatibilityModeCheckBox.Checked = true;
                }

                key.Close();
            }
            catch (Exception)
            {
                // We don't have to do anything
            }

            // We have to add those methods to the EventHandler here so we could avoid accidental firing of those methods after we would change the state of the CheckBox
            runAsAdministratorCheckBox.CheckedChanged += new EventHandler(CheckBox_CheckedChanged);
            HDPIiScalingCheckBox.CheckedChanged += new EventHandler(CheckBox_CheckedChanged);
            comatibilityModeCheckBox.CheckedChanged += new EventHandler(CheckBox_CheckedChanged);
        }

        private void TimerTick(object sender, EventArgs e)
        {
            TimerTool.TimerCaps caps = TimerTool.WinApiCalls.QueryTimerResolution();
            currentTimerResolutionTextBox.Text = caps.PeriodCurrent / 10000.0 + " ms";
        }

        private void SetTimerResolutionButton_Click(object sender, EventArgs e)
        {
            TimerTool.WinApiCalls.SetTimerResolution((uint)(0.5 * 10000));
        }

        private void DefaultTimerResolutionButton_Click(object sender, EventArgs e)
        {
            TimerTool.WinApiCalls.SetTimerResolution(0, false);
        }

        private void SetPropertiesButton_Click(object sender, EventArgs e)
        {
            if (modifyRegistry)
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(REG_PATH, true))
                {
                    if (key != null)
                    {
                        string newValue = "~ ";

                        if (runAsAdministratorCheckBox.Checked)
                            newValue += " " + REG_VALUE_RUN_AS_ADMIN;
                        if (HDPIiScalingCheckBox.Checked)
                            newValue += " " + REG_VALUE_HDPI_AWARE;
                        if (comatibilityModeCheckBox.Checked)
                            newValue += " " + REG_VALUE_COMPATIBILITY_WITH;

                        key.SetValue(REG_KEY, newValue, RegistryValueKind.String);
                    }
                }
                setPropertiesButton.Enabled = false;
            }
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            modifyRegistry = true;
            setPropertiesButton.Enabled = true;
        }
    }
}
