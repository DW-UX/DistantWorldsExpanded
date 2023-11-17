// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.DiplomacyTradeTree
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using BaconDistantWorlds;
using DistantWorlds.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class DiplomacyTradeTree : UserControl
    {
        private Galaxy _Galaxy;

        public Empire _Empire;

        public Empire _OtherEmpire;

        public TradeableItemList _TradeableItems = new TradeableItemList();

        public TradeableItemList _SelectedItems = new TradeableItemList();

        public TradeableItemList _RequiredItems = new TradeableItemList();

        private TradeableItemList _ExcludedItems = new TradeableItemList();

        private bool _AllowDiplomaticThreats;

        private bool _RefactorValuesForEmpire;

        private bool _MoneyToggled;

        private bool _ColoniesToggled;

        private bool _BasesToggled;

        private bool _ResearchToggled;

        private bool _GovernmentStylesToggled;

        private bool _DeclareWarToggled;

        private bool _EndWarToggled;

        private bool _TradeSanctionsToggled;

        private bool _LiftTradeSanctionsToggled;

        private bool _EmpireContactsToggled;

        private bool _SecretLocationsToggled;

        private bool _IndependentColoniesToggled;

        public bool AcceptingMouseMoveEvents = true;

        protected IFontCache _FontCache;

        private float _FontSize = 15.33f;

        private bool _FontIsBold;

        private IContainer components;

        private TreeView TradeableItems;

        public ListBox SelectedItems;

        private Label lblTitle;

        private Label lblSelected;

        private GlassButton btnClearSelectedItems;

        private PictureBox picFlag;

        private System.Windows.Forms.Panel pnlTitleHeaderArea;

        private System.Windows.Forms.Panel pnlSelectedHeaderArea;

        public Empire Empire => _Empire;

        public TradeableItemList SelectedTradeItems => _SelectedItems;

        public TradeableItemList RequiredItems => _RequiredItems;

        public TradeableItemList ExcludedItems => _ExcludedItems;

        public event EventHandler<ResearchProjectHoveredEventArgs> ResearchProjectHovered;

        public virtual void SetFontCache(IFontCache fontCache)
        {
            _FontCache = fontCache;
            if (_FontSize > 0f)
            {
                Font = _FontCache.GenerateFont(_FontSize, _FontIsBold);
            }
        }

        public void SetFont(float pointSize)
        {
            SetFont(pointSize, isBold: false);
        }

        public void SetFont(float pointSize, bool isBold)
        {
            _FontSize = pointSize;
            _FontIsBold = isBold;
            if (_FontCache != null)
            {
                Font = _FontCache.GenerateFont(_FontSize, _FontIsBold);
            }
        }

        public DiplomacyTradeTree()
        {
            InitializeComponent();
            TradeableItems.ShowPlusMinus = true;
            TradeableItems.ShowLines = false;
            TradeableItems.LineColor = Color.FromArgb(24, 24, 80);
        }

        public void DoBind(Galaxy galaxy, Empire empire, Empire otherEmpire, bool allowDiplomaticThreats, bool refactorValuesForEmpire)
        {
            DoLayout();
            _Galaxy = galaxy;
            _Empire = empire;
            _OtherEmpire = otherEmpire;
            _AllowDiplomaticThreats = allowDiplomaticThreats;
            _RefactorValuesForEmpire = refactorValuesForEmpire;
            lblTitle.Text = empire.Name;
            picFlag.Image = PrecacheScaledBitmap(empire.LargeFlagPicture, picFlag.Width, picFlag.Height, InterpolationMode.HighQualityBicubic, CompositingQuality.HighQuality, SmoothingMode.AntiAlias);
        }

        private void LocalizeControlText()
        {
            btnClearSelectedItems.Text = TextResolver.GetText("Clear Offered Items");
        }

        public void SetSelectedItems(TradeableItemList selectedItems)
        {
            SetSelectedItems(selectedItems, new TradeableItemList(), new TradeableItemList());
        }

        public void SetSelectedItems(TradeableItemList selectedItems, TradeableItemList requiredItems, TradeableItemList excludedItems)
        {
            _RequiredItems = requiredItems;
            _ExcludedItems = excludedItems;
            SelectedItems.Items.Clear();
            _SelectedItems.Clear();
            if (_RequiredItems != null)
            {
                _SelectedItems.AddRange(_RequiredItems);
            }
            if (selectedItems != null)
            {
                _SelectedItems.AddRange(selectedItems);
            }
            if (_SelectedItems != null)
            {
                foreach (TradeableItem selectedItem in _SelectedItems)
                {
                    SelectedItems.Items.Add(selectedItem);
                }
            }
            TradeableItemList tradeableItems = FilterOutSelectedAndExcludedItems(_TradeableItems);
            PopulateTradeableItems(tradeableItems);
            UpdateOfferedItemsHeading();
        }

        private void SetSelectedItemsInControl()
        {
            SelectedItems.Items.Clear();
            if (_SelectedItems == null)
            {
                return;
            }
            foreach (TradeableItem selectedItem in _SelectedItems)
            {
                if (_Empire != _Galaxy.PlayerEmpire)
                {
                    selectedItem.ShowSecretLocationNames = false;
                }
                else
                {
                    selectedItem.ShowSecretLocationNames = true;
                }
                SelectedItems.Items.Add(selectedItem);
            }
        }

        private Bitmap PrecacheScaledBitmap(Bitmap unscaledBitmap, int width, int height, InterpolationMode interpolation, CompositingQuality compositing, SmoothingMode smoothing)
        {
            if (width < 1)
            {
                width = 1;
            }
            if (height < 1)
            {
                height = 1;
            }
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.InterpolationMode = interpolation;
            graphics.CompositingQuality = compositing;
            graphics.SmoothingMode = smoothing;
            graphics.DrawImage(unscaledBitmap, new Rectangle(0, 0, width, height));
            graphics.Dispose();
            return bitmap;
        }

        public void Reset()
        {
            _MoneyToggled = true;
            _ColoniesToggled = true;
            _BasesToggled = true;
            _ResearchToggled = true;
            _GovernmentStylesToggled = true;
            _DeclareWarToggled = true;
            _EndWarToggled = true;
            _TradeSanctionsToggled = true;
            _LiftTradeSanctionsToggled = true;
            _EmpireContactsToggled = true;
            _IndependentColoniesToggled = true;
            _SecretLocationsToggled = true;
            bool includeAllItems = false;
            if (_Empire == _Galaxy.PlayerEmpire)
            {
                includeAllItems = true;
            }
            _TradeableItems = _Galaxy.ResolveTradeableItems(_Empire, _OtherEmpire, includeNearestColony: false, _RefactorValuesForEmpire, includeAllItems);
            if (_RequiredItems != null)
            {
                _RequiredItems.Clear();
            }
            _SelectedItems = new TradeableItemList();
            SelectedItems.Items.Clear();
            PopulateTradeableItems(_TradeableItems);
            UpdateOfferedItemsHeading();
        }

        public void UpdateOfferedItemsHeading()
        {
            lblSelected.Text = TextResolver.GetText("Offered Items") + " (" + _SelectedItems.TotalValue.ToString("###,###,###,##0") + ")";
        }

        public void PopulateTradeableItems(TradeableItemList tradeableItems)
        {
            TradeableItems.Nodes.Clear();
            if (_ExcludedItems != null && _ExcludedItems.Count > 0)
            {
                TradeableItemList tradeableItemList = new TradeableItemList();
                foreach (TradeableItem tradeableItem in tradeableItems)
                {
                    int num = _ExcludedItems.IndexOf(tradeableItem);
                    if (num >= 0)
                    {
                        tradeableItemList.Add(tradeableItem);
                    }
                }
                foreach (TradeableItem item in tradeableItemList)
                {
                    tradeableItems.Remove(item);
                }
            }
            if (_SelectedItems != null && _SelectedItems.Count > 0)
            {
                if (_SelectedItems.ContainsType(TradeableItemType.TerritoryMap))
                {
                    int num2 = tradeableItems.IndexOf(new TradeableItem(TradeableItemType.GalaxyMap, null, 0));
                    if (num2 >= 0)
                    {
                        tradeableItems.RemoveAt(num2);
                    }
                }
                else if (_SelectedItems.ContainsType(TradeableItemType.GalaxyMap))
                {
                    int num3 = tradeableItems.IndexOf(new TradeableItem(TradeableItemType.TerritoryMap, null, 0));
                    if (num3 >= 0)
                    {
                        tradeableItems.RemoveAt(num3);
                    }
                }
                if (_SelectedItems.ContainsType(TradeableItemType.ThreatenTradeSanctions))
                {
                    int num4 = tradeableItems.IndexOf(new TradeableItem(TradeableItemType.ThreatenWar, null, 0));
                    if (num4 >= 0)
                    {
                        tradeableItems.RemoveAt(num4);
                    }
                }
                else if (_SelectedItems.ContainsType(TradeableItemType.ThreatenWar))
                {
                    int num5 = tradeableItems.IndexOf(new TradeableItem(TradeableItemType.ThreatenTradeSanctions, null, 0));
                    if (num5 >= 0)
                    {
                        tradeableItems.RemoveAt(num5);
                    }
                }
            }
            double num6 = Math.Max(10000.0, _Empire.StateMoney * 0.1);
            if (_Empire == _Galaxy.PlayerEmpire)
            {
                num6 = 0.0;
            }
            TreeNode treeNode = new TreeNode(TextResolver.GetText("Money"));
            treeNode.NodeFont = Font;
            foreach (TradeableItem tradeableItem2 in tradeableItems)
            {
                if (tradeableItem2.Type == TradeableItemType.Money && tradeableItem2.Item != null)
                {
                    double num7 = (double)tradeableItem2.Item;
                    if (num7 <= _Empire.StateMoney - num6)
                    {
                        TreeNode treeNode2 = new TreeNode(string.Format(TextResolver.GetText("X credits"), num7.ToString("#,###,###,##0")));
                        treeNode2.NodeFont = Font;
                        treeNode2.Tag = tradeableItem2;
                        treeNode.Nodes.Add(treeNode2);
                    }
                }
            }
            if (_MoneyToggled)
            {
                treeNode.Toggle();
            }
            TradeableItems.Nodes.Add(treeNode);
            bool flag = false;
            treeNode = new TreeNode(TextResolver.GetText("Disputed Colonies"));
            treeNode.NodeFont = Font;
            foreach (TradeableItem tradeableItem3 in tradeableItems)
            {
                if (tradeableItem3.Type == TradeableItemType.Colony)
                {
                    flag = true;
                    Habitat habitat = (Habitat)tradeableItem3.Item;
                    Habitat habitat2 = Galaxy.DetermineHabitatSystemStar(habitat);
                    string text = string.Format(TextResolver.GetText("Trade Description Colony NAME PLANETTYPE SYSTEMNAME"), habitat.Name, Galaxy.ResolveDescription(habitat.Type), habitat2.Name);
                    text = text + " (" + tradeableItem3.Value.ToString("###,###,###,##0") + ")";
                    TreeNode treeNode3 = new TreeNode(text);
                    treeNode3.NodeFont = Font;
                    treeNode3.Tag = tradeableItem3;
                    treeNode.Nodes.Add(treeNode3);
                }
            }
            if (flag)
            {
                if (_ColoniesToggled)
                {
                    treeNode.Toggle();
                }
                TradeableItems.Nodes.Add(treeNode);
            }
            bool flag2 = false;
            treeNode = new TreeNode(TextResolver.GetText("Disputed Bases"));
            treeNode.NodeFont = Font;
            foreach (TradeableItem tradeableItem4 in tradeableItems)
            {
                if (tradeableItem4.Type == TradeableItemType.Base)
                {
                    flag2 = true;
                    BuiltObject builtObject = (BuiltObject)tradeableItem4.Item;
                    string arg = string.Empty;
                    if (builtObject.NearestSystemStar != null)
                    {
                        arg = builtObject.NearestSystemStar.Name;
                    }
                    string text2 = string.Format(TextResolver.GetText("Trade Description Base"), builtObject.Name, arg);
                    text2 = text2 + " (" + tradeableItem4.Value.ToString("###,###,###,##0") + ")";
                    TreeNode treeNode4 = new TreeNode(text2);
                    treeNode4.NodeFont = Font;
                    treeNode4.Tag = tradeableItem4;
                    treeNode.Nodes.Add(treeNode4);
                }
            }
            if (flag2)
            {
                if (_BasesToggled)
                {
                    treeNode.Toggle();
                }
                TradeableItems.Nodes.Add(treeNode);
            }
            foreach (TradeableItem tradeableItem5 in tradeableItems)
            {
                if (tradeableItem5.Type == TradeableItemType.TerritoryMap)
                {
                    treeNode = new TreeNode(TextResolver.GetText("Trade Description Territory Map") + " (" + tradeableItem5.Value.ToString("###,###,###,##0") + ")");
                    treeNode.NodeFont = Font;
                    treeNode.Tag = tradeableItem5;
                    TradeableItems.Nodes.Add(treeNode);
                    break;
                }
            }
            foreach (TradeableItem tradeableItem6 in tradeableItems)
            {
                if (tradeableItem6.Type == TradeableItemType.GalaxyMap)
                {
                    treeNode = new TreeNode(TextResolver.GetText("Trade Description Galaxy Map") + " (" + tradeableItem6.Value.ToString("###,###,###,##0") + ")");
                    treeNode.NodeFont = Font;
                    treeNode.Tag = tradeableItem6;
                    TradeableItems.Nodes.Add(treeNode);
                    break;
                }
            }
            bool flag3 = false;
            treeNode = new TreeNode(TextResolver.GetText("Communications with Unknown Empires"));
            treeNode.NodeFont = Font;
            foreach (TradeableItem tradeableItem7 in tradeableItems)
            {
                if (tradeableItem7.Type == TradeableItemType.ContactEmpire)
                {
                    flag3 = true;
                    Empire empire = (Empire)tradeableItem7.Item;
                    string name = empire.Name;
                    name = name + " (" + tradeableItem7.Value.ToString("###,###,###,##0") + ")";
                    TreeNode treeNode5 = new TreeNode(name);
                    treeNode5.NodeFont = Font;
                    treeNode5.Tag = tradeableItem7;
                    treeNode.Nodes.Add(treeNode5);
                }
            }
            if (flag3)
            {
                if (_EmpireContactsToggled)
                {
                    treeNode.Toggle();
                }
                TradeableItems.Nodes.Add(treeNode);
            }
            foreach (TradeableItem tradeableItem8 in tradeableItems)
            {
                if (tradeableItem8.Type == TradeableItemType.SystemMap && tradeableItem8.Item != null && tradeableItem8.Item is Habitat)
                {
                    Habitat habitat3 = (Habitat)tradeableItem8.Item;
                    treeNode = new TreeNode(string.Format(TextResolver.GetText("Trade Description System Map"), habitat3.Name) + " (" + tradeableItem8.Value.ToString("###,###,###,##0") + ")");
                    treeNode.NodeFont = Font;
                    treeNode.Tag = tradeableItem8;
                    TradeableItems.Nodes.Add(treeNode);
                    break;
                }
            }
            bool flag4 = false;
            treeNode = new TreeNode(TextResolver.GetText("Locations of Independent Colonies"));
            treeNode.NodeFont = Font;
            foreach (TradeableItem tradeableItem9 in tradeableItems)
            {
                if (tradeableItem9.Type == TradeableItemType.IndependentColonyLocation)
                {
                    flag4 = true;
                    Habitat habitat4 = (Habitat)tradeableItem9.Item;
                    string name2 = habitat4.Name;
                    name2 = name2 + " (" + tradeableItem9.Value.ToString("###,###,###,##0") + ")";
                    TreeNode treeNode6 = new TreeNode(name2);
                    treeNode6.NodeFont = Font;
                    treeNode6.Tag = tradeableItem9;
                    treeNode.Nodes.Add(treeNode6);
                }
            }
            if (flag4)
            {
                if (_IndependentColoniesToggled)
                {
                    treeNode.Toggle();
                }
                TradeableItems.Nodes.Add(treeNode);
            }
            bool flag5 = false;
            treeNode = new TreeNode(TextResolver.GetText("Secret Locations"));
            treeNode.NodeFont = Font;
            foreach (TradeableItem tradeableItem10 in tradeableItems)
            {
                if (tradeableItem10.Type == TradeableItemType.SecretLocation)
                {
                    flag5 = true;
                    string text3 = string.Empty;
                    if (tradeableItem10.Item is GalaxyLocation)
                    {
                        GalaxyLocation galaxyLocation = (GalaxyLocation)tradeableItem10.Item;
                        text3 = galaxyLocation.Name;
                    }
                    else if (tradeableItem10.Item is Habitat)
                    {
                        Habitat habitat5 = (Habitat)tradeableItem10.Item;
                        text3 = habitat5.Name;
                    }
                    if (_OtherEmpire == _Galaxy.PlayerEmpire)
                    {
                        text3 = TextResolver.GetText("Secret Location");
                    }
                    text3 = text3 + " (" + tradeableItem10.Value.ToString("###,###,###,##0") + ")";
                    TreeNode treeNode7 = new TreeNode(text3);
                    treeNode7.NodeFont = Font;
                    treeNode7.Tag = tradeableItem10;
                    treeNode.Nodes.Add(treeNode7);
                }
            }
            if (flag5)
            {
                if (_SecretLocationsToggled)
                {
                    treeNode.Toggle();
                }
                TradeableItems.Nodes.Add(treeNode);
            }
            bool flag6 = false;
            treeNode = new TreeNode(TextResolver.GetText("Research"));
            treeNode.NodeFont = Font;
            foreach (TradeableItem tradeableItem11 in tradeableItems)
            {
                if (tradeableItem11.Type == TradeableItemType.ResearchProject)
                {
                    flag6 = true;
                    ResearchNode researchNode = (ResearchNode)tradeableItem11.Item;
                    TreeNode treeNode8 = new TreeNode(researchNode.Name + " (" + tradeableItem11.Value.ToString("###,###,###,##0") + ")");
                    treeNode8.NodeFont = Font;
                    treeNode8.Tag = tradeableItem11;
                    treeNode.Nodes.Add(treeNode8);
                }
            }
            if (flag6)
            {
                if (_ResearchToggled)
                {
                    treeNode.Toggle();
                }
                TradeableItems.Nodes.Add(treeNode);
            }
            bool flag7 = false;
            treeNode = new TreeNode(TextResolver.GetText("Declare War on..."));
            treeNode.NodeFont = Font;
            foreach (TradeableItem tradeableItem12 in tradeableItems)
            {
                if (tradeableItem12.Type == TradeableItemType.DeclareWarOther)
                {
                    flag7 = true;
                    Empire empire2 = (Empire)tradeableItem12.Item;
                    TreeNode treeNode9 = new TreeNode(empire2.Name + " (" + tradeableItem12.Value.ToString("###,###,###,##0") + ")");
                    treeNode9.NodeFont = Font;
                    treeNode9.Tag = tradeableItem12;
                    treeNode.Nodes.Add(treeNode9);
                }
            }
            if (flag7)
            {
                if (_DeclareWarToggled)
                {
                    treeNode.Toggle();
                }
                TradeableItems.Nodes.Add(treeNode);
            }
            bool flag8 = false;
            treeNode = new TreeNode(TextResolver.GetText("Initiate Trade Sanctions against..."));
            treeNode.NodeFont = Font;
            foreach (TradeableItem tradeableItem13 in tradeableItems)
            {
                if (tradeableItem13.Type == TradeableItemType.InitiateTradeSanctionsOther)
                {
                    flag8 = true;
                    Empire empire3 = (Empire)tradeableItem13.Item;
                    TreeNode treeNode10 = new TreeNode(empire3.Name + " (" + tradeableItem13.Value.ToString("###,###,###,##0") + ")");
                    treeNode10.NodeFont = Font;
                    treeNode10.Tag = tradeableItem13;
                    treeNode.Nodes.Add(treeNode10);
                }
            }
            if (flag8)
            {
                if (_TradeSanctionsToggled)
                {
                    treeNode.Toggle();
                }
                TradeableItems.Nodes.Add(treeNode);
            }
            bool flag9 = false;
            treeNode = new TreeNode(TextResolver.GetText("End War with..."));
            treeNode.NodeFont = Font;
            foreach (TradeableItem tradeableItem14 in tradeableItems)
            {
                if (tradeableItem14.Type == TradeableItemType.EndWarOther)
                {
                    flag9 = true;
                    Empire empire4 = (Empire)tradeableItem14.Item;
                    TreeNode treeNode11 = new TreeNode(empire4.Name + " (" + tradeableItem14.Value.ToString("###,###,###,##0") + ")");
                    treeNode11.NodeFont = Font;
                    treeNode11.Tag = tradeableItem14;
                    treeNode.Nodes.Add(treeNode11);
                }
            }
            if (flag9)
            {
                if (_EndWarToggled)
                {
                    treeNode.Toggle();
                }
                TradeableItems.Nodes.Add(treeNode);
            }
            bool flag10 = false;
            treeNode = new TreeNode(TextResolver.GetText("Lift Trade Sanctions against..."));
            treeNode.NodeFont = Font;
            foreach (TradeableItem tradeableItem15 in tradeableItems)
            {
                if (tradeableItem15.Type == TradeableItemType.LiftTradeSanctionsOther)
                {
                    flag10 = true;
                    Empire empire5 = (Empire)tradeableItem15.Item;
                    TreeNode treeNode12 = new TreeNode(empire5.Name + " (" + tradeableItem15.Value.ToString("###,###,###,##0") + ")");
                    treeNode12.NodeFont = Font;
                    treeNode12.Tag = tradeableItem15;
                    treeNode.Nodes.Add(treeNode12);
                }
            }
            if (flag10)
            {
                if (_LiftTradeSanctionsToggled)
                {
                    treeNode.Toggle();
                }
                TradeableItems.Nodes.Add(treeNode);
            }
            treeNode = new TreeNode(TextResolver.GetText("Lift Trade Sanctions against Your Empire"));
            treeNode.NodeFont = Font;
            foreach (TradeableItem tradeableItem16 in tradeableItems)
            {
                if (tradeableItem16.Type == TradeableItemType.LiftTradeSanctions)
                {
                    _ = (Empire)tradeableItem16.Item;
                    treeNode.NodeFont = Font;
                    treeNode.Tag = tradeableItem16;
                    TradeableItems.Nodes.Add(treeNode);
                }
            }
            treeNode = new TreeNode(TextResolver.GetText("End War with Your Empire"));
            treeNode.NodeFont = Font;
            foreach (TradeableItem tradeableItem17 in tradeableItems)
            {
                if (tradeableItem17.Type == TradeableItemType.EndWar)
                {
                    _ = (Empire)tradeableItem17.Item;
                    treeNode.NodeFont = Font;
                    treeNode.Tag = tradeableItem17;
                    TradeableItems.Nodes.Add(treeNode);
                }
            }
            if (_AllowDiplomaticThreats)
            {
                treeNode = new TreeNode(TextResolver.GetText("Threaten Trade Sanctions unless you agree"));
                treeNode.NodeFont = Font;
                foreach (TradeableItem tradeableItem18 in tradeableItems)
                {
                    if (tradeableItem18.Type == TradeableItemType.ThreatenTradeSanctions)
                    {
                        _ = (Empire)tradeableItem18.Item;
                        treeNode.NodeFont = Font;
                        treeNode.Tag = tradeableItem18;
                        TradeableItems.Nodes.Add(treeNode);
                    }
                }
                treeNode = new TreeNode(TextResolver.GetText("Threaten War unless you agree"));
                treeNode.NodeFont = Font;
                foreach (TradeableItem tradeableItem19 in tradeableItems)
                {
                    if (tradeableItem19.Type == TradeableItemType.ThreatenWar)
                    {
                        _ = (Empire)tradeableItem19.Item;
                        treeNode.NodeFont = Font;
                        treeNode.Tag = tradeableItem19;
                        TradeableItems.Nodes.Add(treeNode);
                    }
                }
            }
            foreach (TreeNode node in TradeableItems.Nodes)
            {
                node.Text = node.Text;
                foreach (TreeNode node2 in node.Nodes)
                {
                    node2.Text = node2.Text;
                }
            }
        }

        private void DoLayout()
        {
            int num = base.Width;
            int num2 = 100;
            int num3 = 34;
            LocalizeControlText();
            pnlTitleHeaderArea.Size = new Size(num, num3);
            pnlTitleHeaderArea.Location = new Point(0, 0);
            pnlTitleHeaderArea.BringToFront();
            picFlag.Size = new Size(40, 24);
            picFlag.Location = new Point(2, 4);
            picFlag.SizeMode = PictureBoxSizeMode.Zoom;
            picFlag.BringToFront();
            lblTitle.Size = new Size(num - 47, num3);
            lblTitle.Font = new Font(Font.FontFamily, Font.Size + 2f, FontStyle.Bold, GraphicsUnit.Pixel);
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblTitle.Location = new Point(47, 2);
            lblTitle.BringToFront();
            TradeableItems.Scrollable = true;
            TradeableItems.Size = new Size(num, base.Height - (num3 + num3 + num2 + 25));
            TradeableItems.Location = new Point(0, num3);
            pnlSelectedHeaderArea.Size = new Size(num, num3);
            pnlSelectedHeaderArea.Location = new Point(0, num3 + TradeableItems.Height);
            pnlSelectedHeaderArea.BringToFront();
            lblSelected.Size = new Size(num, num3);
            lblSelected.Font = new Font(Font.FontFamily, Font.Size + 2f, FontStyle.Bold, GraphicsUnit.Pixel);
            lblSelected.Location = new Point(0, num3 + TradeableItems.Height + 3);
            lblSelected.TextAlign = ContentAlignment.MiddleCenter;
            lblSelected.BringToFront();
            SelectedItems.Font = Font;
            SelectedItems.Size = new Size(num, num2);
            SelectedItems.Location = new Point(0, num3 + TradeableItems.Height + num3);
            btnClearSelectedItems.Size = new Size(150, 25);
            btnClearSelectedItems.Location = new Point((num - 150) / 2, base.Height - 25);
            btnClearSelectedItems.BringToFront();
            btnClearSelectedItems.Visible = true;
        }

        public TradeableItemList FilterOutSelectedAndExcludedItems(TradeableItemList tradeableItems)
        {
            TradeableItemList tradeableItemList = tradeableItems.Clone();
            List<int> list = new List<int>();
            foreach (TradeableItem selectedItem in _SelectedItems)
            {
                if (selectedItem.Type != TradeableItemType.Money)
                {
                    int num = tradeableItems.IndexOf(selectedItem);
                    if (num >= 0)
                    {
                        list.Add(num);
                    }
                }
            }
            if (_ExcludedItems != null)
            {
                foreach (TradeableItem excludedItem in _ExcludedItems)
                {
                    if (excludedItem.Type != TradeableItemType.Money)
                    {
                        int num2 = tradeableItems.IndexOf(excludedItem);
                        if (num2 >= 0)
                        {
                            list.Add(num2);
                        }
                    }
                }
            }
            list.Sort();
            list.Reverse();
            foreach (int item in list)
            {
                tradeableItemList.RemoveAt(item);
            }
            return tradeableItemList;
        }

        private int ResolveHoveredItemIndex(int x, int y, out Rectangle bounds)
        {
            bounds = Rectangle.Empty;
            if (SelectedItems.Items != null)
            {
                for (int i = 0; i < SelectedItems.Items.Count; i++)
                {
                    Rectangle itemRectangle = SelectedItems.GetItemRectangle(i);
                    if (itemRectangle.Contains(new Point(x, y)))
                    {
                        bounds = itemRectangle;
                        return i;
                    }
                }
            }
            return -1;
        }

        private void SelectedItems_MouseMove(object sender, MouseEventArgs e)
        {
            if (!AcceptingMouseMoveEvents)
            {
                return;
            }
            TradeableItem tradeableItem = null;
            Rectangle bounds = Rectangle.Empty;
            int num = ResolveHoveredItemIndex(e.X, e.Y, out bounds);
            if (num >= 0)
            {
                tradeableItem = _SelectedItems[num];
            }
            if (tradeableItem != null)
            {
                if (tradeableItem.Type == TradeableItemType.ResearchProject && tradeableItem.Item is ResearchNode)
                {
                    ResearchNode researchProject = (ResearchNode)tradeableItem.Item;
                    if (bounds.Contains(e.X, e.Y))
                    {
                        int num2 = Math.Min(bounds.Width, SelectedItems.Width);
                        Rectangle relativeRectangle = new Rectangle(SelectedItems.Location.X + bounds.X, SelectedItems.Location.Y + bounds.Y, num2, bounds.Height);
                        ResearchProjectHoveredEventArgs e2 = new ResearchProjectHoveredEventArgs(researchProject, relativeRectangle);
                        if (this.ResearchProjectHovered != null)
                        {
                            this.ResearchProjectHovered(this, e2);
                        }
                    }
                    else if (this.ResearchProjectHovered != null)
                    {
                        this.ResearchProjectHovered(this, new ResearchProjectHoveredEventArgs(null, Rectangle.Empty));
                    }
                }
                else if (this.ResearchProjectHovered != null)
                {
                    this.ResearchProjectHovered(this, new ResearchProjectHoveredEventArgs(null, Rectangle.Empty));
                }
            }
            else if (this.ResearchProjectHovered != null)
            {
                this.ResearchProjectHovered(this, new ResearchProjectHoveredEventArgs(null, Rectangle.Empty));
            }
        }

        private void SelectedItems_MouseLeave(object sender, EventArgs e)
        {
            if (this.ResearchProjectHovered != null)
            {
                this.ResearchProjectHovered(this, new ResearchProjectHoveredEventArgs(null, Rectangle.Empty));
            }
        }

        private void TradeableItems_MouseMove(object sender, MouseEventArgs e)
        {
            if (!AcceptingMouseMoveEvents)
            {
                return;
            }
            TreeNode nodeAt = TradeableItems.GetNodeAt(e.X, e.Y);
            TradeableItem tradeableItem = null;
            if (nodeAt != null)
            {
                if (nodeAt.Tag is TradeableItem)
                {
                    tradeableItem = (TradeableItem)nodeAt.Tag;
                }
                if (tradeableItem != null)
                {
                    if (tradeableItem.Type == TradeableItemType.ResearchProject && tradeableItem.Item is ResearchNode)
                    {
                        ResearchNode researchProject = (ResearchNode)tradeableItem.Item;
                        if (nodeAt.Bounds.Contains(e.X, e.Y))
                        {
                            int num = Math.Min(nodeAt.Bounds.Width, TradeableItems.Width);
                            Rectangle relativeRectangle = new Rectangle(TradeableItems.Location.X + nodeAt.Bounds.X, TradeableItems.Location.Y + nodeAt.Bounds.Y, num, nodeAt.Bounds.Height);
                            ResearchProjectHoveredEventArgs e2 = new ResearchProjectHoveredEventArgs(researchProject, relativeRectangle);
                            if (this.ResearchProjectHovered != null)
                            {
                                this.ResearchProjectHovered(this, e2);
                            }
                        }
                        else if (this.ResearchProjectHovered != null)
                        {
                            this.ResearchProjectHovered(this, new ResearchProjectHoveredEventArgs(null, Rectangle.Empty));
                        }
                    }
                    else if (this.ResearchProjectHovered != null)
                    {
                        this.ResearchProjectHovered(this, new ResearchProjectHoveredEventArgs(null, Rectangle.Empty));
                    }
                }
                else if (this.ResearchProjectHovered != null)
                {
                    this.ResearchProjectHovered(this, new ResearchProjectHoveredEventArgs(null, Rectangle.Empty));
                }
            }
            else if (this.ResearchProjectHovered != null)
            {
                this.ResearchProjectHovered(this, new ResearchProjectHoveredEventArgs(null, Rectangle.Empty));
            }
        }

        private void TradeableItems_MouseLeave(object sender, EventArgs e)
        {
            if (this.ResearchProjectHovered != null)
            {
                this.ResearchProjectHovered(this, new ResearchProjectHoveredEventArgs(null, Rectangle.Empty));
            }
        }

        private void TradeableItems_MouseClick(object sender, MouseEventArgs e)
        {
            TreeNode nodeAt = TradeableItems.GetNodeAt(e.X, e.Y);
            TradeableItem tradeableItem = null;
            if (nodeAt.Tag is TradeableItem)
            {
                tradeableItem = (TradeableItem)nodeAt.Tag;
            }
            if (tradeableItem != null && e.X >= 25)
            {
                int num = _SelectedItems.IndexOf(tradeableItem);
                if (tradeableItem.Type == TradeableItemType.AdoptGovernmentStyle)
                {
                    num = _SelectedItems.FindAnyGovernmentStyle();
                }
                double num2 = Math.Max(20000.0, _Empire.StateMoney * 0.3);
                if (_Empire == _Galaxy.PlayerEmpire)
                {
                    num2 = 0.0;
                }
                if (num < 0)
                {
                    if (tradeableItem.Type == TradeableItemType.Money && tradeableItem.Item is double)
                    {
                        if (_Empire.StateMoney - num2 >= (double)tradeableItem.Item)
                        {
                            _SelectedItems.Add(tradeableItem);
                        }
                        else if (_Empire.StateMoney - num2 > 0.0)
                        {
                            double num3 = (int)(_Empire.StateMoney - num2);
                            int value = _Galaxy.ValueMoney(num3);
                            _SelectedItems.Add(new TradeableItem(TradeableItemType.Money, num3, value));
                        }
                    }
                    else
                    {
                        _SelectedItems.Add(tradeableItem);
                    }
                    TradeableItemList tradeableItems = FilterOutSelectedAndExcludedItems(_TradeableItems);
                    PopulateTradeableItems(tradeableItems);
                }
                else if (tradeableItem.Type == TradeableItemType.AdoptGovernmentStyle)
                {
                    if (_SelectedItems[num].Item is GovernmentAttributes)
                    {
                        _SelectedItems[num] = tradeableItem;
                        TradeableItemList tradeableItems2 = FilterOutSelectedAndExcludedItems(_TradeableItems);
                        PopulateTradeableItems(tradeableItems2);
                    }
                }
                else if (tradeableItem.Type == TradeableItemType.Money && _SelectedItems[num].Item is double)
                {
                    double num4 = (double)_SelectedItems[num].Item;
                    num4 += (double)tradeableItem.Item;
                    if (num4 > _Empire.StateMoney - num2)
                    {
                        num4 = (int)(_Empire.StateMoney - num2);
                    }
                    int value2 = _Galaxy.ValueMoney(num4);
                    _SelectedItems[num] = new TradeableItem(TradeableItemType.Money, num4, value2);
                }
                UpdateOfferedItemsHeading();
                SetSelectedItemsInControl();
            }
            else if (nodeAt.Nodes != null && nodeAt.Nodes.Count > 0)
            {
                if (nodeAt.Text == TextResolver.GetText("Money"))
                {
                    _MoneyToggled = !_MoneyToggled;
                }
                else if (nodeAt.Text == TextResolver.GetText("Disputed Colonies"))
                {
                    _ColoniesToggled = !_ColoniesToggled;
                }
                else if (nodeAt.Text == TextResolver.GetText("Disputed Bases"))
                {
                    _BasesToggled = !_BasesToggled;
                }
                else if (nodeAt.Text == TextResolver.GetText("Communications with Unknown Empires"))
                {
                    _EmpireContactsToggled = !_EmpireContactsToggled;
                }
                else if (nodeAt.Text == TextResolver.GetText("Locations of Independent Colonies"))
                {
                    _IndependentColoniesToggled = !_IndependentColoniesToggled;
                }
                else if (nodeAt.Text == TextResolver.GetText("Secret Locations"))
                {
                    _SecretLocationsToggled = !_SecretLocationsToggled;
                }
                else if (nodeAt.Text == TextResolver.GetText("Component Tech"))
                {
                    _ResearchToggled = !_ResearchToggled;
                }
                else if (nodeAt.Text == TextResolver.GetText("Adopt Government Style"))
                {
                    _GovernmentStylesToggled = !_GovernmentStylesToggled;
                }
                else if (nodeAt.Text == TextResolver.GetText("Declare War on..."))
                {
                    _DeclareWarToggled = !_DeclareWarToggled;
                }
                else if (nodeAt.Text == TextResolver.GetText("Initiate Trade Sanctions against..."))
                {
                    _TradeSanctionsToggled = !_TradeSanctionsToggled;
                }
                else if (nodeAt.Text == TextResolver.GetText("End War with..."))
                {
                    _EndWarToggled = !_EndWarToggled;
                }
                else if (nodeAt.Text == TextResolver.GetText("Lift Trade Sanctions against..."))
                {
                    _LiftTradeSanctionsToggled = !_LiftTradeSanctionsToggled;
                }
                if (e.X >= 25)
                {
                    nodeAt.Toggle();
                }
            }
        }

        private void SelectedItems_MouseClick(object sender, MouseEventArgs e)
        {
            int selectedIndex = SelectedItems.SelectedIndex;
            if (selectedIndex >= 0 && (_RequiredItems == null || !_RequiredItems.Contains(_SelectedItems[selectedIndex])))
            {
                SelectedItems.Items.RemoveAt(selectedIndex);
                _SelectedItems.RemoveAt(selectedIndex);
                TradeableItemList tradeableItems = FilterOutSelectedAndExcludedItems(_TradeableItems);
                PopulateTradeableItems(tradeableItems);
                UpdateOfferedItemsHeading();
            }
        }

        private void btnClearSelectedItems_Click(object sender, EventArgs e)
        {
            BaconDiplomacyTradeTree.AdjustItemCosts(this);
            SelectedItems.Items.Clear();
            _SelectedItems.Clear();
            if (_RequiredItems != null && _RequiredItems.Count > 0)
            {
                _SelectedItems.AddRange(_RequiredItems);
            }
            if (_SelectedItems != null)
            {
                foreach (TradeableItem selectedItem in _SelectedItems)
                {
                    if (selectedItem != null)
                    {
                        SelectedItems.Items.Add(selectedItem);
                    }
                }
            }
            TradeableItemList tradeableItems = FilterOutSelectedAndExcludedItems(_TradeableItems);
            PopulateTradeableItems(tradeableItems);
            UpdateOfferedItemsHeading();
        }

        private void TradeableItems_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e != null && e.Node != null)
            {
                if (e.Node.Text == TextResolver.GetText("Money"))
                {
                    _MoneyToggled = true;
                }
                else if (e.Node.Text == TextResolver.GetText("Disputed Colonies"))
                {
                    _ColoniesToggled = true;
                }
                else if (e.Node.Text == TextResolver.GetText("Disputed Bases"))
                {
                    _BasesToggled = true;
                }
                else if (e.Node.Text == TextResolver.GetText("Communications with Unknown Empires"))
                {
                    _EmpireContactsToggled = true;
                }
                else if (e.Node.Text == TextResolver.GetText("Locations of Independent Colonies"))
                {
                    _IndependentColoniesToggled = true;
                }
                else if (e.Node.Text == TextResolver.GetText("Secret Locations"))
                {
                    _SecretLocationsToggled = true;
                }
                else if (e.Node.Text == TextResolver.GetText("Component Tech"))
                {
                    _ResearchToggled = true;
                }
                else if (e.Node.Text == TextResolver.GetText("Adopt Government Style"))
                {
                    _GovernmentStylesToggled = true;
                }
                else if (e.Node.Text == TextResolver.GetText("Declare War on..."))
                {
                    _DeclareWarToggled = true;
                }
                else if (e.Node.Text == TextResolver.GetText("Initiate Trade Sanctions against..."))
                {
                    _TradeSanctionsToggled = true;
                }
                else if (e.Node.Text == TextResolver.GetText("End War with..."))
                {
                    _EndWarToggled = true;
                }
                else if (e.Node.Text == TextResolver.GetText("Lift Trade Sanctions against..."))
                {
                    _LiftTradeSanctionsToggled = true;
                }
            }
        }

        private void TradeableItems_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if (e != null && e.Node != null)
            {
                if (e.Node.Text == TextResolver.GetText("Money"))
                {
                    _MoneyToggled = false;
                }
                else if (e.Node.Text == TextResolver.GetText("Disputed Colonies"))
                {
                    _ColoniesToggled = false;
                }
                else if (e.Node.Text == TextResolver.GetText("Disputed Bases"))
                {
                    _BasesToggled = false;
                }
                else if (e.Node.Text == TextResolver.GetText("Communications with Unknown Empires"))
                {
                    _EmpireContactsToggled = false;
                }
                else if (e.Node.Text == TextResolver.GetText("Locations of Independent Colonies"))
                {
                    _IndependentColoniesToggled = false;
                }
                else if (e.Node.Text == TextResolver.GetText("Secret Locations"))
                {
                    _SecretLocationsToggled = false;
                }
                else if (e.Node.Text == TextResolver.GetText("Component Tech"))
                {
                    _ResearchToggled = false;
                }
                else if (e.Node.Text == TextResolver.GetText("Adopt Government Style"))
                {
                    _GovernmentStylesToggled = false;
                }
                else if (e.Node.Text == TextResolver.GetText("Declare War on..."))
                {
                    _DeclareWarToggled = false;
                }
                else if (e.Node.Text == TextResolver.GetText("Initiate Trade Sanctions against..."))
                {
                    _TradeSanctionsToggled = false;
                }
                else if (e.Node.Text == TextResolver.GetText("End War with..."))
                {
                    _EndWarToggled = false;
                }
                else if (e.Node.Text == TextResolver.GetText("Lift Trade Sanctions against..."))
                {
                    _LiftTradeSanctionsToggled = false;
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.TradeableItems = new System.Windows.Forms.TreeView();
            this.SelectedItems = new System.Windows.Forms.ListBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSelected = new System.Windows.Forms.Label();
            this.picFlag = new System.Windows.Forms.PictureBox();
            this.pnlTitleHeaderArea = new System.Windows.Forms.Panel();
            this.pnlSelectedHeaderArea = new System.Windows.Forms.Panel();
            this.btnClearSelectedItems = new DistantWorlds.Controls.GlassButton();
            ((System.ComponentModel.ISupportInitialize)this.picFlag).BeginInit();
            base.SuspendLayout();
            this.TradeableItems.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
            this.TradeableItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TradeableItems.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.TradeableItems.ForeColor = System.Drawing.Color.White;
            this.TradeableItems.Location = new System.Drawing.Point(5, 40);
            this.TradeableItems.Name = "TradeableItems";
            this.TradeableItems.Size = new System.Drawing.Size(121, 97);
            this.TradeableItems.TabIndex = 0;
            this.TradeableItems.MouseClick += new System.Windows.Forms.MouseEventHandler(TradeableItems_MouseClick);
            this.TradeableItems.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(TradeableItems_BeforeExpand);
            this.TradeableItems.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(TradeableItems_BeforeCollapse);
            this.TradeableItems.MouseMove += new System.Windows.Forms.MouseEventHandler(TradeableItems_MouseMove);
            this.TradeableItems.MouseLeave += new System.EventHandler(TradeableItems_MouseLeave);
            this.SelectedItems.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
            this.SelectedItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SelectedItems.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.SelectedItems.ForeColor = System.Drawing.Color.White;
            this.SelectedItems.FormattingEnabled = true;
            this.SelectedItems.Location = new System.Drawing.Point(5, 174);
            this.SelectedItems.Name = "SelectedItems";
            this.SelectedItems.Size = new System.Drawing.Size(120, 91);
            this.SelectedItems.TabIndex = 1;
            this.SelectedItems.MouseClick += new System.Windows.Forms.MouseEventHandler(SelectedItems_MouseClick);
            this.SelectedItems.MouseMove += new System.Windows.Forms.MouseEventHandler(SelectedItems_MouseMove);
            this.SelectedItems.MouseLeave += new System.EventHandler(SelectedItems_MouseLeave);
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(80, 80, 112);
            this.lblTitle.Font = new System.Drawing.Font("Verdana", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(48, 4);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(39, 16);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Title";
            this.lblSelected.AutoSize = true;
            this.lblSelected.BackColor = System.Drawing.Color.FromArgb(80, 80, 112);
            this.lblSelected.Font = new System.Drawing.Font("Verdana", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.lblSelected.ForeColor = System.Drawing.Color.White;
            this.lblSelected.Location = new System.Drawing.Point(17, 144);
            this.lblSelected.Name = "lblSelected";
            this.lblSelected.Size = new System.Drawing.Size(100, 14);
            this.lblSelected.TabIndex = 3;
            this.lblSelected.Text = "Offered Items";
            this.picFlag.BackColor = System.Drawing.Color.Transparent;
            this.picFlag.Location = new System.Drawing.Point(4, 3);
            this.picFlag.Name = "picFlag";
            this.picFlag.Size = new System.Drawing.Size(33, 26);
            this.picFlag.TabIndex = 5;
            this.picFlag.TabStop = false;
            this.pnlTitleHeaderArea.BackColor = System.Drawing.Color.FromArgb(80, 80, 112);
            this.pnlTitleHeaderArea.Location = new System.Drawing.Point(102, 4);
            this.pnlTitleHeaderArea.Name = "pnlTitleHeaderArea";
            this.pnlTitleHeaderArea.Size = new System.Drawing.Size(79, 30);
            this.pnlTitleHeaderArea.TabIndex = 6;
            this.pnlSelectedHeaderArea.BackColor = System.Drawing.Color.FromArgb(80, 80, 112);
            this.pnlSelectedHeaderArea.Location = new System.Drawing.Point(105, 143);
            this.pnlSelectedHeaderArea.Name = "pnlSelectedHeaderArea";
            this.pnlSelectedHeaderArea.Size = new System.Drawing.Size(79, 30);
            this.pnlSelectedHeaderArea.TabIndex = 7;
            this.btnClearSelectedItems.BackColor = System.Drawing.Color.FromArgb(0, 0, 0);
            this.btnClearSelectedItems.ClipBackground = false;
            this.btnClearSelectedItems.DelayFrameRefresh = false;
            this.btnClearSelectedItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.btnClearSelectedItems.ForeColor = System.Drawing.Color.FromArgb(150, 150, 150);
            this.btnClearSelectedItems.GlowColor = System.Drawing.Color.FromArgb(48, 48, 128);
            this.btnClearSelectedItems.InnerBorderColor = System.Drawing.Color.FromArgb(67, 67, 77);
            this.btnClearSelectedItems.IntensifyColors = false;
            this.btnClearSelectedItems.Location = new System.Drawing.Point(5, 271);
            this.btnClearSelectedItems.Name = "btnClearSelectedItems";
            this.btnClearSelectedItems.OuterBorderColor = System.Drawing.Color.FromArgb(0, 0, 16);
            this.btnClearSelectedItems.ShineColor = System.Drawing.Color.FromArgb(112, 112, 128);
            this.btnClearSelectedItems.Size = new System.Drawing.Size(141, 23);
            this.btnClearSelectedItems.TabIndex = 4;
            this.btnClearSelectedItems.Text = "Clear Offered Items";
            this.btnClearSelectedItems.TextColor = System.Drawing.Color.FromArgb(120, 120, 120);
            this.btnClearSelectedItems.TextColor2 = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnClearSelectedItems.Click += new System.EventHandler(btnClearSelectedItems_Click);
            base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
            base.Controls.Add(this.pnlSelectedHeaderArea);
            base.Controls.Add(this.pnlTitleHeaderArea);
            base.Controls.Add(this.picFlag);
            base.Controls.Add(this.btnClearSelectedItems);
            base.Controls.Add(this.lblSelected);
            base.Controls.Add(this.lblTitle);
            base.Controls.Add(this.SelectedItems);
            base.Controls.Add(this.TradeableItems);
            base.Name = "DiplomacyTradeTree";
            base.Size = new System.Drawing.Size(184, 339);
            ((System.ComponentModel.ISupportInitialize)this.picFlag).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}
