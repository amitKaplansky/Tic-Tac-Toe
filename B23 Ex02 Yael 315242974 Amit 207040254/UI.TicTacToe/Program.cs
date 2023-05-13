using System;

namespace UI.TicTacToe;

class Program
{
    public static void Main()
    {
        Engine.TicTacToe.Game game = new Engine.TicTacToe.Game();
        game.CopyBoard();
        Console.WriteLine("Hello");
    }

    private void printMenue()
    {
        String input;
        bool validInput = false;
        Console.WriteLine("Welcome to TicTacTock!");

        while (!validInput) { 
        Console.WriteLine("Please enter the size of the board you want(need to be more then 3 and less then 9):");
        input = Console.ReadLine();
        validInput = isInputValid(input);
        if (!validInput)
            Console.WriteLine("Your input is invalid please try again.");
        }
        else { 

        }
    }

    private bool isInputValid(String input)
    {
        return true;
    }
}