using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TuringMachineForSquareOfNumber
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string memorySet, input, result;
            int number;
            bool isCorrectInput;
            TuringMachine turingMachine = new TuringMachine();
        Start:
            isCorrectInput = false;
            do
            {
                Console.Write("Enter a number to get its square : ");
                input = Console.ReadLine();
                if (int.TryParse(input, out number) && number >= 0)
                {
                    isCorrectInput = true;
                }
                else
                {
                    Console.WriteLine("Make sure you entered nonnegative integer number.");
                    Console.WriteLine("Press any to enter another value.");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            while (!isCorrectInput);
            input = Convert.ToString(number, 2);
            memorySet = "B" + input + "*" + input + "=B";
            //memorySet = "B11001*1000=B";//Test 
            List<char> memory = new List<char>(memorySet);
            turingMachine.RunTuringMachine(memory);
            memorySet = turingMachine.GetMemory();
            memorySet = memorySet.Remove(0, 1);
            memorySet = memorySet.Remove(memorySet.Length - 1, 1);
            result = Convert.ToInt32(memorySet, 2).ToString();
            Console.WriteLine($"Square of {number} is {result}");
            Console.WriteLine("Press 'esc' to exit or press any to recalculate.");
            if (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                Console.Clear();
                goto Start;
            }
        }
    }
}
