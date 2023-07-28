// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Splash
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using DistantWorlds.Controls;
using DistantWorlds.Types;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds
{
    public class Splash : Form
    {
        //private IContainer icontainer_0;
        internal LabelDropshadow lblMessage;
        public bool SplashClosing = false;
        public Splash() : base()
        {

            // ISSUE: explicit constructor call
            this.InitializeComponent();
            ((Control)this.lblMessage).Location = new Point(86, 228);
            ((Control)this.lblMessage).Size = new Size(360, 22);
            this.Visible = false;
            this.FormClosing += Splash_FormClosing;
        }

        private void Splash_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!this.SplashClosing) { e.Cancel= true; return; }
        }

        //public void SetInitialText()
        //{
        //    string text = "Initializing";
        //    this.SuspendLayout();
        //    this.lblMessage.MaximumSize = new Size(450, 30);
        //    this.lblMessage.Text = text;
        //    this.lblMessage.ForeColor = Color.FromArgb(255, 192, 0);
        //    SizeF sizeF = this.lblMessage.Size;
        //    Point point = new Point((this.Width - (int)sizeF.Width) / 2, 195);
        //    using (Graphics graphics = CreateGraphics())
        //    {
        //        sizeF = graphics.MeasureString(text, this.lblMessage.Font, 450);
        //        sizeF = new SizeF(sizeF.Width + 10f, sizeF.Height + 5f);
        //        this.lblMessage.Size = new Size((int)sizeF.Width + 1, (int)sizeF.Height + 1);
        //        this.lblMessage.MaximumSize = new Size((int)sizeF.Width + 1, (int)sizeF.Height + 1);
        //        this.lblMessage.MinimumSize = new Size((int)sizeF.Width + 1, (int)sizeF.Height + 1);
        //        point = new Point((this.Width - (int)sizeF.Width) / 2, 195);
        //        this.lblMessage.Location = point;
        //        this.lblMessage.Text = text;
        //    }
        //    this.lblMessage.Update();
        //    this.ResumeLayout();
        //}
        public void UpdateState(int idx)
        {
            if (!this.InvokeRequired)
            {
                string[] array = new string[14]
                {
                    TextResolver.GetText("Forming nebulae clouds..."),
                    TextResolver.GetText("Igniting stellar cores..."),
                    TextResolver.GetText("Emptying Black Holes..."),
                    TextResolver.GetText("Tidying up asteroid fields..."),
                    TextResolver.GetText("Initiating orbital motion..."),
                    TextResolver.GetText("Building mining stations..."),
                    TextResolver.GetText("Fueling pirate ships..."),
                    TextResolver.GetText("Repairing battle damage..."),
                    TextResolver.GetText("Recharging reactors..."),
                    TextResolver.GetText("Recalibrating hyperdrives..."),
                    TextResolver.GetText("Feeding the Giant Kaltors..."),
                    TextResolver.GetText("Starting the Game"),
                    "",
                    ""
                };
                if (idx < array.Length && this.Visible)
                {
                    string text = array[idx];
                    idx++;
                    idx = Math.Min(idx, array.Length - 1);
                    this.SuspendLayout();
                    this.lblMessage.MaximumSize = new Size(450, 30);
                    this.lblMessage.Text = text;
                    this.lblMessage.ForeColor = Color.FromArgb(255, 192, 0);
                    SizeF sizeF = this.lblMessage.Size;
                    Point point = new Point((this.Width - (int)sizeF.Width) / 2, 195);
                    using (Graphics graphics = CreateGraphics())
                    {
                        sizeF = graphics.MeasureString(text, this.lblMessage.Font, 450);
                        sizeF = new SizeF(sizeF.Width + 10f, sizeF.Height + 5f);
                        this.lblMessage.Size = new Size((int)sizeF.Width + 1, (int)sizeF.Height + 1);
                        this.lblMessage.MaximumSize = new Size((int)sizeF.Width + 1, (int)sizeF.Height + 1);
                        this.lblMessage.MinimumSize = new Size((int)sizeF.Width + 1, (int)sizeF.Height + 1);
                        point = new Point((this.Width - (int)sizeF.Width) / 2, 195);
                        this.lblMessage.Location = point;
                        this.lblMessage.Text = text;
                    }
                    this.lblMessage.Update();
                    this.ResumeLayout();
                    //Application.DoEvents();
                }
            }
            else
            {
                this.Invoke(UpdateState, idx);
            }
        }
        public void SetFont(Font font)
        {
            if (!this.InvokeRequired)
            {

                this.Font = font;

                if (this.Controls == null)
                {
                    return;
                }
                foreach (Control control in this.Controls)
                {
                    SetSubControlFonts(font, control);
                }
            }
            else
            {
                this.Invoke(SetFont, font);
            }
        }
        public void Start()
        {
            if (!this.InvokeRequired)
            {
                this.Visible = true;
                this.Show();
                this.lblMessage.Update();
            }
            else
            {
                this.Invoke(() => this.Show());
            }
        }
        public void Stop()
        {
            SplashClosing = true;
            if (!this.InvokeRequired)
            {
                this.Close();
            }
            else
            {
                this.Invoke(this.Close);
            }
        }
        public void ShowMessageBox(string message, string caption, MessageBoxButtons btn, MessageBoxIcon icon)
        {
            if (!this.InvokeRequired)
            {
                MessageBox.Show(this, message, caption, btn, icon);
            }
            else
            {
                this.Invoke(() => MessageBox.Show(this, message, caption, btn, icon));
            }
        }
        private void SetSubControlFonts(Font font, Control control)
        {
            control.Font = font;
            foreach (Control subCcontrol in control.Controls)
            {
                SetSubControlFonts(font, subCcontrol);
            }
        }
        protected override void Dispose(bool disposing)
        {
            //if (disposing && this.icontainer_0 != null)
            //    this.icontainer_0.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            var resources = new ComponentResourceManager(typeof(Splash));
            this.lblMessage = new LabelDropshadow();
            this.SuspendLayout();
            ((Control)this.lblMessage).AutoSize = true;
            ((Control)this.lblMessage).BackColor = Color.Transparent;
            this.lblMessage.DropshadowColor = Color.Black;
            this.lblMessage.DropshadowOffset = 1;
            ((Control)this.lblMessage).Font = new Font("Tahoma", 9.75f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            ((Control)this.lblMessage).ForeColor = Color.FromArgb(144, 144, (int)byte.MaxValue);
            ((Control)this.lblMessage).Location = new Point(131, 195);
            ((Control)this.lblMessage).MaximumSize = new Size(200, 35);
            ((Control)this.lblMessage).Name = "lblMessage";
            ((Control)this.lblMessage).Size = new Size(0, 16);
            ((Control)this.lblMessage).TabIndex = 0;
            this.AutoScaleMode = AutoScaleMode.None;
            this.BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            this.BackgroundImageLayout = ImageLayout.None;
            this.ClientSize = new Size(471, 290);
            this.Controls.Add((Control)this.lblMessage);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Icon = (Icon)resources.GetObject("$this.Icon");
            this.Name = "Splash";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Distant Worlds";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
