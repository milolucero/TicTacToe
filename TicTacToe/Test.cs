using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    /// <summary>
    /// A playground class for testing purposes. 
    /// </summary>
    internal class Test
    {
        public static void RunTests()
        {
            //SimulateGameFromBoard(GetTestBoard(2));

            //TestTurnSwitching();
            //TestGetShapeOfTurnFromBoard();
            //TestGetBestMove();
            //TestBoardDirection();
            TestMinimax(8);
            //TestBoardClone();
        }

        public static void SimulateGameFromBoard(Board board)
        {
            Game game = new Game(board);
            game.NewGame();
        }

        public static void Test1(Game game)
        {
            Console.WriteLine(game);

            //Console.WriteLine("\n\nSwitching turns...\n\n");
            //game.SwitchTurns();

            //Console.WriteLine(game);
        }

        public static void TestTurnSwitching()
        {
            Game game = new Game();
            Board board = game.Board;


            // Simulate moves
            Board.OccupySpace(board, board.GetBoardSpaceFromInt(9)); // X
            Board.OccupySpace(board, board.GetBoardSpaceFromInt(7)); // O
            Board.OccupySpace(board, board.GetBoardSpaceFromInt(6)); // X
            Board.OccupySpace(board, board.GetBoardSpaceFromInt(3)); // O
            Board.OccupySpace(board, board.GetBoardSpaceFromInt(4)); // X
            Board.OccupySpace(board, board.GetBoardSpaceFromInt(2)); // O
            // Next turn belongs to X

            Console.WriteLine("Turn belongs to");
            Console.WriteLine($"Expected: X");
            Console.WriteLine($"Actual game.GetCurrentTurnPlayer(): {game.CurrentTurnPlayer.Shape}");
            Console.WriteLine($"Actual game.GetNotCurrentTurnPlayer(): {game.NotCurrentTurnPlayer.Shape}");

            Console.WriteLine("Switching turns...\n");
            Board.OccupySpace(board, board.GetBoardSpaceFromInt(1)); // X
            game.DetermineTurn();

            Console.WriteLine("Turn belongs to");
            Console.WriteLine($"Expected: O");
            Console.WriteLine($"Actual game.GetCurrentTurnPlayer(): {game.CurrentTurnPlayer.Shape}");
            Console.WriteLine($"Actual game.GetNotCurrentTurnPlayer(): {game.NotCurrentTurnPlayer.Shape}");
        }

        public static void TestBoard(Game game)
        {
            Console.WriteLine(game.Board);
        }

        public static void TestOccupySpace(Game game)
        {
            Board board = game.Board;
            Player player = game.GetUserPlayer();

            Board.OccupySpace(board, board.GetSpace(new Position(1, 0)), player);
            Board.OccupySpace(board, board.GetSpace(new Position(2, 0)), player);
        }

        public static void TestEmptySpaces(Board board)
        {
            List<Space> emptySpaces = board.EmptySpaces;
            for (int i = 0; i < emptySpaces.Count(); i++)
            {
                Console.WriteLine(emptySpaces[i].ToString());
            }
        }

        public static void TestGetShapeOfTurnFromBoard()
        {
            Board board = GetTestBoard(1);

            board.PrintBoard();

            Console.WriteLine($"{Board.GetShapeOfTurnFromBoard(board)} has the turn.");
        }

        public static void TestGetBestMove()
        {
            Board board = GetTestBoard(3);

            board.PrintBoard();

            Console.WriteLine($"{Board.GetShapeOfTurnFromBoard(board)} has the turn.");

            // In board 3 it's X's turn. The best move for X for Board 3 current state is board.GetBoardSpaceFromInt(3), since it's the only winning move.
            // Choosing board.GetBoardSpaceFromInt(2) leads to losing on the next turn (both remaining options are wins for O).
            // Choosing board.GetBoardSpaceFromInt(1) could either lead to X winning or tie, depending on O move.
            // Space bestMove = BotAI.GetBestMove(board);

            // Board.OccupySpace(board, bestMove);

            board.PrintBoard();
        }

        public static void TestMinimax(int boardNumber)
        {
            SimulateGameFromBoard(GetTestBoard(boardNumber));
        }

        public static void TestBoardClone()
        {
            Game game = new Game(GetTestBoard(3));
            game.NewGame();
        }

        public static void TestBoardDirection()
        {
            Board board = GetTestBoard(2);
            board.PrintBoard();
        }

        /// <summary>
        /// Returns a board filled with specific occupied spaces designed for testing purposes.
        /// </summary>
        /// <param name="boardNumber">The number of the board to get returned (see inline comments to see the board design).</param>
        /// <returns></returns>
        public static Board GetTestBoard(int boardNumber)
        {
            Board board = new Board();

            /*
             * Board 1
             * O.X
             * X.X
             * .OO
             */
            if (boardNumber == 1)
            {
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(9));
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(7));
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(6));
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(3));
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(4));
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(2));
            }

            /*
             * Board 2
             * X..
             * .X.
             * ..O
             */
            if (boardNumber == 2)
            {
                Board.OccupySpace(board, board.GetSpace(new Position(0, 2))); // X
                Board.OccupySpace(board, board.GetSpace(new Position(2, 0))); // O
                Board.OccupySpace(board, board.GetSpace(new Position(1, 1))); // X
            }

            /*
             * Board 3
             * OXX
             * OOX
             * ...
             */
            if (boardNumber == 3)
            {
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(9));
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(7));
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(8));
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(5));
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(6));
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(4));
            }

            /*
             * Board 4 - O Plays. Two options: One move leads to draw, the other leads to O losing.
             * OXX
             * XXO
             * O..
             */
            if (boardNumber == 4)
            {
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(9)); // X
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(7)); // O
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(8)); // X
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(6)); // O
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(5)); // X
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(1)); // O
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(4)); // X
            }

            /*
             * Board 5 - X plays. The best move is on space 4, since it guarantees a win in X's next turn, regardless of O's move.
             * Use this to test the bot's algorithm. Playing as X, the bot should always pick space 4 in its first turn.
             * XO.
             * ..X
             * ..O
             */
            if (boardNumber == 5)
            {
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(7)); // X
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(8)); // O
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(6)); // X
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(3)); // O
            }

            /*
             * Board 6 - O plays. Both alternatives lead to O's losing. Check how the bot behaves when playing as O.
             * XXO
             * OXX
             * O..
             */
            if (boardNumber == 6)
            {
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(8)); // X
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(9)); // O
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(7)); // X
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(4)); // O
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(6)); // X
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(1)); // O
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(5)); // X
            }

            /*
             * Board 7 - O plays. X is about to win by choosing 6. O must decide to choose 6 to keep alive.
             * O.X
             * XX.
             * O..
             */
            if (boardNumber == 7)
            {
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(9)); // X
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(7)); // O
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(5)); // X
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(1)); // O
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(4)); // X
            }

            /*
             * Board 8 - X plays. 3 options, one move leads to a win, the others lead to a lose in the next turn. X must choose 7 to win.
             * .OO
             * X..
             * XXO
             */
            if (boardNumber == 8)
            {
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(1)); // X
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(9)); // O
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(2)); // X
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(3)); // O
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(4)); // X
                Board.OccupySpace(board, board.GetBoardSpaceFromInt(8)); // O
            }

            return board;
        }
    }
}