using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeLibrary
{
    /// <summary>
    /// Defines the rules and limits of the game.
    /// </summary>
    public class Rule
    {
        private static (int height, int width) boardDimensions = (3, 3);
        private static int maxNumberOfPlayers = 2;
        private static int consecutiveShapesToWin = 3;

        /// <summary>
        /// Returns the dimensions of the board.
        /// </summary>
        /// <returns>The dimensions of the board as a tuple.</returns>
        public static (int height, int width) GetBoardDimensions()
        {
            return boardDimensions;
        }

        /// <summary>
        /// Returns the maximum number of players that the game can have.
        /// </summary>
        /// <returns>The maximum number of players that the game can have.</returns>
        public static int GetMaxNumberOfPlayers()
        {
            return maxNumberOfPlayers;
        }

        /// <summary>
        /// Returns the amount of consecutive shapes that the same player must have on the board to win the game.
        /// </summary>
        /// <returns>The amount of consecutive shapes that the same player must have on the board to win the game.</returns>
        public static int GetConsecutiveShapesToWin()
        {
            return consecutiveShapesToWin;
        }
    }
}
