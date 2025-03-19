using System.Collections.Immutable;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace TxtFileParser
{
    internal class CharacterFileConverter
    {
        private const string _tableName = "AgentNames";
        private const string _RaceFamilieCol = "RaceFamilie";
        private const string _RaceIdCol = "RaceId";
        private const string _FirstNameCol = "FirstName";
        private const string _LastNameCol = "LastName";
        private readonly ConvertType convertType;

        public CharacterFileConverter(ConvertType convertType)
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
                List<string[]> agentLastNames = new List<string[]>();
                while (!streamReader.EndOfStream)
                {
                    var temp1 = GetValidFileLine(streamReader).Replace(" ", "").Trim().Split(',');
                    var temp2 = GetValidFileLine(streamReader).Replace(" ", "").Trim().Split(',');
                    agentFirstNames.Add(temp1);
                    agentLastNames.Add(temp2);

                    for (int i = 0; i < temp1.Length; i++)
                    {
                        temp1[i] = temp1[i].Trim();
                    }
                    for (int i = 0; i < temp2.Length; i++)
                    {
                        temp2[i] = temp2[i].Trim();
                    }
                }

                WriteXml(filePath, outputFolder, agentFirstNames, agentLastNames);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Character conversion error: {ex.Message}");
                res = false;
            }
            return res;
        }
        private void WriteXml(string filePath, string outputFolder, List<string[]> agentFirstNames, List<string[]> agentLastNames)
        {
            string xmlFilePath = Path.Combine(outputFolder, Path.ChangeExtension(filePath, ".xml"));
            using FileStream fileStream = new FileStream(xmlFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            fileStream.SetLength(0);
            XDocument doc = new XDocument();
            var root = new XElement("root");
            doc.Add(root);
            for (int i = 0; i < agentFirstNames.Count; i++)
            {
                var firstName = string.Join(',', agentFirstNames[i]);
                var lastName = string.Join(',', agentLastNames[i]);

                var agent = new XElement("Agent");
                if (convertType == ConvertType.Update)
                {
                    agent.Value = $"UPDATE {_tableName} SET {_FirstNameCol} = '{firstName}', {_LastNameCol} = '{lastName}' WHERE {_RaceFamilieCol} = '{i}'";
                }
                else
                {
                    agent.Value = $"INSERT INTO {_tableName} ({_FirstNameCol}, {_LastNameCol}, {_RaceFamilieCol}, {_RaceIdCol}) VALUES ('{firstName}', '{lastName}', {i}, {i})";
                }
                root.Add(agent);
                doc.Save(fileStream);
            }
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