// Decompiled with JetBrains decompiler
// Type: BaconDistantWorlds.BaconInfoPanel
// Assembly: BaconDistantWorlds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BEEE04E-F6B8-4927-96B6-91C74B502CC0
// Assembly location: H:\7\BaconDistantWorlds.dll

using DistantWorlds.Controls;
using DistantWorlds.Types;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;

namespace BaconDistantWorlds
{
  public static class BaconInfoPanel
  {
    public static bool shadow = true;

    public static void DrawStringWithDropShadow(
      InfoPanel infoPanel,
      Graphics graphics,
      string text,
      Font font,
      Point location)
    {
      BaconInfoPanel.DrawStringWithDropShadow(infoPanel, graphics, text, font, location, infoPanel._WhiteBrush);
    }

    public static void DrawStringColorWithDropShadow(
      InfoPanel panel,
      Graphics graphics,
      string text,
      Font font,
      Point location,
      Color color)
    {
      if (BaconInfoPanel.shadow)
      {
        location = new Point(location.X + 1, location.Y + 1);
        graphics.DrawString(text, font, (Brush) panel._BlackBrush, (PointF) location);
        location = new Point(location.X - 1, location.Y - 1);
        using (SolidBrush solidBrush = new SolidBrush(color))
          graphics.DrawString(text, font, (Brush) solidBrush, (PointF) location);
      }
      else
        graphics.DrawString(text, font, (Brush) panel._RedBrush, (PointF) location);
    }

    public static void DrawStringRedWithDropShadow(
      InfoPanel panel,
      Graphics graphics,
      string text,
      Font font,
      Point location)
    {
      if (BaconInfoPanel.shadow)
      {
        location = new Point(location.X + 1, location.Y + 1);
        graphics.DrawString(text, font, (Brush) panel._BlackBrush, (PointF) location);
        location = new Point(location.X - 1, location.Y - 1);
        graphics.DrawString(text, font, (Brush) panel._RedBrush, (PointF) location);
      }
      else
        graphics.DrawString(text, font, (Brush) panel._RedBrush, (PointF) location);
    }

    public static void DrawStringWithDropShadow(
      InfoPanel panel,
      Graphics graphics,
      string text,
      Font font,
      Point location,
      SolidBrush brush)
    {
      if (BaconInfoPanel.shadow)
      {
        location = new Point(location.X + 1, location.Y + 1);
        using (SolidBrush solidBrush = new SolidBrush(panel.CheckDropshadowColor(brush.Color)))
        {
          graphics.DrawString(text, font, (Brush) solidBrush, (PointF) location);
          location = new Point(location.X - 1, location.Y - 1);
          graphics.DrawString(text, font, (Brush) brush, (PointF) location);
        }
      }
      else
      {
        using (SolidBrush solidBrush = new SolidBrush(panel.CheckDropshadowColor(brush.Color)))
        {
          solidBrush.Color = Color.White;
          Font font1 = new Font("Arial", (float) (int) Math.Max(10f, font.Size / 2f));
          graphics.DrawString(text, font1, (Brush) solidBrush, (PointF) location);
        }
      }
    }

    public static void DrawStringWithDropShadow(
      InfoPanel panel,
      Graphics graphics,
      string text,
      Font font,
      Point location,
      SolidBrush brush,
      int maxWidth)
    {
      if (BaconInfoPanel.shadow)
      {
        StringFormat format = new StringFormat(StringFormatFlags.FitBlackBox)
        {
          Trimming = StringTrimming.EllipsisCharacter
        };
        SizeF sizeF = graphics.MeasureString(text, font, maxWidth, format);
        RectangleF layoutRectangle1 = new RectangleF((float) location.X + 1f, (float) location.Y + 1f, sizeF.Width + 3f, sizeF.Height + 3f);
        using (SolidBrush solidBrush = new SolidBrush(panel.CheckDropshadowColor(brush.Color)))
        {
          graphics.DrawString(text, font, (Brush) solidBrush, layoutRectangle1, format);
          RectangleF layoutRectangle2 = new RectangleF((float) location.X, (float) location.Y, sizeF.Width + 3f, sizeF.Height + 3f);
          graphics.DrawString(text, font, (Brush) brush, layoutRectangle2, format);
        }
      }
      else
      {
        StringFormat format = new StringFormat(StringFormatFlags.FitBlackBox)
        {
          Trimming = StringTrimming.EllipsisCharacter
        };
        SizeF sizeF = graphics.MeasureString(text, font, maxWidth, format);
        RectangleF layoutRectangle = new RectangleF((float) location.X, (float) location.Y, sizeF.Width + 3f, sizeF.Height + 3f);
        using (SolidBrush solidBrush = new SolidBrush(panel.CheckDropshadowColor(brush.Color)))
          graphics.DrawString(text, font, (Brush) solidBrush, layoutRectangle, format);
      }
    }

    public static void DrawStringWithDropShadowBounded(
      InfoPanel panel,
      Graphics graphics,
      string text,
      Font font,
      Point location,
      SizeF size,
      SolidBrush brush)
    {
      if (BaconInfoPanel.shadow)
      {
        location = new Point(location.X + 1, location.Y + 1);
        PointF pointF1 = new PointF((float) location.X, (float) location.Y);
        using (StringFormat format = (StringFormat) StringFormat.GenericTypographic.Clone())
        {
          format.Trimming = StringTrimming.EllipsisWord;
          RectangleF layoutRectangle = new RectangleF(pointF1.X, pointF1.Y, size.Width + 2f, size.Height + 2f);
          graphics.DrawString(text, font, (Brush) panel._BlackBrush, layoutRectangle, format);
          location = new Point(location.X - 1, location.Y - 1);
          PointF pointF2 = new PointF((float) location.X, (float) location.Y);
          layoutRectangle = new RectangleF(pointF2.X, pointF2.Y, size.Width + 2f, size.Height + 2f);
          graphics.DrawString(text, font, (Brush) brush, layoutRectangle, format);
        }
      }
      else
      {
        location = new Point(location.X + 1, location.Y + 1);
        PointF pointF = new PointF((float) location.X, (float) location.Y);
        using (StringFormat format = (StringFormat) StringFormat.GenericTypographic.Clone())
        {
          format.Trimming = StringTrimming.EllipsisWord;
          RectangleF layoutRectangle = new RectangleF(pointF.X, pointF.Y, size.Width + 2f, size.Height + 2f);
          using (SolidBrush solidBrush = new SolidBrush(Color.White))
            graphics.DrawString(text, font, (Brush) solidBrush, layoutRectangle, format);
        }
      }
    }

    public static string FormatForLargeNumbers(long value) => value < 1000000L ? value.ToString() : value.ToString("0,,") + "M";

