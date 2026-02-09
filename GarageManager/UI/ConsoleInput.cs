using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GarageManager.UI
{
    static class ConsoleInput
    {
        public static string ReadNonEmptyString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input1 = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input1))
                    Console.WriteLine("Please enter a non-empty value");
                else return input1.Trim();
            }
        }

        public static int ReadIntNonNegative(string prompt)
        {
            int value;
            while (true)
            {
                Console.Write(prompt);
                string? input1 = Console.ReadLine();
                
                if (int.TryParse(input1, out value) && value >= 0) return value;
                Console.WriteLine("Please enter a non-negative integer");
            }
        }
        
        public static decimal ReadDecimalNonNegative(string prompt)
        {
            decimal value;
            while (true)
            {
                Console.Write(prompt);
                string? input1 = Console.ReadLine();

                if(decimal.TryParse(input1, out value) && value >= 0) return value;
                Console.WriteLine("Please enter a non - negative number");
            }
        }
    }
}
