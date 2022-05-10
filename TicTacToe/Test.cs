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
            game.DetermineTurn();

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

        public static void TestEmptySpaces(Board board)
        {
            List<Space> emptySpaces = board.GetEmptySpaces();
            for (int i = 0; i < emptySpaces.Count(); i++)
            {
                Console.WriteLine(emptySpaces[i].ToString());
            }
        }

        public static void TestGetShapeOfTurnFromBoard()
        {
            Game game = new Game();

            // Simulate some moves
            game.OccupySpace(game.GetPlayerFromShape(Board.GetShapeOfTurnFromBoard(game.GetBoard())), game.GetBoard().GetBoardSpaceFromInt(1));
            game.OccupySpace(game.GetPlayerFromShape(Board.GetShapeOfTurnFromBoard(game.GetBoard())), game.GetBoard().GetBoardSpaceFromInt(2));
            game.OccupySpace(game.GetPlayerFromShape(Board.GetShapeOfTurnFromBoard(game.GetBoard())), game.GetBoard().GetBoardSpaceFromInt(8));
            game.OccupySpace(game.GetPlayerFromShape(Board.GetShapeOfTurnFromBoard(game.GetBoard())), game.GetBoard().GetBoardSpaceFromInt(6));
            game.OccupySpace(game.GetPlayerFromShape(Board.GetShapeOfTurnFromBoard(game.GetBoard())), game.GetBoard().GetBoardSpaceFromInt(5));

            game.GetBoard().PrintBoard();

            Console.WriteLine($"{Board.GetShapeOfTurnFromBoard(game.GetBoard())} has the turn.");
        }
    }
}