    public static void DrawBuiltObject(
      InfoPanel infoPanel,
      BuiltObject builtObject,
      Graphics graphics)
    {
      if (builtObject.HasBeenDestroyed)
      {
        infoPanel._Game.SelectedObject = (object) null;
        infoPanel.ClearData();
      }
      else
      {
        graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
        graphics.CompositingQuality = CompositingQuality.HighQuality;
        graphics.InterpolationMode = InterpolationMode.Bilinear;
        SolidBrush solidBrush = new SolidBrush(infoPanel._UnknownColor);
        bool flag1 = false;
        if (builtObject.Empire != infoPanel._Game.PlayerEmpire)
        {
          if (infoPanel._Game.PlayerEmpire.EmpiresViewable.Contains(builtObject.Empire))
            flag1 = true;
          else if (infoPanel._ActualEmpire == infoPanel._Game.PlayerEmpire)
            flag1 = true;
        }
        DateTime dateTime;
        if (!flag1)
        {
          dateTime = DateTime.Now;
          if (dateTime.Subtract(infoPanel._LastBuiltObjectScanTime).TotalSeconds > 3.0)
            infoPanel._BuiltObjectIsScanned = infoPanel._Galaxy.CheckBuiltObjectScanned(builtObject);
          if (infoPanel._BuiltObjectIsScanned)
            flag1 = true;
        }
        int y1 = 4;
        int rowHeight = infoPanel._RowHeight;
        int x1 = 5;
        Rectangle clientRectangle = infoPanel.ClientRectangle;
        int num1 = clientRectangle.Width - 10;
        SizeF sizeF1 = graphics.MeasureString(TextResolver.GetText("Weapons"), infoPanel._NormalFontBold, infoPanel._MaxGraphTextWidth, StringFormat.GenericTypographic);
        int width1 = (int) sizeF1.Width;
        if (builtObject.IsPlanetDestroyer)
        {
          sizeF1 = graphics.MeasureString(TextResolver.GetText("SuperLaser"), infoPanel._NormalFontBold, infoPanel._MaxGraphTextWidth, StringFormat.GenericTypographic);
          width1 = (int) sizeF1.Width;
        }
        Color backgroundColor = Color.FromArgb((int) sbyte.MaxValue, 8, 8, 48);
        Font titleFont = infoPanel._TitleFont;
        infoPanel.DrawBackgroundPicture(graphics);
        Size flagSizeSmall = infoPanel._FlagSizeSmall;
        Point point1 = new Point(num1 - (flagSizeSmall.Width - 5), 4);
        if (infoPanel._ActualEmpire == infoPanel._Galaxy.IndependentEmpire)
        {
          sizeF1 = graphics.MeasureString(infoPanel._ActualEmpire.Name, infoPanel._NormalFont, infoPanel._MaxGraphTextWidth, StringFormat.GenericTypographic);
          int width2 = (int) sizeF1.Width;
          PointF point2 = new PointF((float) (num1 - width2), 6f);
          graphics.DrawString(infoPanel._ActualEmpire.Name, infoPanel._NormalFont, (Brush) infoPanel._WhiteBrush, point2);
        }
        else
        {
          graphics.DrawImageUnscaled((Image) infoPanel._EmpirePicture, point1);
          if (infoPanel._ActualEmpire != null)
            infoPanel.AddHotspot(new Rectangle(point1, infoPanel._EmpirePicture.Size), (object) infoPanel._ActualEmpire, infoPanel._ActualEmpire.Name + " (click for details)");
        }
        if (infoPanel._ActualEmpire == infoPanel._Game.PlayerEmpire && infoPanel._BuiltObject.IsAutoControlled)
        {
          Graphics graphics1 = graphics;
          Bitmap automateImage = InfoPanel._AutomateImage;
          int x2 = x1 + num1 - 16;
          clientRectangle = infoPanel.ClientRectangle;
          int y2 = clientRectangle.Height - 21;
          Point point3 = new Point(x2, y2);
          graphics1.DrawImageUnscaled((Image) automateImage, point3);
        }
        Point point4 = new Point(x1, y1);
        BaconInfoPanel.DrawStringWithDropShadow(infoPanel, graphics, builtObject.Name, titleFont, point4, new SolidBrush(infoPanel._EmpireColor));
        int y3 = y1 + titleFont.Height;
        if (builtObject.IsBlockaded)
        {
          Blockade blockade = infoPanel._Galaxy.Blockades[builtObject];
          if (blockade != null)
          {
            point4 = new Point(x1, y3);
            double num2 = (double) rowHeight / (double) InfoPanel._BlockadeImage.Height;
            int width3 = (int) ((double) InfoPanel._BlockadeImage.Width * num2);
            Rectangle rect = new Rectangle(x1, y3, width3, rowHeight);
            graphics.DrawImage((Image) InfoPanel._BlockadeImage, rect);
            point4 = new Point(x1 + width3 + 2, y3 + 3);
            graphics.DrawImage((Image) blockade.Initiator.SmallFlagPicture, point4);
            string text = string.Format(TextResolver.GetText("Blockaded by EMPIRE"), (object) blockade.Initiator.Name);
            point4 = new Point(x1 + width3 + blockade.Initiator.SmallFlagPicture.Width + 4, y3);
            BaconInfoPanel.DrawStringWithDropShadow(infoPanel, graphics, text, infoPanel._NormalFont, point4, infoPanel._WhiteBrush);
            y3 += rowHeight;
          }
        }
        if (builtObject.RaidCountdown > (byte) 0)
        {
          point4 = new Point(x1, y3);
          using (SolidBrush brush = new SolidBrush(Color.Yellow))
            BaconInfoPanel.DrawStringWithDropShadow(infoPanel, graphics, "(" + TextResolver.GetText("infoPanel base was recently Raided") + ")", infoPanel._NormalFont, point4, brush);
          y3 += rowHeight;
        }
        if (flag1 || infoPanel._ActualEmpire == infoPanel._Game.PlayerEmpire || infoPanel._Game.GodMode)
        {
          ResourceDatePairList resourceDatePairList = (ResourceDatePairList) null;
          if (builtObject.ManufacturingQueue != null)
            resourceDatePairList = builtObject.ManufacturingQueue.DeficientResources;
          else if (builtObject.RetrofitBaseManufacturingQueue != null)
            resourceDatePairList = builtObject.RetrofitBaseManufacturingQueue.DeficientResources;
          if (resourceDatePairList != null && resourceDatePairList.Count > 0)
          {
            ResourceDatePair[] array = resourceDatePairList.ToArray();
            string empty = string.Empty;
            for (int index = 0; index < array.Length; ++index)
            {
              if (index > 0)
                empty += ", ";
              empty += new Resource(array[index].ResourceId).Name;
            }
            Bitmap messageImage = InfoPanel._MessageImages[30];
            Rectangle srcRect = new Rectangle(0, 0, messageImage.Width, messageImage.Height);
            Rectangle destRect = new Rectangle(x1 + 3, y3, rowHeight, rowHeight);
            infoPanel.SetGraphicsQualityToHigh(graphics);
            graphics.DrawImage((Image) messageImage, destRect, srcRect, GraphicsUnit.Pixel);
            point4 = new Point(destRect.Right + 2, y3);
            string text = string.Format(TextResolver.GetText("Construction Resource Shortage Message Short"), (object) empty);
            SizeF size = graphics.MeasureString(text, infoPanel._NormalFont, infoPanel.ClientSize.Width - (point4.X + 5));
            using (SolidBrush brush = new SolidBrush(Color.FromArgb((int) byte.MaxValue, 128, 0)))
            {
              size = new SizeF(size.Width, Math.Min((float) ((double) rowHeight * 2.0 + 1.0), size.Height));
              BaconInfoPanel.DrawStringWithDropShadowBounded(infoPanel, graphics, text, infoPanel._NormalFont, point4, size, brush);
            }
            y3 += (int) size.Height;
          }
        }
        int y4 = y3 + infoPanel._Height3;
        if (builtObject.Role != BuiltObjectRole.Base)
        {
          point4 = new Point(x1, y4);
          bool flag2 = false;
          if (infoPanel._ActualEmpire != infoPanel._Game.PlayerEmpire && builtObject.Role != BuiltObjectRole.Military && builtObject.Mission != null)
          {
            if (builtObject.Mission.TargetBuiltObject != null && builtObject.Mission.TargetBuiltObject.Empire == infoPanel._Game.PlayerEmpire)
              flag2 = true;
            if (builtObject.Mission.TargetHabitat != null && builtObject.Mission.TargetHabitat.Empire == infoPanel._Game.PlayerEmpire)
              flag2 = true;
            if (builtObject.Mission.SecondaryTargetBuiltObject != null && builtObject.Mission.SecondaryTargetBuiltObject.Empire == infoPanel._Game.PlayerEmpire)
              flag2 = true;
            if (builtObject.Mission.SecondaryTargetHabitat != null && builtObject.Mission.SecondaryTargetHabitat.Empire == infoPanel._Game.PlayerEmpire)
              flag2 = true;
          }
          string text1 = "(" + TextResolver.GetText("Unknown mission") + ")";
          if (infoPanel._ActualEmpire == infoPanel._Game.PlayerEmpire || infoPanel._Game.GodMode || flag1 | flag2)
          {
            Empire empire = infoPanel._ActualEmpire ?? infoPanel._Galaxy.IndependentEmpire;
            string text2 = builtObject.Mission == null || builtObject.Mission.Type == 0 ? "(" + TextResolver.GetText("No mission") + ")" : Galaxy.ResolveDescription(empire, builtObject.Mission);
            if (builtObject.Role == BuiltObjectRole.Military)
              text2 = (double) builtObject.AttackRangeSquared != 0.0 ? ((double) builtObject.AttackRangeSquared != 4000000.0 ? ((double) builtObject.AttackRangeSquared != 2304000000.0 ? text2 + " (" + TextResolver.GetText("Engage detected targets") + ")" : text2 + " (" + TextResolver.GetText("Engage system targets") + ")") : text2 + " (" + TextResolver.GetText("Engage nearby targets") + ")") : text2 + " (" + TextResolver.GetText("Engage when attacked") + ")";
            if (builtObject.SubsequentMissions.Count > 0)
            {
              if (infoPanel._ShowExtendedInfo)
              {
                for (int index = 0; index < builtObject.SubsequentMissions.Count; ++index)
                  text2 = text2 + "\n" + TextResolver.GetText("Next").ToUpper(CultureInfo.InvariantCulture) + ": " + Galaxy.ResolveDescription(empire, builtObject.SubsequentMissions[index]);
              }
              else
                text2 = text2 + " (" + builtObject.SubsequentMissions.Count.ToString() + " " + TextResolver.GetText("queued") + ")";
              SizeF size = graphics.MeasureString(text2, infoPanel._NormalFont, num1, StringFormat.GenericDefault);
              BaconInfoPanel.DrawStringWithDropShadowBounded(infoPanel, graphics, text2, infoPanel._NormalFont, point4, size, infoPanel._WhiteBrush);
              y4 += (int) size.Height;
            }
            else
            {
              BaconInfoPanel.DrawStringWithDropShadow(infoPanel, graphics, text2, infoPanel._NormalFont, point4, infoPanel._WhiteBrush);
              y4 += rowHeight;
            }
            if (infoPanel._SmugglingMission != null)
            {
              point4 = new Point(x1, y4);
              string empty = string.Empty;
              string text3 = infoPanel._SmugglingMission.ResourceId != byte.MaxValue ? "(" + string.Format(TextResolver.GetText("Smuggling Mission Description"), (object) new Resource(infoPanel._SmugglingMission.ResourceId).Name, (object) infoPanel._SmugglingMission.Target.Name) + ")" : "(" + string.Format(TextResolver.GetText("Smuggling Mission Description All Resources"), (object) infoPanel._SmugglingMission.Target.Name) + ")";
              BaconInfoPanel.DrawStringWithDropShadow(infoPanel, graphics, text3, infoPanel._NormalFont, point4, infoPanel._WhiteBrush);
              y4 += rowHeight;
            }
          }
          else
          {
            BaconInfoPanel.DrawStringWithDropShadow(infoPanel, graphics, text1, infoPanel._NormalFont, point4, solidBrush);
            y4 += rowHeight;
          }
        }
        int y5 = y4 + infoPanel._Height5;
        if (builtObject.Empire != null && builtObject.Role == BuiltObjectRole.Base && (builtObject.ResearchEnergy > 0 || builtObject.ResearchHighTech > 0 || builtObject.ResearchWeapons > 0) && ((builtObject.Empire == infoPanel._Game.PlayerEmpire ? 1 : (infoPanel._Game.GodMode ? 1 : 0)) | (flag1 ? 1 : 0)) != 0)
        {
          CharacterList characterList = new CharacterList();
          if (builtObject.Characters != null)
            characterList = builtObject.Characters.GetCharactersByRole(CharacterRole.Scientist);
          if (characterList.Count > 0)
          {
            int x3 = num1 - 35;
            for (int index = 0; index < characterList.Count; ++index)
            {
              point4 = new Point(x3, y5 - 8);
              Bitmap characterImageSmall = InfoPanel._CharacterImageCache.ObtainCharacterImageSmall(characterList[index]);
              graphics.DrawImageUnscaled((Image) characterImageSmall, point4);
              string message = characterList[index].Name + " (" + Galaxy.ResolveDescription(characterList[index].Role) + ")   (" + TextResolver.GetText("click for details") + ")";
              infoPanel.AddHotspot(new Rectangle(point4.X, point4.Y, characterImageSmall.Width, characterImageSmall.Height), (object) characterList[index], message);
              x3 -= 38;
            }
          }
        }
        int num3 = width1 + 5;
        int num4 = y5;
        Rectangle rect1 = new Rectangle();
        ref Rectangle local = ref rect1;
        int x4 = x1 - 2;
        int y6 = num4;
        int width4 = x1 + num3 - 1;
        clientRectangle = infoPanel.ClientRectangle;
        int height = clientRectangle.Height - (num4 + 2);
        local = new Rectangle(x4, y6, width4, height);
        graphics.FillRectangle((Brush) infoPanel._LabelAreaBrush, rect1);
        int num5 = num3 - 5;
        point4 = new Point(x1, y5);
        string str1 = infoPanel._Galaxy.DisplayBuiltObjectSubRole(builtObject.Role, builtObject.SubRole);
        string description1 = builtObject.Empire != infoPanel._Galaxy.IndependentEmpire || infoPanel._ActualEmpire == builtObject.Empire || builtObject.PirateEmpireId <= (byte) 0 ? (builtObject.PirateEmpireId <= (byte) 0 || builtObject.Role != BuiltObjectRole.Freight || builtObject.Empire == infoPanel._Galaxy.IndependentEmpire ? (builtObject.Owner != null ? str1 + " (" + TextResolver.GetText("STATE") + ")" : str1 + " (" + TextResolver.GetText("PRIVATE") + ")") : str1 + " (" + TextResolver.GetText("Smuggler").ToUpper(CultureInfo.InvariantCulture) + ")") : str1 + " (" + TextResolver.GetText("Smuggler").ToUpper(CultureInfo.InvariantCulture) + ")";
        infoPanel.DrawLabelledDescription(graphics, TextResolver.GetText("Type"), num5, description1, point4);
        int y7 = y5 + rowHeight;
        point4 = new Point(x1, y7);
        string description2 = "(" + TextResolver.GetText("Abandoned") + ")";
        if (infoPanel._ActualEmpire != null)
          description2 = infoPanel._ActualEmpire.Name;
        if (infoPanel._ActualEmpire == infoPanel._Galaxy.IndependentEmpire)
          description2 = "(" + TextResolver.GetText("Independent") + ")";
        infoPanel.DrawLabelledDescription(graphics, TextResolver.GetText("Empire"), num5, description2, point4);
        int y8 = y7 + rowHeight;
        if (builtObject.Role != BuiltObjectRole.Base)
        {
          int num6 = 10 + num5 + 2;
          point4 = new Point(x1, y8);
          if (builtObject.ShipGroup != null)
          {
            string text = "(" + TextResolver.GetText("None") + ")";
            bool flag3 = false;
            if (builtObject.ShipGroup != null)
            {
              text = builtObject.ShipGroup.Name;
              if (builtObject.ShipGroup.LeadShip == builtObject)
                flag3 = true;
            }
            infoPanel.DrawLabel(graphics, TextResolver.GetText("Fleet"), num5, point4);
            if (flag3)
            {
              point4 = new Point(x1 + num6, y8);
              graphics.DrawImageUnscaled((Image) InfoPanel._ShipGroupLeadShipImage, point4);
              num6 = 10 + num5 + 2 + InfoPanel._ShipGroupLeadShipImage.Width + 2;
            }
            if (builtObject.Empire == infoPanel._Game.PlayerEmpire && builtObject.ShipGroup != null)
            {
              SizeF sizeF2 = graphics.MeasureString(text, infoPanel._NormalFont, infoPanel._MaxGraphTextWidth, StringFormat.GenericTypographic);
              infoPanel.AddHotspot(new Rectangle(x1 + num6, y8, (int) sizeF2.Width + 7, (int) sizeF2.Height + 1), (object) builtObject.ShipGroup, builtObject.ShipGroup.Name + " (" + TextResolver.GetText("click to select Fleet") + ")");
            }
            point4 = new Point(x1 + num6, y8);
            BaconInfoPanel.DrawStringWithDropShadow(infoPanel, graphics, text, infoPanel._NormalFont, point4, infoPanel._WhiteBrush);
            y8 += rowHeight;
          }
          else if (builtObject.ActualEmpire != null && builtObject.ActualEmpire == infoPanel._Game.PlayerEmpire && (double) builtObject.CurrentSpeed >= (double) Math.Max(builtObject.CruiseSpeed, (short) 1))
          {
            infoPanel.DrawLabel(graphics, "WPT ETA", num5, point4);
            int num7 = 10 + num5 + 2;
            string text = Math.Truncate(Math.Sqrt(BaconBuiltObject.FindRangeSquaredToTarget(builtObject)) / (double) builtObject.CurrentSpeed).ToString();
            graphics.MeasureString(text, infoPanel._NormalFont, infoPanel._MaxGraphTextWidth, StringFormat.GenericTypographic);
            point4 = new Point(x1 + num7, y8);
            BaconInfoPanel.DrawStringWithDropShadow(infoPanel, graphics, text, infoPanel._NormalFont, point4, infoPanel._WhiteBrush);
            y8 += rowHeight;
          }
        }
        point4 = new Point(x1, y8);
        string str2 = builtObject.Design.Name + " (" + TextResolver.GetText("Size").ToLower(CultureInfo.InvariantCulture) + " " + builtObject.Size.ToString() + ")";
        infoPanel.DrawLabelledDescription(graphics, TextResolver.GetText("Design"), num5, str2, point4);
        if (infoPanel._ActualEmpire == infoPanel._Game.PlayerEmpire)
        {
          SizeF sizeF3 = graphics.MeasureString(str2, infoPanel._NormalFont, infoPanel._MaxGraphTextWidth, StringFormat.GenericTypographic);
          infoPanel.AddHotspot(new Rectangle(x1 + num5 + 10, point4.Y, (int) sizeF3.Width + 7, (int) sizeF3.Height + 1), (object) builtObject.Design, builtObject.Design.Name + " (" + TextResolver.GetText("click for design details") + ")");
        }
        int y9 = y8 + rowHeight;
        point4 = new Point(x1 + num5 + 10, y9);
        string text4 = "(" + TextResolver.GetText("All components normal") + ")";
        if (infoPanel._ActualEmpire == infoPanel._Game.PlayerEmpire || infoPanel._ActualEmpire == null || infoPanel._Game.GodMode | flag1)
        {
          int damagedComponentCount = builtObject.DamagedComponentCount;
          int unbuiltComponentCount = builtObject.UnbuiltComponentCount;
          int num8 = 0;
          if (builtObject.DisabledComponentIndexes != null)
            num8 = builtObject.DisabledComponentIndexes.Count;
          if (damagedComponentCount > 0 || unbuiltComponentCount > 0 || num8 > 0)
          {
            string str3 = TextResolver.GetText("Components") + ": ";
            if (damagedComponentCount > 0)
              str3 = str3 + damagedComponentCount.ToString() + " " + TextResolver.GetText("damaged") + ", ";
            if (num8 > 0)
              str3 = str3 + num8.ToString() + " " + TextResolver.GetText("disabled") + ", ";
            if (unbuiltComponentCount > 0)
              str3 = str3 + unbuiltComponentCount.ToString() + " " + TextResolver.GetText("unbuilt") + ", ";
            text4 = str3.Substring(0, str3.Length - 2);
          }
          else if (builtObject.RetrofitDesign != null)
            text4 = "(" + string.Format(TextResolver.GetText("Retrofitting to DESIGN"), (object) builtObject.RetrofitDesign.Name) + ")";
          BaconInfoPanel.DrawStringWithDropShadow(infoPanel, graphics, text4, infoPanel._NormalFont, point4, infoPanel._WhiteBrush);
        }
        else
        {
          string text5 = "(" + TextResolver.GetText("Unknown component status") + ")";
          BaconInfoPanel.DrawStringWithDropShadow(infoPanel, graphics, text5, infoPanel._NormalFont, point4, solidBrush);
        }
        int y10 = y9 + rowHeight;
        if (builtObject.SubRole == BuiltObjectSubRole.ResupplyShip)
        {
          if (builtObject.IsDeployed)
          {
            point4 = new Point(x1, y10);
            string upper = TextResolver.GetText("Deployed").ToUpper(CultureInfo.InvariantCulture);
            infoPanel.DrawBarGraph(TextResolver.GetText("Status"), num5, 30, 30, rowHeight - 2, num1, Color.FromArgb(150, 48, 0, 96), Color.FromArgb(150, 128, 0, (int) byte.MaxValue), backgroundColor, graphics, point4, upper);
            y10 += rowHeight;
          }
          else
          {
            point4 = new Point(x1, y10);
            string empty = string.Empty;
            int maximumValue = 30;
            if (builtObject.DeployProgress > 0.0)
            {
              string text6 = TextResolver.GetText("Deploying");
              int currentValue = (int) (builtObject.DeployProgress * (double) maximumValue);
              string[] strArray = new string[6]
              {
                text6,
                " (",
                null,
                null,
                null,
                null
              };
              int num9 = maximumValue - currentValue;
              strArray[2] = num9.ToString();
              strArray[3] = " ";
              strArray[4] = TextResolver.GetText("seconds abbreviation");
              strArray[5] = ")";
              string suffixData = string.Concat(strArray);
              infoPanel.DrawBarGraph(TextResolver.GetText("Status"), num5, maximumValue, currentValue, rowHeight - 2, num1, Color.FromArgb(150, 48, 0, 96), Color.FromArgb(150, 128, 0, (int) byte.MaxValue), backgroundColor, graphics, point4, suffixData);
            }
            else if (builtObject.DeployProgress < 0.0)
            {
              string text7 = TextResolver.GetText("Undeploying");
              int num10 = (int) (Math.Abs(builtObject.DeployProgress) * (double) maximumValue);
              int currentValue = maximumValue - num10;
              string suffixData = text7 + " (" + (maximumValue - num10).ToString() + " " + TextResolver.GetText("seconds abbreviation") + ")";
              infoPanel.DrawBarGraph(TextResolver.GetText("Status"), num5, maximumValue, currentValue, rowHeight - 2, num1, Color.FromArgb(150, 48, 0, 96), Color.FromArgb(150, 128, 0, (int) byte.MaxValue), backgroundColor, graphics, point4, suffixData);
            }
            else
            {
              string suffixData = "(" + TextResolver.GetText("Not Deployed") + ")";
              infoPanel.DrawBarGraph(TextResolver.GetText("Status"), num5, 30, 0, rowHeight - 2, num1, Color.FromArgb(150, 48, 0, 96), Color.FromArgb(150, 128, 0, (int) byte.MaxValue), backgroundColor, graphics, point4, suffixData);
            }
            y10 += rowHeight;
          }
        }
        if (builtObject.SubRole == BuiltObjectSubRole.ColonyShip)
        {
          point4 = new Point(x1, y10);
          if (((infoPanel._ActualEmpire == infoPanel._Game.PlayerEmpire ? 1 : (infoPanel._Game.GodMode ? 1 : 0)) | (flag1 ? 1 : 0)) != 0)
          {
            point4 = new Point(x1, y10);
            string description3 = "(" + TextResolver.GetText("None") + ")";
            if (builtObject.NativeRace != null)
              description3 = builtObject.NativeRace.Name;
            infoPanel.DrawLabelledDescription(graphics, TextResolver.GetText("Race"), num5, description3, point4);
            int y11 = y10 + rowHeight;
            List<HabitatType> habitatTypeList1 = new List<HabitatType>();
            List<HabitatType> habitatTypeList2 = builtObject.Empire == null ? infoPanel._Galaxy.PlayerEmpire.ColonizableHabitatTypesForBuiltObject(builtObject) : builtObject.Empire.ColonizableHabitatTypesForBuiltObjectAndEmpire(builtObject);
            string text8 = "";
            foreach (HabitatType type in habitatTypeList2)
              text8 = text8 + Galaxy.ResolveDescription(type) + ", ";
            if (text8.Length > 0)
              text8 = text8.Substring(0, text8.Length - 2);
            point4 = new Point(x1, y11);
            infoPanel.DrawLabel(graphics, TextResolver.GetText("Colonize"), num5, point4);
            int width5 = num1 - num5;
            SizeF size = graphics.MeasureString(text8, infoPanel._NormalFont, width5, StringFormat.GenericDefault);
            point4 = new Point(x1 + num5 + 10, y11);
            BaconInfoPanel.DrawStringWithDropShadowBounded(infoPanel, graphics, text8, infoPanel._NormalFont, point4, size, infoPanel._WhiteBrush);
            y10 = y11 + (int) size.Height;
          }
          else
          {
            point4 = new Point(x1, y10);
            infoPanel.DrawLabelledDescription(graphics, TextResolver.GetText("Race"), num5, "(" + TextResolver.GetText("Unknown") + ")", point4, solidBrush);
            int y12 = y10 + rowHeight;
            point4 = new Point(x1, y12);
            infoPanel.DrawLabelledDescription(graphics, TextResolver.GetText("Colonize"), num5, "(" + TextResolver.GetText("Unknown") + ")", point4, solidBrush);
            y10 = y12 + rowHeight;
          }
        }
        if (builtObject.PopulationCapacity > 0)
        {
          point4 = new Point(x1, y10);
          if (((infoPanel._ActualEmpire == infoPanel._Game.PlayerEmpire ? 1 : (infoPanel._Game.GodMode ? 1 : 0)) | (flag1 ? 1 : 0)) != 0)
          {
            string description4 = "(" + TextResolver.GetText("No passengers") + ")";
            if (builtObject.Population.TotalAmount > 0L)
            {
              string str4 = builtObject.Population.TotalAmount.ToString("0,K") + " (";
              for (int index = 0; index < builtObject.Population.Count; ++index)
              {
                Population population = builtObject.Population[index];
                str4 = str4 + population.Race.Name + ", ";
              }
              description4 = str4.Substring(0, str4.Length - 2) + ")";
            }
            infoPanel.DrawLabelledDescription(graphics, TextResolver.GetText("Onboard"), num5, description4, point4);
          }
          else
            infoPanel.DrawLabelledDescription(graphics, TextResolver.GetText("Onboard"), num5, "(" + TextResolver.GetText("Unknown passengers") + ")", point4, solidBrush);
          y10 += rowHeight;
        }
        if (builtObject.SubRole != BuiltObjectSubRole.ResupplyShip)
          y10 += infoPanel._Height5;
        point4 = new Point(x1, y10);
        if (((infoPanel._ActualEmpire == infoPanel._Game.PlayerEmpire ? 1 : (infoPanel._Game.GodMode ? 1 : 0)) | (flag1 ? 1 : 0)) != 0)
        {
          int currentValue = Math.Max(0, (int) builtObject.CurrentFuel);
          string suffixData = string.Empty;
          if (builtObject.CurrentFuel <= 0.0 && builtObject.Role != BuiltObjectRole.Base && builtObject.UnbuiltComponentCount == 0)
            suffixData = " (" + TextResolver.GetText("speed reduced") + ")";
          Color fillColorStart = Color.FromArgb(150, 48, 0, 96);
          Color fillColorEnd = Color.FromArgb(150, 128, 0, (int) byte.MaxValue);
          if (builtObject.CurrentFuel < (double) (builtObject.FuelCapacity / 3))
          {
            fillColorStart = Color.FromArgb(150, 128, 0, 40);
            fillColorEnd = Color.FromArgb(150, (int) byte.MaxValue, 0, 96);
          }
          infoPanel.DrawBarGraph(TextResolver.GetText("Fuel"), num5, builtObject.FuelCapacity, currentValue, rowHeight - 2, num1, fillColorStart, fillColorEnd, backgroundColor, graphics, point4, suffixData);
        }
        else
          infoPanel.DrawLabelledDescription(graphics, TextResolver.GetText("Fuel"), num5, "(" + TextResolver.GetText("Unknown") + ")", point4, solidBrush);
        int y13 = y10 + rowHeight;
        point4 = new Point(x1, y13);
        if (((infoPanel._ActualEmpire == infoPanel._Game.PlayerEmpire ? 1 : (infoPanel._Game.GodMode ? 1 : 0)) | (flag1 ? 1 : 0)) != 0)
        {
          int currentValue = Math.Max(0, (int) builtObject.CurrentEnergy);
          infoPanel.DrawBarGraph(TextResolver.GetText("Energy"), num5, builtObject.ReactorStorageCapacity, currentValue, rowHeight - 2, num1, Color.FromArgb(150, 48, 0, 96), Color.FromArgb(150, 128, 0, (int) byte.MaxValue), backgroundColor, graphics, point4);
        }
        else
          infoPanel.DrawLabelledDescription(graphics, TextResolver.GetText("Energy"), num5, "(" + TextResolver.GetText("Unknown") + ")", point4, solidBrush);
        int y14 = y13 + rowHeight;
        point4 = new Point(x1, y14);
        string suffixData1 = string.Empty;
        if (builtObject.ShieldsReducedLocation)
          suffixData1 = " (" + TextResolver.GetText("reducing") + ")";
        infoPanel.DrawBarGraph(TextResolver.GetText("Shields"), num5, builtObject.ShieldsCapacity, (int) builtObject.CurrentShields, rowHeight - 2, num1, Color.FromArgb(150, 48, 0, 96), Color.FromArgb(150, 128, 0, (int) byte.MaxValue), backgroundColor, graphics, point4, suffixData1);
        int y15 = y14 + rowHeight;
        if (builtObject.Role != BuiltObjectRole.Base)
        {
          point4 = new Point(x1, y15);
          string suffixData2 = string.Empty;
          if (builtObject.MovementSlowedLocation && (double) builtObject.CurrentSpeed < (double) builtObject.WarpSpeed)
            suffixData2 = suffixData2 + " (" + TextResolver.GetText("slowed") + ")";
          if (builtObject.HyperjumpDisabledLocation)
            suffixData2 = suffixData2 + " (" + TextResolver.GetText("Hyper block") + ")";
          if (builtObject.WarpSpeed <= 0)
            suffixData2 = " (" + TextResolver.GetText("No Hyperdrive") + ")";
          if (builtObject.HyperjumpPrepare)
            infoPanel.DrawBarGraph(TextResolver.GetText("Speed Abbreviation"), num5, (int) builtObject.TopSpeed, (int) builtObject.CurrentSpeed, rowHeight - 2, num1, Color.FromArgb(150, 48, 0, 96), Color.FromArgb(150, 128, 0, (int) byte.MaxValue), Color.FromArgb(150, 144, 80, 80), Color.FromArgb(150, (int) byte.MaxValue, 160, 160), backgroundColor, graphics, point4, suffixData2);
          else
            infoPanel.DrawBarGraph(TextResolver.GetText("Speed Abbreviation"), num5, (int) builtObject.TopSpeed, (int) builtObject.CurrentSpeed, rowHeight - 2, num1, Color.FromArgb(150, 48, 0, 96), Color.FromArgb(150, 128, 0, (int) byte.MaxValue), backgroundColor, graphics, point4, suffixData2);
          y15 += rowHeight;
        }
        int y16 = y15 + infoPanel._Height5;
        if (builtObject.AssaultAttackValue > (short) 0)
        {
          point4 = new Point(x1, y16);
          string description5 = string.Format(TextResolver.GetText("Boarding Description Selection Panel"), (object) builtObject.AssaultAttackValue.ToString("0"), (object) builtObject.AssaultDefenseValue.ToString("0"));
          infoPanel.DrawLabelledDescription(graphics, TextResolver.GetText("Boarding").ToUpper(CultureInfo.InvariantCulture), num5, description5, point4);
          y16 += rowHeight;
        }
        if (((infoPanel._ActualEmpire == infoPanel._Game.PlayerEmpire ? 1 : (infoPanel._Game.GodMode ? 1 : 0)) | (flag1 ? 1 : 0)) != 0)
        {
          if (builtObject.TroopCapacity > 0 || builtObject.Characters.Count > 0)
          {
            point4 = new Point(x1, y16);
            string prefix = string.Format(TextResolver.GetText("Troop UNITS CAPACITY"), (object) (builtObject.TroopCapacity - builtObject.TroopCapacityRemaining).ToString("0"), (object) builtObject.TroopCapacity.ToString("0"));
            infoPanel.DrawTroopsAgents(num5, builtObject.Troops, (TroopList) null, (TroopList) null, builtObject.Characters, (CharacterList) null, graphics, point4, num1, 0, prefix);
            y16 += rowHeight;
          }
        }
        else
        {
          point4 = new Point(x1, y16);
          infoPanel.DrawLabelledDescription(graphics, TextResolver.GetText("Troops"), num5, "(" + TextResolver.GetText("Unknown") + ")", point4, solidBrush);
          y16 += rowHeight;
        }
        if (builtObject.IsPlanetDestroyer)
        {
          Weapon weapon1 = (Weapon) null;
          for (int index = 0; index < builtObject.Weapons.Count; ++index)
          {
            Weapon weapon2 = builtObject.Weapons[index];
            if (weapon2.IsPlanetDestroyer)
            {
              weapon1 = weapon2;
              break;
            }
          }
          if (weapon1 != null)
          {
            point4 = new Point(x1, y16);
            int maximumValue = weapon1.FireRate / 1000;
            dateTime = infoPanel._Galaxy.CurrentDateTime;
            int currentValue = (int) (Math.Min(1000000.0, dateTime.Subtract(weapon1.LastFired).TotalMilliseconds) / 1000.0);
            Color fillColorStart = Color.FromArgb(150, 48, 0, 96);
            Color fillColorEnd = Color.FromArgb(150, 128, 0, (int) byte.MaxValue);
            string suffixData3 = TextResolver.GetText("Ready to Fire");
            if (currentValue < maximumValue)
            {
              int num11 = maximumValue - currentValue;
              suffixData3 = TextResolver.GetText("Charging") + " (" + num11.ToString() + " " + TextResolver.GetText("seconds abbreviation") + ")";
              fillColorStart = Color.FromArgb(150, 144, 80, 80);
              fillColorEnd = Color.FromArgb(150, (int) byte.MaxValue, 160, 160);
            }
            infoPanel.DrawBarGraph(TextResolver.GetText("Super Laser"), num5, maximumValue, currentValue, rowHeight - 2, num1, fillColorStart, fillColorEnd, backgroundColor, graphics, point4, suffixData3);
            y16 += rowHeight;
          }
        }
        point4 = new Point(x1, y16);
        string description6 = TextResolver.GetText("Firepower") + ": " + builtObject.FirepowerRaw.ToString() + ", " + TextResolver.GetText("Range") + ": " + builtObject.MaximumWeaponsRange.ToString();
        if (builtObject.FirepowerRaw == 0)
          description6 = "(" + TextResolver.GetText("None") + ")";
        infoPanel.DrawLabelledDescription(graphics, TextResolver.GetText("Weapons"), num5, description6, point4);
        int y17 = y16 + rowHeight;
        if (builtObject.AssaultRange > (short) 0 && builtObject.AssaultStrength > (short) 0)
        {
          point4 = new Point(x1, y17);
          int assaultPodCount = 0;
          int assaultPodsAvailable = 0;
          int assaultPodAttackValues = builtObject.CalculateAssaultPodAttackValues(infoPanel._Galaxy.CurrentDateTime, out assaultPodCount, out assaultPodsAvailable);
          string description7 = TextResolver.GetText("Strength") + ": " + assaultPodAttackValues.ToString("0") + "   (" + assaultPodsAvailable.ToString("0") + "/" + assaultPodCount.ToString("0") + " " + TextResolver.GetText("pods") + ")";
          infoPanel.DrawLabelledDescription(graphics, TextResolver.GetText("Boarding"), num5, description7, point4);
          y17 += rowHeight;
        }
        if (((infoPanel._ActualEmpire == infoPanel._Game.PlayerEmpire ? 1 : (infoPanel._Game.GodMode ? 1 : 0)) | (flag1 ? 1 : 0)) != 0)
        {
          if (builtObject.FighterCapacity > 0 || builtObject.Fighters != null && builtObject.Fighters.Count > 0)
          {
            point4 = new Point(x1, y17);
            infoPanel.DrawFighters(num5, builtObject.Fighters, graphics, point4, num1);
            y17 += rowHeight;
          }
        }
        else
        {
          point4 = new Point(x1, y17);
          infoPanel.DrawLabelledDescription(graphics, TextResolver.GetText("Fighters"), num5, "(" + TextResolver.GetText("Unknown") + ")", point4, solidBrush);
          y17 += rowHeight;
        }
        if (builtObject.ConstructionQueue != null && builtObject.ConstructionQueue.ConstructionYards.Count > 0)
        {
          point4 = new Point(x1, y17);
          bool flag4 = false;
          BuiltObjectList builtObjects = new BuiltObjectList();
          for (int index = 0; index < builtObject.ConstructionQueue.ConstructionYards.Count; ++index)
          {
            ConstructionYard constructionYard = builtObject.ConstructionQueue.ConstructionYards[index];
            if (constructionYard.ShipUnderConstruction != null)
            {
              builtObjects.Add(constructionYard.ShipUnderConstruction);
              if ((double) constructionYard.BuildSpeedModifier > 1.0 && !constructionYard.ShipUnderConstruction.Scrap)
                flag4 = true;
            }
          }
          string suffix = string.Empty;
          if (flag4)
            suffix = "(" + TextResolver.GetText("Advanced Tech Bonus") + ")";
          infoPanel.DrawBuiltObjectList(num5, TextResolver.GetText("Building"), builtObjects, builtObject.ConstructionQueue.ConstructionWaitQueue.Count, graphics, point4, num1, suffix);
          y17 += rowHeight;
        }
        if (builtObject.DockingBays != null && builtObject.DockingBays.Count > 0)
        {
          point4 = new Point(x1, y17);
          BuiltObjectList builtObjects = new BuiltObjectList();
          for (int index = 0; index < builtObject.DockingBays.Count; ++index)
          {
            DockingBay dockingBay = builtObject.DockingBays[index];
            if (dockingBay.DockedShip != null)
              builtObjects.Add(dockingBay.DockedShip);
          }
          if (builtObject.DockingBayWaitQueue != null)
            infoPanel.DrawBuiltObjectList(num5, TextResolver.GetText("Docked"), builtObjects, builtObject.DockingBayWaitQueue.Count, graphics, point4, num1);
          else
            infoPanel.DrawBuiltObjectList(num5, TextResolver.GetText("Docked"), builtObjects, 0, graphics, point4, num1);
          y17 += rowHeight;
        }
        if (infoPanel._ShowExtendedInfo && !string.IsNullOrEmpty(infoPanel._CharacterBonuses))
        {
          point4 = new Point(x1, y17);
          infoPanel.DrawLabel(graphics, TextResolver.GetText("Bonuses"), num5, point4);
          int width6 = num1 - num5;
          SizeF size = graphics.MeasureString(infoPanel._CharacterBonuses, infoPanel._NormalFont, width6, StringFormat.GenericDefault);
          point4 = new Point(x1 + num5 + 10, y17 + 1);
          BaconInfoPanel.DrawStringWithDropShadowBounded(infoPanel, graphics, infoPanel._CharacterBonuses, infoPanel._NormalFont, point4, size, infoPanel._WhiteBrush);
          int num12 = y17 + (int) size.Height;
        }
        solidBrush.Dispose();
      }
    }

