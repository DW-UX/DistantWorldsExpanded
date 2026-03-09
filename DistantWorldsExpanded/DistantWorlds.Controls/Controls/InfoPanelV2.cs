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
    public partial class InfoPanelV2 : Panel
    {
        public InfoPanelV2()
        {
            InitializeComponent();

            string text1 = "(" + TextResolver.GetText("Unknown mission") + ")";
            //добавить рисунки персов в панель под таблицой

            //добавить отображение флота
        }
    }
}
