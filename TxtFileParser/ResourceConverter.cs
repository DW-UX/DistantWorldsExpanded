using System.Collections.Immutable;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace TxtFileParser
{
    internal class ResourceConverter
    {
        private const string _tableName = "Resources";
        private const string _IDCol = "ID";
        private const string _NameCol = "Name";
        private const string _PictureRefCol = "PictureRef";
        private const string _BasePriceCol = "BasePrice";
        private const string _TypeOfResourceCol = "TypeOfResource";
        private const string _SuperLuxuryBonusAmountCol = "SuperLuxuryBonusAmount";
        private const string _IsFuelCol = "IsFuel";
        private const string _IsImportantPreWarpResourceCol = "IsImportantPreWarpResource";
        private const string _ColonyGrowthResourceLevelCol = "ColonyGrowthResourceLevel";
        private const string _ColonyManufacturingLevelCol = "ColonyManufacturingLevel";
        private const string _DistributionlCol = "Distribution";
        private readonly ConvertType convertType;

        public ResourceConverter(ConvertType convertType)
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
                    var temp = GetValidFileLine(streamReader);
                    var values = temp.Split(',').Take(10).ToList();
                    for (int i = 0; i < values.Count; i++)
                    {
                        values[i] = values[i].Trim();
                    }
                    string distr = string.Join(",", temp.Split(',', StringSplitOptions.RemoveEmptyEntries).Skip(10).Select(x=>x.Trim()));
                    agentFirstNames.Add(values.Append(distr).ToArray());
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
                var race = new XElement("Resources");
                if (convertType == ConvertType.Update)
                {
                    race.Value = $"UPDATE {_tableName} SET {_IDCol} = {values[i][0]}, {_NameCol} = '{values[i][1]}', {_PictureRefCol} = {values[i][2]}, {_BasePriceCol} = {values[i][3]}, {_TypeOfResourceCol} = {values[i][4]}, {_SuperLuxuryBonusAmountCol} = {values[i][5]}, {_IsFuelCol} = {values[i][6]}, {_IsImportantPreWarpResourceCol} = {values[i][7]}, {_ColonyGrowthResourceLevelCol} = {values[i][8]}, {_ColonyManufacturingLevelCol} = {values[i][9]}, {_DistributionlCol} = '{values[i][10]}' WHERE {_NameCol} = '{values[i][1]}'";


                }
                else
                {
                    race.Value = $"INSERT INTO {_tableName} ({_IDCol}, {_NameCol}, {_PictureRefCol}, {_BasePriceCol}, {_TypeOfResourceCol}, {_SuperLuxuryBonusAmountCol}, {_IsFuelCol}, {_IsImportantPreWarpResourceCol}, {_ColonyGrowthResourceLevelCol}, {_ColonyManufacturingLevelCol}, {_DistributionlCol}) VALUES ({i}, {values[i][0]}, '{values[i][1]}', {values[i][2]}, {values[i][3]}, {values[i][4]}, {values[i][5]}, {values[i][6]}, {values[i][7]}, '{values[i][8]}', {values[i][9]}, '{values[i][10]}')";
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