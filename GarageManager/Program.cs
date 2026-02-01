using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GarageManager.Models;

namespace GarageManager
{
    internal class Program
    {
        static void Main()
        {
            var car = new Car();
            car.Name = "Toyota Crown";
            car.MileageKm = 464500;


            Console.WriteLine(car.Name);
            Console.WriteLine(car.MileageKm);
        }
    }
}
