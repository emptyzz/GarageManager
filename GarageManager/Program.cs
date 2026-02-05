using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GarageManager.Models;
using GarageManager.Services;

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
                Console.WriteLine("3 - Add service record");
                Console.WriteLine("4 - Show service record");
                Console.WriteLine("5 - Total cost for car");
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

                    case "3":
                        Console.Write("Car name: ");
                        var nameRecord = Console.ReadLine();

                        Console.Write("Mileage: ");
                        var mileageRecord = int.Parse(Console.ReadLine());

                        Console.Write("Description record: ");
                        var descriptionRecord = Console.ReadLine();

                        Console.Write("Hom much cost: ");
                        decimal cost = decimal.Parse(Console.ReadLine());

                        var record = new ServiceRecord {CarName = nameRecord, MileageKm = mileageRecord, Description = descriptionRecord, Cost = cost, Date = DateTime.Today};
                        garage.AddRecord(record);
                        Console.WriteLine("Record added successful");
                        break;

                    case "4":
                        Console.Write("Car name: ");
                        var nameGetRecords = Console.ReadLine();

                        var records = garage.GetRecordsForCar(nameGetRecords);

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
