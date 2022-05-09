using System;

namespace TicTacToe
{
    /// <summary>
    /// A simple Tic-tac-toe console game using object oriented programming principles.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();

            // Welcome message
            Console.WriteLine("Tic-tac-toe V1.0");

            game.NewGame();
        }
    }
}