using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DoW_Mod_Manager
{
    public partial class SystemPerformanceManagerForm : Form
    {
        private const string REG_COMPATIBILITY_PATH = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers";
        private readonly string REG_DOW_PATH;

        private const string REG_VALUE_FULLSCREEN_OPTIMIZATIONS = "DISABLEDXMAXIMIZEDWINDOWEDMODE";
        private const string REG_VALUE_RUN_AS_ADMIN = "RUNASADMIN";
        private const string REG_VALUE_HDPI_AWARE = "HIGHDPIAWARE";
        private const string REG_VALUE_COMPATIBILITY_WITH = "WINXPSP2";

        private const string REG_POWER_SCHEMES_PATH = @"SYSTEM\CurrentControlSet\Control\Power\User\PowerSchemes";

        private const string NAME_ULTIMATE_PERFORMANCE = "Ultimate Performance";
        private const string GUID_ULTIMATE_PERFORMANCE = "e9a42b02-d5df-448d-aa00-03f14749eb61";
        private const string GUID_ULTIMATE_PERFORMANCE_2 = "46742d2f-42d6-41df-a6ed-b49d182ee15d";

        private const string NAME_MAX_PERFORMANCE = "High Performance";
        private const string GUID_MAX_PERFORMANCE = "8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c";

        private const string NAME_BALANCED = "Balanced";
        private const string GUID_BALANCED = "381b4222-f694-41f0-9685-ff5bb260df2e";

        private const string NAME_POWER_SAVER = "Power Saver";
        private const string GUID_POWER_SAVER = "a1841308-3541-4fab-bc81-f71556f20b4a";

        private readonly ModManagerForm modManager;
        private readonly Timer timer;
        private bool modifyRegistry = false;
        private readonly int ultimatePerformanceGUIDIndex = 0;

        public SystemPerformanceManagerForm(ModManagerForm form)
        {
            InitializeComponent();

            modManager = form;

            timer = new Timer()
            {
                Enabled = true,
                Interval = 1000
            };
            timer.Tick += new EventHandler(TimerTick);

            // Use the same icon as executable
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            TimerCaps caps = WinApiCalls.QueryTimerResolution();
            minimumTimerResolutionTextBox.Text = caps.PeriodMax / 10000.0 + " ms";
            maximumTimerResolutionTextBox.Text = caps.PeriodMin / 10000.0 + " ms";
            currentTimerResolutionTextBox.Text = caps.PeriodCurrent / 10000.0 + " ms";

            if (caps.PeriodCurrent / 10000.0 < 0.8)
                modManager.IsTimerResolutionLowered = true;

            REG_DOW_PATH = modManager.CurrentDir + "\\" + modManager.CurrentGameEXE;

            // We are checking for Compatibility settings in Registry
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(REG_COMPATIBILITY_PATH, false))
            {
                if (key != null)
                {
                    object valueObject = key.GetValue(REG_DOW_PATH);

                    if (valueObject != null)
                    {
                        string value = valueObject.ToString();

                        if (value.Contains(REG_VALUE_FULLSCREEN_OPTIMIZATIONS))
                            fullscreenOptimizationsCheckBox.Checked = true;
                        if (value.Contains(REG_VALUE_RUN_AS_ADMIN))
                            runAsAdministratorCheckBox.Checked = true;
                        if (value.Contains(REG_VALUE_HDPI_AWARE))
                            HDPIiScalingCheckBox.Checked = true;
                        if (value.Contains(REG_VALUE_COMPATIBILITY_WITH))
                            comatibilityModeCheckBox.Checked = true;
                    }
                }
            }

            // We are checking for Power Settings in Registry
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(REG_POWER_SCHEMES_PATH + "\\" + GUID_ULTIMATE_PERFORMANCE_2, false))
            {
                if (key != null)
                {
                    powerPlanComboBox.Items.Add(NAME_ULTIMATE_PERFORMANCE);
                    unlockUltimatePerformanceButton.Enabled = false;
                    ultimatePerformanceGUIDIndex = 2;
                }
            }
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(REG_POWER_SCHEMES_PATH + "\\" + GUID_ULTIMATE_PERFORMANCE, false))
            {
                if (key != null && ultimatePerformanceGUIDIndex == 0)
                {
                    powerPlanComboBox.Items.Add(NAME_ULTIMATE_PERFORMANCE);
                    unlockUltimatePerformanceButton.Enabled = false;
                    ultimatePerformanceGUIDIndex = 1;
                }
            }
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(REG_POWER_SCHEMES_PATH + "\\" + GUID_MAX_PERFORMANCE, false))
            {
                if (key != null)
                    powerPlanComboBox.Items.Add(NAME_MAX_PERFORMANCE);
            }
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(REG_POWER_SCHEMES_PATH + "\\" + GUID_BALANCED, false))
            {
                if (key != null)
                    powerPlanComboBox.Items.Add(NAME_BALANCED);
            }
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(REG_POWER_SCHEMES_PATH + "\\" + GUID_POWER_SAVER, false))
            {
                if (key != null)
                    powerPlanComboBox.Items.Add(NAME_POWER_SAVER);
            }
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(REG_POWER_SCHEMES_PATH, false))
            {
                if (key != null)
                {
                    object valueObject = key.GetValue("ActivePowerScheme");

                    if (valueObject != null)
                    {
                        string value = valueObject.ToString();

                        switch (value)
                        {
                            case GUID_ULTIMATE_PERFORMANCE:
                            case GUID_ULTIMATE_PERFORMANCE_2:
                                powerPlanComboBox.SelectedItem = NAME_ULTIMATE_PERFORMANCE;
                                break;
                            case GUID_MAX_PERFORMANCE:
                                powerPlanComboBox.SelectedItem = NAME_MAX_PERFORMANCE;
                                break;
                            case GUID_BALANCED:
                                powerPlanComboBox.SelectedItem = NAME_BALANCED;
                                break;
                            case GUID_POWER_SAVER:
                                powerPlanComboBox.SelectedItem = NAME_POWER_SAVER;
                                break;
                        }
                    }
                }
            }

            // We have to add those methods to the EventHandler here so we could avoid accidental firing of those methods after we would change the state of the CheckBox
            runAsAdministratorCheckBox.CheckedChanged += new EventHandler(CheckBox_CheckedChanged);
            HDPIiScalingCheckBox.CheckedChanged += new EventHandler(CheckBox_CheckedChanged);
            comatibilityModeCheckBox.CheckedChanged += new EventHandler(CheckBox_CheckedChanged);
            fullscreenOptimizationsCheckBox.CheckedChanged += new EventHandler(CheckBox_CheckedChanged);

            powerPlanComboBox.SelectedIndexChanged += new EventHandler(PowerPlanComboBox_SelectedIndexChanged);
        }

        private void TimerTick(object sender, EventArgs e)
        {
            TimerCaps caps = WinApiCalls.QueryTimerResolution();
            currentTimerResolutionTextBox.Text = caps.PeriodCurrent / 10000.0 + " ms";
        }

        private void SetTimerResolutionButton_Click(object sender, EventArgs e)
        {
            WinApiCalls.SetTimerResolution((uint)(0.5 * 10000));
            modManager.IsTimerResolutionLowered = true;
        }

        private void DefaultTimerResolutionButton_Click(object sender, EventArgs e)
        {
            WinApiCalls.SetTimerResolution(0, false);
            modManager.IsTimerResolutionLowered = false;
        }

        private void SetPropertiesButton_Click(object sender, EventArgs e)
        {
            if (modifyRegistry)
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(REG_COMPATIBILITY_PATH, true))
                {
                    if (key != null)
                    {
                        string newValue = "~ ";

                        if (fullscreenOptimizationsCheckBox.Checked)
                            newValue += " " + REG_VALUE_FULLSCREEN_OPTIMIZATIONS;
                        if (runAsAdministratorCheckBox.Checked)
                            newValue += " " + REG_VALUE_RUN_AS_ADMIN;
                        if (HDPIiScalingCheckBox.Checked)
                            newValue += " " + REG_VALUE_HDPI_AWARE;
                        if (comatibilityModeCheckBox.Checked)
                            newValue += " " + REG_VALUE_COMPATIBILITY_WITH;

                        key.SetValue(REG_DOW_PATH, newValue, RegistryValueKind.String);
                    }
                }
                setPropertiesButton.Enabled = false;
            }
        }

        private void OpenDoWPropertiesButton_Click(object sender, EventArgs e)
        {
            WinApiCalls.ShowFileProperties(modManager.CurrentGameEXE);
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            modifyRegistry = true;
            setPropertiesButton.Enabled = true;
        }

        private void PowerPlanComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            setPowerPlanButton.Enabled = true;
        }

        private void SetPowerPlanButton_Click(object sender, EventArgs e)
        {
            Guid powerPlanGUID = new Guid(GUID_BALANCED);

            switch (powerPlanComboBox.SelectedItem.ToString())
            {
                case NAME_ULTIMATE_PERFORMANCE:
                    if (ultimatePerformanceGUIDIndex == 2)
                        powerPlanGUID = new Guid(GUID_ULTIMATE_PERFORMANCE_2);
                    else
                        powerPlanGUID = new Guid(GUID_ULTIMATE_PERFORMANCE);
                    break;
                case NAME_MAX_PERFORMANCE:
                    powerPlanGUID = new Guid(GUID_MAX_PERFORMANCE);
                    break;
                case NAME_BALANCED:
                    powerPlanGUID = new Guid(GUID_BALANCED);
                    break;
                case NAME_POWER_SAVER:
                    powerPlanGUID = new Guid(GUID_POWER_SAVER);
                    break;
            }

            WinApiCalls.PowerSetActiveScheme(IntPtr.Zero, ref powerPlanGUID);

            setPowerPlanButton.Enabled = false;
        }

        private void UnlockUltimatePerformanceButton_Click(object sender, EventArgs e)
        {
            Process.Start("powercfg.exe", "-duplicatescheme e9a42b02-d5df-448d-aa00-03f14749eb61");

            unlockUltimatePerformanceButton.Enabled = false;
        }

        private void PowerOptionsButton_Click(object sender, EventArgs e)
        {
            var root = Environment.GetEnvironmentVariable("SystemRoot");
            Process.Start(root + "\\system32\\control.exe", "/name Microsoft.PowerOptions");
        }
    }
}
