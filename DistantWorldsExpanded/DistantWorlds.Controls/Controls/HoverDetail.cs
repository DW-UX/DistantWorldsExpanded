// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.HoverDetail
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class HoverDetail : GradientPanel
  {
    private object _DetailObject;
    private Galaxy _Galaxy;
    private Empire _Empire;
    private int _ItemWidth = 190;
    private int _Margin = 5;
    private bool _OffsetLeft;
    private SolidBrush _WhiteBrush = new SolidBrush(Color.FromArgb(170, 170, 170));

    public void BindData(Galaxy galaxy, Empire empire, object detailObject, bool offsetLeft)
    {
      this._ItemWidth = 300;
      this._Galaxy = galaxy;
      this._Empire = empire;
      this._DetailObject = detailObject;
      this._OffsetLeft = offsetLeft;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      float num = this.DrawDetailObject(e.Graphics, this._DetailObject);
      this.Width = this._ItemWidth;
      this.Height = (int) num;
    }

    private float DrawDetailObject(Graphics graphics, object detailObject)
    {
      float margin1 = (float) this._Margin;
      float margin2 = (float) this._Margin;
      if (detailObject != null)
      {
        graphics.CompositingQuality = CompositingQuality.HighQuality;
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
        if (detailObject is ResearchNode)
        {
          ResearchNode project = (ResearchNode) detailObject;
          List<string[]> allDescriptions;
          List<string[]> allValues;
          this._Galaxy.GenerateBenefitDetail(project, this._Empire, out allDescriptions, out allValues);
          using (Font font1 = new Font(this.Font.FontFamily, 20f, FontStyle.Regular, GraphicsUnit.Pixel))
          {
            using (Font font2 = new Font(this.Font.FontFamily, 20f, FontStyle.Bold, GraphicsUnit.Pixel))
            {
              using (Font font3 = new Font(this.Font.FontFamily, 17f, FontStyle.Regular, GraphicsUnit.Pixel))
              {
                using (Font font4 = new Font(this.Font.FontFamily, 17f, FontStyle.Bold, GraphicsUnit.Pixel))
                {
                  graphics.DrawString(project.Name, font2, (Brush) this._WhiteBrush, new PointF(margin1, margin2), StringFormat.GenericTypographic);
                  margin2 += 22f;
                  string s = TextResolver.GetText("Project Size") + ": " + project.Cost.ToString("0,K");
                  graphics.DrawString(s, font1, (Brush) this._WhiteBrush, new PointF(margin1, margin2), StringFormat.GenericTypographic);
                  margin2 += 22f;
                  for (int index1 = 0; index1 < allDescriptions.Count; ++index1)
                  {
                    using (Pen pen = new Pen(Color.FromArgb(170, 170, 170), 1f))
                    {
                      pen.DashStyle = DashStyle.Dot;
                      graphics.DrawLine(pen, margin1, margin2, margin1 + (float) (this._ItemWidth - this._Margin * 2 + 2), margin2);
                    }
                    margin2 += 7f;
                    string[] strArray1 = allDescriptions[index1];
                    string[] strArray2 = allValues[index1];
                    float num1 = margin1 + ((float) this._ItemWidth - 160f);
                    StringFormat format1 = new StringFormat(StringFormatFlags.FitBlackBox);
                    format1.Trimming = StringTrimming.EllipsisCharacter;
                    SizeF sizeF1 = graphics.MeasureString(strArray1[0], font4, this._ItemWidth - 10, format1);
                    RectangleF layoutRectangle1 = new RectangleF(margin1, margin2, sizeF1.Width + 3f, sizeF1.Height + 3f);
                    graphics.DrawString(strArray1[0], font4, (Brush) this._WhiteBrush, layoutRectangle1, format1);
                    margin2 += sizeF1.Height;
                    if (strArray1.Length > 1)
                    {
                      for (int index2 = 1; index2 < strArray1.Length && !string.IsNullOrEmpty(strArray1[index2]); ++index2)
                      {
                        if (string.IsNullOrEmpty(strArray2[index2]))
                        {
                          StringFormat format2 = new StringFormat(StringFormatFlags.FitBlackBox);
                          format2.Trimming = StringTrimming.EllipsisCharacter;
                          SizeF sizeF2 = graphics.MeasureString(strArray1[index2], font3, this._ItemWidth - 10, format2);
                          RectangleF layoutRectangle2 = new RectangleF(margin1, margin2, sizeF2.Width + 3f, sizeF2.Height + 3f);
                          graphics.DrawString(strArray1[index2], font3, (Brush) this._WhiteBrush, layoutRectangle2, format2);
                          margin2 += sizeF2.Height;
                        }
                        else
                        {
                          SizeF sizeF3 = graphics.MeasureString(strArray1[index2], font3, this._ItemWidth, StringFormat.GenericTypographic);
                          float num2 = num1 - sizeF3.Width;
                          graphics.DrawString(strArray1[index2], font3, (Brush) this._WhiteBrush, new PointF((float) ((double) margin1 + (double) num2 - 5.0), margin2), StringFormat.GenericTypographic);
                          graphics.DrawString(strArray2[index2], font4, (Brush) this._WhiteBrush, new PointF(margin1 + num1, margin2), StringFormat.GenericTypographic);
                          margin2 += 18f;
                        }
                      }
                    }
                    margin2 += 8f;
                  }
                  if (this.Height < (int) margin2)
                    this.Height = (int) margin2;
                }
              }
            }
          }
        }
      }
      return margin2;
    }
  }
}
