namespace BankApp.Misc
{ 
    public static class Generator
    {
        public static string GenerateAccountNumber()
        {
            var random = new Random();
            var accountNumber = new char[16];
            for (int i = 0; i < accountNumber.Length; i++)
            {
                accountNumber[i] = (char)('0' + random.Next(0, 10));
            }
            return new string(accountNumber);
        }
    }
}