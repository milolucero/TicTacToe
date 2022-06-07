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
        private Player[] players;
        private Player userPlayer;
        private Player botPlayer;
        private int userScore;
        private int botScore;
        private int tieScore;
        private int nextAssignableId;
        private int turnCount;
        private List<GameResult> resultHistory;

        /// <summary>
        /// Initializes a new instance of the Game class, with a specific board.
        /// The constructor Game(Board) sets fields like turnCount or player state to initial values. It just allows to simulate a game from a non-empty board for testing purposes.
        /// </summary>
        /// <param name="board">The board to assign to the game.</param>
        public Game(Board board)
        {
            // Create players
            players = new Player[Rule.GetMaxNumberOfPlayers()];
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = new Player(GetNewId());
            }

            // Set the board
            this.Board = board;

            // Since X always starts, what shape has the turn depends on the board, so it should be determined each time a new board is set.
            // player[0] (X) always has the first turn.
            DetermineTurn();

            //Initialize win history
            resultHistory = new List<GameResult>();
        }

        /// <summary>
        /// Initializes a new instance of the Game class. X always has the first turn.
        /// </summary>
        public Game() : this(new Board())
        { 

        }
        
        /// <summary>
        /// Sets the properties for the board of the game.
        /// </summary>
        public Board Board
        {
            get; set;
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
        /// Property representing the player who has the current turn to play on the game.
        /// </summary>
        public Player CurrentTurnPlayer
        {
            get; set;
        }

        /// <summary>
        /// Property representing the player who does not have the current turn to play on the game.
        /// </summary>
        public Player NotCurrentTurnPlayer
        {
            get
            {
                foreach (Player player in players)
                {
                    if (player != CurrentTurnPlayer)
                    {
                        return player;
                    }
                }

                throw new Exception("No player without current turn was found.");
            }
        }

        /// <summary>
        /// Determines and assigns the player who has the turn according to the state of the filled spaces on the board.
        /// </summary>
        public void DetermineTurn()
        {
            Shape shapeOfCurrentTurn = Board.GetShapeOfTurnFromBoard(Board);
            CurrentTurnPlayer = GetPlayerFromShape(shapeOfCurrentTurn);
        }

        /// <summary>
        /// Returns the instance of the player that is assigned to the user.
        /// </summary>
        /// <returns>The instance of the player that is assigned to the user.</returns>
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
            userPlayer.Name = "You";
            botPlayer.Name = "Bot";
        }

        /// <summary>
        /// Returns the instance of the player that is assigned to the computer player.
        /// </summary>
        /// <returns>The instance of the player that is assigned to the computer player.</returns>
        public Player GetBotPlayer()
        {
            return botPlayer;
        }

        /// <summary>
        /// Starts a new game.
        /// </summary>
        public void NewGame()
        {
            // Prompt user shape choice
            Shape userShapeChoice = GetUserShapeChoice();

            // Set user player shape choice (either X or O, player with shape X always has the first turn).
            SetUserPlayer(userShapeChoice);

            // Print initial board
            Board.PrintBoard();

            // While the game is not over
            while (Board.Result == GameResult.Incomplete)
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

            // Display current turn info
            Console.WriteLine($"Turn: {CurrentTurnPlayer.Name} ({CurrentTurnPlayer.Shape})");

            // Choose a space to make the move.
            if (CurrentTurnPlayer == userPlayer)
            {
                choiceOfSpaceToOccupy = PromptPickSpaceToOccupy();
            }
            else if (CurrentTurnPlayer == botPlayer)
            {
                // This block is where the bot AI decides the move to make.
                // Set choiceOfSpaceToOccupy to the Space that the bot decides to take according to the AI decision model.
                DifficultyLevel difficultyLevel = DifficultyLevel.Hard;
                choiceOfSpaceToOccupy = BotAI.GetMove(difficultyLevel, Board);
            }
            else
            {
                throw new Exception("Invalid CurrentTurnPlayer. Game.CurrentTurnPlayer is neither userPlayer nor botPlayer.");
            }

            // Occupy the space chosen and assign that space to the player who has the turn.
            Board.OccupySpace(Board, choiceOfSpaceToOccupy, CurrentTurnPlayer);

            // Print current state of the board
            Board.PrintBoard();

            // If there is a winner or draw
            GameResult gameResult = Board.GetResultFromBoard(Board);
            bool gameIsOver = (gameResult != GameResult.Incomplete);
            if (gameIsOver)
            {
                // Update state.
                Board.Result = gameResult;
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
        /// <exception cref="Exception">Thrown if trying to update the score when the game is incomplete or if the game determined a winner but the winning shape is not X or O.</exception>
        public void UpdateScores()
        {
            GameResult gameResult = Board.Result;

            if (gameResult == GameResult.Incomplete)
            {
                throw new Exception($"Scores can't be updated because game is incomplete.");
            }

            bool hasWinner = (Board.Result == GameResult.WinnerX) || (Board.Result == GameResult.WinnerO);
            bool hasTie = Board.Result == GameResult.Tie;

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
            Board = new Board();

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
                    chosenSpace = Board.GetBoardSpaceFromInt(spaceNumber);

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

            return Board.GetBoardSpaceFromInt(spaceNumber);
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
        /// <param name="gameResult">The result of a finished game.</param>
        /// <exception cref="Exception">Thrown when given a result where the game is still incomplete.</exception>
        public void DisplayWinner(GameResult gameResult)
        {
            if (gameResult == GameResult.Incomplete)
            {
                throw new Exception("Not possible to display the result of an unfinished game.");
            }

            string declareWinnerMessage = "";

            declareWinnerMessage += "\n############\n";

            if (gameResult == GameResult.WinnerX || gameResult == GameResult.WinnerO)
            {
                Player winner = GetWinningPlayerFromResult(gameResult);
                declareWinnerMessage += $"{winner.Name} ({winner.Shape}) won!";
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
            scoreMessage += $"{userPlayer.Name}: {userScore}\n";
            scoreMessage += $"{botPlayer.Name}: {botScore}\n";
            scoreMessage += $"Tie: {tieScore}\n";

            Console.WriteLine(scoreMessage);
        }

        /// <summary>
        /// Returns the player that matches the specified shape.
        /// </summary>
        /// <param name="shape">The shape that the returned player should have assigned.</param>
        /// <returns>The player that matches the specified shape.</returns>
        /// <exception cref="Exception">Thrown if no player with the specified shape is found.</exception>
        public Player GetPlayerFromShape(Shape shape)
        {
            foreach (Player player in players)
            {
                if (player.Shape == shape)
                {
                    return player;
                }
            }

            throw new Exception($"No player was found with the shape {shape}.");
        }

        /// <summary>
        /// Returns the player instance who has the shape that won the given board.
        /// </summary>
        /// <param name="result">The game result of a finished game.</param>
        /// <returns>The player instance who has the shape that won the given board.</returns>
        /// <exception cref="Exception">Thrown if the provided result was not a winner (for example, if given a tie or incomplete result).</exception>
        public Player GetWinningPlayerFromResult(GameResult result)
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
            template += $"currentTurnPlayer: {CurrentTurnPlayer.Identifier}\n";
            template += $"GetCurrentTurnPlayer(): {CurrentTurnPlayer.Identifier}\n";
            template += $"GetNotCurrentTurnPlayer(): {NotCurrentTurnPlayer.Identifier}\n";

            template += $"\n\n-Player info-\n";
            template += $"Player count: {players.Length}\n\n";

            template += $"User player info:\n{GetUserPlayer().ToString()}\n";
            template += $"Bot player info:\n{GetBotPlayer().ToString()}\n";

            return template;
        }
    }
}
