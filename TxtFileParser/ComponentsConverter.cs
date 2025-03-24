using System.Collections.Immutable;
using System.Linq;
using System.Net.Http.Headers;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace TxtFileParser
{
    internal class ComponentsConverter
    {
        private const string _tableName = "Components";
        private const string _IdCol = "ID";
        private const string _NameCol = "Name";
        private const string _TypeCol = "Type";
        private const string _SpecialImageIndexCol = "SpecialImageIndex";
        private const string _PictureRefCol = "PictureRef";
        private const string _SizeCol = "Size";
        private const string _StaticEnergyUsedCol = "StaticEnergyUsed";
        private const string _SoundEffectFilenameCol = "SoundEffectFilename";
        private const string _Value1Col = "Value1";
        private const string _Value2Col = "Value2";
        private const string _Value3Col = "Value3";
        private const string _Value4Col = "Value4";
        private const string _Value5Col = "Value5";
        private const string _Value6Col = "Value6";
        private const string _Value7Col = "Value7";
        private const string _ResourceRequiredCol = "ResourceRequired";
        private readonly ConvertType convertType;

        public ComponentsConverter(ConvertType convertType)
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
                    var values = temp.Split(",",  StringSplitOptions.TrimEntries).Take(15);
                    List<string> resReq = new List<string>();
                    foreach (var value in temp.Split(",", StringSplitOptions.TrimEntries).Skip(15))
                    {
                        if(!string.IsNullOrWhiteSpace(value))
                            resReq.Add(value);
                    }
                    agentFirstNames.Add(values.Append(string.Join(',', resReq)).ToArray());
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
                var component = new XElement("Component");
                if (convertType == ConvertType.Update)
                {
                    component.Value = $"UPDATE {_tableName} SET {_IdCol} = {values[i][0]}, {_NameCol} = '{values[i][1]}', {_PictureRefCol} = {values[i][2]}, {_SpecialImageIndexCol} = {values[i][3]}, {_SoundEffectFilenameCol} = '{values[i][4]}', {_TypeCol} = {values[i][5]}, {_SizeCol} = {values[i][6]}, {_StaticEnergyUsedCol} = {values[i][7]}, {_Value1Col} = {values[i][8]}, {_Value2Col} = {values[i][9]}, {_Value3Col} = {values[i][10]}, {_Value4Col} = {values[i][11]}, {_Value5Col} = {values[i][12]}, {_Value6Col} = {values[i][13]}, {_Value7Col} = {values[i][14]}, {_ResourceRequiredCol} = '{values[i][15]}' WHERE {_NameCol} = '{values[i][1]}'";
                }
                else
                {
                    component.Value = $"INSERT INTO {_tableName} ({_IdCol}, {_NameCol}, {_PictureRefCol}, {_SpecialImageIndexCol}, {_SoundEffectFilenameCol}, {_TypeCol}, {_SizeCol}, {_StaticEnergyUsedCol}, {_Value1Col}, {_Value2Col}, {_Value3Col}, {_Value4Col}, {_Value5Col}, {_Value6Col}, {_Value7Col}, {_ResourceRequiredCol}) VALUES ({values[i][0]}, '{values[i][1]}', {values[i][2]}, {values[i][3]}, '{values[i][4]}', {values[i][5]}, {values[i][6]}, {values[i][7]}, {values[i][8]}, {values[i][9]}, {values[i][10]}, {values[i][11]}, {values[i][12]}, {values[i][13]}, {values[i][14]}, '{values[i][15]}')";
                }
                root.Add(component);
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