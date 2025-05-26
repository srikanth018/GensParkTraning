using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTasksApp
{
    public class PerformArithmeticOperation
    {
        public void Calculate(int num1, int num2, string operation)
        {
            int result = 0;
            switch (operation.ToLower())
            {
                case "+":
                    result = num1 + num2;
                    break;
                case "-":
                    result = num1 - num2;
                    break;
                case "*":
                    result = num1 * num2;
                    break;
                case "/":
                    if (num2 != 0)
                        result = num1 / num2;
                    else
                        Console.WriteLine("Cannot divide by zero.");
                    return; 
                default:
                    Console.WriteLine("Invalid operation. Please use add, subtract, multiply, or divide.");
                    return; 
            }
            Console.WriteLine($"The result of {operation}ing {num1} and {num2} is: {result}");
        }
    }
}
