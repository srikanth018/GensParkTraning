using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentApp.Exceptions
{
    public class EmptyEntityException : Exception
    {
        private string _message = "No data Found";
        public EmptyEntityException(string errorMessage)
        {
            _message = errorMessage;
        }
        public override string Message => _message;

    }
}
