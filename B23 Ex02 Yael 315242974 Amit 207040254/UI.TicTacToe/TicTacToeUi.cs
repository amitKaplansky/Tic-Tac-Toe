using System;
using System.Data.Common;
using System.Drawing;
using System.Security.Cryptography;

namespace UI.TicTacToe;

class TicTacToeUi
{
   
    Engine.TicTacToe.Game m_game = new Engine.TicTacToe.Game();
    public void PlayGame()
    {
       
        String input ="";
        int column = 0 , row = 0;

        initGame();
        Console.WriteLine("Let's start play,\nFY if you want to exit press 'Q'.");
        while (!input.Equals("Q"))
        {
            if (!getValidRowAndColumn(ref row, ref column))
                break;

           

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
        
        return true;   
    }
    private void initGame()
    {
        String input;
        int boardSize;
        bool PlayerTowIsComputer;

        Console.WriteLine("Welcome to TicTacTock!");
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
        String input = "";
        bool validInput = false;

        while (!validInput)
        {
            input = Console.ReadLine();
            validInput = isInputValid(input, i_checkPlayerType);
            if (!validInput)
                Console.WriteLine("Your input is invalid please try again.");
        }
        return input;
    }
    private bool isInputValid(String i_input, bool i_checkPlayerType)
    {
        //check input is int
        foreach (char c in i_input)
            if (!char.IsDigit(c))
                return false;

        if (!i_checkPlayerType)
        {
            if (i_input.Length != 1)
                return false;

            char digit = i_input[0];
            return (digit == '1' || digit == '2');
        }

        return true;
    }
    private bool parseInputToBool(String input) { 
        if(input.Equals("1"))
            return true;
        else
            return false;
    
    }
}