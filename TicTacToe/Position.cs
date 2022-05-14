using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    /// <summary>
    /// Represents a position coordinate in the board.
    /// </summary>
    internal class Position
    {
        /// <summary>
        /// Initializes a new instance of the Position class, with specified coordinates x and y.
        /// </summary>
        /// <param name="x">The horizontal coordinate.</param>
        /// <param name="y">The vertical coordinate.</param>
        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Property representing a horizontal coordinate.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Property representing a vertical coordinate.
        /// </summary>
        public int Y { get; set; }
    }
}
