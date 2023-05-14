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
        bool validMove = false , isGameOver = false;

        isGameOver = initGame();
        
        Console.WriteLine("Let's start to play Reversed Tic Tac Toe,\nFY if you want to exit press 'Q'.");

        while (!input.Equals("Q") && !isGameOver)
        {
            Screen.Clear();
                //sleep?
            //CHECK THAT IF THE PLAYER IS HUMEN
            printBoard();

            if (!m_game.IsPlayerTurnIsComputer())
            {
                do
                {
                    isGameOver = getValidRowAndCol(ref row, ref col);

                    if (isGameOver)
                    {
                        break;
                    }


                    validMove = m_game.NextMove(row - 1, col - 1);

                    if (!validMove)
                        Console.WriteLine("Invalid move please try again");

                } while (!validMove);
            }
            else
            { 
                m_game.NextMove();
            }
             
            if(m_game.DidPlayerLose(row - 1 , col - 1))
            {
                playerLost();
                    
            }
            else if (m_game.IsTie())
            {
                isGameOver = anotherGame();
            }

        }

    }
    private void playerLost()
    {
        PlayerTurn playerTurn = m_game.PlayerTurn;
        Console.WriteLine("Oh no " + playerTurn.ToString() + "lost!");
        printScore();
    }

    private void printScore()
    {
        Console.WriteLine("The score of player 1 is:" + m_game.Player1Score() + "\n The score of player 2 is: " + m_game.Player2Score());
    }

    private bool anotherGame()
    {
        string input;
        bool continuePlaying = false;

        Console.WriteLine("The game ent with a tie");
        Console.WriteLine("Would you like to continue playing? y/n");

        do
        {
            input = Console.ReadLine();
        }
        while (!input.Equals('Q') || !input.Equals('y') || !input.Equals('n'));

        if (input.Equals('y'))
        {
            continuePlaying = true;
            printScore();
            m_game.RestartGame();
        }

        return continuePlaying;
    }

    private bool getValidRowAndCol(ref int o_Row,ref int o_Col)
    {
        String input;
        bool isGameOver = false;

        Console.WriteLine("Please enter column:");
        input = getValidInput();

        if (input.Equals("Q"))
            isGameOver = true;

        o_Col = int.Parse(input);

        Console.WriteLine("Please enter row:");
        input = getValidInput();
        if (input.Equals("Q"))
            isGameOver = true;

        o_Row = int.Parse(input);
        Console.WriteLine();

        return isGameOver;
    }
    private bool initGame()
    {
        String input;
        int boardSize;
        bool PlayerTwoIsComputer, isGameOver = false;

        Console.WriteLine("Welcome to TicTacToe!");
        Console.WriteLine("Please enter the size of the board you want (the size most be between 3 - 9):");
        
        input = getValidInput(false, true);

        if (input == "Q")
        {
            isGameOver = true;
        }

        else
        {
            boardSize = int.Parse(input);
            m_game.InitBoard(boardSize);

            Console.WriteLine("Do you want to play against the computer or friend? (computer = 1 / friend = 2):");
            Console.WriteLine("If you want to play against computer please press 1");
            Console.WriteLine("If you want to play against a friend please press 2");


            input = getValidInput(true);

            if (input == "Q")
            {
                isGameOver = true;
            }
            else
            {
                PlayerTwoIsComputer = parseInputToBool(input);
                m_game.InitPlayers(PlayerTwoIsComputer);
               
            }
        }

        return isGameOver;
    }

    private String getValidInput(bool i_CheckPlayerType = false,bool i_InitGame = false)
    {
        String input = "" ,errorMessage = "";
        bool validInput = false;

        while (!validInput)
        {
            input = Console.ReadLine();
            validInput = isValidInput(input, i_CheckPlayerType, ref errorMessage);
            if (!validInput)
                Console.WriteLine(errorMessage);
        }
        return input;
    }

    private bool isValidInput(String i_Input, bool i_CheckPlayerType, ref string o_ErrorMessage, bool i_InitGame = false)
    {
        if (i_Input.Equals(""))
        {
            o_ErrorMessage = "Please enter some input before pressing Enter, Try again.";
            return false;
        }

        foreach (char c in i_Input)
            if (!char.IsDigit(c))
            {
                o_ErrorMessage = "Invalid input, you must enter only numbers, Try again.";
                return false;
            }

        if (i_CheckPlayerType)
        {
            if (i_Input.Length != 1)
            {
                o_ErrorMessage = "Please enter only one digit , Try again.";
                return false;
            }
            char digit = i_Input[0];
            if(digit != '1' && digit != '2')
            {
                o_ErrorMessage = "The only possible options are 1 or 2, Try again.";
                return false;
            }
        }
        else
        {
            if (!m_game.IsBoardSizeValid(int.Parse(i_Input)) && i_InitGame)
            {
                o_ErrorMessage = "The size is out of range between 3 - 9, Try again.";
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
        if (board != null)
        {
            int size = board.GetLength(0);

            for (int i = 0; i < size; i++)
            {
                Console.Write("  " + (i + 1) + " ");
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
}