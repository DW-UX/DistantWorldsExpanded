// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.EncyclopediaTopicTree
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class EncyclopediaTopicTree : UserControl
    {
        private EncyclopediaItemList _EncyclopediaItems = new EncyclopediaItemList();
        protected IFontCache _FontCache;
        private float _FontSize = 18.67f;
        private bool _FontIsBold;
        private IContainer components;
        private TreeView Topics;
        private bool _alreadyClicked = false;

        public event EventHandler<EncyclopediaItemChangedEventArgs> OnEncyclopediaItemSelected;

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

        public EncyclopediaTopicTree()
        {
            this.InitializeComponent();
            this.LayoutControls();
        }

        public void LayoutControls()
        {
            this.Topics.Size = this.ClientRectangle.Size;
            this.Topics.Location = new Point(0, 0);
        }

        public void BindData(EncyclopediaItemList encyclopediaItems)
        {
            this._EncyclopediaItems = encyclopediaItems;
            this.LayoutControls();
            try
            {
                this.BindItemsToTreeView(this._EncyclopediaItems);
            }
            catch
            {
                // skip
            }
        }

        public void SetSelectedItem(EncyclopediaItem encyclopediaItem)
        {
            if (encyclopediaItem == null)
            {
                this.Topics.SelectedNode = (TreeNode)null;
            }
            else
            {
                if (!this._EncyclopediaItems.Contains(encyclopediaItem))
                    return;
                foreach (TreeNode node1 in this.Topics.Nodes)
                {
                    if (node1.Tag != null && node1.Tag is EncyclopediaItem && (EncyclopediaItem)node1.Tag == encyclopediaItem)
                    {
                        this.Topics.SelectedNode = node1;
                        break;
                    }
                    if (node1.Nodes != null && node1.Nodes.Count > 0)
                    {
                        foreach (TreeNode node2 in node1.Nodes)
                        {
                            if (node2.Tag != null && node2.Tag is EncyclopediaItem && (EncyclopediaItem)node2.Tag == encyclopediaItem)
                            {
                                this.Topics.SelectedNode = node2;
                                return;
                            }
                        }
                    }
                }
            }
        }

        public EncyclopediaItem SelectedItem
        {
            get
            {
                TreeNode selectedNode = this.Topics.SelectedNode;
                return selectedNode != null && selectedNode.Tag != null && selectedNode.Tag is EncyclopediaItem ? (EncyclopediaItem)selectedNode.Tag : (EncyclopediaItem)null;
            }
        }

        private void BindItemsToTreeView(EncyclopediaItemList items)
        {
            this.Topics.Nodes.Clear();
            EncyclopediaItemList encyclopediaItemList1 = new EncyclopediaItemList();
            foreach (EncyclopediaItem encyclopediaItem in (List<EncyclopediaItem>)items)
            {
                if (encyclopediaItem.IsCategoryRoot)
                    encyclopediaItemList1.Add(encyclopediaItem);
            }
            encyclopediaItemList1.Sort();
            foreach (EncyclopediaItem encyclopediaItem1 in (List<EncyclopediaItem>)encyclopediaItemList1)
            {
                TreeNode node = new TreeNode();
                node.NodeFont = this.Font;
                node.Text = encyclopediaItem1.Title;
                node.Tag = (object)encyclopediaItem1;
                List<TreeNode> treeNodeList = new List<TreeNode>();
                EncyclopediaCategory category = encyclopediaItem1.Category;
                EncyclopediaItemList encyclopediaItemList2 = new EncyclopediaItemList();
                foreach (EncyclopediaItem encyclopediaItem2 in (List<EncyclopediaItem>)items)
                {
                    if (!encyclopediaItem2.IsCategoryRoot && encyclopediaItem2.Category == category)
                        encyclopediaItemList2.Add(encyclopediaItem2);
                }
                encyclopediaItemList2.Sort();
                foreach (EncyclopediaItem encyclopediaItem3 in (List<EncyclopediaItem>)encyclopediaItemList2)
                    treeNodeList.Add(new TreeNode(encyclopediaItem3.Title)
                    {
                        NodeFont = this.Font,
                        Text = encyclopediaItem3.Title,
                        Tag = (object)encyclopediaItem3
                    });
                node.Nodes.AddRange(treeNodeList.ToArray());
                this.Topics.Nodes.Add(node);
                node.Text = node.Text;
            }
        }

        public void CollapseAll() => this.Topics.CollapseAll();

        private EncyclopediaItem ResolveEncyclopediaItemFromNode(TreeNode node) => node != null && node.Tag != null && node.Tag is EncyclopediaItem ? (EncyclopediaItem)node.Tag : (EncyclopediaItem)null;

        private void Topics_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (!_alreadyClicked)
            {
                _alreadyClicked = true;
                if (this.OnEncyclopediaItemSelected != null)
                {
                    this.OnEncyclopediaItemSelected((object)this, new EncyclopediaItemChangedEventArgs(this.ResolveEncyclopediaItemFromNode(e.Node)));
                }
                _alreadyClicked = false;
            }
        }

        private void Topics_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!_alreadyClicked)
            {
                _alreadyClicked = true;
                if (this.OnEncyclopediaItemSelected != null)
                { this.OnEncyclopediaItemSelected((object)this, new EncyclopediaItemChangedEventArgs(this.ResolveEncyclopediaItemFromNode(e.Node))); }
                _alreadyClicked = false;
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
            this.Topics = new TreeView();
            this.SuspendLayout();
            this.Topics.BackColor = Color.FromArgb(48, 48, 64);
            this.Topics.BorderStyle = BorderStyle.None;
            this.Topics.Font = new Font("Verdana", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.Topics.ForeColor = Color.FromArgb(170, 170, 170);
            this.Topics.HideSelection = false;
            this.Topics.Location = new Point(3, 3);
            this.Topics.Name = "Topics";
            this.Topics.Size = new Size(121, 97);
            this.Topics.TabIndex = 1;
            this.Topics.AfterSelect += new TreeViewEventHandler(this.Topics_AfterSelect);
            this.Topics.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.Topics_NodeMouseClick);
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add((Control)this.Topics);
            this.Name = nameof(EncyclopediaTopicTree);
            this.Size = new Size(129, 105);
            this.ResumeLayout(false);
        }
    }
}