    public static void DrawShipGroup(InfoPanel infoPanel, ShipGroup shipGroup, Graphics graphics)
    {
      SolidBrush solidBrush = new SolidBrush(infoPanel._UnknownColor);
      graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
      graphics.CompositingQuality = CompositingQuality.HighQuality;
      graphics.InterpolationMode = InterpolationMode.Bilinear;
      bool flag1 = false;
      if (shipGroup.Empire != infoPanel._Game.PlayerEmpire && infoPanel._Game.PlayerEmpire.EmpiresViewable.Contains(shipGroup.Empire))
        flag1 = true;
      int y1 = 6;
      int rowHeight = infoPanel._RowHeight;
      int x1 = 5;
      int width1 = infoPanel.ClientRectangle.Width - 10;
      Size flagSizeSmall = infoPanel._FlagSizeSmall;
      Point point1 = new Point(width1 - (flagSizeSmall.Width - 2), 6);
      if (infoPanel._EmpirePicture != null)
      {
        graphics.DrawImageUnscaled((Image) infoPanel._EmpirePicture, point1);
        if (shipGroup.Empire != null)
          infoPanel.AddHotspot(new Rectangle(point1, infoPanel._EmpirePicture.Size), (object) shipGroup.Empire, shipGroup.Empire.Name + " (" + TextResolver.GetText("click for details") + ")");
      }
      Font titleFont = infoPanel._TitleFont;
      int width2 = (int) graphics.MeasureString(TextResolver.GetText("Lead Ship"), infoPanel._NormalFontBold, infoPanel._MaxGraphTextWidth, StringFormat.GenericTypographic).Width;
      int num1 = width1 - width2;
      Point point2 = new Point(x1, y1);
      using (SolidBrush brush = new SolidBrush(infoPanel._EmpireColor))
      {
        BaconInfoPanel.DrawStringWithDropShadow(infoPanel, graphics, shipGroup.Name, titleFont, point2, brush);
        if (shipGroup.LeadShip != null)
        {
          SizeF sizeF = graphics.MeasureString(shipGroup.Name, titleFont, 300);
          point2 = new Point(x1 + (int) sizeF.Width + 2, y1 + 2);
          string text = "(" + shipGroup.LeadShip.Name + ")";
          BaconInfoPanel.DrawStringWithDropShadow(infoPanel, graphics, text, infoPanel._NormalFont, point2, brush);
        }
      }
      int y2 = y1 + (titleFont.Height + infoPanel._Height2);
      point2 = new Point(x1, y2);
      string text1 = "(" + TextResolver.GetText("Unknown mission") + ")";
      int y3;
      if (((shipGroup.Empire == infoPanel._Game.PlayerEmpire ? 1 : (infoPanel._Game.GodMode ? 1 : 0)) | (flag1 ? 1 : 0)) != 0)
      {
        string str = shipGroup.Mission == null || shipGroup.Mission.Type == 0 ? "(" + TextResolver.GetText("No mission") + ")" : Galaxy.ResolveDescription(shipGroup.Empire, shipGroup.Mission);
        string text2 = (double) shipGroup.AttackRangeSquared != 0.0 ? ((double) shipGroup.AttackRangeSquared != 4000000.0 ? ((double) shipGroup.AttackRangeSquared != 2304000000.0 ? str + " (" + TextResolver.GetText("Engage detected targets") + ")" : str + " (" + TextResolver.GetText("Engage system targets") + ")") : str + " (" + TextResolver.GetText("Engage nearby targets") + ")") : str + " (" + TextResolver.GetText("Engage when attacked") + ")";
        if (shipGroup.SubsequentMissions.Count > 0)
        {
          if (infoPanel._ShowExtendedInfo)
          {
            for (int index = 0; index < shipGroup.SubsequentMissions.Count; ++index)
              text2 = text2 + "\n" + TextResolver.GetText("Next").ToUpper(CultureInfo.InvariantCulture) + ": " + Galaxy.ResolveDescription(shipGroup.Empire, shipGroup.SubsequentMissions[index]);
          }
          else
            text2 = text2 + " (" + shipGroup.SubsequentMissions.Count.ToString() + " " + TextResolver.GetText("queued") + ")";
          SizeF size = graphics.MeasureString(text2, infoPanel._NormalFont, width1, StringFormat.GenericTypographic);
          BaconInfoPanel.DrawStringWithDropShadowBounded(infoPanel, graphics, text2, infoPanel._NormalFont, point2, size, infoPanel._WhiteBrush);
          y3 = y2 + (int) size.Height;
        }
        else
        {
          BaconInfoPanel.DrawStringWithDropShadow(infoPanel, graphics, text2, infoPanel._NormalFont, point2);
          y3 = y2 + rowHeight;
        }
      }
      else
      {
        BaconInfoPanel.DrawStringWithDropShadow(infoPanel, graphics, text1, infoPanel._NormalFont, point2, solidBrush);
        y3 = y2 + rowHeight;
      }
      if (shipGroup.LocalDefenseTacticsApply)
      {
        point2 = new Point(x1, y3);
        BaconInfoPanel.DrawStringColorWithDropShadow(infoPanel, graphics, string.Format(TextResolver.GetText("Local Defense Tactics Description"), (object) "+20%"), infoPanel._NormalFont, point2, Color.FromArgb(0, (int) byte.MaxValue, 0));
        y3 += rowHeight;
      }
      int y4 = y3 + infoPanel._Height4;
      int num2 = width2 + 5;
      int num3 = y4;
      Rectangle rect1 = new Rectangle();
      ref Rectangle local = ref rect1;
      int x2 = x1 - 2;
      int y5 = num3;
      int width3 = x1 + num2 - 1;
      Rectangle clientRectangle = infoPanel.ClientRectangle;
      int height1 = clientRectangle.Height - (num3 + 2);
      local = new Rectangle(x2, y5, width3, height1);
      graphics.FillRectangle((Brush) infoPanel._LabelAreaBrush, rect1);
      int labelWidth = num2 - 5;
      point2 = new Point(x1, y4);
      infoPanel.DrawLabelledDescription(graphics, TextResolver.GetText("Empire"), labelWidth, shipGroup.Empire.Name, point2);
      if (((shipGroup.Empire == infoPanel._Game.PlayerEmpire ? 1 : (infoPanel._Game.GodMode ? 1 : 0)) | (flag1 ? 1 : 0)) != 0)
      {
        CharacterList characterList = new CharacterList();
        if (shipGroup.Empire != null && shipGroup.Empire.Characters != null)
          characterList = shipGroup.Empire.Characters.GetFleetAdmiralsAndGenerals(shipGroup);
        if (characterList.Count > 0)
        {
          int x3 = width1 - 35;
          for (int index = 0; index < characterList.Count; ++index)
          {
            Character character = characterList[index];
            if (character != null && character.TransferDestination == null && (double) character.TransferTimeRemaining <= 0.0)
            {
              point2 = new Point(x3, y4 - 8);
              Bitmap characterImageSmall = InfoPanel._CharacterImageCache.ObtainCharacterImageSmall(character);
              graphics.DrawImageUnscaled((Image) characterImageSmall, point2);
              string message = character.Name + " (" + Galaxy.ResolveDescription(character.Role) + ")   (" + TextResolver.GetText("click for details") + ")";
              infoPanel.AddHotspot(new Rectangle(point2.X, point2.Y, characterImageSmall.Width, characterImageSmall.Height), (object) character, message);
              x3 -= 38;
            }
          }
        }
      }
      int y6 = y4 + rowHeight;
      point2 = new Point(x1, y6);
      string empty1 = string.Empty;
      string description1 = shipGroup.GatherPoint != null ? shipGroup.GatherPoint.Name : "(" + TextResolver.GetText("None") + ")";
      infoPanel.DrawLabelledDescription(graphics, TextResolver.GetText("Based At"), labelWidth, description1, point2);
      int y7 = y6 + rowHeight;
      point2 = new Point(x1, y7);
      if (((shipGroup.Empire == infoPanel._Game.PlayerEmpire ? 1 : (infoPanel._Game.GodMode ? 1 : 0)) | (flag1 ? 1 : 0)) != 0)
      {
        string description2 = Galaxy.ResolveDescriptionFleetPosture(shipGroup);
        infoPanel.DrawLabelledDescription(graphics, TextResolver.GetText("Posture"), labelWidth, description2, point2);
      }
      else
        infoPanel.DrawLabelledDescription(graphics, TextResolver.GetText("Posture"), labelWidth, "(" + TextResolver.GetText("Unknown") + ")", point2, solidBrush);
      int y8 = y7 + rowHeight;
      point2 = new Point(x1, y8);
      string empty2 = string.Empty;
      string description3;
      if (((shipGroup.Empire == infoPanel._Game.PlayerEmpire ? 1 : (infoPanel._Game.GodMode ? 1 : 0)) | (flag1 ? 1 : 0)) != 0)
      {
        int num4 = 0;
        for (int index = 0; index < shipGroup.Ships.Count; ++index)
        {
          if (shipGroup.Ships[index].Fighters != null)
            num4 += shipGroup.Ships[index].Fighters.Count;
        }
        string[] strArray = new string[11];
        int num5 = shipGroup.Ships.Count;
        strArray[0] = num5.ToString();
        strArray[1] = " ";
        strArray[2] = TextResolver.GetText("Ships").ToLower(CultureInfo.InvariantCulture);
        strArray[3] = ", ";
        num5 = shipGroup.TotalFirepower;
        strArray[4] = num5.ToString();
        strArray[5] = " ";
        strArray[6] = TextResolver.GetText("Firepower").ToLower(CultureInfo.InvariantCulture);
        strArray[7] = ", ";
        strArray[8] = num4.ToString();
        strArray[9] = " ";
        strArray[10] = TextResolver.GetText("Fighters").ToLower(CultureInfo.InvariantCulture);
        description3 = string.Concat(strArray);
      }
      else
        description3 = shipGroup.Ships.Count.ToString() + " " + TextResolver.GetText("Ships").ToLower(CultureInfo.InvariantCulture);
      infoPanel.DrawLabelledDescription(graphics, TextResolver.GetText("Summary"), labelWidth, description3, point2);
      int y9 = y8 + rowHeight;
      point2 = new Point(x1, y9);
      int num6 = 0;
      int num7 = 0;
      for (int index = 0; index < shipGroup.Ships.Count; ++index)
      {
        BuiltObject ship = shipGroup.Ships[index];
        if (ship.Troops != null)
        {
          num6 += ship.Troops.Count;
          num7 += ship.Troops.TotalAttackStrength;
        }
      }
      string empty3 = string.Empty;
      if (((shipGroup.Empire == infoPanel._Game.PlayerEmpire ? 1 : (infoPanel._Game.GodMode ? 1 : 0)) | (flag1 ? 1 : 0)) != 0)
      {
        int infantryCount = 0;
        int artilleryCount = 0;
        int armorCount = 0;
        int specialForcesCount = 0;
        shipGroup.GetTroopCountsByType(out infantryCount, out artilleryCount, out armorCount, out specialForcesCount);
        string str = Galaxy.ResolveTroopCompositionDescription(infantryCount, artilleryCount, armorCount, specialForcesCount);
        if (string.IsNullOrEmpty(str))
          str = "(" + TextResolver.GetText("No units") + ")";
        int totalTroopCapacity = shipGroup.TotalTroopCapacity;
        int totalTroopSpaceUsed = shipGroup.TotalTroopSpaceUsed;
        string description4 = string.Format(TextResolver.GetText("Fleet Troop Extended Description STRENGTH CAPACITY UNITS"), (object) num7.ToString("0,K"), (object) totalTroopSpaceUsed.ToString(), (object) totalTroopCapacity.ToString(), (object) str);
        infoPanel.DrawLabelledDescription(graphics, TextResolver.GetText("Troops"), labelWidth, description4, point2);
      }
      else
      {
        string description5 = "(" + TextResolver.GetText("Unknown") + ")";
        infoPanel.DrawLabelledDescription(graphics, TextResolver.GetText("Troops"), labelWidth, description5, point2, solidBrush);
      }
      int y10 = y9 + rowHeight;
      int assaultPodCount = 0;
      int assaultPodsAvailable = 0;
      int assaultPodAttackValues = shipGroup.CalculateAssaultPodAttackValues(infoPanel._Galaxy.CurrentDateTime, out assaultPodCount, out assaultPodsAvailable);
      if (assaultPodCount > 0)
      {
        point2 = new Point(x1, y10);
        string description6 = TextResolver.GetText("Strength") + ": " + assaultPodAttackValues.ToString("0") + "   (" + assaultPodsAvailable.ToString("0") + "/" + assaultPodCount.ToString("0") + " " + TextResolver.GetText("pods") + ")";
        infoPanel.DrawLabelledDescription(graphics, TextResolver.GetText("Boarding"), labelWidth, description6, point2);
        y10 += rowHeight;
      }
      int num8 = y10 + rowHeight / 4;
      int index1 = 0;
      int num9 = -3;
      int num10 = 0;
      int num11 = 27;
      int num12 = num11;
      graphics.InterpolationMode = InterpolationMode.Bilinear;
      while (index1 < shipGroup.Ships.Count)
      {
        Image builtObjectImage = (Image) infoPanel._BuiltObjectImages[shipGroup.Ships[index1].PictureRef];
        point2 = new Point(x1 + labelWidth + 10 + num9, num8 + num10);
        int num13 = Math.Min((int) (Math.Sqrt((double) shipGroup.Ships[index1].Size) * 1.25), num11);
        int num14 = (num11 - num13) / 2;
        string str = string.Empty;
        Rectangle srcRect = new Rectangle(0, 0, builtObjectImage.Width, builtObjectImage.Height);
        Rectangle destRect = new Rectangle(x1 + labelWidth + 10 + num9 + num14, num8 + num10 + num14, num13, num13);
        Rectangle rectangle = new Rectangle(x1 + labelWidth + 10 + num9, num8 + num10, num11, num11);
        if (shipGroup.Ships[index1].DamagedComponentCount > 0)
        {
          if (((shipGroup.Empire == infoPanel._Game.PlayerEmpire ? 1 : (infoPanel._Game.GodMode ? 1 : 0)) | (flag1 ? 1 : 0)) != 0)
          {
            BuiltObject ship = shipGroup.Ships[index1];
            if (ship.Components.Count<BuiltObjectComponent>((Func<BuiltObjectComponent, bool>) (zz => zz.Category == ComponentCategoryType.HyperDrive)) > 0 && shipGroup.Ships[index1].WarpSpeed == 0)
            {
              graphics.FillRectangle((Brush) new SolidBrush(Color.FromArgb(64, 0, (int) byte.MaxValue, (int) byte.MaxValue)), rectangle);
            }
            else
            {
              bool flag2 = true;
              foreach (Component component in ship.Components.Where<BuiltObjectComponent>((Func<BuiltObjectComponent, bool>) (kk => kk.Status == ComponentStatus.Damaged)))
              {
                if (component.Category != ComponentCategoryType.Armor)
                {
                  flag2 = false;
                  break;
                }
              }
              if (flag2)
                graphics.FillRectangle((Brush) new SolidBrush(Color.FromArgb(64, (int) byte.MaxValue, (int) byte.MaxValue, 0)), rectangle);
              else
                graphics.FillRectangle((Brush) new SolidBrush(Color.FromArgb(64, (int) byte.MaxValue, 0, 64)), rectangle);
            }
          }
        }
        else if (shipGroup.Ships[index1].UnbuiltComponentCount > 0 && ((shipGroup.Empire == infoPanel._Game.PlayerEmpire ? 1 : (infoPanel._Game.GodMode ? 1 : 0)) | (flag1 ? 1 : 0)) != 0)
        {
          graphics.FillRectangle((Brush) new SolidBrush(Color.FromArgb(32, (int) byte.MaxValue, 128, 0)), rectangle);
          str = TextResolver.GetText("Under construction");
        }
        graphics.DrawImage(builtObjectImage, destRect, srcRect, GraphicsUnit.Pixel);
        if (infoPanel._Game.PlayerEmpire.IsObjectVisibleToThisEmpire((StellarObject) shipGroup.Ships[index1]))
        {
          if (!string.IsNullOrEmpty(str))
            infoPanel.AddHotspot(rectangle, (object) shipGroup.Ships[index1], Galaxy.ResolveDescription(shipGroup.Ships[index1].SubRole) + " " + shipGroup.Ships[index1].Name + " (" + str + " - " + TextResolver.GetText("click to select") + ")");
          else
            infoPanel.AddHotspot(rectangle, (object) shipGroup.Ships[index1], Galaxy.ResolveDescription(shipGroup.Ships[index1].SubRole) + " " + shipGroup.Ships[index1].Name + " (" + TextResolver.GetText("click to select") + ")");
        }
        if (((shipGroup.Empire == infoPanel._Game.PlayerEmpire ? 1 : (infoPanel._Game.GodMode ? 1 : 0)) | (flag1 ? 1 : 0)) != 0)
        {
          int height2 = (int) (shipGroup.Ships[index1].CurrentFuel / (double) shipGroup.Ships[index1].FuelCapacity * (double) (num11 - 4));
          Rectangle rect2 = new Rectangle(x1 + labelWidth + 10 + num9 + num11 - 4, num8 + num10 + (num11 - 2 - height2), 2, height2);
          graphics.FillRectangle((Brush) new SolidBrush(Color.Green), rect2);
        }
        ++index1;
        if (num11 > num12)
          num12 = num11;
        num9 += num11;
        if (num9 > num1 - num11)
        {
          num10 += num12;
          num9 = -3;
        }
      }
      int y11 = num8 + (num10 + num11);
      if (infoPanel._ShowExtendedInfo && !string.IsNullOrEmpty(infoPanel._CharacterBonuses))
      {
        point2 = new Point(x1, y11);
        infoPanel.DrawLabel(graphics, TextResolver.GetText("Bonuses"), labelWidth, point2);
        int width4 = width1 - labelWidth;
        SizeF size = graphics.MeasureString(infoPanel._CharacterBonuses, infoPanel._NormalFont, width4, StringFormat.GenericDefault);
        point2 = new Point(x1 + labelWidth + 10, y11 + 1);
        infoPanel.DrawStringWithDropShadowBounded(graphics, infoPanel._CharacterBonuses, infoPanel._NormalFont, point2, size);
        int num15 = y11 + (int) size.Height;
      }
      if (shipGroup.Empire == infoPanel._Game.PlayerEmpire && shipGroup.LeadShip.IsAutoControlled)
      {
        Graphics graphics1 = graphics;
        Bitmap automateImage = InfoPanel._AutomateImage;
        int x4 = x1 + width1 - 16;
        clientRectangle = infoPanel.ClientRectangle;
        int y12 = clientRectangle.Height - 21;
        Point point3 = new Point(x4, y12);
        graphics1.DrawImageUnscaled((Image) automateImage, point3);
      }
      solidBrush.Dispose();
    }
  }
}
