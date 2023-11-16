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
    public partial class BorderPanelUserControl : UserControl
    {
        private int _BorderSize = 3;
        private Color _BorderColor1 = Color.FromArgb(96, 200, 200, 200);
        private Color _BorderColor2 = Color.FromArgb(96, 140, 140, 140);
        private Color _BorderColor3 = Color.FromArgb(96, 20, 20, 20);
        private Color _BorderColor4 = Color.FromArgb(96, 80, 80, 80);
        private List<Control> _HighlightControls = new List<Control>();
        private Color _HighlightColor = Color.FromArgb((int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue, 0);
        private Color _HighlightColor1 = Color.FromArgb((int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue, 0);
        private Color _HighlightColor2 = Color.FromArgb(128, (int)byte.MaxValue, 48, 96);
        protected IFontCache _FontCache;
        private float _FontSize = 15.33f;
        private bool _FontIsBold;

        public BorderPanelUserControl()
        {
            InitializeComponent();
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            this.BodyPanel.Controls.Add(e.Control);
        }

        private void BodyPanel_Paint(object sender, PaintEventArgs e)
        {
            //if (this._HighlightControls != null && this._HighlightControls.Count > 0)
            //{
            //    this.UpdateHighlightColor();
            //    using (Pen pen = new Pen(this._HighlightColor, 4f))
            //    {
            //        foreach (Control highlightControl in this._HighlightControls)
            //        {
            //            if (highlightControl.Parent == this || this is ScreenPanel && ((ScreenPanel)this).pnlBody == highlightControl.Parent)
            //            {
            //                Rectangle rect = new Rectangle(highlightControl.Location.X - 3, highlightControl.Location.Y - 3, highlightControl.Size.Width + 6, highlightControl.Size.Height + 6);
            //                if (this is ScreenPanel && ((ScreenPanel)this).pnlBody == highlightControl.Parent)
            //                {
            //                    using (Graphics graphics = ((ScreenPanel)this).pnlBody.CreateGraphics())
            //                        graphics.DrawRectangle(pen, rect);
            //                }
            //                else
            //                    e.Graphics.DrawRectangle(pen, rect);
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    this.BorderStyle = BorderStyle.None;
            //    this.BackColor = Color.FromArgb(48, 48, 64);
            //    base.OnPaint(e);
            //}
            //int num1 = 0;
            //int num2 = this.ClientRectangle.Width - 1;
            //int num3 = 0;
            //int num4 = this.ClientRectangle.Height - 1;
            //Pen pen1 = new Pen(this._BorderColor1, 1f);
            //Pen pen2 = new Pen(this._BorderColor2, 1f);
            //Pen pen3 = new Pen(this._BorderColor3, 1f);
            //Pen pen4 = new Pen(this._BorderColor4, 1f);
            //for (int index = 0; index < this._BorderSize; ++index)
            //{
            //    e.Graphics.DrawLine(pen1, num1 + index, num3 + index, num2 - index, num3 + index);
            //    e.Graphics.DrawLine(pen2, num1 + index, num3 + index, num1 + index, num4 - index);
            //    e.Graphics.DrawLine(pen3, num1 + index, num4 - index, num2 - index, num4 - index);
            //    e.Graphics.DrawLine(pen4, num2 - index, num3 + index, num2 - index, num4 - index);
            //}
            //pen1.Dispose();
            //pen2.Dispose();
            //pen3.Dispose();
            //pen4.Dispose();
        }
    }
}
