using Microsoft.Data.Sqlite;

namespace GarageManager.Services
{
    public class DatabaseInitializer
    {
        public static void Initialize(string connectionString)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            using var command = connection.CreateCommand();

            command.CommandText = "PRAGMA foreign_keys = ON;";
            command.ExecuteNonQuery();

            command.CommandText =
                "CREATE TABLE IF NOT EXISTS Cars (" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                "Name TEXT NOT NULL, " +
                "MileageKm INTEGER NOT NULL" +
                ");";

            command.ExecuteNonQuery();

            command.CommandText =
               "CREATE TABLE IF NOT EXISTS ServiceRecords (" +
               "Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
               "CarId INTEGER NOT NULL, " +
               "Date TEXT NOT NULL, " +
               "MileageKm INTEGER NOT NULL, " +
               "Description TEXT NOT NULL, " +
               "Cost REAL NOT NULL, " +
               "FOREIGN KEY(CarId) REFERENCES Cars(Id) ON DELETE CASCADE" +
               ");";

            command.ExecuteNonQuery();
        }
        public static void Initialize()
        {
            Initialize("Data Source=garage.db");
        }
    }
}
