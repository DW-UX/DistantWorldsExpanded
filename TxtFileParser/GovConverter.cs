using System.Collections.Immutable;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace TxtFileParser
{
    internal class GovernmentConverter
    {
        private const string _tableName = "Governments";
        private const string _IDCol = "ID";
        private const string _NameCol = "Name";
        private const string _CorruptionCol = "Corruption";
        private const string _WarWearinessCol = "WarWeariness";
        private const string _MaintenanceCostsCol = "MaintenanceCosts";
        private const string _ApprovalRatingCol = "ApprovalRating";
        private const string _PopulationGrowthCol = "PopulationGrowth";
        private const string _ResearchSpeedCol = "ResearchSpeed";
        private const string _TroopRecruitmentCol = "TroopRecruitment";
        private const string _IncomeBonusCol = "IncomeBonus";
        private const string _LeaderReplacementLikelinessCol = "LeaderReplacementLikeliness";
        private const string _LeaderReplacementDisruptionLevelCol = "LeaderReplacementDisruptionLevel";
        private const string _LeaderReplacementBoostCol = "LeaderReplacementBoost";
        private const string _LeaderReplacementCharacterPoolCol = "LeaderReplacementCharacterPool";
        private const string _LeaderReplacementTypicalMannerCol = "LeaderReplacementTypicalManner";
        private const string _StabilityCol = "Stability";
        private const string _PopulationsConcernOverOwnReputationCol = "PopulationsConcernOverOwnReputation";
        private const string _ImportanceOfOtherEmpiresReputationsCol = "ImportanceOfOtherEmpiresReputations";
        private const string _SpecialFunctionCodeCol = "SpecialFunctionCode";
        private const string _AvailabilityCol = "Availability";
        private const string _EmpireNameAdjectivesCol = "EmpireNameAdjectives";
        private const string _EmpireNameNounsCol = "EmpireNameNouns";
        private readonly ConvertType convertType;

        public GovernmentConverter(ConvertType convertType)
        {
            this.convertType = convertType;
        }

        public bool Parse(string filePath, string outputFolder)
        {
            bool res = true;
            try
            {

                FileStream fileStream = File.OpenRead(filePath);
                StreamReader streamReader = new StreamReader(fileStream);
                List<string[]> agentFirstNames = new List<string[]>();
                while (!streamReader.EndOfStream)
                {
                    var temp = GetValidFileLine(streamReader).Replace(" ", "").Trim().Split(',');
                    for (int i = 0; i < temp.Length; i++)
                    {
                        temp[i] = temp[i].Trim();
                    }
                    var empAdjectives = string.Join(",", temp.Skip(20).Take(5));
                    var empNouns = string.Join(",", temp.Skip(20 + 5));
                    agentFirstNames.Add(temp.Take(20).Append(empAdjectives).Append(empNouns).ToArray());
                }

                WriteXml(filePath, outputFolder, agentFirstNames);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Character conversion error: {ex.Message}");
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

                var agent = new XElement("Governments");
                if (convertType == ConvertType.Update)
                {
                    agent.Value = $"UPDATE {_tableName} SET {_IDCol} = '{values[i][0]}', {_NameCol} = '{values[i][1]}', {_CorruptionCol} = {values[i][2]}, {_WarWearinessCol} = {values[i][3]}, {_MaintenanceCostsCol} = {values[i][4]}, {_ApprovalRatingCol} = {values[i][5]}, {_PopulationGrowthCol} = {values[i][6]}, {_ResearchSpeedCol} = {values[i][7]}, {_TroopRecruitmentCol} = {values[i][8]}, {_IncomeBonusCol} = {values[i][9]}, {_LeaderReplacementLikelinessCol} = {values[i][10]}, {_LeaderReplacementDisruptionLevelCol} = {values[i][11]}, {_LeaderReplacementBoostCol} = {values[i][12]}, {_LeaderReplacementCharacterPoolCol} = {values[i][13]}, {_LeaderReplacementTypicalMannerCol} = {values[i][14]}, {_StabilityCol} = {values[i][15]}, {_PopulationsConcernOverOwnReputationCol} = {values[i][16]}, {_ImportanceOfOtherEmpiresReputationsCol} = {values[i][17]}, {_SpecialFunctionCodeCol} = {values[i][18]}, {_AvailabilityCol} = {values[i][19]}, {_EmpireNameAdjectivesCol} = '{values[i][20]}', {_EmpireNameNounsCol} = '{values[i][21]}' WHERE {_NameCol} = '{values[i][1]}'";
                }
                else
                {
                    agent.Value = $"INSERT INTO {_tableName} ({_IDCol}, {_NameCol}, {_CorruptionCol}, {_WarWearinessCol}, {_MaintenanceCostsCol}, {_ApprovalRatingCol}, {_PopulationGrowthCol}, {_ResearchSpeedCol}, {_TroopRecruitmentCol}, {_IncomeBonusCol}, {_LeaderReplacementLikelinessCol}, {_LeaderReplacementDisruptionLevelCol}, {_LeaderReplacementBoostCol}, {_LeaderReplacementCharacterPoolCol}, {_LeaderReplacementTypicalMannerCol}, {_StabilityCol}, {_PopulationsConcernOverOwnReputationCol}, {_ImportanceOfOtherEmpiresReputationsCol}, {_SpecialFunctionCodeCol}, {_AvailabilityCol}, {_EmpireNameAdjectivesCol}, {_EmpireNameNounsCol}) VALUES ({values[i][0]} '{values[i][1]}', {values[i][2]}, {values[i][3]}, {values[i][4]}, {values[i][5]}, {values[i][6]}, {values[i][7]}, {values[i][8]}, {values[i][9]}, {values[i][10]}, {values[i][11]}, {values[i][12]}, {values[i][13]}, {values[i][14]}, {values[i][15]}, {values[i][16]}, {values[i][17]}, {values[i][18]}, {values[i][19]}, '{values[i][20]}', '{values[i][21]}')";
                }
                root.Add(agent);
            }
            doc.Save(fileStream);
        }

        private string GetValidFileLine(StreamReader reader)
        {
            string text = string.Empty;
            while (!reader.EndOfStream && (string.IsNullOrEmpty(text) || text.Trim() == string.Empty || text.Trim().Substring(0, 1) == "'"))
            {
                text = reader.ReadLine()?.Trim();
            }
            return text;
        }
    }
}