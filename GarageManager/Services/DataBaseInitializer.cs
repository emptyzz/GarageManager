using Microsoft.Data.Sqlite;

namespace GarageManager.Services
{
    internal class DatabaseInitializer
    {
        public static void Initialize()
        {
            var connectionString = "Data Source=garage.db";

            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText =
                "CREATE TABLE IF NOT EXISTS Cars (" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                "Name TEXT NOT NULL, " +
                "MileageKm INTEGER NOT NULL" +
                ");";

            command.ExecuteNonQuery();
        }
    }
}
