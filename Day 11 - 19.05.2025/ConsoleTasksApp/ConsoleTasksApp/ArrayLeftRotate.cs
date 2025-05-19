using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTasksApp
{
    public class ArrayLeftRotate
    {
        public void Rotate(int[] arr)
        {

            int firstVal = arr[0];

            for(int i=0; i < arr.Length-1; i++)
            {
                arr[i] = arr[i + 1];
            }

            arr[arr.Length - 1] = firstVal;

            printArrayVal(arr);
        }

        void printArrayVal(int[] arr)
        {
            foreach (int val in arr)
            {
                Console.Write($"{val} ");
            }
        }
    }
}
