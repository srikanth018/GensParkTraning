using AppointmentApp.Interfaces;
using AppointmentApp.Repositories;
using AppointmentApp.Services;

namespace AppointmentApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Initialize the appointment service
            IAppointmentService appointmentService = new AppointmentService(new AppointmentRepository());
            ManageAppointments manageAppointments = new ManageAppointments(appointmentService);
            // Start the appointment management system
            manageAppointments.Run();

        }
    }
}
