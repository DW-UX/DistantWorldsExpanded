// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.HabitatPrioritizationListView
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class HabitatPrioritizationListView : ListViewBase
    {
        private HabitatPrioritizationList _HabitatPrioritizations;
        private Galaxy _Galaxy;
        private Bitmap[] _HabitatImages;
        private Bitmap[] _BuiltObjectImages;
        private Bitmap[] _RaceImages;
        private Bitmap[] _ResourceImages;
        private Bitmap[] _RuinImages;
        private Bitmap _GasCloudImage;
        private IContainer components;

        public HabitatPrioritizationListView()
        {
            this.InitializeComponent();
            this._Grid.ScrollBars = ScrollBars.Both;
            DataGridViewImageColumn gridViewImageColumn1 = new DataGridViewImageColumn();
            gridViewImageColumn1.Description = "Picture";
            gridViewImageColumn1.HeaderText = "";
            gridViewImageColumn1.Name = "Picture";
            gridViewImageColumn1.ReadOnly = true;
            gridViewImageColumn1.ImageLayout = DataGridViewImageCellLayout.Normal;
            gridViewImageColumn1.SortMode = DataGridViewColumnSortMode.NotSortable;
            gridViewImageColumn1.ValueType = typeof(Image);
            gridViewImageColumn1.Width = 30;
            gridViewImageColumn1.FillWeight = 30f;
            this._Grid.Columns.Add((DataGridViewColumn)gridViewImageColumn1);
            DataGridViewTextBoxColumn viewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn1.HeaderText = TextResolver.GetText("Name");
            viewTextBoxColumn1.Name = "Name";
            viewTextBoxColumn1.ReadOnly = false;
            viewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn1.ValueType = typeof(string);
            viewTextBoxColumn1.Width = 130;
            viewTextBoxColumn1.FillWeight = 130f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn1);
            DataGridViewTextBoxColumn viewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn2.HeaderText = TextResolver.GetText("Type");
            viewTextBoxColumn2.Name = "Type";
            viewTextBoxColumn2.ReadOnly = false;
            viewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn2.ValueType = typeof(string);
            viewTextBoxColumn2.Width = 80;
            viewTextBoxColumn2.FillWeight = 80f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn2);
            DataGridViewTextBoxColumn viewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn3.HeaderText = TextResolver.GetText("Size");
            viewTextBoxColumn3.Name = "Size";
            viewTextBoxColumn3.ReadOnly = false;
            viewTextBoxColumn3.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn3.ValueType = typeof(int);
            viewTextBoxColumn3.Width = 70;
            viewTextBoxColumn3.FillWeight = 70f;
            viewTextBoxColumn3.DefaultCellStyle.Format = "0,K";
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn3);
            DataGridViewTextBoxColumn viewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn4.HeaderText = TextResolver.GetText("Quality");
            viewTextBoxColumn4.Name = "Quality";
            viewTextBoxColumn4.ReadOnly = false;
            viewTextBoxColumn4.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn4.ValueType = typeof(float);
            viewTextBoxColumn4.Width = 70;
            viewTextBoxColumn4.FillWeight = 70f;
            viewTextBoxColumn4.DefaultCellStyle.Format = "0%";
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn4);
            DataGridViewTextBoxColumn viewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn5.HeaderText = TextResolver.GetText("Distance");
            viewTextBoxColumn5.ToolTipText = TextResolver.GetText("Distance from your nearest Space Port");
            viewTextBoxColumn5.Name = "Distance";
            viewTextBoxColumn5.ReadOnly = false;
            viewTextBoxColumn5.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn5.ValueType = typeof(double);
            viewTextBoxColumn5.Width = 70;
            viewTextBoxColumn5.FillWeight = 70f;
            viewTextBoxColumn5.DefaultCellStyle.Format = "0,K";
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn5);
            DataGridViewImageColumn gridViewImageColumn2 = (DataGridViewImageColumn)new HabitatPrioritizationListView.SortableImageColumn();
            gridViewImageColumn2.Description = "Race";
            gridViewImageColumn2.HeaderText = TextResolver.GetText("Race");
            gridViewImageColumn2.Name = "Race";
            gridViewImageColumn2.ReadOnly = true;
            gridViewImageColumn2.SortMode = DataGridViewColumnSortMode.Automatic;
            gridViewImageColumn2.ValueType = typeof(string);
            gridViewImageColumn2.Width = 30;
            gridViewImageColumn2.FillWeight = 30f;
            gridViewImageColumn2.DefaultCellStyle.NullValue = (object)null;
            this._Grid.Columns.Add((DataGridViewColumn)gridViewImageColumn2);
            DataGridViewTextBoxColumn viewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn6.HeaderText = TextResolver.GetText("Population Abbreviation");
            viewTextBoxColumn6.Name = "TotalPopulation";
            viewTextBoxColumn6.ReadOnly = false;
            viewTextBoxColumn6.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn6.ValueType = typeof(long);
            viewTextBoxColumn6.Width = 45;
            viewTextBoxColumn6.FillWeight = 45f;
            viewTextBoxColumn6.DefaultCellStyle.Format = "0,,M";
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn6);
            DataGridViewImageColumn gridViewImageColumn3 = (DataGridViewImageColumn)new HabitatPrioritizationListView.SortableImageColumn();
            gridViewImageColumn3.Description = TextResolver.GetText("Resources");
            gridViewImageColumn3.HeaderText = TextResolver.GetText("Resources");
            gridViewImageColumn3.Name = "Resources";
            gridViewImageColumn3.ReadOnly = true;
            gridViewImageColumn3.SortMode = DataGridViewColumnSortMode.Automatic;
            gridViewImageColumn3.ValueType = typeof(string);
            gridViewImageColumn3.Width = 95;
            gridViewImageColumn3.FillWeight = 95f;
            gridViewImageColumn3.DefaultCellStyle.NullValue = (object)null;
            this._Grid.Columns.Add((DataGridViewColumn)gridViewImageColumn3);
            DataGridViewImageColumn gridViewImageColumn4 = (DataGridViewImageColumn)new HabitatPrioritizationListView.SortableImageColumn();
            gridViewImageColumn4.Description = "Ruins";
            gridViewImageColumn4.HeaderText = "";
            gridViewImageColumn4.Name = "Ruins";
            gridViewImageColumn4.ReadOnly = true;
            gridViewImageColumn4.SortMode = DataGridViewColumnSortMode.Automatic;
            gridViewImageColumn4.ValueType = typeof(string);
            gridViewImageColumn4.Width = 30;
            gridViewImageColumn4.FillWeight = 30f;
            gridViewImageColumn4.DefaultCellStyle.NullValue = (object)null;
            this._Grid.Columns.Add((DataGridViewColumn)gridViewImageColumn4);
            DataGridViewImageColumn gridViewImageColumn5 = (DataGridViewImageColumn)new HabitatPrioritizationListView.SortableImageColumn();
            gridViewImageColumn5.Description = "ShipAssigned";
            gridViewImageColumn5.HeaderText = "";
            gridViewImageColumn5.Name = "ShipAssigned";
            gridViewImageColumn5.ReadOnly = true;
            gridViewImageColumn5.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridViewImageColumn5.SortMode = DataGridViewColumnSortMode.Automatic;
            gridViewImageColumn5.ValueType = typeof(string);
            gridViewImageColumn5.Width = 50;
            gridViewImageColumn5.FillWeight = 50f;
            gridViewImageColumn5.DefaultCellStyle.NullValue = (object)null;
            this._Grid.Columns.Add((DataGridViewColumn)gridViewImageColumn5);
            DataGridViewImageColumn gridViewImageColumn6 = (DataGridViewImageColumn)new HabitatPrioritizationListView.SortableImageColumn();
            gridViewImageColumn6.Description = "ResourceRarity";
            gridViewImageColumn6.HeaderText = "";
            gridViewImageColumn6.Name = "ResourceRarity";
            gridViewImageColumn6.ReadOnly = true;
            gridViewImageColumn6.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridViewImageColumn6.SortMode = DataGridViewColumnSortMode.Automatic;
            gridViewImageColumn6.ValueType = typeof(string);
            gridViewImageColumn6.Width = 50;
            gridViewImageColumn6.FillWeight = 50f;
            gridViewImageColumn6.DefaultCellStyle.NullValue = (object)null;
            this._Grid.Columns.Add((DataGridViewColumn)gridViewImageColumn6);
        }

        public HabitatPrioritization SelectedHabitatPrioritization
        {
            get
            {
                DataGridViewSelectedRowCollection selectedRows = this._Grid.SelectedRows;
                if (selectedRows.Count != 1)
                    return (HabitatPrioritization)null;
                //index = -1;
                object tag = selectedRows[0].Cells[1].Tag;
                if (tag is int index && this._HabitatPrioritizations != null)
                { return this._HabitatPrioritizations[index]; }
                else
                { return (HabitatPrioritization)null; }
            }
        }

        public void SelectHabitatPrioritization(HabitatPrioritization habitatPrioritization)
        {
            int num1 = -1;
            if (habitatPrioritization == null)
                return;
            for (int index = 0; index < this._HabitatPrioritizations.Count; ++index)
            {
                if (this._HabitatPrioritizations[index].Habitat == habitatPrioritization.Habitat)
                {
                    num1 = index;
                    break;
                }
            }
            if (num1 < 0)
                return;
            for (int index = 0; index < this._Grid.Rows.Count; ++index)
                this._Grid.Rows[index].Selected = false;
            for (int index = 0; index < this._Grid.Rows.Count; ++index)
            {
                object tag = this._Grid.Rows[index].Cells[1].Tag;
                if (tag != null && tag is int num2 && num2 == num1)
                {
                    this._Grid.Rows[index].Selected = true;
                    this._Grid.FirstDisplayedScrollingRowIndex = index;
                    break;
                }
            }
        }

        private void ClearImages()
        {
            this.ClearImageArray(this._HabitatImages);
            this.ClearImageArray(this._BuiltObjectImages);
            this.ClearImageArray(this._RuinImages);
            this.ClearImageArray(this._RaceImages);
            this.ClearImageArray(this._ResourceImages);
        }

        private void ClearImageArray(Bitmap[] imageArray)
        {
            if (imageArray == null)
                return;
            for (int index = 0; index < imageArray.Length; ++index)
            {
                if (imageArray[index] != null)
                {
                    imageArray[index].Dispose();
                    imageArray[index] = (Bitmap)null;
                }
            }
        }

        public void InitializeImages(
          Bitmap[] habitatImages,
          Bitmap[] builtObjectImages,
          Bitmap[] raceImages,
          Bitmap[] resourceImages,
          Bitmap[] ruinImages,
          Bitmap gasCloudImage)
        {
            this.ClearImages();
            this._HabitatImages = new Bitmap[habitatImages.Length];
            this._BuiltObjectImages = new Bitmap[builtObjectImages.Length];
            this._RaceImages = new Bitmap[raceImages.Length];
            this._ResourceImages = new Bitmap[resourceImages.Length];
            this._RuinImages = new Bitmap[ruinImages.Length];
            for (int index = 0; index < habitatImages.Length; ++index)
                this._HabitatImages[index] = this.PrescaleImage(habitatImages[index], 18);
            for (int index = 0; index < builtObjectImages.Length; ++index)
            {
                this._BuiltObjectImages[index] = this.PrescaleImage(builtObjectImages[index], 18);
                this._BuiltObjectImages[index].RotateFlip(RotateFlipType.Rotate270FlipNone);
            }
            for (int index = 0; index < raceImages.Length; ++index)
                this._RaceImages[index] = this.PrescaleImage(raceImages[index], 18);
            for (int index = 0; index < resourceImages.Length; ++index)
                this._ResourceImages[index] = this.PrescaleImage(resourceImages[index], 18, 17);
            for (int index = 0; index < ruinImages.Length; ++index)
                this._RuinImages[index] = this.PrescaleImage(ruinImages[index], 18);
            this._GasCloudImage = this.PrescaleImage(gasCloudImage, 18);
        }

        private Bitmap PrescaleImage(Bitmap image, int maxHeight) => this.PrescaleImage(image, maxHeight, int.MaxValue);

        private new Bitmap PrescaleImage(Bitmap image, int maxHeight, int maxWidth)
        {
            int width1 = image.Width;
            int height1 = image.Height;
            double num = (double)height1 / (double)maxHeight;
            if ((double)width1 / num > (double)maxWidth)
                num = (double)width1 / (double)maxWidth;
            int width2 = (int)((double)width1 / num);
            int height2 = (int)((double)height1 / num);
            Bitmap bitmap = new Bitmap(width2, height2, PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage((Image)bitmap);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.DrawImage((Image)image, new Rectangle(0, 0, width2, height2));
            graphics.Dispose();
            return bitmap;
        }

        private string ResolveHabitatTypeDescription(Habitat habitat)
        {
            string empty = string.Empty;
            string str1 = Galaxy.ResolveDescription(habitat.Type);
            string str2 = Galaxy.ResolveDescription(habitat.Category);
            return habitat.Category != HabitatCategoryType.Asteroid || habitat.Type != HabitatType.BarrenRock ? str1 + " " + str2 : str2;
        }

        private Bitmap BuildResourcesImage(Habitat habitat, int maximumWidth)
        {
            Bitmap bitmap = (Bitmap)null;
            if (habitat.Resources != null && habitat.Resources.Count > 0)
            {
                HabitatResourceList habitatResourceList = habitat.Resources.Clone();
                if (this._Galaxy.PlayerEmpire.ResourceMap != null && this._Galaxy.PlayerEmpire.ResourceMap.CheckResourcesKnown(habitat))
                {
                    int width1 = 0;
                    foreach (HabitatResource habitatResource in (SyncList<HabitatResource>)habitatResourceList)
                    {
                        width1 += this._ResourceImages[habitatResource.PictureRef].Width;
                        width1 += 2;
                    }
                    if (width1 > 2)
                        width1 -= 2;
                    float num1 = 1f;
                    if (width1 > maximumWidth)
                    {
                        int num2 = (habitatResourceList.Count - 1) * 2;
                        int num3 = width1 - num2;
                        num1 = (float)(maximumWidth - num2) / (float)num3;
                        width1 = maximumWidth;
                    }
                    bitmap = new Bitmap(width1, 18, PixelFormat.Format32bppPArgb);
                    Graphics graphics = Graphics.FromImage((Image)bitmap);
                    graphics.CompositingQuality = CompositingQuality.HighSpeed;
                    graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                    graphics.SmoothingMode = SmoothingMode.None;
                    int x = 0;
                    foreach (HabitatResource habitatResource in (SyncList<HabitatResource>)habitatResourceList)
                    {
                        Bitmap resourceImage = this._ResourceImages[habitatResource.PictureRef];
                        int width2 = (int)((double)resourceImage.Width * (double)num1);
                        int height = (int)((double)resourceImage.Height * (double)num1);
                        Rectangle srcRect = new Rectangle(0, 0, resourceImage.Width, resourceImage.Height);
                        Rectangle destRect = new Rectangle(x, (18 - height) / 2, width2, height);
                        ResourceBonusList resourceBonusList = new ResourceBonusList();
                        if (this._Galaxy.PlayerEmpire.DominantRace != null)
                            resourceBonusList = this._Galaxy.PlayerEmpire.DominantRace.CriticalResources;
                        if (resourceBonusList.GetBonusByResourceType(habitatResource.ResourceID) != null)
                        {
                            using (Pen pen = new Pen(Color.Yellow, 1f))
                            {
                                pen.DashStyle = DashStyle.Dot;
                                Rectangle rect = new Rectangle(x, 0, destRect.Width - 1, destRect.Height - 1);
                                graphics.DrawRectangle(pen, rect);
                            }
                        }
                        graphics.DrawImage((Image)resourceImage, destRect, srcRect, GraphicsUnit.Pixel);
                        x = x + width2 + 2;
                    }
                }
            }
            return bitmap;
        }

        private string BuildResourcesDescription(Habitat habitat)
        {
            string str1 = string.Empty;
            string str2;
            if (this._Galaxy.PlayerEmpire.ResourceMap != null && this._Galaxy.PlayerEmpire.ResourceMap.CheckResourcesKnown(habitat))
            {
                if (habitat.Resources != null && habitat.Resources.Count > 0)
                {
                    foreach (HabitatResource resource in (SyncList<HabitatResource>)habitat.Resources)
                    {
                        str1 += resource.Name;
                        double num = (double)resource.Abundance / 10.0;
                        str1 = str1 + " (" + num.ToString("0") + "%)";
                        str1 += ", ";
                    }
                    str2 = str1.Substring(0, str1.Length - 2);
                }
                else
                    str2 = "(" + TextResolver.GetText("No resources") + ")";
            }
            else
                str2 = "(" + TextResolver.GetText("Unknown resources") + ")";
            return str2;
        }

        public void ClearData()
        {
            this._HabitatPrioritizations = (HabitatPrioritizationList)null;
            this._Galaxy = (Galaxy)null;
        }

        public void BindData(
          Galaxy galaxy,
          HabitatPrioritizationList habitatPrioritizations,
          bool bindingForColonization)
        {
            this._Galaxy = galaxy;
            this._HabitatPrioritizations = habitatPrioritizations;
            this._Grid.SuspendLayout();
            DataGridViewAutoSizeColumnsMode autoSizeColumnsMode = this._Grid.AutoSizeColumnsMode;
            DataGridViewAutoSizeRowsMode autoSizeRowsMode = this._Grid.AutoSizeRowsMode;
            this._Grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            this._Grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            this._Grid.AutoSize = false;
            this._Grid.Rows.Clear();
            if (habitatPrioritizations != null && habitatPrioritizations.Count > 0)
                this._Grid.Rows.Add(habitatPrioritizations.Count);
            bool flag1 = this._Galaxy.PlayerEmpire.CheckEmpireTechCanSurviveStorms();
            bool flag2 = this._Galaxy.PlayerEmpire.CheckConstructionShipAndMiningStationCanSurviveStorms();
            if (habitatPrioritizations != null)
            {
                for (int index = 0; index < habitatPrioritizations.Count; ++index)
                {
                    HabitatPrioritization habitatPrioritization = habitatPrioritizations[index];
                    if (habitatPrioritization.Habitat != null)
                    {
                        DataGridViewRow row = this._Grid.Rows[index];
                        row.Cells[0].Value = habitatPrioritization.Habitat.Category != HabitatCategoryType.GasCloud ? (object)this._HabitatImages[(int)habitatPrioritization.Habitat.PictureRef] : (object)this._GasCloudImage;
                        row.Cells[1].Value = (object)habitatPrioritization.Habitat.Name;
                        row.Cells[1].Tag = (object)index;
                        row.Cells[2].Value = (object)this.ResolveHabitatTypeDescription(habitatPrioritization.Habitat);
                        row.Cells[3].Value = (object)((int)habitatPrioritization.Habitat.Diameter * 100);
                        row.Cells[4].Value = (object)habitatPrioritization.Habitat.Quality;
                        BuiltObject nearestSpacePort = galaxy.FastFindNearestSpacePort(habitatPrioritization.Habitat.Xpos, habitatPrioritization.Habitat.Ypos, galaxy.PlayerEmpire);
                        if (nearestSpacePort != null)
                        {
                            double distance = galaxy.CalculateDistance(nearestSpacePort.Xpos, nearestSpacePort.Ypos, habitatPrioritization.Habitat.Xpos, habitatPrioritization.Habitat.Ypos);
                            row.Cells[5].Value = (object)distance;
                        }
                        else
                            row.Cells[5].Value = (object)99999999;
                        if (habitatPrioritization.Habitat.Population != null)
                        {
                            Race dominantRace = habitatPrioritization.Habitat.Population.DominantRace;
                            if (dominantRace != null)
                            {
                                ((HabitatPrioritizationListView.SortableImageCell)row.Cells[6]).ScaledImage = this._RaceImages[dominantRace.PictureRef];
                                row.Cells[6].Value = (object)dominantRace.Name;
                                row.Cells[6].ToolTipText = dominantRace.Name;
                            }
                        }
                        if (habitatPrioritization.Habitat.Population != null)
                        {
                            long totalAmount = habitatPrioritization.Habitat.Population.TotalAmount;
                            row.Cells[7].Value = (object)totalAmount;
                        }
                        else
                            row.Cells[7].Value = (object)0;
                        Bitmap bitmap = this.BuildResourcesImage(habitatPrioritization.Habitat, row.Cells[8].Size.Width);
                        if (bitmap != null)
                            ((HabitatPrioritizationListView.SortableImageCell)row.Cells[8]).ScaledImage = bitmap;
                        string str1 = this.BuildResourcesDescription(habitatPrioritization.Habitat);
                        row.Cells[8].Value = (object)str1;
                        row.Cells[8].ToolTipText = str1;
                        if (habitatPrioritization.Habitat.Ruin != null)
                        {
                            ((HabitatPrioritizationListView.SortableImageCell)row.Cells[9]).ScaledImage = this._RuinImages[habitatPrioritization.Habitat.Ruin.PictureRef];
                            string str2 = habitatPrioritization.Habitat.Ruin.Name + " (" + ((int)(habitatPrioritization.Habitat.Ruin.DevelopmentBonus * 100.0)).ToString("+##0;-##0;0") + "%)";
                            row.Cells[9].Value = (object)str2;
                            row.Cells[9].ToolTipText = habitatPrioritization.Habitat.Ruin.Name;
                        }
                        if (habitatPrioritization.AssignedShip != null)
                        {
                            ((HabitatPrioritizationListView.SortableImageCell)row.Cells[10]).ScaledImage = this._BuiltObjectImages[habitatPrioritization.AssignedShip.PictureRef];
                            row.Cells[10].Value = (object)habitatPrioritization.AssignedShip.Name;
                            string empty = string.Empty;
                            if (habitatPrioritization.AssignedShip.SubRole == BuiltObjectSubRole.ColonyShip)
                                empty += string.Format(TextResolver.GetText("COLONYSHIP colonizing here"), (object)habitatPrioritization.AssignedShip.Name);
                            else if (habitatPrioritization.AssignedShip.SubRole == BuiltObjectSubRole.ConstructionShip)
                                empty += string.Format(TextResolver.GetText("CONSTRUCTIONSHIP building mining station here"), (object)habitatPrioritization.AssignedShip.Name);
                            row.Cells[10].ToolTipText = empty;
                        }
                        row.Cells[11].Value = GetResourceCellRarity(habitatPrioritization.Habitat.Resources);
                        string str3 = string.Empty;
                        Color color1 = Color.FromArgb(170, 170, 170);
                        Color color2 = Color.FromArgb(170, 170, 170);
                        bool canColonizeBecauseAtWar = false;
                        bool flag3 = true;
                        bool flag4;
                        if (bindingForColonization)
                        {
                            flag4 = this._Galaxy.CheckEmpireTerritoryCanColonizeHabitat(this._Galaxy.PlayerEmpire, habitatPrioritization.Habitat, out canColonizeBecauseAtWar);
                            flag3 = this._Galaxy.PlayerEmpire.CanEmpireColonizeHabitatRange(this._Galaxy.PlayerEmpire, habitatPrioritization.Habitat);
                        }
                        else
                            flag4 = this._Galaxy.CheckEmpireTerritoryCanBuildAtHabitat(this._Galaxy.PlayerEmpire, habitatPrioritization.Habitat);
                        SystemInfo system = this._Galaxy.Systems[habitatPrioritization.Habitat.SystemIndex];
                        Empire empire = (Empire)null;
                        if (system.DominantEmpire != null && system.DominantEmpire.Empire != null)
                            empire = system.DominantEmpire.Empire;
                        if (!flag3)
                        {
                            color1 = Color.FromArgb(192, 48, 48);
                            color2 = Color.FromArgb((int)byte.MaxValue, 24, 24);
                            str3 = TextResolver.GetText("Too far from existing colonies");
                        }
                        else if (habitatPrioritization.Habitat.Ruin != null && habitatPrioritization.Habitat.Ruin.PlayerEmpireEncountered && (habitatPrioritization.Habitat.Ruin.BonusDefensive > 0.0 || habitatPrioritization.Habitat.Ruin.BonusDiplomacy > 0.0 || habitatPrioritization.Habitat.Ruin.BonusHappiness > 0.0 || habitatPrioritization.Habitat.Ruin.BonusResearchEnergy > 0.0 || habitatPrioritization.Habitat.Ruin.BonusResearchHighTech > 0.0 || habitatPrioritization.Habitat.Ruin.BonusResearchWeapons > 0.0 || habitatPrioritization.Habitat.Ruin.BonusWealth > 0.0))
                        {
                            color1 = Color.FromArgb(96, 96, (int)byte.MaxValue);
                            color2 = Color.FromArgb(48, 48, (int)byte.MaxValue);
                            str3 = string.Format(TextResolver.GetText("Special ruins at this X"), (object)Galaxy.ResolveDescription(habitatPrioritization.Habitat.Category).ToLower(CultureInfo.InvariantCulture));
                        }
                        else if (habitatPrioritization.Habitat.Resources != null && habitatPrioritization.Habitat.Resources.HasSuperLuxuryResources() && galaxy != null && galaxy.PlayerEmpire != null && galaxy.PlayerEmpire.ResourceMap != null && galaxy.PlayerEmpire.ResourceMap.CheckResourcesKnown(habitatPrioritization.Habitat))
                        {
                            color1 = Color.FromArgb(96, 96, (int)byte.MaxValue);
                            color2 = Color.FromArgb(48, 48, (int)byte.MaxValue);
                            str3 = string.Format(TextResolver.GetText("Special luxury resources at this X"), (object)Galaxy.ResolveDescription(habitatPrioritization.Habitat.Category).ToLower(CultureInfo.InvariantCulture));
                        }
                        else if (bindingForColonization && empire == this._Galaxy.PlayerEmpire && (double)habitatPrioritization.Habitat.Quality >= 0.5)
                        {
                            color1 = Color.FromArgb(0, 192, 0);
                            color2 = Color.FromArgb(0, (int)byte.MaxValue, 0);
                            str3 = string.Format(TextResolver.GetText("X is in one of our systems"), (object)Galaxy.ResolveDescription(habitatPrioritization.Habitat.Category));
                        }
                        else if (!bindingForColonization && empire == this._Galaxy.PlayerEmpire)
                        {
                            color1 = Color.FromArgb(0, 192, 0);
                            color2 = Color.FromArgb(0, (int)byte.MaxValue, 0);
                            str3 = string.Format(TextResolver.GetText("X is in one of our systems"), (object)Galaxy.ResolveDescription(habitatPrioritization.Habitat.Category));
                        }
                        else if (this._Galaxy.PlayerEmpire.CheckNearPirateBase(habitatPrioritization.Habitat, habitatPrioritization.Habitat.Xpos, habitatPrioritization.Habitat.Ypos))
                        {
                            color1 = Color.FromArgb(192, 192, 0);
                            color2 = Color.FromArgb((int)byte.MaxValue, (int)byte.MaxValue, 0);
                            str3 = TextResolver.GetText("Pirate base in this system");
                        }
                        else if (bindingForColonization && habitatPrioritization.Habitat != null && flag4 && canColonizeBecauseAtWar)
                        {
                            color1 = Color.FromArgb(192, 48, 48);
                            color2 = Color.FromArgb((int)byte.MaxValue, 24, 24);
                            str3 = TextResolver.GetText("Colonization target in another empire's system");
                        }
                        else if (!bindingForColonization && habitatPrioritization.Habitat != null && !flag4)
                        {
                            color1 = Color.FromArgb(192, 48, 48);
                            color2 = Color.FromArgb((int)byte.MaxValue, 24, 24);
                            str3 = TextResolver.GetText("Mining location in another empire's system");
                        }
                        else if (bindingForColonization && this._Galaxy.CheckColonizationLikeliness(habitatPrioritization.Habitat, this._Galaxy.PlayerEmpire.DominantRace) <= -5)
                        {
                            color1 = Color.FromArgb(192, 192, 0);
                            color2 = Color.FromArgb((int)byte.MaxValue, (int)byte.MaxValue, 0);
                            str3 = TextResolver.GetText("Colonization unlikely due to hostile population");
                        }
                        else if (bindingForColonization && !flag1 && this._Galaxy.CheckInStorm(habitatPrioritization.Habitat.Xpos, habitatPrioritization.Habitat.Ypos))
                        {
                            color1 = Color.FromArgb(224, 128, 0);
                            color2 = Color.FromArgb((int)byte.MaxValue, 128, 0);
                            str3 = TextResolver.GetText("Galactic storm makes colonization hazardous");
                        }
                        else if (!bindingForColonization && !flag2 && this._Galaxy.CheckInStorm(habitatPrioritization.Habitat.Xpos, habitatPrioritization.Habitat.Ypos))
                        {
                            color1 = Color.FromArgb(224, 128, 0);
                            color2 = Color.FromArgb((int)byte.MaxValue, 128, 0);
                            str3 = TextResolver.GetText("Galactic storm makes construction hazardous");
                        }
                        else if (bindingForColonization && (double)habitatPrioritization.Habitat.Quality < 0.5)
                        {
                            color1 = Color.FromArgb(224, 128, 0);
                            color2 = Color.FromArgb((int)byte.MaxValue, 128, 0);
                            str3 = TextResolver.GetText("Low quality makes colonization undesirable");
                        }
                        else if (this._Galaxy.PlayerEmpire.CheckWhetherHabitatIsDangerous(habitatPrioritization.Habitat))
                        {
                            color1 = Color.FromArgb(192, 192, 0);
                            color2 = Color.FromArgb((int)byte.MaxValue, (int)byte.MaxValue, 0);
                            str3 = TextResolver.GetText("Our last scan of this location showed nearby pirates or space monsters");
                        }
                        if (color1 != Color.Empty)
                        {
                            foreach (DataGridViewCell cell in (BaseCollection)row.Cells)
                            {
                                if (color1 != Color.FromArgb(170, 170, 170))
                                {
                                    cell.Style.ForeColor = color1;
                                    cell.Style.SelectionForeColor = color2;
                                }
                                if (string.IsNullOrEmpty(cell.ToolTipText))
                                    cell.ToolTipText = str3;
                            }
                        }
                    }
                }
            }
            this._Grid.DefaultCellStyle.SelectionForeColor = Color.FromArgb(170, 170, 170);
            this._Grid.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.FromArgb(170, 170, 170);
            this.RememberSorting();
            this._Grid.AutoSizeColumnsMode = autoSizeColumnsMode;
            this._Grid.AutoSizeRowsMode = autoSizeRowsMode;
            this._Grid.ResumeLayout();
        }

        public event HabitatPrioritizationListView.CellContentClickDelegate CellContentClick;

        private void _Grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || !(this._Grid.Columns[e.ColumnIndex].Name == "ShipAssigned"))
                return;
            int tag = (int)this._Grid.Rows[e.RowIndex].Cells[1].Tag;
            if (tag >= this._HabitatPrioritizations.Count || this._HabitatPrioritizations[tag].AssignedShip == null)
                return;
            int builtObjectId = this._HabitatPrioritizations[tag].AssignedShip.BuiltObjectID;
            if (this.CellContentClick == null)
                return;
            this.CellContentClick(sender, builtObjectId);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = (IContainer)new System.ComponentModel.Container();
            this.AutoScaleMode = AutoScaleMode.Font;
            this._Grid.CellContentClick += new DataGridViewCellEventHandler(this._Grid_CellContentClick);
        }

        private string GetResourceCellRarity(HabitatResourceList resource)
        {
            if (resource.HasSuperLuxuryResources())
                return "VR";
            else if (resource.HasLuxuryResources())
                return "R";
            else
                return "C";
        }

        private class SortableImageColumn : DataGridViewImageColumn
        {
            public SortableImageColumn()
            {
                this.CellTemplate = (DataGridViewCell)new HabitatPrioritizationListView.SortableImageCell();
                this.ValueType = typeof(string);
            }
        }

        private class SortableImageCell : DataGridViewImageCell
        {
            private Bitmap _Bitmap;

            public SortableImageCell() => this.ValueType = typeof(string);

            public Bitmap ScaledImage
            {
                get => this._Bitmap;
                set => this._Bitmap = value;
            }

            protected override object GetFormattedValue(
              object value,
              int rowIndex,
              ref DataGridViewCellStyle cellStyle,
              TypeConverter valueTypeConverter,
              TypeConverter formattedValueTypeConverter,
              DataGridViewDataErrorContexts context)
            {
                return (object)this.ScaledImage;
            }

            public override object DefaultNewRowValue => (object)string.Empty;
        }

        private class SortableImageNumberColumn : DataGridViewImageColumn
        {
            public SortableImageNumberColumn()
            {
                this.CellTemplate = (DataGridViewCell)new HabitatPrioritizationListView.SortableImageNumberCell();
                this.ValueType = typeof(int);
            }
        }

        private class SortableImageNumberCell : DataGridViewImageCell
        {
            private Bitmap _Bitmap;

            public SortableImageNumberCell() => this.ValueType = typeof(int);

            public Bitmap ScaledImage
            {
                get => this._Bitmap;
                set => this._Bitmap = value;
            }

            protected override object GetFormattedValue(
              object value,
              int rowIndex,
              ref DataGridViewCellStyle cellStyle,
              TypeConverter valueTypeConverter,
              TypeConverter formattedValueTypeConverter,
              DataGridViewDataErrorContexts context)
            {
                return (object)this.ScaledImage;
            }

            public override object DefaultNewRowValue => (object)0;
        }

        public delegate void CellContentClickDelegate(object sender, int builtObjectId);
    }
}
