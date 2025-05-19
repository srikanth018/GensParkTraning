using System;
using System.Collections.Generic;

namespace ConsoleTasksApp
{
    public class GetArrayFromUser
    {
        public int[] UserArrayInput()
        {
            List<int> arrList = new List<int>();
            int val;
            int index = 1;

            Console.WriteLine("Please enter numbers one by one. Press Enter (empty input) to stop.");

            while (true)
            {
                Console.Write($"(Press Enter (empty input) to stop.) Enter number {index}: ");
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    if (arrList.Count == 0)
                    {
                        Console.WriteLine("Hey! Array must contain values.");
                        continue;
                    }
                    Console.WriteLine("Input stopped.");
                    break;
                }

                if (!int.TryParse(input, out val))
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer.");
                    continue; 
                }

                arrList.Add(val);
                index++;
            }

            return arrList.ToArray();
        }

    }
}
