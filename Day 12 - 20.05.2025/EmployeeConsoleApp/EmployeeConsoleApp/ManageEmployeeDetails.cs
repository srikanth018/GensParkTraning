using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EmployeeConsoleApp
{
    public class ManageEmployeeDetails
    {
        private Dictionary<int, Employee> employeeDetails = new Dictionary<int, Employee>();

        GetUserInput getUserInput = new GetUserInput();
        public object GetData(string message, string dataType)
        {
            object result = getUserInput.SingleUserInput(message, dataType);
            return result;
        }

        public void AddDefaultEmployees()
        {
            employeeDetails.Add(101, new Employee(101, 28, "Ravi", 50000));
            employeeDetails.Add(102, new Employee(102, 32, "Lakshmi", 62000));
            employeeDetails.Add(103, new Employee(103, 30, "Arun", 58000));
            employeeDetails.Add(104, new Employee(104, 26, "Divya", 47000));
            employeeDetails.Add(105, new Employee(105, 35, "Suresh", 72000));
            employeeDetails.Add(106, new Employee(106, 38, "Ravi", 70000));


            Console.WriteLine("Sample Employees Added");
            
        }

        public void ShowAllEmployees()
        {
            if (employeeDetails.Count == 0)
            {
                Console.WriteLine("No employee records to display.");
                return;
            }

            Console.WriteLine("\nAll Employee Details:");
            foreach (var emp in employeeDetails.Values)
            {
                Console.WriteLine("=======================");
                Console.WriteLine(emp.ToString());
            }
        }


        public void GetEmployeeByID()
        {
            int empId = (int)GetData("Enter the employee Id: ", "Integer");
            if (!employeeDetails.ContainsKey(empId))
            {
                Console.WriteLine("Invalid Employee Id");
                return;
            }

            var employeeById = employeeDetails.Where(e => e.Key == empId).Select(e=>e.Value).FirstOrDefault(); ;
            Console.WriteLine(employeeById);
        }

        public void SortBySalary()
        {
            List<Employee> employeeList = employeeDetails.Values.ToList();
            employeeList.Sort();
            Console.WriteLine("Employees sorted by salary (ascending):");
            foreach (var emp in employeeList)
            {
                Console.WriteLine("================");
                Console.WriteLine(emp);
            }
        }

        public void GetEmployeesByName()
        {
            string nameToSearch = (string)GetData("Enter the employee name to search: ", "String");
            var employeesList = employeeDetails.Where(e => e.Value.Name.ToLower() == nameToSearch.ToLower()).ToList();

            if(employeesList.Count() == 0)
            {
                Console.WriteLine("No Employees Found with the provide name");
                return;
            }
            foreach (var emp in employeesList)
            {
                Console.WriteLine("=======================");
                Console.WriteLine(emp);
            }
        }

        public void FindElder()
        {
            int empId = (int)GetData("Enter the employee Id: ", "Integer");
            var givenEmployeeAge = employeeDetails.Where(e => e.Key == empId).Select(e => e.Value.Age).FirstOrDefault();
            var elderThanGivenEmployee = employeeDetails.Where(e => e.Value.Age > givenEmployeeAge).Select(e=>e.Value).FirstOrDefault();

            if(elderThanGivenEmployee is null)
            {
                Console.WriteLine("The given Employee is the eldest of all employees");
                return;
            }
            Console.WriteLine($"The Employee who is elder than the give employee");
            Console.WriteLine(elderThanGivenEmployee);
        }

        public void AddNweEmployee()
        {
            while (true)
            {
                Employee emp = new Employee();
                emp.TakeEmployeeDetailsFromUser();

                if (employeeDetails.ContainsKey(emp.Id))
                {
                    Console.WriteLine("An employee with this ID already exists. Please enter a unique ID.");
                    continue;
                }

                employeeDetails.Add(emp.Id, emp);
                Console.WriteLine("Employee added successfully.");

                string? choice = (string)GetData("Do you want to add another employee? (y/n): ", "String");
                if (choice?.ToLower() != "y")
                {
                    break;
                }
            }
        }

        public void UpdateEmployee()
        {
            int empId = (int)GetData("Enter the employee Id: ", "Integer");

            if (!employeeDetails.ContainsKey(empId))
            {
                Console.WriteLine("Employee with this ID does not exist.");
                return;
            }

            var employeeOldDetails = employeeDetails.Where(e => e.Key == empId).Select(e => e.Value).FirstOrDefault();
            Console.WriteLine($"Existing details of the emp id {empId}");
            Console.WriteLine(employeeOldDetails);
            Console.WriteLine("=================");

            Employee updateEmployeeData = employeeDetails[empId];
            while (true)
            {
                Console.WriteLine("Choose the field you want to update");
                Console.WriteLine("1. Update Name");
                Console.WriteLine("2. Update Age");
                Console.WriteLine("3. Update Salary");
                Console.WriteLine("0. Exit");

                int selection = (int)GetData("select the number from the above list", "Integer");
                switch (selection)
                {
                    case 1:
                        string newName = (string)GetData("Enter new Name: ", "String");
                        updateEmployeeData.Name = newName;
                        Console.WriteLine("Name updated.");
                        break;
                    case 2:
                        int newAge = (int)GetData("Enter new Age: ", "Integer");
                        updateEmployeeData.Age = newAge;
                        Console.WriteLine("Age updated.");
                        break;
                    case 3:
                        double Salary = (double)GetData("Enter new Salary: ", "Double");
                        updateEmployeeData.Salary = Salary;
                        Console.WriteLine("Salary updated.");
                        break;
                    case 0:
                        Console.WriteLine("Exiting update menu.");
                        return;
                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        break;
                }

                
                string? choice = (string)GetData("Do you want to update another field? (y/n): ", "String");
                if (choice?.ToLower() != "y")
                {
                    break;
                }
            }

        }

        public void DeleteEmployee()
        {
            int empId = (int)GetData("Enter the employee Id: ", "Integer");
            if (!employeeDetails.ContainsKey(empId))
            {
                Console.WriteLine("Invalid Employee Id");
                return;
            }
            employeeDetails.Remove(empId);
            Console.WriteLine($"Employee with ID {empId} has been removed.");
        }

        public void ManageDetails()
        {
            AddDefaultEmployees();

            while (true)
            {
                Console.WriteLine("\n================== MENU ==================");
                Console.WriteLine("1. Show All Employees");
                Console.WriteLine("2. Add New Employee");
                Console.WriteLine("3. Update Employee");
                Console.WriteLine("4. Get Employee by ID");
                Console.WriteLine("5. Delete Employee");
                Console.WriteLine("6. Get Employees by Name");
                Console.WriteLine("7. Sort Employees by Salary");
                Console.WriteLine("8. Find Employee Elder Than Given");
                Console.WriteLine("0. Exit");
                Console.WriteLine("==========================================");

                int choice = (int)GetData("Enter your choice: ", "Integer");

                switch (choice)
                {
                    //Hard Questions
                    case 1:
                        ShowAllEmployees();
                        break;

                    case 2:
                        AddNweEmployee();
                        break;

                    case 3:
                        UpdateEmployee();
                        break;

                    case 4:
                        GetEmployeeByID();
                        break;

                    case 5:
                        DeleteEmployee();
                        break;
                // Medium Questions
                    case 6:
                        GetEmployeesByName();
                        break;

                    case 7:
                        SortBySalary();
                        break;

                    case 8:
                        FindElder();
                        break;

                    case 0:
                        Console.WriteLine("Exiting application. Goodbye!");
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please select a number from the menu.");
                        break;
                }

            }
        }


    }
}
