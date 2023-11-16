// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.ListViewBase
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Media;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class ListViewBase : UserControl
  {
    private IFontCache _FontCache;
    private float _FontSize = 15.33f;
    private bool _FontIsBold;
    private bool _SoundsEnabled;
    private static string _SoundLocation;
    private static SoundPlayer _SoundPlayer;
    private static object _SoundLock = new object();
    public static double Volume = 1.0;
    private DataGridViewColumn _SortedColumn;
    private SortOrder _SortOrder;
    private IContainer components;
    public DataGridView _Grid;

    public bool SoundsEnabled
    {
      get => this._SoundsEnabled;
      set => this._SoundsEnabled = value;
    }

    private static void PlaySound()
    {
      lock (ListViewBase._SoundLock)
      {
        if (string.IsNullOrEmpty(ListViewBase._SoundLocation) || ListViewBase._SoundPlayer == null || ListViewBase.Volume <= 0.0)
          return;
        ListViewBase._SoundPlayer.Play();
      }
    }

    public static void SetSoundLocation(string soundLocation)
    {
      if (string.IsNullOrEmpty(soundLocation))
        return;
      ListViewBase._SoundLocation = soundLocation;
      ListViewBase._SoundPlayer = new SoundPlayer(soundLocation);
      ListViewBase._SoundPlayer.Load();
    }

    public void SetFontCache(IFontCache fontCache)
    {
      this._FontCache = fontCache;
      if ((double) this._FontSize <= 0.0)
        return;
      this.Font = this._FontCache.GenerateFont(this._FontSize, this._FontIsBold);
      this._Grid.Font = this.Font;
      this._Grid.ColumnHeadersDefaultCellStyle.Font = this.Font;
      this._Grid.DefaultCellStyle.Font = this.Font;
      this._Grid.AlternatingRowsDefaultCellStyle.Font = this.Font;
      this._Grid.RowsDefaultCellStyle.Font = this.Font;
    }

    public void SetFont(float pixelSize) => this.SetFont(pixelSize, false);

    public void SetFont(float pixelSize, bool isBold)
    {
      this._FontSize = pixelSize;
      this._FontIsBold = isBold;
      if (this._FontCache == null)
        return;
      this.Font = this._FontCache.GenerateFont(this._FontSize, this._FontIsBold);
    }

    public ListViewBase()
    {
      this.InitializeComponent();
      this._Grid.AllowUserToAddRows = false;
      this._Grid.AllowUserToDeleteRows = false;
      this._Grid.AllowUserToOrderColumns = false;
      this._Grid.AllowUserToResizeColumns = true;
      this._Grid.AllowUserToResizeRows = false;
      this._Grid.AutoGenerateColumns = false;
      this._Grid.BackgroundColor = Color.FromArgb(48, 48, 64);
      this._Grid.BorderStyle = BorderStyle.None;
      this._Grid.CellBorderStyle = DataGridViewCellBorderStyle.None;
      this._Grid.ColumnHeadersVisible = true;
      this._Grid.Font = new Font("Verdana", 8f, FontStyle.Regular);
      this._Grid.ForeColor = Color.White;
      this._Grid.GridColor = Color.Black;
      this._Grid.Location = new Point(0, 0);
      this._Grid.Margin = new Padding(0);
      this._Grid.MultiSelect = false;
      this._Grid.ReadOnly = true;
      this._Grid.RowHeadersWidth = 4;
      this._Grid.ScrollBars = ScrollBars.Vertical;
      this._Grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this._Grid.ShowCellToolTips = true;
      this._Grid.Size = new Size(100, 100);
      this._Grid.RowHeadersVisible = false;
      this._Grid.ColumnHeadersVisible = true;
      this._Grid.ColumnHeadersHeight = 14;
      this._Grid.RowTemplate.Height = 20;
      this._Grid.EnableHeadersVisualStyles = false;
      this._Grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(24, 24, 24);
      this._Grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(170, 170, 170);
      this._Grid.ColumnHeadersDefaultCellStyle.Font = new Font("Verdana", 8f, FontStyle.Regular);
      this._Grid.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
      this._Grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
      DataGridViewCellStyle gridViewCellStyle1 = new DataGridViewCellStyle();
      gridViewCellStyle1.BackColor = Color.FromArgb(32, 32, 40);
      gridViewCellStyle1.Font = this._Grid.Font;
      gridViewCellStyle1.ForeColor = Color.FromArgb(170, 170, 170);
      gridViewCellStyle1.SelectionBackColor = Color.FromArgb(96, 96, 96);
      gridViewCellStyle1.SelectionForeColor = Color.Yellow;
      DataGridViewCellStyle gridViewCellStyle2 = new DataGridViewCellStyle();
      gridViewCellStyle2.BackColor = Color.FromArgb(48, 48, 56);
      gridViewCellStyle2.Font = this._Grid.Font;
      gridViewCellStyle2.ForeColor = Color.FromArgb(170, 170, 170);
      gridViewCellStyle2.SelectionBackColor = Color.FromArgb(96, 96, 96);
      gridViewCellStyle2.SelectionForeColor = Color.Yellow;
      this._Grid.DefaultCellStyle = gridViewCellStyle1;
      this._Grid.AlternatingRowsDefaultCellStyle = gridViewCellStyle2;
      this.SelectionChanged = new EventHandler(this.OnSelectionChanged);
      this.Scrolled = new ScrollEventHandler(this.OnScroll);
      this.Sorted = new EventHandler(this.OnSort);
    }

    public DataGridView Grid => this._Grid;

    protected static Bitmap PrescaleImageStatic(Bitmap originalBitmap, int width, int height)
    {
      Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
      Graphics graphics = Graphics.FromImage((Image) bitmap);
      graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      graphics.CompositingQuality = CompositingQuality.HighQuality;
      graphics.SmoothingMode = SmoothingMode.AntiAlias;
      graphics.DrawImage((Image) originalBitmap, new Rectangle(0, 0, width, height));
      graphics.Dispose();
      return bitmap;
    }

    protected Bitmap PrescaleImage(Bitmap originalBitmap, int width, int height)
    {
      Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
      Graphics graphics = Graphics.FromImage((Image) bitmap);
      graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      graphics.CompositingQuality = CompositingQuality.HighQuality;
      graphics.SmoothingMode = SmoothingMode.AntiAlias;
      graphics.DrawImage((Image) originalBitmap, new Rectangle(0, 0, width, height));
      graphics.Dispose();
      return bitmap;
    }

    public event EventHandler SelectionChanged;

    public void OnSelectionChanged(object sender, EventArgs e)
    {
      if (!this._SoundsEnabled)
        return;
      ListViewBase.PlaySound();
    }

    public DataGridViewRowCollection Rows => this._Grid.Rows;

    public int RowTemplateHeight
    {
      get => this._Grid.RowTemplate.Height;
      set => this._Grid.RowTemplate.Height = value;
    }

    public new Size Size
    {
      get => this._Grid.Size;
      set
      {
        this._Grid.Height = value.Height;
        this._Grid.Width = value.Width;
        base.Size = value;
      }
    }

    public new int Height
    {
      get => this._Grid.Height;
      set
      {
        this._Grid.Height = value;
        base.Height = value;
      }
    }

    public new int Width
    {
      get => this._Grid.Width;
      set
      {
        this._Grid.Width = value;
        base.Width = value;
      }
    }

    public void RememberSorting()
    {
      if (this._SortedColumn == null)
        return;
      ListSortDirection direction = ListSortDirection.Ascending;
      if (this._SortOrder == SortOrder.Descending)
        direction = ListSortDirection.Descending;
      this._Grid.Sort(this._SortedColumn, direction);
    }

    public int FirstDisplayedScrollingRowIndex => this._Grid.FirstDisplayedScrollingRowIndex;

    public int ColumnHeadersHeight => this._Grid.ColumnHeadersHeight;

    private void _Grid_SelectionChanged(object sender, EventArgs e) => this.SelectionChanged(sender, e);

    public event ScrollEventHandler Scrolled;

    public void OnScroll(object sender, ScrollEventArgs e)
    {
    }

    private void _Grid_Scroll(object sender, ScrollEventArgs e) => this.Scrolled(sender, e);

    public event EventHandler Sorted;

    public void OnSort(object sender, EventArgs e)
    {
      this._SortedColumn = this._Grid.SortedColumn;
      this._SortOrder = this._Grid.SortOrder;
    }

    private void _Grid_Sorted(object sender, EventArgs e) => this.Sorted(sender, e);

    public event EventHandler RowDoubleClick;

    private void _Grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex < 0 || this.RowDoubleClick == null)
        return;
      this.RowDoubleClick(sender, new EventArgs());
    }

    public event DataGridViewCellEventHandler CellClick;

    private void _Grid_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      if (this.CellClick == null)
        return;
      this.CellClick(sender, e);
    }

    private void _Grid_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
      this._SortedColumn = (DataGridViewColumn) null;
      this._SortOrder = SortOrder.None;
      foreach (DataGridViewColumn column in (BaseCollection) this._Grid.Columns)
      {
        if (column.SortMode != DataGridViewColumnSortMode.NotSortable)
          column.SortMode = DataGridViewColumnSortMode.Programmatic;
      }
      foreach (DataGridViewColumn column in (BaseCollection) this._Grid.Columns)
      {
        if (column.SortMode == DataGridViewColumnSortMode.Programmatic)
          column.SortMode = DataGridViewColumnSortMode.Automatic;
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this._Grid = new DataGridView();
      ((ISupportInitialize) this._Grid).BeginInit();
      this.SuspendLayout();
      this._Grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this._Grid.Location = new Point(3, 3);
      this._Grid.Name = "_Grid";
      this._Grid.Size = new Size(240, 150);
      this._Grid.TabIndex = 0;
      this._Grid.Scroll += new ScrollEventHandler(this._Grid_Scroll);
      this._Grid.ColumnHeaderMouseDoubleClick += new DataGridViewCellMouseEventHandler(this._Grid_ColumnHeaderMouseDoubleClick);
      this._Grid.Sorted += new EventHandler(this._Grid_Sorted);
      this._Grid.CellDoubleClick += new DataGridViewCellEventHandler(this._Grid_CellDoubleClick);
      this._Grid.CellClick += new DataGridViewCellEventHandler(this._Grid_CellClick);
      this._Grid.SelectionChanged += new EventHandler(this._Grid_SelectionChanged);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this._Grid);
      this.Name = nameof (ListViewBase);
      this.Size = new Size(245, 156);
      ((ISupportInitialize) this._Grid).EndInit();
      this.ResumeLayout(false);
    }
  }
}
