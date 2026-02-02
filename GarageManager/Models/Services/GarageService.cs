using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
