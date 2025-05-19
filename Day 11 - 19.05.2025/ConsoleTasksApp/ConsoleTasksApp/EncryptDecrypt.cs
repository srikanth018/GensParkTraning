using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTasksApp
{
    public class EncryptDecrypt
    {
        public string Encrypt(string text)
        {
            char[] textArray = text.ToCharArray();
            for (int i = 0; i < textArray.Length; i++)
            {
                char c = textArray[i];
                textArray[i] = (char)('a' + (c - 'a' + 3) % 26);
            }
            return new string(textArray);
        }

        public string Decrypt(string text)
        {
            char[] textArray = text.ToCharArray();
            for (int i = 0; i < textArray.Length; i++)
            {
                char c = textArray[i];
                textArray[i] = (char)('a' + (c - 'a' - 3 + 26) % 26);
            }
            return new string(textArray);
        }

        public void ShowEncryptedAndDecrypted(string input)
        {
            string encrypted = Encrypt(input);
            string decrypted = Decrypt(encrypted);

            Console.WriteLine($"Input:     {input}");
            Console.WriteLine($"Encrypted: {encrypted}");
            Console.WriteLine($"Decrypted: {decrypted}");
        }
    }
}
