using DistantWorlds.Controls;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DistantWorlds.Controls
{
    public partial class StartNewGameSelectGameTypeScreenPanel : Panel
    {
        public StartNewGameSelectGameTypeScreenPanel()
        {
            InitializeComponent();

            toolTip.SetToolTip(btnStartNewGameYourEmpireTypeNormalClassic, TextResolver.GetText("Start New Game Description - Custom Standard"));
            toolTip.SetToolTip(btnStartNewGameYourEmpireTypePirateClassic, TextResolver.GetText("Start New Game Description - Custom Pirate"));
            toolTip.SetToolTip(btnStartNewGameYourEmpireTypeTheAncientGalaxy, TextResolver.GetText("Start New Game Description - Ancient Galaxy"));
            toolTip.SetToolTip(btnStartNewGameYourEmpireTypePirateShadows, TextResolver.GetText("Start New Game Description - Shadows Pirate"));
            toolTip.SetToolTip(btnStartNewGameYourEmpireTypeNormalShadows, TextResolver.GetText("Start New Game Description - Shadows Standard"));
            toolTip.SetToolTip(btnStartNewGameYourEmpireTypeClassicEra, TextResolver.GetText("Start New Game Description - Classic Era"));
            toolTip.SetToolTip(btnStartNewGameYourEmpireTypeReturnOfTheShakturi, TextResolver.GetText("Start New Game Description - Return of the Shakturi"));
            toolTip.SetToolTip(btnStartNewGameYourEmpireTypeLegends, TextResolver.GetText("Start New Game Description - Legends"));
            toolTip.SetToolTip(btnStartNewGameIntroductory, TextResolver.GetText("Start New Game Description - Introductory Game"));
            btnStartNewGameYourEmpireTypeTheAncientGalaxy.Text = TextResolver.GetText("Start New Game - Ancient Galaxy") + ">>";
            btnStartNewGameYourEmpireTypePirateShadows.Text = TextResolver.GetText("Start New Game - Shadows Pirate") + ">>";
            btnStartNewGameYourEmpireTypeNormalShadows.Text = TextResolver.GetText("Start New Game - Shadows Standard") + ">>";
            btnStartNewGameYourEmpireTypeClassicEra.Text = TextResolver.GetText("Start New Game - Classic Era") + ">>";
            btnStartNewGameYourEmpireTypeReturnOfTheShakturi.Text = TextResolver.GetText("Start New Game - Return of the Shakturi") + ">>";
            btnStartNewGameYourEmpireTypeLegends.Text = TextResolver.GetText("Start New Game - Legends") + ">>";
            btnStartNewGameYourEmpireTypeNormalClassic.Text = TextResolver.GetText("Start New Game - Custom Standard") + ">>";
            btnStartNewGameYourEmpireTypePirateClassic.Text = TextResolver.GetText("Start New Game - Custom Pirate") + ">>";

            lblHelpTitle.Text = TextResolver.GetText("Start New Game New Player Explanation Title UNIVERSE") + ":";
            lblHelpDescription.Text = TextResolver.GetText("Start New Game New Player Explanation Text UNIVERSE");
        }
    }
}
