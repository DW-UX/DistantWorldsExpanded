using System.Collections.Immutable;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace TxtFileParser
{
    internal class ResearchConverter
    {
        private const string _tableName = "Research";
        private const string _IdCol = "ID";
        private const string _ProjectCol = "Project";
        private const string _ComponentsCol = "Components";
        private const string _ComponentImprovementsCol = "ComponentImprovements";
        private const string _FightersCol = "Fighters";
        private const string _FacilityCol = "Facility";
        private const string _AbilitieslCol = "Abilities";
        private const string _PlagueChangeCol = "PlagueChange";
        private const string _AllowedRacesCol = "AllowedRaces";
        private const string _ParentseCol = "Parents";
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
                List<string[]> agentFirstNames = new List<string[]>();
                bool projFound = false;
                string[] curProj = new string[10] { "", "", "", "", "", "", "", "", "", "" };
                while (!streamReader.EndOfStream)
                {
                    var text = streamReader.ReadLine();

                    if (!text.StartsWith('\''))
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            if (text.Trim().StartsWith("PROJECT"))
                            {
                                curProj[1] = text.Split(';')[1].Trim().Replace('\'', '′');
                                curProj[0] = curProj[1].Split(',')[0].Trim().Replace('\'', '′');
                                projFound = true;
                            }
                            else if (text.Trim().StartsWith("COMPONENTS".Replace('\'', '′')))
                            {
                                curProj[2] = text.Split(';')[1].Trim().Replace('\'', '′');
                            }
                            else if (text.Trim().StartsWith("COMPONENT IMPROVEMENTS"))
                            {
                                curProj[3] = text.Split(';')[1].Trim().Replace('\'', '′');
                            }
                            else if (text.Trim().StartsWith("FIGHTERS".Replace('\'', '′')))
                            {
                                curProj[4] = text.Split(';')[1].Trim().Replace('\'', '′');
                            }
                            else if (text.Trim().StartsWith("FACILITY"))
                            {
                                curProj[5] = text.Split(';')[1].Trim().Replace('\'', '′');
                            }
                            else if (text.Trim().StartsWith("ABILITIES"))
                            {
                                curProj[6] = text.Split(';')[1].Trim().Replace('\'', '′');
                            }
                            else if (text.Trim().StartsWith("PLAGUE CHANGE"))
                            {
                                curProj[7] = text.Split(';')[1].Trim().Replace('\'', '′');
                            }
                            else if (text.Trim().StartsWith("ALLOWED RACES"))
                            {
                                curProj[8] = text.Split(';')[1].Trim().Replace('\'', '′');
                            }
                            else if (text.Trim().StartsWith("PARENTS"))
                            {
                                curProj[9] = text.Split(';')[1].Trim().Replace('\'', '′');
                            }
                        }
                        else if (projFound)
                        {
                            projFound = false;
                            agentFirstNames.Add(curProj);
                            curProj = new string[10] { "", "", "", "", "", "", "", "", "", "" };
                        }
                    }
                }
                if (projFound)
                {
                    agentFirstNames.Add(curProj);
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
                var race = new XElement("Research");
                if (convertType == ConvertType.Update)
                {
                    race.Value = $"UPDATE {_tableName} SET {_IdCol} = '{values[i][0]}', {_ProjectCol} = '{values[i][1]}', {_ComponentsCol} = '{values[i][2]}', {_ComponentImprovementsCol} = '{values[i][3]}', {_FightersCol} = '{values[i][4]}', {_FacilityCol} = '{values[i][5]}', {_AbilitieslCol} = '{values[i][6]}', {_PlagueChangeCol} = '{values[i][7]}', {_AllowedRacesCol} = '{values[i][8]}', {_ParentseCol} = '{values[i][9]}' WHERE {_IdCol} = {values[i][0]}";
                }
                else
                {
                    race.Value = $"INSERT INTO {_tableName} ({_IdCol}, {_ProjectCol}, {_ComponentsCol}, {_ComponentImprovementsCol}, {_FightersCol}, {_FacilityCol}, {_AbilitieslCol}, {_PlagueChangeCol}, {_AllowedRacesCol}, {_ParentseCol}) VALUES ({values[i][0]}, '{values[i][1]}', '{values[i][2]}', '{values[i][3]}', '{values[i][4]}', '{values[i][5]}', '{values[i][6]}', '{values[i][7]}', '{values[i][8]}', '{values[i][9]}')";
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