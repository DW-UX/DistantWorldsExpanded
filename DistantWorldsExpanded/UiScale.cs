using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

/// <summary>
/// Global UI scale applied to fonts and pixel layout values.
/// The factor is read once at startup, so changing it needs a restart.
/// </summary>
public static class UiScale
{
    public const double Minimum = 0.75;
    public const double Maximum = 2.5;

    /// <summary>The value chosen in the options screen, before it is limited by screen size.</summary>
    public static readonly double Requested = LoadRequested();

    /// <summary>The factor in effect for this run.</summary>
    public static readonly float Factor = LimitToScreen(Requested);

    public static bool IsScaled => Factor != 1f;

    public static float LabelFactor => 1f + (Factor - 1f) * 0.6f;

    private static string SettingsPath
        => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
            "My Games", "Distant Worlds Universe", "UiScale.txt");

    private static double LoadRequested()
    {
        try
        {
            string path = SettingsPath;
            if (File.Exists(path)
                && double.TryParse(File.ReadAllText(path).Trim().Replace(',', '.'),
                    NumberStyles.Float, CultureInfo.InvariantCulture, out double value)
                && value > 0.0)
            {
                return Math.Clamp(value, Minimum, Maximum);
            }
        }
        catch (Exception)
        {
        }
        return 1.0;
    }

    // The UI is laid out for a 1024x768 minimum, so never scale past what the game window can hold.
    private static float LimitToScreen(double requested)
    {
        double limit = Maximum;
        try
        {
            Size size = WindowedSize() ?? Screen.PrimaryScreen.Bounds.Size;
            limit = Math.Max(1.0, Math.Min(size.Width / 1024.0, size.Height / 768.0));
        }
        catch (Exception)
        {
        }
        // Round down, so a scaled 1024x768 never comes out a pixel larger than the window.
        return (float)(Math.Floor(Math.Min(requested, limit) * 1000.0) / 1000.0);
    }

    // Startup.ini switches the game to a window when it sets both SCREENWIDTH and SCREENHEIGHT.
    private static Size? WindowedSize()
    {
        string path = Path.Combine(Application.StartupPath, "Startup.ini");
        if (!File.Exists(path))
        {
            return null;
        }
        int width = 0, height = 0;
        foreach (string line in File.ReadAllLines(path))
        {
            string[] parts = line.Trim().Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 2 || !double.TryParse(parts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
            {
                continue;
            }
            if (parts[0].Equals("screenwidth", StringComparison.OrdinalIgnoreCase))
            {
                width = (int)value;
            }
            else if (parts[0].Equals("screenheight", StringComparison.OrdinalIgnoreCase))
            {
                height = (int)value;
            }
        }
        return width >= 1024 && height >= 768 ? new Size(width, height) : null;
    }

    /// <summary>Stores the factor for the next start. Returns true if it differs from the one in effect.</summary>
    public static bool Save(double factor)
    {
        factor = Math.Clamp(factor, Minimum, Maximum);
        try
        {
            string path = SettingsPath;
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllText(path, factor.ToString("0.###", CultureInfo.InvariantCulture));
        }
        catch (Exception)
        {
        }
        return Math.Abs(factor - Requested) > 0.0005;
    }

    /// <summary>The size of the framework's default font, before any scaling.</summary>
    public const float DefaultFontPoints = 9f;

    private static readonly System.Runtime.CompilerServices.ConditionalWeakTable<Control, object> ScaledLayouts = new();

    /// <summary>
    /// Scales a container whose layout comes straight from the designer, with unscaled numbers.
    /// Call it once, right after the container has built that layout.
    /// </summary>
    public static void ScaleLayout(Control container)
    {
        if (!IsScaled || ScaledLayouts.TryGetValue(container, out _))
        {
            return;
        }
        // From here on the layout is scaled explicitly, so stop the framework from also deriving it from the font.
        if (container is ContainerControl own)
        {
            own.AutoScaleMode = AutoScaleMode.None;
        }
        // Children without a font of their own inherit this one.
        Font font = container.Font;
        if (Math.Abs(font.SizeInPoints - DefaultFontPoints) < 0.05f)
        {
            container.Font = new Font(font.FontFamily, S(font.SizeInPoints), font.Style);
        }
        container.Scale(new SizeF(Factor, Factor));
        UndoRepeatedScaling(container);
        ScalePictures(container);
        ScaledLayouts.Add(container, null);
    }

    // A nested container that already scaled its own layout has now had it scaled a second time.
    private static void UndoRepeatedScaling(Control parent)
    {
        foreach (Control child in parent.Controls)
        {
            if (ScaledLayouts.TryGetValue(child, out _))
            {
                foreach (Control inner in child.Controls)
                {
                    inner.Scale(new SizeF(1f / Factor, 1f / Factor));
                }
            }
            else
            {
                UndoRepeatedScaling(child);
            }
        }
    }

    private static readonly System.Runtime.CompilerServices.ConditionalWeakTable<Image, object> EnlargedImages = new();

    /// <summary>
    /// Makes picture boxes that draw their image at its own size (rather than fitting it to the box)
    /// enlarge that image, because the boxes themselves have been scaled.
    /// </summary>
    public static void ScalePictures(Control root)
    {
        if (!IsScaled)
        {
            return;
        }
        foreach (Control child in root.Controls)
        {
            if (child is PictureBox box)
            {
                box.Paint -= EnlargePicture;
                box.Paint += EnlargePicture;
            }
            ScalePictures(child);
        }
    }

    private static void EnlargePicture(object sender, PaintEventArgs e)
    {
        var box = (PictureBox)sender;
        // A picture made for the unscaled box would otherwise repeat to fill the larger one.
        if (box.BackgroundImage != null && box.BackgroundImageLayout == ImageLayout.Tile)
        {
            box.BackgroundImageLayout = ImageLayout.Zoom;
        }
        Image image = box.Image;
        if (image == null || box.SizeMode is not (PictureBoxSizeMode.Normal or PictureBoxSizeMode.CenterImage)
            || EnlargedImages.TryGetValue(image, out _))
        {
            return;
        }
        int width = S(image.Width), height = S(image.Height);
        // An image that would no longer fit was already made for the scaled box.
        if (width > box.Width + 2 || height > box.Height + 2)
        {
            EnlargedImages.Add(image, null);
            return;
        }
        var enlarged = new Bitmap(width, height);
        using (Graphics graphics = Graphics.FromImage(enlarged))
        {
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            graphics.DrawImage(image, 0, 0, width, height);
        }
        EnlargedImages.Add(enlarged, null);
        box.Image = enlarged;
    }

    private static readonly System.Runtime.CompilerServices.ConditionalWeakTable<Image, Image> EnlargedCopies = new();

    /// <summary>
    /// Returns a copy of a fixed-size image enlarged by the UI scale, or the image itself when
    /// the UI is not scaled or the enlarged copy would not fit within <paramref name="bounds"/>.
    /// </summary>
    public static Image Enlarged(Image image, Size bounds)
    {
        if (!IsScaled || image == null)
        {
            return image;
        }
        int width = S(image.Width), height = S(image.Height);
        if (width > bounds.Width || height > bounds.Height)
        {
            return image;
        }
        if (!EnlargedCopies.TryGetValue(image, out Image copy))
        {
            var bitmap = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                graphics.DrawImage(image, 0, 0, width, height);
            }
            copy = bitmap;
            EnlargedCopies.Add(image, copy);
        }
        return copy;
    }

    public static int S(int value)
    {
        if (Factor == 1f || value == 0)
        {
            return value;
        }
        int scaled = (int)MathF.Round(value * Factor);
        return scaled != 0 ? scaled : Math.Sign(value);
    }

    public static float S(float value) => value * Factor;

    /// <summary>The smallest font, in unscaled pixels, used once the UI is scaled.</summary>
    public const float MinimumFontPixels = 11f;

    /// <summary>Raises an already scaled pixel size to the readable minimum.</summary>
    public static float Readable(float scaledPixels)
        => IsScaled ? Math.Max(scaledPixels, S(MinimumFontPixels)) : scaledPixels;

    // The game's tiniest captions are 8 pixels high; lift those more than the rest when scaling.
    private static float FontSize(float emSize, GraphicsUnit unit = GraphicsUnit.Point)
    {
        if (!IsScaled)
        {
            return emSize;
        }
        float minimum = unit switch
        {
            GraphicsUnit.Pixel or GraphicsUnit.World => MinimumFontPixels,
            GraphicsUnit.Point => MinimumFontPixels * 72f / 96f,
            _ => 0f,
        };
        return S(Math.Max(emSize, minimum));
    }

    public static double S(double value) => value * Factor;

    public static Font NewFont(FontFamily family, float emSize)
        => new Font(family, FontSize(emSize));

    public static Font NewFont(FontFamily family, float emSize, FontStyle style)
        => new Font(family, FontSize(emSize), style);

    public static Font NewFont(FontFamily family, float emSize, GraphicsUnit unit)
        => new Font(family, FontSize(emSize, unit), unit);

    public static Font NewFont(FontFamily family, float emSize, FontStyle style, GraphicsUnit unit)
        => new Font(family, FontSize(emSize, unit), style, unit);

    public static Font NewFont(FontFamily family, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet)
        => new Font(family, FontSize(emSize, unit), style, unit, gdiCharSet);

    public static Font NewFont(FontFamily family, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet, bool gdiVerticalFont)
        => new Font(family, FontSize(emSize, unit), style, unit, gdiCharSet, gdiVerticalFont);

    public static Font NewFont(string familyName, float emSize)
        => new Font(familyName, FontSize(emSize));

    public static Font NewFont(string familyName, float emSize, FontStyle style)
        => new Font(familyName, FontSize(emSize), style);

    public static Font NewFont(string familyName, float emSize, GraphicsUnit unit)
        => new Font(familyName, FontSize(emSize, unit), unit);

    public static Font NewFont(string familyName, float emSize, FontStyle style, GraphicsUnit unit)
        => new Font(familyName, FontSize(emSize, unit), style, unit);

    public static Font NewFont(string familyName, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet)
        => new Font(familyName, FontSize(emSize, unit), style, unit, gdiCharSet);

    public static Font NewFont(string familyName, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet, bool gdiVerticalFont)
        => new Font(familyName, FontSize(emSize, unit), style, unit, gdiCharSet, gdiVerticalFont);
}
