using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveApprovalSOLIDApp.Interfaces
{
    public interface ILeaveApply<T> where T : class
    {
        T ApplyLeave(T item);
        List<T> GetLeaveHistory(int empId);
    }
}
