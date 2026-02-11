using GarageManager.Models;
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
        public static List<ServiceRecord> GetRecordsForCar(int carId)
        {
            var records = new List<ServiceRecord>();

            var connectionString = "Data Source=garage.db";
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            using var command = connection.CreateCommand();

            command.CommandText = "PRAGMA foreign_keys = ON;";
            command.ExecuteNonQuery();

            command.CommandText = "SELECT Id, CarId, Date, MileageKm, Description, Cost FROM ServiceRecords WHERE CarId = @carId ORDER BY Date;";

            command.Parameters.AddWithValue("@carId", carId);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                int carIdFromDb = reader.GetInt32(1);
                string dateText = reader.GetString(2);
                int mileageKm = reader.GetInt32(3);
                string description = reader.GetString(4);
                double cost = reader.GetDouble(5);

                var date = DateTime.Parse(dateText);

                var serviceRecord = new ServiceRecord() { Id = id, CarId = carIdFromDb, Date = date, MileageKm = mileageKm, Description = description, Cost = (decimal)cost };

                records.Add(serviceRecord);
            }
            return records;
        }

        public static decimal GetTotalCostForCar(int carId)
        {
            var connectionString = "Data Source=garage.db";
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            using var command = connection.CreateCommand();

            command.CommandText = "PRAGMA foreign_keys = ON;";
            command.ExecuteNonQuery();

            command.CommandText = "SELECT COALESCE(SUM(Cost), 0) FROM ServiceRecords WHERE CarId = @carId;";

            command.Parameters.AddWithValue("@carId", carId);

            var resultObj = command.ExecuteScalar();
            double total = Convert.ToDouble(resultObj);
            return (decimal)total;
        }
    }
}
