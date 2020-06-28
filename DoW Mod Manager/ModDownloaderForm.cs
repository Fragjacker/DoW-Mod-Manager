using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace DoW_Mod_Manager
{
    public partial class ModDownloaderForm : Form
    {
        public class Mod
        {
            public string ModName;
            public string ModuleFileName;
            public string SiteLink;
            public string DownloadLink;
            public string PatchLink;

            public Mod(string modName, string moduleFileName, string siteLink, string modDownloadLink, string patchDwonloadLink)
            {
                ModName = modName;
                ModuleFileName = moduleFileName;
                SiteLink = siteLink;
                DownloadLink = modDownloadLink;
                PatchLink = patchDwonloadLink;
            }
        }

        private const string SEARCH_TEXT = "Search...";
        private readonly List<Mod> modlist = new List<Mod>();

        private bool findByModuleName = true;

        private readonly ModManagerForm modManager;

        public ModDownloaderForm(ModManagerForm form, string moduleFileName = "")
        {
            InitializeComponent();

            modManager = form;

            // Use the same icon as executable
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            if (modManager.CurrentGameEXE == ModManagerForm.GameExecutable.SOULSTORM)
            {
                popularModsLabel.Text += "Soulstorm:";

                modlist.Add(new Mod("Adeptus Mechanicus: Explorators mod  (Version RC 1.1)",
                    "AdeptusMechanicus",
                    "https://www.moddb.com/mods/adeptus-mechanicus",
                    "https://www.moddb.com/downloads/start/122435?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fadeptus-mechanicus%2Fdownloads",
                    ""));

                modlist.Add(new Mod("Black Templars - No Remorse! No Fear!  (Version Beta 5 + Patch 5.5.022)",
                    "BTmod",
                    "https://www.moddb.com/mods/black-templars-kaurava-crusade",
                    "https://www.moddb.com/downloads/start/47751?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fblack-templars-kaurava-crusade%2Fdownloads",
                    "https://www.moddb.com/downloads/start/93653?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fblack-templars-kaurava-crusade%2Fdownloads"));

                modlist.Add(new Mod("Blood Angels mod: By the Blood of Sanguinius!  (Version Beta 1.98)",
                    "Blood_Angels_SS",
                    "https://www.moddb.com/mods/stevocarty",
                    "https://www.moddb.com/downloads/start/103615?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fstevocarty%2Fdownloads",
                    ""));

                modlist.Add(new Mod("Chaos Daemons Mod  (Version 2.0)",
                    "Chaos_Daemons",
                    "https://www.moddb.com/mods/daemons-mod",
                    "https://www.moddb.com/downloads/start/93757?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fdaemons-mod%2Fdownloads",
                    ""));

                modlist.Add(new Mod("CornCobMan\'s Fun Mod  (Version 1.88)",
                    "CornCobMod",
                    "https://www.moddb.com/mods/corncobmans-fun-mod-for-soulstorm",
                    "https://www.moddb.com/downloads/start/196666?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fcorncobmans-fun-mod-for-soulstorm%2Fdownloads",
                    ""));

                modlist.Add(new Mod("Dark Angels mod: Repent! For tomorrow you die!  (Version 4.0)",
                    "Dark_Angels",
                    "https://www.moddb.com/mods/dark-angels-mod",
                    "https://www.moddb.com/downloads/start/179584?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fdark-angels-mod%2Fdownloads",
                    ""));

                modlist.Add(new Mod("Dawn Of Skirmish AI Mod  (Version 3.20)",
                    "dowai",
                    "https://www.moddb.com/mods/black-templars-kaurava-crusade/downloads/dawn-of-skirmish-ai-mod-v320-for-dowsoulstorm",
                    "https://www.moddb.com/downloads/start/96961?referer=https%3A%2F%2Fwww.google.com%2F",
                    ""));

                modlist.Add(new Mod("Dawn of War: Strongholds  (Version 1.7.0 + Patch 1.7.6)",
                    "stronghold_missions_ss",
                    "https://www.moddb.com/mods/soulstorm-strongholds",
                    "https://www.moddb.com/downloads/start/136305?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fsoulstorm-strongholds%2Fdownloads",
                    "https://www.moddb.com/downloads/start/195179?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fsoulstorm-strongholds%2Fdownloads"));

                modlist.Add(new Mod("DoWPro  (Version 3.69.4)",
                    "DoWpro",
                    "https://www.moddb.com/mods/dowpro",
                    "https://www.moddb.com/downloads/start/182958?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fdowpro%2Fdownloads",
                    ""));

                modlist.Add(new Mod("DoW40k: Firestorm over Kaurava  (Version 2)",
                    "DoW40k_SS",
                    "https://www.moddb.com/mods/dawn-of-warhammer-40k-firestorm-over-kronus",
                    "https://www.moddb.com/downloads/start/82196?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fdawn-of-warhammer-40k-firestorm-over-kronus%2Fdownloads",
                    ""));

                modlist.Add(new Mod("Emperor\'s Children mod:For the Glory of Slaanesh!  (Version 1.3)",
                    "Emperors_Children",
                    "https://www.moddb.com/mods/emperors-children-modfor-the-glory-of-slaanesh",
                    "https://www.moddb.com/downloads/start/178558?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Femperors-children-modfor-the-glory-of-slaanesh%2Fdownloads",
                    ""));

                modlist.Add(new Mod("FreeUI  (Version 2.46)",
                    "FreeUI",
                    "https://www.moddb.com/mods/unification-mod-dawn-of-war-soulstorm/downloads/freeui",
                    "https://www.moddb.com/downloads/start/132081?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Funification-mod-dawn-of-war-soulstorm%2Fdownloads",
                    ""));

                modlist.Add(new Mod("Hard_mod  (Version 0.0.2)",
                    "BaMod",
                    "https://www.moddb.com/mods/hard-mod",
                    "https://www.moddb.com/downloads/start/174556?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fhard-mod%2Fdownloads",
                    ""));

                modlist.Add(new Mod("HD Dawn Of War  (Version 1.0.0)",
                    "",
                    "https://www.moddb.com/mods/sister-of-battle-hd-retextures-by-leonardgoog",
                    "https://www.moddb.com/downloads/start/63755?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fsister-of-battle-hd-retextures-by-leonardgoog%2Fdownloads",
                    ""));

                modlist.Add(new Mod("Inquisition: Daemonhunt  (Version 3.5)",
                    "Inquisition_Daemonhunt",
                    "https://www.moddb.com/mods/daemonhunters-mod",
                    "https://www.moddb.com/downloads/start/177251?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fdaemonhunters-mod%2Fdownloads",
                    ""));

                modlist.Add(new Mod("Legion of the Damned mod: In dedicato imperatum!  (Version Beta 0.666 + Patch 0.7)",
                    "LotD",
                    "https://www.moddb.com/mods/legion-of-the-damned-modification-for-soulstorm",
                    "https://www.moddb.com/downloads/start/108154?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Flegion-of-the-damned-modification-for-soulstorm%2Fdownloads",
                    "https://www.moddb.com/downloads/start/122434?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Flegion-of-the-damned-modification-for-soulstorm%2Fdownloads"));

                modlist.Add(new Mod("Men of Praetoria  (Version Beta 0.8)",
                    "Praetorian_mod",
                    "https://www.moddb.com/mods/men-of-praetoria",
                    "https://www.moddb.com/downloads/start/116144?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fmen-of-praetoria%2Fdownloads",
                    ""));

                modlist.Add(new Mod("Night Lords mod: We have come for you!  (Version 0.7.29)",
                    "Night_Lords",
                    "https://www.moddb.com/mods/night-lords-modification-for-soulstorm",
                    "https://www.moddb.com/downloads/start/140247?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fnight-lords-modification-for-soulstorm%2Fdownloads",
                    ""));

                modlist.Add(new Mod("Objective Points SS  (Version 17.4.2020)",
                    "objective_points_SS",
                    "https://www.moddb.com/mods/unification-mod-dawn-of-war-soulstorm/downloads/objective-points-ss-v1742020-for-dowdc-and-dowss",
                    "https://www.moddb.com/downloads/start/192702?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Funification-mod-dawn-of-war-soulstorm%2Fdownloads",
                    ""));

                modlist.Add(new Mod("RAGE:World Eaters Warbands  (Version 2.0 + Patch 2.15)",
                    "RAGE",
                    "https://www.moddb.com/mods/rage-world-eaters-warband",
                    "https://www.moddb.com/downloads/start/101179?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Frage-world-eaters-warband%2Fdownloads",
                    "https://www.moddb.com/downloads/start/101765?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Frage-world-eaters-warband%2Fdownloads"));

                modlist.Add(new Mod("Redux Mod  (Version Beta 0.9 + Patch 0.9.3)",
                    "Redux",
                    "https://www.moddb.com/mods/eranthis-project",
                    "https://www.moddb.com/downloads/start/190876?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Feranthis-project%2Fdownloads",
                    "https://www.moddb.com/downloads/start/195426?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Feranthis-project%2Fdownloads"));

                modlist.Add(new Mod("Renegade Guard  (Version 0.93)",
                    "Renegade_Guard",
                    "https://www.moddb.com/mods/renegade-guard",
                    "https://www.moddb.com/downloads/start/135315?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Frenegade-guard%2Fdownloads",
                    ""));

                modlist.Add(new Mod("Salamanders mod REFORGED: Unto the Anvil of War!  (Version 2.0 + Patch 2.01)",
                    "Salamanders",
                    "https://www.moddb.com/mods/salamanders-mod-for-soulstorm",
                    "https://www.moddb.com/downloads/start/184518?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fsalamanders-mod-for-soulstorm%2Fdownloads",
                    "https://www.moddb.com/downloads/start/184624?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fsalamanders-mod-for-soulstorm%2Fdownloads"));

                modlist.Add(new Mod("Space Wolves  (Version 0.77)",
                    "Space_Wolves",
                    "https://www.moddb.com/mods/space-wolves",
                    "https://www.moddb.com/downloads/start/189241?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fspace-wolves%2Fdownloads",
                    ""));

                modlist.Add(new Mod("Steel Legion - Armageddon  (Version 1.0 + SimplePatch)",
                    "Steel_Legions_SS",
                    "https://www.moddb.com/mods/steel-legion-armageddon",
                    "http://www.mediafire.com/?etfl8eu94mkmbyk",
                    "http://www.mediafire.com/file/ec7st9hj8lnry7h/Steel_Legions_SS_1.0_Simple+Patch.rar"));

                modlist.Add(new Mod("Soulstorm Bugfix Mod  (Version 1.56)",
                    "Bugfix",
                    "https://www.moddb.com/mods/soulstorm-bugfix-mod",
                    "https://www.moddb.com/downloads/start/124372?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fsoulstorm-bugfix-mod%2Fdownloads",
                    ""));

                modlist.Add(new Mod("Tartarus and Lorn V Campaigns for Soulstorm  (Version 3 + Patch 3.1)",
                    "CampaignMissions",
                    "https://www.moddb.com/mods/tartarus-and-lorn-v-campaigns-for-soulstorm",
                    "https://www.moddb.com/downloads/start/170256?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Ftartarus-and-lorn-v-campaigns-for-soulstorm%2Fdownloads",
                    "https://www.moddb.com/downloads/start/170654?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Ftartarus-and-lorn-v-campaigns-for-soulstorm%2Fdownloads"));

                modlist.Add(new Mod("The Dance Macabre - Harlequins Mod  (Version 2.4)",
                    "TheDanceMacabre",
                    "https://www.moddb.com/mods/the-dance-macabre-harlequins-mod-for-soulstorm",
                    "https://www.moddb.com/downloads/start/181447?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fthe-dance-macabre-harlequins-mod-for-soulstorm%2Fdownloads",
                    ""));

                modlist.Add(new Mod("Thirteenth Company Mod  (Version 0.22)",
                    "thirteenth_company_mod",
                    "https://www.moddb.com/mods/thirteenth-company-mod-for-dawn-of-war-soulstorm",
                    "https://www.moddb.com/downloads/start/176576?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fthirteenth-company-mod-for-dawn-of-war-soulstorm%2Fdownloads",
                    ""));

                modlist.Add(new Mod("Thousand Sons mod: Knowledge Is Power!  (Version 2.0)",
                    "Thousand_Sons",
                    "https://www.moddb.com/mods/thousand-sons-mod",
                    "https://www.moddb.com/downloads/start/116283?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fthousand-sons-mod%2Fdownloads",
                    ""));

                modlist.Add(new Mod("Titanium Wars Mod for Soulstorm  (Version 1.00.27)",
                    "TitaniumWars_SS",
                    "https://www.moddb.com/mods/titanium-wars-mod-ss",
                    "https://rutracker.org/forum/dl.php?t=4859187",
                    ""));

                modlist.Add(new Mod("Tyranid Mod  (Version Beta 0.5b2)",
                    "Tyranids",
                    "https://www.moddb.com/mods/tyranid-mod",
                    "https://www.moddb.com/downloads/start/46895?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Ftyranid-mod%2Fdownloads",
                    ""));

                modlist.Add(new Mod("Unification Mod  (Version 4.68)",
                    "Unification_New_Races",
                    "https://www.moddb.com/mods/unification-mod-dawn-of-war-soulstorm",
                    "https://www.moddb.com/downloads/start/132080?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Funification-mod-dawn-of-war-soulstorm%2Fdownloads",
                    ""));

                modlist.Add(new Mod("Ultimate Apocalypse Mod  (Version 1.88.731)",
                    "UltimateApocalypse_THB",
                    "https://www.moddb.com/mods/ultimate-apocalypse-mod",
                    "https://www.moddb.com/downloads/start/192014?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fultimate-apocalypse-mod%2Fdownloads",
                    ""));

                modlist.Add(new Mod("Vostroyan Firstborn Modification for Soulstorm  (Version 1.23)",
                    "Vostroyan_Firstborn",
                    "https://www.moddb.com/mods/vostroyan-firstborn-modification-for-soulstorm",
                    "https://www.moddb.com/downloads/start/127232?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fvostroyan-firstborn-modification-for-soulstorm%2Fdownloads",
                    ""));

                modlist.Add(new Mod("Witch Hunters: Adepta Sororitas  (Version 2.6)",
                    "Witch_Hunters",
                    "https://www.moddb.com/mods/witch-hunters-mod-for-soulstorm",
                    "https://www.moddb.com/downloads/start/177550?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fwitch-hunters-mod-for-soulstorm%2Fdownloads",
                    ""));
            }
            else if (modManager.CurrentGameEXE == ModManagerForm.GameExecutable.DARK_CRUSADE)
            {
                popularModsLabel.Text += "Dark Crusade:";

                modlist.Add(new Mod("Dawn of Steel - Steel Legion Mod  (Version 1.0)",
                    "",
                    "https://www.moddb.com/games/dawn-of-war-dark-crusade/addons/dawn-of-steel-steel-legion-mod-1-0",
                    "https://www.moddb.com/addons/start/152298",
                    ""));

                modlist.Add(new Mod("Dawn of War: Dark Crusade Bugfix Mod  (Version 30.0)",
                    "Bugfix",
                    "https://www.moddb.com/mods/dawn-of-war-dark-crusade-bugfix-mod",
                    "https://www.moddb.com/downloads/start/144937?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fdawn-of-war-dark-crusade-bugfix-mod%2Fdownloads",
                    ""));

                modlist.Add(new Mod("DoWPro  (Version 3.40 + Patch 3.41)",
                    "",
                    "https://www.moddb.com/mods/dowpro",
                    "https://www.moddb.com/downloads/start/12671?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fdowpro%2Fdownloads",
                    "https://www.moddb.com/downloads/start/12733?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fdowpro%2Fdownloads"));

                modlist.Add(new Mod("Firestorm over Kronus  (Version 0.3.6)",
                    "",
                    "http://fok.dow-mods.com/viewtopic.php?f=141&t=4234",
                    "http://www.moddb.com/downloads/start/23967",
                    ""));

                modlist.Add(new Mod("Inquisition: Daemonhunt  (Version 2.0)",
                    "",
                    "https://www.moddb.com/mods/daemonhunters-mod",
                    "http://download1347.mediafire.com/delol5z1m9dg/zc41u4pexbg5rua/inquisition_daemonhunt_mod+DC.zip",
                    ""));

                modlist.Add(new Mod("D.O.W. Kingdom  (Version 1.0.1)",
                    "",
                    "https://www.moddb.com/mods/dow-kingdom",
                    "https://www.moddb.com/downloads/start/8179?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fdow-kingdom%2Fdownloads",
                    ""));

                modlist.Add(new Mod("M42  (Version 0.91)",
                    "",
                    "https://www.moddb.com/mods/m42-dark-crusade",
                    "https://www.moddb.com/downloads/start/184047?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fm42-dark-crusade%2Fdownloads",
                    ""));

                modlist.Add(new Mod("Tabletop Round-up  (Version 1.35)",
                    "",
                    "https://www.moddb.com/mods/tabletop-round-up",
                    "https://www.moddb.com/downloads/start/9231?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Ftabletop-round-up%2Fdownloads",
                    ""));

                modlist.Add(new Mod("Titanium Wars Mod  (Version 1.00.38)",
                    "TitaniumWars",
                    "https://www.moddb.com/mods/titanium-wars-mod",
                    "https://rutracker.org/forum/dl.php?t=4859187",
                    ""));

                modlist.Add(new Mod("Tyranid Mod  (Version 0.45 + MultiplayFix)",
                    "",
                    "https://www.moddb.com/mods/tyranid-mod",
                    "https://www.moddb.com/downloads/start/11333?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Ftyranid-mod%2Fdownloads",
                    "https://www.moddb.com/downloads/start/11362?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Ftyranid-mod%2Fdownloads"));

                modlist.Add(new Mod("Updated Campaign Mod  (Version 1.0)",
                    "",
                    "https://www.moddb.com/mods/dc-updated-campaign-mod",
                    "https://www.moddb.com/downloads/start/75192?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fdc-updated-campaign-mod%2Fdownloads",
                    ""));

                modlist.Add(new Mod("Veteran Mod  (Version 1.0001 + Additional)",
                    "",
                    "https://www.moddb.com/mods/dc-veteran-mod",
                    "https://www.moddb.com/downloads/start/131671?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fdc-veteran-mod%2Fdownloads",
                    "https://www.moddb.com/downloads/start/131673?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fdc-veteran-mod%2Fdownloads"));

                modlist.Add(new Mod("Witch Hunters: Adepta Sororitas  (Version 2.00.26)",
                    "",
                    "https://www.moddb.com/mods/witch-hunters-mod-for-soulstorm",
                    "https://www.gamefront.com/files/witch-hunters-mod-beta/download",
                    ""));
            }
            else if (modManager.CurrentGameEXE == ModManagerForm.GameExecutable.WINTER_ASSAULT)
            {
                popularModsLabel.Text += "Winter Assault:";

                modlist.Add(new Mod("Dawn of Steel  (Version 1.5)",
                    "",
                    "https://www.moddb.com/mods/steel-legion-armageddon",
                    "https://www.moddb.com/downloads/start/165510?referer=https%3A%2F%2Fwww.google.com%2F",
                    ""));

                modlist.Add(new Mod("DoWPro  (Version 1.60 + Patch 1.66)",
                    "",
                    "https://www.moddb.com/mods/dowpro",
                    "https://www.moddb.com/downloads/start/6548?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fdowpro%2Fdownloads",
                    "https://www.moddb.com/downloads/start/6549?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fdowpro%2Fdownloads"));

                modlist.Add(new Mod("Necrons Mod - Mike's version  (Version 1.0)",
                    "",
                    "https://www.moddb.com/mods/necrons-mod",
                    "https://www.moddb.com/downloads/start/64724?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fnecrons-mod%2Fdownloads",
                    ""));

                modlist.Add(new Mod("Tabletop Round-up  (Version 1.2)",
                    "",
                    "https://www.moddb.com/mods/tabletop-round-up",
                    "https://www.moddb.com/downloads/start/5311?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Ftabletop-round-up%2Fdownloads",
                    ""));

                modlist.Add(new Mod("Total War: Winter Assault  (Version 3.0)",
                    "",
                    "https://www.moddb.com/mods/total-war-winter-assault",
                    "https://www.moddb.com/downloads/start/1266?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Ftotal-war-winter-assault%2Fdownloads",
                    ""));

                modlist.Add(new Mod("WXP Necron Mod  (Version 0.2)",
                    "",
                    "https://www.moddb.com/mods/wxp-necron-mod",
                    "https://www.moddb.com/downloads/start/76841?referer=https%3A%2F%2Fwww.moddb.com%2Fmods%2Fwxp-necron-mod%2Fdownloads",
                    ""));
            }
            else if (modManager.CurrentGameEXE == ModManagerForm.GameExecutable.ORIGINAL)
            {
                popularModsLabel.Text += "Original:";

                modlist.Add(new Mod("First I have to find a few mods for Original :-)",
                    "",
                    "",
                    "",
                    ""));

                openModPageButton.Enabled = false;
                downloadModButton.Enabled = false;
            }

            if (moduleFileName.Length > 0)
            {
                searchTextBox.Text = moduleFileName;

                return;
            }

            for (int i = 0; i < modlist.Count; i++)
            {
                modListBox.Items.Add(modlist[i].ModName);
            }

            findByModuleName = false;
            modListBox.Select();
        }

        private void OpenModDBButton_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.moddb.com/games/dawn-of-war/mods?sort=rating-desc");
        }

        private void OpenModPageButton_Click(object sender, EventArgs e)
        {
            if (modListBox.SelectedItem == null)
                return;

            string siteAddress = "";

            for (int i = 0; i < modlist.Count; i++)
            {
                if (modListBox.SelectedItem.ToString() == modlist[i].ModName)
                    siteAddress = modlist[i].SiteLink;
            }

            if (siteAddress.Length > 0)
                Process.Start(siteAddress);
        }

        private void DownloadModButton_Click(object sender, EventArgs e)
        {
            if (modListBox.SelectedItem == null)
                return;

            string modAddress = "";
            string patchAddress = "";

            for (int i = 0; i < modlist.Count; i++)
            {
                if (modListBox.SelectedItem.ToString() == modlist[i].ModName)
                    modAddress = modlist[i].DownloadLink;

                if (modListBox.SelectedItem.ToString() == modlist[i].ModName)
                    patchAddress = modlist[i].PatchLink;
            }

            if (modAddress.Length > 0)
                Process.Start(modAddress);

            if (patchAddress.Length > 0)
            {
                Thread.Sleep(250);                                               // Some small delay before trying to download second file
                Process.Start(modAddress);
            }
        }

        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            modListBox.Items.Clear();

            if (searchTextBox.Text.Length > 0 && searchTextBox.Text != SEARCH_TEXT)
            {
                if (findByModuleName)
                {
                    for (int i = 0; i < modlist.Count; i++)
                    {
                        if (modlist[i].ModuleFileName.Contains(searchTextBox.Text))
                        {
                            modListBox.Items.Add(modlist[i].ModName);
                        }
                    }

                    findByModuleName = false;
                }

                for (int i = 0; i < modlist.Count; i++)
                {
                    string modLowerCase = modlist[i].ModName.ToLower();
                    string searchLowerCase = searchTextBox.Text.ToLower();

                    if (modLowerCase.Contains(searchLowerCase))
                    {
                        modListBox.Items.Add(modlist[i].ModName);
                    }
                }
            }
            else
            {
                for (int i = 0; i < modlist.Count; i++)
                {
                    modListBox.Items.Add(modlist[i].ModName);
                }
            }
        }

        private void SearchTextBox_Enter(object sender, EventArgs e)
        {
            if (searchTextBox.Text == SEARCH_TEXT)
            {
                searchTextBox.Text = "";
                searchTextBox.ForeColor = Color.FromArgb(200, 200, 200);
            }
        }

        private void SearchTextBox_Leave(object sender, EventArgs e)
        {
            if (searchTextBox.Text == "")
            {
                searchTextBox.Text = SEARCH_TEXT;
                searchTextBox.ForeColor = Color.Gray;
            }
        }
    }
}