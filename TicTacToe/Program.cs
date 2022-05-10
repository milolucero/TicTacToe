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
            //Test.TestGetShapeOfTurnFromBoard();

            Console.WriteLine("Tic-tac-toe V1.0");
            Console.WriteLine("Author: Camilo Lucero");
            Console.WriteLine("Email: cluceroespitia@rrc.ca\n");

            Game game = new Game();
            game.NewGame();
        }
    }
}