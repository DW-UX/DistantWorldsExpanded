using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace DistantWorlds.DBLoader
{
    public class FileDB
    {
        private const string _DiskDbPath = @"AdvMods\FilesDb.db";
        private readonly SQLiteConnection memoryConnection;
        private readonly SQLiteConnection memoryConnectionFile;
        public FileDB()
        {
            string fullPath = Path.GetFullPath(_DiskDbPath);
            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException("Database file not found at path: " + fullPath);
            }
            SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder()
            {
                //DataSource = "'File:InMemoryDB?Mode=Memory&Cache=Shared'",
                //Version = 3,
                FullUri = "File:InMemoryDB?Mode=Memory&Cache=Shared",
            };
            memoryConnection = new SQLiteConnection("FullUri='file:sharedmemdb?mode=memory&cache=shared'");
            //memoryConnection = new SQLiteConnection("File:InMemoryDB?Mode=Memory&Cache=Shared");
            //memoryConnection = new SQLiteConnection(builder.ConnectionString);
            memoryConnectionFile = new SQLiteConnection($"Data Source='{fullPath}'");
        }

        public void LoadDB()
        {
            memoryConnectionFile.Open();
            memoryConnection.Open();
            //using SQLiteCommand command = new SQLiteCommand("VACUUM INTO File:InMemoryDB?Cache=Shared", memoryConnectionFile);
            using SQLiteCommand command = new SQLiteCommand("VACUUM INTO 'file:sharedmemdb?mode=memory&cache=shared'", memoryConnectionFile);
            command.ExecuteNonQuery();
            memoryConnectionFile.Close();
        }

        public void Close()
        {
            memoryConnection?.Close();
            memoryConnection?.Dispose();
        }

        public void ApplyChanges(List<string> changes)
        {
            using SQLiteTransaction transaction = memoryConnection.BeginTransaction();
            try
            {
                foreach (var change in changes)
                {
                    using SQLiteCommand command = new SQLiteCommand(change, memoryConnection);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                transaction.Commit();
            }
        }

        public SQLiteDataReader GetRaceFamilyBiasReader()
        {
            using SQLiteCommand command = new SQLiteCommand("Select * From RaceFamilyBias", memoryConnection);
            return command.ExecuteReader();
        }
        public SQLiteDataReader GetRaceFamilyReader()
        {
            using SQLiteCommand command = new SQLiteCommand("Select * From RaceFamily", memoryConnection);
            return command.ExecuteReader();
        }
        public SQLiteDataReader GetRaceReader()
        {
            using SQLiteCommand command = new SQLiteCommand("Select * From Race", memoryConnection);
            return command.ExecuteReader();
        }
        public SQLiteDataReader GetGovernmentsReader()
        {
            using SQLiteCommand command = new SQLiteCommand("Select * From Governments", memoryConnection);
            return command.ExecuteReader();
        }
        public SQLiteDataReader GetGovernmentsBiasReader()
        {
            using SQLiteCommand command = new SQLiteCommand("Select * From GovernmentBiases", memoryConnection);
            return command.ExecuteReader();
        }
        public SQLiteDataReader GetAgentNamesReader()
        {
            using SQLiteCommand command = new SQLiteCommand("Select * From AgentNames", memoryConnection);
            return command.ExecuteReader();
        }
        public SQLiteDataReader GetFacilitiesReader()
        {
            using SQLiteCommand command = new SQLiteCommand("Select * From Facilities", memoryConnection);
            return command.ExecuteReader();
        }
        public SQLiteDataReader GetFightersReader()
        {
            using SQLiteCommand command = new SQLiteCommand("Select * From Fighters", memoryConnection);
            return command.ExecuteReader();
        }
        public SQLiteDataReader GetResourcesReader()
        {
            using SQLiteCommand command = new SQLiteCommand("Select * From Resources", memoryConnection);
            return command.ExecuteReader();
        }
        public SQLiteDataReader GetResearchReader()
        {
            using SQLiteCommand command = new SQLiteCommand("Select * From Research", memoryConnection);
            return command.ExecuteReader();
        }
        public SQLiteDataReader GetPlaguesReader()
        {
            using SQLiteCommand command = new SQLiteCommand("Select * From Plagues", memoryConnection);
            return command.ExecuteReader();
        }
        public SQLiteDataReader GetRaceBiasReader()
        {
            using SQLiteCommand command = new SQLiteCommand("Select * From RaceBias", memoryConnection);
            return command.ExecuteReader();
        }
        public SQLiteDataReader GetComponentsReader()
        {
            using SQLiteCommand command = new SQLiteCommand("Select * From Components", memoryConnection);
            return command.ExecuteReader();
        }
        public int GetProjIdCOunt()
        {
            using SQLiteCommand cmd = new SQLiteCommand("SELECT COUNT(ID) FROM Research WHERE Project <> ''", memoryConnection);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }
}
