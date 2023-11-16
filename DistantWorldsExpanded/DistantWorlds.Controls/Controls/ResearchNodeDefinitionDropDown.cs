// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.ResearchNodeDefinitionDropDown
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class ResearchNodeDefinitionDropDown : ComboBox
  {
    private ResearchNodeDefinition[] _ResearchNodes;
    private bool _AllowNullNode;

    public ResearchNodeDefinitionDropDown()
    {
      this.DrawMode = DrawMode.OwnerDrawFixed;
      this.DropDownStyle = ComboBoxStyle.DropDownList;
      this.FlatStyle = FlatStyle.Popup;
      this.BackColor = Color.FromArgb(48, 48, 64);
      this.ForeColor = Color.FromArgb(170, 170, 170);
      this.Font = new Font("Verdana", 8.25f);
    }

    public void ClearData() => this._ResearchNodes = (ResearchNodeDefinition[]) null;

    public void BindData(ResearchNodeDefinitionList researchNodes) => this.BindData(researchNodes, false);

    public void BindData(ResearchNodeDefinitionList researchNodes, bool allowNullNode)
    {
      this._ResearchNodes = researchNodes.SortByName();
      this._AllowNullNode = allowNullNode;
      this.Items.Clear();
      if (this._AllowNullNode)
        this.Items.Add(new object());
      if (this._ResearchNodes == null)
        return;
      this.Items.AddRange((object[]) this._ResearchNodes);
    }

    public ResearchNodeDefinition SelectedResearchNode
    {
      get
      {
        int selectedIndex = this.SelectedIndex;
        if (selectedIndex < 0)
          return (ResearchNodeDefinition) null;
        if (this._AllowNullNode)
        {
          if (selectedIndex == 0)
            return (ResearchNodeDefinition) null;
          --selectedIndex;
        }
        return this._ResearchNodes != null && selectedIndex >= 0 && selectedIndex < this._ResearchNodes.Length ? this._ResearchNodes[selectedIndex] : (ResearchNodeDefinition) null;
      }
    }

    public void SetSelectedResearchNode(ResearchNodeDefinition researchNode)
    {
      if (researchNode != null)
        this.SetSelectedResearchNode(researchNode.ResearchNodeId);
      else
        this.SetSelectedResearchNode(-1);
    }

    public void SetSelectedResearchNode(int researchNodeId)
    {
      int num = -1;
      if (this._ResearchNodes != null && researchNodeId >= 0)
        num = ResearchNodeDefinitionList.IndexNodeById(this._ResearchNodes, researchNodeId);
      if (this._AllowNullNode)
      {
        ++num;
        if (researchNodeId < 0)
          num = 0;
      }
      this.SelectedIndex = num;
    }

    protected override void OnMeasureItem(MeasureItemEventArgs e)
    {
      base.OnMeasureItem(e);
      e.ItemHeight = 21;
    }

    protected override void OnDrawItem(DrawItemEventArgs e)
    {
      base.OnDrawItem(e);
      e.DrawBackground();
      PointF point = new PointF(0.0f, (float) (e.Bounds.Y + 1));
      Font font = this.Font;
      SolidBrush solidBrush = new SolidBrush(this.ForeColor);
      string empty = string.Empty;
      if (this._AllowNullNode && e.Index == 0)
        e.Graphics.DrawString("(" + TextResolver.GetText("None") + ")", font, (Brush) solidBrush, point);
      else if (this._ResearchNodes != null && this._ResearchNodes.Length > 0 && e.Index >= 0)
      {
        int index = e.Index;
        if (this._AllowNullNode)
          --index;
        string s = this._ResearchNodes[index].Name + " (" + Galaxy.ResolveDescription(this._ResearchNodes[index].Category) + ")";
        e.Graphics.DrawString(s, font, (Brush) solidBrush, point);
      }
      e.DrawFocusRectangle();
    }
  }
}
