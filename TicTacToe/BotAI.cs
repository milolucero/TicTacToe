using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    /// <summary>
    /// Represents the decision-making process that a bot player can use to make a move in the game.
    /// </summary>
    internal abstract class BotAI
    {
        /// <summary>
        /// Returns a random empty space.
        /// </summary>
        /// <param name="board">The board from which to choose an empty space.</param>
        /// <returns>A random empty space.</returns>
        public static Space GetRandomEmptySpace(Board board)
        {
            Random random = new Random();
            int maxRandom = board.GetEmptySpaces().Count;
            int randomEmptySpaceInt = random.Next(0, maxRandom);
            Space randomEmptySpace = board.GetEmptySpaces()[randomEmptySpaceInt];

            return randomEmptySpace;
        }

        /// <summary>
        /// Returns the best possible empty space to occupy based on the board's current state and the shape's turn.
        /// </summary>
        /// <param name="board">The board from which to choose a space.</param>
        /// <returns>The best possible empty space to occupy for the shape with the current turn.</returns>
        public static Space? GetBestMove(Board board)
        {
            Space[] spaces = board.GetSpaces();
            Space? bestMove = null; // Make not nullable here and in the method declaration

            Shape shapeToPlay = Board.GetShapeOfTurnFromBoard(board);
            Shape opponentShape = (shapeToPlay == Shape.X) ? Shape.O : Shape.X;

            List<GameResult> resultHistory = new List<GameResult>();

            foreach (Space emptySpace in board.GetEmptySpaces())
            {
                (bool reachedTerminalState, int? scoreForX, int? scoreForO) = GetMoveScore(board, emptySpace);

                if (reachedTerminalState && scoreForX > 0)
                {
                    bestMove = emptySpace;
                    Console.WriteLine("Winning move!");
                    Console.WriteLine(emptySpace);
                }
            }

            // In resultHistory each time that an empty space is looped over, add the MoveScore from picking that space. If no winning move is found, 
            return bestMove;
        }

        public static (bool reachedTerminalState, int? scoreForX, int? scoreForO) GetMoveScore(Board board, Space move)
        {
            int? scoreForX = null;
            int? scoreForO = null;

            Shape shapeToPlay = Board.GetShapeOfTurnFromBoard(board);

            // Simulate the move.
            Board.OccupySpace(board, move);

            // Check if the move resulted in a win or tie.
            (bool hasWinner, Shape? winnerShape) = board.CheckWin();
            bool hasTie = board.CheckTie();

            if (hasWinner)
            {
                if (winnerShape == Shape.X)
                {
                    scoreForX = 1;
                    scoreForO = -1;
                }
                else if (winnerShape == Shape.O)
                {
                    scoreForX = -1;
                    scoreForO = 1;
                }
            } 
            else if (hasTie)
            {
                scoreForX = 0;
                scoreForO = 0;
            }
            else
            {
                //Console.WriteLine($"Move does not result in a win or a tie.");
            }

            bool reachedTerminalState = hasWinner || hasTie;

            return (reachedTerminalState, scoreForX, scoreForO);
        }
    }
}
