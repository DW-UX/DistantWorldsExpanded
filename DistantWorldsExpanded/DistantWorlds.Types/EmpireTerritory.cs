// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.EmpireTerritory
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace DistantWorlds.Types
{
  [Serializable]
  public class EmpireTerritory
  {
    public object _LockObject = new object();
    public static readonly int TerritoryIndexSize = 2000;
    [NonSerialized]
    private byte[][] _Territory;

    public void ReviewEmpireTerritory(Galaxy galaxy)
    {
      lock (this._LockObject)
      {
        Rectangle galaxySection = new Rectangle(0, 0, Galaxy.SizeX, Galaxy.SizeY);
        this._Territory = EmpireTerritory.CalculateEmpireTerritoryGridIndex(galaxy, galaxySection, EmpireTerritory.TerritoryIndexSize, EmpireTerritory.TerritoryIndexSize, (byte[][]) null, false);
      }
    }

    public void ReviewEmpireTerritoryUpdate(Galaxy galaxy, Rectangle galaxySection)
    {
      lock (this._LockObject)
        this._Territory = EmpireTerritory.CalculateEmpireTerritoryGridIndex(galaxy, galaxySection, EmpireTerritory.TerritoryIndexSize, EmpireTerritory.TerritoryIndexSize, this._Territory, false);
    }

    public void ReviewEmpireTerritory(Galaxy galaxy, bool onlySystems)
    {
      lock (this._LockObject)
      {
        Rectangle galaxySection = new Rectangle(0, 0, Galaxy.SizeX, Galaxy.SizeY);
        this._Territory = EmpireTerritory.CalculateEmpireTerritoryGridIndex(galaxy, galaxySection, EmpireTerritory.TerritoryIndexSize, EmpireTerritory.TerritoryIndexSize, (byte[][]) null, onlySystems);
      }
    }

    public int CheckSystemOwnership(Galaxy galaxy, Habitat systemStar, out bool disputed)
    {
      disputed = false;
      SystemInfo bySystemIndex = galaxy.Systems.GetBySystemIndex(systemStar.SystemIndex);
      if (bySystemIndex == null)
        return this.CheckLocationOwnership(systemStar.Xpos, systemStar.Ypos);
      if (bySystemIndex.DominantEmpire == null || bySystemIndex.DominantEmpire.Empire == null)
        return this.CheckLocationOwnership(systemStar.Xpos, systemStar.Ypos);
      if (bySystemIndex.OtherEmpires != null && bySystemIndex.OtherEmpires.Count > 0)
        disputed = true;
      return bySystemIndex.DominantEmpire.Empire.EmpireId;
    }

    public int CheckSystemOwnershipWithOthers(
      Galaxy galaxy,
      Habitat systemStar,
      out bool disputed,
      out List<int> otherEmpireIds)
    {
      disputed = false;
      otherEmpireIds = new List<int>();
      SystemInfo bySystemIndex = galaxy.Systems.GetBySystemIndex(systemStar.SystemIndex);
      if (bySystemIndex == null)
        return this.CheckLocationOwnership(systemStar.Xpos, systemStar.Ypos);
      if (bySystemIndex.DominantEmpire == null || bySystemIndex.DominantEmpire.Empire == null)
        return this.CheckLocationOwnership(systemStar.Xpos, systemStar.Ypos);
      if (bySystemIndex.OtherEmpires != null && bySystemIndex.OtherEmpires.Count > 0)
      {
        for (int index = 0; index < bySystemIndex.OtherEmpires.Count; ++index)
          otherEmpireIds.Add(bySystemIndex.OtherEmpires[index].Empire.EmpireId);
        disputed = true;
      }
      return bySystemIndex.DominantEmpire.Empire.EmpireId;
    }

    public int CheckLocationOwnership(double x, double y)
    {
      if (this._Territory != null)
      {
        int indexX;
        int indexY;
        EmpireTerritory.CalculateTerritoryIndexesForGalaxyPosition(x, y, out indexX, out indexY);
        if (indexX >= 0 && indexX < EmpireTerritory.TerritoryIndexSize && indexY >= 0 && indexY < EmpireTerritory.TerritoryIndexSize)
          return (int) this._Territory[indexX][indexY] - 1;
      }
      return -1;
    }

    private static void CalculateTerritoryIndexesForGalaxyPosition(
      double x,
      double y,
      out int indexX,
      out int indexY)
    {
      indexX = (int) (x / ((double) Galaxy.SizeX / (double) EmpireTerritory.TerritoryIndexSize));
      indexY = (int) (y / ((double) Galaxy.SizeY / (double) EmpireTerritory.TerritoryIndexSize));
    }

    public static byte[][] CalculateEmpireTerritoryGridIndex(
      Galaxy galaxy,
      Rectangle galaxySection,
      int sizeX,
      int sizeY,
      byte[][] influence,
      bool onlySystems)
    {
      if (influence == null)
      {
        influence = new byte[sizeX][];
        for (int index = 0; index < sizeX; ++index)
          influence[index] = new byte[sizeY];
      }
      double num1 = (double) Galaxy.SizeX / (double) sizeX;
      int val2_1 = 0;
      int val1_1 = sizeX;
      int val2_2 = 0;
      int val1_2 = sizeY;
      double num2 = (double) galaxySection.X / num1;
      double num3 = (double) galaxySection.Y / num1;
      for (int index = 0; index < galaxy.Empires.Count; ++index)
      {
        Empire empire = galaxy.Empires[index];
        if (empire != null && empire.Active)
        {
          bool empireHasWarptech = empire.CheckEmpireHasHyperDriveTech(empire);
          empire.RecalculateColonyInfluenceRadiuses(empireHasWarptech);
        }
      }
      HabitatList[][] colonyIndex = EmpireTerritory.BuildColonyIndex(galaxy);
      EmpireTerritoryColonyList territoryColonyList = new EmpireTerritoryColonyList();
      for (int index1 = 0; index1 < galaxy.Empires.Count; ++index1)
      {
        Empire empire = galaxy.Empires[index1];
        if (empire != null && empire.Active && empire.Colonies != null)
        {
          for (int index2 = 0; index2 < empire.Colonies.Count; ++index2)
          {
            Habitat colony = empire.Colonies[index2];
            if (colony != null && !colony.HasBeenDestroyed && colony.Owner == empire)
            {
              int colonyInfluenceRadius = (int) colony.ColonyInfluenceRadius;
              Rectangle rect = new Rectangle((int) colony.Xpos - colonyInfluenceRadius, (int) colony.Ypos - colonyInfluenceRadius, colonyInfluenceRadius * 2, colonyInfluenceRadius * 2);
              if (galaxySection.IntersectsWith(rect))
              {
                EmpireTerritoryColony empireTerritoryColony = new EmpireTerritoryColony(colony);
                if (!onlySystems)
                {
                  HabitatList overlappingColonies = EmpireTerritory.DetermineOverlappingColonies(galaxy, colonyIndex, colony, true, false);
                  if (overlappingColonies.Count > 0 && empireTerritoryColony.OverlappingColonies != null)
                    empireTerritoryColony.OverlappingColonies.AddRange((IEnumerable<Habitat>) overlappingColonies);
                }
                territoryColonyList.Add(empireTerritoryColony);
              }
            }
          }
        }
      }
      if (onlySystems)
      {
        int num4 = 0;
        int x = galaxySection.X;
        int y = galaxySection.Y;
        for (int index3 = 0; index3 < galaxy.Systems.Count; ++index3)
        {
          SystemInfo system = galaxy.Systems[index3];
          if (system != null && system.SystemStar != null)
          {
            Point pt = new Point((int) system.SystemStar.Xpos, (int) system.SystemStar.Ypos);
            if (galaxySection.Contains(pt))
            {
              int indexX;
              int indexY;
              EmpireTerritory.CalculateTerritoryIndexesForGalaxyPosition(system.SystemStar.Xpos, system.SystemStar.Ypos, out indexX, out indexY);
              if (influence[indexX][indexY] == (byte) 0)
              {
                float num5 = 0.0f;
                if (territoryColonyList != null)
                {
                  for (int index4 = 0; index4 < territoryColonyList.Count; ++index4)
                  {
                    EmpireTerritoryColony empireTerritoryColony = territoryColonyList[index4];
                    if (empireTerritoryColony != null && empireTerritoryColony.Colony != null && !empireTerritoryColony.Colony.HasBeenDestroyed && empireTerritoryColony.Colony.Empire != null)
                    {
                      float influenceAtPoint = EmpireTerritory.CalculateColonyInfluenceAtPoint(galaxy, empireTerritoryColony.Colony.ColonyInfluenceRadius, (float) empireTerritoryColony.Colony.Xpos, (float) empireTerritoryColony.Colony.Ypos, (float) system.SystemStar.Xpos, (float) system.SystemStar.Ypos);
                      if ((double) influenceAtPoint > (double) num5)
                      {
                        num4 = empireTerritoryColony.Colony.Empire.EmpireId + 1;
                        num5 = influenceAtPoint;
                      }
                    }
                  }
                }
                if ((double) num5 > 0.0)
                  influence[indexX][indexY] = (byte) num4;
              }
            }
          }
        }
      }
      else
      {
        float num6 = (float) num1;
        int num7 = 0;
        int num8 = 0;
        if (territoryColonyList != null)
        {
          for (int index5 = 0; index5 < territoryColonyList.Count; ++index5)
          {
            EmpireTerritoryColony empireTerritoryColony = territoryColonyList[index5];
            if (empireTerritoryColony != null)
            {
              Habitat colony = empireTerritoryColony.Colony;
              if (colony != null)
              {
                int num9 = 0;
                if (colony != null && colony.Empire != null)
                  num9 = colony.Empire.EmpireId + 1;
                num7 = num9;
                num8 = 0;
                int x1 = galaxySection.X;
                int y1 = galaxySection.Y;
                int indexX;
                int indexY;
                EmpireTerritory.CalculateTerritoryIndexesForGalaxyPosition(colony.Xpos, colony.Ypos, out indexX, out indexY);
                int num10 = (int) ((double) colony.ColonyInfluenceRadius / num1);
                float colonyInfluenceRadius = colony.ColonyInfluenceRadius;
                int val1_3 = indexX - num10;
                int val1_4 = indexY - num10;
                int num11 = num10 * 2 + 2;
                int num12 = num11;
                int num13 = Math.Max(val1_3, val2_1);
                int num14 = Math.Max(val1_4, val2_2);
                int num15 = Math.Min(val1_1, num13 + num11) - num13;
                int num16 = Math.Min(val1_2, num14 + num12) - num14;
                int num17 = num13 + num15;
                int num18 = num14 + num16;
                for (int index6 = num13; index6 < num17; ++index6)
                {
                  float x2 = (float) index6 * num6;
                  for (int index7 = num14; index7 < num18; ++index7)
                  {
                    if (influence[index6][index7] == (byte) 0)
                    {
                      float y2 = (float) index7 * num6;
                      float num19 = EmpireTerritory.CalculateColonyInfluenceAtPoint(galaxy, colonyInfluenceRadius, (float) colony.Xpos, (float) colony.Ypos, x2, y2);
                      if ((double) num19 > 0.0)
                      {
                        int num20 = num9;
                        if (empireTerritoryColony.OverlappingColonies != null)
                        {
                          int count = empireTerritoryColony.OverlappingColonies.Count;
                          for (int index8 = 0; index8 < count; ++index8)
                          {
                            Habitat overlappingColony = empireTerritoryColony.OverlappingColonies[index8];
                            if (overlappingColony != null && overlappingColony.Empire != null)
                            {
                              float influenceAtPoint = EmpireTerritory.CalculateColonyInfluenceAtPoint(galaxy, overlappingColony.ColonyInfluenceRadius, (float) overlappingColony.Xpos, (float) overlappingColony.Ypos, x2, y2);
                              if ((double) influenceAtPoint > (double) num19)
                              {
                                num19 = influenceAtPoint;
                                num20 = overlappingColony.Empire.EmpireId + 1;
                              }
                            }
                          }
                        }
                        if ((double) num19 > 0.0)
                          influence[index6][index7] = (byte) num20;
                      }
                    }
                  }
                }
              }
            }
          }
        }
      }
      return influence;
    }

    public static Bitmap CalculateEmpireSystemTerritory(
      Galaxy galaxy,
      Rectangle galaxySection,
      int sizeX,
      int sizeY,
      Empire viewingEmpire,
      bool godMode,
      Bitmap influenceImage)
    {
      return EmpireTerritory.CalculateEmpireSystemTerritory(galaxy, galaxySection, sizeX, sizeY, viewingEmpire, godMode, influenceImage, 1.0);
    }

    public static Bitmap CalculateEmpireSystemTerritory(
      Galaxy galaxy,
      Rectangle galaxySection,
      int sizeX,
      int sizeY,
      Empire viewingEmpire,
      bool godMode,
      Bitmap influenceImage,
      double systemInfluenceSizeFactor)
    {
      Bitmap bitmapSafely = GraphicsHelper.CreateBitmapSafely(sizeX, sizeY, PixelFormat.Format32bppPArgb);
      EmpireTerritory.CalculateEmpireSystemTerritory(ref bitmapSafely, galaxy, galaxySection, sizeX, sizeY, viewingEmpire, godMode, influenceImage, 1.0);
      return bitmapSafely;
    }

    public static void CalculateEmpireSystemTerritory(
      ref Bitmap influence,
      Galaxy galaxy,
      Rectangle galaxySection,
      int sizeX,
      int sizeY,
      Empire viewingEmpire,
      bool godMode,
      Bitmap influenceImage,
      double systemInfluenceSizeFactor)
    {
      double num1 = (double) galaxySection.Width / (double) sizeX;
      Bitmap bitmap = (Bitmap) null;
      lock (influenceImage)
        bitmap = new Bitmap((Image) influenceImage);
      double num2 = 150000.0 * systemInfluenceSizeFactor;
      float num3 = (float) ((num2 / num1 + 0.5) * 1.1);
      double num4 = (double) galaxySection.Left - num2;
      double num5 = (double) galaxySection.Right + num2;
      double num6 = (double) galaxySection.Top - num2;
      double num7 = (double) galaxySection.Bottom + num2;
      using (Graphics graphics = Graphics.FromImage((Image) influence))
      {
        GraphicsHelper.SetGraphicsQualityToLow(graphics);
        graphics.Clear(Color.Transparent);
        for (int index = 0; index < galaxy.Systems.Count; ++index)
        {
          SystemInfo system = galaxy.Systems[index];
          if (system != null && system.SystemStar != null && system.SystemStar.Xpos > num4 && system.SystemStar.Xpos < num5 && system.SystemStar.Ypos > num6 && system.SystemStar.Ypos < num7 && (godMode || viewingEmpire.CheckSystemExplored(system.SystemStar)))
          {
            bool disputed = false;
            int empireId = galaxy.EmpireTerritory.CheckSystemOwnership(galaxy, system.SystemStar, out disputed);
            if (empireId >= 0)
            {
              Empire byEmpireId = galaxy.Empires.GetByEmpireId(empireId);
              if (byEmpireId != null)
              {
                using (ImageAttributes withTransparency = GraphicsHelper.CalculateImageAttributesWithTransparency(byEmpireId.MainColor, 1.0))
                {
                  float num8 = (float) ((system.SystemStar.Xpos - (double) galaxySection.X) / num1);
                  float num9 = (float) ((system.SystemStar.Ypos - (double) galaxySection.Y) / num1);
                  Rectangle destRect = new Rectangle((int) (num8 - num3 / 2f + 1f), (int) (num9 - num3 / 2f + 1f), (int) num3, (int) num3);
                  graphics.DrawImage((Image) bitmap, destRect, 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, withTransparency);
                }
              }
            }
          }
        }
      }
      bitmap.Dispose();
    }

    public static Bitmap CalculateEmpireTerritoryGrid(
      Galaxy galaxy,
      Rectangle galaxySection,
      int sizeX,
      int sizeY,
      Empire viewingEmpire,
      bool godMode)
    {
      Bitmap influence = new Bitmap(sizeX, sizeY, PixelFormat.Format32bppPArgb);
      EmpireTerritory.CalculateEmpireTerritoryGrid(ref influence, galaxy, galaxySection, sizeX, sizeY, viewingEmpire, godMode);
      return influence;
    }

    public static void CalculateEmpireTerritoryGrid(
      ref Bitmap influence,
      Galaxy galaxy,
      Rectangle galaxySection,
      int sizeX,
      int sizeY,
      Empire viewingEmpire,
      bool godMode)
    {
      if (galaxy == null || galaxy.Empires == null)
        return;
      double num1 = (double) galaxySection.Width / (double) sizeX;
      int val2_1 = 0;
      int val1_1 = sizeX;
      int val2_2 = 0;
      int val1_2 = sizeY;
      for (int index = 0; index < galaxy.Empires.Count; ++index)
      {
        Empire empire = galaxy.Empires[index];
        if (empire != null && empire.Active)
        {
          bool empireHasWarptech = empire.CheckEmpireHasHyperDriveTech(empire);
          empire.RecalculateColonyInfluenceRadiuses(empireHasWarptech);
        }
      }
      HabitatList[][] colonyIndex = EmpireTerritory.BuildColonyIndex(galaxy);
      EmpireTerritoryColonyList territoryColonyList = new EmpireTerritoryColonyList();
      for (int index1 = 0; index1 < galaxy.Empires.Count; ++index1)
      {
        Empire empire = galaxy.Empires[index1];
        if (empire != null && empire.Active && empire.Colonies != null)
        {
          for (int index2 = 0; index2 < empire.Colonies.Count; ++index2)
          {
            Habitat colony = empire.Colonies[index2];
            if (colony != null && !colony.HasBeenDestroyed && colony.Owner == empire)
            {
              int colonyInfluenceRadius = (int) colony.ColonyInfluenceRadius;
              Rectangle rect = new Rectangle((int) colony.Xpos - colonyInfluenceRadius, (int) colony.Ypos - colonyInfluenceRadius, colonyInfluenceRadius * 2, colonyInfluenceRadius * 2);
              if (galaxySection.IntersectsWith(rect) && (godMode || viewingEmpire == null || viewingEmpire.CheckSystemExplored(colony.SystemIndex)))
              {
                EmpireTerritoryColony empireTerritoryColony = new EmpireTerritoryColony(colony);
                HabitatList overlappingColonies = EmpireTerritory.DetermineOverlappingColonies(galaxy, colonyIndex, colony, true, false);
                if (overlappingColonies != null && overlappingColonies.Count > 0)
                {
                  HabitatList items = new HabitatList();
                  for (int index3 = 0; index3 < overlappingColonies.Count; ++index3)
                  {
                    Habitat habitat = overlappingColonies[index3];
                    if (habitat != null && (godMode || viewingEmpire == null || viewingEmpire.CheckSystemExplored(habitat.SystemIndex)))
                      items.Add(habitat);
                  }
                  empireTerritoryColony.OverlappingColonies.AddRange((IEnumerable<Habitat>) items);
                }
                territoryColonyList.Add(empireTerritoryColony);
              }
            }
          }
        }
      }
      using (Graphics graphics = Graphics.FromImage((Image) influence))
      {
        GraphicsHelper.SetGraphicsQualityToLow(graphics);
        graphics.Clear(Color.Transparent);
      }
      FastBitmap fastBitmap = new FastBitmap(influence);
      float num2 = (float) num1;
      Color empty1 = Color.Empty;
      Color color1 = Color.Empty;
      Color empty2 = Color.Empty;
      for (int index4 = 0; index4 < territoryColonyList.Count; ++index4)
      {
        EmpireTerritoryColony empireTerritoryColony = territoryColonyList[index4];
        if (empireTerritoryColony != null && empireTerritoryColony.OverlappingColonies != null)
        {
          Habitat colony = empireTerritoryColony.Colony;
          if (colony != null && (godMode || viewingEmpire == null || viewingEmpire.CheckSystemExplored(colony.SystemIndex)))
          {
            Color color2 = Color.Empty;
            if (colony != null && colony.Empire != null)
              color2 = colony.Empire.MainColor;
            color1 = color2;
            Color color3 = Color.Empty;
            float x1 = (float) galaxySection.X;
            float y1 = (float) galaxySection.Y;
            int num3 = (int) ((colony.Xpos - (double) galaxySection.X) / num1);
            int num4 = (int) ((colony.Ypos - (double) galaxySection.Y) / num1);
            int num5 = (int) ((double) colony.ColonyInfluenceRadius / num1);
            int val1_3 = num3 - num5;
            int val1_4 = num4 - num5;
            int num6 = num5 * 2 + 2;
            int num7 = num6;
            int num8 = Math.Max(val1_3, val2_1);
            int num9 = Math.Max(val1_4, val2_2);
            int num10 = Math.Min(val1_1, num8 + num6) - num8;
            int num11 = Math.Min(val1_2, num9 + num7) - num9;
            int num12 = num8 + num10;
            int num13 = num9 + num11;
            for (int X = num8; X < num12; ++X)
            {
              float x2 = x1 + (float) X * num2;
              for (int Y = num9; Y < num13; ++Y)
              {
                color3 = fastBitmap.GetPixel(X, Y);
                if (color3.A == (byte) 0)
                {
                  float y2 = y1 + (float) Y * num2;
                  float num14 = EmpireTerritory.CalculateColonyInfluenceAtPoint(galaxy, colony.ColonyInfluenceRadius, (float) colony.Xpos, (float) colony.Ypos, x2, y2);
                  Color Colour = color2;
                  for (int index5 = 0; index5 < empireTerritoryColony.OverlappingColonies.Count; ++index5)
                  {
                    Habitat overlappingColony = empireTerritoryColony.OverlappingColonies[index5];
                    if (overlappingColony != null)
                    {
                      float influenceAtPoint = EmpireTerritory.CalculateColonyInfluenceAtPoint(galaxy, overlappingColony.ColonyInfluenceRadius, (float) overlappingColony.Xpos, (float) overlappingColony.Ypos, x2, y2);
                      if ((double) influenceAtPoint > (double) num14 && overlappingColony.Empire != null)
                      {
                        num14 = influenceAtPoint;
                        Colour = overlappingColony.Empire.MainColor;
                      }
                    }
                  }
                  if ((double) num14 > 0.0)
                    fastBitmap.SetPixel(ref X, ref Y, Colour);
                }
              }
            }
          }
        }
      }
      fastBitmap.Dispose();
    }

    public static float CalculateColonyInfluenceAtPoint(
      Galaxy galaxy,
      Habitat colony,
      float x,
      float y)
    {
      return EmpireTerritory.CalculateColonyInfluenceAtPoint(galaxy, colony.ColonyInfluenceRadius, (float) colony.Xpos, (float) colony.Ypos, x, y);
    }

    public static float CalculateColonyInfluenceAtPoint(
      Galaxy galaxy,
      float influenceRadius,
      float colonyX,
      float colonyY,
      float x,
      float y)
    {
      float num1 = colonyX - x;
      float num2 = colonyY - y;
      float num3 = Math.Max(1f, (float) ((double) num2 * (double) num2 + (double) num1 * (double) num1));
      float num4 = influenceRadius * influenceRadius;
      return (double) num4 >= (double) num3 ? num4 / num3 : 0.0f;
    }

    public static float CalculateColonyInfluenceAtPointWithXYCheck(
      Galaxy galaxy,
      float influenceRadius,
      float colonyX,
      float colonyY,
      float x,
      float y)
    {
      if ((double) colonyX + (double) influenceRadius > (double) x && (double) colonyX - (double) influenceRadius < (double) x && (double) colonyY + (double) influenceRadius > (double) y && (double) colonyY - (double) influenceRadius < (double) y)
      {
        float num1 = colonyX - x;
        float num2 = colonyY - y;
        float num3 = (float) ((double) num2 * (double) num2 + (double) num1 * (double) num1);
        float num4 = influenceRadius * influenceRadius;
        if ((double) num4 >= (double) num3)
          return num4 / num3;
      }
      return 0.0f;
    }

    public static Bitmap CalculateEmpireTerritory(
      Galaxy galaxy,
      Rectangle galaxySection,
      int sizeX,
      int sizeY)
    {
      Bitmap empireTerritory = new Bitmap(sizeX, sizeY, PixelFormat.Format32bppPArgb);
      double scaleFactor = (double) galaxySection.Width / (double) sizeX;
      for (int index = 0; index < galaxy.Empires.Count; ++index)
      {
        Empire empire = galaxy.Empires[index];
        if (empire != null && empire.Active)
        {
          bool empireHasWarptech = empire.CheckEmpireHasHyperDriveTech(empire);
          empire.RecalculateColonyInfluenceRadiuses(empireHasWarptech);
        }
      }
      HabitatList[][] colonyIndex = EmpireTerritory.BuildColonyIndex(galaxy);
      EmpireTerritoryColonyList colonies = new EmpireTerritoryColonyList();
      for (int index1 = 0; index1 < galaxy.Empires.Count; ++index1)
      {
        Empire empire = galaxy.Empires[index1];
        if (empire != null && empire.Active)
        {
          for (int index2 = 0; index2 < empire.Colonies.Count; ++index2)
          {
            Habitat colony = empire.Colonies[index2];
            if (colony != null && !colony.HasBeenDestroyed && colony.Owner == empire && galaxySection.Contains((int) colony.Xpos, (int) colony.Ypos))
            {
              EmpireTerritoryColony empireTerritoryColony = new EmpireTerritoryColony(colony);
              HabitatList overlappingColonies = EmpireTerritory.DetermineOverlappingColonies(galaxy, colonyIndex, colony, false, true);
              if (overlappingColonies.Count > 0)
                empireTerritoryColony.OverlappingColonies.AddRange((IEnumerable<Habitat>) overlappingColonies);
              colonies.Add(empireTerritoryColony);
            }
          }
        }
      }
      EmpireTerritoryColonyList territoryColonyList = EmpireTerritoryColonyList.SortColoniesByInfluence(colonies);
      using (Graphics graphics1 = Graphics.FromImage((Image) empireTerritory))
      {
        graphics1.CompositingQuality = CompositingQuality.HighSpeed;
        graphics1.InterpolationMode = InterpolationMode.NearestNeighbor;
        graphics1.SmoothingMode = SmoothingMode.None;
        for (int index3 = 0; index3 < territoryColonyList.Count; ++index3)
        {
          EmpireTerritoryColony empireTerritoryColony = territoryColonyList[index3];
          Habitat colony = empireTerritoryColony.Colony;
          Color color = Color.Empty;
          if (empireTerritoryColony.Colony != null && empireTerritoryColony.Colony.Empire != null)
            color = empireTerritoryColony.Colony.Empire.MainColor;
          if (empireTerritoryColony.OverlappingColonies.Count > 0 && !color.IsEmpty)
          {
            double colonyInfluenceRadius1 = (double) colony.ColonyInfluenceRadius;
            int centerOffset = (int) (colonyInfluenceRadius1 / scaleFactor);
            if (centerOffset > 0)
            {
              Bitmap bitmap = new Bitmap(centerOffset * 2, centerOffset * 2, PixelFormat.Format32bppPArgb);
              using (Graphics graphics2 = Graphics.FromImage((Image) bitmap))
              {
                graphics2.CompositingQuality = CompositingQuality.HighSpeed;
                graphics2.InterpolationMode = InterpolationMode.NearestNeighbor;
                graphics2.SmoothingMode = SmoothingMode.None;
                using (SolidBrush solidBrush1 = new SolidBrush(color))
                {
                  graphics2.Clear(Color.Black);
                  graphics2.FillEllipse((Brush) solidBrush1, 0, 0, centerOffset * 2, centerOffset * 2);
                  for (int index4 = 0; index4 < empireTerritoryColony.OverlappingColonies.Count; ++index4)
                  {
                    Habitat overlappingColony = empireTerritoryColony.OverlappingColonies[index4];
                    if (overlappingColony != null && !overlappingColony.HasBeenDestroyed)
                    {
                      double colonyInfluenceRadius2 = (double) overlappingColony.ColonyInfluenceRadius;
                      double intersect1X;
                      double intersect1Y;
                      double intersect2X;
                      double intersect2Y;
                      int intersectionPoints = EmpireTerritory.DetermineCircleIntersectionPoints(galaxy, colony.Xpos, colony.Ypos, colonyInfluenceRadius1, overlappingColony.Xpos, overlappingColony.Ypos, colonyInfluenceRadius2, out intersect1X, out intersect1Y, out intersect2X, out intersect2Y);
                      switch (intersectionPoints)
                      {
                        case -1:
                        case 1:
                          double distance = galaxy.CalculateDistance(colony.Xpos, colony.Ypos, overlappingColony.Xpos, overlappingColony.Ypos);
                          double num1 = colonyInfluenceRadius2 / (colonyInfluenceRadius2 + colonyInfluenceRadius1);
                          double angle = Galaxy.DetermineAngle(overlappingColony.Xpos, overlappingColony.Ypos, colony.Xpos, colony.Ypos);
                          double num2 = overlappingColony.Xpos + Math.Cos(angle) * distance * num1;
                          double num3 = overlappingColony.Ypos + Math.Sin(angle) * distance * num1;
                          double num4 = angle + Math.PI;
                          double num5 = overlappingColony.Xpos + Math.Cos(num4) * colonyInfluenceRadius2;
                          double num6 = overlappingColony.Ypos + Math.Sin(num4) * colonyInfluenceRadius2;
                          double num7 = (intersect1X - colony.Xpos) / scaleFactor;
                          double num8 = (intersect1Y - colony.Ypos) / scaleFactor;
                          double num9 = (intersect2X - colony.Xpos) / scaleFactor;
                          double num10 = (intersect2Y - colony.Ypos) / scaleFactor;
                          double num11 = (num2 - colony.Xpos) / scaleFactor;
                          double num12 = (num3 - colony.Ypos) / scaleFactor;
                          double num13 = (num5 - colony.Xpos) / scaleFactor;
                          double num14 = (num6 - colony.Ypos) / scaleFactor;
                          if ((double) colony.ColonyInfluenceRadius < (double) overlappingColony.ColonyInfluenceRadius)
                          {
                            using (GraphicsPath path = EmpireTerritory.BuildCurveForTerritoryOverlap(galaxy, colony, overlappingColony, intersectionPoints, scaleFactor, (float) centerOffset))
                            {
                              using (SolidBrush solidBrush2 = new SolidBrush(Color.Black))
                              {
                                graphics2.FillPath((Brush) solidBrush2, path);
                                continue;
                              }
                            }
                          }
                          else
                            continue;
                        default:
                          continue;
                      }
                    }
                  }
                  bitmap.MakeTransparent(Color.Black);
                }
              }
              int num15 = (int) ((colony.Xpos - (double) galaxySection.X) / scaleFactor);
              int num16 = (int) ((colony.Ypos - (double) galaxySection.Y) / scaleFactor);
              graphics1.DrawImage((Image) bitmap, new Point(num15 - centerOffset, num16 - centerOffset));
            }
          }
          else
          {
            int num17 = (int) ((double) colony.ColonyInfluenceRadius / scaleFactor);
            if (num17 > 0)
            {
              using (SolidBrush solidBrush = new SolidBrush(color))
              {
                int num18 = (int) ((colony.Xpos - (double) galaxySection.X) / scaleFactor);
                int num19 = (int) ((colony.Ypos - (double) galaxySection.Y) / scaleFactor);
                graphics1.FillEllipse((Brush) solidBrush, new Rectangle(num18 - num17, num19 - num17, num17 * 2, num17 * 2));
              }
            }
          }
        }
      }
      return empireTerritory;
    }

    private static GraphicsPath BuildCurveForTerritoryOverlap(
      Galaxy galaxy,
      Habitat colony,
      Habitat foreignColony,
      int intersectType,
      double scaleFactor,
      float centerOffset)
    {
      GraphicsPath graphicsPath = new GraphicsPath();
      PointF[] pointFArray1 = new PointF[13];
      double x1 = colony.Xpos / scaleFactor;
      double y1 = colony.Ypos / scaleFactor;
      double x2 = foreignColony.Xpos / scaleFactor;
      double y2 = foreignColony.Ypos / scaleFactor;
      float val1 = colony.ColonyInfluenceRadius / (float) scaleFactor;
      float val2 = foreignColony.ColonyInfluenceRadius / (float) scaleFactor;
      double distance = galaxy.CalculateDistance(x1, y1, x2, y2);
      double num1 = (double) val2 / ((double) val2 + (double) val1);
      double num2 = distance * num1;
      double num3 = (double) val2;
      float y3 = centerOffset + (float) distance - (float) num2;
      float y4 = centerOffset + (float) distance + (float) num3;
      float x3 = centerOffset;
      float y5 = centerOffset + (float) distance;
      double num4 = 0.2761423749154;
      double num5 = 0.0;
      switch (intersectType)
      {
        case -1:
          double num6 = Math.Min(1.0, distance / (double) val1);
          num5 = (double) val2 * num6;
          break;
        case 1:
          double num7 = ((double) val1 * (double) val1 - (double) val2 * (double) val2 + distance * distance) / (2.0 * distance);
          num5 = Math.Sqrt((double) val1 * (double) val1 - num7 * num7);
          y5 = centerOffset + (float) num7;
          if ((double) y5 < (double) y3)
          {
            PointF[] pointFArray2 = new PointF[7];
            PointF[] pts = new PointF[4];
            float num8 = Math.Max(val1, val2);
            float x4 = centerOffset - num8;
            float x5 = centerOffset + num8;
            float x6 = centerOffset - (float) num5;
            float x7 = centerOffset + (float) num5;
            float num9 = (float) (num2 * 2.0 * num4);
            float num10 = (float) (num3 * 2.0 * num4);
            float num11 = (float) (num5 * 2.0 * num4);
            pointFArray2[0] = new PointF(x6, y5);
            pointFArray2[1] = new PointF(x6, y5 + num9);
            pointFArray2[2] = new PointF(x3 - num11, y3);
            pointFArray2[3] = new PointF(x3, y3);
            pointFArray2[4] = new PointF(x3 + num11, y3);
            pointFArray2[5] = new PointF(x7, y5 + num9);
            pointFArray2[6] = new PointF(x7, y5);
            pts[0] = new PointF(x5, y5);
            pts[1] = new PointF(x5, y5 + num10);
            pts[2] = new PointF(x4, y5 + num10);
            pts[3] = new PointF(x4, y5);
            Matrix matrix = new Matrix();
            float angle = (float) (Galaxy.DetermineAngle(x1, y1, x2, y2) - Math.PI / 2.0) * 57.29578f % 360f;
            if ((double) angle < 0.0)
              angle += 360f;
            matrix.RotateAt(angle, new PointF(centerOffset, centerOffset));
            matrix.TransformPoints(pointFArray2);
            matrix.TransformPoints(pts);
            graphicsPath.StartFigure();
            graphicsPath.AddBeziers(pointFArray2);
            graphicsPath.AddLine(pointFArray2[6], pts[0]);
            graphicsPath.AddLine(pts[0], pts[1]);
            graphicsPath.AddLine(pts[1], pts[2]);
            graphicsPath.AddLine(pts[2], pts[3]);
            graphicsPath.AddLine(pts[3], pointFArray2[0]);
            graphicsPath.CloseFigure();
            return graphicsPath;
          }
          break;
      }
      float x8 = centerOffset - (float) num5;
      float x9 = centerOffset + (float) num5;
      float num12 = (float) (num2 * 2.0 * num4);
      float num13 = (float) (num3 * 2.0 * num4);
      float num14 = (float) (num5 * 2.0 * num4);
      pointFArray1[0] = new PointF(x8, y5);
      pointFArray1[1] = new PointF(x8, y5 - num12);
      pointFArray1[2] = new PointF(x3 - num14, y3);
      pointFArray1[3] = new PointF(x3, y3);
      pointFArray1[4] = new PointF(x3 + num14, y3);
      pointFArray1[5] = new PointF(x9, y5 - num12);
      pointFArray1[6] = new PointF(x9, y5);
      pointFArray1[7] = new PointF(x9, y5 + num13);
      pointFArray1[8] = new PointF(x3 + num14, y4);
      pointFArray1[9] = new PointF(x3, y4);
      pointFArray1[10] = new PointF(x3 - num14, y4);
      pointFArray1[11] = new PointF(x8, y5 + num13);
      pointFArray1[12] = new PointF(x8, y5);
      Matrix matrix1 = new Matrix();
      float angle1 = (float) (Galaxy.DetermineAngle(x1, y1, x2, y2) - Math.PI / 2.0) * 57.29578f % 360f;
      if ((double) angle1 < 0.0)
        angle1 += 360f;
      matrix1.RotateAt(angle1, new PointF(centerOffset, centerOffset));
      matrix1.TransformPoints(pointFArray1);
      graphicsPath.StartFigure();
      graphicsPath.AddBeziers(pointFArray1);
      graphicsPath.CloseFigure();
      return graphicsPath;
    }

    private static int DetermineCircleIntersectionPoints(
      Galaxy galaxy,
      double center1X,
      double center1Y,
      double radius1,
      double center2X,
      double center2Y,
      double radius2,
      out double intersect1X,
      out double intersect1Y,
      out double intersect2X,
      out double intersect2Y)
    {
      intersect1X = 0.0;
      intersect1Y = 0.0;
      intersect2X = 0.0;
      intersect2Y = 0.0;
      double num1 = center2X - center1X;
      double num2 = center2Y - center1Y;
      double distance = galaxy.CalculateDistance(center1X, center1Y, center2X, center2Y);
      if (distance > radius1 + radius2)
        return 0;
      if (distance < Math.Abs(radius1 - radius2))
        return -1;
      double num3 = (radius1 * radius1 - radius2 * radius2 + distance * distance) / (2.0 * distance);
      double num4 = center1X + num1 * (num3 / distance);
      double num5 = center1Y + num2 * (num3 / distance);
      double num6 = Math.Sqrt(radius1 * radius1 - num3 * num3);
      double num7 = -num2 * (num6 / distance);
      double num8 = num1 * (num6 / distance);
      intersect1X = num4 + num7;
      intersect1Y = num5 + num8;
      intersect2X = num4 - num7;
      intersect2Y = num5 - num8;
      return 1;
    }

    private static HabitatList[][] BuildColonyIndex(Galaxy galaxy)
    {
      HabitatList[][] habitatListArray = new HabitatList[Galaxy.SectorMaxX][];
      for (int index1 = 0; index1 < Galaxy.SectorMaxX; ++index1)
      {
        habitatListArray[index1] = new HabitatList[Galaxy.SectorMaxY];
        for (int index2 = 0; index2 < Galaxy.SectorMaxY; ++index2)
          habitatListArray[index1][index2] = new HabitatList();
      }
      for (int index3 = 0; index3 < galaxy.Empires.Count; ++index3)
      {
        Empire empire = galaxy.Empires[index3];
        if (empire != null && empire.Active)
        {
          for (int index4 = 0; index4 < empire.Colonies.Count; ++index4)
          {
            Habitat colony = empire.Colonies[index4];
            if (colony != null && !colony.HasBeenDestroyed)
            {
              Sector sector = galaxy.ResolveSector(colony.Xpos, colony.Ypos);
              if (sector != null)
                habitatListArray[sector.X][sector.Y].Add(colony);
            }
          }
        }
      }
      return habitatListArray;
    }

    public static HabitatList ObtainColoniesNearLocation(
      double x,
      double y,
      Galaxy galaxy,
      HabitatList[][] colonyIndex)
    {
      HabitatList coloniesNearLocation = new HabitatList();
      Sector sector = galaxy.ResolveSector(x, y);
      int num1 = Math.Max(0, sector.X - 2);
      int num2 = Math.Min(Galaxy.SectorMaxX - 1, sector.X + 2);
      int num3 = Math.Max(0, sector.Y - 2);
      int num4 = Math.Min(Galaxy.SectorMaxY - 1, sector.Y + 2);
      for (int index1 = num1; index1 <= num2; ++index1)
      {
        for (int index2 = num3; index2 <= num4; ++index2)
          coloniesNearLocation.AddRange((IEnumerable<Habitat>) colonyIndex[index1][index2]);
      }
      return coloniesNearLocation;
    }

    public static HabitatList DetermineOverlappingColonies(
      Galaxy galaxy,
      HabitatList[][] colonyIndex,
      Habitat colony,
      bool useRectangularEvaluation,
      bool onlyForeignColonies)
    {
      HabitatList coloniesNearLocation = EmpireTerritory.ObtainColoniesNearLocation(colony.Xpos, colony.Ypos, galaxy, colonyIndex);
      return EmpireTerritory.DetermineOverlappingColonies(galaxy, coloniesNearLocation, colony, useRectangularEvaluation, onlyForeignColonies);
    }

    public static HabitatList DetermineOverlappingColonies(
      Galaxy galaxy,
      HabitatList nearbyColonies,
      Habitat colony,
      bool useRectangularEvaluation,
      bool onlyForeignColonies)
    {
      HabitatList overlappingColonies = new HabitatList();
      if (nearbyColonies != null)
      {
        float colonyInfluenceRadius1 = colony.ColonyInfluenceRadius;
        double num1 = (double) colonyInfluenceRadius1 * (double) colonyInfluenceRadius1;
        Rectangle rectangle = new Rectangle((int) (colony.Xpos - (double) colonyInfluenceRadius1), (int) (colony.Ypos - (double) colonyInfluenceRadius1), (int) ((double) colonyInfluenceRadius1 * 2.0), (int) ((double) colonyInfluenceRadius1 * 2.0));
        for (int index = 0; index < nearbyColonies.Count; ++index)
        {
          Habitat nearbyColony = nearbyColonies[index];
          if (nearbyColony != null && nearbyColony != colony && (!onlyForeignColonies || nearbyColony.Empire != colony.Empire))
          {
            if (useRectangularEvaluation)
            {
              float colonyInfluenceRadius2 = nearbyColony.ColonyInfluenceRadius;
              Rectangle rect = new Rectangle((int) (nearbyColony.Xpos - (double) colonyInfluenceRadius2), (int) (nearbyColony.Ypos - (double) colonyInfluenceRadius2), (int) ((double) colonyInfluenceRadius2 * 2.0), (int) ((double) colonyInfluenceRadius2 * 2.0));
              if (rectangle.IntersectsWith(rect))
                overlappingColonies.Add(nearbyColony);
            }
            else
            {
              double distanceSquared = galaxy.CalculateDistanceSquared(colony.Xpos, colony.Ypos, nearbyColony.Xpos, nearbyColony.Ypos);
              double num2 = (double) nearbyColony.ColonyInfluenceRadius * (double) nearbyColony.ColonyInfluenceRadius;
              if (distanceSquared < num1 + num2)
                overlappingColonies.Add(nearbyColony);
            }
          }
        }
      }
      return overlappingColonies;
    }

    public static double CalculateColonyInfluenceAtPoint(
      Galaxy galaxy,
      double x,
      double y,
      double colonyInfluencePower,
      double colonyInfluenceDistance,
      double colonyX,
      double colonyY)
    {
      double influenceAtPoint = 0.0;
      double distance = galaxy.CalculateDistance(x, y, colonyX, colonyY);
      if (distance < colonyInfluenceDistance)
        influenceAtPoint = colonyInfluencePower * (distance / colonyInfluenceDistance);
      return influenceAtPoint;
    }
  }
}
