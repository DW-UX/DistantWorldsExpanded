using DistantWorlds.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistantWorlds.BaconDistantWorlds.DTO
{
    public class ShipPictureInfo
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string Family { get; set; }
        public int FamilyIdx { get; set; }
        public ShipPictureInfoType Type { get; set; }

        public ShipPictureInfo(string filePath, string fileName, string family, int familyIdx, ShipPictureInfoType type)
        {
            FilePath = filePath;
            FileName = fileName;
            Family = family;
            FamilyIdx = familyIdx;
            Type = type;
        }

        public static List<ShipPictureInfo> CreateShipPictureInfoFromPath(List<string> filePaths)
        {
            List<ShipPictureInfo> shipPictureInfos = new List<ShipPictureInfo>();
            foreach (string filePath in filePaths)
            {
                DirectoryInfo family = new DirectoryInfo(Path.GetDirectoryName(filePath));
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                ShipPictureInfoType type;
                DirectoryInfo temp = family.Parent;
                bool found = false;
                while (true)
                {
                    if (string.Equals(temp.Name, "Ships", StringComparison.InvariantCultureIgnoreCase))
                    {
                        type = ShipPictureInfoType.Family;
                        found = true;
                        break;
                    }
                    else if (string.Equals(temp.Name, "Other", StringComparison.InvariantCultureIgnoreCase))
                    {
                        type = ShipPictureInfoType.Other;
                        found = true;
                        break;
                    }
                    else if (string.Equals(temp.Name, "MajorSets", StringComparison.InvariantCultureIgnoreCase))
                    {
                        type = ShipPictureInfoType.Major;
                        found = true;
                        break;
                    }
                    else if (string.Equals(temp.Name, "MinorSets", StringComparison.InvariantCultureIgnoreCase))
                    {
                        type = ShipPictureInfoType.Minor;
                        found = true;
                        break;
                    }
                    temp = temp.Parent;
                }
                if (!found)
                {
                    { throw new InvalidOperationException($"Unknown ship picture type. Parent folders are not Ships, Other, Major or Minor: {filePath}"); }
                }
                int familyIdx = -1;
                if (family.Name[family.Name.Length-1] >= '0' && family.Name[family.Name.Length-1] <= '9')
                    familyIdx = int.Parse(family.Name.Substring("family".Length));
                shipPictureInfos.Add(new ShipPictureInfo(filePath, fileName, family.Name, familyIdx, type));
            }
            return shipPictureInfos;
        }
    }
}
