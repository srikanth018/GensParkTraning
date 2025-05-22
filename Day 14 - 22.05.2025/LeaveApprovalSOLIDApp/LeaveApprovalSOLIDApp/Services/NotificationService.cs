using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveApprovalSOLIDApp.Interfaces;

namespace LeaveApprovalSOLIDApp.Services
{
    public class NotificationService : INotification
    {
        public void Notify(string message)
        {
            Console.WriteLine($"Notification: {message}");
        }
    }
}
