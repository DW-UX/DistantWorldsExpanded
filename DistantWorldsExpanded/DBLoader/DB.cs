using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace DistantWorlds.DBLoader
{
    internal class DB
    {
        private const string _DbPath = @"AdvMods\FilesDb.db";
        private readonly SQLiteConnection sQLiteConnection;
        public DB()
        {
            string fullPath = Path.GetFullPath(_DbPath);
            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException("Database file not found at path: " + fullPath);
            }
            sQLiteConnection = new SQLiteConnection("Data Source=" + fullPath);
        }

        public void LoadDB()
        {
            sQLiteConnection.Open();
        }

        public void ApplyChanges(List<string> changes)
        {
            using SQLiteTransaction transaction = sQLiteConnection.BeginTransaction();
            try
            {
                foreach (var change in changes)
                {
                    using SQLiteCommand command = new SQLiteCommand(change.Replace("'", "''"), sQLiteConnection);
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
    }
}
