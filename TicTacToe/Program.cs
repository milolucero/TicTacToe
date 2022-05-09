using System;
namespace TicTacToe
{

    /// <summary>
    /// A simple tic-tac-toe game in console.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();

            // Test.Test1(game);
            // Test2(game);
            
            //Test.TestBoard(game);
            //Test.TestOccupySpace(game);
            //Test.TestBoard(game);

            game.NewGame();
        }
    }
}