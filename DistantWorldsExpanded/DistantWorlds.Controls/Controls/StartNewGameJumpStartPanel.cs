using DistantWorlds.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DistantWorlds.Controls
{
    public partial class StartNewGameJumpStartPanel : Panel
    {
        public StartNewGameJumpStartPanel()
        {
            InitializeComponent();

            tbarJumpStartTheGalaxyStarDensity.Setup();

            string text = TextResolver.GetText("stars");

            lblJumpStartVictoryPiratePlaystyle.Text = TextResolver.GetText("Pirate Playstyle");
            tbarJumpStartTheGalaxyStarDensity.SetLabels(new string[6]
            {
            TextResolver.GetText("Dwarf") + "\n100 " + text,
            TextResolver.GetText("Tiny") + "\n250 " + text,
            TextResolver.GetText("Small") + "\n400 " + text,
            TextResolver.GetText("Standard") + "\n700 " + text,
            TextResolver.GetText("Large") + "\n1000 " + text,
            TextResolver.GetText("Huge") + "\n1400 " + text
            });
            tbarJumpStartTheGalaxyStarDensity.LabelText = TextResolver.GetText("Star\\nAmount");


            tbarJumpStartTheGalaxyDimensions.Setup();

            string text2 = TextResolver.GetText("sectors");
            tbarJumpStartTheGalaxyDimensions.SetLabels(new string[5]
            {
            TextResolver.GetText("Tiny") + "\n4x4 " + text2,
            TextResolver.GetText("Small") + "\n6x6 " + text2,
            TextResolver.GetText("Medium") + "\n8x8 " + text2,
            TextResolver.GetText("Large") + "\n10x10 " + text2,
            TextResolver.GetText("Huge") + "\n15x15 " + text2
            });
            tbarJumpStartTheGalaxyDimensions.LabelText = TextResolver.GetText("Physical\\nSize");

            tbarJumpStartTheGalaxyDifficulty.Setup();
            tbarJumpStartTheGalaxyDifficulty.SetLabels(new string[5]
            {
            TextResolver.GetText("Easy"),
            TextResolver.GetText("Normal"),
            TextResolver.GetText("Hard"),
            TextResolver.GetText("Very Hard"),
            TextResolver.GetText("Extreme")
            });

            chkJumpStartTheGalaxyDifficultyScaling.Text = TextResolver.GetText("Difficulty scales as player nears victory");
            tbarJumpStartTheGalaxyDifficulty.LabelText = TextResolver.GetText("Difficulty");

            cmbJumpStartYourEmpireGovernment.NullItemText = "(" + TextResolver.GetText("Random") + ")";
        }
    }
}
