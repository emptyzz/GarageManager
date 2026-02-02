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
            while (true)
            {
                Console.WriteLine("\n=== Garage Manager ===");
                Console.WriteLine("1 - Add car");
                Console.WriteLine("2 - Show cars");
                Console.WriteLine("0 - Exit");

                Console.WriteLine("Choose option: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.Write("Car name: ");
                        var name = Console.ReadLine();

                        Console.Write("Mileage: ");
                        int mileage = int.Parse(Console.ReadLine());

                        var car = new Car { Name = name, MileageKm = mileage};
                        garage.AddCar(car);
                        Console.WriteLine("Car added successful");
                        break;

                    case "2":
                        var cars = garage.GetCars();

                        if(cars.Count == 0)
                        {
                            Console.WriteLine("No cars added yet.");
                        }
                        else
                        {
                            foreach (var c in cars)
                            {
                                Console.WriteLine(c.Name + " - " + c.MileageKm + " km");
                            }
                        }
                        break;

                    case "0":

                        return;

                    default:
                        Console.WriteLine("Unknown option");
                        break;
                }
            }
        }
    }
}
