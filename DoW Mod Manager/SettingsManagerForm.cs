using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Management;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

namespace DoW_Mod_Manager
{
    public partial class SettingsManagerForm : Form
    {
        public class Profile
        {
            public string ProfileName;
            public string PlayerName;

            public Profile(string profileName, string playerName)
            {
                ProfileName = profileName;
                PlayerName = playerName;
            }
        }
        
        private const string CANCEL_LABEL = "CANCEL";
        private const string CLOSE_LABEL = "CLOSE";

        private const string SETTINGS_FILE = "Local.ini";

        // Here are all settings from Local.ini in correct order
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

        private readonly string PROFILES_PATH;
        private const string NAME_DAT = "name.dat";
        private const string PLAYERCONFIG = "playercfg.lua";
        private const string PROFILE = "Profile";

        // Here are some usefull settings from playercfg.lua in correct order
        private const string INVERT_DECLINATION = "invertDeclination";
        private const string INVERT_PAN = "invertPan";
        private const string SCROLL_RATE = "scrollRate";

        private const string SOUND_VOLUME_AMBIENT = "VolumeAmbient";
        private const string SOUND_VOLUME_MUSIC = "VolumeMusic";
        private const string SOUND_VOLUME_SFX = "VolumeSfx";
        private const string SOUND_VOLUME_VOICE = "VolumeVoice";

        private readonly ModManagerForm modManager;

        // Those two settings could indicate that we need to modify DoW start options
        private bool enableHighPoly = false;
        private bool disableHighPoly = false;

        // Not the same settings as in ModManagerForm!
        private Dictionary<string, string> settings;

        private List<Profile> profiles;

        /// <summary>
        /// Creates the Form of the Settings Manager Window
        /// </summary>
        /// <param name="form"></param>
        public SettingsManagerForm(ModManagerForm form)
        {
            InitializeComponent();

            modManager = form;

            // Use the same icon as executable
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            // You could change PROFILES_PATH only in constructor because it's readonly
            PROFILES_PATH = modManager.CurrentDir + "\\Profiles";

            InitializeSettingsWithDefaults();

            ReadSettingsFromLocalINI();

            FindAllProfilesInDirectory(clearProfiles: false);

            ReadSettingsFromPlayercfgLUA();

            InitializeGUIWithSettings(localINI: true, playercfgLUA: true);

            // We have to add those methods to the EventHandler here so we could avoid accidental firing of those methods after we would change the state of the CheckBox
            newPlayerTextBox.TextChanged += new EventHandler(NewPlayerTextBox_TextChanged);
            loginAttemptsComboBox.SelectedIndexChanged += new EventHandler(LoginAttemptsComboBox_SelectedIndexChanged);
            scrollRateTrackBar.Scroll += new EventHandler(ScrollRateTrackBar_Scroll);
            inverseDeclinationCheckBox.CheckedChanged += new EventHandler(InverseDeclinationCheckBox_CheckedChanged);
            inversePanCheckBox.CheckedChanged += new EventHandler(InversePanCheckBox_CheckedChanged);
            parentalControlCheckBox.CheckedChanged += new EventHandler(ParentalControlCheckBox_CheckedChanged);
            currentPlayerComboBox.SelectedIndexChanged += new EventHandler(CurrentPlayerComboBox_SelectedIndexChanged);
            
            dynamicLightsComboBox.SelectedIndexChanged += new EventHandler(DynamicLightsComboBox_SelectedIndexChanged);
            effectsDetailComboBox.SelectedIndexChanged += new EventHandler(EffectsDetailComboBox_SelectedIndexChanged);
            worldEventsComboBox.SelectedIndexChanged += new EventHandler(WorldEventsComboBox_SelectedIndexChanged);
            shadowsDetailComboBox.SelectedIndexChanged += new EventHandler(ShadowsDetailComboBox_SelectedIndexChanged);
            full3DCameraCheckBox.CheckedChanged += new EventHandler(Full3DCameraCheckBox_CheckedChanged);
            persistentScarringComboBox.SelectedIndexChanged += new EventHandler(PersistentScarringComboBox_SelectedIndexChanged);
            betterTeamcoloredTexturexCheckBox.CheckedChanged += new EventHandler(BetterTeamcoloredTexturexCheckBox_CheckedChanged);
            unitsOcclusionCheckBox.CheckedChanged += new EventHandler(UnitsOcclusionCheckBox_CheckedChanged);
            persistentBodiesComboBox.SelectedIndexChanged += new EventHandler(PersistentBodiesComboBox_SelectedIndexChanged);
            terrainDetailComboBox.SelectedIndexChanged += new EventHandler(TerrainDetailComboBox_SelectedIndexChanged);
            modelDetailComboBox.SelectedIndexChanged += new EventHandler(ModelDetailComboBox_SelectedIndexChanged);
            textureDetailComboBox.SelectedIndexChanged += new EventHandler(TextureDetailComboBox_SelectedIndexChanged);
            rendererComboBox.SelectedIndexChanged += new EventHandler(RendererComboBox_SelectedIndexChanged);
            colorDepthComboBox.SelectedIndexChanged += new EventHandler(ColorDepthComboBox_SelectedIndexChanged);
            activeVideocardComboBox.SelectedIndexChanged += new EventHandler(ActiveVideocardComboBox_SelectedIndexChanged);
            antialiasingCheckBox.CheckedChanged += new EventHandler(AntialiasingCheckBox_CheckedChanged);
            refreshRateComboBox.SelectedIndexChanged += new EventHandler(RefreshRateComboBox_SelectedIndexChanged);
            windowedCheckBox.CheckedChanged += new EventHandler(WindowedCheckBox_CheckedChanged);
            vSyncCheckBox.CheckedChanged += new EventHandler(VSyncCheckBox_CheckedChanged);
            gammaTrackBar.Scroll += new EventHandler(GammaTrackBar_Scroll);
            screenResolutionComboBox.SelectedIndexChanged += new EventHandler(ScreenResolutionComboBox_SelectedIndexChanged);
            
            soundEnabledCheckBox.CheckedChanged += new EventHandler(SoundEnabledCheckBox_CheckedChanged);
            musicVolumeTrackBar.Scroll += new EventHandler(MusicVolumeTrackBar_Scroll);
            voiceVolumeTrackBar.Scroll += new EventHandler(VoiceVolumeTrackBar_Scroll);
            effectsVolumeTrackBar.Scroll += new EventHandler(EffectsVolumeTrackBar_Scroll);
            ambientVolumeTrackBar.Scroll += new EventHandler(AmbientVolumeTarckBar_Scroll);
            soundChannelsComboBox.SelectedIndexChanged += new EventHandler(SoundChannelsComboBox_SelectedIndexChanged);
            soundQualityComboBox.SelectedIndexChanged += new EventHandler(SoundQualityComboBox_SelectedIndexChanged);
            randomizedSoundsCheckBox.CheckedChanged += new EventHandler(RandomizedSoundsCheckBox_CheckedChanged);

            closeButton.Text = CLOSE_LABEL;
            saveButton.Enabled = false;
        }

