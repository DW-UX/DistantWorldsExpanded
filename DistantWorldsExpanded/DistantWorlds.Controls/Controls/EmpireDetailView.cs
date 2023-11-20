// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.EmpireDetailView
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using BaconDistantWorlds;
using DistantWorlds.Types;
//using DistantWorlds.Controls.Mods;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class EmpireDetailView : GradientPanel
    {
        private Font _TitleFont = new Font("Verdana", 13.5f, FontStyle.Bold);
        private Color _TitleFontColor = Color.White;
        private Font _HeaderFont = new Font("Verdana", 11.5f, FontStyle.Bold);
        private Color _HeaderFontColor = Color.FromArgb(200, 200, 200);
        private Font _NormalFont = new Font("Verdana", 8f, FontStyle.Regular);
        private Font _NormalFontBold = new Font("Verdana", 8f, FontStyle.Bold);
        private Color _NormalFontColor = Color.FromArgb(200, 200, 200);
        private Font _LargeFont = new Font("Verdana", 8f, FontStyle.Bold);
        private Game _Game;
        private Empire _Empire;
        private Empire _PlayerEmpire;
        private RaceList _EmpireRaces;
        private List<double> _EmpireRacePopulationAmounts;
        private List<string> _DominantRaceCharacteristics;
        private double _GalaxyIntoleranceLevel;
        private CharacterImageCache _CharacterImageCache;
        private RaceImageCache _RaceImageCache;
        private GlassButton btnEmpireDetailAcceptTreaty;
        private Color _MutualDefenseColor = Color.FromArgb(64, 64, 232);
        private Color _ProtectorateColor = Color.FromArgb(112, 112, (int)byte.MaxValue);
        private Color _FreeTradeColor = Color.FromArgb(0, (int)byte.MaxValue, 0);
        private Color _NoneColor = Color.FromArgb(128, 128, 128);
        private Color _TruceColor = Color.Yellow;
        private Color _SubjugatedColor = Color.Yellow;
        private Color _TradeSanctionsColor = Color.Orange;
        private Color _WarColor = Color.FromArgb((int)byte.MaxValue, 0, 0);
        public TradeRestrictedResourcesPanel tradeRestrictedResourcesPanel;
        private Color _NotMetColor = Color.Tan;
        private Color _PirateProtectionColor = Color.FromArgb(160, 160, (int)byte.MaxValue);
        private bool _LargeSize;

        public event EmpireDetailView.TreatyAcceptedEventHandler _TreatyAccepted;
        //public static event EventHandler<SetColorForDiplomacyBackgroundModsArgs> SetColorForDiplomacyBackgroundMods;

        public override void SetFontCache(IFontCache fontCache)
        {
            this._FontCache = fontCache;
            this._TitleFont = this._FontCache.GenerateFont(FontSize.Title, true);
            this._HeaderFont = this._FontCache.GenerateFont(FontSize.Heading, true);
            this._NormalFont = this._FontCache.GenerateFont(FontSize.Normal, false);
            this._NormalFontBold = this._FontCache.GenerateFont(FontSize.Normal, true);
            this._LargeFont = this._FontCache.GenerateFont(FontSize.Large, true);
            this.btnEmpireDetailAcceptTreaty.Font = this._NormalFontBold;
        }

        public bool LargeSize => this._LargeSize;

        public void Kickstart(Size screenSize)
        {
            if (screenSize.Width >= 1180 && screenSize.Height >= 900)
            {
                this._LargeSize = true;
                this._TitleFont = this._FontCache.GenerateFont(FontSize.Title, true);
                this._HeaderFont = this._FontCache.GenerateFont(20f, true);
                this._NormalFont = this._FontCache.GenerateFont(18f, false);
                this._NormalFontBold = this._FontCache.GenerateFont(18f, true);
                this.btnEmpireDetailAcceptTreaty.Font = this._NormalFontBold;
                this.btnEmpireDetailAcceptTreaty.Location = new Point(250, 496);
            }
            else
            {
                this._LargeSize = false;
                this._TitleFont = this._FontCache.GenerateFont(FontSize.Title, true);
                this._HeaderFont = this._FontCache.GenerateFont(FontSize.Heading, true);
                this._NormalFont = this._FontCache.GenerateFont(FontSize.Normal, false);
                this._NormalFontBold = this._FontCache.GenerateFont(FontSize.Normal, true);
                this.btnEmpireDetailAcceptTreaty.Font = this._NormalFontBold;
                this.btnEmpireDetailAcceptTreaty.Location = new Point(230, 456);
            }
        }

        public EmpireDetailView()
        {
            this.InitializeComponent();
            this.btnEmpireDetailAcceptTreaty.BringToFront();
            this.btnEmpireDetailAcceptTreaty.Size = new Size(130, 25);
            this.btnEmpireDetailAcceptTreaty.Location = new Point(230, 456);
        }

        public void InitializeImages(RaceImageCache raceImageCache) => this._RaceImageCache = raceImageCache;

        private Bitmap PrescaleImage(Bitmap originalBitmap, int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage((Image)bitmap);
            graphics.InterpolationMode = InterpolationMode.High;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.DrawImage((Image)originalBitmap, new Rectangle(0, 0, width, height));
            graphics.Dispose();
            return bitmap;
        }

        public void ClearData()
        {
            this._Game = (Game)null;
            this._Empire = (Empire)null;
            this._PlayerEmpire = (Empire)null;
            this._EmpireRaces = (RaceList)null;
            this._EmpireRacePopulationAmounts = (List<double>)null;
            this.tradeRestrictedResourcesPanel.ClearSettings();
        }

        public void BindData(
          Game game,
          Empire empire,
          Empire playerEmpire,
          CharacterImageCache characterImageCache)
        {
            this._Game = game;
            this._Empire = empire;
            this._PlayerEmpire = playerEmpire;
            this._EmpireRaces = new RaceList();
            this._EmpireRacePopulationAmounts = new List<double>();
            this._CharacterImageCache = characterImageCache;
            this._GalaxyIntoleranceLevel = this._Game.Galaxy.IntoleranceLevel;
            this._EmpireRaces.Add(this._Empire.DominantRace);
            this._EmpireRacePopulationAmounts.Add(0.0);
            this.Invalidate();
            for (int index1 = 0; index1 < this._Empire.Colonies.Count; ++index1)
            {
                Habitat colony = this._Empire.Colonies[index1];
                for (int index2 = 0; index2 < colony.Population.Count; ++index2)
                {
                    Population population = colony.Population[index2];
                    int num = this._EmpireRaces.IndexOf(population.Race);
                    if (num >= 0)
                    {
                        List<double> populationAmounts;
                        int index3;
                        (populationAmounts = this._EmpireRacePopulationAmounts)[index3 = num] = populationAmounts[index3] + (double)population.Amount / 1000000.0;
                    }
                    else
                    {
                        this._EmpireRaces.Add(population.Race);
                        this._EmpireRacePopulationAmounts.Add((double)population.Amount / 1000000.0);
                    }
                }
            }
            this._DominantRaceCharacteristics = Galaxy.ResolveRaceCharacteristics(this._Empire.DominantRace);
            if (this._Empire != null)
            {
                Color color = BaconMain.SetColorForDiplomacyBackground(this._Empire);
                //Color color = EmpireDetailView.OnSetColorForDiplomacyBackgroundMods(this._Empire);
                this.BackColor = Color.FromArgb(39, 40, 44);
                this.BackColor2 = color;
            }
            else
            {
                this.BackColor = Color.FromArgb(39, 40, 44);
                this.BackColor2 = Color.FromArgb(22, 21, 26);
            }
            if (empire != game.PlayerEmpire && empire.PirateEmpireBaseHabitat == null)
            {
                this.tradeRestrictedResourcesPanel.Size = new Size(this.Width - 40, this.tradeRestrictedResourcesPanel.Height);
                this.tradeRestrictedResourcesPanel.BindSettings(this._Game.Galaxy, empire);
                if (this.tradeRestrictedResourcesPanel.ShouldBeVisible)
                {
                    this.tradeRestrictedResourcesPanel.Visible = true;
                    this.tradeRestrictedResourcesPanel.Parent = (Control)this;
                    this.tradeRestrictedResourcesPanel.Location = new Point(20, this.Height - (15 + this.tradeRestrictedResourcesPanel.Height));
                    this.tradeRestrictedResourcesPanel.BringToFront();
                }
                else
                    this.tradeRestrictedResourcesPanel.Visible = false;
            }
            else
                this.tradeRestrictedResourcesPanel.Visible = false;
        }

        private Color ResolveLightColor(Color color, int increaseAmount) => Color.FromArgb(Math.Max(0, Math.Min((int)byte.MaxValue, (int)color.R + increaseAmount)), Math.Max(0, Math.Min((int)byte.MaxValue, (int)color.G + increaseAmount)), Math.Max(0, Math.Min((int)byte.MaxValue, (int)color.B + increaseAmount)));

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this.DrawEmpireDetail(this._Empire, e.Graphics);
        }

        private void DrawEmpireDetail(Empire empire, Graphics graphics)
        {
            try
            {
                if (empire == null)
                    return;
                bool flag1 = false;
                if (empire.PirateEmpireBaseHabitat != null)
                    flag1 = true;
                SolidBrush brush1 = new SolidBrush(this._NormalFontColor);
                SolidBrush brush2 = new SolidBrush(this._HeaderFontColor);
                graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                graphics.DrawImage((Image)empire.LargeFlagPicture, new Point(20, 20));
                Rectangle rect1 = new Rectangle(140, 20, 60, 60);
                GraphicsHelper.SetGraphicsQualityToHigh(graphics);
                graphics.DrawImage((Image)this._RaceImageCache.GetEmpireDominantRaceImage(empire), rect1);
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = PixelOffsetMode.Default;
                int num1 = 1;
                int y1 = 20;
                for (int index = 1; index < this._EmpireRaces.Count; ++index)
                {
                    if (index == 5)
                    {
                        y1 = 53;
                        num1 = 1;
                    }
                    if (index >= 9)
                    {
                        graphics.DrawString("...", this._NormalFont, (Brush)brush1, new PointF(340f, 48f));
                        break;
                    }
                    rect1 = new Rectangle(220 + (num1 - 1) * 30, y1, 27, 27);
                    graphics.DrawImage((Image)this._RaceImageCache.GetRaceImage(this._EmpireRaces[index].PictureRef), rect1);
                    ++num1;
                }
                SizeF sizeF1 = graphics.MeasureString(empire.Name, this._TitleFont);
                using (SolidBrush brush3 = new SolidBrush(this._TitleFontColor))
                    GraphicsHelper.DrawStringWithDropShadow(graphics, empire.Name, this._TitleFont, new Point(20, 85), brush3);
                string highestAllianceName = empire.DiplomaticRelations.GetHighestAllianceName();
                if (!string.IsNullOrEmpty(highestAllianceName))
                {
                    SizeF sizeF2 = graphics.MeasureString(highestAllianceName, this._NormalFontBold, 340 - (int)sizeF1.Width);
                    int num2 = 340 - (int)sizeF1.Width;
                    int height = (int)sizeF1.Height;
                    int x = 20 + (int)sizeF1.Width + (num2 - (int)sizeF2.Width) / 2;
                    int y2 = 85 + (height - (int)sizeF2.Height) / 2;
                    GraphicsHelper.DrawStringWithDropShadow(graphics, highestAllianceName, this._NormalFontBold, new Point(x, y2));
                }
                SizeF sizeF3 = graphics.MeasureString(TextResolver.GetText("Government") + ": ", this._NormalFont, 300, StringFormat.GenericTypographic);
                int num3 = 10 + (int)sizeF3.Width;
                SolidBrush solidBrush1 = new SolidBrush(Color.FromArgb(96, 0, 0, 0));
                if (flag1)
                {
                    if (this.btnEmpireDetailAcceptTreaty != null)
                        this.btnEmpireDetailAcceptTreaty.Visible = false;
                    Rectangle rect2 = new Rectangle(12, 115, 356, 555);
                    int width1 = 300;
                    float num4 = 15f;
                    float num5 = 8f;
                    float num6 = 25f;
                    float num7 = 10f;
                    if (this._LargeSize)
                    {
                        rect2 = new Rectangle(12, 115, 496, 695);
                        width1 = 480;
                        num4 = 20f;
                        num5 = 11f;
                        num6 = 33f;
                        num7 = 13f;
                    }
                    graphics.FillRectangle((Brush)solidBrush1, rect2);
                    float y3 = 120f;
                    sizeF3 = graphics.MeasureString(TextResolver.GetText("Pirate Base") + " ", this._NormalFont, width1, StringFormat.GenericTypographic);
                    int num8 = 20 + (int)sizeF3.Width;
                    sizeF3 = graphics.MeasureString(TextResolver.GetText("Pirate Base") + " ", this._NormalFont, width1, StringFormat.GenericTypographic);
                    double width2 = (double)sizeF3.Width;
                    string empty = string.Empty;
                    for (int index = 0; index < this._PlayerEmpire.KnownPirateBases.Count; ++index)
                    {
                        BuiltObject knownPirateBase = this._PlayerEmpire.KnownPirateBases[index];
                        if (knownPirateBase != null && !knownPirateBase.HasBeenDestroyed && knownPirateBase.Empire == empire)
                        {
                            if (knownPirateBase.ParentHabitat != null)
                                empty += string.Format(TextResolver.GetText("ITEM at LOCATION (NAME system)"), (object)knownPirateBase.Name, (object)knownPirateBase.ParentHabitat.Name, (object)Galaxy.DetermineHabitatSystemStar(knownPirateBase.ParentHabitat).Name);
                            else if (knownPirateBase.NearestSystemStar != null)
                                empty += string.Format(TextResolver.GetText("ITEM at LOCATION (NAME system)"), (object)knownPirateBase.Name, (object)knownPirateBase.NearestSystemStar.Name, (object)Galaxy.DetermineHabitatSystemStar(knownPirateBase.NearestSystemStar).Name);
                            empty += "\n";
                        }
                    }
                    string str1 = "(" + TextResolver.GetText("Unknown pirate base") + ")";
                    if (!string.IsNullOrEmpty(empty))
                        str1 = empty;
                    graphics.DrawString(str1, this._NormalFontBold, (Brush)brush1, new PointF(18f, y3));
                    SizeF sizeF4 = graphics.MeasureString(str1, this._NormalFontBold);
                    float y4 = y3 + sizeF4.Height + num7 + num7;
                    int x1 = 20;
                    GraphicsHelper.DrawStringWithDropShadow(graphics, string.Format(TextResolver.GetText("Pirate Playstyle Description"), (object)Galaxy.ResolveDescription(empire.PiratePlayStyle)), this._NormalFontBold, new Point(x1, (int)y4));
                    float y5 = y4 + num6;
                    int x2 = 40;
                    GraphicsHelper.DrawStringWithDropShadow(graphics, TextResolver.GetText("Controlled Colonies") + ": " + empire.Colonies.Count.ToString("0"), this._NormalFont, new Point(x2, (int)y5));
                    float y6 = y5 + num4 + num5;
                    GraphicsHelper.DrawStringWithDropShadow(graphics, TextResolver.GetText("Destroyed Ships and Bases") + ": " + (empire.Counters.DestroyedEnemyMilitaryShipCount + empire.Counters.DestroyedEnemyCivilianShipCount).ToString("0"), this._NormalFont, new Point(x2, (int)y6));
                    float y7 = y6 + num4;
                    GraphicsHelper.DrawStringWithDropShadow(graphics, TextResolver.GetText("Captured Ships and Bases") + ": " + empire.Counters.CaptureShipCount.ToString("0"), this._NormalFont, new Point(x2, (int)y7));
                    float y8 = y7 + num4;
                    GraphicsHelper.DrawStringWithDropShadow(graphics, TextResolver.GetText("Successful Raids") + ": " + empire.Counters.RaidSuccessCount.ToString("0"), this._NormalFont, new Point(x2, (int)y8));
                    float y9 = y8 + num4;
                    GraphicsHelper.DrawStringWithDropShadow(graphics, TextResolver.GetText("Successful Intelligence Missions") + ": " + (empire.Counters.IntelligenceMissionSuccessEspionageCount + empire.Counters.IntelligenceMissionSuccessSabotageCount).ToString("0"), this._NormalFont, new Point(x2, (int)y9));
                    float y10 = y9 + num4 + num5;
                    int num9 = empire.BuiltObjects.CountByRole(BuiltObjectRole.Military);
                    int num10 = empire.BuiltObjects.TotalMobileMilitaryFirepower();
                    GraphicsHelper.DrawStringWithDropShadow(graphics, TextResolver.GetText("Military Ships") + ": " + num9.ToString("0") + "     (" + num10.ToString(TextResolver.GetText("firepower format")) + ")", this._NormalFont, new Point(x2, (int)y10));
                    float y11 = y10 + num4;
                    int num11 = empire.PrivateBuiltObjects.CountByRole(BuiltObjectRole.Freight);
                    GraphicsHelper.DrawStringWithDropShadow(graphics, TextResolver.GetText("Smuggling Freighters") + ": " + num11.ToString("0"), this._NormalFont, new Point(x2, (int)y11));
                    float y12 = y11 + num4;
                    GraphicsHelper.DrawStringWithDropShadow(graphics, TextResolver.GetText("Completed Attack Missions") + ": " + empire.Counters.CompletedPirateMissionAttackCount.ToString("0"), this._NormalFont, new Point(x2, (int)y12));
                    float y13 = y12 + num4;
                    GraphicsHelper.DrawStringWithDropShadow(graphics, TextResolver.GetText("Completed Defense Missions") + ": " + empire.Counters.CompletedPirateMissionDefendCount.ToString("0"), this._NormalFont, new Point(x2, (int)y13));
                    float y14 = y13 + num4 + num6;
                    if (empire == this._PlayerEmpire)
                        return;
                    PirateRelation pirateRelation = empire.ObtainPirateRelation(this._PlayerEmpire);
                    if (pirateRelation.Type == PirateRelationType.Protection)
                    {
                        if (this._PlayerEmpire.PirateEmpireBaseHabitat != null)
                        {
                            string text = TextResolver.GetText("Pirate Protection Description New Pirates");
                            GraphicsHelper.DrawStringWithDropShadow(graphics, text, this._HeaderFont, new Point(20, (int)y14), brush1);
                        }
                        else
                        {
                            double protectionFeeToThisEmpire = pirateRelation.MonthlyProtectionFeeToThisEmpire;
                            string text = string.Format(TextResolver.GetText("Pirate Protection Description New"), (object)protectionFeeToThisEmpire.ToString("0"));
                            GraphicsHelper.DrawStringWithDropShadow(graphics, text, this._HeaderFont, new Point(20, (int)y14), brush1);
                        }
                        y14 = y14 + num4 + num4;
                    }
                    string text1 = string.Format(TextResolver.GetText("FEELING with us"), (object)this._Game.PlayerEmpire.ResolveFeelingDescription(pirateRelation)) + " (" + pirateRelation.Evaluation.ToString("+0;-0;0") + ")";
                    GraphicsHelper.DrawStringWithDropShadow(graphics, text1, this._NormalFontBold, new Point(20, (int)y14), brush1);
                    float y15 = y14 + 18f;
                    EmpireRelationshipFactorList relationshipFactors = this._Game.PlayerEmpire.DetermineEmpireRelationshipFactors(this._Empire);
                    using (SolidBrush solidBrush2 = new SolidBrush(Color.LightGreen))
                    {
                        using (SolidBrush solidBrush3 = new SolidBrush(Color.Red))
                        {
                            for (int index = 0; index < relationshipFactors.Count; ++index)
                            {
                                double num12 = relationshipFactors[index].Value;
                                string text2 = relationshipFactors[index].Description + " (" + num12.ToString("+0;-0;0") + ")";
                                StringFormat format = new StringFormat((StringFormatFlags)0);
                                SizeF sizeF5 = graphics.MeasureString(text2, this._NormalFont, width1, format);
                                RectangleF rectangleF1 = new RectangleF(20f, y15, sizeF5.Width + 1f, sizeF5.Height + 1f);
                                RectangleF rectangleF2 = new RectangleF(21f, y15 + 1f, sizeF5.Width + 1f, sizeF5.Height + 1f);
                                SizeF maxSize = new SizeF((float)width1, sizeF5.Height);
                                if (num12 < 0.0)
                                    GraphicsHelper.DrawStringWithDropShadow(graphics, text2, this._NormalFont, new Point(20, (int)y15), (Brush)solidBrush3, maxSize);
                                else
                                    GraphicsHelper.DrawStringWithDropShadow(graphics, text2, this._NormalFont, new Point(20, (int)y15), (Brush)solidBrush2, maxSize);
                                y15 += (float)((int)sizeF5.Height - 1);
                            }
                        }
                    }
                    if (empire.PirateMissions == null || empire.PirateMissions.ResolveActivitiesByType(EmpireActivityType.Attack).Count <= 0)
                        return;
                    EmpireActivityList empireActivityList = empire.PirateMissions.ResolveActivitiesByType(EmpireActivityType.Attack);
                    int num13 = 0;
                    for (int index = 0; index < empireActivityList.Count; ++index)
                    {
                        if (empireActivityList[index].RequestingEmpire == this._PlayerEmpire)
                        {
                            Empire targetEmpire = empireActivityList[index].TargetEmpire;
                            long expiryDate = empireActivityList[index].ExpiryDate;
                            StellarObject target = empireActivityList[index].Target;
                            string str2 = string.Empty;
                            if (target != null)
                                str2 = target.Name;
                            double price = empireActivityList[index].Price;
                            string text3 = string.Format(TextResolver.GetText("Pirate Attack Description New"), (object)str2, (object)targetEmpire.Name, (object)price.ToString("0"));
                            GraphicsHelper.DrawStringWithDropShadow(graphics, text3, this._NormalFont, new Point(20, (int)y15), brush1);
                            ++num13;
                            y15 = y15 + num7 + num5;
                        }
                    }
                }
                else
                {
                    Rectangle rect3 = new Rectangle(12, 115, 356, 72);
                    Rectangle rect4 = new Rectangle(12, 196, 356, 182);
                    Rectangle rect5 = new Rectangle(12, 387, 356, 280);
                    int y16 = 441;
                    int width3 = 300;
                    int x3 = 210;
                    float num14 = 12f;
                    float num15 = 10f;
                    int num16 = 30;
                    int num17 = 18;
                    int num18 = 13;
                    int num19 = 40;
                    int x4 = 65;
                    if (this._LargeSize)
                    {
                        rect3 = new Rectangle(12, 115, 496, 97);
                        rect4 = new Rectangle(12, 221, 496, 232);
                        rect5 = new Rectangle(12, 462, 496, 345);
                        y16 = 516;
                        width3 = 480;
                        x3 = 280;
                        num14 = 17f;
                        num15 = 14f;
                        num16 = 40;
                        num17 = 24;
                        num18 = 15;
                        num19 = 53;
                        x4 = 78;
                    }
                    graphics.FillRectangle((Brush)solidBrush1, rect3);
                    float y17 = 121f;
                    bool flag2 = false;
                    if (empire.Capital != null)
                    {
                        if (this._Game.PlayerEmpire.CheckSystemExplored(empire.Capital.SystemIndex))
                            flag2 = true;
                        sizeF3 = graphics.MeasureString(TextResolver.GetText("Capital") + " ", this._NormalFont, width3, StringFormat.GenericTypographic);
                        int width4 = (int)sizeF3.Width;
                        GraphicsHelper.DrawStringWithDropShadow(graphics, TextResolver.GetText("Capital"), this._NormalFont, new Point(num3 - width4, (int)y17), brush1);
                    }
                    string text4 = "(" + TextResolver.GetText("Unknown") + ")";
                    if (flag2)
                        text4 = empire.Capital.Name + " (" + Galaxy.DetermineHabitatSystemStar(empire.Capital).Name + " " + TextResolver.GetText("System").ToLower(CultureInfo.InvariantCulture) + ")";
                    GraphicsHelper.DrawStringWithDropShadow(graphics, text4, this._NormalFontBold, new Point(num3 + 5, (int)y17 - 2), brush1);
                    float y18 = y17 + num14;
                    sizeF3 = graphics.MeasureString(TextResolver.GetText("Government"), this._NormalFont, width3, StringFormat.GenericTypographic);
                    int width5 = (int)sizeF3.Width;
                    GraphicsHelper.DrawStringWithDropShadow(graphics, TextResolver.GetText("Government"), this._NormalFont, new Point(num3 - width5, (int)y18), brush1);
                    SolidBrush brush4 = brush1;
                    if (empire.GovernmentAttributes.Availability == 3)
                        brush4 = new SolidBrush(Color.FromArgb(192, 48, 48));
                    else if (empire.GovernmentAttributes.Availability == 2)
                        brush4 = new SolidBrush(Color.Gold);
                    GraphicsHelper.DrawStringWithDropShadow(graphics, empire.GovernmentAttributes.Name, this._NormalFontBold, new Point(num3 + 5, (int)y18 - 2), brush4);
                    float y19 = y18 + num14;
                    sizeF3 = graphics.MeasureString(TextResolver.GetText("Reputation"), this._NormalFont, width3, StringFormat.GenericTypographic);
                    int width6 = (int)sizeF3.Width;
                    GraphicsHelper.DrawStringWithDropShadow(graphics, TextResolver.GetText("Reputation"), this._NormalFont, new Point(num3 - width6, (int)y19), brush1);
                    GraphicsHelper.DrawStringWithDropShadow(graphics, empire.CivilityDescription(), this._NormalFontBold, new Point(num3 + 5, (int)y19 - 2), brush1);
                    float y20 = y19 + num14;
                    if (empire.Colonies != null)
                    {
                        sizeF3 = graphics.MeasureString(TextResolver.GetText("Colonies"), this._NormalFont, width3, StringFormat.GenericTypographic);
                        int width7 = (int)sizeF3.Width;
                        GraphicsHelper.DrawStringWithDropShadow(graphics, TextResolver.GetText("Colonies"), this._NormalFont, new Point(num3 - width7, (int)y20), brush1);
                        GraphicsHelper.DrawStringWithDropShadow(graphics, empire.Colonies.Count.ToString("##0"), this._NormalFontBold, new Point(num3 + 5, (int)y20 - 2), brush1);
                        y20 += num14;
                    }
                    sizeF3 = graphics.MeasureString(TextResolver.GetText("Population"), this._NormalFont, width3, StringFormat.GenericTypographic);
                    int width8 = (int)sizeF3.Width;
                    GraphicsHelper.DrawStringWithDropShadow(graphics, TextResolver.GetText("Population"), this._NormalFont, new Point(num3 - width8, (int)y20), brush1);
                    double num20 = (double)empire.TotalPopulation / 1000000.0;
                    GraphicsHelper.DrawStringWithDropShadow(graphics, num20.ToString("###,###,##0") + "M", this._NormalFontBold, new Point(num3 + 5, (int)y20 - 2), brush1);
                    float num21 = y20 + num14;
                    int num22 = x3;
                    sizeF3 = graphics.MeasureString(TextResolver.GetText("Military Strength") + ": ", this._NormalFont, width3, StringFormat.GenericTypographic);
                    int width9 = (int)sizeF3.Width;
                    int num23 = num22 + width9;
                    float y21 = 121f + num14;
                    int num24 = (int)(empire.AnnualTaxRevenue / 1000.0);
                    sizeF3 = graphics.MeasureString(TextResolver.GetText("Tax Revenue"), this._NormalFont, width3, StringFormat.GenericTypographic);
                    int width10 = (int)sizeF3.Width;
                    GraphicsHelper.DrawStringWithDropShadow(graphics, TextResolver.GetText("Tax Revenue"), this._NormalFont, new Point(num23 - width10, (int)y21), brush1);
                    GraphicsHelper.DrawStringWithDropShadow(graphics, num24.ToString("#####0K"), this._NormalFontBold, new Point(num23 + 5, (int)y21 - 2), brush1);
                    float y22 = y21 + num14;
                    int num25 = (int)(empire.PrivateAnnualRevenue / 1000.0);
                    sizeF3 = graphics.MeasureString(TextResolver.GetText("Annual GDP"), this._NormalFont, 300, StringFormat.GenericTypographic);
                    int width11 = (int)sizeF3.Width;
                    GraphicsHelper.DrawStringWithDropShadow(graphics, TextResolver.GetText("Annual GDP"), this._NormalFont, new Point(num23 - width11, (int)y22), brush1);
                    GraphicsHelper.DrawStringWithDropShadow(graphics, num25.ToString("######0K"), this._NormalFontBold, new Point(num23 + 5, (int)y22 - 2), brush1);
                    float y23 = y22 + num14;
                    int num26 = empire.TotalColonyStrategicValue / 1000;
                    sizeF3 = graphics.MeasureString(TextResolver.GetText("Strategic Value"), this._NormalFont, 300, StringFormat.GenericTypographic);
                    int width12 = (int)sizeF3.Width;
                    GraphicsHelper.DrawStringWithDropShadow(graphics, TextResolver.GetText("Strategic Value"), this._NormalFont, new Point(num23 - width12, (int)y23), brush1);
                    GraphicsHelper.DrawStringWithDropShadow(graphics, num26.ToString("#####0K"), this._NormalFontBold, new Point(num23 + 5, (int)y23 - 2), brush1);
                    float y24 = y23 + num14;
                    sizeF3 = graphics.MeasureString(TextResolver.GetText("Military Strength"), this._NormalFont, 300, StringFormat.GenericTypographic);
                    int width13 = (int)sizeF3.Width;
                    GraphicsHelper.DrawStringWithDropShadow(graphics, TextResolver.GetText("Military Strength"), this._NormalFont, new Point(num23 - width13, (int)y24), brush1);
                    GraphicsHelper.DrawStringWithDropShadow(graphics, empire.MilitaryPotency.ToString("######0"), this._NormalFontBold, new Point(num23 + 5, (int)y24 - 2), brush1);
                    num21 = y24 + num14;
                    graphics.FillRectangle((Brush)solidBrush1, rect4);
                    GraphicsHelper.DrawStringWithDropShadow(graphics, TextResolver.GetText("Dominant Race"), this._HeaderFont, new Point(rect4.Left + 8, rect4.Top + 8), brush2);
                    string name1 = this._EmpireRaces[0].Name;
                    string text5 = "(" + string.Format(TextResolver.GetText("RACE family"), (object)Galaxy.ResolveRaceFamilyDescription(this._EmpireRaces[0].FamilyId)) + ")";
                    string text6 = this._EmpireRacePopulationAmounts[0].ToString("###,##0M");
                    rect1 = new Rectangle(rect4.Left + 8, rect4.Top + 33, num19, num19);
                    graphics.DrawImage((Image)this._RaceImageCache.GetRaceImage(this._EmpireRaces[0].PictureRef), rect1);
                    float y25 = (float)(rect4.Top + 33);
                    GraphicsHelper.DrawStringWithDropShadow(graphics, name1, this._NormalFontBold, new Point(x4, (int)y25), brush1);
                    float y26 = y25 + num14;
                    GraphicsHelper.DrawStringWithDropShadow(graphics, text5, this._NormalFont, new Point(x4, (int)y26), brush1);
                    float y27 = y26 + num14;
                    GraphicsHelper.DrawStringWithDropShadow(graphics, text6, this._NormalFont, new Point(x4, (int)y27), brush1);
                    float num27 = y27 + num14 + num15;
                    for (int index = 0; index < this._DominantRaceCharacteristics.Count; ++index)
                        GraphicsHelper.DrawStringWithDropShadow(graphics, this._DominantRaceCharacteristics[index], this._NormalFontBold, new Point(x3, index * (int)num14 + rect4.Top + 12), brush1);
                    List<string> stringList = empire.ResolveEmpireAbilityBonusDescriptions();
                    for (int index = 0; index < stringList.Count; ++index)
                        GraphicsHelper.DrawStringWithDropShadow(graphics, stringList[index], this._NormalFont, new Point(20, index * (int)num14 + (int)num27), brush1);
                    if (this._Game.PlayerEmpire.PirateEmpireBaseHabitat != null)
                    {
                        if (this._Empire == null)
                            return;
                        PirateRelation pirateRelation1 = this._Game.PlayerEmpire.ObtainPirateRelation(this._Empire);
                        if (this._Game.PlayerEmpire == this._Empire)
                            return;
                        string text7 = string.Empty;
                        Color color = Color.White;
                        switch (pirateRelation1.Type)
                        {
                            case PirateRelationType.NotMet:
                                color = this._NotMetColor;
                                break;
                            case PirateRelationType.None:
                                color = this._NoneColor;
                                text7 = TextResolver.GetText("None");
                                break;
                            case PirateRelationType.Protection:
                                color = this._PirateProtectionColor;
                                text7 = TextResolver.GetText("Pirate Protection");
                                break;
                        }
                        SolidBrush solidBrush4 = new SolidBrush(Color.FromArgb(96, 0, 0, 0));
                        graphics.FillRectangle((Brush)solidBrush4, rect5);
                        GraphicsHelper.DrawStringWithDropShadow(graphics, TextResolver.GetText("Current Relationship With Us"), this._HeaderFont, new Point(20, rect5.Top + 5), brush2);
                        using (SolidBrush brush5 = new SolidBrush(color))
                        {
                            using (Font font = this._FontCache.GenerateFont(FontSize.Large, true))
                                GraphicsHelper.DrawStringWithDropShadow(graphics, text7, font, new Point(25, rect5.Top + 28), brush5);
                        }
                        PirateRelation pirateRelation2 = empire.ObtainPirateRelation(this._Game.PlayerEmpire);
                        string text8 = string.Format(TextResolver.GetText("FEELING with us"), (object)this._Game.PlayerEmpire.ResolveFeelingDescription(pirateRelation2)) + " (" + pirateRelation2.Evaluation.ToString("+0;-0;0") + ")";
                        GraphicsHelper.DrawStringWithDropShadow(graphics, text8, this._NormalFontBold, new Point(20, y16), brush1);
                        if (pirateRelation1.Type == PirateRelationType.Protection)
                        {
                            y16 += num16;
                            double num28 = pirateRelation1.MonthlyProtectionFeeToThisEmpire * 12.0;
                            string text9 = string.Format(TextResolver.GetText("Pirate Protection Payment Description"), (object)pirateRelation1.MonthlyProtectionFeeToThisEmpire.ToString("0"), (object)num28.ToString("0"));
                            GraphicsHelper.DrawStringWithDropShadow(graphics, text9, this.Font, new Point(20, y16), brush1);
                        }
                        int y28 = y16 + num17;
                        EmpireRelationshipFactorList relationshipFactors = this._Game.PlayerEmpire.DetermineEmpireRelationshipFactors(this._Empire);
                        for (int index = 0; index < relationshipFactors.Count; ++index)
                        {
                            double num29 = relationshipFactors[index].Value;
                            string text10 = relationshipFactors[index].Description + " (" + num29.ToString("+0;-0;0") + ")";
                            StringFormat format = new StringFormat((StringFormatFlags)0);
                            SizeF sizeF6 = graphics.MeasureString(text10, this._NormalFont, width3, format);
                            RectangleF rectangleF3 = new RectangleF(20f, (float)y28, sizeF6.Width + 1f, sizeF6.Height + 1f);
                            RectangleF rectangleF4 = new RectangleF(21f, (float)y28 + 1f, sizeF6.Width + 1f, sizeF6.Height + 1f);
                            SizeF maxSize = new SizeF((float)width3, sizeF6.Height);
                            if (num29 < 0.0)
                                GraphicsHelper.DrawStringWithDropShadow(graphics, text10, this._NormalFont, new Point(20, y28), (Brush)new SolidBrush(Color.Red), maxSize);
                            else
                                GraphicsHelper.DrawStringWithDropShadow(graphics, text10, this._NormalFont, new Point(20, y28), (Brush)new SolidBrush(Color.LightGreen), maxSize);
                            y28 += (int)sizeF6.Height - 1;
                        }
                    }
                    else
                    {
                        DiplomaticRelation diplomaticRelation1 = this._Game.PlayerEmpire.ObtainDiplomaticRelation(this._Empire);
                        if (this._Game.PlayerEmpire != this._Empire)
                        {
                            string text11 = Galaxy.ResolveDescription(diplomaticRelation1.Type);
                            Color color1 = Color.White;
                            switch (diplomaticRelation1.Type)
                            {
                                case DiplomaticRelationType.NotMet:
                                    color1 = this._NotMetColor;
                                    break;
                                case DiplomaticRelationType.None:
                                    color1 = this._NoneColor;
                                    break;
                                case DiplomaticRelationType.FreeTradeAgreement:
                                    color1 = this._FreeTradeColor;
                                    break;
                                case DiplomaticRelationType.MutualDefensePact:
                                    color1 = this._MutualDefenseColor;
                                    break;
                                case DiplomaticRelationType.SubjugatedDominion:
                                    color1 = this._SubjugatedColor;
                                    text11 = diplomaticRelation1.Initiator != this._Game.PlayerEmpire ? text11 + " (" + TextResolver.GetText("They subjugate us") + ")" : text11 + " (" + TextResolver.GetText("We subjugate them") + ")";
                                    break;
                                case DiplomaticRelationType.Protectorate:
                                    color1 = this._ProtectorateColor;
                                    text11 = diplomaticRelation1.Initiator != this._Game.PlayerEmpire ? text11 + " (" + TextResolver.GetText("They protect us") + ")" : text11 + " (" + TextResolver.GetText("We protect them") + ")";
                                    break;
                                case DiplomaticRelationType.TradeSanctions:
                                    color1 = this._TradeSanctionsColor;
                                    break;
                                case DiplomaticRelationType.War:
                                    color1 = this._WarColor;
                                    break;
                                case DiplomaticRelationType.Truce:
                                    color1 = this._TruceColor;
                                    break;
                            }
                            SolidBrush solidBrush5 = new SolidBrush(Color.FromArgb(96, 0, 0, 0));
                            graphics.FillRectangle((Brush)solidBrush5, rect5);
                            GraphicsHelper.DrawStringWithDropShadow(graphics, TextResolver.GetText("Current Relationship With Us"), this._HeaderFont, new Point(20, rect5.Top + 5), brush2);
                            SolidBrush brush6 = new SolidBrush(color1);
                            Font largeFont = this._LargeFont;
                            GraphicsHelper.DrawStringWithDropShadow(graphics, text11, largeFont, new Point(25, rect5.Top + 28), brush6);
                            CharacterList locationNotTransferring = this._Game.PlayerEmpire.Characters.FindCharactersAtLocationNotTransferring((StellarObject)this._Empire.Capital, CharacterRole.Ambassador);
                            Character character = (Character)null;
                            if (locationNotTransferring.Count > 0)
                                character = locationNotTransferring[0];
                            if (character != null)
                            {
                                int x5 = rect5.Right - 88;
                                int y29 = rect5.Top + 5;
                                Point point = new Point(x5 - 40, y29);
                                Bitmap characterImageSmall = this._CharacterImageCache.ObtainCharacterImageSmall(character);
                                if (characterImageSmall != null && characterImageSmall.PixelFormat != PixelFormat.Undefined)
                                    graphics.DrawImage((Image)characterImageSmall, point);
                                string text12 = Galaxy.ResolveDescription(character.Role);
                                string name2 = character.Name;
                                string text13 = ((double)character.Diplomacy / 100.0).ToString("+#0%;-#0%");
                                if (!character.BonusesKnown)
                                    text13 = "?%";
                                GraphicsHelper.DrawStringWithDropShadow(graphics, text12, this._NormalFont, new Point(x5, y29), brush1);
                                int y30 = y29 + num18;
                                GraphicsHelper.DrawStringWithDropShadow(graphics, name2, this._NormalFont, new Point(x5, y30), brush1);
                                int y31 = y30 + num18;
                                GraphicsHelper.DrawStringWithDropShadow(graphics, text13, this._NormalFont, new Point(x5, y31), brush1);
                            }
                            DiplomaticRelation diplomaticRelation2 = (DiplomaticRelation)null;
                            if (this._PlayerEmpire.ProposedDiplomaticRelations != null)
                                diplomaticRelation2 = this._PlayerEmpire.ProposedDiplomaticRelations[this._Empire];
                            if (diplomaticRelation2 != null)
                            {
                                bool flag3 = true;
                                long num30 = (long)(Galaxy.TreatyOfferValidYears * (double)Galaxy.RealSecondsInGalacticYear * 1000.0);
                                if (this._Game.Galaxy.CurrentStarDate > diplomaticRelation2.LastDiplomacyTradeOfferDate + num30)
                                    flag3 = false;
                                DiplomaticRelation diplomaticRelation3 = this._Empire.ObtainDiplomaticRelation(this._PlayerEmpire);
                                if (this._Empire.DetermineDesiredDiplomaticRelationTypical(diplomaticRelation3.Strategy, diplomaticRelation3.Type) != diplomaticRelation2.Type)
                                    flag3 = false;
                                if (!flag3)
                                    this._PlayerEmpire.ProposedDiplomaticRelations.Remove(diplomaticRelation2);
                                if (flag3)
                                {
                                    string text14 = string.Empty;
                                    Color color2 = Color.White;
                                    switch (diplomaticRelation2.Type)
                                    {
                                        case DiplomaticRelationType.None:
                                            switch (diplomaticRelation1.Type)
                                            {
                                                case DiplomaticRelationType.FreeTradeAgreement:
                                                case DiplomaticRelationType.MutualDefensePact:
                                                case DiplomaticRelationType.Protectorate:
                                                    text14 = TextResolver.GetText("Cancelling Treaty");
                                                    color2 = this._NoneColor;
                                                    break;
                                                case DiplomaticRelationType.SubjugatedDominion:
                                                    if (diplomaticRelation1.Initiator == this._PlayerEmpire)
                                                    {
                                                        text14 = TextResolver.GetText("Request release from Subjugation");
                                                        color2 = this._NoneColor;
                                                        break;
                                                    }
                                                    text14 = TextResolver.GetText("Offering release from Subjugation");
                                                    color2 = this._NoneColor;
                                                    break;
                                                case DiplomaticRelationType.TradeSanctions:
                                                    text14 = TextResolver.GetText("Lifting Trade Sanctions");
                                                    color2 = this._NoneColor;
                                                    break;
                                                case DiplomaticRelationType.War:
                                                    text14 = TextResolver.GetText("Ending War");
                                                    color2 = this._NoneColor;
                                                    break;
                                                case DiplomaticRelationType.Truce:
                                                    text14 = TextResolver.GetText("Peace Treaty");
                                                    color2 = this._NoneColor;
                                                    break;
                                            }
                                            break;
                                        case DiplomaticRelationType.FreeTradeAgreement:
                                            text14 = Galaxy.ResolveDescription(DiplomaticRelationType.FreeTradeAgreement);
                                            color2 = this._FreeTradeColor;
                                            break;
                                        case DiplomaticRelationType.MutualDefensePact:
                                            text14 = Galaxy.ResolveDescription(DiplomaticRelationType.MutualDefensePact);
                                            color2 = this._MutualDefenseColor;
                                            break;
                                        case DiplomaticRelationType.SubjugatedDominion:
                                            text14 = Galaxy.ResolveDescription(DiplomaticRelationType.SubjugatedDominion);
                                            color2 = this._SubjugatedColor;
                                            break;
                                        case DiplomaticRelationType.Protectorate:
                                            text14 = Galaxy.ResolveDescription(DiplomaticRelationType.Protectorate);
                                            color2 = this._ProtectorateColor;
                                            break;
                                        case DiplomaticRelationType.TradeSanctions:
                                            text14 = Galaxy.ResolveDescription(DiplomaticRelationType.TradeSanctions);
                                            color2 = this._TradeSanctionsColor;
                                            break;
                                        case DiplomaticRelationType.War:
                                            text14 = Galaxy.ResolveDescription(DiplomaticRelationType.War);
                                            color2 = this._WarColor;
                                            break;
                                        case DiplomaticRelationType.Truce:
                                            text14 = Galaxy.ResolveDescription(DiplomaticRelationType.Truce);
                                            color2 = this._TruceColor;
                                            break;
                                    }
                                    GraphicsHelper.DrawStringWithDropShadow(graphics, TextResolver.GetText("Treaty on Offer"), this._HeaderFont, new Point(20, rect5.Top + 59), brush2);
                                    SolidBrush brush7 = new SolidBrush(color2);
                                    GraphicsHelper.DrawStringWithDropShadow(graphics, text14, this._NormalFontBold, new Point(25, rect5.Top + 84), brush7);
                                    if (this.btnEmpireDetailAcceptTreaty != null)
                                        this.btnEmpireDetailAcceptTreaty.Visible = true;
                                    y16 += 52;
                                }
                                else if (this.btnEmpireDetailAcceptTreaty != null)
                                    this.btnEmpireDetailAcceptTreaty.Visible = false;
                            }
                            else if (this.btnEmpireDetailAcceptTreaty != null)
                                this.btnEmpireDetailAcceptTreaty.Visible = false;
                            EmpireEvaluation empireEvaluation = empire.ObtainEmpireEvaluation(this._Game.PlayerEmpire);
                            if (empireEvaluation == null)
                                return;
                            int y32 = y16 + 6;
                            string text15 = string.Format(TextResolver.GetText("FEELING with us"), (object)this._Game.PlayerEmpire.ResolveFeelingDescription(empireEvaluation)) + " (" + empireEvaluation.OverallAttitude.ToString("+0;-0;0") + ")";
                            GraphicsHelper.DrawStringWithDropShadow(graphics, text15, this._NormalFontBold, new Point(20, y32), brush1);
                            int y33 = y32 + num17;
                            EmpireRelationshipFactorList relationshipFactors = this._Game.PlayerEmpire.DetermineEmpireRelationshipFactors(this._Empire);
                            for (int index = 0; index < relationshipFactors.Count; ++index)
                            {
                                double num31 = relationshipFactors[index].Value;
                                string text16 = relationshipFactors[index].Description + " (" + num31.ToString("+0;-0;0") + ")";
                                StringFormat format = new StringFormat((StringFormatFlags)0);
                                SizeF sizeF7 = graphics.MeasureString(text16, this._NormalFont, width3, format);
                                RectangleF rectangleF5 = new RectangleF(20f, (float)y33, sizeF7.Width + 1f, sizeF7.Height + 1f);
                                RectangleF rectangleF6 = new RectangleF(21f, (float)y33 + 1f, sizeF7.Width + 1f, sizeF7.Height + 1f);
                                SizeF maxSize = new SizeF((float)width3, sizeF7.Height);
                                if (num31 < 0.0)
                                    GraphicsHelper.DrawStringWithDropShadow(graphics, text16, this._NormalFont, new Point(20, y33), (Brush)new SolidBrush(Color.Red), maxSize);
                                else
                                    GraphicsHelper.DrawStringWithDropShadow(graphics, text16, this._NormalFont, new Point(20, y33), (Brush)new SolidBrush(Color.LightGreen), maxSize);
                                y33 += (int)sizeF7.Height - 1;
                            }
                        }
                        else
                        {
                            if (this.btnEmpireDetailAcceptTreaty == null)
                                return;
                            this.btnEmpireDetailAcceptTreaty.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void InitializeComponent()
        {
            this.btnEmpireDetailAcceptTreaty = new GlassButton();
            this.tradeRestrictedResourcesPanel = new TradeRestrictedResourcesPanel();
            this.SuspendLayout();
            this.btnEmpireDetailAcceptTreaty.BackColor = Color.FromArgb(0, 0, 0);
            this.btnEmpireDetailAcceptTreaty.ClipBackground = false;
            this.btnEmpireDetailAcceptTreaty.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
            this.btnEmpireDetailAcceptTreaty.ForeColor = Color.FromArgb(150, 150, 150);
            this.btnEmpireDetailAcceptTreaty.GlowColor = Color.FromArgb(48, 48, 128);
            this.btnEmpireDetailAcceptTreaty.InnerBorderColor = Color.FromArgb(67, 67, 77);
            this.btnEmpireDetailAcceptTreaty.IntensifyColors = false;
            this.btnEmpireDetailAcceptTreaty.Location = new Point(0, 0);
            this.btnEmpireDetailAcceptTreaty.Name = "btnEmpireDetailAcceptTreaty";
            this.btnEmpireDetailAcceptTreaty.OuterBorderColor = Color.FromArgb(0, 0, 16);
            this.btnEmpireDetailAcceptTreaty.ShineColor = Color.FromArgb(96, 96, 104);
            this.btnEmpireDetailAcceptTreaty.Size = new Size(75, 28);
            this.btnEmpireDetailAcceptTreaty.TabIndex = 0;
            this.btnEmpireDetailAcceptTreaty.Text = TextResolver.GetText("Accept Offer");
            this.btnEmpireDetailAcceptTreaty.TextColor = Color.FromArgb(120, 120, 120);
            this.btnEmpireDetailAcceptTreaty.TextColor2 = Color.FromArgb((int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue);
            this.btnEmpireDetailAcceptTreaty.Visible = false;
            this.btnEmpireDetailAcceptTreaty.Click += new EventHandler(this.btnEmpireDetailAcceptTreaty_Click);
            this.tradeRestrictedResourcesPanel.BackColor = Color.Transparent;
            this.tradeRestrictedResourcesPanel.Location = new Point(0, 0);
            this.tradeRestrictedResourcesPanel.Name = "tradeRestrictedResourcesPanel";
            this.tradeRestrictedResourcesPanel.Size = new Size(196, 76);
            this.tradeRestrictedResourcesPanel.TabIndex = 0;
            this.Controls.Add((Control)this.btnEmpireDetailAcceptTreaty);
            this.ResumeLayout(false);
        }

        private void btnEmpireDetailAcceptTreaty_Click(object sender, EventArgs e)
        {
            DiplomaticRelation diplomaticRelation1 = this._PlayerEmpire.ProposedDiplomaticRelations[this._Empire];
            if (diplomaticRelation1 == null)
                return;
            DiplomaticRelation diplomaticRelation2 = this._PlayerEmpire.DiplomaticRelations[this._Empire] ?? new DiplomaticRelation(DiplomaticRelationType.NotMet, this._PlayerEmpire, this._PlayerEmpire, this._Empire, false);
            switch (diplomaticRelation1.Type)
            {
                case DiplomaticRelationType.None:
                case DiplomaticRelationType.SubjugatedDominion:
                case DiplomaticRelationType.Truce:
                    switch (diplomaticRelation2.Type)
                    {
                        case DiplomaticRelationType.TradeSanctions:
                            this._PlayerEmpire.ChangeDiplomaticRelation(diplomaticRelation2, diplomaticRelation1.Type);
                            this._PlayerEmpire.CancelBlockades(this._Empire);
                            this._Empire.CancelBlockades(this._PlayerEmpire);
                            break;
                        case DiplomaticRelationType.War:
                            this._PlayerEmpire.ResetAttitudeLevelsAtEndOfWar(diplomaticRelation2);
                            diplomaticRelation2.Type = diplomaticRelation1.Type;
                            diplomaticRelation2.LastDiplomacyTradeOfferDate = this._Game.Galaxy.CurrentStarDate;
                            DiplomaticRelation diplomaticRelation3 = this._Empire.DiplomaticRelations[this._PlayerEmpire];
                            if (diplomaticRelation3 == null)
                            {
                                diplomaticRelation3 = new DiplomaticRelation(DiplomaticRelationType.NotMet, this._Empire, this._Empire, this._PlayerEmpire, false);
                                this._Empire.DiplomaticRelations.Add(diplomaticRelation3);
                            }
                            diplomaticRelation3.Type = diplomaticRelation1.Type;
                            diplomaticRelation3.LastDiplomacyTradeOfferDate = this._Game.Galaxy.CurrentStarDate;
                            this._PlayerEmpire.ProcessEndOfWarWithEmpire(this._Empire);
                            this._Empire.ProcessEndOfWarWithEmpire(this._PlayerEmpire);
                            break;
                    }
                    break;
                default:
                    this._PlayerEmpire.ChangeDiplomaticRelation(diplomaticRelation2, diplomaticRelation1.Type);
                    break;
            }
            this._PlayerEmpire.ProposedDiplomaticRelations.Remove(diplomaticRelation1);
            this.Invalidate();
            this._TreatyAccepted((object)this._Empire, new EventArgs());
        }

        public delegate void TreatyAcceptedEventHandler(object sender, EventArgs e);

        //private static Color OnSetColorForDiplomacyBackgroundMods(Empire empire)
        //{
        //    var tmp = EmpireDetailView.SetColorForDiplomacyBackgroundMods;
        //    Color res = empire.MainColor;
        //    if (tmp != null)
        //    {
        //        var args = new SetColorForDiplomacyBackgroundModsArgs(empire);
        //        tmp(null, args);
        //        res = args.Result;
        //    }
        //    return res;
        //}
    }
}
