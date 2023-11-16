// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.EmpireDropDown
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class EmpireDropDown : ComboBox
  {
    private string _NoEmpire = "(" + TextResolver.GetText("No Empire") + ")";
    private Empire _PlayerEmpire;
    private DistantWorlds.Types.EmpireList _Empires;
    private DistantWorlds.Types.EmpireList _PirateFactions;
    private Empire _IndependentEmpire;
    private bool _IncludeNoEmpire;

    public EmpireDropDown()
    {
      this.DrawMode = DrawMode.OwnerDrawFixed;
      this.DropDownStyle = ComboBoxStyle.DropDownList;
      this.FlatStyle = FlatStyle.Popup;
      this.BackColor = Color.FromArgb(48, 48, 64);
      this.ForeColor = Color.FromArgb(170, 170, 170);
      this.Font = new Font("Verdana", 8.25f);
    }

    public void ClearData()
    {
      this._PlayerEmpire = (Empire) null;
      this._Empires = (DistantWorlds.Types.EmpireList) null;
      this._PirateFactions = (DistantWorlds.Types.EmpireList) null;
      this._IndependentEmpire = (Empire) null;
    }

    public void BindData(
      Empire playerEmpire,
      DistantWorlds.Types.EmpireList empires,
      DistantWorlds.Types.EmpireList pirateFactions,
      Empire independentEmpire,
      bool includeNoEmpire)
    {
      this._PlayerEmpire = playerEmpire;
      this._Empires = empires;
      this._PirateFactions = pirateFactions;
      this._IndependentEmpire = independentEmpire;
      this._IncludeNoEmpire = includeNoEmpire;
      this.Items.Clear();
      if (this._IncludeNoEmpire)
        this.Items.Add((object) this._NoEmpire);
      if (this._IndependentEmpire != null)
        this.Items.Add((object) this._IndependentEmpire);
      if (this._Empires != null)
      {
        this._Empires.Sort();
        this.Items.AddRange((object[]) this._Empires.ToArray());
      }
      if (this._PirateFactions == null)
        return;
      this._PirateFactions.Sort();
      this.Items.AddRange((object[]) this._PirateFactions.ToArray());
    }

    public void SetSelectedEmpire(Empire empire)
    {
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      int num4 = 0;
      if (this._IncludeNoEmpire)
      {
        ++num2;
        ++num3;
        ++num4;
      }
      if (this._IndependentEmpire != null)
      {
        ++num3;
        ++num4;
      }
      if (this._Empires != null)
        num4 += this._Empires.Count;
      if (empire == null)
      {
        if (this._IncludeNoEmpire)
          this.SelectedIndex = num1;
        else
          this.SelectedIndex = -1;
      }
      else if (empire == this._IndependentEmpire)
      {
        if (this._IndependentEmpire != null)
          this.SelectedIndex = num2;
        else
          this.SelectedIndex = -1;
      }
      else if (this._Empires != null && this._Empires.Contains(empire))
      {
        int num5 = this._Empires.IndexOf(empire);
        this.SelectedIndex = num3 + num5;
      }
      else if (this._PirateFactions != null && this._PirateFactions.Contains(empire))
      {
        int num6 = this._PirateFactions.IndexOf(empire);
        this.SelectedIndex = num4 + num6;
      }
      else
        this.SelectedIndex = -1;
    }

    public Empire SelectedEmpire
    {
      get
      {
        int selectedIndex = this.SelectedIndex;
        if (selectedIndex < 0)
          return (Empire) null;
        int num1 = 0;
        int num2 = 0;
        if (this._IncludeNoEmpire)
        {
          num1 = 1;
          num2 = 1;
          if (selectedIndex == 0)
            return (Empire) null;
        }
        if (this._IndependentEmpire != null)
        {
          ++num1;
          ++num2;
          if (this._IncludeNoEmpire)
          {
            if (selectedIndex == 1)
              return this._IndependentEmpire;
          }
          else if (selectedIndex == 0)
            return this._IndependentEmpire;
        }
        if (this._Empires != null)
        {
          num2 += this._Empires.Count;
          if (selectedIndex >= num1 && selectedIndex < num1 + this._Empires.Count)
            return this._Empires[selectedIndex - num1];
        }
        if (this._PirateFactions == null || selectedIndex < num2 || selectedIndex >= num2 + this._PirateFactions.Count)
          return (Empire) null;
        int num3 = num1;
        if (this._Empires != null)
          num3 += this._Empires.Count;
        return this._PirateFactions[selectedIndex - num3];
      }
    }

    protected override void OnMeasureItem(MeasureItemEventArgs e) => e.ItemHeight = 22;

    protected override void OnDrawItem(DrawItemEventArgs e)
    {
      base.OnDrawItem(e);
      e.DrawBackground();
      int num1 = 0;
      PointF point = new PointF(30f, (float) (e.Bounds.Y + 1));
      Font font = this.Font;
      SolidBrush solidBrush1 = new SolidBrush(this.ForeColor);
      SolidBrush solidBrush2 = new SolidBrush(Color.FromArgb(128, (int) byte.MaxValue, 0, 128));
      if (this._IncludeNoEmpire)
      {
        if (e.Index == num1)
        {
          e.Graphics.DrawString(this._NoEmpire, font, (Brush) solidBrush1, point);
          return;
        }
        ++num1;
      }
      if (this._IndependentEmpire != null)
      {
        if (e.Index == num1)
        {
          e.Graphics.DrawString(this._IndependentEmpire.Name, font, (Brush) solidBrush1, point);
          return;
        }
        ++num1;
      }
      if (this._Empires != null)
      {
        int num2 = num1;
        num1 += this._Empires.Count;
        if (e.Index >= num2 && e.Index < num1)
        {
          int index = e.Index - num2;
          if (this._Empires[index] == this._PlayerEmpire)
            e.Graphics.FillRectangle((Brush) solidBrush2, e.Bounds);
          e.Graphics.DrawString(this._Empires[index].Name, font, (Brush) solidBrush1, point);
          Bitmap largeFlagPicture = this._Empires[index].LargeFlagPicture;
          e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
          e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
          int height = e.Bounds.Height - 2;
          double num3 = (double) height / (double) largeFlagPicture.Height;
          int width = (int) ((double) largeFlagPicture.Width * num3);
          Rectangle rect = new Rectangle(3, e.Bounds.Y + 1, width, height);
          e.Graphics.DrawImage((Image) largeFlagPicture, rect);
          e.DrawFocusRectangle();
          return;
        }
      }
      if (this._PirateFactions == null)
        return;
      int num4 = num1;
      int num5 = num1 + this._PirateFactions.Count;
      if (e.Index < num4 || e.Index >= num5)
        return;
      int index1 = e.Index - num4;
      e.Graphics.DrawString(this._PirateFactions[index1].Name, font, (Brush) solidBrush1, point);
      Bitmap largeFlagPicture1 = this._PirateFactions[index1].LargeFlagPicture;
      e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
      e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      int height1 = e.Bounds.Height - 2;
      double num6 = (double) height1 / (double) largeFlagPicture1.Height;
      int width1 = (int) ((double) largeFlagPicture1.Width * num6);
      Rectangle rect1 = new Rectangle(3, e.Bounds.Y + 1, width1, height1);
      e.Graphics.DrawImage((Image) largeFlagPicture1, rect1);
      e.DrawFocusRectangle();
    }
  }
}
