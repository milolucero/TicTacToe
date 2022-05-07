using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    internal class Game
    {
        private Board board;
        private Player[] players;
        private Player userPlayer;
        private Player botPlayer;
        private Player currentTurnPlayer;
        private int nextAssignableId;
        private int countOfTurns;

        public Game(Shape userShapeChoice)
        {
            board = new Board();
            players = new Player[2];

            players[0] = new Player(GetNewId());
            players[1] = new Player(GetNewId());

            currentTurnPlayer = GetCurrentTurnPlayer();

            countOfTurns = 0;

            // Set user player shape choice (either X or O, player with shape X always has the first turn).
            SetUserPlayer(userShapeChoice);

            //Set player names
            userPlayer.SetName("You");
            botPlayer.SetName("Bot");
        }

        public Game() : this(Shape.X)
        {

        }

        public int GetNewId()
        {
            int newId = nextAssignableId;

            nextAssignableId++;

            return newId;
        }

        public Player GetCurrentTurnPlayer()
        {
            return players[0].GetHasTurn() ? players[0] : players[1];
        }

        /// <summary>
        /// Returns the player who has NOT the current turn.
        /// </summary>
        /// <returns></returns>
        public Player GetNotCurrentTurnPlayer()
        {
            return players[0].GetHasTurn() ? players[1] : players[0];
        }

        //public Player GetCurrentTurnPlayer()
        //{
        //    return currentTurnPlayer;
        //}

        public void SetCurrentTurnPlayer(Player player)
        {
            GetNotCurrentTurnPlayer().SetHasTurn(true);
            GetCurrentTurnPlayer().SetHasTurn(false);
            currentTurnPlayer = player;
        }

        /// <summary>
        /// Switches the player's turn.
        /// </summary>
        public void SwitchTurns()
        {
            SetCurrentTurnPlayer(GetNotCurrentTurnPlayer());
        }

        public Player GetUserPlayer()
        {
            return userPlayer;
        }

        public void SetUserPlayer(Shape userShapeChoice)
        {
            if (userShapeChoice == Shape.X)
            {
                userPlayer = players[0];
                botPlayer = players[1];
            }
            else if (userShapeChoice == Shape.O)
            {
                userPlayer = players[1];
                botPlayer = players[0];
            }
            else
            {
                Console.WriteLine("Error: Unrecognized shape.");
            }
        }

        public Player GetBotPlayer()
        {
            return botPlayer;
        }

        public override string ToString()
        {
            string template = "";
            template += $"--Game--\n\n";

            template += $"-Game info-\n";
            template += $"countOfTurns: {countOfTurns}\n";
            template += $"nextAssignableId: {nextAssignableId}\n";
            template += $"currentTurnPlayer: {currentTurnPlayer.GetId()}\n";
            template += $"GetCurrentTurnPlayer(): {GetCurrentTurnPlayer().GetId()}\n";
            template += $"GetNotCurrentTurnPlayer(): {GetNotCurrentTurnPlayer().GetId()}\n";

            template += $"\n\n-Player info-\n";
            template += $"Player count: {players.Length}\n\n";

            template += $"User player info:\n{GetUserPlayer().ToString()}\n";
            template += $"Bot player info:\n{GetBotPlayer().ToString()}\n";

            template += $"";

            // string C = $"Player {currentTurnPlayer.GetShape()} has the turn. Player {GetNotCurrentTurnPlayer().GetShape()} does not have the turn.\n\n";

            return template;
        }
    }
}
