// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.MessagePopup
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class MessagePopup : GradientPanel
  {
    private EmpireMessage _EmpireMessage;
    private Empire _PlayerEmpire;
    private Bitmap[] _MessageImages;
    private Bitmap[] _LandscapeImages;
    private RaceImageCache _RaceImageCache;
    private Bitmap[] _ComponentImages;
    private Bitmap[] _ResourceImages;
    private Bitmap[] _RuinImages;
    private Bitmap[] _BuiltObjectImages;
    private Bitmap[] _FighterImages;
    private Bitmap[] _FacilityImages;
    private Bitmap[] _HabitatTypeImages;
    private Bitmap[] _TroopImagesInfantry;
    private Bitmap[] _TroopImagesArmored;
    private Bitmap[] _TroopImagesArtillery;
    private Bitmap[] _TroopImagesSpecialForces;
    private Bitmap[] _TroopImagesPirateRaider;
    private Bitmap _ConstructionImage;
    private Bitmap _RefuelImage;
    private Bitmap _MiningImage;
    private Bitmap _AttackTargetImage;
    private Bitmap _MainImage;
    private Bitmap _FlagImage;
    private string _MessageDescription;
    private string _Message = string.Empty;
    private int _Padding = 12;

    public MessagePopup()
    {
      this.SetFont(FontSize.Large);
      this.BorderColor = Color.FromArgb(0, 0, (int) byte.MaxValue);
      this.BorderStyle = BorderStyle.FixedSingle;
      this.BorderWidth = 3;
      this.CurveMode = CornerCurveMode.None;
      this.Curvature = 20;
      this.GradientMode = LinearGradientMode.Vertical;
      this.BackColor = Color.FromArgb(8, 8, 32);
      this.BackColor2 = Color.Navy;
      this.Padding = new Padding(12);
    }

    public void SetPosition(int x, int y) => this.Location = new Point(x, y);

    public void ClearData()
    {
      this._EmpireMessage = (EmpireMessage) null;
      this._PlayerEmpire = (Empire) null;
    }

    public void Ignite(
      Galaxy galaxy,
      CharacterImageCache characterImageCache,
      BuiltObjectImageCache builtObjectImageCache,
      HabitatImageCache habitatImageCache,
      Bitmap[] messageImages,
      Bitmap[] landscapeImages,
      RaceImageCache raceImageCache,
      Bitmap[] componentImages,
      Bitmap[] resourceImages,
      Bitmap[] ruinImages,
      Bitmap[] builtObjectImages,
      Bitmap[] fighterImages,
      Bitmap[] facilityImages,
      Bitmap[] habitatTypeImages,
      Bitmap constructionImage,
      Empire playerEmpire,
      EmpireMessage empireMessage,
      Bitmap refuelImage,
      Bitmap miningImage,
      Bitmap attackTargetImage,
      Bitmap[] troopImagesInfantry,
      Bitmap[] troopImagesArmored,
      Bitmap[] troopImagesArtillery,
      Bitmap[] troopImagesSpecialForces,
      Bitmap[] troopImagesPirateRaider)
    {
      this._EmpireMessage = empireMessage;
      this._PlayerEmpire = playerEmpire;
      this._MessageImages = messageImages;
      this._LandscapeImages = landscapeImages;
      this._RaceImageCache = raceImageCache;
      this._ComponentImages = componentImages;
      this._ResourceImages = resourceImages;
      this._RuinImages = ruinImages;
      this._BuiltObjectImages = builtObjectImages;
      this._FighterImages = fighterImages;
      this._FacilityImages = facilityImages;
      this._HabitatTypeImages = habitatTypeImages;
      this._ConstructionImage = constructionImage;
      this._AttackTargetImage = attackTargetImage;
      this._TroopImagesInfantry = troopImagesInfantry;
      this._TroopImagesArmored = troopImagesArmored;
      this._TroopImagesArtillery = troopImagesArtillery;
      this._TroopImagesSpecialForces = troopImagesSpecialForces;
      this._TroopImagesPirateRaider = troopImagesPirateRaider;
      double num1 = (double) refuelImage.Width / (double) refuelImage.Height;
      if (this._RefuelImage == null || this._RefuelImage.PixelFormat == PixelFormat.Undefined)
        this._RefuelImage = this.ScaleImage(refuelImage, (int) (70.0 * num1), 70, true);
      double num2 = (double) miningImage.Width / (double) miningImage.Height;
      if (this._MiningImage == null || this._MiningImage.PixelFormat == PixelFormat.Undefined)
        this._MiningImage = this.ScaleImage(miningImage, (int) (70.0 * num2), 70, true);
      this.SetUpMessageDisplay(galaxy, characterImageCache, builtObjectImageCache, habitatImageCache);
    }

    private void SetUpMessageDisplay(
      Galaxy galaxy,
      CharacterImageCache characterImageCache,
      BuiltObjectImageCache builtObjectImageCache,
      HabitatImageCache habitatImageCache)
    {
      if (this._MainImage != null && this._MainImage.PixelFormat != PixelFormat.Undefined)
        this._MainImage.Dispose();
      if (this._FlagImage != null && this._FlagImage.PixelFormat != PixelFormat.Undefined)
        this._FlagImage.Dispose();
      this._MainImage = (Bitmap) null;
      this._FlagImage = (Bitmap) null;
      this._MessageDescription = string.Empty;
      if (this._EmpireMessage != null)
      {
        DiplomaticRelationType diplomaticRelationType = DiplomaticRelationType.NotMet;
        if (this._EmpireMessage.Subject is DiplomaticRelationType)
          diplomaticRelationType = (DiplomaticRelationType) this._EmpireMessage.Subject;
        if (this._EmpireMessage.Sender != this._PlayerEmpire)
        {
          this._FlagImage = this.FadeImage(this._EmpireMessage.Sender.LargeFlagPicture, 0.5f);
          if (this._EmpireMessage.MessageType != EmpireMessageType.GalacticNewsNet)
            this._MessageDescription = this._EmpireMessage.Sender.Name + ": ";
        }
        BuiltObject builtObject = (BuiltObject) null;
        Bitmap image1 = (Bitmap) null;
        if (this._EmpireMessage.Subject is BuiltObject)
        {
          builtObject = (BuiltObject) this._EmpireMessage.Subject;
          if (builtObject != null)
          {
            image1 = builtObjectImageCache.ObtainImage(builtObject);
            if (builtObject.Empire != null)
              image1 = image1.Width >= 90 ? this.PrepareBuiltObjectImage(builtObject, image1, builtObject.Empire.MainColor, builtObject.Empire.SecondaryColor, 1.0, 1, true) : this.PrepareBuiltObjectImage(builtObject, image1, builtObject.Empire.MainColor, builtObject.Empire.SecondaryColor, 1.0, 3, true);
          }
        }
        this._MessageDescription += this._EmpireMessage.Description;
        switch (this._EmpireMessage.MessageType)
        {
          case EmpireMessageType.DiplomaticRelationChange:
          case EmpireMessageType.AcceptDiplomaticRelation:
            DiplomaticRelation diplomaticRelation1 = this._PlayerEmpire.DiplomaticRelations[this._EmpireMessage.Sender];
            switch (diplomaticRelationType)
            {
              case DiplomaticRelationType.None:
                if (diplomaticRelation1 != null)
                {
                  switch (diplomaticRelation1.Type)
                  {
                    case DiplomaticRelationType.TradeSanctions:
                      this._MainImage = this._MessageImages[9];
                      break;
                    case DiplomaticRelationType.War:
                      this._MainImage = this._MessageImages[4];
                      break;
                    default:
                      this._MainImage = this._MessageImages[12];
                      break;
                  }
                }
                else
                {
                  this._MainImage = this._MessageImages[12];
                  break;
                }
                break;
              case DiplomaticRelationType.FreeTradeAgreement:
                this._MainImage = this._MessageImages[5];
                break;
              case DiplomaticRelationType.MutualDefensePact:
                this._MainImage = this._MessageImages[6];
                break;
              case DiplomaticRelationType.SubjugatedDominion:
                this._MainImage = this._MessageImages[10];
                break;
              case DiplomaticRelationType.Protectorate:
                this._MainImage = this._MessageImages[7];
                break;
              case DiplomaticRelationType.TradeSanctions:
                this._MainImage = this._MessageImages[11];
                break;
              case DiplomaticRelationType.War:
                this._MainImage = this._MessageImages[3];
                break;
            }
            if (this._MainImage != null)
            {
              this._MainImage = this.ImprintFlag(this._MainImage, this._EmpireMessage.Sender);
              break;
            }
            break;
          case EmpireMessageType.ProposeDiplomaticRelation:
            DiplomaticRelation diplomaticRelation2 = this._PlayerEmpire.DiplomaticRelations[this._EmpireMessage.Sender];
            switch (diplomaticRelationType)
            {
              case DiplomaticRelationType.None:
                if (diplomaticRelation2 != null)
                {
                  switch (diplomaticRelation2.Type)
                  {
                    case DiplomaticRelationType.TradeSanctions:
                      this._MainImage = this._MessageImages[9];
                      break;
                    case DiplomaticRelationType.War:
                      this._MainImage = this._MessageImages[4];
                      break;
                    default:
                      this._MainImage = !this._EmpireMessage.Description.ToLower(CultureInfo.InvariantCulture).Contains("trade") ? this._MessageImages[12] : this._MessageImages[9];
                      break;
                  }
                }
                else
                {
                  this._MainImage = this._MessageImages[12];
                  break;
                }
                break;
              case DiplomaticRelationType.FreeTradeAgreement:
                this._MainImage = this._MessageImages[5];
                break;
              case DiplomaticRelationType.MutualDefensePact:
                this._MainImage = this._MessageImages[6];
                break;
              case DiplomaticRelationType.SubjugatedDominion:
                this._MainImage = this._MessageImages[10];
                break;
              case DiplomaticRelationType.Protectorate:
                this._MainImage = this._MessageImages[7];
                break;
              case DiplomaticRelationType.TradeSanctions:
                this._MainImage = this._MessageImages[11];
                break;
              case DiplomaticRelationType.War:
                this._MainImage = this._MessageImages[3];
                break;
            }
            if (this._MainImage != null)
            {
              this._MainImage = this.ImprintFlag(this._MainImage, this._EmpireMessage.Sender);
              break;
            }
            break;
          case EmpireMessageType.RefuseDiplomaticRelation:
            this._MainImage = this._MessageImages[13];
            break;
          case EmpireMessageType.RemoveColoniesFromSystem:
          case EmpireMessageType.StopMissionsAgainstUs:
          case EmpireMessageType.StopAttacks:
          case EmpireMessageType.LeaveSystem:
          case EmpireMessageType.RemoveForcesFromSystem:
            this._MainImage = this._MessageImages[15];
            this._MainImage = this.ImprintFlag(this._MainImage, this._EmpireMessage.Sender);
            break;
          case EmpireMessageType.RequestJointWar:
          case EmpireMessageType.RequestJointTradeSanctions:
          case EmpireMessageType.RequestStopWar:
          case EmpireMessageType.RequestLiftTradeSanctions:
            this._MainImage = this._MessageImages[17];
            this._MainImage = this.ImprintFlag(this._MainImage, this._EmpireMessage.Sender);
            break;
          case EmpireMessageType.GiveGift:
            this._MainImage = this._MessageImages[14];
            break;
          case EmpireMessageType.ShipBaseCompleted:
            this._MainImage = this._MessageImages[16];
            break;
          case EmpireMessageType.NewColony:
            if (this._EmpireMessage.Subject is Habitat)
            {
              Habitat subject = (Habitat) this._EmpireMessage.Subject;
              if (subject != null && subject.LandscapePictureRef >= (short) 0)
              {
                this._MainImage = this._LandscapeImages[(int) subject.LandscapePictureRef];
                break;
              }
              break;
            }
            break;
          case EmpireMessageType.NewColonyFailed:
            if (this._EmpireMessage.Subject is Habitat)
            {
              Habitat subject = (Habitat) this._EmpireMessage.Subject;
              if (subject != null && subject.LandscapePictureRef >= (short) 0)
                this._MainImage = this._LandscapeImages[(int) subject.LandscapePictureRef];
            }
            this._MainImage = this.StripeBitmap(this._MainImage);
            break;
          case EmpireMessageType.ResearchBreakthrough:
          case EmpireMessageType.ResearchCriticalBreakthrough:
          case EmpireMessageType.ResearchCriticalFailure:
            if (this._EmpireMessage.Subject is ResearchNode)
            {
              ResearchNode subject = (ResearchNode) this._EmpireMessage.Subject;
              if (subject.PlanetaryFacility != null)
              {
                this._MainImage = this._FacilityImages[(int) subject.PlanetaryFacility.PictureRef];
                break;
              }
              if (subject.Components != null && subject.Components.Count > 0)
              {
                Bitmap bitmap = new Bitmap(170, 134, PixelFormat.Format32bppPArgb);
                using (Graphics graphics = Graphics.FromImage((Image) bitmap))
                {
                  graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                  Rectangle rect = new Rectangle(0, 0, 79, 134);
                  graphics.DrawImage((Image) this._MessageImages[8], rect);
                  int width = (int) ((double) this._ComponentImages[subject.Components[0].PictureRef].Width * 2.5);
                  int height = (int) ((double) this._ComponentImages[subject.Components[0].PictureRef].Height * 2.5);
                  rect = new Rectangle(170 - width, (134 - height) / 2, width, height);
                  graphics.DrawImage((Image) this._ComponentImages[subject.Components[0].PictureRef], rect);
                }
                this._MainImage = bitmap;
                break;
              }
              if (subject.ComponentImprovements != null && subject.ComponentImprovements.Count > 0)
              {
                Bitmap bitmap = new Bitmap(170, 134, PixelFormat.Format32bppPArgb);
                using (Graphics graphics = Graphics.FromImage((Image) bitmap))
                {
                  graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                  Rectangle rect = new Rectangle(0, 0, 79, 134);
                  graphics.DrawImage((Image) this._MessageImages[8], rect);
                  int width = (int) ((double) this._ComponentImages[subject.ComponentImprovements[0].ImprovedComponent.PictureRef].Width * 2.5);
                  int height = (int) ((double) this._ComponentImages[subject.ComponentImprovements[0].ImprovedComponent.PictureRef].Height * 2.5);
                  rect = new Rectangle(170 - width, (134 - height) / 2, width, height);
                  graphics.DrawImage((Image) this._ComponentImages[subject.ComponentImprovements[0].ImprovedComponent.PictureRef], rect);
                }
                this._MainImage = bitmap;
                break;
              }
              if (subject.Abilities != null && subject.Abilities.Count > 0)
              {
                if (subject.Abilities[0].Type == ResearchAbilityType.EnableShipSubRole)
                {
                  if (subject.Abilities[0].RelatedObject != null && subject.Abilities[0].RelatedObject is BuiltObjectSubRole)
                  {
                    int pictureRef = ShipImageHelper.ResolveNewShipImageIndex((BuiltObjectSubRole) subject.Abilities[0].RelatedObject, this._PlayerEmpire.DominantRace, this._PlayerEmpire.PirateEmpireBaseHabitat != null);
                    Bitmap image2 = builtObjectImageCache.ObtainImage(pictureRef);
                    if (image2 != null && image2.PixelFormat != PixelFormat.Undefined)
                    {
                      this._MainImage = new Bitmap((Image) image2);
                      this._MainImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
                      break;
                    }
                    break;
                  }
                  this._MainImage = this._ConstructionImage;
                  break;
                }
                if (subject.Abilities[0].Type == ResearchAbilityType.Boarding)
                {
                  this._MainImage = new Bitmap((Image) this._ComponentImages[115]);
                  break;
                }
                if (subject.Abilities[0].Type == ResearchAbilityType.Troop)
                {
                  if (subject.Abilities[0].RelatedObject != null && subject.Abilities[0].RelatedObject is TroopType)
                  {
                    TroopType relatedObject = (TroopType) subject.Abilities[0].RelatedObject;
                    this._MainImage = this._TroopImagesInfantry[this._PlayerEmpire.DominantRace.PictureRef];
                    switch (relatedObject)
                    {
                      case TroopType.Infantry:
                        this._MainImage = this._TroopImagesInfantry[this._PlayerEmpire.DominantRace.PictureRef];
                        break;
                      case TroopType.Armored:
                        this._MainImage = this._TroopImagesArmored[this._PlayerEmpire.DominantRace.PictureRef];
                        break;
                      case TroopType.Artillery:
                        this._MainImage = this._TroopImagesArtillery[this._PlayerEmpire.DominantRace.PictureRef];
                        break;
                      case TroopType.SpecialForces:
                        this._MainImage = this._TroopImagesSpecialForces[this._PlayerEmpire.DominantRace.PictureRef];
                        break;
                      case TroopType.PirateRaider:
                        this._MainImage = this._TroopImagesPirateRaider[this._PlayerEmpire.DominantRace.PictureRef];
                        break;
                    }
                  }
                  else
                  {
                    this._MainImage = this._TroopImagesInfantry[this._PlayerEmpire.DominantRace.PictureRef];
                    break;
                  }
                }
                else
                {
                  if (subject.Abilities[0].Type == ResearchAbilityType.ConstructionSize)
                  {
                    this._MainImage = this._ConstructionImage;
                    break;
                  }
                  if (subject.Abilities[0].Type == ResearchAbilityType.ColonizeHabitatType || subject.Abilities[0].Type == ResearchAbilityType.PopulationGrowthRate)
                  {
                    HabitatType habitatType = HabitatType.Undefined;
                    switch (subject.Abilities[0].Value)
                    {
                      case 1:
                        habitatType = HabitatType.Continental;
                        break;
                      case 2:
                        habitatType = HabitatType.MarshySwamp;
                        break;
                      case 3:
                        habitatType = HabitatType.Ocean;
                        break;
                      case 4:
                        habitatType = HabitatType.Desert;
                        break;
                      case 5:
                        habitatType = HabitatType.Ice;
                        break;
                      case 6:
                        habitatType = HabitatType.Volcanic;
                        break;
                    }
                    if (habitatType != HabitatType.Undefined)
                    {
                      Bitmap bitmap = new Bitmap(170, 134, PixelFormat.Format32bppPArgb);
                      using (Graphics graphics = Graphics.FromImage((Image) bitmap))
                      {
                        int index = 0;
                        switch (habitatType)
                        {
                          case HabitatType.Volcanic:
                            index = 5;
                            break;
                          case HabitatType.Desert:
                            index = 3;
                            break;
                          case HabitatType.MarshySwamp:
                            index = 1;
                            break;
                          case HabitatType.Continental:
                            index = 0;
                            break;
                          case HabitatType.Ocean:
                            index = 2;
                            break;
                          case HabitatType.Ice:
                            index = 4;
                            break;
                        }
                        Bitmap habitatTypeImage = this._HabitatTypeImages[index];
                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        Rectangle rect = new Rectangle(0, 0, 79, 134);
                        graphics.DrawImage((Image) this._MessageImages[8], rect);
                        int width = (int) ((double) habitatTypeImage.Width * 2.5);
                        int height = (int) ((double) habitatTypeImage.Height * 2.5);
                        rect = new Rectangle(170 - width, (134 - height) / 2, width, height);
                        graphics.DrawImage((Image) habitatTypeImage, rect);
                      }
                      this._MainImage = bitmap;
                      break;
                    }
                    break;
                  }
                  break;
                }
              }
              else
              {
                if (subject.Fighters != null && subject.Fighters.Count > 0)
                {
                  Bitmap bitmap1 = new Bitmap(170, 134, PixelFormat.Format32bppPArgb);
                  using (Graphics graphics = Graphics.FromImage((Image) bitmap1))
                  {
                    Bitmap bitmap2 = new Bitmap((Image) this._FighterImages[subject.Fighters[0].Type != FighterType.Bomber ? ShipImageHelper.ResolveNewFighterImageIndex(this._PlayerEmpire.DominantRace, this._PlayerEmpire.PirateEmpireBaseHabitat != null) : ShipImageHelper.ResolveNewBomberImageIndex(this._PlayerEmpire.DominantRace, this._PlayerEmpire.PirateEmpireBaseHabitat != null)]);
                    bitmap2.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    Rectangle rect = new Rectangle(0, 0, 79, 134);
                    graphics.DrawImage((Image) this._MessageImages[8], rect);
                    double num = Math.Min(2.5, 120.0 / (double) Math.Max(bitmap2.Width, bitmap2.Height));
                    int width = (int) ((double) bitmap2.Width * num);
                    int height = (int) ((double) bitmap2.Height * num);
                    int x = 170 - width;
                    int y = (134 - height) / 2;
                    Rectangle srcRect = new Rectangle(0, 0, bitmap2.Width, bitmap2.Height);
                    Rectangle destRect = new Rectangle(x, y, width, height);
                    graphics.DrawImage((Image) bitmap2, destRect, srcRect, GraphicsUnit.Pixel);
                    bitmap2.Dispose();
                  }
                  this._MainImage = bitmap1;
                  break;
                }
                break;
              }
            }
            else
            {
              if (this._EmpireMessage.Subject is Component)
              {
                Component subject = (Component) this._EmpireMessage.Subject;
                if (subject != null)
                {
                  Bitmap bitmap = new Bitmap(170, 134, PixelFormat.Format32bppPArgb);
                  using (Graphics graphics = Graphics.FromImage((Image) bitmap))
                  {
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    Rectangle rect = new Rectangle(0, 0, 79, 134);
                    graphics.DrawImage((Image) this._MessageImages[8], rect);
                    int width = (int) ((double) this._ComponentImages[subject.PictureRef].Width * 2.5);
                    int height = (int) ((double) this._ComponentImages[subject.PictureRef].Height * 2.5);
                    rect = new Rectangle(170 - width, (134 - height) / 2, width, height);
                    graphics.DrawImage((Image) this._ComponentImages[subject.PictureRef], rect);
                  }
                  this._MainImage = bitmap;
                  break;
                }
                break;
              }
              if (this._EmpireMessage.Subject is HabitatType)
              {
                HabitatType subject = (HabitatType) this._EmpireMessage.Subject;
                if (subject != HabitatType.Undefined)
                {
                  Bitmap bitmap = new Bitmap(170, 134, PixelFormat.Format32bppPArgb);
                  using (Graphics graphics = Graphics.FromImage((Image) bitmap))
                  {
                    int index = 0;
                    switch (subject)
                    {
                      case HabitatType.Volcanic:
                        index = 5;
                        break;
                      case HabitatType.Desert:
                        index = 3;
                        break;
                      case HabitatType.MarshySwamp:
                        index = 1;
                        break;
                      case HabitatType.Continental:
                        index = 0;
                        break;
                      case HabitatType.Ocean:
                        index = 2;
                        break;
                      case HabitatType.Ice:
                        index = 4;
                        break;
                    }
                    Bitmap habitatTypeImage = this._HabitatTypeImages[index];
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    Rectangle rect = new Rectangle(0, 0, 79, 134);
                    graphics.DrawImage((Image) this._MessageImages[8], rect);
                    int width = (int) ((double) habitatTypeImage.Width * 2.5);
                    int height = (int) ((double) habitatTypeImage.Height * 2.5);
                    rect = new Rectangle(170 - width, (134 - height) / 2, width, height);
                    graphics.DrawImage((Image) habitatTypeImage, rect);
                  }
                  this._MainImage = bitmap;
                  break;
                }
                break;
              }
              if (this._EmpireMessage.Subject is PlanetaryFacilityDefinition)
              {
                PlanetaryFacilityDefinition subject = (PlanetaryFacilityDefinition) this._EmpireMessage.Subject;
                if (subject != null)
                {
                  Bitmap bitmap = new Bitmap(170, 134, PixelFormat.Format32bppPArgb);
                  using (Graphics graphics = Graphics.FromImage((Image) bitmap))
                  {
                    Bitmap facilityImage = this._FacilityImages[(int) subject.PictureRef];
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    Rectangle rect = new Rectangle(0, 0, 79, 134);
                    graphics.DrawImage((Image) this._MessageImages[8], rect);
                    double num = Math.Min(2.5, 120.0 / (double) Math.Max(facilityImage.Width, facilityImage.Height));
                    int width = (int) ((double) facilityImage.Width * num);
                    int height = (int) ((double) facilityImage.Height * num);
                    int x = 170 - width;
                    int y = (134 - height) / 2;
                    Rectangle srcRect = new Rectangle(0, 0, facilityImage.Width, facilityImage.Height);
                    Rectangle destRect = new Rectangle(x, y, width, height);
                    graphics.DrawImage((Image) facilityImage, destRect, srcRect, GraphicsUnit.Pixel);
                  }
                  this._MainImage = bitmap;
                  break;
                }
                break;
              }
              if (this._EmpireMessage.Subject is FighterSpecification)
              {
                FighterSpecification subject = (FighterSpecification) this._EmpireMessage.Subject;
                if (subject != null)
                {
                  Bitmap bitmap3 = new Bitmap(170, 134, PixelFormat.Format32bppPArgb);
                  using (Graphics graphics = Graphics.FromImage((Image) bitmap3))
                  {
                    Bitmap bitmap4 = new Bitmap((Image) this._FighterImages[subject.Type != FighterType.Bomber ? ShipImageHelper.ResolveNewFighterImageIndex(this._PlayerEmpire.DominantRace, this._PlayerEmpire.PirateEmpireBaseHabitat != null) : ShipImageHelper.ResolveNewBomberImageIndex(this._PlayerEmpire.DominantRace, this._PlayerEmpire.PirateEmpireBaseHabitat != null)]);
                    bitmap4.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    Rectangle rect = new Rectangle(0, 0, 79, 134);
                    graphics.DrawImage((Image) this._MessageImages[8], rect);
                    double num = Math.Min(2.5, 120.0 / (double) Math.Max(bitmap4.Width, bitmap4.Height));
                    int width = (int) ((double) bitmap4.Width * num);
                    int height = (int) ((double) bitmap4.Height * num);
                    int x = 170 - width;
                    int y = (134 - height) / 2;
                    Rectangle srcRect = new Rectangle(0, 0, bitmap4.Width, bitmap4.Height);
                    Rectangle destRect = new Rectangle(x, y, width, height);
                    graphics.DrawImage((Image) bitmap4, destRect, srcRect, GraphicsUnit.Pixel);
                    bitmap4.Dispose();
                  }
                  this._MainImage = bitmap3;
                  break;
                }
                break;
              }
              break;
            }
            break;
          case EmpireMessageType.BattleUnderAttack:
          case EmpireMessageType.BattleAttacking:
          case EmpireMessageType.IncomingEnemyFleet:
            this._MainImage = this._MessageImages[0];
            break;
          case EmpireMessageType.CharacterAppearance:
          case EmpireMessageType.CharacterSkillTraitChange:
            if (this._EmpireMessage.Subject is Character)
            {
              Character subject = (Character) this._EmpireMessage.Subject;
              this._MainImage = characterImageCache.ObtainCharacterImage(subject);
              break;
            }
            this._MainImage = this._MessageImages[18];
            break;
          case EmpireMessageType.CharacterDeath:
            if (this._EmpireMessage.Subject is Character)
            {
              Character subject = (Character) this._EmpireMessage.Subject;
              this._MainImage = characterImageCache.ObtainCharacterImage(subject);
              break;
            }
            this._MainImage = this._MessageImages[19];
            break;
          case EmpireMessageType.CharacterMissionAccomplished:
            this._MainImage = this._MessageImages[18];
            break;
          case EmpireMessageType.CharacterMissionFailure:
            this._MainImage = this._MessageImages[19];
            break;
          case EmpireMessageType.EmpireDiscovered:
            if (this._EmpireMessage.Subject is Empire)
            {
              Empire subject = (Empire) this._EmpireMessage.Subject;
              if (subject != null)
              {
                Bitmap bitmap = new Bitmap(170, 60, PixelFormat.Format32bppPArgb);
                using (Graphics graphics = Graphics.FromImage((Image) bitmap))
                {
                  graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                  Rectangle rect = new Rectangle(0, 0, 100, 60);
                  graphics.DrawImage((Image) subject.LargeFlagPicture, rect);
                  rect = new Rectangle(110, 0, 60, 60);
                  graphics.DrawImage((Image) this._RaceImageCache.GetEmpireDominantRaceImage(subject), rect);
                }
                this._MainImage = bitmap;
                break;
              }
              break;
            }
            break;
          case EmpireMessageType.ColonyGained:
            this._MainImage = this._MessageImages[1];
            break;
          case EmpireMessageType.ColonyLost:
            this._MainImage = this._MessageImages[2];
            break;
          case EmpireMessageType.ColonyDefended:
          case EmpireMessageType.ColonyRebelling:
          case EmpireMessageType.Revolution:
            this._MainImage = this._MessageImages[2];
            break;
          case EmpireMessageType.EmpireDefeated:
            if (this._EmpireMessage.Subject is Empire)
            {
              Empire subject = (Empire) this._EmpireMessage.Subject;
              if (subject != null)
              {
                Bitmap image3 = new Bitmap(170, 60, PixelFormat.Format32bppPArgb);
                using (Graphics graphics = Graphics.FromImage((Image) image3))
                {
                  graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                  Rectangle rect = new Rectangle(0, 0, 100, 60);
                  graphics.DrawImage((Image) subject.LargeFlagPicture, rect);
                  rect = new Rectangle(110, 0, 60, 60);
                  graphics.DrawImage((Image) this._RaceImageCache.GetEmpireDominantRaceImage(subject), rect);
                }
                Bitmap bitmap5 = image3;
                Bitmap bitmap6 = this.StripeBitmap(image3);
                bitmap5.Dispose();
                this._MainImage = bitmap6;
                break;
              }
              break;
            }
            break;
          case EmpireMessageType.BlockadeInitiated:
            this._MainImage = this._MessageImages[20];
            break;
          case EmpireMessageType.BlockadeCancelled:
            this._MainImage = this._MessageImages[21];
            break;
          case EmpireMessageType.ExplorationRuins:
            if (this._EmpireMessage.Subject is Habitat)
            {
              Habitat subject = (Habitat) this._EmpireMessage.Subject;
              if (subject != null && subject.Ruin != null && subject.Ruin.PictureRef >= 0)
              {
                this._MainImage = this._RuinImages[subject.Ruin.PictureRef];
                break;
              }
              break;
            }
            break;
          case EmpireMessageType.ExplorationBuiltObject:
            if (this._EmpireMessage.Subject is BuiltObject && builtObject != null)
            {
              this._MainImage = image1;
              break;
            }
            break;
          case EmpireMessageType.ExplorationHabitat:
            if (this._EmpireMessage.Subject is Habitat)
            {
              Habitat subject = (Habitat) this._EmpireMessage.Subject;
              if (subject != null && subject.Ruin != null && subject.Ruin.PictureRef >= 0)
                this._MainImage = this._RuinImages[subject.Ruin.PictureRef];
            }
            if (this._MainImage == null)
            {
              this._MainImage = this._MessageImages[24];
              break;
            }
            break;
          case EmpireMessageType.ExplorationLocation:
            this._MainImage = this._MessageImages[24];
            break;
          case EmpireMessageType.GalacticHistory:
            this._MainImage = this._MessageImages[25];
            break;
          case EmpireMessageType.RestrictedResourceDiscovered:
            if (this._EmpireMessage.Subject is Resource)
            {
              Resource subject = (Resource) this._EmpireMessage.Subject;
              if (subject != null)
                this._MainImage = this._ResourceImages[subject.PictureRef];
            }
            else if (this._EmpireMessage.Subject is Habitat)
            {
              Habitat subject = (Habitat) this._EmpireMessage.Subject;
              if (subject != null)
              {
                int index = -1;
                foreach (HabitatResource resource in (SyncList<HabitatResource>) subject.Resources)
                {
                  if (resource.IsRestrictedResource)
                  {
                    index = resource.PictureRef;
                    break;
                  }
                }
                if (index >= 0)
                  this._MainImage = this._ResourceImages[index];
              }
            }
            if (this._MainImage == null)
            {
              this._MainImage = this._MessageImages[24];
              break;
            }
            break;
          case EmpireMessageType.RestrictedResourceTradingAllowed:
          case EmpireMessageType.RestrictedResourceTradingBlocked:
            if (this._EmpireMessage.Subject is Resource)
            {
              Resource subject = (Resource) this._EmpireMessage.Subject;
              if (subject != null)
                this._MainImage = this._ResourceImages[subject.PictureRef];
            }
            else
            {
              ResourceList resourcesEmpireSupplies = this._EmpireMessage.Sender.DetermineResourcesEmpireSupplies();
              for (int index = 0; index < galaxy.ResourceSystem.SuperLuxuryResources.Count; ++index)
              {
                ResourceDefinition superLuxuryResource = galaxy.ResourceSystem.SuperLuxuryResources[index];
                if (superLuxuryResource != null)
                {
                  Resource resource = new Resource(superLuxuryResource.ResourceID);
                  if (resourcesEmpireSupplies.Contains(resource))
                    this._MainImage = this._ResourceImages[resource.PictureRef];
                }
              }
            }
            if (this._MainImage == null)
            {
              this._MainImage = this._MessageImages[24];
              break;
            }
            break;
          case EmpireMessageType.ShipMissionComplete:
          case EmpireMessageType.ShipNeedsRefuelling:
          case EmpireMessageType.ShipNeedsRepair:
          case EmpireMessageType.ShipBaseBoardedCaptured:
          case EmpireMessageType.ShipBaseBoardedLost:
          case EmpireMessageType.PirateAttackMissionCompleted:
          case EmpireMessageType.PirateSmugglerDetected:
          case EmpireMessageType.ShipBaseScrapped:
            if (builtObject != null)
            {
              this._MainImage = image1;
              break;
            }
            break;
          case EmpireMessageType.GeneralWarning:
            this._MainImage = this._MessageImages[15];
            break;
          case EmpireMessageType.GeneralBadEvent:
            if (this._EmpireMessage.Subject is Habitat)
            {
              Habitat subject = (Habitat) this._EmpireMessage.Subject;
              if (subject != null)
              {
                this._MainImage = habitatImageCache.ObtainImage(subject);
                break;
              }
              break;
            }
            this._MainImage = this._EmpireMessage.Sender.LargeFlagPicture;
            break;
          case EmpireMessageType.GeneralNeutralEvent:
            this._MainImage = this._EmpireMessage.Sender.LargeFlagPicture;
            if (this._EmpireMessage.Subject is Race)
            {
              this._MainImage = this._RaceImageCache.GetRaceImage(((Race) this._EmpireMessage.Subject).PictureRef);
              break;
            }
            break;
          case EmpireMessageType.GeneralGoodEvent:
            this._MainImage = this._EmpireMessage.Sender.LargeFlagPicture;
            if (image1 != null)
            {
              this._MainImage = image1;
              break;
            }
            break;
          case EmpireMessageType.GeneralDecision:
            this._MainImage = this._EmpireMessage.Sender.LargeFlagPicture;
            break;
          case EmpireMessageType.HistoryOfferLocationHint:
          case EmpireMessageType.HistoryOfferStoryClue:
          case EmpireMessageType.StoryMessage:
            this._MainImage = this._MessageImages[25];
            break;
          case EmpireMessageType.ColonyFacilityCompleted:
          case EmpireMessageType.ColonyFacilityCancelled:
          case EmpireMessageType.ColonyWonderBegun:
          case EmpireMessageType.PlanetaryFacilityDestroyed:
          case EmpireMessageType.PlanetaryFacilityDamaged:
            if (this._EmpireMessage.Subject is Habitat)
            {
              Habitat subject = (Habitat) this._EmpireMessage.Subject;
              if (subject.Facilities != null && subject.Facilities.Count > 0)
              {
                PlanetaryFacility planetaryFacility = (PlanetaryFacility) null;
                for (int index = subject.Facilities.Count - 1; index >= 0; --index)
                {
                  if ((double) subject.Facilities[index].ConstructionProgress >= 1.0)
                  {
                    planetaryFacility = subject.Facilities[index];
                    break;
                  }
                }
                if (planetaryFacility != null)
                {
                  this._MainImage = this._FacilityImages[(int) planetaryFacility.PictureRef];
                  break;
                }
                break;
              }
              break;
            }
            if (this._EmpireMessage.Subject is PlanetaryFacilityDefinition)
            {
              PlanetaryFacilityDefinition subject = (PlanetaryFacilityDefinition) this._EmpireMessage.Subject;
              if (subject != null)
              {
                this._MainImage = this._FacilityImages[(int) subject.PictureRef];
                if (this._EmpireMessage.MessageType == EmpireMessageType.ColonyFacilityCancelled || this._EmpireMessage.MessageType == EmpireMessageType.PlanetaryFacilityDestroyed)
                {
                  this._MainImage = this.StripeBitmap(this._MainImage);
                  break;
                }
                if (this._EmpireMessage.MessageType != EmpireMessageType.ColonyWonderBegun)
                  break;
                break;
              }
              break;
            }
            if (this._EmpireMessage.Subject is PlanetaryFacility)
            {
              PlanetaryFacility subject = (PlanetaryFacility) this._EmpireMessage.Subject;
              if (subject != null)
              {
                this._MainImage = this._FacilityImages[(int) subject.PictureRef];
                if (this._EmpireMessage.MessageType == EmpireMessageType.ColonyFacilityCancelled || this._EmpireMessage.MessageType == EmpireMessageType.PlanetaryFacilityDestroyed)
                {
                  this._MainImage = this.StripeBitmap(this._MainImage);
                  break;
                }
                if (this._EmpireMessage.MessageType != EmpireMessageType.ColonyWonderBegun)
                  break;
                break;
              }
              break;
            }
            break;
          case EmpireMessageType.ColonyShipMissionCancelled:
            if (this._EmpireMessage.Subject is BuiltObject && builtObject != null)
            {
              this._MainImage = image1;
              break;
            }
            break;
          case EmpireMessageType.AdvisorSuggestion:
            this._MainImage = this._MessageImages[16];
            break;
          case EmpireMessageType.ColonyDestroyed:
            this._MainImage = this._MessageImages[28];
            break;
          case EmpireMessageType.MilitaryRefuelingAllowed:
          case EmpireMessageType.MilitaryRefuelingBlocked:
            this._MainImage = this._RefuelImage;
            break;
          case EmpireMessageType.MiningRightsAllowed:
          case EmpireMessageType.MiningRightsBlocked:
            this._MainImage = this._MiningImage;
            break;
          case EmpireMessageType.GalacticNewsNet:
            this._MainImage = this._MessageImages[29];
            break;
          case EmpireMessageType.PirateAttackMissionAvailable:
          case EmpireMessageType.PirateDefendMissionAvailable:
          case EmpireMessageType.PirateSmugglingMissionAvailable:
            this._MainImage = this._MessageImages[14];
            break;
          case EmpireMessageType.PirateAttackMissionFailed:
          case EmpireMessageType.PirateDefendMissionFailed:
            this._MainImage = this._MessageImages[27];
            break;
          case EmpireMessageType.PirateDefendMissionCompleted:
          case EmpireMessageType.PirateSmugglingMissionCompleted:
            if (builtObject != null)
            {
              this._MainImage = image1;
              break;
            }
            if (this._EmpireMessage.Subject is Habitat)
            {
              Habitat subject = (Habitat) this._EmpireMessage.Subject;
              if (subject != null)
              {
                this._MainImage = habitatImageCache.ObtainImage(subject);
                break;
              }
              break;
            }
            break;
          case EmpireMessageType.ConstructionResourceShortage:
            this._MainImage = this._MessageImages[30];
            break;
          case EmpireMessageType.RaidBonuses:
          case EmpireMessageType.RaidVictim:
            this._MainImage = this._MessageImages[27];
            break;
        }
      }
      if (this._MainImage == null)
        return;
      this._MainImage = this.LimitImageSize(this._MainImage, 240, 180);
    }

    public EmpireMessage Message => this._EmpireMessage;

    private Bitmap FadeImage(Bitmap image, float transparencyLevel)
    {
      Bitmap bitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppPArgb);
      using (Graphics graphics = Graphics.FromImage((Image) bitmap))
      {
        graphics.CompositingQuality = CompositingQuality.HighQuality;
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        Rectangle destRect = new Rectangle(0, 0, image.Width, image.Height);
        ColorMatrix newColorMatrix = new ColorMatrix(new float[5][]
        {
          new float[5]{ 1f, 0.0f, 0.0f, 0.0f, 0.0f },
          new float[5]{ 0.0f, 1f, 0.0f, 0.0f, 0.0f },
          new float[5]{ 0.0f, 0.0f, 1f, 0.0f, 0.0f },
          new float[5]{ 0.0f, 0.0f, 0.0f, transparencyLevel, 0.0f },
          new float[5]{ 0.0f, 0.0f, 0.0f, 0.0f, 1f }
        });
        using (ImageAttributes imageAttrs = new ImageAttributes())
        {
          imageAttrs.SetColorMatrix(newColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
          graphics.DrawImage((Image) image, destRect, 0.0f, 0.0f, (float) image.Width, (float) image.Height, GraphicsUnit.Pixel, imageAttrs);
        }
      }
      return bitmap;
    }

    private Bitmap ImprintFlag(Bitmap image, Empire empire)
    {
      Bitmap bitmap = new Bitmap((Image) image);
      using (Graphics graphics = Graphics.FromImage((Image) bitmap))
      {
        graphics.CompositingQuality = CompositingQuality.HighQuality;
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        Rectangle rect = new Rectangle(65, 30, 100, 60);
        graphics.DrawImage((Image) empire.LargeFlagPicture, rect);
      }
      return bitmap;
    }

    private Bitmap StripeBitmap(Bitmap image)
    {
      Bitmap bitmap = new Bitmap((Image) image);
      using (Graphics graphics = Graphics.FromImage((Image) bitmap))
      {
        graphics.CompositingQuality = CompositingQuality.HighQuality;
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        int num = image.Height / 10;
        SolidBrush solidBrush = new SolidBrush(Color.Black);
        for (int index = 0; index < num; ++index)
        {
          Rectangle rect = new Rectangle(0, index * 10, bitmap.Width, 2);
          graphics.FillRectangle((Brush) solidBrush, rect);
        }
      }
      bitmap.MakeTransparent(Color.Black);
      return bitmap;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      if (this._MainImage == null || this._MainImage.PixelFormat == PixelFormat.Undefined)
        return;
      e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
      int x = (this.ClientRectangle.Width - this._MainImage.Width) / 2;
      SizeF sizeF = e.Graphics.MeasureString(this._MessageDescription, this.Font, this.Width - this._Padding * 2, StringFormat.GenericTypographic);
      int y = Math.Max(0, this.ClientRectangle.Height - (this._MainImage.Height + this._Padding + (int) sizeF.Height)) / 2;
      e.Graphics.DrawImage((Image) this._MainImage, x, y);
      if (this._FlagImage != null)
      {
        Rectangle rect = new Rectangle((this.ClientRectangle.Width - 50) / 2, y + this._MainImage.Height + this._Padding + ((int) sizeF.Height - 30) / 2, 50, 30);
        e.Graphics.DrawImage((Image) this._FlagImage, rect);
      }
      RectangleF layoutRectangle1 = new RectangleF((float) this._Padding, (float) (y + this._MainImage.Height + this._Padding), (float) (this.ClientRectangle.Width - this._Padding * 2), (float) (this.ClientRectangle.Height - this._Padding * 2));
      RectangleF layoutRectangle2 = new RectangleF((float) (this._Padding + 1), (float) (y + this._MainImage.Height + this._Padding + 1), (float) (this.ClientRectangle.Width - this._Padding * 2), (float) (this.ClientRectangle.Height - this._Padding * 2));
      using (SolidBrush solidBrush1 = new SolidBrush(Color.FromArgb(170, 170, 170)))
      {
        using (SolidBrush solidBrush2 = new SolidBrush(Color.Black))
        {
          e.Graphics.DrawString(this._MessageDescription, this.Font, (Brush) solidBrush2, layoutRectangle2, StringFormat.GenericTypographic);
          e.Graphics.DrawString(this._MessageDescription, this.Font, (Brush) solidBrush1, layoutRectangle1, StringFormat.GenericTypographic);
        }
      }
    }

    private Bitmap LimitImageSize(Bitmap image, int maxWidth, int maxHeight)
    {
      int width = image.Width;
      int height = image.Height;
      if (width > maxWidth)
      {
        double num = (double) maxWidth / (double) width;
        width = (int) ((double) width * num);
        height = (int) ((double) height * num);
      }
      if (height > maxHeight)
      {
        double num = (double) maxHeight / (double) height;
        width = (int) ((double) width * num);
        height = (int) ((double) height * num);
      }
      return this.ScaleImage(image, width, height);
    }

    private Bitmap ScaleImage(Bitmap image, int width, int height) => this.ScaleImage(image, width, height, false);

    private Bitmap ScaleImage(Bitmap image, int width, int height, bool highQuality)
    {
      Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
      using (Graphics graphics = Graphics.FromImage((Image) bitmap))
      {
        if (highQuality)
        {
          graphics.CompositingQuality = CompositingQuality.HighQuality;
          graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
          graphics.SmoothingMode = SmoothingMode.AntiAlias;
        }
        else
        {
          graphics.CompositingQuality = CompositingQuality.HighSpeed;
          graphics.InterpolationMode = InterpolationMode.Bilinear;
          graphics.SmoothingMode = SmoothingMode.None;
        }
        graphics.DrawImage((Image) image, new Rectangle(0, 0, width, height));
      }
      return bitmap;
    }

    public Bitmap PrepareBuiltObjectImage(
      BuiltObject builtObject,
      Bitmap image,
      Color mainColor,
      Color secondaryColor,
      double size,
      int targetSize,
      bool allowPreRotate)
    {
      double num = Math.Sqrt((double) targetSize / size);
      int width = (int) ((double) image.Width * num);
      int height = (int) ((double) image.Height * num);
      Bitmap bitmap1 = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
      using (Graphics graphics = Graphics.FromImage((Image) bitmap1))
      {
        graphics.CompositingQuality = CompositingQuality.HighSpeed;
        graphics.InterpolationMode = InterpolationMode.Bilinear;
        graphics.SmoothingMode = SmoothingMode.None;
        graphics.DrawImage((Image) image, new Rectangle(0, 0, width, height));
      }
      if (allowPreRotate)
      {
        Bitmap bitmap2 = bitmap1;
        bitmap1 = this.RotateImage((Image) bitmap1, builtObject.Heading * -1f);
        bitmap2.Dispose();
      }
      return bitmap1;
    }

    private Bitmap RotateImage(Image image, float angle)
    {
      float num = image != null ? (float) image.Width : throw new ArgumentNullException(nameof (image));
      float height1 = (float) image.Height;
      angle *= -1f;
      angle *= 57.29578f;
      angle %= 360f;
      if ((double) angle < 0.0)
        angle += 360f;
      PointF[] pts = new PointF[4];
      pts[1].X = num;
      pts[2].X = num;
      pts[2].Y = height1;
      pts[3].Y = height1;
      Matrix matrix = new Matrix();
      matrix.Rotate(angle);
      matrix.TransformPoints(pts);
      double val1_1 = double.MinValue;
      double val1_2 = double.MinValue;
      double val1_3 = double.MaxValue;
      double val1_4 = double.MaxValue;
      foreach (PointF pointF in pts)
      {
        val1_1 = Math.Max(val1_1, (double) pointF.X);
        val1_3 = Math.Min(val1_3, (double) pointF.X);
        val1_2 = Math.Max(val1_2, (double) pointF.Y);
        val1_4 = Math.Min(val1_4, (double) pointF.Y);
      }
      double width = Math.Ceiling(val1_1 - val1_3);
      double height2 = Math.Ceiling(val1_2 - val1_4);
      Bitmap bitmap = new Bitmap((int) width, (int) height2);
      using (Graphics graphics = Graphics.FromImage((Image) bitmap))
      {
        graphics.CompositingQuality = CompositingQuality.HighSpeed;
        graphics.InterpolationMode = InterpolationMode.Bilinear;
        graphics.SmoothingMode = SmoothingMode.None;
        PointF point1 = new PointF((float) (width / 2.0), (float) (height2 / 2.0));
        PointF point2 = new PointF(point1.X - num / 2f, point1.Y - num / 2f);
        matrix.Reset();
        matrix.RotateAt(angle, point1);
        graphics.Transform = matrix;
        graphics.DrawImage(image, point2);
      }
      return bitmap;
    }
  }
}
