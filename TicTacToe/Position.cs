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
        private int x;
        private int y;

        /// <summary>
        /// Initializes a new instance of the Position class, with specified coordinates x and y.
        /// </summary>
        /// <param name="x">The coordinate x.</param>
        /// <param name="y">The coordinate y.</param>
        public Position(int x, int y)
        {
            SetX(x);
            SetY(y);
        }

        /// <summary>
        /// Returns the coordinate x.
        /// </summary>
        /// <returns>The coordinate x.</returns>
        public int GetX()
        {
            return x;
        }

        /// <summary>
        /// Sets the coordinate x to the specified integer value.
        /// </summary>
        /// <param name="x">The new coordinate x.</param>
        public void SetX(int x)
        {
            this.x = x;
        }

        /// <summary>
        /// Returns the coordinate y.
        /// </summary>
        /// <returns>The coordinate y.</returns>
        public int GetY()
        {
            return y;
        }

        /// <summary>
        /// Sets the coordinate y to the specified integer value.
        /// </summary>
        /// <param name="y">The new coordinate y.</param>
        public void SetY(int y)
        {
            this.y = y;
        }
    }
}
