using System;
using System.Collections.Immutable;
using System.Linq;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace TxtFileParser
{
    internal class RaceConverter
    {
        private const string _tableName = "Race";
        private const string _IDCol = "ID";
        private const string _NameCol = "Name";
        private const string _PictureIndexCol = "PictureIndex";
        private const string _RaceFamilyCol = "RaceFamily";
        private const string _ReproductionRateCol = "ReproductionRate";
        private const string _IntelligenceCol = "Intelligence";
        private const string _AggressionCol = "Aggression";
        private const string _CautionCol = "Caution";
        private const string _FriendlinessCol = "Friendliness";
        private const string _LoyaltyCol = "Loyalty";
        private const string _DesignsPictureFamilyIndexCol = "DesignsPictureFamilyIndex";
        private const string _DesignNamesIndexCol = "DesignNamesIndex";
        private const string _ShipMaintenanceSavingsCol = "ShipMaintenanceSavings";
        private const string _TroopMaintenanceSavingsCol = "TroopMaintenanceSavings";
        private const string _ResourceExtractionBonusCol = "ResourceExtractionBonus";
        private const string _WarWearinessAttenuationCol = "WarWearinessAttenuation";
        private const string _SatisfactionModifierCol = "SatisfactionModifier";
        private const string _ResearchBonusCol = "ResearchBonus";
        private const string _EspionageBonusCol = "EspionageBonus";
        private const string _TradeBonusCol = "TradeBonus";
        private const string _OverallShipDesignFocusCol = "OverallShipDesignFocus";
        private const string _TechFocus1Col = "TechFocus1";
        private const string _TechFocus2Col = "TechFocus2";
        private const string _NativePlanetTypeCol = "NativePlanetType";
        private const string _SpecialComponentCol = "SpecialComponent";
        private const string _WeaponsResearchProjectOrderCol = "WeaponsResearchProjectOrder";
        private const string _EnergyResearchProjectOrderCol = "EnergyResearchProjectOrder";
        private const string _HighTechResearchProjectOrderCol = "HighTechResearchProjectOrder";
        private const string _SpecialGovernmentCol = "SpecialGovernment";
        private const string _PreferredStartingGovernmentCol = "PreferredStartingGovernment";
        private const string _DisallowedGovernmentsCol = "DisallowedGovernments";
        private const string _CanChangeGovernmentCol = "CanChangeGovernment";
        private const string _ExpandingCol = "Expanding";
        private const string _CanBePirateCol = "CanBePirate";
        private const string _CanBeNormalEmpireCol = "CanBeNormalEmpire";
        private const string _PlayableCol = "Playable";
        private const string _PeriodicChangeIntervalCol = "PeriodicChangeInterval";
        private const string _PeriodicChangeLengthCol = "PeriodicChangeLength";
        private const string _PeriodicFactorsGrowthCol = "PeriodicFactorsGrowth";
        private const string _PeriodicFactorsAggressionCol = "PeriodicFactorsAggression";
        private const string _PeriodicFactorsCautionCol = "PeriodicFactorsCaution";
        private const string _PeriodicFactorsFriendlinessCol = "PeriodicFactorsFriendliness";
        private const string _PeriodicChangeCycleEventCol = "PeriodicChangeCycleEvent";
        private const string _ShipSizeFactorCivilianCol = "ShipSizeFactorCivilian";
        private const string _ShipSizeFactorMilitaryCol = "ShipSizeFactorMilitary";
        private const string _DisallowedResearchArea1Col = "DisallowedResearchArea1";
        private const string _DisallowedResearchArea2Col = "DisallowedResearchArea2";
        private const string _DisallowedResearchArea3Col = "DisallowedResearchArea3";
        private const string _DisallowedComponentIdsCol = "DisallowedComponentIds";
        private const string _AdditionalIntelligenceAgentsCol = "AdditionalIntelligenceAgents";
        private const string _ConstructionSpeedFactorCol = "ConstructionSpeedFactor";
        private const string _DefaultPrimaryColorCol = "DefaultPrimaryColor";
        private const string _DefaultSecondaryColorCol = "DefaultSecondaryColor";
        private const string _DefaultFlagDesignCol = "DefaultFlagDesign";
        private const string _HomeSystemNameCol = "HomeSystemName";
        private const string _TroopStrengthCol = "TroopStrength";
        private const string _TroopNameCol = "TroopName";
        private const string _TroopNameArmoredCol = "TroopNameArmored";
        private const string _TroopNamePlanetaryDefenseCol = "TroopNamePlanetaryDefense";
        private const string _TroopNameSpecialForcesCol = "TroopNameSpecialForces";
        private const string _Resource1TypeCol = "Resource1Type";
        private const string _Resource1EffectCol = "Resource1Effect";
        private const string _Resource1AmountCol = "Resource1Amount";
        private const string _Resource1AppliesOnlyToSourceCol = "Resource1AppliesOnlyToSource";
        private const string _Resource2TypeCol = "Resource2Type";
        private const string _Resource2EffectCol = "Resource2Effect";
        private const string _Resource2AmountCol = "Resource2Amount";
        private const string _Resource2AppliesOnlyToSourceCol = "Resource2AppliesOnlyToSource";
        private const string _Resource3TypeCol = "Resource3Type";
        private const string _Resource3EffectCol = "Resource3Effect";
        private const string _Resource3AmountCol = "Resource3Amount";
        private const string _Resource3AppliesOnlyToSourceCol = "Resource3AppliesOnlyToSource";
        private const string _Condition1TypeCol = "Condition1Type";
        private const string _Condition1ValueCol = "Condition1Value";
        private const string _Condition1ProportionCol = "Condition1Proportion";
        private const string _Condition1AdditionalDataCol = "Condition1AdditionalData";
        private const string _Condition2TypeCol = "Condition2Type";
        private const string _Condition2ValueCol = "Condition2Value";
        private const string _Condition2ProportionCol = "Condition2Proportion";
        private const string _Condition2AdditionalDataCol = "Condition2AdditionalData";
        private const string _Condition3TypeCol = "Condition3Type";
        private const string _Condition3ValueCol = "Condition3Value";
        private const string _Condition3ProportionCol = "Condition3Proportion";
        private const string _Condition3AdditionalDataCol = "Condition3AdditionalData";
        private const string _Condition4TypeCol = "Condition4Type";
        private const string _Condition4ValueCol = "Condition4Value";
        private const string _Condition4ProportionCol = "Condition4Proportion";
        private const string _Condition4AdditionalDataCol = "Condition4AdditionalData";
        private const string _Condition5TypeCol = "Condition5Type";
        private const string _Condition5ValueCol = "Condition5Value";
        private const string _Condition5ProportionCol = "Condition5Proportion";
        private const string _Condition5AdditionalDataCol = "Condition5AdditionalData";
        private const string _RaceEvent1TypeCol = "RaceEvent1Type";
        private const string _RaceEvent1FrequencyCol = "RaceEvent1Frequency";
        private const string _RaceEvent2TypeCol = "RaceEvent2Type";
        private const string _RaceEvent2FrequencyCol = "RaceEvent2Frequency";
        private const string _CharacterRandomAppearanceChanceLeaderCol = "CharacterRandomAppearanceChanceLeader";
        private const string _CharacterRandomAppearanceChanceAmbassadorCol = "CharacterRandomAppearanceChanceAmbassador";
        private const string _CharacterRandomAppearanceChanceGovernorCol = "CharacterRandomAppearanceChanceGovernor";
        private const string _CharacterRandomAppearanceChanceAdmiralCol = "CharacterRandomAppearanceChanceAdmiral";
        private const string _CharacterRandomAppearanceChanceGeneralCol = "CharacterRandomAppearanceChanceGeneral";
        private const string _CharacterRandomAppearanceChanceScientistCol = "CharacterRandomAppearanceChanceScientist";
        private const string _CharacterRandomAppearanceChanceIntelligenceAgentCol = "CharacterRandomAppearanceChanceIntelligenceAgent";
        private const string _CharacterRandomAppearanceChanceShipCaptainCol = "CharacterRandomAppearanceChanceShipCaptain";
        private const string _CharacterRandomAppearanceChancePirateLeaderCol = "CharacterRandomAppearanceChancePirateLeader";
        private const string _CharacterStartingTraitLeaderCol = "CharacterStartingTraitLeader";
        private const string _CharacterStartingTraitAmbassadorCol = "CharacterStartingTraitAmbassador";
        private const string _CharacterStartingTraitGovernorCol = "CharacterStartingTraitGovernor";
        private const string _CharacterStartingTraitAdmiralCol = "CharacterStartingTraitAdmiral";
        private const string _CharacterStartingTraitGeneralCol = "CharacterStartingTraitGeneral";
        private const string _CharacterStartingTraitScientistCol = "CharacterStartingTraitScientist";
        private const string _CharacterStartingTraitIntelligenceAgentCol = "CharacterStartingTraitIntelligenceAgent";
        private const string _CharacterStartingTraitShipCaptainCol = "CharacterStartingTraitShipCaptain";
        private const string _CharacterStartingTraitPirateLeaderCol = "CharacterStartingTraitPirateLeader";
        private const string _ResearchColonizationCostFactorContinentalCol = "ResearchColonizationCostFactorContinental";
        private const string _ResearchColonizationCostFactorMarshySwampCol = "ResearchColonizationCostFactorMarshySwamp";
        private const string _ResearchColonizationCostFactorOceanCol = "ResearchColonizationCostFactorOcean";
        private const string _ResearchColonizationCostFactorDesertCol = "ResearchColonizationCostFactorDesert";
        private const string _ResearchColonizationCostFactorIceCol = "ResearchColonizationCostFactorIce";
        private const string _ResearchColonizationCostFactorVolcanicCol = "ResearchColonizationCostFactorVolcanic";
        private const string _ColonyConstructionSpeedFactorContinentalCol = "ColonyConstructionSpeedFactorContinental";
        private const string _ColonyConstructionSpeedFactorMarshySwampCol = "ColonyConstructionSpeedFactorMarshySwamp";
        private const string _ColonyConstructionSpeedFactorOceanCol = "ColonyConstructionSpeedFactorOcean";
        private const string _ColonyConstructionSpeedFactorDesertCol = "ColonyConstructionSpeedFactorDesert";
        private const string _ColonyConstructionSpeedFactorIceCol = "ColonyConstructionSpeedFactorIce";
        private const string _ColonyConstructionSpeedFactorVolcanicCol = "ColonyConstructionSpeedFactorVolcanic";
        private const string _ColonyPopulationPolicyGrowthFactorExterminateCol = "ColonyPopulationPolicyGrowthFactorExterminate";
        private const string _ImmuneNaturalDisastersAtColonyTypeCol = "ImmuneNaturalDisastersAtColonyType";
        private const string _SpaceportArmorStrengthFactorCol = "SpaceportArmorStrengthFactor";
        private const string _KnownStartingGalacticHistoryLocationsCol = "KnownStartingGalacticHistoryLocations";
        private const string _TourismIncomeFactorCol = "TourismIncomeFactor";
        private const string _FreeTradeIncomeFactorCol = "FreeTradeIncomeFactor";
        private const string _MigrationFactorCol = "MigrationFactor";
        private const string _TroopRegenerationFactorCol = "TroopRegenerationFactor";
        private const string _PirateDefaultPlaystyleCol = "PirateDefaultPlaystyle";
        private const string _DesignsPictureFamilyIndexPiratesCol = "DesignsPictureFamilyIndexPirates";
        private const string _ImmuneToPlaguesCol = "ImmuneToPlagues";
        private readonly ConvertType convertType;


        public RaceConverter(ConvertType convertType)
        {
            this.convertType = convertType;
        }

        public bool Parse(string dirPath, string outputFolder)
        {
            bool res = true;
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(dirPath);
                List<string[]> agentFirstNames = new List<string[]>();
                foreach (var item in directoryInfo.GetFiles("*.txt", SearchOption.TopDirectoryOnly))
                {
                    FileStream fileStream = item.OpenRead();
                    StreamReader streamReader = new StreamReader(fileStream);

                    List<string> raceRes = new List<string>();
                    var fileText = streamReader.ReadToEnd().Split("\r\n", StringSplitOptions.TrimEntries);
                    foreach (var item2 in fileText)
                    {
                        if (string.IsNullOrEmpty(item2) || !item2.StartsWith("'"))
                        {
                            var temp = item2.Split(";");
                            if (temp.Length == 2)
                            { raceRes.Add(temp[1].Replace('\'', '′')); }
                        }
                    }

                    agentFirstNames.Add(raceRes.Prepend("0").ToArray());
                    //agentFirstNames[^1] = agentFirstNames[^1].Prepend("0").ToArray();  //fake id 
                }


                WriteXml(dirPath, outputFolder, agentFirstNames);
            }


            catch (Exception ex)
            {
                Console.WriteLine($"Race conversion error: {ex.Message}");
                res = false;
            }
            return res;
        }
        private void WriteXml(string filePath, string outputFolder, List<string[]> values)
        {
            string xmlFilePath = Path.Combine(outputFolder, Path.ChangeExtension(filePath, ".xml"));
            using FileStream fileStream = new FileStream(xmlFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            fileStream.SetLength(0);
            XDocument doc = new XDocument();
            var root = new XElement("root");
            doc.Add(root);
            for (int i = 0; i < values.Count; i++)
            {
                var race = new XElement("Race");
                bool immune = values[i][136].ToUpperInvariant() == "Y" ? true : false;
                bool canChangeGov = values[i][31].ToUpperInvariant() == "Y" ? true : false;
                bool expanding = values[i][32].ToUpperInvariant() == "Y" ? true : false;
                bool canBePirate = values[i][33].ToUpperInvariant() == "Y" ? true : false;
                bool canBeNormalEmpire = values[i][34].ToUpperInvariant() == "Y" ? true : false;
                bool playable = values[i][35].ToUpperInvariant() == "Y" ? true : false;
                bool resource1AppliesOnlyToSource = values[i][63].ToUpperInvariant() == "Y" ? true : false;
                bool resource2AppliesOnlyToSource = values[i][67].ToUpperInvariant() == "Y" ? true : false;
                bool resource3AppliesOnlyToSource = values[i][71].ToUpperInvariant() == "Y" ? true : false;
                if (convertType == ConvertType.Update)
                {
                    race.Value = $"UPDATE {_tableName} SET {_NameCol} = '{values[i][1]}', {_PictureIndexCol} = {values[i][2]}, {_RaceFamilyCol} = {values[i][3]}, {_ReproductionRateCol} = {values[i][4]}, {_IntelligenceCol} = {values[i][5]}, {_AggressionCol} = {values[i][6]}, {_CautionCol} = {values[i][7]}, {_FriendlinessCol} = {values[i][8]}, {_LoyaltyCol} = {values[i][9]}, {_DesignsPictureFamilyIndexCol} = {values[i][10]}, {_DesignNamesIndexCol} = {values[i][11]}, {_ShipMaintenanceSavingsCol} = {values[i][12]}, {_TroopMaintenanceSavingsCol} = {values[i][13]}, {_ResourceExtractionBonusCol} = {values[i][14]}, {_WarWearinessAttenuationCol} = {values[i][15]}, {_SatisfactionModifierCol} = {values[i][16]}, {_ResearchBonusCol} = {values[i][17]}, {_EspionageBonusCol} = {values[i][18]}, {_TradeBonusCol} = {values[i][19]}, {_OverallShipDesignFocusCol} = {values[i][20]}, {_TechFocus1Col} = {values[i][21]}, {_TechFocus2Col} = {values[i][22]}, {_NativePlanetTypeCol} = {values[i][23]}, {_SpecialComponentCol} = {values[i][24]}, {_WeaponsResearchProjectOrderCol} = '{values[i][25]}', {_EnergyResearchProjectOrderCol} = '{values[i][26]}', {_HighTechResearchProjectOrderCol} = '{values[i][27]}', {_SpecialGovernmentCol} = {values[i][28]}, {_PreferredStartingGovernmentCol} = {values[i][29]}, {_DisallowedGovernmentsCol} = '{values[i][30]}', {_CanChangeGovernmentCol} = {canChangeGov}, {_ExpandingCol} = {expanding}, {_CanBePirateCol} = {canBePirate}, {_CanBeNormalEmpireCol} = {canBeNormalEmpire}, {_PlayableCol} = {playable}, {_PeriodicChangeIntervalCol} = {values[i][36]}, {_PeriodicChangeLengthCol} = {values[i][37]}, {_PeriodicFactorsGrowthCol} = {values[i][38]}, {_PeriodicFactorsAggressionCol} = {values[i][39]}, {_PeriodicFactorsCautionCol} = {values[i][40]}, {_PeriodicFactorsFriendlinessCol} = {values[i][41]}, {_PeriodicChangeCycleEventCol} = {values[i][42]}, {_ShipSizeFactorCivilianCol} = {values[i][43]}, {_ShipSizeFactorMilitaryCol} = {values[i][44]}, {_DisallowedResearchArea1Col} = {values[i][45]}, {_DisallowedResearchArea2Col} = {values[i][46]}, {_DisallowedResearchArea3Col} = {values[i][47]}, {_DisallowedComponentIdsCol} = '{values[i][48]}', {_AdditionalIntelligenceAgentsCol} = {values[i][49]}, {_ConstructionSpeedFactorCol} = {values[i][50]}, {_DefaultPrimaryColorCol} = {values[i][51]}, {_DefaultSecondaryColorCol} = {values[i][52]}, {_DefaultFlagDesignCol} = {values[i][53]}, {_HomeSystemNameCol} = '{values[i][54]}', {_TroopStrengthCol} = {values[i][55]}, {_TroopNameCol} = '{values[i][56]}', {_TroopNameArmoredCol} = '{values[i][57]}', {_TroopNamePlanetaryDefenseCol} = '{values[i][58]}', {_TroopNameSpecialForcesCol} = '{values[i][59]}', {_Resource1TypeCol} = {values[i][60]}, {_Resource1EffectCol} = {values[i][61]}, {_Resource1AmountCol} = {values[i][62]}, {_Resource1AppliesOnlyToSourceCol} = {resource1AppliesOnlyToSource.ToString()}, {_Resource2TypeCol} = {values[i][64]}, {_Resource2EffectCol} = {values[i][65]}, {_Resource2AmountCol} = {values[i][66]}, {_Resource2AppliesOnlyToSourceCol} = {resource2AppliesOnlyToSource.ToString()}, {_Resource3TypeCol} = {values[i][68]}, {_Resource3EffectCol} = {values[i][69]}, {_Resource3AmountCol} = {values[i][70]}, {_Resource3AppliesOnlyToSourceCol} = {resource3AppliesOnlyToSource.ToString()}, {_Condition1TypeCol} = {values[i][72]}, {_Condition1ValueCol} = {values[i][73]}, {_Condition1ProportionCol} = {values[i][74]}, {_Condition1AdditionalDataCol} = {values[i][75]}, {_Condition2TypeCol} = {values[i][76]}, {_Condition2ValueCol} = {values[i][77]}, {_Condition2ProportionCol} = {values[i][78]}, {_Condition2AdditionalDataCol} = {values[i][79]}, {_Condition3TypeCol} = {values[i][80]}, {_Condition3ValueCol} = {values[i][81]}, {_Condition3ProportionCol} = {values[i][82]}, {_Condition3AdditionalDataCol} = {values[i][83]}, {_Condition4TypeCol} = {values[i][84]}, {_Condition4ValueCol} = {values[i][85]}, {_Condition4ProportionCol} = {values[i][86]}, {_Condition4AdditionalDataCol} = {values[i][87]}, {_Condition5TypeCol} = {values[i][88]}, {_Condition5ValueCol} = {values[i][89]}, {_Condition5ProportionCol} = {values[i][90]}, {_Condition5AdditionalDataCol} = {values[i][91]}, {_RaceEvent1TypeCol} = {values[i][92]}, {_RaceEvent1FrequencyCol} = {values[i][93]}, {_RaceEvent2TypeCol} = {values[i][94]}, {_RaceEvent2FrequencyCol} = {values[i][95]}, {_CharacterRandomAppearanceChanceLeaderCol} = {values[i][96]}, {_CharacterRandomAppearanceChanceAmbassadorCol} = {values[i][97]}, {_CharacterRandomAppearanceChanceGovernorCol} = {values[i][98]}, {_CharacterRandomAppearanceChanceAdmiralCol} = {values[i][99]}, {_CharacterRandomAppearanceChanceGeneralCol} = {values[i][100]}, {_CharacterRandomAppearanceChanceScientistCol} = {values[i][101]}, {_CharacterRandomAppearanceChanceIntelligenceAgentCol} = {values[i][102]}, {_CharacterRandomAppearanceChanceShipCaptainCol} = {values[i][103]}, {_CharacterRandomAppearanceChancePirateLeaderCol} = {values[i][104]}, {_CharacterStartingTraitLeaderCol} = {values[i][105]}, {_CharacterStartingTraitAmbassadorCol} = {values[i][106]}, {_CharacterStartingTraitGovernorCol} = {values[i][107]}, {_CharacterStartingTraitAdmiralCol} = {values[i][108]}, {_CharacterStartingTraitGeneralCol} = {values[i][109]}, {_CharacterStartingTraitScientistCol} = {values[i][110]}, {_CharacterStartingTraitIntelligenceAgentCol} = {values[i][111]}, {_CharacterStartingTraitShipCaptainCol} = {values[i][112]}, {_CharacterStartingTraitPirateLeaderCol} = {values[i][113]}, {_ResearchColonizationCostFactorContinentalCol} = {values[i][114]}, {_ResearchColonizationCostFactorMarshySwampCol} = {values[i][115]}, {_ResearchColonizationCostFactorOceanCol} = {values[i][116]}, {_ResearchColonizationCostFactorDesertCol} = {values[i][117]}, {_ResearchColonizationCostFactorIceCol} = {values[i][118]}, {_ResearchColonizationCostFactorVolcanicCol} = {values[i][119]}, {_ColonyConstructionSpeedFactorContinentalCol} = {values[i][120]}, {_ColonyConstructionSpeedFactorMarshySwampCol} = {values[i][121]}, {_ColonyConstructionSpeedFactorOceanCol} = {values[i][122]}, {_ColonyConstructionSpeedFactorDesertCol} = {values[i][123]}, {_ColonyConstructionSpeedFactorIceCol} = {values[i][124]}, {_ColonyConstructionSpeedFactorVolcanicCol} = {values[i][125]}, {_ColonyPopulationPolicyGrowthFactorExterminateCol} = {values[i][126]}, {_ImmuneNaturalDisastersAtColonyTypeCol} = {values[i][127]}, {_SpaceportArmorStrengthFactorCol} = {values[i][128]}, {_KnownStartingGalacticHistoryLocationsCol} = {values[i][129]}, {_TourismIncomeFactorCol} = {values[i][130]}, {_FreeTradeIncomeFactorCol} = {values[i][131]}, {_MigrationFactorCol} = {values[i][132]}, {_TroopRegenerationFactorCol} = {values[i][133]}, {_PirateDefaultPlaystyleCol} = {values[i][134]}, {_DesignsPictureFamilyIndexPiratesCol} = {values[i][135]}, {_ImmuneToPlaguesCol} = {immune.ToString()} WHERE {_NameCol} = {values[i][1]}";
                }
                else
                {

                    race.Value = $"INSERT INTO {_tableName} ({_IDCol}, {_NameCol}, {_PictureIndexCol}, {_RaceFamilyCol}, {_ReproductionRateCol}, {_IntelligenceCol}, {_AggressionCol}, {_CautionCol}, {_FriendlinessCol}, {_LoyaltyCol}, {_DesignsPictureFamilyIndexCol}, {_DesignNamesIndexCol}, {_ShipMaintenanceSavingsCol}, {_TroopMaintenanceSavingsCol}, {_ResourceExtractionBonusCol}, {_WarWearinessAttenuationCol}, {_SatisfactionModifierCol}, {_ResearchBonusCol}, {_EspionageBonusCol}, {_TradeBonusCol}, {_OverallShipDesignFocusCol}, {_TechFocus1Col}, {_TechFocus2Col}, {_NativePlanetTypeCol}, {_SpecialComponentCol}, {_WeaponsResearchProjectOrderCol}, {_EnergyResearchProjectOrderCol}, {_HighTechResearchProjectOrderCol}, {_SpecialGovernmentCol}, {_PreferredStartingGovernmentCol}, {_DisallowedGovernmentsCol}, {_CanChangeGovernmentCol}, {_ExpandingCol}, {_CanBePirateCol}, {_CanBeNormalEmpireCol}, {_PlayableCol}, {_PeriodicChangeIntervalCol}, {_PeriodicChangeLengthCol}, {_PeriodicFactorsGrowthCol}, {_PeriodicFactorsAggressionCol}, {_PeriodicFactorsCautionCol}, {_PeriodicFactorsFriendlinessCol}, {_PeriodicChangeCycleEventCol}, {_ShipSizeFactorCivilianCol}, {_ShipSizeFactorMilitaryCol}, {_DisallowedResearchArea1Col}, {_DisallowedResearchArea2Col}, {_DisallowedResearchArea3Col}, {_DisallowedComponentIdsCol}, {_AdditionalIntelligenceAgentsCol}, {_ConstructionSpeedFactorCol}, {_DefaultPrimaryColorCol}, {_DefaultSecondaryColorCol}, {_DefaultFlagDesignCol}, {_HomeSystemNameCol}, {_TroopStrengthCol}, {_TroopNameCol}, {_TroopNameArmoredCol}, {_TroopNamePlanetaryDefenseCol}, {_TroopNameSpecialForcesCol}, {_Resource1TypeCol}, {_Resource1EffectCol}, {_Resource1AmountCol}, {_Resource1AppliesOnlyToSourceCol}, {_Resource2TypeCol}, {_Resource2EffectCol}, {_Resource2AmountCol}, {_Resource2AppliesOnlyToSourceCol}, {_Resource3TypeCol}, {_Resource3EffectCol}, {_Resource3AmountCol}, {_Resource3AppliesOnlyToSourceCol}, {_Condition1TypeCol}, {_Condition1ValueCol}, {_Condition1ProportionCol}, {_Condition1AdditionalDataCol}, {_Condition2TypeCol}, {_Condition2ValueCol}, {_Condition2ProportionCol}, {_Condition2AdditionalDataCol}, {_Condition3TypeCol}, {_Condition3ValueCol}, {_Condition3ProportionCol}, {_Condition3AdditionalDataCol}, {_Condition4TypeCol}, {_Condition4ValueCol}, {_Condition4ProportionCol}, {_Condition4AdditionalDataCol}, {_Condition5TypeCol}, {_Condition5ValueCol}, {_Condition5ProportionCol}, {_Condition5AdditionalDataCol}, {_RaceEvent1TypeCol}, {_RaceEvent1FrequencyCol}, {_RaceEvent2TypeCol}, {_RaceEvent2FrequencyCol}, {_CharacterRandomAppearanceChanceLeaderCol}, {_CharacterRandomAppearanceChanceAmbassadorCol}, {_CharacterRandomAppearanceChanceGovernorCol}, {_CharacterRandomAppearanceChanceAdmiralCol}, {_CharacterRandomAppearanceChanceGeneralCol}, {_CharacterRandomAppearanceChanceScientistCol}, {_CharacterRandomAppearanceChanceIntelligenceAgentCol}, {_CharacterRandomAppearanceChanceShipCaptainCol}, {_CharacterRandomAppearanceChancePirateLeaderCol}, {_CharacterStartingTraitLeaderCol}, {_CharacterStartingTraitAmbassadorCol}, {_CharacterStartingTraitGovernorCol}, {_CharacterStartingTraitAdmiralCol}, {_CharacterStartingTraitGeneralCol}, {_CharacterStartingTraitScientistCol}, {_CharacterStartingTraitIntelligenceAgentCol}, {_CharacterStartingTraitShipCaptainCol}, {_CharacterStartingTraitPirateLeaderCol}, {_ResearchColonizationCostFactorContinentalCol}, {_ResearchColonizationCostFactorMarshySwampCol}, {_ResearchColonizationCostFactorOceanCol}, {_ResearchColonizationCostFactorDesertCol}, {_ResearchColonizationCostFactorIceCol}, {_ResearchColonizationCostFactorVolcanicCol}, {_ColonyConstructionSpeedFactorContinentalCol}, {_ColonyConstructionSpeedFactorMarshySwampCol}, {_ColonyConstructionSpeedFactorOceanCol}, {_ColonyConstructionSpeedFactorDesertCol}, {_ColonyConstructionSpeedFactorIceCol}, {_ColonyConstructionSpeedFactorVolcanicCol}, {_ColonyPopulationPolicyGrowthFactorExterminateCol}, {_ImmuneNaturalDisastersAtColonyTypeCol}, {_SpaceportArmorStrengthFactorCol}, {_KnownStartingGalacticHistoryLocationsCol}, {_TourismIncomeFactorCol}, {_FreeTradeIncomeFactorCol}, {_MigrationFactorCol}, {_TroopRegenerationFactorCol}, {_PirateDefaultPlaystyleCol}, {_DesignsPictureFamilyIndexPiratesCol}, {_ImmuneToPlaguesCol}) VALUES ({i}, '{values[i][1]}', {values[i][2]}, {values[i][3]}, {values[i][4]}, {values[i][5]}, {values[i][6]}, {values[i][7]}, {values[i][8]}, {values[i][9]}, {values[i][10]}, {values[i][11]}, {values[i][12]}, {values[i][13]}, {values[i][14]}, {values[i][15]}, {values[i][16]}, {values[i][17]}, {values[i][18]}, {values[i][19]}, {values[i][20]}, {values[i][21]}, {values[i][22]}, {values[i][23]}, {values[i][24]}, '{values[i][25]}', '{values[i][26]}', '{values[i][27]}', {values[i][28]}, {values[i][29]}, '{values[i][30]}', {canChangeGov}, {expanding}, {canBePirate}, {canBeNormalEmpire}, {playable}, {values[i][36]}, {values[i][37]}, {values[i][38]}, {values[i][39]}, {values[i][40]}, {values[i][41]}, {values[i][42]}, {values[i][43]}, {values[i][44]}, {values[i][45]}, {values[i][46]}, {values[i][47]}, '{values[i][48]}', {values[i][49]}, {values[i][50]}, {values[i][51]}, {values[i][52]}, {values[i][53]}, '{values[i][54]}', {values[i][55]}, '{values[i][56]}', '{values[i][57]}', '{values[i][58]}', '{values[i][59]}', {values[i][60]}, {values[i][61]}, {values[i][62]}, {resource1AppliesOnlyToSource.ToString()}, {values[i][64]}, {values[i][65]}, {values[i][66]}, {resource2AppliesOnlyToSource.ToString()}, {values[i][68]}, {values[i][69]}, {values[i][70]}, {resource3AppliesOnlyToSource.ToString()}, {values[i][72]}, {values[i][73]}, {values[i][74]}, {values[i][75]}, {values[i][76]}, {values[i][77]}, {values[i][78]}, {values[i][79]}, {values[i][80]}, {values[i][81]}, {values[i][82]}, {values[i][83]}, {values[i][84]}, {values[i][85]}, {values[i][86]}, {values[i][87]}, {values[i][88]}, {values[i][89]}, {values[i][90]}, {values[i][91]}, {values[i][92]}, {values[i][93]}, {values[i][94]}, {values[i][95]}, {values[i][96]}, {values[i][97]}, {values[i][98]}, {values[i][99]}, {values[i][100]}, {values[i][101]}, {values[i][102]}, {values[i][103]}, {values[i][104]}, {values[i][105]}, {values[i][106]}, {values[i][107]}, {values[i][108]}, {values[i][109]}, {values[i][110]}, {values[i][111]}, {values[i][112]}, {values[i][113]}, {values[i][114]}, {values[i][115]}, {values[i][116]}, {values[i][117]}, {values[i][118]}, {values[i][119]}, {values[i][120]}, {values[i][121]}, {values[i][122]}, {values[i][123]}, {values[i][124]}, {values[i][125]}, {values[i][126]}, {values[i][127]}, {values[i][128]}, {values[i][129]}, {values[i][130]}, {values[i][131]}, {values[i][132]}, {values[i][133]}, {values[i][134]}, {values[i][135]}, {immune.ToString()})";
                }
                root.Add(race);
            }
            doc.Save(fileStream);
        }

    }
}