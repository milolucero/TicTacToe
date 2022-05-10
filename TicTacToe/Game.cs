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
        private int userScore;
        private int botScore;
        private int tieScore;
        private Player currentTurnPlayer;
        private int nextAssignableId;
        private int turnCount;
        private List<GameResult> resultHistory;

        /// <summary>
        /// Initializes a new instance of the Game class, especifying the side/shape (either "X" or "O") chosen by the user. X always has the first turn.
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
            DetermineTurn();

            //Initialize win history
            resultHistory = new List<GameResult>();
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
        }

        /// <summary>
        /// Returns the instance of the player who does not have the current turn to play.
        /// </summary>
        /// <returns>The instance of the player who does not have the current turn to play.</returns>
        public Player GetNotCurrentTurnPlayer()
        {
            foreach (Player player in players)
            {
                if (player != currentTurnPlayer)
                {
                    return player;
                }
            }

            throw new Exception("No player without current turn was found.");
        }

        /// <summary>
        /// Sets the current turn to play to the specified player.
        /// </summary>
        /// <param name="player">The player who is being set to have the current turn to play.</param>
        public void SetCurrentTurnPlayer(Player player)
        {
            currentTurnPlayer = player;
        }

        /// <summary>
        /// Determines and assigns the player who has the turn according to the state of the filled spaces on the board.
        /// </summary>
        public void DetermineTurn()
        {
            Shape shapeOfCurrentTurn = Board.GetShapeOfTurnFromBoard(board);
            SetCurrentTurnPlayer(GetPlayerFromShape(shapeOfCurrentTurn));
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
                throw new Exception("Error: Unrecognized shape.");
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

            if (!space.IsOccupied())
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
            // player[0] (X) always has the first turn.
            SetCurrentTurnPlayer(players[0]);

            // Prompt user shape choice
            Shape userShapeChoice = GetUserShapeChoice();

            // Set user player shape choice (either X or O, player with shape X always has the first turn).
            SetUserPlayer(userShapeChoice);

            bool gameIsNotOver = (board.GetResult() == GameResult.Incomplete);
            while (gameIsNotOver)
            {
                NewTurn();
            }
        }

        /// <summary>
        /// Starts a new turn.
        /// </summary>
        public void NewTurn()
        {
            Space choiceOfSpaceToOccupy;

            if (currentTurnPlayer == userPlayer)
            {
                Console.WriteLine($"Turn: {currentTurnPlayer.GetName()} ({currentTurnPlayer.GetShape()})");

                choiceOfSpaceToOccupy = PromptPickSpaceToOccupy();
            } 
            else if (currentTurnPlayer == botPlayer)
            {
                // This block is where the bot AI will decide to make a move.
                // Here we should set the choiceOfSpaceToOccupy to the Space that the bot decides to take according to its decision model.
                // For testing purposes, the current model is a simple random empty space picker.

                // Get a random empty space
                choiceOfSpaceToOccupy = BotAI.GetRandomEmptySpace(board);
            }
            else
            {
                throw new Exception("Error: Invalid currentTurnPlayer. currentTurnPlayer is neither userPlayer nor botPlayer.");
            }

            OccupySpace(currentTurnPlayer, choiceOfSpaceToOccupy);

            // Print current state of the board
            board.PrintBoard();

            // If there is a winner or draw
            GameResult gameResult = Board.GetResultFromBoard(board);
            bool gameIsOver = (gameResult != GameResult.Incomplete);
            if (gameIsOver)
            {
                // Update state.
                board.SetResult(gameResult);
                UpdateScores();

                // Display winner and scores.
                GameResult lastResult = resultHistory[resultHistory.Count - 1];
                DisplayWinner(lastResult);
                DisplayPlayersScore();

                // Restart game if user chooses to.
                if (PromptPlayAgain())
                {
                    RestartGame();
                }
            }
            else
            {
                DetermineTurn();
            }
        }

        /// <summary>
        /// Updates the state of the scores and result history. Should be called when a game is over (winner or tie).
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void UpdateScores()
        {
            GameResult gameResult = board.GetResult();

            if (gameResult == GameResult.Incomplete)
            {
                throw new Exception($"Scores can't be updated because game is incomplete.");
            }

            bool hasWinner = (board.GetResult() == GameResult.WinnerX) || (board.GetResult() == GameResult.WinnerO);
            bool hasTie = board.GetResult() == GameResult.Tie;

            if (hasWinner)
            {
                Player winnerPlayer;

                // Determine winning Player
                if (gameResult == GameResult.WinnerX)
                {
                    winnerPlayer = GetPlayerFromShape(Shape.X);
                }
                else if (gameResult == GameResult.WinnerO)
                {
                    winnerPlayer = GetPlayerFromShape(Shape.O);
                }
                else 
                {
                    throw new Exception($"No winner player can be determined from result \"{gameResult}\"");
                }

                // Update winning player scores
                if (winnerPlayer == userPlayer)
                {
                    userScore++;
                }
                else if (winnerPlayer == botPlayer)
                {
                    botScore++;
                }
            }
            // Update tie score
            else if (hasTie)
            {
                tieScore++;
            }

            // Add the result to the result history.
            resultHistory.Add(gameResult);
        }

        /// <summary>
        /// Resets the game to play a new round.
        /// </summary>
        public void RestartGame()
        {
            board = new Board();

            DetermineTurn();

            turnCount = 0;

            foreach (Player player in players)
            {
                player.ResetOccupiedSpaces();
            }

            NewGame();
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
                Console.Write("Choose a side. Enter \"X\" or \"O\": ");
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
            Space chosenSpace;
            string spaceInput;
            bool spaceInputIsInt;
            bool spaceInputIsValidInt;
            bool spaceIsValid = false;
            int spaceNumber;

            do
            {
                Console.Write($"Choose a space (1-9): ");
                spaceInput = Console.ReadLine();

                // Validate if input is an int between 1 and 9
                spaceInputIsInt = int.TryParse(spaceInput, out spaceNumber);
                spaceInputIsValidInt = spaceInputIsInt && (spaceNumber >= 1 && spaceNumber <= 9);

                if (spaceInputIsValidInt)
                {
                    spaceNumber = int.Parse(spaceInput);
                    chosenSpace = board.GetBoardSpaceFromInt(spaceNumber);

                    // Validate if chosen space is already taken.
                    if (chosenSpace.IsOccupied())
                    {
                        Console.WriteLine("The chosen space is taken. Pick another one.\n");
                    }
                    else
                    {
                        spaceIsValid = true;
                    }
                }
                else
                {
                    Console.WriteLine($"Invalid input \"{spaceInput}\". Please enter a number between 1 and 9.\n");
                }
            } while (!spaceIsValid);

            return board.GetBoardSpaceFromInt(spaceNumber);
        }

        /// <summary>
        /// Prompts the user to play a new round of the game. Returns true is the user agrees; otherwise, false.
        /// </summary>
        /// <returns>True is the user wants to play a new round of the game; otherwise, false.</returns>
        public bool PromptPlayAgain()
        {
            bool playAgain = false;

            do
            {
                Console.Write("Play again? (Y/n): ");
                string userInput = Console.ReadLine();

                if (userInput.ToLower() == "y" || userInput.ToLower() == "")
                {
                    playAgain = true;
                    break;
                }
                else if (userInput.ToLower() == "n")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                }
            } while (true);

            Console.WriteLine();

            return playAgain;
        }

        /// <summary>
        /// Prints a message declaring the winner or a tie.
        /// </summary>
        /// <param name="gameResult">The result of the game.</param>
        public void DisplayWinner(GameResult gameResult)
        {
            string declareWinnerMessage = "";

            declareWinnerMessage += "\n############\n";

            if (gameResult == GameResult.WinnerX || gameResult == GameResult.WinnerO)
            {
                Player winner = GetPlayerFromResult(gameResult);
                declareWinnerMessage += $"{winner.GetName()} ({winner.GetShape()}) won!";
            }
            else if (gameResult == GameResult.Tie)
            {
                declareWinnerMessage += $"It's a tie!";
            }

            declareWinnerMessage += "\n############\n";

            Console.WriteLine(declareWinnerMessage);
        }

        /// <summary>
        /// Prints a message with the current score of all the players.
        /// </summary>
        public void DisplayPlayersScore()
        {
            string scoreMessage = "";
            scoreMessage += "SCORE\n";
            scoreMessage += $"{userPlayer.GetName()}: {userScore}\n";
            scoreMessage += $"{botPlayer.GetName()}: {botScore}\n";
            scoreMessage += $"Tie: {tieScore}\n";

            Console.WriteLine(scoreMessage);
        }

        /// <summary>
        /// Returns the player that matches the specified shape.
        /// </summary>
        /// <param name="shape"></param>
        /// <returns>The player that matches the specified shape.</returns>
        /// <exception cref="Exception">Error if no player with the specified shape is found.</exception>
        public Player GetPlayerFromShape(Shape shape)
        {
            foreach (Player player in players)
            {
                if (player.GetShape() == shape)
                {
                    return player;
                }
            }

            throw new Exception($"No player was found with the shape {shape}.");
        }

        public Player GetPlayerFromResult(GameResult result)
        {
            Player player;

            if (result == GameResult.WinnerX)
            {
                player = GetPlayerFromShape(Shape.X);
            } 
            else if (result == GameResult.WinnerO)
            {
                player = GetPlayerFromShape(Shape.O);
            }
            else
            {
                throw new Exception($"No player can be determined from the result \"{result}\".");
            }

            return player;
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