        /// <summary>
        /// This method initializes settings Dictionary with default values
        /// </summary>
        // Request the inlining of this method
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void InitializeSettingsWithDefaults()
        {
            settings = new Dictionary<string, string>
            {
                // For Local.ini
                [CAMERA_DETAIL] = "1",
                [CURRENT_MOD] = "W40k",
                [DYNAMIC_LIGHTS] = "2",
                [EVENT_DETAIL_LEVEL] = "2",
                [FORCE_WATCH_MOVIES] = "1",
                [FULLRES_TEAMCOLOUR] = "0",
                [FX_DETAIL_LEVEL] = "2",
                [MODEL_DETAIL] = "2",
                [PARENTAL_CONTROL] = "0",
                [PERSISTENT_BODIES] = "0",
                [PERSISTENT_DECALS] = "0",
                [PLAYER_PROFILE] = "Profile1",
                [RL_SSO_NUM_TIMES_SHOWN] = "1",
                [SCREEN_ADAPTER] = "0",
                [SCREEN_ANIALIAS] = "0",
                [SCREEN_DEPTH] = "32",
                [SCREEN_DEVICE] = "Dx9 : Hardware TnL",
                [SCREEN_GAMMA] = "10",
                [SCREEN_HEIGHT] = "768",
                [SCREEN_NO_VSYNC] = "0",
                [SCREEN_REFRESH] = "0",
                [SCREEN_WIDTH] = "1024",
                [SCREEN_WINDOWED] = "0",
                [SHADOW_BLOB] = "0",
                [SHADOW_MAP] = "0",
                [SHADOW_VOLUME] = "0",
                [SOUND_ENABLED] = "1",
                [SOUND_LIMIT_SAMPLES] = "0",
                [SOUND_NR_CHANNELS] = "32",
                [SOUND_QUALITY] = "2",
                [TERRAIN_ENABLE_FOW_BLUR] = "0",
                [TEXTURE_DETAIL] = "1",
                [TOTAL_MATCHES] = "0",
                [UNIT_OCCLUSION] = "0",

                // For playercfg.lua
                [INVERT_DECLINATION] = "0",
                [INVERT_PAN] = "1",
                [SCROLL_RATE] = "1",

                [SOUND_VOLUME_AMBIENT] = "0.75",
                [SOUND_VOLUME_MUSIC] = "0.75",
                [SOUND_VOLUME_SFX] = "0.75",
                [SOUND_VOLUME_VOICE] = "0.75"
            };

            profiles = new List<Profile>();
        }

