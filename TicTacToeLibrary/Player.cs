using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeLibrary
{
    /// <summary>
    /// Represents a player of the game.
    /// </summary>
    public class Player
    {

        /// <summary>
        /// Initializes a new instance of the Player, with specified id, name, score and spaces that are filled with the player's shape.
        /// </summary>
        /// <param name="identifier">The player's identifier. Since only two players are possible, it can only take the values 0 and 1.</param>
        /// <param name="name">The player's name.</param>
        /// <param name="occupiedSpaces">A list of the spaces that are occupied by the player's shapes ("X" or "O").</param>
        public Player(int identifier, string name, List<Space> occupiedSpaces)
        {
            this.Identifier = identifier;
            this.Name = name;
            this.OccupiedSpaces = occupiedSpaces;

            // The player[0] has the first turn and is assigned the shape "X". The player[1] is assigned the shape "O".
            // When the user picks sides ("X" or "O") at the beginning of the game, he is picking to be player[0] or player[1].

            // Set player[0] (X) and player[1] (O) shapes.
            if (this.Identifier == 0)
            {
                this.Shape = Shape.X;
            } 
            else if (this.Identifier == 1)
            {
                this.Shape = Shape.O;
            }
            else
            {
                throw new Exception("Unable to instantiate. The game can only have 2 players.");
            }
        }

        /// <summary>
        /// Initializes a new instance of the Player, with specified id, name and score. The list of spaces occupied by the player is set to empty.
        /// </summary>
        /// <param name="identifier">The player's identifier.</param>
        /// <param name="name">The player's name.</param>
        public Player(int identifier, string name) : this(identifier, name, new List<Space>())
        {

        }

        /// <summary>
        /// Initializes a new instance of the Player, with specified identifier.
        /// </summary>
        /// <param name="identifier">The player's identifier.</param>
        public Player(int identifier) : this(identifier, $"Player {identifier}")
        {

        }

        /// <summary>
        /// Property representing the player's unique identifier.
        /// </summary>
        public int Identifier
        {
            get;
        }

        /// <summary>
        /// Property representing the player's name.
        /// </summary>
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// Property representing the shape (enumeration constant) that the player uses to fill the spaces on the board. Can be either "X", "O", or "None".
        /// </summary>
        public Shape Shape
        {
            get; set;
        }

        /// <summary>
        /// Property representing a list of the spaces currently occupied by the player.
        /// </summary>
        public List<Space> OccupiedSpaces
        {
            get; set;
        }

        /// <summary>
        /// Empties the list of spaces occupied by the player.
        /// </summary>
        public void ResetOccupiedSpaces()
        {
            this.OccupiedSpaces = new List<Space>();
        }

        /// <summary>
        /// Adds the given space to the player's instance occupied spaces.
        /// </summary>
        /// <param name="space">The space to add to the player's occupied spaces.</param>
        public void AddToOccupiedSpaces(Space space)
        {
            this.OccupiedSpaces.Add(space);
        }

        /// <summary>
        /// Returns true if the player currently has the turn to play based on a given board, false otherwise.
        /// </summary>
        /// <param name="board">The board to check the current turn.</param>
        /// <returns>True if the player currently has the turn to play, false otherwise.</returns>
        public bool HasTurn(Board board)
        {
            return this.Shape == Board.GetShapeOfTurnFromBoard(board);
        }

        /// <summary>
        /// Returns a string representation of the player's state.
        /// </summary>
        /// <returns>A string representation of the player's state.</returns>
        public override string ToString()
        {
            string template = "";
            template += $"--Player--\n";
            template += $"id: {this.Identifier}\n";
            template += $"name: {this.Name}\n";
            template += $"shape: {this.Shape}\n";
            return template;
        }
    }
}
