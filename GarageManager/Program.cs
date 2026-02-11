using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using GarageManager.Models;
using GarageManager.Services;
using GarageManager.UI;

namespace GarageManager
{
    internal class Program
    {
        static void Main()
        {
            DatabaseInitializer.Initialize();

            var garage = new GarageService();

            while (true)
            {
                Console.WriteLine("\n=== Garage Manager ===");
                Console.WriteLine("1 - Add car");
                Console.WriteLine("2 - Show cars");
                Console.WriteLine("3 - Add service record");
                Console.WriteLine("4 - Show service record");
                Console.WriteLine("5 - Total cost for car");
                Console.WriteLine("6 - Save data");
                Console.WriteLine("7 - Load data");
                Console.WriteLine("8 - Delete car");
                Console.WriteLine("0 - Exit");

                Console.WriteLine("Choose option: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        var name = ConsoleInput.ReadNonEmptyString("Car name: ");
                        var mileage = ConsoleInput.ReadIntNonNegative("Mileage: ");

                        CarRepository.AddCar(name, mileage);
                        Console.WriteLine("Car added successful");
                        break;

                    case "2":
                        var cars = CarRepository.GetCars();

                        if (cars.Count == 0)
                        {
                            Console.WriteLine("No cars added yet.");
                        }
                        else
                        {
                            foreach (var c in cars)
                            {
                                Console.WriteLine("Id:" + c.Id + " " + c.Name + " - " + c.MileageKm + " km");
                            }
                        }
                        break;

                    case "3":
                        var carId = ConsoleInput.ReadIntNonNegative("Car id: ");
                        var mileageRecord = ConsoleInput.ReadIntNonNegative("Mileage: ");

                        Console.Write("Description record: ");
                        var descriptionRecord = Console.ReadLine();

                        int cost;
                        while (true)
                        {
                            Console.Write("Hom much cost: ");
                            string? input1 = Console.ReadLine();

                            if (int.TryParse(input1, out cost) && cost >= 0) break;

                            Console.WriteLine("Please enter a non-negative integer");
                        }

                        ServiceRecordRepository.AddRecord(carId, mileageRecord, descriptionRecord, cost, DateTime.Today);
                        Console.WriteLine("Record added successful");
                        break;

                    case "4":
                        var carIdRecords = ConsoleInput.ReadIntNonNegative("Car id: ");
                        var records = ServiceRecordRepository.GetRecordsForCar(carIdRecords);

                        if (records.Count == 0)
                        {
                            Console.WriteLine("No records");
                        }

                        else
                        {
                            foreach (var r in records)
                            {
                                Console.WriteLine(r.Date + " | " + r.Description + " | " + r.Cost);
                            }
                        }
                        break;

                    case "5":
                        Console.Write("Car name: ");
                        var nameCost = Console.ReadLine();
                        var total = garage.GetTotalCostForCar(nameCost);
                        if (total == 0)
                            Console.WriteLine("No record / total 0");
                        else
                            Console.WriteLine("Total cost for " + nameCost + ": " + total);

                        break;

                    case "6":
                        var id = ConsoleInput.ReadIntNonNegative("Car id: ");

                        if (CarRepository.DeleteCarById(id))
                            Console.WriteLine("Car deleted successful");
                        else Console.WriteLine("Car not found");

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
