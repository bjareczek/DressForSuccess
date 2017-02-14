using DressForSuccess;
using System;
using System.Collections.Generic;

namespace GoingToWork
{
    class Program
    {
        static void Main(string[] args)
        {
            string Temperature;
            Temperatures TemperatureParam;
            Console.WriteLine("Please enter HOT or COLD, followed by dress commands COMMA DELIMITED.");
            var input = Console.ReadLine();
            var inputParsed = input.Replace(" ", "").Split(',');
            Temperature = inputParsed[0].ToString();
            TemperatureParam = Temperature.ToLower() == "hot" ? Temperatures.Hot : Temperatures.Cold;
            List<int> DressCommands = new List<int>();
            for (int i = 1; i < inputParsed.Length; i++)
            {
                int number;
                bool result = Int32.TryParse(inputParsed[i], out number);
                if (result)
                {
                    DressCommands.Add(number);
                }
                else
                {
                    Console.WriteLine("Invalid input.  Please try again.  E.g. 'HOT, 8, 6, 4, 2, 1, 7'");
                }
            }
            GetDressed getDressed = new GetDressed();
            var dressResults = getDressed.ExecuteDressCommandsByTemperature(TemperatureParam, DressCommands);
            Console.WriteLine(dressResults);
            Console.ReadKey();
        }
    }
}
