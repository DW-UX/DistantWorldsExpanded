// Decompiled with JetBrains decompiler
// Type: BaconDistantWorlds.BaconCharacter
// Assembly: BaconDistantWorlds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BEEE04E-F6B8-4927-96B6-91C74B502CC0
// Assembly location: H:\7\BaconDistantWorlds.dll

using DistantWorlds.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BaconDistantWorlds
{
  public static class BaconCharacter
  {
    public static int mySkillMultiplier = 3;
    public static float spyCaptureChance = 1f;
    public static int spyBaseValue = 25000;
    public static double spyBaseEscapeChance = 0.02;
    public static double spyBaseDefectChance = 0.02;

    public static int IncrementSkillProgress(Character character)
    {
      int num = 1;
      if (character.Empire != null && character.Empire.Name.Contains("Romulan"))
        num = BaconCharacter.mySkillMultiplier;
      return num;
    }

    public static bool Kill(Character character)
    {
      bool flag1 = true;
      if (character.Empire != null && character.Empire.Name.Contains("Romulan"))
      {
        if (character.Role == CharacterRole.IntelligenceAgent)
          flag1 = false;
        else if (character.Role == CharacterRole.Leader)
        {
          character.Role = CharacterRole.ColonyGovernor;
          flag1 = false;
        }
      }
      try
      {
        Random random = new Random();
        if ((double) BaconCharacter.spyCaptureChance > random.NextDouble())
        {
          string name = new StackFrame(2).GetMethod().Name;
          bool flag2 = false;
          bool flag3 = false;
          bool flag4 = false;
          Empire spyTargetEmpire = BaconCharacter.GetSpyTargetEmpire(character);
          if (name == "PerformIntelligenceMissions")
            flag2 = true;
          if (character.Empire == BaconBuiltObject.myMain._Game.PlayerEmpire)
            flag3 = true;
          if (BaconBuiltObject.myMain._Game.PlayerEmpire == spyTargetEmpire)
            flag4 = true;
          if (flag2)
          {
            if (!flag3 && !flag4)
            {
              int characterValue = BaconCharacter.GetCharacterValue(character);
              Empire empire = character.Empire;
              if (spyTargetEmpire != null && empire.StateMoney > (double) characterValue)
              {
                spyTargetEmpire.StateMoney += (double) characterValue;
                empire.StateMoney -= (double) characterValue;
                flag1 = false;
              }
            }
            else if (spyTargetEmpire.PirateEmpireBaseHabitat != null)
            {
              BuiltObject builtObject = spyTargetEmpire.BuiltObjects[0];
              if (builtObject.BaconValues == null)
                builtObject.BaconValues = new Dictionary<string, object>();
              List<Character> characterList = !builtObject.BaconValues.Keys.Contains<string>("capturedSpies") ? new List<Character>() : (List<Character>) builtObject.BaconValues["capturedSpies"];
              if (!characterList.Contains(character))
                characterList.Add(character);
              builtObject.BaconValues["capturedSpies"] = (object) characterList;
              if (character.Empire != null && character.Empire.Characters != null && character.Empire.Characters.Contains(character))
                character.Empire.Characters.Remove(character);
              if (character.Location != null && character.Location.Characters != null && character.Location.Characters.Contains(character))
                character.Location.Characters.Remove(character);
              flag1 = false;
            }
            else
            {
              Habitat capital = spyTargetEmpire.Capital;
              if (capital.BaconValues == null)
                capital.BaconValues = new Dictionary<string, object>();
              List<Character> characterList = !capital.BaconValues.Keys.Contains<string>("capturedSpies") ? new List<Character>() : (List<Character>) capital.BaconValues["capturedSpies"];
              if (!characterList.Contains(character))
                characterList.Add(character);
              capital.BaconValues["capturedSpies"] = (object) characterList;
              if (character.Empire != null && character.Empire.Characters != null && character.Empire.Characters.Contains(character))
                character.Empire.Characters.Remove(character);
              if (character.Location != null && character.Location.Characters != null && character.Location.Characters.Contains(character))
                character.Location.Characters.Remove(character);
              flag1 = false;
            }
            CharacterEvent character1 = BaconCharacter.AddEventToCharacter("Agent Captured", character.Name + " was captured by " + spyTargetEmpire.Name, character);
            character.EventHistory.Add(character1);
          }
        }
      }
      catch (Exception ex)
      {
      }
      return flag1;
    }

    public static int GetCharacterValue(Character character)
    {
      float num = 0.0f;
      double d = character.Empire.StateMoney / Convert.ToDouble(BaconCharacter.spyBaseValue);
      if (d > 1.0)
        d = Math.Sqrt(d);
      foreach (CharacterSkill skill in (List<CharacterSkill>) character.Skills)
        num += (float) skill.Level;
      return Math.Max(BaconCharacter.spyBaseValue / 2, (int) ((1.0 + (double) num / 100.0) * d * (double) BaconCharacter.spyBaseValue));
    }

    public static Empire GetSpyTargetEmpire(Character spy)
    {
      Empire spyTargetEmpire = (Empire) null;
      if (spy.Mission == null)
        return spyTargetEmpire;
      IntelligenceMission mission = spy.Mission;
      if (mission.TargetEmpire != null)
        spyTargetEmpire = mission.TargetEmpire;
      else if (mission.Target is BuiltObject)
        spyTargetEmpire = (mission.Target as BuiltObject).ActualEmpire;
      else if (mission.Target is Character)
        spyTargetEmpire = (mission.Target as Character).Empire;
      else if (mission.Target is Habitat)
        spyTargetEmpire = (mission.Target as Habitat).Empire;
      else if (mission.Target is Empire)
        spyTargetEmpire = mission.Target as Empire;
      return spyTargetEmpire;
    }

    public static CharacterEvent AddEventToCharacter(
      string title,
      string description,
      Character character)
    {
      return new CharacterEvent(CharacterEventType.IntelligenceAgentOursCaptured, (object) new Dictionary<string, object>()
      {
        {
          "spyMessage",
          (object) new List<string>() { title, description }
        }
      }, BaconBuiltObject.myMain._Game.Galaxy.CurrentStarDate);
    }
  }
}
