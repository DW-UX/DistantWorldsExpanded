using DistantWorlds.Types;
using System.Drawing;

namespace DistantWorlds.Controls
{
    public partial class ThemesScreenPanel : ScreenPanel
    {
        public ThemesScreenPanel()
        {
            InitializeComponent();
            this.HeaderTitle = TextResolver.GetText("Change Theme");
            btnThemeCancel.Text = TextResolver.GetText("Cancel");
            btnThemeSwitch.Text = TextResolver.GetText("Switch Theme");


            lblThemeGalaxyMaps.MaximumSize = new Size(UiScale.S(200), UiScale.S(100));
            lblThemeGalaxyMaps.Location = new Point(UiScale.S(338), UiScale.S(255));
            picThemeImage.Size = new Size(UiScale.S(200), UiScale.S(200));
            picThemeImage.Location = new Point(UiScale.S(338), UiScale.S(45));
            lblThemeDescription.Location = new Point(UiScale.S(10), UiScale.S(45));
            lblThemeDescription.MaximumSize = new Size(UiScale.S(320), UiScale.S(480));
            lblThemeTitle.ForeColor = Color.White;
            lblThemeTitle.Location = new Point(UiScale.S(10), UiScale.S(10));
            lblCurrentTheme.Location = new Point(UiScale.S(10), UiScale.S(10));
            pnlThemeDetail.Size = new Size(UiScale.S(550), UiScale.S(530));
            pnlThemeDetail.Location = new Point(UiScale.S(250), UiScale.S(40));
            btnThemeCancel.Size = new Size(UiScale.S(200), UiScale.S(30));
            btnThemeCancel.Location = new Point(UiScale.S(250), UiScale.S(580));
            btnThemeSwitch.Size = new Size(UiScale.S(340), UiScale.S(30));
            btnThemeSwitch.Location = new Point(UiScale.S(460), UiScale.S(580));
        }
    }
}
