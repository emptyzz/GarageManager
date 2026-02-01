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

        public void AddCar(Car car)
        {
            _cars.Add(car);
        }

        public IReadOnlyList<Car> GetCars()
        {
            return _cars;
        }
    }
}
