using DistantWorlds.Types;
using SharpDX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public partial class StartNewGameYourRacePanel : Panel
    {
        public StartNewGameYourRacePanel()
        {
            InitializeComponent();

            btnStartNewGameYourRaceNext.Text = TextResolver.GetText("Next: Your Empire") + " >>";
            btnStartNewGameYourRacePrevious.Text = "<< " + TextResolver.GetText("Previous: Colonization and Territory");
            lnkStartNewGameYourEmpireRace.Text = TextResolver.GetText("Read more about this race") + "...";
        }

        private void pnlStartNewGameYourEmpireRaceAttributes_Enter(object sender, EventArgs e)
        {
            pnlStartNewGameYourEmpireRaceAttributesContainer.Focus();
        }

        private void pnlStartNewGameYourEmpireRaceAttributes_MouseEnter(object sender, EventArgs e)
        {
            pnlStartNewGameYourEmpireRaceAttributesContainer.Focus();
        }
    }
}
