using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    /// <summary>
    /// Represents a player of the game.
    /// </summary>
    internal class Player
    {
        private readonly int id;
        private string name;
        private Shape shape;
        private int score;
        private List<Space> occupiedSpaces;
        private bool hasTurn;

        /// <summary>
        /// Initializes a new instance of the Player, with specified id, name, score and spaces that are filled with the player's shape.
        /// </summary>
        /// <param name="id">The player's identifier. Since only two players are possible, it can only take the values 0 and 1.</param>
        /// <param name="name">The player's name.</param>
        /// <param name="score">The player's score.</param>
        /// <param name="occupiedSpaces">A list of the spaces that are occupied by the player's shapes ("X" or "O").</param>
        public Player(int id, string name, int score, List<Space> occupiedSpaces)
        {
            this.id = id;
            SetName(name);
            SetScore(score);
            this.occupiedSpaces = occupiedSpaces;

            // The player[0] has the first turn and is assigned the shape "X". The player[1] is assigned the shape "O".
            // When the user picks sides ("X" or "O") at the beginning of the game, he is picking to be player[0] or player[1].

            // Set player[0] (X) and player[1] (O) their shape and turn.
            if (this.id == 0)
            {
                shape = Shape.X;
                SetHasTurn(true);
            } 
            else if (this.id == 1)
            {
                shape = Shape.O;
                SetHasTurn(false);
            }
            else
            {
                Console.WriteLine("Error: The game can only have 2 players.");
            }
        }

        /// <summary>
        /// Initializes a new instance of the Player, with specified id, name and score. The list of spaces occupied by the player is set to empty.
        /// </summary>
        /// <param name="id">The player's identifier.</param>
        /// <param name="name">The player's name.</param>
        /// <param name="score">The player's score.</param>
        public Player(int id, string name, int score) : this(id, name, score, new List<Space>())
        {

        }

        /// <summary>
        /// Initializes a new instance of the Player, with specified id and name.
        /// </summary>
        /// <param name="id">The player's identifier.</param>
        /// <param name="name">The player's name.</param>
        public Player(int id, string name) : this(id, name, 0)
        {

        }

        /// <summary>
        /// Initializes a new instance of the Player, with specified id.
        /// </summary>
        /// <param name="id">The player's identifier.</param>
        public Player(int id) : this(id, $"Player {id}")
        {

        }

        /// <summary>
        /// Returns the player id.
        /// </summary>
        /// <returns>The player id.</returns>
        public int GetId()
        {
            return id;
        }

        /// <summary>
        /// Returns the player name.
        /// </summary>
        /// <returns>The player name.</returns>
        public string GetName()
        {
            return name;
        }

        /// <summary>
        /// Sets the player name to the specified string value.
        /// </summary>
        /// <param name="name">The new player name.</param>
        public void SetName(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Returns the shape (enumeration constant) that the player uses to fill the spaces on the board. Can be either "X" or "O".
        /// </summary>
        /// <returns>The shape (enumeration constant) that the player uses to fill the spaces on the board. Can be either "X" or "O".</returns>
        public Shape GetShape()
        {
            return shape;
        }

        /// <summary>
        /// Sets the shape (enumeration constant) that the player uses to fill the spaces on the board. Can be either "X" or "O".
        /// </summary>
        /// <param name="shape">The shape (enumeration constant) that the player uses to fill the spaces on the board. Can be either "X" or "O".</param>
        public void SetShape(Shape shape)
        {
            this.shape = shape;
        }

        /// <summary>
        /// Gets the current score of the player.
        /// </summary>
        /// <returns>The current score of the player.</returns>
        public int GetScore()
        {
            return score;
        }

        /// <summary>
        /// Sets the score of the player.
        /// </summary>
        /// <param name="score">The new score of the player.</param>
        public void SetScore(int score)
        {
            this.score = score;
        }

        /// <summary>
        /// Returns true if the player currently has the turn to play, false otherwise.
        /// </summary>
        /// <returns>True if the player currently has the turn to play, false otherwise.</returns>
        public bool GetHasTurn()
        {
            return hasTurn;
        }

        /// <summary>
        /// Sets the hasTurn attribute to the specified boolean value.
        /// </summary>
        /// <param name="hasTurn">The new value to set the hasTurn attribute.</param>
        public void SetHasTurn(bool hasTurn)
        {
            this.hasTurn = hasTurn;
        }

        /// <summary>
        /// Returns a list of the spaces currently occupied by the player.
        /// </summary>
        /// <returns>A list of the spaces currently occupied by the player.</returns>
        public List<Space> GetOccupiedSpaces()
        {
            return occupiedSpaces;
        }

        /// <summary>
        /// Empties the list of spaces occupied by the player.
        /// </summary>
        public void ResetOccupiedSpaces()
        {
            occupiedSpaces = new List<Space>();
        }

        public void AddToOccupiedSpaces(Space space)
        {
            occupiedSpaces.Add(space);
        }

        /// <summary>
        /// Returns a string representation of the player's state.
        /// </summary>
        /// <returns>A string representation of the player's state.</returns>
        public override string ToString()
        {
            string template = "";
            template += $"--Player--\n";
            template += $"id: {id}\n";
            template += $"name: {name}\n";
            template += $"shape: {shape}\n";
            template += $"score: {score}\n";
            template += $"hasTurn: {hasTurn}\n\n";
            return template;
        }
    }
}
