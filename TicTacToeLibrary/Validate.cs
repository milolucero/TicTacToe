using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeLibrary
{
    public class Validate
    {
        public static void BoardConstructorSpacesArgument(Space[] spaces)
        {
            // Argument spaces must contain less or equal items as the board's height * width
            int height = Rule.GetBoardDimensions().height;
            int width = Rule.GetBoardDimensions().width;

            if (spaces.Length > height * width)
                throw new ArgumentOutOfRangeException(nameof(spaces), "The given spaces contain more items than the board can hold.");

            // Argument spaces must not contain two spaces at the same position
            List<(int, int)> positionsOccupied = new List<(int, int)>();

            foreach (Space space in spaces)
            {
                foreach ((int X, int Y) positionOccupied in positionsOccupied)
                {
                    if (space.Position.X == positionOccupied.X && space.Position.Y == positionOccupied.Y)
                        throw new ArgumentException($"The given spaces argument contains two spaces at the same position. (X: {space.Position.X}, Y: {space.Position.Y})", nameof(spaces));
                }
                positionsOccupied.Add((space.Position.X, space.Position.Y));
            }
        }
    }
}
