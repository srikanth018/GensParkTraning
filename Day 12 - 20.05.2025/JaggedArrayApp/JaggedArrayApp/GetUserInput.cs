using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaggedArrayApp
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
