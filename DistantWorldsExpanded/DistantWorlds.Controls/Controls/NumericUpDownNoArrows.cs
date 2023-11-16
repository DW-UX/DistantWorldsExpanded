using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public partial class NumericUpDownNoArrows : NumericUpDown
    {
        public NumericUpDownNoArrows()
        {
            InitializeComponent();

            this.Controls[0].Visible = false;
        }
        protected override void OnTextBoxResize(object source, EventArgs e)
        {
            //just in case anyone uses this, if you specify no border, you will have to add 4 instead

            if (this.BorderStyle != BorderStyle.None)
            { this.Controls[1].Width = Width - 4; }
            else
            { this.Controls[1].Width = Width + 4; }
        }
    }
}
