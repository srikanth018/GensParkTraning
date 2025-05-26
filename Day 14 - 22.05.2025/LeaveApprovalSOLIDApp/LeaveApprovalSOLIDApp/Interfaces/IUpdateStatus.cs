using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveApprovalSOLIDApp.Interfaces
{
    public interface IUpdateStatus<T> where T : class
    {
        T ApproveLeave(int leaveId);
        T RejectLeave(int leaveId);
    }
}
