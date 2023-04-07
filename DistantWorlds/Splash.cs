// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Splash
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using DistantWorlds.Controls;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds
{
    public class Splash : Form
    {
        private IContainer icontainer_0;
        internal LabelDropshadow lblMessage;

        public Splash() : base()
        {
            Class7.VEFSJNszvZKMZ();
            // ISSUE: explicit constructor call
            this.InitializeComponent();
            ((Control)this.lblMessage).Location = new Point(86, 228);
            ((Control)this.lblMessage).Size = new Size(360, 22);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.icontainer_0 != null)
                this.icontainer_0.Dispose();
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
