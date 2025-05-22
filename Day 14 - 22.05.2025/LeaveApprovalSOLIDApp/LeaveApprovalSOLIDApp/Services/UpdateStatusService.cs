using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveApprovalSOLIDApp.Interfaces;
using LeaveApprovalSOLIDApp.Models;

namespace LeaveApprovalSOLIDApp.Services
{
    public class UpdateStatusService : IUpdateStatus<Leave>
    {
        private readonly List<Leave> _leaveList;
        private readonly INotification _notifier;

        public UpdateStatusService(List<Leave> leaveList, INotification notifier)
        {
            _leaveList = leaveList;
            _notifier = notifier;
        }

        public Leave ApproveLeave(int leaveId)
        {
            var leave = _leaveList.FirstOrDefault(l => l.LeaveId == leaveId);
            if (leave == null)
            {
                Console.WriteLine("❌ Leave ID not found.");
                return null;
            }

            leave.Status = "Approved";
            _notifier.Notify($"Leave ID {leaveId} has been approved for {leave.EmployeeName}.");
            return leave;
        }

        public Leave RejectLeave(int leaveId)
        {
            var leave = _leaveList.FirstOrDefault(l => l.LeaveId == leaveId);
            if (leave == null)
            {
                Console.WriteLine("❌ Leave ID not found.");
                return null;
            }

            leave.Status = "Rejected";
            _notifier.Notify($"Leave ID {leaveId} has been rejected for {leave.EmployeeName}.");
            return leave;
        }

        public List<Leave> GetAllLeaves()
        {
            return _leaveList;
        }
    }
}
