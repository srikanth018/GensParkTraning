using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentApp.Interfaces;
using AppointmentApp.Models;
using AppointmentApp.Services;

namespace AppointmentApp
{
    public class ManageAppointments
    {
        private IAppointmentService _appointmentService;

        public ManageAppointments(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public void Run()
        {
            
            while (true)
            {
                ShowMainMenu();
                int choice = GetUserChoice();
                switch (choice)
                {
                    case 1:
                        Appointment appointment = GetAppointmentDetails();
                        Appointment? newAppointment =  _appointmentService.AddAppointment(appointment);
                        Console.WriteLine("----------Appointment added successfully.----------");
                        ShowAddedAppointment(newAppointment);
                        break;
                    case 2:
                        while (true)
                        {
                            ShowSearchMenu();
                            int Searchchoice = GetUserChoice();
                            GetSearchAppointment(Searchchoice);
                            Console.WriteLine("---Do you want to search again (y/n)");
                            string searchAgain = Console.ReadLine() ?? string.Empty;
                            if (searchAgain.ToLower() != "y")
                            {
                                break;
                            }
                        }
                        break;
                    case 0:
                        Console.WriteLine("Exiting....");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
                
            }
        }

        private void GetSearchAppointment(int searchchoice)
        {
            while (true)
            {
                AppointmentSearchModel appointmentSearchModel = new AppointmentSearchModel();
                switch (searchchoice)
                {
                    case 1:
                        Console.Write("Enter Patient Name: ");
                        appointmentSearchModel.PatientName = Console.ReadLine();
                        break;
                    case 2:
                        Console.Write("Enter Appointment Date (yyyy-MM-dd): ");
                        if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
                        {
                            appointmentSearchModel.AppointmentDate = date;
                        }
                        else
                        {
                            Console.WriteLine("Invalid date format.");
                        }
                        break;
                    case 3:
                        Console.Write("Enter Reason: ");
                        appointmentSearchModel.Reason = Console.ReadLine();
                        break;
                    case 4:
                        int minAge, maxAge;
                        while (true)
                        {
                            Console.Write("Enter Minimum Age: ");
                            if (int.TryParse(Console.ReadLine(), out minAge) && minAge >= 0)
                                break;
                            Console.WriteLine("Invalid input. Please enter a non-negative integer.");
                        }
                        while (true)
                        {
                            Console.Write("Enter Maximum Age: ");
                            if (int.TryParse(Console.ReadLine(), out maxAge) && maxAge >= minAge)
                                break;
                            Console.WriteLine("Invalid input. Maximum must be greater than or equal to Minimum Age.");
                        }
                        appointmentSearchModel.AgeRange = new Range<int> { Min = minAge, Max = maxAge };
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        return;
                }

                var searchResults = _appointmentService.SearchAppointments(appointmentSearchModel);
                List<Appointment> appointments = searchResults?.ToList() ?? new List<Appointment>();

                ShowSearchResults(appointments);
                return;
            }

        }

        private void ShowSearchMenu()
        {
            Console.WriteLine("----Search Appointments by:------");
            Console.WriteLine("1. Patient Name");
            Console.WriteLine("2. Appointment Date");
            Console.WriteLine("3. Reason");
            Console.WriteLine("4. Age Range");
            Console.WriteLine("0. Back to Main Menu");
        }

        private void ShowAddedAppointment(Appointment? newAppointment)
        {
            if (newAppointment == null)
            {
                Console.WriteLine("No appointment details to display.");
                return;
            }

            Console.WriteLine("Added Appointment Details:");
            Console.WriteLine($"ID: {newAppointment.Id}");
            Console.WriteLine($"Patient Name: {newAppointment.PatientName}");
            Console.WriteLine($"Patient Age: {newAppointment.PatientAge}");
            Console.WriteLine($"Appointment Date: {newAppointment.AppointmentDate:yyyy-MM-dd HH:mm}");
            Console.WriteLine($"Reason: {newAppointment.Reason}");
            Console.WriteLine("------------------------------------");
        }

        private void ShowMainMenu()
        {
            Console.WriteLine("------Welcome to the Appointment Management System--------");
            Console.WriteLine("1. Add Appointment");
            Console.WriteLine("2. Search Appointments");
            Console.WriteLine("0. Exit");
        }
        private int GetUserChoice() 
        { 
            while(true)
            {
                Console.Write("Please enter your choice: ");
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int choice))
                {
                    return choice;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }
        }

        private Appointment GetAppointmentDetails()
        {
            Console.WriteLine("Enter Appointment Details");

            Console.Write("Patient Name: ");
            string patientName = Console.ReadLine() ?? string.Empty;

            int patientAge;
            while (true)
            {
                Console.Write("Patient Age: ");
                if (int.TryParse(Console.ReadLine(), out patientAge) && patientAge > 0)
                    break;

                Console.WriteLine("Invalid age. Please enter a valid positive number.");
            }

            DateTime appointmentDate;
            while (true)
            {
                Console.Write("Appointment Date (yyyy-MM-dd HH:mm): ");
                if (DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd HH:mm", null, System.Globalization.DateTimeStyles.None, out appointmentDate))
                    break;

                Console.WriteLine("Invalid date format. Please use yyyy-MM-dd HH:mm format.");
            }

            Console.Write("Reason: ");
            string reason = Console.ReadLine() ?? string.Empty;

            return new Appointment(patientName, patientAge, appointmentDate, reason);
        }

        private void ShowSearchResults(List<Appointment>? appointments)
        {
            if (appointments == null || appointments.Count == 0)
            {
                Console.WriteLine("No appointments found.");
                return;
            }
            Console.WriteLine("Search Results:");
            foreach (var appointment in appointments)
            {
                Console.WriteLine($"ID: {appointment.Id}, Patient Name: {appointment.PatientName}, Age: {appointment.PatientAge}, Date: {appointment.AppointmentDate:yyyy-MM-dd HH:mm}, Reason: {appointment.Reason}");
            }
        }
    }
}
