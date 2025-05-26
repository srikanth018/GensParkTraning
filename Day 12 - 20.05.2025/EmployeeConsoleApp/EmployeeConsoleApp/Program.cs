using System;

namespace EmployeeConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Preparation
            //Employee emp = new Employee();
            //emp.TakeEmployeeDetailsFromUser();

            //Console.WriteLine("\nEmployee Details:");
            //Console.WriteLine(emp);

            //// Easy 1,2,3,4:
            //EmployeePromotion employeePromotion = new EmployeePromotion();
            //employeePromotion.ManageEmployeePromotion();



            // Medium 1,2,3,4, Hard 1:
            ManageEmployeeDetails manageEmployeeDetails = new ManageEmployeeDetails();
            manageEmployeeDetails.ManageDetails();

            // Aditional Question
            //Product product = new Product();
            //product.AddProduct();

        }
    }
}
