// Services/LeaveApplyService.cs
using LeaveApprovalSOLIDApp.Interfaces;
using LeaveApprovalSOLIDApp.Models;

namespace LeaveApprovalSOLIDApp.Services
{
    public class LeaveApplyService : ILeaveApply<Leave>
    {
        private readonly List<Leave> _leaveList = new();
        private List<Leave> sharedList;

        public LeaveApplyService(List<Leave> sharedList)
        {
            this._leaveList = sharedList;
        }

        public Leave ApplyLeave(Leave leave)
        {
            leave.LeaveId = _leaveList.Count + 1;
            
            _leaveList.Add(leave);

            Console.WriteLine("\n Leave applied successfully.\n");
            return leave;
        }

        public List<Leave> GetLeaveHistory(int empId)
        {
            return _leaveList
                .Where(l => l.EmployeeId == empId)
                .ToList();
        }
    }
}
