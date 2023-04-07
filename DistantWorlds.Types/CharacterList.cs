// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.CharacterList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;

namespace DistantWorlds.Types
{
  [Serializable]
  public class CharacterList : SyncList<Character>
  {
    private new object _LockObject = new object();

    public bool ActivateAndRemoveCharacter(
      Character character,
      Galaxy galaxy,
      Empire empire,
      StellarObject location)
    {
      lock (this._LockObject)
      {
        if (this.Contains(character))
        {
          character.Activate(galaxy, empire, location);
          this.Remove(character);
          return true;
        }
      }
      return false;
    }

    public Character ObtainNextCharacter(CharacterRole role)
    {
      lock (this._LockObject)
      {
        Character nextCharacter = (Character) null;
        this.Sort();
        for (int index = 0; index < this.Count; ++index)
        {
          Character character = this[index];
          if (character != null && character.AppearanceOrder >= 0 && (role == CharacterRole.Undefined || character.Role == role))
          {
            nextCharacter = character;
            break;
          }
        }
        if (nextCharacter != null)
          this.Remove(nextCharacter);
        return nextCharacter;
      }
    }

    public CharacterList ObtainStartingCharactersExcludingRoles(List<CharacterRole> rolesToExclude)
    {
      lock (this._LockObject)
      {
        CharacterList charactersExcludingRoles = new CharacterList();
        for (int index = 0; index < this.Count; ++index)
        {
          Character character = this[index];
          if (character != null && character.AppearanceOrder == 0 && !rolesToExclude.Contains(character.Role))
            charactersExcludingRoles.Add(character);
        }
        for (int index = 0; index < charactersExcludingRoles.Count; ++index)
          this.Remove(charactersExcludingRoles[index]);
        return charactersExcludingRoles;
      }
    }

    public CharacterList ObtainStartingCharacters()
    {
      lock (this._LockObject)
      {
        CharacterList startingCharacters = new CharacterList();
        for (int index = 0; index < this.Count; ++index)
        {
          Character character = this[index];
          if (character != null && character.AppearanceOrder == 0)
            startingCharacters.Add(character);
        }
        for (int index = 0; index < startingCharacters.Count; ++index)
          this.Remove(startingCharacters[index]);
        return startingCharacters;
      }
    }

