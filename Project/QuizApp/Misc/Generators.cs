using BCrypt.Net;

namespace QuizApp.Misc
{
    public static class Generators
    {
        private static readonly Random _random = new Random();

        public static string GenerateID(string prefix)
        {
            var timePart = DateTime.UtcNow.ToString("HHmmss");
            var randomSuffix = _random.Next(10, 100);
            return $"{prefix}{timePart}{randomSuffix}";
        }
        public static string GenerateHashedPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be null or empty");

            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(hashedPassword))
                throw new ArgumentException("Password and hashed password cannot be null or empty");

            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

    }
}