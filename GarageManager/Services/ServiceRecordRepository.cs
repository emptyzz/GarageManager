using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManager.Services
{
    internal class ServiceRecordRepository
    {
        public static void AddRecord(int carId, int mileageKm, string description, decimal cost, DateTime date)
        {
            var connectionString = "Data Source=garage.db";

            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            using var command = connection.CreateCommand();

            command.CommandText = "PRAGMA foreign_keys = ON;";
            command.ExecuteNonQuery();

            command.CommandText =
                "INSERT INTO ServiceRecords (CarId, Date, MileageKm, Description, Cost) VALUES (@carId, @date, @mileage, @desc, @cost);";

            command.Parameters.AddWithValue("@carId", carId);
            command.Parameters.AddWithValue("@mileage", mileageKm);
            command.Parameters.AddWithValue("@desc", description);
            command.Parameters.AddWithValue("@cost", cost);
            command.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd"));


            command.ExecuteNonQuery();
        }
    }
}
