// Misc/GetUserInput.cs
using LeaveApprovalSOLIDApp.Models;

namespace LeaveApprovalSOLIDApp.Misc
{
    public class GetUserInput
    {
        public Leave CollectLeaveApplicationInput()
        {
            int empId;
            while (true)
            {
                Console.Write("Enter Employee ID: ");
                if (int.TryParse(Console.ReadLine(), out empId)) break;
                Console.WriteLine("Invalid ID. Try again.");
            }

            string employeeName;
            while (true)
            {
                Console.Write("Enter Employee Name: ");
                employeeName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(employeeName)) break;
                Console.WriteLine("Name cannot be empty.");
            }

            DateTime startDate;
            while (true)
            {
                Console.Write("Enter Start Date (yyyy-mm-dd): ");
                if (DateTime.TryParse(Console.ReadLine(), out startDate)) break;
                Console.WriteLine("Invalid date. Try again.");
            }

            DateTime endDate;
            while (true)
            {
                Console.Write("Enter End Date (yyyy-mm-dd): ");
                if (DateTime.TryParse(Console.ReadLine(), out endDate) && endDate >= startDate) break;
                Console.WriteLine("Invalid or earlier than start date.");
            }

            string reason;
            while (true)
            {
                Console.Write("Enter Reason for Leave: ");
                reason = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(reason)) break;
                Console.WriteLine("Reason cannot be empty.");
            }

            return new Leave
            {
                EmployeeId = empId,
                EmployeeName = employeeName,
                StartDate = startDate,
                EndDate = endDate,
                Reason = reason,
                Status = "Pending"
            };
        }

        public string GetEmployeeName()
        {
            Console.Write("Enter Employee Name: ");
            return Console.ReadLine();
        }
    }
}
