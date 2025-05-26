using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeConsoleApp
{
    public class GetUserInput
    {
        public object SingleUserInput(string message, string dataType)
        {
            dataType = dataType.ToLower();

            while (true)
            {
                Console.Write(message);
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Input cannot be empty. Please try again.");
                    continue;
                    //return null;
                }

                if (dataType == "integer")
                {
                    if (int.TryParse(input, out int result))
                    {
                        return result;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid integer.");
                        continue;
                    }
                }
                else if(dataType == "double")
                {
                    if (double.TryParse(input, out double result))
                    {
                        return result;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid double.");
                        continue;
                    }
                }
                else if (dataType == "string")
                {
                    return input;
                }
                else
                {
                    Console.WriteLine("Invalid data type. Please use 'string' or 'integer'.");
                }
            }
        }
    }
}
