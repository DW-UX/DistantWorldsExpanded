// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.ResearchTree
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.Timers;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class ResearchTree : UserControl
  {
    private Galaxy _Galaxy;
    private Empire _Empire;
    private IndustryType _Industry;
    private ResearchSystem _Research;
    private Bitmap[] _ComponentImages;
    private Bitmap[] _HabitatTypeImages;
    private Bitmap[] _FighterImages;
    private Bitmap[] _RaceImages;
    private Bitmap[] _FacilityImages;
    private Bitmap[] _PlagueImages;
    private Bitmap[] _TroopImagesInfantry;
    private Bitmap[] _TroopImagesArmored;
    private Bitmap[] _TroopImagesArtillery;
    private Bitmap[] _TroopImagesSpecialForces;
    private Bitmap _UpArrowImage;
    private Bitmap _CrashImage;
    private Bitmap _CarrierImage;
    private Bitmap _ResupplyShipImage;
    private AdjustableArrowCap _ArrowCapLarge = new AdjustableArrowCap(3f, 5f);
    private Cursor _DefaultMouseCursor;
    private Font _Font;
    private Font _LargeFont;
    private Font _LargeFontBold;
    private Font _NormalFont;
    private Font _NormalFontBold;
    private Font _ProjectTitleFont;
    private Font _ProjectDetailFont;
    private SolidBrush _WhiteBrush = new SolidBrush(Color.White);
    private SolidBrush _BlackTransparentBrush = new SolidBrush(Color.FromArgb(224, 0, 0, 0));
    private SolidBrush _UnresearchableBrush = new SolidBrush(Color.FromArgb((int) byte.MaxValue, 32, 0));
    private SolidBrush _RedBrush = new SolidBrush(Color.FromArgb((int) byte.MaxValue, 0, 0));
    private Pen _FadedDotPen;
    private System.Timers.Timer _Timer;
    public int X;
    public int Y;
    public double Zoom = 1.0;
    private int _NodeWidth = 150;
    private int _NodeHeight = 60;
    private int _GapWidth = 60;
    private int _GapHeight = 20;
    private int _Margin = 20;
    private int _ImageSize = 28;
    private bool _Dragging;
    private float _MouseX;
    private float _MouseY;
    private int _ContainerWidth;
    private int _ContainerHeight;
    private ScrollableControl _Container;
    private bool _ClipBackground;
    private bool _IntensifyColors;
    private Color _TextColor;
    private Color _TextColor2;
    private Color _ClipColor = Color.FromArgb(48, 48, 48);
    public bool CornerCurvedTopLeft = true;
    public bool CornerCurvedTopRight = true;
    public bool CornerCurvedBottomRight = true;
    public bool CornerCurvedBottomLeft = true;
    private ResearchNode _HighlightedNode;
    private int _LowestTechLevel = int.MaxValue;
    private int _HighestTechLevel;
    private int _LowestRow = int.MaxValue;
    private int _HighestRow;
    private List<Bitmap> _NodeButtonImagesGlowing = new List<Bitmap>();
    private List<Bitmap> _NodeButtonImagesDull = new List<Bitmap>();
    private List<Bitmap> _NodeButtonImagesDullHatched = new List<Bitmap>();
    private List<Bitmap> _NodeButtonImagesDullBlocked = new List<Bitmap>();
    private List<Color> _NodeButtonColors = new List<Color>();
    public bool EditMode;
    private Color backColor;
    private Color innerBorderColor;
    private Color outerBorderColor;
    private Color shineColor;
    private Color glowColor;
    private IContainer components;

    public event ResearchTree.NodeClickedHandler NodeClicked;

    public ResearchTree()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.UpdateStyles();
      this._Timer = new System.Timers.Timer();
      this._Timer.Elapsed += new ElapsedEventHandler(this._Timer_Elapsed);
      this._Timer.Interval = 100.0;
      this._Timer.Stop();
    }

    public IndustryType Industry => this._Industry;

    private void _Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
      if (this._Timer != null)
        this._Timer.Stop();
      Application.DoEvents();
      this.Invalidate();
      Application.DoEvents();
      if (this._Timer == null)
        return;
      this._Timer.Start();
    }

    public void StopPainting()
    {
      if (this._Timer == null)
        return;
      this._Timer.Stop();
    }

    public void StartPainting()
    {
      if (this._Timer == null)
        return;
      this._Timer.Start();
    }

    private void ClearImages()
    {
      this.ClearImageArray(this._ComponentImages);
      this.ClearImageArray(this._HabitatTypeImages);
      this.ClearImageArray(this._RaceImages);
      this.ClearImageArray(this._FacilityImages);
      this.ClearImageArray(this._PlagueImages);
      this.ClearImageArray(this._FighterImages);
      this.ClearImageArray(this._TroopImagesInfantry);
      this.ClearImageArray(this._TroopImagesArmored);
      this.ClearImageArray(this._TroopImagesArtillery);
      this.ClearImageArray(this._TroopImagesSpecialForces);
      if (this._UpArrowImage != null)
      {
        this._UpArrowImage.Dispose();
        this._UpArrowImage = (Bitmap) null;
      }
      if (this._CrashImage != null)
      {
        this._CrashImage.Dispose();
        this._CrashImage = (Bitmap) null;
      }
      if (this._CarrierImage != null)
      {
        this._CarrierImage.Dispose();
        this._CarrierImage = (Bitmap) null;
      }
      if (this._ResupplyShipImage == null)
        return;
      this._ResupplyShipImage.Dispose();
      this._ResupplyShipImage = (Bitmap) null;
    }

    private void ClearImageArray(Bitmap[] imageArray)
    {
      if (imageArray == null)
        return;
      for (int index = 0; index < imageArray.Length; ++index)
      {
        if (imageArray[index] != null)
        {
          imageArray[index].Dispose();
          imageArray[index] = (Bitmap) null;
        }
      }
    }

    private void ClearImageLists()
    {
      this._NodeButtonColors.Clear();
      this.ClearImageList(this._NodeButtonImagesGlowing);
      this.ClearImageList(this._NodeButtonImagesDull);
      this.ClearImageList(this._NodeButtonImagesDullHatched);
      this.ClearImageList(this._NodeButtonImagesDullBlocked);
    }

    private void ClearImageList(List<Bitmap> imageList)
    {
      if (imageList == null)
        return;
      for (int index = 0; index < imageList.Count; ++index)
      {
        if (imageList[index] != null)
        {
          imageList[index].Dispose();
          imageList[index] = (Bitmap) null;
        }
      }
      imageList.Clear();
    }

    public void InitializeImages(
      Bitmap[] componentImages,
      Bitmap[] fighterImages,
      Bitmap[] habitatTypeImages,
      Bitmap[] raceImages,
      Bitmap[] facilityImages,
      Bitmap upArrowImage,
      Bitmap crashImage,
      Bitmap[] troopImagesInfantry,
      Bitmap[] troopImagesArmored,
      Bitmap[] troopImagesArtillery,
      Bitmap[] troopImagesSpecialForces,
      Bitmap[] plagueImages)
    {
      this.ClearImages();
      this._ComponentImages = this.ScaleLimitImages(componentImages, this._ImageSize, this._ImageSize);
      this._HabitatTypeImages = this.ScaleLimitImages(habitatTypeImages, this._ImageSize, this._ImageSize);
      this._RaceImages = this.ScaleLimitImages(raceImages, this._ImageSize, this._ImageSize);
      this._FacilityImages = this.ScaleLimitImages(facilityImages, this._ImageSize, this._ImageSize);
      this._PlagueImages = this.ScaleLimitImages(plagueImages, this._ImageSize, this._ImageSize);
      this._TroopImagesInfantry = this.ScaleLimitImages(troopImagesInfantry, this._ImageSize, this._ImageSize);
      this._TroopImagesArmored = this.ScaleLimitImages(troopImagesArmored, this._ImageSize, this._ImageSize);
      this._TroopImagesArtillery = this.ScaleLimitImages(troopImagesArtillery, this._ImageSize, this._ImageSize);
      this._TroopImagesSpecialForces = this.ScaleLimitImages(troopImagesSpecialForces, this._ImageSize, this._ImageSize);
      this._UpArrowImage = new Bitmap((Image) upArrowImage);
      this._CrashImage = new Bitmap((Image) crashImage);
      this._FighterImages = new Bitmap[fighterImages.Length];
      for (int index = 0; index < fighterImages.Length; ++index)
      {
        Bitmap bitmap = this.ScaleLimitImage(fighterImages[index], this._ImageSize, this._ImageSize);
        bitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
        this._FighterImages[index] = bitmap;
      }
    }

    public void BindData(
      Galaxy galaxy,
      Empire empire,
      ResearchSystem researchSystem,
      IndustryType industry,
      int containerWidth,
      int containerHeight,
      ScrollableControl container,
      ResearchNode highlightedNode,
      Bitmap carrierImage,
      Bitmap resupplyShipImage,
      Cursor defaultMouseCursor)
    {
      this._Timer.Stop();
      this.ClearImageLists();
      this._HighlightedNode = highlightedNode;
      this._Galaxy = galaxy;
      this._Empire = empire;
      this._Research = researchSystem;
      this._Industry = industry;
      this._ContainerWidth = containerWidth;
      this._ContainerHeight = containerHeight;
      this._Container = container;
      if (this._Font == null)
      {
        this._Font = new Font(this.Font.FontFamily, 17f, FontStyle.Regular, GraphicsUnit.Pixel);
        this._NormalFont = new Font(this.Font.FontFamily, 18f, FontStyle.Regular, GraphicsUnit.Pixel);
        this._NormalFontBold = new Font(this.Font.FontFamily, 18f, FontStyle.Bold, GraphicsUnit.Pixel);
        this._LargeFont = new Font(this.Font.FontFamily, 20f, FontStyle.Regular, GraphicsUnit.Pixel);
        this._LargeFontBold = new Font(this.Font.FontFamily, 20f, FontStyle.Bold, GraphicsUnit.Pixel);
        this._ProjectTitleFont = new Font(this.Font.FontFamily, 14f, FontStyle.Bold, GraphicsUnit.Pixel);
        this._ProjectDetailFont = new Font(this.Font.FontFamily, 13f, FontStyle.Regular, GraphicsUnit.Pixel);
        this._FadedDotPen = new Pen(Color.FromArgb(170, 170, 170), 1f);
        this._FadedDotPen.DashStyle = DashStyle.Dot;
      }
      if (this._CarrierImage != null)
      {
        this._CarrierImage.Dispose();
        this._CarrierImage = (Bitmap) null;
      }
      this._CarrierImage = this.ScaleLimitImage(carrierImage, this._ImageSize, this._ImageSize);
      this._CarrierImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
      if (this._ResupplyShipImage != null)
      {
        this._ResupplyShipImage.Dispose();
        this._ResupplyShipImage = (Bitmap) null;
      }
      this._ResupplyShipImage = this.ScaleLimitImage(resupplyShipImage, this._ImageSize, this._ImageSize);
      this._ResupplyShipImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
      this._DefaultMouseCursor = defaultMouseCursor;
      this._NodeWidth = 175;
      this._NodeHeight = 52;
      this._LowestTechLevel = int.MaxValue;
      this._HighestTechLevel = 0;
      this._LowestRow = int.MaxValue;
      this._HighestRow = 0;
      this.DetermineRanges(industry, out this._LowestTechLevel, out this._HighestTechLevel, out this._LowestRow, out this._HighestRow);
      this.SizeControlToTreeContents();
      base.BackColor = Color.FromArgb(0, 0, 20);
      this.BackColor = Color.FromArgb(20, 20, 40);
      this.ForeColor = Color.FromArgb(120, 120, 120);
      this._TextColor = this.ForeColor;
      this._TextColor2 = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.OuterBorderColor = Color.FromArgb(0, 0, 20);
      this.InnerBorderColor = Color.FromArgb(48, 48, 68);
      this.ShineColor = Color.FromArgb(128, 128, 148);
      this.GlowColor = Color.FromArgb(80, 80, 160);
      this.Focus();
      Point position = this.CalculatePosition(this._HighlightedNode, this._LowestTechLevel, this._HighestTechLevel, this._LowestRow, this._HighestRow);
      this._Container.AutoScrollPosition = new Point(Math.Abs(position.X), Math.Abs(position.Y));
      this.Invalidate();
      this._Timer.Start();
    }

    public Point CalculatePosition(
      ResearchNode highlightedNode,
      int lowestTechLevel,
      int highestTechLevel,
      int lowestRow,
      int highestRow)
    {
      ResearchNode node = this.ResolveCurrentProject(this._Industry);
      if (highlightedNode != null)
        node = highlightedNode;
      if (node == null)
        return new Point(0, 0);
      Rectangle nodeRectangle = this.CalculateNodeRectangle(node, lowestTechLevel, highestTechLevel, lowestRow, highestRow);
      int val2_1 = nodeRectangle.X + nodeRectangle.Width / 2 - this._ContainerWidth / 2;
      int val2_2 = nodeRectangle.Y + nodeRectangle.Height / 2 - this._ContainerHeight / 2;
      return new Point(Math.Min(this._Container.HorizontalScroll.Maximum, Math.Max(0, val2_1)), Math.Min(this._Container.VerticalScroll.Maximum, Math.Max(0, val2_2)));
    }

    public ResearchNode ResolveCurrentProject(IndustryType industry)
    {
      ResearchNodeList researchNodeList = (ResearchNodeList) null;
      switch (industry)
      {
        case IndustryType.Weapon:
          researchNodeList = this._Empire.Research.ResearchQueueWeapons;
          break;
        case IndustryType.Energy:
          researchNodeList = this._Empire.Research.ResearchQueueEnergy;
          break;
        case IndustryType.HighTech:
          researchNodeList = this._Empire.Research.ResearchQueueHighTech;
          break;
      }
      return researchNodeList != null && researchNodeList.Count > 0 ? researchNodeList[0] : (ResearchNode) null;
    }

    private Bitmap[] ScaleLimitImages(Bitmap[] images, int maxWidth, int maxHeight)
    {
      Bitmap[] bitmapArray = new Bitmap[images.Length];
      for (int index = 0; index < images.Length; ++index)
        bitmapArray[index] = this.ScaleLimitImage(images[index], 28, 28);
      return bitmapArray;
    }

    private Bitmap ScaleLimitImage(Bitmap image, int maxWidth, int maxHeight)
    {
      double num = Math.Min((double) maxWidth / (double) image.Width, (double) maxHeight / (double) image.Height);
      Bitmap bitmap;
      if (num < 1.0)
      {
        bitmap = new Bitmap((int) ((double) image.Width * num), (int) ((double) image.Height * num), PixelFormat.Format32bppPArgb);
        using (Graphics graphics = Graphics.FromImage((Image) bitmap))
        {
          graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
          Rectangle srcRect = new Rectangle(0, 0, image.Width, image.Height);
          Rectangle destRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
          graphics.DrawImage((Image) image, destRect, srcRect, GraphicsUnit.Pixel);
        }
      }
      else
        bitmap = new Bitmap((Image) image);
      return bitmap;
    }

    private Bitmap TransparentImage(Bitmap image, float alphaLevel)
    {
      Bitmap bitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppPArgb);
      using (Graphics graphics = Graphics.FromImage((Image) bitmap))
      {
        this.SetGraphicsQualityToLow(graphics);
        ImageAttributes attributesTransparency = this.CalculateImageAttributesTransparency(alphaLevel);
        graphics.DrawImage((Image) image, new Rectangle(0, 0, bitmap.Width, bitmap.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributesTransparency);
      }
      return bitmap;
    }

    private ImageAttributes CalculateImageAttributesTransparency(float alphaLevel)
    {
      ColorMatrix newColorMatrix = new ColorMatrix(new float[5][]
      {
        new float[5]{ 1f, 0.0f, 0.0f, 0.0f, 0.0f },
        new float[5]{ 0.0f, 1f, 0.0f, 0.0f, 0.0f },
        new float[5]{ 0.0f, 0.0f, 1f, 0.0f, 0.0f },
        new float[5]{ 0.0f, 0.0f, 0.0f, alphaLevel, 0.0f },
        new float[5]{ 0.0f, 0.0f, 0.0f, 0.0f, 1f }
      });
      ImageAttributes attributesTransparency = new ImageAttributes();
      attributesTransparency.SetColorMatrix(newColorMatrix);
      return attributesTransparency;
    }

    private void SizeControlToTreeContents()
    {
      this.Size = new Size(this._Margin + (this._HighestTechLevel - this._LowestTechLevel + 1) * (this._NodeWidth + this._GapWidth), this._Margin + this._Margin + (this._HighestRow - this._LowestRow + 1) * (this._NodeHeight + this._GapHeight));
      this.Invalidate();
    }

    private void DetermineRanges(
      IndustryType industry,
      out int lowestTechLevel,
      out int highestTechLevel,
      out int lowestRow,
      out int highestRow)
    {
      lowestTechLevel = int.MaxValue;
      highestTechLevel = 0;
      lowestRow = int.MaxValue;
      highestRow = 0;
      for (int index = 0; index < this._Research.TechTree.Count; ++index)
      {
        if (this._Research.TechTree[index].Industry == industry && this._Research.TechTree[index].TechLevel < 100)
        {
          if (this._Research.TechTree[index].TechLevel < lowestTechLevel)
            lowestTechLevel = this._Research.TechTree[index].TechLevel;
          if (this._Research.TechTree[index].TechLevel > highestTechLevel)
            highestTechLevel = this._Research.TechTree[index].TechLevel;
          if (this._Research.TechTree[index].Row < lowestRow)
            lowestRow = this._Research.TechTree[index].Row;
          if (this._Research.TechTree[index].Row > highestRow)
            highestRow = this._Research.TechTree[index].Row;
        }
      }
    }

    protected override Point ScrollToControl(Control activeControl) => this.AutoScrollPosition;

    protected override void OnPaint(PaintEventArgs e)
    {
      try
      {
        Color color = Color.Black;
        switch (this._Industry)
        {
          case IndustryType.Weapon:
            color = Color.FromArgb(28, 0, 0);
            break;
          case IndustryType.Energy:
            color = Color.FromArgb(0, 0, 28);
            break;
          case IndustryType.HighTech:
            color = Color.FromArgb(0, 28, 0);
            break;
        }
        e.Graphics.Clear(color);
        e.Graphics.CompositingQuality = CompositingQuality.HighSpeed;
        e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
        e.Graphics.PixelOffsetMode = PixelOffsetMode.None;
        e.Graphics.SmoothingMode = SmoothingMode.None;
        ResearchNode hoveredNode = this.DetectHoveredNode(this._LowestTechLevel, this._HighestTechLevel, this._LowestRow, this._HighestRow);
        this.DrawNodePaths(e.Graphics, this._LowestTechLevel, this._HighestTechLevel, this._LowestRow, this._HighestRow);
        this.DrawTree(e.Graphics, hoveredNode, this._LowestTechLevel, this._HighestTechLevel, this._LowestRow, this._HighestRow);
      }
      catch (Exception ex)
      {
      }
    }

    private ResearchNode DetectHoveredNode(
      int lowestTechLevel,
      int highestTechLevel,
      int lowestRow,
      int highestRow)
    {
      if (this._Research != null && this._Research.TechTree != null)
      {
        Point client = this.PointToClient(MouseHelper.GetCursorPosition());
        int num1 = (int) ((double) client.X * this.Zoom);
        int num2 = (int) ((double) client.Y * this.Zoom);
        int num3 = num1 - this._Margin;
        int num4 = num2 - this._Margin;
        int num5 = -1;
        int num6 = -1;
        double num7 = (double) this._NodeWidth / (double) (this._NodeWidth + this._GapWidth);
        double num8 = (double) num3 / (double) (this._NodeWidth + this._GapWidth);
        double num9 = num8 - (double) (int) num8;
        double num10 = (double) this._NodeHeight / (double) (this._NodeHeight + this._GapHeight);
        double num11 = (double) num4 / (double) (this._NodeHeight + this._GapHeight);
        double num12 = num11 - (double) (int) num11;
        if (num9 < num7 && num12 < num10)
        {
          num5 = num3 / (this._NodeWidth + this._GapWidth);
          int num13 = num4 / (this._NodeHeight + this._GapHeight);
          if (lowestTechLevel == 1)
            ++num5;
          num6 = num13 + 1;
        }
        if (num6 >= 0 && num5 >= 0)
        {
          for (int index = 0; index < this._Research.TechTree.Count; ++index)
          {
            if (this._Research.TechTree[index].Industry == this._Industry && this._Research.TechTree[index].TechLevel == num5 && this._Research.TechTree[index].Row == num6)
              return this._Research.TechTree[index];
          }
        }
      }
      return (ResearchNode) null;
    }

    private void DrawProjectInfo(
      Graphics graphics,
      ResearchNode project,
      Point location,
      int researchQueueIndex)
    {
      if (project == null)
        return;
      float num1 = 20f;
      float num2 = 10f;
      float num3 = 15f;
      float num4 = (float) location.X + 5f;
      float y1 = (float) location.Y + 5f;
      int height = (int) ((double) num1 * 14.0);
      if (researchQueueIndex >= 0)
        height += (int) num1;
      if (project.AllowedRaces != null && project.AllowedRaces.Count > 0)
        height += (int) num1;
      if (project.DisallowedRaces != null && project.DisallowedRaces.Count > 0)
        height += (int) num1;
      if (project.ComponentImprovements != null && project.ComponentImprovements.Count > 0)
        height += (int) ((double) num1 + (double) num2);
      if (project.PlanetaryFacility != null)
        height += (int) ((double) num1 + (double) num2);
      switch (project.ResolveComponentType())
      {
        case ComponentType.WeaponGravityBeam:
          height += (int) ((double) num1 * 2.0);
          break;
        case ComponentType.WeaponAreaGravity:
          height += (int) ((double) num1 * 4.5);
          break;
        case ComponentType.AssaultPod:
          height += (int) ((double) num1 * 3.0);
          break;
        case ComponentType.WeaponPhaser:
        case ComponentType.WeaponSuperPhaser:
          height += (int) ((double) num1 * 2.0);
          break;
        case ComponentType.WeaponRailGun:
        case ComponentType.WeaponSuperRailGun:
          height += (int) ((double) num1 * 3.0);
          break;
      }
      int num5 = project.CountRequiredParents();
      if (num5 > 0)
      {
        switch (num5)
        {
          case 1:
          case 2:
            height += (int) ((double) num1 + (double) num2);
            break;
          case 3:
            height += (int) ((double) num1 * 3.0);
            break;
          case 4:
          case 5:
            height += (int) ((double) num1 * 4.0);
            break;
          default:
            height += (int) ((double) num1 * 5.0);
            break;
        }
      }
      int benefitCount = project.BenefitCount;
      int num6 = (int) ((double) this._NodeWidth * 1.6);
      float num7 = 135f;
      if (benefitCount > 4)
      {
        num6 = (int) ((double) this._NodeWidth * 1.2);
        num7 = 85f;
      }
      else if (benefitCount > 3)
      {
        num6 = (int) ((double) this._NodeWidth * 1.35);
        num7 = 100f;
      }
      int width = Math.Max(num6, benefitCount * num6);
      Rectangle rect = new Rectangle(location.X, location.Y, width, height);
      graphics.FillRectangle((Brush) this._BlackTransparentBrush, rect);
      graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
      graphics.DrawString(project.Name, this._LargeFontBold, (Brush) this._WhiteBrush, new PointF(num4, y1), StringFormat.GenericTypographic);
      float y2 = y1 + num1;
      bool flag1 = true;
      if (project.AllowedRaces != null && project.AllowedRaces.Count > 0)
      {
        flag1 = false;
        string str1 = string.Empty;
        for (int index = 0; index < project.AllowedRaces.Count; ++index)
        {
          if (project.AllowedRaces[index] == this._Empire.DominantRace)
            flag1 = true;
          str1 = str1 + project.AllowedRaces[index].Name + ", ";
        }
        if (!string.IsNullOrEmpty(str1) && str1.Length >= 2)
          str1 = str1.Substring(0, str1.Length - 2);
        string str2 = "(" + string.Format(TextResolver.GetText("RACE only"), (object) str1) + ")";
        graphics.DrawString(str2.ToUpper(CultureInfo.InvariantCulture), this._LargeFont, (Brush) this._UnresearchableBrush, new PointF(num4, y2), StringFormat.GenericTypographic);
        y2 += num1;
      }
      if (project.DisallowedRaces != null && project.DisallowedRaces.Count > 0)
      {
        flag1 = true;
        string str3 = string.Empty;
        for (int index = 0; index < project.DisallowedRaces.Count; ++index)
        {
          if (project.DisallowedRaces[index] == this._Empire.DominantRace)
            flag1 = false;
          str3 = str3 + project.DisallowedRaces[index].Name + ", ";
        }
        if (!string.IsNullOrEmpty(str3) && str3.Length >= 2)
          str3 = str3.Substring(0, str3.Length - 2);
        string str4 = "(" + string.Format(TextResolver.GetText("Not RACE"), (object) str3) + ")";
        graphics.DrawString(str4.ToUpper(CultureInfo.InvariantCulture), this._LargeFont, (Brush) this._UnresearchableBrush, new PointF(num4, y2), StringFormat.GenericTypographic);
        y2 += num1;
      }
      if (flag1)
      {
        if (!project.IsResearched && researchQueueIndex < 0)
        {
          if (this._Research.CanResearchNode(project))
          {
            graphics.DrawString("(" + TextResolver.GetText("Click to queue research") + ")", this._LargeFont, (Brush) this._WhiteBrush, new PointF(num4, y2), StringFormat.GenericTypographic);
            y2 += num1;
          }
          else
          {
            bool flag2 = false;
            if (project.ParentNodes != null && project.ParentNodes.Count > 0)
            {
              string str5 = string.Empty;
              for (int index = 0; index < project.ParentNodes.Count; ++index)
              {
                if (project.ParentIsRequired[index])
                  str5 = str5 + project.ParentNodes[index].Name + " + ";
              }
              if (!string.IsNullOrEmpty(str5))
              {
                flag2 = true;
                string str6 = str5.Substring(0, str5.Length - 3);
                string str7 = string.Format(TextResolver.GetText("Must first research PROJECT"), (object) str6);
                StringFormat format = new StringFormat(StringFormatFlags.FitBlackBox);
                format.Trimming = StringTrimming.EllipsisCharacter;
                SizeF sizeF = graphics.MeasureString(str7, this._NormalFontBold, num6 - 10, format);
                RectangleF layoutRectangle = new RectangleF(num4, y2, sizeF.Width + 3f, sizeF.Height + 3f);
                graphics.DrawString(str7, this._NormalFontBold, (Brush) this._RedBrush, layoutRectangle, format);
                y2 += sizeF.Height;
              }
            }
            if (!project.IsEnabled)
            {
              string str = string.Empty;
              switch (project.SpecialFunctionCode)
              {
                case 2:
                  str = TextResolver.GetText("Project is disabled. Must enable through exploration");
                  break;
                case 5:
                  str = TextResolver.GetText("Project is disabled. Must enable through game event");
                  break;
              }
              StringFormat format = new StringFormat(StringFormatFlags.FitBlackBox);
              format.Trimming = StringTrimming.EllipsisCharacter;
              SizeF sizeF = graphics.MeasureString(str, this._NormalFontBold, num6 - 10, format);
              RectangleF layoutRectangle = new RectangleF(num4, y2, sizeF.Width + 3f, sizeF.Height + 3f);
              graphics.DrawString(str, this._NormalFontBold, (Brush) this._RedBrush, layoutRectangle, format);
              y2 += sizeF.Height;
            }
            else if (!flag2)
            {
              graphics.DrawString(TextResolver.GetText("Must first research preceding project"), this._NormalFontBold, (Brush) this._RedBrush, new PointF(num4, y2), StringFormat.GenericTypographic);
              y2 += num3;
            }
          }
        }
        else if (!project.IsResearched && researchQueueIndex >= 0)
        {
          if (project.IsRushing)
          {
            graphics.DrawString("(" + TextResolver.GetText("Cannot cancel crash programs") + ")", this._LargeFont, (Brush) this._RedBrush, new PointF(num4, y2), StringFormat.GenericTypographic);
            y2 += num1;
          }
          else
          {
            graphics.DrawString("(" + TextResolver.GetText("Right-click to cancel") + ")", this._LargeFont, (Brush) this._WhiteBrush, new PointF(num4, y2), StringFormat.GenericTypographic);
            y2 += num1;
          }
        }
      }
      if (project.IsRushing && !project.IsResearched)
      {
        graphics.DrawString(TextResolver.GetText("CRASH  RESEARCH  (3x speed)"), this._LargeFont, (Brush) this._WhiteBrush, new PointF(num4, y2), StringFormat.GenericTypographic);
        y2 += num1;
      }
      else if (researchQueueIndex == 0)
      {
        graphics.DrawString("(" + TextResolver.GetText("Click to initiate Crash Program") + ")", this._LargeFont, (Brush) this._WhiteBrush, new PointF(num4, y2), StringFormat.GenericTypographic);
        y2 += num1;
      }
      float y3 = y2 + 5f;
      string s = TextResolver.GetText("Project Size") + ": " + project.Cost.ToString("0,K");
      if (project.IsResearched)
        s = s + "   (" + TextResolver.GetText("Completed").ToUpper(CultureInfo.InvariantCulture) + ")";
      else if ((double) project.Progress > 0.0)
      {
        float num8 = (float) ((double) project.Progress / (double) project.Cost * 100.0);
        s = s + "   (" + num8.ToString("0") + "% " + TextResolver.GetText("Complete").ToLower(CultureInfo.InvariantCulture) + ")";
      }
      else if (researchQueueIndex > 0)
        s = s + "   (" + string.Format(TextResolver.GetText("#X in queue"), (object) (researchQueueIndex + 1).ToString()) + ")";
      if (project.IsResearched)
        graphics.DrawString(s, this._LargeFont, (Brush) this._UnresearchableBrush, new PointF(num4, y3), StringFormat.GenericTypographic);
      else
        graphics.DrawString(s, this._LargeFont, (Brush) this._WhiteBrush, new PointF(num4, y3), StringFormat.GenericTypographic);
      float num9 = y3 + num1;
      float num10 = 150f;
      List<string[]> allDescriptions;
      List<string[]> allValues;
      this._Galaxy.GenerateBenefitDetail(project, this._Empire, out allDescriptions, out allValues);
      graphics.DrawLine(this._FadedDotPen, num4, num9, (float) ((double) num4 + (double) (num6 * allDescriptions.Count) - 10.0), num9);
      float num11 = num9 + 5f;
      for (int index1 = 0; index1 < allDescriptions.Count; ++index1)
      {
        string[] strArray1 = allDescriptions[index1];
        string[] strArray2 = allValues[index1];
        float x = num4 + (float) (index1 * num6);
        float num12 = (float) num6 - num7;
        float num13 = num11;
        if (index1 < allDescriptions.Count - 1 && allDescriptions.Count > 1)
          graphics.DrawLine(this._FadedDotPen, (float) ((double) x + (double) num6 - 3.0), num13, (float) ((double) x + (double) num6 - 3.0), num13 + num10);
        StringFormat format1 = new StringFormat(StringFormatFlags.FitBlackBox);
        format1.Trimming = StringTrimming.EllipsisCharacter;
        SizeF sizeF1 = graphics.MeasureString(strArray1[0], this._NormalFontBold, num6 - 10, format1);
        RectangleF layoutRectangle1 = new RectangleF(x, num13, sizeF1.Width + 3f, sizeF1.Height + 3f);
        graphics.DrawString(strArray1[0], this._NormalFontBold, (Brush) this._WhiteBrush, layoutRectangle1, format1);
        float y4 = num13 + sizeF1.Height;
        if (strArray1.Length > 1)
        {
          for (int index2 = 1; index2 < strArray1.Length && !string.IsNullOrEmpty(strArray1[index2]); ++index2)
          {
            if (string.IsNullOrEmpty(strArray2[index2]))
            {
              float y5 = y4 + 5f;
              StringFormat format2 = new StringFormat(StringFormatFlags.FitBlackBox);
              format2.Trimming = StringTrimming.EllipsisCharacter;
              SizeF sizeF2 = graphics.MeasureString(strArray1[index2], this._NormalFont, num6 - 10, format2);
              RectangleF layoutRectangle2 = new RectangleF(x, y5, sizeF2.Width + 3f, sizeF2.Height + 3f);
              graphics.DrawString(strArray1[index2], this._NormalFont, (Brush) this._WhiteBrush, layoutRectangle2, format2);
              y4 = y5 + sizeF2.Height;
            }
            else
            {
              SizeF sizeF3 = graphics.MeasureString(strArray1[index2], this._NormalFont, num6, StringFormat.GenericTypographic);
              float num14 = num12 - sizeF3.Width;
              graphics.DrawString(strArray1[index2], this._NormalFont, (Brush) this._WhiteBrush, new PointF((float) ((double) x + (double) num14 - 5.0), y4), StringFormat.GenericTypographic);
              graphics.DrawString(strArray2[index2], this._NormalFontBold, (Brush) this._WhiteBrush, new PointF(x + num12, y4), StringFormat.GenericTypographic);
              y4 += sizeF3.Height - 1f;
            }
          }
        }
      }
    }

    public Rectangle CalculateNodeRectangle(
      ResearchNode node,
      int lowestTechLevel,
      int highestTechLevel,
      int lowestRow,
      int highestRow)
    {
      int num1 = Math.Max(0, node.TechLevel);
      if (lowestTechLevel == 1)
        num1 = Math.Max(0, node.TechLevel - 1);
      int num2 = Math.Max(0, node.Row - 1);
      return new Rectangle((int) ((double) this._Margin + (double) (num1 * (this._NodeWidth + this._GapWidth)) / this.Zoom), (int) ((double) this._Margin + (double) (num2 * (this._NodeHeight + this._GapHeight)) / this.Zoom), (int) ((double) this._NodeWidth / this.Zoom), (int) ((double) this._NodeHeight / this.Zoom));
    }

    private void DrawNodeComplete(
      Graphics graphics,
      ResearchNode researchNode,
      ResearchNode hoveredNode,
      Font normalFont,
      Font headingFont,
      int lowestTechLevel,
      int highestTechLevel,
      int lowestRow,
      int highestRow)
    {
      Rectangle nodeRectangle = this.CalculateNodeRectangle(researchNode, lowestTechLevel, highestTechLevel, lowestRow, highestRow);
      bool isHovered = false;
      if (researchNode == hoveredNode)
        isHovered = true;
      int researchQueueIndex = -1;
      switch (this._Industry)
      {
        case IndustryType.Weapon:
          researchQueueIndex = this._Research.ResearchQueueWeapons.IndexOf(researchNode);
          break;
        case IndustryType.Energy:
          researchQueueIndex = this._Research.ResearchQueueEnergy.IndexOf(researchNode);
          break;
        case IndustryType.HighTech:
          researchQueueIndex = this._Research.ResearchQueueHighTech.IndexOf(researchNode);
          break;
      }
      this.DrawNode(graphics, researchNode, nodeRectangle.X, nodeRectangle.Y, nodeRectangle.Width, nodeRectangle.Height, headingFont, normalFont, isHovered, researchQueueIndex);
    }

    private void DrawTree(
      Graphics graphics,
      ResearchNode hoveredNode,
      int lowestTechLevel,
      int highestTechLevel,
      int lowestRow,
      int highestRow)
    {
      if (this._Research == null || this._Research.TechTree == null)
        return;
      double num1 = (double) this._NodeWidth / this.Zoom;
      int num2 = (int) ((double) this._NodeHeight / this.Zoom);
      float num3 = 20f;
      float num4 = 10f;
      int num5 = this.Left * -1 + this._ContainerWidth;
      int num6 = this.Left * -1;
      int num7 = this.Top * -1 + this._ContainerHeight;
      int y1 = this.Top * -1;
      Rectangle visibleBounds = new Rectangle(num6, y1, num5 - num6, num7 - y1);
      for (int index = 0; index < this._Research.TechTree.Count; ++index)
      {
        if (this._Research.TechTree[index].Industry == this._Industry && this.DetermineNodeVisible(this._Research.TechTree[index], visibleBounds, lowestTechLevel, highestTechLevel, lowestRow, highestRow))
          this.DrawNodeComplete(graphics, this._Research.TechTree[index], hoveredNode, this._ProjectDetailFont, this._ProjectTitleFont, lowestTechLevel, highestTechLevel, lowestRow, highestRow);
      }
      if (hoveredNode == null)
        return;
      int num8 = Math.Max(0, hoveredNode.TechLevel);
      if (lowestTechLevel == 1)
        num8 = Math.Max(0, hoveredNode.TechLevel - 1);
      int num9 = Math.Max(0, hoveredNode.Row - 1);
      int num10 = (int) ((double) this._Margin + (double) (num8 * (this._NodeWidth + this._GapWidth)) / this.Zoom);
      int num11 = (int) ((double) this._Margin + (double) (num9 * (this._NodeHeight + this._GapHeight)) / this.Zoom);
      int num12 = (int) ((double) this._NodeWidth * 1.6);
      int val2 = num10 - (num12 - this._NodeWidth) / 2;
      int benefitCount = hoveredNode.BenefitCount;
      if (benefitCount > 1)
      {
        int num13 = val2 + benefitCount * num12 - num5;
        if (num13 > 0)
          val2 -= num13;
      }
      int x = Math.Max(num6, val2);
      int researchQueueIndex = -1;
      switch (this._Industry)
      {
        case IndustryType.Weapon:
          researchQueueIndex = this._Research.ResearchQueueWeapons.IndexOf(hoveredNode);
          break;
        case IndustryType.Energy:
          researchQueueIndex = this._Research.ResearchQueueEnergy.IndexOf(hoveredNode);
          break;
        case IndustryType.HighTech:
          researchQueueIndex = this._Research.ResearchQueueHighTech.IndexOf(hoveredNode);
          break;
      }
      int y2 = num11 + this._NodeHeight;
      int num14 = (int) ((double) num3 * 14.0);
      if (researchQueueIndex >= 0)
        num14 += (int) num3;
      if (hoveredNode.AllowedRaces != null && hoveredNode.AllowedRaces.Count > 0)
        num14 += (int) num3;
      if (hoveredNode.DisallowedRaces != null && hoveredNode.DisallowedRaces.Count > 0)
        num14 += (int) num3;
      if (hoveredNode.ComponentImprovements != null && hoveredNode.ComponentImprovements.Count > 0)
        num14 += (int) ((double) num3 + (double) num4);
      if (hoveredNode.PlanetaryFacility != null)
        num14 += (int) ((double) num3 + (double) num4);
      switch (hoveredNode.ResolveComponentType())
      {
        case ComponentType.WeaponGravityBeam:
          num14 += (int) ((double) num3 * 2.0);
          break;
        case ComponentType.WeaponAreaGravity:
          num14 += (int) ((double) num3 * 4.5);
          break;
        case ComponentType.AssaultPod:
          num14 += (int) ((double) num3 * 3.0);
          break;
        case ComponentType.WeaponPhaser:
        case ComponentType.WeaponSuperPhaser:
          num14 += (int) ((double) num3 * 2.0);
          break;
        case ComponentType.WeaponRailGun:
        case ComponentType.WeaponSuperRailGun:
          num14 += (int) ((double) num3 * 3.0);
          break;
      }
      int num15 = hoveredNode.CountRequiredParents();
      if (num15 > 0)
      {
        switch (num15)
        {
          case 1:
          case 2:
            num14 += (int) ((double) num3 + (double) num4);
            break;
          case 3:
            num14 += (int) ((double) num3 * 3.0);
            break;
          case 4:
          case 5:
            num14 += (int) ((double) num3 * 4.0);
            break;
          default:
            num14 += (int) ((double) num3 * 5.0);
            break;
        }
      }
      int num16 = num7 - 50;
      if (y2 > num16 - num14)
        y2 -= num2 + num14;
      this.DrawProjectInfo(graphics, hoveredNode, new Point(x, y2), researchQueueIndex);
    }

    private void DrawNodePaths(
      Graphics graphics,
      int lowestTechLevel,
      int highestTechLevel,
      int lowestRow,
      int highestRow)
    {
      if (this._Research == null || this._Research.TechTree == null)
        return;
      graphics.SmoothingMode = SmoothingMode.AntiAlias;
      int num1 = (int) ((double) this._NodeHeight / this.Zoom);
      double num2 = 15.0 / this.Zoom;
      double num3 = (double) num1 - num2 * 2.0;
      for (int index1 = 0; index1 < this._Research.TechTree.Count; ++index1)
      {
        if (this._Research.TechTree[index1].Industry == this._Industry && this._Research.TechTree[index1].ParentNodes != null && this._Research.TechTree[index1].ParentNodes.Count > 0)
        {
          Point nodePathDefaultEnd = this.CalculateNodePathDefaultEnd(this._Research.TechTree[index1], lowestTechLevel, highestTechLevel, lowestRow, highestRow);
          for (int index2 = 0; index2 < this._Research.TechTree[index1].ParentNodes.Count; ++index2)
          {
            Point nodePathStart = this.CalculateNodePathStart(this._Research.TechTree[index1].ParentNodes[index2], lowestTechLevel, highestTechLevel, lowestRow, highestRow);
            Point pt2 = nodePathDefaultEnd;
            if (this._Research.TechTree[index1].ParentNodes.Count > 1)
            {
              Point nodeLocation = this.CalculateNodeLocation(this._Research.TechTree[index1], lowestTechLevel, highestTechLevel, lowestRow, highestRow);
              int num4 = (int) ((double) index2 / (double) (this._Research.TechTree[index1].ParentNodes.Count - 1) * num3);
              pt2 = new Point(nodeLocation.X, nodeLocation.Y + (int) (num2 + (double) num4));
            }
            bool flag1 = true;
            if (this._Research.TechTree[index1].AllowedRaces != null && this._Research.TechTree[index1].AllowedRaces.Count > 0)
            {
              flag1 = false;
              for (int index3 = 0; index3 < this._Research.TechTree[index1].AllowedRaces.Count; ++index3)
              {
                if (this._Research.TechTree[index1].AllowedRaces[index3] == this._Empire.DominantRace)
                  flag1 = true;
              }
            }
            if (this._Research.TechTree[index1].DisallowedRaces != null && this._Research.TechTree[index1].DisallowedRaces.Count > 0)
            {
              for (int index4 = 0; index4 < this._Research.TechTree[index1].DisallowedRaces.Count; ++index4)
              {
                if (this._Research.TechTree[index1].DisallowedRaces[index4] == this._Empire.DominantRace)
                  flag1 = false;
              }
            }
            Color color = Color.FromArgb(170, 170, 170);
            bool flag2 = false;
            if (this._Research.TechTree[index1].ParentIsRequired[index2])
            {
              if (this._Research.TechTree[index1].Category != this._Research.TechTree[index1].ParentNodes[index2].Category)
                flag2 = true;
              else if (this._Research.TechTree[index1].Name.ToLower(CultureInfo.InvariantCulture).Contains(TextResolver.GetText("Colonization").ToLower(CultureInfo.InvariantCulture)) && !this._Research.TechTree[index1].ParentNodes[index2].Name.ToLower(CultureInfo.InvariantCulture).Contains(TextResolver.GetText("Colonization").ToLower(CultureInfo.InvariantCulture)))
                flag2 = true;
              color = !this._Research.TechTree[index1].IsResearched || !flag1 ? Color.FromArgb(80, 0, 0) : Color.FromArgb((int) byte.MaxValue, 0, 0);
            }
            else if (!this._Research.TechTree[index1].IsResearched || !flag1)
              color = Color.FromArgb(56, 56, 56);
            using (Pen pen = new Pen(color))
            {
              pen.CustomEndCap = (CustomLineCap) this._ArrowCapLarge;
              pen.Width = 4f;
              if (flag2)
                pen.DashStyle = DashStyle.Dash;
              graphics.DrawLine(pen, nodePathStart, pt2);
            }
          }
        }
      }
      graphics.SmoothingMode = SmoothingMode.None;
    }

    private Point CalculateNodePathStart(
      ResearchNode node,
      int lowestTechLevel,
      int highestTechLevel,
      int lowestRow,
      int highestRow)
    {
      Point nodeLocation = this.CalculateNodeLocation(node, lowestTechLevel, highestTechLevel, lowestRow, highestRow);
      int num1 = (int) ((double) this._NodeWidth / this.Zoom);
      int num2 = (int) ((double) this._NodeHeight / this.Zoom);
      return new Point(nodeLocation.X + num1, nodeLocation.Y + num2 / 2);
    }

    private Point CalculateNodePathDefaultEnd(
      ResearchNode node,
      int lowestTechLevel,
      int highestTechLevel,
      int lowestRow,
      int highestRow)
    {
      Point nodeLocation = this.CalculateNodeLocation(node, lowestTechLevel, highestTechLevel, lowestRow, highestRow);
      double num1 = (double) this._NodeWidth / this.Zoom;
      int num2 = (int) ((double) this._NodeHeight / this.Zoom);
      return new Point(nodeLocation.X, nodeLocation.Y + num2 / 2);
    }

    private Point CalculateNodeLocation(
      ResearchNode node,
      int lowestTechLevel,
      int highestTechLevel,
      int lowestRow,
      int highestRow)
    {
      int num1 = Math.Max(0, node.TechLevel);
      if (lowestTechLevel == 1)
        num1 = Math.Max(0, node.TechLevel - 1);
      int num2 = Math.Max(0, node.Row - 1);
      return new Point((int) ((double) this._Margin + (double) (num1 * (this._NodeWidth + this._GapWidth)) / this.Zoom), (int) ((double) this._Margin + (double) (num2 * (this._NodeHeight + this._GapHeight)) / this.Zoom));
    }

    private bool DetermineNodeVisible(
      ResearchNode node,
      Rectangle visibleBounds,
      int lowestTechLevel,
      int highestTechLevel,
      int lowestRow,
      int highestRow)
    {
      Point nodeLocation = this.CalculateNodeLocation(node, lowestTechLevel, highestTechLevel, lowestRow, highestRow);
      int width = (int) ((double) this._NodeWidth / this.Zoom);
      int height = (int) ((double) this._NodeHeight / this.Zoom);
      Rectangle rect = new Rectangle(nodeLocation.X, nodeLocation.Y, width, height);
      return visibleBounds.IntersectsWith(rect);
    }

    internal MessageBoxEx GenerateMessageBoxExYesNo(string text, string title)
    {
      MessageBoxEx messageBox = MessageBoxExManager.CreateMessageBox((string) null, this._Font);
      text = text.Replace("\n", Environment.NewLine);
      messageBox.Text = text;
      messageBox.Caption = title;
      messageBox.AddButton(MessageBoxExButtons.Yes);
      messageBox.AddButton(MessageBoxExButtons.No);
      messageBox.Icon = MessageBoxExIcon.Question;
      return messageBox;
    }

    private MessageBoxEx GenerateMessageBoxExOk(string text, string title) => this.GenerateMessageBoxExOk(text, title, MessageBoxExIcon.Warning);

    private MessageBoxEx GenerateMessageBoxExOk(
      string text,
      string title,
      MessageBoxExIcon iconType)
    {
      MessageBoxEx messageBox = MessageBoxExManager.CreateMessageBox((string) null, this._Font);
      text = text.Replace("\n", Environment.NewLine);
      messageBox.Text = text;
      messageBox.Caption = title;
      messageBox.AddButton(MessageBoxExButtons.Ok);
      messageBox.Icon = iconType;
      return messageBox;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      Point autoScrollPosition = this._Container.AutoScrollPosition;
      Point point = new Point(Math.Abs(autoScrollPosition.X), Math.Abs(autoScrollPosition.Y));
      base.OnMouseDown(e);
      if (e.Button == MouseButtons.Left)
      {
        this._Dragging = true;
        this._MouseX = (float) e.X;
        this._MouseY = (float) e.Y;
      }
      this._Container.AutoScrollPosition = point;
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      if (this._Dragging)
        this._Dragging = false;
      Cursor.Current = this._DefaultMouseCursor;
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      if (this._Dragging)
      {
        Cursor.Current = Cursors.SizeAll;
        int num1 = this._Container.AutoScrollPosition.X * -1;
        int num2 = this._Container.AutoScrollPosition.Y * -1;
        int val2_1 = num1 - (int) ((double) e.X - (double) this._MouseX);
        int val2_2 = num2 - (int) ((double) e.Y - (double) this._MouseY);
        this._Container.AutoScrollPosition = new Point(Math.Min(this._Container.HorizontalScroll.Maximum, Math.Max(0, val2_1)), Math.Min(this._Container.VerticalScroll.Maximum, Math.Max(0, val2_2)));
      }
      else
        Cursor.Current = this._DefaultMouseCursor;
    }

    protected override void OnMouseClick(MouseEventArgs e)
    {
      base.OnMouseClick(e);
      ResearchNode researchNode = this.DetectHoveredNode(this._LowestTechLevel, this._HighestTechLevel, this._LowestRow, this._HighestRow);
      if (this.EditMode)
      {
        if (researchNode != null)
        {
          ResearchNodeList researchNodeList = new ResearchNodeList();
          switch (this._Industry)
          {
            case IndustryType.Weapon:
              researchNodeList = this._Research.ResearchQueueWeapons;
              break;
            case IndustryType.Energy:
              researchNodeList = this._Research.ResearchQueueEnergy;
              break;
            case IndustryType.HighTech:
              researchNodeList = this._Research.ResearchQueueHighTech;
              break;
          }
          if (e.Button == MouseButtons.Left)
          {
            researchNode.IsResearched = true;
            researchNode.IsRushing = false;
            researchNode.Progress = researchNode.Cost;
            if (researchNodeList.Contains(researchNode))
              researchNodeList.Remove(researchNode);
          }
          else if (e.Button == MouseButtons.Right)
          {
            researchNode.IsResearched = false;
            researchNode.IsRushing = false;
            researchNode.Progress = 0.0f;
            if (researchNodeList.Contains(researchNode))
              researchNodeList.Remove(researchNode);
          }
        }
      }
      else if (researchNode != null && !researchNode.IsResearched)
      {
        ResearchNodeList items = new ResearchNodeList();
        switch (this._Industry)
        {
          case IndustryType.Weapon:
            items = this._Research.ResearchQueueWeapons;
            break;
          case IndustryType.Energy:
            items = this._Research.ResearchQueueEnergy;
            break;
          case IndustryType.HighTech:
            items = this._Research.ResearchQueueHighTech;
            break;
        }
        if (e.Button == MouseButtons.Left && !items.Contains(researchNode))
        {
          if (this._Research.CheckNodeValidForRace(researchNode, this._Empire.DominantRace) && this._Research.CanResearchNode(researchNode))
            items.Add(researchNode);
        }
        else if (e.Button == MouseButtons.Left && items.Contains(researchNode))
        {
          if (items.IndexOf(researchNode) == 0 && !researchNode.IsRushing)
          {
            double researchProgramCost = Galaxy.CalculateCrashResearchProgramCost(this._Empire, researchNode);
            if (this._Empire.StateMoney >= researchProgramCost)
            {
              if (this.GenerateMessageBoxExYesNo(string.Format(TextResolver.GetText("Crash Research Initiate Question"), (object) researchNode.Name, (object) researchProgramCost.ToString("###,###,###,##0")), TextResolver.GetText("Initiate Crash Research Program?")).Show((IWin32Window) this).ToLower(CultureInfo.InvariantCulture) == "yes")
              {
                this._Empire.StateMoney -= researchProgramCost;
                this._Empire.PirateEconomy.PerformExpense(researchProgramCost, PirateExpenseType.CrashResearch, this._Galaxy.CurrentStarDate);
                researchNode.IsRushing = true;
              }
            }
            else
              this.GenerateMessageBoxExOk(string.Format(TextResolver.GetText("Crash Research Cannot Afford"), (object) researchProgramCost.ToString("###,###,###,##0"), (object) this._Empire.StateMoney.ToString("###,###,###,##0")), TextResolver.GetText("Not enough money for Crash Research Program"), MessageBoxExIcon.Stop).Show((IWin32Window) this);
          }
        }
        else if (e.Button == MouseButtons.Right && items.Contains(researchNode))
        {
          bool flag = true;
          if (researchNode.IsRushing)
            flag = false;
          if (flag)
          {
            int index1 = items.IndexOf(researchNode);
            items.RemoveAt(index1);
            ResearchNodeList researchNodeList = new ResearchNodeList();
            researchNodeList.AddRange((IEnumerable<ResearchNode>) items);
            if (index1 < researchNodeList.Count)
            {
              for (int index2 = index1; index2 < researchNodeList.Count; ++index2)
              {
                if (!this._Research.CanResearchNode(researchNodeList[index2]))
                  items.Remove(researchNodeList[index2]);
              }
            }
          }
        }
      }
      if (this.NodeClicked == null)
        return;
      this.NodeClicked((object) this, researchNode);
    }

    private ResearchNodeList RemoveInvalidNodesFromQueue(ResearchNodeList researchQueue)
    {
      ResearchNodeList researchNodeList = new ResearchNodeList();
      if (researchQueue.Count > 0)
      {
        for (int index1 = 0; index1 < researchQueue.Count; ++index1)
        {
          if (researchQueue[index1].ParentNodes != null && researchQueue[index1].ParentNodes.Count > 0)
          {
            bool flag = false;
            for (int index2 = 0; index2 < researchQueue[index1].ParentNodes.Count; ++index2)
            {
              if (researchQueue[index1].ParentIsRequired[index2] && !researchQueue.Contains(researchQueue[index1].ParentNodes[index2]) && !researchQueue[index1].ParentNodes[index2].IsResearched)
              {
                flag = false;
                break;
              }
              if ((researchQueue[index1].ParentNodes[index2].IsResearched || researchQueue.Contains(researchQueue[index1].ParentNodes[index2])) && !researchNodeList.Contains(researchQueue[index1].ParentNodes[index2]))
                flag = true;
            }
            if (!flag)
              researchNodeList.Add(researchQueue[index1]);
          }
        }
      }
      for (int index = 0; index < researchNodeList.Count; ++index)
        researchQueue.Remove(researchNodeList[index]);
      for (int index = 0; index < researchQueue.Count; ++index)
        researchQueue[index].SortTag = (float) researchQueue[index].TechLevel;
      researchQueue.Sort();
      return researchQueue;
    }

    private void DrawNode(
      Graphics graphics,
      ResearchNode researchNode,
      int x,
      int y,
      int width,
      int height,
      Font headingFont,
      Font font,
      bool isHovered,
      int researchQueueIndex)
    {
      if (researchNode.Components != null && researchNode.Components.Count > 0)
        this.ResolveCategoryColor(researchNode.Category, researchNode.Components[0]);
      else if (researchNode.ComponentImprovements != null && researchNode.ComponentImprovements.Count > 0)
        this.ResolveCategoryColor(researchNode.Category, researchNode.ComponentImprovements[0].ImprovedComponent);
      else
        this.ResolveCategoryColor(researchNode.Category, (DistantWorlds.Types.Component) null);
      bool flag1 = this._Research.CanResearchNode(researchNode);
      if (!flag1)
        isHovered = false;
      if (researchNode.IsResearched)
        isHovered = true;
      bool restricted = this._Research.CheckNodeValidForRace(researchNode, this._Empire.DominantRace);
      bool enabled = restricted;
      if (!researchNode.IsResearched && researchQueueIndex != 0)
        enabled = false;
      if (researchNode.IsResearched)
        enabled = true;
      if (!restricted && !researchNode.IsResearched)
        isHovered = false;
      bool ghosted = false;
      if (!researchNode.IsResearched && !flag1)
        ghosted = true;
      Bitmap bitmap1 = (Bitmap) null;
      if (researchQueueIndex == 0)
        isHovered = true;
      int index1 = this._NodeButtonColors.IndexOf(this.BackColor);
      if (index1 >= 0)
      {
        if (isHovered)
        {
          if (this._NodeButtonImagesGlowing[index1] == null)
          {
            bitmap1 = (Bitmap) this.CreateBackgroundFrame(false, isHovered, false, enabled, ghosted, restricted, 1f, new Rectangle(0, 0, width, height));
            this._NodeButtonImagesGlowing[index1] = bitmap1;
          }
          else
            bitmap1 = this._NodeButtonImagesGlowing[index1];
        }
        else if (!restricted)
        {
          if (this._NodeButtonImagesDullBlocked[index1] == null)
          {
            bitmap1 = (Bitmap) this.CreateBackgroundFrame(false, isHovered, false, enabled, ghosted, restricted, 1f, new Rectangle(0, 0, width, height));
            this._NodeButtonImagesDullBlocked[index1] = bitmap1;
          }
          else
            bitmap1 = this._NodeButtonImagesDullBlocked[index1];
        }
        else if (ghosted)
        {
          if (this._NodeButtonImagesDullHatched[index1] == null)
          {
            bitmap1 = (Bitmap) this.CreateBackgroundFrame(false, isHovered, false, enabled, ghosted, restricted, 1f, new Rectangle(0, 0, width, height));
            this._NodeButtonImagesDullHatched[index1] = bitmap1;
          }
          else
            bitmap1 = this._NodeButtonImagesDullHatched[index1];
        }
        else if (!enabled)
        {
          if (this._NodeButtonImagesDull[index1] == null)
          {
            bitmap1 = (Bitmap) this.CreateBackgroundFrame(false, isHovered, false, enabled, ghosted, restricted, 1f, new Rectangle(0, 0, width, height));
            this._NodeButtonImagesDull[index1] = bitmap1;
          }
          else
            bitmap1 = this._NodeButtonImagesDull[index1];
        }
        else if (this._NodeButtonImagesDull[index1] == null)
        {
          bitmap1 = (Bitmap) this.CreateBackgroundFrame(false, isHovered, false, enabled, ghosted, restricted, 1f, new Rectangle(0, 0, width, height));
          this._NodeButtonImagesDull[index1] = bitmap1;
        }
        else
          bitmap1 = this._NodeButtonImagesDull[index1];
      }
      int num1 = 0;
      if (researchQueueIndex >= 0)
      {
        num1 = 3;
        if (researchQueueIndex == 0)
          num1 = 5;
      }
      bool flag2 = false;
      if (this._HighlightedNode != null && researchNode == this._HighlightedNode)
      {
        flag2 = true;
        num1 = 5;
      }
      using (Bitmap bitmap2 = new Bitmap(width + num1 * 2, height + num1 * 2, PixelFormat.Format32bppPArgb))
      {
        using (Graphics graphics1 = Graphics.FromImage((Image) bitmap2))
        {
          graphics1.InterpolationMode = InterpolationMode.NearestNeighbor;
          graphics1.PixelOffsetMode = PixelOffsetMode.HighSpeed;
          graphics1.SmoothingMode = SmoothingMode.None;
          graphics1.TextRenderingHint = TextRenderingHint.AntiAlias;
          if (num1 > 0)
          {
            using (GraphicsPath roundRectangle = this.CreateRoundRectangle(new Rectangle(0, 0, bitmap2.Width, bitmap2.Height), 10))
            {
              Color color = Color.FromArgb(128, (int) byte.MaxValue, (int) byte.MaxValue, 0);
              if (flag2)
                color = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 0, 0);
              if (researchQueueIndex == 0)
                color = this.UpdateColor(Color.FromArgb(128, (int) byte.MaxValue, 96, 0), Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0));
              using (SolidBrush solidBrush = new SolidBrush(color))
              {
                graphics1.SmoothingMode = SmoothingMode.AntiAlias;
                graphics1.FillPath((Brush) solidBrush, roundRectangle);
                graphics1.SmoothingMode = SmoothingMode.None;
              }
            }
          }
          graphics1.DrawImage((Image) bitmap1, new Point(num1, num1));
          float alpha = 1f;
          if (!enabled)
            alpha = 0.3f;
          Bitmap[] nodeImages = this.GenerateNodeImages(researchNode, alpha);
          if (nodeImages != null && nodeImages.Length > 0)
          {
            int num2 = 0;
            for (int index2 = 0; index2 < nodeImages.Length; ++index2)
            {
              graphics1.DrawImage((Image) nodeImages[index2], new Point(5 + num1 + num2, 18 + num1));
              num2 += nodeImages[index2].Width + 5;
              nodeImages[index2].Dispose();
              nodeImages[index2] = (Bitmap) null;
            }
          }
          int num3 = 0;
          if (researchNode.AllowedRaces != null && researchNode.AllowedRaces.Count > 0)
          {
            for (int index3 = 0; index3 < researchNode.AllowedRaces.Count; ++index3)
            {
              int num4 = 5 + num1 + (this._ImageSize + 5) * (index3 + 1);
              Bitmap image = this._RaceImages[researchNode.AllowedRaces[index3].PictureRef];
              if (!enabled)
                image = this.TransparentImage(image, 0.3f);
              graphics1.DrawImage((Image) image, new Point(width - num4, 18 + num1));
            }
            num3 = (this._ImageSize + 5) * researchNode.AllowedRaces.Count;
          }
          if (researchNode.DisallowedRaces != null && researchNode.DisallowedRaces.Count > 0)
          {
            for (int index4 = 0; index4 < researchNode.DisallowedRaces.Count; ++index4)
            {
              int num5 = 5 + num1 + (this._ImageSize + 5);
              int num6 = num3 + num5;
              Bitmap image = this._RaceImages[researchNode.DisallowedRaces[index4].PictureRef];
              if (!enabled)
                image = this.TransparentImage(image, 0.3f);
              graphics1.DrawImage((Image) image, new Point(width - num6, 18 + num1));
              graphics1.SmoothingMode = SmoothingMode.AntiAlias;
              Color color = Color.Red;
              if (!enabled)
                color = Color.FromArgb(76, (int) byte.MaxValue, 0, 0);
              using (Pen pen = new Pen(color))
              {
                pen.Width = 3f;
                int x1 = width - num6;
                int num7 = 18 + num1;
                graphics1.DrawLine(pen, x1, num7, x1 + image.Width, num7 + image.Height);
                graphics1.DrawLine(pen, x1, num7 + image.Height, x1 + image.Width, num7);
              }
              graphics1.SmoothingMode = SmoothingMode.None;
              num3 += num5;
              if (num3 > width - 35)
                break;
            }
          }
          if (researchNode.IsRushing && !researchNode.IsResearched)
            graphics1.DrawImage((Image) this._CrashImage, new Point(width + num1 - (this._CrashImage.Width + 4), height + num1 - (this._CrashImage.Height + 4)));
          Color color1 = Color.FromArgb(170, 170, 170);
          Color color2 = Color.FromArgb(48, 48, 48);
          if (isHovered)
          {
            color1 = Color.White;
            color2 = Color.Black;
          }
          else if (!enabled)
          {
            color1 = Color.FromArgb(80, 80, 80);
            color2 = Color.FromArgb(128, 48, 48, 48);
          }
          PointF point = new PointF(4f + (float) num1, 3f + (float) num1);
          using (SolidBrush solidBrush1 = new SolidBrush(color1))
          {
            using (SolidBrush solidBrush2 = new SolidBrush(color2))
            {
              string s = researchNode.Name;
              if (researchQueueIndex >= 0)
                s = s + " (" + (researchQueueIndex + 1).ToString() + ")";
              graphics1.DrawString(s, headingFont, (Brush) solidBrush2, new PointF(point.X + 1f, point.Y + 1f));
              graphics1.DrawString(s, headingFont, (Brush) solidBrush1, point);
            }
          }
          if (nodeImages != null)
          {
            if (nodeImages.Length > 0)
              goto label_114;
          }
          PointF pointF = new PointF(43f + (float) num1, 17f + (float) num1);
          using (SolidBrush solidBrush = new SolidBrush(color1))
          {
            string s = Galaxy.ResolveDescription(researchNode);
            RectangleF layoutRectangle = new RectangleF(43f + (float) num1, 17f + (float) num1, (float) (width - 48), (float) (height - 18));
            graphics1.DrawString(s, font, (Brush) solidBrush, layoutRectangle, StringFormat.GenericTypographic);
          }
        }
