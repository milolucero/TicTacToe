using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    internal class Space
    {
        private readonly Position position;
        private bool isOccupied;
        private Player? occupant;

        public Space(Position position, Player? occupant)
        {
            this.position = position;
            this.occupant = occupant;
        }

        public Space(Position position) : this(position, null)
        {

        }

        public Position GetPosition()
        {
            return position;
        }

        public Player? GetOccupant()
        {
            return occupant;
        }

        public void SetOccupant(Player? occupant)
        {
            this.occupant = occupant;
        }
    }
}
