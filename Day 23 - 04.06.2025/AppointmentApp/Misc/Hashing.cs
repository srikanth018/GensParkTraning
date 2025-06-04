using AppointmentApp.Models;
using BCrypt.Net;

namespace AppointmentApp.Misc
{
    public static class Hashing
    {
        public static EncryptModel HashPassword(EncryptModel encryptModel)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(encryptModel.Data);
            return new EncryptModel { HashedData = hash };
        }

        public static bool VerifyPassword(EncryptModel encryptModel)
        {
            var isMatched = BCrypt.Net.BCrypt.Verify(encryptModel.Data, encryptModel.HashedData);
            return isMatched;
        }
    }
}