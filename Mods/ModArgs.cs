using DistantWorlds.Types;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistantWorlds.Controls.Mods
{
    public class BindDataGenericModsArgs
    {
        public BuiltObjectListView BoListView { get; }
        public StellarObjectList StellarObjects { get; }
        public Galaxy Galaxy { get; }
        public bool ShowDetails { get; }
        public BindDataGenericModsArgs(BuiltObjectListView boListView, StellarObjectList stellarObjects, Galaxy galaxy, bool showDetails)
        {
            BoListView = boListView;
            StellarObjects = stellarObjects;
            Galaxy = galaxy;
            ShowDetails = showDetails;
        }
    }
    public class FormatForLargeNumbersModsArgs
    {
        public string Result { get; set; }
        public long Value { get; }

        public FormatForLargeNumbersModsArgs(long value)
        {
            Value = value;
        }
    }
    public class DrawBuiltObjectModsArgs
    {
        public InfoPanel InfoPanel { get; }
        public BuiltObject BuiltObject { get; }
        public Graphics Graphics { get; }

        public DrawBuiltObjectModsArgs(InfoPanel infoPanel, BuiltObject builtObject, Graphics graphics)
        {
            InfoPanel = infoPanel;
            BuiltObject = builtObject;
            Graphics = graphics;
        }
    }
    public class DrawStringRedWithDropShadowtModsArgs
    {
        public InfoPanel Panel { get; }
        public Graphics Graphics { get; }
        public string Text { get; }
        public Font Font { get; }
        public Point Location { get; }

        public DrawStringRedWithDropShadowtModsArgs(InfoPanel panel, Graphics graphics, string text, Font font, Point location)
        {
            Graphics = graphics;
            Text = text;
            Font = font;
            Location = location;
            Panel = panel;
        }
    }
    public class DrawStringWithDropShadowModsArgs
    {
        public InfoPanel Panel { get; }
        public Graphics Graphics { get; }
        public string Text { get; }
        public Font Font { get; }
        public Point Location { get; }
        public SolidBrush Brush { get; }

        public DrawStringWithDropShadowModsArgs(InfoPanel panel, Graphics graphics, string text, Font font, Point location,      SolidBrush brush)
        {
            Graphics = graphics;
            Text = text;
            Font = font;
            Location = location;
            Panel = panel;
            Brush = brush;
        }
    }
    public class DrawStringColorWithDropShadowModsArgs
    {
        public InfoPanel Panel { get; }
        public Graphics Graphics { get; }
        public string Text { get; }
        public Font Font { get; }
        public Point Location { get; }
        public Color Color { get; }

        public DrawStringColorWithDropShadowModsArgs(InfoPanel panel, Graphics graphics, string text, Font font, Point location, Color color)
        {
            Graphics = graphics;
            Text = text;
            Font = font;
            Location = location;
            Panel = panel;
            Color = color;
        }
    }
    public class DrawStringWithDropShadowBoundedModsArgs
    {
        public InfoPanel Panel { get; }
        public Graphics Graphics { get; }
        public string Text { get; }
        public Font Font { get; }
        public Point Location { get; }
        public SizeF Size { get; }
        public SolidBrush Brush { get; }

        public DrawStringWithDropShadowBoundedModsArgs(InfoPanel panel, Graphics graphics, string text, Font font, Point location, SizeF size,      SolidBrush brush)
        {
            Graphics = graphics;
            Text = text;
            Font = font;
            Location = location;
            Panel = panel;
            Size = size;
            Brush = brush;
        }
    }
    public class DrawShipGroupModsArgs
    {
        public InfoPanel Panel { get; }
        public ShipGroup ShipGroup { get; }
        public Graphics Graphics { get; }

        public DrawShipGroupModsArgs(InfoPanel panel, ShipGroup shipGroup, Graphics graphics)
        {
            Panel = panel;
            ShipGroup = shipGroup;
            Graphics = graphics;
        }
    }
    public class DrawStringWithDropShadowModsArgs2
    {
        public InfoPanel Panel { get; }
        public Graphics Graphics { get; }
        public string Text { get; }
        public Font Font { get; }
        public Point Location { get; }
        public SolidBrush Brush { get; }
        public int MaxWidth { get; }

        public DrawStringWithDropShadowModsArgs2(InfoPanel panel, Graphics graphics, string text, Font font, Point location, SolidBrush brush, int maxWidth)
        {
            Panel = panel;
            Graphics = graphics;
            Text = text;
            Font = font;
            Location = location;
            Brush = brush;
            MaxWidth = maxWidth;
        }
    }
    public class BindDataMessageImagesModsArgs
    {
        public EmpireMessageListView Emlv { get; }
        public EmpireMessageList EmpireMessages { get; }
        public int Indexer { get; }
        public Bitmap Original { get; }
        public Bitmap Result { get; set; }

        public BindDataMessageImagesModsArgs(EmpireMessageListView emlv, EmpireMessageList empireMessages, int indexer, Bitmap original)
        {
            Emlv = emlv;
            EmpireMessages = empireMessages;
            Indexer = indexer;
            Original = original;
        }
    }
    public class SetColorForDiplomacyBackgroundModsArgs
    {
        public Color Result { get; }
        public Empire Empire { get; }

        public SetColorForDiplomacyBackgroundModsArgs(Empire empire)
        {
            Empire = empire;
        }
    }
}

