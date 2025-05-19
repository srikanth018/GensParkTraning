using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTasksApp
{
    public class FindLargestNumber
    {
        public void LargestNumber(int num1, int num2)
        {
            if (num1 == num2)
            {
                Console.WriteLine("Numbers are Equal!!...");
                return;
            }

            int largest = (num1 > num2) ? num1 : num2;
            Console.WriteLine($"Largest number is {largest}");
        }
    }
}
