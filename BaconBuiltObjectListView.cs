// Decompiled with JetBrains decompiler
// Type: BaconDistantWorlds.BaconBuiltObjectListView
// Assembly: BaconDistantWorlds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BEEE04E-F6B8-4927-96B6-91C74B502CC0
// Assembly location: H:\7\BaconDistantWorlds.dll

using DistantWorlds.Controls;
using DistantWorlds.Types;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BaconDistantWorlds
{
  public static class BaconBuiltObjectListView
  {
    public static void BindDataGeneric(
      BuiltObjectListView boListView,
      StellarObjectList stellarObjects,
      Galaxy galaxy,
      bool showDetails)
    {
      boListView._StellarObjects = stellarObjects;
      boListView._ShowDetails = showDetails;
      Empire defaultEmpire = (Empire) null;
      boListView.Visible = false;
      boListView.SuspendLayout();
      boListView._Grid.SuspendLayout();
      boListView._Grid.Rows.Clear();
      if (boListView._ShowBuiltObjectDetail)
      {
        boListView._Grid.Columns[6].Visible = true;
        boListView._Grid.Columns[7].Visible = true;
        boListView._Grid.Columns[8].Visible = true;
      }
      else
      {
        boListView._Grid.Columns[6].Visible = false;
        boListView._Grid.Columns[7].Visible = false;
        boListView._Grid.Columns[8].Visible = false;
      }
      if (stellarObjects != null && stellarObjects.Count > 0)
      {
        boListView._Grid.Rows.Add();
        if (stellarObjects.Count > 1)
          boListView._Grid.Rows.AddCopies(0, stellarObjects.Count - 1);
        for (int index = 0; index < stellarObjects.Count; ++index)
        {
          if (index == 0)
            defaultEmpire = stellarObjects[index].Empire;
          DataGridViewRow row = boListView._Grid.Rows[index];
          if (stellarObjects[index] is BuiltObject)
          {
            BuiltObject stellarObject = (BuiltObject) stellarObjects[index];
            boListView.BindSingleBuiltObject(stellarObject, row, defaultEmpire, galaxy);
            if (!stellarObject.Components.Any<BuiltObjectComponent>((Func<BuiltObjectComponent, bool>) (x => x.Status == ComponentStatus.Unbuilt)))
            {
              if (stellarObject.Components.Count<BuiltObjectComponent>((Func<BuiltObjectComponent, bool>) (x => x.Category == ComponentCategoryType.HyperDrive)) > 0 && stellarObject.WarpSpeed == 0)
              {
                row.Cells[2].Style.ForeColor = Color.Aqua;
                row.Cells[2].Style.SelectionForeColor = Color.Aqua;
              }
              else if (stellarObject.Components.Any<BuiltObjectComponent>((Func<BuiltObjectComponent, bool>) (x => x.Status == ComponentStatus.Damaged)))
              {
                bool flag = true;
                foreach (Component component in stellarObject.Components.Where<BuiltObjectComponent>((Func<BuiltObjectComponent, bool>) (x => x.Status == ComponentStatus.Damaged)))
                {
                  if (component.Category != ComponentCategoryType.Armor)
                  {
                    flag = false;
                    break;
                  }
                }
                if (flag)
                {
                  row.Cells[2].Style.ForeColor = Color.Yellow;
                  row.Cells[2].Style.SelectionForeColor = Color.Yellow;
                }
                else
                {
                  row.Cells[2].Style.ForeColor = Color.Red;
                  row.Cells[2].Style.SelectionForeColor = Color.Red;
                }
              }
            }
          }
          else if (stellarObjects[index] is Habitat)
            boListView.BindSingleHabitat((Habitat) stellarObjects[index], row, defaultEmpire);
        }
      }
      boListView.RememberSorting();
      boListView._Grid.ResumeLayout();
      boListView.ResumeLayout();
      boListView.Visible = true;
    }
  }
}
