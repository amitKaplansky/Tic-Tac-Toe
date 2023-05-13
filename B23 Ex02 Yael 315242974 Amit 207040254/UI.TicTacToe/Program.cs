using System;
using System.Drawing;

namespace UI.TicTacToe;

class Program
{
    public static void Main()
    {
        Engine.TicTacToe.Game game = new Engine.TicTacToe.Game();
       
        Console.WriteLine("Hello");
        TicTacToeUi ui = new TicTacToeUi(); 
        ui.PlayGame();
    }

   
}