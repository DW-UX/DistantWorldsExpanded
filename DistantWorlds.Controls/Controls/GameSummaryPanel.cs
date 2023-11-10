// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.GameSummaryPanel
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Timers;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class GameSummaryPanel : Panel
  {
    private bool _DataBound;
    private System.Timers.Timer _Timer;
    private GameSummaryList _GameSummaries;
    private Game _CurrentGame;
    private RaceImageCache _RaceImageCache;
    private Empire _CurrentGameSelectedFaction;
    private object _HoveredElement;
    private bool _HoveredElementIsAchievementCollection;
    private bool _HoveredElementIsSelectedFactionAchievement;
    private List<string> _OverlayTextLines = new List<string>();
    private Bitmap[] _GalaxyImages;
    private Bitmap[] _MedalImagesLarge;
    private Bitmap[] _MedalImagesSmall;
    private Bitmap[] _MedalImagesVerySmall;
    private Bitmap[] _RaceImages;
    private Bitmap _LeftArrow;
    private Bitmap _RightArrow;
    private Bitmap _UpArrow;
    private Bitmap _DownArrow;
    private Font _NormalFont;
    private Font _BoldFont;
    private Font _HeadingFont;
    private Font _HugeFont;
    private Size _Size = new Size(100, 60);
    private Rectangle _FactionDetailArea;
    private Rectangle _FactionAchievementArea;
    private Rectangle _FactionSummaryArea;
    private Rectangle _AchievementCollectionArea;
    private Rectangle _GameSummaryCollectionArea;
    private int _FactionAchievementScrollPosition;
    private int _FactionSummaryScrollPosition;
    private int _AchievementCollectionScrollPosition;
    private int _GameSummaryCollectionScrollPosition;
    private int _Gap = 10;
    private int _Margin = 5;
    private int _DoubleMargin = 10;
    public static Size MedalLargeSize = new Size(140, 140);
    public static Size MedalSmallSize = new Size(70, 70);
    public static Size MedalVerySmallSize = new Size(20, 20);
    private Size _RaceSize = new Size(80, 80);
    private int _ScrollArrowWidth = 13;
    private int _ScrollArrowHeight = 13;
    private SolidBrush _HoverHighlightBrush = new SolidBrush(Color.FromArgb(128, 170, 170, 170));

    public void InitializeImages(
      Bitmap[] galaxyImages,
      Bitmap[] medalImagesLarge,
      Bitmap[] medalImagesSmall,
      Bitmap[] raceImages,
      Bitmap leftArrow,
      Bitmap rightArrow,
      Bitmap upArrow,
      Bitmap downArrow)
    {
      this._LeftArrow = leftArrow;
      this._RightArrow = rightArrow;
      this._UpArrow = upArrow;
      this._DownArrow = downArrow;
      this._GalaxyImages = galaxyImages;
      this._MedalImagesLarge = medalImagesLarge;
      this._MedalImagesSmall = medalImagesSmall;
      this._MedalImagesVerySmall = GraphicsHelper.ScaleImages(medalImagesLarge, GameSummaryPanel.MedalVerySmallSize.Width, GameSummaryPanel.MedalVerySmallSize.Height);
      this._RaceImages = raceImages;
    }

    public void ClearImages()
    {
      if (this._LeftArrow != null && this._LeftArrow.PixelFormat != PixelFormat.Undefined)
        this._LeftArrow.Dispose();
      if (this._RightArrow != null && this._RightArrow.PixelFormat != PixelFormat.Undefined)
        this._RightArrow.Dispose();
      if (this._UpArrow != null && this._UpArrow.PixelFormat != PixelFormat.Undefined)
        this._UpArrow.Dispose();
      if (this._DownArrow != null && this._DownArrow.PixelFormat != PixelFormat.Undefined)
        this._DownArrow.Dispose();
      GraphicsHelper.DisposeImageArray(this._GalaxyImages);
      GraphicsHelper.DisposeImageArray(this._MedalImagesLarge);
      GraphicsHelper.DisposeImageArray(this._MedalImagesSmall);
      GraphicsHelper.DisposeImageArray(this._MedalImagesVerySmall);
      GraphicsHelper.DisposeImageArray(this._RaceImages);
    }

    public void BindData(
      GameSummaryList gameSummaries,
      Game currentGame,
      RaceImageCache raceImageCache,
      Font normalFont,
      Font boldFont,
      Font headingFont,
      Font hugeFont)
    {
      this._GameSummaries = gameSummaries;
      this._CurrentGame = currentGame;
      this._RaceImageCache = raceImageCache;
      this._NormalFont = normalFont;
      this._BoldFont = boldFont;
      this._HeadingFont = headingFont;
      this._HugeFont = hugeFont;
      this._CurrentGameSelectedFaction = (Empire) null;
      if (this._CurrentGame != null && this._CurrentGame.Galaxy != null)
      {
        this._CurrentGame.Galaxy.ReviewAchievements();
        this._CurrentGameSelectedFaction = this._CurrentGame.Galaxy.PlayerEmpire;
      }
      if (gameSummaries != null)
      {
        gameSummaries.Sort();
        gameSummaries.Reverse();
      }
      this._FactionAchievementScrollPosition = 0;
      this._FactionSummaryScrollPosition = 0;
      this._AchievementCollectionScrollPosition = 0;
      this._GameSummaryCollectionScrollPosition = 0;
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.UserPaint, true);
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.SetStyle(ControlStyles.Opaque, true);
      if (this._Timer != null)
      {
        this._Timer.Stop();
        this._Timer.Elapsed -= new ElapsedEventHandler(this._Timer_Elapsed);
      }
      this._Timer = new System.Timers.Timer(100.0);
      this._Timer.Elapsed += new ElapsedEventHandler(this._Timer_Elapsed);
      this._Timer.Start();
      this._DataBound = true;
    }

    public Empire CurrentGameSelectedFaction
    {
      get => this._CurrentGameSelectedFaction;
      set => this._CurrentGameSelectedFaction = value;
    }

    private void _Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
      if ((Control.MouseButtons & MouseButtons.Left) == MouseButtons.Left)
      {
        Point location = MouseHelper.GetCursorPosition();
        this.Invoke((Delegate) (new Action(() => location = this.PointToClient(location))));
        if (this.ClientRectangle.Contains(location))
          this.MouseClick(location.X, location.Y);
      }
      this.Invalidate();
    }

    public void ClearData()
    {
      this._DataBound = false;
      this._GameSummaries = (GameSummaryList) null;
      this._CurrentGame = (Game) null;
      this._RaceImageCache = (RaceImageCache) null;
      this._NormalFont = (Font) null;
      this._BoldFont = (Font) null;
      this._HeadingFont = (Font) null;
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      this._HoveredElement = this.DetectHoveredElement(e.X, e.Y);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      if (!this._DataBound)
        return;
      this.DrawPanel(e.Graphics);
    }

    protected override void OnClick(EventArgs e)
    {
      base.OnClick(e);
      Point client = this.PointToClient(MouseHelper.GetCursorPosition());
      this.MouseClick(client.X, client.Y);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      Point client = this.PointToClient(MouseHelper.GetCursorPosition());
      this.MouseClick(client.X, client.Y);
    }

    public new Size Size
    {
      get => this._Size;
      set
      {
        base.Size = value;
        this._Size = value;
        this.CalculateDrawAreas(new Rectangle(0, 0, this._Size.Width, this._Size.Height));
      }
    }

    private void CalculateDrawAreas(Rectangle drawArea)
    {
      this._FactionDetailArea = new Rectangle(this._Gap, this._Gap, 240, 170);
      this._FactionAchievementArea = new Rectangle(this._Gap + this._FactionDetailArea.Width + this._Gap, this._Gap, drawArea.Width - (this._Gap + this._FactionDetailArea.Width + this._Gap + this._Gap), this._FactionDetailArea.Height);
      this._FactionSummaryArea = new Rectangle(this._Gap, this._FactionDetailArea.Bottom + this._Gap, drawArea.Width - (this._Gap + this._Gap), this._RaceSize.Height + this._Gap + this._Gap + this._Margin + GameSummaryPanel.MedalVerySmallSize.Height + this._Margin);
      this._AchievementCollectionArea = new Rectangle(this._Gap, this._FactionSummaryArea.Bottom + this._Gap, drawArea.Width - (this._Gap + this._Gap), GameSummaryPanel.MedalSmallSize.Height + this._DoubleMargin);
      this._GameSummaryCollectionArea = new Rectangle(this._Gap, this._AchievementCollectionArea.Bottom + this._Gap, drawArea.Width - (this._Gap + this._Gap), drawArea.Bottom - (this._AchievementCollectionArea.Bottom + this._Gap + this._Gap));
    }

    public void MouseMove(int x, int y) => this._HoveredElement = this.DetectHoveredElement(x, y);

    public void MouseClick(int x, int y)
    {
      object hoveredElement = this._HoveredElement;
      switch (hoveredElement)
      {
        case string _:
          int num = 20;
          switch (((string) hoveredElement).ToLower(CultureInfo.InvariantCulture))
          {
            case "scrollfactionachievementleft":
              this._FactionAchievementScrollPosition = Math.Max(0, this._FactionAchievementScrollPosition - num);
              return;
            case "scrollfactionachievementright":
              int val1_1 = 0;
              if (this._CurrentGameSelectedFaction != null && this._CurrentGameSelectedFaction.Achievements != null && this._CurrentGameSelectedFaction.Achievements.Count > 0)
                val1_1 = (this._CurrentGameSelectedFaction.Achievements.Count - 1) * GameSummaryPanel.MedalLargeSize.Width;
              this._FactionAchievementScrollPosition = Math.Min(val1_1, this._FactionAchievementScrollPosition + num);
              return;
            case "scrollfactionsummaryleft":
              this._FactionSummaryScrollPosition = Math.Max(0, this._FactionSummaryScrollPosition - num);
              return;
            case "scrollfactionsummaryright":
              int val1_2 = 0;
              DistantWorlds.Types.EmpireList empireList = this.ResolveValidEmpires();
              if (empireList != null && empireList.Count > 0)
                val1_2 = (empireList.Count - 1) * (this._RaceSize.Width + this._Gap);
              this._FactionSummaryScrollPosition = Math.Min(val1_2, this._FactionSummaryScrollPosition + num);
              return;
            case "scrollachievementcollectionleft":
              this._AchievementCollectionScrollPosition = Math.Max(0, this._AchievementCollectionScrollPosition - num);
              return;
            case "scrollachievementcollectionright":
              int val1_3 = 0;
              if (this._GameSummaries != null && this._GameSummaries.PlayerAchievements != null && this._GameSummaries.PlayerAchievements.Count > 0)
                val1_3 = (this._GameSummaries.PlayerAchievements.Count - 1) * GameSummaryPanel.MedalSmallSize.Width;
              this._AchievementCollectionScrollPosition = Math.Min(val1_3, this._AchievementCollectionScrollPosition + num);
              return;
            case "scrollgamesummarycollectionup":
              this._GameSummaryCollectionScrollPosition = Math.Max(0, this._GameSummaryCollectionScrollPosition - num);
              return;
            case "scrollgamesummarycollectiondown":
              int val1_4 = 0;
              if (this._GameSummaries != null && this._GameSummaries.Count > 0)
                val1_4 = (this._GameSummaries.Count - 1) * GameSummaryPanel.MedalVerySmallSize.Height;
              this._GameSummaryCollectionScrollPosition = Math.Min(val1_4, this._GameSummaryCollectionScrollPosition + num);
              return;
            case null:
              return;
            default:
              return;
          }
        case Empire _:
          this._CurrentGameSelectedFaction = (Empire) hoveredElement;
          this._FactionAchievementScrollPosition = 0;
          break;
      }
    }

    public object DetectHoveredElement(int x, int y)
    {
      this._HoveredElementIsAchievementCollection = false;
      this._HoveredElementIsSelectedFactionAchievement = false;
      if (this._CurrentGame != null && this._CurrentGame.Galaxy != null && this._GameSummaries != null)
      {
        if (this._FactionDetailArea.Contains(x, y))
        {
          if (this._CurrentGameSelectedFaction != null)
            return (object) this._CurrentGameSelectedFaction;
        }
        else if (this._FactionAchievementArea.Contains(x, y))
        {
          if (x < this._FactionAchievementArea.Left + this._ScrollArrowWidth)
            return (object) "scrollfactionachievementleft";
          if (x > this._FactionAchievementArea.Right - this._ScrollArrowWidth)
            return (object) "scrollfactionachievementright";
          int num = x - (this._FactionAchievementArea.Left + this._ScrollArrowWidth) + this._FactionAchievementScrollPosition;
          if (this._CurrentGameSelectedFaction != null && this._CurrentGameSelectedFaction.Achievements != null && this._CurrentGameSelectedFaction.Achievements.Count > 0)
          {
            int index = num / GameSummaryPanel.MedalLargeSize.Width;
            if (index >= 0 && index < this._CurrentGameSelectedFaction.Achievements.Count)
            {
              this._HoveredElementIsSelectedFactionAchievement = true;
              return (object) this._CurrentGameSelectedFaction.Achievements[index];
            }
          }
        }
        else if (this._FactionSummaryArea.Contains(x, y))
        {
          if (x < this._FactionSummaryArea.Left + this._ScrollArrowWidth)
            return (object) "scrollfactionsummaryleft";
          if (x > this._FactionSummaryArea.Right - this._ScrollArrowWidth)
            return (object) "scrollfactionsummaryright";
          int index = (x - (this._FactionSummaryArea.Left + this._ScrollArrowWidth) + this._FactionSummaryScrollPosition) / (this._RaceSize.Width + this._Gap);
          DistantWorlds.Types.EmpireList empireList = this.ResolveValidEmpires();
          if (empireList != null && empireList.Count > 0 && index >= 0 && index < empireList.Count)
            return (object) empireList[index];
        }
        else if (this._AchievementCollectionArea.Contains(x, y))
        {
          if (x < this._AchievementCollectionArea.Left + this._ScrollArrowWidth)
            return (object) "scrollachievementcollectionleft";
          if (x > this._AchievementCollectionArea.Right - this._ScrollArrowWidth)
            return (object) "scrollachievementcollectionright";
          int index = (x - (this._AchievementCollectionArea.Left + this._ScrollArrowWidth) + this._AchievementCollectionScrollPosition) / GameSummaryPanel.MedalSmallSize.Width;
          if (this._GameSummaries != null && this._GameSummaries.PlayerAchievements != null && this._GameSummaries.PlayerAchievements.Count > 0 && index >= 0 && index < this._GameSummaries.PlayerAchievements.Count)
          {
            this._HoveredElementIsAchievementCollection = true;
            return (object) this._GameSummaries.PlayerAchievements[index];
          }
        }
        else if (this._GameSummaryCollectionArea.Contains(x, y))
        {
          if (y < this._GameSummaryCollectionArea.Top + this._ScrollArrowHeight)
            return (object) "scrollgamesummarycollectionup";
          if (y > this._GameSummaryCollectionArea.Bottom - this._ScrollArrowHeight)
            return (object) "scrollgamesummarycollectiondown";
          int index = (y - (this._GameSummaryCollectionArea.Top + this._ScrollArrowHeight) + this._GameSummaryCollectionScrollPosition) / GameSummaryPanel.MedalVerySmallSize.Height;
          if (this._GameSummaries != null && this._GameSummaries.Count > 0 && index >= 0 && index < this._GameSummaries.Count)
            return (object) this._GameSummaries[index];
        }
      }
      return (object) null;
    }

    public void DrawPanel(Graphics graphics)
    {
      try
      {
        this.DrawGalaxyBackground(graphics, new Rectangle(0, 0, this._Size.Width, this._Size.Height));
        this.DrawFactionDetail(graphics, this._FactionDetailArea, this._CurrentGame.Galaxy.GameSummary, this._CurrentGameSelectedFaction);
        this.DrawFactionAchievements(graphics, this._FactionAchievementArea, this._CurrentGameSelectedFaction, this._FactionAchievementScrollPosition);
        this.DrawFactionSummaries(graphics, this._FactionSummaryArea, this._CurrentGame.Galaxy, this._FactionSummaryScrollPosition);
        this.DrawAchievementCollection(graphics, this._AchievementCollectionArea, this._GameSummaries.PlayerAchievements, this._AchievementCollectionScrollPosition);
        this.DrawGameSummaryCollection(graphics, this._GameSummaryCollectionArea, this._GameSummaries, this._GameSummaryCollectionScrollPosition);
        this.DrawTooltips(graphics);
      }
      catch (Exception ex)
      {
      }
    }

    public void DrawGalaxyBackground(Graphics graphics, Rectangle rectangle) => graphics.DrawImage((Image) this._GalaxyImages[0], rectangle);

    public void DrawFactionDetail(
      Graphics graphics,
      Rectangle rectangle,
      GameSummary gameSummary,
      Empire selectFaction)
    {
      int x1 = this._FactionDetailArea.X + this._Margin;
      int y1 = this._FactionDetailArea.Y + this._Margin;
      int width = this._FactionDetailArea.Width - this._DoubleMargin;
      using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(48, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue)))
        graphics.FillRectangle((Brush) solidBrush, this._FactionDetailArea);
      if (selectFaction == null || gameSummary == null)
        return;
      using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(64, selectFaction.MainColor)))
        graphics.FillRectangle((Brush) solidBrush, this._FactionDetailArea);
      Bitmap dominantRaceImage = this._RaceImageCache.GetEmpireDominantRaceImage(selectFaction);
      if (dominantRaceImage != null)
      {
        GraphicsHelper.SetGraphicsQuality(graphics, GraphicsQuality.High);
        int x2 = this._FactionDetailArea.X + (this._FactionDetailArea.Width - this._RaceSize.Width) / 2;
        graphics.DrawImage((Image) dominantRaceImage, new Rectangle(x2, y1, this._RaceSize.Width, this._RaceSize.Height));
        y1 += this._RaceSize.Height;
        GraphicsHelper.SetGraphicsQuality(graphics, GraphicsQuality.Medium);
      }
      int y2 = y1 + GraphicsHelper.DrawStringWithDropShadowCentered(graphics, selectFaction.Name, this._HeadingFont, new Rectangle(x1, y1, width, this._FactionDetailArea.Height)) - 2;
      if (selectFaction.GovernmentAttributes != null)
        y2 = y2 + GraphicsHelper.DrawStringWithDropShadowCentered(graphics, selectFaction.GovernmentAttributes.Name, this._NormalFont, new Rectangle(x1, y2, width, this._FactionDetailArea.Height)) - 4;
      else if (selectFaction.PirateEmpireBaseHabitat != null)
        y2 = y2 + GraphicsHelper.DrawStringWithDropShadowCentered(graphics, TextResolver.GetText("Pirate Faction"), this._NormalFont, new Rectangle(x1, y2, width, this._FactionDetailArea.Height)) - 4;
      int y3 = y2 + GraphicsHelper.DrawStringWithDropShadowCentered(graphics, string.Format(TextResolver.GetText("Game Summary Galaxy Size"), (object) gameSummary.GalaxyStarCount.ToString()), this._NormalFont, new Rectangle(x1, y2, width, this._FactionDetailArea.Height)) - 4;
      int y4 = y3 + GraphicsHelper.DrawStringWithDropShadowCentered(graphics, string.Format(TextResolver.GetText("Game Summary Difficulty"), (object) Galaxy.ResolveDifficultyDescription(gameSummary.DifficultyLevel)), this._NormalFont, new Rectangle(x1, y3, width, this._FactionDetailArea.Height)) - 4;
      int num = y4 + GraphicsHelper.DrawStringWithDropShadowCentered(graphics, string.Format(TextResolver.GetText("Game Summary Score"), (object) selectFaction.Score.ToString("###,###,##0")), this._NormalFont, new Rectangle(x1, y4, width, this._FactionDetailArea.Height));
    }

    public DistantWorlds.Types.EmpireList ResolveValidEmpires()
    {
      DistantWorlds.Types.EmpireList empireList = new DistantWorlds.Types.EmpireList();
      if (this._CurrentGame != null && this._CurrentGame.Galaxy != null)
      {
        Empire empire1 = this._CurrentGame.Galaxy.IdentifyMechanoidEmpire();
        Empire empire2 = this._CurrentGame.Galaxy.IdentifyShakturiEmpire();
        if (this._CurrentGame.IsFinished)
        {
          empireList.AddRange((IEnumerable<Empire>) this._CurrentGame.Galaxy.Empires);
          if (this._CurrentGame.Galaxy.PlayerEmpire.PirateEmpireBaseHabitat != null)
          {
            empireList.Clear();
            empireList.AddRange((IEnumerable<Empire>) this._CurrentGame.Galaxy.PirateEmpires);
          }
          if (empireList.Contains(empire1))
            empireList.Remove(empire1);
          if (empireList.Contains(empire2))
            empireList.Remove(empire2);
        }
        else
        {
          empireList = this._CurrentGame.Galaxy.PlayerEmpire.GetEmpiresWeHaveMetOfMatchingType();
          if (empireList.Contains(empire1))
            empireList.Remove(empire1);
          if (empireList.Contains(empire2))
            empireList.Remove(empire2);
        }
      }
      return empireList;
    }

    public Bitmap ObtainMedalImage(Achievement achievement, int size, out bool disposeWhenFinished)
    {
      disposeWhenFinished = false;
      int achievementLevel = Galaxy.DetermineAchievementLevel(achievement.Type, achievement.Value);
      int index = Galaxy.ResolveAchievementMedalImageIndex(achievement.Type, achievementLevel);
      Bitmap original = (Bitmap) null;
      switch (size)
      {
        case 0:
          original = this._MedalImagesLarge[index];
          break;
        case 1:
          original = this._MedalImagesSmall[index];
          break;
        case 2:
          original = this._MedalImagesVerySmall[index];
          break;
      }
      if (achievement.Type == AchievementType.AchieveAllRaceVictoryConditions && achievement.AdditionalData != null && achievement.AdditionalData is Race)
      {
        RectangleF rectangleF = new RectangleF(0.375f, 0.66f, 0.25f, 0.25f);
        Bitmap raceImage = this._RaceImageCache.GetRaceImage(((Race) achievement.AdditionalData).PictureRef);
        if (raceImage != null)
        {
          Bitmap medalImage = new Bitmap((Image) original);
          using (Graphics graphics = Graphics.FromImage((Image) medalImage))
          {
            GraphicsHelper.SetGraphicsQuality(graphics, GraphicsQuality.High);
            Rectangle rect = new Rectangle((int) ((double) rectangleF.X * (double) medalImage.Width), (int) ((double) rectangleF.Y * (double) medalImage.Height), (int) ((double) rectangleF.Width * (double) medalImage.Width), (int) ((double) rectangleF.Height * (double) medalImage.Height));
            graphics.DrawImage((Image) raceImage, rect);
          }
          disposeWhenFinished = true;
          return medalImage;
        }
      }
      return original;
    }

    public void DrawFactionAchievements(
      Graphics graphics,
      Rectangle rectangle,
      Empire empire,
      int horizontalScrollPosition)
    {
      int x = this._FactionAchievementArea.X;
      int y1 = this._FactionAchievementArea.Y;
      graphics.SetClip(this._FactionAchievementArea);
      using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(48, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue)))
        graphics.FillRectangle((Brush) solidBrush, this._FactionAchievementArea);
      using (SolidBrush brush = new SolidBrush(Color.FromArgb(24, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue)))
      {
        using (SolidBrush dropShadowBrush = new SolidBrush(Color.FromArgb(24, 0, 0, 0)))
        {
          Rectangle textArea = new Rectangle(this._FactionAchievementArea.X, this._FactionAchievementArea.Y + this._FactionAchievementArea.Height / 2 - 20, this._FactionAchievementArea.Width, 40);
          GraphicsHelper.DrawStringWithDropShadowCentered(graphics, TextResolver.GetText("Empire Achievements"), this._HugeFont, brush, dropShadowBrush, textArea);
        }
      }
      if (empire != null && empire.Achievements != null)
      {
        int y2 = this._FactionAchievementArea.Y;
        int num = (this._FactionAchievementArea.Height - GameSummaryPanel.MedalLargeSize.Height) / 2;
        for (int index = 0; index < empire.Achievements.Count; ++index)
        {
          Achievement achievement = empire.Achievements[index];
          if (achievement != null)
          {
            Rectangle rect = this.ResolveFactionAchievementDrawLocation(index);
            if (rect.Right >= 0 && rect.Left < this._FactionAchievementArea.Right)
            {
              bool disposeWhenFinished = false;
              Bitmap medalImage = this.ObtainMedalImage(achievement, 0, out disposeWhenFinished);
              if (medalImage != null)
              {
                graphics.DrawImage((Image) medalImage, rect);
                if (disposeWhenFinished)
                  medalImage.Dispose();
              }
            }
          }
        }
      }
      int y3 = this._FactionAchievementArea.Top + (this._FactionAchievementArea.Height - this._LeftArrow.Height) / 2;
      if (this._HoveredElement == (object) "scrollfactionachievementleft")
        graphics.FillRectangle((Brush) this._HoverHighlightBrush, new Rectangle(this._FactionAchievementArea.X, this._FactionAchievementArea.Y, this._ScrollArrowWidth, this._FactionAchievementArea.Height));
      else if (this._HoveredElement == (object) "scrollfactionachievementright")
        graphics.FillRectangle((Brush) this._HoverHighlightBrush, new Rectangle(this._FactionAchievementArea.Right - this._ScrollArrowWidth, this._FactionAchievementArea.Y, this._ScrollArrowWidth, this._FactionAchievementArea.Height));
      graphics.DrawImage((Image) this._LeftArrow, new Point(this._FactionAchievementArea.X, y3));
      graphics.DrawImage((Image) this._RightArrow, new Point(this._FactionAchievementArea.Right - this._ScrollArrowWidth, y3));
      graphics.ResetClip();
    }

    public Rectangle ResolveFactionAchievementDrawLocation(int index) => new Rectangle(this._FactionAchievementArea.X + this._Margin + this._ScrollArrowWidth + index * GameSummaryPanel.MedalLargeSize.Width - this._FactionAchievementScrollPosition, this._FactionAchievementArea.Y + (this._FactionAchievementArea.Height - GameSummaryPanel.MedalLargeSize.Height) / 2, GameSummaryPanel.MedalLargeSize.Width, GameSummaryPanel.MedalLargeSize.Height);

    public void DrawTooltips(Graphics graphics)
    {
      if (this._HoveredElement is Achievement)
      {
        Achievement hoveredElement = (Achievement) this._HoveredElement;
        if (this._HoveredElementIsSelectedFactionAchievement)
        {
          int index = 0;
          if (this._CurrentGameSelectedFaction != null && this._CurrentGameSelectedFaction.Achievements != null)
            index = this._CurrentGameSelectedFaction.Achievements.IndexOf(hoveredElement);
          Rectangle rectangle1 = this.ResolveFactionAchievementDrawLocation(index);
          int width = (int) ((double) GameSummaryPanel.MedalLargeSize.Width * 1.6);
          int x = rectangle1.X + rectangle1.Width / 2 - width / 2;
          int bottom = rectangle1.Bottom;
          if (x < 0)
            x = 0;
          else if (x + width > this._Size.Width)
            x = this._Size.Width - width;
          Rectangle rectangle2 = new Rectangle(x, bottom, width, 40);
          this.DrawMedalSummary(graphics, hoveredElement, rectangle2, Color.FromArgb(128, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), false);
        }
        else if (this._HoveredElementIsAchievementCollection)
        {
          int index = 0;
          if (this._GameSummaries != null && this._GameSummaries.PlayerAchievements != null)
            index = this._GameSummaries.PlayerAchievements.IndexOf(hoveredElement);
          Rectangle rectangle3 = this.ResolveAchievementCollectionDrawLocation(index);
          int width = (int) ((double) GameSummaryPanel.MedalLargeSize.Width * 1.4);
          int height = GameSummaryPanel.MedalLargeSize.Height + 55;
          int x = rectangle3.X + rectangle3.Width / 2 - width / 2;
          int bottom = rectangle3.Bottom;
          if (x < 0)
            x = 0;
          else if (x + width > this._Size.Width)
            x = this._Size.Width - width;
          Rectangle rectangle4 = new Rectangle(x, bottom, width, height);
          this.DrawMedalSummary(graphics, hoveredElement, rectangle4, Color.FromArgb(128, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), true);
        }
      }
      if (this._HoveredElement is Empire && this._FactionDetailArea.Contains(this.PointToClient(MouseHelper.GetCursorPosition())))
      {
        Empire hoveredElement = (Empire) this._HoveredElement;
        if (hoveredElement != null)
        {
          int population;
          int economy;
          int colonies;
          int military;
          int research;
          int wonders;
          int empireScore = this._CurrentGame.Galaxy.CalculateEmpireScore(hoveredElement, out population, out economy, out colonies, out military, out research, out wonders);
          Rectangle rectangle = new Rectangle(this._FactionDetailArea.X, this._FactionDetailArea.Bottom, this._FactionDetailArea.Width, 130);
          int top = rectangle.Top;
          int height = 20;
          using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(192, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue)))
            graphics.FillRectangle((Brush) solidBrush, rectangle);
          using (SolidBrush brush = new SolidBrush(Color.Orange))
          {
            int y1 = top + GraphicsHelper.DrawStringWithDropShadowCentered(graphics, string.Format(TextResolver.GetText("Game Summary Score"), (object) empireScore.ToString("###,###,##0")), this._BoldFont, brush, rectangle);
            Rectangle textArea1 = new Rectangle(this._FactionDetailArea.X, y1, this._FactionDetailArea.Width, height);
            int y2 = y1 + GraphicsHelper.DrawStringWithDropShadowCentered(graphics, string.Format(TextResolver.GetText("Game Summary Score Population"), (object) population.ToString("###,###,##0")), this._NormalFont, brush, textArea1);
            Rectangle textArea2 = new Rectangle(this._FactionDetailArea.X, y2, this._FactionDetailArea.Width, height);
            int y3 = y2 + GraphicsHelper.DrawStringWithDropShadowCentered(graphics, string.Format(TextResolver.GetText("Game Summary Score Economy"), (object) economy.ToString("###,###,##0")), this._NormalFont, brush, textArea2);
            Rectangle textArea3 = new Rectangle(this._FactionDetailArea.X, y3, this._FactionDetailArea.Width, height);
            int y4 = y3 + GraphicsHelper.DrawStringWithDropShadowCentered(graphics, string.Format(TextResolver.GetText("Game Summary Score Colonies"), (object) colonies.ToString("###,###,##0")), this._NormalFont, brush, textArea3);
            Rectangle textArea4 = new Rectangle(this._FactionDetailArea.X, y4, this._FactionDetailArea.Width, height);
            int y5 = y4 + GraphicsHelper.DrawStringWithDropShadowCentered(graphics, string.Format(TextResolver.GetText("Game Summary Score Military"), (object) military.ToString("###,###,##0")), this._NormalFont, brush, textArea4);
            Rectangle textArea5 = new Rectangle(this._FactionDetailArea.X, y5, this._FactionDetailArea.Width, height);
            int y6 = y5 + GraphicsHelper.DrawStringWithDropShadowCentered(graphics, string.Format(TextResolver.GetText("Game Summary Score Research"), (object) research.ToString("###,###,##0")), this._NormalFont, brush, textArea5);
            Rectangle textArea6 = new Rectangle(this._FactionDetailArea.X, y6, this._FactionDetailArea.Width, height);
            int num = y6 + GraphicsHelper.DrawStringWithDropShadowCentered(graphics, string.Format(TextResolver.GetText("Game Summary Score Wonders"), (object) wonders.ToString("###,###,##0")), this._NormalFont, brush, textArea6);
          }
        }
      }
      if (this._OverlayTextLines.Count <= 0)
        return;
      Font hugeFont = this._HugeFont;
      int height1 = this._OverlayTextLines.Count * hugeFont.Height + 5;
      int y = (this._Size.Height - height1) / 2;
      for (int index = 0; index < this._OverlayTextLines.Count; ++index)
      {
        string overlayTextLine = this._OverlayTextLines[index];
        using (SolidBrush brush = new SolidBrush(Color.Yellow))
          y += GraphicsHelper.DrawStringWithDropShadowCentered(graphics, overlayTextLine, hugeFont, brush, new Rectangle(0, y, this._Size.Width, height1));
      }
    }

    public List<string> OverlayTextLines
    {
      get => this._OverlayTextLines;
      set => this._OverlayTextLines = value;
    }

    public void DrawMedalSummary(
      Graphics graphics,
      Achievement achievement,
      Rectangle rectangle,
      Color backgroundColor,
      bool showMedalImage)
    {
      using (SolidBrush solidBrush = new SolidBrush(backgroundColor))
        graphics.FillRectangle((Brush) solidBrush, rectangle);
      int y1 = rectangle.Top;
      if (showMedalImage)
      {
        bool disposeWhenFinished = false;
        Bitmap medalImage = this.ObtainMedalImage(achievement, 0, out disposeWhenFinished);
        if (medalImage != null)
        {
          int x = rectangle.X + (rectangle.Width - medalImage.Width) / 2;
          graphics.DrawImage((Image) medalImage, new Point(x, y1));
          y1 = y1 + medalImage.Height + 5;
          if (disposeWhenFinished)
            medalImage.Dispose();
        }
      }
      string text1 = Galaxy.ResolveAchievementTitleComplete(achievement);
      Rectangle textArea1 = new Rectangle(rectangle.X, y1, rectangle.Width, 35);
      using (SolidBrush brush = new SolidBrush(Color.Yellow))
      {
        int y2 = y1 + GraphicsHelper.DrawStringWithDropShadowCentered(graphics, text1, this._BoldFont, brush, textArea1);
        string text2 = Galaxy.ResolveDescription(achievement);
        Rectangle textArea2 = new Rectangle(rectangle.X, y2, rectangle.Width, 40);
        int num = y2 + GraphicsHelper.DrawStringWithDropShadowCentered(graphics, text2, this._NormalFont, brush, textArea2);
      }
    }

    public void DrawFactionSummaries(
      Graphics graphics,
      Rectangle rectangle,
      Galaxy galaxy,
      int horizontalScrollPosition)
    {
      int num1 = this._FactionSummaryArea.X + this._Margin;
      int y1 = this._FactionSummaryArea.Y + this._Margin;
      graphics.SetClip(this._FactionSummaryArea);
      using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(48, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue)))
        graphics.FillRectangle((Brush) solidBrush, this._FactionSummaryArea);
      using (SolidBrush brush = new SolidBrush(Color.FromArgb(24, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue)))
      {
        using (SolidBrush dropShadowBrush = new SolidBrush(Color.FromArgb(24, 0, 0, 0)))
        {
          Rectangle textArea = new Rectangle(this._FactionSummaryArea.X, this._FactionSummaryArea.Y + this._FactionSummaryArea.Height / 2 - 20, this._FactionSummaryArea.Width, 40);
          GraphicsHelper.DrawStringWithDropShadowCentered(graphics, TextResolver.GetText("Empires in this Game"), this._HugeFont, brush, dropShadowBrush, textArea);
        }
      }
      if (galaxy != null && galaxy.Empires != null && galaxy.PirateEmpires != null)
      {
        DistantWorlds.Types.EmpireList empireList = this.ResolveValidEmpires();
        for (int index1 = 0; index1 < empireList.Count; ++index1)
        {
          Empire empire = empireList[index1];
          if (empire != null)
          {
            int x1 = num1 + this._ScrollArrowWidth + index1 * (this._RaceSize.Width + this._Gap) - this._FactionSummaryScrollPosition;
            if (this._HoveredElement == empire)
              graphics.FillRectangle((Brush) this._HoverHighlightBrush, new Rectangle(x1, y1, this._RaceSize.Width, this._FactionSummaryArea.Height - this._DoubleMargin));
            if (x1 + this._RaceSize.Width >= 0 && x1 < this._FactionSummaryArea.Right)
            {
              GraphicsHelper.SetGraphicsQuality(graphics, GraphicsQuality.High);
              Bitmap raceImage = this._RaceImageCache.GetRaceImage(empire.DominantRace.PictureRef);
              if (raceImage != null)
              {
                Rectangle rect = new Rectangle(x1, y1, this._RaceSize.Width, this._RaceSize.Height);
                graphics.DrawImage((Image) raceImage, rect);
              }
              GraphicsHelper.SetGraphicsQuality(graphics, GraphicsQuality.Medium);
              Rectangle textArea = new Rectangle(x1, y1 + this._RaceSize.Height, this._RaceSize.Width, 15);
              string text = string.Format(TextResolver.GetText("Game Summary Score"), (object) empire.Score.ToString("###,###,##0"));
              GraphicsHelper.DrawStringWithDropShadowCentered(graphics, text, this._NormalFont, textArea);
              int x2 = x1;
              int num2 = x1 + this._RaceSize.Width;
              for (int index2 = 0; index2 < empire.Achievements.Count; ++index2)
              {
                Achievement achievement = empire.Achievements[index2];
                if (achievement != null)
                {
                  bool disposeWhenFinished = false;
                  Bitmap medalImage = this.ObtainMedalImage(achievement, 2, out disposeWhenFinished);
                  if (medalImage != null)
                  {
                    graphics.DrawImage((Image) medalImage, new Point(x2, y1 + 12 + this._RaceSize.Height + this._Margin));
                    if (disposeWhenFinished)
                      medalImage.Dispose();
                    x2 += GameSummaryPanel.MedalVerySmallSize.Width;
                    if (x2 >= num2)
                      break;
                  }
                }
              }
            }
          }
        }
      }
      int y2 = this._FactionSummaryArea.Top + (this._FactionSummaryArea.Height - this._LeftArrow.Height) / 2;
      if (this._HoveredElement == (object) "scrollfactionsummaryleft")
        graphics.FillRectangle((Brush) this._HoverHighlightBrush, new Rectangle(this._FactionSummaryArea.X, this._FactionSummaryArea.Y, this._ScrollArrowWidth, this._FactionSummaryArea.Height));
      else if (this._HoveredElement == (object) "scrollfactionsummaryright")
        graphics.FillRectangle((Brush) this._HoverHighlightBrush, new Rectangle(this._FactionSummaryArea.Right - this._ScrollArrowWidth, this._FactionSummaryArea.Y, this._ScrollArrowWidth, this._FactionSummaryArea.Height));
      graphics.DrawImage((Image) this._LeftArrow, new Point(this._FactionSummaryArea.X, y2));
      graphics.DrawImage((Image) this._RightArrow, new Point(this._FactionSummaryArea.Right - this._ScrollArrowWidth, y2));
      graphics.ResetClip();
    }

    public void DrawAchievementCollection(
      Graphics graphics,
      Rectangle rectangle,
      AchievementList achievements,
      int horizontalScrollPosition)
    {
      int x = this._AchievementCollectionArea.X;
      int y1 = this._AchievementCollectionArea.Y;
      graphics.SetClip(this._AchievementCollectionArea);
      using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(48, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue)))
        graphics.FillRectangle((Brush) solidBrush, this._AchievementCollectionArea);
      using (SolidBrush brush = new SolidBrush(Color.FromArgb(24, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue)))
      {
        using (SolidBrush dropShadowBrush = new SolidBrush(Color.FromArgb(24, 0, 0, 0)))
        {
          Rectangle textArea = new Rectangle(this._AchievementCollectionArea.X, this._AchievementCollectionArea.Y + this._AchievementCollectionArea.Height / 2 - 20, this._AchievementCollectionArea.Width, 40);
          GraphicsHelper.DrawStringWithDropShadowCentered(graphics, TextResolver.GetText("Player's Achievements"), this._HugeFont, brush, dropShadowBrush, textArea);
        }
      }
      if (achievements != null)
      {
        for (int index = 0; index < achievements.Count; ++index)
        {
          Achievement achievement = achievements[index];
          if (achievement != null)
          {
            Rectangle rect = this.ResolveAchievementCollectionDrawLocation(index);
            if (rect.Right >= 0 && rect.Left < this._AchievementCollectionArea.Right)
            {
              bool disposeWhenFinished = false;
              Bitmap medalImage = this.ObtainMedalImage(achievement, 1, out disposeWhenFinished);
              if (medalImage != null)
              {
                graphics.DrawImage((Image) medalImage, rect);
                if (disposeWhenFinished)
                  medalImage.Dispose();
              }
            }
          }
        }
      }
      int y2 = this._AchievementCollectionArea.Top + (this._AchievementCollectionArea.Height - this._LeftArrow.Height) / 2;
      if (this._HoveredElement == (object) "scrollachievementcollectionleft")
        graphics.FillRectangle((Brush) this._HoverHighlightBrush, new Rectangle(this._AchievementCollectionArea.X, this._AchievementCollectionArea.Y, this._ScrollArrowWidth, this._AchievementCollectionArea.Height));
      else if (this._HoveredElement == (object) "scrollachievementcollectionright")
        graphics.FillRectangle((Brush) this._HoverHighlightBrush, new Rectangle(this._AchievementCollectionArea.Right - this._ScrollArrowWidth, this._AchievementCollectionArea.Y, this._ScrollArrowWidth, this._AchievementCollectionArea.Height));
      graphics.DrawImage((Image) this._LeftArrow, new Point(this._AchievementCollectionArea.X, y2));
      graphics.DrawImage((Image) this._RightArrow, new Point(this._AchievementCollectionArea.Right - this._ScrollArrowWidth, y2));
      graphics.ResetClip();
    }

    public Rectangle ResolveAchievementCollectionDrawLocation(int index) => new Rectangle(this._AchievementCollectionArea.X + this._Margin + this._ScrollArrowWidth + index * GameSummaryPanel.MedalSmallSize.Width - this._AchievementCollectionScrollPosition, this._AchievementCollectionArea.Y + this._Margin, GameSummaryPanel.MedalSmallSize.Width, GameSummaryPanel.MedalSmallSize.Height);

    public void DrawGameSummaryCollection(
      Graphics graphics,
      Rectangle rectangle,
      GameSummaryList gameSummaries,
      int verticalScrollPosition)
    {
      int num1 = this._GameSummaryCollectionArea.X + this._Margin;
      int num2 = this._GameSummaryCollectionArea.Y + this._Margin;
      graphics.SetClip(this._GameSummaryCollectionArea);
      using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(48, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue)))
        graphics.FillRectangle((Brush) solidBrush, this._GameSummaryCollectionArea);
      using (SolidBrush brush = new SolidBrush(Color.FromArgb(24, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue)))
      {
        using (SolidBrush dropShadowBrush = new SolidBrush(Color.FromArgb(24, 0, 0, 0)))
        {
          Rectangle textArea = new Rectangle(this._GameSummaryCollectionArea.X, this._GameSummaryCollectionArea.Y + this._GameSummaryCollectionArea.Height / 2 - 20, this._GameSummaryCollectionArea.Width, 40);
          GraphicsHelper.DrawStringWithDropShadowCentered(graphics, TextResolver.GetText("Completed Games"), this._HugeFont, brush, dropShadowBrush, textArea);
        }
      }
      if (gameSummaries != null)
      {
        for (int index1 = 0; index1 < gameSummaries.Count; ++index1)
        {
          GameSummary gameSummary = gameSummaries[index1];
          if (gameSummary != null)
          {
            int y = num2 + this._ScrollArrowHeight + index1 * GameSummaryPanel.MedalVerySmallSize.Height - this._GameSummaryCollectionScrollPosition;
            if (y + GameSummaryPanel.MedalVerySmallSize.Height >= 0 && y < this._GameSummaryCollectionArea.Bottom)
            {
              if (gameSummary == this._CurrentGame.Galaxy.GameSummary)
              {
                using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(128, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue)))
                  graphics.FillRectangle((Brush) solidBrush, new Rectangle(this._GameSummaryCollectionArea.X, y, this._GameSummaryCollectionArea.Width, GameSummaryPanel.MedalVerySmallSize.Height));
              }
              else if (index1 % 2 == 0)
              {
                using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(48, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue)))
                  graphics.FillRectangle((Brush) solidBrush, new Rectangle(this._GameSummaryCollectionArea.X, y, this._GameSummaryCollectionArea.Width, GameSummaryPanel.MedalVerySmallSize.Height));
              }
              int x1 = num1;
              Bitmap raceImage = this._RaceImageCache.GetRaceImage(gameSummary.PlayerRace.PictureRef);
              if (raceImage != null)
              {
                Rectangle rect = new Rectangle(x1, y, GameSummaryPanel.MedalVerySmallSize.Height, GameSummaryPanel.MedalVerySmallSize.Height);
                GraphicsHelper.SetGraphicsQuality(graphics, GraphicsQuality.High);
                graphics.DrawImage((Image) raceImage, rect);
                GraphicsHelper.SetGraphicsQuality(graphics, GraphicsQuality.Medium);
              }
              int x2 = x1 + (GameSummaryPanel.MedalVerySmallSize.Height + this._Margin);
              using (SolidBrush brush = new SolidBrush(gameSummary.PlayerMainColor))
              {
                int num3 = 3;
                GraphicsHelper.DrawStringWithDropShadow(graphics, gameSummary.PlayerEmpireName, this._BoldFont, new Point(x2, y), brush);
                int x3 = x2 + 180;
                GraphicsHelper.DrawStringWithDropShadow(graphics, gameSummary.PlayerGovernmentName, this._NormalFont, new Point(x3, y + num3), brush);
                int x4 = x3 + 100;
                GraphicsHelper.DrawStringWithDropShadow(graphics, string.Format(TextResolver.GetText("Game Summary Galaxy Size"), (object) gameSummary.GalaxyStarCount.ToString()), this._NormalFont, new Point(x4, y + num3), brush);
                int x5 = x4 + 120;
                GraphicsHelper.DrawStringWithDropShadow(graphics, string.Format(TextResolver.GetText("Game Summary Difficulty"), (object) Galaxy.ResolveDifficultyDescription(gameSummary.DifficultyLevel)), this._NormalFont, new Point(x5, y + num3), brush);
                int x6 = x5 + 100;
                GraphicsHelper.DrawStringWithDropShadow(graphics, string.Format(TextResolver.GetText("Game Summary Score"), (object) gameSummary.PlayerScore.ToString()), this._NormalFont, new Point(x6, y + num3), brush);
                int x7 = x6 + 80;
                if (gameSummary.PlayerVictory)
                  GraphicsHelper.DrawStringWithDropShadow(graphics, TextResolver.GetText("Game Summary Victory"), this._NormalFont, new Point(x7, y + num3), brush);
                else
                  GraphicsHelper.DrawStringWithDropShadow(graphics, TextResolver.GetText("Game Summary Defeat"), this._NormalFont, new Point(x7, y + num3), brush);
                int num4 = x7 + 50;
              }
              int x8 = 670;
              for (int index2 = 0; index2 < gameSummary.PlayerAchievements.Count; ++index2)
              {
                Achievement playerAchievement = gameSummary.PlayerAchievements[index2];
                if (playerAchievement != null)
                {
                  bool disposeWhenFinished = false;
                  Bitmap medalImage = this.ObtainMedalImage(playerAchievement, 2, out disposeWhenFinished);
                  if (medalImage != null)
                  {
                    graphics.DrawImage((Image) medalImage, new Point(x8, y));
                    x8 += GameSummaryPanel.MedalVerySmallSize.Width;
                    if (disposeWhenFinished)
                      medalImage.Dispose();
                  }
                }
              }
            }
          }
        }
      }
      int x = this._GameSummaryCollectionArea.Left + (this._GameSummaryCollectionArea.Width - this._UpArrow.Width) / 2;
      if (this._HoveredElement == (object) "scrollgamesummarycollectionup")
        graphics.FillRectangle((Brush) this._HoverHighlightBrush, new Rectangle(this._GameSummaryCollectionArea.X, this._GameSummaryCollectionArea.Y, this._GameSummaryCollectionArea.Width, this._ScrollArrowHeight));
      else if (this._HoveredElement == (object) "scrollgamesummarycollectiondown")
        graphics.FillRectangle((Brush) this._HoverHighlightBrush, new Rectangle(this._GameSummaryCollectionArea.X, this._GameSummaryCollectionArea.Bottom - this._ScrollArrowHeight, this._GameSummaryCollectionArea.Width, this._ScrollArrowHeight));
      graphics.DrawImage((Image) this._UpArrow, new Point(x, this._GameSummaryCollectionArea.Y));
      graphics.DrawImage((Image) this._DownArrow, new Point(x, this._GameSummaryCollectionArea.Bottom - this._ScrollArrowHeight));
      graphics.ResetClip();
    }
  }
}
