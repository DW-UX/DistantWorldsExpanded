// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.ScrollingCreditsPanel
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Timers;
using System.Windows.Forms;
using System.ComponentModel;

namespace DistantWorlds.Controls
{
  public class ScrollingCreditsPanel : Panel
  {
    private object _LockObject = new object();
    private volatile bool _CanPaint = true;
    private List<object> _Items = new List<object>();
    private List<int> _ItemTops = new List<int>();
    private List<double> _ScrollingItemTops = new List<double>();
    private System.Timers.Timer _Timer = new System.Timers.Timer();
    private double _ScrollSpeed = 20.0;
    private double _SecondsPerTimerInterval = 0.05;
    private Font _DefaultFont = new Font("Verdana", 12f, FontStyle.Bold);
    private Color _DefaultTextColor = Color.Yellow;
    private int _DefaultSpacerHeight = 20;
    public ScrollingCreditsPanel.DoScrollDelegate DoScrollMethod;
    private Image _BackgroundImage;
    private bool _ShowBackground;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override Image BackgroundImage
    {
      get => this._BackgroundImage;
      set
      {
        this._BackgroundImage = value;
        if (this._BackgroundImage == null)
        {
          this._ShowBackground = false;
          this.BackColor = Color.Transparent;
        }
        else
        {
          this._ShowBackground = true;
          this.BackColor = Color.Black;
        }
      }
    }

    public ScrollingCreditsPanel()
    {
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.UserPaint, true);
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.SetStyle(ControlStyles.SupportsTransparentBackColor, false);
      this.SetStyle(ControlStyles.Opaque, true);
      this.SetStyle(ControlStyles.ResizeRedraw, false);
      this.UpdateStyles();
      this._Timer.AutoReset = true;
      this._Timer.Interval = this._SecondsPerTimerInterval * 1000.0;
      this._Timer.Elapsed += new ElapsedEventHandler(this._Timer_Elapsed);
      this.DoScrollMethod = new ScrollingCreditsPanel.DoScrollDelegate(this.DoScroll);
    }

    ~ScrollingCreditsPanel()
    {
      this._Timer.Stop();
      this._Timer.Elapsed -= new ElapsedEventHandler(this._Timer_Elapsed);
      this.DoScrollMethod = (ScrollingCreditsPanel.DoScrollDelegate) null;
    }

