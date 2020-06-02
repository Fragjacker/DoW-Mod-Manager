using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace DoW_Mod_Manager
{
    public partial class ModDownloaderForm : Form
    {
        private readonly string[] modWebAdresses = new string[25];

        public ModDownloaderForm()
        {
            InitializeComponent();

            modListBox.Items.AddRange(new object[]
            {
                "DoW40k: Firestorm over Kaurava",
                "Ultimate Apocalypse Mod",
                "Black Templars - No Remorse! No Fear!",
                "DoWPro",
                "CornCobMan\'s Fun Mod",
                "Soulstorm Bugfix Mod",
                "Titanium Wars Mod for Soulstorm",
                "Dark Angels mod: Repent! For tomorrow you die!",
                "Hard_mod",
                "Redux Mod",
                "HD Dawn Of War",
                "Dawn of War: Strongholds",
                "The Dance Macabre - Harlequins Mod",
                "Witch Hunters: Adepta Sororitas",
                "Emperor\'s Children mod:For the Glory of Slaanesh!",
                "Steel Legion - Armageddon",
                "Thousand Sons mod: Knowledge Is Power!",
                "Inquisition: Daemonhunt",
                "Unification Mod",
                "Salamanders mod REFORGED: Unto the Anvil of War!",
                "Adeptus Mechanicus: Explorators mod",
                "Tyranid Mod",
                "Night Lords mod: We have come for you!",
                "RAGE:World Eaters Warbands",
                "Tartarus and Lorn V Campaigns for Soulstorm"
            });

            modWebAdresses[0] = "https://www.moddb.com/mods/dawn-of-warhammer-40k-firestorm-over-kronus";
            modWebAdresses[1] = "https://www.moddb.com/mods/ultimate-apocalypse-mod";
            modWebAdresses[2] = "https://www.moddb.com/mods/black-templars-kaurava-crusade";
            modWebAdresses[3] = "https://www.moddb.com/mods/dowpro";
            modWebAdresses[4] = "https://www.moddb.com/mods/corncobmans-fun-mod-for-soulstorm";
            modWebAdresses[5] = "https://www.moddb.com/mods/soulstorm-bugfix-mod";
            modWebAdresses[6] = "https://www.moddb.com/mods/titanium-wars-mod-ss";
            modWebAdresses[7] = "https://www.moddb.com/mods/dark-angels-mod";
            modWebAdresses[8] = "https://www.moddb.com/mods/hard-mod";
            modWebAdresses[9] = "https://www.moddb.com/mods/eranthis-project";
            modWebAdresses[10] = "https://www.moddb.com/mods/sister-of-battle-hd-retextures-by-leonardgoog";
            modWebAdresses[11] = "https://www.moddb.com/mods/soulstorm-strongholds";
            modWebAdresses[12] = "https://www.moddb.com/mods/the-dance-macabre-harlequins-mod-for-soulstorm";
            modWebAdresses[13] = "https://www.moddb.com/mods/witch-hunters-mod-for-soulstorm";
            modWebAdresses[14] = "https://www.moddb.com/mods/emperors-children-modfor-the-glory-of-slaanesh";
            modWebAdresses[15] = "https://www.moddb.com/mods/steel-legion-armageddon";
            modWebAdresses[16] = "https://www.moddb.com/mods/thousand-sons-mod";
            modWebAdresses[17] = "https://www.moddb.com/mods/daemonhunters-mod"; 
            modWebAdresses[18] = "https://www.moddb.com/mods/unification-mod-dawn-of-war-soulstorm";
            modWebAdresses[19] = "https://www.moddb.com/mods/salamanders-mod-for-soulstorm";
            modWebAdresses[20] = "https://www.moddb.com/mods/adeptus-mechanicus";
            modWebAdresses[21] = "https://www.moddb.com/mods/tyranid-mod";
            modWebAdresses[22] = "https://www.moddb.com/mods/night-lords-modification-for-soulstorm";
            modWebAdresses[23] = "https://www.moddb.com/mods/rage-world-eaters-warband";
            modWebAdresses[24] = "https://www.moddb.com/mods/tartarus-and-lorn-v-campaigns-for-soulstorm";

            modListBox.Select();
        }

        private void OpenModDBButton_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.moddb.com/games/dawn-of-war/mods?sort=rating-desc");
        }

        private void DownloadModButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not so fast cowboy! This feature a'int ready yet! :-)");
        }

        private void OpenModPageButton_Click(object sender, EventArgs e)
        {
            Process.Start(modWebAdresses[modListBox.SelectedIndex]);
        }
    }
}
