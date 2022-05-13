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
        private readonly Position position;
        private Shape? occupant;

        /// <summary>
        /// Initializes a new instance of the Space class, with specified position and occupant.
        /// </summary>
        /// <param name="position">The position where the space is located.</param>
        /// <param name="occupant">The player that occupies the space.</param>
        public Space(Position position, Shape? occupant)
        {
            this.position = position;
            this.occupant = occupant;
        }

        /// <summary>
        /// Initializes a new instance of the Space class, with specified position and no occupant (null).
        /// </summary>
        /// <param name="position">The position where the space is located.</param>
        public Space(Position position) : this(position, null)
        {

        }

        /// <summary>
        /// Returns the position of the space.
        /// </summary>
        /// <returns>The position of the space.</returns>
        public Position GetPosition()
        {
            return position;
        }

        /// <summary>
        /// Returns the player who occupies the space, or null if it is unoccupied.
        /// </summary>
        /// <returns>The player who occupies the space, or null if it is unoccupied.</returns>
        public Shape? GetOccupant()
        {
            return occupant;
        }

        /// <summary>
        /// Sets a specified player to occupy the space, or sets the space empty if null.
        /// </summary>
        /// <param name="occupant">The specified player to occupy the space (or null to set it empty).</param>
        public void SetOccupant(Shape? occupant)
        {
            this.occupant = occupant;
        }

        /// <summary>
        /// Returns true if the space is occupied by a player, false otherwise.
        /// </summary>
        /// <returns>True if the space is occupied by a player, false otherwise.</returns>
        public bool IsOccupied()
        {
            return occupant != null;
        }

        public override string ToString()
        {
            string template = "";
            template += $"Space ({GetPosition().GetX()}, {GetPosition().GetY()}) - ";
            template += $"{(IsOccupied() ? GetOccupant() : "Empty")}";
            return template;
        }

        /// <summary>
        /// Creates a copy of the given space object.
        /// </summary>
        /// <param name="space">The space to copy.</param>
        /// <returns>A copy of the given space.</returns>
        public static Space GetSpaceClone(Space space)
        {
            Position cloneSpacePosition = new Position(space.GetPosition().GetX(), space.GetPosition().GetY());

            Space cloneSpace = new Space(cloneSpacePosition, space.GetOccupant());

            return cloneSpace;
        }
    }
}
