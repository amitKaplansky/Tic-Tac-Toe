using System;
using System.Data.Common;
using System.Drawing;
using System.Security.Cryptography;
using Engine.TicTacToe;
using Ex02.ConsoleUtils;
namespace UI.TicTacToe;

class TicTacToeUi
{
    Game m_game = new Engine.TicTacToe.Game();
    public void PlayGame()
    {
       
        String input ="";
        int col = 0 , row = 0;
        bool isHummen = false, validMove; 
        initGame();
        Console.WriteLine("Let's start play,\nFY if you want to exit press 'Q'.");
        while (!input.Equals("Q"))
        {
            Screen.Clear();//nned to think about sleep is the next move is a computer...
            //CHECK THAT IF THE PLAYER IS HUMEN
            if (isHummen)
            {
                if(!getValidRowAndColumn(ref row, ref col))
                    break;

               validMove =  m_game.NextMove(row, col);
            }

            else
            {
                m_game.computerMove();
                validMove = true;
            }

            if (!validMove)
                Console.WriteLine("Invalid move please try again");
         
            printBoard();

        }

    }

    private bool getValidRowAndColumn(ref int o_row,ref int o_column)
    {
        String input;
        Console.WriteLine("Please enter column:");
        input = getValidInpt();
        if (input.Equals("Q"))
            return false;

        o_column = int.Parse(input);

        Console.WriteLine("Please enter row:");
        input = getValidInpt();
        if (input.Equals("Q"))
            return false;

        o_row = int.Parse(input);
        Console.WriteLine();
        return true;   
    }
    private void initGame()
    {
        String input;
        int boardSize;
        bool PlayerTowIsComputer;

        Console.WriteLine("Welcome to TicTacToce!");
        Console.WriteLine("Please enter the size of the board you want(need to be more then 3 and less then 9):");
        
        input = getValidInpt();
        boardSize = int.Parse(input);
        
        Console.WriteLine("Do you want to play against the computer or friend?(computer = 1/ friend = 2):");
        
        input = getValidInpt(true);
        PlayerTowIsComputer = parseInputToBool(input);
        m_game.InitislizeGame(boardSize, PlayerTowIsComputer);
    }
    private String getValidInpt(bool i_checkPlayerType = false)
    {
        String input = "" ,errorMessage ="";
        bool validInput = false;

        while (!validInput)
        {
            input = Console.ReadLine();
            validInput = isInputValid(input, i_checkPlayerType, ref errorMessage);
            if (!validInput)
                Console.WriteLine(errorMessage);
        }
        return input;
    }
    private bool isInputValid(String i_input, bool i_checkPlayerType, ref string o_errorMessage)
    {
        //check input is int
        if (i_input.Equals(""))
        {
            o_errorMessage = "You must enter input and then press enter, Try again.";
            return false;
        }
        foreach (char c in i_input)
            if (!char.IsDigit(c))
            {
                o_errorMessage = "Invalid input, your Input must only contain numbers, Try again.";
                return false;
            }

        if (i_checkPlayerType)
        {
            if (i_input.Length != 1)
            {
                o_errorMessage = "In this section you must enter only one digit, Try again.";
                return false;
            }
            char digit = i_input[0];
            if(digit != '1' && digit != '2')
            {
                o_errorMessage = "You can only enter the digit 1 or 2, Try again.";
                return false;
            }
        }

        return true;
    }
    private bool parseInputToBool(String input) { 
        if(input.Equals("1"))
            return true;
        else
            return false;
    
    }

    private void printBoard()
    {
        Symbol[,] board = m_game.CopyBoard();
        int size = board.GetLength(0);

        for (int i = 0; i < size; i++)
        {
            Console.Write("  " + (i + 1) +" ");
        }
        Console.Write("\n");
        for (int i = 0; i < size; i++)
        {
            Console.Write((i + 1) + "|");
            for (int j = 0; j < size; j++)
            {
                if (board[i, j] != Symbol.Empty)
                    Console.Write(board[i, j] + "  |");
                else
                    Console.Write("   |");
                
            }
            Console.Write("\n");

            for (int k = 0; k < size; k++)
                Console.Write("====");

            Console.Write("=\n");
        }
    }
}