    public bool CheckCharactersOfEmpirePresent(Empire empire)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        Character character = this[index];
        if (character != null && character.Empire == empire)
          return true;
      }
      return false;
    }

    public CharacterList FindCharactersAtLocation(StellarObject location)
    {
      CharacterList charactersAtLocation = new CharacterList();
      for (int index = 0; index < this.Count; ++index)
      {
        Character character = this[index];
        if (character != null && character.Location == location)
          charactersAtLocation.Add(character);
      }
      return charactersAtLocation;
    }

    public CharacterList FindCharactersAtLocationNotTransferring(StellarObject location)
    {
      CharacterList locationNotTransferring = new CharacterList();
      for (int index = 0; index < this.Count; ++index)
      {
        Character character = this[index];
        if (character != null && character.Location == location && character.TransferDestination == null)
          locationNotTransferring.Add(character);
      }
      return locationNotTransferring;
    }

    public CharacterList FindCharactersAtLocationNotTransferring(
      StellarObject location,
      CharacterRole role)
    {
      CharacterList locationNotTransferring = new CharacterList();
      for (int index = 0; index < this.Count; ++index)
      {
        Character character = this[index];
        if (character != null && character.Role == role && character.Location == location && character.TransferDestination == null)
          locationNotTransferring.Add(character);
      }
      return locationNotTransferring;
    }

    public CharacterList FindCharactersAtLocationNotTransferring(
      StellarObject location,
      Empire empire,
      Character characterToExclude)
    {
      CharacterList locationNotTransferring = new CharacterList();
      for (int index = 0; index < this.Count; ++index)
      {
        Character character = this[index];
        if (character != null && character.Empire == empire && character.Location == location && character.TransferDestination == null && character != characterToExclude)
          locationNotTransferring.Add(character);
      }
      return locationNotTransferring;
    }

    public CharacterList FindCharactersAtLocationOrTransferring(StellarObject location)
    {
      CharacterList locationOrTransferring = new CharacterList();
      for (int index = 0; index < this.Count; ++index)
      {
        Character character = this[index];
        if (character != null)
        {
          if (character.Location == location)
          {
            if (character.TransferDestination == null)
              locationOrTransferring.Add(character);
          }
          else if (character.TransferDestination == location)
            locationOrTransferring.Add(character);
        }
      }
      return locationOrTransferring;
    }

    public Character GetCharacterWithHighestSkillLevelExcludeRole(
      CharacterSkillType skillType,
      CharacterRole roleToExclude)
    {
      Character levelExcludeRole = (Character) null;
      int num = -100;
      for (int index = 0; index < this.Count; ++index)
      {
        Character character = this[index];
        if (character != null && (roleToExclude == CharacterRole.Undefined || character.Role != roleToExclude) && (character.Skills.GetSkillByType(skillType) != null || character.TraitSkills.GetSkillByType(skillType) != null))
        {
          int skillLevel = character.GetSkillLevel(skillType);
          if (skillLevel > num)
          {
            levelExcludeRole = character;
            num = skillLevel;
          }
        }
      }
      if (num == -100)
        levelExcludeRole = (Character) null;
      return levelExcludeRole;
    }

    public int GetHighestSkillLevelExcludeRole(
      CharacterSkillType skillType,
      CharacterRole roleToExclude)
    {
      int levelExcludeRole = -100;
      bool flag = false;
      for (int index = 0; index < this.Count; ++index)
      {
        Character character = this[index];
        if (character != null && (roleToExclude == CharacterRole.Undefined || character.Role != roleToExclude) && (flag || character.BonusesKnown) && (character.Skills.GetSkillByType(skillType) != null || character.TraitSkills.GetSkillByType(skillType) != null))
        {
          int skillLevel = character.GetSkillLevel(skillType);
          if (skillLevel > levelExcludeRole)
            levelExcludeRole = skillLevel;
        }
      }
      if (levelExcludeRole == -100)
        levelExcludeRole = 0;
      return levelExcludeRole;
    }

    public int GetHighestSkillLevelExcludeLeaders(CharacterSkillType skillType)
    {
      int levelExcludeLeaders = -100;
      bool flag = false;
      for (int index = 0; index < this.Count; ++index)
      {
        Character character = this[index];
        if (character != null && character.Role != CharacterRole.Leader && character.Role != CharacterRole.PirateLeader && (flag || character.BonusesKnown) && (character.Skills.GetSkillByType(skillType) != null || character.TraitSkills.GetSkillByType(skillType) != null))
        {
          int skillLevel = this[index].GetSkillLevel(skillType);
          if (skillLevel > levelExcludeLeaders)
            levelExcludeLeaders = skillLevel;
        }
      }
      if (levelExcludeLeaders == -100)
        levelExcludeLeaders = 0;
      return levelExcludeLeaders;
    }

    public int GetHighestSkillLevelExcludeRoles(
      CharacterSkillType skillType,
      List<CharacterRole> rolesToExclude)
    {
      int levelExcludeRoles = -100;
      bool flag = false;
      for (int index = 0; index < this.Count; ++index)
      {
        Character character = this[index];
        if ((rolesToExclude.Count == 0 || !rolesToExclude.Contains(character.Role)) && (flag || character.BonusesKnown) && (character.Skills.GetSkillByType(skillType) != null || character.TraitSkills.GetSkillByType(skillType) != null))
        {
          int skillLevel = character.GetSkillLevel(skillType);
          if (skillLevel > levelExcludeRoles)
            levelExcludeRoles = skillLevel;
        }
      }
      if (levelExcludeRoles == -100)
        levelExcludeRoles = 0;
      return levelExcludeRoles;
    }

    public int GetHighestSkillLevel(CharacterSkillType skillType)
    {
      int highestSkillLevel = -100;
      for (int index = 0; index < this.Count; ++index)
      {
        Character character = this[index];
        if (character != null)
        {
          int skillLevel = character.GetSkillLevel(skillType);
          if (skillLevel > highestSkillLevel)
            highestSkillLevel = skillLevel;
        }
      }
      if (highestSkillLevel == -100)
        highestSkillLevel = 0;
      return highestSkillLevel;
    }

    public CharacterList GetCharactersNotIntelligenceAgents()
    {
      CharacterList intelligenceAgents = new CharacterList();
      for (int index = 0; index < this.Count; ++index)
      {
        Character character = this[index];
        if (character != null && character.Role != CharacterRole.IntelligenceAgent)
          intelligenceAgents.Add(character);
      }
      return intelligenceAgents;
    }

    public CharacterList GetAmbassadorsForEmpire(Empire empire)
    {
      CharacterList ambassadorsForEmpire = new CharacterList();
      if (empire != null && empire.Capital != null)
        ambassadorsForEmpire = this.FindCharactersAtLocationNotTransferring((StellarObject) empire.Capital);
      return ambassadorsForEmpire;
    }

    public CharacterList GetFleetAdmiralsAndGenerals(ShipGroup fleet)
    {
      CharacterList admiralsAndGenerals = new CharacterList();
      if (fleet != null)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          Character character = this[index];
          if (character != null && (character.Role == CharacterRole.FleetAdmiral || character.Role == CharacterRole.TroopGeneral || character.Role == CharacterRole.PirateLeader) && character.Location != null && character.Location is BuiltObject && ((BuiltObject) character.Location).ShipGroup == fleet)
            admiralsAndGenerals.Add(character);
        }
      }
      return admiralsAndGenerals;
    }

    public CharacterList GetNonTransferringCharacters() => this.GetNonTransferringCharacters(CharacterRole.Undefined);

    public CharacterList GetNonTransferringCharacters(CharacterRole role)
    {
      CharacterList transferringCharacters = new CharacterList();
      for (int index = 0; index < this.Count; ++index)
      {
        Character character = this[index];
        if (character != null && character.TransferDestination == null && (double) character.TransferTimeRemaining <= 0.0 && (role == CharacterRole.Undefined || character.Role == role))
          transferringCharacters.Add(character);
      }
      return transferringCharacters;
    }

    public CharacterList GetNonTransferringCharacters(
      CharacterRole role,
      Character characterToExclude)
    {
      CharacterList transferringCharacters = new CharacterList();
      for (int index = 0; index < this.Count; ++index)
      {
        Character character = this[index];
        if (character != null && character.TransferDestination == null && (double) character.TransferTimeRemaining <= 0.0 && (role == CharacterRole.Undefined || character.Role == role) && character != characterToExclude)
          transferringCharacters.Add(character);
      }
      return transferringCharacters;
    }

    public CharacterList GetCharactersWithTraits(List<CharacterTraitType> traits)
    {
      CharacterList charactersWithTraits = new CharacterList();
      for (int index1 = 0; index1 < this.Count; ++index1)
      {
        Character character = this[index1];
        if (character != null)
        {
          for (int index2 = 0; index2 < traits.Count; ++index2)
          {
            if (character.Traits.Contains(traits[index2]))
              charactersWithTraits.Add(character);
          }
        }
      }
      return charactersWithTraits;
    }

    public Character GetFirstByName(string characterName)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        Character firstByName = this[index];
        if (firstByName != null && firstByName.Name == characterName)
          return firstByName;
      }
      return (Character) null;
    }

    public Character GetFirstCharacterWithSkill(CharacterSkillType skillType)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        Character characterWithSkill = this[index];
        if (characterWithSkill != null && characterWithSkill.Skills.GetSkillByType(skillType) != null)
          return characterWithSkill;
      }
      return (Character) null;
    }

    public Character GetFirstCharacterWithTrait(CharacterTraitType trait)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        Character characterWithTrait = this[index];
        if (characterWithTrait != null && characterWithTrait.Traits.Contains(trait))
          return characterWithTrait;
      }
      return (Character) null;
    }

    public bool CheckCharactersForTrait(CharacterRole role, CharacterTraitType trait)
    {
      CharacterList charactersByRole = this.GetCharactersByRole(role);
      if (charactersByRole != null)
      {
        for (int index = 0; index < charactersByRole.Count; ++index)
        {
          if (charactersByRole[index].Traits.Contains(trait))
            return true;
        }
      }
      return false;
    }

    public CharacterList GetScientistsAtResearchStations(IndustryType industry)
    {
      CharacterList researchStations = new CharacterList();
      for (int index = 0; index < this.Count; ++index)
      {
        Character character = this[index];
        if (character != null && character.Role == CharacterRole.Scientist && character.Location != null && character.Location is BuiltObject)
        {
          BuiltObject location = (BuiltObject) character.Location;
          if (location != null)
          {
            if (industry == IndustryType.Undefined)
            {
              if (location.ResearchWeapons > 0 || location.ResearchEnergy > 0 || location.ResearchHighTech > 0)
                researchStations.Add(character);
            }
            else
            {
              if (industry == IndustryType.Weapon && location.ResearchWeapons > 0)
                researchStations.Add(character);
              if (industry == IndustryType.Energy && location.ResearchEnergy > 0)
                researchStations.Add(character);
              if (industry == IndustryType.HighTech && location.ResearchHighTech > 0)
                researchStations.Add(character);
            }
          }
        }
      }
      return researchStations;
    }

    public CharacterList GetCharactersByRole(CharacterRole role)
    {
      CharacterList charactersByRole = new CharacterList();
      for (int index = 0; index < this.Count; ++index)
      {
        Character character = this[index];
        if (character != null && character.Role == role)
          charactersByRole.Add(character);
      }
      return charactersByRole;
    }

    public int CountCharactersByRole(CharacterRole role)
    {
      int num = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        Character character = this[index];
        if (character != null && character.Role == role)
          ++num;
      }
      return num;
    }

    public double TotalDiminishingResearchBonusesWeapons()
    {
      double num = 0.0;
      if (this.Count > 0)
      {
        List<int> intList = new List<int>();
        for (int index = 0; index < this.Count; ++index)
        {
          Character character = this[index];
          intList.Add(character.ResearchWeapons);
        }
        Character[] arrayThreadSafe = ListHelper.ToArrayThreadSafe(this);
        Array.Sort<int, Character>(intList.ToArray(), arrayThreadSafe);
        Array.Reverse((Array) arrayThreadSafe);
        for (int index = 0; index < arrayThreadSafe.Length; ++index)
          num += (double) arrayThreadSafe[index].ResearchWeapons / (double) (index + 1);
        num /= 100.0;
      }
      return num;
    }

    public double TotalDiminishingResearchBonusesEnergy()
    {
      List<int> intList = new List<int>();
      for (int index = 0; index < this.Count; ++index)
      {
        Character character = this[index];
        intList.Add(character.ResearchEnergy);
      }
      Character[] arrayThreadSafe = ListHelper.ToArrayThreadSafe(this);
      Array.Sort<int, Character>(intList.ToArray(), arrayThreadSafe);
      Array.Reverse((Array) arrayThreadSafe);
      double num = 0.0;
      for (int index = 0; index < arrayThreadSafe.Length; ++index)
        num += (double) arrayThreadSafe[index].ResearchEnergy / (double) (index + 1);
      return num / 100.0;
    }

    public double TotalDiminishingResearchBonusesHighTech()
    {
      List<int> intList = new List<int>();
      for (int index = 0; index < this.Count; ++index)
      {
        Character character = this[index];
        intList.Add(character.ResearchHighTech);
      }
      Character[] arrayThreadSafe = ListHelper.ToArrayThreadSafe(this);
      Array.Sort<int, Character>(intList.ToArray(), arrayThreadSafe);
      Array.Reverse((Array) arrayThreadSafe);
      double num = 0.0;
      for (int index = 0; index < arrayThreadSafe.Length; ++index)
        num += (double) arrayThreadSafe[index].ResearchHighTech / (double) (index + 1);
      return num / 100.0;
    }

    public int GetHighestAppearanceOrder()
    {
      int highestAppearanceOrder = int.MinValue;
      foreach (Character character in (SyncList<Character>) this)
      {
        if (character.AppearanceOrder > highestAppearanceOrder)
          highestAppearanceOrder = character.AppearanceOrder;
      }
      return highestAppearanceOrder;
    }

    [Obsolete]
    public Character GetNextAppearanceCharacter(Race race)
    {
      this.Sort();
      foreach (Character appearanceCharacter in (SyncList<Character>) this)
      {
        if (appearanceCharacter.StartDate <= 0L && appearanceCharacter.Race == race)
          return appearanceCharacter;
      }
      return (Character) null;
    }
  }
}
