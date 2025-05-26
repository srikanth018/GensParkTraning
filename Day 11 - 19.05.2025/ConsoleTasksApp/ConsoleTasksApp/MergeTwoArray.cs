using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleTasksApp
{
    public class MergeTwoArray
    {
        public void MergeArray(int[] arr1, int[] arr2)
        {
            int[] mergedArray = new int[arr1.Length + arr2.Length];

            Array.Copy(arr1,0, mergedArray,0, arr1.Length);
            Array.Copy(arr2,0, mergedArray, arr1.Length, arr2.Length);

            printArrayVal(mergedArray);
        }

        void printArrayVal(int[] arr)
        {
            Console.Write("Merged Array: ");
            foreach (int val in arr)
            {
                Console.Write($"{val} ");
            }
        }
    }
}
