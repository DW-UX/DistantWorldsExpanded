using System.Collections.Immutable;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace TxtFileParser
{
    internal class ResearchConverter
    {
        private const string _ProjIdCol = "ProjectID";
        private const string _ComponentIdCol = "ComponentID";

        private const string _researchTableName = "Research";

        private const string _IdCol = "ID";
        private const string _ProjectNameCol = "Name";
        private const string _TechLevelCol = "TechLevel";
        private const string _RowIdxCol = "RowIdx";
        private const string _IndustryCol = "Industry";
        private const string _CategoryCol = "Category";
        private const string _SpecialFunctionCodeCol = "SpecialFunctionCode";
        private const string _BaseCostMultiplierOverrideCol = "BaseCostMultiplierOverride";
        private const string _FacilityIdCol = "FacilityID";

        private const string _ResearchComponentsTableName = "ResearchComponents";

        private const string _ResearchComponentsImprovementsTableName = "ResearchComponentImprovements";
        private const string _Value1Col = "Value1";
        private const string _Value2Col = "Value2";
        private const string _Value3Col = "Value3";
        private const string _Value4Col = "Value4";
        private const string _Value5Col = "Value5";
        private const string _Value6Col = "Value6";
        private const string _Value7Col = "Value7";

        private const string _ResearchFightersTableName = "ResearchFighters";
        private const string _FighterIdCol = "FighterID";

        private const string _ResearchAbilitiesTableName = "ResearchAbilities";
        private const string _ResearchAbilityNameCol = "Name";
        private const string _ResearchTypeCol = "Type";
        private const string _ResearchLevelCol = "Level";
        private const string _ResearchValueCol = "Value";
        private const string _ResearchRelatedObjectIndexCol = "RelatedObjectIndex";

        private const string _ResearchPlagueChangeTableName = "ResearchPlagueChange";
        private const string _PlagueIdCol = "PlagueID";
        private const string _DescriptionCol = "Description";
        private const string _MortalityRateCol = "MortalityRate";
        private const string _InfectionChanceCol = "InfectionChance";
        private const string _DurationCol = "Duration";
        private const string _ExceptionMortalityRateCol = "ExceptionMortalityRate";
        private const string _ExceptionInfectionChanceCol = "ExceptionInfectionChance";
        private const string _ExceptionDurationCol = "ExceptionDuration";

        private const string _ResearchPAllowedRacesTableName = "ResearchAllowedRaces";
        private const string _RaceNameCol = "RaceName";

        private const string _ResearchParentTableName = "ResearchParent";
        private const string _ParentProjectIDCol = "ParentProjectID";
        private const string _RequiredParentCol = "RequiredParent";

        private readonly ConvertType convertType;

        public ResearchConverter(ConvertType convertType)
        {
            this.convertType = convertType;
        }

        public bool Parse(string filePath, string outputFolder)
        {
            bool res = true;
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File {filePath} not found");
                return true;
            }
            try
            {

                FileStream fileStream = File.OpenRead(filePath);
                StreamReader streamReader = new StreamReader(fileStream);
                bool projFound = false;
                List<string[]> curProj = new List<string[]>();
                List<List<string>> componentsIdList = new List<List<string>>();
                List<List<string>> componentsImproveList = new List<List<string>>();
                List<List<string>> fightersList = new List<List<string>>();
                List<List<string>> abilitiesList = new List<List<string>>();
                List<List<string>> plagueList = new List<List<string>>();
                List<List<string>> allowedRacesList = new List<List<string>>();
                List<List<string>> parentList = new List<List<string>>();
                while (!streamReader.EndOfStream)
                {
                    var text = streamReader.ReadLine().Trim().Replace('\'', '′');

                    if (!text.StartsWith('′'))
                    {
                        if (!string.IsNullOrWhiteSpace(text))
                        {
                            if (text.StartsWith("PROJECT"))
                            {
                                curProj.Add(new string[9]);
                                curProj[^1] = text.Split(';')[1].Split(',');
                                projFound = true;
                                curProj[^1][8] = (-1).ToString();
                            }
                            else if (text.StartsWith("COMPONENTS"))
                            {
                                foreach (var item in text.Split(';')[1].Split(',', StringSplitOptions.TrimEntries))
                                {
                                    componentsIdList.Add(new List<string>());
                                    componentsIdList[^1].Add(curProj[^1][0]);
                                    componentsIdList[^1].Add(item);
                                }
                            }
                            else if (text.Trim().StartsWith("COMPONENT IMPROVEMENTS"))
                            {
                                string[] txtArr = text.Split(';')[1].Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                                for (int i = 0; i < txtArr.Length; i++)
                                {
                                    if (i % 9 == 0)
                                    {
                                        componentsImproveList.Add(new List<string>());
                                        componentsImproveList[^1].Add(curProj[^1][0]);
                                    }
                                    componentsImproveList[^1].Add(txtArr[i]);
                                }
                            }
                            else if (text.StartsWith("FIGHTERS"))
                            {
                                foreach (var item in text.Split(';')[1].Split(',', StringSplitOptions.TrimEntries))
                                {
                                    fightersList.Add(new List<string>());
                                    fightersList[^1].Add(curProj[^1][0]);
                                    fightersList[^1].Add(item);
                                }
                            }
                            else if (text.StartsWith("FACILITY"))
                            {
                                curProj[^1][8] = text.Split(';')[1].Trim();
                            }
                            else if (text.Trim().StartsWith("ABILITIES"))
                            {
                                string[] txtArr = text.Split(';')[1].Split(',', StringSplitOptions.TrimEntries);
                                for (int i = 0; i < txtArr.Length; i++)
                                {
                                    if (i % 5 == 0)
                                    {
                                        abilitiesList.Add(new List<string>());
                                        abilitiesList[^1].Add(curProj[^1][0]);
                                    }
                                    abilitiesList[^1].AddRange(txtArr[i]);
                                }
                            }
                            else if (text.StartsWith("PLAGUE CHANGE"))
                            {
                                string[] txtArr = text.Split(';')[1].Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                                for (int i = 0; i < txtArr.Length; i++)
                                {
                                    if (i % 8 == 0)
                                    {
                                        plagueList.Add(new List<string>());
                                        plagueList[^1].Add(curProj[^1][0]);
                                    }
                                    plagueList[^1].AddRange(txtArr[i]);
                                }
                            }
                            else if (text.Trim().StartsWith("ALLOWED RACES"))
                            {
                                foreach (var item in text.Split(';')[1].Split(',', StringSplitOptions.TrimEntries))
                                {
                                    allowedRacesList.Add(new List<string>());
                                    allowedRacesList[^1].Add(curProj[^1][0]);
                                    allowedRacesList[^1].Add(item);
                                }
                            }
                            else if (text.Trim().StartsWith("PARENTS"))
                            {
                                string[] txtArr = text.Split(';')[1].Split(',', StringSplitOptions.TrimEntries);
                                for (int i = 0; i < txtArr.Length; i += 2)
                                {
                                    if (i % 2 == 0)
                                    {
                                        parentList.Add(new List<string>());
                                        parentList[^1].Add(curProj[^1][0]);
                                    }
                                    parentList[^1].Add(txtArr[i]);
                                    parentList[^1].Add(Helper.ParseBool(txtArr[i + 1]).ToString());
                                }
                            }
                        }
                        else if (projFound)
                        {
                            projFound = false;
                        }
                    }
                }

                WriteXml(filePath, outputFolder, curProj);
                WriteComponentsXml(filePath, outputFolder, componentsIdList);
                WriteComponentImprovementsXml(filePath, outputFolder, componentsImproveList);
                WriteFightersXml(filePath, outputFolder, fightersList);
                WriteAbilitiesXml(filePath, outputFolder, abilitiesList);
                WritePlagueXml(filePath, outputFolder, plagueList);
                WriteAllowedRacesXml(filePath, outputFolder, allowedRacesList);
                WriteParentsXml(filePath, outputFolder, parentList);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Research conversion error: {ex.Message}");
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
                var race = new XElement("Research");
                if (convertType == ConvertType.Update)
                {
                    race.Value = $"UPDATE {_researchTableName} SET {_IdCol} = {values[i][0]}, {_ProjectNameCol} = '{values[i][1]}', {_TechLevelCol} = {values[i][2]}, {_RowIdxCol} = {values[i][3]}, {_IndustryCol} = {values[i][4]}, {_CategoryCol} = {values[i][5]}, {_SpecialFunctionCodeCol} = {values[i][6]}, {_BaseCostMultiplierOverrideCol} = {values[i][7]}, {_FacilityIdCol} = {values[i][8]} WHERE {_ProjectNameCol} = {values[i][1]}";
                }
                else
                {
                    race.Value = $"INSERT INTO {_researchTableName} ({_IdCol}, {_ProjectNameCol}, {_TechLevelCol}, {_RowIdxCol}, {_IndustryCol}, {_CategoryCol}, {_SpecialFunctionCodeCol}, {_BaseCostMultiplierOverrideCol}, {_FacilityIdCol}) VALUES ({values[i][0]}, '{values[i][1]}', {values[i][2]}, {values[i][3]}, {values[i][4]}, {values[i][5]}, {values[i][6]}, {values[i][7]}, {values[i][8]})";
                }
                root.Add(race);
            }
            doc.Save(fileStream);
        }

        private void WriteComponentsXml(string filePath, string outputFolder, List<List<string>> values)
        {
            string xmlFilePath = Path.Combine(outputFolder, "ResearchComponents.xml");
            using FileStream fileStream = new FileStream(xmlFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            fileStream.SetLength(0);
            XDocument doc = new XDocument();
            var root = new XElement("root");
            doc.Add(root);

            foreach (var value in values)
            {
                var component = new XElement("ResearchComponent");
                if (convertType == ConvertType.Update)
                {
                    component.Value = $"UPDATE {_ResearchComponentsTableName} SET {_ComponentIdCol} = {value[1]} WHERE {_ProjIdCol} = {value[0]} AND  {_ComponentIdCol} = {value[1]}";
                }
                else
                {
                    component.Value = $"INSERT INTO {_ResearchComponentsTableName} ({_ProjIdCol}, {_ComponentIdCol}) VALUES ({value[0]}, {value[1]})";
                }
                root.Add(component);
            }
            doc.Save(fileStream);
        }

        private void WriteComponentImprovementsXml(string filePath, string outputFolder, List<List<string>> values)
        {
            string xmlFilePath = Path.Combine(outputFolder, "ResearchImprovements.xml");
            using FileStream fileStream = new FileStream(xmlFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            fileStream.SetLength(0);
            XDocument doc = new XDocument();
            var root = new XElement("root");
            doc.Add(root);

            foreach (var value in values)
            {
                var improvement = new XElement("ComponentImprovement");
                if (convertType == ConvertType.Update)
                {
                    improvement.Value = $"UPDATE {_ResearchComponentsImprovementsTableName} SET {_ProjIdCol} = {value[0]}, {_ComponentIdCol} = {value[1]},{_TechLevelCol} = {value[2]}, {_Value1Col} = {value[3]}, {_Value2Col} = {value[4]}, {_Value3Col} = {value[5]}, {_Value4Col} = {value[6]}, {_Value5Col} = {value[7]}, {_Value6Col} = {value[8]}, {_Value7Col} = {value[9]} WHERE {_ProjIdCol} = {value[0]} AND {_ComponentIdCol} = {value[1]}";
                }
                else
                {
                    improvement.Value = $"INSERT INTO {_ResearchComponentsImprovementsTableName} ({_ProjIdCol}, {_ComponentIdCol}, {_TechLevelCol}, {_Value1Col}, {_Value2Col}, {_Value3Col}, {_Value4Col}, {_Value5Col}, {_Value6Col}, {_Value7Col}) VALUES ({value[0]}, {value[1]}, {value[2]}, {value[3]}, {value[4]}, {value[5]}, {value[6]}, {value[7]}, {value[8]}, {value[9]})";
                }
                root.Add(improvement);
            }
            doc.Save(fileStream);
        }

        private void WriteFightersXml(string filePath, string outputFolder, List<List<string>> values)
        {
            string xmlFilePath = Path.Combine(outputFolder, "ResearchFighters.xml");
            using FileStream fileStream = new FileStream(xmlFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            fileStream.SetLength(0);
            XDocument doc = new XDocument();
            var root = new XElement("root");
            doc.Add(root);

            foreach (var value in values)
            {
                var component = new XElement("ResearchFighter");
                if (convertType == ConvertType.Update)
                {
                    component.Value = $"UPDATE {_ResearchFightersTableName} SET {_FighterIdCol} = {value[1]} WHERE {_ProjIdCol} = {value[0]} AND  {_FighterIdCol} = {value[1]}";

                }
                else
                {
                    component.Value = $"INSERT INTO {_ResearchFightersTableName} ({_ProjIdCol}, {_FighterIdCol}) VALUES ({value[0]}, {value[1]})";
                }
                root.Add(component);
            }
            doc.Save(fileStream);
        }

        private void WriteAbilitiesXml(string filePath, string outputFolder, List<List<string>> values)
        {
            string xmlFilePath = Path.Combine(outputFolder, "ResearchAbilities.xml");
            using FileStream fileStream = new FileStream(xmlFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            fileStream.SetLength(0);
            XDocument doc = new XDocument();
            var root = new XElement("root");
            doc.Add(root);

            foreach (var value in values)
            {
                var ability = new XElement("Ability");
                if (convertType == ConvertType.Update)
                {
                    ability.Value = $"UPDATE {_ResearchAbilitiesTableName} SET {_ProjIdCol} = {value[0]}, {_ResearchAbilityNameCol} = '{value[1]}', {_ResearchTypeCol} = {value[2]}, {_ResearchLevelCol} = {value[3]}, {_ResearchValueCol} = {value[4]}, {_ResearchRelatedObjectIndexCol} = {value[5]} WHERE {_ProjIdCol} = {value[0]}";
                }
                else
                {
                    ability.Value = $"INSERT INTO {_ResearchAbilitiesTableName} " +
                        $"({_ProjIdCol}, {_ResearchAbilityNameCol}, {_ResearchTypeCol}, {_ResearchLevelCol}, {_ResearchValueCol}, {_ResearchRelatedObjectIndexCol}) " +
                        $"VALUES ({value[0]}, '{value[1]}', {value[2]}, {value[3]}, {value[4]}, {value[5]})";
                }
                root.Add(ability);
            }
            doc.Save(fileStream);
        }

        private void WritePlagueXml(string filePath, string outputFolder, List<List<string>> values)
        {
            string xmlFilePath = Path.Combine(outputFolder, "ResearchPlague.xml");
            using FileStream fileStream = new FileStream(xmlFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            fileStream.SetLength(0);
            XDocument doc = new XDocument();
            var root = new XElement("root");
            doc.Add(root);

            foreach (var value in values)
            {
                var plague = new XElement("Plague");
                if (convertType == ConvertType.Update)
                {
                    plague.Value = $"UPDATE {_ResearchPlagueChangeTableName} SET {_ProjIdCol} = {value[0]}, {_PlagueIdCol} = {value[1]}, {_DescriptionCol} = '{value[2]}', {_MortalityRateCol} = {value[3]}, {_InfectionChanceCol} = {value[4]}, {_DurationCol} = {value[5]}, {_ExceptionMortalityRateCol} = {value[6]}, {_ExceptionInfectionChanceCol} = {value[7]}, {_ExceptionDurationCol} = {value[8]} WHERE {_ProjIdCol} = {value[0]} AND {_PlagueIdCol} = {value[1]}";
                }
                else
                {
                    plague.Value = $"INSERT INTO {_ResearchPlagueChangeTableName} ({_ProjIdCol}, {_PlagueIdCol}, {_DescriptionCol}, {_MortalityRateCol}, {_InfectionChanceCol}, {_DurationCol}, {_ExceptionMortalityRateCol}, {_ExceptionInfectionChanceCol}, {_ExceptionDurationCol}) VALUES ({value[0]}, {value[1]}, '{value[2]}', {value[3]}, {value[4]}, {value[5]}, {value[6]}, {value[7]}, {value[8]})";
                }
                root.Add(plague);
            }
            doc.Save(fileStream);
        }

        private void WriteAllowedRacesXml(string filePath, string outputFolder, List<List<string>> values)
        {
            string xmlFilePath = Path.Combine(outputFolder, "ResearchAllowedRace.xml");
            using FileStream fileStream = new FileStream(xmlFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            fileStream.SetLength(0);
            XDocument doc = new XDocument();
            var root = new XElement("root");
            doc.Add(root);

            foreach (var value in values)
            {
                var allowedRace = new XElement("AllowedRace");
                if (convertType == ConvertType.Update)
                {
                    allowedRace.Value = $"UPDATE {_ResearchPAllowedRacesTableName} SET {_RaceNameCol} = '{value[1]}' WHERE {_ProjIdCol} = {value[0]} AND  {_RaceNameCol} = {value[1]}";
                }
                else
                {
                    allowedRace.Value = $"INSERT INTO {_ResearchPAllowedRacesTableName} ({_ProjIdCol}, {_RaceNameCol}) VALUES ({value[0]}, '{value[1]}')";
                }
                root.Add(allowedRace);
            }
            doc.Save(fileStream);
        }
        private void WriteParentsXml(string filePath, string outputFolder, List<List<string>> values)
        {
            string xmlFilePath = Path.Combine(outputFolder, "ResearchParent.xml");
            using FileStream fileStream = new FileStream(xmlFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            fileStream.SetLength(0);
            XDocument doc = new XDocument();
            var root = new XElement("root");
            doc.Add(root);

            foreach (var value in values)
            {
                var component = new XElement("ResearchComponent");
                if (convertType == ConvertType.Update)
                {
                    component.Value = $"UPDATE {_ResearchParentTableName} SET {_ParentProjectIDCol} = {value[1]}, {_RequiredParentCol} = {value[2]} WHERE {_ProjIdCol} = {value[0]} AND  {_ParentProjectIDCol} = {value[1]}";
                }
                else
                {
                    component.Value = $"INSERT INTO {_ResearchParentTableName} ({_ProjIdCol}, {_ParentProjectIDCol},{_RequiredParentCol}) VALUES ({value[0]}, {value[1]}, {value[2]})";
                }
                root.Add(component);
            }
            doc.Save(fileStream);
        }
    }
}