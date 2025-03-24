﻿using System.Collections.Immutable;

namespace TxtFileParser
{
    enum ConvertType
    {
        Insert,
        Update
    }
    internal class Program
    {
        private static readonly ImmutableList<string> _files = ImmutableList.Create<string>("characterNames.txt", "raceFamilies.txt", "raceFamilyBiases.txt", "plagues.txt", "races", "governments.txt", "GovernmentBiases.txt", "Facilities.txt", "Fighters.txt", "Resources.txt", "Research.txt", "RaceBiases.txt", "Components.txt");
        private const string _outputFolder = "Converted";
        static void Main(string[] args)
        {
            if(args.Length == 0)
            {
                Console.WriteLine("Please provide argument");
                Console.ReadLine();
                return;
            }
            else if (args.Length > 0 && (!args[0].Equals("insert", StringComparison.InvariantCultureIgnoreCase) || !args[0].Equals("update", StringComparison.InvariantCultureIgnoreCase)))
            {
                Console.WriteLine("Invalide argument");
                Console.ReadLine();
                return;
            }
            ConvertType convertType;
            if (args[0].Equals("insert", StringComparison.InvariantCultureIgnoreCase))
            {
                convertType = ConvertType.Insert;
            }
            else
            {
                convertType = ConvertType.Update;
            }


            if (!Directory.Exists(_outputFolder))
            {
                Directory.CreateDirectory(_outputFolder);
            }
            var charParser = new CharacterFileConverter(convertType);
            var raceFamParser = new RaceFamiliesConverter(convertType);
            var raceFamBiasParser = new RaceFamiliesBiasConverter(convertType);
            var plagueParser = new PlaguesConverter(convertType);
            var raceParser = new RaceConverter(convertType);
            var govParser = new GovernmentConverter(convertType);
            var govBaiasParser = new GovBiasConverter(convertType);
            var facilitiesParser = new FacilitiesConverter(convertType);
            var fightersParser = new FightersConverter(convertType);
            var fresourceParser = new ResourceConverter(convertType);
            var researchParser = new ResearchConverter(convertType);
            var raceBiasParser = new RaceBiasConverter(convertType);
            var componentParser = new ComponentsConverter(convertType);
            int i = 0;

            if (!charParser.Parse(_files[i++], _outputFolder) || !raceFamParser.Parse(_files[i++], _outputFolder) || !raceFamBiasParser.Parse(_files[i++], _outputFolder) || !plagueParser.Parse(_files[i++], _outputFolder) || !raceParser.Parse(_files[i++], _outputFolder) || !govParser.Parse(_files[i++], _outputFolder) || !govBaiasParser.Parse(_files[i++], _outputFolder) || !facilitiesParser.Parse(_files[i++], _outputFolder) || !fightersParser.Parse(_files[i++], _outputFolder) || !fresourceParser.Parse(_files[i++], _outputFolder) || !researchParser.Parse(_files[i++], _outputFolder) || !raceBiasParser.Parse(_files[i++], _outputFolder) || !componentParser.Parse(_files[i++], _outputFolder))
            {
                Console.WriteLine("Conversion failed");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Conversion successful");
                Console.ReadLine();
            }
        }
    }
}
