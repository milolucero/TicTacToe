using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
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
    }
}