label_114:
        Rectangle srcRect = new Rectangle(0, 0, bitmap2.Width, bitmap2.Height);
        Rectangle destRect = new Rectangle(x - num1, y - num1, width + num1 * 2, height + num1 * 2);
        graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
        graphics.DrawImage((Image) bitmap2, destRect, srcRect, GraphicsUnit.Pixel);
        if ((double) researchNode.Progress <= 0.0 || researchNode.IsResearched)
          return;
        this.DrawBarGraph((int) researchNode.Cost, (int) researchNode.Progress, 2, width - 10, Color.FromArgb((int) byte.MaxValue, 48, 48, 0), Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0), Color.FromArgb((int) byte.MaxValue, 48, 48, 0), Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0), Color.FromArgb(80, 0, 0, 0), graphics, new Point(x + 5, y + height - 6));
      }
    }

    private void SetGraphicsQualityToLow(Graphics graphics)
    {
      graphics.CompositingQuality = CompositingQuality.HighSpeed;
      graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
      graphics.SmoothingMode = SmoothingMode.None;
    }

    private Bitmap[] GenerateNodeImages(ResearchNode researchNode) => this.GenerateNodeImages(researchNode, 1f);

    private Bitmap[] GenerateNodeImages(ResearchNode researchNode, float alpha)
    {
      List<Bitmap> bitmapList = new List<Bitmap>();
      if (researchNode.Components != null && researchNode.Components.Count > 0)
      {
        for (int index = 0; index < researchNode.Components.Count; ++index)
        {
          Bitmap bitmap = new Bitmap(this._ImageSize, this._ImageSize, PixelFormat.Format32bppPArgb);
          using (Graphics graphics = Graphics.FromImage((Image) bitmap))
          {
            this.SetGraphicsQualityToLow(graphics);
            Bitmap componentImage = this._ComponentImages[researchNode.Components[index].PictureRef];
            Point point = new Point((this._ImageSize - componentImage.Width) / 2, (this._ImageSize - componentImage.Height) / 2);
            graphics.DrawImage((Image) componentImage, point);
          }
          bitmapList.Add(bitmap);
        }
      }
      if (researchNode.ComponentImprovements != null && researchNode.ComponentImprovements.Count > 0)
      {
        for (int index = 0; index < researchNode.ComponentImprovements.Count; ++index)
          bitmapList.Add(this.GenerateImprovedComponentImage(this._ComponentImages[researchNode.ComponentImprovements[index].ImprovedComponent.PictureRef]));
      }
      if (researchNode.Fighters != null && researchNode.Fighters.Count > 0)
      {
        for (int index1 = 0; index1 < researchNode.Fighters.Count; ++index1)
        {
          int index2 = researchNode.Fighters[index1].Type != FighterType.Bomber ? ShipImageHelper.ResolveNewFighterImageIndex(this._Galaxy.PlayerEmpire.DominantRace, this._Galaxy.PlayerEmpire.PirateEmpireBaseHabitat != null) : ShipImageHelper.ResolveNewBomberImageIndex(this._Galaxy.PlayerEmpire.DominantRace, this._Galaxy.PlayerEmpire.PirateEmpireBaseHabitat != null);
          bitmapList.Add(new Bitmap((Image) this._FighterImages[index2]));
        }
      }
      if (researchNode.Abilities != null && researchNode.Abilities.Count > 0)
      {
        for (int index = 0; index < researchNode.Abilities.Count; ++index)
        {
          switch (researchNode.Abilities[index].Type)
          {
            case ResearchAbilityType.PopulationGrowthRate:
              bitmapList.Add(this.GenerateImprovedComponentImage(this._HabitatTypeImages[researchNode.Abilities[index].Value - 1]));
              break;
            case ResearchAbilityType.ColonizeHabitatType:
              bitmapList.Add(new Bitmap((Image) this._HabitatTypeImages[researchNode.Abilities[index].Value - 1]));
              break;
            case ResearchAbilityType.EnableShipSubRole:
              if (researchNode.Abilities[index].RelatedObject is BuiltObjectSubRole)
              {
                switch ((BuiltObjectSubRole) researchNode.Abilities[index].RelatedObject)
                {
                  case BuiltObjectSubRole.Carrier:
                    bitmapList.Add(new Bitmap((Image) this._CarrierImage));
                    continue;
                  case BuiltObjectSubRole.ResupplyShip:
                    bitmapList.Add(new Bitmap((Image) this._ResupplyShipImage));
                    continue;
                  default:
                    continue;
                }
              }
              else
                break;
            case ResearchAbilityType.Troop:
              Bitmap bitmap1 = this._TroopImagesInfantry[0];
              if (researchNode.Abilities[index].RelatedObject != null && researchNode.Abilities[index].RelatedObject is TroopType && this._Empire != null && this._Empire.DominantRace != null)
              {
                switch ((TroopType) researchNode.Abilities[index].RelatedObject)
                {
                  case TroopType.Infantry:
                    bitmap1 = this._TroopImagesInfantry[this._Empire.DominantRace.PictureRef];
                    break;
                  case TroopType.Armored:
                    bitmap1 = this._TroopImagesArmored[this._Empire.DominantRace.PictureRef];
                    break;
                  case TroopType.Artillery:
                    bitmap1 = this._TroopImagesArtillery[this._Empire.DominantRace.PictureRef];
                    break;
                  case TroopType.SpecialForces:
                    bitmap1 = this._TroopImagesSpecialForces[this._Empire.DominantRace.PictureRef];
                    break;
                }
              }
              if (researchNode.Abilities[index].Value != 0)
                bitmap1 = this.GenerateImprovedComponentImage(bitmap1);
              bitmapList.Add(new Bitmap((Image) bitmap1));
              break;
            case ResearchAbilityType.Boarding:
              Bitmap bitmap2 = new Bitmap(this._ImageSize, this._ImageSize, PixelFormat.Format32bppPArgb);
              using (Graphics graphics = Graphics.FromImage((Image) bitmap2))
              {
                this.SetGraphicsQualityToLow(graphics);
                Bitmap componentImage = this._ComponentImages[115];
                Point point = new Point((this._ImageSize - componentImage.Width) / 2, (this._ImageSize - componentImage.Height) / 2);
                graphics.DrawImage((Image) componentImage, point);
              }
              bitmapList.Add(bitmap2);
              break;
          }
        }
      }
      if (researchNode.PlanetaryFacility != null)
        bitmapList.Add(new Bitmap((Image) this._FacilityImages[(int) researchNode.PlanetaryFacility.PictureRef]));
      if (researchNode.PlagueChange != null)
        bitmapList.Add(new Bitmap((Image) this._PlagueImages[researchNode.PlagueChange.PictureRef]));
      if ((double) alpha < 1.0)
      {
        for (int index = 0; index < bitmapList.Count; ++index)
        {
          if (bitmapList[index] != null)
            bitmapList[index] = this.TransparentImage(bitmapList[index], alpha);
        }
      }
      return bitmapList.ToArray();
    }

    private Bitmap GenerateImprovedComponentImage(Bitmap componentImage)
    {
      Bitmap improvedComponentImage = new Bitmap(this._ImageSize, this._ImageSize, PixelFormat.Format32bppPArgb);
      using (Graphics graphics = Graphics.FromImage((Image) improvedComponentImage))
      {
        graphics.CompositingQuality = CompositingQuality.HighQuality;
        graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
        graphics.SmoothingMode = SmoothingMode.None;
        int x = (this._ImageSize - componentImage.Width) / 2;
        int y = (this._ImageSize - componentImage.Height) / 2;
        graphics.DrawImage((Image) componentImage, new Point(x, y));
        Point point = new Point(0, Math.Max(0, (componentImage.Height - this._UpArrowImage.Height) / 2));
        graphics.DrawImage((Image) this._UpArrowImage, point);
      }
      return improvedComponentImage;
    }

    private void ResolveCategoryColor(ComponentCategoryType category, DistantWorlds.Types.Component component)
    {
      switch (category)
      {
        case ComponentCategoryType.WeaponBeam:
        case ComponentCategoryType.WeaponSuperBeam:
          this.BackColor = Color.FromArgb(80, 20, 20);
          this.OuterBorderColor = Color.FromArgb(32, 0, 20);
          this.ShineColor = Color.FromArgb(192, 128, 148);
          this.GlowColor = Color.FromArgb((int) byte.MaxValue, 80, 128);
          if (component != null && (component.Type == ComponentType.WeaponRailGun || component.Type == ComponentType.WeaponSuperRailGun))
          {
            this.BackColor = Color.FromArgb(96, 128, 64);
            this.OuterBorderColor = Color.FromArgb(0, 24, 32);
            this.ShineColor = Color.FromArgb(160, 208, 128);
            this.GlowColor = Color.FromArgb(200, 240, 176);
            break;
          }
          break;
        case ComponentCategoryType.WeaponTorpedo:
        case ComponentCategoryType.WeaponSuperTorpedo:
          this.BackColor = Color.FromArgb(64, 0, 10);
          this.OuterBorderColor = Color.FromArgb(30, 0, 8);
          this.ShineColor = Color.FromArgb(208, 40, 72);
          this.GlowColor = Color.FromArgb((int) byte.MaxValue, 32, 64);
          if (component != null && component.Value7 > 0 && !component.Name.StartsWith("Shaktur"))
          {
            this.BackColor = Color.FromArgb(10, 120, 90);
            this.OuterBorderColor = Color.FromArgb(0, 32, 24);
            this.ShineColor = Color.FromArgb(80, 208, 176);
            this.GlowColor = Color.FromArgb(64, 240, 224);
            break;
          }
          if (component != null && (component.Type == ComponentType.WeaponMissile || component.Type == ComponentType.WeaponSuperMissile))
          {
            this.BackColor = Color.FromArgb(10, 90, 120);
            this.OuterBorderColor = Color.FromArgb(0, 24, 32);
            this.ShineColor = Color.FromArgb(80, 176, 208);
            this.GlowColor = Color.FromArgb(64, 192, (int) byte.MaxValue);
            break;
          }
          break;
        case ComponentCategoryType.WeaponArea:
        case ComponentCategoryType.WeaponSuperArea:
          this.BackColor = Color.FromArgb(64, 48, 20);
          this.OuterBorderColor = Color.FromArgb(48, 20, 10);
          this.ShineColor = Color.FromArgb(240, 160, 80);
          this.GlowColor = Color.FromArgb((int) byte.MaxValue, 112, 48);
          break;
        case ComponentCategoryType.WeaponPointDefense:
          this.BackColor = Color.FromArgb(10, 60, 144);
          this.OuterBorderColor = Color.FromArgb(0, 16, 32);
          this.ShineColor = Color.FromArgb(80, 144, 216);
          this.GlowColor = Color.FromArgb(64, 144, (int) byte.MaxValue);
          break;
        case ComponentCategoryType.WeaponIon:
          this.BackColor = Color.FromArgb(48, 20, 128);
          this.OuterBorderColor = Color.FromArgb(32, 0, 96);
          this.ShineColor = Color.FromArgb(136, 128, 224);
          this.GlowColor = Color.FromArgb(176, 80, (int) byte.MaxValue);
          break;
        case ComponentCategoryType.WeaponGravity:
          this.BackColor = Color.FromArgb(0, 96, 96);
          this.OuterBorderColor = Color.FromArgb(0, 28, 32);
          this.ShineColor = Color.FromArgb(20, 232, 240);
          this.GlowColor = Color.FromArgb(16, 216, (int) byte.MaxValue);
          break;
        case ComponentCategoryType.Armor:
          this.BackColor = Color.FromArgb(64, 96, 112);
          this.OuterBorderColor = Color.FromArgb(32, 36, 48);
          this.ShineColor = Color.FromArgb(176, 208, 232);
          this.GlowColor = Color.FromArgb(192, 224, (int) byte.MaxValue);
          break;
        case ComponentCategoryType.AssaultPod:
          this.BackColor = Color.FromArgb(12, 6, 0);
          this.OuterBorderColor = Color.FromArgb(48, 0, 0);
          this.ShineColor = Color.FromArgb(176, 40, 0);
          this.GlowColor = Color.FromArgb((int) byte.MaxValue, 64, 0);
          break;
        case ComponentCategoryType.Fighter:
          this.BackColor = Color.FromArgb(80, 20, 60);
          this.OuterBorderColor = Color.FromArgb(32, 0, 54);
          this.ShineColor = Color.FromArgb(192, 128, 192);
          this.GlowColor = Color.FromArgb((int) byte.MaxValue, 80, 240);
          break;
        case ComponentCategoryType.Shields:
        case ComponentCategoryType.ShieldRecharge:
          this.BackColor = Color.FromArgb(10, 20, 80);
          this.OuterBorderColor = Color.FromArgb(0, 16, 64);
          this.ShineColor = Color.FromArgb(80, 112, 208);
          this.GlowColor = Color.FromArgb(64, 96, (int) byte.MaxValue);
          break;
        case ComponentCategoryType.Engine:
          this.BackColor = Color.FromArgb(64, 0, 10);
          this.OuterBorderColor = Color.FromArgb(30, 0, 8);
          this.ShineColor = Color.FromArgb(208, 40, 72);
          this.GlowColor = Color.FromArgb((int) byte.MaxValue, 32, 64);
          if (component != null && component.Type == ComponentType.EngineVectoring)
          {
            this.BackColor = Color.FromArgb(64, 0, 32);
            this.OuterBorderColor = Color.FromArgb(30, 0, 20);
            this.ShineColor = Color.FromArgb(208, 40, 128);
            this.GlowColor = Color.FromArgb((int) byte.MaxValue, 32, 112);
            break;
          }
          break;
        case ComponentCategoryType.HyperDrive:
          this.BackColor = Color.FromArgb(32, 96, 112);
          this.OuterBorderColor = Color.FromArgb(12, 36, 48);
          this.ShineColor = Color.FromArgb(96, 208, 232);
          this.GlowColor = Color.FromArgb(128, 224, (int) byte.MaxValue);
          break;
        case ComponentCategoryType.HyperDisrupt:
          this.BackColor = Color.FromArgb(32, 112, 96);
          this.OuterBorderColor = Color.FromArgb(12, 48, 36);
          this.ShineColor = Color.FromArgb(96, 232, 208);
          this.GlowColor = Color.FromArgb(128, (int) byte.MaxValue, 224);
          break;
        case ComponentCategoryType.Reactor:
          this.BackColor = Color.FromArgb(96, 10, 64);
          this.OuterBorderColor = Color.FromArgb(40, 0, 32);
          this.ShineColor = Color.FromArgb(224, 80, 184);
          this.GlowColor = Color.FromArgb((int) byte.MaxValue, 64, 192);
          break;
        case ComponentCategoryType.EnergyCollector:
          this.BackColor = Color.FromArgb(60, 80, 20);
          this.OuterBorderColor = Color.FromArgb(24, 32, 0);
          this.ShineColor = Color.FromArgb(144, 208, 80);
          this.GlowColor = Color.FromArgb(160, (int) byte.MaxValue, 64);
          break;
        case ComponentCategoryType.Extractor:
          this.BackColor = Color.FromArgb(80, 60, 20);
          this.OuterBorderColor = Color.FromArgb(32, 24, 0);
          this.ShineColor = Color.FromArgb(208, 144, 80);
          this.GlowColor = Color.FromArgb((int) byte.MaxValue, 160, 64);
          break;
        case ComponentCategoryType.Manufacturer:
        case ComponentCategoryType.Construction:
          this.BackColor = Color.FromArgb((int) byte.MaxValue, 204, 0);
          this.OuterBorderColor = Color.FromArgb(51, 40, 20);
          this.ShineColor = Color.FromArgb(180, 168, 84);
          this.GlowColor = Color.FromArgb((int) byte.MaxValue, 153, 0);
          break;
        case ComponentCategoryType.Storage:
          this.BackColor = Color.FromArgb(80, 20, 10);
          this.OuterBorderColor = Color.FromArgb(32, 16, 0);
          this.ShineColor = Color.FromArgb(208, 112, 80);
          this.GlowColor = Color.FromArgb((int) byte.MaxValue, 96, 64);
          break;
        case ComponentCategoryType.Sensor:
          this.BackColor = Color.FromArgb(10, 70, 20);
          this.OuterBorderColor = Color.FromArgb(0, 48, 16);
          this.ShineColor = Color.FromArgb(64, 192, 96);
          this.GlowColor = Color.FromArgb(80, (int) byte.MaxValue, 112);
          break;
        case ComponentCategoryType.Computer:
        case ComponentCategoryType.Labs:
          this.BackColor = Color.FromArgb(10, 20, 96);
          this.OuterBorderColor = Color.FromArgb(0, 16, 80);
          this.ShineColor = Color.FromArgb(80, 112, 224);
          this.GlowColor = Color.FromArgb(64, 96, (int) byte.MaxValue);
          break;
        case ComponentCategoryType.Habitation:
          this.BackColor = Color.FromArgb(10, 48, 96);
          this.OuterBorderColor = Color.FromArgb(0, 40, 80);
          this.ShineColor = Color.FromArgb(80, 160, 224);
          this.GlowColor = Color.FromArgb(64, 128, (int) byte.MaxValue);
          break;
        default:
          this.BackColor = Color.FromArgb(20, 20, 40);
          this.OuterBorderColor = Color.FromArgb(0, 0, 20);
          this.ShineColor = Color.FromArgb(128, 128, 148);
          this.GlowColor = Color.FromArgb(80, 80, 160);
          break;
      }
      if (this._NodeButtonColors.Contains(this.BackColor))
        return;
      this._NodeButtonColors.Add(this.BackColor);
      this._NodeButtonImagesGlowing.Add((Bitmap) null);
      this._NodeButtonImagesDull.Add((Bitmap) null);
      this._NodeButtonImagesDullHatched.Add((Bitmap) null);
      this._NodeButtonImagesDullBlocked.Add((Bitmap) null);
    }

    private void DrawBarGraph(
      int maximumValue,
      int currentValue,
      int height,
      int graphWidth,
      Color fillColorStart,
      Color fillColorEnd,
      Color alternateFillColorStart,
      Color alternateFillColorEnd,
      Color backgroundColor,
      Graphics graphics,
      Point location)
    {
      Color color1 = fillColorStart;
      Color color2 = fillColorEnd;
      if (alternateFillColorStart != Color.Empty)
        color1 = this.UpdateColor(fillColorStart, alternateFillColorStart);
      if (alternateFillColorEnd != Color.Empty)
        color2 = this.UpdateColor(fillColorEnd, alternateFillColorEnd);
      int width = (int) ((double) currentValue / (double) maximumValue * (double) graphWidth);
      if (width > graphWidth)
        width = graphWidth;
      Rectangle rect1 = new Rectangle(location.X, location.Y, graphWidth, height);
      Rectangle rect2 = new Rectangle(location.X, location.Y, width, height);
      Rectangle rect3 = new Rectangle(location.X - 1, location.Y, width + 2, height);
      LinearGradientBrush linearGradientBrush = (LinearGradientBrush) null;
      if (rect2.Width > 0)
        linearGradientBrush = new LinearGradientBrush(rect3, color1, color2, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
      SolidBrush solidBrush = new SolidBrush(backgroundColor);
      graphics.FillRectangle((Brush) solidBrush, rect1);
      if (rect2.Width > 0)
        graphics.FillRectangle((Brush) linearGradientBrush, rect2);
      linearGradientBrush?.Dispose();
      solidBrush.Dispose();
    }

    private Color UpdateColor(Color normalColor, Color alternateColor)
    {
      int second = DateTime.Now.ToUniversalTime().Second;
      int millisecond = DateTime.Now.ToUniversalTime().Millisecond;
      if (second % 2 == 1)
        millisecond += 1000;
      double num = millisecond <= 1000 ? (double) Math.Abs(1000 - millisecond) / 1000.0 : (double) (millisecond - 1000) / 1000.0;
      return Color.FromArgb((int) (byte) ((uint) normalColor.A - (uint) (byte) ((double) ((int) normalColor.A - (int) alternateColor.A) * num)), (int) (byte) ((uint) normalColor.R - (uint) (byte) ((double) ((int) normalColor.R - (int) alternateColor.R) * num)), (int) (byte) ((uint) normalColor.G - (uint) (byte) ((double) ((int) normalColor.G - (int) alternateColor.G) * num)), (int) (byte) ((uint) normalColor.B - (uint) (byte) ((double) ((int) normalColor.B - (int) alternateColor.B) * num)));
    }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new virtual Color BackColor
    {
      get => this.backColor;
      set => this.backColor = value;
    }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new virtual Color ForeColor
    {
      get => base.ForeColor;
      set => base.ForeColor = value;
    }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public virtual Color TextColor
    {
      get => this._TextColor;
      set => this._TextColor = value;
    }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public virtual Color TextColor2
    {
      get => this._TextColor2;
      set => this._TextColor2 = value;
    }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override Image BackgroundImage
    {
      get => base.BackgroundImage;
      set => base.BackgroundImage = value;
    }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override ImageLayout BackgroundImageLayout
    {
      get => base.BackgroundImageLayout;
      set => base.BackgroundImageLayout = value;
    }

    [Category("Appearance")]
    [Description("The inner border color of the control.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public virtual Color InnerBorderColor
    {
      get => this.innerBorderColor;
      set
      {
        if (!(this.innerBorderColor != value))
          return;
        this.innerBorderColor = value;
        int num = this.IsHandleCreated ? 1 : 0;
      }
    }

    [Description("The outer border color of the control.")]
    [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public virtual Color OuterBorderColor
    {
      get => this.outerBorderColor;
      set
      {
        if (!(this.outerBorderColor != value))
          return;
        this.outerBorderColor = value;
      }
    }

    [Description("The shine color of the control.")]
    [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public virtual Color ShineColor
    {
      get => this.shineColor;
      set
      {
        if (!(this.shineColor != value))
          return;
        this.shineColor = value;
      }
    }

    [Category("Appearance")]
    [Description("The glow color of the control.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public virtual Color GlowColor
    {
      get => this.glowColor;
      set
      {
        if (!(this.glowColor != value))
          return;
        this.glowColor = value;
      }
    }

    public Image CreateBackgroundFrame(
      bool pressed,
      bool hovered,
      bool animating,
      bool enabled,
      bool ghosted,
      bool restricted,
      float glowOpacity,
      Rectangle rect)
    {
      if (rect.Width <= 0)
        rect.Width = 1;
      if (rect.Height <= 0)
        rect.Height = 1;
      Image image = (Image) new Bitmap(rect.Width, rect.Height);
      using (Graphics g = Graphics.FromImage(image))
      {
        if (this._ClipBackground)
          g.Clear(Color.Black);
        else
          g.Clear(Color.Transparent);
        int radius = 7;
        if (rect.Height < 30)
          radius = 6;
        this.DrawButtonBackground(g, rect, pressed, hovered, animating, enabled, ghosted, restricted, this.outerBorderColor, this.backColor, this.glowColor, this.shineColor, this.innerBorderColor, glowOpacity, radius, this._ClipBackground, this._ClipColor);
      }
      return image;
    }

    private void DrawButtonBackground(
      Graphics g,
      Rectangle rectangle,
      bool pressed,
      bool hovered,
      bool animating,
      bool enabled,
      bool ghosted,
      bool restricted,
      Color outerBorderColor,
      Color backColor,
      Color glowColor,
      Color shineColor,
      Color innerBorderColor,
      float glowOpacity,
      int radius,
      bool clipBackground,
      Color clipColor)
    {
      SmoothingMode smoothingMode = g.SmoothingMode;
      g.SmoothingMode = SmoothingMode.AntiAlias;
      Rectangle rectangle1 = rectangle;
      --rectangle1.Width;
      --rectangle1.Height;
      using (GraphicsPath roundRectangle = this.CreateRoundRectangle(rectangle1, radius))
      {
        if (clipBackground)
        {
          using (Pen pen = new Pen(Color.Black))
            g.DrawPath(pen, roundRectangle);
        }
        else
        {
          using (Pen pen = new Pen(outerBorderColor))
            g.DrawPath(pen, roundRectangle);
        }
      }
      ++rectangle1.X;
      ++rectangle1.Y;
      rectangle1.Width -= 2;
      rectangle1.Height -= 2;
      Rectangle rectangle2 = rectangle1;
      rectangle2.Height >>= 1;
      using (GraphicsPath roundRectangle = this.CreateRoundRectangle(rectangle1, radius - 2))
      {
        if (ghosted)
        {
          if (!restricted)
          {
            using (Brush brush = (Brush) new HatchBrush(HatchStyle.WideDownwardDiagonal, outerBorderColor))
              g.FillPath(brush, roundRectangle);
          }
          else
          {
            using (Brush brush = (Brush) new HatchBrush(HatchStyle.DarkUpwardDiagonal, outerBorderColor))
              g.FillPath(brush, roundRectangle);
          }
        }
        else if (!restricted)
        {
          using (Brush brush = (Brush) new HatchBrush(HatchStyle.WideDownwardDiagonal, outerBorderColor))
            g.FillPath(brush, roundRectangle);
        }
        else
        {
          using (Brush brush = (Brush) new SolidBrush(outerBorderColor))
            g.FillPath(brush, roundRectangle);
        }
      }
      if ((hovered || animating) && !pressed)
      {
        using (GraphicsPath roundRectangle = this.CreateRoundRectangle(rectangle1, radius - 2))
        {
          g.SetClip(roundRectangle, CombineMode.Intersect);
          using (GraphicsPath bottomRadialPath = ResearchTree.CreateBottomRadialPath(rectangle1))
          {
            using (PathGradientBrush pathGradientBrush = new PathGradientBrush(bottomRadialPath))
            {
              int alpha = (int) (178.0 * (double) glowOpacity + 0.5);
              RectangleF bounds = bottomRadialPath.GetBounds();
              pathGradientBrush.CenterPoint = new PointF((float) (((double) bounds.Left + (double) bounds.Right) / 2.0), (float) (((double) bounds.Top + (double) bounds.Bottom) / 2.0));
              pathGradientBrush.CenterColor = Color.FromArgb(alpha, glowColor);
              pathGradientBrush.SurroundColors = new Color[1]
              {
                Color.FromArgb(0, glowColor)
              };
              g.FillPath((Brush) pathGradientBrush, bottomRadialPath);
            }
          }
          g.ResetClip();
        }
      }
      if (rectangle2.Width > 0 && rectangle2.Height > 0)
      {
        ++rectangle2.Height;
        using (GraphicsPath topRoundRectangle = this.CreateTopRoundRectangle(rectangle2, radius - 2))
        {
          ++rectangle2.Height;
          int alpha = 153;
          if (pressed | !enabled)
            alpha = (int) (0.40000000596046448 * (double) alpha + 0.5);
          using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rectangle2, Color.FromArgb(alpha, shineColor), Color.FromArgb(alpha / 3, shineColor), System.Drawing.Drawing2D.LinearGradientMode.Vertical))
            g.FillPath((Brush) linearGradientBrush, topRoundRectangle);
        }
        rectangle2.Height -= 2;
      }
      using (GraphicsPath roundRectangle = this.CreateRoundRectangle(rectangle1, radius - 1))
      {
        using (Pen pen = new Pen(innerBorderColor))
          g.DrawPath(pen, roundRectangle);
      }
      g.SmoothingMode = smoothingMode;
    }

    private GraphicsPath CreateRoundRectangle(Rectangle rectangle, int radius) => this.CreateRoundRectangle(new RectangleF((float) rectangle.X, (float) rectangle.Y, (float) rectangle.Width, (float) rectangle.Height), (float) radius);

    private GraphicsPath CreateRoundRectangle(RectangleF rectangle, float radius)
    {
      GraphicsPath graphicsPath = new GraphicsPath();
      double left = (double) rectangle.Left;
      double top = (double) rectangle.Top;
      double width = (double) rectangle.Width;
      double height = (double) rectangle.Height;
      float diameter = radius * 2f;
      return this.DefineOutline(rectangle.Left, rectangle.Top, rectangle.Width, rectangle.Height, diameter, radius, 0.0f);
    }

    private GraphicsPath DefineOutline(
      float left,
      float top,
      float width,
      float height,
      float diameter,
      float radius,
      float correctionAmount)
    {
      GraphicsPath graphicsPath = new GraphicsPath();
      if (this.CornerCurvedTopLeft)
      {
        graphicsPath.AddArc(left, top, diameter, diameter, 180f, 90f);
        if (this.CornerCurvedTopRight)
          graphicsPath.AddLine(left + radius, top + correctionAmount, left + width - radius, top + correctionAmount);
        else
          graphicsPath.AddLine(left + (radius + correctionAmount), top + correctionAmount, left + width, top + correctionAmount);
      }
      else if (this.CornerCurvedTopRight)
        graphicsPath.AddLine(left, top + correctionAmount, (float) ((double) left + (double) width - ((double) radius + (double) correctionAmount)), top + correctionAmount);
      else
        graphicsPath.AddLine(left, top + correctionAmount, left + width, top + correctionAmount);
      if (this.CornerCurvedTopRight)
      {
        graphicsPath.AddArc((float) ((double) left + (double) width - ((double) diameter + (double) correctionAmount)), top, diameter, diameter, 270f, 90f);
        if (this.CornerCurvedBottomRight)
          graphicsPath.AddLine(left + width - correctionAmount, top + radius, left + width - correctionAmount, top + height - radius);
        else
          graphicsPath.AddLine(left + width - correctionAmount, top + (radius + correctionAmount), left + width - correctionAmount, top + height);
      }
      else if (this.CornerCurvedBottomRight)
        graphicsPath.AddLine(left + width - correctionAmount, top, left + width - correctionAmount, (float) ((double) top + (double) height - ((double) radius + (double) correctionAmount)));
      else
        graphicsPath.AddLine(left + width - correctionAmount, top, left + width - correctionAmount, top + height);
      if (this.CornerCurvedBottomRight)
      {
        graphicsPath.AddArc((float) ((double) left + (double) width - ((double) diameter + (double) correctionAmount)), (float) ((double) top + (double) height - ((double) diameter + (double) correctionAmount)), diameter, diameter, 0.0f, 90f);
        if (this.CornerCurvedBottomLeft)
          graphicsPath.AddLine(left + width - radius, top + height - correctionAmount, left + radius, top + height - correctionAmount);
        else
          graphicsPath.AddLine(left, top + height - correctionAmount, left + (radius - correctionAmount), top + height - correctionAmount);
      }
      else if (this.CornerCurvedBottomLeft)
        graphicsPath.AddLine((float) ((double) left + (double) width - ((double) radius + (double) correctionAmount)), top + height - correctionAmount, left, top + height - correctionAmount);
      else
        graphicsPath.AddLine(left + width, top + height - correctionAmount, left, top + height - correctionAmount);
      if (this.CornerCurvedBottomLeft)
      {
        graphicsPath.AddArc(left, (float) ((double) top + (double) height - ((double) diameter + (double) correctionAmount)), diameter, diameter, 90f, 90f);
        if (this.CornerCurvedTopLeft)
          graphicsPath.AddLine(left + correctionAmount, top + height - radius, left + correctionAmount, top + radius);
        else
          graphicsPath.AddLine(left + correctionAmount, top, left + correctionAmount, (float) ((double) top + (double) height - ((double) radius + (double) correctionAmount)));
      }
      else if (this.CornerCurvedTopLeft)
        graphicsPath.AddLine(left + correctionAmount, top + (radius + correctionAmount), left + correctionAmount, top + height);
      else
        graphicsPath.AddLine(left + correctionAmount, top + height, left + correctionAmount, top);
      graphicsPath.CloseFigure();
      return graphicsPath;
    }

    private GraphicsPath CreateTopRoundRectangle(Rectangle rectangle, int radius)
    {
      GraphicsPath graphicsPath = new GraphicsPath();
      int left = rectangle.Left;
      int top = rectangle.Top;
      int width = rectangle.Width;
      int height = rectangle.Height;
      int diameter = radius << 1;
      return this.DefineOutline((float) rectangle.Left, (float) rectangle.Top, (float) rectangle.Width, (float) rectangle.Height, (float) diameter, (float) radius, 0.0f);
    }

    private static GraphicsPath CreateBottomRadialPath(Rectangle rectangle)
    {
      GraphicsPath bottomRadialPath = new GraphicsPath();
      RectangleF rect = (RectangleF) rectangle;
      rect.X -= rect.Width * 0.35f;
      rect.Y -= rect.Height * 0.15f;
      rect.Width *= 1.7f;
      rect.Height *= 2.3f;
      bottomRadialPath.AddEllipse(rect);
      bottomRadialPath.CloseFigure();
      return bottomRadialPath;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new System.ComponentModel.Container();
      this.AutoScaleMode = AutoScaleMode.Font;
    }

    public delegate void NodeClickedHandler(object sender, ResearchNode nodeClicked);
  }
}
