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

            // Create players
            players = new Player[Rule.GetMaxNumberOfPlayers()];
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = new Player(GetNewId());
            }

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

        public Board GetBoard()
        {
            return board;
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

        /// <summary>
        /// Sets a player's shape into a specific space. Returns true if the space was taken succesfully, false if it was already occupied.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="space"></param>
        /// <returns>True if the space was taken succesfully, false if it was already occupied</returns>
        public bool OccupySpace(Player player, Space space)
        {
            bool spaceOccupiedSuccessfully = false;

            if (space.GetOccupant() == null)
            {
                space.SetOccupant(player);
                spaceOccupiedSuccessfully = true;
            }
            else
            {
                Console.WriteLine("The space is already taken. Choose another one.");
            }

            return spaceOccupiedSuccessfully;
        }

        public void NewGame()
        {
            // Welcome message
            Console.WriteLine("Tic-tac-toe");

            // Prompt user shape choice

            Shape userShapeChoice = GetUserShapeChoice();

            Console.WriteLine(userShapeChoice.ToString());
        }

        /// <summary>
        /// Prompts the user to pick a side (either "X" or "O"). Keeps prompting until the input is valid. Returns the Shape chosen. 
        /// </summary>
        /// <returns>The Shape chosen by the user.</returns>
        public Shape GetUserShapeChoice()
        {
            Shape userShapeChoice = Shape.None;
            while (userShapeChoice == Shape.None)
            {
                Console.WriteLine("Choose a side. Enter \"X\" or \"O\" (case insensitive).");
                string userInput = Console.ReadLine().ToUpper();

                if (Enum.IsDefined(typeof(Shape), userInput))
                {
                    // Convert the userInput string into its equivalent Shape.
                    userShapeChoice = (Shape) Enum.Parse(typeof(Shape), userInput);
                }
                else
                {
                    Console.WriteLine("Invalid side. Please try again.");
                }
            }

            return userShapeChoice;
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

            return template;
        }
    }
}
