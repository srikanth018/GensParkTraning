using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTasksApp
{
    public class SingleUserInput
    {
        public object GetInput(string message, string dataType)
        {
            while (true)
            {
                Console.Write(message);
                string? input = Console.ReadLine();
                dataType.ToLower();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Input cannot be empty. Please try again.");
                    continue;
                }

                if (dataType == "int")
                {
                    return Convert.ToInt32(input);
                }
                else if (dataType == "string") {
                    
                    return input ?? ""; 
                }
                else Console.WriteLine("Invalid.. Enter String or Int");
            }
        }
    }
}