        /// <summary>
        /// This method reads settings from Local.ini file to settings Dictionary
        /// </summary>
        // Request the inlining of this method
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ReadSettingsFromLocalINI()
        {
            if (File.Exists(SETTINGS_FILE))
            {
                string[] lines = File.ReadAllLines(SETTINGS_FILE);

                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i].Trim();

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
                                    if (value.Contains(PROFILE))
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
                            }
                        }
                    }
                }
            }
            else
                saveButton.Enabled = true;
        }

        /// <summary>
        /// This method finds all profiles stored in Profiles directory
        /// </summary>
        // Request the inlining of this method
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void FindAllProfilesInDirectory(bool clearProfiles)
        {
            if (clearProfiles)
                profiles.Clear();

            if (Directory.Exists(PROFILES_PATH))
            {
                string[] profileDirectories = Directory.GetDirectories(PROFILES_PATH);

                // Check if there are at least one Profile directory
                if (profileDirectories.Length > 0)
                {
                    for (int i = 0; i < profileDirectories.Length; i++)
                    {
                        int indexOfLastSlah = profileDirectories[i].LastIndexOf("\\");
                        string profileName = profileDirectories[i].Substring(indexOfLastSlah + 1);

                        string playerName = "Player";
                        string playerNamePath = profileDirectories[i] + "\\" + NAME_DAT;
                        if (File.Exists(playerNamePath))
                            playerName = File.ReadAllText(playerNamePath);

                        profiles.Add(new Profile(profileName, playerName));
                    }

                    bool isProfileExist = false;

                    // Checks if profile listed in settings Dictionary is really exists
                    for (int i = 0; i < profiles.Count; i++)
                    {
                        if (settings[PLAYER_PROFILE] == profiles[i].ProfileName)
                        {
                            isProfileExist = true;
                            break;
                        }
                    }

                    if (!isProfileExist)
                        settings[PLAYER_PROFILE] = profiles[0].ProfileName;
                }
            }
        }

        /// <summary>
        /// This method reads settings from player.cfg to settimgs Dictionary
        /// </summary>
        // TODO: Investigate why this method is called twice after SettingManagerForm is launched
        // Request the inlining of this method
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ReadSettingsFromPlayercfgLUA()
        {
            string profileName = PROFILE + (currentPlayerComboBox.SelectedIndex + 1);
            string pathToPlayerConfig = PROFILES_PATH + "\\" + profileName + "\\" + PLAYERCONFIG;

            if (File.Exists(pathToPlayerConfig))
            {
                using (StreamReader file = new StreamReader(pathToPlayerConfig))
                {
                    string line;

                    while ((line = file.ReadLine()) != null)
                    {
                        if (line.EndsWith(","))
                        {
                            line = line.Replace(" ", "");

                            int indexOfEqualSign = line.IndexOf('=');

                            if (indexOfEqualSign > 0)
                            {
                                string stringValue = line.Substring(indexOfEqualSign + 1, line.Length - indexOfEqualSign - 2);

                                if (line.Contains(INVERT_DECLINATION))
                                    settings[INVERT_DECLINATION] = stringValue;
                                else if (line.Contains(INVERT_PAN))
                                    settings[INVERT_PAN] = stringValue;
                                else if (line.Contains(SCROLL_RATE))
                                    settings[SCROLL_RATE] = stringValue;
                                else if (line.Contains(SOUND_VOLUME_AMBIENT))
                                    settings[SOUND_VOLUME_AMBIENT] = stringValue;
                                else if (line.Contains(SOUND_VOLUME_MUSIC))
                                    settings[SOUND_VOLUME_MUSIC] = stringValue;
                                else if (line.Contains(SOUND_VOLUME_SFX))
                                    settings[SOUND_VOLUME_SFX] = stringValue;
                                else if (line.Contains(SOUND_VOLUME_VOICE))
                                {
                                    settings[SOUND_VOLUME_VOICE] = stringValue;
                                    
                                    // We found all the setting we need
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This method initializes all GUI elements with options from settings Dictioary
        /// </summary>
        /// <param name="localINI"></param>
        /// <param name="playercfgLUA"></param>
        private void InitializeGUIWithSettings(bool localINI, bool playercfgLUA)
        {
            if (localINI)
            {
                // Now we could set all ComboBoxes (METALLBAWHKSESS!!!) and CheckBoxes in our Form
                // Fun fact: Convert.ToBoolean("true") works but Convert.ToBoolean("1") fails. Only Convert.ToBoolean(1) is a good alternative
                full3DCameraCheckBox.Checked = settings[CAMERA_DETAIL] == "1";
                // Skipp CurrtentMod setting
                dynamicLightsComboBox.SelectedIndex = Convert.ToInt32(settings[DYNAMIC_LIGHTS]);
                worldEventsComboBox.SelectedIndex = Convert.ToInt32(settings[EVENT_DETAIL_LEVEL]);
                // Skip Force Watch Movies setting because it doesn't really work
                betterTeamcoloredTexturexCheckBox.Checked = settings[FULLRES_TEAMCOLOUR] == "1";
                effectsDetailComboBox.SelectedIndex = Convert.ToInt32(settings[FX_DETAIL_LEVEL]);
                modelDetailComboBox.SelectedIndex = Convert.ToInt32(settings[MODEL_DETAIL]);
                parentalControlCheckBox.Checked = settings[PARENTAL_CONTROL] == "1";
                persistentBodiesComboBox.SelectedIndex = Convert.ToInt32(settings[PERSISTENT_BODIES]);
                persistentScarringComboBox.SelectedIndex = Convert.ToInt32(settings[PERSISTENT_DECALS]);
                if (profiles.Count > 0)
                {
                    currentPlayerComboBox.Items.Clear();

                    for (int i = 0; i < profiles.Count; i++)
                    {
                        currentPlayerComboBox.Items.Add(profiles[i].PlayerName);

                        if (settings[PLAYER_PROFILE] == profiles[i].ProfileName)
                            currentPlayerComboBox.SelectedIndex = i;
                    }
                }
                else
                    deleteProfileButton.Enabled = false;

                loginAttemptsComboBox.SelectedItem = settings[RL_SSO_NUM_TIMES_SHOWN];
                // Test for oerformance!
                List<string> videocards = GetAllVideocards();
                int currentScreenAdapter = Convert.ToInt32(settings[SCREEN_ADAPTER]);

                activeVideocardComboBox.Items.AddRange(videocards.ToArray());
                if (currentScreenAdapter <= videocards.Count)
                    activeVideocardComboBox.SelectedIndex = currentScreenAdapter;
                else
                {
                    activeVideocardComboBox.SelectedIndex = 0;
                    settings[SCREEN_ADAPTER] = "0";
                    WriteSettings(localINI: true, playercgfLUA: false);
                }
                // Test for oerformance!
                antialiasingCheckBox.Checked = settings[SCREEN_ANIALIAS] == "1";
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
                if (settings[SCREEN_NO_VSYNC] == "1")       // We have to invert it for covienience
                    vSyncCheckBox.Checked = false;
                else
                    vSyncCheckBox.Checked = true;
                if (settings[SCREEN_REFRESH] == "0")
                    refreshRateComboBox.SelectedItem = "Auto";
                else
                    refreshRateComboBox.SelectedItem = settings[SCREEN_REFRESH] + " Hz";
                windowedCheckBox.Checked = settings[SCREEN_WINDOWED] == "1";
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
                soundEnabledCheckBox.Checked = settings[SOUND_ENABLED] == "1";
                if (settings[SOUND_LIMIT_SAMPLES] == "1")       // We have to invert it for covienience
                    randomizedSoundsCheckBox.Checked = false;
                else
                    randomizedSoundsCheckBox.Checked = false;
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
                unitsOcclusionCheckBox.Checked = settings[UNIT_OCCLUSION] == "1";
            }

            if (playercfgLUA)
            {
                inverseDeclinationCheckBox.Checked = settings[INVERT_DECLINATION] == "1";
                inversePanCheckBox.Checked = settings[INVERT_PAN] == "1";
                double doubleValue = Convert.ToDouble(settings[SCROLL_RATE], new CultureInfo("en-US"));
                scrollRateTrackBar.Value = Convert.ToInt32(doubleValue * 100d);
                doubleValue = Convert.ToDouble(settings[SOUND_VOLUME_AMBIENT], new CultureInfo("en-US"));
                ambientVolumeTrackBar.Value = Convert.ToInt32(doubleValue * 100d);
                doubleValue = Convert.ToDouble(settings[SOUND_VOLUME_MUSIC], new CultureInfo("en-US"));
                musicVolumeTrackBar.Value = Convert.ToInt32(doubleValue * 100d);
                doubleValue = Convert.ToDouble(settings[SOUND_VOLUME_SFX], new CultureInfo("en-US"));
                effectsVolumeTrackBar.Value = Convert.ToInt32(doubleValue * 100d);
                doubleValue = Convert.ToDouble(settings[SOUND_VOLUME_VOICE], new CultureInfo("en-US"));
                voiceVolumeTrackBar.Value = Convert.ToInt32(doubleValue * 100d);
            }
        }

        /// <summary>
        /// This method will find all the videocards
        /// </summary>
        /// <returns>List<string></returns>
        // TODO: this method may benefit from an optimization pass!
        private List<string> GetAllVideocards()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");
            List<string> videocards = new List<string>();

            foreach (ManagementObject mo in searcher.Get())
            {
                PropertyData currentBitsPerPixel = mo.Properties["CurrentBitsPerPixel"];
                PropertyData description = mo.Properties["Description"];
                
                if (currentBitsPerPixel != null && description != null)
                {
                    if (currentBitsPerPixel.Value != null)
                        videocards.Add(description.Value.ToString());
                }
            }

            return videocards;
        }

        /// <summary>
        /// This method reacts on SaveButton press
        /// </summary>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            WriteSettings(localINI: true, playercgfLUA: true);

            closeButton.Text = CLOSE_LABEL;
            saveButton.Enabled = false;
        }

        /// <summary>
        /// This method saves all settings to their respective files
        /// </summary>
        /// <param name="localINI"></param>
        /// <param name="playercgfLUA"></param>
        private void WriteSettings(bool localINI, bool playercgfLUA)
        {
            // There is no Profile selected
            if (currentPlayerComboBox.Text.Length == 0)
            {
                closeButton.Text = CLOSE_LABEL;
                saveButton.Enabled = false;
                return;
            }

            if (localINI)
            {
                // You have to use "\r\n" instead of "\n" or Dawn of War will NOT recognise the end of the line!
                // Save settings that are stored in Local.ini
                StringBuilder sb = new StringBuilder();
                sb.Append($"[global]\r\n");
                sb.Append($"{CAMERA_DETAIL}={settings[CAMERA_DETAIL]}\r\n");
                sb.Append($"{CURRENT_MOD}={settings[CURRENT_MOD]}\r\n");
                sb.Append($"{DYNAMIC_LIGHTS}={settings[DYNAMIC_LIGHTS]}\r\n");
                sb.Append($"{EVENT_DETAIL_LEVEL}={settings[EVENT_DETAIL_LEVEL]}\r\n");
                sb.Append($"{FORCE_WATCH_MOVIES}={settings[FORCE_WATCH_MOVIES]}\r\n");
                sb.Append($"{FULLRES_TEAMCOLOUR}={settings[FULLRES_TEAMCOLOUR]}\r\n");
                sb.Append($"{FX_DETAIL_LEVEL}={settings[FX_DETAIL_LEVEL]}\r\n");
                sb.Append($"{MODEL_DETAIL}={settings[MODEL_DETAIL]}\r\n");
                sb.Append($"{PARENTAL_CONTROL}={settings[PARENTAL_CONTROL]}\r\n");
                sb.Append($"{PERSISTENT_BODIES}={settings[PERSISTENT_BODIES]}\r\n");
                sb.Append($"{PERSISTENT_DECALS}={settings[PERSISTENT_DECALS]}\r\n");
                sb.Append($"{PLAYER_PROFILE}={settings[PLAYER_PROFILE]}\r\n");
                sb.Append($"{RL_SSO_NUM_TIMES_SHOWN}={settings[RL_SSO_NUM_TIMES_SHOWN]}\r\n");
                sb.Append($"{SCREEN_ADAPTER}={settings[SCREEN_ADAPTER]}\r\n");
                sb.Append($"{SCREEN_ANIALIAS}={settings[SCREEN_ANIALIAS]}\r\n");
                sb.Append($"{SCREEN_DEPTH}={settings[SCREEN_DEPTH]}\r\n");
                sb.Append($"{SCREEN_DEVICE}={settings[SCREEN_DEVICE]}\r\n");
                sb.Append($"{SCREEN_GAMMA}={settings[SCREEN_GAMMA]}\r\n");
                sb.Append($"{SCREEN_HEIGHT}={settings[SCREEN_HEIGHT]}\r\n");
                sb.Append($"{SCREEN_NO_VSYNC}={settings[SCREEN_NO_VSYNC]}\r\n");
                sb.Append($"{SCREEN_REFRESH}={settings[SCREEN_REFRESH]}\r\n");
                sb.Append($"{SCREEN_WIDTH}={settings[SCREEN_WIDTH]}\r\n");
                sb.Append($"{SCREEN_WINDOWED}={settings[SCREEN_WINDOWED]}\r\n");
                sb.Append($"{SHADOW_BLOB}={settings[SHADOW_BLOB]}\r\n");
                sb.Append($"{SHADOW_MAP}={settings[SHADOW_MAP]}\r\n");
                sb.Append($"{SHADOW_VOLUME}={settings[SHADOW_VOLUME]}\r\n");
                sb.Append($"{SOUND_ENABLED}={settings[SOUND_ENABLED]}\r\n");
                sb.Append($"{SOUND_LIMIT_SAMPLES}={settings[SOUND_LIMIT_SAMPLES]}\r\n");
                sb.Append($"{SOUND_NR_CHANNELS}={settings[SOUND_NR_CHANNELS]}\r\n");
                sb.Append($"{SOUND_QUALITY}={settings[SOUND_QUALITY]}\r\n");
                sb.Append($"{TERRAIN_ENABLE_FOW_BLUR}={settings[TERRAIN_ENABLE_FOW_BLUR]}\r\n");
                sb.Append($"{TEXTURE_DETAIL}={settings[TEXTURE_DETAIL]}\r\n");
                sb.Append($"{TOTAL_MATCHES}={settings[TOTAL_MATCHES]}\r\n");
                sb.Append($"{UNIT_OCCLUSION}={settings[UNIT_OCCLUSION]}");

                File.WriteAllText(SETTINGS_FILE, sb.ToString());
            }

            if (playercgfLUA)
            {
                // Save settings that are stored in playercfg.lua
                // TODO: Use Streams insted of reading and writing the whoile file at once
                string pathToPlayerConfig = PROFILES_PATH + "\\" + PROFILE + (currentPlayerComboBox.SelectedIndex + 1).ToString() + "\\" + PLAYERCONFIG;

                if (File.Exists(pathToPlayerConfig))
                {
                    string[] lines = File.ReadAllLines(pathToPlayerConfig);

                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i].EndsWith(","))
                        {
                            if (lines[i].Contains(INVERT_DECLINATION))
                                lines[i] = $"\t{INVERT_DECLINATION} = {settings[INVERT_DECLINATION]},";
                            else if (lines[i].Contains(INVERT_PAN))
                                lines[i] = $"\t{INVERT_PAN} = {settings[INVERT_PAN]},";
                            else if (lines[i].Contains(SCROLL_RATE))
                                lines[i] = $"\t{SCROLL_RATE} = {settings[SCROLL_RATE]},";
                            else if (lines[i].Contains(SOUND_VOLUME_AMBIENT))
                                lines[i] = $"\t{SOUND_VOLUME_AMBIENT} = {settings[SOUND_VOLUME_AMBIENT]},";
                            else if (lines[i].Contains(SOUND_VOLUME_MUSIC))
                                lines[i] = $"\t{SOUND_VOLUME_MUSIC} = {settings[SOUND_VOLUME_MUSIC]},";
                            else if (lines[i].Contains(SOUND_VOLUME_SFX))
                                lines[i] = $"\t{SOUND_VOLUME_SFX} = {settings[SOUND_VOLUME_SFX]},";
                            else if (lines[i].Contains(SOUND_VOLUME_VOICE))
                            {
                                lines[i] = $"\t{SOUND_VOLUME_VOICE} = {settings[SOUND_VOLUME_VOICE]},";

                                // We found all the settings we searched for
                                break;
                            }
                        }
                    }
                    File.WriteAllLines(pathToPlayerConfig, lines);
                }
                else
                {
                    StringBuilder sb = new StringBuilder();

                    sb.Append("Controls = \r\n");
                    sb.Append("{\r\n");
                    sb.Append($"\t{INVERT_DECLINATION} = {settings[INVERT_DECLINATION]},\r\n");
                    sb.Append($"\t{INVERT_PAN} = {settings[INVERT_PAN]},\r\n");
                    sb.Append($"\t{SCROLL_RATE} = {settings[SCROLL_RATE]},\r\n");
                    sb.Append("}\r\n");
                    sb.Append("Sound = \r\n");
                    sb.Append("{\r\n");
                    sb.Append($"\t{SOUND_VOLUME_AMBIENT} = {settings[SOUND_VOLUME_AMBIENT]},\r\n");
                    sb.Append($"\t{SOUND_VOLUME_MUSIC} = {settings[SOUND_VOLUME_MUSIC]},\r\n");
                    sb.Append($"\t{SOUND_VOLUME_SFX} = {settings[SOUND_VOLUME_SFX]},\r\n");
                    sb.Append($"\t{SOUND_VOLUME_VOICE} = {settings[SOUND_VOLUME_VOICE]},\r\n");
                    sb.Append("}\r\n");
                    sb.Append("player_preferences = \r\n");
                    sb.Append("{\r\n");
                    sb.Append("\tcampaign_played_disorder = false,\r\n");
                    sb.Append("\tcampaign_played_order = false,\r\n");
                    sb.Append("\tforce_name = \"Blood Ravens\",\r\n");
                    sb.Append("\trace = \"space_marine_race\",\r\n");
                    sb.Append("}\r\n");

                    File.WriteAllText(pathToPlayerConfig, sb.ToString());
                }
            }

            if (enableHighPoly)
            {
                modManager.ChangeSetting(ModManagerForm.FORCE_HIGH_POLY, 1);
                enableHighPoly = false;
            }
            else if (disableHighPoly)
            {
                modManager.ChangeSetting(ModManagerForm.FORCE_HIGH_POLY, 0);
                disableHighPoly = false;
            }
        }

        /// <summary>
        /// This method restores all settings to their default values
        /// </summary>
        private void DefaultsButton_Click(object sender, EventArgs e)
        {
            InitializeSettingsWithDefaults();
            InitializeGUIWithSettings(localINI: true, playercfgLUA: true);

            saveButton.Enabled = true;
            defaultsButton.Enabled = false;
            saveButton.Focus();
        }

        /// <summary>
        /// This method closes the SettingManagerForm
        /// </summary>
        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// This method reacts to changes in currentPlayerComboBox.SelectedIndex
        /// </summary>
        private void CurrentPlayerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReadSettingsFromPlayercfgLUA();
            InitializeGUIWithSettings(localINI: false, playercfgLUA: true);

            settings[PLAYER_PROFILE] = PROFILE + (currentPlayerComboBox.SelectedIndex + 1).ToString();

            closeButton.Text = CANCEL_LABEL;
            saveButton.Enabled = true;
            defaultsButton.Enabled = true;
        }

        /// <summary>
        /// This method reacts to changes in parentalControlCheckBox.Checked
        /// </summary>
        private void ParentalControlCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (parentalControlCheckBox.Checked)
                settings[PARENTAL_CONTROL] = "1";
            else
                settings[PARENTAL_CONTROL] = "0";

            closeButton.Text = CANCEL_LABEL;
            saveButton.Enabled = true;
            defaultsButton.Enabled = true;
        }

        /// <summary>
        /// This method reacts to changes in inversePanCheckBox.Checked
        /// </summary>
        private void InversePanCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (inversePanCheckBox.Checked)
                settings[INVERT_PAN] = "1";
            else
                settings[INVERT_PAN] = "0";

            closeButton.Text = CANCEL_LABEL;
            saveButton.Enabled = true;
            defaultsButton.Enabled = true;
        }

        /// <summary>
        /// This method reacts to changes in inverseDeclinationCheckBox.Checked
        /// </summary>
        private void InverseDeclinationCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (inverseDeclinationCheckBox.Checked)
                settings[INVERT_DECLINATION] = "1";
            else
                settings[INVERT_DECLINATION] = "0";

            closeButton.Text = CANCEL_LABEL;
            saveButton.Enabled = true;
            defaultsButton.Enabled = true;
        }

        /// <summary>
        /// This method reacts to changes in scrollRateTrackBar.Value
        /// </summary>
        private void ScrollRateTrackBar_Scroll(object sender, EventArgs e)
        {
            double doubleValue = Convert.ToDouble(scrollRateTrackBar.Value);
            double settingValue = doubleValue / 100d;

            settings[SCROLL_RATE] = settingValue.ToString("F5", new CultureInfo("en-US"));

            closeButton.Text = CANCEL_LABEL;
            saveButton.Enabled = true;
            defaultsButton.Enabled = true;
        }

        /// <summary>
        /// This method reacts to changes in loginAttemptsComboBox.SelectedItem
        /// </summary>
        private void LoginAttemptsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings[RL_SSO_NUM_TIMES_SHOWN] = loginAttemptsComboBox.SelectedItem.ToString();

            closeButton.Text = CANCEL_LABEL;
            saveButton.Enabled = true;
            defaultsButton.Enabled = true;
        }

        /// <summary>
        /// This method reacts to changes in activeVideocardComboBox.SelectedItem
        /// </summary>
        private void ActiveVideocardComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings[SCREEN_ADAPTER] = activeVideocardComboBox.SelectedIndex.ToString();

            closeButton.Text = CANCEL_LABEL;
            saveButton.Enabled = true;
            defaultsButton.Enabled = true;
        }

        /// <summary>
        /// This method reacts to changes in rendererComboBox.SelectedItem
        /// </summary>
        private void RendererComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings[SCREEN_DEVICE] = rendererComboBox.SelectedItem.ToString();

            closeButton.Text = CANCEL_LABEL;
            saveButton.Enabled = true;
            defaultsButton.Enabled = true;
        }

        /// <summary>
        /// This method reacts to changes in screenResolutionComboBox.SelectedItem
        /// </summary>
        private void ScreenResolutionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = screenResolutionComboBox.SelectedItem.ToString();
            int x = str.IndexOf('×');

            settings[SCREEN_WIDTH] = str.Substring(0, x);
            settings[SCREEN_HEIGHT] = str.Substring(x + 1, str.Length - x - 1);

            closeButton.Text = CANCEL_LABEL;
            saveButton.Enabled = true;
            defaultsButton.Enabled = true;
        }

        /// <summary>
        /// This method reacts to changes in refreshRateComboBox.SelectedItem
        /// </summary>
        private void RefreshRateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = refreshRateComboBox.SelectedItem.ToString();
            int indexOfSpace = str.IndexOf(" ");

            if (str == "Auto")
                settings[SCREEN_REFRESH] = "0";
            else
                settings[SCREEN_REFRESH] = str.Substring(0, indexOfSpace);

            closeButton.Text = CANCEL_LABEL;
            saveButton.Enabled = true;
            defaultsButton.Enabled = true;
        }

        /// <summary>
        /// This method reacts to changes in colorDepthComboBox.SelectedIndex
        /// </summary>
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

            closeButton.Text = CANCEL_LABEL;
            saveButton.Enabled = true;
            defaultsButton.Enabled = true;
        }

        /// <summary>
        /// This method reacts to changes in gammaTrackBar.Value
        /// </summary>
        private void GammaTrackBar_Scroll(object sender, EventArgs e)
        {
            double doubleValue = Convert.ToDouble(gammaTrackBar.Value);
            double settingValue = doubleValue / 100d;

            settings[SCREEN_GAMMA] = settingValue.ToString("F5", new CultureInfo("en-US"));

            closeButton.Text = CANCEL_LABEL;
            saveButton.Enabled = true;
            defaultsButton.Enabled = true;
        }

        /// <summary>
        /// This method reacts to changes in vSyncCheckBox.Checked
        /// </summary>
        private void VSyncCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (vSyncCheckBox.Checked)      // We have to invert this for convienience
                settings[SCREEN_NO_VSYNC] = "0";
            else
                settings[SCREEN_NO_VSYNC] = "1";

            closeButton.Text = CANCEL_LABEL;
            saveButton.Enabled = true;
            defaultsButton.Enabled = true;
        }

        /// <summary>
        /// This method reacts to changes in windowedCheckBox.Checked
        /// </summary>
        private void WindowedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (windowedCheckBox.Checked)
                settings[SCREEN_WINDOWED] = "1";
            else
                settings[SCREEN_WINDOWED] = "0";

            closeButton.Text = CANCEL_LABEL;
            saveButton.Enabled = true;
            defaultsButton.Enabled = true;
        }

        /// <summary>
        /// This method reacts to changes in antialiasingCheckBox.Checked
        /// </summary>
        private void AntialiasingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (antialiasingCheckBox.Checked)
                settings[SCREEN_ANIALIAS] = "1";
            else
                settings[SCREEN_ANIALIAS] = "0";

            closeButton.Text = CANCEL_LABEL;
            saveButton.Enabled = true;
            defaultsButton.Enabled = true;
        }

        /// <summary>
        /// This method reacts to changes in textureDetailComboBox.SelectedIndex
        /// </summary>
        private void TextureDetailComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings[TEXTURE_DETAIL] = textureDetailComboBox.SelectedIndex.ToString();

            closeButton.Text = CANCEL_LABEL;
            saveButton.Enabled = true;
            defaultsButton.Enabled = true;
        }

        /// <summary>
        /// This method reacts to changes in modelDetailComboBox.SelectedIndex
        /// </summary>
        private void ModelDetailComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings[MODEL_DETAIL] = modelDetailComboBox.SelectedIndex.ToString();

            closeButton.Text = CANCEL_LABEL;
            saveButton.Enabled = true;
            defaultsButton.Enabled = true;
        }

        /// <summary>
        /// This method reacts to changes in terrainDetailComboBox.SelectedIndex
        /// </summary>
        private void TerrainDetailComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings[TERRAIN_ENABLE_FOW_BLUR] = terrainDetailComboBox.SelectedIndex.ToString();

            closeButton.Text = CANCEL_LABEL;
            saveButton.Enabled = true;
            defaultsButton.Enabled = true;
        }

        /// <summary>
        /// This method reacts to changes in betterTeamcoloredTexturexCheckBox.Checked
        /// </summary>
        private void BetterTeamcoloredTexturexCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (betterTeamcoloredTexturexCheckBox.Checked)
                settings[FULLRES_TEAMCOLOUR] = "1";
            else
                settings[FULLRES_TEAMCOLOUR] = "0";

            closeButton.Text = CANCEL_LABEL;
            saveButton.Enabled = true;
            defaultsButton.Enabled = true;
        }

        /// <summary>
        /// This method reacts to changes in shadowsDetailComboBox.SelectedIndex
        /// </summary>
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

            closeButton.Text = CANCEL_LABEL;
            saveButton.Enabled = true;
            defaultsButton.Enabled = true;
        }

        /// <summary>
        /// This method reacts to changes in worldEventsComboBox.SelectedIndex
        /// </summary>
        private void WorldEventsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings[EVENT_DETAIL_LEVEL] = worldEventsComboBox.SelectedIndex.ToString();

            closeButton.Text = CANCEL_LABEL;
            saveButton.Enabled = true;
            defaultsButton.Enabled = true;
        }

        /// <summary>
        /// This method reacts to changes in effectsDetailComboBox.SelectedIndex
        /// </summary>
        private void EffectsDetailComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings[FX_DETAIL_LEVEL] = effectsDetailComboBox.SelectedIndex.ToString();

            closeButton.Text = CANCEL_LABEL;
            saveButton.Enabled = true;
            defaultsButton.Enabled = true;
        }

        /// <summary>
        /// This method reacts to changes in persistentBodiesComboBox.SelectedIndex
        /// </summary>
        private void PersistentBodiesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings[PERSISTENT_BODIES] = persistentBodiesComboBox.SelectedIndex.ToString();

            closeButton.Text = CANCEL_LABEL;
            saveButton.Enabled = true;
            defaultsButton.Enabled = true;
        }

        /// <summary>
        /// This method reacts to changes in persistentScarringComboBox.SelectedIndex
        /// </summary>
        private void PersistentScarringComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings[PERSISTENT_DECALS] = persistentScarringComboBox.SelectedIndex.ToString();

            closeButton.Text = CANCEL_LABEL;
            saveButton.Enabled = true;
            defaultsButton.Enabled = true;
        }

        /// <summary>
        /// This method reacts to changes in dynamicLightsComboBox.SelectedIndex
        /// </summary>
        private void DynamicLightsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings[DYNAMIC_LIGHTS] = dynamicLightsComboBox.SelectedIndex.ToString();

            closeButton.Text = CANCEL_LABEL;
            saveButton.Enabled = true;
            defaultsButton.Enabled = true;
        }

        /// <summary>
        /// This method reacts to changes in full3DCameraCheckBox.Checked
        /// </summary>
        private void Full3DCameraCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (full3DCameraCheckBox.Checked)
                settings[CAMERA_DETAIL] = "1";
            else
                settings[CAMERA_DETAIL] = "0";

            closeButton.Text = CANCEL_LABEL;
            saveButton.Enabled = true;
            defaultsButton.Enabled = true;
        }

        /// <summary>
        /// This method reacts to changes in unitsOcclusionCheckBox.Checked
        /// </summary>
        private void UnitsOcclusionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (unitsOcclusionCheckBox.Checked)
                settings[UNIT_OCCLUSION] = "1";
            else
                settings[UNIT_OCCLUSION] = "0";

            closeButton.Text = CANCEL_LABEL;
            saveButton.Enabled = true;
            defaultsButton.Enabled = true;
        }

        /// <summary>
        /// This method reacts to changes in soundEnabledCheckBox.Checked
        /// </summary>
        private void SoundEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (soundEnabledCheckBox.Checked)
                settings[SOUND_ENABLED] = "1";
            else
                settings[SOUND_ENABLED] = "0";

            closeButton.Text = CANCEL_LABEL;
            saveButton.Enabled = true;
            defaultsButton.Enabled = true;
        }

        /// <summary>
        /// This method reacts to changes in randomizedSoundsCheckBox.Checked
        /// </summary>
        private void RandomizedSoundsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (randomizedSoundsCheckBox.Checked)       // We have to invert it for covienience
                settings[SOUND_LIMIT_SAMPLES] = "0";
            else
                settings[SOUND_LIMIT_SAMPLES] = "1";

            closeButton.Text = CANCEL_LABEL;
            saveButton.Enabled = true;
            defaultsButton.Enabled = true;
        }

        /// <summary>
        /// This method reacts to changes in soundQualityComboBox.SelectedIndex
        /// </summary>
        private void SoundQualityComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings[SOUND_QUALITY] = soundQualityComboBox.SelectedIndex.ToString();

            closeButton.Text = CANCEL_LABEL;
            saveButton.Enabled = true;
            defaultsButton.Enabled = true;
        }

        /// <summary>
        /// This method reacts to changes in soundChannelsComboBox.SelectedIndex
        /// </summary>
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

            closeButton.Text = CANCEL_LABEL;
            saveButton.Enabled = true;
            defaultsButton.Enabled = true;
        }

        /// <summary>
        /// This method reacts to changes in ambientVolumeTrackBar.Value
        /// </summary>
        private void AmbientVolumeTarckBar_Scroll(object sender, EventArgs e)
        {
            double doubleValue = Convert.ToDouble(ambientVolumeTrackBar.Value);
            double settingValue = doubleValue / 100d;

            settings[SOUND_VOLUME_AMBIENT] = settingValue.ToString("F5", new CultureInfo("en-US"));

            closeButton.Text = CANCEL_LABEL;
            saveButton.Enabled = true;
            defaultsButton.Enabled = true;
        }

        /// <summary>
        /// This method reacts to changes in effectsVolumeTrackBar.Value
        /// </summary>
        private void EffectsVolumeTrackBar_Scroll(object sender, EventArgs e)
        {
            double doubleValue = Convert.ToDouble(effectsVolumeTrackBar.Value);
            double settingValue = doubleValue / 100d;

            settings[SOUND_VOLUME_SFX] = settingValue.ToString("F5", new CultureInfo("en-US"));

            closeButton.Text = CANCEL_LABEL;
            saveButton.Enabled = true;
            defaultsButton.Enabled = true;
        }

        /// <summary>
        /// This method reacts to changes in voiceVolumeTrackBar.Value
        /// </summary>
        private void VoiceVolumeTrackBar_Scroll(object sender, EventArgs e)
        {
            double doubleValue = Convert.ToDouble(voiceVolumeTrackBar.Value);
            double settingValue = doubleValue / 100d;

            settings[SOUND_VOLUME_VOICE] = settingValue.ToString("F5", new CultureInfo("en-US"));

            closeButton.Text = CANCEL_LABEL;
            saveButton.Enabled = true;
            defaultsButton.Enabled = true;
        }

        /// <summary>
        /// This method reacts to changes in musicVolumeTrackBar.Value
        /// </summary>
        private void MusicVolumeTrackBar_Scroll(object sender, EventArgs e)
        {
            double doubleValue = Convert.ToDouble(musicVolumeTrackBar.Value);
            double settingValue = doubleValue / 100d;

            settings[SOUND_VOLUME_MUSIC] = settingValue.ToString("F5", new CultureInfo("en-US"));

            closeButton.Text = CANCEL_LABEL;
            saveButton.Enabled = true;
            defaultsButton.Enabled = true;
        }

        /// <summary>
        /// This method sets the graphics to "Low" preset
        /// </summary>
        private void LowGraphicsButton_Click(object sender, EventArgs e)
        {
            settings[CAMERA_DETAIL] = "0";
            settings[DYNAMIC_LIGHTS] = "0";
            settings[EVENT_DETAIL_LEVEL] = "0";
            settings[FULLRES_TEAMCOLOUR] = "0";
            settings[FX_DETAIL_LEVEL] = "0";
            settings[MODEL_DETAIL] = "0";
            settings[PERSISTENT_BODIES] = "0";
            settings[PERSISTENT_DECALS] = "0";
            settings[SCREEN_ANIALIAS] = "0";
            settings[SCREEN_DEPTH] = "16";
            settings[SHADOW_BLOB] = "0";
            settings[SHADOW_MAP] = "0";
            settings[SHADOW_VOLUME] = "0";
            settings[TERRAIN_ENABLE_FOW_BLUR] = "0";
            settings[TEXTURE_DETAIL] = "0";

            disableHighPoly = true;

            InitializeGUIWithSettings(localINI: true, playercfgLUA: false);

            closeButton.Text = CANCEL_LABEL;
            saveButton.Enabled = true;
            saveButton.Focus();
            defaultsButton.Enabled = true;
        }

        /// <summary>
        /// This method sets the graphics to "Medium" preset
        /// </summary>
        private void MediumGraphicsButton_Click(object sender, EventArgs e)
        {
            settings[CAMERA_DETAIL] = "1";
            settings[DYNAMIC_LIGHTS] = "1";
            settings[EVENT_DETAIL_LEVEL] = "1";
            settings[FULLRES_TEAMCOLOUR] = "0";
            settings[FX_DETAIL_LEVEL] = "1";
            settings[MODEL_DETAIL] = "1";
            settings[PERSISTENT_BODIES] = "1";
            settings[PERSISTENT_DECALS] = "1";
            settings[SCREEN_ANIALIAS] = "0";
            settings[SCREEN_DEPTH] = "32";
            settings[SHADOW_BLOB] = "1";
            settings[SHADOW_MAP] = "0";
            settings[SHADOW_VOLUME] = "0";
            settings[TERRAIN_ENABLE_FOW_BLUR] = "1";
            settings[TEXTURE_DETAIL] = "1";

            InitializeGUIWithSettings(localINI: true, playercfgLUA: false);

            closeButton.Text = CANCEL_LABEL;
            saveButton.Enabled = true;
            saveButton.Focus();
            defaultsButton.Enabled = true;
        }

        /// <summary>
        /// This method sets the graphics to "High" preset
        /// </summary>
        private void HighGraphicsButton_Click(object sender, EventArgs e)
        {
            settings[CAMERA_DETAIL] = "1";
            settings[DYNAMIC_LIGHTS] = "2";
            settings[EVENT_DETAIL_LEVEL] = "2";
            settings[FULLRES_TEAMCOLOUR] = "0";
            settings[FX_DETAIL_LEVEL] = "2";
            settings[MODEL_DETAIL] = "2";
            settings[PERSISTENT_BODIES] = "2";
            settings[PERSISTENT_DECALS] = "2";
            settings[SCREEN_ANIALIAS] = "0";
            settings[SCREEN_DEPTH] = "32";
            settings[SHADOW_BLOB] = "1";
            settings[SHADOW_MAP] = "1";
            settings[SHADOW_VOLUME] = "0";
            settings[TERRAIN_ENABLE_FOW_BLUR] = "2";
            settings[TEXTURE_DETAIL] = "2";

            InitializeGUIWithSettings(localINI: true, playercfgLUA: false);

            closeButton.Text = CANCEL_LABEL;
            saveButton.Enabled = true;
            saveButton.Focus();
            defaultsButton.Enabled = true;
        }

        /// <summary>
        /// This method sets the graphics to "Ultra" preset
        /// </summary>
        private void UltraGraphicsButton_Click(object sender, EventArgs e)
        {
            settings[CAMERA_DETAIL] = "1";
            settings[DYNAMIC_LIGHTS] = "3";
            settings[EVENT_DETAIL_LEVEL] = "2";
            settings[FULLRES_TEAMCOLOUR] = "1";
            settings[FX_DETAIL_LEVEL] = "2";
            settings[MODEL_DETAIL] = "2";
            settings[PERSISTENT_BODIES] = "3";
            settings[PERSISTENT_DECALS] = "2";
            settings[SCREEN_ANIALIAS] = "1";
            settings[SCREEN_DEPTH] = "32";
            settings[SHADOW_BLOB] = "1";
            settings[SHADOW_MAP] = "1";
            settings[SHADOW_VOLUME] = "1";
            settings[TERRAIN_ENABLE_FOW_BLUR] = "2";
            settings[TEXTURE_DETAIL] = "2";

            enableHighPoly = true;

            InitializeGUIWithSettings(localINI: true, playercfgLUA: false);

            closeButton.Text = CANCEL_LABEL;
            saveButton.Enabled = true;
            saveButton.Focus();
            defaultsButton.Enabled = true;
        }

        /// <summary>
        /// This method sets the sound quality to "Low" preset
        /// </summary>
        private void LowAudioButton_Click(object sender, EventArgs e)
        {
            settings[SOUND_ENABLED] = "1";
            settings[SOUND_LIMIT_SAMPLES] = "1";
            settings[SOUND_NR_CHANNELS] = "16";
            settings[SOUND_QUALITY] = "0";

            InitializeGUIWithSettings(localINI: true, playercfgLUA: false);

            closeButton.Text = CANCEL_LABEL;
            saveButton.Enabled = true;
            saveButton.Focus();
            defaultsButton.Enabled = true;
        }

        /// <summary>
        /// This method sets the sound quality to "Medium" preset
        /// </summary>
        private void MediumAudioButton_Click(object sender, EventArgs e)
        {
            settings[SOUND_ENABLED] = "1";
            settings[SOUND_LIMIT_SAMPLES] = "0";
            settings[SOUND_NR_CHANNELS] = "32";
            settings[SOUND_QUALITY] = "1";

            InitializeGUIWithSettings(localINI: true, playercfgLUA: false);

            closeButton.Text = CANCEL_LABEL;
            saveButton.Enabled = true;
            saveButton.Focus();
            defaultsButton.Enabled = true;
        }

        /// <summary>
        /// This method sets the sound quality to "High" preset
        /// </summary>
        private void HighAudioButton_Click(object sender, EventArgs e)
        {
            settings[SOUND_ENABLED] = "1";
            settings[SOUND_LIMIT_SAMPLES] = "0";
            settings[SOUND_NR_CHANNELS] = "64";
            settings[SOUND_QUALITY] = "2";

            InitializeGUIWithSettings(localINI: true, playercfgLUA: false);

            closeButton.Text = CANCEL_LABEL;
            saveButton.Enabled = true;
            saveButton.Focus();
            defaultsButton.Enabled = true;
        }

        /// <summary>
        /// This method starts the SystemPerformanceManagerForm
        /// </summary>
        private void SystemPerformanceManagerButton_Click(object sender, EventArgs e)
        {
            new SystemPerformanceManagerForm(modManager).Show();
        }

        /// <summary>
        /// This method deletes the current selected Player Profile
        /// </summary>
        private void DeleteProfileButton_Click(object sender, EventArgs e)
        {
            string playerNameToDelete = currentPlayerComboBox.SelectedItem.ToString();

            for (int i = 0; i < profiles.Count; i++)
            {
                if (playerNameToDelete == profiles[i].PlayerName)
                {
                    string profilePathToDelete = PROFILES_PATH + "\\" + profiles[i].ProfileName;

                    if (Directory.Exists(profilePathToDelete))
                    {
                        Directory.Delete(profilePathToDelete, true);

                        currentPlayerComboBox.Items.RemoveAt(i);
                    }

                    break;
                }
            }

            FindAllProfilesInDirectory(clearProfiles: true);
            InitializeGUIWithSettings(localINI: true, playercfgLUA: true);
        }

        /// <summary>
        /// This method creates a new Player Profile
        /// </summary>
        private void CreateProfileButton_Click(object sender, EventArgs e)
        {
            int indexOfNewProfile = 1;

            if (Directory.Exists(PROFILES_PATH))
            {
                string[] profiles = Directory.GetDirectories(PROFILES_PATH);
                int[] profilesIndexes = new int[profiles.Length];

                for (int i = 0; i < profiles.Length; i++)
                {
                    // Delete the full patch
                    int indexOfLastSlah = profiles[i].LastIndexOf("\\");
                    profiles[i] = profiles[i].Substring(indexOfLastSlah + 1);

                    // Delete the "Profile" part of the string and convert the rest to int
                    profilesIndexes[i] = Convert.ToInt32(profiles[i].Substring(7));

                    if (indexOfNewProfile == profilesIndexes[i])
                        indexOfNewProfile++;
                }
            }

            string newProfileName = PROFILE + indexOfNewProfile;
            string newProfilePath = PROFILES_PATH + "\\" + newProfileName;

            Directory.CreateDirectory(newProfilePath);
            try
            {
                File.WriteAllText(newProfilePath + "\\" + NAME_DAT, newPlayerTextBox.Text, Encoding.GetEncoding("utf-16"));

                newPlayerTextBox.Text = "";
                deleteProfileButton.Enabled = true;

                FindAllProfilesInDirectory(clearProfiles: true);
                InitializeGUIWithSettings(localINI: true, playercfgLUA: true);
            }
            catch (Exception ex)
            {
                ThemedMessageBox.Show(ex.Message, "Error:");
            }
        }

        /// <summary>
        /// This method renames the current selected Player Profile
        /// </summary>
        private void RenameProfileButton_Click(object sender, EventArgs e)
        {
            string currentPLayerName = currentPlayerComboBox.SelectedItem.ToString();

            for (int i = 0; i < profiles.Count; i++)
            {
                if (currentPLayerName == profiles[i].PlayerName)
                {
                    string profilePathToRename = PROFILES_PATH + "\\" + profiles[i].ProfileName + "\\" + NAME_DAT;
                    string playersNewName = newPlayerTextBox.Text;
                    try
                    {
                        File.WriteAllText(profilePathToRename, playersNewName, Encoding.GetEncoding("utf-16"));

                        profiles[i].PlayerName = playersNewName;
                        newPlayerTextBox.Text = "";

                        int selectedIndex = currentPlayerComboBox.SelectedIndex;
                        currentPlayerComboBox.Items.RemoveAt(i);
                        currentPlayerComboBox.Items.Insert(i, playersNewName);
                        currentPlayerComboBox.SelectedIndex = selectedIndex;
                    }
                    catch (Exception ex)
                    {
                        ThemedMessageBox.Show(ex.Message, "Error:");
                    }

                    break;
                }
            }
        }

        /// <summary>
        /// This method reacts to chanes in newPlayerTextBox.Text
        /// </summary>
        private void NewPlayerTextBox_TextChanged(object sender, EventArgs e)
        {
            if (newPlayerTextBox.TextLength > 0)
            {
                createProfileButton.Enabled = true;
                if (currentPlayerComboBox.Text.Length > 0)
                    renameProfileButton.Enabled = true;
            }
            else
            {
                createProfileButton.Enabled = false;
                renameProfileButton.Enabled = false;
            }
        }
    }
}