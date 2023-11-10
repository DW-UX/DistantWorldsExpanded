// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.CharacterDropDown
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class CharacterDropDown : ComboBox
  {
    private CharacterList _Characters;
    private bool _AllowNullSelection;
    private bool _AllowRoleSelection;
    private CharacterImageCache _CharacterImageCache;
    private Empire _Empire;
    private Galaxy _Galaxy;
    private List<CharacterRole> _CharacterRoles = new List<CharacterRole>();

    public CharacterDropDown()
    {
      this.DrawMode = DrawMode.OwnerDrawFixed;
      this.DropDownStyle = ComboBoxStyle.DropDownList;
      this.FlatStyle = FlatStyle.Popup;
      this.BackColor = Color.FromArgb(48, 48, 64);
      this.ForeColor = Color.FromArgb(170, 170, 170);
      this.Font = new Font("Verdana", 8.25f);
    }

    public void ClearData()
    {
      this._Characters = (CharacterList) null;
      this._CharacterImageCache = (CharacterImageCache) null;
      this._Empire = (Empire) null;
      this._Galaxy = (Galaxy) null;
    }

    public void BindData(
      Empire empire,
      CharacterList characters,
      CharacterImageCache characterImageCache,
      Galaxy galaxy,
      bool allowRoleSelection,
      bool allowNullSelection)
    {
      this._Empire = empire;
      this._Characters = characters;
      this._AllowRoleSelection = allowRoleSelection;
      this._AllowNullSelection = allowNullSelection;
      this._Galaxy = galaxy;
      this._CharacterImageCache = characterImageCache;
      this.Items.Clear();
      if (this._AllowNullSelection)
        this.Items.Add((object) "");
      if (this._AllowRoleSelection)
        this.AddRoleSelectionItems();
      if (this._Characters == null)
        return;
      this._Characters.Sort();
      this.Items.AddRange((object[]) this._Characters.ToArray());
    }

    private void AddRoleSelectionItems()
    {
      this._CharacterRoles = new List<CharacterRole>();
      this._CharacterRoles.Add(CharacterRole.Leader);
      this._CharacterRoles.Add(CharacterRole.Ambassador);
      this._CharacterRoles.Add(CharacterRole.ColonyGovernor);
      this._CharacterRoles.Add(CharacterRole.FleetAdmiral);
      this._CharacterRoles.Add(CharacterRole.TroopGeneral);
      this._CharacterRoles.Add(CharacterRole.Scientist);
      this._CharacterRoles.Add(CharacterRole.IntelligenceAgent);
      this._CharacterRoles.Add(CharacterRole.PirateLeader);
      this._CharacterRoles.Add(CharacterRole.ShipCaptain);
      for (int index = 0; index < this._CharacterRoles.Count; ++index)
        this.Items.Add((object) this._CharacterRoles[index]);
    }

    public void SetSelectedCharacter(CharacterRole characterRole)
    {
      if (characterRole != CharacterRole.Undefined)
      {
        if (this._AllowRoleSelection)
        {
          int num = this._CharacterRoles.IndexOf(characterRole);
          if (this._AllowNullSelection)
          {
            if (num >= 0)
              ++num;
            else
              num = 0;
          }
          this.SelectedIndex = num;
        }
        else if (this._AllowNullSelection)
          this.SelectedIndex = 0;
        else
          this.SelectedIndex = -1;
      }
      else if (this._AllowNullSelection)
        this.SelectedIndex = 0;
      else
        this.SelectedIndex = -1;
    }

    public void SetSelectedCharacter(Character character)
    {
      if (character != null)
      {
        int num = -1;
        if (this._Characters != null)
          num = this._Characters.IndexOf(character);
        if (this._AllowNullSelection)
        {
          if (num >= 0)
            ++num;
          else
            num = 0;
        }
        if (this._AllowRoleSelection && num >= 0)
          num += this._CharacterRoles.Count;
        this.SelectedIndex = num;
      }
      else if (this._AllowNullSelection)
        this.SelectedIndex = 0;
      else
        this.SelectedIndex = -1;
    }

    public CharacterRole GetSelectedCharacterRole()
    {
      CharacterRole characterRole = CharacterRole.Undefined;
      this.GetSelectedCharacter(out characterRole);
      return characterRole;
    }

    public Character GetSelectedCharacter()
    {
      CharacterRole characterRole = CharacterRole.Undefined;
      return this.GetSelectedCharacter(out characterRole);
    }

    public Character GetSelectedCharacter(out CharacterRole characterRole)
    {
      characterRole = CharacterRole.Undefined;
      int selectedIndex = this.SelectedIndex;
      int num1 = 0;
      int num2 = 0;
      if (this._AllowNullSelection)
      {
        ++num1;
        ++num2;
        if (selectedIndex == 0)
          return (Character) null;
      }
      if (this._AllowRoleSelection)
      {
        num1 += this._CharacterRoles.Count;
        if (selectedIndex >= num2 && selectedIndex < num2 + this._CharacterRoles.Count)
        {
          characterRole = this._CharacterRoles[selectedIndex - num2];
          return (Character) null;
        }
      }
      if (selectedIndex < 0)
        return (Character) null;
      int index = selectedIndex - num1;
      return this._Characters != null && index >= 0 && index < this._Characters.Count ? this._Characters[index] : (Character) null;
    }

    protected override void OnMeasureItem(MeasureItemEventArgs e)
    {
      base.OnMeasureItem(e);
      e.ItemHeight = 19;
    }

    protected override void OnDrawItem(DrawItemEventArgs e)
    {
      base.OnDrawItem(e);
      e.DrawBackground();
      PointF point = new PointF(60f, (float) (e.Bounds.Y + 1));
      Font font = this.Font;
      SolidBrush solidBrush = new SolidBrush(this.ForeColor);
      if (e.Index >= 0)
      {
        int num1 = 0;
        int num2 = 0;
        if (this._AllowNullSelection)
        {
          ++num1;
          ++num2;
        }
        CharacterRole role = CharacterRole.Undefined;
        if (this._AllowRoleSelection)
        {
          num1 += this._CharacterRoles.Count;
          int index = e.Index - num2;
          if (index >= 0 && index < this._CharacterRoles.Count)
            role = this._CharacterRoles[index];
        }
        if (this._AllowNullSelection && e.Index == 0)
          e.Graphics.DrawString("(" + TextResolver.GetText("None") + ")", font, (Brush) solidBrush, point);
        else if (role != CharacterRole.Undefined)
        {
          string s = "(" + string.Format(TextResolver.GetText("Random CHARACTER ROLE"), (object) Galaxy.ResolveDescription(role)) + ")";
          e.Graphics.DrawString(s, font, (Brush) solidBrush, point);
          Rectangle rect = new Rectangle();
          if (this._Empire != null && this._Empire != this._Galaxy.IndependentEmpire)
          {
            Bitmap largeFlagPicture = this._Empire.LargeFlagPicture;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            int height = e.Bounds.Height - 2;
            double num3 = (double) height / (double) largeFlagPicture.Height;
            int width = (int) ((double) largeFlagPicture.Width * num3);
            rect = new Rectangle(3, e.Bounds.Y + 1, width, height);
            e.Graphics.DrawImage((Image) largeFlagPicture, rect);
          }
          Bitmap roleIcon = this._CharacterImageCache.GetRoleIcon(role);
          if (roleIcon != null)
          {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            int height = e.Bounds.Height - 2;
            double num4 = (double) height / (double) roleIcon.Height;
            int width = (int) ((double) roleIcon.Width * num4);
            rect = new Rectangle(38, e.Bounds.Y + 1, width, height);
            e.Graphics.DrawImage((Image) roleIcon, rect);
          }
        }
        else
        {
          int index = e.Index - num1;
          if (this._Characters != null && index >= 0 && index < this._Characters.Count)
          {
            Character character = this._Characters[index];
            if (character != null)
            {
              e.Graphics.DrawString(character.Name, font, (Brush) solidBrush, point);
              Rectangle rect = new Rectangle();
              if (character.Empire != null && character.Empire != this._Galaxy.IndependentEmpire)
              {
                Bitmap largeFlagPicture = character.Empire.LargeFlagPicture;
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                int height = e.Bounds.Height - 2;
                double num5 = (double) height / (double) largeFlagPicture.Height;
                int width = (int) ((double) largeFlagPicture.Width * num5);
                rect = new Rectangle(3, e.Bounds.Y + 1, width, height);
                e.Graphics.DrawImage((Image) largeFlagPicture, rect);
              }
              Bitmap characterImageVerySmall = this._CharacterImageCache.ObtainCharacterImageVerySmall(character);
              if (characterImageVerySmall != null)
                e.Graphics.DrawImage((Image) characterImageVerySmall, new Point(38, e.Bounds.Y + 1));
            }
          }
        }
      }
      e.DrawFocusRectangle();
    }
  }
}
