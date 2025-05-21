using EmployeeWholeApp.Interfaces;
using EmployeeWholeApp.Models;
using EmployeeWholeApp.Repositories;
using EmployeeWholeApp.Services;

namespace EmployeeWholeApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IRepository<int, Employee> employeeRepository = new EmployeeRepository();
            IEmployeeService employeeService = new EmployeeService(employeeRepository);
            ManageEmployee manageEmployee = new ManageEmployee(employeeService);
            manageEmployee.Start();


        }
    }
}
