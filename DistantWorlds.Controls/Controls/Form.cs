using System;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;
using DistantWorlds.Types;

namespace DistantWorlds.Controls;

public partial class Form : System.Windows.Forms.Form {

  public static Main? Main
    => Application.OpenForms.OfType<Main>().FirstOrDefault();

  private static double GuiScale
    => Main?.gameOptions_0.GuiScale ?? 1.0;

}