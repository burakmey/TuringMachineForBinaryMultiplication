using System.Collections.Generic;

namespace TuringMachineForSquareOfNumber
{
    internal class TuringMachine
    {
        List<char> memory;
        int controlHead = 1;
        int nextState = 0;
        bool isFinished = false;
        List<char> Memory { get => memory; set => memory = value; }
        public void RunTuringMachine(List<char> newMemory)
        {
            Memory = newMemory;
            while (!isFinished)
            {
                State(nextState);
            }
        }
        public string GetMemory()
        {
            string copyMemory = new string(Memory.ToArray());
            return copyMemory;
        }
        void State(int currentState)
        {
            char c = Memory[controlHead];
            switch (currentState)
            {
                case 0: //***************************************************************************************
                    //Go right and stay this state until read '='.
                    if (c == '0' || c == '1' || c == '*') // (0/0,R) (1/1,R) (*/*,R)
                    {
                        controlHead++;
                        nextState = currentState;
                    }
                    else if (c == '=') // (=/=,L)
                    {
                        controlHead--;
                        nextState = 1;
                    }
                    break;
                case 1: //***************************************************************************************
                    //Go left and stay this state until read '1'.
                    if (c == 'X') // (X/X,L)
                    {
                        controlHead--;
                        nextState = currentState;
                    }
                    else if (c == '0') // (0/X,L)
                    {
                        Memory[controlHead] = 'X';
                        controlHead--;
                        nextState = currentState;
                    }
                    else if (c == '1') // (1/1,L)
                    {
                        controlHead--;
                        nextState = 2;
                    }
                    break;
                case 2: //***************************************************************************************
                    //Go left and stay this state until read 'B'.
                    if (c == '0' || c == '1' || c == '*') // (0/0,R) (1/1,R) (*/*,R)
                    {
                        controlHead--;
                        nextState = currentState;
                    }
                    else if (c == 'B') // (B/B,R)
                    {
                        controlHead++;
                        nextState = 3;
                    }
                    break;
                case 3: //***************************************************************************************
                    if (c == '0') // (0/O,R) Read zero, write O, go right.
                    {
                        Memory[controlHead] = 'O';
                        controlHead++;
                        nextState = 4;
                    }
                    else if (c == '1') // (1/I,R) Read one, write I, go right.
                    {
                        Memory[controlHead] = 'I';
                        controlHead++;
                        nextState = 6;
                    }
                    else if(c == '*') // (*/*,R)
                    {
                        controlHead++;
                        nextState = 8;
                    }
                    break;
                case 4: //***************************************************************************************
                    //Go right and stay this state until read 'B'.
                    if (c == '0' || c == '1' || c == '*' || c == 'X' || c == '=' || c == '+') // (0/0,R) (1/1,R) (*/*,R) (X/X,R) (=/=,R) (+/+,R)
                    {
                        controlHead++;
                        nextState = currentState;
                    }
                    else if (c == 'B') // (B/0,L) Read blank, write zero, go left.
                    {
                        Memory[controlHead] = '0';
                        Memory.Add('B'); //Assuming both left and right terminals are finite in this Turing Machine.
                        controlHead--;
                        nextState = 5;
                    }
                    break;
                case 5: //***************************************************************************************
                    //Go left and stay this state until read 'O'(not zero).
                    if (c == '0' || c == '1' || c == '*' || c == 'X' || c == '=' || c == '+') // (0/0,L) (1/1,L) (*/*,L) (X/X,L) (=/=,L) (+/+,L)
                    {
                        controlHead--;
                        nextState = currentState;
                    }
                    else if (c == 'O') // (O/0,R) Read O, write zero, go right.
                    {
                        Memory[controlHead] = '0';
                        controlHead++;
                        nextState = 3;
                    }
                    break;
                case 6: //***************************************************************************************
                    //Go right and stay this state until read 'B'.
                    if (c == '0' || c == '1' || c == '*' || c == 'X' || c == '=' || c == '+') // (0/0,R) (1/1,R) (*/*,R) (X/X,R) (=/=,R) (+/+,R)
                    {
                        controlHead++;
                        nextState = currentState;
                    }
                    else if (c == 'B') // (B/1,L) Read blank, write one, go left.
                    {
                        Memory[controlHead] = '1';
                        Memory.Add('B'); //Assuming both left and right terminals are finite in this Turing Machine.
                        controlHead--;
                        nextState = 7;
                    }
                    break;
                case 7: //***************************************************************************************
                    //Go left and stay this state until read 'I'(not one).
                    if (c == '0' || c == '1' || c == '*' || c == 'X' || c == '=' || c == '+') // (0/0,L) (1/1,L) (*/*,L) (X/X,L) (=/=,L) (+/+,L)
                    {
                        controlHead--;
                        nextState = currentState;
                    }
                    else if (c == 'I') // (I/1,R) Read I, write one, go right.
                    {
                        Memory[controlHead] = '1';
                        controlHead++;
                        nextState = 3;
                    }
                    break;
                case 8: //***************************************************************************************
                    //Go right and stay this state until read 'X' or '='.
                    if (c == '0' || c == '1') // (0/0,R) (1/1,R)
                    {
                        controlHead++;
                        nextState = currentState;
                    }
                    else if (c == 'X') // (X/Y,R)
                    {
                        Memory[controlHead] = 'Y';
                        controlHead++;
                        nextState = 9;
                    }
                    else if (c == '=') // (=/=,L)
                    {
                        controlHead--;
                        nextState = 11;
                    }
                    break;
                case 9: //***************************************************************************************
                    //Go right and stay this state until read 'B'.
                    if (c == '0' || c == '1' || c == 'X' || c == '=' || c == '+') // (0/0,R) (1/1,R) (X/X,R) (=/=,R) (+/+,R)
                    {
                        controlHead++;
                        nextState = currentState;
                    }
                    else if (c == 'B') // (B/0,L)
                    {
                        Memory[controlHead] = '0';
                        Memory.Add('B'); //Assuming both left and right terminals are finite in this Turing Machine.
                        controlHead--;
                        nextState = 10;
                    }
                    break;
                case 10: //***************************************************************************************
                    //Go left and stay this state until read 'Y'.
                    if (c == '0' || c == '1' || c == 'X' || c == '=' || c == '+') // (0/0,L) (1/1,L) (X/X,L) (=/=,L) (+/+,L)
                    {
                        controlHead--;
                        nextState = currentState;
                    }
                    else if (c == 'Y') // (Y/X,R)
                    {
                        Memory[controlHead] = 'X';
                        controlHead++;
                        nextState = 8;
                    }
                    break;
                case 11: //***************************************************************************************
                    //Go left and stay this state until read '1'.
                    if (c == 'X') // (X/X,L)
                    {
                        controlHead--;
                        nextState = currentState;
                    }
                    else if (c == '1')
                    {
                        Memory[controlHead] = 'X';
                        controlHead--;
                        nextState = 12;
                    }
                    break;
                case 12: //***************************************************************************************
                    //If there is any digit to read go back.
                    if (c == '0' || c == '1') // (0/0,R) (1/1,R)
                    {
                        controlHead++;
                        nextState = 13;
                    }
                    else if (c == '*') // (*/*,L)
                    {
                        controlHead--;
                        nextState = 15;
                    }
                    break;
                case 13: //***************************************************************************************
                    //Go right and stay this state until read 'B'.
                    if (c == '0' || c == '1' || c == '*' || c == 'X' || c == '=' || c == '+') // (0/0,R) (1/1,R) (*/*,R) (X/X,R) (=/=,R) (+/+,R)
                    {
                        controlHead++;
                        nextState = currentState;
                    }
                    else if (c == 'B') // (B/+,L)
                    {
                        Memory[controlHead] = '+';
                        Memory.Add('B'); //Assuming both left and right terminals are finite in this Turing Machine.
                        controlHead--;
                        nextState = 14;
                    }
                    break;
                case 14: //***************************************************************************************
                    //Go left and stay this state until read 'X'.
                    if (c == '0' || c == '1' || c == '*' || c == '=' || c == '+') // (0/0,L) (1/1,L) (*/*,L) (=/=,L) (+/+,L)
                    {
                        controlHead--;
                        nextState = currentState;
                    }
                    else if (c == 'X')
                    {
                        controlHead--;
                        nextState = 1;
                    }
                    break;
                case 15: //***************************************************************************************
                    //Go left and stay this state until read 'B'.
                    if (c == '0' || c == '1') // (0/0,L) (1/1,L)
                    {
                        controlHead--;
                        nextState = currentState;
                    }
                    else if (c == 'B') // (B/B,R)
                    {
                        controlHead++;
                        nextState = 16;
                    }
                    break;
                case 16: //***************************************************************************************
                    //Remove elements from 'B' to '='.
                    if (c == '0' || c == '1' || c == '*' || c == 'X') // (0/B,R) (1/B,R) (*/B,R) (X/B,R)
                    {
                        Memory.RemoveAt(controlHead);
                        nextState = currentState;
                    }
                    else if (c == '=') // (=/B,R)
                    {
                        Memory.RemoveAt(controlHead);
                        nextState = 17;
                    }
                    break;
                case 17: //***************************************************************************************
                    //Go right and stay this state until read '+'.
                    if (c == '0' || c == '1') // (0/0,R) (1/1,R)
                    {
                        controlHead++;
                        nextState = currentState;
                    }
                    else if (c == '+') // (+/+,R)
                    {
                        controlHead++;
                        nextState = 18;
                    }
                    else if (c == 'B') // (B/B,L)
                    {
                        controlHead--;
                        nextState = 40;
                    }
                    break;
                case 18: //***************************************************************************************
                    //Go right and stay this state until read '+' or 'B'.
                    if (c == '0' || c == '1') // (0/0,R) (1/1,R)
                    {
                        controlHead++;
                        nextState = currentState;
                    }
                    else if (c == '+' || c == 'B') // (+/+,L) (B/B,L)
                    {
                        controlHead--;
                        nextState = 19;
                    }
                    break;
                case 19: //***************************************************************************************
                    if (c == '0') // (0/O,L) Read zero, write O, go left.
                    {
                        Memory[controlHead] = 'O';
                        controlHead--;
                        nextState = 20;
                    }
                    else if (c == '1') // (1/I,L) Read one, write I, go left.
                    {
                        Memory[controlHead] = 'I';
                        controlHead--;
                        nextState = 24;
                    }
                    else if (c == '+') // (+/X,L)
                    {
                        Memory[controlHead] = 'X';
                        controlHead--;
                        nextState = 34;
                    }
                    break;
                case 20: //***************************************************************************************
                    //Go left and stay this state until read '+'.
                    if (c == '0' || c == '1') // (0/0,L) (1/1,L)
                    {
                        controlHead--;
                        nextState = currentState;
                    }
                    else if (c == '+') // (+/+,L)
                    {
                        controlHead--;
                        nextState = 21;
                    }
                    break;
                case 21: //***************************************************************************************
                    //Go left and stay this state until read '0' or '1'.
                    if (c == 'X') // (X/X,L)
                    {
                        controlHead--;
                        nextState = currentState;
                    }
                    else if (c == '0') // (0/X,R)
                    {
                        Memory[controlHead] = 'X';
                        controlHead++;
                        nextState = 22;
                    }
                    else if (c == '1') // (1/X,R)
                    {
                        Memory[controlHead] = 'X';
                        controlHead++;
                        nextState = 23;
                    }
                    else if (c == 'B') // (B/B,R)
                    {
                        controlHead++;
                        nextState = 31;
                    }
                    break;
                case 22: //***************************************************************************************
                    //Go right and stay this state until read 'O'(not zero).
                    if (c == '0' || c == '1' || c == '+' || c == 'X') // (0/0,R) (1/1,R) (+/+,R) (X/X,R)
                    {
                        controlHead++;
                        nextState = currentState;
                    }
                    else if (c == 'O') // (O/0,L) Read O, write zero, go left.
                    {
                        Memory[controlHead] = '0';
                        controlHead--;
                        nextState = 19;
                    }
                    break;
                case 23: //***************************************************************************************
                    //Go right and stay this state until read 'O'(not zero).
                    if (c == '0' || c == '1' || c == '+' || c == 'X') // (0/0,R) (1/1,R) (+/+,R) (X/X,R)
                    {
                        controlHead++;
                        nextState = currentState;
                    }
                    else if (c == 'O') // (O/1,L) Read O, write one, go left.
                    {
                        Memory[controlHead] = '1';
                        controlHead--;
                        nextState = 19;
                    }
                    break;
                case 24: //***************************************************************************************
                    //Go left and stay this state until read '+'.
                    if (c == '0' || c == '1') // (0/0,L) (1/1,L)
                    {
                        controlHead--;
                        nextState = currentState;
                    }
                    else if (c == '+') // (+/+,L)
                    {
                        controlHead--;
                        nextState = 25;
                    }
                    break;
                case 25: //***************************************************************************************
                    //Go left and stay this state until read '0' or '1'.
                    if (c == 'X') // (X/X,L)
                    {
                        controlHead--;
                        nextState = currentState;
                    }
                    else if (c == '0') // (0/X,R)
                    {
                        Memory[controlHead] = 'X';
                        controlHead++;
                        nextState = 26;
                    }
                    else if (c == '1') // (1/X,R)
                    {
                        Memory[controlHead] = 'X';
                        controlHead++;
                        nextState = 27;
                    }
                    else if (c == 'B') // (B/B,R)
                    {
                        controlHead++;
                        nextState = 31;
                    }
                    break;
                case 26: //***************************************************************************************
                    //Go right and stay this state until read 'I'(not one).
                    if (c == '0' || c == '1' || c == '+' || c == 'X') // (0/0,R) (1/1,R) (+/+,R) (X/X,R)
                    {
                        controlHead++;
                        nextState = currentState;
                    }
                    else if (c == 'I') // (I/1,L) Read I, write one, go left.
                    {
                        Memory[controlHead] = '1';
                        controlHead--;
                        nextState = 19;
                    }
                    break;
                case 27: //***************************************************************************************
                    //Go right and stay this state until read 'I'(not one).
                    if (c == '0' || c == '1' || c == '+' || c == 'X') // (0/0,R) (1/1,R) (+/+,R) (X/X,R)
                    {
                        controlHead++;
                        nextState = currentState;
                    }
                    else if (c == 'I') // (I/O,L) Read I, write O, go left.
                    {
                        Memory[controlHead] = 'O';
                        controlHead--;
                        nextState = 28;
                    }
                    break;
                case 28: //***************************************************************************************
                    if (c == '1') // (1/0,L)
                    {
                        Memory[controlHead] = '0';
                        controlHead--;
                        nextState = currentState;
                    }
                    else if (c == '0') // (0/1,R)
                    {
                        Memory[controlHead] = '1';
                        controlHead++;
                        nextState = 29;
                    }
                    else if (c == '+') // (+/1,L)
                    {
                        Memory[controlHead] = '1';
                        controlHead--;
                        nextState = 30;
                    }
                    break;
                case 29: //***************************************************************************************
                    //Go right and stay this state until read 'O'(not zero).
                    if (c == '0' || c == '1') // (0/0,R) (1/1,R)
                    {
                        controlHead++;
                        nextState = currentState;
                    }
                    else if (c == 'O') // (O/0,L) Read O, write zero, go left.
                    {
                        Memory[controlHead] = '0';
                        controlHead--;
                        nextState = 19;
                    }
                    break;
                case 30: //***************************************************************************************
                    if (c == 'X') // (X/+,R)
                    {
                        Memory[controlHead] = '+';
                        controlHead++;
                        nextState = 29;
                    }
                    break;
                case 31: //***************************************************************************************
                    //Go right and stay this state until read 'O'(not zero) or 'I'(not one).
                    if (c == '0' || c == '1' || c == '+' || c == 'X') // (0/0,R) (1/1,R) (+/+,R) (X/X,R)
                    {
                        controlHead++;
                        nextState = currentState;
                    }
                    else if (c == 'O') // (O/0,L) Read O, write zero, go left.
                    {
                        Memory[controlHead] = '0';
                        controlHead--;
                        nextState = 32;
                    }
                    else if (c == 'I') // (I/1,L) Read I, write one, go left.
                    {
                        Memory[controlHead] = '1';
                        controlHead--;
                        nextState = 32;
                    }
                    break;
                case 32: //***************************************************************************************
                    //Go left and stay this state until read 'B'.
                    if (c == '0' || c == '1' || c == '+' || c == 'X') // (0/0,L) (1/1,L) (+/+,L) (X/X,L)
                    {
                        controlHead--;
                        nextState = currentState;
                    }
                    else if (c == 'B') // (B/B,R)
                    {
                        controlHead++;
                        nextState = 33;
                    }
                    break;
                case 33: //***************************************************************************************
                    //Remove elements from 'B' to '1'.
                    if (c == 'X' || c == '+') // (X/B,R) (+/B,R)
                    {
                        Memory.RemoveAt(controlHead); //Assuming both left and right terminals are finite in this Turing Machine.
                        nextState = currentState;
                    }
                    else if (c == '1') // (1/1,R)
                    {
                        controlHead++;
                        nextState = 17;
                    }
                    break;
                case 34: //***************************************************************************************
                    if (c == 'X') // (X/X,L)
                    {
                        controlHead--;
                        nextState = currentState;
                    }
                    else if (c == '0') // (0/X,R)
                    {
                        Memory[controlHead] = 'X';
                        controlHead++;
                        nextState = 35;
                    }
                    else if (c == '1') // (1/X,R)
                    {
                        controlHead++;
                        nextState = 37;
                    }
                    else if (c == 'B') // (B/B,R)
                    {
                        controlHead++;
                        nextState = 39;
                    }
                    break;
                case 35: //***************************************************************************************
                    if (c == 'X') // (X/X,R)
                    {
                        controlHead++;
                        nextState = currentState;
                    }
                    else if (c == '0' || c == '1') // (0/0,L) (1/1,L)
                    {
                        controlHead--;
                        nextState = 36;
                    }
                    break;
                case 36: //***************************************************************************************
                    if (c == 'X') // (X/0,L)
                    {
                        Memory[controlHead] = '0';
                        controlHead--;
                        nextState = 34;
                    }
                    break;
                case 37: //***************************************************************************************
                    if (c == 'X') // (X/X,R)
                    {
                        controlHead++;
                        nextState = currentState;
                    }
                    else if (c == '0' || c == '1') // (0/0,L) (1/1,L)
                    {
                        controlHead--;
                        nextState = 38;
                    }
                    break;
                case 38: //***************************************************************************************
                    if (c == 'X') // (X/1,L)
                    {
                        Memory[controlHead] = '1';
                        controlHead--;
                        nextState = 34;
                    }
                    break;
                case 39: //***************************************************************************************
                    //Remove elements from 'B' to '1'.
                    if (c == 'X') // (X/B,R) 
                    {
                        Memory.RemoveAt(controlHead); //Assuming both left and right terminals are finite in this Turing Machine.
                        nextState = currentState;
                    }
                    else if (c == '1') // (1/1,R)
                    {
                        controlHead++;
                        nextState = 17;
                    }
                    break;
                case 40: //***************************************************************************************
                    //Turing Machine's calculation is finished!
                    isFinished = true;
                    break;
                default: //***************************************************************************************
                    break;
            }
        }
    }
}
