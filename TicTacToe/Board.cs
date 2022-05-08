using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    internal class Board
    {
        private readonly int height;
        private readonly int width;
        private Space[] spaces;

        public Board(Space[] spaces)
        {
            height = Rule.GetBoardDimensions().height;
            width = Rule.GetBoardDimensions().width;
            this.spaces = spaces;
        }

        public Board()
        {
            height = Rule.GetBoardDimensions().height;
            width = Rule.GetBoardDimensions().width;

            spaces = new Space[height * width];

            // Populate spaces array with new spaces at the specified coordinates
            int count = 0;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Position position = new Position(i, j);
                    spaces[count] = new Space(position);

                    count++;
                }
            }
        }

        public Space GetSpace(Position position)
        {
            foreach (Space space in spaces)
            {
                if (space.GetPosition().GetX() == position.GetX() &&
                    space.GetPosition().GetY() == position.GetY())
                {
                    return space;
                }
            }

            return null;
            // If no result was found
            Console.WriteLine("Error: No space matched the given position.");
        }

        /// <summary>
        /// Prints a string representation of the board.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string template = "";

            int count = 0;
            for (int i = 0; i < this.height; i++)
            {

                for (int j = 0; j < this.width; j++)
                {
                    Space space = spaces[count];

                    string shape = "";

                    if (space.GetOccupant() == null)
                    {
                        shape = ".";
                    }
                    else
                    {
                        shape = space.GetOccupant().GetShape().ToString();
                    }

                    template += $"{shape}";
                    count++;
                }

                template += "\n";
            }

            return template;
        }
    }
}