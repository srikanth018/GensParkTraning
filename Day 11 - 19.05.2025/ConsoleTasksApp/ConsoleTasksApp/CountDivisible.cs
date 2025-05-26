using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTasksApp
{
    public class CountDivisible
    {
        public void CountDivisibleBySeven(int[] arr)
        {
            int count = 0;
            foreach (int val in arr)
            {
                if (val % 7 == 0)
                {
                    count++;
                }
            }
            Console.WriteLine($"The count of numbers divisible by 7 is: {count}");
        }
    }
}
