using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    /// <summary>
    /// Represents the tic-tac-toe game.
    /// </summary>
    internal class Game
    {
        private Board board;
        private Player[] players;
        private Player userPlayer;
        private Player botPlayer;
        private Player currentTurnPlayer;
        private int nextAssignableId;
        private int turnCount;
        private int drawCount;
        private List<Player?> winHistory;

        /// <summary>
        /// Initializes a new instance of the Game class, especifying the side/shape (either "X" or "O") chosen by the user.
        /// </summary>
        public Game()
        {
            board = new Board();

            // Create players
            players = new Player[Rule.GetMaxNumberOfPlayers()];
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = new Player(GetNewId());
            }

            // player[0] (X) always has the first turn.
            currentTurnPlayer = players[0];

            // Initialize turn counter
            turnCount = 0;

            // Initialize draw counter
            drawCount = 0;

            //Initialize win history
            winHistory = new List<Player?>();
        }

        /// <summary>
        /// Returns the tic-tac-toe Board instance of the game.
        /// </summary>
        /// <returns>The tic-tac-toe Board instance of the game.</returns>
        public Board GetBoard()
        {
            return board;
        }

        /// <summary>
        /// Generates a new unique numeric id, starting from zero.
        /// </summary>
        /// <returns>A new unique numeric id.</returns>
        public int GetNewId()
        {
            int newId = nextAssignableId;

            nextAssignableId++;

            return newId;
        }

        /// <summary>
        /// Returns the instance of the player who has the current turn to play.
        /// </summary>
        /// <returns>The instance of the player who has the current turn to play.</returns>
        public Player GetCurrentTurnPlayer()
        {
            return currentTurnPlayer;
            // return players[0].GetHasTurn() ? players[0] : players[1];
        }

        /// <summary>
        /// Returns the instance of the player who does not have the current turn to play.
        /// </summary>
        /// <returns>The instance of the player who does not have the current turn to play.</returns>
        public Player GetNotCurrentTurnPlayer()
        {
            return players[0].GetHasTurn() ? players[1] : players[0];
        }

        /// <summary>
        /// Sets the current turn to play to the specified player.
        /// </summary>
        /// <param name="player">The player who is being set to have the current turn to play.</param>
        public void SetCurrentTurnPlayer(Player player)
        {
            for (int i = 0; i < players.Length; i++)
            {
                players[i].SetHasTurn(players[i].GetId() == player.GetId());
            }

            currentTurnPlayer = player;
        }

        /// <summary>
        /// Switches turns between the players.
        /// </summary>
        public void SwitchTurns()
        {
            SetCurrentTurnPlayer(GetNotCurrentTurnPlayer());
        }

        /// <summary>
        /// Gets the instance of the player that is assigned to the user.
        /// </summary>
        /// <returns></returns>
        public Player GetUserPlayer()
        {
            return userPlayer;
        }

        /// <summary>
        /// Assigns the specified side/shape to the user player.
        /// </summary>
        /// <param name="userShapeChoice">The side/shape to assign to the user.</param>
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

            // Set player names
            userPlayer.SetName("You");
            botPlayer.SetName("Bot");
        }

        /// <summary>
        /// Gets the instance of the player that is assigned to the computer player.
        /// </summary>
        /// <returns>The instance of the player that is assigned to the computer player.</returns>
        public Player GetBotPlayer()
        {
            return botPlayer;
        }

        /// <summary>
        /// Sets a player's shape into a specific space. Returns true if the space was taken succesfully, false if it was already occupied.
        /// </summary>
        /// <param name="player">The player that should occupy the space.</param>
        /// <param name="space">The space being occupied.</param>
        /// <returns>True if the space was taken succesfully, false if it was already occupied</returns>
        public bool OccupySpace(Player player, Space space)
        {
            bool spaceOccupiedSuccessfully = false;

            if (!space.isOccupied())
            {
                space.SetOccupant(player);
                player.AddToOccupiedSpaces(space);

                List<Space> emptySpaces = board.GetEmptySpaces();
                emptySpaces.Remove(space);
                board.SetEmptySpaces(emptySpaces);

                spaceOccupiedSuccessfully = true;
            }
            else
            {
                Console.WriteLine("The space is already taken. Choose another one.");
            }

            return spaceOccupiedSuccessfully;
        }

        /// <summary>
        /// Starts a new game.
        /// </summary>
        public void NewGame()
        {
            // Welcome message
            Console.WriteLine("Tic-tac-toe");

            // Prompt user shape choice
            Shape userShapeChoice = GetUserShapeChoice();

            // Set user player shape choice (either X or O, player with shape X always has the first turn).
            SetUserPlayer(userShapeChoice);

            // Print the board
            //while (!game.winner)
            while (true)
            {
                Test.TestEmptySpaces(board);
                NewTurn();
            }
        }

        /// <summary>
        /// Starts a new turn.
        /// </summary>
        public void NewTurn()
        {
            Console.WriteLine($"Turn: {currentTurnPlayer.GetName()} ({currentTurnPlayer.GetShape()})");

            // Prompt user to pick a space. Keep prompting until an empty space is chose.
            Space userChoiceOfSpaceToOccupy;
            do
            {
                userChoiceOfSpaceToOccupy = PromptPickSpaceToOccupy();

                if (userChoiceOfSpaceToOccupy.isOccupied())
                {
                    Console.WriteLine("The chosen space is taken. Pick another one.\n");
                }
            } while (userChoiceOfSpaceToOccupy.isOccupied());

            OccupySpace(currentTurnPlayer, userChoiceOfSpaceToOccupy);

            board.PrintBoard();

            CheckGameResult();

            SwitchTurns();
        }

        /// <summary>
        /// Returns true if the game has either a winner or a draw; otherwise, false.
        /// </summary>
        /// <returns>True if the game has either a winner or a draw; otherwise, false.</returns>
        public bool CheckGameResult()
        {
            (bool hasWinner, Player? winner) = board.CheckWin();
            bool hasDraw = board.CheckDraw();

            if (hasWinner)
            {
                winHistory.Add(winner);
                winner.SetScore(winner.GetScore() + 1);
            }
            else if (hasDraw)
            {
                winHistory.Add(null);
                drawCount++;
            }

            bool hasWinnerOrDraw = (hasWinner || hasDraw);

            return hasWinnerOrDraw;
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
                Console.Write("Choose a side. Enter \"X\" or \"O\" (case insensitive): ");
                string userInput = Console.ReadLine().ToUpper();

                // Check if the given user input matches a Shape.
                if (Enum.IsDefined(typeof(Shape), userInput))
                {
                    // Convert the userInput string into its equivalent Shape.
                    userShapeChoice = (Shape) Enum.Parse(typeof(Shape), userInput);
                }
                else
                {
                    Console.WriteLine("Invalid side. Please try again.\n");
                }
            }

            Console.WriteLine();

            return userShapeChoice;
        }

        /// <summary>
        /// Prompts the user to pick a space of the board to place its shape. Returns the chosen space.
        /// </summary>
        /// <returns>The space chosen by the user to place its shape.</returns>
        public Space PromptPickSpaceToOccupy()
        {
            string spaceInput = "";
            bool spaceInputIsInt = false;
            bool spaceInputIsValidInt = false;
            int spaceNumber = 0;

            Space testingSpace = new Space(new Position(1,1));

            do
            {
                Console.Write($"Choose a space (1-9): ");
                spaceInput = Console.ReadLine();

                spaceInputIsInt = int.TryParse(spaceInput, out spaceNumber);
                spaceInputIsValidInt = spaceInputIsInt && (spaceNumber >= 1 && spaceNumber <= 9);

                if (spaceInputIsValidInt)
                {
                    spaceNumber = int.Parse(spaceInput);
                }
                else
                {
                    Console.WriteLine($"Invalid input \"{spaceInput}\". Please enter a number between 1 and 9.\n");
                }

            } while (!spaceInputIsValidInt);

            return board.GetBoardSpaceFromInt(spaceNumber);
        }

        /// <summary>
        /// Returns a string representation of the game's state.
        /// </summary>
        /// <returns>A string representation of the game's state.</returns>
        public override string ToString()
        {
            string template = "";
            template += $"--Game--\n\n";

            template += $"-Game info-\n";
            template += $"countOfTurns: {turnCount}\n";
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
