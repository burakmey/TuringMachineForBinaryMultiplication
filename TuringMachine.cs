﻿using System.Collections.Generic;

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
                        Memory.Add('B'); // Assuming both left and right terminals are finite in this Turing Machine.
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
                        Memory.Add('B'); // Assuming both left and right terminals are finite in this Turing Machine.
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
                        Memory.Add('B'); // Assuming both left and right terminals are finite in this Turing Machine.
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
                    //If there is any digit to read go back
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
                        Memory.Add('B'); // Assuming both left and right terminals are finite in this Turing Machine.
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
                    if (true)
                    {
                        //isFinished = true;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}