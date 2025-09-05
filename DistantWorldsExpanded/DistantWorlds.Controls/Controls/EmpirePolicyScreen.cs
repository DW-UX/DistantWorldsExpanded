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
        private Font _font6;
        private BuiltObjectImageCache builtObjectImageCache_0;
        public EmpirePolicyScreen(Font font2, Font font6, BuiltObjectImageCache builtObjectImageCache_0)
        {
            InitializeComponent();
            this._font2 = font2;
            this._font6 = font6;
            this.builtObjectImageCache_0 = builtObjectImageCache_0;
        }
    }
}
