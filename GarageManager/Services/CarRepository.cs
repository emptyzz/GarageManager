using GarageManager.Models;
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

        public static List<Car> GetCars()
        {
            var cars = new List<Car>();

            var connectionString = "Data Source=garage.db";
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT Id, Name, MileageKm FROM Cars ORDER BY Id;";
            
            using var reader = command.ExecuteReader();
            while (reader.Read()) 
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                int mileage = reader.GetInt32(2);

                var car = new Car() { Id = id, Name = name, MileageKm = mileage };

                cars.Add(car);
            }
            return cars;
        }

        public static bool DeleteCarById(int id)
        {
            var connectionString = "Data Source=garage.db";
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Cars WHERE Id = @id;";
            command.Parameters.AddWithValue("@id", id);

            int rows = command.ExecuteNonQuery();
            return rows > 0;
        }
        public static bool Exists(int carId)
        {
            var connectionString = "Data Source=garage.db";
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT 1 FROM Cars WHERE Id = @id LIMIT 1;";

            command.Parameters.AddWithValue("@id", carId);

            var resultObj = command.ExecuteScalar();
            return resultObj != null;
        }
    }
}
