using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    internal class Player
    {
        private readonly int id;
        private string name;
        private Shape shape;
        private int score;
        private Space[] occupiedSpaces;
        private bool hasTurn;

        /// <summary>
        /// player[0] will be X, and he has the first turn. player[1] will be O. If user picks to be X or O, he is picking to be player[0] or [1].
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="score"></param>
        /// <param name="occupiedSpaces"></param>
        public Player(int id, string name, int score, Space[] occupiedSpaces)
        {
            this.id = id;
            SetName(name);
            SetScore(score);
            this.occupiedSpaces = occupiedSpaces;

            // Set player[0] (X) and player[1] (O) different shape and hasTurn
            if(this.id == 0)
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

        public Player(int id, string name, int score) : this(id, name, score, new Space[0])
        {

        }

        public Player(int id, string name) : this(id, name, 0)
        {

        }

        public Player(int id) : this(id, $"Player {id}")
        {

        }

        public int GetId()
        {
            return id;
        }

        public string GetName()
        {
            return name;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public Shape GetShape()
        {
            return shape;
        }

        public void SetShape(Shape shape)
        {
            this.shape = shape;
        }

        public int GetScore()
        {
            return score;
        }

        public void SetScore(int score)
        {
            this.score = score;
        }

        public bool GetHasTurn()
        {
            return hasTurn;
        }

        public void SetHasTurn(bool hasTurn)
        {
            this.hasTurn = hasTurn;
        }

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
