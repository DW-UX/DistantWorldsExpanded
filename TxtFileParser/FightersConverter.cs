using System.Collections.Immutable;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace TxtFileParser
{
    internal class FightersConverter
    {
        private const string _tableName = "Fighters";
        private const string _IDCol = "ID";
        private const string _NameCol = "Name";
        private const string _TypeCol = "Type";
        private const string _TechLevelCol = "TechLevel";
        private const string _EnergyCapacityCol = "EnergyCapacity";
        private const string _EnergyRechargeRateCol = "EnergyRechargeRate";
        private const string _TopSpeedCol = "TopSpeed";
        private const string _TopSpeedEnergyConsumptionRateCol = "TopSpeedEnergyConsumptionRate";
        private const string _AccelerationRateCol = "AccelerationRate";
        private const string _TurnRateCol = "TurnRate";
        private const string _EngineExhaustImageIndexCol = "EngineExhaustImageIndex";
        private const string _ShieldsCapacityCol = "ShieldsCapacity";
        private const string _ShieldRechargeRateCol = "ShieldRechargeRate";
        private const string _DamageRepairRateCol = "DamageRepairRate";
        private const string _CountermeasureModifierCol = "CountermeasureModifier";
        private const string _TargettingModifierCol = "TargettingModifier";
        private const string _WeaponTypeCol = "WeaponType";
        private const string _WeaponImageIndexCol = "WeaponImageIndex";
        private const string _WeaponDamageCol = "WeaponDamage";
        private const string _WeaponRangeCol = "WeaponRange";
        private const string _WeaponEnergyRequiredCol = "WeaponEnergyRequired";
        private const string _WeaponSpeedCol = "WeaponSpeed";
        private const string _WeaponDamageLossCol = "WeaponDamageLoss";
        private const string _WeaponFireRateCol = "WeaponFireRate";
        private const string _WeaponSoundEffectFilenameCol = "WeaponSoundEffectFilename";
        private readonly ConvertType convertType;

        public FightersConverter(ConvertType convertType)
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
                    var temp = GetValidFileLine(streamReader).Trim().Split(',');
                    for (int i = 0; i < temp.Length; i++)
                    {
                        temp[i] = temp[i].Trim();
                    }
                    agentFirstNames.Add(temp);
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
                var race = new XElement("Fighters");
                if (convertType == ConvertType.Update)
                {
                    race.Value = $"UPDATE {_tableName} SET {_IDCol} = {values[i][0]}, {_NameCol} = '{values[i][1]}', {_TypeCol} = {values[i][2]}, {_TechLevelCol} = {values[i][3]}, {_EnergyCapacityCol} = {values[i][4]}, {_EnergyRechargeRateCol} = {values[i][5]}, {_TopSpeedCol} = {values[i][6]}, {_TopSpeedEnergyConsumptionRateCol} = {values[i][7]}, {_AccelerationRateCol} = {values[i][8]}, {_TurnRateCol} = {values[i][9]}, {_EngineExhaustImageIndexCol} = {values[i][10]}, {_ShieldsCapacityCol} = {values[i][11]}, {_ShieldRechargeRateCol} = {values[i][12]}, {_DamageRepairRateCol} = {values[i][13]}, {_CountermeasureModifierCol} = {values[i][14]}, {_TargettingModifierCol} = {values[i][15]}, {_WeaponTypeCol} = {values[i][16]}, {_WeaponImageIndexCol} = {values[i][17]}, {_WeaponDamageCol} = {values[i][18]}, {_WeaponRangeCol} = {values[i][19]}, {_WeaponEnergyRequiredCol} = {values[i][20]}, {_WeaponSpeedCol} = {values[i][21]}, {_WeaponDamageLossCol} = {values[i][22]}, {_WeaponFireRateCol} = {values[i][23]}, {_WeaponSoundEffectFilenameCol} = '{values[i][24]}' WHERE {_NameCol} = '{values[i][1]}'";
                }
                else
                {
                    race.Value = $"INSERT INTO {_tableName} ({_IDCol}, {_NameCol}, {_TypeCol}, {_TechLevelCol}, {_EnergyCapacityCol}, {_EnergyRechargeRateCol}, {_TopSpeedCol}, {_TopSpeedEnergyConsumptionRateCol}, {_AccelerationRateCol}, {_TurnRateCol}, {_EngineExhaustImageIndexCol}, {_ShieldsCapacityCol}, {_ShieldRechargeRateCol}, {_DamageRepairRateCol}, {_CountermeasureModifierCol}, {_TargettingModifierCol}, {_WeaponTypeCol}, {_WeaponImageIndexCol}, {_WeaponDamageCol}, {_WeaponRangeCol}, {_WeaponEnergyRequiredCol}, {_WeaponSpeedCol}, {_WeaponDamageLossCol}, {_WeaponFireRateCol}, {_WeaponSoundEffectFilenameCol}) VALUES ({i}, {values[i][0]}, '{values[i][1]}', {values[i][2]}, {values[i][3]}, {values[i][4]}, {values[i][5]}, {values[i][6]}, {values[i][7]}, '{values[i][8]}', {values[i][9]}, {values[i][10]}, {values[i][11]}, {values[i][12]}, {values[i][13]}, {values[i][14]}, {values[i][15]}, {values[i][16]}, {values[i][17]}, {values[i][18]}, {values[i][19]}, {values[i][20]}, {values[i][21]}, {values[i][22]}, {values[i][23]}, '{values[i][24]}')";
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