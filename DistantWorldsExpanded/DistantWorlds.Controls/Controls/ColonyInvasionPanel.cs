// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.ColonyInvasionPanel
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace DistantWorlds.Controls
{
  public class ColonyInvasionPanel : Panel
  {
    private ColonyInvasion _ColonyInvasion = new ColonyInvasion();
    private Galaxy _Galaxy;
    private DateTime _LastUpdate = DateTime.MinValue;
    private Size _ScreenSize = new Size(1024, 768);
    private Bitmap _AssaultPodImage;
    private Bitmap _WeaponInfantryImage;
    private Bitmap _WeaponArmoredImage;
    private Bitmap _WeaponPlanetaryDefenseImage;
    private Bitmap _WeaponSpecialForcesImage;

    public ColonyInvasionPanel() => this.Font = new Font("Verdana", 8f);

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
      if (this._ColonyInvasion == null)
        return;
      this._AssaultPodImage = assaultPodImage;
      this._WeaponInfantryImage = weaponInfantryImage;
      this._WeaponArmoredImage = weaponArmoredImage;
      this._WeaponPlanetaryDefenseImage = weaponPlanetaryDefenseImage;
      this._WeaponSpecialForcesImage = weaponSpecialForcesImage;
      this._ColonyInvasion.InitializeImages(troopImagesInfantry, troopImagesArmored, troopImagesArtillery, troopImagesSpecialForces, troopImagesPirateRaider, facilityImages, builtObjectImageCache, characterImageCache, raceImages, explosionImages, spaceImage, assaultPodImage, weaponInfantryImage, weaponArmoredImage, weaponPlanetaryDefenseImage, weaponSpecialForcesImage, applicationStartupPath, customizationSetName);
    }

    public void BindData(
      Galaxy galaxy,
      Habitat colony,
      Font normalFont,
      Font normalBoldFont,
      Font largeFont,
      Font hugeFont,
      Size screenSize)
    {
      this._ScreenSize = screenSize;
      this._Galaxy = galaxy;
      this._LastUpdate = galaxy.CurrentDateTime;
      this._ColonyInvasion.BindData(colony, normalFont, normalBoldFont, largeFont, hugeFont);
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
    }

    public ColonyInvasion ColonyInvasion => this._ColonyInvasion;

    public void ClearColony()
    {
      if (this._ColonyInvasion == null)
        return;
      this._ColonyInvasion.ClearColony();
    }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new Size Size
    {
      get => base.Size;
      set
      {
        base.Size = value;
        if (this._ColonyInvasion == null)
          return;
        this._ColonyInvasion.Size = value;
      }
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
    }

    private void CheckUpdateBattle(DateTime time)
    {
      TimeSpan timePassed = time.Subtract(this._LastUpdate);
      if (this._Galaxy == null || this._ColonyInvasion == null || timePassed.TotalSeconds < 1.0)
        return;
      this._ColonyInvasion.Update(this._Galaxy, timePassed);
      this._LastUpdate = time;
    }

    private void UpdateHovering()
    {
      if (this._ColonyInvasion == null)
        return;
      Point client = this.PointToClient(MouseHelper.GetCursorPosition());
      this._ColonyInvasion.CheckHovered(client.X, client.Y);
    }

    protected override void OnClick(EventArgs e)
    {
      base.OnClick(e);
      if (this._ColonyInvasion == null || this._ColonyInvasion.HoveredHotspot == null)
        return;
      if (this._ColonyInvasion.HoveredHotspot.RelatedObject == (object) "close")
      {
        this.Visible = false;
        this.SendToBack();
        this.ClearColony();
      }
      else
      {
        if (this._ColonyInvasion.HoveredHotspot.RelatedObject != (object) "resize")
          return;
        if (this._ColonyInvasion.PanelSize > 0)
        {
          this._ColonyInvasion.ResetSize(0, this._AssaultPodImage, this._WeaponInfantryImage, this._WeaponArmoredImage, this._WeaponPlanetaryDefenseImage, this._WeaponSpecialForcesImage);
          int width = 555;
          int height = 520;
          Point point = new Point(this.Parent.Parent.Parent.Controls["pnlInfoPanel"].Right + 20, this.Parent.Parent.Parent.ClientRectangle.Height - (height + 70 + 10));
          this.Size = new Size(width, height);
          this.Parent.Parent.Location = point;
          this.Parent.Parent.Size = new Size(width + 25, height + 70);
          this._ColonyInvasion.Size = new Size(width, height);
          ((ScreenPanel) this.Parent.Parent).DoLayout();
          this.Parent.Parent.Invalidate();
        }
        else if (this._ScreenSize.Width < 1120)
        {
          this._ColonyInvasion.ResetSize(0, this._AssaultPodImage, this._WeaponInfantryImage, this._WeaponArmoredImage, this._WeaponPlanetaryDefenseImage, this._WeaponSpecialForcesImage);
          int width = 555;
          int height = 520;
          Point point = new Point(this.Parent.Parent.Parent.Controls["pnlInfoPanel"].Right + 20, this.Parent.Parent.Parent.ClientRectangle.Height - (height + 70 + 10));
          this.Size = new Size(width, height);
          this.Parent.Parent.Location = point;
          this.Parent.Parent.Size = new Size(width + 25, height + 70);
          this._ColonyInvasion.Size = new Size(width, height);
          ((ScreenPanel) this.Parent.Parent).DoLayout();
          this.Parent.Parent.Invalidate();
        }
        else if (this._ScreenSize.Width < 1320 || this._ScreenSize.Height < 820)
        {
          this._ColonyInvasion.ResetSize(1, this._AssaultPodImage, this._WeaponInfantryImage, this._WeaponArmoredImage, this._WeaponPlanetaryDefenseImage, this._WeaponSpecialForcesImage);
          int width = 725;
          int height = 647;
          Point point = new Point(this.Parent.Parent.Parent.Controls["pnlInfoPanel"].Right + 20, this.Parent.Parent.Parent.ClientRectangle.Height - (height + 70 + 10));
          this.Size = new Size(width, height);
          this.Parent.Parent.Location = point;
          this.Parent.Parent.Size = new Size(width + 25, height + 70);
          this._ColonyInvasion.Size = new Size(width, height);
          ((ScreenPanel) this.Parent.Parent).DoLayout();
          this.Parent.Parent.Invalidate();
        }
        else
        {
          this._ColonyInvasion.ResetSize(2, this._AssaultPodImage, this._WeaponInfantryImage, this._WeaponArmoredImage, this._WeaponPlanetaryDefenseImage, this._WeaponSpecialForcesImage);
          int width = 910;
          int height = 786;
          Point point = new Point(this.Parent.Parent.Parent.Controls["pnlInfoPanel"].Right + 20, this.Parent.Parent.Parent.ClientRectangle.Height - (height + 70 + 10));
          this.Size = new Size(width, height);
          this.Parent.Parent.Location = point;
          this.Parent.Parent.Size = new Size(width + 25, height + 70);
          this._ColonyInvasion.Size = new Size(width, height);
          ((ScreenPanel) this.Parent.Parent).DoLayout();
          this.Parent.Parent.Invalidate();
        }
      }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      try
      {
        if (this._ColonyInvasion != null && this._Galaxy != null)
        {
          DateTime currentDateTime = this._Galaxy.CurrentDateTime;
          this.CheckUpdateBattle(currentDateTime);
          this.UpdateHovering();
          this._ColonyInvasion.Draw(e.Graphics, currentDateTime);
        }
        else
          base.OnPaint(e);
      }
      catch (Exception ex)
      {
      }
    }
  }
}
