using BaconDistantWorlds;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace DistantWorlds.Types
{
    public partial class Galaxy
    {
        public static string ResolveDescription(CharacterEvent characterEvent, Empire callingEmpire, out string title)
        {
            title = string.Empty;
            string text = string.Empty;
            if (characterEvent != null)
            {
                switch (characterEvent.Type)
                {
                    case CharacterEventType.Boarding:
                        if (characterEvent.EventData != null && characterEvent.EventData is BuiltObject)
                        {
                            BuiltObject builtObject8 = (BuiltObject)characterEvent.EventData;
                            if (builtObject8 != null)
                            {
                                title = string.Format(TextResolver.GetText("Character Event Title Boarding"), builtObject8.Name);
                                text = string.Format(TextResolver.GetText("Character Event Description Boarding"), builtObject8.Name);
                            }
                        }
                        break;
                    case CharacterEventType.Raid:
                        if (characterEvent.EventData == null)
                        {
                            break;
                        }
                        if (characterEvent.EventData is Habitat)
                        {
                            Habitat habitat3 = (Habitat)characterEvent.EventData;
                            if (habitat3 != null)
                            {
                                title = string.Format(TextResolver.GetText("Character Event Title Raid"), habitat3.Name);
                                text = string.Format(TextResolver.GetText("Character Event Description Raid"), habitat3.Name);
                            }
                        }
                        else if (characterEvent.EventData is BuiltObject)
                        {
                            BuiltObject builtObject3 = (BuiltObject)characterEvent.EventData;
                            if (builtObject3 != null)
                            {
                                title = string.Format(TextResolver.GetText("Character Event Title Raid"), builtObject3.Name);
                                text = string.Format(TextResolver.GetText("Character Event Description Raid"), builtObject3.Name);
                            }
                        }
                        break;
                    case CharacterEventType.SmugglingSuccess:
                        if (characterEvent.EventData != null && characterEvent.EventData is StellarObject)
                        {
                            StellarObject stellarObject4 = (StellarObject)characterEvent.EventData;
                            if (stellarObject4 != null)
                            {
                                title = string.Format(TextResolver.GetText("Character Event Title Smuggling Success"), stellarObject4.Name);
                                text = string.Format(TextResolver.GetText("Character Event Description Smuggling Success"), stellarObject4.Name);
                            }
                        }
                        break;
                    case CharacterEventType.SmugglingDetection:
                        if (characterEvent.EventData != null && characterEvent.EventData is StellarObject)
                        {
                            StellarObject stellarObject = (StellarObject)characterEvent.EventData;
                            if (stellarObject != null)
                            {
                                title = string.Format(TextResolver.GetText("Character Event Title Smuggling Detection"), stellarObject.Name);
                                text = string.Format(TextResolver.GetText("Character Event Description Smuggling Detection"), stellarObject.Name);
                            }
                        }
                        break;
                    case CharacterEventType.CriticalResearchFailure:
                        if (characterEvent.EventData != null && characterEvent.EventData is ResearchNode)
                        {
                            ResearchNode researchNode = (ResearchNode)characterEvent.EventData;
                            if (researchNode != null)
                            {
                                text = string.Format(TextResolver.GetText("Character Event Description Critical Research Failure"), researchNode.Name);
                            }
                        }
                        break;
                    case CharacterEventType.CriticalResearchSuccess:
                        if (characterEvent.EventData != null && characterEvent.EventData is ResearchNode)
                        {
                            ResearchNode researchNode3 = (ResearchNode)characterEvent.EventData;
                            if (researchNode3 != null)
                            {
                                text = string.Format(TextResolver.GetText("Character Event Description Critical Research Success"), researchNode3.Name);
                            }
                        }
                        break;
                    case CharacterEventType.Subjugated:
                        if (characterEvent.EventData != null && characterEvent.EventData is Empire)
                        {
                            Empire empire = (Empire)characterEvent.EventData;
                            if (empire != null)
                            {
                                title = string.Format(TextResolver.GetText("Character Event Title Subjugated"), empire.Name);
                                text = string.Format(TextResolver.GetText("Character Event Description Subjugated"), empire.Name);
                            }
                        }
                        break;
                    case CharacterEventType.TreatyBroken:
                        if (characterEvent.EventData != null && characterEvent.EventData is Empire)
                        {
                            Empire empire4 = (Empire)characterEvent.EventData;
                            if (empire4 != null)
                            {
                                title = string.Format(TextResolver.GetText("Character Event Title Treaty Broken"), empire4.Name);
                                text = string.Format(TextResolver.GetText("Character Event Description Treaty Broken"), empire4.Name);
                            }
                        }
                        break;
                    case CharacterEventType.AmbassadorAssignedToEmpire:
                        if (characterEvent.EventData != null && characterEvent.EventData is Empire)
                        {
                            Empire empire2 = (Empire)characterEvent.EventData;
                            if (empire2 != null)
                            {
                                title = string.Format(TextResolver.GetText("Character Event Title Ambassador Assigned To Empire"), empire2.Name);
                                text = string.Format(TextResolver.GetText("Character Event Description Ambassador Assigned To Empire"), empire2.Name);
                            }
                        }
                        break;
                    case CharacterEventType.TargetOfFailedAssassination:
                        text = TextResolver.GetText("Character Event Description Target Of Failed Assassination");
                        break;
                    case CharacterEventType.BuildMilitaryShip:
                    case CharacterEventType.BuildCivilianShip:
                    case CharacterEventType.BuildColonyShip:
                        if (characterEvent.EventData != null && characterEvent.EventData is BuiltObject)
                        {
                            BuiltObject builtObject = (BuiltObject)characterEvent.EventData;
                            if (builtObject != null)
                            {
                                title = string.Format(TextResolver.GetText("Character Event Title ShipBase Built"), ResolveDescription(builtObject.SubRole));
                                text = string.Format(TextResolver.GetText("Character Event Description Build Ship"), ResolveDescription(builtObject.SubRole), builtObject.Name);
                            }
                        }
                        break;
                    case CharacterEventType.BuildMilitaryBase:
                    case CharacterEventType.BuildResearchStationWeapons:
                    case CharacterEventType.BuildResearchStationEnergy:
                    case CharacterEventType.BuildResearchStationHighTech:
                    case CharacterEventType.BuildMiningStation:
                    case CharacterEventType.BuildResortBase:
                    case CharacterEventType.BuildOtherBase:
                        if (characterEvent.EventData != null && characterEvent.EventData is BuiltObject)
                        {
                            BuiltObject builtObject4 = (BuiltObject)characterEvent.EventData;
                            if (builtObject4 != null)
                            {
                                title = string.Format(TextResolver.GetText("Character Event Title ShipBase Built"), ResolveDescription(builtObject4.SubRole));
                                text = string.Format(TextResolver.GetText("Character Event Description Build Base"), ResolveDescription(builtObject4.SubRole), builtObject4.Name);
                            }
                        }
                        break;
                    case CharacterEventType.BuildSpaceport:
                        if (characterEvent.EventData != null && characterEvent.EventData is BuiltObject)
                        {
                            BuiltObject builtObject2 = (BuiltObject)characterEvent.EventData;
                            if (builtObject2 != null)
                            {
                                title = string.Format(TextResolver.GetText("Character Event Title ShipBase Built"), builtObject2.Name);
                                text = string.Format(TextResolver.GetText("Character Event Description Build Spaceport"), ResolveDescription(builtObject2.SubRole), builtObject2.Name);
                            }
                        }
                        break;
                    case CharacterEventType.CashNegative:
                        text = TextResolver.GetText("Character Event Description Cash Negative");
                        break;
                    case CharacterEventType.CashPositive:
                        text = TextResolver.GetText("Character Event Description Cash Positive");
                        break;
                    case CharacterEventType.ColonyDevelopmentIncrease:
                    case CharacterEventType.ColonyDevelopmentDecrease:
                        if (characterEvent.EventData != null && characterEvent.EventData is Habitat)
                        {
                            Habitat habitat2 = (Habitat)characterEvent.EventData;
                            if (habitat2 != null)
                            {
                                text = string.Format(TextResolver.GetText("Character Event Description Colony Development Change"), habitat2.Name);
                            }
                        }
                        break;
                    case CharacterEventType.HyperjumpExit:
                        if (characterEvent.EventData != null && characterEvent.EventData is BuiltObject)
                        {
                            BuiltObject builtObject6 = (BuiltObject)characterEvent.EventData;
                            if (builtObject6 != null)
                            {
                                text = string.Format(TextResolver.GetText("Character Event Description Hyperjump Exit"), ResolveDescription(builtObject6.SubRole), builtObject6.Name);
                            }
                        }
                        break;
                    case CharacterEventType.IntelligenceAgentRecruited:
                        if (characterEvent.EventData != null && characterEvent.EventData is Character)
                        {
                            Character character3 = (Character)characterEvent.EventData;
                            if (character3 != null)
                            {
                                text = string.Format(TextResolver.GetText("Character Event Description Intelligence Agent Recruited"), character3.Name);
                            }
                        }
                        break;
                    case CharacterEventType.CharacterTransferLocation:
                        {
                            if (characterEvent.EventData == null || !(characterEvent.EventData is object[]))
                            {
                                break;
                            }
                            object[] array = (object[])characterEvent.EventData;
                            if (array.Length != 2)
                            {
                                break;
                            }
                            Character character4 = null;
                            StellarObject stellarObject2 = null;
                            if (array[0] != null && array[0] is Character)
                            {
                                character4 = (Character)array[0];
                            }
                            string text2 = string.Empty;
                            if (array[1] != null && array[1] is StellarObject)
                            {
                                stellarObject2 = (StellarObject)array[1];
                                text2 = stellarObject2.Name;
                                if (stellarObject2 is BuiltObject)
                                {
                                    BuiltObject builtObject5 = (BuiltObject)stellarObject2;
                                    if (builtObject5 != null && builtObject5.Role != BuiltObjectRole.Base)
                                    {
                                        text2 = ResolveDescription(builtObject5.SubRole) + " " + builtObject5.Name;
                                        if (builtObject5.ShipGroup != null)
                                        {
                                            text2 = text2 + " (" + builtObject5.ShipGroup.Name + ")";
                                        }
                                    }
                                }
                            }
                            if (character4 != null && stellarObject2 != null)
                            {
                                text = string.Format(TextResolver.GetText("Character Event Description Transfer Location"), character4.Name, text2);
                            }
                            break;
                        }
                    case CharacterEventType.CharacterStart:
                        {
                            if (characterEvent.EventData == null || !(characterEvent.EventData is object[]))
                            {
                                break;
                            }
                            object[] array2 = (object[])characterEvent.EventData;
                            if (array2.Length != 2)
                            {
                                break;
                            }
                            Character character5 = null;
                            StellarObject stellarObject3 = null;
                            if (array2[0] != null && array2[0] is Character)
                            {
                                character5 = (Character)array2[0];
                            }
                            string text3 = string.Empty;
                            if (array2[1] != null && array2[1] is StellarObject)
                            {
                                stellarObject3 = (StellarObject)array2[1];
                                text3 = stellarObject3.Name;
                                if (stellarObject3 is BuiltObject)
                                {
                                    BuiltObject builtObject7 = (BuiltObject)stellarObject3;
                                    if (builtObject7 != null && builtObject7.Role != BuiltObjectRole.Base)
                                    {
                                        text3 = ResolveDescription(builtObject7.SubRole) + " " + builtObject7.Name;
                                        if (builtObject7.ShipGroup != null)
                                        {
                                            text3 = text3 + " (" + builtObject7.ShipGroup.Name + ")";
                                        }
                                    }
                                }
                            }
                            if (character5 != null && stellarObject3 != null)
                            {
                                text = string.Format(TextResolver.GetText("Character Event Description Start"), ResolveDescription(character5.Role), character5.Name, text3);
                            }
                            break;
                        }
                    case CharacterEventType.CharacterTraitGain:
                        if (characterEvent.EventData != null && characterEvent.EventData is CharacterTraitType)
                        {
                            CharacterTraitType characterTraitType = (CharacterTraitType)characterEvent.EventData;
                            if (characterTraitType != 0)
                            {
                                text = string.Format(TextResolver.GetText("Character Event Description Trait Gain"), ResolveDescription(characterTraitType));
                            }
                        }
                        break;
                    case CharacterEventType.CharacterSkillGain:
                        if (characterEvent.EventData != null && characterEvent.EventData is CharacterSkill)
                        {
                            CharacterSkill characterSkill = (CharacterSkill)characterEvent.EventData;
                            if (characterSkill != null)
                            {
                                text = string.Format(TextResolver.GetText("Character Event Description Skill Gain"), ResolveDescription(characterSkill.Type));
                            }
                        }
                        break;
                    case CharacterEventType.CharacterSkillProgress:
                        if (characterEvent.EventData != null && characterEvent.EventData is CharacterSkill)
                        {
                            CharacterSkill characterSkill2 = (CharacterSkill)characterEvent.EventData;
                            if (characterSkill2 != null)
                            {
                                text = string.Format(TextResolver.GetText("Character Event Description Skill Progress"), ResolveDescription(characterSkill2.Type));
                            }
                        }
                        break;
                    case CharacterEventType.ResearchAdvanceWeapons:
                    case CharacterEventType.ResearchAdvanceEnergy:
                    case CharacterEventType.ResearchAdvanceHighTech:
                        if (characterEvent.EventData != null && characterEvent.EventData is ResearchNode)
                        {
                            ResearchNode researchNode2 = (ResearchNode)characterEvent.EventData;
                            if (researchNode2 != null)
                            {
                                title = string.Format(TextResolver.GetText("Character Event Title Research Breakthrough"), researchNode2.Name);
                                text = string.Format(TextResolver.GetText("Character Event Description Research Advance"), researchNode2.Name);
                            }
                        }
                        break;
                    case CharacterEventType.TourismIncome:
                        text = TextResolver.GetText("Character Event Description Tourism Income");
                        break;
                    case CharacterEventType.TradeIncome:
                        text = BaconGalaxy.ResolveDescription(characterEvent, out title);
                        break;
                    case CharacterEventType.TreatySigned:
                        if (characterEvent.EventData != null && characterEvent.EventData is DiplomaticRelation)
                        {
                            DiplomaticRelation diplomaticRelation = (DiplomaticRelation)characterEvent.EventData;
                            if (diplomaticRelation != null)
                            {
                                title = string.Format(TextResolver.GetText("Character Event Title Treaty Signed"), diplomaticRelation.OtherEmpire.Name);
                                text = string.Format(TextResolver.GetText("Character Event Description Treaty Signed"), ResolveDescription(diplomaticRelation.Type), diplomaticRelation.OtherEmpire.Name);
                            }
                        }
                        break;
                    case CharacterEventType.TroopComplete:
                        if (characterEvent.EventData != null && characterEvent.EventData is Troop)
                        {
                            Troop troop = (Troop)characterEvent.EventData;
                            if (troop != null)
                            {
                                text = string.Format(TextResolver.GetText("Character Event Description Troop Complete"), troop.Name);
                            }
                        }
                        break;
                    case CharacterEventType.WarEnded:
                        if (characterEvent.EventData != null && characterEvent.EventData is Empire)
                        {
                            Empire empire5 = (Empire)characterEvent.EventData;
                            if (empire5 != null)
                            {
                                title = string.Format(TextResolver.GetText("Character Event Title War Ended"), empire5.Name);
                                text = string.Format(TextResolver.GetText("Character Event Description War Ended"), empire5.Name);
                            }
                        }
                        break;
                    case CharacterEventType.WarStarted:
                        if (characterEvent.EventData != null && characterEvent.EventData is Empire)
                        {
                            Empire empire3 = (Empire)characterEvent.EventData;
                            if (empire3 != null)
                            {
                                title = string.Format(TextResolver.GetText("Character Event Title War Started"), empire3.Name);
                                text = string.Format(TextResolver.GetText("Character Event Description War Started"), empire3.Name);
                            }
                        }
                        break;
                    case CharacterEventType.SpaceBattle:
                        {
                            if (characterEvent.EventData == null || !(characterEvent.EventData is SpaceBattleStats))
                            {
                                break;
                            }
                            SpaceBattleStats spaceBattleStats = (SpaceBattleStats)characterEvent.EventData;
                            if (spaceBattleStats == null)
                            {
                                break;
                            }
                            int num = spaceBattleStats.DestroyedEnemyShipBaseSize + spaceBattleStats.DestroyedEnemyShipBaseSizeByFighters;
                            int num2 = spaceBattleStats.DestroyedEnemyShipsEscort + spaceBattleStats.DestroyedEnemyShipsFrigate + spaceBattleStats.DestroyedEnemyShipsDestroyer + spaceBattleStats.DestroyedEnemyShipsCruiser + spaceBattleStats.DestroyedEnemyShipsCapitalShip + spaceBattleStats.DestroyedEnemyShipsCarrier + spaceBattleStats.DestroyedEnemyShipsTroopTransport + spaceBattleStats.DestroyedEnemyShipsResupplyShip + spaceBattleStats.DestroyedEnemyShipsDefensiveBase + spaceBattleStats.DestroyedEnemyShipsOtherBase + spaceBattleStats.DestroyedEnemyShipsOtherShips + spaceBattleStats.DestroyedEnemyShipsSpaceport;
                            double num3 = (double)spaceBattleStats.DestroyedEnemyShipBaseSizeByFighters / (1.0 + (double)num);
                            int num4 = spaceBattleStats.DestroyedFriendlyShipBaseSize + spaceBattleStats.DestroyedFriendlyShipBaseSizeByFighters;
                            int num5 = spaceBattleStats.DestroyedFriendlyShipsEscort + spaceBattleStats.DestroyedFriendlyShipsFrigate + spaceBattleStats.DestroyedFriendlyShipsDestroyer + spaceBattleStats.DestroyedFriendlyShipsCruiser + spaceBattleStats.DestroyedFriendlyShipsCapitalShip + spaceBattleStats.DestroyedFriendlyShipsCarrier + spaceBattleStats.DestroyedFriendlyShipsTroopTransport + spaceBattleStats.DestroyedFriendlyShipsResupplyShip + spaceBattleStats.DestroyedFriendlyShipsDefensiveBase + spaceBattleStats.DestroyedFriendlyShipsOtherBase + spaceBattleStats.DestroyedFriendlyShipsOtherShips + spaceBattleStats.DestroyedFriendlyShipsSpaceport;
                            double num6 = (double)spaceBattleStats.DestroyedFriendlyShipBaseSizeByFighters / (1.0 + (double)num4);
                            _ = (1.0 + (double)num) / (1.0 + (double)num4);
                            int num7 = num + num4;
                            if (spaceBattleStats.Location != null)
                            {
                                if (num7 > 800)
                                {
                                    if (spaceBattleStats.NearLocation)
                                    {
                                        title = string.Format(TextResolver.GetText("Space Battle Title"), spaceBattleStats.Location.Name);
                                    }
                                    else
                                    {
                                        title = string.Format(TextResolver.GetText("Space Battle Title Nearby"), spaceBattleStats.Location.Name);
                                    }
                                }
                                else if (spaceBattleStats.NearLocation)
                                {
                                    title = string.Format(TextResolver.GetText("Space Skirmish Title"), spaceBattleStats.Location.Name);
                                }
                                else
                                {
                                    title = string.Format(TextResolver.GetText("Space Skirmish Title Nearby"), spaceBattleStats.Location.Name);
                                }
                            }
                            text += string.Format(TextResolver.GetText("Space Battle Stats Destroyed Tonnage"), num2.ToString("0"), num.ToString("###,###,##0"), num3.ToString("0%"), num5.ToString("0"), num4.ToString("###,###,##0"), num6.ToString("0%"));
                            text += "\n\n";
                            string text4 = string.Empty;
                            if (num <= 0 && spaceBattleStats.DestroyedEnemyFighters <= 0)
                            {
                                text4 = TextResolver.GetText("None");
                            }
                            if (spaceBattleStats.DestroyedEnemyShipsEscort > 0)
                            {
                                string text5 = text4;
                                text4 = text5 + spaceBattleStats.DestroyedEnemyShipsEscort.ToString("0") + " x " + ResolveDescription(BuiltObjectSubRole.Escort) + ", ";
                            }
                            if (spaceBattleStats.DestroyedEnemyShipsFrigate > 0)
                            {
                                string text6 = text4;
                                text4 = text6 + spaceBattleStats.DestroyedEnemyShipsFrigate.ToString("0") + " x " + ResolveDescription(BuiltObjectSubRole.Frigate) + ", ";
                            }
                            if (spaceBattleStats.DestroyedEnemyShipsDestroyer > 0)
                            {
                                string text7 = text4;
                                text4 = text7 + spaceBattleStats.DestroyedEnemyShipsDestroyer.ToString("0") + " x " + ResolveDescription(BuiltObjectSubRole.Destroyer) + ", ";
                            }
                            if (spaceBattleStats.DestroyedEnemyShipsCruiser > 0)
                            {
                                string text8 = text4;
                                text4 = text8 + spaceBattleStats.DestroyedEnemyShipsCruiser.ToString("0") + " x " + ResolveDescription(BuiltObjectSubRole.Cruiser) + ", ";
                            }
                            if (spaceBattleStats.DestroyedEnemyShipsCapitalShip > 0)
                            {
                                string text5 = text4;
                                text4 = text5 + spaceBattleStats.DestroyedEnemyShipsCapitalShip.ToString("0") + " x " + ResolveDescription(BuiltObjectSubRole.CapitalShip) + ", ";
                            }
                            if (spaceBattleStats.DestroyedEnemyShipsCarrier > 0)
                            {
                                string text5 = text4;
                                text4 = text5 + spaceBattleStats.DestroyedEnemyShipsCarrier.ToString("0") + " x " + ResolveDescription(BuiltObjectSubRole.Carrier) + ", ";
                            }
                            if (spaceBattleStats.DestroyedEnemyShipsTroopTransport > 0)
                            {
                                string text5 = text4;
                                text4 = text5 + spaceBattleStats.DestroyedEnemyShipsTroopTransport.ToString("0") + " x " + ResolveDescription(BuiltObjectSubRole.TroopTransport) + ", ";
                            }
                            if (spaceBattleStats.DestroyedEnemyFighters > 0)
                            {
                                string text5 = text4;
                                text4 = text5 + spaceBattleStats.DestroyedEnemyFighters.ToString("0") + " x " + TextResolver.GetText("Fighters") + ", ";
                            }
                            if (spaceBattleStats.DestroyedEnemyShipsResupplyShip > 0)
                            {
                                string text5 = text4;
                                text4 = text5 + spaceBattleStats.DestroyedEnemyShipsResupplyShip.ToString("0") + " x " + ResolveDescription(BuiltObjectSubRole.ResupplyShip) + ", ";
                            }
                            if (spaceBattleStats.DestroyedEnemyShipsSpaceport > 0)
                            {
                                string text5 = text4;
                                text4 = text5 + spaceBattleStats.DestroyedEnemyShipsSpaceport.ToString("0") + " x " + TextResolver.GetText("Space Port") + ", ";
                            }
                            if (spaceBattleStats.DestroyedEnemyShipsDefensiveBase > 0)
                            {
                                string text5 = text4;
                                text4 = text5 + spaceBattleStats.DestroyedEnemyShipsDefensiveBase.ToString("0") + " x " + ResolveDescription(BuiltObjectSubRole.DefensiveBase) + ", ";
                            }
                            if (spaceBattleStats.DestroyedEnemyShipsOtherBase > 0)
                            {
                                string text5 = text4;
                                text4 = text5 + spaceBattleStats.DestroyedEnemyShipsOtherBase.ToString("0") + " x " + TextResolver.GetText("Other Bases") + ", ";
                            }
                            if (spaceBattleStats.DestroyedEnemyShipsOtherShips > 0)
                            {
                                string text5 = text4;
                                text4 = text5 + spaceBattleStats.DestroyedEnemyShipsOtherShips.ToString("0") + " x " + TextResolver.GetText("Other Ships") + ", ";
                            }
                            if ((num > 0 || spaceBattleStats.DestroyedEnemyFighters > 0) && !string.IsNullOrEmpty(text4) && text4.Length >= 2)
                            {
                                text4 = text4.Substring(0, text4.Length - 2);
                            }
                            if (!string.IsNullOrEmpty(text4))
                            {
                                text += string.Format(TextResolver.GetText("Space Battle Enemy Losses"), text4);
                                text += "\n\n";
                            }
                            string text9 = string.Empty;
                            if (num4 <= 0 && spaceBattleStats.DestroyedFriendlyFighters <= 0)
                            {
                                text9 = TextResolver.GetText("None");
                            }
                            if (spaceBattleStats.DestroyedFriendlyShipsEscort > 0)
                            {
                                string text5 = text9;
                                text9 = text5 + spaceBattleStats.DestroyedFriendlyShipsEscort.ToString("0") + " x " + ResolveDescription(BuiltObjectSubRole.Escort) + ", ";
                            }
                            if (spaceBattleStats.DestroyedFriendlyShipsFrigate > 0)
                            {
                                string text5 = text9;
                                text9 = text5 + spaceBattleStats.DestroyedFriendlyShipsFrigate.ToString("0") + " x " + ResolveDescription(BuiltObjectSubRole.Frigate) + ", ";
                            }
                            if (spaceBattleStats.DestroyedFriendlyShipsDestroyer > 0)
                            {
                                string text5 = text9;
                                text9 = text5 + spaceBattleStats.DestroyedFriendlyShipsDestroyer.ToString("0") + " x " + ResolveDescription(BuiltObjectSubRole.Destroyer) + ", ";
                            }
                            if (spaceBattleStats.DestroyedFriendlyShipsCruiser > 0)
                            {
                                string text5 = text9;
                                text9 = text5 + spaceBattleStats.DestroyedFriendlyShipsCruiser.ToString("0") + " x " + ResolveDescription(BuiltObjectSubRole.Cruiser) + ", ";
                            }
                            if (spaceBattleStats.DestroyedFriendlyShipsCapitalShip > 0)
                            {
                                string text5 = text9;
                                text9 = text5 + spaceBattleStats.DestroyedFriendlyShipsCapitalShip.ToString("0") + " x " + ResolveDescription(BuiltObjectSubRole.CapitalShip) + ", ";
                            }
                            if (spaceBattleStats.DestroyedFriendlyShipsCarrier > 0)
                            {
                                string text5 = text9;
                                text9 = text5 + spaceBattleStats.DestroyedFriendlyShipsCarrier.ToString("0") + " x " + ResolveDescription(BuiltObjectSubRole.Carrier) + ", ";
                            }
                            if (spaceBattleStats.DestroyedFriendlyShipsTroopTransport > 0)
                            {
                                string text5 = text9;
                                text9 = text5 + spaceBattleStats.DestroyedFriendlyShipsTroopTransport.ToString("0") + " x " + ResolveDescription(BuiltObjectSubRole.TroopTransport) + ", ";
                            }
                            if (spaceBattleStats.DestroyedFriendlyFighters > 0)
                            {
                                string text5 = text9;
                                text9 = text5 + spaceBattleStats.DestroyedFriendlyFighters.ToString("0") + " x " + TextResolver.GetText("Fighters") + ", ";
                            }
                            if (spaceBattleStats.DestroyedFriendlyShipsResupplyShip > 0)
                            {
                                string text5 = text9;
                                text9 = text5 + spaceBattleStats.DestroyedFriendlyShipsResupplyShip.ToString("0") + " x " + ResolveDescription(BuiltObjectSubRole.ResupplyShip) + ", ";
                            }
                            if (spaceBattleStats.DestroyedFriendlyShipsSpaceport > 0)
                            {
                                string text5 = text9;
                                text9 = text5 + spaceBattleStats.DestroyedFriendlyShipsSpaceport.ToString("0") + " x " + TextResolver.GetText("Space Port") + ", ";
                            }
                            if (spaceBattleStats.DestroyedFriendlyShipsDefensiveBase > 0)
                            {
                                string text5 = text9;
                                text9 = text5 + spaceBattleStats.DestroyedFriendlyShipsDefensiveBase.ToString("0") + " x " + ResolveDescription(BuiltObjectSubRole.DefensiveBase) + ", ";
                            }
                            if (spaceBattleStats.DestroyedFriendlyShipsOtherBase > 0)
                            {
                                string text5 = text9;
                                text9 = text5 + spaceBattleStats.DestroyedFriendlyShipsOtherBase.ToString("0") + " x " + TextResolver.GetText("Other Bases") + ", ";
                            }
                            if (spaceBattleStats.DestroyedFriendlyShipsOtherShips > 0)
                            {
                                string text5 = text9;
                                text9 = text5 + spaceBattleStats.DestroyedFriendlyShipsOtherShips.ToString("0") + " x " + TextResolver.GetText("Other Ships") + ", ";
                            }
                            if ((num4 > 0 || spaceBattleStats.DestroyedFriendlyFighters > 0) && !string.IsNullOrEmpty(text9) && text9.Length >= 2)
                            {
                                text9 = text9.Substring(0, text9.Length - 2);
                            }
                            if (!string.IsNullOrEmpty(text9))
                            {
                                text += string.Format(TextResolver.GetText("Space Battle Friendly Losses"), text9);
                                text += "\n\n";
                            }
                            break;
                        }
                    case CharacterEventType.GroundInvasion:
                        {
                            if (characterEvent.EventData == null || !(characterEvent.EventData is InvasionStats))
                            {
                                break;
                            }
                            InvasionStats invasionStats = (InvasionStats)characterEvent.EventData;
                            if (invasionStats == null)
                            {
                                break;
                            }
                            if (invasionStats.Colony != null)
                            {
                                title = string.Format(TextResolver.GetText("Colony Invasion Title"), invasionStats.Colony.Name);
                                if (invasionStats.DefendingEmpire == callingEmpire)
                                {
                                    title = string.Format(TextResolver.GetText("Colony Defense Title"), invasionStats.Colony.Name);
                                }
                                text = ((invasionStats.DefendingEmpire == callingEmpire) ? ((!invasionStats.InvasionSucceeded) ? (text + string.Format(TextResolver.GetText("Colony Defense Succeeded"), invasionStats.Colony.Name)) : (text + string.Format(TextResolver.GetText("Colony Defense Failed"), invasionStats.Colony.Name))) : ((!invasionStats.InvasionSucceeded) ? (text + string.Format(TextResolver.GetText("Colony Invasion Failed"), invasionStats.Colony.Name)) : (text + string.Format(TextResolver.GetText("Colony Invasion Succeeded"), invasionStats.Colony.Name))));
                                text += "\n\n";
                            }
                            text += string.Format(TextResolver.GetText("Colony Invasion Troop Losses"), invasionStats.DestroyedDefendingTroops.ToString("0"), invasionStats.DestroyedInvadingTroops.ToString("0"));
                            break;
                        }
                    case CharacterEventType.IntelligenceAgentOursCaptured:
                        if (characterEvent.EventData != null && characterEvent.EventData is Character)
                        {
                            Character character2 = (Character)characterEvent.EventData;
                            if (character2 != null)
                            {
                                title = string.Format(TextResolver.GetText("Character Event Title Intelligence Agent Captured"), character2.Name);
                                text += ResolveDescriptionFull(character2.Mission, callingEmpire);
                            }
                        }
                        break;
                    case CharacterEventType.IntelligenceMissionInterceptEnemy:
                        if (characterEvent.EventData != null && characterEvent.EventData is Character)
                        {
                            Character character = (Character)characterEvent.EventData;
                            if (character != null)
                            {
                                text += string.Format(TextResolver.GetText("Enemy Agent Captured Generic"), character.Name);
                            }
                        }
                        break;
                    case CharacterEventType.IntelligenceMissionSucceedEspionage:
                    case CharacterEventType.IntelligenceMissionSucceedSabotage:
                    case CharacterEventType.IntelligenceMissionFailEspionage:
                    case CharacterEventType.IntelligenceMissionFailSabotage:
                        if (characterEvent.EventData != null && characterEvent.EventData is IntelligenceMission)
                        {
                            IntelligenceMission intelligenceMission = (IntelligenceMission)characterEvent.EventData;
                            if (intelligenceMission != null)
                            {
                                text += ResolveDescriptionFull(intelligenceMission, callingEmpire);
                            }
                        }
                        break;
                    case CharacterEventType.BuildFacility:
                    case CharacterEventType.BuildWonder:
                        {
                            if (characterEvent.EventData == null || !(characterEvent.EventData is PlanetaryFacility))
                            {
                                break;
                            }
                            PlanetaryFacility planetaryFacility = (PlanetaryFacility)characterEvent.EventData;
                            if (planetaryFacility != null)
                            {
                                string arg = string.Empty;
                                Habitat habitat = callingEmpire.Colonies.FindColonyWithFacility(planetaryFacility);
                                if (habitat != null)
                                {
                                    arg = habitat.Name;
                                }
                                if (planetaryFacility.Type == PlanetaryFacilityType.Wonder)
                                {
                                    title = string.Format(TextResolver.GetText("Wonder Build Title"), planetaryFacility.Name);
                                    text += string.Format(TextResolver.GetText("Wonder Built"), planetaryFacility.Name, arg);
                                }
                                else
                                {
                                    title = string.Format(TextResolver.GetText("Character Event Title Facility Built"), planetaryFacility.Name);
                                    text += string.Format(TextResolver.GetText("Facility Built"), planetaryFacility.Name, arg);
                                }
                            }
                            break;
                        }
                }
                if (string.IsNullOrEmpty(title))
                {
                    title = ResolveDescription(characterEvent.Type);
                }
            }
            return text;
        }

        public static string ResolveDescriptionFull(IntelligenceMission mission, Empire callingEmpire)
        {
            string result = string.Empty;
            if (mission != null)
            {
                Character agent = mission.Agent;
                switch (mission.Outcome)
                {
                    case IntelligenceMissionOutcome.Capture:
                        result = ((callingEmpire != mission.TargetEmpire) ? string.Format(TextResolver.GetText("Our Agent Captured In Act"), agent.Name, ResolveDescription(mission, callingEmpire)) : string.Format(TextResolver.GetText("Enemy Agent Captured In Act"), agent.Name, agent.Empire.Name, ResolveDescription(mission, callingEmpire)));
                        break;
                    case IntelligenceMissionOutcome.FailDetect:
                        result = ((callingEmpire != mission.TargetEmpire) ? string.Format(TextResolver.GetText("Our Agent Detect Fail"), agent.Name, ResolveDescription(mission, callingEmpire)) : string.Format(TextResolver.GetText("Enemy Agent Detect Fail"), agent.Name, agent.Empire.Name, ResolveDescription(mission, callingEmpire)));
                        break;
                    case IntelligenceMissionOutcome.SucceedDetect:
                        result = ((callingEmpire != mission.TargetEmpire) ? string.Format(TextResolver.GetText("Our Agent Detect Succeed"), agent.Name, ResolveDescription(mission, callingEmpire)) : string.Format(TextResolver.GetText("Enemy Agent Detect Succeed"), agent.Name, agent.Empire.Name, ResolveDescription(mission, callingEmpire)));
                        break;
                    case IntelligenceMissionOutcome.FailNotDetect:
                        result = ((callingEmpire != mission.TargetEmpire) ? string.Format(TextResolver.GetText("Our Agent Fail"), agent.Name, ResolveDescription(mission, callingEmpire)) : string.Format(TextResolver.GetText("Enemy Agent Fail"), agent.Name, agent.Empire.Name, ResolveDescription(mission, callingEmpire)));
                        break;
                    case IntelligenceMissionOutcome.SucceedNotDetect:
                        result = ((callingEmpire != mission.TargetEmpire) ? string.Format(TextResolver.GetText("Our Agent Succeed"), agent.Name, ResolveDescription(mission, callingEmpire)) : ((agent.Mission.Type != IntelligenceMissionType.InciteRevolution) ? string.Format(TextResolver.GetText("Enemy Agent Succeed"), agent.Name, agent.Empire.Name, ResolveDescription(mission, callingEmpire)) : string.Format(TextResolver.GetText("Agent Revolution"), mission.TargetEmpire.GovernmentAttributes.Name)));
                        break;
                }
            }
            return result;
        }

        public static string ResolveDescription(MultipleEventActionType multipleEventActionType)
        {
            string result = string.Empty;
            switch (multipleEventActionType)
            {
                case MultipleEventActionType.ExecuteAllActions:
                    result = TextResolver.GetText("MultipleEventActionType ExecuteAllActions");
                    break;
                case MultipleEventActionType.ExecuteSingleRandomAction:
                    result = TextResolver.GetText("MultipleEventActionType ExecuteSingleRandomAction");
                    break;
            }
            return result;
        }

        public static string ResolveDescription(EventTriggerType eventTriggerType)
        {
            string result = string.Empty;
            switch (eventTriggerType)
            {
                case EventTriggerType.Build:
                    result = TextResolver.GetText("EventTriggerType Build");
                    break;
                case EventTriggerType.Capture:
                    result = TextResolver.GetText("EventTriggerType Capture");
                    break;
                case EventTriggerType.Destroy:
                    result = TextResolver.GetText("EventTriggerType Destroy");
                    break;
                case EventTriggerType.Investigate:
                    result = TextResolver.GetText("EventTriggerType Investigate");
                    break;
                case EventTriggerType.DiplomaticRelationChange:
                    result = TextResolver.GetText("EventTriggerType DiplomaticRelationChange");
                    break;
                case EventTriggerType.EmpireEncounter:
                    result = TextResolver.GetText("EventTriggerType EmpireEncounter");
                    break;
                case EventTriggerType.ResearchBreakthrough:
                    result = TextResolver.GetText("EventTriggerType ResearchBreakthrough");
                    break;
                case EventTriggerType.PlanetDestroyerConstructionCompleted:
                    result = TextResolver.GetText("EventTriggerType PlanetDestroyerConstructionCompleted");
                    break;
                case EventTriggerType.EmpireEliminated:
                    result = TextResolver.GetText("EventTriggerType EmpireEliminated");
                    break;
                case EventTriggerType.CharacterAppears:
                    result = TextResolver.GetText("EventTriggerType CharacterAppears");
                    break;
                case EventTriggerType.CharacterKilled:
                    result = TextResolver.GetText("EventTriggerType CharacterKilled");
                    break;
            }
            return result;
        }

        public static string ResolveDescription(DesignImageScalingMode designImageScalingMode)
        {
            string result = string.Empty;
            switch (designImageScalingMode)
            {
                case DesignImageScalingMode.None:
                    result = TextResolver.GetText("DesignImageScalingMode None");
                    break;
                case DesignImageScalingMode.Absolute:
                    result = TextResolver.GetText("DesignImageScalingMode Absolute");
                    break;
                case DesignImageScalingMode.Scaled:
                    result = TextResolver.GetText("DesignImageScalingMode Scaled");
                    break;
            }
            return result;
        }

        public static string ResolveDescription(GameEvent gameEvent)
        {
            string result = string.Empty;
            BuiltObject builtObject = null;
            Habitat habitat = null;
            Creature creature = null;
            if (gameEvent != null)
            {
                if (gameEvent.TriggerObject != null)
                {
                    if (gameEvent.TriggerObject is BuiltObject)
                    {
                        builtObject = (BuiltObject)gameEvent.TriggerObject;
                    }
                    else if (gameEvent.TriggerObject is Habitat)
                    {
                        habitat = (Habitat)gameEvent.TriggerObject;
                    }
                    else if (gameEvent.TriggerObject is Creature)
                    {
                        creature = (Creature)gameEvent.TriggerObject;
                    }
                }
                switch (gameEvent.TriggerType)
                {
                    case EventTriggerType.Build:
                        if (habitat != null)
                        {
                            result = string.Format(TextResolver.GetText("GameEvent TriggerType Build Description"), string.Empty, habitat.Name);
                            if (gameEvent.TriggerFacility != null)
                            {
                                result = string.Format(TextResolver.GetText("GameEvent TriggerType Build Description"), gameEvent.TriggerFacility.Name, habitat.Name);
                            }
                            else if (gameEvent.TriggerBuiltObjectSubRole != 0)
                            {
                                result = string.Format(TextResolver.GetText("GameEvent TriggerType Build Description"), ResolveDescription(gameEvent.TriggerBuiltObjectSubRole), habitat.Name);
                            }
                        }
                        break;
                    case EventTriggerType.Capture:
                        if (habitat != null)
                        {
                            result = string.Format(TextResolver.GetText("GameEvent TriggerType Capture Description Colony"), habitat.Name);
                        }
                        else if (builtObject != null)
                        {
                            result = ((builtObject.Role != BuiltObjectRole.Base) ? string.Format(TextResolver.GetText("GameEvent TriggerType Capture Description"), ResolveDescription(builtObject.SubRole), builtObject.Name) : string.Format(TextResolver.GetText("GameEvent TriggerType Capture Description Base"), builtObject.Name));
                        }
                        break;
                    case EventTriggerType.Destroy:
                        if (habitat != null)
                        {
                            result = string.Format(TextResolver.GetText("GameEvent TriggerType Destroy Description Planet"), habitat.Name);
                        }
                        else if (builtObject != null)
                        {
                            result = string.Format(TextResolver.GetText("GameEvent TriggerType Destroy Description"), ResolveDescription(builtObject.SubRole), builtObject.Name);
                        }
                        else if (creature != null)
                        {
                            result = string.Format(TextResolver.GetText("GameEvent TriggerType Destroy Description Creature"), creature.Name);
                        }
                        break;
                    case EventTriggerType.Investigate:
                        if (builtObject != null)
                        {
                            result = ((builtObject.Role != BuiltObjectRole.Base) ? string.Format(TextResolver.GetText("GameEvent TriggerType Investigate Description"), ResolveDescription(builtObject.SubRole), builtObject.Name) : string.Format(TextResolver.GetText("GameEvent TriggerType Investigate Description Base"), builtObject.Name));
                        }
                        else if (gameEvent.TriggerRuin != null)
                        {
                            result = string.Format(TextResolver.GetText("GameEvent TriggerType Investigate Description Ruins"), gameEvent.TriggerRuin.Name);
                        }
                        break;
                    case EventTriggerType.DiplomaticRelationChange:
                        if (gameEvent.Empire != null && gameEvent.EmpireOther != null)
                        {
                            result = string.Format(TextResolver.GetText("GameEvent TriggerType DiplomaticRelationChange"), gameEvent.Empire.Name, gameEvent.EmpireOther.Name, ResolveDescription(gameEvent.DiplomaticRelationType));
                        }
                        break;
                    case EventTriggerType.EmpireEncounter:
                        if (gameEvent.Empire != null && gameEvent.EmpireOther != null)
                        {
                            result = string.Format(TextResolver.GetText("GameEvent TriggerType EmpireEncounter"), gameEvent.Empire.Name, gameEvent.EmpireOther.Name);
                        }
                        break;
                    case EventTriggerType.ResearchBreakthrough:
                        if (gameEvent.Empire != null && gameEvent.ResearchProjectId >= 0 && gameEvent.ResearchProjectId < ResearchNodeDefinitionsStatic.Count)
                        {
                            ResearchNodeDefinition researchNodeDefinition = ResearchNodeDefinitionsStatic[gameEvent.ResearchProjectId];
                            if (researchNodeDefinition != null)
                            {
                                result = string.Format(TextResolver.GetText("GameEvent TriggerType ResearchBreakthrough"), gameEvent.Empire.Name, researchNodeDefinition.Name);
                            }
                        }
                        break;
                    case EventTriggerType.PlanetDestroyerConstructionCompleted:
                        if (gameEvent.Empire != null)
                        {
                            result = string.Format(TextResolver.GetText("GameEvent TriggerType PlanetDestroyerConstructionCompleted"), gameEvent.Empire.Name);
                        }
                        break;
                    case EventTriggerType.EmpireEliminated:
                        if (gameEvent.Empire != null)
                        {
                            result = ((gameEvent.EmpireOther != null) ? string.Format(TextResolver.GetText("GameEvent TriggerType EmpireEliminated By Empire"), gameEvent.Empire.Name, gameEvent.EmpireOther.Name) : string.Format(TextResolver.GetText("GameEvent TriggerType EmpireEliminated"), gameEvent.Empire.Name));
                        }
                        break;
                    case EventTriggerType.CharacterAppears:
                        if (gameEvent.Character != null)
                        {
                            result = string.Format(TextResolver.GetText("GameEvent TriggerType CharacterAppears"), gameEvent.Character.Name);
                        }
                        break;
                    case EventTriggerType.CharacterKilled:
                        if (gameEvent.Character != null)
                        {
                            result = string.Format(TextResolver.GetText("GameEvent TriggerType CharacterKilled"), gameEvent.Character.Name);
                        }
                        break;
                }
            }
            return result;
        }

        public static string ResolveDescription(EventAction eventAction)
        {
            string result = string.Empty;
            BuiltObject builtObject = null;
            Habitat habitat = null;
            if (eventAction != null)
            {
                if (eventAction.Target != null)
                {
                    if (eventAction.Target is BuiltObject)
                    {
                        builtObject = (BuiltObject)eventAction.Target;
                    }
                    else if (eventAction.Target is Habitat)
                    {
                        habitat = (Habitat)eventAction.Target;
                    }
                }
                if (eventAction != null)
                {
                    switch (eventAction.Type)
                    {
                        case EventActionType.AcquireBuiltObject:
                            if (builtObject != null)
                            {
                                result = ((builtObject.Role != BuiltObjectRole.Base) ? string.Format(TextResolver.GetText("EventActionType Description AcquireBuiltObject"), ResolveDescription(builtObject.SubRole), builtObject.Name) : string.Format(TextResolver.GetText("EventActionType Description AcquireBuiltObject"), TextResolver.GetText("Base").ToLower(CultureInfo.InvariantCulture), builtObject.Name));
                            }
                            break;
                        case EventActionType.AcquireHabitat:
                            if (habitat != null)
                            {
                                result = string.Format(TextResolver.GetText("EventActionType Description AcquireHabitat"), ResolveDescription(habitat.Category).ToLower(CultureInfo.InvariantCulture), habitat.Name);
                            }
                            break;
                        case EventActionType.BuildPlanetaryFacility:
                            if (eventAction.Value >= 0 && eventAction.Value < PlanetaryFacilityDefinitionsStatic.Count)
                            {
                                PlanetaryFacilityDefinition planetaryFacilityDefinition = PlanetaryFacilityDefinitionsStatic[eventAction.Value];
                                if (planetaryFacilityDefinition != null)
                                {
                                    result = ((habitat == null) ? string.Format(TextResolver.GetText("EventActionType Description BuildPlanetaryFacility Alt"), planetaryFacilityDefinition.Name) : string.Format(TextResolver.GetText("EventActionType Description BuildPlanetaryFacility"), planetaryFacilityDefinition.Name, habitat.Name));
                                }
                            }
                            break;
                        case EventActionType.ChangeEmpireGovernment:
                            if (eventAction.Empire != null && eventAction.Value >= 0 && eventAction.Value < GovernmentsStatic.Count)
                            {
                                GovernmentAttributes governmentAttributes = GovernmentsStatic[eventAction.Value];
                                if (governmentAttributes != null)
                                {
                                    result = string.Format(TextResolver.GetText("EventActionType Description ChangeEmpireGovernment"), eventAction.Empire.Name, governmentAttributes.Name);
                                }
                            }
                            break;
                        case EventActionType.ChangeRaceBias:
                            if (eventAction.Race != null && eventAction.RaceOther != null)
                            {
                                result = string.Format(TextResolver.GetText("EventActionType Description ChangeRaceBias"), eventAction.Race.Name, eventAction.RaceOther.Name, eventAction.Value.ToString("0"));
                            }
                            break;
                        case EventActionType.RevealObject:
                            if (builtObject != null)
                            {
                                result = ((builtObject.Role != BuiltObjectRole.Base) ? string.Format(TextResolver.GetText("EventActionType Description RevealShip"), ResolveDescription(builtObject.SubRole), builtObject.Name) : string.Format(TextResolver.GetText("EventActionType Description RevealBase"), builtObject.Name));
                            }
                            else if (habitat != null)
                            {
                                result = string.Format(TextResolver.GetText("EventActionType Description RevealPlanet"), ResolveDescription(habitat.Category), habitat.Name);
                            }
                            break;
                        case EventActionType.DestroyBuiltObject:
                            if (builtObject != null)
                            {
                                result = string.Format(TextResolver.GetText("EventActionType Description DestroyBuiltObject"), builtObject.Name);
                            }
                            break;
                        case EventActionType.DestroyPlanetaryFacility:
                            if (eventAction.Value >= 0 && eventAction.Value < PlanetaryFacilityDefinitionsStatic.Count)
                            {
                                PlanetaryFacilityDefinition planetaryFacilityDefinition2 = PlanetaryFacilityDefinitionsStatic[eventAction.Value];
                                if (planetaryFacilityDefinition2 != null)
                                {
                                    result = ((habitat == null) ? string.Format(TextResolver.GetText("EventActionType Description DestroyPlanetaryFacility Alt"), planetaryFacilityDefinition2.Name) : string.Format(TextResolver.GetText("EventActionType Description DestroyPlanetaryFacility"), planetaryFacilityDefinition2.Name, habitat.Name));
                                }
                            }
                            break;
                        case EventActionType.DisasterAtColony:
                            if (habitat != null)
                            {
                                result = string.Format(TextResolver.GetText("EventActionType Description DisasterAtColony"), habitat.Name);
                            }
                            break;
                        case EventActionType.EmpireDeclaresWarOnTriggerEmpire:
                            if (eventAction.Empire != null)
                            {
                                result = string.Format(TextResolver.GetText("EventActionType Description EmpireDeclaresWarOnTriggerEmpire"), eventAction.Empire.Name);
                            }
                            break;
                        case EventActionType.EndPlague:
                            result = ((habitat == null) ? TextResolver.GetText("EventActionType Description EndPlague Alt") : string.Format(TextResolver.GetText("EventActionType Description EndPlague"), habitat.Name));
                            break;
                        case EventActionType.EnemyFleetDefectsToTriggerEmpire:
                            result = TextResolver.GetText("EventActionType Description EnemyFleetDefectsToTriggerEmpire");
                            break;
                        case EventActionType.FindMoneyTreasure:
                            result = string.Format(TextResolver.GetText("EventActionType Description FindMoneyTreasure"), eventAction.MoneyAmount.ToString("###,###,##0"));
                            break;
                        case EventActionType.GenerateBuiltObject:
                            if (habitat != null && eventAction.BuiltObjectSubRole != 0)
                            {
                                result = string.Format(TextResolver.GetText("EventActionType Description GenerateBuiltObject"), ResolveDescription(eventAction.BuiltObjectSubRole), habitat.Name);
                            }
                            break;
                        case EventActionType.GenerateCreatureSwarm:
                            if (habitat != null && eventAction.CreatureType != 0)
                            {
                                result = string.Format(TextResolver.GetText("EventActionType Description GenerateCreatureSwarm"), eventAction.Value.ToString("0"), ResolveDescription(eventAction.CreatureType), habitat.Name);
                            }
                            break;
                        case EventActionType.GenerateNewEmpire:
                            if (eventAction.Race != null && habitat != null)
                            {
                                result = string.Format(TextResolver.GetText("EventActionType Description GenerateNewEmpire"), eventAction.Race.Name, habitat.Name);
                            }
                            break;
                        case EventActionType.GenerateNewPirateFaction:
                            if (eventAction.Race != null && habitat != null)
                            {
                                result = string.Format(TextResolver.GetText("EventActionType Description GenerateNewPirateFaction"), eventAction.Race.Name, habitat.Name);
                            }
                            break;
                        case EventActionType.GeneratePirateAmbush:
                            if (habitat != null)
                            {
                                result = string.Format(TextResolver.GetText("EventActionType Description GeneratePirateAmbush"), eventAction.Value.ToString("0"), habitat.Name);
                            }
                            break;
                        case EventActionType.GenerateRefugeeFleet:
                            if (habitat != null)
                            {
                                result = ((eventAction.Race == null) ? string.Format(TextResolver.GetText("EventActionType Description GenerateRefugeeFleet Alt"), habitat.Name) : string.Format(TextResolver.GetText("EventActionType Description GenerateRefugeeFleet"), eventAction.Race.Name, habitat.Name));
                            }
                            break;
                        case EventActionType.GenerateResourceAtHabitat:
                            if (habitat != null && eventAction.Value >= 0 && eventAction.Value < ResourceSystemStatic.Resources.Count)
                            {
                                ResourceDefinition resourceDefinition2 = ResourceSystemStatic.Resources[eventAction.Value];
                                if (resourceDefinition2 != null)
                                {
                                    result = string.Format(TextResolver.GetText("EventActionType Description GenerateResourceAtHabitat"), resourceDefinition2.Name, habitat.Name);
                                }
                            }
                            break;
                        case EventActionType.InterceptResource:
                            if (eventAction.Value >= 0 && eventAction.Value < ResourceSystemStatic.Resources.Count)
                            {
                                ResourceDefinition resourceDefinition = ResourceSystemStatic.Resources[eventAction.Value];
                                if (resourceDefinition != null)
                                {
                                    result = string.Format(TextResolver.GetText("EventActionType Description InterceptResource"), resourceDefinition.Name);
                                }
                            }
                            break;
                        case EventActionType.LearnAboutLostColony:
                            if (habitat != null && eventAction.Race != null)
                            {
                                result = string.Format(TextResolver.GetText("EventActionType Description LearnAboutLostColony"), eventAction.Race.Name, habitat.Name);
                            }
                            break;
                        case EventActionType.LearnAboutSpecialLocation:
                            if (eventAction.Location != null)
                            {
                                result = string.Format(TextResolver.GetText("EventActionType Description LearnAboutSpecialLocation"), eventAction.Location.Name);
                            }
                            break;
                        case EventActionType.LearnExplorationInfo:
                            if (eventAction.Value > 0)
                            {
                                result = string.Format(TextResolver.GetText("EventActionType Description LearnExplorationInfo"), eventAction.Value.ToString("0"));
                            }
                            break;
                        case EventActionType.LearnGovernmentType:
                            if (eventAction.Value >= 0 && eventAction.Value < GovernmentsStatic.Count)
                            {
                                GovernmentAttributes governmentAttributes2 = GovernmentsStatic[eventAction.Value];
                                if (governmentAttributes2 != null)
                                {
                                    result = string.Format(TextResolver.GetText("EventActionType Description LearnGovernmentType"), governmentAttributes2.Name);
                                }
                            }
                            break;
                        case EventActionType.LearnTech:
                            if (eventAction.Value >= 0 && eventAction.Value < ResearchNodeDefinitionsStatic.Count)
                            {
                                ResearchNodeDefinition researchNodeDefinition4 = ResearchNodeDefinitionsStatic[eventAction.Value];
                                if (researchNodeDefinition4 != null)
                                {
                                    result = string.Format(TextResolver.GetText("EventActionType Description LearnTech"), researchNodeDefinition4.Name);
                                }
                            }
                            break;
                        case EventActionType.MakeEmpireContact:
                            if (eventAction.Empire != null)
                            {
                                result = string.Format(TextResolver.GetText("EventActionType Description MakeEmpireContact"), eventAction.Empire.Name);
                            }
                            break;
                        case EventActionType.PirateFactionJoinsTriggerEmpire:
                            if (eventAction.Empire != null)
                            {
                                result = string.Format(TextResolver.GetText("EventActionType Description PirateFactionJoinsTriggerEmpire"), eventAction.Empire.Name);
                            }
                            break;
                        case EventActionType.RemoveResourceAtHabitat:
                            if (habitat != null && eventAction.Value >= 0 && eventAction.Value < ResourceSystemStatic.Resources.Count)
                            {
                                ResourceDefinition resourceDefinition3 = ResourceSystemStatic.Resources[eventAction.Value];
                                if (resourceDefinition3 != null)
                                {
                                    result = string.Format(TextResolver.GetText("EventActionType Description RemoveResourceAtHabitat"), resourceDefinition3.Name, habitat.Name);
                                }
                            }
                            break;
                        case EventActionType.SleepingRaceAwokenAtHabitat:
                            if (habitat != null && eventAction.Race != null)
                            {
                                result = string.Format(TextResolver.GetText("EventActionType Description SleepingRaceAwokenAtHabitat"), eventAction.Race.Name, habitat.Name);
                            }
                            break;
                        case EventActionType.SplitEmpireCivilWar:
                            if (eventAction.Empire != null)
                            {
                                result = string.Format(TextResolver.GetText("EventActionType Description SplitEmpireCivilWar"), eventAction.Empire.Name);
                            }
                            break;
                        case EventActionType.SplitEmpirePeacefully:
                            if (eventAction.Empire != null)
                            {
                                result = string.Format(TextResolver.GetText("EventActionType Description SplitEmpirePeacefully"), eventAction.Empire.Name);
                            }
                            break;
                        case EventActionType.StartPlague:
                            if (habitat != null)
                            {
                                result = string.Format(TextResolver.GetText("EventActionType Description StartPlague"), habitat.Name);
                            }
                            break;
                        case EventActionType.UnlockTech:
                            if (eventAction.Value >= 0 && eventAction.Value < ResearchNodeDefinitionsStatic.Count)
                            {
                                ResearchNodeDefinition researchNodeDefinition = ResearchNodeDefinitionsStatic[eventAction.Value];
                                if (researchNodeDefinition != null)
                                {
                                    result = string.Format(TextResolver.GetText("EventActionType Description UnlockTech"), researchNodeDefinition.Name);
                                }
                            }
                            break;
                        case EventActionType.ChangeEmpireReputation:
                            if (eventAction.Empire != null)
                            {
                                result = string.Format(TextResolver.GetText("EventActionType Description ChangeEmpireReputation"), eventAction.Empire.Name, eventAction.Value.ToString("0"));
                            }
                            break;
                        case EventActionType.ChangeEmpireEvaluation:
                            if (eventAction.Empire != null && eventAction.EmpireOther != null)
                            {
                                result = string.Format(TextResolver.GetText("EventActionType Description ChangeEmpireEvaluation"), eventAction.Empire.Name, eventAction.EmpireOther.Name, eventAction.Value.ToString("0"));
                            }
                            break;
                        case EventActionType.InitiateTreaty:
                            if (eventAction.Empire != null && eventAction.EmpireOther != null && eventAction.DiplomaticRelationType != DiplomaticRelationType.None)
                            {
                                result = string.Format(TextResolver.GetText("EventActionType Description InitiateTreaty"), eventAction.Empire.Name, eventAction.EmpireOther.Name, ResolveDescription(eventAction.DiplomaticRelationType));
                            }
                            break;
                        case EventActionType.BreakTreaty:
                            if (eventAction.Empire != null && eventAction.EmpireOther != null)
                            {
                                result = string.Format(TextResolver.GetText("EventActionType Description BreakTreaty"), eventAction.Empire.Name, eventAction.EmpireOther.Name);
                            }
                            break;
                        case EventActionType.StartTradingSuperLuxuryResources:
                            if (eventAction.Empire != null && eventAction.EmpireOther != null)
                            {
                                result = string.Format(TextResolver.GetText("EventActionType Description StartTradingSuperLuxuryResources"), eventAction.Empire.Name, eventAction.EmpireOther.Name);
                            }
                            break;
                        case EventActionType.StopTradingSuperLuxuryResources:
                            if (eventAction.Empire != null && eventAction.EmpireOther != null)
                            {
                                result = string.Format(TextResolver.GetText("EventActionType Description StopTradingSuperLuxuryResources"), eventAction.Empire.Name, eventAction.EmpireOther.Name);
                            }
                            break;
                        case EventActionType.GeneralMessageToEmpire:
                            if (eventAction.Empire != null)
                            {
                                result = string.Format(TextResolver.GetText("EventActionType Description GeneralMessageToEmpire"), eventAction.Empire.Name);
                            }
                            break;
                        case EventActionType.EmpireMessageToEmpire:
                            if (eventAction.Empire != null && eventAction.EmpireOther != null)
                            {
                                result = string.Format(TextResolver.GetText("EventActionType Description EmpireMessageToEmpire"), eventAction.Empire.Name, eventAction.EmpireOther.Name);
                            }
                            break;
                        case EventActionType.ResearchBonusInProject:
                            if (eventAction.Value >= 0 && eventAction.Value < ResearchNodeDefinitionsStatic.Count)
                            {
                                ResearchNodeDefinition researchNodeDefinition3 = ResearchNodeDefinitionsStatic[eventAction.Value];
                                if (researchNodeDefinition3 != null)
                                {
                                    result = string.Format(TextResolver.GetText("EventActionType Description ResearchBonusInProject"), researchNodeDefinition3.Name);
                                }
                            }
                            break;
                        case EventActionType.UnlockTechForEmpire:
                            if (eventAction.Empire != null && eventAction.Value >= 0 && eventAction.Value < ResearchNodeDefinitionsStatic.Count)
                            {
                                ResearchNodeDefinition researchNodeDefinition2 = ResearchNodeDefinitionsStatic[eventAction.Value];
                                if (researchNodeDefinition2 != null)
                                {
                                    result = string.Format(TextResolver.GetText("EventActionType Description UnlockTechForEmpire"), researchNodeDefinition2.Name, eventAction.Empire.Name);
                                }
                            }
                            break;
                        case EventActionType.EmpireDeclaresWarOnOtherEmpire:
                            if (eventAction.Empire != null && eventAction.EmpireOther != null)
                            {
                                result = string.Format(TextResolver.GetText("EventActionType Description EmpireDeclaresWarOnOtherEmpire"), eventAction.Empire.Name, eventAction.EmpireOther.Name);
                            }
                            break;
                        case EventActionType.VictoryConditionBonus:
                            if (eventAction.Empire != null)
                            {
                                result = string.Format(TextResolver.GetText("EventActionType Description VictoryConditionBonus"), eventAction.Empire.Name, eventAction.Value.ToString("+0;-0"));
                            }
                            break;
                        case EventActionType.SendFleetAttack:
                            if (eventAction.Target != null && eventAction.Empire != null && eventAction.EmpireOther != null)
                            {
                                result = string.Format(TextResolver.GetText("EventActionType Description SendFleetAttack"), eventAction.Empire.Name, eventAction.Target.Name, eventAction.EmpireOther.Name);
                            }
                            break;
                        case EventActionType.SendPlanetDestroyerAttack:
                            if (eventAction.Target != null && eventAction.Empire != null && eventAction.EmpireOther != null)
                            {
                                result = string.Format(TextResolver.GetText("EventActionType Description SendPlanetDestroyerAttack"), eventAction.Empire.Name, eventAction.Target.Name, eventAction.EmpireOther.Name);
                            }
                            break;
                        case EventActionType.IntergalacticConvoyMilitary:
                            if (eventAction.Empire != null && eventAction.Value > 0)
                            {
                                result = string.Format(TextResolver.GetText("EventActionType Description IntergalacticConvoyMilitary"), eventAction.Empire.Name, eventAction.Value.ToString());
                            }
                            break;
                        case EventActionType.IntergalacticConvoyCivilian:
                            if (eventAction.Empire != null && eventAction.Value > 0)
                            {
                                result = string.Format(TextResolver.GetText("EventActionType Description IntergalacticConvoyCivilian"), eventAction.Empire.Name, eventAction.Value.ToString());
                            }
                            break;
                        case EventActionType.CharacterGenerate:
                            if (eventAction.Empire != null)
                            {
                                if (eventAction.Character != null)
                                {
                                    result = string.Format(TextResolver.GetText("EventActionType Description CharacterGenerate"), ResolveDescription(eventAction.Character.Role), eventAction.Character.Name, eventAction.Target.Name);
                                }
                                else if (eventAction.CharacterRole != 0)
                                {
                                    result = string.Format(TextResolver.GetText("EventActionType Description CharacterGenerate Role"), ResolveDescription(eventAction.CharacterRole), eventAction.Target.Name);
                                }
                            }
                            break;
                        case EventActionType.CharacterKill:
                            if (eventAction.Empire != null && eventAction.Character != null)
                            {
                                result = string.Format(TextResolver.GetText("EventActionType Description CharacterKill"), ResolveDescription(eventAction.Character.Role), eventAction.Character.Name);
                            }
                            break;
                        case EventActionType.CharacterChangeEmpire:
                            if (eventAction.Empire != null && eventAction.EmpireOther != null && eventAction.Character != null)
                            {
                                result = string.Format(TextResolver.GetText("EventActionType Description CharacterChangeEmpire"), ResolveDescription(eventAction.Character.Role), eventAction.Character.Name, eventAction.Empire.Name, eventAction.EmpireOther.Name);
                            }
                            break;
                        case EventActionType.CharacterChangeRole:
                            if (eventAction.Empire != null && eventAction.Character != null && eventAction.CharacterRole != 0)
                            {
                                result = string.Format(TextResolver.GetText("EventActionType Description CharacterChangeRole"), eventAction.Character.Name, ResolveDescription(eventAction.Character.Role), ResolveDescription(eventAction.CharacterRole));
                            }
                            break;
                        case EventActionType.CharacterChangeImage:
                            if (eventAction.Empire != null && eventAction.Character != null)
                            {
                                result = string.Format(TextResolver.GetText("EventActionType Description CharacterChangeImage"), ResolveDescription(eventAction.Character.Role), eventAction.Character.Name, eventAction.ImageFilename);
                            }
                            break;
                    }
                }
            }
            return result;
        }

        public static string ResolveDescription(EventActionExecutionType eventActionExecutionType)
        {
            string result = string.Empty;
            switch (eventActionExecutionType)
            {
                case EventActionExecutionType.Immediately:
                    result = TextResolver.GetText("EventActionExecutionType Immediately");
                    break;
                case EventActionExecutionType.Delay:
                    result = TextResolver.GetText("EventActionExecutionType Delay");
                    break;
                case EventActionExecutionType.RandomDelay:
                    result = TextResolver.GetText("EventActionExecutionType RandomDelay");
                    break;
            }
            return result;
        }

        public static string ResolveDescription(EventActionType eventActionType)
        {
            string result = string.Empty;
            switch (eventActionType)
            {
                case EventActionType.AcquireBuiltObject:
                    result = TextResolver.GetText("EventActionType AcquireBuiltObject");
                    break;
                case EventActionType.AcquireHabitat:
                    result = TextResolver.GetText("EventActionType AcquireHabitat");
                    break;
                case EventActionType.BuildPlanetaryFacility:
                    result = TextResolver.GetText("EventActionType BuildPlanetaryFacility");
                    break;
                case EventActionType.ChangeEmpireGovernment:
                    result = TextResolver.GetText("EventActionType ChangeEmpireGovernment");
                    break;
                case EventActionType.ChangeRaceBias:
                    result = TextResolver.GetText("EventActionType ChangeRaceBias");
                    break;
                case EventActionType.RevealObject:
                    result = TextResolver.GetText("EventActionType RevealObject");
                    break;
                case EventActionType.DestroyBuiltObject:
                    result = TextResolver.GetText("EventActionType DestroyBuiltObject");
                    break;
                case EventActionType.DestroyPlanetaryFacility:
                    result = TextResolver.GetText("EventActionType DestroyPlanetaryFacility");
                    break;
                case EventActionType.DisasterAtColony:
                    result = TextResolver.GetText("EventActionType DisasterAtColony");
                    break;
                case EventActionType.EmpireDeclaresWarOnTriggerEmpire:
                    result = TextResolver.GetText("EventActionType EmpireDeclaresWarOnTriggerEmpire");
                    break;
                case EventActionType.EndPlague:
                    result = TextResolver.GetText("EventActionType EndPlague");
                    break;
                case EventActionType.EnemyFleetDefectsToTriggerEmpire:
                    result = TextResolver.GetText("EventActionType EnemyFleetDefectsToTriggerEmpire");
                    break;
                case EventActionType.FindMoneyTreasure:
                    result = TextResolver.GetText("EventActionType FindMoneyTreasure");
                    break;
                case EventActionType.GenerateBuiltObject:
                    result = TextResolver.GetText("EventActionType GenerateBuiltObject");
                    break;
                case EventActionType.GenerateCreatureSwarm:
                    result = TextResolver.GetText("EventActionType GenerateCreatureSwarm");
                    break;
                case EventActionType.GenerateErutkah:
                    result = TextResolver.GetText("EventActionType GenerateErutkah");
                    break;
                case EventActionType.GenerateNewEmpire:
                    result = TextResolver.GetText("EventActionType GenerateNewEmpire");
                    break;
                case EventActionType.GenerateNewPirateFaction:
                    result = TextResolver.GetText("EventActionType GenerateNewPirateFaction");
                    break;
                case EventActionType.GeneratePirateAmbush:
                    result = TextResolver.GetText("EventActionType GeneratePirateAmbush");
                    break;
                case EventActionType.GenerateRefugeeFleet:
                    result = TextResolver.GetText("EventActionType GenerateRefugeeFleet");
                    break;
                case EventActionType.GenerateResourceAtHabitat:
                    result = TextResolver.GetText("EventActionType GenerateResourceAtHabitat");
                    break;
                case EventActionType.InterceptResource:
                    result = TextResolver.GetText("EventActionType InterceptResource");
                    break;
                case EventActionType.LearnAboutLostColony:
                    result = TextResolver.GetText("EventActionType LearnAboutLostColony");
                    break;
                case EventActionType.LearnAboutSpecialLocation:
                    result = TextResolver.GetText("EventActionType LearnAboutSpecialLocation");
                    break;
                case EventActionType.LearnExplorationInfo:
                    result = TextResolver.GetText("EventActionType LearnExplorationInfo");
                    break;
                case EventActionType.LearnGovernmentType:
                    result = TextResolver.GetText("EventActionType LearnGovernmentType");
                    break;
                case EventActionType.LearnTech:
                    result = TextResolver.GetText("EventActionType LearnTech");
                    break;
                case EventActionType.MakeEmpireContact:
                    result = TextResolver.GetText("EventActionType MakeEmpireContact");
                    break;
                case EventActionType.PirateFactionJoinsTriggerEmpire:
                    result = TextResolver.GetText("EventActionType PirateFactionJoinsTriggerEmpire");
                    break;
                case EventActionType.RemoveResourceAtHabitat:
                    result = TextResolver.GetText("EventActionType RemoveResourceAtHabitat");
                    break;
                case EventActionType.SleepingRaceAwokenAtHabitat:
                    result = TextResolver.GetText("EventActionType SleepingRaceAwokenAtHabitat");
                    break;
                case EventActionType.SplitEmpireCivilWar:
                    result = TextResolver.GetText("EventActionType SplitEmpireCivilWar");
                    break;
                case EventActionType.SplitEmpirePeacefully:
                    result = TextResolver.GetText("EventActionType SplitEmpirePeacefully");
                    break;
                case EventActionType.StartPlague:
                    result = TextResolver.GetText("EventActionType StartPlague");
                    break;
                case EventActionType.UnlockTech:
                    result = TextResolver.GetText("EventActionType UnlockTech");
                    break;
                case EventActionType.ChangeEmpireReputation:
                    result = TextResolver.GetText("EventActionType ChangeEmpireReputation");
                    break;
                case EventActionType.ChangeEmpireEvaluation:
                    result = TextResolver.GetText("EventActionType ChangeEmpireEvaluation");
                    break;
                case EventActionType.InitiateTreaty:
                    result = TextResolver.GetText("EventActionType InitiateTreaty");
                    break;
                case EventActionType.BreakTreaty:
                    result = TextResolver.GetText("EventActionType BreakTreaty");
                    break;
                case EventActionType.StartTradingSuperLuxuryResources:
                    result = TextResolver.GetText("EventActionType StartTradingSuperLuxuryResources");
                    break;
                case EventActionType.StopTradingSuperLuxuryResources:
                    result = TextResolver.GetText("EventActionType StopTradingSuperLuxuryResources");
                    break;
                case EventActionType.GeneralMessageToEmpire:
                    result = TextResolver.GetText("EventActionType GeneralMessageToEmpire");
                    break;
                case EventActionType.EmpireMessageToEmpire:
                    result = TextResolver.GetText("EventActionType EmpireMessageToEmpire");
                    break;
                case EventActionType.ResearchBonusInProject:
                    result = TextResolver.GetText("EventActionType ResearchBonusInProject");
                    break;
                case EventActionType.UnlockTechForEmpire:
                    result = TextResolver.GetText("EventActionType UnlockTechForEmpire");
                    break;
                case EventActionType.EmpireDeclaresWarOnOtherEmpire:
                    result = TextResolver.GetText("EventActionType EmpireDeclaresWarOnOtherEmpire");
                    break;
                case EventActionType.VictoryConditionBonus:
                    result = TextResolver.GetText("EventActionType VictoryConditionBonus");
                    break;
                case EventActionType.SendFleetAttack:
                    result = TextResolver.GetText("EventActionType SendFleetAttack");
                    break;
                case EventActionType.SendPlanetDestroyerAttack:
                    result = TextResolver.GetText("EventActionType SendPlanetDestroyerAttack");
                    break;
                case EventActionType.IntergalacticConvoyMilitary:
                    result = TextResolver.GetText("EventActionType IntergalacticConvoyMilitary");
                    break;
                case EventActionType.IntergalacticConvoyCivilian:
                    result = TextResolver.GetText("EventActionType IntergalacticConvoyCivilian");
                    break;
                case EventActionType.CharacterGenerate:
                    result = TextResolver.GetText("EventActionType CharacterGenerate");
                    break;
                case EventActionType.CharacterKill:
                    result = TextResolver.GetText("EventActionType CharacterKill");
                    break;
                case EventActionType.CharacterChangeEmpire:
                    result = TextResolver.GetText("EventActionType CharacterChangeEmpire");
                    break;
                case EventActionType.CharacterChangeRole:
                    result = TextResolver.GetText("EventActionType CharacterChangeRole");
                    break;
                case EventActionType.CharacterChangeImage:
                    result = TextResolver.GetText("EventActionType CharacterChangeImage");
                    break;
            }
            return result;
        }

        public static string ResolveDescription(CharacterTraitType traitType)
        {
            string result = string.Empty;
            switch (traitType)
            {
                case CharacterTraitType.IntelligenceUninhibited:
                    result = TextResolver.GetText("Character Trait Uninhibited");
                    break;
                case CharacterTraitType.IntelligenceMeasured:
                    result = TextResolver.GetText("Character Trait Measured");
                    break;
                case CharacterTraitType.IntelligenceAddict:
                    result = TextResolver.GetText("Character Trait Addict");
                    break;
                case CharacterTraitType.IntelligenceSober:
                    result = TextResolver.GetText("Character Trait Sober");
                    break;
                case CharacterTraitType.IntelligenceCourageous:
                    result = TextResolver.GetText("Character Trait Courageous");
                    break;
                case CharacterTraitType.IntelligenceWeak:
                    result = TextResolver.GetText("Character Trait Weak");
                    break;
                case CharacterTraitType.IntelligenceTolerant:
                    result = TextResolver.GetText("Character Trait Tolerant");
                    break;
                case CharacterTraitType.IntelligenceXenophobic:
                    result = TextResolver.GetText("Character Trait Xenophobic");
                    break;
                case CharacterTraitType.IntelligenceEloquentSpeaker:
                    result = TextResolver.GetText("Character Trait EloquentSpeaker");
                    break;
                case CharacterTraitType.IntelligencePoorSpeaker:
                    result = TextResolver.GetText("Character Trait PoorSpeaker");
                    break;
                case CharacterTraitType.IntelligenceCorrupt:
                    result = TextResolver.GetText("Character Trait Corrupt");
                    break;
                case CharacterTraitType.IntelligenceLawful:
                    result = TextResolver.GetText("Character Trait Lawful");
                    break;
                case CharacterTraitType.Undefined:
                    result = TextResolver.GetText("None");
                    break;
                case CharacterTraitType.Addict:
                    result = TextResolver.GetText("Character Trait Addict");
                    break;
                case CharacterTraitType.BeanCounter:
                    result = TextResolver.GetText("Character Trait BeanCounter");
                    break;
                case CharacterTraitType.CarefulAttacker:
                    result = TextResolver.GetText("Character Trait CarefulAttacker");
                    break;
                case CharacterTraitType.Corrupt:
                    result = TextResolver.GetText("Character Trait Corrupt");
                    break;
                case CharacterTraitType.Courageous:
                    result = TextResolver.GetText("Character Trait Courageous");
                    break;
                case CharacterTraitType.Creative:
                    result = TextResolver.GetText("Character Trait Creative");
                    break;
                case CharacterTraitType.Demoralizing:
                    result = TextResolver.GetText("Character Trait Demoralizing");
                    break;
                case CharacterTraitType.Diplomat:
                    result = TextResolver.GetText("Character Trait Diplomat");
                    break;
                case CharacterTraitType.Disliked:
                    result = TextResolver.GetText("Character Trait Disliked");
                    break;
                case CharacterTraitType.Disorganized:
                    result = TextResolver.GetText("Character Trait Disorganized");
                    break;
                case CharacterTraitType.DoubleAgent:
                    result = TextResolver.GetText("Character Trait DoubleAgent");
                    break;
                case CharacterTraitType.Drunk:
                    result = TextResolver.GetText("Character Trait Drunk");
                    break;
                case CharacterTraitType.EloquentSpeaker:
                    result = TextResolver.GetText("Character Trait EloquentSpeaker");
                    break;
                case CharacterTraitType.Energetic:
                    result = TextResolver.GetText("Character Trait Energetic");
                    break;
                case CharacterTraitType.Engineer:
                    result = TextResolver.GetText("Character Trait Engineer");
                    break;
                case CharacterTraitType.Environmentalist:
                    result = TextResolver.GetText("Character Trait Environmentalist");
                    break;
                case CharacterTraitType.Expansionist:
                    result = TextResolver.GetText("Character Trait Expansionist");
                    break;
                case CharacterTraitType.Famous:
                    result = TextResolver.GetText("Character Trait Famous");
                    break;
                case CharacterTraitType.ForeignSpy:
                    result = TextResolver.GetText("Character Trait ForeignSpy");
                    break;
                case CharacterTraitType.FreeTrader:
                    result = TextResolver.GetText("Character Trait FreeTrader");
                    break;
                case CharacterTraitType.Generous:
                    result = TextResolver.GetText("Character Trait Generous");
                    break;
                case CharacterTraitType.GoodAdministrator:
                    result = TextResolver.GetText("Character Trait GoodAdministrator");
                    break;
                case CharacterTraitType.GoodGroundLogistician:
                    result = TextResolver.GetText("Character Trait GoodGroundLogistician");
                    break;
                case CharacterTraitType.GoodRecruiter:
                    result = TextResolver.GetText("Character Trait GoodRecruiter");
                    break;
                case CharacterTraitType.GoodSpaceLogistician:
                    result = TextResolver.GetText("Character Trait GoodSpaceLogistician");
                    break;
                case CharacterTraitType.GoodStrategist:
                    result = TextResolver.GetText("Character Trait GoodStrategist");
                    break;
                case CharacterTraitType.GoodTactician:
                    result = TextResolver.GetText("Character Trait GoodTactician");
                    break;
                case CharacterTraitType.HealthOriented:
                    result = TextResolver.GetText("Character Trait HealthOriented");
                    break;
                case CharacterTraitType.Industrialist:
                    result = TextResolver.GetText("Character Trait Industrialist");
                    break;
                case CharacterTraitType.InspiringPresence:
                    result = TextResolver.GetText("Character Trait InspiringPresence");
                    break;
                case CharacterTraitType.Isolationist:
                    result = TextResolver.GetText("Character Trait Isolationist");
                    break;
                case CharacterTraitType.LaborOriented:
                    result = TextResolver.GetText("Character Trait LaborOriented");
                    break;
                case CharacterTraitType.Lawful:
                    result = TextResolver.GetText("Character Trait Lawful");
                    break;
                case CharacterTraitType.LaxDiscipline:
                    result = TextResolver.GetText("Character Trait LaxDiscipline");
                    break;
                case CharacterTraitType.Lazy:
                    result = TextResolver.GetText("Character Trait Lazy");
                    break;
                case CharacterTraitType.Linguist:
                    result = TextResolver.GetText("Character Trait Linguist");
                    break;
                case CharacterTraitType.LocalDefenseTactics:
                    result = TextResolver.GetText("Character Trait LocalDefenseTactics");
                    break;
                case CharacterTraitType.Logical:
                    result = TextResolver.GetText("Character Trait Logical");
                    break;
                case CharacterTraitType.Luddite:
                    result = TextResolver.GetText("Character Trait Luddite");
                    break;
                case CharacterTraitType.Measured:
                    result = TextResolver.GetText("Character Trait Measured");
                    break;
                case CharacterTraitType.Methodical:
                    result = TextResolver.GetText("Character Trait Methodical");
                    break;
                case CharacterTraitType.NaturalGroundLeader:
                    result = TextResolver.GetText("Character Trait NaturalGroundLeader");
                    break;
                case CharacterTraitType.NaturalSpaceLeader:
                    result = TextResolver.GetText("Character Trait NaturalSpaceLeader");
                    break;
                case CharacterTraitType.NonTechnical:
                    result = TextResolver.GetText("Character Trait NonTechnical");
                    break;
                case CharacterTraitType.Obnoxious:
                    result = TextResolver.GetText("Character Trait Obnoxious");
                    break;
                case CharacterTraitType.Organized:
                    result = TextResolver.GetText("Character Trait Organized");
                    break;
                case CharacterTraitType.Pacifist:
                    result = TextResolver.GetText("Character Trait Pacifist");
                    break;
                case CharacterTraitType.Paranoid:
                    result = TextResolver.GetText("Character Trait Paranoid");
                    break;
                case CharacterTraitType.Patriot:
                    result = TextResolver.GetText("Character Trait Patriot");
                    break;
                case CharacterTraitType.PeaceThroughStrength:
                    result = TextResolver.GetText("Character Trait PeaceThroughStrength");
                    break;
                case CharacterTraitType.PlanetarySupport:
                    result = TextResolver.GetText("Character Trait PlanetarySupport");
                    break;
                case CharacterTraitType.PoorAdministrator:
                    result = TextResolver.GetText("Character Trait PoorAdministrator");
                    break;
                case CharacterTraitType.PoorGroundAttacker:
                    result = TextResolver.GetText("Character Trait PoorGroundAttacker");
                    break;
                case CharacterTraitType.PoorGroundDefender:
                    result = TextResolver.GetText("Character Trait PoorGroundDefender");
                    break;
                case CharacterTraitType.PoorGroundLogistician:
                    result = TextResolver.GetText("Character Trait PoorGroundLogistician");
                    break;
                case CharacterTraitType.PoorNavigator:
                    result = TextResolver.GetText("Character Trait PoorNavigator");
                    break;
                case CharacterTraitType.PoorRecruiter:
                    result = TextResolver.GetText("Character Trait PoorRecruiter");
                    break;
                case CharacterTraitType.PoorSpaceAttacker:
                    result = TextResolver.GetText("Character Trait PoorSpaceAttacker");
                    break;
                case CharacterTraitType.PoorSpaceDefender:
                    result = TextResolver.GetText("Character Trait PoorSpaceDefender");
                    break;
                case CharacterTraitType.PoorSpaceLogistician:
                    result = TextResolver.GetText("Character Trait PoorSpaceLogistician");
                    break;
                case CharacterTraitType.PoorSpeaker:
                    result = TextResolver.GetText("Character Trait PoorSpeaker");
                    break;
                case CharacterTraitType.PoorStrategist:
                    result = TextResolver.GetText("Character Trait PoorStrategist");
                    break;
                case CharacterTraitType.PoorTactician:
                    result = TextResolver.GetText("Character Trait PoorTactician");
                    break;
                case CharacterTraitType.Protectionist:
                    result = TextResolver.GetText("Character Trait Protectionist");
                    break;
                case CharacterTraitType.RecklessAttacker:
                    result = TextResolver.GetText("Character Trait RecklessAttacker");
                    break;
                case CharacterTraitType.SkilledNavigator:
                    result = TextResolver.GetText("Character Trait SkilledNavigator");
                    break;
                case CharacterTraitType.Sober:
                    result = TextResolver.GetText("Character Trait Sober");
                    break;
                case CharacterTraitType.Spiritual:
                    result = TextResolver.GetText("Character Trait Spiritual");
                    break;
                case CharacterTraitType.StrongGroundAttacker:
                    result = TextResolver.GetText("Character Trait StrongGroundAttacker");
                    break;
                case CharacterTraitType.StrongGroundDefender:
                    result = TextResolver.GetText("Character Trait StrongGroundDefender");
                    break;
                case CharacterTraitType.StrongSpaceAttacker:
                    result = TextResolver.GetText("Character Trait StrongSpaceAttacker");
                    break;
                case CharacterTraitType.StrongSpaceDefender:
                    result = TextResolver.GetText("Character Trait StrongSpaceDefender");
                    break;
                case CharacterTraitType.Technical:
                    result = TextResolver.GetText("Character Trait Technical");
                    break;
                case CharacterTraitType.Tolerant:
                    result = TextResolver.GetText("Character Trait Tolerant");
                    break;
                case CharacterTraitType.TongueTied:
                    result = TextResolver.GetText("Character Trait TongueTied");
                    break;
                case CharacterTraitType.ToughDiscipline:
                    result = TextResolver.GetText("Character Trait ToughDiscipline");
                    break;
                case CharacterTraitType.Trusting:
                    result = TextResolver.GetText("Character Trait Trusting");
                    break;
                case CharacterTraitType.UltraGenius:
                    result = TextResolver.GetText("Character Trait UltraGenius");
                    break;
                case CharacterTraitType.Uninhibited:
                    result = TextResolver.GetText("Character Trait Uninhibited");
                    break;
                case CharacterTraitType.Weak:
                    result = TextResolver.GetText("Character Trait Weak");
                    break;
                case CharacterTraitType.Xenophobic:
                    result = TextResolver.GetText("Character Trait Xenophobic");
                    break;
                case CharacterTraitType.Smuggler:
                    result = TextResolver.GetText("Character Trait Smuggler");
                    break;
                case CharacterTraitType.BountyHunter:
                    result = TextResolver.GetText("Character Trait BountyHunter");
                    break;
            }
            return result;
        }

        public static string ResolveDescription(CharacterSkillType skillType)
        {
            string result = string.Empty;
            switch (skillType)
            {
                case CharacterSkillType.Undefined:
                    result = TextResolver.GetText("None");
                    break;
                case CharacterSkillType.Assassination:
                    result = TextResolver.GetText("Character Skill Assassination");
                    break;
                case CharacterSkillType.CivilianBaseMaintenance:
                    result = TextResolver.GetText("Character Skill CivilianBaseMaintenance");
                    break;
                case CharacterSkillType.CivilianShipConstructionSpeed:
                    result = TextResolver.GetText("Character Skill CivilianShipConstructionSpeed");
                    break;
                case CharacterSkillType.CivilianShipMaintenance:
                    result = TextResolver.GetText("Character Skill CivilianShipMaintenance");
                    break;
                case CharacterSkillType.ColonyCorruption:
                    result = TextResolver.GetText("Character Skill ColonyCorruption");
                    break;
                case CharacterSkillType.ColonyHappiness:
                    result = TextResolver.GetText("Character Skill ColonyHappiness");
                    break;
                case CharacterSkillType.ColonyIncome:
                    result = TextResolver.GetText("Character Skill ColonyIncome");
                    break;
                case CharacterSkillType.ColonyShipConstructionSpeed:
                    result = TextResolver.GetText("Character Skill ColonyShipConstructionSpeed");
                    break;
                case CharacterSkillType.Concealment:
                    result = TextResolver.GetText("Character Skill Concealment");
                    break;
                case CharacterSkillType.CounterEspionage:
                    result = TextResolver.GetText("Character Skill CounterEspionage");
                    break;
                case CharacterSkillType.Countermeasures:
                    result = TextResolver.GetText("Character Skill Countermeasures");
                    break;
                case CharacterSkillType.DamageControl:
                    result = TextResolver.GetText("Character Skill DamageControl");
                    break;
                case CharacterSkillType.Diplomacy:
                    result = TextResolver.GetText("Character Skill Diplomacy");
                    break;
                case CharacterSkillType.Espionage:
                    result = TextResolver.GetText("Character Skill Espionage");
                    break;
                case CharacterSkillType.FacilityConstructionSpeed:
                    result = TextResolver.GetText("Character Skill FacilityConstructionSpeed");
                    break;
                case CharacterSkillType.Fighters:
                    result = TextResolver.GetText("Character Skill Fighters");
                    break;
                case CharacterSkillType.HyperjumpSpeed:
                    result = TextResolver.GetText("Character Skill HyperjumpSpeed");
                    break;
                case CharacterSkillType.MilitaryBaseMaintenance:
                    result = TextResolver.GetText("Character Skill MilitaryBaseMaintenance");
                    break;
                case CharacterSkillType.MilitaryShipConstructionSpeed:
                    result = TextResolver.GetText("Character Skill MilitaryShipConstructionSpeed");
                    break;
                case CharacterSkillType.MilitaryShipMaintenance:
                    result = TextResolver.GetText("Character Skill MilitaryShipMaintenance");
                    break;
                case CharacterSkillType.MiningRate:
                    result = TextResolver.GetText("Character Skill MiningRate");
                    break;
                case CharacterSkillType.PopulationGrowth:
                    result = TextResolver.GetText("Character Skill PopulationGrowth");
                    break;
                case CharacterSkillType.PsyOps:
                    result = TextResolver.GetText("Character Skill PsyOps");
                    break;
                case CharacterSkillType.RepairBonus:
                    result = TextResolver.GetText("Character Skill RepairBonus");
                    break;
                case CharacterSkillType.ResearchEnergy:
                    result = TextResolver.GetText("Character Skill ResearchEnergy");
                    break;
                case CharacterSkillType.ResearchHighTech:
                    result = TextResolver.GetText("Character Skill ResearchHighTech");
                    break;
                case CharacterSkillType.ResearchWeapons:
                    result = TextResolver.GetText("Character Skill ResearchWeapons");
                    break;
                case CharacterSkillType.Sabotage:
                    result = TextResolver.GetText("Character Skill Sabotage");
                    break;
                case CharacterSkillType.ShieldRechargeRate:
                    result = TextResolver.GetText("Character Skill ShieldRechargeRate");
                    break;
                case CharacterSkillType.ShipEnergyUsage:
                    result = TextResolver.GetText("Character Skill ShipEnergyUsage");
                    break;
                case CharacterSkillType.ShipManeuvering:
                    result = TextResolver.GetText("Character Skill ShipManeuvering");
                    break;
                case CharacterSkillType.Targeting:
                    result = TextResolver.GetText("Character Skill Targeting");
                    break;
                case CharacterSkillType.TourismIncome:
                    result = TextResolver.GetText("Character Skill TourismIncome");
                    break;
                case CharacterSkillType.TradeIncome:
                    result = TextResolver.GetText("Character Skill TradeIncome");
                    break;
                case CharacterSkillType.TroopExperienceGain:
                    result = TextResolver.GetText("Character Skill TroopExperienceGain");
                    break;
                case CharacterSkillType.TroopGroundAttack:
                    result = TextResolver.GetText("Character Skill TroopGroundAttack");
                    break;
                case CharacterSkillType.TroopGroundDefense:
                    result = TextResolver.GetText("Character Skill TroopGroundDefense");
                    break;
                case CharacterSkillType.TroopMaintenance:
                    result = TextResolver.GetText("Character Skill TroopMaintenance");
                    break;
                case CharacterSkillType.TroopRecoveryRate:
                    result = TextResolver.GetText("Character Skill TroopRecoveryRate");
                    break;
                case CharacterSkillType.TroopRecruitment:
                    result = TextResolver.GetText("Character Skill TroopRecruitment");
                    break;
                case CharacterSkillType.WarWeariness:
                    result = TextResolver.GetText("Character Skill WarWeariness");
                    break;
                case CharacterSkillType.WeaponsDamage:
                    result = TextResolver.GetText("Character Skill WeaponsDamage");
                    break;
                case CharacterSkillType.WeaponsRange:
                    result = TextResolver.GetText("Character Skill WeaponsRange");
                    break;
                case CharacterSkillType.TroopStrengthArmor:
                    result = TextResolver.GetText("Character Skill TroopStrengthArmor");
                    break;
                case CharacterSkillType.TroopStrengthInfantry:
                    result = TextResolver.GetText("Character Skill TroopStrengthInfantry");
                    break;
                case CharacterSkillType.TroopStrengthSpecialForces:
                    result = TextResolver.GetText("Character Skill TroopStrengthSpecialForces");
                    break;
                case CharacterSkillType.TroopStrengthPlanetaryDefense:
                    result = TextResolver.GetText("Character Skill TroopStrengthPlanetaryDefense");
                    break;
                case CharacterSkillType.SmugglingIncome:
                    result = TextResolver.GetText("Character Skill SmugglingIncome");
                    break;
                case CharacterSkillType.SmugglingEvasion:
                    result = TextResolver.GetText("Character Skill SmugglingEvasion");
                    break;
                case CharacterSkillType.BoardingAssault:
                    result = TextResolver.GetText("Character Skill BoardingAssault");
                    break;
            }
            return result;
        }

        public static string ResolveDescription(CharacterRole role)
        {
            string result = string.Empty;
            switch (role)
            {
                case CharacterRole.Ambassador:
                    result = TextResolver.GetText("Ambassador");
                    break;
                case CharacterRole.ColonyGovernor:
                    result = TextResolver.GetText("Colony Governor");
                    break;
                case CharacterRole.FleetAdmiral:
                    result = TextResolver.GetText("Fleet Admiral");
                    break;
                case CharacterRole.IntelligenceAgent:
                    result = TextResolver.GetText("Intelligence Agent");
                    break;
                case CharacterRole.Leader:
                    result = TextResolver.GetText("Leader");
                    break;
                case CharacterRole.Scientist:
                    result = TextResolver.GetText("Scientist");
                    break;
                case CharacterRole.TroopGeneral:
                    result = TextResolver.GetText("Troop General");
                    break;
                case CharacterRole.PirateLeader:
                    result = TextResolver.GetText("Pirate Leader");
                    break;
                case CharacterRole.ShipCaptain:
                    result = TextResolver.GetText("Ship Captain");
                    break;
            }
            return result;
        }

        public static string ResolveDescription(ColonyPopulationPolicy colonyPopulationPolicy)
        {
            string result = string.Empty;
            switch (colonyPopulationPolicy)
            {
                case ColonyPopulationPolicy.Assimilate:
                    result = TextResolver.GetText("Assimilate");
                    break;
                case ColonyPopulationPolicy.DoNotAccept:
                    result = TextResolver.GetText("Do Not Accept");
                    break;
                case ColonyPopulationPolicy.Resettle:
                    result = TextResolver.GetText("Resettle");
                    break;
                case ColonyPopulationPolicy.Enslave:
                    result = TextResolver.GetText("Enslave");
                    break;
                case ColonyPopulationPolicy.Exterminate:
                    result = TextResolver.GetText("Exterminate");
                    break;
            }
            return result;
        }

        public static string ResolveDescription(BuiltObjectMissionType missionType)
        {
            string empty = string.Empty;
            return missionType switch
            {
                BuiltObjectMissionType.ExtractResources => TextResolver.GetText("Mission Mine"),
                BuiltObjectMissionType.Undefined => "(" + TextResolver.GetText("No mission") + ")",
                BuiltObjectMissionType.Attack => TextResolver.GetText("Mission Attack"),
                BuiltObjectMissionType.Blockade => TextResolver.GetText("Mission Blockade"),
                BuiltObjectMissionType.Bombard => TextResolver.GetText("Mission Bombard"),
                BuiltObjectMissionType.Build => TextResolver.GetText("Mission Build"),
                BuiltObjectMissionType.BuildRepair => TextResolver.GetText("Mission Build"),
                BuiltObjectMissionType.Colonize => TextResolver.GetText("Mission Colonize"),
                BuiltObjectMissionType.Deploy => TextResolver.GetText("Mission Deploy"),
                BuiltObjectMissionType.Escape => TextResolver.GetText("Mission Escape"),
                BuiltObjectMissionType.Escort => TextResolver.GetText("Mission Escort"),
                BuiltObjectMissionType.Explore => TextResolver.GetText("Mission Explore"),
                BuiltObjectMissionType.Hold => TextResolver.GetText("Mission Wait"),
                BuiltObjectMissionType.LoadTroops => TextResolver.GetText("Mission Load Troops"),
                BuiltObjectMissionType.Move => TextResolver.GetText("Mission Move"),
                BuiltObjectMissionType.MoveAndWait => TextResolver.GetText("Mission Move and Wait"),
                BuiltObjectMissionType.Patrol => TextResolver.GetText("Mission Patrol"),
                BuiltObjectMissionType.Refuel => TextResolver.GetText("Mission Refuel"),
                BuiltObjectMissionType.Repair => TextResolver.GetText("Mission Repair"),
                BuiltObjectMissionType.Rescue => TextResolver.GetText("Mission Rescue"),
                BuiltObjectMissionType.Retire => TextResolver.GetText("Mission Retire"),
                BuiltObjectMissionType.Retrofit => TextResolver.GetText("Mission Retrofit"),
                BuiltObjectMissionType.Transport => TextResolver.GetText("Mission Transport"),
                BuiltObjectMissionType.Undeploy => TextResolver.GetText("Mission Undeploy"),
                BuiltObjectMissionType.UnloadTroops => TextResolver.GetText("Mission Unload Troops"),
                BuiltObjectMissionType.WaitAndAttack => TextResolver.GetText("Mission Prepare and Attack"),
                BuiltObjectMissionType.WaitAndBombard => TextResolver.GetText("Mission Prepare and Bombard"),
                BuiltObjectMissionType.Waypoint => TextResolver.GetText("Mission Waypoint"),
                _ => SplitString(missionType.ToString()),
            };
        }

        public static string ResolveRaceFamilyDescription(byte raceFamilyId)
        {
            if (raceFamilyId >= 0 && raceFamilyId < RaceFamiliesStatic.Count)
            {
                return RaceFamiliesStatic[raceFamilyId].Name;
            }
            return string.Empty;
        }

        public static string ResolveDescription(BuiltObjectRole role)
        {
            return role switch
            {
                BuiltObjectRole.Base => TextResolver.GetText("Ship Role Base"),
                BuiltObjectRole.Build => TextResolver.GetText("Ship Role Build"),
                BuiltObjectRole.Colony => TextResolver.GetText("Ship Role Colony"),
                BuiltObjectRole.Exploration => TextResolver.GetText("Ship Role Exploration"),
                BuiltObjectRole.Freight => TextResolver.GetText("Ship Role Freight"),
                BuiltObjectRole.Military => TextResolver.GetText("Ship Role Military"),
                BuiltObjectRole.Passenger => TextResolver.GetText("Ship Role Passenger"),
                BuiltObjectRole.Resource => TextResolver.GetText("Ship Role Resource"),
                BuiltObjectRole.Undefined => TextResolver.GetText("None"),
                _ => SplitString(role.ToString()),
            };
        }

        public static string ResolveDescriptionFleetPosture(ShipGroup fleet)
        {
            string result = "(" + TextResolver.GetText("None") + ")";
            if (fleet != null)
            {
                switch (fleet.Posture)
                {
                    case FleetPosture.Attack:
                        result = ((fleet.AttackPoint == null) ? TextResolver.GetText("Fleet Posture Attack Anywhere") : ((!(fleet.PostureRangeSquared <= 2250000.0)) ? ((!(fleet.PostureRangeSquared <= 2304000000.0)) ? ((!(fleet.PostureRangeSquared <= 250000000000.0)) ? ((!(fleet.PostureRangeSquared <= 1000000000000.0)) ? string.Format(TextResolver.GetText("Fleet Posture Attack Unlimited"), fleet.AttackPoint.Name) : string.Format(TextResolver.GetText("Fleet Posture Attack Sector"), fleet.AttackPoint.Name)) : string.Format(TextResolver.GetText("Fleet Posture Attack Area"), fleet.AttackPoint.Name)) : string.Format(TextResolver.GetText("Fleet Posture Attack System"), fleet.AttackPoint.Name)) : string.Format(TextResolver.GetText("Fleet Posture Attack Target"), fleet.AttackPoint.Name)));
                        break;
                    case FleetPosture.Defend:
                        result = ((fleet.GatherPoint == null) ? TextResolver.GetText("Fleet Posture Defend Anywhere") : ((!(fleet.PostureRangeSquared <= 2250000.0)) ? ((!(fleet.PostureRangeSquared <= 2304000000.0)) ? ((!(fleet.PostureRangeSquared <= 250000000000.0)) ? ((!(fleet.PostureRangeSquared <= 1000000000000.0)) ? string.Format(TextResolver.GetText("Fleet Posture Defend Unlimited"), fleet.GatherPoint.Name) : string.Format(TextResolver.GetText("Fleet Posture Defend Sector"), fleet.GatherPoint.Name)) : string.Format(TextResolver.GetText("Fleet Posture Defend Area"), fleet.GatherPoint.Name)) : string.Format(TextResolver.GetText("Fleet Posture Defend System"), fleet.GatherPoint.Name)) : string.Format(TextResolver.GetText("Fleet Posture Defend Target"), fleet.GatherPoint.Name)));
                        break;
                }
            }
            return result;
        }

        public static string ResolveDescription(BuiltObjectFleeWhen fleeWhen)
        {
            return fleeWhen switch
            {
                BuiltObjectFleeWhen.Attacked => TextResolver.GetText("Flee When Attacked"),
                BuiltObjectFleeWhen.EnemyMilitarySighted => TextResolver.GetText("Flee When Enemy Military Sighted"),
                BuiltObjectFleeWhen.Never => TextResolver.GetText("Flee When Never"),
                BuiltObjectFleeWhen.Shields20 => TextResolver.GetText("Flee When Shields 20"),
                BuiltObjectFleeWhen.Armor50 => TextResolver.GetText("Flee When Armor 50 or Shields 20"),
                BuiltObjectFleeWhen.Shields50 => TextResolver.GetText("Flee When Shields 50"),
                BuiltObjectFleeWhen.Undefined => TextResolver.GetText("None"),
                _ => SplitString(fleeWhen.ToString()),
            };
        }

        public static string ResolveDescription(BuiltObjectSubRole subRole)
        {
            return subRole switch
            {
                BuiltObjectSubRole.Carrier => TextResolver.GetText("Ship SubRole Carrier"),
                BuiltObjectSubRole.CapitalShip => TextResolver.GetText("Ship SubRole CapitalShip"),
                BuiltObjectSubRole.ColonyShip => TextResolver.GetText("Ship SubRole ColonyShip"),
                BuiltObjectSubRole.ConstructionShip => TextResolver.GetText("Ship SubRole ConstructionShip"),
                BuiltObjectSubRole.Cruiser => TextResolver.GetText("Ship SubRole Cruiser"),
                BuiltObjectSubRole.DefensiveBase => TextResolver.GetText("Ship SubRole DefensiveBase"),
                BuiltObjectSubRole.Destroyer => TextResolver.GetText("Ship SubRole Destroyer"),
                BuiltObjectSubRole.EnergyResearchStation => TextResolver.GetText("Ship SubRole EnergyResearchStation"),
                BuiltObjectSubRole.Escort => TextResolver.GetText("Ship SubRole Escort"),
                BuiltObjectSubRole.ExplorationShip => TextResolver.GetText("Ship SubRole ExplorationShip"),
                BuiltObjectSubRole.Frigate => TextResolver.GetText("Ship SubRole Frigate"),
                BuiltObjectSubRole.GasMiningShip => TextResolver.GetText("Ship SubRole GasMiningShip"),
                BuiltObjectSubRole.GasMiningStation => TextResolver.GetText("Ship SubRole GasMiningStation"),
                BuiltObjectSubRole.GenericBase => TextResolver.GetText("Ship SubRole GenericBase"),
                BuiltObjectSubRole.HighTechResearchStation => TextResolver.GetText("Ship SubRole HighTechResearchStation"),
                BuiltObjectSubRole.LargeFreighter => TextResolver.GetText("Ship SubRole LargeFreighter"),
                BuiltObjectSubRole.LargeSpacePort => TextResolver.GetText("Ship SubRole LargeSpacePort"),
                BuiltObjectSubRole.MediumFreighter => TextResolver.GetText("Ship SubRole MediumFreighter"),
                BuiltObjectSubRole.MediumSpacePort => TextResolver.GetText("Ship SubRole MediumSpacePort"),
                BuiltObjectSubRole.MiningShip => TextResolver.GetText("Ship SubRole MiningShip"),
                BuiltObjectSubRole.MiningStation => TextResolver.GetText("Ship SubRole MiningStation"),
                BuiltObjectSubRole.MonitoringStation => TextResolver.GetText("Ship SubRole MonitoringStation"),
                BuiltObjectSubRole.PassengerShip => TextResolver.GetText("Ship SubRole PassengerShip"),
                BuiltObjectSubRole.ResortBase => TextResolver.GetText("Ship SubRole ResortBase"),
                BuiltObjectSubRole.ResupplyShip => TextResolver.GetText("Ship SubRole ResupplyShip"),
                BuiltObjectSubRole.SmallFreighter => TextResolver.GetText("Ship SubRole SmallFreighter"),
                BuiltObjectSubRole.SmallSpacePort => TextResolver.GetText("Ship SubRole SmallSpacePort"),
                BuiltObjectSubRole.TroopTransport => TextResolver.GetText("Ship SubRole TroopTransport"),
                BuiltObjectSubRole.WeaponsResearchStation => TextResolver.GetText("Ship SubRole WeaponsResearchStation"),
                BuiltObjectSubRole.Undefined => TextResolver.GetText("None"),
                _ => SplitString(subRole.ToString()),
            };
        }

        public static string ResolveDescription(BuiltObjectStance stance)
        {
            return stance switch
            {
                BuiltObjectStance.AttackEnemies => TextResolver.GetText("Stance AttackEnemies"),
                BuiltObjectStance.AttackIfAttacked => TextResolver.GetText("Stance AttackIfAttacked"),
                BuiltObjectStance.AttackUnallied => TextResolver.GetText("Stance AttackUnallied"),
                BuiltObjectStance.DoNotAttack => TextResolver.GetText("Stance DoNotAttack"),
                BuiltObjectStance.Undefined => TextResolver.GetText("None"),
                _ => SplitString(stance.ToString()),
            };
        }

        public static string ResolveDescription(CommandAction action)
        {
            return SplitString(action.ToString());
        }

        public static string ResolveComponentCategoryAbbreviation(ComponentCategoryType category)
        {
            return category switch
            {
                ComponentCategoryType.Armor => "ARM",
                ComponentCategoryType.Computer => "CMP",
                ComponentCategoryType.Construction => "CST",
                ComponentCategoryType.EnergyCollector => "ECL",
                ComponentCategoryType.Engine => "ENG",
                ComponentCategoryType.Extractor => "EXT",
                ComponentCategoryType.Habitation => "HAB",
                ComponentCategoryType.HyperDrive => "HYP",
                ComponentCategoryType.Labs => "LAB",
                ComponentCategoryType.Manufacturer => "MNF",
                ComponentCategoryType.Reactor => "RCT",
                ComponentCategoryType.Sensor => "SEN",
                ComponentCategoryType.Shields => "SHD",
                ComponentCategoryType.ShieldRecharge => "SHR",
                ComponentCategoryType.Storage => "STR",
                ComponentCategoryType.WeaponArea => "WAR",
                ComponentCategoryType.WeaponBeam => "WBM",
                ComponentCategoryType.WeaponSuperArea => "WSA",
                ComponentCategoryType.WeaponSuperBeam => "WSB",
                ComponentCategoryType.WeaponSuperTorpedo => "WST",
                ComponentCategoryType.WeaponTorpedo => "WTP",
                ComponentCategoryType.Fighter => "FTR",
                ComponentCategoryType.WeaponPointDefense => "WPD",
                ComponentCategoryType.WeaponIon => "WIO",
                ComponentCategoryType.HyperDisrupt => "HDR",
                ComponentCategoryType.WeaponGravity => "WGR",
                ComponentCategoryType.AssaultPod => "ASP",
                _ => string.Empty,
            };
        }

        public static string ResolveDescription(ComponentCategoryType category)
        {
            return category switch
            {
                ComponentCategoryType.Armor => TextResolver.GetText("Component Category Armor"),
                ComponentCategoryType.Computer => TextResolver.GetText("Component Category Computer"),
                ComponentCategoryType.Construction => TextResolver.GetText("Component Category Construction"),
                ComponentCategoryType.EnergyCollector => TextResolver.GetText("Component Category EnergyCollector"),
                ComponentCategoryType.Engine => TextResolver.GetText("Component Category Engine"),
                ComponentCategoryType.Extractor => TextResolver.GetText("Component Category Extractor"),
                ComponentCategoryType.Habitation => TextResolver.GetText("Component Category Habitation"),
                ComponentCategoryType.HyperDrive => TextResolver.GetText("Component Category HyperDrive"),
                ComponentCategoryType.Labs => TextResolver.GetText("Component Category Labs"),
                ComponentCategoryType.Manufacturer => TextResolver.GetText("Component Category Manufacturer"),
                ComponentCategoryType.Reactor => TextResolver.GetText("Component Category Reactor"),
                ComponentCategoryType.Sensor => TextResolver.GetText("Component Category Sensor"),
                ComponentCategoryType.Shields => TextResolver.GetText("Component Category Shields"),
                ComponentCategoryType.Storage => TextResolver.GetText("Component Category Storage"),
                ComponentCategoryType.WeaponArea => TextResolver.GetText("Component Category WeaponArea"),
                ComponentCategoryType.WeaponBeam => TextResolver.GetText("Component Category WeaponBeam"),
                ComponentCategoryType.WeaponSuperArea => TextResolver.GetText("Component Category WeaponSuperArea"),
                ComponentCategoryType.WeaponSuperBeam => TextResolver.GetText("Component Category WeaponSuperBeam"),
                ComponentCategoryType.WeaponSuperTorpedo => TextResolver.GetText("Component Category WeaponSuperTorpedo"),
                ComponentCategoryType.WeaponTorpedo => TextResolver.GetText("Component Category WeaponTorpedo"),
                ComponentCategoryType.WeaponIon => TextResolver.GetText("Component Category Ion Weapon"),
                ComponentCategoryType.WeaponPointDefense => TextResolver.GetText("Component Category Point Defense Weapon"),
                ComponentCategoryType.WeaponGravity => TextResolver.GetText("Component Category WeaponGravity"),
                ComponentCategoryType.AssaultPod => TextResolver.GetText("Component Category AssaultPod"),
                ComponentCategoryType.Undefined => TextResolver.GetText("None"),
                _ => SplitString(category.ToString()),
            };
        }

        public static string ResolveDescription(ResearchNode researchProject)
        {
            string text = string.Empty;
            if (researchProject != null)
            {
                if (researchProject.Components != null && researchProject.Components.Count > 0)
                {
                    for (int i = 0; i < researchProject.Components.Count; i++)
                    {
                        text += researchProject.Components[i].Name;
                        text += ", ";
                    }
                    text = text.Substring(0, text.Length - 2);
                }
                if (researchProject.ComponentImprovements != null && researchProject.ComponentImprovements.Count > 0)
                {
                    if (text.Length > 0)
                    {
                        text += " + ";
                    }
                    string text2 = string.Empty;
                    for (int j = 0; j < researchProject.ComponentImprovements.Count; j++)
                    {
                        text2 += researchProject.ComponentImprovements[j].ImprovedComponent.Name;
                        text2 += ", ";
                    }
                    if (!string.IsNullOrEmpty(text2) && text2.Length >= 3)
                    {
                        text2 = text2.Substring(0, text2.Length - 2);
                    }
                    text += string.Format(TextResolver.GetText("Improvements to COMPONENT"), text2);
                }
                if (researchProject.Abilities != null && researchProject.Abilities.Count > 0)
                {
                    if (text.Length > 0)
                    {
                        text += " + ";
                    }
                    for (int k = 0; k < researchProject.Abilities.Count; k++)
                    {
                        text += researchProject.Abilities[k].Name;
                        text += ", ";
                    }
                    text = text.Substring(0, text.Length - 2);
                }
                if (researchProject.Fighters != null && researchProject.Fighters.Count > 0)
                {
                    if (text.Length > 0)
                    {
                        text += " + ";
                    }
                    for (int l = 0; l < researchProject.Fighters.Count; l++)
                    {
                        text += researchProject.Fighters[l].Name;
                        text += ", ";
                    }
                    text = text.Substring(0, text.Length - 2);
                }
                if (researchProject.PlanetaryFacility != null)
                {
                    if (text.Length > 0)
                    {
                        text += " + ";
                    }
                    text += string.Format(TextResolver.GetText("Build FACILITY"), researchProject.PlanetaryFacility.Name);
                }
            }
            return text;
        }

        public static string ResolveDescription(IndustryType industry)
        {
            return industry switch
            {
                IndustryType.Energy => TextResolver.GetText("Energy"),
                IndustryType.HighTech => TextResolver.GetText("HighTech"),
                IndustryType.Weapon => TextResolver.GetText("Weapons"),
                IndustryType.Undefined => TextResolver.GetText("None"),
                _ => SplitString(industry.ToString()),
            };
        }

        public static string ResolveDescription(ComponentType type)
        {
            return type switch
            {
                ComponentType.SensorStealth => TextResolver.GetText("Component Type Stealth"),
                ComponentType.DamageControl => TextResolver.GetText("Component Type Damage Control"),
                ComponentType.ComputerCommandCenter => TextResolver.GetText("Component Type Command Center"),
                ComponentType.ComputerCommerceCenter => TextResolver.GetText("Component Type Commerce Center"),
                ComponentType.ComputerCountermeasures => TextResolver.GetText("Component Type Countermeasures"),
                ComponentType.ComputerTargetting => TextResolver.GetText("Component Type Targetting"),
                ComponentType.ConstructionBuild => TextResolver.GetText("Component Type Construction Yard"),
                ComponentType.EngineMainThrust => TextResolver.GetText("Component Type Main Thrust Engine"),
                ComponentType.EngineVectoring => TextResolver.GetText("Component Type Vectoring Engine"),
                ComponentType.ExtractorGasExtractor => TextResolver.GetText("Component Type Gas Extractor"),
                ComponentType.ExtractorLuxury => TextResolver.GetText("Component Type Luxury Resource Extractor"),
                ComponentType.ExtractorMine => TextResolver.GetText("Component Type Mine"),
                ComponentType.HabitationColonization => TextResolver.GetText("Component Type Colony"),
                ComponentType.HabitationHabModule => TextResolver.GetText("Component Type Habitation Module"),
                ComponentType.HabitationLifeSupport => TextResolver.GetText("Component Type Life Support"),
                ComponentType.HabitationMedicalCenter => TextResolver.GetText("Component Type Medical Center"),
                ComponentType.HabitationRecreationCenter => TextResolver.GetText("Component Type Recreation Center"),
                ComponentType.LabsEnergyLab => TextResolver.GetText("Component Type Energy Lab"),
                ComponentType.LabsHighTechLab => TextResolver.GetText("Component Type HighTech Lab"),
                ComponentType.LabsWeaponsLab => TextResolver.GetText("Component Type Weapons Lab"),
                ComponentType.ManufacturerEnergyPlant => TextResolver.GetText("Component Type Energy Manufacturer"),
                ComponentType.ManufacturerHighTechPlant => TextResolver.GetText("Component Type HighTech Manufacturer"),
                ComponentType.ManufacturerWeaponsPlant => TextResolver.GetText("Component Type Weapons Manufacturer"),
                ComponentType.SensorProximityArray => TextResolver.GetText("Component Type Proximity Array"),
                ComponentType.SensorResourceProfileSensor => TextResolver.GetText("Component Type Resource Profile Sensor"),
                ComponentType.SensorLongRange => TextResolver.GetText("Component Type Long Range Scanner"),
                ComponentType.StorageCargo => TextResolver.GetText("Component Type Cargo Module"),
                ComponentType.StorageDockingBay => TextResolver.GetText("Component Type Docking Bay"),
                ComponentType.StorageFuel => TextResolver.GetText("Component Type Fuel Storage Cell"),
                ComponentType.StorageTroop => TextResolver.GetText("Component Type Troop Module"),
                ComponentType.WeaponAreaDestruction => TextResolver.GetText("Component Type Area Weapon"),
                ComponentType.WeaponSuperArea => TextResolver.GetText("Component Type Super Area Weapon"),
                ComponentType.WeaponSuperBeam => TextResolver.GetText("Component Type Super Beam Weapon"),
                ComponentType.WeaponSuperTorpedo => TextResolver.GetText("Component Type Super Torpedo Weapon"),
                ComponentType.WeaponSuperMissile => TextResolver.GetText("Component Type Super Missile Weapon"),
                ComponentType.WeaponSuperRailGun => TextResolver.GetText("Component Type Super RailGun Weapon"),
                ComponentType.WeaponSuperPhaser => TextResolver.GetText("Component Type Super Phaser Weapon"),
                ComponentType.WeaponMissile => TextResolver.GetText("Component Type Missile Weapon"),
                ComponentType.WeaponPointDefense => TextResolver.GetText("Component Type Point Defense"),
                ComponentType.WeaponIonCannon => TextResolver.GetText("Component Type Ion Cannon"),
                ComponentType.WeaponIonPulse => TextResolver.GetText("Component Type Ion Pulse"),
                ComponentType.WeaponIonDefense => TextResolver.GetText("Component Type Ion Defense"),
                ComponentType.HyperDeny => TextResolver.GetText("Component Type HyperDeny Weapon"),
                ComponentType.HyperStop => TextResolver.GetText("Component Type HyperStop"),
                ComponentType.FighterBay => TextResolver.GetText("Component Type Fighter Bay"),
                ComponentType.SensorTraceScanner => TextResolver.GetText("Component Type Sensor TraceScanner"),
                ComponentType.SensorScannerJammer => TextResolver.GetText("Component Type Sensor ScannerJammer"),
                ComponentType.ComputerTargettingFleet => TextResolver.GetText("Component Type Targetting Fleet"),
                ComponentType.ComputerCountermeasuresFleet => TextResolver.GetText("Component Type Countermeasures Fleet"),
                ComponentType.AssaultPod => TextResolver.GetText("Component Type Assault Pod"),
                ComponentType.WeaponTractorBeam => TextResolver.GetText("Component Type Tractor Beam Weapon"),
                ComponentType.WeaponAreaGravity => TextResolver.GetText("Component Type Gravity Area Weapon"),
                ComponentType.WeaponGravityBeam => TextResolver.GetText("Component Type Gravity Beam Weapon"),
                ComponentType.WeaponBombard => TextResolver.GetText("Component Type Bombard Weapon"),
                ComponentType.WeaponBeam => TextResolver.GetText("Component Type Beam Weapon"),
                ComponentType.WeaponTorpedo => TextResolver.GetText("Component Type Torpedo Weapon"),
                ComponentType.Shields => TextResolver.GetText("Component Type Shields"),
                ComponentType.HyperDrive => TextResolver.GetText("Component Type HyperDrive"),
                _ => SplitString(type.ToString()),
            };
        }

        public static string ResolveEngagementStanceDescription(BuiltObject militaryShip)
        {
            string result = "(" + TextResolver.GetText("None") + ")";
            if (militaryShip.Role == BuiltObjectRole.Military)
            {
                result = ((militaryShip.AttackRangeSquared == 0f) ? TextResolver.GetText("When attacked") : ((militaryShip.AttackRangeSquared == 4000000f) ? TextResolver.GetText("Nearby targets") : ((militaryShip.AttackRangeSquared != 2.304E+09f) ? TextResolver.GetText("Detected targets") : TextResolver.GetText("System targets"))));
            }
            return result;
        }

        public static string ResolveEngagementStanceDescription(ShipGroup shipGroup)
        {
            string text = "(" + TextResolver.GetText("None") + ")";
            if (shipGroup.AttackRangeSquared == 0f)
            {
                return TextResolver.GetText("When attacked");
            }
            if (shipGroup.AttackRangeSquared == 4000000f)
            {
                return TextResolver.GetText("Nearby targets");
            }
            if (shipGroup.AttackRangeSquared == 2.304E+09f)
            {
                return TextResolver.GetText("System targets");
            }
            return TextResolver.GetText("Detected targets");
        }

        public static string ResolveDescription(GalaxyShape galaxyShape)
        {
            return galaxyShape switch
            {
                GalaxyShape.Elliptical => TextResolver.GetText("Elliptical"),
                GalaxyShape.Spiral => TextResolver.GetText("Spiral"),
                GalaxyShape.Ring => TextResolver.GetText("Ring"),
                GalaxyShape.Irregular => TextResolver.GetText("Irregular"),
                GalaxyShape.ClustersEven => TextResolver.GetText("Even Clusters"),
                GalaxyShape.ClustersVaried => TextResolver.GetText("Varied Clusters"),
                _ => SplitString(galaxyShape.ToString()),
            };
        }

        public static string ResolveDescription(BattleTactics tactics)
        {
            return tactics switch
            {
                BattleTactics.AllWeapons => TextResolver.GetText("BattleTactics All Weapons"),
                BattleTactics.Evade => TextResolver.GetText("BattleTactics Evade"),
                BattleTactics.PointBlank => TextResolver.GetText("BattleTactics Point Blank"),
                BattleTactics.Standoff => TextResolver.GetText("BattleTactics Standoff"),
                BattleTactics.Undefined => TextResolver.GetText("None"),
                _ => SplitString(tactics.ToString()),
            };
        }

        public static string ResolveDescription(InvasionTactics tactics)
        {
            return tactics switch
            {
                InvasionTactics.DoNotInvade => TextResolver.GetText("InvasionTactics Do Not Invade"),
                InvasionTactics.InvadeImmediately => TextResolver.GetText("InvasionTactics Invade Immediately"),
                InvasionTactics.InvadeWhenClear => TextResolver.GetText("InvasionTactics Invade When Clear"),
                InvasionTactics.Undefined => TextResolver.GetText("None"),
                _ => SplitString(tactics.ToString()),
            };
        }

        public static string ResolveDescription(DisasterEventType disasterType)
        {
            return disasterType switch
            {
                DisasterEventType.Blizzard => TextResolver.GetText("Colony Disaster Blizzard"),
                DisasterEventType.Earthquake => TextResolver.GetText("Colony Disaster Earthquake"),
                DisasterEventType.EconomicCrisis => TextResolver.GetText("Empire Disaster Economic Crisis"),
                DisasterEventType.Eruption => TextResolver.GetText("Colony Disaster Eruption"),
                DisasterEventType.Plague => TextResolver.GetText("Colony Disaster Plague"),
                DisasterEventType.Sandstorm => TextResolver.GetText("Colony Disaster Sandstorm"),
                DisasterEventType.Sinkhole => TextResolver.GetText("Colony Disaster Sinkhole"),
                DisasterEventType.Tsunami => TextResolver.GetText("Colony Disaster Tsunami"),
                _ => string.Empty,
            };
        }

        public static string ResolveDescription(DiplomaticRelationType type)
        {
            return type switch
            {
                DiplomaticRelationType.FreeTradeAgreement => TextResolver.GetText("DiplomaticRelationType FreeTradeAgreement"),
                DiplomaticRelationType.MutualDefensePact => TextResolver.GetText("DiplomaticRelationType MutualDefensePact"),
                DiplomaticRelationType.None => TextResolver.GetText("DiplomaticRelationType None"),
                DiplomaticRelationType.NotMet => TextResolver.GetText("DiplomaticRelationType NotMet"),
                DiplomaticRelationType.Protectorate => TextResolver.GetText("DiplomaticRelationType Protectorate"),
                DiplomaticRelationType.SubjugatedDominion => TextResolver.GetText("DiplomaticRelationType SubjugatedDominion"),
                DiplomaticRelationType.TradeSanctions => TextResolver.GetText("DiplomaticRelationType TradeSanctions"),
                DiplomaticRelationType.Truce => TextResolver.GetText("DiplomaticRelationType Truce"),
                DiplomaticRelationType.War => TextResolver.GetText("DiplomaticRelationType War"),
                _ => SplitString(type.ToString()),
            };
        }

        public static string ResolveDescription(HabitatAtmosphereType atmosphere)
        {
            return SplitString(atmosphere.ToString());
        }

        public static string ResolveDescription(HabitatCategoryType type)
        {
            return type switch
            {
                HabitatCategoryType.Asteroid => TextResolver.GetText("HabitatCategoryType Asteroid"),
                HabitatCategoryType.GasCloud => TextResolver.GetText("HabitatCategoryType GasCloud"),
                HabitatCategoryType.Moon => TextResolver.GetText("HabitatCategoryType Moon"),
                HabitatCategoryType.Planet => TextResolver.GetText("HabitatCategoryType Planet"),
                HabitatCategoryType.Star => TextResolver.GetText("HabitatCategoryType Star"),
                _ => SplitString(type.ToString()),
            };
        }

        public static string ResolveDescription(HabitatType type)
        {
            return type switch
            {
                HabitatType.Ammonia => TextResolver.GetText("HabitatType Ammonia"),
                HabitatType.Argon => TextResolver.GetText("HabitatType Argon"),
                HabitatType.BarrenRock => TextResolver.GetText("HabitatType BarrenRock"),
                HabitatType.BlackHole => TextResolver.GetText("HabitatType BlackHole"),
                HabitatType.CarbonDioxide => TextResolver.GetText("HabitatType CarbonDioxide"),
                HabitatType.Chlorine => TextResolver.GetText("HabitatType Chlorine"),
                HabitatType.Continental => TextResolver.GetText("HabitatType Continental"),
                HabitatType.Desert => TextResolver.GetText("HabitatType Desert"),
                HabitatType.FrozenGasGiant => TextResolver.GetText("HabitatType FrozenGasGiant"),
                HabitatType.GasGiant => TextResolver.GetText("HabitatType GasGiant"),
                HabitatType.Helium => TextResolver.GetText("HabitatType Helium"),
                HabitatType.Hydrogen => TextResolver.GetText("HabitatType Hydrogen"),
                HabitatType.Ice => TextResolver.GetText("HabitatType Ice"),
                HabitatType.MainSequence => TextResolver.GetText("HabitatType MainSequence"),
                HabitatType.MarshySwamp => TextResolver.GetText("HabitatType MarshySwamp"),
                HabitatType.Metal => TextResolver.GetText("HabitatType Metal"),
                HabitatType.Neutron => TextResolver.GetText("HabitatType Neutron"),
                HabitatType.NitrogenOxygen => TextResolver.GetText("HabitatType NitrogenOxygen"),
                HabitatType.Ocean => TextResolver.GetText("HabitatType Ocean"),
                HabitatType.Oxygen => TextResolver.GetText("HabitatType Oxygen"),
                HabitatType.RedGiant => TextResolver.GetText("HabitatType RedGiant"),
                HabitatType.SuperGiant => TextResolver.GetText("HabitatType SuperGiant"),
                HabitatType.SuperNova => TextResolver.GetText("HabitatType SuperNova"),
                HabitatType.Volcanic => TextResolver.GetText("HabitatType Volcanic"),
                HabitatType.WhiteDwarf => TextResolver.GetText("HabitatType WhiteDwarf"),
                HabitatType.Undefined => TextResolver.GetText("None"),
                _ => SplitString(type.ToString()),
            };
        }

        public static string ResolveDescription(CreatureType type)
        {
            string empty = string.Empty;
            return type switch
            {
                CreatureType.Ardilus => TextResolver.GetText("Ardilus"),
                CreatureType.DesertSpaceSlug => TextResolver.GetText("Sand Slug"),
                CreatureType.RockSpaceSlug => TextResolver.GetText("Space Slug"),
                CreatureType.Kaltor => TextResolver.GetText("Giant Kaltor"),
                CreatureType.SilverMist => TextResolver.GetText("SilverMist"),
                _ => "(" + TextResolver.GetText("Unknown") + ")",
            };
        }

        public static string ResolveDescription(EncyclopediaCategory category)
        {
            return category switch
            {
                EncyclopediaCategory.Components => TextResolver.GetText("Components"),
                EncyclopediaCategory.Creatures => TextResolver.GetText("Space Creatures"),
                EncyclopediaCategory.Editor => TextResolver.GetText("Game Editor"),
                EncyclopediaCategory.GameConcepts => TextResolver.GetText("Game Concepts"),
                EncyclopediaCategory.GovernmentTypes => TextResolver.GetText("Government Types"),
                EncyclopediaCategory.PlanetsAndStars => TextResolver.GetText("Planet Types"),
                EncyclopediaCategory.Races => TextResolver.GetText("Alien Races"),
                EncyclopediaCategory.Resources => TextResolver.GetText("Resources"),
                EncyclopediaCategory.Screens => TextResolver.GetText("Game Screens"),
                EncyclopediaCategory.Ships => TextResolver.GetText("Ships and Bases"),
                EncyclopediaCategory.UserInterface => TextResolver.GetText("Finding Your Way Around"),
                EncyclopediaCategory.Undefined => TextResolver.GetText("None"),
                _ => SplitString(category.ToString()),
            };
        }

        public static string ResolveDescription(IntelligenceMissionType type)
        {
            return type switch
            {
                IntelligenceMissionType.CounterIntelligence => TextResolver.GetText("IntelligenceMissionType CounterIntelligence"),
                IntelligenceMissionType.DeepCover => TextResolver.GetText("IntelligenceMissionType DeepCover"),
                IntelligenceMissionType.InciteRevolution => TextResolver.GetText("IntelligenceMissionType InciteRevolution"),
                IntelligenceMissionType.SabotageColony => TextResolver.GetText("IntelligenceMissionType SabotageColony"),
                IntelligenceMissionType.SabotageConstruction => TextResolver.GetText("IntelligenceMissionType SabotageConstruction"),
                IntelligenceMissionType.StealGalaxyMap => TextResolver.GetText("IntelligenceMissionType StealGalaxyMap"),
                IntelligenceMissionType.StealOperationsMap => TextResolver.GetText("IntelligenceMissionType StealOperationsMap"),
                IntelligenceMissionType.StealTechData => TextResolver.GetText("IntelligenceMissionType StealTechData"),
                IntelligenceMissionType.StealTerritoryMap => TextResolver.GetText("IntelligenceMissionType StealTerritoryMap"),
                IntelligenceMissionType.DestroyBase => TextResolver.GetText("IntelligenceMissionType DestroyBase"),
                IntelligenceMissionType.AssassinateCharacter => TextResolver.GetText("IntelligenceMissionType AssassinateCharacter"),
                IntelligenceMissionType.Undefined => TextResolver.GetText("None"),
                _ => SplitString(type.ToString()),
            };
        }

        public static string CapitalizeFirstLetter(string text)
        {
            string text2 = text.Substring(0, 1);
            string text3 = text.Substring(1, text.Length - 1);
            return text2.ToUpper(CultureInfo.InvariantCulture) + text3;
        }

        public static string ResolveDescription(PiratePlayStyle piratePlayStyle)
        {
            string result = string.Empty;
            switch (piratePlayStyle)
            {
                case PiratePlayStyle.Undefined:
                    result = TextResolver.GetText("None");
                    break;
                case PiratePlayStyle.Balanced:
                    result = TextResolver.GetText("PiratePlayStyle Balanced");
                    break;
                case PiratePlayStyle.Pirate:
                    result = TextResolver.GetText("PiratePlayStyle Pirate");
                    break;
                case PiratePlayStyle.Mercenary:
                    result = TextResolver.GetText("PiratePlayStyle Mercenary");
                    break;
                case PiratePlayStyle.Smuggler:
                    result = TextResolver.GetText("PiratePlayStyle Smuggler");
                    break;
            }
            return result;
        }

        public static string ResolveDescription(RaceVictoryCondition raceVictoryCondition, Empire empire)
        {
            string result = string.Empty;
            if (raceVictoryCondition != null)
            {
                switch (raceVictoryCondition.Type)
                {
                    case RaceVictoryConditionType.PirateBuildCriminalNetwork:
                        result = TextResolver.GetText("Race Victory Condition PirateBuildCriminalNetwork");
                        break;
                    case RaceVictoryConditionType.PirateBuildHiddenFortress:
                        result = TextResolver.GetText("Race Victory Condition PirateBuildHiddenFortress");
                        break;
                    case RaceVictoryConditionType.PirateBuildMostHiddenBases:
                        result = TextResolver.GetText("Race Victory Condition PirateBuildMostHiddenBases");
                        break;
                    case RaceVictoryConditionType.PirateControlColoniesPercentage:
                        result = string.Format(TextResolver.GetText("Race Victory Condition PirateControlColoniesPercentage"), raceVictoryCondition.Amount.ToString("0"));
                        break;
                    case RaceVictoryConditionType.PirateEliminateMostPirateFactions:
                        result = TextResolver.GetText("Race Victory Condition PirateEliminateMostPirateFactions");
                        break;
                    case RaceVictoryConditionType.PirateMostProtectionIncome:
                        result = TextResolver.GetText("Race Victory Condition PirateMostProtectionIncome");
                        break;
                    case RaceVictoryConditionType.PirateMostSmugglingIncome:
                        result = TextResolver.GetText("Race Victory Condition PirateMostSmugglingIncome");
                        break;
                    case RaceVictoryConditionType.PirateMostSuccessfulMissionsAttack:
                        result = TextResolver.GetText("Race Victory Condition PirateMostSuccessfulMissionsAttack");
                        break;
                    case RaceVictoryConditionType.PirateMostSuccessfulRaids:
                        result = TextResolver.GetText("Race Victory Condition PirateMostSuccessfulRaids");
                        break;
                    case RaceVictoryConditionType.PirateMostSuccessfulMissionsDefend:
                        result = TextResolver.GetText("Race Victory Condition PirateMostSuccessfulMissionsDefend");
                        break;
                    case RaceVictoryConditionType.CaptureMostShips:
                        result = TextResolver.GetText("Race Victory Condition CaptureMostShips");
                        break;
                    case RaceVictoryConditionType.BuildWonder:
                        if (raceVictoryCondition.AdditionalData is PlanetaryFacilityDefinition)
                        {
                            PlanetaryFacilityDefinition planetaryFacilityDefinition = (PlanetaryFacilityDefinition)raceVictoryCondition.AdditionalData;
                            result = string.Format(TextResolver.GetText("Race Victory Condition BuildWonder"), planetaryFacilityDefinition.Name);
                        }
                        break;
                    case RaceVictoryConditionType.ConquerMostEnemyColonies:
                        result = TextResolver.GetText("Race Victory Condition ConquerMostEnemyColonies");
                        break;
                    case RaceVictoryConditionType.ControlHomeworld:
                        result = ((empire == null || empire.HomeWorld == null) ? string.Format(TextResolver.GetText("Race Victory Condition ControlHomeworld"), "") : string.Format(TextResolver.GetText("Race Victory Condition ControlHomeworld"), "(" + empire.HomeWorld.Name + ")"));
                        break;
                    case RaceVictoryConditionType.ControlLargestColoniesByType:
                        if (raceVictoryCondition.AdditionalData is HabitatType)
                        {
                            HabitatType type3 = (HabitatType)raceVictoryCondition.AdditionalData;
                            result = string.Format(TextResolver.GetText("Race Victory Condition ControlLargestColoniesByType"), raceVictoryCondition.Amount.ToString("0"), ResolveDescription(type3));
                        }
                        break;
                    case RaceVictoryConditionType.ControlMostRuins:
                        result = TextResolver.GetText("Race Victory Condition ControlMostRuins");
                        break;
                    case RaceVictoryConditionType.ControlPlanetTypePercentage:
                        if (raceVictoryCondition.AdditionalData is HabitatType)
                        {
                            HabitatType type2 = (HabitatType)raceVictoryCondition.AdditionalData;
                            result = string.Format(TextResolver.GetText("Race Victory Condition ControlPlanetTypePercentage"), raceVictoryCondition.Amount.ToString("0"), ResolveDescription(type2));
                        }
                        break;
                    case RaceVictoryConditionType.ControlRestrictedResourceSupply:
                        result = string.Format(TextResolver.GetText("Race Victory Condition ControlRestrictedResourceSupply"), raceVictoryCondition.Amount.ToString("0"));
                        break;
                    case RaceVictoryConditionType.DestroyMoreEnemyTroopsThanLoseTimesFactor:
                        result = ((raceVictoryCondition.Amount != 1.0) ? ((raceVictoryCondition.Amount % 1.0 != 0.0) ? string.Format(TextResolver.GetText("Race Victory Condition DestroyMoreEnemyTroopsThanLoseTimesFactor"), raceVictoryCondition.Amount.ToString("0.0")) : string.Format(TextResolver.GetText("Race Victory Condition DestroyMoreEnemyTroopsThanLoseTimesFactor"), raceVictoryCondition.Amount.ToString("0"))) : string.Format(TextResolver.GetText("Race Victory Condition DestroyMoreEnemyTroopsThanLoseTimesFactorSingle")));
                        break;
                    case RaceVictoryConditionType.DestroyMoreShipsThanLoseTimesFactor:
                        result = ((raceVictoryCondition.Amount != 1.0) ? ((raceVictoryCondition.Amount % 1.0 != 0.0) ? string.Format(TextResolver.GetText("Race Victory Condition DestroyMoreShipsThanLoseTimesFactor"), raceVictoryCondition.Amount.ToString("0.0")) : string.Format(TextResolver.GetText("Race Victory Condition DestroyMoreShipsThanLoseTimesFactor"), raceVictoryCondition.Amount.ToString("0"))) : string.Format(TextResolver.GetText("Race Victory Condition DestroyMoreShipsThanLoseTimesFactorSingle")));
                        break;
                    case RaceVictoryConditionType.DestroyMostCreaturesByType:
                        if (raceVictoryCondition.AdditionalData is CreatureType)
                        {
                            CreatureType type = (CreatureType)raceVictoryCondition.AdditionalData;
                            result = string.Format(TextResolver.GetText("Race Victory Condition DestroyMostCreaturesByType"), ResolveDescription(type));
                        }
                        break;
                    case RaceVictoryConditionType.DestroyMostShips:
                        result = TextResolver.GetText("Race Victory Condition DestroyMostShips");
                        break;
                    case RaceVictoryConditionType.DestroyMostTroops:
                        result = TextResolver.GetText("Race Victory Condition DestroyMostTroops");
                        break;
                    case RaceVictoryConditionType.EnslavePopulationProportionEmpire:
                        result = string.Format(TextResolver.GetText("Race Victory Condition EnslavePopulationProportionEmpire"), raceVictoryCondition.Amount.ToString("0"));
                        break;
                    case RaceVictoryConditionType.ExploreGalaxyPercentage:
                        result = string.Format(TextResolver.GetText("Race Victory Condition ExploreGalaxyPercentage"), raceVictoryCondition.Amount.ToString("0"));
                        break;
                    case RaceVictoryConditionType.ExploreMostSystems:
                        result = TextResolver.GetText("Race Victory Condition ExploreMostSystems");
                        break;
                    case RaceVictoryConditionType.ExterminateOrEnslaveMostPopulation:
                        result = TextResolver.GetText("Race Victory Condition ExterminateOrEnslaveMostPopulation");
                        break;
                    case RaceVictoryConditionType.FreeTradeAgreementsFormedProportionAllEmpires:
                        result = string.Format(TextResolver.GetText("Race Victory Condition FreeTradeAgreementsFormedProportionAllEmpires"), raceVictoryCondition.Amount.ToString("0"));
                        break;
                    case RaceVictoryConditionType.HighestPrivateRevenue:
                        result = TextResolver.GetText("Race Victory Condition HighestPrivateRevenue");
                        break;
                    case RaceVictoryConditionType.HighestTradeVolume:
                        result = TextResolver.GetText("Race Victory Condition HighestTradeVolume");
                        break;
                    case RaceVictoryConditionType.KeepLeaderAlive:
                        result = TextResolver.GetText("Race Victory Condition KeepLeaderAlive");
                        break;
                    case RaceVictoryConditionType.LargestMilitary:
                        result = TextResolver.GetText("Race Victory Condition LargestMilitary");
                        break;
                    case RaceVictoryConditionType.LargestMilitaryNonAllied:
                        result = TextResolver.GetText("Race Victory Condition LargestMilitaryNonAllied");
                        break;
                    case RaceVictoryConditionType.LeastBrokenTreaties:
                        result = TextResolver.GetText("Race Victory Condition LeastBrokenTreaties");
                        break;
                    case RaceVictoryConditionType.LeastTimeWarring:
                        result = TextResolver.GetText("Race Victory Condition LeastTimeWarring");
                        break;
                    case RaceVictoryConditionType.LeastTreaties:
                        result = TextResolver.GetText("Race Victory Condition LeastTreaties");
                        break;
                    case RaceVictoryConditionType.LeastWarsStarted:
                        result = TextResolver.GetText("Race Victory Condition LeastWars");
                        break;
                    case RaceVictoryConditionType.LoseFewestShips:
                        result = TextResolver.GetText("Race Victory Condition LoseFewestShips");
                        break;
                    case RaceVictoryConditionType.LoseFewestTroops:
                        result = TextResolver.GetText("Race Victory Condition LoseFewestTroops");
                        break;
                    case RaceVictoryConditionType.MostExperiencedAdmiral:
                        result = TextResolver.GetText("Race Victory Condition MostExperiencedAdmiral");
                        break;
                    case RaceVictoryConditionType.MostExperiencedGeneral:
                        result = TextResolver.GetText("Race Victory Condition MostExperiencedGeneral");
                        break;
                    case RaceVictoryConditionType.MostHomeworlds:
                        result = TextResolver.GetText("Race Victory Condition MostHomeworlds");
                        break;
                    case RaceVictoryConditionType.MostIntelligenceMissionsIntercepted:
                        result = TextResolver.GetText("Race Victory Condition MostIntelligenceMissionsIntercepted");
                        break;
                    case RaceVictoryConditionType.MostIntelligenceMissionsSucceed:
                        result = TextResolver.GetText("Race Victory Condition MostIntelligenceMissionsSucceed");
                        break;
                    case RaceVictoryConditionType.MostMiningStations:
                        result = TextResolver.GetText("Race Victory Condition MostMiningStations");
                        break;
                    case RaceVictoryConditionType.MostResortBases:
                        result = TextResolver.GetText("Race Victory Condition MostResortBases");
                        break;
                    case RaceVictoryConditionType.MostScientists:
                        result = TextResolver.GetText("Race Victory Condition MostScientists");
                        break;
                    case RaceVictoryConditionType.MostSpaceports:
                        result = TextResolver.GetText("Race Victory Condition MostSpaceports");
                        break;
                    case RaceVictoryConditionType.MostSubjugatedDominions:
                        result = TextResolver.GetText("Race Victory Condition MostSubjugatedDominions");
                        break;
                    case RaceVictoryConditionType.MostTimeWarring:
                        result = TextResolver.GetText("Race Victory Condition MostTimeWarring");
                        break;
                    case RaceVictoryConditionType.MostTourismIncome:
                        result = TextResolver.GetText("Race Victory Condition MostTourismIncome");
                        break;
                    case RaceVictoryConditionType.MostTradeIncome:
                        result = TextResolver.GetText("Race Victory Condition MostTradeIncome");
                        break;
                    case RaceVictoryConditionType.MostTroops:
                        result = TextResolver.GetText("Race Victory Condition MostTroops");
                        break;
                    case RaceVictoryConditionType.MostTroopsNonAllied:
                        result = TextResolver.GetText("Race Victory Condition MostTroopsNonAllied");
                        break;
                    case RaceVictoryConditionType.MutualDefensePactsFormedProportionAllEmpires:
                        result = string.Format(TextResolver.GetText("Race Victory Condition MutualDefensePactsFormedProportionAllEmpires"), raceVictoryCondition.Amount.ToString("0"));
                        break;
                    case RaceVictoryConditionType.OldestFreeTradeAgreement:
                        result = TextResolver.GetText("Race Victory Condition OldestFreeTradeAgreement");
                        break;
                    case RaceVictoryConditionType.OldestMutualDefensePact:
                        result = TextResolver.GetText("Race Victory Condition OldestMutualDefensePact");
                        break;
                    case RaceVictoryConditionType.OwnLargestCapitalShip:
                        result = TextResolver.GetText("Race Victory Condition OwnLargestCapitalShip");
                        break;
                    case RaceVictoryConditionType.PopulationHappiest:
                        result = TextResolver.GetText("Race Victory Condition PopulationHappiest");
                        break;
                    case RaceVictoryConditionType.PopulationHighest:
                        result = TextResolver.GetText("Race Victory Condition PopulationHighest");
                        break;
                    case RaceVictoryConditionType.ResearchLeastAdvanced:
                        result = TextResolver.GetText("Race Victory Condition ResearchLeastAdvanced");
                        break;
                    case RaceVictoryConditionType.ResearchMostAdvanced:
                        result = TextResolver.GetText("Race Victory Condition ResearchMostAdvanced");
                        break;
                    case RaceVictoryConditionType.ResearchMostCompletedBranches:
                        result = TextResolver.GetText("Race Victory Condition ResearchMostCompletedBranches");
                        break;
                    case RaceVictoryConditionType.ResearchMostCompletedBranchesByIndustry:
                        if (raceVictoryCondition.AdditionalData is IndustryType)
                        {
                            IndustryType industry = (IndustryType)raceVictoryCondition.AdditionalData;
                            result = string.Format(TextResolver.GetText("Race Victory Condition ResearchMostCompletedBranchesByIndustry"), ResolveDescription(industry));
                        }
                        break;
                    case RaceVictoryConditionType.MineMostResourcesLuxury:
                        result = TextResolver.GetText("Race Victory Condition MineMostResourcesLuxury");
                        break;
                    case RaceVictoryConditionType.MineMostResourcesStrategic:
                        result = TextResolver.GetText("Race Victory Condition MineMostResourcesStrategic");
                        break;
                    case RaceVictoryConditionType.MineMostResourcesColonyManufactured:
                        result = TextResolver.GetText("Race Victory Condition MineMostResourcesColonyManufactured");
                        break;
                    case RaceVictoryConditionType.BuildMostMilitaryShips:
                        result = TextResolver.GetText("Race Victory Condition BuildMostMilitaryShips");
                        break;
                    case RaceVictoryConditionType.BuildMostCivilianShips:
                        result = TextResolver.GetText("Race Victory Condition BuildMostCivilianShips");
                        break;
                    case RaceVictoryConditionType.BuildMostBases:
                        result = TextResolver.GetText("Race Victory Condition BuildMostBases");
                        break;
                }
            }
            return result;
        }

        public static string ResolveDescription(RaceEventType raceEventType)
        {
            string result = string.Empty;
            switch (raceEventType)
            {
                case RaceEventType.AntiXenoRiotsExterminate:
                    result = TextResolver.GetText("Race Event Title AntiXenoRiotsExterminate");
                    break;
                case RaceEventType.CannibalismPopulationShrinks:
                    result = TextResolver.GetText("Race Event Title CannibalismPopulationShrinks");
                    break;
                case RaceEventType.CreativeReengineeringFreeCrashResearch:
                    result = TextResolver.GetText("Race Event Title CreativeReengineeringFreeCrashResearch");
                    break;
                case RaceEventType.DeathCultExterminate:
                    result = TextResolver.GetText("Race Event Title DeathCultExterminate");
                    break;
                case RaceEventType.DestinyCharacterTraits:
                    result = TextResolver.GetText("Race Event Title DestinyCharacterTraits");
                    break;
                case RaceEventType.ForcedRetirementLeaderReplaced:
                    result = TextResolver.GetText("Race Event Title ForcedRetirementLeaderReplaced");
                    break;
                case RaceEventType.FriendsInManyPlacesRevealTerritory:
                    result = TextResolver.GetText("Race Event Title FriendsInManyPlacesRevealTerritory");
                    break;
                case RaceEventType.GrandPerformanceDiplomacyBonus:
                    result = TextResolver.GetText("Race Event Title GrandPerformanceDiplomacyBonus");
                    break;
                case RaceEventType.GreatHuntStrongTroops:
                    result = TextResolver.GetText("Race Event Title GreatHuntStrongTroops");
                    break;
                case RaceEventType.HistoricalDiscoveryExploreRuinsForResearchBoost:
                    result = TextResolver.GetText("Race Event Title HistoricalDiscoveryExploreRuinsForResearchBoost");
                    break;
                case RaceEventType.HistoricalKnowledgeUncoverHiddenLocation:
                    result = TextResolver.GetText("Race Event Title HistoricalKnowledgeUncoverHiddenLocation");
                    break;
                case RaceEventType.IsolationistsResetFirstContactPenalty:
                    result = TextResolver.GetText("Race Event Title IsolationistsResetFirstContactPenalty");
                    break;
                case RaceEventType.LuckyAvertColonyDisaster:
                    result = TextResolver.GetText("Race Event Title LuckyAvertColonyDisaster");
                    break;
                case RaceEventType.MetamorphosisCharacterChange:
                    result = TextResolver.GetText("Race Event Title MetamorphosisCharacterChange");
                    break;
                case RaceEventType.NaturalHarmonyColonyQualityIncreased:
                    result = TextResolver.GetText("Race Event Title NaturalHarmonyColonyQualityIncreased");
                    break;
                case RaceEventType.NepthysWineVintage:
                    result = TextResolver.GetText("Race Event Title NepthysWineVintage");
                    break;
                case RaceEventType.NeverSurrenderWarWearinessReset:
                    result = TextResolver.GetText("Race Event Title NeverSurrenderWarWearinessReset");
                    break;
                case RaceEventType.PredictiveHistory:
                    result = TextResolver.GetText("Race Event Title PredictiveHistory");
                    break;
                case RaceEventType.ScientificBreakthroughResearchProgress:
                    result = TextResolver.GetText("Race Event Title ScientificBreakthroughResearchProgress");
                    break;
                case RaceEventType.SecurityConcernsCharacterReplaced:
                    result = TextResolver.GetText("Race Event Title SecurityConcernsCharacterReplaced");
                    break;
                case RaceEventType.ShakturiArtifactWeaponResearch:
                    result = TextResolver.GetText("Race Event Title ShakturiArtifactWeaponResearch");
                    break;
                case RaceEventType.StrengthInNumbersMaintenanceLowerForSmallShips:
                    result = TextResolver.GetText("Race Event Title StrengthInNumbersMaintenanceLowerForSmallShips");
                    break;
                case RaceEventType.SuppressedKnowledgeLoseResearch:
                    result = TextResolver.GetText("Race Event Title SuppressedKnowledgeLoseResearch");
                    break;
                case RaceEventType.SupremeWarriorNewGeneral:
                    result = TextResolver.GetText("Race Event Title SupremeWarriorNewGeneral");
                    break;
                case RaceEventType.SwarmsFullTroopTransport:
                    result = TextResolver.GetText("Race Event Title SwarmsFullTroopTransport");
                    break;
                case RaceEventType.TodashGalacticChampionships:
                    result = TextResolver.GetText("Race Event Title TodashGalacticChampionships");
                    break;
                case RaceEventType.UnderwaterLeviathan:
                    result = TextResolver.GetText("Race Event Title UnderwaterLeviathan");
                    break;
                case RaceEventType.WarriorWaveTroopRecruitment:
                    result = TextResolver.GetText("Race Event Title WarriorWaveTroopRecruitment");
                    break;
                case RaceEventType.XenophobiaNoAssimilate:
                    result = TextResolver.GetText("Race Event Title XenophobiaNoAssimilate");
                    break;
            }
            return result;
        }

        public static RaceSummary GenerateRaceSummary(Race race)
        {
            if (race != null)
            {
                RaceSummary raceSummary = new RaceSummary(race);
                RaceSummarySection raceSummarySection = null;
                List<string> list = ResolveRaceBonuses(race);
                List<string> list2 = ResolveRaceCharacteristics(race);
                List<int> list3 = Empire.ResolveRaceSpecificGovernmentTypes(race);
                raceSummarySection = new RaceSummarySection("");
                raceSummarySection.Items.Add(TextResolver.GetText("Race Family") + ": " + ResolveRaceFamilyDescription(race.FamilyId));
                raceSummarySection.Items.Add(TextResolver.GetText("Native Planet Type") + ": " + ResolveDescription(race.NativeHabitatType));
                raceSummarySection.Items.Add(TextResolver.GetText("Default Reproduction Rate") + ": " + (race.ReproductiveRate - 1.0).ToString("+0%"));
                raceSummary.Sections.Add(raceSummarySection);
                raceSummarySection = new RaceSummarySection(TextResolver.GetText("Characteristics"));
                for (int i = 0; i < list2.Count; i++)
                {
                    raceSummarySection.Items.Add(list2[i]);
                }
                raceSummary.Sections.Add(raceSummarySection);
                if (list.Count > 0)
                {
                    raceSummarySection = new RaceSummarySection(TextResolver.GetText("Bonuses"));
                    for (int j = 0; j < list.Count; j++)
                    {
                        raceSummarySection.Items.Add(list[j]);
                    }
                    raceSummary.Sections.Add(raceSummarySection);
                }
                if (race.CriticalResources != null && race.CriticalResources.Count > 0)
                {
                    raceSummarySection = new RaceSummarySection(TextResolver.GetText("Resource Bonuses"));
                    for (int k = 0; k < race.CriticalResources.Count; k++)
                    {
                        ResourceBonus resourceBonus = race.CriticalResources[k];
                        if (resourceBonus != null)
                        {
                            raceSummarySection.Items.Add(ResolveDescriptionGeneral(resourceBonus));
                        }
                    }
                    raceSummary.Sections.Add(raceSummarySection);
                }
                if (race.VictoryConditions != null && race.VictoryConditions.Count > 0)
                {
                    raceSummarySection = new RaceSummarySection(TextResolver.GetText("Race Victory Conditions"));
                    for (int l = 0; l < race.VictoryConditions.Count; l++)
                    {
                        RaceVictoryCondition raceVictoryCondition = race.VictoryConditions[l];
                        if (raceVictoryCondition != null)
                        {
                            raceSummarySection.Items.Add(raceVictoryCondition.Proportion.ToString("0") + "%:  " + ResolveDescription(raceVictoryCondition, null));
                        }
                    }
                    raceSummary.Sections.Add(raceSummarySection);
                }
                raceSummarySection = new RaceSummarySection(TextResolver.GetText("Colonies"));
                if (race.ResearchColonizationCostFactorContinental != 1.0)
                {
                    raceSummarySection.Items.Add(string.Format(TextResolver.GetText("Colonization Cost Factor Description"), ResolveDescription(HabitatType.Continental), (race.ResearchColonizationCostFactorContinental - 1.0).ToString("+0%;-0%")));
                }
                if (race.ResearchColonizationCostFactorMarshySwamp != 1.0)
                {
                    raceSummarySection.Items.Add(string.Format(TextResolver.GetText("Colonization Cost Factor Description"), ResolveDescription(HabitatType.MarshySwamp), (race.ResearchColonizationCostFactorMarshySwamp - 1.0).ToString("+0%;-0%")));
                }
                if (race.ResearchColonizationCostFactorOcean != 1.0)
                {
                    raceSummarySection.Items.Add(string.Format(TextResolver.GetText("Colonization Cost Factor Description"), ResolveDescription(HabitatType.Ocean), (race.ResearchColonizationCostFactorOcean - 1.0).ToString("+0%;-0%")));
                }
                if (race.ResearchColonizationCostFactorDesert != 1.0)
                {
                    raceSummarySection.Items.Add(string.Format(TextResolver.GetText("Colonization Cost Factor Description"), ResolveDescription(HabitatType.Desert), (race.ResearchColonizationCostFactorDesert - 1.0).ToString("+0%;-0%")));
                }
                if (race.ResearchColonizationCostFactorIce != 1.0)
                {
                    raceSummarySection.Items.Add(string.Format(TextResolver.GetText("Colonization Cost Factor Description"), ResolveDescription(HabitatType.Ice), (race.ResearchColonizationCostFactorIce - 1.0).ToString("+0%;-0%")));
                }
                if (race.ResearchColonizationCostFactorVolcanic != 1.0)
                {
                    raceSummarySection.Items.Add(string.Format(TextResolver.GetText("Colonization Cost Factor Description"), ResolveDescription(HabitatType.Volcanic), (race.ResearchColonizationCostFactorVolcanic - 1.0).ToString("+0%;-0%")));
                }
                if (race.ColonyConstructionSpeedFactorContinental != 1.0)
                {
                    raceSummarySection.Items.Add(string.Format(TextResolver.GetText("Colony Construction Speed Factor Description"), ResolveDescription(HabitatType.Continental), (race.ColonyConstructionSpeedFactorContinental - 1.0).ToString("+0%;-0%")));
                }
                if (race.ColonyConstructionSpeedFactorMarshySwamp != 1.0)
                {
                    raceSummarySection.Items.Add(string.Format(TextResolver.GetText("Colony Construction Speed Factor Description"), ResolveDescription(HabitatType.MarshySwamp), (race.ColonyConstructionSpeedFactorMarshySwamp - 1.0).ToString("+0%;-0%")));
                }
                if (race.ColonyConstructionSpeedFactorOcean != 1.0)
                {
                    raceSummarySection.Items.Add(string.Format(TextResolver.GetText("Colony Construction Speed Factor Description"), ResolveDescription(HabitatType.Ocean), (race.ColonyConstructionSpeedFactorOcean - 1.0).ToString("+0%;-0%")));
                }
                if (race.ColonyConstructionSpeedFactorDesert != 1.0)
                {
                    raceSummarySection.Items.Add(string.Format(TextResolver.GetText("Colony Construction Speed Factor Description"), ResolveDescription(HabitatType.Desert), (race.ColonyConstructionSpeedFactorDesert - 1.0).ToString("+0%;-0%")));
                }
                if (race.ColonyConstructionSpeedFactorIce != 1.0)
                {
                    raceSummarySection.Items.Add(string.Format(TextResolver.GetText("Colony Construction Speed Factor Description"), ResolveDescription(HabitatType.Ice), (race.ColonyConstructionSpeedFactorIce - 1.0).ToString("+0%;-0%")));
                }
                if (race.ColonyConstructionSpeedFactorVolcanic != 1.0)
                {
                    raceSummarySection.Items.Add(string.Format(TextResolver.GetText("Colony Construction Speed Factor Description"), ResolveDescription(HabitatType.Volcanic), (race.ColonyConstructionSpeedFactorVolcanic - 1.0).ToString("+0%;-0%")));
                }
                if (race.ColonyPopulationPolicyGrowthFactorExterminate != 1.0)
                {
                    raceSummarySection.Items.Add(string.Format(TextResolver.GetText("Colony Exterminate Policy Growth Factor Description"), (race.ColonyPopulationPolicyGrowthFactorExterminate - 1.0).ToString("+0%;-0%")));
                }
                if (race.ImmuneNaturalDisastersAtColonyType != 0)
                {
                    raceSummarySection.Items.Add(string.Format(TextResolver.GetText("Colony Immune Disasters Description"), ResolveDescription(race.ImmuneNaturalDisastersAtColonyType)));
                }
                if (raceSummarySection.Items.Count > 0)
                {
                    raceSummary.Sections.Add(raceSummarySection);
                }
                raceSummarySection = new RaceSummarySection(TextResolver.GetText("Characters"));
                if (race.IntelligenceAgentAdditional > 0)
                {
                    raceSummarySection.Items.Add(TextResolver.GetText("Extra Intelligence Agents") + ": " + race.IntelligenceAgentAdditional);
                }
                if (race.CharacterRandomAppearanceChanceLeader < 1.0)
                {
                    raceSummarySection.Items.Add(string.Format(TextResolver.GetText("Character Appearance Chance Less Description"), ResolveDescription(CharacterRole.Leader), (race.CharacterRandomAppearanceChanceLeader - 1.0).ToString("+0%;-0%")));
                }
                else if (race.CharacterRandomAppearanceChanceLeader > 1.0)
                {
                    raceSummarySection.Items.Add(string.Format(TextResolver.GetText("Character Appearance Chance More Description"), ResolveDescription(CharacterRole.Leader), (race.CharacterRandomAppearanceChanceLeader - 1.0).ToString("+0%;-0%")));
                }
                if (race.CharacterRandomAppearanceChanceAmbassador < 1.0)
                {
                    raceSummarySection.Items.Add(string.Format(TextResolver.GetText("Character Appearance Chance Less Description"), ResolveDescription(CharacterRole.Ambassador), (race.CharacterRandomAppearanceChanceAmbassador - 1.0).ToString("+0%;-0%")));
                }
                else if (race.CharacterRandomAppearanceChanceAmbassador > 1.0)
                {
                    raceSummarySection.Items.Add(string.Format(TextResolver.GetText("Character Appearance Chance More Description"), ResolveDescription(CharacterRole.Ambassador), (race.CharacterRandomAppearanceChanceAmbassador - 1.0).ToString("+0%;-0%")));
                }
                if (race.CharacterRandomAppearanceChanceGovernor < 1.0)
                {
                    raceSummarySection.Items.Add(string.Format(TextResolver.GetText("Character Appearance Chance Less Description"), ResolveDescription(CharacterRole.ColonyGovernor), (race.CharacterRandomAppearanceChanceGovernor - 1.0).ToString("+0%;-0%")));
                }
                else if (race.CharacterRandomAppearanceChanceGovernor > 1.0)
                {
                    raceSummarySection.Items.Add(string.Format(TextResolver.GetText("Character Appearance Chance More Description"), ResolveDescription(CharacterRole.ColonyGovernor), (race.CharacterRandomAppearanceChanceGovernor - 1.0).ToString("+0%;-0%")));
                }
                if (race.CharacterRandomAppearanceChanceAdmiral < 1.0)
                {
                    raceSummarySection.Items.Add(string.Format(TextResolver.GetText("Character Appearance Chance Less Description"), ResolveDescription(CharacterRole.FleetAdmiral), (race.CharacterRandomAppearanceChanceAdmiral - 1.0).ToString("+0%;-0%")));
                }
                else if (race.CharacterRandomAppearanceChanceAdmiral > 1.0)
                {
                    raceSummarySection.Items.Add(string.Format(TextResolver.GetText("Character Appearance Chance More Description"), ResolveDescription(CharacterRole.FleetAdmiral), (race.CharacterRandomAppearanceChanceAdmiral - 1.0).ToString("+0%;-0%")));
                }
                if (race.CharacterRandomAppearanceChanceGeneral < 1.0)
                {
                    raceSummarySection.Items.Add(string.Format(TextResolver.GetText("Character Appearance Chance Less Description"), ResolveDescription(CharacterRole.TroopGeneral), (race.CharacterRandomAppearanceChanceGeneral - 1.0).ToString("+0%;-0%")));
                }
                else if (race.CharacterRandomAppearanceChanceGeneral > 1.0)
                {
                    raceSummarySection.Items.Add(string.Format(TextResolver.GetText("Character Appearance Chance More Description"), ResolveDescription(CharacterRole.TroopGeneral), (race.CharacterRandomAppearanceChanceGeneral - 1.0).ToString("+0%;-0%")));
                }
                if (race.CharacterRandomAppearanceChanceScientist < 1.0)
                {
                    raceSummarySection.Items.Add(string.Format(TextResolver.GetText("Character Appearance Chance Less Description"), ResolveDescription(CharacterRole.Scientist), (race.CharacterRandomAppearanceChanceScientist - 1.0).ToString("+0%;-0%")));
                }
                else if (race.CharacterRandomAppearanceChanceScientist > 1.0)
                {
                    raceSummarySection.Items.Add(string.Format(TextResolver.GetText("Character Appearance Chance More Description"), ResolveDescription(CharacterRole.Scientist), (race.CharacterRandomAppearanceChanceScientist - 1.0).ToString("+0%;-0%")));
                }
                if (race.CharacterRandomAppearanceChanceIntelligenceAgent < 1.0)
                {
                    raceSummarySection.Items.Add(string.Format(TextResolver.GetText("Character Appearance Chance Less Description"), ResolveDescription(CharacterRole.IntelligenceAgent), (race.CharacterRandomAppearanceChanceIntelligenceAgent - 1.0).ToString("+0%;-0%")));
                }
                else if (race.CharacterRandomAppearanceChanceIntelligenceAgent > 1.0)
                {
                    raceSummarySection.Items.Add(string.Format(TextResolver.GetText("Character Appearance Chance More Description"), ResolveDescription(CharacterRole.IntelligenceAgent), (race.CharacterRandomAppearanceChanceIntelligenceAgent - 1.0).ToString("+0%;-0%")));
                }
                if (race.CharacterRandomAppearanceChancePirateLeader < 1.0)
                {
                    raceSummarySection.Items.Add(string.Format(TextResolver.GetText("Character Appearance Chance Less Description"), ResolveDescription(CharacterRole.PirateLeader), (race.CharacterRandomAppearanceChancePirateLeader - 1.0).ToString("+0%;-0%")));
                }
                else if (race.CharacterRandomAppearanceChancePirateLeader > 1.0)
                {
                    raceSummarySection.Items.Add(string.Format(TextResolver.GetText("Character Appearance Chance More Description"), ResolveDescription(CharacterRole.PirateLeader), (race.CharacterRandomAppearanceChancePirateLeader - 1.0).ToString("+0%;-0%")));
                }
                if (race.CharacterRandomAppearanceChanceShipCaptain < 1.0)
                {
                    raceSummarySection.Items.Add(string.Format(TextResolver.GetText("Character Appearance Chance Less Description"), ResolveDescription(CharacterRole.ShipCaptain), (race.CharacterRandomAppearanceChanceShipCaptain - 1.0).ToString("+0%;-0%")));
                }
                else if (race.CharacterRandomAppearanceChanceShipCaptain > 1.0)
                {
                    raceSummarySection.Items.Add(string.Format(TextResolver.GetText("Character Appearance Chance More Description"), ResolveDescription(CharacterRole.ShipCaptain), (race.CharacterRandomAppearanceChanceShipCaptain - 1.0).ToString("+0%;-0%")));
                }
                if (race.CharacterStartingTraitLeader != 0)
                {
                    raceSummarySection.Items.Add(string.Format(TextResolver.GetText("Character Starting Trait Description"), ResolveDescription(CharacterRole.Leader), ResolveDescription(race.CharacterStartingTraitLeader)));
                }
                if (race.CharacterStartingTraitAmbassador != 0)
                {
                    raceSummarySection.Items.Add(string.Format(TextResolver.GetText("Character Starting Trait Description"), ResolveDescription(CharacterRole.Ambassador), ResolveDescription(race.CharacterStartingTraitAmbassador)));
                }
                if (race.CharacterStartingTraitGovernor != 0)
                {
                    raceSummarySection.Items.Add(string.Format(TextResolver.GetText("Character Starting Trait Description"), ResolveDescription(CharacterRole.ColonyGovernor), ResolveDescription(race.CharacterStartingTraitGovernor)));
                }
                if (race.CharacterStartingTraitAdmiral != 0)
                {
                    raceSummarySection.Items.Add(string.Format(TextResolver.GetText("Character Starting Trait Description"), ResolveDescription(CharacterRole.FleetAdmiral), ResolveDescription(race.CharacterStartingTraitAdmiral)));
                }
                if (race.CharacterStartingTraitGeneral != 0)
                {
                    raceSummarySection.Items.Add(string.Format(TextResolver.GetText("Character Starting Trait Description"), ResolveDescription(CharacterRole.TroopGeneral), ResolveDescription(race.CharacterStartingTraitGeneral)));
                }
                if (race.CharacterStartingTraitScientist != 0)
                {
                    raceSummarySection.Items.Add(string.Format(TextResolver.GetText("Character Starting Trait Description"), ResolveDescription(CharacterRole.Scientist), ResolveDescription(race.CharacterStartingTraitScientist)));
                }
                if (race.CharacterStartingTraitIntelligenceAgent != 0)
                {
                    raceSummarySection.Items.Add(string.Format(TextResolver.GetText("Character Starting Trait Description"), ResolveDescription(CharacterRole.IntelligenceAgent), ResolveDescription(race.CharacterStartingTraitIntelligenceAgent)));
                }
                if (race.CharacterStartingTraitPirateLeader != 0)
                {
                    raceSummarySection.Items.Add(string.Format(TextResolver.GetText("Character Starting Trait Description"), ResolveDescription(CharacterRole.PirateLeader), ResolveDescription(race.CharacterStartingTraitPirateLeader)));
                }
                if (race.CharacterStartingTraitShipCaptain != 0)
                {
                    raceSummarySection.Items.Add(string.Format(TextResolver.GetText("Character Starting Trait Description"), ResolveDescription(CharacterRole.ShipCaptain), ResolveDescription(race.CharacterStartingTraitShipCaptain)));
                }
                if (raceSummarySection.Items.Count > 0)
                {
                    raceSummary.Sections.Add(raceSummarySection);
                }
                raceSummarySection = new RaceSummarySection(TextResolver.GetText("Other"));
                foreach (int item3 in list3)
                {
                    GovernmentAttributes governmentAttributes = GovernmentsStatic[item3];
                    raceSummarySection.Items.Add(TextResolver.GetText("Special Government") + ": " + governmentAttributes.Name);
                }
                if (race.DisallowedGovernmentIds.Count > 0)
                {
                    string text = TextResolver.GetText("Disallowed Governments") + ": ";
                    for (int m = 0; m < race.DisallowedGovernmentIds.Count; m++)
                    {
                        GovernmentAttributes governmentAttributes2 = GovernmentsStatic[race.DisallowedGovernmentIds[m]];
                        text = text + governmentAttributes2.Name + ", ";
                    }
                    text = text.TrimEnd(' ', ',');
                    raceSummarySection.Items.Add(text);
                }
                ComponentList componentList = ResearchNodeDefinitionsStatic.ResolveRaceSpecificComponents(race);
                if (componentList != null)
                {
                    string text2 = TextResolver.GetText("Special Technology") + ": ";
                    if (componentList.Count > 0)
                    {
                        for (int n = 0; n < componentList.Count; n++)
                        {
                            string text3 = text2;
                            text2 = text3 + componentList[n].Name + " (" + ResolveDescription(componentList[n].Type) + "), ";
                        }
                        text2 = text2.Substring(0, text2.Length - 2);
                    }
                    else
                    {
                        text2 = text2 + "(" + TextResolver.GetText("None") + ")";
                    }
                    raceSummarySection.Items.Add(text2);
                }
                if (race.DisallowedResearchAreas != null && race.DisallowedResearchAreas.Count > 0)
                {
                    string text4 = string.Empty;
                    bool flag = false;
                    for (int num = 0; num < race.DisallowedResearchAreas.Count; num++)
                    {
                        if (race.DisallowedResearchAreas[num] != 0)
                        {
                            flag = true;
                        }
                    }
                    if (race.DisallowedComponents != null && race.DisallowedComponents.Count > 0)
                    {
                        flag = true;
                    }
                    if (!flag)
                    {
                        text4 = "(" + TextResolver.GetText("None") + ")";
                    }
                    else if (race.DisallowedResearchAreas.Count == 1 && race.DisallowedResearchAreas[0] == ComponentCategoryType.Undefined && (race.DisallowedComponents == null || race.DisallowedComponents.Count <= 0))
                    {
                        text4 = "(" + TextResolver.GetText("None") + ")";
                    }
                    else
                    {
                        for (int num2 = 0; num2 < race.DisallowedResearchAreas.Count; num2++)
                        {
                            ComponentCategoryType componentCategoryType = race.DisallowedResearchAreas[num2];
                            switch (componentCategoryType)
                            {
                                case ComponentCategoryType.WeaponPointDefense:
                                    text4 = text4 + ResolveDescription(ComponentType.WeaponMissile) + ", ";
                                    break;
                                default:
                                    text4 = text4 + ResolveDescription(componentCategoryType) + ", ";
                                    break;
                                case ComponentCategoryType.Undefined:
                                    break;
                            }
                        }
                        for (int num3 = 0; num3 < race.DisallowedComponents.Count; num3++)
                        {
                            Component component = race.DisallowedComponents[num3];
                            if (component != null)
                            {
                                text4 = text4 + component.Name + ", ";
                            }
                        }
                        if (text4.Length > 2)
                        {
                            text4 = text4.Substring(0, text4.Length - 2);
                        }
                    }
                    raceSummarySection.Items.Add(TextResolver.GetText("Disallowed Technology") + ": " + text4);
                }
                if (race.ChangePeriodYearsInterval > 0 && race.ChangePeriodYearsLength > 0)
                {
                    string arg = ResolveRaceChangeQualitiesDescription(race);
                    string item = string.Format(TextResolver.GetText("Regular X-year change cycle: For Y years have CHANGES"), race.ChangePeriodYearsInterval.ToString(), race.ChangePeriodYearsLength.ToString(), arg);
                    raceSummarySection.Items.Add(item);
                }
                if (race.MilitaryShipSizeFactor != 1.0)
                {
                    string empty = string.Empty;
                    double num4 = race.MilitaryShipSizeFactor - 1.0;
                    empty = ((!(race.MilitaryShipSizeFactor > 1.0)) ? (TextResolver.GetText("Smaller military ship sizes") + ": " + num4.ToString("0%")) : (TextResolver.GetText("Larger military ship sizes") + ": +" + num4.ToString("0%")));
                    raceSummarySection.Items.Add(empty);
                }
                if (race.CivilianShipSizeFactor != 1.0)
                {
                    string empty2 = string.Empty;
                    double num5 = race.CivilianShipSizeFactor - 1.0;
                    empty2 = ((!(race.CivilianShipSizeFactor > 1.0)) ? (TextResolver.GetText("Smaller civilian ship sizes") + ": " + num5.ToString("0%")) : (TextResolver.GetText("Larger civilian ship sizes") + ": +" + num5.ToString("0%")));
                    raceSummarySection.Items.Add(empty2);
                }
                if (race.ConstructionSpeedModifier != 1.0)
                {
                    double num6 = race.ConstructionSpeedModifier - 1.0;
                    string item2 = string.Empty;
                    if (num6 > 0.0)
                    {
                        item2 = TextResolver.GetText("Faster Construction Speed") + ": +" + num6.ToString("0%");
                    }
                    else if (num6 < 0.0)
                    {
                        item2 = TextResolver.GetText("Slower Construction Speed") + ": " + num6.ToString("0%");
                    }
                    raceSummarySection.Items.Add(item2);
                }
                if (race.SpaceportArmorStrengthFactor > 1.0)
                {
                    raceSummarySection.Items.Add(TextResolver.GetText("Stronger Spaceport Armor") + ": " + (race.SpaceportArmorStrengthFactor - 1.0).ToString("+0%;-0%"));
                }
                else if (race.SpaceportArmorStrengthFactor < 1.0)
                {
                    raceSummarySection.Items.Add(TextResolver.GetText("Weaker Spaceport Armor") + ": " + (race.SpaceportArmorStrengthFactor - 1.0).ToString("+0%;-0%"));
                }
                if (race.TourismIncomeFactor > 1.0)
                {
                    raceSummarySection.Items.Add(TextResolver.GetText("Higher Tourism Income") + ": " + (race.TourismIncomeFactor - 1.0).ToString("+0%;-0%"));
                }
                else if (race.TourismIncomeFactor < 1.0)
                {
                    raceSummarySection.Items.Add(TextResolver.GetText("Lower Tourism Income") + ": " + (race.TourismIncomeFactor - 1.0).ToString("+0%;-0%"));
                }
                if (race.FreeTradeIncomeFactor > 1.0)
                {
                    raceSummarySection.Items.Add(TextResolver.GetText("Higher Trade Income") + ": " + (race.FreeTradeIncomeFactor - 1.0).ToString("+0%;-0%"));
                }
                else if (race.FreeTradeIncomeFactor < 1.0)
                {
                    raceSummarySection.Items.Add(TextResolver.GetText("Lower Trade Income") + ": " + (race.FreeTradeIncomeFactor - 1.0).ToString("+0%;-0%"));
                }
                if (race.MigrationFactor > 1.0)
                {
                    raceSummarySection.Items.Add(TextResolver.GetText("Higher Migration Rate") + ": " + (race.MigrationFactor - 1.0).ToString("+0%;-0%"));
                }
                else if (race.MigrationFactor < 1.0)
                {
                    raceSummarySection.Items.Add(TextResolver.GetText("Lower Migration Rate") + ": " + (race.MigrationFactor - 1.0).ToString("+0%;-0%"));
                }
                if (race.TroopRegenerationFactor > 1.0)
                {
                    raceSummarySection.Items.Add(TextResolver.GetText("Faster Troop Regeneration") + ": " + (race.TroopRegenerationFactor - 1.0).ToString("+0%;-0%"));
                }
                else if (race.TroopRegenerationFactor < 1.0)
                {
                    raceSummarySection.Items.Add(TextResolver.GetText("Slower Troop Regeneration") + ": " + (race.TroopRegenerationFactor - 1.0).ToString("+0%;-0%"));
                }
                if ((double)race.KnownStartingGalacticHistoryLocations > 1.0)
                {
                    raceSummarySection.Items.Add(TextResolver.GetText("Historical Locations Known at Game Start") + ": " + race.KnownStartingGalacticHistoryLocations.ToString("0"));
                }
                if (raceSummarySection.Items.Count > 0)
                {
                    raceSummary.Sections.Add(raceSummarySection);
                }
                return raceSummary;
            }
            return null;
        }

        public static string ResolveDescription(AdvisorMessageType advisorMessageType, EmpireMessage message)
        {
            string result = string.Empty;
            switch (advisorMessageType)
            {
                case AdvisorMessageType.AllowTradeRestrictedResources:
                    result = TextResolver.GetText("Advisor Message AllowTradeRestrictedResources");
                    break;
                case AdvisorMessageType.BuildOneOff:
                    result = TextResolver.GetText("Advisor Message BuildOneOff");
                    break;
                case AdvisorMessageType.BuildOrder:
                    result = TextResolver.GetText("Advisor Message BuildOrder");
                    break;
                case AdvisorMessageType.CancelMilitaryRefueling:
                    result = TextResolver.GetText("Advisor Message CancelMilitaryRefueling");
                    break;
                case AdvisorMessageType.CancelMiningRights:
                    result = TextResolver.GetText("Advisor Message CancelMiningRights");
                    break;
                case AdvisorMessageType.Colonization:
                    result = TextResolver.GetText("Advisor Message Colonization");
                    break;
                case AdvisorMessageType.ColonyFacility:
                    result = TextResolver.GetText("Advisor Message ColonyFacility");
                    break;
                case AdvisorMessageType.ComplyTradeSanctionsOther:
                    result = TextResolver.GetText("Advisor Message ComplyTradeSanctionsOther");
                    break;
                case AdvisorMessageType.ComplyWarOther:
                    result = TextResolver.GetText("Advisor Message ComplyWarOther");
                    break;
                case AdvisorMessageType.DefendTerritory:
                    result = TextResolver.GetText("Advisor Message DefendTerritory");
                    break;
                case AdvisorMessageType.DiplomaticGift:
                    result = TextResolver.GetText("Advisor Message DiplomaticGift");
                    break;
                case AdvisorMessageType.DisallowTradeRestrictedResources:
                    result = TextResolver.GetText("Advisor Message DisallowTradeRestrictedResources");
                    break;
                case AdvisorMessageType.EnemyAttack:
                    result = TextResolver.GetText("Advisor Message EnemyAttack");
                    break;
                case AdvisorMessageType.EnemyAttackPlanetDestroyer:
                    result = TextResolver.GetText("Advisor Message EnemyAttackPlanetDestroyer");
                    break;
                case AdvisorMessageType.EnemyBlockade:
                    result = TextResolver.GetText("Advisor Message EnemyBlockade");
                    break;
                case AdvisorMessageType.EnemyBombard:
                    result = TextResolver.GetText("Advisor Message EnemyBombard");
                    break;
                case AdvisorMessageType.IntelligenceMission:
                    result = TextResolver.GetText("Advisor Message IntelligenceMission");
                    break;
                case AdvisorMessageType.InvadeIndependent:
                    result = TextResolver.GetText("Advisor Message InvadeIndependent");
                    break;
                case AdvisorMessageType.OfferMilitaryRefueling:
                    result = TextResolver.GetText("Advisor Message OfferMilitaryRefueling");
                    break;
                case AdvisorMessageType.OfferMiningRights:
                    result = TextResolver.GetText("Advisor Message OfferMiningRights");
                    break;
                case AdvisorMessageType.OfferPirateAttackMission:
                    result = TextResolver.GetText("Advisor Message OfferPirateAttackMission");
                    break;
                case AdvisorMessageType.OfferPirateDefendMission:
                    result = TextResolver.GetText("Advisor Message OfferPirateDefendMission");
                    break;
                case AdvisorMessageType.DefendTarget:
                    result = TextResolver.GetText("Advisor Message EnemyAttack");
                    break;
                case AdvisorMessageType.OfferPirateSmuggleMission:
                    result = TextResolver.GetText("Advisor Message OfferPirateSmuggleMission");
                    break;
                case AdvisorMessageType.AcceptPirateSmugglingMission:
                    result = TextResolver.GetText("Advisor Message AcceptPirateSmugglingMission");
                    break;
                case AdvisorMessageType.PirateRaid:
                    result = TextResolver.GetText("Advisor Message PirateRaid");
                    break;
                case AdvisorMessageType.PirateFacilityEradicate:
                    result = TextResolver.GetText("Advisor Message PirateFacilityEradicate");
                    break;
                case AdvisorMessageType.PrepareRaid:
                    result = TextResolver.GetText("Advisor Message PrepareRaid");
                    break;
                case AdvisorMessageType.RequestEndWarOther:
                    result = TextResolver.GetText("Advisor Message RequestEndWarOther");
                    break;
                case AdvisorMessageType.RequestLiftTradeSanctionsOther:
                    result = TextResolver.GetText("Advisor Message RequestLiftTradeSanctionsOther");
                    break;
                case AdvisorMessageType.Retrofit:
                    result = TextResolver.GetText("Advisor Message Retrofit");
                    break;
                case AdvisorMessageType.TreatyOffer:
                    result = TextResolver.GetText("Advisor Message TreatyOffer");
                    break;
                case AdvisorMessageType.WarTradeSanctions:
                    result = TextResolver.GetText("Advisor Message WarTradeSanctions");
                    if (message != null && message.Subject is Empire)
                    {
                        Empire empire = (Empire)message.Subject;
                        if (empire != null && message.AdvisorMessageData != null && message.AdvisorMessageData is DiplomaticRelationType)
                        {
                            result = (DiplomaticRelationType)message.AdvisorMessageData switch
                            {
                                DiplomaticRelationType.War => TextResolver.GetText("Advisor Message DeclareWar"),
                                DiplomaticRelationType.TradeSanctions => TextResolver.GetText("Advisor Message InitiateTradeSanctions"),
                                _ => TextResolver.GetText("Advisor Message WarTradeSanctions"),
                            };
                        }
                    }
                    break;
            }
            return result;
        }

        public static string ResolveCharacterSummary(Empire empire)
        {
            string text = string.Empty;
            if (empire != null && empire.Characters != null)
            {
                if (empire.PirateEmpireBaseHabitat == null)
                {
                    string text2 = text;
                    text = text2 + empire.Characters.CountCharactersByRole(CharacterRole.Leader) + " " + TextResolver.GetText("Leader") + ", ";
                    string text3 = text;
                    text = text3 + empire.Characters.CountCharactersByRole(CharacterRole.Ambassador) + " " + TextResolver.GetText("Ambassador") + ", ";
                    string text4 = text;
                    text = text4 + empire.Characters.CountCharactersByRole(CharacterRole.ColonyGovernor) + " " + TextResolver.GetText("Colony Governor") + ", ";
                    string text5 = text;
                    text = text5 + empire.Characters.CountCharactersByRole(CharacterRole.FleetAdmiral) + " " + TextResolver.GetText("Fleet Admiral") + ", ";
                    string text6 = text;
                    text = text6 + empire.Characters.CountCharactersByRole(CharacterRole.ShipCaptain) + " " + TextResolver.GetText("Ship Captain") + ", ";
                    string text7 = text;
                    text = text7 + empire.Characters.CountCharactersByRole(CharacterRole.TroopGeneral) + " " + TextResolver.GetText("Troop General") + ", ";
                    string text8 = text;
                    text = text8 + empire.Characters.CountCharactersByRole(CharacterRole.Scientist) + " " + TextResolver.GetText("Scientist") + ", ";
                    text = text + empire.Characters.CountCharactersByRole(CharacterRole.IntelligenceAgent) + " " + TextResolver.GetText("Intelligence Agent");
                }
                else
                {
                    string text9 = text;
                    text = text9 + empire.Characters.CountCharactersByRole(CharacterRole.PirateLeader) + " " + TextResolver.GetText("Pirate Leader") + ", ";
                    string text10 = text;
                    text = text10 + empire.Characters.CountCharactersByRole(CharacterRole.FleetAdmiral) + " " + TextResolver.GetText("Fleet Admiral") + ", ";
                    string text11 = text;
                    text = text11 + empire.Characters.CountCharactersByRole(CharacterRole.ShipCaptain) + " " + TextResolver.GetText("Ship Captain") + ", ";
                    string text12 = text;
                    text = text12 + empire.Characters.CountCharactersByRole(CharacterRole.Scientist) + " " + TextResolver.GetText("Scientist") + ", ";
                    text = text + empire.Characters.CountCharactersByRole(CharacterRole.IntelligenceAgent) + " " + TextResolver.GetText("Intelligence Agent");
                }
            }
            return text;
        }

        public static string ResolveCharacterLocationDescription(Character character)
        {
            string text = "(" + TextResolver.GetText("Unknown") + ")";
            if (character != null)
            {
                if (character.Location == null)
                {
                    text = "(" + TextResolver.GetText("None") + ")";
                }
                else
                {
                    switch (character.Role)
                    {
                        case CharacterRole.FleetAdmiral:
                        case CharacterRole.TroopGeneral:
                        case CharacterRole.ShipCaptain:
                            text = character.Location.Name;
                            if (character.Location is BuiltObject)
                            {
                                BuiltObject builtObject = (BuiltObject)character.Location;
                                if (builtObject.ShipGroup != null)
                                {
                                    text = text + "  (" + builtObject.ShipGroup.Name + ")";
                                }
                            }
                            break;
                        case CharacterRole.IntelligenceAgent:
                            if (character.Mission != null && character.Mission.Type != 0)
                            {
                                IntelligenceMissionType type = character.Mission.Type;
                                text = ((type != IntelligenceMissionType.CounterIntelligence) ? ("(" + TextResolver.GetText("Unknown") + ")") : character.Location.Name);
                            }
                            else
                            {
                                text = character.Location.Name;
                            }
                            break;
                        default:
                            text = character.Location.Name;
                            break;
                    }
                }
            }
            return text;
        }

        public static string GenerateLeaderBonusDescription(Character leader)
        {
            string text = string.Empty;
            if (leader != null)
            {
                if (leader.BonusesKnown)
                {
                    if (leader.Diplomacy != 0)
                    {
                        string text2 = text;
                        text = text2 + ResolveDescription(CharacterSkillType.Diplomacy) + " " + ((double)leader.Diplomacy / 100.0).ToString("+0%;-0%") + ", ";
                    }
                    if (leader.ColonyIncome != 0)
                    {
                        string text3 = text;
                        text = text3 + ResolveDescription(CharacterSkillType.ColonyIncome) + " " + ((double)leader.ColonyIncome / 100.0).ToString("+0%;-0%") + ", ";
                    }
                    if (leader.ColonyHappiness != 0)
                    {
                        string text4 = text;
                        text = text4 + ResolveDescription(CharacterSkillType.ColonyHappiness) + " " + ((double)leader.ColonyHappiness / 100.0).ToString("+0%;-0%") + ", ";
                    }
                    if (leader.PopulationGrowth != 0)
                    {
                        string text5 = text;
                        text = text5 + ResolveDescription(CharacterSkillType.PopulationGrowth) + " " + ((double)leader.PopulationGrowth / 100.0).ToString("+0%;-0%") + ", ";
                    }
                    if (leader.TradeIncome != 0)
                    {
                        string text6 = text;
                        text = text6 + ResolveDescription(CharacterSkillType.TradeIncome) + " " + ((double)leader.TradeIncome / 100.0).ToString("+0%;-0%") + ", ";
                    }
                    if (leader.TourismIncome != 0)
                    {
                        string text7 = text;
                        text = text7 + ResolveDescription(CharacterSkillType.TourismIncome) + " " + ((double)leader.TourismIncome / 100.0).ToString("+0%;-0%") + ", ";
                    }
                    if (leader.ColonyCorruption != 0)
                    {
                        string text8 = text;
                        text = text8 + ResolveDescription(CharacterSkillType.ColonyCorruption) + " " + ((double)leader.ColonyCorruption / 100.0).ToString("+0%;-0%") + ", ";
                    }
                    if (leader.MiningRate != 0)
                    {
                        string text9 = text;
                        text = text9 + ResolveDescription(CharacterSkillType.MiningRate) + " " + ((double)leader.MiningRate / 100.0).ToString("+0%;-0%") + ", ";
                    }
                    if (leader.TroopRecruitmentRate != 0)
                    {
                        string text10 = text;
                        text = text10 + ResolveDescription(CharacterSkillType.TroopRecruitment) + " " + ((double)leader.TroopRecruitmentRate / 100.0).ToString("+0%;-0%") + ", ";
                    }
                    if (leader.MilitaryShipConstructionSpeed != 0)
                    {
                        string text11 = text;
                        text = text11 + ResolveDescription(CharacterSkillType.MilitaryShipConstructionSpeed) + " " + ((double)leader.MilitaryShipConstructionSpeed / 100.0).ToString("+0%;-0%") + ", ";
                    }
                    if (leader.CivilianShipConstructionSpeed != 0)
                    {
                        string text12 = text;
                        text = text12 + ResolveDescription(CharacterSkillType.CivilianShipConstructionSpeed) + " " + ((double)leader.CivilianShipConstructionSpeed / 100.0).ToString("+0%;-0%") + ", ";
                    }
                    if (leader.ColonyShipConstructionSpeed != 0)
                    {
                        string text13 = text;
                        text = text13 + ResolveDescription(CharacterSkillType.ColonyShipConstructionSpeed) + " " + ((double)leader.ColonyShipConstructionSpeed / 100.0).ToString("+0%;-0%") + ", ";
                    }
                    if (leader.FacilityConstructionSpeed != 0)
                    {
                        string text14 = text;
                        text = text14 + ResolveDescription(CharacterSkillType.FacilityConstructionSpeed) + " " + ((double)leader.FacilityConstructionSpeed / 100.0).ToString("+0%;-0%") + ", ";
                    }
                    if (leader.ResearchEnergy != 0)
                    {
                        string text15 = text;
                        text = text15 + ResolveDescription(CharacterSkillType.ResearchEnergy) + " " + ((double)leader.ResearchEnergy / 100.0).ToString("+0%;-0%") + ", ";
                    }
                    if (leader.ResearchHighTech != 0)
                    {
                        string text16 = text;
                        text = text16 + ResolveDescription(CharacterSkillType.ResearchHighTech) + " " + ((double)leader.ResearchHighTech / 100.0).ToString("+0%;-0%") + ", ";
                    }
                    if (leader.ResearchWeapons != 0)
                    {
                        string text17 = text;
                        text = text17 + ResolveDescription(CharacterSkillType.ResearchWeapons) + " " + ((double)leader.ResearchWeapons / 100.0).ToString("+0%;-0%") + ", ";
                    }
                    if (leader.Espionage != 0)
                    {
                        string text18 = text;
                        text = text18 + ResolveDescription(CharacterSkillType.Espionage) + " " + ((double)leader.Espionage / 100.0).ToString("+0%;-0%") + ", ";
                    }
                    if (leader.CounterEspionage != 0)
                    {
                        string text19 = text;
                        text = text19 + ResolveDescription(CharacterSkillType.CounterEspionage) + " " + ((double)leader.CounterEspionage / 100.0).ToString("+0%;-0%") + ", ";
                    }
                    if (leader.MilitaryShipMaintenance != 0)
                    {
                        string text20 = text;
                        text = text20 + ResolveDescription(CharacterSkillType.MilitaryShipMaintenance) + " " + ((double)leader.MilitaryShipMaintenance / 100.0).ToString("+0%;-0%") + ", ";
                    }
                    if (leader.MilitaryBaseMaintenance != 0)
                    {
                        string text21 = text;
                        text = text21 + ResolveDescription(CharacterSkillType.MilitaryBaseMaintenance) + " " + ((double)leader.MilitaryBaseMaintenance / 100.0).ToString("+0%;-0%") + ", ";
                    }
                    if (leader.CivilianShipMaintenance != 0)
                    {
                        string text2 = text;
                        text = text2 + ResolveDescription(CharacterSkillType.CivilianShipMaintenance) + " " + ((double)leader.CivilianShipMaintenance / 100.0).ToString("+0%;-0%") + ", ";
                    }
                    if (leader.CivilianBaseMaintenance != 0)
                    {
                        string text2 = text;
                        text = text2 + ResolveDescription(CharacterSkillType.CivilianBaseMaintenance) + " " + ((double)leader.CivilianBaseMaintenance / 100.0).ToString("+0%;-0%") + ", ";
                    }
                    if (leader.TroopMaintenance != 0)
                    {
                        string text2 = text;
                        text = text2 + ResolveDescription(CharacterSkillType.TroopMaintenance) + " " + ((double)leader.TroopMaintenance / 100.0).ToString("+0%;-0%") + ", ";
                    }
                    if (leader.WarWeariness != 0)
                    {
                        string text2 = text;
                        text = text2 + ResolveDescription(CharacterSkillType.WarWeariness) + " " + ((double)leader.WarWeariness / 100.0).ToString("+0%;-0%") + ", ";
                    }
                    if (!string.IsNullOrEmpty(text) && text.Length >= 2)
                    {
                        text = text.Substring(0, text.Length - 2);
                    }
                }
                else
                {
                    text = "?";
                }
                if (!string.IsNullOrEmpty(text))
                {
                    text = ResolveDescription(CharacterRole.Leader) + " " + leader.Name + ": " + text;
                }
            }
            return text;
        }

        public static string GenerateCharacterBonusDescription(Habitat habitat)
        {
            string text = string.Empty;
            if (habitat.Empire != null && habitat.Characters != null)
            {
                List<CharacterRole> list = new List<CharacterRole>();
                list.Add(CharacterRole.Leader);
                list.Add(CharacterRole.PirateLeader);
                list.Add(CharacterRole.Ambassador);
                List<CharacterRole> rolesToExclude = list;
                bool flag = false;
                bool flag2 = false;
                bool flag3 = false;
                int num = 0;
                int num2 = 0;
                int num3 = 0;
                int num4 = 0;
                int num5 = 0;
                int num6 = 0;
                int num7 = 0;
                int num8 = 0;
                int num9 = 0;
                int num10 = 0;
                int num11 = 0;
                int num12 = 0;
                int num13 = 0;
                int num14 = 0;
                int num15 = 0;
                int num16 = 0;
                int num17 = 0;
                int num18 = 0;
                if (habitat.Characters != null && habitat.Characters.Count > 0)
                {
                    num = habitat.Characters.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.ColonyIncome);
                    num2 = habitat.Characters.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.ColonyHappiness);
                    num3 = habitat.Characters.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.PopulationGrowth);
                    num4 = habitat.Characters.GetHighestSkillLevelExcludeRoles(CharacterSkillType.TradeIncome, rolesToExclude);
                    num5 = habitat.Characters.GetHighestSkillLevelExcludeRoles(CharacterSkillType.TourismIncome, rolesToExclude);
                    num6 = habitat.Characters.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.ColonyCorruption);
                    num7 = habitat.Characters.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.MiningRate);
                    num8 = habitat.Characters.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.MilitaryShipConstructionSpeed);
                    num9 = habitat.Characters.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.CivilianShipConstructionSpeed);
                    num10 = habitat.Characters.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.ColonyShipConstructionSpeed);
                    num11 = habitat.Characters.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.FacilityConstructionSpeed);
                    num12 = habitat.Characters.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.MilitaryBaseMaintenance);
                    num13 = habitat.Characters.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.CivilianBaseMaintenance);
                    num14 = habitat.Characters.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.WarWeariness);
                    if (num != 0 || num2 != 0 || num3 != 0 || num4 != 0 || num5 != 0 || num6 != 0 || num7 != 0 || num8 != 0 || num9 != 0 || num10 != 0 || num11 != 0 || num12 != 0 || num13 != 0 || num14 != 0)
                    {
                        flag2 = true;
                    }
                    num15 = habitat.Characters.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.TroopMaintenance);
                    if (num15 != 0)
                    {
                        Character characterWithHighestSkillLevelExcludeRole = habitat.Characters.GetCharacterWithHighestSkillLevelExcludeRole(CharacterSkillType.TroopMaintenance, CharacterRole.Leader);
                        if (characterWithHighestSkillLevelExcludeRole != null)
                        {
                            if (characterWithHighestSkillLevelExcludeRole.Role == CharacterRole.ColonyGovernor)
                            {
                                flag2 = true;
                            }
                            else if (characterWithHighestSkillLevelExcludeRole.Role == CharacterRole.TroopGeneral)
                            {
                                flag3 = true;
                            }
                        }
                    }
                    num16 = habitat.Characters.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.TroopRecruitment);
                    if (num16 != 0)
                    {
                        Character characterWithHighestSkillLevelExcludeRole2 = habitat.Characters.GetCharacterWithHighestSkillLevelExcludeRole(CharacterSkillType.TroopRecruitment, CharacterRole.Leader);
                        if (characterWithHighestSkillLevelExcludeRole2 != null)
                        {
                            if (characterWithHighestSkillLevelExcludeRole2.Role == CharacterRole.ColonyGovernor)
                            {
                                flag2 = true;
                            }
                            else if (characterWithHighestSkillLevelExcludeRole2.Role == CharacterRole.TroopGeneral)
                            {
                                flag3 = true;
                            }
                        }
                    }
                    num17 = habitat.Characters.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.TroopGroundDefense);
                    num18 = habitat.Characters.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.TroopRecoveryRate);
                    if (num17 != 0 || num18 != 0)
                    {
                        flag3 = true;
                    }
                }
                if (habitat.Empire != null && habitat.Empire.Leader != null)
                {
                    if (habitat.Empire.Leader.ColonyIncome != 0)
                    {
                        num += habitat.Empire.Leader.ColonyIncome;
                        flag = true;
                    }
                    if (habitat.Empire.Leader.ColonyHappiness != 0)
                    {
                        num2 += habitat.Empire.Leader.ColonyHappiness;
                        flag = true;
                    }
                    if (habitat.Empire.Leader.PopulationGrowth != 0)
                    {
                        num3 += habitat.Empire.Leader.PopulationGrowth;
                        flag = true;
                    }
                    if (habitat.Empire.Leader.TradeIncome != 0)
                    {
                        num4 += habitat.Empire.Leader.TradeIncome;
                        flag = true;
                    }
                    if (habitat.Empire.Leader.TourismIncome != 0)
                    {
                        num5 += habitat.Empire.Leader.TourismIncome;
                        flag = true;
                    }
                    if (habitat.Empire.Leader.ColonyCorruption != 0)
                    {
                        num6 += habitat.Empire.Leader.ColonyCorruption;
                        flag = true;
                    }
                    if (habitat.Empire.Leader.MiningRate != 0)
                    {
                        num7 += habitat.Empire.Leader.MiningRate;
                        flag = true;
                    }
                    if (habitat.Empire.Leader.MilitaryShipConstructionSpeed != 0)
                    {
                        num8 += habitat.Empire.Leader.MilitaryShipConstructionSpeed;
                        flag = true;
                    }
                    if (habitat.Empire.Leader.CivilianShipConstructionSpeed != 0)
                    {
                        num9 += habitat.Empire.Leader.CivilianShipConstructionSpeed;
                        flag = true;
                    }
                    if (habitat.Empire.Leader.ColonyShipConstructionSpeed != 0)
                    {
                        num10 += habitat.Empire.Leader.ColonyShipConstructionSpeed;
                        flag = true;
                    }
                    if (habitat.Empire.Leader.FacilityConstructionSpeed != 0)
                    {
                        num11 += habitat.Empire.Leader.FacilityConstructionSpeed;
                        flag = true;
                    }
                    if (habitat.Empire.Leader.MilitaryBaseMaintenance != 0)
                    {
                        num12 += habitat.Empire.Leader.MilitaryBaseMaintenance;
                        flag = true;
                    }
                    if (habitat.Empire.Leader.CivilianBaseMaintenance != 0)
                    {
                        num13 += habitat.Empire.Leader.CivilianBaseMaintenance;
                        flag = true;
                    }
                    if (habitat.Empire.Leader.WarWeariness != 0)
                    {
                        num14 += habitat.Empire.Leader.WarWeariness;
                        flag = true;
                    }
                    if (habitat.Empire.Leader.TroopMaintenance != 0)
                    {
                        num15 += habitat.Empire.Leader.TroopMaintenance;
                        flag = true;
                    }
                    if (habitat.Empire.Leader.TroopRecruitmentRate != 0)
                    {
                        num16 += habitat.Empire.Leader.TroopRecruitmentRate;
                        flag = true;
                    }
                    if (habitat.Empire.Leader.TroopGroundDefense != 0)
                    {
                        num17 += habitat.Empire.Leader.TroopGroundDefense;
                        flag = true;
                    }
                    if (habitat.Empire.Leader.TroopRecoveryRate != 0)
                    {
                        num18 += habitat.Empire.Leader.TroopRecoveryRate;
                        flag = true;
                    }
                }
                if (num != 0)
                {
                    string text2 = text;
                    text = text2 + ((double)num / 100.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.ColonyIncome) + ", ";
                }
                if (num2 != 0)
                {
                    string text3 = text;
                    text = text3 + ((double)num2 / 100.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.ColonyHappiness) + ", ";
                }
                if (num3 != 0)
                {
                    string text4 = text;
                    text = text4 + ((double)num3 / 100.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.PopulationGrowth) + ", ";
                }
                if (num4 != 0)
                {
                    string text5 = text;
                    text = text5 + ((double)num4 / 100.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.TradeIncome) + ", ";
                }
                if (num5 != 0)
                {
                    string text6 = text;
                    text = text6 + ((double)num5 / 100.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.TourismIncome) + ", ";
                }
                if (num6 != 0)
                {
                    string text7 = text;
                    text = text7 + ((double)num6 / 100.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.ColonyCorruption) + ", ";
                }
                if (num7 != 0)
                {
                    string text8 = text;
                    text = text8 + ((double)num7 / 100.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.MiningRate) + ", ";
                }
                if (num8 != 0)
                {
                    string text9 = text;
                    text = text9 + ((double)num8 / 100.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.MilitaryShipConstructionSpeed) + ", ";
                }
                if (num9 != 0)
                {
                    string text10 = text;
                    text = text10 + ((double)num9 / 100.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.CivilianShipConstructionSpeed) + ", ";
                }
                if (num10 != 0)
                {
                    string text11 = text;
                    text = text11 + ((double)num10 / 100.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.ColonyShipConstructionSpeed) + ", ";
                }
                if (num11 != 0)
                {
                    string text12 = text;
                    text = text12 + ((double)num11 / 100.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.FacilityConstructionSpeed) + ", ";
                }
                if (num12 != 0)
                {
                    string text13 = text;
                    text = text13 + ((double)num12 / 100.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.MilitaryBaseMaintenance) + ", ";
                }
                if (num13 != 0)
                {
                    string text2 = text;
                    text = text2 + ((double)num13 / 100.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.CivilianBaseMaintenance) + ", ";
                }
                if (num14 != 0)
                {
                    string text2 = text;
                    text = text2 + ((double)num14 / 100.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.WarWeariness) + ", ";
                }
                if (num15 != 0)
                {
                    string text2 = text;
                    text = text2 + ((double)num15 / 100.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.TroopMaintenance) + ", ";
                }
                if (num16 != 0)
                {
                    string text2 = text;
                    text = text2 + ((double)num16 / 100.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.TroopRecruitment) + ", ";
                }
                if (num17 != 0)
                {
                    string text2 = text;
                    text = text2 + ((double)num17 / 100.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.TroopGroundDefense) + ", ";
                }
                if (num18 != 0)
                {
                    string text2 = text;
                    text = text2 + ((double)num18 / 100.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.TroopRecoveryRate) + ", ";
                }
                if (!string.IsNullOrEmpty(text) && text.Length >= 2)
                {
                    text = text.Substring(0, text.Length - 2);
                }
                if (flag || flag2 || flag3)
                {
                    string text14 = string.Empty;
                    if (flag)
                    {
                        text14 = text14 + ResolveDescription(CharacterRole.Leader) + " & ";
                    }
                    if (flag2)
                    {
                        text14 = text14 + ResolveDescription(CharacterRole.ColonyGovernor) + " & ";
                    }
                    if (flag3)
                    {
                        text14 = text14 + ResolveDescription(CharacterRole.TroopGeneral) + " & ";
                    }
                    if (!string.IsNullOrEmpty(text14) && text14.Length >= 3)
                    {
                        text14 = text14.Substring(0, text14.Length - 3);
                    }
                    text = text14 + ": " + text;
                }
            }
            return text;
        }

        public static string GenerateCharacterBonusDescription(Fighter fighter)
        {
            string text = string.Empty;
            if (fighter != null && fighter.ParentBuiltObject != null)
            {
                bool flag = false;
                bool flag2 = false;
                double num = fighter.ParentBuiltObject.CaptainFightersBonus;
                if (num != 1.0)
                {
                    flag = true;
                }
                if (fighter.ParentBuiltObject.ShipGroup != null)
                {
                    num += fighter.ParentBuiltObject.ShipGroup.FightersBonus;
                    if (fighter.ParentBuiltObject.ShipGroup.FightersBonus != 1.0)
                    {
                        flag2 = true;
                    }
                }
                if (num != 1.0)
                {
                    if (flag)
                    {
                        text = text + ResolveDescription(CharacterRole.ShipCaptain) + ", ";
                    }
                    if (flag2)
                    {
                        text = text + ResolveDescription(CharacterRole.FleetAdmiral) + ", ";
                    }
                    if (!string.IsNullOrEmpty(text) && text.Length > 2)
                    {
                        text = text.Substring(0, text.Length - 2);
                    }
                    string text2 = text;
                    text = text2 + ": " + (num - 1.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.Fighters);
                }
            }
            return text;
        }

        public static string GenerateCharacterBonusDescription(ShipGroup fleet)
        {
            string text = string.Empty;
            if (fleet != null)
            {
                if (fleet.TargetingBonus != 1.0)
                {
                    string text2 = text;
                    text = text2 + (fleet.TargetingBonus - 1.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.Targeting) + ", ";
                }
                if (fleet.CountermeasuresBonus != 1.0)
                {
                    string text3 = text;
                    text = text3 + (fleet.CountermeasuresBonus - 1.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.Countermeasures) + ", ";
                }
                if (fleet.ShipManeuveringBonus != 1.0)
                {
                    string text4 = text;
                    text = text4 + (fleet.ShipManeuveringBonus - 1.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.ShipManeuvering) + ", ";
                }
                if (fleet.FightersBonus != 1.0)
                {
                    string text5 = text;
                    text = text5 + (fleet.FightersBonus - 1.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.Fighters) + ", ";
                }
                if (fleet.ShipEnergyUsageBonus != 1.0)
                {
                    string text6 = text;
                    text = text6 + (fleet.ShipEnergyUsageBonus - 1.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.ShipEnergyUsage) + ", ";
                }
                if (fleet.WeaponsDamageBonus != 1.0)
                {
                    string text7 = text;
                    text = text7 + (fleet.WeaponsDamageBonus - 1.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.WeaponsDamage) + ", ";
                }
                if (fleet.WeaponsRangeBonus != 1.0)
                {
                    string text8 = text;
                    text = text8 + (fleet.WeaponsRangeBonus - 1.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.WeaponsRange) + ", ";
                }
                if (fleet.ShieldRechargeRateBonus != 1.0)
                {
                    string text9 = text;
                    text = text9 + (fleet.ShieldRechargeRateBonus - 1.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.ShieldRechargeRate) + ", ";
                }
                if (fleet.DamageControlBonus != 1.0)
                {
                    string text10 = text;
                    text = text10 + (fleet.DamageControlBonus - 1.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.DamageControl) + ", ";
                }
                if (fleet.RepairBonus != 1.0)
                {
                    string text11 = text;
                    text = text11 + (fleet.RepairBonus - 1.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.RepairBonus) + ", ";
                }
                if (fleet.HyperjumpSpeedBonus != 1.0)
                {
                    string text12 = text;
                    text = text12 + (fleet.HyperjumpSpeedBonus - 1.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.HyperjumpSpeed) + ", ";
                }
                if (!string.IsNullOrEmpty(text) && text.Length >= 2)
                {
                    text = text.Substring(0, text.Length - 2);
                }
            }
            return text;
        }

        public static string GenerateCharacterBonusDescription(BuiltObject builtObject)
        {
            string text = string.Empty;
            if (builtObject != null)
            {
                List<CharacterRole> list = new List<CharacterRole>();
                list.Add(CharacterRole.Leader);
                list.Add(CharacterRole.Ambassador);
                List<CharacterRole> rolesToExclude = list;
                bool flag = false;
                bool flag2 = false;
                bool flag3 = false;
                bool flag4 = false;
                bool flag5 = false;
                if (builtObject.IsResourceExtractor)
                {
                    int num = 0;
                    CharacterList characterList = new CharacterList();
                    if (builtObject.Characters != null && builtObject.Characters.Count > 0)
                    {
                        characterList.AddRange(builtObject.Characters);
                    }
                    if (builtObject.ParentHabitat != null && builtObject.ParentHabitat.Empire == builtObject.Empire && builtObject.ParentHabitat.Characters != null && builtObject.ParentHabitat.Characters.Count > 0)
                    {
                        characterList.AddRange(builtObject.ParentHabitat.Characters);
                    }
                    if (characterList.Count > 0)
                    {
                        num = characterList.GetHighestSkillLevelExcludeRoles(CharacterSkillType.MiningRate, rolesToExclude);
                        if (num != 0)
                        {
                            flag2 = true;
                        }
                    }
                    if (builtObject.Empire != null && builtObject.Empire.Leader != null && builtObject.Empire.Leader.MiningRate != 0)
                    {
                        num += builtObject.Empire.Leader.MiningRate;
                        flag = true;
                    }
                    if (num != 0)
                    {
                        string text2 = text;
                        text = text2 + ((double)num / 100.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.MiningRate) + ", ";
                    }
                }
                if (builtObject.IsSpacePort)
                {
                    int num2 = 0;
                    CharacterList characterList2 = new CharacterList();
                    if (builtObject.Characters != null && builtObject.Characters.Count > 0)
                    {
                        characterList2.AddRange(builtObject.Characters);
                    }
                    if (builtObject.ParentHabitat != null && builtObject.ParentHabitat.Empire == builtObject.Empire && builtObject.ParentHabitat.Characters != null && builtObject.ParentHabitat.Characters.Count > 0)
                    {
                        characterList2.AddRange(builtObject.ParentHabitat.Characters);
                    }
                    if (characterList2.Count > 0)
                    {
                        num2 = characterList2.GetHighestSkillLevelExcludeRoles(CharacterSkillType.TradeIncome, rolesToExclude);
                        if (num2 != 0)
                        {
                            flag2 = true;
                        }
                    }
                    if (builtObject.Empire != null && builtObject.Empire.Leader != null && builtObject.Empire.Leader.TradeIncome != 0)
                    {
                        num2 += builtObject.Empire.Leader.TradeIncome;
                        flag = true;
                    }
                    if (num2 != 0)
                    {
                        string text3 = text;
                        text = text3 + ((double)num2 / 100.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.TradeIncome) + ", ";
                    }
                }
                if (builtObject.SubRole == BuiltObjectSubRole.ResortBase)
                {
                    int num3 = 0;
                    CharacterList characterList3 = new CharacterList();
                    if (builtObject.Characters != null && builtObject.Characters.Count > 0)
                    {
                        characterList3.AddRange(builtObject.Characters);
                    }
                    if (builtObject.ParentHabitat != null && builtObject.ParentHabitat.Empire == builtObject.Empire && builtObject.ParentHabitat.Characters != null && builtObject.ParentHabitat.Characters.Count > 0)
                    {
                        characterList3.AddRange(builtObject.ParentHabitat.Characters);
                    }
                    if (characterList3.Count > 0)
                    {
                        num3 = characterList3.GetHighestSkillLevelExcludeRoles(CharacterSkillType.TourismIncome, rolesToExclude);
                        if (num3 != 0)
                        {
                            flag2 = true;
                        }
                    }
                    if (builtObject.Empire != null && builtObject.Empire.Leader != null && builtObject.Empire.Leader.TourismIncome != 0)
                    {
                        num3 += builtObject.Empire.Leader.TourismIncome;
                        flag = true;
                    }
                    if (num3 != 0)
                    {
                        string text4 = text;
                        text = text4 + ((double)num3 / 100.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.TourismIncome) + ", ";
                    }
                }
                if (builtObject.ResearchEnergy > 0 || builtObject.ResearchHighTech > 0 || builtObject.ResearchWeapons > 0)
                {
                    int num4 = 0;
                    int num5 = 0;
                    int num6 = 0;
                    if (builtObject.Characters != null && builtObject.Characters.Count > 0)
                    {
                        num4 = builtObject.Characters.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.ResearchEnergy);
                        num5 = builtObject.Characters.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.ResearchHighTech);
                        num6 = builtObject.Characters.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.ResearchWeapons);
                        if (num4 != 0 || num5 != 0 || num6 != 0)
                        {
                            flag3 = true;
                        }
                    }
                    if (builtObject.Empire != null && builtObject.Empire.Leader != null)
                    {
                        if (builtObject.Empire.Leader.ResearchEnergy != 0)
                        {
                            num4 += builtObject.Empire.Leader.ResearchEnergy;
                            flag = true;
                        }
                        if (builtObject.Empire.Leader.ResearchHighTech != 0)
                        {
                            num5 += builtObject.Empire.Leader.ResearchHighTech;
                            flag = true;
                        }
                        if (builtObject.Empire.Leader.ResearchWeapons != 0)
                        {
                            num6 += builtObject.Empire.Leader.ResearchWeapons;
                            flag = true;
                        }
                    }
                    if (num4 != 0)
                    {
                        string text5 = text;
                        text = text5 + ((double)num4 / 100.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.ResearchEnergy) + ", ";
                    }
                    if (num5 != 0)
                    {
                        string text6 = text;
                        text = text6 + ((double)num5 / 100.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.ResearchHighTech) + ", ";
                    }
                    if (num6 != 0)
                    {
                        string text7 = text;
                        text = text7 + ((double)num6 / 100.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.ResearchWeapons) + ", ";
                    }
                }
                if (builtObject.Role == BuiltObjectRole.Base)
                {
                    int num7 = 0;
                    int num8 = 0;
                    CharacterList characterList4 = new CharacterList();
                    if (builtObject.Characters != null && builtObject.Characters.Count > 0)
                    {
                        characterList4.AddRange(builtObject.Characters);
                    }
                    if (builtObject.ParentHabitat != null && builtObject.ParentHabitat.Empire == builtObject.Empire && builtObject.ParentHabitat.Characters != null && builtObject.ParentHabitat.Characters.Count > 0)
                    {
                        characterList4.AddRange(builtObject.ParentHabitat.Characters);
                    }
                    if (characterList4.Count > 0)
                    {
                        switch (builtObject.SubRole)
                        {
                            case BuiltObjectSubRole.SmallSpacePort:
                            case BuiltObjectSubRole.MediumSpacePort:
                            case BuiltObjectSubRole.LargeSpacePort:
                            case BuiltObjectSubRole.DefensiveBase:
                                num7 = characterList4.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.MilitaryBaseMaintenance);
                                break;
                            default:
                                num8 = characterList4.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.CivilianBaseMaintenance);
                                break;
                        }
                        if (num7 != 0 || num8 != 0)
                        {
                            flag2 = true;
                        }
                    }
                    if (builtObject.Empire != null && builtObject.Empire.Leader != null)
                    {
                        if (builtObject.Empire.Leader.MilitaryBaseMaintenance != 0)
                        {
                            num7 += builtObject.Empire.Leader.MilitaryBaseMaintenance;
                            flag = true;
                        }
                        if (builtObject.Empire.Leader.CivilianBaseMaintenance != 0)
                        {
                            num8 += builtObject.Empire.Leader.CivilianBaseMaintenance;
                            flag = true;
                        }
                    }
                    if (num7 != 0)
                    {
                        string text8 = text;
                        text = text8 + ((double)num7 / 100.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.MilitaryBaseMaintenance) + ", ";
                    }
                    if (num8 != 0)
                    {
                        string text2 = text;
                        text = text2 + ((double)num8 / 100.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.CivilianBaseMaintenance) + ", ";
                    }
                }
                else
                {
                    int num9 = 0;
                    int num10 = 0;
                    if (builtObject.Empire != null && builtObject.Empire.Leader != null)
                    {
                        if (builtObject.Role == BuiltObjectRole.Military && builtObject.Empire.Leader.MilitaryShipMaintenance != 0)
                        {
                            num9 += builtObject.Empire.Leader.MilitaryShipMaintenance;
                            flag = true;
                        }
                        if (builtObject.Role != BuiltObjectRole.Military && builtObject.Empire.Leader.CivilianShipMaintenance != 0)
                        {
                            num10 += builtObject.Empire.Leader.CivilianShipMaintenance;
                            flag = true;
                        }
                    }
                    if (builtObject.Role == BuiltObjectRole.Military && num9 != 0)
                    {
                        string text2 = text;
                        text = text2 + ((double)num9 / 100.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.MilitaryShipMaintenance) + ", ";
                    }
                    if (builtObject.Role != BuiltObjectRole.Military && num10 != 0)
                    {
                        string text2 = text;
                        text = text2 + ((double)num10 / 100.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.CivilianShipMaintenance) + ", ";
                    }
                }
                if (builtObject.IsShipYard && builtObject.ConstructionQueue != null)
                {
                    int num11 = 0;
                    int num12 = 0;
                    CharacterList characterList5 = new CharacterList();
                    if (builtObject.Characters != null && builtObject.Characters.Count > 0)
                    {
                        characterList5.AddRange(builtObject.Characters);
                    }
                    if (builtObject.ParentHabitat != null && builtObject.ParentHabitat.Empire == builtObject.Empire && builtObject.ParentHabitat.Characters != null && builtObject.ParentHabitat.Characters.Count > 0)
                    {
                        characterList5.AddRange(builtObject.ParentHabitat.Characters);
                    }
                    if (characterList5.Count > 0)
                    {
                        num11 = characterList5.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.MilitaryShipConstructionSpeed);
                        num12 = characterList5.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.CivilianShipConstructionSpeed);
                        if (num11 != 0 || num12 != 0)
                        {
                            flag2 = true;
                        }
                    }
                    if (builtObject.Empire != null && builtObject.Empire.Leader != null)
                    {
                        if (builtObject.Empire.Leader.MilitaryShipConstructionSpeed != 0)
                        {
                            num11 += builtObject.Empire.Leader.MilitaryShipConstructionSpeed;
                            flag = true;
                        }
                        if (builtObject.Empire.Leader.CivilianShipConstructionSpeed != 0)
                        {
                            num12 += builtObject.Empire.Leader.CivilianShipConstructionSpeed;
                            flag = true;
                        }
                    }
                    if (num11 != 0)
                    {
                        string text2 = text;
                        text = text2 + ((double)num11 / 100.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.MilitaryShipConstructionSpeed) + ", ";
                    }
                    if (num12 != 0)
                    {
                        string text2 = text;
                        text = text2 + ((double)num12 / 100.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.CivilianShipConstructionSpeed) + ", ";
                    }
                }
                double num13 = 1.0;
                double num14 = builtObject.CaptainTargetingBonus;
                double num15 = builtObject.CaptainCountermeasuresBonus;
                double num16 = builtObject.CaptainShipManeuveringBonus;
                double num17 = builtObject.CaptainFightersBonus;
                double num18 = builtObject.CaptainShipEnergyUsageBonus;
                double num19 = builtObject.CaptainWeaponsDamageBonus;
                double num20 = builtObject.CaptainWeaponsRangeBonus;
                double num21 = builtObject.CaptainShieldRechargeRateBonus;
                double num22 = builtObject.CaptainDamageControlBonus;
                double num23 = builtObject.CaptainRepairBonus;
                double num24 = builtObject.CaptainHyperjumpSpeedBonus;
                if (builtObject.ShipGroup != null)
                {
                    num13 += 1.0;
                    num14 += builtObject.ShipGroup.TargetingBonus;
                    num15 += builtObject.ShipGroup.CountermeasuresBonus;
                    num16 += builtObject.ShipGroup.ShipManeuveringBonus;
                    num17 += builtObject.ShipGroup.FightersBonus;
                    num18 += builtObject.ShipGroup.ShipEnergyUsageBonus;
                    num19 += builtObject.ShipGroup.WeaponsDamageBonus;
                    num20 += builtObject.ShipGroup.WeaponsRangeBonus;
                    num21 += builtObject.ShipGroup.ShieldRechargeRateBonus;
                    num22 += builtObject.ShipGroup.DamageControlBonus;
                    num23 += builtObject.ShipGroup.RepairBonus;
                    num24 += builtObject.ShipGroup.HyperjumpSpeedBonus;
                }
                if (num14 != num13)
                {
                    string text2 = text;
                    text = text2 + (num14 - num13).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.Targeting) + ", ";
                    flag4 = true;
                    flag5 = true;
                }
                if (num15 != num13)
                {
                    string text2 = text;
                    text = text2 + (num15 - num13).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.Countermeasures) + ", ";
                    flag4 = true;
                    flag5 = true;
                }
                if (num16 != num13)
                {
                    string text2 = text;
                    text = text2 + (num16 - num13).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.ShipManeuvering) + ", ";
                    flag4 = true;
                    flag5 = true;
                }
                if (num17 != num13)
                {
                    string text2 = text;
                    text = text2 + (num17 - num13).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.Fighters) + ", ";
                    flag4 = true;
                    flag5 = true;
                }
                if (num18 != num13)
                {
                    string text2 = text;
                    text = text2 + (num18 - num13).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.ShipEnergyUsage) + ", ";
                    flag4 = true;
                    flag5 = true;
                }
                if (num19 != num13)
                {
                    string text2 = text;
                    text = text2 + (num19 - num13).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.WeaponsDamage) + ", ";
                    flag4 = true;
                    flag5 = true;
                }
                if (num20 != num13)
                {
                    string text2 = text;
                    text = text2 + (num20 - num13).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.WeaponsRange) + ", ";
                    flag4 = true;
                    flag5 = true;
                }
                if (num21 != num13)
                {
                    string text2 = text;
                    text = text2 + (num21 - num13).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.ShieldRechargeRate) + ", ";
                    flag4 = true;
                    flag5 = true;
                }
                if (num22 != num13)
                {
                    string text2 = text;
                    text = text2 + (num22 - num13).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.DamageControl) + ", ";
                    flag4 = true;
                    flag5 = true;
                }
                if (num23 != num13)
                {
                    string text2 = text;
                    text = text2 + (num23 - num13).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.RepairBonus) + ", ";
                    flag4 = true;
                    flag5 = true;
                }
                if (num24 != num13)
                {
                    string text2 = text;
                    text = text2 + (num24 - num13).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.HyperjumpSpeed) + ", ";
                    flag4 = true;
                    flag5 = true;
                }
                double num25 = 1.0 + (double)builtObject.Characters.GetHighestSkillLevel(CharacterSkillType.BoardingAssault) / 100.0;
                double num26 = 1.0 + (double)builtObject.Characters.GetHighestSkillLevel(CharacterSkillType.SmugglingIncome) / 100.0;
                double num27 = 1.0 + (double)builtObject.Characters.GetHighestSkillLevel(CharacterSkillType.SmugglingEvasion) / 100.0;
                if (num25 != 1.0)
                {
                    string text2 = text;
                    text = text2 + (num25 - 1.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.BoardingAssault) + ", ";
                    flag4 = true;
                    flag5 = true;
                }
                if (num26 != 1.0)
                {
                    string text2 = text;
                    text = text2 + (num26 - 1.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.SmugglingIncome) + ", ";
                    flag4 = true;
                    flag5 = true;
                }
                if (num27 != 1.0)
                {
                    string text2 = text;
                    text = text2 + (num27 - 1.0).ToString("+0%;-0%") + " " + ResolveDescription(CharacterSkillType.SmugglingEvasion) + ", ";
                    flag4 = true;
                    flag5 = true;
                }
                if (!string.IsNullOrEmpty(text) && text.Length >= 2)
                {
                    text = text.Substring(0, text.Length - 2);
                }
                if (flag || flag3 || flag2 || flag4)
                {
                    string text9 = string.Empty;
                    if (flag)
                    {
                        text9 = text9 + ResolveDescription(CharacterRole.Leader) + " & ";
                    }
                    if (flag2)
                    {
                        text9 = text9 + ResolveDescription(CharacterRole.ColonyGovernor) + " & ";
                    }
                    if (flag3)
                    {
                        text9 = text9 + ResolveDescription(CharacterRole.Scientist) + " & ";
                    }
                    if (flag5)
                    {
                        text9 = text9 + ResolveDescription(CharacterRole.ShipCaptain) + " & ";
                    }
                    if (flag4)
                    {
                        text9 = text9 + ResolveDescription(CharacterRole.FleetAdmiral) + " & ";
                    }
                    if (!string.IsNullOrEmpty(text9) && text9.Length >= 3)
                    {
                        text9 = text9.Substring(0, text9.Length - 3);
                    }
                    text = text9 + ": " + text;
                }
            }
            return text;
        }

        public static string ResolveCharacterDescription(Character character)
        {
            return ResolveCharacterDescription(character, includeName: true);
        }

        public static string ResolveCharacterDescription(Character character, bool includeName)
        {
            string text = string.Empty;
            if (character != null)
            {
                if (includeName)
                {
                    string text2 = text;
                    text = text2 + character.Name + " (" + ResolveDescription(character.Role) + ")\n";
                    text += "\n";
                }
                if (!character.BonusesKnown)
                {
                    text = text + "(" + TextResolver.GetText("Character untested - skill levels and traits unknown") + ")\n\n";
                }
                text = text + TextResolver.GetText("Skills").ToUpper(CultureInfo.InvariantCulture) + " (";
                text = text + TextResolver.GetText("including bonuses from traits") + ")\n";
                List<CharacterSkillType> list = character.ResolveCharacterSkillTypes(includeUnknownBonuses: false);
                for (int i = 0; i < list.Count; i++)
                {
                    int skillLevel = character.GetSkillLevel(list[i]);
                    if (character.BonusesKnown)
                    {
                        if (skillLevel != 0)
                        {
                            text = text + ResolveDescription(list[i]) + ": ";
                            text = text + skillLevel.ToString("+#0;-#0") + "%\n";
                        }
                    }
                    else
                    {
                        text = text + ResolveDescription(list[i]) + ": ";
                        text += "?%\n";
                    }
                }
                if (character.Traits != null && character.Traits.Count > 0)
                {
                    text += "\n";
                    text = text + TextResolver.GetText("Traits").ToUpper(CultureInfo.InvariantCulture) + "\n";
                    if (character.BonusesKnown)
                    {
                        for (int j = 0; j < character.Traits.Count; j++)
                        {
                            CharacterTraitType characterTraitType = character.Traits[j];
                            CharacterSkillList characterSkillList = DetermineEffectsOfCharacterTrait(characterTraitType, character.Role);
                            text = text + ResolveDescription(characterTraitType) + ": ";
                            switch (characterTraitType)
                            {
                                case CharacterTraitType.Lazy:
                                case CharacterTraitType.PoorTactician:
                                case CharacterTraitType.Drunk:
                                case CharacterTraitType.LaxDiscipline:
                                    text += string.Format(TextResolver.GetText("Amount To All Skills"), "-5%");
                                    break;
                                case CharacterTraitType.Energetic:
                                case CharacterTraitType.GoodTactician:
                                case CharacterTraitType.ToughDiscipline:
                                    text += string.Format(TextResolver.GetText("Amount To All Skills"), "+5%");
                                    break;
                                case CharacterTraitType.InspiringPresence:
                                    text += TextResolver.GetText("Character Trait Description InspiringPresence");
                                    break;
                                case CharacterTraitType.Demoralizing:
                                    text += TextResolver.GetText("Character Trait Description Demoralizing");
                                    break;
                                case CharacterTraitType.LocalDefenseTactics:
                                    text += string.Format(TextResolver.GetText("Character Trait Description LocalDefenseTactics"), "+20%");
                                    break;
                                case CharacterTraitType.ForeignSpy:
                                    text += TextResolver.GetText("Character Trait Description ForeignSpy");
                                    break;
                                case CharacterTraitType.Patriot:
                                    text += TextResolver.GetText("Character Trait Description Patriot");
                                    break;
                                case CharacterTraitType.UltraGenius:
                                    text += string.Format(TextResolver.GetText("Character Trait Description UltraGenius"), "+20%");
                                    break;
                                case CharacterTraitType.Creative:
                                    text += TextResolver.GetText("Character Trait Description Creative");
                                    break;
                                case CharacterTraitType.Methodical:
                                    text += TextResolver.GetText("Character Trait Description Methodical");
                                    break;
                                default:
                                    {
                                        int num = 0;
                                        for (int k = 0; k < characterSkillList.Count; k++)
                                        {
                                            CharacterSkill characterSkill = characterSkillList[k];
                                            if (characterSkill != null)
                                            {
                                                if (num > 2)
                                                {
                                                    text += "\n";
                                                    num = 0;
                                                }
                                                string text3 = text;
                                                text = text3 + characterSkill.Level.ToString("+#0;-#0") + "% " + ResolveDescription(characterSkill.Type) + ", ";
                                                num++;
                                            }
                                        }
                                        if (characterSkillList.Count > 0)
                                        {
                                            text = text.Substring(0, text.Length - 2);
                                        }
                                        if (characterTraitType == CharacterTraitType.IntelligenceSober || characterTraitType == CharacterTraitType.IntelligenceAddict)
                                        {
                                            text = text + " (" + TextResolver.GetText("Character Trait Description OnlyAppliesToExistingSkills") + ")";
                                        }
                                        break;
                                    }
                            }
                            text += "\n";
                        }
                    }
                    else
                    {
                        text += "?\n";
                    }
                }
                if (!string.IsNullOrEmpty(text) && text.Length > 1)
                {
                    text = text.Substring(0, text.Length - 1);
                }
            }
            return text;
        }

        public CharacterList ResolveCharactersValidForLocation(StellarObject location, Empire empire)
        {
            CharacterList characterList = new CharacterList();
            if (empire != null && location != null)
            {
                if (location.Empire == empire)
                {
                    if (location is Habitat)
                    {
                        characterList = empire.Characters.GetNonTransferringCharacters();
                    }
                    else if (location is BuiltObject)
                    {
                        BuiltObject builtObject = (BuiltObject)location;
                        if (builtObject.Owner == empire)
                        {
                            characterList = empire.Characters.GetNonTransferringCharacters();
                        }
                    }
                }
                else if (location is Habitat)
                {
                    Habitat habitat = (Habitat)location;
                    if (habitat.Empire != null && habitat.Empire != IndependentEmpire && habitat.Population != null && habitat.Population.TotalAmount > 0 && habitat == habitat.Empire.Capital)
                    {
                        DiplomaticRelation diplomaticRelation = empire.ObtainDiplomaticRelation(habitat.Empire);
                        if (diplomaticRelation.Type != 0 && diplomaticRelation.Type != DiplomaticRelationType.War)
                        {
                            characterList = empire.Characters.GetNonTransferringCharacters(CharacterRole.Ambassador);
                        }
                    }
                }
                CharacterList characterList2 = new CharacterList();
                for (int i = 0; i < characterList.Count; i++)
                {
                    if (characterList[i].Location == location)
                    {
                        characterList2.Add(characterList[i]);
                    }
                }
                for (int j = 0; j < characterList2.Count; j++)
                {
                    characterList.Remove(characterList2[j]);
                }
            }
            return characterList;
        }

        public bool ChanceColonyGovernorPromotion(Empire empire, Habitat colony)
        {
            if (empire != null && colony != null && colony.Characters != null && Rnd.Next(0, 40) == 1)
            {
                CharacterList charactersByRole = colony.Characters.GetCharactersByRole(CharacterRole.ColonyGovernor);
                if (charactersByRole.Count > 0)
                {
                    int index = Rnd.Next(0, charactersByRole.Count);
                    Character character = charactersByRole[index];
                }
            }
            return false;
        }

        public bool ChanceNewColonyGovernor(Empire empire, Habitat colony)
        {
            if (empire != null && colony != null && empire.Colonies != null && empire.Colonies.Count > 1)
            {
                int num = 15;
                if (empire.DominantRace != null)
                {
                    num = Math.Max(2, (int)((double)num / empire.DominantRace.CharacterRandomAppearanceChanceGovernor));
                }
                if (Rnd.Next(0, num) == 1 && empire.CharactersCanGenerateAmountNonIntelligenceAgent() > 0)
                {
                    Character character = empire.GenerateNewCharacter(CharacterRole.ColonyGovernor, colony);
                    string title = string.Format(TextResolver.GetText("New Character Event Title"), ResolveDescription(character.Role));
                    string description = string.Format(TextResolver.GetText("New Character Event Colony Governor"), colony.Name, character.Name);
                    empire.SendMessageToEmpireWithTitle(empire, EmpireMessageType.CharacterAppearance, character, description, title);
                    return true;
                }
            }
            return false;
        }

        public bool ChanceNewAmbassador(Empire empire, DiplomaticRelationType newRelationType, Empire otherEmpire)
        {
            if (empire != null && otherEmpire != null)
            {
                int num = 1000;
                switch (newRelationType)
                {
                    case DiplomaticRelationType.FreeTradeAgreement:
                        num = 20;
                        break;
                    case DiplomaticRelationType.Protectorate:
                        num = 10;
                        break;
                    case DiplomaticRelationType.MutualDefensePact:
                        num = 6;
                        break;
                }
                if (empire.DominantRace != null)
                {
                    num = Math.Max(2, (int)((double)num / empire.DominantRace.CharacterRandomAppearanceChanceAmbassador));
                }
                if (num < 100 && Rnd.Next(0, num) == 1 && empire.CharactersCanGenerateAmountNonIntelligenceAgent() > 0 && otherEmpire != null && otherEmpire.Capital != null)
                {
                    Character character = empire.GenerateNewCharacter(CharacterRole.Ambassador, otherEmpire.Capital);
                    string title = string.Format(TextResolver.GetText("New Character Event Title"), ResolveDescription(character.Role));
                    string description = string.Format(TextResolver.GetText("New Character Event Ambassador"), ResolveDescription(newRelationType), otherEmpire.Name, character.Name);
                    empire.SendMessageToEmpireWithTitle(empire, EmpireMessageType.CharacterAppearance, character, description, title);
                    return true;
                }
            }
            return false;
        }

        public void ChanceScientistPromotion(Empire empire, ResearchNode researchProject)
        {
            if (empire == null || researchProject == null)
            {
                return;
            }
            CharacterList characterList = new CharacterList();
            CharacterEventType eventType = CharacterEventType.ResearchAdvanceEnergy;
            switch (researchProject.Industry)
            {
                case IndustryType.Weapon:
                    if (empire.ResearchBonusWeaponsStation != null && empire.ResearchBonusWeaponsStation.Characters != null)
                    {
                        characterList = empire.ResearchBonusWeaponsStation.Characters.GetCharactersByRole(CharacterRole.Scientist);
                        eventType = CharacterEventType.ResearchAdvanceWeapons;
                    }
                    break;
                case IndustryType.Energy:
                    if (empire.ResearchBonusEnergyStation != null && empire.ResearchBonusEnergyStation.Characters != null)
                    {
                        characterList = empire.ResearchBonusEnergyStation.Characters.GetCharactersByRole(CharacterRole.Scientist);
                        eventType = CharacterEventType.ResearchAdvanceEnergy;
                    }
                    break;
                case IndustryType.HighTech:
                    if (empire.ResearchBonusHighTechStation != null && empire.ResearchBonusHighTechStation.Characters != null)
                    {
                        characterList = empire.ResearchBonusHighTechStation.Characters.GetCharactersByRole(CharacterRole.Scientist);
                        eventType = CharacterEventType.ResearchAdvanceHighTech;
                    }
                    break;
            }
            if (characterList.Count > 0)
            {
                DoCharacterEvent(eventType, researchProject, characterList);
            }
        }

        public bool ChanceNewScientist(Empire empire, ResearchNode researchProject)
        {
            if (empire != null && researchProject != null)
            {
                int num = 25;
                if (empire.DominantRace != null)
                {
                    num = Math.Max(2, (int)((double)num / empire.DominantRace.CharacterRandomAppearanceChanceScientist));
                }
                if (Rnd.Next(0, num) == 1 && empire.CharactersCanGenerateAmountNonIntelligenceAgent() > 0)
                {
                    Character character = null;
                    switch (researchProject.Industry)
                    {
                        case IndustryType.Weapon:
                            if (empire.ResearchBonusWeaponsStation != null && empire.ResearchBonusWeaponsStation.Characters != null)
                            {
                                character = empire.GenerateNewCharacter(CharacterRole.Scientist, empire.ResearchBonusWeaponsStation);
                            }
                            break;
                        case IndustryType.Energy:
                            if (empire.ResearchBonusEnergyStation != null && empire.ResearchBonusEnergyStation.Characters != null)
                            {
                                character = empire.GenerateNewCharacter(CharacterRole.Scientist, empire.ResearchBonusEnergyStation);
                            }
                            break;
                        case IndustryType.HighTech:
                            if (empire.ResearchBonusHighTechStation != null && empire.ResearchBonusHighTechStation.Characters != null)
                            {
                                character = empire.GenerateNewCharacter(CharacterRole.Scientist, empire.ResearchBonusHighTechStation);
                            }
                            break;
                    }
                    if (character != null)
                    {
                        string title = string.Format(TextResolver.GetText("New Character Event Title"), ResolveDescription(character.Role));
                        string description = string.Format(TextResolver.GetText("New Character Event Scientist"), researchProject.Name, character.Name);
                        empire.SendMessageToEmpireWithTitle(empire, EmpireMessageType.CharacterAppearance, character, description, title);
                        return true;
                    }
                }
            }
            return false;
        }

        public bool ChanceNewScientistCriticalSuccess(Empire empire, ResearchNode researchProject)
        {
            if (empire != null && researchProject != null)
            {
                int num = 6;
                if (empire.DominantRace != null)
                {
                    num = Math.Max(2, (int)((double)num / empire.DominantRace.CharacterRandomAppearanceChanceScientist));
                }
                if (Rnd.Next(0, num) == 1 && empire.CharactersCanGenerateAmountNonIntelligenceAgent() > 0)
                {
                    Character character = null;
                    switch (researchProject.Industry)
                    {
                        case IndustryType.Weapon:
                            if (empire.ResearchBonusWeaponsStation != null && empire.ResearchBonusWeaponsStation.Characters != null)
                            {
                                character = empire.GenerateNewCharacter(CharacterRole.Scientist, empire.ResearchBonusWeaponsStation);
                            }
                            break;
                        case IndustryType.Energy:
                            if (empire.ResearchBonusEnergyStation != null && empire.ResearchBonusEnergyStation.Characters != null)
                            {
                                character = empire.GenerateNewCharacter(CharacterRole.Scientist, empire.ResearchBonusEnergyStation);
                            }
                            break;
                        case IndustryType.HighTech:
                            if (empire.ResearchBonusHighTechStation != null && empire.ResearchBonusHighTechStation.Characters != null)
                            {
                                character = empire.GenerateNewCharacter(CharacterRole.Scientist, empire.ResearchBonusHighTechStation);
                            }
                            break;
                    }
                    if (character != null)
                    {
                        string title = string.Format(TextResolver.GetText("New Character Event Title"), ResolveDescription(character.Role));
                        string description = string.Format(TextResolver.GetText("New Character Event Scientist Critical Success"), researchProject.Name, character.Name);
                        empire.SendMessageToEmpireWithTitle(empire, EmpireMessageType.CharacterAppearance, character, description, title);
                        return true;
                    }
                }
            }
            return false;
        }

        public bool ChanceRaceEvent(BuiltObject targetDestroyed, BuiltObject destroyer)
        {
            if (targetDestroyed != null && destroyer != null && destroyer.ShipGroup != null)
            {
                int num = 0;
                if (targetDestroyed.SubRole == BuiltObjectSubRole.MediumSpacePort)
                {
                    num = 6;
                }
                if (targetDestroyed.SubRole == BuiltObjectSubRole.LargeSpacePort)
                {
                    num = 3;
                }
                if (targetDestroyed.SubRole == BuiltObjectSubRole.Carrier)
                {
                    num = 10;
                }
                else if (targetDestroyed.SubRole == BuiltObjectSubRole.ResupplyShip)
                {
                    num = 6;
                }
                else if (targetDestroyed.SubRole == BuiltObjectSubRole.CapitalShip)
                {
                    num = 8;
                    if (targetDestroyed.IsPlanetDestroyer || targetDestroyed.Size > 2000)
                    {
                        num = 2;
                    }
                }
                if (num > 0 && Rnd.Next(0, num) == 1 && destroyer.Empire != null && destroyer.Empire.WarWeariness != 0.0 && destroyer.Empire.DominantRace != null && destroyer.Empire.DominantRace.RaceEvents.ContainsEventType(RaceEventType.NeverSurrenderWarWearinessReset))
                {
                    double num2 = 0.4 + Rnd.NextDouble() * 0.2;
                    destroyer.Empire.WarWearinessRaw *= num2;
                    string title = ResolveDescription(RaceEventType.NeverSurrenderWarWearinessReset);
                    string message = string.Format(TextResolver.GetText("Race Event Description NeverSurrenderWarWearinessReset"), destroyer.ShipGroup.Name);
                    destroyer.Empire.SendEventMessageToEmpire(EventMessageType.RaceEvent, title, message, RaceEventType.NeverSurrenderWarWearinessReset, destroyer);
                    return true;
                }
            }
            return false;
        }

        public bool ChanceNewShipCaptain(BuiltObject targetDestroyed, Empire empire, StellarObject location)
        {
            return ChanceNewShipCaptain(targetDestroyed, empire, location, targetCaptured: false, smuggler: false);
        }

        public bool ChanceNewShipCaptain(BuiltObject targetDestroyed, Empire empire, StellarObject location, bool targetCaptured, bool smuggler)
        {
            if (targetDestroyed != null && empire != null && location != null && !location.HasBeenDestroyed)
            {
                int num = 0;
                if (targetDestroyed.Role == BuiltObjectRole.Base && targetDestroyed.Size > 500)
                {
                    num = 7;
                }
                if (targetDestroyed.SubRole == BuiltObjectSubRole.MediumSpacePort)
                {
                    num = 6;
                }
                if (targetDestroyed.SubRole == BuiltObjectSubRole.LargeSpacePort)
                {
                    num = 3;
                }
                if (targetDestroyed.SubRole == BuiltObjectSubRole.Carrier)
                {
                    num = 8;
                }
                else if (targetDestroyed.SubRole == BuiltObjectSubRole.Cruiser)
                {
                    num = 11;
                }
                else if (targetDestroyed.SubRole == BuiltObjectSubRole.Destroyer)
                {
                    num = 25;
                }
                else if (targetDestroyed.SubRole == BuiltObjectSubRole.ResupplyShip)
                {
                    num = 7;
                }
                else if (targetDestroyed.SubRole == BuiltObjectSubRole.CapitalShip)
                {
                    num = 5;
                    if (targetDestroyed.IsPlanetDestroyer || targetDestroyed.Size > 2000)
                    {
                        num = 2;
                    }
                }
                if (num > 0)
                {
                    if (empire.DominantRace != null)
                    {
                        num = Math.Max(2, (int)((double)num / empire.DominantRace.CharacterRandomAppearanceChanceShipCaptain));
                    }
                    if (Rnd.Next(0, num) == 1 && empire.CharactersCanGenerateAmountNonIntelligenceAgent() > 0)
                    {
                        Character character = empire.GenerateNewCharacter(CharacterRole.ShipCaptain, location);
                        if (smuggler)
                        {
                            character.AddTrait(CharacterTraitType.Smuggler, starting: true, this);
                        }
                        string title = string.Format(TextResolver.GetText("New Character Event Title"), ResolveDescription(character.Role));
                        string empty = string.Empty;
                        empty = ((!targetCaptured) ? string.Format(TextResolver.GetText("New Character Event Ship Captain"), targetDestroyed.Name, character.Name) : ((!smuggler) ? string.Format(TextResolver.GetText("New Character Event Ship Captain Capture"), targetDestroyed.Name, character.Name) : string.Format(TextResolver.GetText("New Character Event Ship Captain Smuggler Capture"), targetDestroyed.Name, character.Name)));
                        empire.SendMessageToEmpireWithTitle(empire, EmpireMessageType.CharacterAppearance, character, empty, title);
                        return true;
                    }
                }
            }
            return false;
        }

        public bool ChanceNewFleetAdmiral(BuiltObject targetDestroyed, Empire empire, StellarObject location)
        {
            return ChanceNewFleetAdmiral(targetDestroyed, empire, location, targetCaptured: false);
        }

        public bool ChanceNewFleetAdmiral(BuiltObject targetDestroyed, Empire empire, StellarObject location, bool targetCaptured)
        {
            if (targetDestroyed != null && empire != null && location != null && !location.HasBeenDestroyed)
            {
                int num = 0;
                if (targetDestroyed.Role == BuiltObjectRole.Base && targetDestroyed.Empire != null && targetDestroyed.Empire.PirateEmpireBaseHabitat != null)
                {
                    num = 10;
                }
                if (targetDestroyed.SubRole == BuiltObjectSubRole.MediumSpacePort)
                {
                    num = 8;
                }
                if (targetDestroyed.SubRole == BuiltObjectSubRole.LargeSpacePort)
                {
                    num = 4;
                }
                if (targetDestroyed.SubRole == BuiltObjectSubRole.Carrier)
                {
                    num = 12;
                }
                else if (targetDestroyed.SubRole == BuiltObjectSubRole.Cruiser)
                {
                    num = 16;
                }
                else if (targetDestroyed.SubRole == BuiltObjectSubRole.Destroyer)
                {
                    num = 40;
                }
                else if (targetDestroyed.SubRole == BuiltObjectSubRole.ResupplyShip)
                {
                    num = 10;
                }
                else if (targetDestroyed.SubRole == BuiltObjectSubRole.CapitalShip)
                {
                    num = 8;
                    if (targetDestroyed.IsPlanetDestroyer || targetDestroyed.Size > 2000)
                    {
                        num = 2;
                    }
                }
                if (num > 0 && Rnd.Next(0, num) == 1 && empire.CharactersCanGenerateAmountNonIntelligenceAgent() > 0)
                {
                    Character character = empire.GenerateNewCharacter(CharacterRole.FleetAdmiral, location);
                    string title = string.Format(TextResolver.GetText("New Character Event Title"), ResolveDescription(character.Role));
                    string empty = string.Empty;
                    empty = ((!targetCaptured) ? string.Format(TextResolver.GetText("New Character Event Fleet Admiral"), targetDestroyed.Name, character.Name) : string.Format(TextResolver.GetText("New Character Event Fleet Admiral Capture"), targetDestroyed.Name, character.Name));
                    empire.SendMessageToEmpireWithTitle(empire, EmpireMessageType.CharacterAppearance, character, empty, title);
                    return true;
                }
            }
            return false;
        }

        public bool ChanceNewFleetAdmiralFromConstruction(BuiltObject builtObjectConstructed, Empire empire, StellarObject location)
        {
            if (builtObjectConstructed != null && empire != null && location != null && !location.HasBeenDestroyed)
            {
                int num = 0;
                if (builtObjectConstructed.SubRole == BuiltObjectSubRole.DefensiveBase)
                {
                    num = 30;
                }
                else if (builtObjectConstructed.SubRole == BuiltObjectSubRole.Carrier)
                {
                    num = 20;
                }
                else if (builtObjectConstructed.SubRole == BuiltObjectSubRole.ResupplyShip)
                {
                    num = 15;
                }
                else if (builtObjectConstructed.SubRole == BuiltObjectSubRole.Cruiser)
                {
                    num = 20;
                }
                else if (builtObjectConstructed.SubRole == BuiltObjectSubRole.CapitalShip)
                {
                    num = 15;
                    if (builtObjectConstructed.IsPlanetDestroyer || builtObjectConstructed.Size > 2000)
                    {
                        num = 5;
                    }
                }
                if (num > 0 && Rnd.Next(0, num) == 1 && empire.CharactersCanGenerateAmountNonIntelligenceAgent() > 0)
                {
                    Character character = empire.GenerateNewCharacter(CharacterRole.FleetAdmiral, location);
                    string title = string.Format(TextResolver.GetText("New Character Event Title"), ResolveDescription(character.Role));
                    string description = string.Format(TextResolver.GetText("New Character Event Construction Fleet Admiral"), ResolveDescription(builtObjectConstructed.SubRole), builtObjectConstructed.Name, character.Name);
                    empire.SendMessageToEmpireWithTitle(empire, EmpireMessageType.CharacterAppearance, character, description, title);
                    return true;
                }
            }
            return false;
        }

        public bool ChanceNewTroopGeneralFromInvasion(Empire empire, Habitat invadedColony, bool invading)
        {
            if (empire != null && invadedColony != null && !invadedColony.HasBeenDestroyed && invadedColony.Population != null && invadedColony.Population.TotalAmount > 100000000)
            {
                int num = 8;
                if (empire.DominantRace != null)
                {
                    num = Math.Max(2, (int)((double)num / empire.DominantRace.CharacterRandomAppearanceChanceGeneral));
                }
                if (invading)
                {
                    num = (int)((double)num * 1.5);
                }
                if (Rnd.Next(0, num) == 1 && empire.CharactersCanGenerateAmountNonIntelligenceAgent() > 0)
                {
                    Character character = empire.GenerateNewCharacter(CharacterRole.TroopGeneral, invadedColony);
                    Habitat habitat = DetermineHabitatSystemStar(invadedColony);
                    string title = string.Format(TextResolver.GetText("New Character Event Title"), ResolveDescription(character.Role));
                    string empty = string.Empty;
                    empty = ((!invading) ? string.Format(TextResolver.GetText("New Character Event Defense Troop General"), invadedColony.Name, habitat.Name, character.Name) : string.Format(TextResolver.GetText("New Character Event Invasion Troop General"), ResolveDescription(invadedColony.Type).ToLower(CultureInfo.InvariantCulture), ResolveDescription(invadedColony.Category).ToLower(CultureInfo.InvariantCulture), invadedColony.Name, habitat.Name, character.Name));
                    empire.SendMessageToEmpireWithTitle(empire, EmpireMessageType.CharacterAppearance, character, empty, title);
                    return true;
                }
            }
            return false;
        }

        public bool ChanceNewTroopGeneralFromRecruitment(Troop troopRecruited, Empire empire, StellarObject location)
        {
            if (troopRecruited != null && empire != null && location != null && !location.HasBeenDestroyed)
            {
                int num = 70;
                if (empire.DominantRace != null)
                {
                    num = Math.Max(2, (int)((double)num / empire.DominantRace.CharacterRandomAppearanceChanceGeneral));
                }
                if (Rnd.Next(0, num) == 1 && empire.CharactersCanGenerateAmountNonIntelligenceAgent() > 0)
                {
                    Character character = empire.GenerateNewCharacter(CharacterRole.TroopGeneral, location);
                    string title = string.Format(TextResolver.GetText("New Character Event Title"), ResolveDescription(character.Role));
                    string description = string.Format(TextResolver.GetText("New Character Event Troop Recruit Troop General"), troopRecruited.Name, character.Name);
                    empire.SendMessageToEmpireWithTitle(empire, EmpireMessageType.CharacterAppearance, character, description, title);
                    return true;
                }
            }
            return false;
        }

        public static string ResolveDescriptionCharacterTask(Character character, Galaxy galaxy)
        {
            string result = string.Empty;
            if (character != null)
            {
                switch (character.Role)
                {
                    case CharacterRole.IntelligenceAgent:
                        result = "(" + TextResolver.GetText("No mission") + ")";
                        if (character.Mission != null && character.Mission.Type != 0)
                        {
                            string text3 = ResolveDescription(character.Mission, character.Empire);
                            text3 = CapitalizeFirstLetter(text3);
                            if (character.Mission.Type == IntelligenceMissionType.DeepCover && character.Mission.Outcome == IntelligenceMissionOutcome.SucceedNotDetect)
                            {
                                text3 = string.Format(TextResolver.GetText("Deep cover in the EMPIRE"), character.Mission.TargetEmpire.Name);
                            }
                            string arg3 = ResolveStarDateDescription(character.Mission.StartDate + character.Mission.TimeLength);
                            arg3 = string.Format(TextResolver.GetText("completed DATE"), arg3);
                            if (character.Mission.TimeLength > 1000000000)
                            {
                                arg3 = TextResolver.GetText("Until cancelled");
                            }
                            result = text3 + "  (" + arg3 + ")";
                        }
                        else if (character.TransferDestination != null && character.TransferTimeRemaining > 0f)
                        {
                            result = string.Format(TextResolver.GetText("Transferring to DESTINATION"), character.TransferDestination.Name);
                            string arg4 = ResolveStarDateDescription(character.TransferExpectedArrivalDate(galaxy));
                            result = result + " (" + string.Format(TextResolver.GetText("Expected Arrival"), arg4) + ")";
                        }
                        break;
                    case CharacterRole.TroopGeneral:
                        if (character.Location != null && character.Location.Empire != character.Empire && character.Empire.CheckAtWarWithEmpire(character.Location.Empire))
                        {
                            result = string.Format(TextResolver.GetText("Leading invasion of LOCATION"), character.Location.Name);
                        }
                        else
                        {
                            if (character.TransferDestination == null || !(character.TransferTimeRemaining > 0f))
                            {
                                break;
                            }
                            string text2 = character.TransferDestination.Name;
                            if (character.TransferDestination is BuiltObject)
                            {
                                BuiltObject builtObject3 = (BuiltObject)character.TransferDestination;
                                if (builtObject3.ShipGroup != null)
                                {
                                    text2 = text2 + "  (" + builtObject3.ShipGroup.Name + ")";
                                }
                            }
                            result = string.Format(TextResolver.GetText("Transferring to DESTINATION"), text2);
                            string arg2 = ResolveStarDateDescription(character.TransferExpectedArrivalDate(galaxy));
                            result = result + " (" + string.Format(TextResolver.GetText("Expected Arrival"), arg2) + ")";
                        }
                        break;
                    case CharacterRole.FleetAdmiral:
                        if (character.Location == null)
                        {
                            break;
                        }
                        if (character.Location is BuiltObject)
                        {
                            BuiltObject builtObject6 = (BuiltObject)character.Location;
                            if (builtObject6 != null)
                            {
                                result = ((builtObject6.ShipGroup == null) ? string.Format(TextResolver.GetText("Commanding SHIPNAME"), builtObject6.Name) : string.Format(TextResolver.GetText("Commanding FLEETNAME"), builtObject6.ShipGroup.Name));
                            }
                        }
                        else
                        {
                            result = string.Format(TextResolver.GetText("Waiting at X"), character.Location.Name);
                        }
                        break;
                    case CharacterRole.ShipCaptain:
                        if (character.Location == null)
                        {
                            break;
                        }
                        if (character.Location is BuiltObject)
                        {
                            BuiltObject builtObject2 = (BuiltObject)character.Location;
                            if (builtObject2 != null)
                            {
                                result = ((builtObject2.Role == BuiltObjectRole.Base) ? string.Format(TextResolver.GetText("Commanding SHIPNAME"), character.Location.Name) : string.Format(TextResolver.GetText("Commanding SHIPNAME"), builtObject2.Name));
                            }
                        }
                        else
                        {
                            result = string.Format(TextResolver.GetText("Waiting at X"), character.Location.Name);
                        }
                        break;
                    case CharacterRole.Ambassador:
                        if (character.Location == null)
                        {
                            break;
                        }
                        if (character.Location is Habitat)
                        {
                            Habitat habitat = (Habitat)character.Location;
                            if (habitat != null)
                            {
                                result = ((habitat.Empire == null || habitat.Empire.Capital != habitat || habitat.Empire == character.Empire) ? string.Format(TextResolver.GetText("Waiting at X"), character.Location.Name) : string.Format(TextResolver.GetText("Ambassador to EMPIRE at COLONY"), habitat.Empire.Name, habitat.Name));
                            }
                        }
                        else
                        {
                            result = string.Format(TextResolver.GetText("Waiting at X"), character.Location.Name);
                        }
                        break;
                    case CharacterRole.ColonyGovernor:
                        if (character.Location == null)
                        {
                            break;
                        }
                        if (character.Location is Habitat)
                        {
                            Habitat habitat2 = (Habitat)character.Location;
                            if (habitat2 != null)
                            {
                                result = ((habitat2.Empire != character.Empire) ? string.Format(TextResolver.GetText("Waiting at X"), character.Location.Name) : string.Format(TextResolver.GetText("Governing COLONY"), habitat2.Name));
                            }
                        }
                        else
                        {
                            result = string.Format(TextResolver.GetText("Waiting at X"), character.Location.Name);
                        }
                        break;
                    case CharacterRole.Leader:
                        if (character.Location == null)
                        {
                            break;
                        }
                        if (character.Location is Habitat)
                        {
                            Habitat habitat4 = (Habitat)character.Location;
                            if (habitat4 != null)
                            {
                                result = ((habitat4.Empire == null || habitat4.Empire != character.Empire || !habitat4.Empire.Capitals.Contains(habitat4)) ? string.Format(TextResolver.GetText("Waiting at X"), character.Location.Name) : string.Format(TextResolver.GetText("Ruling from COLONY"), habitat4.Name));
                            }
                        }
                        else
                        {
                            result = string.Format(TextResolver.GetText("Waiting at X"), character.Location.Name);
                        }
                        break;
                    case CharacterRole.PirateLeader:
                        if (character.Location == null)
                        {
                            break;
                        }
                        if (character.Location is Habitat)
                        {
                            Habitat habitat3 = (Habitat)character.Location;
                            if (habitat3 != null)
                            {
                                result = ((!habitat3.GetPirateControl().CheckFactionHasControl(character.Empire.EmpireId)) ? string.Format(TextResolver.GetText("Waiting at X"), character.Location.Name) : string.Format(TextResolver.GetText("Ruling from COLONY"), habitat3.Name));
                            }
                        }
                        else if (character.Location is BuiltObject)
                        {
                            BuiltObject builtObject5 = (BuiltObject)character.Location;
                            if (builtObject5 != null)
                            {
                                result = ((builtObject5.Empire != null && builtObject5.Empire == character.Empire && builtObject5.Role == BuiltObjectRole.Base) ? string.Format(TextResolver.GetText("Ruling from COLONY"), builtObject5.Name) : ((builtObject5.ShipGroup == null) ? string.Format(TextResolver.GetText("Onboard SHIPNAME"), builtObject5.Name) : string.Format(TextResolver.GetText("Commanding FLEETNAME"), builtObject5.ShipGroup.Name)));
                            }
                        }
                        else
                        {
                            result = string.Format(TextResolver.GetText("Waiting at X"), character.Location.Name);
                        }
                        break;
                    case CharacterRole.Scientist:
                        if (character.Location == null)
                        {
                            break;
                        }
                        if (character.Location is BuiltObject)
                        {
                            BuiltObject builtObject4 = (BuiltObject)character.Location;
                            if (builtObject4 != null)
                            {
                                result = ((builtObject4.ResearchEnergy <= 0 && builtObject4.ResearchWeapons <= 0 && builtObject4.ResearchHighTech <= 0) ? string.Format(TextResolver.GetText("Waiting at X"), character.Location.Name) : string.Format(TextResolver.GetText("Researching at X"), builtObject4.Name));
                            }
                        }
                        else
                        {
                            result = string.Format(TextResolver.GetText("Waiting at X"), character.Location.Name);
                        }
                        break;
                    default:
                        {
                            if (character.TransferDestination == null || !(character.TransferTimeRemaining > 0f))
                            {
                                break;
                            }
                            string text = character.TransferDestination.Name;
                            if (character.TransferDestination is BuiltObject)
                            {
                                BuiltObject builtObject = (BuiltObject)character.TransferDestination;
                                if (builtObject.ShipGroup != null)
                                {
                                    text = text + "  (" + builtObject.ShipGroup.Name + ")";
                                }
                            }
                            result = string.Format(TextResolver.GetText("Transferring to DESTINATION"), text);
                            string arg = ResolveStarDateDescription(character.TransferExpectedArrivalDate(galaxy));
                            result = result + " (" + string.Format(TextResolver.GetText("Expected Arrival"), arg) + ")";
                            break;
                        }
                }
                if (character.TransferDestination != null && character.TransferTimeRemaining > 0f)
                {
                    string text4 = character.TransferDestination.Name;
                    if (character.TransferDestination is BuiltObject)
                    {
                        BuiltObject builtObject7 = (BuiltObject)character.TransferDestination;
                        if (builtObject7.ShipGroup != null)
                        {
                            text4 = text4 + "  (" + builtObject7.ShipGroup.Name + ")";
                        }
                    }
                    result = string.Format(TextResolver.GetText("Transferring to DESTINATION"), text4);
                    string arg5 = ResolveStarDateDescription(character.TransferExpectedArrivalDate(galaxy));
                    result = result + " (" + string.Format(TextResolver.GetText("Expected Arrival"), arg5) + ")";
                }
            }
            return result;
        }

        public static string ResolveDescriptionGeneral(ResourceBonus resourceBonus)
        {
            string result = string.Empty;
            if (resourceBonus != null)
            {
                Resource resource = new Resource(resourceBonus.ResourceId);
                switch (resourceBonus.Effect)
                {
                    case ColonyResourceEffect.BaseMaintenanceReduction:
                        result = ((!resourceBonus.AppliesOnlyToSources) ? string.Format(TextResolver.GetText("Race Resource Bonus General BaseMaintenanceReduction"), resource.Name, resourceBonus.Value.ToString("#0")) : string.Format(TextResolver.GetText("Race Resource Bonus General BaseMaintenanceReduction Source"), resource.Name, resourceBonus.Value.ToString("#0")));
                        break;
                    case ColonyResourceEffect.ConstructionSpeed:
                        result = ((!resourceBonus.AppliesOnlyToSources) ? string.Format(TextResolver.GetText("Race Resource Bonus General ConstructionSpeed"), resource.Name, resourceBonus.Value.ToString("#0")) : string.Format(TextResolver.GetText("Race Resource Bonus General ConstructionSpeed Source"), resource.Name, resourceBonus.Value.ToString("#0")));
                        break;
                    case ColonyResourceEffect.Development:
                        result = ((!resourceBonus.AppliesOnlyToSources) ? string.Format(TextResolver.GetText("Race Resource Bonus General Development"), resource.Name, resourceBonus.Value.ToString("#0")) : string.Format(TextResolver.GetText("Race Resource Bonus General Development Source"), resource.Name, resourceBonus.Value.ToString("#0")));
                        break;
                    case ColonyResourceEffect.Happiness:
                        result = ((!resourceBonus.AppliesOnlyToSources) ? string.Format(TextResolver.GetText("Race Resource Bonus General Happiness"), resource.Name, resourceBonus.Value.ToString("#0")) : string.Format(TextResolver.GetText("Race Resource Bonus General Happiness Source"), resource.Name, resourceBonus.Value.ToString("#0")));
                        break;
                    case ColonyResourceEffect.IncomeBoost:
                        result = ((!resourceBonus.AppliesOnlyToSources) ? string.Format(TextResolver.GetText("Race Resource Bonus General IncomeBoost"), resource.Name, resourceBonus.Value.ToString("#0")) : string.Format(TextResolver.GetText("Race Resource Bonus General IncomeBoost Source"), resource.Name, resourceBonus.Value.ToString("#0")));
                        break;
                    case ColonyResourceEffect.PopulationGrowthRate:
                        result = ((!resourceBonus.AppliesOnlyToSources) ? string.Format(TextResolver.GetText("Race Resource Bonus General PopulationGrowthRate"), resource.Name, resourceBonus.Value.ToString("#0")) : string.Format(TextResolver.GetText("Race Resource Bonus General PopulationGrowthRate Source"), resource.Name, resourceBonus.Value.ToString("#0")));
                        break;
                    case ColonyResourceEffect.RecruitedTroopStrength:
                        result = ((!resourceBonus.AppliesOnlyToSources) ? string.Format(TextResolver.GetText("Race Resource Bonus General RecruitedTroopStrength"), resource.Name, resourceBonus.Value.ToString("#0")) : string.Format(TextResolver.GetText("Race Resource Bonus General RecruitedTroopStrength Source"), resource.Name, resourceBonus.Value.ToString("#0")));
                        break;
                    case ColonyResourceEffect.ResearchEnergy:
                        result = ((!resourceBonus.AppliesOnlyToSources) ? string.Format(TextResolver.GetText("Race Resource Bonus General ResearchEnergy"), resource.Name, resourceBonus.Value.ToString("#0")) : string.Format(TextResolver.GetText("Race Resource Bonus General ResearchEnergy Source"), resource.Name, resourceBonus.Value.ToString("#0")));
                        break;
                    case ColonyResourceEffect.ResearchHighTech:
                        result = ((!resourceBonus.AppliesOnlyToSources) ? string.Format(TextResolver.GetText("Race Resource Bonus General ResearchHighTech"), resource.Name, resourceBonus.Value.ToString("#0")) : string.Format(TextResolver.GetText("Race Resource Bonus General ResearchHighTech Source"), resource.Name, resourceBonus.Value.ToString("#0")));
                        break;
                    case ColonyResourceEffect.ResearchWeapons:
                        result = ((!resourceBonus.AppliesOnlyToSources) ? string.Format(TextResolver.GetText("Race Resource Bonus General ResearchWeapons"), resource.Name, resourceBonus.Value.ToString("#0")) : string.Format(TextResolver.GetText("Race Resource Bonus General ResearchWeapons Source"), resource.Name, resourceBonus.Value.ToString("#0")));
                        break;
                    case ColonyResourceEffect.WarWearinessReduction:
                        result = ((!resourceBonus.AppliesOnlyToSources) ? string.Format(TextResolver.GetText("Race Resource Bonus General WarWearinessReduction"), resource.Name, resourceBonus.Value.ToString("#0")) : string.Format(TextResolver.GetText("Race Resource Bonus General WarWearinessReduction Source"), resource.Name, resourceBonus.Value.ToString("#0")));
                        break;
                }
            }
            return result;
        }

        public static string ResolveDescription(ResourceBonus resourceBonus)
        {
            string result = string.Empty;
            if (resourceBonus != null)
            {
                Resource resource = new Resource(resourceBonus.ResourceId);
                switch (resourceBonus.Effect)
                {
                    case ColonyResourceEffect.BaseMaintenanceReduction:
                        result = ((!resourceBonus.AppliesOnlyToSources) ? string.Format(TextResolver.GetText("Race Resource Bonus BaseMaintenanceReduction"), resource.Name, resourceBonus.Value.ToString("#0")) : string.Format(TextResolver.GetText("Race Resource Bonus BaseMaintenanceReduction Source"), resource.Name, resourceBonus.Value.ToString("#0")));
                        break;
                    case ColonyResourceEffect.ConstructionSpeed:
                        result = ((!resourceBonus.AppliesOnlyToSources) ? string.Format(TextResolver.GetText("Race Resource Bonus ConstructionSpeed"), resource.Name, resourceBonus.Value.ToString("#0")) : string.Format(TextResolver.GetText("Race Resource Bonus ConstructionSpeed Source"), resource.Name, resourceBonus.Value.ToString("#0")));
                        break;
                    case ColonyResourceEffect.Development:
                        result = ((!resourceBonus.AppliesOnlyToSources) ? string.Format(TextResolver.GetText("Race Resource Bonus Development"), resource.Name, resourceBonus.Value.ToString("#0")) : string.Format(TextResolver.GetText("Race Resource Bonus Development Source"), resource.Name, resourceBonus.Value.ToString("#0")));
                        break;
                    case ColonyResourceEffect.Happiness:
                        result = ((!resourceBonus.AppliesOnlyToSources) ? string.Format(TextResolver.GetText("Race Resource Bonus Happiness"), resource.Name, resourceBonus.Value.ToString("#0")) : string.Format(TextResolver.GetText("Race Resource Bonus Happiness Source"), resource.Name, resourceBonus.Value.ToString("#0")));
                        break;
                    case ColonyResourceEffect.IncomeBoost:
                        result = ((!resourceBonus.AppliesOnlyToSources) ? string.Format(TextResolver.GetText("Race Resource Bonus IncomeBoost"), resource.Name, resourceBonus.Value.ToString("#0")) : string.Format(TextResolver.GetText("Race Resource Bonus IncomeBoost Source"), resource.Name, resourceBonus.Value.ToString("#0")));
                        break;
                    case ColonyResourceEffect.PopulationGrowthRate:
                        result = ((!resourceBonus.AppliesOnlyToSources) ? string.Format(TextResolver.GetText("Race Resource Bonus PopulationGrowthRate"), resource.Name, resourceBonus.Value.ToString("#0")) : string.Format(TextResolver.GetText("Race Resource Bonus PopulationGrowthRate Source"), resource.Name, resourceBonus.Value.ToString("#0")));
                        break;
                    case ColonyResourceEffect.RecruitedTroopStrength:
                        result = ((!resourceBonus.AppliesOnlyToSources) ? string.Format(TextResolver.GetText("Race Resource Bonus RecruitedTroopStrength"), resource.Name, resourceBonus.Value.ToString("#0")) : string.Format(TextResolver.GetText("Race Resource Bonus RecruitedTroopStrength Source"), resource.Name, resourceBonus.Value.ToString("#0")));
                        break;
                    case ColonyResourceEffect.ResearchEnergy:
                        result = ((!resourceBonus.AppliesOnlyToSources) ? string.Format(TextResolver.GetText("Race Resource Bonus ResearchEnergy"), resource.Name, resourceBonus.Value.ToString("#0")) : string.Format(TextResolver.GetText("Race Resource Bonus ResearchEnergy Source"), resource.Name, resourceBonus.Value.ToString("#0")));
                        break;
                    case ColonyResourceEffect.ResearchHighTech:
                        result = ((!resourceBonus.AppliesOnlyToSources) ? string.Format(TextResolver.GetText("Race Resource Bonus ResearchHighTech"), resource.Name, resourceBonus.Value.ToString("#0")) : string.Format(TextResolver.GetText("Race Resource Bonus ResearchHighTech Source"), resource.Name, resourceBonus.Value.ToString("#0")));
                        break;
                    case ColonyResourceEffect.ResearchWeapons:
                        result = ((!resourceBonus.AppliesOnlyToSources) ? string.Format(TextResolver.GetText("Race Resource Bonus ResearchWeapons"), resource.Name, resourceBonus.Value.ToString("#0")) : string.Format(TextResolver.GetText("Race Resource Bonus ResearchWeapons Source"), resource.Name, resourceBonus.Value.ToString("#0")));
                        break;
                    case ColonyResourceEffect.WarWearinessReduction:
                        result = ((!resourceBonus.AppliesOnlyToSources) ? string.Format(TextResolver.GetText("Race Resource Bonus WarWearinessReduction"), resource.Name, resourceBonus.Value.ToString("#0")) : string.Format(TextResolver.GetText("Race Resource Bonus WarWearinessReduction Source"), resource.Name, resourceBonus.Value.ToString("#0")));
                        break;
                }
            }
            return result;
        }

        public static string ResolveDescription(IntelligenceMission mission, Empire callingEmpire)
        {
            string result = string.Empty;
            if (mission != null)
            {
                bool flag = false;
                switch (mission.Outcome)
                {
                    case IntelligenceMissionOutcome.FailNotDetect:
                    case IntelligenceMissionOutcome.FailDetect:
                    case IntelligenceMissionOutcome.Capture:
                        flag = false;
                        break;
                    case IntelligenceMissionOutcome.SucceedNotDetect:
                    case IntelligenceMissionOutcome.SucceedDetect:
                        flag = true;
                        break;
                }
                switch (mission.Type)
                {
                    case IntelligenceMissionType.CounterIntelligence:
                        result = TextResolver.GetText("IntelligenceMissionOutcome CounterIntelligence");
                        break;
                    case IntelligenceMissionType.DeepCover:
                        result = ((mission.TargetEmpire != callingEmpire) ? ((!flag) ? string.Format(TextResolver.GetText("IntelligenceMissionOutcome DeepCover Fail"), mission.TargetEmpire.Name) : string.Format(TextResolver.GetText("IntelligenceMissionOutcome DeepCover Succeed"), mission.TargetEmpire.Name)) : ((!flag) ? TextResolver.GetText("IntelligenceMissionOutcome DeepCover OurEmpire Fail") : TextResolver.GetText("IntelligenceMissionOutcome DeepCover OurEmpire Succeed")));
                        break;
                    case IntelligenceMissionType.InciteRevolution:
                        result = ((mission.TargetEmpire != callingEmpire) ? ((!flag) ? string.Format(TextResolver.GetText("IntelligenceMissionOutcome InciteRevolution Fail"), mission.TargetEmpire.Name) : string.Format(TextResolver.GetText("IntelligenceMissionOutcome InciteRevolution Succeed"), mission.TargetEmpire.Name)) : ((!flag) ? TextResolver.GetText("IntelligenceMissionOutcome InciteRevolution OurEmpire Fail") : TextResolver.GetText("IntelligenceMissionOutcome InciteRevolution OurEmpire Succeed")));
                        break;
                    case IntelligenceMissionType.SabotageColony:
                        {
                            string arg2 = string.Empty;
                            if (mission.Target is BuiltObject)
                            {
                                arg2 = ((BuiltObject)mission.Target).Name;
                            }
                            else if (mission.Target is Habitat)
                            {
                                arg2 = ((Habitat)mission.Target).Name;
                            }
                            result = ((!flag) ? string.Format(TextResolver.GetText("IntelligenceMissionOutcome SabotageColony Fail"), arg2) : string.Format(TextResolver.GetText("IntelligenceMissionOutcome SabotageColony Succeed"), arg2));
                            break;
                        }
                    case IntelligenceMissionType.DestroyBase:
                        {
                            string arg = string.Empty;
                            if (mission.Target is BuiltObject)
                            {
                                arg = ((BuiltObject)mission.Target).Name;
                            }
                            result = ((!flag) ? string.Format(TextResolver.GetText("IntelligenceMissionOutcome DestroyBase Fail"), arg) : string.Format(TextResolver.GetText("IntelligenceMissionOutcome DestroyBase Succeed"), arg));
                            break;
                        }
                    case IntelligenceMissionType.AssassinateCharacter:
                        {
                            string arg4 = string.Empty;
                            string arg5 = string.Empty;
                            if (mission.Target is Character)
                            {
                                Character character = (Character)mission.Target;
                                arg4 = character.Name;
                                if (character.Location != null)
                                {
                                    arg5 = character.Location.Name;
                                }
                            }
                            result = ((!flag) ? string.Format(TextResolver.GetText("IntelligenceMissionOutcome AssassinateCharacter Fail"), arg4, arg5) : string.Format(TextResolver.GetText("IntelligenceMissionOutcome AssassinateCharacter Succeed"), arg4, arg5));
                            break;
                        }
                    case IntelligenceMissionType.SabotageConstruction:
                        {
                            string arg3 = string.Empty;
                            if (mission.Target is BuiltObject)
                            {
                                arg3 = ((BuiltObject)mission.Target).Name;
                            }
                            else if (mission.Target is Habitat)
                            {
                                arg3 = ((Habitat)mission.Target).Name;
                            }
                            result = ((!flag) ? string.Format(TextResolver.GetText("IntelligenceMissionOutcome SabotageConstruction Fail"), arg3) : string.Format(TextResolver.GetText("IntelligenceMissionOutcome SabotageConstruction Succeed"), arg3));
                            break;
                        }
                    case IntelligenceMissionType.StealGalaxyMap:
                        result = ((mission.TargetEmpire != callingEmpire) ? ((!flag) ? string.Format(TextResolver.GetText("IntelligenceMissionOutcome StealGalaxyMap Fail"), mission.TargetEmpire.Name) : string.Format(TextResolver.GetText("IntelligenceMissionOutcome StealGalaxyMap Succeed"), mission.TargetEmpire.Name)) : ((!flag) ? TextResolver.GetText("IntelligenceMissionOutcome StealGalaxyMap OurEmpire Fail") : TextResolver.GetText("IntelligenceMissionOutcome StealGalaxyMap OurEmpire Succeed")));
                        break;
                    case IntelligenceMissionType.StealTerritoryMap:
                        result = ((mission.TargetEmpire != callingEmpire) ? ((!flag) ? string.Format(TextResolver.GetText("IntelligenceMissionOutcome StealTerritoryMap Fail"), mission.TargetEmpire.Name) : string.Format(TextResolver.GetText("IntelligenceMissionOutcome StealTerritoryMap Succeed"), mission.TargetEmpire.Name)) : ((!flag) ? TextResolver.GetText("IntelligenceMissionOutcome StealTerritoryMap OurEmpire Fail") : TextResolver.GetText("IntelligenceMissionOutcome StealTerritoryMap OurEmpire Succeed")));
                        break;
                    case IntelligenceMissionType.StealOperationsMap:
                        result = ((mission.TargetEmpire != callingEmpire) ? ((!flag) ? string.Format(TextResolver.GetText("IntelligenceMissionOutcome StealOperationsMap Fail"), mission.TargetEmpire.Name) : string.Format(TextResolver.GetText("IntelligenceMissionOutcome StealOperationsMap Succeed"), mission.TargetEmpire.Name)) : ((!flag) ? TextResolver.GetText("IntelligenceMissionOutcome StealOperationsMap OurEmpire Fail") : TextResolver.GetText("IntelligenceMissionOutcome StealOperationsMap OurEmpire Succeed")));
                        break;
                    case IntelligenceMissionType.StealTechData:
                        {
                            if (mission.TargetEmpire == callingEmpire)
                            {
                                result = ((!flag) ? TextResolver.GetText("IntelligenceMissionOutcome StealTechData OurEmpire Fail") : TextResolver.GetText("IntelligenceMissionOutcome StealTechData OurEmpire Succeed"));
                                break;
                            }
                            ResearchNode researchNode = null;
                            if (mission.Target is ResearchNode)
                            {
                                researchNode = (ResearchNode)mission.Target;
                            }
                            result = ((!flag) ? ((researchNode == null) ? string.Format(TextResolver.GetText("IntelligenceMissionOutcome StealTechData Fail"), mission.TargetEmpire.Name) : string.Format(TextResolver.GetText("IntelligenceMissionOutcome StealTechData Project Fail"), researchNode.Name, mission.TargetEmpire.Name)) : ((researchNode == null) ? string.Format(TextResolver.GetText("IntelligenceMissionOutcome StealTechData Succeed"), mission.TargetEmpire.Name) : string.Format(TextResolver.GetText("IntelligenceMissionOutcome StealTechData Project Succeed"), researchNode.Name, mission.TargetEmpire.Name)));
                            break;
                        }
                }
            }
            return result;
        }

        public static string ResolveMissionDescription(Fighter fighter)
        {
            string text = string.Empty;
            if (fighter != null)
            {
                switch (fighter.MissionType)
                {
                    case FighterMissionType.Undefined:
                        text = "(" + TextResolver.GetText("No mission") + ")";
                        break;
                    case FighterMissionType.Attack:
                        text = ((fighter.CurrentTarget == null) ? TextResolver.GetText("Attack") : string.Format(TextResolver.GetText("Attack X"), fighter.CurrentTarget.Name));
                        break;
                    case FighterMissionType.Patrol:
                        text = ((fighter.ParentBuiltObject == null) ? TextResolver.GetText("Patrol") : string.Format(TextResolver.GetText("Patrol X"), fighter.ParentBuiltObject.Name));
                        break;
                    case FighterMissionType.ReturnToCarrier:
                        text = TextResolver.GetText("Return to carrier");
                        if (fighter.ParentBuiltObject != null)
                        {
                            text = text + " (" + fighter.ParentBuiltObject.Name + ")";
                        }
                        break;
                }
            }
            return text;
        }
    }
}
