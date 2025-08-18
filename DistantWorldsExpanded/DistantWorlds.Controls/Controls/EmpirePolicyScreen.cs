using DistantWorlds.Controls;
using DistantWorlds.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistantWorlds.Controls
{
    public partial class EmpirePolicyScreen : ScreenPanel
    {
        private Font _font2;
        public EmpirePolicyScreen(Font font2)
        {
            InitializeComponent();
            _font2 = font2;

            lblDiplomacyTreaties.Text = TextResolver.GetText("Diplomacy - Treaties");
            lblDiplomacyTreaties.Font = _font2;
            cmbDiplomacyAuto.Items.AddRange(TextResolver.GetText("Control manually"), TextResolver.GetText("Suggest new treaties"), TextResolver.GetText("Fully automate"));

            lblFreeTradeAgrPriority.Text = TextResolver.GetText("Free Trade Agreement Priority");
            cmbFreeTradeAgrPriority.Items.AddRange(TextResolver.GetText("Low"), TextResolver.GetText("Normal"), TextResolver.GetText("High"), TextResolver.GetText("Very High"));

            lblDefencePact.Text = TextResolver.GetText("Mutual Defense Pact Priority");
            cmbDefencePact.Items.AddRange(TextResolver.GetText("Low"), TextResolver.GetText("Normal"), TextResolver.GetText("High"), TextResolver.GetText("Very High"));

            lblBreakTreatie.Text = TextResolver.GetText("Willingness to Break Treaties");
            cmbBreakTreatie.Items.AddRange(TextResolver.GetText("Low"), TextResolver.GetText("Normal"), TextResolver.GetText("High"), TextResolver.GetText("Very High"));

            lblDiploWarTrade.Text = TextResolver.GetText("Diplomacy - War and Trade Sanctions");
            lblDiploWarTrade.Font = _font2;
            cmbDiploWarTrade.Items.AddRange(TextResolver.GetText("Control manually"), TextResolver.GetText("Suggest war and trade sanctions"), TextResolver.GetText("Fully automate"));

            lblUseBlockades.Text = TextResolver.GetText("Use Blockades when have Trade Sanctions against an empire");
            chkUseBlockades.Text = TextResolver.GetText("DiplomacyTradeSanctionsUseBlockades");

            lblSubjugation.Text = TextResolver.GetText("Subjugation Priority");
            cmbSubjugation.Items.AddRange(TextResolver.GetText("Low"), TextResolver.GetText("Normal"), TextResolver.GetText("High"), TextResolver.GetText("Very High"));

            lblWarWilingnes.Text = TextResolver.GetText("Willingness to Go To War");
            cmbWarWilingnes.Items.AddRange(TextResolver.GetText("Low"), TextResolver.GetText("Normal"), TextResolver.GetText("High"), TextResolver.GetText("Very High"));
        }
    }
}
