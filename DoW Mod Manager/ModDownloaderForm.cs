using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoW_Mod_Manager
{
    public partial class ModDownloaderForm : Form
    {
        private readonly string[] modWebAdresses = new string[20];

        public ModDownloaderForm()
        {
            InitializeComponent();

            modWebAdresses[0] = "https://www.moddb.com/mods/dawn-of-warhammer-40k-firestorm-over-kronus";
            modWebAdresses[1] = "https://www.moddb.com/mods/ultimate-apocalypse-mod";
            modWebAdresses[2] = "https://www.moddb.com/mods/black-templars-kaurava-crusade";
            modWebAdresses[3] = "https://www.moddb.com/mods/dowpro";
            modWebAdresses[4] = "https://www.moddb.com/mods/corncobmans-fun-mod-for-soulstorm";
            modWebAdresses[5] = "https://www.moddb.com/mods/soulstorm-bugfix-mod";
            modWebAdresses[6] = "https://www.moddb.com/mods/titanium-wars-mod-ss";
            modWebAdresses[7] = "https://www.moddb.com/mods/dark-angels-mod";
            modWebAdresses[8] = "https://www.moddb.com/mods/eranthis-project";
            modWebAdresses[9] = "https://www.moddb.com/mods/sister-of-battle-hd-retextures-by-leonardgoog";
            modWebAdresses[10] = "https://www.moddb.com/mods/soulstorm-strongholds";
            modWebAdresses[11] = "https://www.moddb.com/mods/witch-hunters-mod-for-soulstorm"; 
            modWebAdresses[12] = "https://www.moddb.com/mods/steel-legion-armageddon"; 
            modWebAdresses[13] = "https://www.moddb.com/mods/daemonhunters-mod"; 
            modWebAdresses[14] = "https://www.moddb.com/mods/unification-mod-dawn-of-war-soulstorm";
            modWebAdresses[15] = "https://www.moddb.com/mods/thousand-sons-mod";
            modWebAdresses[16] = "https://www.moddb.com/mods/emperors-children-modfor-the-glory-of-slaanesh";
            modWebAdresses[17] = "https://www.moddb.com/mods/salamanders-mod-for-soulstorm";
            modWebAdresses[18] = "https://www.moddb.com/mods/adeptus-mechanicus";
            modWebAdresses[19] = "https://www.moddb.com/mods/tyranid-mod";

            modListBox.Select();
        }

        private void OpenModDBButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.moddb.com/games/dawn-of-war/mods?sort=rating-desc");
        }

        private void DownloadModButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not so fast cowboy! This feature a'int ready yet! :-)");
        }

        private void OpenModPageButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(modWebAdresses[modListBox.SelectedIndex]);
        }
    }
}
