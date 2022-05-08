using System;

/// <summary>
/// A simple tic-tac-toe game in console.
/// </summary>
namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(Shape.X);

            // Test.Test1(game);
            // Test2(game);
            
            //Test.TestBoard(game);
            //Test.TestOccupySpace(game);
            //Test.TestBoard(game);

            game.NewGame();
        }
    }
}