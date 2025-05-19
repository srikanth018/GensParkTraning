using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTasksApp
{
    public class SecretWordGame
    {
        private readonly string Secret = "GAME";

        //string GetStringFromUser()
        //{
        //    string input = "";
        //    while (true)
        //    {
        //        input = "";
        //        Console.Write("Please Enter the 4 letter word: ");
        //        input = Console.ReadLine();
        //        if(input.Length != 4)
        //        {
        //            Console.WriteLine("Please Enter the valid input. Input should contains only 4 letter");
        //            continue;
        //        }
        //        break;
        //    }
        //    return input;
        //}

        public void CompareWords(string Guess)
        {
            //string Guess = GetStringFromUser();
            if (Guess.Length != 4)
            {
                Console.WriteLine("Please Enter the valid input. Input should contains only 4 letter");
                return;
            }
            Guess = Guess.ToUpper();

            int bulls = 0;
            int cows = 0;
            for(int i=0; i < 4; i++)
            {
                if (Guess[i] == Secret[i]) bulls++;
                else if (Secret.Contains(Guess[i])) cows++;
            }

            Console.WriteLine($"{bulls} Bulls and {cows} Cows");
        }
    }
}
