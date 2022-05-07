using System;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(Shape.X);

            game.SetCurrentTurnPlayer(game.GetBotPlayer());

            Test.Test1(game);
            // Test2(game);
        }
    }
}