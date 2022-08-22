using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeLibrary
{
    /// <summary>
    /// Represents the tic-tac-toe game.
    /// </summary>
    public class Game
    {
        //private Player[] players;
        //private Player userPlayer;
        //private Player botPlayer;
        //private int userScore;
        //private int botScore;
        //private int tieScore;
        //private int nextAssignableId;
        //private int turnCount;
        //private List<GameResult> resultHistory;



        #region Properties
        /// <summary>
        /// Gets or sets the the board of the game.
        /// </summary>
        public Board Board { get; set; }

        /// <summary>
        /// Gets or sets the the score of the game.
        /// </summary>
        public Score Score { get; set; }

        /// <summary>
        /// Gets or sets the level of difficulty of the game.
        /// </summary>
        public DifficultyLevel DifficultyLevel { get; set; }

        /// <summary>
        /// Gets or sets the shape of the user player.
        /// </summary>
        public Shape UserShape { get; set; }

        /// <summary>
        /// Gets the shape of the bot player.
        /// </summary>
        public Shape BotShape
        {
            get { return UserShape == Shape.X ? Shape.O : Shape.X ; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the Game class, with a specified user shape, level of difficulty, board and score.
        /// </summary>
        /// <param name="userShape">The shape to be asigned to the user in the board.</param>
        /// <param name="difficultyLevel">The level of difficulty of the AI.</param>
        /// <param name="board">The board to assign to the game.</param>
        /// <param name="score">The score of the previous games.</param>
        public Game(Shape userShape, DifficultyLevel difficultyLevel, Board board, Score score)
        {
            this.UserShape = userShape;
            this.DifficultyLevel = difficultyLevel;
            this.Board = board;
            this.Score = score;
        }

        /// <summary>
        /// Initializes a new instance of the Game class, with a specified user shape, level of difficulty and board.
        /// </summary>
        /// <param name="userShape">The shape to be asigned to the user in the board.</param>
        /// <param name="difficultyLevel">The level of difficulty of the AI.</param>
        /// <param name="board">The board to assign to the game.</param>
        public Game(Shape userShape, DifficultyLevel difficultyLevel, Board board) : this(userShape, difficultyLevel, board, score: new Score())
        {
        }

        /// <summary>
        /// Initializes a new instance of the Game class, with a specified user shape and level of difficulty.
        /// </summary>
        /// <param name="userShape">The shape to be asigned to the user in the board.</param>
        /// <param name="difficultyLevel">The level of difficulty of the AI.</param>
        public Game(Shape userShape, DifficultyLevel difficultyLevel) : this(userShape, difficultyLevel, new Board())
        {
        }

        /// <summary>
        /// Initializes a new instance of the Game class, with a specified user shape. The difficulty level is set to "Hard".
        /// </summary>
        /// <param name="userShape">The shape to be asigned to the user in the board.</param>
        public Game(Shape userShape) : this(userShape, difficultyLevel: DifficultyLevel.Hard)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Game class. The user shape is set to "X" and the difficulty level is set to "Hard".
        /// </summary>
        public Game() : this(Shape.X)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets the current turn number based on the amount of occupied spaces on the board.
        /// </summary>
        /// <returns>The current turn number.</returns>
        public int TurnCount()
        {
            (int boardHeight, int boardWidth) = Rule.GetBoardDimensions();
            int boardSpacesCount = boardHeight * boardWidth;
            int turnCount = boardSpacesCount - this.Board.EmptySpaces.Count;

            return turnCount;
        }

        /// <summary>
        /// Returns the shape that has the turn to play. X always plays first.
        /// </summary>
        /// <returns>The shape that has the turn to play.</returns>
        public Shape CurrentTurnShape()
        {
            // If turn is even, X plays. Otherwise, O plays.
            return TurnCount() % 2 == 0 ? Shape.X : Shape.O;
        }

        /// <summary>
        /// Occupies the space of the board in the given coordinates with the shape that has the turn to play.
        /// </summary>
        /// <param name="x">The horizontal coordinate of the space (left to right).</param>
        /// <param name="y">The vertical coordinate of the space (bottom to top).</param>
        /// <exception cref="ArgumentOutOfRangeException">If the given coordinate is out of the board's bounds.</exception>
        /// <exception cref="InvalidOperationException">If the space in the given coordinate is already taken.</exception>
        public void MakeMove(int x, int y)
        {
            Position position = new Position(x, y);
            Space spaceInPosition = Board.GetSpace(position);

            (int boardHeight, int boardWidth) = Rule.GetBoardDimensions();

            // Out of range
            if (x > boardWidth || y > boardHeight)
                throw new ArgumentOutOfRangeException($"Position {{{x}, {y}}} is out of the board's bounds.");

            // Occupied
            if (spaceInPosition.IsOccupied())
                throw new InvalidOperationException($"Position {{{x}, {y}}} is occupied by {spaceInPosition.Occupant}");

            // Successful move
            Board.OccupySpace(Board, spaceInPosition);
        }


        public (int x, int y) GetMove()
        {
            if (Board.EmptySpaces.Count == 0)
                throw new InvalidOperationException($"No empty spaces were found in the board.");

            Space move = BotAI.GetMove(DifficultyLevel, Board);

            return (move.Position.X, move.Position.Y);
        }

        /// <summary>
        /// Assigns the specified side/shape to the user player.
        /// </summary>
        /// <param name="userShapeChoice">The side/shape to assign to the user.</param>
        //public void SetUserPlayer(Shape userShapeChoice)
        //{
        //    if (userShapeChoice == Shape.X)
        //    {
        //        userPlayer = players[0];
        //        botPlayer = players[1];
        //    }
        //    else if (userShapeChoice == Shape.O)
        //    {
        //        userPlayer = players[1];
        //        botPlayer = players[0];
        //    }
        //    else
        //    {
        //        throw new Exception("Error: Unrecognized shape.");
        //    }

        //    // Set player names
        //    userPlayer.Name = "You";
        //    botPlayer.Name = "Bot";
        //}

        /// <summary>
        /// Returns the instance of the player that is assigned to the computer player.
        /// </summary>
        /// <returns>The instance of the player that is assigned to the computer player.</returns>
        //public Player GetBotPlayer()
        //{
        //    return botPlayer;
        //}

        /// <summary>
        /// Starts a new game.
        /// </summary>
        public void NewGame()
        {
            //// Prompt user shape choice
            //Shape userShapeChoice = GetUserShapeChoice();

            //// Set user player shape choice (either X or O, player with shape X always has the first turn).
            //SetUserPlayer(userShapeChoice);

            //// Print initial board
            //Board.PrintBoard();

            // While the game is not over
            //while (Board.Result == GameResult.Incomplete)
            //{
            //    NewTurn();
            //}
        }

        /// <summary>
        /// Starts a new turn.
        /// </summary>
        //public void NewTurn()
        //{
        //    Space choiceOfSpaceToOccupy;

        //    // Display current turn info
        //    Console.WriteLine($"Turn: {CurrentTurnPlayer.Name} ({CurrentTurnPlayer.Shape})");

        //    // Choose a space to make the move.
        //    if (CurrentTurnPlayer == userPlayer)
        //    {
        //        choiceOfSpaceToOccupy = PromptPickSpaceToOccupy();
        //    }
        //    else if (CurrentTurnPlayer == botPlayer)
        //    {
        //        // This block is where the bot AI decides the move to make.
        //        // Set choiceOfSpaceToOccupy to the Space that the bot decides to take according to the AI decision model.
        //        DifficultyLevel difficultyLevel = DifficultyLevel.Hard;
        //        choiceOfSpaceToOccupy = BotAI.GetMove(difficultyLevel, Board);
        //    }
        //    else
        //    {
        //        throw new Exception("Invalid CurrentTurnPlayer. Game.CurrentTurnPlayer is neither userPlayer nor botPlayer.");
        //    }

        //    // Occupy the space chosen and assign that space to the player who has the turn.
        //    Board.OccupySpace(Board, choiceOfSpaceToOccupy, CurrentTurnPlayer);

        //    // Print current state of the board
        //    Board.PrintBoard();

        //    // If there is a winner or draw
        //    GameResult gameResult = Board.GetResultFromBoard(Board);
        //    bool gameIsOver = (gameResult != GameResult.Incomplete);
        //    if (gameIsOver)
        //    {
        //        // Update state.
        //        Board.Result = gameResult;
        //        UpdateScores();

        //        // Display winner and scores.
        //        GameResult lastResult = resultHistory[resultHistory.Count - 1];
        //        DisplayWinner(lastResult);
        //        DisplayPlayersScore();

        //        // Restart game if user chooses to.
        //        if (PromptPlayAgain())
        //        {
        //            RestartGame();
        //        }
        //    }
        //    else
        //    {
        //        DetermineTurn();
        //    }
        //}

        ///// <summary>
        ///// Updates the state of the scores and result history. Should be called when a game is over (winner or tie).
        ///// </summary>
        ///// <exception cref="Exception">Thrown if trying to update the score when the game is incomplete or if the game determined a winner but the winning shape is not X or O.</exception>
        //public void UpdateScores()
        //{
        //    GameResult gameResult = Board.Result;

        //    if (gameResult == GameResult.Incomplete)
        //    {
        //        throw new Exception($"Scores can't be updated because game is incomplete.");
        //    }

        //    bool hasWinner = (Board.Result == GameResult.WinnerX) || (Board.Result == GameResult.WinnerO);
        //    bool hasTie = Board.Result == GameResult.Tie;

        //    if (hasWinner)
        //    {
        //        Player winnerPlayer;

        //        // Determine winning Player
        //        if (gameResult == GameResult.WinnerX)
        //        {
        //            winnerPlayer = GetPlayerFromShape(Shape.X);
        //        }
        //        else if (gameResult == GameResult.WinnerO)
        //        {
        //            winnerPlayer = GetPlayerFromShape(Shape.O);
        //        }
        //        else
        //        {
        //            throw new Exception($"No winner player can be determined from result \"{gameResult}\"");
        //        }

        //        // Update winning player scores
        //        if (winnerPlayer == userPlayer)
        //        {
        //            userScore++;
        //        }
        //        else if (winnerPlayer == botPlayer)
        //        {
        //            botScore++;
        //        }
        //    }
        //    // Update tie score
        //    else if (hasTie)
        //    {
        //        tieScore++;
        //    }

        //    // Add the result to the result history.
        //    resultHistory.Add(gameResult);
        //}

        ///// <summary>
        ///// Resets the game to play a new round.
        ///// </summary>
        //public void RestartGame()
        //{
        //    Board = new Board();

        //    DetermineTurn();

        //    turnCount = 0;

        //    foreach (Player player in players)
        //    {
        //        player.ResetOccupiedSpaces();
        //    }

        //    NewGame();
        //}

        ///// <summary>
        ///// Prompts the user to pick a side (either "X" or "O"). Keeps prompting until the input is valid. Returns the Shape chosen. 
        ///// </summary>
        ///// <returns>The Shape chosen by the user.</returns>
        //public Shape GetUserShapeChoice()
        //{
        //    Shape userShapeChoice = Shape.None;
        //    while (userShapeChoice == Shape.None)
        //    {
        //        Console.Write("Choose a side. Enter \"X\" or \"O\": ");
        //        string userInput = Console.ReadLine().ToUpper();

        //        // Check if the given user input matches a Shape.
        //        if (Enum.IsDefined(typeof(Shape), userInput))
        //        {
        //            // Convert the userInput string into its equivalent Shape.
        //            userShapeChoice = (Shape)Enum.Parse(typeof(Shape), userInput);
        //        }
        //        else
        //        {
        //            Console.WriteLine("Invalid side. Please try again.\n");
        //        }
        //    }

        //    Console.WriteLine();

        //    return userShapeChoice;
        //}

        ///// <summary>
        ///// Prompts the user to pick a space of the board to place its shape. Returns the chosen space.
        ///// </summary>
        ///// <returns>The space chosen by the user to place its shape.</returns>
        ///// <exception cref="FormatException">Thrown when the string given by the user is not an integer from 1 to 9 inclusive.</exception>
        ///// <exception cref="ArgumentOutOfRangeException">Thrown if the integer position is out of range for the board's spaces.</exception>
        ///// <exception cref="InvalidOperationException">Thrown if space corresponding to the user input is already taken.</exception>
        //public Space PromptPickSpaceToOccupy()
        //{
        //    Space chosenSpace = null;
        //    bool spaceIsValid = false;

        //    do
        //    {
        //        Console.Write($"Choose a space (1-9): ");
        //        string spaceInput = Console.ReadLine();

        //        try
        //        {
        //            // Validate that user input is an integer.
        //            int spaceNumber;
        //            bool userInputIsInt = int.TryParse(spaceInput, out spaceNumber);
        //            if (!userInputIsInt)
        //                throw new FormatException($"Invalid user input. The value entered must be a number between 1 and 9.");

        //            // Validate that the integer is between the board's range and take the corresponding space.
        //            chosenSpace = Board.GetBoardSpaceFromInt(spaceNumber);

        //            // Validate if chosen space is already taken.
        //            if (chosenSpace.IsOccupied())
        //                throw new InvalidOperationException("The chosen space is taken.");

        //            spaceIsValid = true;
        //        }
        //        catch (Exception ex)
        //        {
        //            if (ex is FormatException || ex is ArgumentOutOfRangeException)
        //            {
        //                Console.WriteLine($"Invalid input \"{spaceInput}\". Please enter a number between 1 and 9.\n");
        //            }
        //            else if (ex is InvalidOperationException)
        //            {
        //                Console.WriteLine($"The chosen space is taken. Please choose another one.");
        //            }
        //        }
        //    } while (!spaceIsValid);

        //    return chosenSpace;
        //}

        ///// <summary>
        ///// Prompts the user to play a new round of the game. Returns true is the user agrees; otherwise, false.
        ///// </summary>
        ///// <returns>True is the user wants to play a new round of the game; otherwise, false.</returns>
        //public bool PromptPlayAgain()
        //{
        //    bool playAgain = false;

        //    do
        //    {
        //        Console.Write("Play again? (Y/n): ");
        //        string userInput = Console.ReadLine();

        //        if (userInput.ToLower() == "y" || userInput.ToLower() == "")
        //        {
        //            playAgain = true;
        //            break;
        //        }
        //        else if (userInput.ToLower() == "n")
        //        {
        //            break;
        //        }
        //        else
        //        {
        //            Console.WriteLine("Invalid input.");
        //        }
        //    } while (true);

        //    Console.WriteLine();

        //    return playAgain;
        //}

        ///// <summary>
        ///// Prints a message declaring the winner or a tie.
        ///// </summary>
        ///// <param name="gameResult">The result of a finished game.</param>
        ///// <exception cref="Exception">Thrown when given a result where the game is still incomplete.</exception>
        //public void DisplayWinner(GameResult gameResult)
        //{
        //    if (gameResult == GameResult.Incomplete)
        //    {
        //        throw new Exception("Not possible to display the result of an unfinished game.");
        //    }

        //    string declareWinnerMessage = "";

        //    declareWinnerMessage += "\n############\n";

        //    if (gameResult == GameResult.WinnerX || gameResult == GameResult.WinnerO)
        //    {
        //        Player winner = GetWinningPlayerFromResult(gameResult);
        //        declareWinnerMessage += $"{winner.Name} ({winner.Shape}) won!";
        //    }
        //    else if (gameResult == GameResult.Tie)
        //    {
        //        declareWinnerMessage += $"It's a tie!";
        //    }

        //    declareWinnerMessage += "\n############\n";

        //    Console.WriteLine(declareWinnerMessage);
        //}

        ///// <summary>
        ///// Returns the player that matches the specified shape.
        ///// </summary>
        ///// <param name="shape">The shape that the returned player should have assigned.</param>
        ///// <returns>The player that matches the specified shape.</returns>
        ///// <exception cref="Exception">Thrown if no player with the specified shape is found.</exception>
        //public Player GetPlayerFromShape(Shape shape)
        //{
        //    foreach (Player player in players)
        //    {
        //        if (player.Shape == shape)
        //        {
        //            return player;
        //        }
        //    }

        //    throw new Exception($"No player was found with the shape {shape}.");
        //}

        ///// <summary>
        ///// Returns the player instance who has the shape that won the given board.
        ///// </summary>
        ///// <param name="result">The game result of a finished game.</param>
        ///// <returns>The player instance who has the shape that won the given board.</returns>
        ///// <exception cref="Exception">Thrown if the provided result was not a winner (for example, if given a tie or incomplete result).</exception>
        //public Player GetWinningPlayerFromResult(GameResult result)
        //{
        //    Player player;

        //    if (result == GameResult.WinnerX)
        //    {
        //        player = GetPlayerFromShape(Shape.X);
        //    }
        //    else if (result == GameResult.WinnerO)
        //    {
        //        player = GetPlayerFromShape(Shape.O);
        //    }
        //    else
        //    {
        //        throw new Exception($"No player can be determined from the result \"{result}\".");
        //    }

        //    return player;
        //}

        ///// <summary>
        ///// Returns a string representation of the game's state.
        ///// </summary>
        ///// <returns>A string representation of the game's state.</returns>
        //public override string ToString()
        //{
        //    string template = "";
        //    template += $"--Game--\n\n";

        //    template += $"-Game info-\n";
        //    template += $"countOfTurns: {turnCount}\n";
        //    template += $"nextAssignableId: {nextAssignableId}\n";
        //    template += $"currentTurnPlayer: {CurrentTurnPlayer.Identifier}\n";
        //    template += $"GetCurrentTurnPlayer(): {CurrentTurnPlayer.Identifier}\n";
        //    template += $"GetNotCurrentTurnPlayer(): {NotCurrentTurnPlayer.Identifier}\n";

        //    template += $"\n\n-Player info-\n";
        //    template += $"Player count: {players.Length}\n\n";

        //    template += $"User player info:\n{GetUserPlayer().ToString()}\n";
        //    template += $"Bot player info:\n{GetBotPlayer().ToString()}\n";

        //    return template;
        //}
        #endregion
    }
}
