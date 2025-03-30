using System.Collections.Immutable;
using System.Linq;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace TxtFileParser
{
    internal class FacilitiesConverter
    {
        private const string _tableName = "Facilities"; 
        private const string _IdCol = "ID";
        private const string _NameCol = "Name";
        private const string _TypeCol = "Type";
        private const string _WonderTypeCol = "WonderType";
        private const string _PictureRefCol = "PictureRef";
        private const string _BuildCostCol = "BuildCost";
        private const string _MaintenanceCostCol = "MaintenanceCost";
        private const string _Value1Col = "Value1";
        private const string _Value2Col = "Value2";
        private const string _Value3Col = "Value3";
        private const string _Description = "Description";
        private readonly ConvertType convertType;

        public FacilitiesConverter(ConvertType convertType)
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
                    var temp = GetValidFileLine(streamReader).Replace('\'', '′');
                    var values = temp.Split(",").Take(10).ToArray();
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = values[i].Trim();
                    }
                    var description = string.Join(",", temp.Split(",").Skip(10)).Trim();
                    agentFirstNames.Add(values.Append(description).ToArray());;
                }


                WriteXml(filePath, outputFolder, agentFirstNames);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Facility conversion error: {ex.Message}");
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
                var race = new XElement("Facility");
                if (convertType == ConvertType.Update)
                {
                    race.Value = $"UPDATE {_tableName} SET {_IdCol} = {values[i][0]}, {_NameCol} = '{values[i][1]}', {_TypeCol} = {values[i][2]}, {_WonderTypeCol} = {values[i][3]}, {_PictureRefCol} = {values[i][4]}, {_BuildCostCol} = {values[i][5]}, {_MaintenanceCostCol} = {values[i][6]}, {_Value1Col} = {values[i][7]}, {_Value2Col} = {values[i][8]}, {_Value3Col} = {values[i][9]}, {_Description} = '{values[i][10]}' WHERE {_NameCol} = {values[i][1]}";
                }
                else
                {
                    race.Value = $"INSERT INTO {_tableName} ({_IdCol}, {_NameCol}, {_TypeCol}, {_WonderTypeCol}, {_PictureRefCol}, {_BuildCostCol}, {_MaintenanceCostCol}, {_Value1Col}, {_Value2Col}, {_Value3Col}, {_Description}) VALUES ({values[i][0]}, '{values[i][1]}', {values[i][2]}, {values[i][3]}, {values[i][4]}, {values[i][5]}, {values[i][6]}, {values[i][7]}, {values[i][8]}, {values[i][9]}, '{values[i][10]}')";
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