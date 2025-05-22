using LeaveApprovalSOLIDApp.Interfaces;
using LeaveApprovalSOLIDApp.Models;
using LeaveApprovalSOLIDApp.Misc;
using LeaveApprovalSOLIDApp.Services;
using System;
using System.Collections.Generic;

namespace LeaveApprovalSOLIDApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Leave> leaveList = new();
            ILeaveApply<Leave> leaveApplyService = new LeaveApplyServiceWrapper(leaveList);
            INotification notificationService = new NotificationService();
            IUpdateStatus<Leave> statusService = new UpdateStatusService(leaveList, notificationService);
            GetUserInput input = new();

            ManageLeaveFlow flow = new(leaveApplyService, statusService, input);
            flow.Run();
        }
    }

    // Wrapper to inject shared leave list into LeaveApplyService
    public class LeaveApplyServiceWrapper : ILeaveApply<Leave>
    {
        private readonly LeaveApplyService _service;

        public LeaveApplyServiceWrapper(List<Leave> sharedList)
        {
            _service = new LeaveApplyService(sharedList);
        }

        public Leave ApplyLeave(Leave item) => _service.ApplyLeave(item);
        public List<Leave> GetLeaveHistory(int empId) => _service.GetLeaveHistory(empId);
    }
}
