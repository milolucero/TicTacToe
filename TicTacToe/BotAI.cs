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
    internal class BotAI
    {
        // Input: Board, turnShape -> Output: Space

        public static Space GetRandomEmptySpace(Board board)
        {
            Random random = new Random();
            int maxRandom = board.GetEmptySpaces().Count;
            int randomEmptySpaceInt = random.Next(0, maxRandom);
            Space randomEmptySpace = board.GetEmptySpaces()[randomEmptySpaceInt];

            return randomEmptySpace;
        }
    }
}
