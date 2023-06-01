using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionMod
{
    internal static class Helper
    {
        public const string _ModsRootFolder = "AdvMods";
        private const string _ExpansionModFolder = "ExpansionMod";
        private const string _ModConfigFile = "ModConfig.json";
        public static string GetModPath(string filePath)
        {
            return Path.Combine(_ModsRootFolder, _ExpansionModFolder, filePath);
        }
        public static string GetModConfigPath()
        {
            return Path.Combine(_ModsRootFolder, _ModConfigFile);
        }
    }
}
