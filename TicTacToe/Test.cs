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
        public static void RunTests()
        {
            TestGetShapeOfTurnFromBoard();
            //TestGetBestMove();
            //TestBoardDirection();
        }
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
            Board board = game.GetBoard();
            Player player = game.GetUserPlayer();

            Board.OccupySpace(board, board.GetSpace(new Position(1, 0)), player);
            Board.OccupySpace(board, board.GetSpace(new Position(2, 0)), player);
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
            Board board = game.GetBoard();

            // Simulate some moves
            Board.OccupySpace(board, board.GetBoardSpaceFromInt(5));
            Board.OccupySpace(board, board.GetBoardSpaceFromInt(8));
            Board.OccupySpace(board, board.GetBoardSpaceFromInt(2));
            Board.OccupySpace(board, board.GetBoardSpaceFromInt(1));
            Board.OccupySpace(board, board.GetBoardSpaceFromInt(4));
            Board.OccupySpace(board, board.GetBoardSpaceFromInt(7));

            game.GetBoard().PrintBoard();

            Console.WriteLine($"{Board.GetShapeOfTurnFromBoard(game.GetBoard())} has the turn.");
        }

        public static void TestGetBestMove()
        {
            Game game = new Game();
            Board board = game.GetBoard();

            // Simulate some moves
            Board.OccupySpace(board, board.GetBoardSpaceFromInt(5));
            Board.OccupySpace(board, board.GetBoardSpaceFromInt(8));
            Board.OccupySpace(board, board.GetBoardSpaceFromInt(2));
            Board.OccupySpace(board, board.GetBoardSpaceFromInt(1));
            Board.OccupySpace(board, board.GetBoardSpaceFromInt(4));
            Board.OccupySpace(board, board.GetBoardSpaceFromInt(7));

            board.PrintBoard();

            Console.WriteLine($"{Board.GetShapeOfTurnFromBoard(board)} has the turn.");

            BotAI.GetBestMove(game.GetBoard());

        }

        public static void TestBoardDirection()
        {
            Game game = new Game();
            Board board = game.GetBoard();

            Board.OccupySpace(board, board.GetSpace(new Position(0, 2))); // X
            Board.OccupySpace(board, board.GetSpace(new Position(2, 0))); // O
            Board.OccupySpace(board, board.GetSpace(new Position(1, 1))); // X

            /*
            Expected:

            X..
            .X.
            ..O

            */

            board.PrintBoard();
        }
    }
}
