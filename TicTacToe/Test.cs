using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    /// <summary>
    /// A class for testing purposes.
    /// </summary>
    internal class Test
    {
        public static void Test1(Game game)
        {
            Console.WriteLine(game);

            //Console.WriteLine("\n\nSwitching turns...\n\n");
            //game.SwitchTurns();

            //Console.WriteLine(game);
        }

        public static void Test2(Game game)
        {
            // Console.WriteLine($"game.currentTurnPlayer(): {game.currentTurnPlayer}");
            Console.WriteLine($"game.GetCurrentTurnPlayer(): {game.GetCurrentTurnPlayer()}");
            Console.WriteLine($"game.GetNotCurrentTurnPlayer(): {game.GetNotCurrentTurnPlayer()}");

            Console.WriteLine("Switching turns...\n");
            game.SwitchTurns();

            // Console.WriteLine($"\n\ngame.currentTurnPlayer(): {game.currentTurnPlayer}");
            Console.WriteLine($"game.GetCurrentTurnPlayer(): {game.GetCurrentTurnPlayer()}");
            Console.WriteLine($"game.GetNotCurrentTurnPlayer(): {game.GetNotCurrentTurnPlayer()}");
        }

        public static void TestBoard(Game game)
        {
            Console.WriteLine(game.GetBoard());
        }

        public static void TestOccupySpace(Game game)
        {
            game.OccupySpace(game.GetUserPlayer(), game.GetBoard().GetSpace(new Position(1, 0)));
            game.OccupySpace(game.GetBotPlayer(), game.GetBoard().GetSpace(new Position(2, 0)));
        }
    }
}
