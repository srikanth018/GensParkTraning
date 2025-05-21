using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentApp.Models;

namespace AppointmentApp.Repositories
{
    public class AppointmentRepository : Repository<int, Appointment>
    {
        private int _InitialId = 101;
        public override int GenerateId()
        {
            int newId = (_items.Count == 0) ? _InitialId : _items.Max(x => x.Id) + 1;
            return newId;
        }

        public override Appointment GetById(int id)
        {
            var appointment = _items.FirstOrDefault(x => x.Id.Equals(id));
            if(appointment == null)
            {
                throw new KeyNotFoundException("No Appointment found");
            }
            return appointment;
        }

    }
}
