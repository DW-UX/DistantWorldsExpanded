// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.ResourceDropDown
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class ResourceDropDown : ComboBox
  {
    private ResourceList _Resources;
    private Bitmap[] _ResourceImages;
    private bool _AllowNullResource;
    private bool _AllowCriticalResources;
    private string _AllowNullResourceText = "(" + TextResolver.GetText("All Resources") + ")";

    public ResourceDropDown() => this.SetDefaults(this.Font);

    private void SetDefaults(Font font)
    {
      this.DrawMode = DrawMode.OwnerDrawFixed;
      this.DropDownStyle = ComboBoxStyle.DropDownList;
      this.FlatStyle = FlatStyle.Popup;
      this.BackColor = Color.FromArgb(22, 21, 26);
      this.ForeColor = Color.FromArgb(170, 170, 170);
      this.Font = font;
    }

    public void ClearData() => this._Resources = (ResourceList) null;

    public void BindData(
      Font font,
      ResourceDefinitionList resourceDefinitions,
      Bitmap[] resourceImages)
    {
      this.BindData(font, resourceDefinitions, resourceImages, true, false);
    }

    public void BindData(
      Font font,
      ResourceDefinitionList resourceDefinitions,
      Bitmap[] resourceImages,
      bool allowNullResource,
      bool allowCriticalResources)
    {
      this.SetDefaults(font);
      ResourceList resourceList = new ResourceList();
      foreach (ResourceDefinition resourceDefinition in (List<ResourceDefinition>) resourceDefinitions)
      {
        Resource resource = new Resource(resourceDefinition.ResourceID);
        resourceList.Add(resource);
      }
      resourceList.Sort();
      this._Resources = resourceList;
      this._AllowNullResource = allowNullResource;
      this._AllowCriticalResources = allowCriticalResources;
      this.Items.Clear();
      if (this._AllowNullResource)
        this.Items.Add(new object());
      if (this._AllowCriticalResources)
        this.Items.Add(new object());
      if (this._Resources != null)
        this.Items.AddRange((object[]) this._Resources.ToArray());
      this._ResourceImages = new Bitmap[0];
      if (resourceImages == null || resourceImages.Length <= 0)
        return;
      this._ResourceImages = new Bitmap[resourceImages.Length];
      for (int index = 0; index < resourceImages.Length; ++index)
      {
        int height = 19;
        double num = (double) height / (double) resourceImages[index].Height;
        int width = (int) ((double) resourceImages[index].Width * num);
        this._ResourceImages[index] = this.PrecacheScaledBitmap(resourceImages[index], width, height);
      }
    }

    private Bitmap PrecacheScaledBitmap(Bitmap unscaledBitmap, int width, int height)
    {
      if (width < 1)
        width = 1;
      if (height < 1)
        height = 1;
      Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
      Graphics graphics = Graphics.FromImage((Image) bitmap);
      graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      graphics.CompositingQuality = CompositingQuality.HighQuality;
      graphics.SmoothingMode = SmoothingMode.AntiAlias;
      graphics.DrawImage((Image) unscaledBitmap, new Rectangle(0, 0, width, height));
      graphics.Dispose();
      return bitmap;
    }

    public void SetSelectedResource(Resource resource)
    {
      int num = -1;
      if (this._Resources != null && resource != null)
        num = this._Resources.IndexOf(resource.ResourceID);
      if (this._AllowCriticalResources)
      {
        ++num;
        if (resource == null)
          num = 0;
      }
      if (this._AllowNullResource)
      {
        ++num;
        if (resource == null)
          num = 0;
      }
      this.SelectedIndex = num;
    }

    public string AllowNullResourceText
    {
      get => this._AllowNullResourceText;
      set => this._AllowNullResourceText = value;
    }

    public bool CriticalResourcesSelected
    {
      get
      {
        int selectedIndex = this.SelectedIndex;
        if (selectedIndex < 0 || !this._AllowCriticalResources)
          return false;
        if (this._AllowNullResource)
        {
          if (selectedIndex == 1)
            return true;
        }
        else if (selectedIndex == 0)
          return true;
        return false;
      }
    }

    public Resource SelectedResource
    {
      get
      {
        int selectedIndex = this.SelectedIndex;
        if (selectedIndex < 0)
          return (Resource) null;
        if (this._AllowNullResource || this._AllowCriticalResources)
        {
          if (this._AllowNullResource && !this._AllowCriticalResources)
          {
            if (selectedIndex == 0)
              return (Resource) null;
          }
          else if (this._AllowCriticalResources && !this._AllowNullResource)
          {
            if (selectedIndex == 0)
              return (Resource) null;
          }
          else if (selectedIndex == 0 || selectedIndex == 1)
            return (Resource) null;
        }
        if (this._AllowNullResource)
          --selectedIndex;
        if (this._AllowCriticalResources)
          --selectedIndex;
        return this._Resources != null && selectedIndex >= 0 && selectedIndex < this._Resources.Count ? this._Resources[selectedIndex] : (Resource) null;
      }
    }

    protected override void OnMeasureItem(MeasureItemEventArgs e)
    {
      base.OnMeasureItem(e);
      e.ItemHeight = 19;
    }

    protected override void OnDrawItem(DrawItemEventArgs e)
    {
      base.OnDrawItem(e);
      e.DrawBackground();
      PointF point = new PointF(30f, (float) (e.Bounds.Y + 2));
      Font font = this.Font;
      SolidBrush solidBrush = new SolidBrush(this.ForeColor);
      if (this._AllowNullResource && e.Index == 0)
        e.Graphics.DrawString(this._AllowNullResourceText, font, (Brush) solidBrush, point);
      else if (this._AllowCriticalResources && !this._AllowNullResource && e.Index == 0 || this._AllowCriticalResources && this._AllowNullResource && e.Index == 1)
        e.Graphics.DrawString(TextResolver.GetText("Critical Empire Resources"), font, (Brush) solidBrush, point);
      else if (this._Resources != null && this._Resources.Count > 0 && e.Index >= 0)
      {
        int index = e.Index;
        if (this._AllowNullResource)
          --index;
        if (this._AllowCriticalResources)
          --index;
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        e.Graphics.DrawString(this._Resources[index].Name, font, (Brush) solidBrush, point);
        Rectangle rect = new Rectangle();
        Bitmap resourceImage = this._ResourceImages[this._Resources[index].PictureRef];
        int height = e.Bounds.Height - 2;
        double num = (double) height / (double) resourceImage.Height;
        int width = (int) ((double) resourceImage.Width * num);
        rect = new Rectangle((30 - resourceImage.Width) / 2, e.Bounds.Y + 1, width, height);
        e.Graphics.DrawImage((Image) resourceImage, rect);
      }
      e.DrawFocusRectangle();
    }
  }
}
