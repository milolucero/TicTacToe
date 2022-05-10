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
        /// <returns>The best possible empty space to occupy.</returns>
        public static Space GetBestMove(Board board)
        {
            Shape shapeToPlay = Board.GetShapeOfTurnFromBoard(board);

            Space[] spaces = board.GetSpaces();

            foreach (Space emptySpace in board.GetEmptySpaces())
            {
                Console.WriteLine(emptySpace.ToString());

                Board nextTurnBoard = new Board(spaces);

            }

            return spaces[0];
        }
    }
}
