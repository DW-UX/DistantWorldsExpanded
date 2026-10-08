using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Bitmap = System.Drawing.Bitmap;
using Font = System.Drawing.Font;
using FontFamily = System.Drawing.FontFamily;
using FontStyle = System.Drawing.FontStyle;
using XnaColor = Microsoft.Xna.Framework.Color;
using XnaRectangle = Microsoft.Xna.Framework.Rectangle;

namespace DistantWorlds
{
    /// <summary>
    /// Builds a larger version of a pre-built sprite font so main view labels follow the UI scale.
    /// The glyphs are drawn fresh from the game's typeface at the new size; if the sprite font
    /// turns out not to be that typeface, its own glyph sheet is enlarged instead.
    /// </summary>
    internal static class ScaledSpriteFont
    {
        private const int Padding = 2;

        public static SpriteFont Create(GraphicsDevice device, SpriteFont font, float factor, FontFamily family, bool bold)
        {
            if (device == null || font == null || factor == 1f)
            {
                return font;
            }
            try
            {
                return (family != null ? Redraw(device, font, factor, family, bold) : null) ?? Enlarge(device, font, factor);
            }
            catch (Exception)
            {
                // An unscaled label is better than no main view.
                return font;
            }
        }

        // ------------------------------------------------------------ drawn from the typeface

        private static SpriteFont Redraw(GraphicsDevice device, SpriteFont font, float factor, FontFamily family, bool bold)
        {
            var characters = new List<char>(font.Characters);
            using var probeSurface = new Bitmap(1, 1, PixelFormat.Format32bppPArgb);
            using System.Drawing.Graphics probe = System.Drawing.Graphics.FromImage(probeSurface);
            probe.TextRenderingHint = TextRenderingHint.AntiAlias;
            using var format = new System.Drawing.StringFormat(System.Drawing.StringFormat.GenericTypographic);
            format.FormatFlags |= System.Drawing.StringFormatFlags.MeasureTrailingSpaces;

            FontStyle style = bold && family.IsStyleAvailable(FontStyle.Bold) ? FontStyle.Bold : FontStyle.Regular;
            if (!family.IsStyleAvailable(style))
            {
                return null;
            }

            // Size the typeface so its capital letters are as tall as the sprite font's,
            // measuring the visible ink on both sides because glyph boxes carry padding.
            Texture2D originalSheet = font.Texture;
            XnaColor[] originalPixels = ReadPixels(device, originalSheet);
            const float probeSize = 200f;
            double originalCapitals = 0.0, probeCapitals = 0.0;
            using (var probeFont = new Font(family, probeSize, style, System.Drawing.GraphicsUnit.Pixel))
            using (var capitalSurface = new Bitmap((int)probeSize * 2, (int)probeSize * 2, PixelFormat.Format32bppPArgb))
            using (System.Drawing.Graphics capitals = System.Drawing.Graphics.FromImage(capitalSurface))
            {
                capitals.TextRenderingHint = TextRenderingHint.AntiAlias;
                foreach (char capital in "HEITLXZ")
                {
                    if (!characters.Contains(capital))
                    {
                        continue;
                    }
                    capitals.Clear(System.Drawing.Color.Transparent);
                    capitals.DrawString(capital.ToString(), probeFont, System.Drawing.Brushes.White, 20f, 20f, format);
                    XnaColor[] ink = FromBitmap(capitalSurface);
                    (int first, int last) = InkRows(ink, capitalSurface.Width, 0, 0, capitalSurface.Width, capitalSurface.Height);
                    XnaRectangle box = font.Glyphs[capital].BoundsInTexture;
                    (int originalFirst, int originalLast) = InkRows(originalPixels, originalSheet.Width, box.X, box.Y, box.Width, box.Height);
                    if (first < 0 || originalFirst < 0)
                    {
                        continue;
                    }
                    originalCapitals += originalLast - originalFirst + 1;
                    probeCapitals += last - first + 1;
                }
            }
            if (originalCapitals <= 0.0 || probeCapitals <= 0.0)
            {
                return null;
            }
            double pixelSize = probeSize * originalCapitals / probeCapitals;

            using var drawFont = new Font(family, (float)(pixelSize * factor), style, System.Drawing.GraphicsUnit.Pixel);
            int margin = (int)Math.Ceiling(drawFont.Size * 0.2f) + Padding;
            int cellHeight = (int)Math.Ceiling(drawFont.GetHeight(probe)) + margin * 2;
            var cells = new List<(char Character, float Advance, int X, int Y, int Width)>(characters.Count);
            int sheetWidth = drawFont.Size > 40f ? 2048 : 1024;
            int penX = Padding, penY = Padding;
            foreach (char character in characters)
            {
                float advance = probe.MeasureString(character.ToString(), drawFont, System.Drawing.PointF.Empty, format).Width;
                int width = (int)Math.Ceiling(advance) + margin * 2;
                if (penX + width + Padding > sheetWidth)
                {
                    penX = Padding;
                    penY += cellHeight + Padding;
                }
                cells.Add((character, advance, penX, penY, width));
                penX += width + Padding;
            }
            int sheetHeight = penY + cellHeight + Padding;

            using var sheet = new Bitmap(sheetWidth, sheetHeight, PixelFormat.Format32bppPArgb);
            using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(sheet))
            {
                graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                foreach ((char character, _, int x, int y, _) in cells)
                {
                    graphics.DrawString(character.ToString(), drawFont, System.Drawing.Brushes.White,
                        new System.Drawing.PointF(x + margin, y + margin), format);
                }
            }
            XnaColor[] pixels = FromBitmap(sheet);

