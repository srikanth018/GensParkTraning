﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentApp.Models;

namespace AppointmentApp.Interfaces
{
    public interface IAppointmentService
    {
        Appointment? AddAppointment(Appointment appointment);
        List<Appointment>? SearchAppointments(AppointmentSearchModel appointmentSearchModel);
    }
}
