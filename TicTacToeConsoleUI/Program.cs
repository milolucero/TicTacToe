using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeLibrary;

namespace TicTacToeConsoleUI
{
    public class Program
    {
        static void Main(string[] args)
        {
            PrintProgramInformation(
                programName: "Tic-Tac-Toe",
                programVersion: "2.0.0",
                authorName: "Camilo Lucero",
                authorEmail: "cluceroespitia@rrc.ca");

            bool playAgain = false;

            do
            {
                (Shape userShapeChoice, DifficultyLevel difficultyLevel) = PromptInitialSettings();
                
                Game game = new Game(userShapeChoice, difficultyLevel);

                StartGame(game);

                //playAgain = PromptPlayAgain();

                Console.ReadLine();
            } while (playAgain);

            // Test.RunTests();
        }

        /// <summary>
        /// Prompts the user to make the initial game setup.
        /// </summary>
        /// <returns>A tuple containing the user shape and level of difficulty chosen by the user.</returns>
        private static (Shape userShapeChoice, DifficultyLevel difficultyLevel) PromptInitialSettings()
        {
            Shape userShapeChoice = PromptUserShapeChoice();

            DifficultyLevel difficultyLevel = PromptLevelOfDifficulty();

            Console.WriteLine($"\nUser shape: {userShapeChoice}\nDifficulty level: {difficultyLevel}\n");

            return (userShapeChoice, difficultyLevel);
        }

        /// <summary>
        /// Starts a new game.
        /// </summary>
        /// <param name="game">The instance of the game.</param>
        private static void StartGame(Game game)
        {
            while (game.Board.Result == GameResult.Incomplete)
            {
                PrintBoard(game);

                DisplayTurn(game);

                MakeMove(game);
            }
        }

        /// <summary>
        /// Prompts the user to make a move or makes the bot make a move according to who has the turn to play.
        /// </summary>
        /// <param name="game">The instance of the game.</param>
        private static void MakeMove(Game game)
        {
            (int x, int y) moveCoordinates;

            // If user has the turn, prompt user to choose a space.
            bool userHasTheTurn = game.CurrentTurnShape() == game.UserShape;

            if (userHasTheTurn)
            {
                moveCoordinates = PromptUserForMove();
            }
            else
            {
                // Get AI move according to level of difficulty and board state.
                // moveCoordinates = 
            }
        }

        /// <summary>
        /// Prompts the user to pick a space to make a move.
        /// </summary>
        /// <returns>The board coordinate of the space chosen by the user.</returns>
        private static (int x, int y) PromptUserForMove()
        {
            string userInput;
            int userSpaceChoiceInt;
            bool inputIsInt;
            bool inputIsInRange;
            bool userInputIsValid = false;
            (int, int) boardCoordinate = (-1, -1);

            do
            {
                // Get user input
                Console.Write("\nChoose a space (1-9): ");
                userInput = Console.ReadLine();
                Console.WriteLine();

                // Validate input
                inputIsInt = int.TryParse(userInput, out userSpaceChoiceInt);

                if (!inputIsInt)
                {
                    Console.WriteLine($"\"{userInput}\" is not a number. Please enter a number between 1 and 9.");
                    continue;
                }

                inputIsInRange = userSpaceChoiceInt >= 1 && userSpaceChoiceInt <= 9;

                if (!inputIsInRange)
                {
                    Console.WriteLine($"The number {userSpaceChoiceInt} is not valid. Please enter a number between 1 and 9.");
                    continue;
                }

                // Convert input into a coordinate of the board
                boardCoordinate = GetBoardCoordinateFromInt(userSpaceChoiceInt);

                // Check if space is taken
                //if (spaceIsTaken)
                //{
                //    Console.WriteLine($"Space number {userSpaceChoiceInt} is already taken by {}. Please choose another one.");
                //}

                userInputIsValid = true;
            } while (!userInputIsValid);

            return boardCoordinate;
        }

        /// <summary>
        /// Takes a number between 1 and 9, returns the X and Y coordinate that the given number would represent on the board (left to right, bottom to top).
        /// </summary>
        /// <param name="position">A number between 1 and 9 representing a position on the board.</param>
        /// <returns>The (X, Y) coordinate of the given board position.</returns>
        private static (int x, int y) GetBoardCoordinateFromInt(int position)
        {
            // Using integer division to floor the result.
            int x = (position - 1) / 3;
            int y = (position - 1) % 3;

            return (x, y);
        }

