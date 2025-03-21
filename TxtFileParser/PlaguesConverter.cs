using System.Collections.Immutable;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace TxtFileParser
{
    internal class PlaguesConverter
    {
        private const string _tableName = "Plagues";
        private const string _IdCol = "ID";
        private const string _PlagueNameCol = "PlagueName";
        private const string _PictureRefCol = "PictureRef";
        private const string _MortalityRateCol = "MortalityRate";
        private const string _InfectionChanceCol = "InfectionChance";
        private const string _DurationCol = "Duration";
        private const string _NaturalOccurrenceLevelCol = "NaturalOccurrenceLevel";
        private const string _CanCompletelyEliminatePopulationCol = "CanCompletelyEliminatePopulation";
        private const string _ExceptionRaceNameCol = "ExceptionRaceName";
        private const string _ExceptionMortalityRateCol = "ExceptionMortalityRate";
        private const string _ExceptionInfectionChanceCol = "ExceptionInfectionChance";
        private const string _ExceptionDurationCol = "ExceptionDuration";
        private const string _SpecialFunctionCodeCol = "SpecialFunctionCode";
        private const string _DescriptionCol = "Description";
        private readonly ConvertType convertType;

        public PlaguesConverter(ConvertType convertType)
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
                    var temp = GetValidFileLine(streamReader).Trim();
                    var values = temp.Split(',', StringSplitOptions.RemoveEmptyEntries).Take(13).ToArray();
                    var desc = temp.Split(',', StringSplitOptions.RemoveEmptyEntries).Skip(13).Take(1).ToArray()[0].Trim().Replace('\'', '′');
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = values[i].Trim().Replace('\'', '′');
                    }

                    agentFirstNames.Add(values.Append(desc).ToArray());
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
                var race = new XElement("Plagues");
                if (convertType == ConvertType.Update)
                {
                    race.Value = $"UPDATE {_tableName} SET {_IdCol} = {values[i][0]}, {_PlagueNameCol} = '{values[i][1]}', {_PictureRefCol} = {values[i][2]}, {_MortalityRateCol} = {values[i][3]}, {_InfectionChanceCol} = {values[i][4]}, {_DurationCol} = {values[i][5]}, {_NaturalOccurrenceLevelCol} = {values[i][6]}, {_CanCompletelyEliminatePopulationCol} = {(values[i][7].Trim().ToUpperInvariant() == "Y" ? true : false)}, {_ExceptionRaceNameCol} = '{values[i][8]}', {_ExceptionMortalityRateCol} = {values[i][9]}, {_ExceptionInfectionChanceCol} = {values[i][10]}, {_ExceptionDurationCol} = {values[i][11]}, {_SpecialFunctionCodeCol} = {values[i][12]}, {_DescriptionCol} = '{values[i][13]}' WHERE {_PlagueNameCol} = '{values[i][1]}'";


                }
                else
                {
                    race.Value = $"INSERT INTO {_tableName} ({_IdCol}, {_PlagueNameCol}, {_PictureRefCol}, {_MortalityRateCol}, {_InfectionChanceCol}, {_DurationCol}, {_NaturalOccurrenceLevelCol}, {_CanCompletelyEliminatePopulationCol}, {_ExceptionRaceNameCol}, {_ExceptionMortalityRateCol}, {_ExceptionInfectionChanceCol}, {_ExceptionDurationCol}, {_SpecialFunctionCodeCol}, {_DescriptionCol}) VALUES ({values[i][0]}, '{values[i][1]}', {values[i][2]}, {values[i][3]}, {values[i][4]}, {values[i][5]}, {values[i][6]}, {(values[i][7].Trim().ToUpperInvariant() == "Y" ? true : false)}, '{values[i][8]}', {values[i][9]}, {values[i][10]}, {values[i][11]}, {values[i][12]}, '{values[i][13]}')";
                }
                root.Add(race);
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