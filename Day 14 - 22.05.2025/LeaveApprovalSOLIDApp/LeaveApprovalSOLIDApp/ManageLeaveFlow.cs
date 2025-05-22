using LeaveApprovalSOLIDApp.Interfaces;
using LeaveApprovalSOLIDApp.Models;
using LeaveApprovalSOLIDApp.Misc;
using System;
using System.Collections.Generic;
using LeaveApprovalSOLIDApp.Services;

namespace LeaveApprovalSOLIDApp
{
    public class ManageLeaveFlow
    {
        private readonly ILeaveApply<Leave> _leaveApplyService;
        private readonly IUpdateStatus<Leave> _statusService;
        private readonly GetUserInput _input;

        public ManageLeaveFlow(ILeaveApply<Leave> leaveApplyService, IUpdateStatus<Leave> statusService, GetUserInput input)
        {
            _leaveApplyService = leaveApplyService;
            _statusService = statusService;
            _input = input;
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("==== Login ====");
                Console.WriteLine("1. Employee");
                Console.WriteLine("2. Manager");
                Console.WriteLine("3. Exit");
                Console.Write("Select your role: ");
                string roleChoice = Console.ReadLine();

                switch (roleChoice)
                {
                    case "1":
                        RunEmployeeMenu();
                        break;
                    case "2":
                        RunManagerMenu();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
            
        }

        private void RunEmployeeMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n==== Employee Menu ====");
                Console.WriteLine("1. Apply for Leave");
                Console.WriteLine("2. View Leave History");
                Console.WriteLine("3. Exit");
                Console.Write("Select option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Leave leave = _input.CollectLeaveApplicationInput();
                        _leaveApplyService.ApplyLeave(leave);
                        break;
                    case "2":
                        Console.Write("Enter Employee ID: ");
                        if (int.TryParse(Console.ReadLine(), out int empId))
                        {
                            var history = _leaveApplyService.GetLeaveHistory(empId);
                            if (history.Count == 0)
                                Console.WriteLine("No leave history found.");
                            else
                            {
                                Console.WriteLine("\nLeave History:");
                                foreach (var l in history)
                                {
                                    Console.WriteLine($"ID: {l.LeaveId}, From: {l.StartDate.ToShortDateString()}, To: {l.EndDate.ToShortDateString()}, Status: {l.Status}");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Employee ID.");
                        }
                        break;
                    case "3":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        private void RunManagerMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n==== Manager Menu ====");
                Console.WriteLine("1. View All Leave Requests");
                Console.WriteLine("2. Change Leave Status");
                Console.WriteLine("3. Exit");
                Console.Write("Select option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var allLeaves = ((UpdateStatusService)_statusService).GetAllLeaves();
                        if (allLeaves.Count == 0)
                            Console.WriteLine("No leave requests found.");
                        else
                        {
                            Console.WriteLine("\nAll Leave Requests:");
                            foreach (var l in allLeaves)
                            {
                                Console.WriteLine($"ID: {l.LeaveId}, Name: {l.EmployeeName}, From: {l.StartDate.ToShortDateString()}, To: {l.EndDate.ToShortDateString()}, Status: {l.Status}");
                            }
                        }
                        break;
                    case "2":
                        Console.Write("Enter Leave ID to update status: ");
                        if (int.TryParse(Console.ReadLine(), out int leaveId))
                        {
                            Console.WriteLine("1. Approve");
                            Console.WriteLine("2. Reject");
                            Console.Write("Select action: ");
                            string statusChoice = Console.ReadLine();

                            if (statusChoice == "1")
                                _statusService.ApproveLeave(leaveId);
                            else if (statusChoice == "2")
                                _statusService.RejectLeave(leaveId);
                            else
                                Console.WriteLine("Invalid status option.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid Leave ID.");
                        }
                        break;
                    case "3":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
    }
}
