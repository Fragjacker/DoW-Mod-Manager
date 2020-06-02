using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace DoW_Mod_Manager
{
    public partial class ModDownloaderForm : Form
    {
        private readonly string[,] modWebAdresses = new string[25, 2];

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

            modWebAdresses[0, 0] = "https://www.moddb.com/mods/dawn-of-warhammer-40k-firestorm-over-kronus";
            modWebAdresses[0, 1] = "https://www.moddb.com/downloads/mirror/82196/119/04ca2b48993200c43c06b6605dd331e3/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fdawn-of-warhammer-40k-firestorm-over-kronus%2Fdownloads";
            modWebAdresses[1, 0] = "https://www.moddb.com/mods/ultimate-apocalypse-mod";
            modWebAdresses[1, 1] = "https://www.moddb.com/downloads/mirror/192014/119/dd57ed990962423a1ef224f880755ca4/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fultimate-apocalypse-mod%2Fdownloads";
            modWebAdresses[2, 0] = "https://www.moddb.com/mods/black-templars-kaurava-crusade";
            modWebAdresses[2, 1] = "https://www.moddb.com/downloads/mirror/47751/123/3562674cf21e0a83ab1a30aade5cf741/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fblack-templars-kaurava-crusade%2Fdownloads";
            modWebAdresses[3, 0] = "https://www.moddb.com/mods/dowpro";
            modWebAdresses[3, 1] = "https://www.moddb.com/downloads/mirror/182958/125/2830bbbba10cf596f0b376bab9b66edb/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fdowpro%2Fdownloads";
            modWebAdresses[4, 0] = "https://www.moddb.com/mods/corncobmans-fun-mod-for-soulstorm";
            modWebAdresses[4, 1] = "https://www.moddb.com/downloads/mirror/194920/124/c26d60180ae2a281678a2629b5ad100d/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fcorncobmans-fun-mod-for-soulstorm%2Fdownloads";
            modWebAdresses[5, 0] = "https://www.moddb.com/mods/soulstorm-bugfix-mod";
            modWebAdresses[5, 1] = "https://www.moddb.com/downloads/mirror/124372/121/bad3d3d8ad18f5557892606afc0147e6/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fsoulstorm-bugfix-mod%2Fdownloads";
            modWebAdresses[6, 0] = "https://www.moddb.com/mods/titanium-wars-mod-ss";
            modWebAdresses[6, 1] = "https://rutracker.org/forum/dl.php?t=4859187";
            modWebAdresses[7, 0] = "https://www.moddb.com/mods/dark-angels-mod";
            modWebAdresses[7, 1] = "https://www.moddb.com/downloads/mirror/179584/126/8b63e10a341d56c81d3ee01b19cc54a8/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fdark-angels-mod%2Fdownloads";
            modWebAdresses[8, 0] = "https://www.moddb.com/mods/hard-mod";
            modWebAdresses[8, 1] = "https://www.moddb.com/downloads/mirror/174556/126/41844df7dc86ac7fa957c502fb2ce8d8/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fhard-mod%2Fdownloads";
            modWebAdresses[9, 0] = "https://www.moddb.com/mods/eranthis-project";
            modWebAdresses[9, 1] = "https://www.moddb.com/downloads/mirror/190876/126/f24aa80417a679395d91a5ffb0e93f7d/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Feranthis-project%2Fdownloads";
            modWebAdresses[10, 0] = "https://www.moddb.com/mods/sister-of-battle-hd-retextures-by-leonardgoog";
            modWebAdresses[10, 1] = "https://www.moddb.com/downloads/mirror/63755/125/7c75bc836603e76999880de6f45da163/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fsister-of-battle-hd-retextures-by-leonardgoog%2Fdownloads";
            modWebAdresses[11, 0] = "https://www.moddb.com/mods/soulstorm-strongholds";
            modWebAdresses[11, 1] = "https://www.moddb.com/downloads/mirror/136305/125/6503cd63f316ef704e4da8734c653088/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fsoulstorm-strongholds%2Fdownloads";
            modWebAdresses[12, 0] = "https://www.moddb.com/mods/the-dance-macabre-harlequins-mod-for-soulstorm";
            modWebAdresses[12, 1] = "https://www.moddb.com/downloads/mirror/181447/125/b468fee500cdf2ad7fb9a2f7edcf6e89/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fthe-dance-macabre-harlequins-mod-for-soulstorm%2Fdownloads";
            modWebAdresses[13, 0] = "https://www.moddb.com/mods/witch-hunters-mod-for-soulstorm";
            modWebAdresses[13, 1] = "https://www.moddb.com/downloads/mirror/177550/125/fa7e30abe888f9aa3b0fed146f57a4a5/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fwitch-hunters-mod-for-soulstorm%2Fdownloads";
            modWebAdresses[14, 0] = "https://www.moddb.com/mods/emperors-children-modfor-the-glory-of-slaanesh";
            modWebAdresses[14, 1] = "https://www.moddb.com/downloads/mirror/178558/125/c28e558b88c28d033f0a5a4c69683509/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Femperors-children-modfor-the-glory-of-slaanesh%2Fdownloads";
            modWebAdresses[15, 0] = "https://www.moddb.com/mods/steel-legion-armageddon";
            modWebAdresses[15, 1] = "https://www.moddb.com/downloads/mirror/163326/120/3b4ac5b5b823d7138bca0f8ebe154cc7/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fsteel-legion-armageddon%2Fdownloads";
            modWebAdresses[16, 0] = "https://www.moddb.com/mods/thousand-sons-mod";
            modWebAdresses[16, 1] = "https://www.moddb.com/downloads/mirror/116283/120/3ff6bcd681c2fe06857400b71619d886/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fthousand-sons-mod%2Fdownloads";
            modWebAdresses[17, 0] = "https://www.moddb.com/mods/daemonhunters-mod";
            modWebAdresses[17, 1] = "https://www.moddb.com/downloads/mirror/177251/120/cf4522b725346ef11d7eff89cbf74cf5/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fdaemonhunters-mod%2Fdownloads";
            modWebAdresses[18, 0] = "https://www.moddb.com/mods/unification-mod-dawn-of-war-soulstorm";
            modWebAdresses[18, 1] = "https://www.moddb.com/downloads/mirror/132080/120/8f70b780a8896bac3a0220270630da7c/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Funification-mod-dawn-of-war-soulstorm%2Fdownloads";
            modWebAdresses[19, 0] = "https://www.moddb.com/mods/salamanders-mod-for-soulstorm";
            modWebAdresses[19, 1] = "https://www.moddb.com/downloads/mirror/184518/126/9e749c69b89db8c85915304827bd1d12/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fsalamanders-mod-for-soulstorm%2Fdownloads";
            modWebAdresses[20, 0] = "https://www.moddb.com/mods/adeptus-mechanicus";
            modWebAdresses[20, 1] = "https://www.moddb.com/downloads/mirror/122435/126/4fb290a78e80b1f9801d7f797d0047ac/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fadeptus-mechanicus%2Fdownloads";
            modWebAdresses[21, 0] = "https://www.moddb.com/mods/tyranid-mod";
            modWebAdresses[21, 1] = "https://www.moddb.com/downloads/mirror/46895/126/3c66a5eea67341017acea0450b9ea8e6/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Ftyranid-mod%2Fdownloads";
            modWebAdresses[22, 0] = "https://www.moddb.com/mods/night-lords-modification-for-soulstorm";
            modWebAdresses[22, 1] = "https://www.moddb.com/downloads/mirror/140247/126/689f1731ce4a6a14c6ee9df16f36dd06/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fnight-lords-modification-for-soulstorm%2Fdownloads";
            modWebAdresses[23, 0] = "https://www.moddb.com/mods/rage-world-eaters-warband";
            modWebAdresses[23, 1] = "https://www.moddb.com/downloads/mirror/101179/126/94d7285eb9f9de26328ebe4e157acde3/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Frage-world-eaters-warband%2Fdownloads";
            modWebAdresses[24, 0] = "https://www.moddb.com/mods/tartarus-and-lorn-v-campaigns-for-soulstorm";
            modWebAdresses[24, 1] = "https://www.moddb.com/downloads/mirror/170256/123/1802a56445449454dfea62fbd33671d9/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Ftartarus-and-lorn-v-campaigns-for-soulstorm%2Fdownloads";

            modListBox.Select();
        }

        private void OpenModDBButton_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.moddb.com/games/dawn-of-war/mods?sort=rating-desc");
        }

        private void DownloadModButton_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Not so fast cowboy! This feature a'int ready yet! :-)");
            Process.Start(modWebAdresses[modListBox.SelectedIndex, 1]);
        }

        private void OpenModPageButton_Click(object sender, EventArgs e)
        {
            Process.Start(modWebAdresses[modListBox.SelectedIndex, 0]);
        }
    }
}