            // Line the new glyphs up with the old ones by matching where a capital letter starts.
            int verticalShift = -margin;
            char reference = characters.Contains('H') ? 'H' : characters.Find(char.IsUpper);
            int referenceIndex = cells.FindIndex(cell => cell.Character == reference);
            if (reference != '\0' && referenceIndex >= 0)
            {
                (_, _, int x, int y, int width) = cells[referenceIndex];
                (int top, int bottom) = InkRows(pixels, sheetWidth, x, y, width, cellHeight);
                SpriteFont.Glyph original = font.Glyphs[reference];
                XnaRectangle box = original.BoundsInTexture;
                (int originalTop, int originalBottom) = InkRows(originalPixels, originalSheet.Width, box.X, box.Y, box.Width, box.Height);
                if (top >= 0 && originalTop >= 0)
                {
                    // Match the middle of the letter, which splits any rounding between top and baseline.
                    float originalMiddle = original.Cropping.Y + (originalTop + originalBottom + 1) / 2f;
                    verticalShift = (int)MathF.Round(originalMiddle * factor - (top + bottom + 1) / 2f);
                }
            }

            var bounds = new List<XnaRectangle>(cells.Count);
            var cropping = new List<XnaRectangle>(cells.Count);
            var kerning = new List<Vector3>(cells.Count);
            foreach ((_, float advance, int x, int y, int width) in cells)
            {
                bounds.Add(new XnaRectangle(x, y, width, cellHeight));
                cropping.Add(new XnaRectangle(-margin, verticalShift, width, cellHeight));
                kerning.Add(new Vector3(0f, advance, 0f));
            }
            var texture = new Texture2D(device, sheetWidth, sheetHeight, false, SurfaceFormat.Color);
            texture.SetData(pixels);
            return new SpriteFont(texture, bounds, cropping, characters,
                (int)MathF.Round(font.LineSpacing * factor), font.Spacing * factor, kerning, font.DefaultCharacter);
        }

