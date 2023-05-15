using System;
using System.Data.Common;
using System.Drawing;
using System.Security.Cryptography;
using Engine.Reverse.TicTacToe;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UI.Reverse.TicTacToe;

class UI
{
    Game m_game = new Engine.Reverse.TicTacToe.Game();

    public void PlayReverseTicTacToe()
    {
        int col, row;
        bool isValidMove = false, gameOn = true;
        string turnMessege = "";

        Console.WriteLine("Welcome to TicTacToe!");

        Console.WriteLine("Let's start to play Reversed Tic Tac Toe,\nTo end game press 'Q'.");

        gameOn = initGame();

        while (gameOn)
        {
            //Screen.Clear();
            
            printBoard();

            if (!m_game.IsPlayerTurnIsComputer())
            {
                turnMessege = m_game.PlayerTurn == ePlayerTurn.Player1 ? "It's Player1 turn" : "It's Player2 turn";
                Console.WriteLine(turnMessege);
                do
                {
                    getValidRowAndColOrQ(out row, out col, ref gameOn);

                    if (!gameOn)
                    {
                        break;
                    }

                    isValidMove = m_game.NextMove(row, col);

                    if (!isValidMove)
                        Console.WriteLine("Invalid move please try again");

                } while (!isValidMove);
            }
            else
            {
                Console.WriteLine("It's Player2 turn");
                m_game.NextMove();
            }

            
            gameOn = gameOn && isRoundOver();

            System.Threading.Thread.Sleep(2000);
        }

        Console.WriteLine("Hope to see you again, Bye!");

        System.Threading.Thread.Sleep(1000);
    }

    
    private void playerLost()
    {
        ePlayerTurn playerTurn = m_game.PlayerTurn;
        Console.WriteLine("Oh no " + playerTurn.ToString() + " lost!");
    }

    private void printScore()
    {
        Console.WriteLine("The score of player 1 is: " + m_game.Player1Score() + "\nThe score of player 2 is: " + m_game.Player2Score());
    }
    private bool isRoundOver()
    {
        bool lose = m_game.DidPlayerLose();
        bool tie = m_game.IsTie();
        if (lose)
        {
            playerLost();
        }
        else if (tie)
        {
            Console.WriteLine("This round ended in tie");
        }

        return tie || lose ? continueGame() : true ;
        
    }

    private bool continueGame()
    {
        string input;
        bool continuePlaying = false;
        List<string> validInputs = new List<string> { "y", "n", "Q" };

        printScore();

        Console.WriteLine("Would you like to continue playing? y/n");

        input = Console.ReadLine();
            
        while (!validInputs.Contains(input)){

            Console.WriteLine("Wrong input, Please try again");

            input = Console.ReadLine();
        }

        if (input == "y")
        {
            continuePlaying = true;
            m_game.RestartGame();
        }


        return continuePlaying;
    }

    private void getValidRowAndColOrQ(out int o_Row, out int o_Col, ref bool o_GameOn)
    {

        o_Row = o_Col = -1;

        Console.WriteLine("Please enter row:");
        o_GameOn = !getNumberOrEndGame(out o_Row);

        if (o_GameOn)
        {
            Console.WriteLine("Please enter column:");

            o_GameOn = !getNumberOrEndGame(out o_Col);
        }
        

    }

    private bool getNumberOrEndGame(out int i_Number)
    {
        string input = "";
        input = Console.ReadLine();

        i_Number = -1;

        while (!(input == "Q" || int.TryParse(input, out i_Number)))
        {
            Console.WriteLine("Invalid move, Please Try again");
            input = Console.ReadLine();
        }

        return input == "Q";
    }

    private bool initGame()
    {
        
        bool endGame;

        endGame = !initBoard();

        if (endGame)
        {
            endGame = !initPlayers();
        }

        return endGame;
    }

    private bool initBoard()
    {

        string userInput = "";
        int boardSize;
        bool isNumber = false, endGame = false, isLegal = false;

        Console.WriteLine("Please enter the size of the board you want (the size most be between 3 - 9):");

        do
        {
            userInput = Console.ReadLine();

            if (userInput.Equals("Q"))
            {
                endGame = true;
                break;
            }
            else
            {
                isNumber = int.TryParse(userInput, out boardSize);
            }

            if (isNumber)
            {
                isLegal = m_game.InitBoard(boardSize);
            }

            if(isLegal)
            {
                break;
            }
            else
            {
                Console.WriteLine("Wrong input, Please Enter a digit or Q to end the game");
                
            }
        }
        while (true);

        return endGame;
    }

    private bool initPlayers()
    {
        string userInput = "";
        bool endGame = false;

        List<string> validInputs = new List<string> { "1", "2", "Q" };

        Console.WriteLine("Do you want to play against the computer or friend?");
        Console.WriteLine("For Computer please press 1");
        Console.WriteLine("For a Friend please press 2");
        
        userInput = Console.ReadLine();

        while (!validInputs.Contains(userInput))
        {
            Console.WriteLine("Wrong input, please enter a valid option: 1/ 2/ Q");
            userInput = Console.ReadLine();
        }

        if (userInput == "Q")
        {
            endGame = true;
        }
        else
        {
            m_game.InitPlayers(userInput == "1");
        }

        return endGame;
    }

    private void printBoard()
    {
        eBoardSymbols[,] board = m_game.CopyBoard();

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
                    if (board[i, j] != eBoardSymbols.Empty)
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