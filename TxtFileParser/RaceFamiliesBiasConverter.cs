using System.Collections.Immutable;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace TxtFileParser
{
    internal class RaceFamiliesBiasConverter
    {
        private const string _tableName = "RaceFamilyBias";
        private const string _IdCol = "ID";
        private const string _RaceFamilyNameCol = "Name";
        private const string _BiasesCol = "Biases";
        private readonly ConvertType convertType;

        public RaceFamiliesBiasConverter(ConvertType convertType)
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
                List<string[]> agentFirstNames = new List<string[]>();
                while (!streamReader.EndOfStream)
                {
                    var temp = GetValidFileLine(streamReader).Replace(" ", "").Trim().Split(',');
                    agentFirstNames.Add(temp);
                    for (int i = 0; i < temp.Length; i++)
                    {
                        temp[i] = temp[i].Trim();
                    }
                }

                WriteXml(filePath, outputFolder, agentFirstNames);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"RaceFamiliesBias conversion error: {ex.Message}");
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
                var biases = string.Join(',', values[i].AsSpan(2));

                var race = new XElement("RaceFamilyBias");
                if (convertType == ConvertType.Update)
                {
                    race.Value = $"UPDATE {_tableName} SET {_RaceFamilyNameCol} = '{values[i][1]}', {_BiasesCol} = '{biases}' WHERE {_RaceFamilyNameCol} = '{values[i][1]}'";

                }
                else
                {
                    race.Value = $"INSERT INTO {_tableName} ({_IdCol}, {_RaceFamilyNameCol}, {_BiasesCol}) VALUES ({values[i][0]}, '{values[i][1]}', '{biases}')";
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