        // The first and last rows of a region that hold clearly visible ink, or (-1, -1) if it is blank.
        private static (int First, int Last) InkRows(XnaColor[] pixels, int sheetWidth, int x, int y, int width, int height)
        {
            int first = -1, last = -1;
            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    if (pixels[(y + row) * sheetWidth + x + column].A > 96)
                    {
                        if (first < 0)
                        {
                            first = row;
                        }
                        last = row;
                        break;
                    }
                }
            }
            return (first, last);
        }

        // ------------------------------------------------------------ enlarged glyph sheet

        private static SpriteFont Enlarge(GraphicsDevice device, SpriteFont font, float factor)
        {
            Texture2D sheet = font.Texture;
            using Bitmap source = ToBitmap(ReadPixels(device, sheet), sheet.Width, sheet.Height);

            var characters = new List<char>(font.Characters);
            var placements = new List<Placement>(characters.Count);
            int sheetWidth = factor > 1.5f ? 2048 : 1024;
            int penX = Padding, penY = Padding, rowHeight = 0;
            foreach (char character in characters)
            {
                SpriteFont.Glyph glyph = font.Glyphs[character];
                float offsetX = glyph.Cropping.X * factor, offsetY = glyph.Cropping.Y * factor;
                var placement = new Placement
                {
                    Glyph = glyph,
                    CropX = (int)MathF.Floor(offsetX),
                    CropY = (int)MathF.Floor(offsetY),
                };
                placement.FractionX = offsetX - placement.CropX;
                placement.FractionY = offsetY - placement.CropY;
                placement.Width = (int)MathF.Ceiling(glyph.BoundsInTexture.Width * factor + placement.FractionX) + 1;
                placement.Height = (int)MathF.Ceiling(glyph.BoundsInTexture.Height * factor + placement.FractionY) + 1;
                if (penX + placement.Width + Padding > sheetWidth)
                {
                    penX = Padding;
                    penY += rowHeight + Padding;
                    rowHeight = 0;
                }
                placement.X = penX;
                placement.Y = penY;
                penX += placement.Width + Padding;
                rowHeight = Math.Max(rowHeight, placement.Height);
                placements.Add(placement);
            }
            int sheetHeight = penY + rowHeight + Padding;

            var bounds = new List<XnaRectangle>(characters.Count);
            var cropping = new List<XnaRectangle>(characters.Count);
            var kerning = new List<Vector3>(characters.Count);
            using var target = new Bitmap(sheetWidth, sheetHeight, PixelFormat.Format32bppPArgb);
            using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(target))
            using (var attributes = new ImageAttributes())
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = PixelOffsetMode.Half;
                // Mirror at the edges so a glyph never samples its neighbours on the sheet.
                attributes.SetWrapMode(WrapMode.TileFlipXY);
                foreach (Placement placement in placements)
                {
                    XnaRectangle from = placement.Glyph.BoundsInTexture;
                    if (from.Width > 0 && from.Height > 0)
                    {
                        using Bitmap cell = source.Clone(
                            new System.Drawing.Rectangle(from.X, from.Y, from.Width, from.Height), PixelFormat.Format32bppPArgb);
                        var destination = new System.Drawing.PointF[]
                        {
                            new(placement.X + placement.FractionX, placement.Y + placement.FractionY),
                            new(placement.X + placement.FractionX + from.Width * factor, placement.Y + placement.FractionY),
                            new(placement.X + placement.FractionX, placement.Y + placement.FractionY + from.Height * factor),
                        };
                        graphics.DrawImage(cell, destination,
                            new System.Drawing.RectangleF(0f, 0f, from.Width, from.Height), System.Drawing.GraphicsUnit.Pixel, attributes);
                    }
                    bounds.Add(new XnaRectangle(placement.X, placement.Y, placement.Width, placement.Height));
                    cropping.Add(new XnaRectangle(placement.CropX, placement.CropY,
                        (int)MathF.Round(placement.Glyph.Cropping.Width * factor), (int)MathF.Round(placement.Glyph.Cropping.Height * factor)));
                    kerning.Add(new Vector3(placement.Glyph.LeftSideBearing * factor, placement.Glyph.Width * factor,
                        placement.Glyph.RightSideBearing * factor));
                }
            }

            var texture = new Texture2D(device, sheetWidth, sheetHeight, false, SurfaceFormat.Color);
            texture.SetData(FromBitmap(target));
            return new SpriteFont(texture, bounds, cropping, characters,
                (int)MathF.Round(font.LineSpacing * factor), font.Spacing * factor, kerning, font.DefaultCharacter);
        }

        private sealed class Placement
        {
            public SpriteFont.Glyph Glyph;
            public int X, Y, Width, Height, CropX, CropY;
            public float FractionX, FractionY;
        }

        // Font sheets are usually DXT compressed, which cannot be read back directly,
        // so draw the sheet into a plain colour target first.
        private static XnaColor[] ReadPixels(GraphicsDevice device, Texture2D sheet)
        {
            var pixels = new XnaColor[sheet.Width * sheet.Height];
            if (sheet.Format == SurfaceFormat.Color)
            {
                sheet.GetData(pixels);
                return pixels;
            }
            RenderTargetBinding[] previous = device.GetRenderTargets();
            using var target = new RenderTarget2D(device, sheet.Width, sheet.Height, false, SurfaceFormat.Color, DepthFormat.None);
            try
            {
                device.SetRenderTarget(target);
                device.Clear(XnaColor.Transparent);
                using var batch = new SpriteBatch(device);
                batch.Begin(SpriteSortMode.Immediate, BlendState.Opaque, SamplerState.PointClamp,
                    DepthStencilState.None, RasterizerState.CullNone);
                batch.Draw(sheet, Vector2.Zero, XnaColor.White);
                batch.End();
            }
            finally
            {
                device.SetRenderTargets(previous);
            }
            target.GetData(pixels);
            return pixels;
        }

        // Sprite font sheets hold premultiplied RGBA; GDI+ wants premultiplied BGRA.
        private static Bitmap ToBitmap(XnaColor[] pixels, int width, int height)
        {
            var bytes = new byte[pixels.Length * 4];
            for (int i = 0; i < pixels.Length; i++)
            {
                XnaColor pixel = pixels[i];
                bytes[i * 4] = pixel.B;
                bytes[i * 4 + 1] = pixel.G;
                bytes[i * 4 + 2] = pixel.R;
                bytes[i * 4 + 3] = pixel.A;
            }
            var bitmap = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
            BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, width, height),
                ImageLockMode.WriteOnly, PixelFormat.Format32bppPArgb);
            try
            {
                for (int row = 0; row < height; row++)
                {
                    Marshal.Copy(bytes, row * width * 4, data.Scan0 + row * data.Stride, width * 4);
                }
            }
            finally
            {
                bitmap.UnlockBits(data);
            }
            return bitmap;
        }

        private static XnaColor[] FromBitmap(Bitmap bitmap)
        {
            int width = bitmap.Width, height = bitmap.Height;
            var bytes = new byte[width * 4];
            var pixels = new XnaColor[width * height];
            BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, width, height),
                ImageLockMode.ReadOnly, PixelFormat.Format32bppPArgb);
            try
            {
                for (int row = 0; row < height; row++)
                {
                    Marshal.Copy(data.Scan0 + row * data.Stride, bytes, 0, width * 4);
                    for (int column = 0; column < width; column++)
                    {
                        int at = column * 4;
                        // The bytes are already premultiplied, so bypass Color's own alpha handling.
                        pixels[row * width + column] = new XnaColor(bytes[at + 2], bytes[at + 1], bytes[at], bytes[at + 3]);
                    }
                }
            }
            finally
            {
                bitmap.UnlockBits(data);
            }
            return pixels;
        }
    }
}
