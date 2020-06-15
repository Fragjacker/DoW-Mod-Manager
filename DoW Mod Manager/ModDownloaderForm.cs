using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace DoW_Mod_Manager
{
    public partial class ModDownloaderForm : Form
    {
        private readonly ModManagerForm modManager;
        private readonly string[,] modWebAddresses = new string[35, 3];         // Maybe I change that to List<string> in future

        public ModDownloaderForm(ModManagerForm form)
        {
            InitializeComponent();

            modManager = form;

            if (modManager.CurrentGameEXE == ModManagerForm.GameExecutable.SOULSTORM)
            {
                popularModsLabel.Text += "Soulstorm:";

                modListBox.Items.AddRange(new object[]
                {
                    "Adeptus Mechanicus: Explorators mod",
                    "Black Templars - No Remorse! No Fear!",
                    "Blood Angels mod: By the Blood of Sanguinius!",
                    "Chaos Daemons Mod",
                    "CornCobMan\'s Fun Mod",
                    "Dark Angels mod: Repent! For tomorrow you die!",
                    "Dawn of War: Strongholds",
                    "DoWPro",
                    "DoW40k: Firestorm over Kaurava",
                    "Emperor\'s Children mod:For the Glory of Slaanesh!",
                    "FreeUI",
                    "Hard_mod",
                    "HD Dawn Of War",
                    "Inquisition: Daemonhunt",
                    "Legion of the Damned mod: In dedicato imperatum!",
                    "Men of Praetoria",
                    "Night Lords mod: We have come for you!",
                    "Objective Points SS",
                    "RAGE:World Eaters Warbands",
                    "Redux Mod",
                    "Renegade Guard",
                    "Salamanders mod REFORGED: Unto the Anvil of War!",
                    "Space Wolves",
                    "Steel Legion - Armageddon",
                    "Soulstorm Bugfix Mod",
                    "Tartarus and Lorn V Campaigns for Soulstorm",
                    "The Dance Macabre - Harlequins Mod",
                    "Thirteenth Company Mod",
                    "Thousand Sons mod: Knowledge Is Power!",
                    "Titanium Wars Mod for Soulstorm",
                    "Tyranid Mod",
                    "Unification Mod",
                    "Ultimate Apocalypse Mod",
                    "Vostroyan Firstborn Modification for Soulstorm",
                    "Witch Hunters: Adepta Sororitas"
                });

                byte i = 0;

                // modWebAddresses[i  , 0] - Mod page address
                // modWebAddresses[i  , 1] - Mod download address
                // modWebAddresses[i++, 2] - Mod patch address (and incrementing counter for the next mod)

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/adeptus-mechanicus";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/122435?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fadeptus-mechanicus%2Fdownloads";
                modWebAddresses[i++, 2] = "";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/black-templars-kaurava-crusade";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/47751?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fblack-templars-kaurava-crusade%2Fdownloads";
                modWebAddresses[i++, 2] = "https://www.moddb.com/downloads/start/93653?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fblack-templars-kaurava-crusade%2Fdownloads";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/stevocarty";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/103615?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fstevocarty%2Fdownloads";
                modWebAddresses[i++, 2] = "";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/daemons-mod";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/93757?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fdaemons-mod%2Fdownloads";
                modWebAddresses[i++, 2] = "";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/corncobmans-fun-mod-for-soulstorm";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/196090?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fcorncobmans-fun-mod-for-soulstorm%2Fdownloads";
                modWebAddresses[i++, 2] = "";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/dark-angels-mod";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/179584?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fdark-angels-mod%2Fdownloads";
                modWebAddresses[i++, 2] = "";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/soulstorm-strongholds";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/136305?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fsoulstorm-strongholds%2Fdownloads";
                modWebAddresses[i++, 2] = "https://www.moddb.com/downloads/start/195179?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fsoulstorm-strongholds%2Fdownloads";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/dowpro";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/182958?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fdowpro%2Fdownloads";
                modWebAddresses[i++, 2] = "";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/dawn-of-warhammer-40k-firestorm-over-kronus";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/82196?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fdawn-of-warhammer-40k-firestorm-over-kronus%2Fdownloads";
                modWebAddresses[i++, 2] = "";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/emperors-children-modfor-the-glory-of-slaanesh";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/178558?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Femperors-children-modfor-the-glory-of-slaanesh%2Fdownloads";
                modWebAddresses[i++, 2] = "";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/unification-mod-dawn-of-war-soulstorm/downloads/freeui";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/132081?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Funification-mod-dawn-of-war-soulstorm%2Fdownloads";
                modWebAddresses[i++, 2] = "";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/hard-mod";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/174556?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fhard-mod%2Fdownloads";
                modWebAddresses[i++, 2] = "";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/sister-of-battle-hd-retextures-by-leonardgoog";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/63755?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fsister-of-battle-hd-retextures-by-leonardgoog%2Fdownloads";
                modWebAddresses[i++, 2] = "";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/daemonhunters-mod";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/177251?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fdaemonhunters-mod%2Fdownloads";
                modWebAddresses[i++, 2] = "";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/legion-of-the-damned-modification-for-soulstorm";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/108154?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Flegion-of-the-damned-modification-for-soulstorm%2Fdownloads";
                modWebAddresses[i++, 2] = "https://www.moddb.com/downloads/start/122434?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Flegion-of-the-damned-modification-for-soulstorm%2Fdownloads";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/men-of-praetoria";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/116144?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fmen-of-praetoria%2Fdownloads";
                modWebAddresses[i++, 2] = "";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/night-lords-modification-for-soulstorm";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/140247?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fnight-lords-modification-for-soulstorm%2Fdownloads";
                modWebAddresses[i++, 2] = "";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/unification-mod-dawn-of-war-soulstorm/downloads/objective-points-ss-v1742020-for-dowdc-and-dowss";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/192702?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Funification-mod-dawn-of-war-soulstorm%2Fdownloads";
                modWebAddresses[i++, 2] = "";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/rage-world-eaters-warband";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/101179?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Frage-world-eaters-warband%2Fdownloads";
                modWebAddresses[i++, 2] = "https://www.moddb.com/downloads/start/101765?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Frage-world-eaters-warband%2Fdownloads";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/eranthis-project";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/190876?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Feranthis-project%2Fdownloads";
                modWebAddresses[i++, 2] = "https://www.moddb.com/downloads/start/195426?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Feranthis-project%2Fdownloads";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/renegade-guard";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/135315?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Frenegade-guard%2Fdownloads";
                modWebAddresses[i++, 2] = "";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/salamanders-mod-for-soulstorm";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/184518?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fsalamanders-mod-for-soulstorm%2Fdownloads";
                modWebAddresses[i++, 2] = "https://www.moddb.com/downloads/start/184624?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fsalamanders-mod-for-soulstorm%2Fdownloads";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/space-wolves";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/189241?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fspace-wolves%2Fdownloads";
                modWebAddresses[i++, 2] = "";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/steel-legion-armageddon";
                modWebAddresses[i, 1] = "http://www.mediafire.com/?etfl8eu94mkmbyk";
                modWebAddresses[i++, 2] = "http://www.mediafire.com/file/ec7st9hj8lnry7h/Steel_Legions_SS_1.0_Simple+Patch.rar";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/soulstorm-bugfix-mod";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/124372?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fsoulstorm-bugfix-mod%2Fdownloads";
                modWebAddresses[i++, 2] = "";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/tartarus-and-lorn-v-campaigns-for-soulstorm";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/170256?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Ftartarus-and-lorn-v-campaigns-for-soulstorm%2Fdownloads";
                modWebAddresses[i++, 2] = "https://www.moddb.com/downloads/start/170654?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Ftartarus-and-lorn-v-campaigns-for-soulstorm%2Fdownloads";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/the-dance-macabre-harlequins-mod-for-soulstorm";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/181447?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fthe-dance-macabre-harlequins-mod-for-soulstorm%2Fdownloads";
                modWebAddresses[i++, 2] = "";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/thirteenth-company-mod-for-dawn-of-war-soulstorm";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/176576?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fthirteenth-company-mod-for-dawn-of-war-soulstorm%2Fdownloads";
                modWebAddresses[i++, 2] = "";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/thousand-sons-mod";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/116283?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fthousand-sons-mod%2Fdownloads";
                modWebAddresses[i++, 2] = "";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/titanium-wars-mod-ss";
                modWebAddresses[i, 1] = "https://rutracker.org/forum/dl.php?t=4859187";
                modWebAddresses[i++, 2] = "";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/tyranid-mod";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/46895?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Ftyranid-mod%2Fdownloads";
                modWebAddresses[i++, 2] = "";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/unification-mod-dawn-of-war-soulstorm";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/132080?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Funification-mod-dawn-of-war-soulstorm%2Fdownloads";
                modWebAddresses[i++, 2] = "";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/ultimate-apocalypse-mod";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/192014?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fultimate-apocalypse-mod%2Fdownloads";
                modWebAddresses[i++, 2] = "";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/vostroyan-firstborn-modification-for-soulstorm";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/127232?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fvostroyan-firstborn-modification-for-soulstorm%2Fdownloads";
                modWebAddresses[i++, 2] = "";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/witch-hunters-mod-for-soulstorm";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/177550?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fwitch-hunters-mod-for-soulstorm%2Fdownloads";
                modWebAddresses[i++, 2] = "";
            }
            else if (modManager.CurrentGameEXE == ModManagerForm.GameExecutable.DARK_CRUSADE)
            {
                popularModsLabel.Text += "Dark Crusade:";

                modListBox.Items.AddRange(new object[]
                {
                    "Dawn of Steel - Steel Legion Mod",
                    "Dawn of War: Dark Crusade Bugfix Mod",
                    "DoWPro",
                    "Firestorm over Kronus",
                    "Inquisition: Daemonhunt",
                    "D.O.W. Kingdom",
                    "M42",
                    "Tabletop Round-up",
                    "Titanium Wars Mod",
                    "Tyranid Mod",
                    "Updated Campaign Mod",
                    "Veteran Mod",
                    "Witch Hunters: Adepta Sororitas"
                });

                byte i = 0;

                modWebAddresses[i, 0] = "https://www.moddb.com/games/dawn-of-war-dark-crusade/addons/dawn-of-steel-steel-legion-mod-1-0";
                modWebAddresses[i, 1] = "https://www.moddb.com/addons/start/152298";
                modWebAddresses[i++, 2] = "";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/dawn-of-war-dark-crusade-bugfix-mod";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/144937?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fdawn-of-war-dark-crusade-bugfix-mod%2Fdownloads";
                modWebAddresses[i++, 2] = "";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/dowpro";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/12671?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fdowpro%2Fdownloads";
                modWebAddresses[i++, 2] = "https://www.moddb.com/downloads/start/12733?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fdowpro%2Fdownloads";

                modWebAddresses[i, 0] = "http://fok.dow-mods.com/viewtopic.php?f=141&t=4234";
                modWebAddresses[i, 1] = "http://www.moddb.com/downloads/start/23967";
                modWebAddresses[i++, 2] = "";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/daemonhunters-mod";
                modWebAddresses[i, 1] = "http://download1347.mediafire.com/delol5z1m9dg/zc41u4pexbg5rua/inquisition_daemonhunt_mod+DC.zip";
                modWebAddresses[i++, 2] = "";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/dow-kingdom";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/8179?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fdow-kingdom%2Fdownloads";
                modWebAddresses[i++, 2] = "";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/m42-dark-crusade";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/184047?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fm42-dark-crusade%2Fdownloads";
                modWebAddresses[i++, 2] = "";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/tabletop-round-up";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/9231?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Ftabletop-round-up%2Fdownloads";
                modWebAddresses[i++, 2] = "";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/titanium-wars-mod";
                modWebAddresses[i, 1] = "https://rutracker.org/forum/dl.php?t=4859187";
                modWebAddresses[i++, 2] = "";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/tyranid-mod";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/11333?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Ftyranid-mod%2Fdownloads";
                modWebAddresses[i++, 2] = "https://www.moddb.com/downloads/start/11362?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Ftyranid-mod%2Fdownloads";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/dc-updated-campaign-mod";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/75192?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fdc-updated-campaign-mod%2Fdownloads";
                modWebAddresses[i++, 2] = "";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/dc-veteran-mod";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/131673?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fdc-veteran-mod%2Fdownloads";
                modWebAddresses[i++, 2] = "";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/witch-hunters-mod-for-soulstorm";
                modWebAddresses[i, 1] = "https://www.gamefront.com/files/witch-hunters-mod-beta/download";
                modWebAddresses[i++, 2] = "";
            }
            else if (modManager.CurrentGameEXE == ModManagerForm.GameExecutable.WINTER_ASSAULT)
            {
                popularModsLabel.Text += "Winter Assault:";

                modListBox.Items.AddRange(new object[]
                {
                    "Dawn of Steel",
                    "DoWPro",
                    "Necrons Mod - Mike's version",
                    "Tabletop Round-up",
                    "Total War: Winter Assault",
                    "WXP Necron Mod"
                });

                byte i = 0;

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/steel-legion-armageddon";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/165510?referer=https%3A%2F%2Fwww.google.com%2F";
                modWebAddresses[i++, 2] = "";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/dowpro";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/6548?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fdowpro%2Fdownloads";
                modWebAddresses[i++, 2] = "https://www.moddb.com/downloads/start/6549?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fdowpro%2Fdownloads";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/necrons-mod";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/64724?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fnecrons-mod%2Fdownloads";
                modWebAddresses[i++, 2] = "";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/tabletop-round-up";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/5311?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Ftabletop-round-up%2Fdownloads";
                modWebAddresses[i++, 2] = "";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/total-war-winter-assault";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/1266?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Ftotal-war-winter-assault%2Fdownloads";
                modWebAddresses[i++, 2] = "";

                modWebAddresses[i, 0] = "https://www.moddb.com/mods/wxp-necron-mod";
                modWebAddresses[i, 1] = "https://www.moddb.com/downloads/start/76841?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fwxp-necron-mod%2Fdownloads";
                modWebAddresses[i++, 2] = "";
            }
            else if (modManager.CurrentGameEXE == ModManagerForm.GameExecutable.ORIGINAL)
            {
                popularModsLabel.Text += "Original:";

                // I have to find a few mods for Original first :-)
                modListBox.Items.AddRange(new object[]
                {
                    "First I have to find a few mods for Original :-)"
                });

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

            address = modWebAddresses[modListBox.SelectedIndex, 2];
            if (address.Length > 0)
            {
                Thread.Sleep(250);
                Process.Start(address);
            }
        }
    }
}