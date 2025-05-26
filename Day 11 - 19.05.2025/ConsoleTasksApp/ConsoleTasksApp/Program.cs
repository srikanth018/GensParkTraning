using System;

namespace ConsoleTasksApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            SingleUserInput singleUserInput = new SingleUserInput();


            GetArrayFromUser getArrayFromUser = new GetArrayFromUser();
            int[] inputArr = [];

            //1.
            //GreetUser greetUser = new GreetUser();
            //string name = (string)singleUserInput.GetInput("Please Enter your name: ", "string");
            //greetUser.Greet(name);

            //2.
            //FindLargestNumber findLargestNumber = new FindLargestNumber();
            //int num1 = (int)singleUserInput.GetInput("Please Enter First Number: ", "int");
            //int num2 = (int)singleUserInput.GetInput("Please Enter Second Number: ", "int");
            //findLargestNumber.LargestNumber(num1, num2);

            //3.
            //PerformArithmeticOperation performArithmeticOperation = new PerformArithmeticOperation();
            //int num1 = (int)singleUserInput.GetInput("Please Enter First Number: ", "int");
            //int num2 = (int)singleUserInput.GetInput("Please Enter Second Number: ", "int");
            //string operation = (string)singleUserInput.GetInput("Please Enter Operation (+, -, *, /): ", "string");
            //performArithmeticOperation.Calculate(num1, num2, operation);


            //4.
            //LoginValidation loginValidation = new LoginValidation();
            //string username = (string)singleUserInput.GetInput("Please Enter your username: ", "string");
            //string password = (string)singleUserInput.GetInput("Please Enter your password: ", "string");
            //loginValidation.ValidateLogin(username, password);

            //5.
            //CountDivisible countDivisible = new CountDivisible();
            //Console.WriteLine("Heyy!! Please Enter the array values");
            //inputArr = getArrayFromUser.UserArrayInput();
            //countDivisible.CountDivisibleBySeven(inputArr);

            //6.
            //CountFrequency countFrequency = new CountFrequency();
            //Console.WriteLine("Heyy!! Please Enter the array values");
            //inputArr = [];
            //inputArr = getArrayFromUser.UserArrayInput();
            //countFrequency.CountFrequencyOfElements(inputArr);

            ////7.
            //ArrayLeftRotate arrayLeftRotate = new ArrayLeftRotate();
            //Console.WriteLine("Heyy!! Please Enter the array values");
            //inputArr = [];
            //inputArr = getArrayFromUser.UserArrayInput();
            //arrayLeftRotate.Rotate(inputArr);

            //8.
            //MergeTwoArray mergeTwoArray = new MergeTwoArray();
            //Console.WriteLine("Please Enter First Array Values");
            //int[] arr1 = getArrayFromUser.UserArrayInput();
            //Console.WriteLine("Please Enter Second Array Values");
            //int[] arr2 = getArrayFromUser.UserArrayInput();
            //mergeTwoArray.MergeArray(arr1,arr2);

            //9.
            //SecretWordGame secretWordGame = new SecretWordGame();
            //string guess = (string)singleUserInput.GetInput("Please Enter the 4 letter word: ", "string");
            //secretWordGame.CompareWords(guess);

            //10.
            //ValidRow validRow = new ValidRow();
            //Console.WriteLine("Heyy!! Please Enter the array values");
            //inputArr = [];
            //inputArr = getArrayFromUser.UserArrayInput();
            //validRow.isValid(inputArr);

            //11.
            //ValidateAllRows validateAllRows = new ValidateAllRows();
            //validateAllRows.ValidateRows();

            //12.
            EncryptDecrypt encryptDecrypt = new EncryptDecrypt();
            string text = (string)singleUserInput.GetInput("Please Enter the text to encrypt: ", "string");
            encryptDecrypt.ShowEncryptedAndDecrypted(text);
        }
    }
}

