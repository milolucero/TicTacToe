using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    internal class Rule
    {
        private static (int height, int width) boardDimensions = (3, 3);
        private static int maxNumberOfPlayers = 2;
        private static int consecutiveShapesToWin = 3;

        public static (int height, int width) GetBoardDimensions()
        {
            return boardDimensions;
        }

        public static int GetMaxNumberOfPlayers()
        {
            return maxNumberOfPlayers;
        }

        public static int GetConsecutiveShapesToWin()
        {
            return consecutiveShapesToWin;
        }
    }
}
