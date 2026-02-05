using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GarageManager.Models.Services
{
    internal class GarageService
    {
        private List<Car> _cars = new List<Car>();
        private List<ServiceRecord> _records = new List<ServiceRecord>();

        public void AddCar(Car car)
        {
            _cars.Add(car);
        }

        public IReadOnlyList<Car> GetCars()
        {
            return _cars;
        }

        public void AddRecord(ServiceRecord record)
        {
            _records.Add(record);
        }

        public IReadOnlyList<ServiceRecord> GetRecords()
        {
            return _records;
        }

        public List<ServiceRecord> GetRecordsForCar(string carName)
        {
            var result = new List<ServiceRecord>();

            foreach (var record in _records)
            {
                if (string.Equals(record.CarName, carName, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(record);
                }
            }
            return result;
        }
        public decimal GetTotalCostForCar(string carName)
        {
            decimal totalCost = 0;
            foreach (var record in _records)
            {
                if(string.Equals(record.CarName, carName, StringComparison.OrdinalIgnoreCase))
                {
                    totalCost += record.Cost;
                }
            }
            return totalCost;
        }

        public GarageData ExportData()
        {
            var data = new GarageData();
            data.Cars.AddRange(_cars);
            data.Records.AddRange(_records);
            return data;
        }

        public void ImportData(GarageData data)
        {
            _cars.Clear();
            _records.Clear();

            if (data == null) return;

            _cars.AddRange(data.Cars);
            _records.AddRange(data.Records);
        }
    }
}
