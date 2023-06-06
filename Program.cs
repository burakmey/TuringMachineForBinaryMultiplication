using System.Collections.Generic;
using System.Windows.Forms;

namespace TuringMachineForSquareOfNumber
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TuringMachine turingMachine = new TuringMachine();
            List<char> memory = new List<char>();
            string memoryCopy = "B1011*1011=B";
            for (int i = 0; i < memoryCopy.Length; i++)
            {
                memory.Add(memoryCopy[i]);
            }
            turingMachine.RunTuringMachine(memory);
            memoryCopy = turingMachine.GetMemory();
            MessageBox.Show($"{memoryCopy}");
        }
    }
}
