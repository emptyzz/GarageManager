using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GarageManager.Models;
using GarageManager.Models.Services;

namespace GarageManager
{
    internal class Program
    {
        static void Main()
        {
            var garage = new GarageService();

            var car1 = new Car();
            car1.Name = "Toyota Crown";
            car1.MileageKm = 245000;

            var car2 = new Car();
            car2.Name = "Mark II";
            car2.MileageKm = 180000;

            garage.AddCar(car1);
            garage.AddCar(car2);

            var cars = garage.GetCars();

            foreach (var c in cars)
            {
                Console.WriteLine(c.Name + " - " + c.MileageKm + " km");
            }

        }
    }
}