    private void _Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
      this._Timer.Stop();
      if (this.DoScrollMethod != null)
        this.Invoke((Delegate) this.DoScrollMethod);
      this._Timer.Start();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      if (!this._CanPaint)
        return;
      if (this._ShowBackground)
      {
        e.Graphics.CompositingQuality = CompositingQuality.HighSpeed;
        e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
        e.Graphics.PixelOffsetMode = PixelOffsetMode.None;
        e.Graphics.SmoothingMode = SmoothingMode.None;
        e.Graphics.DrawImage(this._BackgroundImage, 0, 0);
      }
      this.Draw(e.Graphics);
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
      if (!this._CanPaint || this._ShowBackground)
        return;
      base.OnPaintBackground(e);
    }

    private void Draw(Graphics graphics)
    {
      using (SolidBrush solidBrush1 = new SolidBrush(this._DefaultTextColor))
      {
        using (SolidBrush solidBrush2 = new SolidBrush(Color.Black))
        {
          for (int index = 0; index < this._Items.Count; ++index)
          {
            object obj = this._Items[index];
            SizeF itemSize;
            if (obj != null && this.CheckItemInView(obj, this._ScrollingItemTops[index], this.Size, graphics, out itemSize))
            {
              int x = (this.Width - (int) itemSize.Width) / 2;
              int scrollingItemTop = (int) this._ScrollingItemTops[index];
              switch (obj)
              {
                case string _:
                  string s = (string) obj;
                  RectangleF layoutRectangle = new RectangleF((float) x + 1f, (float) scrollingItemTop + 1f, itemSize.Width + 1f, itemSize.Height + 1f);
                  graphics.DrawString(s, this._DefaultFont, (Brush) solidBrush2, layoutRectangle);
                  layoutRectangle = new RectangleF((float) x, (float) scrollingItemTop, itemSize.Width + 1f, itemSize.Height + 1f);
                  graphics.DrawString(s, this._DefaultFont, (Brush) solidBrush1, layoutRectangle);
                  continue;
                case Bitmap _:
                  Bitmap bitmap = (Bitmap) obj;
                  graphics.DrawImage((Image) bitmap, new Point(x, scrollingItemTop));
                  continue;
                default:
                  continue;
              }
            }
          }
        }
      }
    }

    private void DoScroll()
    {
      lock (this._LockObject)
      {
        this._CanPaint = false;
        double num = this._SecondsPerTimerInterval * this._ScrollSpeed;
        for (int index1 = 0; index1 < this._ScrollingItemTops.Count; ++index1)
        {
          List<double> scrollingItemTops;
          int index2;
          (scrollingItemTops = this._ScrollingItemTops)[index2 = index1] = scrollingItemTops[index2] - num;
        }
        bool flag = false;
        using (Graphics graphics = this.CreateGraphics())
        {
          for (int index = 0; index < this._Items.Count; ++index)
          {
            if (this.CheckItemInView(this._Items[index], this._ScrollingItemTops[index], this.Size, graphics))
            {
              flag = true;
              break;
            }
          }
        }
        if (!flag)
        {
          this.StopScroll();
          this.ResetControlPositions();
          this.StartScroll();
        }
        this._CanPaint = true;
      }
      this.Invalidate();
    }

    private void ResetControlPositions()
    {
      for (int index = 0; index < this._ItemTops.Count; ++index)
        this._ScrollingItemTops[index] = (double) this._ItemTops[index];
    }

    private SizeF GetItemSize(object item, Graphics graphics)
    {
      SizeF itemSize = SizeF.Empty;
      switch (item)
      {
        case int height:
          itemSize = new SizeF(1f, (float) height);
          break;
        case string _:
          string text = (string) item;
          itemSize = graphics.MeasureString(text, this._DefaultFont, this.Width);
          break;
        case Bitmap _:
          Bitmap bitmap = (Bitmap) item;
          itemSize = new SizeF((float) bitmap.Width, (float) bitmap.Height);
          break;
      }
      return itemSize;
    }

    private bool CheckItemInView(
      object item,
      double topPosition,
      Size viewSize,
      Graphics graphics)
    {
      return this.CheckItemInView(item, topPosition, viewSize, graphics, out SizeF _);
    }

    private bool CheckItemInView(
      object item,
      double topPosition,
      Size viewSize,
      Graphics graphics,
      out SizeF itemSize)
    {
      itemSize = this.GetItemSize(item, graphics);
      int num1 = (int) (topPosition + (double) itemSize.Height);
      int num2 = (int) topPosition;
      return num1 >= 0 && num2 <= viewSize.Height;
    }

    public void SetScrollPosition(double position)
    {
      lock (this._LockObject)
      {
        for (int index = 0; index < this._ScrollingItemTops.Count; ++index)
          this._ScrollingItemTops[index] = (double) this._ItemTops[index] - position;
      }
    }

    public void StartScroll() => this._Timer.Start();

    public void StopScroll() => this._Timer.Stop();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public double ScrollSpeed
    {
      get => this._ScrollSpeed;
      set => this._ScrollSpeed = value;
    }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new Font DefaultFont
    {
      get => this._DefaultFont;
      set => this._DefaultFont = value;
    }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color DefaultTextColor
    {
      get => this._DefaultTextColor;
      set => this._DefaultTextColor = value;
    }

    public void ClearAll()
    {
      this._Timer.Stop();
      lock (this._LockObject)
      {
        this._Items.Clear();
        this._ItemTops.Clear();
        this._ScrollingItemTops.Clear();
      }
    }

    private void AddItem(object item)
    {
      lock (this._LockObject)
      {
        int currentTopPosition = this.CalculateCurrentTopPosition();
        this._ItemTops.Add(currentTopPosition);
        this._ScrollingItemTops.Add((double) currentTopPosition);
        this._Items.Add(item);
      }
    }

    public void AddText(string text) => this.AddItem((object) text);

    public void AddSpacer() => this.AddSpacer(this._DefaultSpacerHeight);

    public void AddSpacer(int height) => this.AddItem((object) height);

    public void AddImage(Bitmap image) => this.AddItem((object) image);

    private int CalculateCurrentTopPosition()
    {
      int currentTopPosition = 0;
      using (Graphics graphics = this.CreateGraphics())
      {
        foreach (object obj in this._Items)
        {
          SizeF itemSize = this.GetItemSize(obj, graphics);
          currentTopPosition += (int) itemSize.Height;
        }
      }
      return currentTopPosition;
    }

    public delegate void DoScrollDelegate();
  }
}
