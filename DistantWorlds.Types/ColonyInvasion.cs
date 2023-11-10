// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ColonyInvasion
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace DistantWorlds.Types
{
  public class ColonyInvasion
  {
    private Bitmap[] _TroopImagesInfantry;
    private Bitmap[] _TroopImagesArmored;
    private Bitmap[] _TroopImagesArtillery;
    private Bitmap[] _TroopImagesSpecialForces;
    private Bitmap[] _TroopImagesPirateRaider;
    private Bitmap[] _FacilityImages;
    private BuiltObjectImageCache _BuiltObjectImageCache;
    private CharacterImageCache _CharacterImageCache;
    private Bitmap[] _RaceImages;
    private Bitmap[][] _ExplosionImages;
    private Bitmap _SpaceImage;
    private Bitmap _AssaultPodImage;
    private string _ApplicationStartupPath;
    private string _CustomizationSetName;
    private string[] _LandscapeFilenames;
    private Bitmap _LandscapeImage;
    private Bitmap _WeaponInfantryImage;
    private Bitmap _WeaponArmoredImage;
    private Bitmap _WeaponPlanetaryDefenseImage;
    private Bitmap _WeaponSpecialForcesImage;
    private SolidBrush _WhiteBrush = new SolidBrush(Color.FromArgb(170, 170, 170));
    private SolidBrush _BlackBrush = new SolidBrush(Color.Black);
    private SolidBrush _GreenBrush = new SolidBrush(Color.FromArgb(0, (int) byte.MaxValue, 0));
    private SolidBrush _RedBrush = new SolidBrush(Color.FromArgb((int) byte.MaxValue, 0, 0));
    private SolidBrush _YellowBrush = new SolidBrush(Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 0));
    private Habitat _Colony;
    private Font _LargeFont;
    private Font _NormalBoldFont;
    private Font _NormalFont;
    private Font _HugeFont;
    private object _LockObject = new object();
    private List<object> _ExplodingItems = new List<object>();
    private List<object> _FiringItems = new List<object>();
    private List<bool> _ExplodingItemIsLarge = new List<bool>();
    private AnimationSystem _AnimationSystem = new AnimationSystem();
    private Dictionary<string, float> _InvadersLandingProgress = new Dictionary<string, float>();
    private Dictionary<string, float> _InvadersLandingExplosionPoint = new Dictionary<string, float>();
    private Dictionary<string, object> _InvadersLandingExplosionFirer = new Dictionary<string, object>();
    private DateTime _LastInvaderLandingProgressUpdate = DateTime.MinValue;
    private Random _Rnd;
    private int _HeaderSize = 150;
    private int _StatusBarAreaHeight = 32;
    private Size _FacilitySize = new Size(40, 40);
    private Size _TroopSize = new Size(30, 30);
    private Size _CharacterSize = new Size(30, 30);
    private Size _PopulationSize = new Size(30, 30);
    private Size _BuiltObjectSize = new Size(40, 40);
    private int _DefendingCharactersX = 5;
    private int _PopulationX = 40;
    private int _FacilityX = 70;
    private int _DefendingTroopsSpecialForcesX = 110;
    private int _AttackingTroopsSpecialForcesX = 145;
    private int _DefendingTroopsArtilleryX = 180;
    private int _DefendingTroopsInfantryX = 215;
    private int _DefendingTroopsArmorX = 250;
    private int _AttackingTroopsArmorX = 310;
    private int _AttackingTroopsInfantryX = 345;
    private int _AttackingTroopsArtilleryX = 380;
    private int _AttackingCharactersX = 415;
    private int _AttackingForcesLowerAtmosphereX = 450;
    private int _AttackingForcesOrbitX = 510;
    private float _DefendingTroopsSpecialForcesXRatio;
    private float _AttackingTroopsSpecialForcesXRatio = 0.2f;
    private float _DefendingTroopsArtilleryXRatio = 0.4f;
    private float _DefendingTroopsInfantryXRatio = 0.6f;
    private float _DefendingTroopsArmorXRatio = 0.8f;
    private float _AttackingTroopsArmorXRatio;
    private float _AttackingTroopsInfantryXRatio = 0.25f;
    private float _AttackingTroopsArtilleryXRatio = 0.5f;
    private float _AttackingCharactersXRatio = 0.75f;
    private int _FrontlineX;
    public int PanelSize;
    public Size Size;
    private bool _AddHotspots = true;
    private HotspotList _Hotspots = new HotspotList();
    private Hotspot _HoveredHotspot;
    private DateTime _LastHotspotUpdate = DateTime.MinValue;
    private DateTime _LastHotspotRebind = DateTime.MinValue;

    private void AddHotspot(Rectangle region, object relatedObject, string message)
    {
      if (!this._AddHotspots)
        return;
      this._Hotspots.Add(new Hotspot(region, relatedObject, message));
    }

    public Hotspot HoveredHotspot => this._HoveredHotspot;

    public void ResetSize(
      int panelSize,
      Bitmap assaultPodImage,
      Bitmap weaponInfantryImage,
      Bitmap weaponArmoredImage,
      Bitmap weaponPlanetaryDefenseImage,
      Bitmap weaponSpecialForcesImage)
    {
      switch (panelSize)
      {
        case 0:
          this._FacilitySize = new Size(40, 40);
          this._TroopSize = new Size(30, 30);
          this._CharacterSize = new Size(30, 30);
          this._PopulationSize = new Size(30, 30);
          this._BuiltObjectSize = new Size(40, 40);
          this._DefendingCharactersX = 5;
          this._PopulationX = 40;
          this._FacilityX = 70;
          this._DefendingTroopsSpecialForcesX = 110;
          this._AttackingTroopsSpecialForcesX = 145;
          this._DefendingTroopsArtilleryX = 180;
          this._DefendingTroopsInfantryX = 215;
          this._DefendingTroopsArmorX = 250;
          this._AttackingTroopsArmorX = 310;
          this._AttackingTroopsInfantryX = 345;
          this._AttackingTroopsArtilleryX = 380;
          this._AttackingCharactersX = 415;
          this._AttackingForcesLowerAtmosphereX = 450;
          this._AttackingForcesOrbitX = 510;
          break;
        case 1:
          this._FacilitySize = new Size(50, 50);
          this._TroopSize = new Size(42, 42);
          this._CharacterSize = new Size(42, 42);
          this._PopulationSize = new Size(42, 42);
          this._BuiltObjectSize = new Size(50, 50);
          this._DefendingCharactersX = 5;
          this._PopulationX = 55;
          this._FacilityX = 97;
          this._DefendingTroopsSpecialForcesX = 150;
          this._AttackingTroopsSpecialForcesX = 200;
          this._DefendingTroopsArtilleryX = 250;
          this._DefendingTroopsInfantryX = 300;
          this._DefendingTroopsArmorX = 350;
          this._AttackingTroopsArmorX = 425;
          this._AttackingTroopsInfantryX = 475;
          this._AttackingTroopsArtilleryX = 525;
          this._AttackingCharactersX = 575;
          this._AttackingForcesLowerAtmosphereX = 625;
          this._AttackingForcesOrbitX = 700;
          break;
        case 2:
          this._FacilitySize = new Size(60, 60);
          this._TroopSize = new Size(56, 56);
          this._CharacterSize = new Size(56, 56);
          this._PopulationSize = new Size(56, 56);
          this._BuiltObjectSize = new Size(60, 60);
          this._DefendingCharactersX = 5;
          this._PopulationX = 65;
          this._FacilityX = 121;
          this._DefendingTroopsSpecialForcesX = 180;
          this._AttackingTroopsSpecialForcesX = 240;
          this._DefendingTroopsArtilleryX = 300;
          this._DefendingTroopsInfantryX = 360;
          this._DefendingTroopsArmorX = 420;
          this._AttackingTroopsArmorX = 510;
          this._AttackingTroopsInfantryX = 570;
          this._AttackingTroopsArtilleryX = 630;
          this._AttackingCharactersX = 690;
          this._AttackingForcesLowerAtmosphereX = 750;
          this._AttackingForcesOrbitX = 840;
          break;
      }
      this.ResizeWeaponImages(assaultPodImage, weaponInfantryImage, weaponArmoredImage, weaponPlanetaryDefenseImage, weaponSpecialForcesImage);
      this.PanelSize = panelSize;
    }

    private void ReviewXCoords()
    {
      switch (this.PanelSize)
      {
        case 0:
          this._DefendingCharactersX = 5;
          this._PopulationX = 40;
          this._FacilityX = 70;
          this._DefendingTroopsSpecialForcesX = 110;
          this._AttackingTroopsSpecialForcesX = 145;
          this._DefendingTroopsArtilleryX = 180;
          this._DefendingTroopsInfantryX = 215;
          this._DefendingTroopsArmorX = 250;
          this._AttackingTroopsArmorX = 310;
          this._AttackingTroopsInfantryX = 345;
          this._AttackingTroopsArtilleryX = 380;
          this._AttackingCharactersX = 415;
          this._AttackingForcesLowerAtmosphereX = 450;
          this._AttackingForcesOrbitX = 510;
          break;
        case 1:
          this._DefendingCharactersX = 5;
          this._PopulationX = 55;
          this._FacilityX = 97;
          this._DefendingTroopsSpecialForcesX = 150;
          this._AttackingTroopsSpecialForcesX = 200;
          this._DefendingTroopsArtilleryX = 250;
          this._DefendingTroopsInfantryX = 300;
          this._DefendingTroopsArmorX = 350;
          this._AttackingTroopsArmorX = 425;
          this._AttackingTroopsInfantryX = 475;
          this._AttackingTroopsArtilleryX = 525;
          this._AttackingCharactersX = 575;
          this._AttackingForcesLowerAtmosphereX = 625;
          this._AttackingForcesOrbitX = 700;
          break;
        case 2:
          this._DefendingCharactersX = 5;
          this._PopulationX = 65;
          this._FacilityX = 121;
          this._DefendingTroopsSpecialForcesX = 180;
          this._AttackingTroopsSpecialForcesX = 240;
          this._DefendingTroopsArtilleryX = 300;
          this._DefendingTroopsInfantryX = 360;
          this._DefendingTroopsArmorX = 420;
          this._AttackingTroopsArmorX = 510;
          this._AttackingTroopsInfantryX = 570;
          this._AttackingTroopsArtilleryX = 630;
          this._AttackingCharactersX = 690;
          this._AttackingForcesLowerAtmosphereX = 750;
          this._AttackingForcesOrbitX = 840;
          break;
      }
      TroopList infantryTroops;
      TroopList armoredTroops;
      TroopList artilleryTroops;
      TroopList specialForcesTroops;
      this._Colony.Troops.SplitTroopsByType(true, out infantryTroops, out armoredTroops, out artilleryTroops, out specialForcesTroops);
      if (armoredTroops.Count <= 0)
        this._DefendingTroopsInfantryX = this._DefendingTroopsArmorX;
      if (infantryTroops.Count <= 0)
        this._DefendingTroopsArtilleryX = this._DefendingTroopsInfantryX;
      if (specialForcesTroops.Count <= 0)
        this._AttackingTroopsSpecialForcesX = this._DefendingTroopsSpecialForcesX;
      this._Colony.InvadingTroops.SplitTroopsByType(true, out infantryTroops, out armoredTroops, out artilleryTroops, out specialForcesTroops);
      if (armoredTroops.Count <= 0)
        this._AttackingTroopsInfantryX = this._AttackingTroopsArmorX;
      if (infantryTroops.Count <= 0)
        this._AttackingTroopsArtilleryX = this._AttackingTroopsInfantryX;
      if (specialForcesTroops.Count > 0)
        return;
      this._DefendingTroopsSpecialForcesX = this._DefendingTroopsArtilleryX - 35;
    }

    public void InitializeImages(
      Bitmap[] troopImagesInfantry,
      Bitmap[] troopImagesArmored,
      Bitmap[] troopImagesArtillery,
      Bitmap[] troopImagesSpecialForces,
      Bitmap[] troopImagesPirateRaider,
      Bitmap[] facilityImages,
      BuiltObjectImageCache builtObjectImageCache,
      CharacterImageCache characterImageCache,
      Bitmap[] raceImages,
      Bitmap[][] explosionImages,
      Bitmap spaceImage,
      Bitmap assaultPodImage,
      Bitmap weaponInfantryImage,
      Bitmap weaponArmoredImage,
      Bitmap weaponPlanetaryDefenseImage,
      Bitmap weaponSpecialForcesImage,
      string applicationStartupPath,
      string customizationSetName)
    {
      this.Clear();
      this._TroopImagesInfantry = troopImagesInfantry;
      this._TroopImagesArmored = troopImagesArmored;
      this._TroopImagesArtillery = troopImagesArtillery;
      this._TroopImagesSpecialForces = troopImagesSpecialForces;
      this._TroopImagesPirateRaider = troopImagesPirateRaider;
      this._FacilityImages = facilityImages;
      this._BuiltObjectImageCache = builtObjectImageCache;
      this._CharacterImageCache = characterImageCache;
      this._RaceImages = raceImages;
      this._ExplosionImages = explosionImages;
      this._SpaceImage = spaceImage;
      this._ApplicationStartupPath = applicationStartupPath;
      this._CustomizationSetName = customizationSetName;
      this._LandscapeFilenames = new string[30];
      this._LandscapeFilenames[0] = "";
      this._LandscapeFilenames[1] = "";
      this._LandscapeFilenames[2] = "";
      this._LandscapeFilenames[3] = "";
      this._LandscapeFilenames[4] = "continental1.png";
      this._LandscapeFilenames[5] = "continental2.png";
      this._LandscapeFilenames[6] = "continental3.png";
      this._LandscapeFilenames[7] = "continental4.png";
      this._LandscapeFilenames[8] = "jungle1.png";
      this._LandscapeFilenames[9] = "";
      this._LandscapeFilenames[10] = "";
      this._LandscapeFilenames[11] = "";
      this._LandscapeFilenames[12] = "";
      this._LandscapeFilenames[13] = "";
      this._LandscapeFilenames[14] = "";
      this._LandscapeFilenames[15] = "";
      this._LandscapeFilenames[16] = "";
      this._LandscapeFilenames[17] = "ice1.png";
      this._LandscapeFilenames[18] = "ice2.png";
      this._LandscapeFilenames[19] = "ice3.png";
      this._LandscapeFilenames[20] = "marsh1.png";
      this._LandscapeFilenames[21] = "marsh2.png";
      this._LandscapeFilenames[22] = "marsh3.png";
      this._LandscapeFilenames[23] = "ocean1.png";
      this._LandscapeFilenames[24] = "ocean2.png";
      this._LandscapeFilenames[25] = "desert1.png";
      this._LandscapeFilenames[26] = "desert2.png";
      this._LandscapeFilenames[27] = "desert3.png";
      this._LandscapeFilenames[28] = "volcanic1.png";
      this._LandscapeFilenames[29] = "volcanic2.png";
      this._AssaultPodImage = GraphicsHelper.ScaleImage(assaultPodImage, this._TroopSize.Width, this._TroopSize.Width, 1f);
      int num = (int) ((double) this._TroopSize.Width * 0.67);
      this._WeaponInfantryImage = GraphicsHelper.ScaleImage(weaponInfantryImage, num, num, 1f);
      this._WeaponArmoredImage = GraphicsHelper.ScaleImage(weaponArmoredImage, num, num, 1f);
      this._WeaponPlanetaryDefenseImage = GraphicsHelper.ScaleImage(weaponPlanetaryDefenseImage, num, num, 1f);
      this._WeaponSpecialForcesImage = GraphicsHelper.ScaleImage(weaponSpecialForcesImage, num, num, 1f);
    }

    private void ResizeWeaponImages(
      Bitmap assaultPodImage,
      Bitmap weaponInfantryImage,
      Bitmap weaponArmoredImage,
      Bitmap weaponPlanetaryDefenseImage,
      Bitmap weaponSpecialForcesImage)
    {
      Bitmap assaultPodImage1 = this._AssaultPodImage;
      Bitmap weaponInfantryImage1 = this._WeaponInfantryImage;
      Bitmap weaponArmoredImage1 = this._WeaponArmoredImage;
      Bitmap planetaryDefenseImage = this._WeaponPlanetaryDefenseImage;
      Bitmap specialForcesImage = this._WeaponSpecialForcesImage;
      this._AssaultPodImage = GraphicsHelper.ScaleImage(assaultPodImage, this._TroopSize.Width, this._TroopSize.Width, 1f);
      int num = (int) ((double) this._TroopSize.Width * 0.5);
      this._WeaponInfantryImage = GraphicsHelper.ScaleImage(weaponInfantryImage, num, num, 1f);
      this._WeaponArmoredImage = GraphicsHelper.ScaleImage(weaponArmoredImage, num, num, 1f);
      this._WeaponPlanetaryDefenseImage = GraphicsHelper.ScaleImage(weaponPlanetaryDefenseImage, num, num, 1f);
      this._WeaponSpecialForcesImage = GraphicsHelper.ScaleImage(weaponSpecialForcesImage, num, num, 1f);
      assaultPodImage1?.Dispose();
      weaponInfantryImage1?.Dispose();
      weaponArmoredImage1?.Dispose();
      planetaryDefenseImage?.Dispose();
      specialForcesImage?.Dispose();
    }

    public void Clear()
    {
      this._TroopImagesInfantry = (Bitmap[]) null;
      this._TroopImagesArmored = (Bitmap[]) null;
      this._TroopImagesArtillery = (Bitmap[]) null;
      this._TroopImagesSpecialForces = (Bitmap[]) null;
      this._TroopImagesPirateRaider = (Bitmap[]) null;
      this._FacilityImages = (Bitmap[]) null;
      this._BuiltObjectImageCache = (BuiltObjectImageCache) null;
      this._CharacterImageCache = (CharacterImageCache) null;
      this._RaceImages = (Bitmap[]) null;
      this._ExplosionImages = (Bitmap[][]) null;
      this._SpaceImage = (Bitmap) null;
      Bitmap assaultPodImage = this._AssaultPodImage;
      this._AssaultPodImage = (Bitmap) null;
      assaultPodImage?.Dispose();
      if (this._LandscapeImage != null)
      {
        this._LandscapeImage.Dispose();
        this._LandscapeImage = (Bitmap) null;
      }
      Bitmap weaponInfantryImage = this._WeaponInfantryImage;
      this._WeaponInfantryImage = (Bitmap) null;
      weaponInfantryImage?.Dispose();
      Bitmap weaponArmoredImage = this._WeaponArmoredImage;
      this._WeaponArmoredImage = (Bitmap) null;
      weaponArmoredImage?.Dispose();
      Bitmap planetaryDefenseImage = this._WeaponPlanetaryDefenseImage;
      this._WeaponPlanetaryDefenseImage = (Bitmap) null;
      planetaryDefenseImage?.Dispose();
      Bitmap specialForcesImage = this._WeaponSpecialForcesImage;
      this._WeaponSpecialForcesImage = (Bitmap) null;
      specialForcesImage?.Dispose();
      this._InvadersLandingProgress.Clear();
      this._InvadersLandingExplosionPoint.Clear();
      this._InvadersLandingExplosionFirer.Clear();
      lock (this._LockObject)
      {
        this._ExplodingItems.Clear();
        this._FiringItems.Clear();
        this._ExplodingItemIsLarge.Clear();
      }
      this._AnimationSystem.ClearAnimations();
    }

    public void ClearColony()
    {
      if (this._Colony != null)
        this._Colony.ColonyInvasion = (ColonyInvasion) null;
      this._Colony = (Habitat) null;
    }

    public void BindData(
      Habitat colony,
      Font normalFont,
      Font normalBoldFont,
      Font largeFont,
      Font hugeFont)
    {
      this._AddHotspots = true;
      if (this._Colony != null)
        this._Colony.ColonyInvasion = (ColonyInvasion) null;
      if (this._Colony != colony)
      {
        this._InvadersLandingProgress.Clear();
        this._InvadersLandingExplosionPoint.Clear();
        this._InvadersLandingExplosionFirer.Clear();
      }
      this._Colony = colony;
      this._Colony.ColonyInvasion = this;
      this._NormalFont = normalFont;
      this._NormalBoldFont = normalBoldFont;
      this._LargeFont = largeFont;
      this._HugeFont = hugeFont;
      this._Rnd = new Random(this._Colony.HabitatIndex);
      Bitmap landscapeImage = this._LandscapeImage;
      int index = (int) this._Colony.LandscapePictureRef;
      if (index >= this._LandscapeFilenames.Length || string.IsNullOrEmpty(this._LandscapeFilenames[index]))
      {
        switch (this._Colony.Type)
        {
          case HabitatType.Volcanic:
            index = 28;
            break;
          case HabitatType.Desert:
            index = 25;
            break;
          case HabitatType.MarshySwamp:
            index = 20;
            break;
          case HabitatType.Continental:
            index = 4;
            break;
          case HabitatType.Ocean:
            index = 23;
            break;
          case HabitatType.Ice:
            index = 17;
            break;
          default:
            index = 4;
            break;
        }
      }
      string str = this._ApplicationStartupPath + "\\images\\environment\\planetmaps\\" + this._LandscapeFilenames[index];
      if (!string.IsNullOrEmpty(this._CustomizationSetName))
      {
        string path = this._ApplicationStartupPath + "\\customization\\" + this._CustomizationSetName + "\\images\\environment\\planetmaps\\" + this._LandscapeFilenames[index];
        if (File.Exists(path))
          str = path;
      }
      if (File.Exists(str))
        this._LandscapeImage = GraphicsHelper.LoadImageFromFilePath(str);
      if (landscapeImage != null && landscapeImage.PixelFormat != PixelFormat.Undefined)
        landscapeImage.Dispose();
      this._AnimationSystem.ClearAnimations();
      lock (this._LockObject)
      {
        this._ExplodingItems.Clear();
        this._FiringItems.Clear();
        this._ExplodingItemIsLarge.Clear();
      }
    }

    public void AddExplosion(Troop troop, bool isLarge, object firer) => this.AddExplosionCore((object) troop, isLarge, firer);

    public void AddExplosion(Character character, bool isLarge, object firer) => this.AddExplosionCore((object) character, isLarge, firer);

    public void AddExplosion(PlanetaryFacility facility, bool isLarge, object firer) => this.AddExplosionCore((object) facility, isLarge, firer);

    public void AddExplosion(Population population, bool isLarge, object firer) => this.AddExplosionCore((object) population, isLarge, firer);

    private void AddExplosionCore(object item, bool isLarge, object firer) => this.AddExplosionCore(item, isLarge, firer, true);

    private void AddExplosionCore(object item, bool isLarge, object firer, bool doLock)
    {
      if (isLarge)
      {
        Rectangle area = new Rectangle(0, this._HeaderSize + this._StatusBarAreaHeight, this.Size.Width, this.Size.Height - (this._HeaderSize + this._StatusBarAreaHeight));
        item = (object) this.ResolveLocation(item, area);
      }
      if (doLock)
      {
        lock (this._LockObject)
        {
          this._ExplodingItems.Add(item);
          this._FiringItems.Add(firer);
          this._ExplodingItemIsLarge.Add(isLarge);
        }
      }
      else
      {
        this._ExplodingItems.Add(item);
        this._FiringItems.Add(firer);
        this._ExplodingItemIsLarge.Add(isLarge);
      }
    }

    private void ProcessExplosions(DateTime time)
    {
      lock (this._LockObject)
      {
        Rectangle area = new Rectangle(0, this._HeaderSize + this._StatusBarAreaHeight, this.Size.Width, this.Size.Height - (this._HeaderSize + this._StatusBarAreaHeight));
        for (int index = 0; index < this._ExplodingItems.Count; ++index)
        {
          Rectangle rectangle = Rectangle.Empty;
          if (this._ExplodingItems[index] is Rectangle)
            rectangle = (Rectangle) this._ExplodingItems[index];
          if (this._ExplodingItems[index] is Troop)
          {
            Troop explodingItem = (Troop) this._ExplodingItems[index];
            bool flag = false;
            if (this._Colony.Troops.Contains(explodingItem))
              flag = true;
            if (flag)
            {
              rectangle = this.ResolveLocationDefendingTroop(explodingItem, area, this._DefendingTroopsSpecialForcesX, this._FrontlineX);
            }
            else
            {
              bool landing = false;
              rectangle = this.ResolveLocationAttackingTroop(explodingItem, area, this._AttackingForcesLowerAtmosphereX, this._FrontlineX, out landing);
            }
          }
          else if (this._ExplodingItems[index] is Character)
          {
            Character explodingItem = (Character) this._ExplodingItems[index];
            if (this._Colony.Characters.Contains(explodingItem))
            {
              rectangle = this.ResolveLocationDefendingCharacter(explodingItem, area);
            }
            else
            {
              bool landing = false;
              rectangle = this.ResolveLocationAttackingCharacter(explodingItem, area, out landing);
            }
          }
          else if (this._ExplodingItems[index] is PlanetaryFacility)
            rectangle = this.ResolveLocationFacility((PlanetaryFacility) this._ExplodingItems[index], area);
          else if (this._ExplodingItems[index] is Population)
            rectangle = this.ResolveLocationDefendingPopulationForExplosion((Population) this._ExplodingItems[index], area);
          bool flag1 = this._ExplodingItemIsLarge[index];
          int width = 40;
          int height = 40;
          if (flag1)
          {
            width = 100;
            height = 100;
          }
          switch (this.PanelSize)
          {
            case 0:
              width = 40;
              height = 40;
              if (flag1)
              {
                width = 100;
                height = 100;
                break;
              }
              break;
            case 1:
              width = 56;
              height = 56;
              if (flag1)
              {
                width = 140;
                height = 140;
                break;
              }
              break;
            case 2:
              width = 75;
              height = 75;
              if (flag1)
              {
                width = 188;
                height = 188;
                break;
              }
              break;
          }
          double x = (double) (rectangle.X + rectangle.Width / 2 - width / 2);
          double y = (double) (rectangle.Y + rectangle.Height / 2 - height / 2);
          this._AnimationSystem.AddAnimation(new Animation(this._ExplosionImages[this._Rnd.Next(0, this._ExplosionImages.Length)], time, 30, x, y, width, height)
          {
            ExtraData = this._FiringItems[index]
          });
        }
        this._ExplodingItems.Clear();
        this._FiringItems.Clear();
        this._ExplodingItemIsLarge.Clear();
      }
    }

    public Rectangle ResolveLocation(object item, Rectangle area)
    {
      switch (item)
      {
        case BuiltObject _:
          return this.ResolveLocationBaseAtHabitat((BuiltObject) item, area);
        case PlanetaryFacility _:
          return this.ResolveLocationFacility((PlanetaryFacility) item, area);
        case Troop _:
          Troop troop = (Troop) item;
          bool flag = false;
          if (this._Colony.Troops.Contains(troop))
            flag = true;
          if (flag)
            return this.ResolveLocationDefendingTroop(troop, area, this._DefendingTroopsSpecialForcesX, this._FrontlineX);
          bool landing1 = false;
          return this.ResolveLocationAttackingTroop(troop, area, this._AttackingForcesLowerAtmosphereX, this._FrontlineX, out landing1);
        case Character _:
          Character character = (Character) item;
          if (character.Empire == this._Colony.Empire)
            return this.ResolveLocationDefendingCharacter(character, area);
          bool landing2 = false;
          return this.ResolveLocationAttackingCharacter(character, area, out landing2);
        case Population _:
          return this.ResolveLocationDefendingPopulationForExplosion((Population) item, area);
        default:
          return Rectangle.Empty;
      }
    }

    private float CheckInvaderLandingOffset(object item)
    {
      float num = 0.0f;
      switch (item)
      {
        case Troop _:
          Troop troop = (Troop) item;
          if (this._InvadersLandingProgress.ContainsKey(troop.Name))
          {
            num = this._InvadersLandingProgress[troop.Name];
            break;
          }
          break;
        case Character _:
          Character character = (Character) item;
          if (this._InvadersLandingProgress.ContainsKey(character.Name))
          {
            num = this._InvadersLandingProgress[character.Name];
            break;
          }
          break;
      }
      return num;
    }

    public void AddInvaderLanding(object invader)
    {
      lock (this._LockObject)
      {
        switch (invader)
        {
          case Troop _:
            Troop troop = (Troop) invader;
            if (this._InvadersLandingProgress.ContainsKey(troop.Name))
              break;
            this._InvadersLandingProgress.Add(troop.Name, 1f);
            break;
          case Character _:
            Character character = (Character) invader;
            if (this._InvadersLandingProgress.ContainsKey(character.Name))
              break;
            this._InvadersLandingProgress.Add(character.Name, 1f);
            break;
        }
      }
    }

    public void AddInvaderLandingExplosion(object invader, object firer, Random rnd)
    {
      lock (this._LockObject)
      {
        float num = (float) (0.3 + rnd.NextDouble() * 0.45);
        switch (invader)
        {
          case Troop _:
            Troop troop = (Troop) invader;
            if (this._InvadersLandingExplosionPoint.ContainsKey(troop.Name))
              break;
            this._InvadersLandingExplosionPoint.Add(troop.Name, num);
            this._InvadersLandingExplosionFirer.Add(troop.Name, firer);
            break;
          case Character _:
            Character character = (Character) invader;
            if (this._InvadersLandingExplosionPoint.ContainsKey(character.Name))
              break;
            this._InvadersLandingExplosionPoint.Add(character.Name, num);
            this._InvadersLandingExplosionFirer.Add(character.Name, firer);
            break;
        }
      }
    }

    public void UpdateInvaderLandingProgress(DateTime time)
    {
      lock (this._LockObject)
      {
        double totalSeconds = time.Subtract(this._LastInvaderLandingProgressUpdate).TotalSeconds;
        List<string> stringList1 = new List<string>();
        List<string> stringList2 = new List<string>();
        List<string> stringList3 = new List<string>();
        foreach (string str in new List<string>((IEnumerable<string>) this._InvadersLandingProgress.Keys))
        {
          float num1 = this._InvadersLandingProgress[str];
          float num2 = (float) (totalSeconds * 0.25);
          float num3 = num1 - num2;
          if (this._InvadersLandingExplosionPoint.ContainsKey(str))
          {
            float num4 = this._InvadersLandingExplosionPoint[str];
            object firer = (object) null;
            if (this._InvadersLandingExplosionFirer.ContainsKey(str))
              firer = this._InvadersLandingExplosionFirer[str];
            if ((double) num1 >= (double) num4 && (double) num4 >= (double) num3)
            {
              object obj = (object) this._Colony.InvadingTroops.GetFirstByName(str) ?? (object) this._Colony.InvadingCharacters.GetFirstByName(str);
              if (obj != null)
              {
                Rectangle area = new Rectangle(0, this._HeaderSize + this._StatusBarAreaHeight, this.Size.Width, this.Size.Height - (this._HeaderSize + this._StatusBarAreaHeight));
                this.AddExplosionCore((object) this.ResolveLocation(obj, area), false, firer, false);
              }
              stringList2.Add(str);
              stringList3.Add(str);
            }
          }
          if ((double) num3 > 0.0)
            this._InvadersLandingProgress[str] = num3;
          else
            stringList1.Add(str);
        }
        for (int index = 0; index < stringList1.Count; ++index)
        {
          if (this._InvadersLandingProgress.ContainsKey(stringList1[index]))
            this._InvadersLandingProgress.Remove(stringList1[index]);
        }
        for (int index = 0; index < stringList2.Count; ++index)
        {
          if (this._InvadersLandingExplosionPoint.ContainsKey(stringList2[index]))
            this._InvadersLandingExplosionPoint.Remove(stringList2[index]);
        }
        for (int index = 0; index < stringList3.Count; ++index)
        {
          if (this._InvadersLandingExplosionFirer.ContainsKey(stringList3[index]))
            this._InvadersLandingExplosionFirer.Remove(stringList3[index]);
        }
        this._LastInvaderLandingProgressUpdate = time;
      }
    }

    public Rectangle ResolveLocationFacility(PlanetaryFacility facility, Rectangle area)
    {
      Rectangle rectangle = Rectangle.Empty;
      int num1 = this._Colony.Facilities.IndexOf(facility);
      if (num1 >= 0)
      {
        int num2 = area.Height / this._Colony.Facilities.Count;
        int y = area.Y + num2 / 2 + num2 * num1 - this._FacilitySize.Height / 2;
        rectangle = new Rectangle(area.X + this._FacilityX, y, this._FacilitySize.Width, this._FacilitySize.Height);
      }
      return rectangle;
    }

    public Rectangle ResolveLocationBaseAtHabitat(BuiltObject builtObject, Rectangle area)
    {
      Rectangle rectangle = Rectangle.Empty;
      if (this._Colony.BasesAtHabitat != null)
      {
        int num1 = this._Colony.BasesAtHabitat.IndexOf(builtObject);
        if (num1 >= 0)
        {
          int num2 = area.Height / this._Colony.BasesAtHabitat.Count;
          int y = area.Y + num2 / 2 + num2 * num1 - this._BuiltObjectSize.Height / 2;
          rectangle = new Rectangle(area.X + this._AttackingForcesOrbitX, y, this._BuiltObjectSize.Width, this._BuiltObjectSize.Height);
        }
      }
      return rectangle;
    }

    public Rectangle ResolveLocationDefendingCharacter(Character character, Rectangle area)
    {
      Rectangle rectangle = Rectangle.Empty;
      int num1 = this._Colony.Characters.IndexOf(character);
      if (num1 >= 0)
      {
        int num2 = (area.Height - this._CharacterSize.Height) / this._Colony.Characters.Count;
        int y = area.Y + num2 / 2 + num2 * num1;
        rectangle = new Rectangle(area.X + this._DefendingCharactersX, y, this._CharacterSize.Width, this._CharacterSize.Height);
      }
      return rectangle;
    }

    public Rectangle ResolveLocationAttackingCharacter(
      Character character,
      Rectangle area,
      out bool landing)
    {
      landing = false;
      Rectangle rectangle = Rectangle.Empty;
      int num1 = this._Colony.InvadingCharacters.IndexOf(character);
      if (num1 >= 0)
      {
        int num2 = (area.Height - this._CharacterSize.Height) / this._Colony.InvadingCharacters.Count;
        int y = area.Y + num2 / 2 + num2 * num1;
        float num3 = this.CheckInvaderLandingOffset((object) character);
        if ((double) num3 > 0.0)
          landing = true;
        rectangle = new Rectangle(area.X + this._AttackingCharactersX + (int) ((double) num3 * (double) (area.Right - this._AttackingCharactersX)), y, this._CharacterSize.Width, this._CharacterSize.Height);
      }
      return rectangle;
    }

    public Rectangle ResolveLocationDefendingPopulationForExplosion(
      Population population,
      Rectangle area)
    {
      Rectangle rectangle = Rectangle.Empty;
      if (this._Colony.Population != null)
      {
        int maxValue = 1 + (int) (this._Colony.Population.TotalAmount / 1000000000L);
        int num1 = (area.Height - this._PopulationSize.Height) / maxValue;
        int num2 = this._Rnd.Next(0, maxValue);
        rectangle = new Rectangle(this._PopulationX, area.Y + num1 / 2 + num1 * num2, this._PopulationSize.Width, this._PopulationSize.Height);
      }
      return rectangle;
    }

    public Rectangle ResolveLocationDefendingTroop(
      Troop troop,
      Rectangle area,
      int backlineX,
      int frontlineX,
      TroopList infantry,
      TroopList armored,
      TroopList artillery,
      TroopList specialForces)
    {
      Rectangle rectangle = Rectangle.Empty;
      TroopList troopList = new TroopList();
      float num1 = 0.0f;
      switch (troop.Type)
      {
        case TroopType.Infantry:
        case TroopType.PirateRaider:
          troopList = infantry;
          num1 = this._DefendingTroopsInfantryXRatio;
          break;
        case TroopType.Armored:
          troopList = armored;
          num1 = this._DefendingTroopsArmorXRatio;
          break;
        case TroopType.Artillery:
          troopList = artillery;
          num1 = this._DefendingTroopsArtilleryXRatio;
          break;
        case TroopType.SpecialForces:
          troopList = specialForces;
          break;
      }
      int num2 = troopList.IndexOf(troop);
      if (num2 >= 0)
      {
        int num3 = (area.Height - this._TroopSize.Height) / troopList.Count;
        int y = area.Y + num3 / 2 + num3 * num2;
        int x = backlineX + (int) ((double) (frontlineX - backlineX) * (double) num1);
        switch (troop.Type)
        {
          case TroopType.Infantry:
          case TroopType.PirateRaider:
            rectangle = new Rectangle(x, y, this._TroopSize.Width, this._TroopSize.Height);
            break;
          case TroopType.Armored:
            rectangle = new Rectangle(x, y, this._TroopSize.Width, this._TroopSize.Height);
            break;
          case TroopType.Artillery:
            rectangle = new Rectangle(x, y, this._TroopSize.Width, this._TroopSize.Height);
            break;
          case TroopType.SpecialForces:
            rectangle = new Rectangle(area.X + this._DefendingTroopsSpecialForcesX, y, this._TroopSize.Width, this._TroopSize.Height);
            break;
        }
      }
      return rectangle;
    }

    public Rectangle ResolveLocationDefendingTroop(
      Troop troop,
      Rectangle area,
      int backlineX,
      int frontlineX)
    {
      Rectangle rectangle = Rectangle.Empty;
      TroopList byType = this._Colony.Troops.GetByType(troop.Type);
      if (troop.Type == TroopType.Infantry || troop.Type == TroopType.PirateRaider)
        byType = this._Colony.Troops.GetByType(new List<TroopType>()
        {
          TroopType.Infantry,
          TroopType.PirateRaider
        });
      int num1 = byType.IndexOf(troop);
      if (num1 >= 0)
      {
        int num2 = (area.Height - this._TroopSize.Height) / byType.Count;
        int y = area.Y + num2 / 2 + num2 * num1;
        float num3 = 0.0f;
        switch (troop.Type)
        {
          case TroopType.Infantry:
          case TroopType.PirateRaider:
            num3 = this._DefendingTroopsInfantryXRatio;
            break;
          case TroopType.Armored:
            num3 = this._DefendingTroopsArmorXRatio;
            break;
          case TroopType.Artillery:
            num3 = this._DefendingTroopsArtilleryXRatio;
            break;
        }
        int x = backlineX + (int) ((double) (frontlineX - backlineX) * (double) num3);
        switch (troop.Type)
        {
          case TroopType.Infantry:
          case TroopType.PirateRaider:
            rectangle = new Rectangle(x, y, this._TroopSize.Width, this._TroopSize.Height);
            break;
          case TroopType.Armored:
            rectangle = new Rectangle(x, y, this._TroopSize.Width, this._TroopSize.Height);
            break;
          case TroopType.Artillery:
            rectangle = new Rectangle(x, y, this._TroopSize.Width, this._TroopSize.Height);
            break;
          case TroopType.SpecialForces:
            rectangle = new Rectangle(area.X + this._DefendingTroopsSpecialForcesX, y, this._TroopSize.Width, this._TroopSize.Height);
            break;
        }
      }
      return rectangle;
    }

    public Rectangle ResolveLocationAttackingTroop(
      Troop troop,
      Rectangle area,
      int backlineX,
      int frontlineX,
      TroopList infantry,
      TroopList armored,
      TroopList artillery,
      TroopList specialForces,
      out bool landing)
    {
      landing = false;
      Rectangle rectangle = Rectangle.Empty;
      TroopList troopList = new TroopList();
      float num1 = 0.0f;
      switch (troop.Type)
      {
        case TroopType.Infantry:
        case TroopType.PirateRaider:
          troopList = infantry;
          num1 = this._AttackingTroopsInfantryXRatio;
          break;
        case TroopType.Armored:
          troopList = armored;
          num1 = this._AttackingTroopsArmorXRatio;
          break;
        case TroopType.Artillery:
          troopList = artillery;
          num1 = this._AttackingTroopsArtilleryXRatio;
          break;
        case TroopType.SpecialForces:
          troopList = specialForces;
          break;
      }
      int num2 = troopList.IndexOf(troop);
      if (num2 >= 0)
      {
        int num3 = (area.Height - this._TroopSize.Height) / troopList.Count;
        int y = area.Y + num3 / 2 + num3 * num2;
        int num4 = frontlineX + (int) ((double) (backlineX - frontlineX) * (double) num1);
        float num5 = this.CheckInvaderLandingOffset((object) troop);
        if ((double) num5 > 0.0)
          landing = true;
        int x = num4 + (int) ((double) num5 * (double) (area.Right - num4));
        switch (troop.Type)
        {
          case TroopType.Infantry:
          case TroopType.PirateRaider:
            rectangle = new Rectangle(x, y, this._TroopSize.Width, this._TroopSize.Height);
            break;
          case TroopType.Armored:
            rectangle = new Rectangle(x, y, this._TroopSize.Width, this._TroopSize.Height);
            break;
          case TroopType.Artillery:
            rectangle = new Rectangle(x, y, this._TroopSize.Width, this._TroopSize.Height);
            break;
          case TroopType.SpecialForces:
            rectangle = new Rectangle(area.X + this._AttackingTroopsSpecialForcesX, y, this._TroopSize.Width, this._TroopSize.Height);
            break;
        }
      }
      return rectangle;
    }

    public Rectangle ResolveLocationAttackingTroop(
      Troop troop,
      Rectangle area,
      int backlineX,
      int frontlineX,
      out bool landing)
    {
      landing = false;
      Rectangle rectangle = Rectangle.Empty;
      TroopList byType = this._Colony.InvadingTroops.GetByType(troop.Type);
      if (troop.Type == TroopType.Infantry || troop.Type == TroopType.PirateRaider)
        byType = this._Colony.InvadingTroops.GetByType(new List<TroopType>()
        {
          TroopType.Infantry,
          TroopType.PirateRaider
        });
      int num1 = byType.IndexOf(troop);
      if (num1 >= 0)
      {
        int num2 = (area.Height - this._TroopSize.Height) / byType.Count;
        int y = area.Y + num2 / 2 + num2 * num1;
        float num3 = 0.0f;
        switch (troop.Type)
        {
          case TroopType.Infantry:
          case TroopType.PirateRaider:
            num3 = this._AttackingTroopsInfantryXRatio;
            break;
          case TroopType.Armored:
            num3 = this._AttackingTroopsArmorXRatio;
            break;
          case TroopType.Artillery:
            num3 = this._AttackingTroopsArtilleryXRatio;
            break;
        }
        int num4 = frontlineX + (int) ((double) (backlineX - frontlineX) * (double) num3);
        float num5 = this.CheckInvaderLandingOffset((object) troop);
        if ((double) num5 > 0.0)
          landing = true;
        int x = num4 + (int) ((double) num5 * (double) (area.Right - num4));
        switch (troop.Type)
        {
          case TroopType.Infantry:
          case TroopType.PirateRaider:
            rectangle = new Rectangle(x, y, this._TroopSize.Width, this._TroopSize.Height);
            break;
          case TroopType.Armored:
            rectangle = new Rectangle(x, y, this._TroopSize.Width, this._TroopSize.Height);
            break;
          case TroopType.Artillery:
            rectangle = new Rectangle(x, y, this._TroopSize.Width, this._TroopSize.Height);
            break;
          case TroopType.SpecialForces:
            rectangle = new Rectangle(area.X + this._AttackingTroopsSpecialForcesX, y, this._TroopSize.Width, this._TroopSize.Height);
            break;
        }
      }
      return rectangle;
    }

    public void Update(Galaxy galaxy, TimeSpan timePassed)
    {
      if (this._Colony == null)
        return;
      this._Colony.ProcessColonyTroops(timePassed.TotalSeconds);
      this._Colony.ResolveInvasionBattles(timePassed, galaxy);
    }

    public void CheckHovered(int x, int y)
    {
      DateTime now = DateTime.Now;
      if (now.Subtract(this._LastHotspotUpdate).TotalSeconds > 0.2)
      {
        this._HoveredHotspot = this._Hotspots.ResolveHotspotAtPoint(x, y);
        this._LastHotspotUpdate = now;
      }
      if (now.Subtract(this._LastHotspotRebind).TotalSeconds <= 1.0)
        return;
      this._Hotspots.Clear();
      this._AddHotspots = true;
      this._LastHotspotRebind = now;
    }

    public void Draw(Graphics graphics, DateTime time)
    {
      Rectangle area = new Rectangle(0, this._HeaderSize + this._StatusBarAreaHeight, this.Size.Width, this.Size.Height - (this._HeaderSize + this._StatusBarAreaHeight));
      int alpha = 32;
      if (this._Colony != null)
      {
        this.UpdateInvaderLandingProgress(time);
        this.ReviewXCoords();
        Color color = Color.Gray;
        if (this._Colony.Empire != null)
        {
          color = this._Colony.Empire.MainColor;
          if (this._Colony.Empire.PirateEmpireBaseHabitat != null && this._Colony.Empire.MainColor == Color.FromArgb(1, 1, 1))
            color = Color.FromArgb(48, 48, 48);
          using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(32, 32, 32)))
            graphics.FillRectangle((Brush) solidBrush, new Rectangle(0, 0, this.Size.Width, this._HeaderSize));
        }
        int num1 = this.Size.Width - this._SpaceImage.Width;
        Rectangle destRect1 = new Rectangle(0, this._HeaderSize, num1, this.Size.Height - this._HeaderSize);
        Rectangle destRect2 = new Rectangle(num1, this._HeaderSize, this.Size.Width - num1, this.Size.Height - this._HeaderSize);
        Bitmap landscapeImage = this._LandscapeImage;
        if (landscapeImage != null && landscapeImage.PixelFormat != PixelFormat.Undefined)
          graphics.DrawImage((Image) landscapeImage, destRect1, new Rectangle(0, 0, landscapeImage.Width, landscapeImage.Height), GraphicsUnit.Pixel);
        graphics.DrawImage((Image) this._SpaceImage, destRect2, new Rectangle(0, 0, this._SpaceImage.Width, this._SpaceImage.Height), GraphicsUnit.Pixel);
        Color color1 = Color.FromArgb(64, 64, 64);
        switch (this._Colony.Type)
        {
          case HabitatType.Volcanic:
            color1 = Color.FromArgb(160, 80, 0);
            break;
          case HabitatType.Desert:
            color1 = Color.FromArgb(160, 128, 96);
            break;
          case HabitatType.MarshySwamp:
            color1 = Color.FromArgb(0, 72, 56);
            break;
          case HabitatType.Continental:
            color1 = Color.FromArgb(24, 40, 80);
            break;
          case HabitatType.Ocean:
            color1 = Color.FromArgb(24, 48, 96);
            break;
          case HabitatType.Ice:
            color1 = Color.FromArgb(96, 108, 160);
            break;
        }
        Rectangle rect = new Rectangle(num1 - 1, this._HeaderSize, 40, this.Size.Height - this._HeaderSize);
        using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, color1, Color.Transparent, LinearGradientMode.Horizontal))
          graphics.FillRectangle((Brush) linearGradientBrush, rect);
        int num2 = this._AttackingTroopsArmorX;
        int defendingStrength = 0;
        int attackingStrength = 0;
        if (this._Colony.Empire != null && this._Colony.Troops != null && this._Colony.Characters != null && this._Colony.InvadingTroops != null && this._Colony.InvadingCharacters != null)
        {
          Empire invader = (Empire) null;
          Empire defender = (Empire) null;
          this._Colony.ResolveInvasionEmpires(out defender, out invader);
          if (defender == null)
            defender = this._Colony.Empire;
          double totalDefendModifier = 0.0;
          double totalAttackModifier = 0.0;
          List<double> modifierAmountsDefense;
          List<string> modifierReasonsDefense;
          List<double> modifierAmountsAttack;
          List<string> modifierReasonsAttack;
          this._Colony.CalculateForceStrengths(defender, invader, this._Colony.Troops, this._Colony.Characters, this._Colony.InvadingTroops, this._Colony.InvadingCharacters, out defendingStrength, out attackingStrength, out totalDefendModifier, out totalAttackModifier, out modifierAmountsDefense, out modifierReasonsDefense, out modifierAmountsAttack, out modifierReasonsAttack);
          bool isDefending = false;
          int populationStrength = this._Colony.CalculatePopulationStrength(out isDefending, invader, defender);
          if (isDefending)
            defendingStrength += populationStrength;
          else
            attackingStrength += populationStrength;
          bool flag = false;
          if (this._Colony.InvadingTroops.Count > 0 && this._Colony.InvadingTroops[0].Type == TroopType.PirateRaider)
            flag = true;
          int num3 = 40;
          SizeF maxSize1 = new SizeF((float) area.Width, 40f);
          int num4 = this._NormalFont.Height - 2;
          int num5 = this._LargeFont.Height + modifierAmountsDefense.Count * num4;
          int num6 = this._LargeFont.Height + modifierAmountsAttack.Count * num4;
          int y1 = (this._HeaderSize - num5) / 2;
          int y2 = (this._HeaderSize - num6) / 2;
          SizeF maxSize2 = new SizeF((float) (220 - num3), (float) area.Height);
          this.DrawStringWithDropShadow(graphics, defender.Name, this._LargeFont, new Point(area.X, y1));
          for (int index = 0; index < modifierAmountsDefense.Count; ++index)
          {
            int y3 = y1 + this._LargeFont.Height + index * (this._NormalFont.Height - 2);
            this.DrawStringWithDropShadow(graphics, modifierAmountsDefense[index].ToString("+0%;-0%"), this._NormalFont, new Point(5, y3));
            this.DrawStringWithDropShadow(graphics, modifierReasonsDefense[index], this._NormalFont, new Point(5 + num3, y3), (Brush) this._WhiteBrush, maxSize2);
          }
          if (invader != null)
          {
            int x = area.Right - (5 + (int) maxSize2.Width);
            string text = invader.Name;
            if (flag)
              text = text + " (" + TextResolver.GetText("Raiding") + ")";
            this.DrawStringWithDropShadow(graphics, text, this._LargeFont, new Point(x, y2));
            for (int index = 0; index < modifierAmountsAttack.Count; ++index)
            {
              int y4 = y2 + this._LargeFont.Height + index * (this._NormalFont.Height - 2);
              this.DrawStringWithDropShadow(graphics, modifierAmountsAttack[index].ToString("+0%;-0%"), this._NormalFont, new Point(5 + x, y4));
              this.DrawStringWithDropShadow(graphics, modifierReasonsAttack[index], this._NormalFont, new Point(5 + x + num3, y4), (Brush) this._WhiteBrush, maxSize2);
            }
          }
          if (invader != null)
          {
            string text = string.Format(TextResolver.GetText("Battle Strength Description"), (object) defendingStrength.ToString("0,K"), (object) attackingStrength.ToString("0,K"));
            int y5 = (this._HeaderSize - this._HugeFont.Height) / 2;
            Point location = new Point(area.X + area.Width / 2, y5);
            this.DrawStringWithDropShadowCentered(graphics, text, this._HugeFont, location, maxSize1);
          }
          else
          {
            string text = defendingStrength.ToString("0,K");
            int y6 = (this._HeaderSize - this._HugeFont.Height) / 2;
            Point location = new Point(area.X + area.Width / 2, y6);
            this.DrawStringWithDropShadowCentered(graphics, text, this._HugeFont, location, maxSize1);
          }
          if (attackingStrength > 0)
          {
            int num7 = defendingStrength + attackingStrength;
            float num8 = (float) defendingStrength / (float) num7;
            int width = this._AttackingForcesLowerAtmosphereX - this._DefendingTroopsSpecialForcesX;
            num2 = this._DefendingTroopsSpecialForcesX + (int) ((double) width * (double) num8);
            using (Pen pen = new Pen(Color.FromArgb(128, (int) byte.MaxValue, 0, 0), 2f))
            {
              pen.DashStyle = DashStyle.Dash;
              graphics.DrawLine(pen, num2, this._HeaderSize, num2, this.Size.Height);
            }
            if (defender != null)
            {
              using (SolidBrush solidBrush = new SolidBrush(defender.MainColor))
                graphics.FillRectangle((Brush) solidBrush, new Rectangle(this._DefendingTroopsSpecialForcesX, this._HeaderSize + 5, width, this._StatusBarAreaHeight - 10));
            }
            if (invader != null)
            {
              using (SolidBrush solidBrush = new SolidBrush(invader.MainColor))
                graphics.FillRectangle((Brush) solidBrush, new Rectangle(num2, this._HeaderSize + 5, width - (num2 - this._DefendingTroopsSpecialForcesX), this._StatusBarAreaHeight - 10));
            }
            this.DrawStringWithDropShadowCentered(graphics, string.Format(TextResolver.GetText("Battle Strength Description"), (object) defendingStrength.ToString("0,K"), (object) attackingStrength.ToString("0,K")), this._NormalBoldFont, new Point(this._DefendingTroopsSpecialForcesX + width / 2, this._HeaderSize + 6));
          }
        }
        if (this._Colony.BasesAtHabitat != null && this._Colony.BasesAtHabitat.Count > 0)
        {
          for (int index = 0; index < this._Colony.BasesAtHabitat.Count; ++index)
          {
            BuiltObject builtObject = this._Colony.BasesAtHabitat[index];
            if (builtObject != null && !builtObject.HasBeenDestroyed)
            {
              Rectangle rectangle = this.ResolveLocation((object) builtObject, area);
              Bitmap image = this._BuiltObjectImageCache.ObtainImage(builtObject);
              graphics.DrawImage((Image) image, rectangle, new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
              this.AddHotspot(rectangle, (object) builtObject, builtObject.Name);
            }
          }
        }
        if (this._Colony.PlanetaryShieldPresent)
        {
          using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(128, 160, 224, (int) byte.MaxValue)))
          {
            Rectangle rectangle = new Rectangle(this._AttackingForcesLowerAtmosphereX + (this._AttackingForcesOrbitX - this._AttackingForcesLowerAtmosphereX) / 2 - 3, area.Top, 6, area.Bottom);
            graphics.FillRectangle((Brush) solidBrush, rectangle);
            this.AddHotspot(rectangle, (object) 1, TextResolver.GetText("Planetary Facility Planetary Shield"));
          }
        }
        if (this._Colony.Population != null && this._Colony.Population.DominantRace != null)
        {
          int num9 = 1 + (int) (this._Colony.Population.TotalAmount / 1000000000L);
          bool isDefending = true;
          int num10 = this._Colony.CalculatePopulationStrength(out isDefending) / num9;
          int num11 = area.Height / num9;
          Bitmap raceImage = this._RaceImages[this._Colony.Population.DominantRace.PictureRef];
          for (int index = 0; index < num9; ++index)
          {
            Rectangle rectangle = new Rectangle(this._PopulationX, area.Y + num11 / 2 + num11 * index - this._PopulationSize.Height / 2, this._PopulationSize.Width, this._PopulationSize.Height);
            graphics.DrawImage((Image) raceImage, rectangle);
            if (attackingStrength > 0)
              this.AddHotspot(rectangle, (object) this._Colony.Population, string.Format(TextResolver.GetText("Colony Population Defending RACE Description"), (object) this._Colony.Population.DominantRace.Name, (object) num10.ToString("0")));
            else
              this.AddHotspot(rectangle, (object) this._Colony.Population, string.Format(TextResolver.GetText("Colony Population RACE Description"), (object) this._Colony.Population.DominantRace.Name));
          }
        }
        if (this._Colony.Facilities != null)
        {
          for (int index = 0; index < this._Colony.Facilities.Count; ++index)
          {
            PlanetaryFacility facility = this._Colony.Facilities[index];
            if (facility != null)
            {
              Rectangle rectangle = this.ResolveLocationFacility(facility, area);
              graphics.DrawImage((Image) this._FacilityImages[(int) facility.PictureRef], rectangle, new Rectangle(0, 0, this._FacilityImages[(int) facility.PictureRef].Width, this._FacilityImages[(int) facility.PictureRef].Height), GraphicsUnit.Pixel);
              this.AddHotspot(rectangle, (object) facility, facility.Name);
            }
          }
        }
        if (this._Colony.Characters != null)
        {
          GraphicsHelper.SetGraphicsQuality(graphics, GraphicsQuality.High);
          for (int index = 0; index < this._Colony.Characters.Count; ++index)
          {
            Character character = this._Colony.Characters[index];
            if (character != null)
            {
              Rectangle rectangle = this.ResolveLocationDefendingCharacter(character, area);
              Bitmap bitmap = this._CharacterImageCache.ObtainCharacterImageSmall(character);
              if (bitmap != null && rectangle.Width > bitmap.Width)
                bitmap = this._CharacterImageCache.ObtainCharacterImage(character);
              using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(alpha, character.Empire.MainColor)))
                graphics.FillRectangle((Brush) solidBrush, rectangle);
              graphics.DrawImage((Image) bitmap, rectangle, new Rectangle(0, 0, bitmap.Width, bitmap.Height), GraphicsUnit.Pixel);
              this.AddHotspot(rectangle, (object) character, character.Name + " (" + Galaxy.ResolveDescription(character.Role) + ")");
            }
          }
          GraphicsHelper.SetGraphicsQuality(graphics, GraphicsQuality.Medium);
        }
        float readiness;
        if (this._Colony.Troops != null)
        {
          TroopList infantryTroops;
          TroopList armoredTroops;
          TroopList artilleryTroops;
          TroopList specialForcesTroops;
          this._Colony.Troops.SplitTroopsByType(true, out infantryTroops, out armoredTroops, out artilleryTroops, out specialForcesTroops);
          for (int index = 0; index < this._Colony.Troops.Count; ++index)
          {
            Troop troop = this._Colony.Troops[index];
            if (troop != null)
            {
              Rectangle rectangle = this.ResolveLocationDefendingTroop(troop, area, this._DefendingTroopsSpecialForcesX, num2, infantryTroops, armoredTroops, artilleryTroops, specialForcesTroops);
              Bitmap image = (Bitmap) null;
              switch (troop.Type)
              {
                case TroopType.Infantry:
                  image = this._TroopImagesInfantry[troop.PictureRef];
                  break;
                case TroopType.Armored:
                  image = this._TroopImagesArmored[troop.PictureRef];
                  break;
                case TroopType.Artillery:
                  image = this._TroopImagesArtillery[troop.PictureRef];
                  break;
                case TroopType.SpecialForces:
                  image = this._TroopImagesSpecialForces[troop.PictureRef];
                  break;
                case TroopType.PirateRaider:
                  image = this._TroopImagesPirateRaider[troop.PictureRef];
                  break;
              }
              rectangle = this.CorrectRectangleToImage(rectangle, image);
              Rectangle destRect3 = new Rectangle(rectangle.Right, rectangle.Y, rectangle.Width * -1, rectangle.Height);
              graphics.DrawImage((Image) image, destRect3, new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
              Rectangle region = rectangle;
              Troop relatedObject = troop;
              string text = TextResolver.GetText("Troop Description Defend NAME STRENGTH HEALTH");
              string name = troop.Name;
              string str1 = troop.OverallDefendStrength.ToString("0");
              readiness = troop.Readiness;
              string str2 = readiness.ToString("0") + "%";
              string message = string.Format(text, (object) name, (object) str1, (object) str2);
              this.AddHotspot(region, (object) relatedObject, message);
              int width = (int) ((double) troop.Readiness / 100.0 * (double) this._TroopSize.Width);
              if ((double) troop.Readiness < 100.0)
                graphics.FillRectangle((Brush) this._RedBrush, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, 2));
              graphics.FillRectangle((Brush) this._GreenBrush, new Rectangle(rectangle.X, rectangle.Y, width, 2));
            }
          }
        }
        if (this._Colony.InvadingTroops != null)
        {
          TroopList infantryTroops;
          TroopList armoredTroops;
          TroopList artilleryTroops;
          TroopList specialForcesTroops;
          this._Colony.InvadingTroops.SplitTroopsByType(true, out infantryTroops, out armoredTroops, out artilleryTroops, out specialForcesTroops);
          for (int index = 0; index < this._Colony.InvadingTroops.Count; ++index)
          {
            Troop invadingTroop = this._Colony.InvadingTroops[index];
            if (invadingTroop != null)
            {
              bool landing = false;
              Rectangle rectangle = this.ResolveLocationAttackingTroop(invadingTroop, area, this._AttackingForcesLowerAtmosphereX, num2, infantryTroops, armoredTroops, artilleryTroops, specialForcesTroops, out landing);
              Bitmap image = (Bitmap) null;
              if (landing)
              {
                image = this._AssaultPodImage;
              }
              else
              {
                switch (invadingTroop.Type)
                {
                  case TroopType.Infantry:
                    image = this._TroopImagesInfantry[invadingTroop.PictureRef];
                    break;
                  case TroopType.Armored:
                    image = this._TroopImagesArmored[invadingTroop.PictureRef];
                    break;
                  case TroopType.Artillery:
                    image = this._TroopImagesArtillery[invadingTroop.PictureRef];
                    break;
                  case TroopType.SpecialForces:
                    image = this._TroopImagesSpecialForces[invadingTroop.PictureRef];
                    break;
                  case TroopType.PirateRaider:
                    image = this._TroopImagesPirateRaider[invadingTroop.PictureRef];
                    break;
                }
              }
              rectangle = this.CorrectRectangleToImage(rectangle, image);
              graphics.DrawImage((Image) image, rectangle, new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
              Rectangle region = rectangle;
              Troop relatedObject = invadingTroop;
              string text = TextResolver.GetText("Troop Description Attack NAME STRENGTH HEALTH");
              string name = invadingTroop.Name;
              string str3 = invadingTroop.OverallAttackStrength.ToString("0");
              readiness = invadingTroop.Readiness;
              string str4 = readiness.ToString("0") + "%";
              string message = string.Format(text, (object) name, (object) str3, (object) str4);
              this.AddHotspot(region, (object) relatedObject, message);
              int width = (int) ((double) invadingTroop.Readiness / 100.0 * (double) this._TroopSize.Width);
              if ((double) invadingTroop.Readiness < 100.0)
                graphics.FillRectangle((Brush) this._RedBrush, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, 2));
              graphics.FillRectangle((Brush) this._GreenBrush, new Rectangle(rectangle.X, rectangle.Y, width, 2));
            }
          }
        }
        if (this._Colony.InvadingCharacters != null)
        {
          GraphicsHelper.SetGraphicsQuality(graphics, GraphicsQuality.High);
          for (int index = 0; index < this._Colony.InvadingCharacters.Count; ++index)
          {
            Character invadingCharacter = this._Colony.InvadingCharacters[index];
            if (invadingCharacter != null)
            {
              bool landing = false;
              Rectangle rectangle = this.ResolveLocationAttackingCharacter(invadingCharacter, area, out landing);
              Bitmap bitmap;
              if (landing)
              {
                bitmap = this._AssaultPodImage;
              }
              else
              {
                bitmap = this._CharacterImageCache.ObtainCharacterImageSmall(invadingCharacter);
                if (bitmap != null && rectangle.Width > bitmap.Width)
                  bitmap = this._CharacterImageCache.ObtainCharacterImage(invadingCharacter);
              }
              using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(alpha, invadingCharacter.Empire.MainColor)))
                graphics.FillRectangle((Brush) solidBrush, rectangle);
              graphics.DrawImage((Image) bitmap, rectangle, new Rectangle(0, 0, bitmap.Width, bitmap.Height), GraphicsUnit.Pixel);
              this.AddHotspot(rectangle, (object) invadingCharacter, invadingCharacter.Name + " (" + Galaxy.ResolveDescription(invadingCharacter.Role) + ")");
            }
          }
          GraphicsHelper.SetGraphicsQuality(graphics, GraphicsQuality.Medium);
        }
        using (Pen pen = new Pen(Color.FromArgb((int) byte.MaxValue, 170, 170, 170), 3f))
        {
          if (this._HoveredHotspot != null && this._HoveredHotspot.RelatedObject == (object) "resize")
            pen.Color = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
          int num12 = 18;
          int y = 6;
          int num13 = 6;
          Rectangle region = new Rectangle(this.Size.Width - (num12 + num13), y, num12, num12);
          if (this.PanelSize > 0)
          {
            graphics.DrawLine(pen, region.Left + 2, region.Top + 2, region.Left + 2, region.Bottom - 2);
            graphics.DrawLine(pen, region.Right - 2, region.Bottom - 2, region.Left + 2, region.Bottom - 2);
            graphics.DrawLine(pen, region.Right - 2, region.Top + 2, region.Left + 2, region.Bottom - 2);
            this.AddHotspot(region, (object) "resize", TextResolver.GetText("Shrink Screen"));
          }
          else
          {
            graphics.DrawLine(pen, region.Left + 2, region.Top + 2, region.Right - 2, region.Top + 2);
            graphics.DrawLine(pen, region.Right - 2, region.Bottom - 2, region.Right - 2, region.Top + 2);
            graphics.DrawLine(pen, region.Right - 2, region.Top + 2, region.Left + 2, region.Bottom - 2);
            this.AddHotspot(region, (object) "resize", TextResolver.GetText("Expand Screen"));
          }
        }
        this._FrontlineX = num2;
        if (this._HoveredHotspot != null)
        {
          Point location = new Point(this.Size.Width / 2, this._HeaderSize - 20);
          this.DrawStringWithDropShadowCentered(graphics, this._HoveredHotspot.HoverMessage, this._NormalFont, location, (Brush) this._YellowBrush);
          using (Pen pen = new Pen(Color.FromArgb(170, 170, 170), 1f))
          {
            pen.DashStyle = DashStyle.Dot;
            graphics.DrawRectangle(pen, this._HoveredHotspot.Region);
          }
        }
      }
      this.ProcessExplosions(time);
      using (new Pen(Color.FromArgb((int) byte.MaxValue, 0, 0), 1f))
      {
        GraphicsHelper.SetGraphicsQuality(graphics, GraphicsQuality.Low);
        foreach (Animation animation in this._AnimationSystem.GetAnimations())
        {
          if (animation != null && animation.ExtraData != null)
          {
            Point point1 = new Point((int) animation.Xpos + animation.Width / 2, (int) animation.Ypos + animation.Height / 2);
            Rectangle rectangle = this.ResolveLocation(animation.ExtraData, area);
            Point point2 = new Point(rectangle.X + rectangle.Width / 2, rectangle.Y + rectangle.Height / 2);
            float num14 = (float) (time.Subtract(animation.StartTime).TotalMilliseconds / ((double) animation.Images.Length / (double) animation.FramesPerSecond * 1000.0));
            int num15 = point2.X - (int) ((double) (point2.X - point1.X) * (double) num14);
            int num16 = point2.Y - (int) ((double) (point2.Y - point1.Y) * (double) num14);
            float angle1 = (float) Galaxy.DetermineAngle((double) point2.X, (double) point2.Y, (double) point1.X, (double) point1.Y);
            Bitmap image = (Bitmap) null;
            if (animation.ExtraData is Troop)
            {
              switch (((Troop) animation.ExtraData).Type)
              {
                case TroopType.Infantry:
                case TroopType.PirateRaider:
                  image = this._WeaponInfantryImage;
                  break;
                case TroopType.Armored:
                  image = this._WeaponArmoredImage;
                  break;
                case TroopType.Artillery:
                  image = this._WeaponPlanetaryDefenseImage;
                  break;
                case TroopType.SpecialForces:
                  image = this._WeaponSpecialForcesImage;
                  break;
              }
            }
            if (image != null)
            {
              int width = this._TroopSize.Width;
              Point point3 = new Point(num15 - image.Width / 2, num16 - image.Height / 2);
              float angle2 = angle1 * -1f;
              Bitmap bitmap = GraphicsHelper.RotateImage(image, angle2, GraphicsQuality.Medium);
              graphics.DrawImage((Image) bitmap, point3);
              bitmap.Dispose();
            }
          }
        }
        GraphicsHelper.SetGraphicsQualityToLow(graphics);
      }
      this._AnimationSystem.DoAnimations(graphics, time);
      this._AddHotspots = false;
    }

    private Rectangle CorrectRectangleToImage(Rectangle rectangle, Bitmap image)
    {
      float num = (float) image.Height / (float) image.Width;
      int height = (int) ((double) rectangle.Height * (double) num);
      return new Rectangle(rectangle.X, rectangle.Y + (rectangle.Height - height) / 2, rectangle.Width, height);
    }

    private void DrawStringWithDropShadowCentered(
      Graphics graphics,
      string text,
      Font font,
      Point location)
    {
      this.DrawStringWithDropShadowCentered(graphics, text, font, location, SizeF.Empty);
    }

    private void DrawStringWithDropShadowCentered(
      Graphics graphics,
      string text,
      Font font,
      Point location,
      Brush brush)
    {
      this.DrawStringWithDropShadowCentered(graphics, text, font, location, brush, SizeF.Empty);
    }

    private void DrawStringWithDropShadowCentered(
      Graphics graphics,
      string text,
      Font font,
      Point location,
      SizeF maxSize)
    {
      this.DrawStringWithDropShadowCentered(graphics, text, font, location, (Brush) this._WhiteBrush, maxSize);
    }

    private void DrawStringWithDropShadowCentered(
      Graphics graphics,
      string text,
      Font font,
      Point location,
      Brush brush,
      SizeF maxSize)
    {
      if (maxSize.IsEmpty)
      {
        SizeF sizeF = graphics.MeasureString(text, font, this.Size.Width);
        int x = location.X - (int) ((double) sizeF.Width / 2.0);
        this.DrawStringWithDropShadow(graphics, text, font, new Point(x, location.Y), brush);
      }
      else
      {
        SizeF sizeF = graphics.MeasureString(text, font, maxSize);
        int x = location.X - (int) ((double) sizeF.Width / 2.0);
        this.DrawStringWithDropShadow(graphics, text, font, new Point(x, location.Y), brush, maxSize);
      }
    }

    private void DrawStringWithDropShadow(
      Graphics graphics,
      string text,
      Font font,
      Point location)
    {
      this.DrawStringWithDropShadow(graphics, text, font, location, (Brush) this._WhiteBrush);
    }

    private void DrawStringWithDropShadow(
      Graphics graphics,
      string text,
      Font font,
      Point location,
      Brush brush)
    {
      this.DrawStringWithDropShadow(graphics, text, font, location, brush, SizeF.Empty);
    }

    private void DrawStringWithDropShadow(
      Graphics graphics,
      string text,
      Font font,
      Point location,
      Brush brush,
      SizeF maxSize)
    {
      if (maxSize != SizeF.Empty)
      {
        location = new Point(location.X + 1, location.Y + 1);
        RectangleF layoutRectangle = new RectangleF((float) location.X, (float) location.Y, maxSize.Width, maxSize.Height);
        graphics.DrawString(text, font, (Brush) this._BlackBrush, layoutRectangle, StringFormat.GenericTypographic);
        location = new Point(location.X - 1, location.Y - 1);
        layoutRectangle = new RectangleF((float) location.X, (float) location.Y, maxSize.Width, maxSize.Height);
        graphics.DrawString(text, font, brush, layoutRectangle, StringFormat.GenericTypographic);
      }
      else
      {
        location = new Point(location.X + 1, location.Y + 1);
        graphics.DrawString(text, font, (Brush) this._BlackBrush, (PointF) location);
        location = new Point(location.X - 1, location.Y - 1);
        graphics.DrawString(text, font, brush, (PointF) location);
      }
    }
  }
}
