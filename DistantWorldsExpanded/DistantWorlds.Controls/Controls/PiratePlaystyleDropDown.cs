// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.PiratePlaystyleDropDown
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class PiratePlaystyleDropDown : ComboBox
    {
        private List<PiratePlayStyle> _Playstyles;

        public PiratePlaystyleDropDown()
        {
            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.DropDownStyle = ComboBoxStyle.DropDownList;
            this.FlatStyle = FlatStyle.Popup;
            this.BackColor = Color.FromArgb(48, 48, 64);
            this.ForeColor = Color.FromArgb(170, 170, 170);
            this.Font = new Font("Verdana", 8.25f);
        }

        public void ClearData() => this._Playstyles = (List<PiratePlayStyle>)null;

        private List<PiratePlayStyle> ResolvePiratePlaystyles() => new List<PiratePlayStyle>()
        {
          PiratePlayStyle.Balanced,
          PiratePlayStyle.Smuggler,
          PiratePlayStyle.Pirate,
          PiratePlayStyle.Mercenary,
          PiratePlayStyle.Legendary
        };

        public void BindData()
        {
            this._Playstyles = this.ResolvePiratePlaystyles();
            this.ItemHeight = 22;
            this.Items.Clear();
            if (this._Playstyles == null)
                return;
            for (int index = 0; index < this._Playstyles.Count; ++index)
                this.Items.Add((object)this._Playstyles[index]);
        }

        public void SetSelectedPlaystyle(PiratePlayStyle playStyle)
        {
            if (this._Playstyles == null)
                this.SelectedIndex = -1;
            else if (playStyle != PiratePlayStyle.Undefined)
                this.SelectedIndex = this._Playstyles.IndexOf(playStyle);
            else
                this.SelectedIndex = -1;
        }

        public PiratePlayStyle SelectedPlaystyle
        {
            get
            {
                int selectedIndex = this.SelectedIndex;
                return selectedIndex < 0 || this._Playstyles == null || this._Playstyles.Count <= selectedIndex ? PiratePlayStyle.Undefined : this._Playstyles[selectedIndex];
            }
        }

        protected override void OnMeasureItem(MeasureItemEventArgs e) => e.ItemHeight = 22;

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);
            e.DrawBackground();
            PointF point = new PointF(4f, (float)(e.Bounds.Y + 3));
            Font font = this.Font;
            SolidBrush solidBrush1 = new SolidBrush(this.ForeColor);
            SolidBrush solidBrush2 = new SolidBrush(Color.FromArgb(128, (int)byte.MaxValue, 0, 128));
            if (this._Playstyles != null && this._Playstyles.Count > 0 && e.Index >= 0)
            {
                PiratePlayStyle playstyle = this._Playstyles[e.Index];
                if (playstyle != PiratePlayStyle.Undefined)
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    string s = Galaxy.ResolveDescription(playstyle);
                    e.Graphics.DrawString(s, font, (Brush)solidBrush1, point);
                }
            }
            e.DrawFocusRectangle();
        }
    }
}
