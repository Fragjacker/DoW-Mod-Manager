using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace DoW_Mod_Manager
{
    public partial class ModDownloaderForm : Form
    {
        private readonly ModManagerForm modManager;
        private readonly string[,] modWebAddresses = new string[30, 3];

        public ModDownloaderForm(ModManagerForm form)
        {
            InitializeComponent();

            modManager = form;

            if (modManager.CurrentGameEXE == ModManagerForm.GameExecutable.SOULSTORM)
            {
                popularModsLabel.Text += "Soulstorm:";
                
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
                    "Tartarus and Lorn V Campaigns for Soulstorm",
                    "Legion of the Damned mod: In dedicato imperatum!",
                    "Vostroyan Firstborn Modification for Soulstorm",
                    "Chaos Daemons Mod",
                    "Blood Angels mod: By the Blood of Sanguinius!",
                    "Renegade Guard"
                });

                sbyte i = 0;

                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/dawn-of-warhammer-40k-firestorm-over-kronus";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/82196/119/04ca2b48993200c43c06b6605dd331e3/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fdawn-of-warhammer-40k-firestorm-over-kronus%2Fdownloads";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/ultimate-apocalypse-mod";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/192014/119/dd57ed990962423a1ef224f880755ca4/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fultimate-apocalypse-mod%2Fdownloads";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/black-templars-kaurava-crusade";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/47751/123/3562674cf21e0a83ab1a30aade5cf741/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fblack-templars-kaurava-crusade%2Fdownloads";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/dowpro";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/182958/125/2830bbbba10cf596f0b376bab9b66edb/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fdowpro%2Fdownloads";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/corncobmans-fun-mod-for-soulstorm";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/194920/124/c26d60180ae2a281678a2629b5ad100d/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fcorncobmans-fun-mod-for-soulstorm%2Fdownloads";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/soulstorm-bugfix-mod";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/124372/121/bad3d3d8ad18f5557892606afc0147e6/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fsoulstorm-bugfix-mod%2Fdownloads";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/titanium-wars-mod-ss";
                modWebAddresses[i  , 1] = "https://rutracker.org/forum/dl.php?t=4859187";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/dark-angels-mod";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/179584/126/8b63e10a341d56c81d3ee01b19cc54a8/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fdark-angels-mod%2Fdownloads";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/hard-mod";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/174556/126/41844df7dc86ac7fa957c502fb2ce8d8/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fhard-mod%2Fdownloads";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/eranthis-project";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/190876/126/f24aa80417a679395d91a5ffb0e93f7d/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Feranthis-project%2Fdownloads";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/sister-of-battle-hd-retextures-by-leonardgoog";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/63755/125/7c75bc836603e76999880de6f45da163/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fsister-of-battle-hd-retextures-by-leonardgoog%2Fdownloads";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/soulstorm-strongholds";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/136305/125/6503cd63f316ef704e4da8734c653088/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fsoulstorm-strongholds%2Fdownloads";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/the-dance-macabre-harlequins-mod-for-soulstorm";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/181447/125/b468fee500cdf2ad7fb9a2f7edcf6e89/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fthe-dance-macabre-harlequins-mod-for-soulstorm%2Fdownloads";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/witch-hunters-mod-for-soulstorm";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/177550/125/fa7e30abe888f9aa3b0fed146f57a4a5/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fwitch-hunters-mod-for-soulstorm%2Fdownloads";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/emperors-children-modfor-the-glory-of-slaanesh";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/178558/125/c28e558b88c28d033f0a5a4c69683509/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Femperors-children-modfor-the-glory-of-slaanesh%2Fdownloads";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/steel-legion-armageddon";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/163326/120/3b4ac5b5b823d7138bca0f8ebe154cc7/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fsteel-legion-armageddon%2Fdownloads";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/thousand-sons-mod";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/116283/120/3ff6bcd681c2fe06857400b71619d886/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fthousand-sons-mod%2Fdownloads";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/daemonhunters-mod";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/177251/120/cf4522b725346ef11d7eff89cbf74cf5/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fdaemonhunters-mod%2Fdownloads";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/unification-mod-dawn-of-war-soulstorm";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/132080/120/8f70b780a8896bac3a0220270630da7c/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Funification-mod-dawn-of-war-soulstorm%2Fdownloads";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/salamanders-mod-for-soulstorm";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/184518/126/9e749c69b89db8c85915304827bd1d12/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fsalamanders-mod-for-soulstorm%2Fdownloads";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/adeptus-mechanicus";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/122435/126/4fb290a78e80b1f9801d7f797d0047ac/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fadeptus-mechanicus%2Fdownloads";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/tyranid-mod";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/46895/126/3c66a5eea67341017acea0450b9ea8e6/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Ftyranid-mod%2Fdownloads";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/night-lords-modification-for-soulstorm";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/140247/126/689f1731ce4a6a14c6ee9df16f36dd06/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fnight-lords-modification-for-soulstorm%2Fdownloads";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/rage-world-eaters-warband";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/101179/126/94d7285eb9f9de26328ebe4e157acde3/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Frage-world-eaters-warband%2Fdownloads";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/tartarus-and-lorn-v-campaigns-for-soulstorm";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/170256/123/1802a56445449454dfea62fbd33671d9/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Ftartarus-and-lorn-v-campaigns-for-soulstorm%2Fdownloads";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/legion-of-the-damned-modification-for-soulstorm";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/108154/119/1a7779b0957e30066ac1a95af24afc5b/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Flegion-of-the-damned-modification-for-soulstorm%2Fdownloads";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/vostroyan-firstborn-modification-for-soulstorm";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/127232/125/88ea1567b372339d7bc8e6c44c24bd5f/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fvostroyan-firstborn-modification-for-soulstorm%2Fdownloads";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/daemons-mod";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/93757/115/0ff4a82d5d8c1d46c0134c7bda686f37/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fdaemons-mod%2Fdownloads";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/stevocarty";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/103615/115/1123174931b82db5e917775589fc8113/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fstevocarty%2Fdownloads";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/renegade-guard";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/135315/126/1ab67a8a932ce7e5a16bbea4ad3fb630/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Frenegade-guard%2Fdownloads";
                modWebAddresses[i++, 2] = "";
            }
            else if (modManager.CurrentGameEXE == ModManagerForm.GameExecutable.DARK_CRUSADE)
            {
                popularModsLabel.Text += "Dark Crusade:";

                modListBox.Items.AddRange(new object[]
                {
                    "DoWPro",
                    "Titanium Wars Mod",
                    "Veteran Mod",
                    "Tyranid Mod",
                    "Tabletop Round-up",
                    "M42",
                    "Dawn of War: Dark Crusade Bugfix Mod",
                    "Updated Campaign Mod",
                    "Dawn of Steel - Steel Legion Mod",
                    "Inquisition: Daemonhunt",
                    "Witch Hunters: Adepta Sororitas",
                    "Firestorm over Kronus",
                    "D.O.W. Kingdom"
                });

                sbyte i = 0;

                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/dowpro";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/12671/121/2838f64cc5b631f3f3deba83dba88818/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fdowpro%2Fdownloads";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/titanium-wars-mod";
                modWebAddresses[i  , 1] = "https://rutracker.org/forum/dl.php?t=4859187";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/dc-veteran-mod";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/131671/124/77bd844e16e34decbe263c3b32010630/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fdc-veteran-mod%2Fdownloads";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/tyranid-mod";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/11333/123/b2ffd67f6fec1144762e5770cb389019/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Ftyranid-mod%2Fdownloads";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/tabletop-round-up";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/9231/123/eb8d8037325d3a3500931fb6e1167452/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Ftabletop-round-up%2Fdownloads";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/m42-dark-crusade";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/184047/125/51e067ddcca7095dd0e19c2ca6400d56/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fm42-dark-crusade%2Fdownloads";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/dawn-of-war-dark-crusade-bugfix-mod";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/144937/122/449656b2818b41bdb8488838febd9ce0/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fdawn-of-war-dark-crusade-bugfix-mod%2Fdownloads";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/dc-updated-campaign-mod";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/75192/123/99ab16a9b37551d305f760b797b43214/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fdc-updated-campaign-mod%2Fdownloads";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/games/dawn-of-war-dark-crusade/addons/dawn-of-steel-steel-legion-mod-1-0";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/152298/124/6d9de7c5cb8c6cd146cc5c56c783aa32/?referer=https%3A%2F%2Fwww.google.com%2F";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/daemonhunters-mod";
                modWebAddresses[i  , 1] = "http://download1347.mediafire.com/delol5z1m9dg/zc41u4pexbg5rua/inquisition_daemonhunt_mod+DC.zip";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/witch-hunters-mod-for-soulstorm";
                modWebAddresses[i  , 1] = "https://www.gamefront.com/files/witch-hunters-mod-beta/download";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "http://fok.dow-mods.com/viewtopic.php?f=141&t=4234";
                modWebAddresses[i  , 1] = "http://www.moddb.com/downloads/start/23967";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/dow-kingdom";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/8179/122/bca82830235d05da192448814fc56d64/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fdow-kingdom%2Fdownloads";
                modWebAddresses[i++, 2] = "";
            }
            else if (modManager.CurrentGameEXE == ModManagerForm.GameExecutable.WINTER_ASSAULT)
            {
                popularModsLabel.Text += "Winter Assault:";

                modListBox.Items.AddRange(new object[]
                {
                    "DoWPro",
                    "Dawn of Steel",
                    "Tabletop Round-up",
                    "Necrons Mod - Mike's version",
                    "WXP Necron Mod",
                    "Total War: Winter Assault"
                });

                sbyte i = 0;

                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/dowpro";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/6548/120/8788be1e3afd31874b5fe8bdcfcfc01d/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fdowpro%2Fdownloads";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/steel-legion-armageddon";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/165510/124/9c8d5711e7a8a3e06ee86da48c95f34f/?referer=https%3A%2F%2Fwww.google.com%2F";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/tabletop-round-up";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/5311/121/a19cf9b5ef9ddb70829897805df44c47/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Ftabletop-round-up%2Fdownloads";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/necrons-mod";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/64724/126/a0c40bd12cabc09c88a131131942a340/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fnecrons-mod%2Fdownloads";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/wxp-necron-mod";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/76841/121/a3c0124133005765fbea9c4d425841a6/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fwxp-necron-mod%2Fdownloads";
                modWebAddresses[i++, 2] = "";
                modWebAddresses[i  , 0] = "https://www.moddb.com/mods/total-war-winter-assault";
                modWebAddresses[i  , 1] = "https://www.moddb.com/downloads/mirror/1266/123/c364f5e488add7b541dab73f434f5964/?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Ftotal-war-winter-assault%2Fdownloads";
                modWebAddresses[i++, 2] = "";
            }
            else if (modManager.CurrentGameEXE == ModManagerForm.GameExecutable.ORIGINAL)
            {
                popularModsLabel.Text += "Original:";

                // I have to find a few mods for Original first :-)

                openModPageButton.Enabled = false;
                downloadModButton.Enabled = false;
            }

            modListBox.Select();
        }

        private void OpenModDBButton_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.moddb.com/games/dawn-of-war/mods?sort=rating-desc");
        }

        private void OpenModPageButton_Click(object sender, EventArgs e)
        {
            string address = modWebAddresses[modListBox.SelectedIndex, 0];
            if (address.Length > 0)
                Process.Start(address);
        }

        private void DownloadModButton_Click(object sender, EventArgs e)
        {
            string address = modWebAddresses[modListBox.SelectedIndex, 1];
            if (address.Length > 0)
                Process.Start(address);
        }
    }
}
