// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.DiplomacyTradeTree
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

//using BaconDistantWorlds;
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
        private Panel pnlTitleHeaderArea;
        private Panel pnlSelectedHeaderArea;

        public event EventHandler<ResearchProjectHoveredEventArgs> ResearchProjectHovered;

        public virtual void SetFontCache(IFontCache fontCache)
        {
            this._FontCache = fontCache;
            if ((double)this._FontSize <= 0.0)
                return;
            this.Font = this._FontCache.GenerateFont(this._FontSize, this._FontIsBold);
        }

        public void SetFont(float pointSize) => this.SetFont(pointSize, false);

        public void SetFont(float pointSize, bool isBold)
        {
            this._FontSize = pointSize;
            this._FontIsBold = isBold;
            if (this._FontCache == null)
                return;
            this.Font = this._FontCache.GenerateFont(this._FontSize, this._FontIsBold);
        }

        public DiplomacyTradeTree()
        {
            this.InitializeComponent();
            this.TradeableItems.ShowPlusMinus = true;
            this.TradeableItems.ShowLines = false;
            this.TradeableItems.LineColor = Color.FromArgb(24, 24, 80);
        }

        public Empire Empire => this._Empire;

        public TradeableItemList SelectedTradeItems => this._SelectedItems;

        public TradeableItemList RequiredItems => this._RequiredItems;

        public TradeableItemList ExcludedItems => this._ExcludedItems;

        public void DoBind(
          Galaxy galaxy,
          Empire empire,
          Empire otherEmpire,
          bool allowDiplomaticThreats,
          bool refactorValuesForEmpire)
        {
            this.DoLayout();
            this._Galaxy = galaxy;
            this._Empire = empire;
            this._OtherEmpire = otherEmpire;
            this._AllowDiplomaticThreats = allowDiplomaticThreats;
            this._RefactorValuesForEmpire = refactorValuesForEmpire;
            this.lblTitle.Text = empire.Name;
            this.picFlag.Image = (Image)this.PrecacheScaledBitmap(empire.LargeFlagPicture, this.picFlag.Width, this.picFlag.Height, InterpolationMode.HighQualityBicubic, CompositingQuality.HighQuality, SmoothingMode.AntiAlias);
        }

        private void LocalizeControlText() => this.btnClearSelectedItems.Text = TextResolver.GetText("Clear Offered Items");

        public void SetSelectedItems(TradeableItemList selectedItems) => this.SetSelectedItems(selectedItems, new TradeableItemList(), new TradeableItemList());

        public void SetSelectedItems(
          TradeableItemList selectedItems,
          TradeableItemList requiredItems,
          TradeableItemList excludedItems)
        {
            this._RequiredItems = requiredItems;
            this._ExcludedItems = excludedItems;
            this.SelectedItems.Items.Clear();
            this._SelectedItems.Clear();
            if (this._RequiredItems != null)
                this._SelectedItems.AddRange((IEnumerable<TradeableItem>)this._RequiredItems);
            if (selectedItems != null)
                this._SelectedItems.AddRange((IEnumerable<TradeableItem>)selectedItems);
            if (this._SelectedItems != null)
            {
                foreach (object selectedItem in (SyncList<TradeableItem>)this._SelectedItems)
                    this.SelectedItems.Items.Add(selectedItem);
            }
            this.PopulateTradeableItems(this.FilterOutSelectedAndExcludedItems(this._TradeableItems));
            this.UpdateOfferedItemsHeading();
        }

        private void SetSelectedItemsInControl()
        {
            this.SelectedItems.Items.Clear();
            if (this._SelectedItems == null)
                return;
            foreach (TradeableItem selectedItem in (SyncList<TradeableItem>)this._SelectedItems)
            {
                selectedItem.ShowSecretLocationNames = this._Empire == this._Galaxy.PlayerEmpire;
                this.SelectedItems.Items.Add((object)selectedItem);
            }
        }

        private Bitmap PrecacheScaledBitmap(
          Bitmap unscaledBitmap,
          int width,
          int height,
          InterpolationMode interpolation,
          CompositingQuality compositing,
          SmoothingMode smoothing)
        {
            if (width < 1)
                width = 1;
            if (height < 1)
                height = 1;
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage((Image)bitmap);
            graphics.InterpolationMode = interpolation;
            graphics.CompositingQuality = compositing;
            graphics.SmoothingMode = smoothing;
            graphics.DrawImage((Image)unscaledBitmap, new Rectangle(0, 0, width, height));
            graphics.Dispose();
            return bitmap;
        }

        public void Reset()
        {
            this._MoneyToggled = true;
            this._ColoniesToggled = true;
            this._BasesToggled = true;
            this._ResearchToggled = true;
            this._GovernmentStylesToggled = true;
            this._DeclareWarToggled = true;
            this._EndWarToggled = true;
            this._TradeSanctionsToggled = true;
            this._LiftTradeSanctionsToggled = true;
            this._EmpireContactsToggled = true;
            this._IndependentColoniesToggled = true;
            this._SecretLocationsToggled = true;
            bool includeAllItems = false;
            if (this._Empire == this._Galaxy.PlayerEmpire)
                includeAllItems = true;
            this._TradeableItems = this._Galaxy.ResolveTradeableItems(this._Empire, this._OtherEmpire, false, this._RefactorValuesForEmpire, includeAllItems);
            if (this._RequiredItems != null)
                this._RequiredItems.Clear();
            this._SelectedItems = new TradeableItemList();
            this.SelectedItems.Items.Clear();
            this.PopulateTradeableItems(this._TradeableItems);
            this.UpdateOfferedItemsHeading();
        }

        public void UpdateOfferedItemsHeading() => this.lblSelected.Text = TextResolver.GetText("Offered Items") + " (" + this._SelectedItems.TotalValue.ToString("###,###,###,##0") + ")";

        public void PopulateTradeableItems(TradeableItemList tradeableItems)
        {
            this.TradeableItems.Nodes.Clear();
            if (this._ExcludedItems != null && this._ExcludedItems.Count > 0)
            {
                TradeableItemList tradeableItemList = new TradeableItemList();
                foreach (TradeableItem tradeableItem in (SyncList<TradeableItem>)tradeableItems)
                {
                    if (this._ExcludedItems.IndexOf(tradeableItem) >= 0)
                        tradeableItemList.Add(tradeableItem);
                }
                foreach (TradeableItem tradeableItem in (SyncList<TradeableItem>)tradeableItemList)
                    tradeableItems.Remove(tradeableItem);
            }
            if (this._SelectedItems != null && this._SelectedItems.Count > 0)
            {
                if (this._SelectedItems.ContainsType(TradeableItemType.TerritoryMap))
                {
                    int index = tradeableItems.IndexOf(new TradeableItem(TradeableItemType.GalaxyMap, (object)null, 0));
                    if (index >= 0)
                        tradeableItems.RemoveAt(index);
                }
                else if (this._SelectedItems.ContainsType(TradeableItemType.GalaxyMap))
                {
                    int index = tradeableItems.IndexOf(new TradeableItem(TradeableItemType.TerritoryMap, (object)null, 0));
                    if (index >= 0)
                        tradeableItems.RemoveAt(index);
                }
                if (this._SelectedItems.ContainsType(TradeableItemType.ThreatenTradeSanctions))
                {
                    int index = tradeableItems.IndexOf(new TradeableItem(TradeableItemType.ThreatenWar, (object)null, 0));
                    if (index >= 0)
                        tradeableItems.RemoveAt(index);
                }
                else if (this._SelectedItems.ContainsType(TradeableItemType.ThreatenWar))
                {
                    int index = tradeableItems.IndexOf(new TradeableItem(TradeableItemType.ThreatenTradeSanctions, (object)null, 0));
                    if (index >= 0)
                        tradeableItems.RemoveAt(index);
                }
            }
            double num1 = Math.Max(10000.0, this._Empire.StateMoney * 0.1);
            if (this._Empire == this._Galaxy.PlayerEmpire)
                num1 = 0.0;
            TreeNode node1 = new TreeNode(TextResolver.GetText("Money"));
            node1.NodeFont = this.Font;
            foreach (TradeableItem tradeableItem in (SyncList<TradeableItem>)tradeableItems)
            {
                if (tradeableItem.Type == TradeableItemType.Money && tradeableItem.Item != null)
                {
                    double num2 = (double)tradeableItem.Item;
                    if (num2 <= this._Empire.StateMoney - num1)
                        node1.Nodes.Add(new TreeNode(string.Format(TextResolver.GetText("X credits"), (object)num2.ToString("#,###,###,##0")))
                        {
                            NodeFont = this.Font,
                            Tag = (object)tradeableItem
                        });
                }
            }
            if (this._MoneyToggled)
                node1.Toggle();
            this.TradeableItems.Nodes.Add(node1);
            bool flag1 = false;
            TreeNode node2 = new TreeNode(TextResolver.GetText("Disputed Colonies"));
            node2.NodeFont = this.Font;
            foreach (TradeableItem tradeableItem in (SyncList<TradeableItem>)tradeableItems)
            {
                if (tradeableItem.Type == TradeableItemType.Colony)
                {
                    flag1 = true;
                    Habitat habitat = (Habitat)tradeableItem.Item;
                    Habitat habitatSystemStar = Galaxy.DetermineHabitatSystemStar(habitat);
                    node2.Nodes.Add(new TreeNode(string.Format(TextResolver.GetText("Trade Description Colony NAME PLANETTYPE SYSTEMNAME"), (object)habitat.Name, (object)Galaxy.ResolveDescription(habitat.Type), (object)habitatSystemStar.Name) + " (" + tradeableItem.Value.ToString("###,###,###,##0") + ")")
                    {
                        NodeFont = this.Font,
                        Tag = (object)tradeableItem
                    });
                }
            }
            if (flag1)
            {
                if (this._ColoniesToggled)
                    node2.Toggle();
                this.TradeableItems.Nodes.Add(node2);
            }
            bool flag2 = false;
            TreeNode node3 = new TreeNode(TextResolver.GetText("Disputed Bases"));
            node3.NodeFont = this.Font;
            foreach (TradeableItem tradeableItem in (SyncList<TradeableItem>)tradeableItems)
            {
                if (tradeableItem.Type == TradeableItemType.Base)
                {
                    flag2 = true;
                    BuiltObject builtObject = (BuiltObject)tradeableItem.Item;
                    string str = string.Empty;
                    if (builtObject.NearestSystemStar != null)
                        str = builtObject.NearestSystemStar.Name;
                    node3.Nodes.Add(new TreeNode(string.Format(TextResolver.GetText("Trade Description Base"), (object)builtObject.Name, (object)str) + " (" + tradeableItem.Value.ToString("###,###,###,##0") + ")")
                    {
                        NodeFont = this.Font,
                        Tag = (object)tradeableItem
                    });
                }
            }
            if (flag2)
            {
                if (this._BasesToggled)
                    node3.Toggle();
                this.TradeableItems.Nodes.Add(node3);
            }
            foreach (TradeableItem tradeableItem in (SyncList<TradeableItem>)tradeableItems)
            {
                if (tradeableItem.Type == TradeableItemType.TerritoryMap)
                {
                    this.TradeableItems.Nodes.Add(new TreeNode(TextResolver.GetText("Trade Description Territory Map") + " (" + tradeableItem.Value.ToString("###,###,###,##0") + ")")
                    {
                        NodeFont = this.Font,
                        Tag = (object)tradeableItem
                    });
                    break;
                }
            }
            foreach (TradeableItem tradeableItem in (SyncList<TradeableItem>)tradeableItems)
            {
                if (tradeableItem.Type == TradeableItemType.GalaxyMap)
                {
                    this.TradeableItems.Nodes.Add(new TreeNode(TextResolver.GetText("Trade Description Galaxy Map") + " (" + tradeableItem.Value.ToString("###,###,###,##0") + ")")
                    {
                        NodeFont = this.Font,
                        Tag = (object)tradeableItem
                    });
                    break;
                }
            }
            bool flag3 = false;
            TreeNode node4 = new TreeNode(TextResolver.GetText("Communications with Unknown Empires"));
            node4.NodeFont = this.Font;
            foreach (TradeableItem tradeableItem in (SyncList<TradeableItem>)tradeableItems)
            {
                if (tradeableItem.Type == TradeableItemType.ContactEmpire)
                {
                    flag3 = true;
                    node4.Nodes.Add(new TreeNode(((Empire)tradeableItem.Item).Name + " (" + tradeableItem.Value.ToString("###,###,###,##0") + ")")
                    {
                        NodeFont = this.Font,
                        Tag = (object)tradeableItem
                    });
                }
            }
            if (flag3)
            {
                if (this._EmpireContactsToggled)
                    node4.Toggle();
                this.TradeableItems.Nodes.Add(node4);
            }
            foreach (TradeableItem tradeableItem in (SyncList<TradeableItem>)tradeableItems)
            {
                if (tradeableItem.Type == TradeableItemType.SystemMap && tradeableItem.Item != null && tradeableItem.Item is Habitat)
                {
                    Habitat habitat = (Habitat)tradeableItem.Item;
                    this.TradeableItems.Nodes.Add(new TreeNode(string.Format(TextResolver.GetText("Trade Description System Map"), (object)habitat.Name) + " (" + tradeableItem.Value.ToString("###,###,###,##0") + ")")
                    {
                        NodeFont = this.Font,
                        Tag = (object)tradeableItem
                    });
                    break;
                }
            }
            bool flag4 = false;
            TreeNode node5 = new TreeNode(TextResolver.GetText("Locations of Independent Colonies"));
            node5.NodeFont = this.Font;
            foreach (TradeableItem tradeableItem in (SyncList<TradeableItem>)tradeableItems)
            {
                if (tradeableItem.Type == TradeableItemType.IndependentColonyLocation)
                {
                    flag4 = true;
                    node5.Nodes.Add(new TreeNode(((StellarObject)tradeableItem.Item).Name + " (" + tradeableItem.Value.ToString("###,###,###,##0") + ")")
                    {
                        NodeFont = this.Font,
                        Tag = (object)tradeableItem
                    });
                }
            }
            if (flag4)
            {
                if (this._IndependentColoniesToggled)
                    node5.Toggle();
                this.TradeableItems.Nodes.Add(node5);
            }
            bool flag5 = false;
            TreeNode node6 = new TreeNode(TextResolver.GetText("Secret Locations"));
            node6.NodeFont = this.Font;
            foreach (TradeableItem tradeableItem in (SyncList<TradeableItem>)tradeableItems)
            {
                if (tradeableItem.Type == TradeableItemType.SecretLocation)
                {
                    flag5 = true;
                    string str = string.Empty;
                    if (tradeableItem.Item is GalaxyLocation)
                        str = ((GalaxyLocation)tradeableItem.Item).Name;
                    else if (tradeableItem.Item is Habitat)
                        str = ((StellarObject)tradeableItem.Item).Name;
                    if (this._OtherEmpire == this._Galaxy.PlayerEmpire)
                        str = TextResolver.GetText("Secret Location");
                    node6.Nodes.Add(new TreeNode(str + " (" + tradeableItem.Value.ToString("###,###,###,##0") + ")")
                    {
                        NodeFont = this.Font,
                        Tag = (object)tradeableItem
                    });
                }
            }
            if (flag5)
            {
                if (this._SecretLocationsToggled)
                    node6.Toggle();
                this.TradeableItems.Nodes.Add(node6);
            }
            bool flag6 = false;
            TreeNode node7 = new TreeNode(TextResolver.GetText("Research"));
            node7.NodeFont = this.Font;
            foreach (TradeableItem tradeableItem in (SyncList<TradeableItem>)tradeableItems)
            {
                if (tradeableItem.Type == TradeableItemType.ResearchProject)
                {
                    flag6 = true;
                    node7.Nodes.Add(new TreeNode(((ResearchNode)tradeableItem.Item).Name + " (" + tradeableItem.Value.ToString("###,###,###,##0") + ")")
                    {
                        NodeFont = this.Font,
                        Tag = (object)tradeableItem
                    });
                }
            }
            if (flag6)
            {
                if (this._ResearchToggled)
                    node7.Toggle();
                this.TradeableItems.Nodes.Add(node7);
            }
            bool flag7 = false;
            TreeNode node8 = new TreeNode(TextResolver.GetText("Declare War on..."));
            node8.NodeFont = this.Font;
            foreach (TradeableItem tradeableItem in (SyncList<TradeableItem>)tradeableItems)
            {
                if (tradeableItem.Type == TradeableItemType.DeclareWarOther)
                {
                    flag7 = true;
                    node8.Nodes.Add(new TreeNode(((Empire)tradeableItem.Item).Name + " (" + tradeableItem.Value.ToString("###,###,###,##0") + ")")
                    {
                        NodeFont = this.Font,
                        Tag = (object)tradeableItem
                    });
                }
            }
            if (flag7)
            {
                if (this._DeclareWarToggled)
                    node8.Toggle();
                this.TradeableItems.Nodes.Add(node8);
            }
            bool flag8 = false;
            TreeNode node9 = new TreeNode(TextResolver.GetText("Initiate Trade Sanctions against..."));
            node9.NodeFont = this.Font;
            foreach (TradeableItem tradeableItem in (SyncList<TradeableItem>)tradeableItems)
            {
                if (tradeableItem.Type == TradeableItemType.InitiateTradeSanctionsOther)
                {
                    flag8 = true;
                    node9.Nodes.Add(new TreeNode(((Empire)tradeableItem.Item).Name + " (" + tradeableItem.Value.ToString("###,###,###,##0") + ")")
                    {
                        NodeFont = this.Font,
                        Tag = (object)tradeableItem
                    });
                }
            }
            if (flag8)
            {
                if (this._TradeSanctionsToggled)
                    node9.Toggle();
                this.TradeableItems.Nodes.Add(node9);
            }
            bool flag9 = false;
            TreeNode node10 = new TreeNode(TextResolver.GetText("End War with..."));
            node10.NodeFont = this.Font;
            foreach (TradeableItem tradeableItem in (SyncList<TradeableItem>)tradeableItems)
            {
                if (tradeableItem.Type == TradeableItemType.EndWarOther)
                {
                    flag9 = true;
                    node10.Nodes.Add(new TreeNode(((Empire)tradeableItem.Item).Name + " (" + tradeableItem.Value.ToString("###,###,###,##0") + ")")
                    {
                        NodeFont = this.Font,
                        Tag = (object)tradeableItem
                    });
                }
            }
            if (flag9)
            {
                if (this._EndWarToggled)
                    node10.Toggle();
                this.TradeableItems.Nodes.Add(node10);
            }
            bool flag10 = false;
            TreeNode node11 = new TreeNode(TextResolver.GetText("Lift Trade Sanctions against..."));
            node11.NodeFont = this.Font;
            foreach (TradeableItem tradeableItem in (SyncList<TradeableItem>)tradeableItems)
            {
                if (tradeableItem.Type == TradeableItemType.LiftTradeSanctionsOther)
                {
                    flag10 = true;
                    node11.Nodes.Add(new TreeNode(((Empire)tradeableItem.Item).Name + " (" + tradeableItem.Value.ToString("###,###,###,##0") + ")")
                    {
                        NodeFont = this.Font,
                        Tag = (object)tradeableItem
                    });
                }
            }
            if (flag10)
            {
                if (this._LiftTradeSanctionsToggled)
                    node11.Toggle();
                this.TradeableItems.Nodes.Add(node11);
            }
            TreeNode node12 = new TreeNode(TextResolver.GetText("Lift Trade Sanctions against Your Empire"));
            node12.NodeFont = this.Font;
            foreach (TradeableItem tradeableItem in (SyncList<TradeableItem>)tradeableItems)
            {
                if (tradeableItem.Type == TradeableItemType.LiftTradeSanctions)
                {
                    Empire empire = (Empire)tradeableItem.Item;
                    node12.NodeFont = this.Font;
                    node12.Tag = (object)tradeableItem;
                    this.TradeableItems.Nodes.Add(node12);
                }
            }
            TreeNode node13 = new TreeNode(TextResolver.GetText("End War with Your Empire"));
            node13.NodeFont = this.Font;
            foreach (TradeableItem tradeableItem in (SyncList<TradeableItem>)tradeableItems)
            {
                if (tradeableItem.Type == TradeableItemType.EndWar)
                {
                    Empire empire = (Empire)tradeableItem.Item;
                    node13.NodeFont = this.Font;
                    node13.Tag = (object)tradeableItem;
                    this.TradeableItems.Nodes.Add(node13);
                }
            }
            if (this._AllowDiplomaticThreats)
            {
                TreeNode node14 = new TreeNode(TextResolver.GetText("Threaten Trade Sanctions unless you agree"));
                node14.NodeFont = this.Font;
                foreach (TradeableItem tradeableItem in (SyncList<TradeableItem>)tradeableItems)
                {
                    if (tradeableItem.Type == TradeableItemType.ThreatenTradeSanctions)
                    {
                        Empire empire = (Empire)tradeableItem.Item;
                        node14.NodeFont = this.Font;
                        node14.Tag = (object)tradeableItem;
                        this.TradeableItems.Nodes.Add(node14);
                    }
                }
                TreeNode node15 = new TreeNode(TextResolver.GetText("Threaten War unless you agree"));
                node15.NodeFont = this.Font;
                foreach (TradeableItem tradeableItem in (SyncList<TradeableItem>)tradeableItems)
                {
                    if (tradeableItem.Type == TradeableItemType.ThreatenWar)
                    {
                        Empire empire = (Empire)tradeableItem.Item;
                        node15.NodeFont = this.Font;
                        node15.Tag = (object)tradeableItem;
                        this.TradeableItems.Nodes.Add(node15);
                    }
                }
            }
            foreach (TreeNode node16 in this.TradeableItems.Nodes)
            {
                node16.Text = node16.Text;
                foreach (TreeNode node17 in node16.Nodes)
                    node17.Text = node17.Text;
            }
        }

        private void DoLayout()
        {
            int width = this.Width;
            int height = 100;
            int num = 34;
            this.LocalizeControlText();
            this.pnlTitleHeaderArea.Size = new Size(width, num);
            this.pnlTitleHeaderArea.Location = new Point(0, 0);
            this.pnlTitleHeaderArea.BringToFront();
            this.picFlag.Size = new Size(40, 24);
            this.picFlag.Location = new Point(2, 4);
            this.picFlag.SizeMode = PictureBoxSizeMode.Zoom;
            this.picFlag.BringToFront();
            this.lblTitle.Size = new Size(width - 47, num);
            this.lblTitle.Font = new Font(this.Font.FontFamily, this.Font.Size + 2f, FontStyle.Bold, GraphicsUnit.Pixel);
            this.lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            this.lblTitle.Location = new Point(47, 2);
            this.lblTitle.BringToFront();
            this.TradeableItems.Scrollable = true;
            this.TradeableItems.Size = new Size(width, this.Height - (num + num + height + 25));
            this.TradeableItems.Location = new Point(0, num);
            this.pnlSelectedHeaderArea.Size = new Size(width, num);
            this.pnlSelectedHeaderArea.Location = new Point(0, num + this.TradeableItems.Height);
            this.pnlSelectedHeaderArea.BringToFront();
            this.lblSelected.Size = new Size(width, num);
            this.lblSelected.Font = new Font(this.Font.FontFamily, this.Font.Size + 2f, FontStyle.Bold, GraphicsUnit.Pixel);
            this.lblSelected.Location = new Point(0, num + this.TradeableItems.Height + 3);
            this.lblSelected.TextAlign = ContentAlignment.MiddleCenter;
            this.lblSelected.BringToFront();
            this.SelectedItems.Font = this.Font;
            this.SelectedItems.Size = new Size(width, height);
            this.SelectedItems.Location = new Point(0, num + this.TradeableItems.Height + num);
            this.btnClearSelectedItems.Size = new Size(150, 25);
            this.btnClearSelectedItems.Location = new Point((width - 150) / 2, this.Height - 25);
            this.btnClearSelectedItems.BringToFront();
            this.btnClearSelectedItems.Visible = true;
        }

        public TradeableItemList FilterOutSelectedAndExcludedItems(TradeableItemList tradeableItems)
        {
            TradeableItemList tradeableItemList = tradeableItems.Clone();
            List<int> intList = new List<int>();
            foreach (TradeableItem selectedItem in (SyncList<TradeableItem>)this._SelectedItems)
            {
                if (selectedItem.Type != TradeableItemType.Money)
                {
                    int num = tradeableItems.IndexOf(selectedItem);
                    if (num >= 0)
                        intList.Add(num);
                }
            }
            if (this._ExcludedItems != null)
            {
                foreach (TradeableItem excludedItem in (SyncList<TradeableItem>)this._ExcludedItems)
                {
                    if (excludedItem.Type != TradeableItemType.Money)
                    {
                        int num = tradeableItems.IndexOf(excludedItem);
                        if (num >= 0)
                            intList.Add(num);
                    }
                }
            }
            intList.Sort();
            intList.Reverse();
            foreach (int index in intList)
                tradeableItemList.RemoveAt(index);
            return tradeableItemList;
        }

        private int ResolveHoveredItemIndex(int x, int y, out Rectangle bounds)
        {
            bounds = Rectangle.Empty;
            if (this.SelectedItems.Items != null)
            {
                for (int index = 0; index < this.SelectedItems.Items.Count; ++index)
                {
                    Rectangle itemRectangle = this.SelectedItems.GetItemRectangle(index);
                    if (itemRectangle.Contains(new Point(x, y)))
                    {
                        bounds = itemRectangle;
                        return index;
                    }
                }
            }
            return -1;
        }

        private void SelectedItems_MouseMove(object sender, MouseEventArgs e)
        {
            if (!this.AcceptingMouseMoveEvents)
                return;
            TradeableItem tradeableItem = (TradeableItem)null;
            Rectangle bounds = Rectangle.Empty;
            int index = this.ResolveHoveredItemIndex(e.X, e.Y, out bounds);
            if (index >= 0)
                tradeableItem = this._SelectedItems[index];
            if (tradeableItem != null)
            {
                if (tradeableItem.Type == TradeableItemType.ResearchProject && tradeableItem.Item is ResearchNode)
                {
                    ResearchNode researchProject = (ResearchNode)tradeableItem.Item;
                    if (bounds.Contains(e.X, e.Y))
                    {
                        int width = Math.Min(bounds.Width, this.SelectedItems.Width);
                        Rectangle relativeRectangle = new Rectangle(this.SelectedItems.Location.X + bounds.X, this.SelectedItems.Location.Y + bounds.Y, width, bounds.Height);
                        ResearchProjectHoveredEventArgs e1 = new ResearchProjectHoveredEventArgs(researchProject, relativeRectangle);
                        if (this.ResearchProjectHovered == null)
                            return;
                        this.ResearchProjectHovered((object)this, e1);
                    }
                    else
                    {
                        if (this.ResearchProjectHovered == null)
                            return;
                        this.ResearchProjectHovered((object)this, new ResearchProjectHoveredEventArgs((ResearchNode)null, Rectangle.Empty));
                    }
                }
                else
                {
                    if (this.ResearchProjectHovered == null)
                        return;
                    this.ResearchProjectHovered((object)this, new ResearchProjectHoveredEventArgs((ResearchNode)null, Rectangle.Empty));
                }
            }
            else
            {
                if (this.ResearchProjectHovered == null)
                    return;
                this.ResearchProjectHovered((object)this, new ResearchProjectHoveredEventArgs((ResearchNode)null, Rectangle.Empty));
            }
        }

        private void SelectedItems_MouseLeave(object sender, EventArgs e)
        {
            if (this.ResearchProjectHovered == null)
                return;
            this.ResearchProjectHovered((object)this, new ResearchProjectHoveredEventArgs((ResearchNode)null, Rectangle.Empty));
        }

        private void TradeableItems_MouseMove(object sender, MouseEventArgs e)
        {
            if (!this.AcceptingMouseMoveEvents)
                return;
            TreeNode nodeAt = this.TradeableItems.GetNodeAt(e.X, e.Y);
            TradeableItem tradeableItem = (TradeableItem)null;
            if (nodeAt != null)
            {
                if (nodeAt.Tag is TradeableItem)
                    tradeableItem = (TradeableItem)nodeAt.Tag;
                if (tradeableItem != null)
                {
                    if (tradeableItem.Type == TradeableItemType.ResearchProject && tradeableItem.Item is ResearchNode)
                    {
                        ResearchNode researchProject = (ResearchNode)tradeableItem.Item;
                        if (nodeAt.Bounds.Contains(e.X, e.Y))
                        {
                            int width = Math.Min(nodeAt.Bounds.Width, this.TradeableItems.Width);
                            Rectangle relativeRectangle = new Rectangle(this.TradeableItems.Location.X + nodeAt.Bounds.X, this.TradeableItems.Location.Y + nodeAt.Bounds.Y, width, nodeAt.Bounds.Height);
                            ResearchProjectHoveredEventArgs e1 = new ResearchProjectHoveredEventArgs(researchProject, relativeRectangle);
                            if (this.ResearchProjectHovered == null)
                                return;
                            this.ResearchProjectHovered((object)this, e1);
                        }
                        else
                        {
                            if (this.ResearchProjectHovered == null)
                                return;
                            this.ResearchProjectHovered((object)this, new ResearchProjectHoveredEventArgs((ResearchNode)null, Rectangle.Empty));
                        }
                    }
                    else
                    {
                        if (this.ResearchProjectHovered == null)
                            return;
                        this.ResearchProjectHovered((object)this, new ResearchProjectHoveredEventArgs((ResearchNode)null, Rectangle.Empty));
                    }
                }
                else
                {
                    if (this.ResearchProjectHovered == null)
                        return;
                    this.ResearchProjectHovered((object)this, new ResearchProjectHoveredEventArgs((ResearchNode)null, Rectangle.Empty));
                }
            }
            else
            {
                if (this.ResearchProjectHovered == null)
                    return;
                this.ResearchProjectHovered((object)this, new ResearchProjectHoveredEventArgs((ResearchNode)null, Rectangle.Empty));
            }
        }

        private void TradeableItems_MouseLeave(object sender, EventArgs e)
        {
            if (this.ResearchProjectHovered == null)
                return;
            this.ResearchProjectHovered((object)this, new ResearchProjectHoveredEventArgs((ResearchNode)null, Rectangle.Empty));
        }

        private void TradeableItems_MouseClick(object sender, MouseEventArgs e)
        {
            TreeNode nodeAt = this.TradeableItems.GetNodeAt(e.X, e.Y);
            TradeableItem tradeableItem = (TradeableItem)null;
            if (nodeAt.Tag is TradeableItem)
                tradeableItem = (TradeableItem)nodeAt.Tag;
            if (tradeableItem != null && e.X >= 25)
            {
                int index = this._SelectedItems.IndexOf(tradeableItem);
                if (tradeableItem.Type == TradeableItemType.AdoptGovernmentStyle)
                    index = this._SelectedItems.FindAnyGovernmentStyle();
                double num1 = Math.Max(20000.0, this._Empire.StateMoney * 0.3);
                if (this._Empire == this._Galaxy.PlayerEmpire)
                    num1 = 0.0;
                if (index < 0)
                {
                    if (tradeableItem.Type == TradeableItemType.Money && tradeableItem.Item is double)
                    {
                        if (this._Empire.StateMoney - num1 >= (double)tradeableItem.Item)
                            this._SelectedItems.Add(tradeableItem);
                        else if (this._Empire.StateMoney - num1 > 0.0)
                        {
                            double moneyAmount = (double)(int)(this._Empire.StateMoney - num1);
                            int num2 = this._Galaxy.ValueMoney(moneyAmount);
                            this._SelectedItems.Add(new TradeableItem(TradeableItemType.Money, (object)moneyAmount, num2));
                        }
                    }
                    else
                        this._SelectedItems.Add(tradeableItem);
                    this.PopulateTradeableItems(this.FilterOutSelectedAndExcludedItems(this._TradeableItems));
                }
                else if (tradeableItem.Type == TradeableItemType.AdoptGovernmentStyle)
                {
                    if (this._SelectedItems[index].Item is GovernmentAttributes)
                    {
                        this._SelectedItems[index] = tradeableItem;
                        this.PopulateTradeableItems(this.FilterOutSelectedAndExcludedItems(this._TradeableItems));
                    }
                }
                else if (tradeableItem.Type == TradeableItemType.Money && this._SelectedItems[index].Item is double)
                {
                    double moneyAmount = (double)this._SelectedItems[index].Item + (double)tradeableItem.Item;
                    if (moneyAmount > this._Empire.StateMoney - num1)
                        moneyAmount = (double)(int)(this._Empire.StateMoney - num1);
                    int num3 = this._Galaxy.ValueMoney(moneyAmount);
                    this._SelectedItems[index] = new TradeableItem(TradeableItemType.Money, (object)moneyAmount, num3);
                }
                this.UpdateOfferedItemsHeading();
                this.SetSelectedItemsInControl();
            }
            else
            {
                if (nodeAt.Nodes == null || nodeAt.Nodes.Count <= 0)
                    return;
                if (nodeAt.Text == TextResolver.GetText("Money"))
                    this._MoneyToggled = !this._MoneyToggled;
                else if (nodeAt.Text == TextResolver.GetText("Disputed Colonies"))
                    this._ColoniesToggled = !this._ColoniesToggled;
                else if (nodeAt.Text == TextResolver.GetText("Disputed Bases"))
                    this._BasesToggled = !this._BasesToggled;
                else if (nodeAt.Text == TextResolver.GetText("Communications with Unknown Empires"))
                    this._EmpireContactsToggled = !this._EmpireContactsToggled;
                else if (nodeAt.Text == TextResolver.GetText("Locations of Independent Colonies"))
                    this._IndependentColoniesToggled = !this._IndependentColoniesToggled;
                else if (nodeAt.Text == TextResolver.GetText("Secret Locations"))
                    this._SecretLocationsToggled = !this._SecretLocationsToggled;
                else if (nodeAt.Text == TextResolver.GetText("Component Tech"))
                    this._ResearchToggled = !this._ResearchToggled;
                else if (nodeAt.Text == TextResolver.GetText("Adopt Government Style"))
                    this._GovernmentStylesToggled = !this._GovernmentStylesToggled;
                else if (nodeAt.Text == TextResolver.GetText("Declare War on..."))
                    this._DeclareWarToggled = !this._DeclareWarToggled;
                else if (nodeAt.Text == TextResolver.GetText("Initiate Trade Sanctions against..."))
                    this._TradeSanctionsToggled = !this._TradeSanctionsToggled;
                else if (nodeAt.Text == TextResolver.GetText("End War with..."))
                    this._EndWarToggled = !this._EndWarToggled;
                else if (nodeAt.Text == TextResolver.GetText("Lift Trade Sanctions against..."))
                    this._LiftTradeSanctionsToggled = !this._LiftTradeSanctionsToggled;
                if (e.X < 25)
                    return;
                nodeAt.Toggle();
            }
        }

        private void SelectedItems_MouseClick(object sender, MouseEventArgs e)
        {
            int selectedIndex = this.SelectedItems.SelectedIndex;
            if (selectedIndex < 0 || this._RequiredItems != null && this._RequiredItems.Contains(this._SelectedItems[selectedIndex]))
                return;
            this.SelectedItems.Items.RemoveAt(selectedIndex);
            this._SelectedItems.RemoveAt(selectedIndex);
            this.PopulateTradeableItems(this.FilterOutSelectedAndExcludedItems(this._TradeableItems));
            this.UpdateOfferedItemsHeading();
        }

        private void btnClearSelectedItems_Click(object sender, EventArgs e)
        {
            //BaconDiplomacyTradeTree.AdjustItemCosts(this);
            this.SelectedItems.Items.Clear();
            this._SelectedItems.Clear();
            if (this._RequiredItems != null && this._RequiredItems.Count > 0)
                this._SelectedItems.AddRange((IEnumerable<TradeableItem>)this._RequiredItems);
            if (this._SelectedItems != null)
            {
                foreach (TradeableItem selectedItem in (SyncList<TradeableItem>)this._SelectedItems)
                {
                    if (selectedItem != null)
                        this.SelectedItems.Items.Add((object)selectedItem);
                }
            }
            this.PopulateTradeableItems(this.FilterOutSelectedAndExcludedItems(this._TradeableItems));
            this.UpdateOfferedItemsHeading();
        }

        private void TradeableItems_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e == null || e.Node == null)
                return;
            if (e.Node.Text == TextResolver.GetText("Money"))
                this._MoneyToggled = true;
            else if (e.Node.Text == TextResolver.GetText("Disputed Colonies"))
                this._ColoniesToggled = true;
            else if (e.Node.Text == TextResolver.GetText("Disputed Bases"))
                this._BasesToggled = true;
            else if (e.Node.Text == TextResolver.GetText("Communications with Unknown Empires"))
                this._EmpireContactsToggled = true;
            else if (e.Node.Text == TextResolver.GetText("Locations of Independent Colonies"))
                this._IndependentColoniesToggled = true;
            else if (e.Node.Text == TextResolver.GetText("Secret Locations"))
                this._SecretLocationsToggled = true;
            else if (e.Node.Text == TextResolver.GetText("Component Tech"))
                this._ResearchToggled = true;
            else if (e.Node.Text == TextResolver.GetText("Adopt Government Style"))
                this._GovernmentStylesToggled = true;
            else if (e.Node.Text == TextResolver.GetText("Declare War on..."))
                this._DeclareWarToggled = true;
            else if (e.Node.Text == TextResolver.GetText("Initiate Trade Sanctions against..."))
                this._TradeSanctionsToggled = true;
            else if (e.Node.Text == TextResolver.GetText("End War with..."))
            {
                this._EndWarToggled = true;
            }
            else
            {
                if (!(e.Node.Text == TextResolver.GetText("Lift Trade Sanctions against...")))
                    return;
                this._LiftTradeSanctionsToggled = true;
            }
        }

        private void TradeableItems_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if (e == null || e.Node == null)
                return;
            if (e.Node.Text == TextResolver.GetText("Money"))
                this._MoneyToggled = false;
            else if (e.Node.Text == TextResolver.GetText("Disputed Colonies"))
                this._ColoniesToggled = false;
            else if (e.Node.Text == TextResolver.GetText("Disputed Bases"))
                this._BasesToggled = false;
            else if (e.Node.Text == TextResolver.GetText("Communications with Unknown Empires"))
                this._EmpireContactsToggled = false;
            else if (e.Node.Text == TextResolver.GetText("Locations of Independent Colonies"))
                this._IndependentColoniesToggled = false;
            else if (e.Node.Text == TextResolver.GetText("Secret Locations"))
                this._SecretLocationsToggled = false;
            else if (e.Node.Text == TextResolver.GetText("Component Tech"))
                this._ResearchToggled = false;
            else if (e.Node.Text == TextResolver.GetText("Adopt Government Style"))
                this._GovernmentStylesToggled = false;
            else if (e.Node.Text == TextResolver.GetText("Declare War on..."))
                this._DeclareWarToggled = false;
            else if (e.Node.Text == TextResolver.GetText("Initiate Trade Sanctions against..."))
                this._TradeSanctionsToggled = false;
            else if (e.Node.Text == TextResolver.GetText("End War with..."))
            {
                this._EndWarToggled = false;
            }
            else
            {
                if (!(e.Node.Text == TextResolver.GetText("Lift Trade Sanctions against...")))
                    return;
                this._LiftTradeSanctionsToggled = false;
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
            this.TradeableItems = new TreeView();
            this.SelectedItems = new ListBox();
            this.lblTitle = new Label();
            this.lblSelected = new Label();
            this.picFlag = new PictureBox();
            this.pnlTitleHeaderArea = new Panel();
            this.pnlSelectedHeaderArea = new Panel();
            this.btnClearSelectedItems = new GlassButton();
            ((ISupportInitialize)this.picFlag).BeginInit();
            this.SuspendLayout();
            this.TradeableItems.BackColor = Color.FromArgb(48, 48, 64);
            this.TradeableItems.BorderStyle = BorderStyle.None;
            this.TradeableItems.Font = new Font("Verdana", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.TradeableItems.ForeColor = Color.White;
            this.TradeableItems.Location = new Point(5, 40);
            this.TradeableItems.Name = "TradeableItems";
            this.TradeableItems.Size = new Size(121, 97);
            this.TradeableItems.TabIndex = 0;
            this.TradeableItems.MouseClick += new MouseEventHandler(this.TradeableItems_MouseClick);
            this.TradeableItems.BeforeExpand += new TreeViewCancelEventHandler(this.TradeableItems_BeforeExpand);
            this.TradeableItems.BeforeCollapse += new TreeViewCancelEventHandler(this.TradeableItems_BeforeCollapse);
            this.TradeableItems.MouseMove += new MouseEventHandler(this.TradeableItems_MouseMove);
            this.TradeableItems.MouseLeave += new EventHandler(this.TradeableItems_MouseLeave);
            this.SelectedItems.BackColor = Color.FromArgb(48, 48, 64);
            this.SelectedItems.BorderStyle = BorderStyle.None;
            this.SelectedItems.Font = new Font("Verdana", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.SelectedItems.ForeColor = Color.White;
            this.SelectedItems.FormattingEnabled = true;
            this.SelectedItems.Location = new Point(5, 174);
            this.SelectedItems.Name = "SelectedItems";
            this.SelectedItems.Size = new Size(120, 91);
            this.SelectedItems.TabIndex = 1;
            this.SelectedItems.MouseClick += new MouseEventHandler(this.SelectedItems_MouseClick);
            this.SelectedItems.MouseMove += new MouseEventHandler(this.SelectedItems_MouseMove);
            this.SelectedItems.MouseLeave += new EventHandler(this.SelectedItems_MouseLeave);
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = Color.FromArgb(80, 80, 112);
            this.lblTitle.Font = new Font("Verdana", 9.75f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.Location = new Point(48, 4);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(39, 16);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Title";
            this.lblSelected.AutoSize = true;
            this.lblSelected.BackColor = Color.FromArgb(80, 80, 112);
            this.lblSelected.Font = new Font("Verdana", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.lblSelected.ForeColor = Color.White;
            this.lblSelected.Location = new Point(17, 144);
            this.lblSelected.Name = "lblSelected";
            this.lblSelected.Size = new Size(100, 14);
            this.lblSelected.TabIndex = 3;
            this.lblSelected.Text = "Offered Items";
            this.picFlag.BackColor = Color.Transparent;
            this.picFlag.Location = new Point(4, 3);
            this.picFlag.Name = "picFlag";
            this.picFlag.Size = new Size(33, 26);
            this.picFlag.TabIndex = 5;
            this.picFlag.TabStop = false;
            this.pnlTitleHeaderArea.BackColor = Color.FromArgb(80, 80, 112);
            this.pnlTitleHeaderArea.Location = new Point(102, 4);
            this.pnlTitleHeaderArea.Name = "pnlTitleHeaderArea";
            this.pnlTitleHeaderArea.Size = new Size(79, 30);
            this.pnlTitleHeaderArea.TabIndex = 6;
            this.pnlSelectedHeaderArea.BackColor = Color.FromArgb(80, 80, 112);
            this.pnlSelectedHeaderArea.Location = new Point(105, 143);
            this.pnlSelectedHeaderArea.Name = "pnlSelectedHeaderArea";
            this.pnlSelectedHeaderArea.Size = new Size(79, 30);
            this.pnlSelectedHeaderArea.TabIndex = 7;
            this.btnClearSelectedItems.BackColor = Color.FromArgb(0, 0, 0);
            this.btnClearSelectedItems.ClipBackground = false;
            this.btnClearSelectedItems.DelayFrameRefresh = false;
            this.btnClearSelectedItems.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Pixel);
            this.btnClearSelectedItems.ForeColor = Color.FromArgb(150, 150, 150);
            this.btnClearSelectedItems.GlowColor = Color.FromArgb(48, 48, 128);
            this.btnClearSelectedItems.InnerBorderColor = Color.FromArgb(67, 67, 77);
            this.btnClearSelectedItems.IntensifyColors = false;
            this.btnClearSelectedItems.Location = new Point(5, 271);
            this.btnClearSelectedItems.Name = "btnClearSelectedItems";
            this.btnClearSelectedItems.OuterBorderColor = Color.FromArgb(0, 0, 16);
            this.btnClearSelectedItems.ShineColor = Color.FromArgb(112, 112, 128);
            this.btnClearSelectedItems.Size = new Size(141, 23);
            this.btnClearSelectedItems.TabIndex = 4;
            this.btnClearSelectedItems.Text = "Clear Offered Items";
            this.btnClearSelectedItems.TextColor = Color.FromArgb(120, 120, 120);
            this.btnClearSelectedItems.TextColor2 = Color.FromArgb((int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue);
            this.btnClearSelectedItems.Click += new EventHandler(this.btnClearSelectedItems_Click);
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(48, 48, 64);
            this.Controls.Add((Control)this.pnlSelectedHeaderArea);
            this.Controls.Add((Control)this.pnlTitleHeaderArea);
            this.Controls.Add((Control)this.picFlag);
            this.Controls.Add((Control)this.btnClearSelectedItems);
            this.Controls.Add((Control)this.lblSelected);
            this.Controls.Add((Control)this.lblTitle);
            this.Controls.Add((Control)this.SelectedItems);
            this.Controls.Add((Control)this.TradeableItems);
            this.Name = nameof(DiplomacyTradeTree);
            this.Size = new Size(184, 339);
            ((ISupportInitialize)this.picFlag).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }


}
