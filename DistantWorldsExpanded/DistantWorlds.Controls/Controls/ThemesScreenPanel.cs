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


            lblThemeGalaxyMaps.MaximumSize = new Size(200, 100);
            lblThemeGalaxyMaps.Location = new Point(338, 255);
            picThemeImage.Size = new Size(200, 200);
            picThemeImage.Location = new Point(338, 45);
            lblThemeDescription.Location = new Point(10, 45);
            lblThemeDescription.MaximumSize = new Size(320, 480);
            lblThemeTitle.ForeColor = Color.White;
            lblThemeTitle.Location = new Point(10, 10);
            lblCurrentTheme.Location = new Point(10, 10);
            pnlThemeDetail.Size = new Size(550, 530);
            pnlThemeDetail.Location = new Point(250, 40);
            btnThemeCancel.Size = new Size(200, 30);
            btnThemeCancel.Location = new Point(250, 580);
            btnThemeSwitch.Size = new Size(340, 30);
            btnThemeSwitch.Location = new Point(460, 580);
        }
    }
}
