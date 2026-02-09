using Microsoft.Data.Sqlite;

namespace GarageManager.Services
{
    internal class CarRepository
    {
        public static void AddCar(string name, int mileage)
        {
            var connectionString = "Data Source=garage.db";

            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText =
                "INSERT INTO Cars (Name, MileageKm) VALUES (@name, @mileage);";

            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@mileage", mileage);

            command.ExecuteNonQuery();
        }
    }
}
