using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTasksApp
{
    public class ValidRow
    {
        //private List<int> getFrequency(int[] arr)
        //{
        //    List<int> elementsList = new List<int>();
        //    List<int> frequencyList = new List<int>();

        //    for (int i = 0; i < arr.Length; i++)
        //    {
        //        int index = elementsList.IndexOf(arr[i]);
        //        if (index == -1)
        //        {
        //            elementsList.Add(arr[i]);
        //            frequencyList.Add(1);
        //        }
        //        else
        //        {
        //            frequencyList[index]++;
        //        }
        //    }

        //    return frequencyList;
        //}
        

        //public void isValid(int[] arr)
        //{
        //    if(arr.Length != 9)
        //    {
        //        Console.WriteLine("Please enter 9 values only..");
        //        return;
        //    }
        //    List<int> frequencyList = getFrequency(arr);

        //    foreach (int val in frequencyList)
        //    {
        //        if(val != 1)
        //        {
        //            Console.WriteLine("Sorry!! The Given row is not representing a valid Sudoku row");
        //            return;
        //        }
        //    }
        //    Console.WriteLine("Yes!! This is a valid Sudoku Row");
        //}

        public bool isValid(int[] arr)
        {
            if (arr.Length != 9)
            {
                Console.WriteLine("Please enter 9 values..");
                return false;
            }
            HashSet<int> uniqueNumbers = new HashSet<int>();

            foreach (int val in arr) uniqueNumbers.Add(val);

            if(uniqueNumbers.Count != 9)
            {
                //Console.Write("Sorry!! The Given row is not representing a valid Sudoku row");
                return false;
            }
            
            //Console.WriteLine("Yes!! This is a valid Sudoku Row");
            return true;

        }
    }
}
