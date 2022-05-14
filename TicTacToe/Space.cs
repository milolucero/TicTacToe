using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    /// <summary>
    /// Represents a space of the tic-tac-toe board.
    /// </summary>
    internal class Space
    {
        /// <summary>
        /// Initializes a new instance of the Space class, with specified position and occupant.
        /// </summary>
        /// <param name="position">The position where the space is located.</param>
        /// <param name="occupant">The player that occupies the space.</param>
        public Space(Position position, Shape? occupant)
        {
            this.Position = position;
            this.Occupant = occupant;
        }

        /// <summary>
        /// Initializes a new instance of the Space class, with specified position and no occupant (null).
        /// </summary>
        /// <param name="position">The position where the space is located.</param>
        public Space(Position position) : this(position, null)
        {

        }

        /// <summary>
        /// Property representing the position (coordinates) of the board where the space is located.
        /// </summary>
        public Position Position
        {
            get;
        }

        /// <summary>
        /// Property representing the player who occupies the space, or null if it is unoccupied.
        /// </summary>
        public Shape? Occupant
        {
            get; set;
        }

        /// <summary>
        /// Returns true if the space is occupied by a player, false otherwise.
        /// </summary>
        /// <returns>True if the space is occupied by a player, false otherwise.</returns>
        public bool IsOccupied()
        {
            return Occupant != null;
        }

        /// <summary>
        /// Creates a copy of the given space object.
        /// </summary>
        /// <param name="space">The space to copy.</param>
        /// <returns>A copy of the given space.</returns>
        public static Space GetSpaceClone(Space space)
        {
            Position cloneSpacePosition = new Position(space.Position.X, space.Position.Y);

            Space cloneSpace = new Space(cloneSpacePosition, space.Occupant);

            return cloneSpace;
        }

        public override string ToString()
        {
            string template = "";
            template += $"Space ({this.Position.X}, {this.Position.Y}) - ";
            template += $"{(IsOccupied() ? Occupant : "Empty")}";
            return template;
        }
    }
}
