using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace DoW_Mod_Manager
{
    public partial class SettingsManagerForm : Form
    {
        private const string SETTINGS_FILE = "Local.ini";

        // Here are all settings from Local.ini in right order
        private const string CAMERA_DETAIL = "cameradetail";
        private const string CURRENT_MOD = "currentmoddc";
        private const string DYNAMIC_LIGHTS = "dynamiclights";
        private const string EVENT_DETAIL_LEVEL = "event_detail_level";
        private const string FORCE_WATCH_MOVIES = "force_watch_movies";
        private const string FULLRES_TEAMCOLOUR = "fullres_teamcolour";
        private const string FX_DETAIL_LEVEL = "fx_detail_level";
        private const string MODEL_DETAIL = "modeldetail";
        private const string PARENTAL_CONTROL = "parentalcontrol";
        private const string PERSISTENT_BODIES = "persistent_bodies";
        private const string PERSISTENT_DECALS = "persistent_decals";
        private const string PLAYER_PROFILE = "playerprofile";
        private const string RL_SSO_NUM_TIMES_SHOWN = "rl_sso_num_times_shown";
        private const string SCREEN_ADAPTER = "screenadapter";
        private const string SCREEN_ANIALIAS = "screenantialias";
        private const string SCREEN_DEPTH = "screendepth";
        private const string SCREEN_DEVICE = "screendevice";
        private const string SCREEN_GAMMA = "screengamma";
        private const string SCREEN_HEIGHT = "screenheight";
        private const string SCREEN_NO_VSYNC = "screennovsync";
        private const string SCREEN_REFRESH = "screenrefresh";
        private const string SCREEN_WIDTH = "screenwidth";
        private const string SCREEN_WINDOWED = "screenwindowed";
        private const string SHADOW_BLOB = "shadowblob";
        private const string SHADOW_MAP = "shadowmap";
        private const string SHADOW_VOLUME = "shadowvolume";
        private const string SOUND_ENABLED = "sound_enabled";
        private const string SOUND_LIMIT_SAMPLES = "sound_limitsamples";
        private const string SOUND_NR_CHANNELS = "sound_nrchannels";
        private const string SOUND_QUALITY = "sound_quality";
        private const string TERRAIN_ENABLE_FOW_BLUR = "terrainenablefowblur";
        private const string TEXTURE_DETAIL = "texturedetail";
        private const string TOTAL_MATCHES = "total_matches";
        private const string UNIT_OCCLUSION = "unit_occlusion";

        // Gere are all the default values. Empty strings are not mistakes - those strings are absent by default
        private readonly Dictionary<string, string> settings = new Dictionary<string, string>();
        //private readonly Dictionary<string, string> settings = new Dictionary<string, string>
        //{
        //    [CAMERA_DETAIL] = "1",
        //    [CURRENT_MOD] = "dxp2",
        //    [DYNAMIC_LIGHTS] = "3",
        //    [EVENT_DETAIL_LEVEL] = "2",
        //    [FORCE_WATCH_MOVIES] = "1",
        //    [FULLRES_TEAMCOLOUR] = "0",
        //    [FX_DETAIL_LEVEL] = "2",
        //    [MODEL_DETAIL] = "2",
        //    [PARENTAL_CONTROL] = "0",
        //    [PERSISTENT_BODIES] = "3",
        //    [PERSISTENT_DECALS] = "2",
        //    [PLAYER_PROFILE] = "Profile1",
        //    [RL_SSO_NUM_TIMES_SHOWN] = "1",
        //    [SCREEN_ADAPTER] = "0",
        //    [SCREEN_ANIALIAS] = "0",
        //    [SCREEN_DEPTH] = "32",
        //    [SCREEN_DEVICE] = "Dx9 : Hardware TnL",
        //    [SCREEN_GAMMA] = "10",
        //    [SCREEN_HEIGHT] = "1080",
        //    [SCREEN_NO_VSYNC] = "0",
        //    [SCREEN_REFRESH] = "0",
        //    [SCREEN_WIDTH] = "1920",
        //    [SCREEN_WINDOWED] = "0",
        //    [SHADOW_BLOB] = "1",
        //    [SHADOW_MAP] = "1",
        //    [SHADOW_VOLUME] = "1",
        //    [SOUND_ENABLED] = "1",
        //    [SOUND_LIMIT_SAMPLES] = "0",
        //    [SOUND_NR_CHANNELS] = "64",
        //    [SOUND_QUALITY] = "2",
        //    [TERRAIN_ENABLE_FOW_BLUR] = "1",
        //    [TEXTURE_DETAIL] = "2",
        //    [TOTAL_MATCHES] = "0",
        //    [UNIT_OCCLUSION] = "0"
        //};

        public SettingsManagerForm()
        {
            InitializeComponent();

            string[] lines = File.ReadAllLines(SETTINGS_FILE);
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                line = line.Trim();

                int firstIndexOfEqualSign = line.IndexOf('=');
                int lastIndexOfEqualSign = line.LastIndexOf('=');

                // There must be only one "=" in the line!
                if (firstIndexOfEqualSign == lastIndexOfEqualSign)
                {
                    if (firstIndexOfEqualSign > 0)
                    {
                        string setting = line.Substring(0, firstIndexOfEqualSign);
                        string value = line.Substring(firstIndexOfEqualSign + 1, line.Length - firstIndexOfEqualSign - 1);

                        switch (setting)
                        {
                            case CAMERA_DETAIL:
                                if (value == "0" || value == "1")
                                    settings[CAMERA_DETAIL] = value;
                                break;
                            case CURRENT_MOD:
                                settings[CURRENT_MOD] = value;
                                break;
                            case DYNAMIC_LIGHTS:
                                if (value == "0" || value == "1" || value == "2" || value == "3")
                                    settings[DYNAMIC_LIGHTS] = value;
                                break;
                            case EVENT_DETAIL_LEVEL:
                                if (value == "0" || value == "1" || value == "2")
                                    settings[EVENT_DETAIL_LEVEL] = value;
                                break;
                            case FORCE_WATCH_MOVIES:
                                if (value == "0" || value == "1")
                                    settings[FORCE_WATCH_MOVIES] = value;
                                break;
                            case FULLRES_TEAMCOLOUR:
                                if (value == "0" || value == "1")
                                    settings[FULLRES_TEAMCOLOUR] = value;
                                break;
                            case FX_DETAIL_LEVEL:
                                if (value == "0" || value == "1" || value == "2")
                                    settings[FX_DETAIL_LEVEL] = value;
                                break;
                            case MODEL_DETAIL:
                                if (value == "0" || value == "1" || value == "2")
                                    settings[MODEL_DETAIL] = value;
                                break;
                            case PARENTAL_CONTROL:
                                if (value == "0" || value == "1")
                                    settings[PARENTAL_CONTROL] = value;
                                break;
                            case PERSISTENT_BODIES:
                                if (value == "0" || value == "1" || value == "2" || value == "3")
                                    settings[PERSISTENT_BODIES] = value;
                                break;
                            case PERSISTENT_DECALS:
                                if (value == "0" || value == "1" || value == "2")
                                    settings[PERSISTENT_DECALS] = value;
                                break;
                            case PLAYER_PROFILE:
                                if (value.Contains("Profile"))
                                    settings[PLAYER_PROFILE] = value;
                                break;
                            case RL_SSO_NUM_TIMES_SHOWN:
                                if (Convert.ToInt32(value) >= 0)
                                    settings[RL_SSO_NUM_TIMES_SHOWN] = value;
                                break;
                            case SCREEN_ADAPTER:
                                if (Convert.ToInt32(value) >= 0)
                                    settings[SCREEN_ADAPTER] = value;
                                break;
                            case SCREEN_ANIALIAS:
                                if (value == "0" || value == "1")
                                    settings[SCREEN_ANIALIAS] = value;
                                break;
                            case SCREEN_DEPTH:
                                if (value == "16" || value == "24" || value == "32")
                                    settings[SCREEN_DEPTH] = value;
                                break;
                            case SCREEN_DEVICE:
                                if (value == "Dx9 : Hardware TnL")
                                    settings[SCREEN_DEVICE] = value;
                                break;
                            case SCREEN_GAMMA:
                                if (Convert.ToInt32(value) >= 0 && Convert.ToInt32(value) <= 30)        // Range must be tested!
                                    settings[SCREEN_GAMMA] = value;
                                break;
                            case SCREEN_HEIGHT:
                                if (value == "600" || value == "664" || value == "720" || value == "768" || value == "800" || value == "960" || value == "1024" || value == "1050" || value == "1080" || value == "1440" || value == "2160")
                                    settings[SCREEN_HEIGHT] = value;
                                break;
                            case SCREEN_NO_VSYNC:
                                if (value == "0" || value == "1")
                                    settings[SCREEN_NO_VSYNC] = value;
                                break;
                            case SCREEN_REFRESH:
                                if (value == "0" || value == "59" || value == "60" || value == "120" || value == "144")
                                    settings[SCREEN_REFRESH] = value;
                                break;
                            case SCREEN_WIDTH:
                                if (value == "800" || value == "1024" || value == "1152" || value == "1176" || value == "1280" || value == "1366" || value == "1400" || value == "1440" || value == "1600" || value == "1680" || value == "1920" || value == "2560" || value == "4096")
                                    settings[SCREEN_WIDTH] = value;
                                break;
                            case SCREEN_WINDOWED:
                                if (value == "0" || value == "1")
                                    settings[SCREEN_WINDOWED] = value;
                                break;
                            case SHADOW_BLOB:
                                if (value == "0" || value == "1")
                                    settings[SHADOW_BLOB] = value;
                                break;
                            case SHADOW_MAP:
                                if (value == "0" || value == "1")
                                    settings[SHADOW_MAP] = value;
                                break;
                            case SHADOW_VOLUME:
                                if (value == "0" || value == "1")
                                    settings[SHADOW_VOLUME] = value;
                                break;
                            case SOUND_ENABLED:
                                if (value == "0" || value == "1")
                                    settings[SOUND_ENABLED] = value;
                                break;
                            case SOUND_LIMIT_SAMPLES:
                                if (value == "0" || value == "1")
                                    settings[SOUND_LIMIT_SAMPLES] = value;
                                break;
                            case SOUND_NR_CHANNELS:
                                if (Convert.ToInt32(value) > 0 && Convert.ToInt32(value) < 65)        // Range must be tested!
                                    settings[SOUND_NR_CHANNELS] = value;
                                break;
                            case SOUND_QUALITY:
                                if (value == "0" || value == "1" || value == "2")
                                    settings[SOUND_QUALITY] = value;
                                break;
                            case TERRAIN_ENABLE_FOW_BLUR:
                                if (value == "0" || value == "1" || value == "2")
                                    settings[TERRAIN_ENABLE_FOW_BLUR] = value;
                                break;
                            case TEXTURE_DETAIL:
                                if (value == "0" || value == "1" || value == "2")
                                    settings[TEXTURE_DETAIL] = value;
                                break;
                            case TOTAL_MATCHES:
                                if (Convert.ToInt32(value) >= 0)
                                    settings[TOTAL_MATCHES] = value;
                                break;
                            case UNIT_OCCLUSION:
                                if (value == "0" || value == "1")
                                    settings[UNIT_OCCLUSION] = value;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            // This is for debuging
            string text = "Parsed pairs of key=value\n\n";
            foreach (var kvp in settings)
            {
                text += kvp.Key + "=" + kvp.Value + "\n";
            }
            MessageBox.Show(text, "Debug Info");

            // Now we could set all ComboBoxes (METALLBAWHKSESS!!!) and CheckBoxes in our Form
            // Fun fact: Convert.ToBoolean("true") works but Convert.ToBoolean("1") fails. Only Convert.ToBoolean(1) is a good alternative
            full3DCameraCheckBox.Checked = Convert.ToBoolean(Convert.ToInt32(settings[CAMERA_DETAIL]));
            // Skipp CurrtentMod setting
            dynamicLightsComboBox.SelectedIndex = Convert.ToInt32(settings[DYNAMIC_LIGHTS]);
            worldEventsComboBox.SelectedIndex = Convert.ToInt32(settings[EVENT_DETAIL_LEVEL]);
            watchMoviesCheckBox.Checked = Convert.ToBoolean(Convert.ToInt32(settings[FORCE_WATCH_MOVIES]));
            betterTeamcoloredTexturexCheckBox.Checked = Convert.ToBoolean(Convert.ToInt32(settings[FULLRES_TEAMCOLOUR]));
            effectsDetailComboBox.SelectedIndex = Convert.ToInt32(settings[FX_DETAIL_LEVEL]);
            modelDetailComboBox.SelectedIndex = Convert.ToInt32(settings[MODEL_DETAIL]);
            parentalControlCheckBox.Checked = Convert.ToBoolean(Convert.ToInt32(settings[PARENTAL_CONTROL]));
            persistentBodiesComboBox.SelectedIndex = Convert.ToInt32(settings[PERSISTENT_BODIES]);
            persistentScarringComboBox.SelectedIndex = Convert.ToInt32(settings[PERSISTENT_DECALS]);
            currentPlayerComboBox.SelectedItem = settings[PLAYER_PROFILE];
            unknownSettingComboBox.SelectedItem = settings[RL_SSO_NUM_TIMES_SHOWN];
            activeVideocardComboBox.SelectedItem = settings[SCREEN_ADAPTER];
            antialiasingCheckBox.Checked = Convert.ToBoolean(Convert.ToInt32(settings[SCREEN_ANIALIAS]));
            switch (settings[SCREEN_DEPTH])
            {
                case "32":
                    colorDepthComboBox.SelectedIndex = 2;
                    break;
                case "24":
                    colorDepthComboBox.SelectedIndex = 1;
                    break;
                case "16":
                    colorDepthComboBox.SelectedIndex = 0;
                    break;
            }
            rendererComboBox.SelectedItem = settings[SCREEN_DEVICE];
            gammaTrackBar.Value = Convert.ToInt32(settings[SCREEN_GAMMA]);
            screenResolutionComboBox.SelectedItem = settings[SCREEN_WIDTH] + "×" + settings[SCREEN_HEIGHT];
            vSyncCheckBox.Checked = Convert.ToBoolean(Convert.ToInt32(settings[SCREEN_NO_VSYNC]));
            if (settings[SCREEN_REFRESH] == "0")
                refreshRateComboBox.SelectedItem = "Auto";
            else
                refreshRateComboBox.SelectedItem = settings[SCREEN_REFRESH] + " Hz";
            windowedCheckBox.Checked = Convert.ToBoolean(Convert.ToInt32(settings[SCREEN_WINDOWED]));
            int index = 0;
            if (settings[SHADOW_BLOB] == "1")
            {
                index = 1;
                if (settings[SHADOW_MAP] == "1")
                {
                    index = 2;
                    if (settings[SHADOW_VOLUME] == "1")
                        index = 3;
                }
            }
            shadowsDetailComboBox.SelectedIndex = index;
            soundEnabledCheckBox.Checked = Convert.ToBoolean(Convert.ToInt32(settings[SOUND_ENABLED]));
            randomizedSoundsCheckBox.Checked = Convert.ToBoolean(Convert.ToInt32(settings[SOUND_LIMIT_SAMPLES]));
            switch (settings[SOUND_NR_CHANNELS])
            {
                case "64":
                    soundChannelsComboBox.SelectedIndex = 2;
                    break;
                case "32":
                    soundChannelsComboBox.SelectedIndex = 1;
                    break;
                case "16":
                    soundChannelsComboBox.SelectedIndex = 0;
                    break;
            }
            soundQualityComboBox.SelectedIndex = Convert.ToInt32(settings[SOUND_QUALITY]);
            terrainDetailComboBox.SelectedIndex = Convert.ToInt32(settings[TERRAIN_ENABLE_FOW_BLUR]);
            textureDetailComboBox.SelectedIndex = Convert.ToInt32(settings[TEXTURE_DETAIL]);
            // Skip TotalMatchces setting
            unitsOcclusionCheckBox.Checked = Convert.ToBoolean(Convert.ToInt32(settings[UNIT_OCCLUSION]));
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            string str = $"[global]\n" +
             $"{CAMERA_DETAIL}={settings[CAMERA_DETAIL]}\n" +
             $"{CURRENT_MOD}={settings[CURRENT_MOD]}\n" +
             $"{DYNAMIC_LIGHTS}={settings[DYNAMIC_LIGHTS]}\n" +
             $"{EVENT_DETAIL_LEVEL}={settings[EVENT_DETAIL_LEVEL]}\n" +
             $"{FORCE_WATCH_MOVIES}={settings[FORCE_WATCH_MOVIES]}\n" +
             $"{FULLRES_TEAMCOLOUR}={settings[FULLRES_TEAMCOLOUR]}\n" +
             $"{FX_DETAIL_LEVEL}={settings[FX_DETAIL_LEVEL]}\n" +
             $"{MODEL_DETAIL}={settings[MODEL_DETAIL]}\n" +
             $"{PARENTAL_CONTROL}={settings[PARENTAL_CONTROL]}\n" +
             $"{PERSISTENT_BODIES}={settings[PERSISTENT_BODIES]}\n" +
             $"{PERSISTENT_DECALS}={settings[PERSISTENT_DECALS]}\n" +
             $"{PLAYER_PROFILE}={settings[PLAYER_PROFILE]}\n" +
             $"{RL_SSO_NUM_TIMES_SHOWN}={settings[RL_SSO_NUM_TIMES_SHOWN]}\n" +
             $"{SCREEN_ADAPTER}={settings[SCREEN_ADAPTER]}\n" +
             $"{SCREEN_ANIALIAS}={settings[SCREEN_ANIALIAS]}\n" +
             $"{SCREEN_DEPTH}={settings[SCREEN_DEPTH]}\n" +
             $"{SCREEN_DEVICE}={settings[SCREEN_DEVICE]}\n" +
             $"{SCREEN_GAMMA}={settings[SCREEN_GAMMA]}\n" +
             $"{SCREEN_HEIGHT}={settings[SCREEN_HEIGHT]}\n" +
             $"{SCREEN_NO_VSYNC}={settings[SCREEN_NO_VSYNC]}\n" +
             $"{SCREEN_REFRESH}={settings[SCREEN_REFRESH]}\n" +
             $"{SCREEN_WIDTH}={settings[SCREEN_WIDTH]}\n" +
             $"{SCREEN_WINDOWED}={settings[SCREEN_WINDOWED]}\n" +
             $"{SHADOW_BLOB}={settings[SHADOW_BLOB]}\n" +
             $"{SHADOW_MAP}={settings[SHADOW_MAP]}\n" +
             $"{SHADOW_VOLUME}={settings[SHADOW_VOLUME]}\n" +
             $"{SOUND_ENABLED}={settings[SOUND_ENABLED]}\n" +
             $"{SOUND_LIMIT_SAMPLES}={settings[SOUND_LIMIT_SAMPLES]}\n" +
             $"{SOUND_NR_CHANNELS}={settings[SOUND_NR_CHANNELS]}\n" +
             $"{SOUND_QUALITY}={settings[SOUND_QUALITY]}\n" +
             $"{TERRAIN_ENABLE_FOW_BLUR}={settings[TERRAIN_ENABLE_FOW_BLUR]}\n" +
             $"{TEXTURE_DETAIL}={settings[TEXTURE_DETAIL]}\n" +
             $"{TOTAL_MATCHES}={settings[TOTAL_MATCHES]}\n" +
             $"{UNIT_OCCLUSION}={settings[UNIT_OCCLUSION]}";
            File.WriteAllText(SETTINGS_FILE, str);
        }

        private void DefaultsButton_Click(object sender, EventArgs e)
        {

        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CurrentPlayerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings[PLAYER_PROFILE] = currentPlayerComboBox.SelectedItem.ToString();
        }

        private void ParentalControlCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (parentalControlCheckBox.Checked)
                settings[PARENTAL_CONTROL] = "1";
            else
                settings[PARENTAL_CONTROL] = "0";
        }

        private void InversePanCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // Can't find this setting!
        }

        private void InverseDeclinationCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // Can't find this setting!
        }

        private void ScrollRateTrackBar_Scroll(object sender, EventArgs e)
        {
            // Can't find this setting!
        }

        private void UnknownSettingComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings[RL_SSO_NUM_TIMES_SHOWN] = unknownSettingComboBox.SelectedItem.ToString();
        }

        private void ActiveVideocardComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings[SCREEN_ADAPTER] = activeVideocardComboBox.SelectedItem.ToString();
        }

        private void RendererComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings[SCREEN_DEVICE] = rendererComboBox.SelectedItem.ToString();
        }

        private void ScreenResolutionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = screenResolutionComboBox.SelectedItem.ToString();
            int x = str.IndexOf('×');

            settings[SCREEN_WIDTH] = str.Substring(0, x);
            settings[SCREEN_HEIGHT] = str.Substring(x + 1, str.Length - x - 1);
        }

        private void RefreshRateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = refreshRateComboBox.SelectedItem.ToString();
            int indexOfSpace = str.IndexOf(" ");

            if (str == "Auto")
                settings[SCREEN_REFRESH] = "0";
            else
                settings[SCREEN_REFRESH] = str.Substring(0, indexOfSpace);
        }

        private void ColorDepthComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (colorDepthComboBox.SelectedIndex)
            {
                case 2:
                    settings[SCREEN_DEPTH] = "32";
                    break;
                case 1:
                    settings[SCREEN_DEPTH] = "24";
                    break;
                case 0:
                    settings[SCREEN_DEPTH] = "16";
                    break;
            }
        }

        private void GammaTrackBar_Scroll(object sender, EventArgs e)
        {
            settings[SCREEN_GAMMA] = gammaTrackBar.Value.ToString();
        }

        private void VSyncCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (vSyncCheckBox.Checked)
                settings[SCREEN_NO_VSYNC] = "1";
            else
                settings[SCREEN_NO_VSYNC] = "0";
        }

        private void WindowedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (windowedCheckBox.Checked)
                settings[SCREEN_WINDOWED] = "1";
            else
                settings[SCREEN_WINDOWED] = "0";
        }

        private void AntialiasingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (antialiasingCheckBox.Checked)
                settings[SCREEN_ANIALIAS] = "1";
            else
                settings[SCREEN_ANIALIAS] = "0";
        }

        private void TextureDetailComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings[TEXTURE_DETAIL] = textureDetailComboBox.SelectedIndex.ToString();
        }

        private void ModelDetailComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings[MODEL_DETAIL] = modelDetailComboBox.SelectedIndex.ToString();
        }

        private void TerrainDetailComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings[TERRAIN_ENABLE_FOW_BLUR] = terrainDetailComboBox.SelectedIndex.ToString();
        }

        private void BetterTeamcoloredTexturexCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (betterTeamcoloredTexturexCheckBox.Checked)
                settings[FULLRES_TEAMCOLOUR] = "1";
            else
                settings[FULLRES_TEAMCOLOUR] = "0";
        }

        private void ShadowsDetailComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Using goto we could fall through even if case is not empty!
            switch (shadowsDetailComboBox.SelectedIndex)
            {
                case 0:
                    settings[SHADOW_BLOB] = "0";
                    settings[SHADOW_MAP] = "0";
                    settings[SHADOW_VOLUME] = "0";
                    break;
                case 3:
                    settings[SHADOW_VOLUME] = "1";
                    goto case 2;
                case 2:
                    settings[SHADOW_MAP] = "1";
                    goto case 1;
                case 1:
                    settings[SHADOW_BLOB] = "1";
                    break;
            }
        }

        private void WorldEventsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings[EVENT_DETAIL_LEVEL] = worldEventsComboBox.SelectedIndex.ToString();
        }

        private void EffectsDetailComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings[FX_DETAIL_LEVEL] = effectsDetailComboBox.SelectedIndex.ToString();
        }

        private void PersistentBodiesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings[PERSISTENT_BODIES] = persistentBodiesComboBox.SelectedIndex.ToString();
        }

        private void PersistentScarringComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings[PERSISTENT_DECALS] = persistentScarringComboBox.SelectedIndex.ToString();
        }

        private void DynamicLightsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings[DYNAMIC_LIGHTS] = dynamicLightsComboBox.SelectedIndex.ToString();
        }

        private void Full3DCameraCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (full3DCameraCheckBox.Checked)
                settings[CAMERA_DETAIL] = "1";
            else
                settings[CAMERA_DETAIL] = "0";
        }

        private void UnitsOcclusionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (unitsOcclusionCheckBox.Checked)
                settings[UNIT_OCCLUSION] = "1";
            else
                settings[UNIT_OCCLUSION] = "0";
        }

        private void WatchMoviesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (watchMoviesCheckBox.Checked)
                settings[FORCE_WATCH_MOVIES] = "1";
            else
                settings[FORCE_WATCH_MOVIES] = "0";
        }

        private void SoundEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (soundEnabledCheckBox.Checked)
                settings[SOUND_ENABLED] = "1";
            else
                settings[SOUND_ENABLED] = "0";
        }

        private void RandomizedSoundsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (randomizedSoundsCheckBox.Checked)
                settings[SOUND_LIMIT_SAMPLES] = "1";
            else
                settings[SOUND_LIMIT_SAMPLES] = "0";
        }

        private void SoundQualityComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings[SOUND_QUALITY] = soundQualityComboBox.SelectedIndex.ToString();
        }

        private void SoundChannelsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (soundChannelsComboBox.SelectedIndex)
            {
                case 2:
                    settings[SOUND_NR_CHANNELS] = "64";
                    break;
                case 1:
                    settings[SOUND_NR_CHANNELS] = "32";
                    break;
                case 0:
                    settings[SOUND_NR_CHANNELS] = "16";
                    break;
            }
        }

        private void AmbientVolumeTarckBar_Scroll(object sender, EventArgs e)
        {
            // Can't find this setting!
        }

        private void EffectsVolumeTrackBar_Scroll(object sender, EventArgs e)
        {
            // Can't find this setting!
        }

        private void VoiceVolumeTrackBar_Scroll(object sender, EventArgs e)
        {
            // Can't find this setting!
        }

        private void MusicVolumeTrackBar_Scroll(object sender, EventArgs e)
        {
            // Can't find this setting!
        }
    }
}