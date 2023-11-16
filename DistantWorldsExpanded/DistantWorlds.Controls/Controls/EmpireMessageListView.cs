// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.EmpireMessageListView
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using BaconDistantWorlds;
using DistantWorlds.Types;
//using DistantWorlds.Controls.Mods;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class EmpireMessageListView : ListViewBase
    {
        private IContainer components;
        private EmpireMessageList _EmpireMessages;
        public Bitmap[] _MessageImages;

        //public static event EventHandler<BindDataMessageImagesModsArgs> BindDataMessageImagesMods;

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
        }

        public EmpireMessageListView()
        {
            this.InitializeComponent();
            DataGridViewImageColumn gridViewImageColumn = new DataGridViewImageColumn();
            gridViewImageColumn.Description = "Icon";
            gridViewImageColumn.HeaderText = "";
            gridViewImageColumn.Name = "Icon";
            gridViewImageColumn.ReadOnly = true;
            gridViewImageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            gridViewImageColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            gridViewImageColumn.ValueType = typeof(Image);
            gridViewImageColumn.Width = 40;
            gridViewImageColumn.FillWeight = 40f;
            this._Grid.Columns.Add((DataGridViewColumn)gridViewImageColumn);
            DataGridViewTextBoxColumn viewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn1.HeaderText = TextResolver.GetText("Subject");
            viewTextBoxColumn1.Name = "Title";
            viewTextBoxColumn1.ReadOnly = true;
            viewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn1.ValueType = typeof(string);
            viewTextBoxColumn1.Width = 200;
            viewTextBoxColumn1.FillWeight = 200f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn1);
            DataGridViewTextBoxColumn viewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn2.HeaderText = TextResolver.GetText("Star Date");
            viewTextBoxColumn2.Name = "StarDate";
            viewTextBoxColumn2.ReadOnly = true;
            viewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn2.ValueType = typeof(string);
            viewTextBoxColumn2.Width = 60;
            viewTextBoxColumn2.FillWeight = 60f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn2);
        }

        public EmpireMessage SelectedMessage
        {
            get
            {
                DataGridViewSelectedRowCollection selectedRows = this._Grid.SelectedRows;
                if (selectedRows.Count != 1)
                    return (EmpireMessage)null;
                //index = -1;
                object tag = selectedRows[0].Cells[1].Tag;
                if (tag is int index)
                { return this._EmpireMessages[index]; }
                else { return (EmpireMessage)null; }
            }
        }

        public void ClearData() => this._EmpireMessages = (EmpireMessageList)null;

        public void BindData(
          EmpireMessageList empireMessages,
          Bitmap[] messageImages,
          Bitmap[] facilityImages,
          CharacterImageCache characterImageCache,
          Bitmap attackTargetImage)
        {
            this._EmpireMessages = empireMessages;
            this._MessageImages = new Bitmap[messageImages.Length];
            for (int index = 0; index < messageImages.Length; ++index)
            {
                double num = (double)messageImages[index].Width / (double)messageImages[index].Height;
                int height = 25;
                int width = (int)(num * (double)height);
                this._MessageImages[index] = this.PrescaleImage(messageImages[index], width, height);
            }
            this._Grid.Rows.Clear();
            if (empireMessages != null)
            {
                for (int index = 0; index < empireMessages.Count; ++index)
                {
                    this._Grid.Rows.Add();
                    DataGridViewRow row = this._Grid.Rows[index];
                    Bitmap original = (Bitmap)null;
                    switch (empireMessages[index].MessageType)
                    {
                        case EmpireMessageType.DiplomaticRelationChange:
                        case EmpireMessageType.ProposeDiplomaticRelation:
                        case EmpireMessageType.AcceptDiplomaticRelation:
                        case EmpireMessageType.RefuseDiplomaticRelation:
                        case EmpireMessageType.CancelPirateProtection:
                            original = !(empireMessages[index].Subject is Empire) ? this.PrescaleImage(empireMessages[index].Sender.LargeFlagPicture, 30, 18) : this.PrescaleImage(((Empire)empireMessages[index].Subject).LargeFlagPicture, 30, 18);
                            break;
                        case EmpireMessageType.RemoveColoniesFromSystem:
                        case EmpireMessageType.StopMissionsAgainstUs:
                        case EmpireMessageType.StopAttacks:
                        case EmpireMessageType.LeaveSystem:
                            original = this._MessageImages[15];
                            break;
                        case EmpireMessageType.RequestJointWar:
                        case EmpireMessageType.RequestJointTradeSanctions:
                        case EmpireMessageType.RequestStopWar:
                        case EmpireMessageType.RequestLiftTradeSanctions:
                        case EmpireMessageType.RequestHonorMutualDefense:
                            original = this._MessageImages[17];
                            break;
                        case EmpireMessageType.GiveGift:
                            original = this._MessageImages[14];
                            break;
                        case EmpireMessageType.Informational:
                            original = this._MessageImages[26];
                            break;
                        case EmpireMessageType.ShipBaseCompleted:
                        case EmpireMessageType.ShipBaseScrapped:
                            original = this._MessageImages[16];
                            break;
                        case EmpireMessageType.NewColony:
                        case EmpireMessageType.ColonyGained:
                            original = this._MessageImages[1];
                            break;
                        case EmpireMessageType.NewColonyFailed:
                        case EmpireMessageType.ColonyLost:
                        case EmpireMessageType.ColonyDefended:
                        case EmpireMessageType.ColonyRebelling:
                        case EmpireMessageType.Revolution:
                        case EmpireMessageType.ColonyShipMissionCancelled:
                            original = this._MessageImages[2];
                            break;
                        case EmpireMessageType.ResearchBreakthrough:
                        case EmpireMessageType.ResearchCriticalBreakthrough:
                        case EmpireMessageType.ResearchCriticalFailure:
                            original = this._MessageImages[8];
                            break;
                        case EmpireMessageType.BattleUnderAttack:
                        case EmpireMessageType.BattleAttacking:
                        case EmpireMessageType.IncomingEnemyFleet:
                            original = this._MessageImages[0];
                            break;
                        case EmpireMessageType.CharacterAppearance:
                        case EmpireMessageType.CharacterSkillTraitChange:
                            if (empireMessages[index].Subject is Character)
                            {
                                Character subject = (Character)empireMessages[index].Subject;
                                original = characterImageCache.ObtainCharacterImageSmall(subject);
                                break;
                            }
                            original = this._MessageImages[18];
                            break;
                        case EmpireMessageType.CharacterDeath:
                            if (empireMessages[index].Subject is Character)
                            {
                                Character subject = (Character)empireMessages[index].Subject;
                                original = characterImageCache.ObtainCharacterImageSmall(subject);
                                break;
                            }
                            original = this._MessageImages[19];
                            break;
                        case EmpireMessageType.CharacterMissionAccomplished:
                            original = this._MessageImages[18];
                            break;
                        case EmpireMessageType.CharacterMissionFailure:
                            original = this._MessageImages[23];
                            break;
                        case EmpireMessageType.EmpireDiscovered:
                        case EmpireMessageType.EmpireDefeated:
                            original = !(empireMessages[index].Subject is Empire) ? this._MessageImages[13] : this.PrescaleImage(((Empire)empireMessages[index].Subject).LargeFlagPicture, 30, 18);
                            break;
                        case EmpireMessageType.BlockadeInitiated:
                            original = this._MessageImages[20];
                            break;
                        case EmpireMessageType.BlockadeCancelled:
                            original = this._MessageImages[21];
                            break;
                        case EmpireMessageType.ExplorationRuins:
                        case EmpireMessageType.ExplorationBuiltObject:
                        case EmpireMessageType.ExplorationHabitat:
                        case EmpireMessageType.ExplorationLocation:
                            original = this._MessageImages[24];
                            break;
                        case EmpireMessageType.GalacticHistory:
                            original = this._MessageImages[25];
                            break;
                        case EmpireMessageType.SellInfoUnmetEmpire:
                        case EmpireMessageType.SellInfoIndependentColony:
                        case EmpireMessageType.SellInfoSystemMap:
                        case EmpireMessageType.SellInfoRuins:
                        case EmpireMessageType.SellInfoDebrisField:
                        case EmpireMessageType.SellInfoRestrictedArea:
                        case EmpireMessageType.SellInfoPlanetDestroyer:
                        case EmpireMessageType.PirateOfferProtection:
                            original = this._MessageImages[27];
                            if (empireMessages[index].Sender != null)
                            {
                                original = this.PrescaleImage(empireMessages[index].Sender.LargeFlagPicture, 30, 18);
                                break;
                            }
                            break;
                        case EmpireMessageType.RestrictedResourceDiscovered:
                            original = this._MessageImages[24];
                            break;
                        case EmpireMessageType.RestrictedResourceTradingAllowed:
                        case EmpireMessageType.RestrictedResourceTradingBlocked:
                        case EmpireMessageType.MilitaryRefuelingAllowed:
                        case EmpireMessageType.MilitaryRefuelingBlocked:
                        case EmpireMessageType.MiningRightsAllowed:
                        case EmpireMessageType.MiningRightsBlocked:
                            original = this.PrescaleImage(empireMessages[index].Sender.LargeFlagPicture, 30, 18);
                            break;
                        case EmpireMessageType.OfferTrade:
                            original = this.PrescaleImage(empireMessages[index].Sender.LargeFlagPicture, 30, 18);
                            break;
                        case EmpireMessageType.ShipMissionComplete:
                        case EmpireMessageType.ShipNeedsRefuelling:
                        case EmpireMessageType.ShipNeedsRepair:
                            original = this._MessageImages[26];
                            break;
                        case EmpireMessageType.RemoveForcesFromSystem:
                        case EmpireMessageType.GeneralWarning:
                            original = this.PrescaleImage(empireMessages[index].Sender.LargeFlagPicture, 30, 18);
                            break;
                        case EmpireMessageType.GeneralBadEvent:
                        case EmpireMessageType.GeneralGoodEvent:
                            original = this.PrescaleImage(empireMessages[index].Sender.LargeFlagPicture, 30, 18);
                            break;
                        case EmpireMessageType.GeneralNeutralEvent:
                        case EmpireMessageType.GeneralDecision:
                            original = this._MessageImages[26];
                            break;
                        case EmpireMessageType.HistoryOfferLocationHint:
                        case EmpireMessageType.HistoryOfferStoryClue:
                        case EmpireMessageType.StoryMessage:
                            original = this._MessageImages[25];
                            break;
                        case EmpireMessageType.ColonyFacilityCompleted:
                        case EmpireMessageType.ColonyFacilityCancelled:
                        case EmpireMessageType.ColonyWonderBegun:
                        case EmpireMessageType.PlanetaryFacilityDestroyed:
                        case EmpireMessageType.PlanetaryFacilityDamaged:
                            if (empireMessages[index].Subject is Habitat)
                            {
                                Habitat subject = (Habitat)empireMessages[index].Subject;
                                if (subject.Facilities != null && subject.Facilities.Count > 0)
                                {
                                    original = facilityImages[(int)subject.Facilities[subject.Facilities.Count - 1].PictureRef];
                                    break;
                                }
                                break;
                            }
                            if (empireMessages[index].Subject is PlanetaryFacilityDefinition)
                            {
                                PlanetaryFacilityDefinition subject = (PlanetaryFacilityDefinition)empireMessages[index].Subject;
                                if (subject != null)
                                {
                                    original = facilityImages[(int)subject.PictureRef];
                                    break;
                                }
                                break;
                            }
                            if (empireMessages[index].Subject is PlanetaryFacility)
                            {
                                PlanetaryFacility subject = (PlanetaryFacility)empireMessages[index].Subject;
                                if (subject != null)
                                {
                                    original = facilityImages[(int)subject.PictureRef];
                                    break;
                                }
                                break;
                            }
                            original = this._MessageImages[16];
                            break;
                        case EmpireMessageType.AdvisorSuggestion:
                            original = this._MessageImages[16];
                            break;
                        case EmpireMessageType.ColonyDestroyed:
                            original = this._MessageImages[28];
                            break;
                        case EmpireMessageType.GalacticNewsNet:
                            original = this._MessageImages[29];
                            break;
                        case EmpireMessageType.ShipBaseBoardedCaptured:
                        case EmpireMessageType.ShipBaseBoardedLost:
                        case EmpireMessageType.PirateSmugglerDetected:
                            original = this._MessageImages[0];
                            break;
                        case EmpireMessageType.PirateAttackMissionAvailable:
                        case EmpireMessageType.PirateAttackMissionCompleted:
                        case EmpireMessageType.PirateDefendMissionAvailable:
                        case EmpireMessageType.PirateDefendMissionCompleted:
                        case EmpireMessageType.PirateSmugglingMissionAvailable:
                        case EmpireMessageType.PirateSmugglingMissionCompleted:
                            original = this._MessageImages[14];
                            break;
                        case EmpireMessageType.PirateAttackMissionFailed:
                        case EmpireMessageType.PirateDefendMissionFailed:
                            original = this._MessageImages[27];
                            break;
                        case EmpireMessageType.ConstructionResourceShortage:
                            original = this._MessageImages[30];
                            break;
                        case EmpireMessageType.RaidBonuses:
                        case EmpireMessageType.RaidVictim:
                            original = this._MessageImages[27];
                            break;
                    }
                    row.Cells[0].Value = (object)BaconEmpireMessageListView.BindDataMessageImages(this, empireMessages, index, original);
                    //row.Cells[0].Value = (object)EmpireMessageListView.OnBindDataMessageImagesMods(this, empireMessages, index, original);
                    row.Cells[1].Value = (object)empireMessages[index].Title;
                    row.Cells[1].Tag = (object)index;
                    row.Cells[2].Value = (object)Galaxy.ResolveStarDateDescription(empireMessages[index].StarDate);
                }
            }
            this.RememberSorting();
        }

        private class SortableImageColumn : DataGridViewImageColumn
        {
            public SortableImageColumn()
            {
                this.CellTemplate = (DataGridViewCell)new EmpireMessageListView.SortableImageCell();
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

        //private static Bitmap OnBindDataMessageImagesMods(EmpireMessageListView emlv, EmpireMessageList empireMessages, int indexer, Bitmap original)
        //{
        //    var tmp = EmpireMessageListView.BindDataMessageImagesMods;
        //    Bitmap res = null;
        //    if (tmp != null)
        //    {
        //        var args = new BindDataMessageImagesModsArgs(emlv, empireMessages, indexer, original);
        //        tmp(null, args);
        //        res = args.Result;
        //    }
        //    return res;
        //}
    }
}
