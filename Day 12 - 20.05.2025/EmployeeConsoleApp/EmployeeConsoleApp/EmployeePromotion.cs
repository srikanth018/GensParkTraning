using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeConsoleApp
{
    public class EmployeePromotion
    {
        GetUserInput getUserInput = new GetUserInput();
        private List<string> employeePromotionList = new List<string>();

        public object GetData(string message, string dataType)
        {
            object result = getUserInput.SingleUserInput(message, dataType);
            return result;
        }
        public void AddEmployee()
        {
            int count = 1;
            Console.WriteLine("Please enter the employee names in the order of their eligibility for promotion(Please enter blank to stop) ");
            while (true)
            {
                string? name = (string)GetData($"Please Enter Employee {count} name:", "String");

                if(name == null)
                {
                    Console.WriteLine("Input Stopped");
                    break;
                }
                employeePromotionList.Add(name);
                count++;
            }
        }

        public void getEmployeePosition()
        {
            string? name = (string)GetData($"Please enter the name of the employee to check promotion position: ", "String");
            int index = employeePromotionList.IndexOf(name);
            Console.WriteLine($"“{name}” is in the position {index} for promotion.");
        }
        public void ReduceMemory()
        {
            Console.WriteLine($"Current Size: {employeePromotionList.Capacity}, Current Count: {employeePromotionList.Count}");
            employeePromotionList.TrimExcess();
            Console.WriteLine($"After Trimmed Size: {employeePromotionList.Capacity}, After Trimmed Count: {employeePromotionList.Count}");
        }

        public void ShowPromotedList()
        {
            employeePromotionList.Sort();
            Console.WriteLine("Promoted Employees List");
            
            foreach(var employee in employeePromotionList)
            {
                Console.WriteLine(employee);
            }
        }
        public void ManageEmployeePromotion()
        {
            AddEmployee();
            //getEmployeePosition();
            //ReduceMemory();
            ShowPromotedList();

        }
    }
}
