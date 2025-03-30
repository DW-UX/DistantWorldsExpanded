using System.Collections.Immutable;
using System.Data.SQLite;
using System.Threading.Channels;
using System.Xml.Linq;

namespace Patcher
{
    internal class Program
    {
        private static readonly ImmutableList<string> _Files = ImmutableList.Create<string>("characterNames.xml", "raceFamilies.xml", "raceFamilyBiases.xml", "plagues.xml", "races.xml", "governments.xml", "GovernmentBiases.xml", "Facilities.xml", "Fighters.xml", "Resources.xml", "Research.xml", "RaceBiases.xml", "Components.xml", "Policy.xml", "ComponentResourceRequired.xml", "ResearchAbilities.xml", "ResearchAllowedRace.xml", "ResearchComponents.xml", "ResearchImprovements.xml", "ResearchParent.xml", "ResearchPlague.xml", "ResearchFighters.xml");

        private static SQLiteConnection inputFile;
        private static SQLiteConnection outputFile;

        static void Main(string[] args)
        {
            string fullPath = Path.GetFullPath(args[0]);
            if (!File.Exists(args[0]))
            {
                throw new FileNotFoundException("Database file not found at path: " + args[0]);
            }
            inputFile = new SQLiteConnection($"Data Source='{args[0]}'");
            outputFile = new SQLiteConnection($"Data Source='{args[1]}'");
            inputFile.Open();
            if (File.Exists(args[1]))
                File.Delete(args[1]);
            using SQLiteCommand commandVac = new SQLiteCommand($"VACUUM INTO '{args[1]}'", inputFile);
            commandVac.ExecuteNonQuery();
            inputFile.Close();
            outputFile.Open();
            var changes = ParseChanges(args[2]);

            using SQLiteTransaction transaction = outputFile.BeginTransaction();
            try
            {
                foreach (var change in changes)
                {
                    using SQLiteCommand command = new SQLiteCommand(change, outputFile);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                transaction.Rollback();
                Console.ReadLine();
            }
            finally
            {
                transaction.Commit();
            }
        }

        private static List<string> ParseChanges(string modFolder)
        {
            var changes = new List<string>();
            DirectoryInfo directory = new DirectoryInfo(modFolder);
            var files = directory.GetFiles("*.xml", SearchOption.TopDirectoryOnly);
            foreach (var file in files.Where(x => _Files.Any(y => y.Equals(x.Name, StringComparison.InvariantCultureIgnoreCase))))
            {
                XDocument doc = XDocument.Load(file.FullName);
                changes.AddRange(doc.Root.Descendants().Select(x => x.Value).ToList());
            }
            foreach (var file in files.Where(x => !_Files.Any(y => y.Equals(x.Name, StringComparison.InvariantCultureIgnoreCase))))
            {
                XDocument doc = XDocument.Load(file.FullName);
                changes.AddRange(doc.Root.Descendants().Select(x => x.Value).ToList());
            }
            return changes;
        }
    }
}
