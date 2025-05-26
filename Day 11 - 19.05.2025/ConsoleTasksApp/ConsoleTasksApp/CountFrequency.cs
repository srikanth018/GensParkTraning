using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTasksApp
{
    public class CountFrequency
    {

        public void CountFrequencyOfElements(int[] arr)
        {
            
            List<int> elementsArr = new List<int>();
            List<int> frequencyArr = new List<int>();

            for(int i = 0; i < arr.Length; i++)
            {
                int index = elementsArr.IndexOf(arr[i]);
                if (index == -1)
                {
                    elementsArr.Add(arr[i]);
                    frequencyArr.Add(1);
                }
                else
                {
                    frequencyArr[index]++;
                }
            }

            for(int i=0; i < elementsArr.Count; i++)
            {
                Console.WriteLine($"Element {elementsArr[i]} occurs {frequencyArr[i]} times");
            }
        }
    }
}