        /// <summary>
        /// Prints a message to display who has the turn to play.
        /// </summary>
        /// <param name="game">The instance of the game.</param>
        private static void DisplayTurn(Game game)
        {
            Shape currentTurnShape = game.CurrentTurnShape();
            Shape userShape = game.UserShape;

            string currentTurnPlayer = currentTurnShape == userShape ? "You" : "Bot";

            string message = $"Turn: {currentTurnPlayer} ({currentTurnShape})";

            Console.WriteLine(message);
        }

        /// <summary>
        /// Prints a representation of the current board of the game.
        /// </summary>
        /// <param name="game"></param>
        private static void PrintBoard(Game game)
        {
            string template = "";
            Board board = game.Board;


            string[] shapes = new string[board.Spaces.Length];

            for (int i = 0; i < board.Spaces.Length; i++)
            {
                if (board.Spaces[i].Occupant is Shape.None)
                {
                    shapes[i] = " ";
                }
                else
                {
                    shapes[i] = board.Spaces[i].Occupant.ToString();
                }
            }

            template += $"   |   |   \n";
            template += $" {shapes[6]} | {shapes[7]} | {shapes[8]} \n";
            template += $"___|___|___\n";
            template += $"   |   |   \n";
            template += $" {shapes[3]} | {shapes[4]} | {shapes[5]} \n";
            template += $"___|___|___\n";
            template += $"   |   |   \n";
            template += $" {shapes[0]} | {shapes[1]} | {shapes[2]} \n";
            template += $"   |   |   \n";

            Console.WriteLine(template);
        }

        /// <summary>
        /// Displays a message with information of the program and author.
        /// </summary>
        /// <param name="programName">The name of the program.</param>
        /// <param name="programVersion">The version of the program.</param>
        /// <param name="authorName">The name of the author.</param>
        /// <param name="authorEmail">The email of the author.</param>
        public static void PrintProgramInformation(string programName, string programVersion, string authorName, string authorEmail)
        {
            string message = "";
            message += $"{programName} v{programVersion}\n";
            message += $"Author: {authorName}\n";
            message += $"Email: {authorEmail}\n";

            Console.WriteLine(message);
        }
        
        /// <summary>
        /// Prompts the user to pick a side (either "X" or "O"). Keeps prompting until the input is valid. Returns the Shape chosen. 
        /// </summary>
        /// <returns>The Shape chosen by the user.</returns>
        public static Shape PromptUserShapeChoice()
        {
            Shape userShapeChoice = Shape.None;
            while (userShapeChoice == Shape.None)
            {
                Console.Write("\nChoose a side. Enter \"X\" or \"O\": ");
                string userInput = Console.ReadLine().ToUpper();

                // Check if the given user input matches a Shape.
                if (Enum.IsDefined(typeof(Shape), userInput))
                {
                    // Convert the userInput string into its equivalent Shape.
                    userShapeChoice = (Shape)Enum.Parse(typeof(Shape), userInput);
                }
                else
                {
                    Console.WriteLine("Invalid side. Please try again.\n");
                }
            }

            return userShapeChoice;
        }

        /// <summary>
        /// Prompts the user to pick a level of difficulty. Keeps prompting until the input is valid. Returns the DifficultyLevel chosen.
        /// </summary>
        /// <returns>The DifficultyLevel chosen.</returns>
        private static DifficultyLevel PromptLevelOfDifficulty()
        {
            string userInput;
            int userNumber;
            bool userInputIsInt;
            bool userInputIsValidDifficultyLevel = false;

            do
            {
                // Prompt for user input
                Console.Write("\nChoose a number to pick the level of difficulty:\n" +
                    "1 - Easy\n" +
                    "2 - Medium\n" +
                    "3 - Hard\n\n");

                userInput = Console.ReadLine();

                // Validate input
                userInputIsInt = int.TryParse(userInput, out userNumber);

                if (userInputIsInt)
                {
                    userInputIsValidDifficultyLevel = Enum.IsDefined(typeof(DifficultyLevel), userNumber);

                    if (!userInputIsValidDifficultyLevel)
                    {
                        Console.WriteLine($"\"{userInput}\" is not a valid level of difficulty. Please enter a number between 1 and 3.");
                    }
                }
                else
                {
                    Console.WriteLine($"\"{userInput}\" is not a number. Please enter a number between 1 and 3.");
                }

            } while(!userInputIsValidDifficultyLevel);

            return (DifficultyLevel)userNumber;
        }

        /// <summary>
        /// Prompts the user to play again. Keeps prompting until the input is valid.
        /// </summary>
        /// <returns>True if the user wants to play again; otherwise, false.</returns>
        private static bool PromptPlayAgain()
        {
            throw new NotImplementedException();
        }
    }
}
